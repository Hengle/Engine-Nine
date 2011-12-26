﻿#region Copyright 2009 - 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
#endregion

namespace Nine.Graphics.ParticleEffects
{
    /// <summary>
    /// Defines how each particle should be rendered.
    /// </summary>
    public enum ParticleType
    {
        /// <summary>
        /// The particle will be rendered as 3D billboard that always faces the camera.
        /// </summary>
        Billboard,

        /// <summary>
        /// The particle will be rendered as 3D constrained billboard that is constrained
        /// by the forward moving axis while still faces the camera.
        /// </summary>
        ConstrainedBillboard,

        /// <summary>
        /// The particle will be rendered as 3D constrained billboard that is constrained
        /// by the specified axis while still faces the camera.
        /// </summary>
        ConstrainedBillboardUp,

        /// <summary>
        /// The particle will be rendered as 3D ribbon trail.
        /// </summary>
        [Obsolete("This behavior has not been implemented")]
        RibbonTrail,
    }

    /// <summary>
    /// Action for particles.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public delegate void ParticleAction(ref Particle particle);

    /// <summary>
    /// Defines a special visual effect made up of particles.
    /// </summary>
    [ContentSerializable]
    public class ParticleEffect : IDisposable
    {
        /// <summary>
        /// Gets or sets the type of each particle.
        /// </summary>
        public ParticleType ParticleType { get; set; }

        /// <summary>
        /// Gets or sets whether this particle effect is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a scale factor along the forward axis when drawing this
        /// particle effect using constrained billboard.
        /// </summary>
        public float Stretch { get; set; }

        /// <summary>
        /// Gets or sets the up axis of each particle.
        /// </summary>
        public Vector3 Up { get; set; }

        /// <summary>
        /// Gets or sets the texture used by this particle effect.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Gets or sets the source rectangle in the texture.
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        /// <summary>
        /// Gets or sets the texture and source rectangle using <see cref="TextureList"/>.
        /// </summary>
        /// <remarks>
        /// You can bind the TextureList property with a <see cref="SpriteAnimation"/>.
        /// </remarks>
        [ContentSerializerIgnore]
        public TextureListItem TextureList
        {
            get { return new TextureListItem(Texture, SourceRectangle); }
            set
            {
                Texture = value.Texture;
                SourceRectangle = value.SourceRectangle;
            }
        }

        /// <summary>
        /// Gets or sets the blend state between each particles of this particle effect.
        /// </summary>
        public BlendState BlendState { get; set; }

        /// <summary>
        /// Gets or sets the blend state used to blend this particle effect with the background.
        /// Specify null to use the current <c>BlendState</c>.
        /// </summary>
        [ContentSerializerIgnore]
        [Obsolete("This behavior has not been implemented")]
        public BlendState BackgroundBlendState { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether two pass rendering technique is used to sort each particle based on depth.
        /// The default value is false. This flag is only available when you are drawing using ParticleBatch.
        /// </summary>
        /// <remarks>
        /// When depth sort is enabled, particles are not sorted based on their distance to the camera. Instead, a two pass
        /// rendering technique is used to eliminate depth order problems.
        /// During the first pass, depth stencial state is set to Default and alpha test is turned on, so the opaque part of the
        /// particles are ordered using the depth buffer.
        /// The second pass draws the particles with depth stencial state set to DepthRead and alpha blend is turned on, so the
        /// transparent part of the particles are rendered.
        /// </remarks>
        public bool DepthSortEnabled { get; set; }

        /// <summary>
        /// Gets or sets the reference alpha value used in two pass rendering.
        /// </summary>
        public int ReferenceAlpha { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether particles should softly blends with other scene objects.
        /// </summary>
        public bool SoftParticleEnabled { get; set; }

        /// <summary>
        /// Gets or sets the emitter of this particle effect.
        /// </summary>
        public IParticleEmitter Emitter
        {
            get { return emitter ?? (emitter = new PointEmitter()); }
            set { emitter = value; }
        }
        IParticleEmitter emitter;

        /// <summary>
        /// Gets a collection of controllers that defines the visual of this particle effect.
        /// </summary>
        public ParticleControllerCollection Controllers { get; private set; }

        /// <summary>
        /// Gets a collection of particle effects that is used as the appareance of each
        /// particle spawned by this particle effect.
        /// </summary>
        public ParticleEffectCollection ChildEffects { get; private set; }

        /// <summary>
        /// Gets a collection of particle effects that is fired when each particle spawned
        /// by this particle effect is about to die.
        /// </summary>
        public ParticleEffectCollection EndingEffects { get; private set; }

        /// <summary>
        /// Gets or sets user data.
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// Gets the approximate particle count.
        /// </summary>
        public int ParticleCount { get; private set; }
        
        /// <summary>
        /// Gets a list of triggers owned by this <c>ParticleEffect</c>.
        /// </summary>
        [ContentSerializerIgnore]
        public IList<IParticleEmitter> ActiveEmitters { get; private set; }

        /// <summary>
        /// Occurs when a particle is about to die.
        /// </summary>
        public event ParticleAction ParticleEmitted;

        /// <summary>
        /// Occurs when a particle is about to die.
        /// </summary>
        public event ParticleAction ParticleRetired;

        // An array of particles, treated as a circular queue.
        internal int MaxParticleCount = 1024;
        internal int CurrentParticle = 0;

        private Particle[] particles;
        private int firstParticle = 0;
        private int lastParticle = 0;

        private int CurrentController = 0;
        private float elapsedSeconds = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;
        private Random random = new Random();

        private ParticleAction cachedEmit;
        private ParticleAction cachedForEachAction;
        private ParticleAction cachedForEachDelegate;
        private ParticleAction cachedUpdateControllersDelegate;
        private ParticleAction cachedUpdateParticlesDelegate;

        /// <summary>
        /// Creates a new particle effect.
        /// </summary>
        public ParticleEffect() : this(32) 
        {

        }

        /// <summary>
        /// Creates a new particle effect.
        /// </summary>
        public ParticleEffect(int capacity)
        {
            this.MaxParticleCount = capacity;
            this.particles = new Particle[MaxParticleCount];

            this.Up = Vector3.UnitZ;
            this.Enabled = true;
            this.Stretch = 1;
            this.ReferenceAlpha = 128;
            this.BackgroundBlendState = null;
            this.BlendState = BlendState.Additive;
            this.SoftParticleEnabled = false;
            this.Emitter = new PointEmitter();
            this.ActiveEmitters = new List<IParticleEmitter>();

            this.Controllers = new ParticleControllerCollection();
            this.Controllers.ParticleEffect = this;

            this.ChildEffects = new ParticleEffectCollection();
            this.EndingEffects = new ParticleEffectCollection();

            this.cachedEmit = new ParticleAction(EmitNewParticle);
            this.cachedForEachDelegate = new ParticleAction(ForEachDelegateMethod);
            this.cachedUpdateControllersDelegate = new ParticleAction(UpdateControllersDelegateMethod);
            this.cachedUpdateParticlesDelegate = new ParticleAction(UpdateParticlesDelegateMethod);
        }

        /// <summary>
        /// Creates a merged effect from serveral input particle effects.
        /// See http://nine.codeplex.com/discussions/272121
        /// </summary>
        public static ParticleEffect CreateMerged(IEnumerable<ParticleEffect> effects)
        {
            if (effects == null)
                throw new ArgumentNullException("effects");

            var root = new ParticleEffect(4) 
            {
                Emitter = new PointEmitter { EmitCount = 1, Duration = float.MaxValue } 
            };

            foreach (var effect in effects)
            {
                if (effect != null)
                    root.ChildEffects.Add(effect);
            }
            return root;
        }

        /// <summary>
        /// Creates an instance of the particle effect.
        /// </summary>
        public IParticleEmitter Trigger()
        {
            var result = Emitter.Clone();
            ActiveEmitters.Add(result);
            return result;
        }

        /// <summary>
        /// Creates an instance of the particle effect at the specified position.
        /// </summary>
        public IParticleEmitter Trigger(Vector3 position)
        {
            var result = Emitter.Clone();
            result.Position = position;
            ActiveEmitters.Add(result);
            return result;
        }

        /// <summary>
        /// Creates an instance of the particle effect at the specified position.
        /// </summary>
        public IParticleEmitter Trigger(Vector3 position, TimeSpan lifetime)
        {
            var result = Emitter.Clone() as ParticleEmitter;
            if (result == null)
                throw new InvalidOperationException("Target must be an instance of ParticleEmitter");

            result.Position = position;
            result.Lifetime = lifetime;
            ActiveEmitters.Add(result);
            return result;
        }

        /// <summary>
        /// Creates an instance of the particle effect at the specified position.
        /// </summary>
        public IParticleEmitter Trigger(Vector3 position, int emitCount)
        {
            var result = Emitter.Clone() as ParticleEmitter;
            if (result == null)
                throw new InvalidOperationException("Target must be an instance of ParticleEmitter");

            result.Position = position;
            result.EmitCount = emitCount;
            ActiveEmitters.Add(result);
            return result;
        }

        /// <summary>
        /// Traverses all active particles.
        /// </summary>
        public void ForEach(ParticleAction action)
        {
            cachedForEachAction = action;
            ForEachInternal(cachedForEachDelegate);
            cachedForEachAction = null;
        }

        private void ForEachDelegateMethod(ref Particle particle)
        {
            if (particle.Age <= 1)
                cachedForEachAction(ref particles[CurrentParticle]);
        }

        private void ForEachInternal(ParticleAction action)
        {
            if (ParticleCount > 0)
            {
                if (firstParticle < lastParticle)
                {
                    // ParticleConstroller<T>.Update requires the CurrentParticle to be the correct index.
                    for (CurrentParticle = firstParticle; CurrentParticle < lastParticle; CurrentParticle++)
                        action(ref particles[CurrentParticle]);
                }
                else
                {
                    // UpdateParticles requires the enumeration to always start from firstParticle.
                    for (CurrentParticle = firstParticle; CurrentParticle < MaxParticleCount; CurrentParticle++)
                        action(ref particles[CurrentParticle]);
                    for (CurrentParticle = 0; CurrentParticle < lastParticle; CurrentParticle++)
                        action(ref particles[CurrentParticle]);
                }
            }
        }

        /// <summary>
        /// Updates the particle system.
        /// </summary>
        public void Update(TimeSpan elapsedTime)
        {
            Update(elapsedTime, false);
        }

        private void Update(TimeSpan elapsedTime, bool supressEmitters)
        {
            float elapsedSeconds = (float)elapsedTime.TotalSeconds;
            
            if (!supressEmitters)
                UpdateEmitters(elapsedSeconds);
            UpdateControllers(elapsedSeconds);
            UpdateParticles(elapsedTime);
            RetireParticles();

            for (int i = 0; i < ChildEffects.Count; i++)
            {
                ChildEffects[i].Update(elapsedTime, true);
            }

            for (int i = 0; i < EndingEffects.Count; i++)
            {
                EndingEffects[i].Update(elapsedTime);
            }
        }

        private void UpdateEmitters(float elapsedSeconds)
        {
            if (!Enabled)
                return;

            for (int i = 0; i < ActiveEmitters.Count; i++)
            {
                if (ActiveEmitters[i].Update(elapsedSeconds, cachedEmit))
                {
                    ActiveEmitters.RemoveAt(i);
                    i--;
                    continue;
                }
            }
        }

        private void EmitNewParticle(ref Particle particle)
        {
            CurrentParticle = lastParticle;

            particle.Age = 0;
            particle.ElapsedTime = 0;

            particles[CurrentParticle] = particle;

            for (int currentController = 0; currentController < Controllers.Count; currentController++)
            {
                Controllers[currentController].Reset(ref particle);
            }

            ParticleCount++;

            if (ParticleEmitted != null)
                ParticleEmitted(ref particles[CurrentParticle]);

            // Expand storage when the queue is full.
            if (ParticleCount >= MaxParticleCount)
            {
                var currentLength = MaxParticleCount;
                Array.Resize(ref particles, MaxParticleCount = MaxParticleCount * 2);
                if (lastParticle < firstParticle)
                {
                    currentLength -= firstParticle;
                    Array.Copy(particles, firstParticle, particles, MaxParticleCount - currentLength, currentLength);
                    firstParticle = MaxParticleCount - currentLength;
                }
            }

            lastParticle = (lastParticle + 1) % MaxParticleCount;
        }
        
        private void UpdateControllers(float elapsedTime)
        {   
            if (ParticleCount <= 0)
                return;

            this.elapsedSeconds = elapsedTime;
            for (CurrentController = 0; CurrentController < Controllers.Count; CurrentController++)
            {                
                ForEachInternal(cachedUpdateControllersDelegate);
            }
        }

        private void UpdateControllersDelegateMethod(ref Particle particle)
        {
            Controllers[CurrentController].Update(elapsedSeconds, ref particle);
        }

        private void UpdateParticles(TimeSpan elapsedTime)
        {
            this.elapsedTime = elapsedTime;
            this.elapsedSeconds = (float)(elapsedTime.TotalSeconds);

            ForEachInternal(cachedUpdateParticlesDelegate);
        }

        private void UpdateParticlesDelegateMethod(ref Particle particle)
        {
            particle.Update(elapsedSeconds);

            for (int i = 0; i < ChildEffects.Count; i++)
            {
                var childEffect = ChildEffects[i];
                
                if (childEffect.ActiveEmitters.Count > 1)
                    throw new InvalidOperationException();
                
                if (childEffect.ActiveEmitters.Count <= 0)
                    childEffect.Trigger();

                childEffect.ActiveEmitters[0].Position = particle.Position;
                childEffect.UpdateEmitters(elapsedSeconds);
            }

            if (particle.Age > 1 && particle.Age < float.MaxValue)
            {
                if (EndingEffects.Count > 0)
                {
                    for (int i = 0; i < EndingEffects.Count; i++)
                        EndingEffects[i].Trigger(particle.Position);
                }

                if (ParticleRetired != null)
                {
                    ParticleRetired(ref particle);
                }

                particle.Age = float.MaxValue;
            }
        }

        private void RetireParticles()
        {
            while (ParticleCount > 0 && particles[firstParticle].Age > 1)
            {
                firstParticle = (firstParticle + 1) % MaxParticleCount;
                ParticleCount--;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        ~ParticleEffect()
        {
            Dispose(false);
        }
    }
}
