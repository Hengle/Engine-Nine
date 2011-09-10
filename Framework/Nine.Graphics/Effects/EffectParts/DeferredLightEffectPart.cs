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

    class DeferredLightsEffectPart : LinkedEffectPart, IEffectMaterial, IEffectTexture
    {
        private uint DirtyMask = 0;
        
        private EffectParameter halfPixelParameter;

        private Texture lightTexture;
        private EffectParameter lightTextureParameter;
        private const uint lightTextureDirtyMask = 1 << 1;

        private Vector3 diffuseColor;
        private EffectParameter diffuseColorParameter;
        private const uint diffuseColorDirtyMask = 1 << 2;

        private Vector3 emissiveColor;
        private EffectParameter emissiveColorParameter;
        private const uint emissiveColorDirtyMask = 1 << 3;

        private Vector3 specularColor;
        private EffectParameter specularColorParameter;
        private const uint specularColorDirtyMask = 1 << 4;

        [ContentSerializer(Optional = true)]
        public Vector3 DiffuseColor
        {
            get { return diffuseColor; }
            set { diffuseColor = value; DirtyMask |= diffuseColorDirtyMask; }
        }

        [ContentSerializer(Optional = true)]
        public Vector3 EmissiveColor
        {
            get { return emissiveColor; }
            set { emissiveColor = value; DirtyMask |= emissiveColorDirtyMask; }
        }

        [ContentSerializer(Optional = true)]
        public Vector3 SpecularColor
        {
            get { return specularColor; }
            set { specularColor = value; DirtyMask |= specularColorDirtyMask; }
        }

        [ContentSerializerIgnore]
        public Texture LightTexture
        {
            get { return lightTexture; }
            set { if (lightTexture != value) { lightTexture = value; DirtyMask |= lightTextureDirtyMask; } }
        }

        float IEffectMaterial.Alpha
        {
            get { return 1; }
            set { }
        }

        float IEffectMaterial.SpecularPower
        {
            get { return 16; }
            set { }
        }

        Texture2D IEffectTexture.Texture
        {
            get { return null; }
            set { }
        }

        public override bool IsMaterial { get { return true; } }

        void IEffectTexture.SetTexture(TextureUsage usage, Texture texture)
        {
            if (usage == TextureUsage.LightBuffer)
                LightTexture = texture as Texture2D;
        }

        protected internal override void OnApply()
        {
            if ((DirtyMask & diffuseColorDirtyMask) != 0)
            {
                if (diffuseColorParameter == null)
                    diffuseColorParameter = GetParameter("DiffuseColor");
                diffuseColorParameter.SetValue(diffuseColor);
                DirtyMask &= ~diffuseColorDirtyMask;
            }

            if ((DirtyMask & emissiveColorDirtyMask) != 0)
            {
                if (emissiveColorParameter == null)
                    emissiveColorParameter = GetParameter("EmissiveColor");
                emissiveColorParameter.SetValue(emissiveColor);
                DirtyMask &= ~emissiveColorDirtyMask;
            }

            if ((DirtyMask & specularColorDirtyMask) != 0)
            {
                if (specularColorParameter == null)
                    specularColorParameter = GetParameter("SpecularColor");
                specularColorParameter.SetValue(specularColor);
                DirtyMask &= ~specularColorDirtyMask;
            }

            if ((DirtyMask & lightTextureDirtyMask) != 0)
            {
                if (lightTextureParameter == null)
                    lightTextureParameter = GetParameter("LightTexture");
                lightTextureParameter.SetValue(lightTexture);
                DirtyMask &= ~lightTextureDirtyMask;
            }
            
            if (halfPixelParameter == null)
                halfPixelParameter = GetParameter("halfPixel");
            halfPixelParameter.SetValue(new Vector2(0.5f / GraphicsDevice.Viewport.Width, 0.5f / GraphicsDevice.Viewport.Height));
        }

        protected internal override void OnApply(LinkedEffectPart part)
        {
            var effectPart = (DeferredLightsEffectPart)part;
            effectPart.DiffuseColor = DiffuseColor;
            effectPart.EmissiveColor = EmissiveColor;
            effectPart.SpecularColor = SpecularColor;
            effectPart.LightTexture = LightTexture;
        }

        public DeferredLightsEffectPart()
        {
            DiffuseColor = Vector3.One;
            EmissiveColor = Vector3.Zero;
            SpecularColor = Vector3.Zero;            
        }

        protected internal override LinkedEffectPart Clone()
        {
            return new DeferredLightsEffectPart()
            {
                DiffuseColor = this.DiffuseColor,
                EmissiveColor = this.EmissiveColor,
                SpecularColor = this.SpecularColor,
                LightTexture = this.LightTexture,
            };
        }
    }

#endif
}