﻿#region Copyright 2009 - 2011 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2011 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Nine.Graphics.ParticleEffects
{
    using Nine.Animations;

    /// <summary>
    /// Represents a triggered animation instance of the particle effect.
    /// </summary>
    public class ParticleAnimation : Animation
    {
        /// <summary>
        /// Gets or sets whether this particle effect is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the position of this particle effect.
        /// </summary>
        public Vector3 Position { get; set; }

        /// <summary>
        /// Gets or sets the duration of this animation.
        /// </summary>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets the approximate bounds of this particle effect trigger.
        /// </summary>
        public BoundingBox BoundingBox
        {
            get
            {
                if (Effect.Emitter == null)
                    return new BoundingBox();

                // TODO: Should cache this value
                BoundingBox box = Effect.Emitter.BoundingBox;

                Vector3 maxBorder = Vector3.Zero;
                for (int currentController = 0; currentController < Effect.Controllers.Count; currentController++)
                {
                    Vector3 border = Effect.Controllers[currentController].Border;

                    if (border.X > maxBorder.X)
                        maxBorder.X = border.X;
                    if (border.Y > maxBorder.Y)
                        maxBorder.Y = border.Y;
                    if (border.Z > maxBorder.Z)
                        maxBorder.Z = border.Z;
                }

                box.Max += maxBorder;
                box.Min -= maxBorder;
                return box;
            }
        }

        /// <summary>
        /// Gets the parent particle effect used by this trigger.
        /// </summary>
        public ParticleEffect Effect { get; internal set; }
        
        public override event EventHandler Completed;

        internal ParticleAnimation() 
        {
            Enabled = true;
            Duration = TimeSpan.MaxValue;
        }

        public override void Update(GameTime gameTime)
        {
            if (Enabled && State == AnimationState.Playing)
            {
                elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
                if (elapsedTime >= Duration.TotalSeconds)
                {
                    Stop();
                    if (Completed != null)
                        Completed(this, EventArgs.Empty);
                }
            }
        }

        protected override void OnStarted()
        {
            elapsedTime = 0;
            base.OnStarted();
        }

        double elapsedTime;
    }
}
