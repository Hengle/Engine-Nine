// -----------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EffectCustomTool v1.5.1.0.
//     Runtime Version: v4.0.30319
//
//     EffectCustomTool is a part of Engine Nine. (http://nine.codeplex.com)
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// -----------------------------------------------------------------------------

namespace Nine.Graphics.ScreenEffects
{
#if !WINDOWS_PHONE

    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [System.CodeDom.Compiler.GeneratedCode("Nine.Tools.EffectCustomTool", "1.5.1.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    partial class Adoption : Effect
    {
        public Adoption(GraphicsDevice graphics) 
            : base(graphics, graphics.GraphicsProfile == GraphicsProfile.Reach ? ReachEffectCode :
                                                                                 HiDefEffectCode)
        {
            CacheEffectParameters(Parameters);

            OnCreated();
        }

        /// <summary>
        /// Creates a new Adoption by cloning parameter settings from an existing instance.
        /// </summary>
        protected Adoption(Adoption cloneSource) : base(cloneSource)
        {
            CacheEffectParameters(cloneSource.Parameters);

            OnCreated();

            CloneFrom(cloneSource);

            OnClone(cloneSource);
        }

        /// <summary>
        /// Creates a clone of the current Adoption instance.
        /// </summary>
        public override Effect Clone()
        {
            return new Adoption(this);
        }

        protected override void OnApply()
        {
            OnApplyChanges();

            ApplyChanges();

            base.OnApply();
        }

        private void CacheEffectParameters(EffectParameterCollection cloneSource)
        {
            this._deltaTimeParameter = cloneSource["deltaTime"];
            this._SpeedParameter = cloneSource["Speed"];
            this._LastFrameTextureParameter = cloneSource["LastFrameTexture"];
            this._shaderIndexParameter = cloneSource["shaderIndex"];

        }

        #region Dirty Flags

        uint dirtyFlag = 0;

        const uint deltaTimeDirtyFlag = 1 << 0;
        const uint SpeedDirtyFlag = 1 << 1;
        const uint LastFrameTextureDirtyFlag = 1 << 2;
        const uint shaderIndexDirtyFlag = 1 << 3;

        #endregion

        #region Properties

        private float _deltaTime;
        private EffectParameter _deltaTimeParameter;

        internal float deltaTime
        {
            get { return _deltaTime; }
            set { if (_deltaTime != value) { _deltaTime = value; dirtyFlag |= deltaTimeDirtyFlag; } }
        }

        private float _Speed;
        private EffectParameter _SpeedParameter;

        public float Speed
        {
            get { return _Speed; }
            set { if (_Speed != value) { _Speed = value; dirtyFlag |= SpeedDirtyFlag; } }
        }

        private Texture2D _LastFrameTexture;
        private EffectParameter _LastFrameTextureParameter;

        public Texture2D LastFrameTexture
        {
            get { return _LastFrameTexture; }
            set { if (_LastFrameTexture != value) { _LastFrameTexture = value; dirtyFlag |= LastFrameTextureDirtyFlag; } }
        }

        private int _shaderIndex;
        private EffectParameter _shaderIndexParameter;

        internal int shaderIndex
        {
            get { return _shaderIndex; }
            set { if (_shaderIndex != value) { _shaderIndex = value; dirtyFlag |= shaderIndexDirtyFlag; } }
        }


        #endregion

        #region Apply
        private void ApplyChanges()
        {
            if ((this.dirtyFlag & deltaTimeDirtyFlag) != 0)
            {
                this._deltaTimeParameter.SetValue(_deltaTime);
                this.dirtyFlag &= ~deltaTimeDirtyFlag;
            }
            if ((this.dirtyFlag & SpeedDirtyFlag) != 0)
            {
                this._SpeedParameter.SetValue(_Speed);
                this.dirtyFlag &= ~SpeedDirtyFlag;
            }
            if ((this.dirtyFlag & LastFrameTextureDirtyFlag) != 0)
            {
                this._LastFrameTextureParameter.SetValue(_LastFrameTexture);
                this.dirtyFlag &= ~LastFrameTextureDirtyFlag;
            }
            if ((this.dirtyFlag & shaderIndexDirtyFlag) != 0)
            {
                this._shaderIndexParameter.SetValue(_shaderIndex);
                this.dirtyFlag &= ~shaderIndexDirtyFlag;
            }

        }
        #endregion

        #region Clone
        private void CloneFrom(Adoption cloneSource)
        {
            this._deltaTime = cloneSource._deltaTime;
            this._Speed = cloneSource._Speed;
            this._LastFrameTexture = cloneSource._LastFrameTexture;
            this._shaderIndex = cloneSource._shaderIndex;

        }
        #endregion

        #region Structures

        #endregion

        #region ByteCode
        internal static byte[] ReachEffectCode = null;
        internal static byte[] HiDefEffectCode = null;

        static Adoption()
        {
#if XBOX360
            ReachEffectCode = HiDefEffectCode = new byte[] 
            {
0xBC, 0xF0, 0x0B, 0xCF, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x02, 0xFE, 0xFF, 0x09, 0x01, 0x00, 0x00, 0x02, 0xAC, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x64, 0x65, 0x6C, 0x74, 0x61, 0x54, 0x69, 0x6D, 
0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x0F, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 
0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x11, 
0x4C, 0x61, 0x73, 0x74, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 
0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x02, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x05, 
0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 
0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 
0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 
0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x6E, 
0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0xD4, 0x00, 0x00, 0x00, 0xD0, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0xEC, 
0x00, 0x00, 0x00, 0xE8, 0x00, 0x00, 0x00, 0x73, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x01, 0x0C, 0x00, 0x00, 0x01, 0x08, 0x00, 0x00, 0x00, 0x75, 
0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x01, 0x2C, 0x00, 0x00, 0x01, 0x28, 0x00, 0x00, 0x00, 0x6F, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x01, 0x4C, 
0x00, 0x00, 0x01, 0x48, 0x00, 0x00, 0x00, 0x70, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x01, 0x6C, 0x00, 0x00, 0x01, 0x68, 0x00, 0x00, 0x00, 0x71, 
0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x01, 0x8C, 0x00, 0x00, 0x01, 0x88, 0x00, 0x00, 0x00, 0x0C, 0x6C, 0x61, 0x73, 0x74, 0x53, 0x61, 0x6D, 0x70, 
0x6C, 0x65, 0x72, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x4C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x73, 0x68, 0x61, 0x64, 0x65, 0x72, 0x49, 0x6E, 
0x64, 0x65, 0x78, 0x00, 0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x02, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 
0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x08, 0x50, 0x53, 0x41, 0x72, 0x72, 0x61, 0x79, 0x00, 0x00, 0x00, 0x00, 0x05, 
0x00, 0x00, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x08, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x06, 
0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x34, 
0x00, 0x00, 0x00, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8C, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xBC, 
0x00, 0x00, 0x01, 0xA8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x2C, 0x00, 0x00, 0x02, 0x48, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x5C, 0x00, 0x00, 0x02, 0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xA0, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x02, 0x9C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x5D, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x88, 0x00, 0x00, 0x02, 0x84, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x03, 
0x00, 0x00, 0x02, 0x10, 0x10, 0x2A, 0x11, 0x00, 0x00, 0x00, 0x01, 0x64, 0x00, 0x00, 0x00, 0xAC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 
0x00, 0x00, 0x01, 0x18, 0x00, 0x00, 0x01, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x00, 0x00, 0x00, 0x1C, 
0x00, 0x00, 0x00, 0xE3, 0xFF, 0xFF, 0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDC, 
0x00, 0x00, 0x00, 0x6C, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 0x84, 0x00, 0x00, 0x00, 0x94, 
0x00, 0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xB4, 0x00, 0x02, 0x00, 0x00, 
0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0xD0, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 
0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 
0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0xAB, 0x00, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 
0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x65, 0x6C, 0x74, 0x61, 0x54, 0x69, 0x6D, 0x65, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x6C, 0x61, 0x73, 0x74, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 
0x70, 0x73, 0x5F, 0x33, 0x5F, 0x30, 0x00, 0x32, 0x2E, 0x30, 0x2E, 0x31, 0x31, 0x36, 0x32, 0x36, 0x2E, 0x30, 0x00, 0xAB, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 0x01, 0xFC, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x00, 0x00, 0x00, 0x6C, 0x10, 0x00, 0x02, 0x00, 
0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x21, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x30, 0x50, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0x20, 0x02, 0x00, 0x00, 0x12, 0x00, 
0xC4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 0x04, 0x00, 0x00, 0x22, 0x00, 0x00, 0x00, 0x00, 0x00, 0x10, 0x08, 0x20, 0x01, 0x1F, 0x1F, 0xF6, 0x88, 
0x00, 0x00, 0x40, 0x00, 0x10, 0x18, 0x00, 0x01, 0x1F, 0x1F, 0xF6, 0x88, 0x00, 0x00, 0x40, 0x00, 0xC8, 0x01, 0x00, 0x01, 0x00, 0x6C, 0x6C, 0x00, 
0x21, 0x00, 0x01, 0x00, 0xC8, 0x01, 0x00, 0x01, 0x00, 0x6C, 0x6C, 0x00, 0xA3, 0x01, 0xFF, 0x00, 0xC8, 0x0F, 0x00, 0x02, 0x02, 0x00, 0x00, 0x00, 
0xE0, 0x02, 0x00, 0x00, 0xC8, 0x0F, 0x80, 0x00, 0x00, 0x00, 0x6C, 0x00, 0xEB, 0x02, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x02, 0x58, 0x10, 0x2A, 0x11, 0x00, 0x00, 0x00, 0x01, 0x64, 0x00, 0x00, 0x00, 0xF4, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x00, 0x00, 0x01, 0x18, 0x00, 0x00, 0x01, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0xF0, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0xE3, 0xFF, 0xFF, 0x03, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x1C, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDC, 0x00, 0x00, 0x00, 0x6C, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 
0x00, 0x00, 0x00, 0x84, 0x00, 0x00, 0x00, 0x94, 0x00, 0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0xB4, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 0xC0, 0x00, 0x00, 0x00, 0xD0, 
0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0xAB, 0xAB, 
0x00, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0xAB, 
0x00, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x65, 0x6C, 0x74, 0x61, 0x54, 0x69, 0x6D, 
0x65, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x6C, 0x61, 0x73, 0x74, 
0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0x70, 0x73, 0x5F, 0x33, 0x5F, 0x30, 0x00, 0x32, 0x2E, 0x30, 0x2E, 0x31, 0x31, 0x36, 0x32, 0x36, 
0x2E, 0x30, 0x00, 0xAB, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x14, 
0x01, 0xFC, 0x00, 0x10, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x40, 
0x00, 0x00, 0x00, 0xB4, 0x10, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x08, 0x21, 0x00, 0x01, 0x00, 0x01, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x30, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3F, 0x80, 0x00, 0x00, 0x3B, 0x80, 0x80, 0x81, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x05, 0x20, 0x02, 0x00, 0x00, 0x12, 0x00, 0xC4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x04, 0x40, 0x0A, 0x12, 0x00, 0x22, 0x00, 0x00, 0x00, 
0x10, 0x08, 0x20, 0x01, 0x1F, 0x1F, 0xF6, 0x88, 0x00, 0x00, 0x40, 0x00, 0x10, 0x18, 0x00, 0x01, 0x1F, 0x1F, 0xF6, 0x88, 0x00, 0x00, 0x40, 0x00, 
0xC8, 0x01, 0x00, 0x01, 0x00, 0x6C, 0x6C, 0x00, 0x21, 0x00, 0x01, 0x00, 0xC8, 0x01, 0x00, 0x01, 0x00, 0x6C, 0x6C, 0x00, 0xA3, 0x01, 0xFF, 0x00, 
0xC8, 0x0F, 0x00, 0x02, 0x02, 0x00, 0x00, 0x00, 0xE0, 0x02, 0x00, 0x00, 0xC8, 0x0F, 0x00, 0x03, 0x00, 0x00, 0x6C, 0x00, 0xE1, 0x82, 0x01, 0x00, 
0xC8, 0x0F, 0x00, 0x01, 0x00, 0x00, 0xB1, 0x00, 0xA3, 0x82, 0xFF, 0x00, 0xC8, 0x0F, 0x00, 0x04, 0x02, 0x00, 0x00, 0x00, 0xE5, 0x02, 0x02, 0x00, 
0xC8, 0x0F, 0x00, 0x02, 0x04, 0x00, 0x00, 0x00, 0xE5, 0x02, 0x02, 0x00, 0xC8, 0x0F, 0x00, 0x02, 0x02, 0x00, 0x00, 0x00, 0xE0, 0x04, 0x02, 0x00, 
0xC8, 0x0F, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0xE2, 0x03, 0x01, 0x00, 0xC8, 0x0F, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00, 0xEB, 0x02, 0x01, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xBC, 0x00, 0x00, 0x00, 0x08, 
0x50, 0x53, 0x41, 0x72, 0x72, 0x61, 0x79, 0x00, 0x46, 0x58, 0x02, 0x00, 0x00, 0x19, 0xFF, 0xFE, 0x42, 0x41, 0x54, 0x43, 0x00, 0x00, 0x00, 0x1C, 
0x00, 0x00, 0x00, 0x5F, 0x46, 0x58, 0x02, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x5C, 
0x00, 0x00, 0x00, 0x30, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00, 0x4C, 0x73, 0x68, 0x61, 0x64, 
0x65, 0x72, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x78, 0x00, 0x00, 0x00, 0x02, 0xFF, 0xFE, 
0x54, 0x49, 0x4C, 0x43, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C, 0xFF, 0xFE, 0x43, 0x4C, 0x58, 0x46, 0x00, 0x00, 0x00, 0x01, 0x10, 0x00, 0x00, 0x01, 
0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 
0x00, 0x00, 0x00, 0x00, 0xF0, 0xF0, 0xF0, 0xF0, 0x0F, 0x0F, 0x0F, 0x0F, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x04, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x11, 0x4C, 0x61, 0x73, 0x74, 0x46, 0x72, 0x61, 0x6D, 
0x65, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x00, 0x00, 0x00, 0x00, 
            };
#else
            ReachEffectCode = new byte[] 
            {
0xCF, 0x0B, 0xF0, 0xBC, 0x10, 0x00, 0x00, 0x00, 0x10, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x09, 0xFF, 0xFE, 0xB0, 0x02, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x24, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00, 0x00, 0x00, 0x64, 0x65, 0x6C, 0x74, 0x61, 0x54, 0x69, 0x6D, 
0x65, 0x00, 0xB1, 0x02, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x06, 0x00, 0x00, 0x00, 0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0x00, 0x00, 
0x0A, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x0F, 0x00, 0x00, 0x00, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0x1D, 0x05, 0x00, 0x00, 0x00, 
0x04, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 
0x4C, 0x61, 0x73, 0x74, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x00, 0x27, 0xB1, 0x02, 0x0A, 0x00, 0x00, 0x00, 
0x04, 0x00, 0x00, 0x00, 0x1C, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x05, 0x00, 0x00, 0x00, 
0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0xA4, 0x00, 0x00, 0x00, 
0x00, 0x01, 0x00, 0x00, 0xD4, 0x00, 0x00, 0x00, 0xD0, 0x00, 0x00, 0x00, 0xAA, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0xEC, 0x00, 0x00, 0x00, 
0xE8, 0x00, 0x00, 0x00, 0xA9, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x0C, 0x01, 0x00, 0x00, 0x08, 0x01, 0x00, 0x00, 0xAB, 0x00, 0x00, 0x00, 
0x00, 0x01, 0x00, 0x00, 0x2C, 0x01, 0x00, 0x00, 0x28, 0x01, 0x00, 0x00, 0xA5, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x4C, 0x01, 0x00, 0x00, 
0x48, 0x01, 0x00, 0x00, 0xA6, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x6C, 0x01, 0x00, 0x00, 0x68, 0x01, 0x00, 0x00, 0xA7, 0x00, 0x00, 0x00, 
0x00, 0x01, 0x00, 0x00, 0x8C, 0x01, 0x00, 0x00, 0x88, 0x01, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x6C, 0x61, 0x73, 0x74, 0x53, 0x61, 0x6D, 0x70, 
0x6C, 0x65, 0x72, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x4C, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x73, 0x68, 0x61, 0x64, 0x65, 0x72, 0x49, 0x6E, 
0x64, 0x65, 0x78, 0x00, 0x0F, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x78, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x03, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x50, 0x53, 0x41, 0x72, 0x72, 0x61, 0x79, 0x00, 0x05, 0x00, 0x00, 0x00, 
0x0F, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x00, 0x00, 0xCC, 0x70, 0x08, 0x00, 0x00, 0x00, 0x44, 0x65, 0x66, 0x61, 0x75, 0x6C, 0x74, 0x00, 0x07, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x06, 0x00, 0x00, 0x00, 0x06, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x34, 0x00, 0x00, 0x00, 0x50, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x00, 0x00, 0x00, 0x74, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8C, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0xBC, 0x00, 0x00, 0x00, 0xA8, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x2C, 0x02, 0x00, 0x00, 0x48, 0x02, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x5C, 0x02, 0x00, 0x00, 0x70, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0xA4, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x9C, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x93, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x88, 0x02, 0x00, 0x00, 0x84, 0x02, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x03, 0x00, 0x00, 0x00, 0x54, 0x03, 0x00, 0x00, 0x00, 0x02, 0xFF, 0xFF, 0xFE, 0xFF, 0x2F, 0x00, 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 
0x87, 0x00, 0x00, 0x00, 0x00, 0x02, 0xFF, 0xFF, 0x02, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 
0x44, 0x00, 0x00, 0x00, 0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x02, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 
0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 
0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0xAB, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x6C, 0x61, 0x73, 0x74, 0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x70, 0x73, 0x5F, 0x32, 0x5F, 0x30, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 
0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 
0x32, 0x36, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x32, 0x38, 0x34, 0x34, 0x00, 0xFE, 0xFF, 0x89, 0x00, 0x50, 0x52, 0x45, 0x53, 0x01, 0x02, 0x58, 0x46, 
0xFE, 0xFF, 0x34, 0x00, 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 0x9B, 0x00, 0x00, 0x00, 0x01, 0x02, 0x58, 0x46, 0x02, 0x00, 0x00, 0x00, 
0x1C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x98, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x4C, 0x00, 0x00, 0x00, 0x5C, 0x00, 0x00, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 
0x88, 0x00, 0x00, 0x00, 0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x65, 0x6C, 0x74, 
0x61, 0x54, 0x69, 0x6D, 0x65, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x78, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 
0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 
0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 0x32, 0x36, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x32, 0x38, 0x34, 0x34, 0x00, 0xFE, 0xFF, 0x0C, 0x00, 
0x50, 0x52, 0x53, 0x49, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0xFE, 0xFF, 0x1A, 0x00, 0x43, 0x4C, 0x49, 0x54, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0xBF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x28, 0x00, 0x46, 0x58, 0x4C, 0x43, 0x03, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x50, 0xA0, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x02, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x40, 0xA0, 
0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 
0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x30, 0x03, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0xF0, 0xF0, 0xF0, 0xF0, 0x0F, 0x0F, 0x0F, 0x0F, 0xFF, 0xFF, 0x00, 0x00, 0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x03, 0xB0, 
0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x90, 0x00, 0x08, 0x0F, 0xA0, 0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x90, 0x01, 0x08, 0x0F, 0xA0, 
0x42, 0x00, 0x00, 0x03, 0x00, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0xB0, 0x00, 0x08, 0xE4, 0xA0, 0x42, 0x00, 0x00, 0x03, 0x01, 0x00, 0x0F, 0x80, 
0x00, 0x00, 0xE4, 0xB0, 0x01, 0x08, 0xE4, 0xA0, 0x12, 0x00, 0x00, 0x04, 0x02, 0x00, 0x0F, 0x80, 0x00, 0x00, 0x00, 0xA0, 0x00, 0x00, 0xE4, 0x80, 
0x01, 0x00, 0xE4, 0x80, 0x01, 0x00, 0x00, 0x02, 0x00, 0x08, 0x0F, 0x80, 0x02, 0x00, 0xE4, 0x80, 0xFF, 0xFF, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 
0x08, 0x04, 0x00, 0x00, 0x00, 0x02, 0xFF, 0xFF, 0xFE, 0xFF, 0x2F, 0x00, 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 0x87, 0x00, 0x00, 0x00, 
0x00, 0x02, 0xFF, 0xFF, 0x02, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00, 
0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x02, 0x00, 0x54, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x00, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x70, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 0x53, 0x61, 0x6D, 0x70, 0x6C, 
0x65, 0x72, 0x00, 0xAB, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x6C, 0x61, 0x73, 0x74, 
0x53, 0x61, 0x6D, 0x70, 0x6C, 0x65, 0x72, 0x00, 0x04, 0x00, 0x0C, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x70, 0x73, 0x5F, 0x32, 0x5F, 0x30, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53, 
0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 0x32, 0x36, 0x2E, 0x39, 
0x35, 0x32, 0x2E, 0x32, 0x38, 0x34, 0x34, 0x00, 0xFE, 0xFF, 0x89, 0x00, 0x50, 0x52, 0x45, 0x53, 0x01, 0x02, 0x58, 0x46, 0xFE, 0xFF, 0x34, 0x00, 
0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 0x9B, 0x00, 0x00, 0x00, 0x01, 0x02, 0x58, 0x46, 0x02, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 
0x00, 0x01, 0x00, 0x00, 0x98, 0x00, 0x00, 0x00, 0x44, 0x00, 0x00, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x4C, 0x00, 0x00, 0x00, 
0x5C, 0x00, 0x00, 0x00, 0x6C, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x78, 0x00, 0x00, 0x00, 0x88, 0x00, 0x00, 0x00, 
0x53, 0x70, 0x65, 0x65, 0x64, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x80, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x64, 0x65, 0x6C, 0x74, 0x61, 0x54, 0x69, 0x6D, 
0x65, 0x00, 0xAB, 0xAB, 0x00, 0x00, 0x03, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x78, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 
0x20, 0x28, 0x52, 0x29, 0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 
0x72, 0x20, 0x39, 0x2E, 0x32, 0x36, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x32, 0x38, 0x34, 0x34, 0x00, 0xFE, 0xFF, 0x0C, 0x00, 0x50, 0x52, 0x53, 0x49, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x1A, 0x00, 
0x43, 0x4C, 0x49, 0x54, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0xBF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0x3F, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFE, 0xFF, 0x28, 0x00, 0x46, 0x58, 0x4C, 0x43, 0x03, 0x00, 0x00, 0x00, 0x01, 0x00, 0x50, 0xA0, 
0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x40, 0xA0, 0x02, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x07, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x30, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x07, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x07, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0xF0, 0xF0, 0xF0, 
0x0F, 0x0F, 0x0F, 0x0F, 0xFF, 0xFF, 0x00, 0x00, 0x51, 0x00, 0x00, 0x05, 0x01, 0x00, 0x0F, 0xA0, 0x81, 0x80, 0x80, 0x3B, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x51, 0x00, 0x00, 0x05, 0x02, 0x00, 0x0F, 0xA0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x80, 0x3F, 
0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x80, 0xBF, 0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x80, 0x00, 0x00, 0x03, 0xB0, 0x1F, 0x00, 0x00, 0x02, 
0x00, 0x00, 0x00, 0x90, 0x00, 0x08, 0x0F, 0xA0, 0x1F, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0x90, 0x01, 0x08, 0x0F, 0xA0, 0x42, 0x00, 0x00, 0x03, 
0x00, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0xB0, 0x00, 0x08, 0xE4, 0xA0, 0x42, 0x00, 0x00, 0x03, 0x01, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0xB0, 
0x01, 0x08, 0xE4, 0xA0, 0x02, 0x00, 0x00, 0x03, 0x00, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0x80, 0x01, 0x00, 0xE4, 0x81, 0x58, 0x00, 0x00, 0x04, 
0x02, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0x81, 0x02, 0x00, 0x00, 0xA0, 0x02, 0x00, 0x55, 0xA0, 0x58, 0x00, 0x00, 0x04, 0x03, 0x00, 0x0F, 0x80, 
0x00, 0x00, 0xE4, 0x80, 0x02, 0x00, 0xAA, 0xA0, 0x02, 0x00, 0xFF, 0xA0, 0x23, 0x00, 0x00, 0x02, 0x00, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0x80, 
0x02, 0x00, 0x00, 0x03, 0x02, 0x00, 0x0F, 0x80, 0x02, 0x00, 0xE4, 0x80, 0x03, 0x00, 0xE4, 0x80, 0x05, 0x00, 0x00, 0x03, 0x03, 0x00, 0x0F, 0x80, 
0x00, 0x00, 0xE4, 0x80, 0x00, 0x00, 0x00, 0xA0, 0x0A, 0x00, 0x00, 0x03, 0x04, 0x00, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0x80, 0x01, 0x00, 0x00, 0xA0, 
0x0B, 0x00, 0x00, 0x03, 0x00, 0x00, 0x0F, 0x80, 0x03, 0x00, 0xE4, 0x80, 0x04, 0x00, 0xE4, 0x80, 0x04, 0x00, 0x00, 0x04, 0x00, 0x00, 0x0F, 0x80, 
0x02, 0x00, 0xE4, 0x80, 0x00, 0x00, 0xE4, 0x80, 0x01, 0x00, 0xE4, 0x80, 0x01, 0x00, 0x00, 0x02, 0x00, 0x08, 0x0F, 0x80, 0x00, 0x00, 0xE4, 0x80, 
0xFF, 0xFF, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 
0x00, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 0xEC, 0x00, 0x00, 0x00, 0x08, 0x00, 0x00, 0x00, 0x50, 0x53, 0x41, 0x72, 0x72, 0x61, 0x79, 0x00, 
0x00, 0x02, 0x58, 0x46, 0xFE, 0xFF, 0x25, 0x00, 0x43, 0x54, 0x41, 0x42, 0x1C, 0x00, 0x00, 0x00, 0x5F, 0x00, 0x00, 0x00, 0x00, 0x02, 0x58, 0x46, 
0x01, 0x00, 0x00, 0x00, 0x1C, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x5C, 0x00, 0x00, 0x00, 0x30, 0x00, 0x00, 0x00, 0x02, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x3C, 0x00, 0x00, 0x00, 0x4C, 0x00, 0x00, 0x00, 0x73, 0x68, 0x61, 0x64, 0x65, 0x72, 0x49, 0x6E, 0x64, 0x65, 0x78, 0x00, 
0x00, 0x00, 0x02, 0x00, 0x01, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x74, 0x78, 0x00, 0x4D, 0x69, 0x63, 0x72, 0x6F, 0x73, 0x6F, 0x66, 0x74, 0x20, 0x28, 0x52, 0x29, 
0x20, 0x48, 0x4C, 0x53, 0x4C, 0x20, 0x53, 0x68, 0x61, 0x64, 0x65, 0x72, 0x20, 0x43, 0x6F, 0x6D, 0x70, 0x69, 0x6C, 0x65, 0x72, 0x20, 0x39, 0x2E, 
0x32, 0x36, 0x2E, 0x39, 0x35, 0x32, 0x2E, 0x32, 0x38, 0x34, 0x34, 0x00, 0xFE, 0xFF, 0x02, 0x00, 0x43, 0x4C, 0x49, 0x54, 0x00, 0x00, 0x00, 0x00, 
0xFE, 0xFF, 0x0C, 0x00, 0x46, 0x58, 0x4C, 0x43, 0x01, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x10, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x02, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF0, 0xF0, 0xF0, 0xF0, 
0x0F, 0x0F, 0x0F, 0x0F, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0x04, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 
0x01, 0x00, 0x00, 0x00, 0x11, 0x00, 0x00, 0x00, 0x4C, 0x61, 0x73, 0x74, 0x46, 0x72, 0x61, 0x6D, 0x65, 0x54, 0x65, 0x78, 0x74, 0x75, 0x72, 0x65, 
0x00, 0x27, 0xB1, 0x02, 
            };
            HiDefEffectCode = ReachEffectCode;
#endif
        }
        #endregion
    }

#endif
}