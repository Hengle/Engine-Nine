﻿namespace Nine.Graphics.PostEffects
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Nine.Graphics.Materials;

    /// <summary>
    /// Represents a blur post processing effect.
    /// </summary>
    [ContentSerializable]
    public class BlurEffect : PostEffectChain
    {
        public GraphicsDevice GraphicsDevice { get; private set; }

        public float BlurAmount
        {
            get { return blurAmount; }
            set { blurAmount = value; UpdateBlurAmount(); }
        }
        private float blurAmount = -1;

        List<BlurMaterial> blurs = new List<BlurMaterial>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BlurEffect"/> class.
        /// </summary>
        public BlurEffect(GraphicsDevice graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            GraphicsDevice = graphics;
            BlurAmount = MaterialConstants.BlurAmount;
        }

        private void UpdateBlurAmount()
        {
            // TODO: Use down scale
            var i = 0;
            var leftover = this.blurAmount;
            while (leftover > 0)
            {
                var amount = Math.Min(leftover, BlurMaterial.MaxBlurAmount);
                leftover = Math.Max(leftover - BlurMaterial.MaxBlurAmount, 0);

                if (i * 2 >= blurs.Count)
                {
                    BlurMaterial blurH, blurV;
                    Effects.Add(new PostEffect(blurH = new BlurMaterial(GraphicsDevice) { BlurAmount = amount }));
                    Effects.Add(new PostEffect(blurV = new BlurMaterial(GraphicsDevice) { Direction = MathHelper.PiOver2, BlurAmount = amount }));
                    blurs.Add(blurH);
                    blurs.Add(blurV);
                }
                else
                {
                    Effects[i * 2].Enabled = true;
                    Effects[i * 2 + 1].Enabled = true;
                    ((BlurMaterial)Effects[i * 2].Material).BlurAmount = amount;
                    ((BlurMaterial)Effects[i * 2 + 1].Material).BlurAmount = amount;
                }

                i++;
            }

            while (i * 2 < blurs.Count)
            {
                Effects[i * 2].Enabled = false;
                Effects[i * 2 + 1].Enabled = false;
                i++;
            }
        }

        [ContentSerializerIgnore]
        public override IList<PostEffect> Effects
        {
            get { return base.Effects; }
        }
    }
}