﻿#region Copyright 2009 - 2010 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2010 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Nine.Graphics.Effects.EffectParts
{
#if !WINDOWS_PHONE

    internal class ColorMatrixEffectPart : LinkedEffectPart, IEffectColorMatrix
    {
        private uint DirtyMask = 0;
        
        private Matrix transform;
        private EffectParameter transformParameter;
        private const uint transformDirtyMask = 1 << 0;

        [ContentSerializer(Optional = true)]
        public Matrix ColorMatrix 
        {
            get { return transform; }
            set { transform = value; DirtyMask |= transformDirtyMask; }
        }

        public override bool IsMaterial { get { return true; } }

        public ColorMatrixEffectPart()
        {
            ColorMatrix = Matrix.Identity;
        }

        protected internal override void OnApply()
        {
            if ((DirtyMask & transformDirtyMask) != 0)
            {
                if (transformParameter == null)
                    transformParameter = GetParameter("Transform");
                transformParameter.SetValue(transform);
                DirtyMask &= ~transformDirtyMask;
            }
        }

        protected internal override void OnApply(LinkedEffectPart part)
        {
            var effectPart = (ColorMatrixEffectPart)part;
            effectPart.ColorMatrix = ColorMatrix;
        }

        protected internal override LinkedEffectPart Clone()
        {
            return new ColorMatrixEffectPart()
            {
                ColorMatrix = this.ColorMatrix,
            };
        }
    }

#endif
}
