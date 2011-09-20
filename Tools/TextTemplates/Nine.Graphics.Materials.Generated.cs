﻿// -----------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a text template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// -----------------------------------------------------------------------------


#if !TEXT_TEMPLATE
namespace Nine.Graphics.Effects
{
    /// <summary>
    /// Effect instance for <c>AlphaTestEffect</c>.
    /// </summary>
    [Nine.ContentSerializable()]
    [System.CodeDom.Compiler.GeneratedCode("Materials.tt", "1.1.0.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public partial class AlphaTestMaterial : Nine.Graphics.Effects.Material
    {        
        /// <summary>
        /// Gets the underlying AlphaTestMaterial.
        /// </summary>
        public override Microsoft.Xna.Framework.Graphics.Effect Effect { get { return effect; } }
        Microsoft.Xna.Framework.Graphics.AlphaTestEffect effect;
        /// <summary>
        /// 
        /// </summary>
        public System.Single Alpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.CompareFunction AlphaFunction { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 DiffuseColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ReferenceAlpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean VertexColorEnabled { get; set; }

        private AlphaTestMaterial() { }

        /// <summary>
        /// Initializes a new instance of <c>AlphaTestMaterial</c>.
        /// </summary>
        public AlphaTestMaterial(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
            : this(Nine.Graphics.GraphicsResources<Microsoft.Xna.Framework.Graphics.AlphaTestEffect>.GetInstance(graphics))
        {
            OnCreate();
        }

        /// <summary>
        /// Initializes a new instance of <c>AlphaTestMaterial</c>.
        /// </summary>
        public AlphaTestMaterial(Microsoft.Xna.Framework.Graphics.AlphaTestEffect effect)
        {
            this.effect = effect;
            this.Alpha = effect.Alpha;
            this.AlphaFunction = effect.AlphaFunction;
            this.DiffuseColor = effect.DiffuseColor;
            this.ReferenceAlpha = effect.ReferenceAlpha;
            this.Texture = effect.Texture;
            this.VertexColorEnabled = effect.VertexColorEnabled;
        }
        
        partial void OnApply();
        partial void OnCreate();
        partial void OnClone(AlphaTestMaterial cloned);

        /// <summary>
        /// Applys the parameter values to the underlying AlphaTestMaterial.
        /// </summary>
        public override void Apply()
        {
            if (this.effect.Alpha != this.Alpha)
                this.effect.Alpha = this.Alpha;
            if (this.effect.AlphaFunction != this.AlphaFunction)
                this.effect.AlphaFunction = this.AlphaFunction;
            if (this.effect.DiffuseColor != this.DiffuseColor)
                this.effect.DiffuseColor = this.DiffuseColor;
            if (this.effect.ReferenceAlpha != this.ReferenceAlpha)
                this.effect.ReferenceAlpha = this.ReferenceAlpha;
            if (this.effect.Texture != this.Texture)
                this.effect.Texture = this.Texture;
            if (this.effect.VertexColorEnabled != this.VertexColorEnabled)
                this.effect.VertexColorEnabled = this.VertexColorEnabled;
      
        }
        
        /// <summary>
        /// Clones the parameter values to a new instance of AlphaTestMaterial.
        /// </summary>
        public override Nine.Graphics.Effects.Material Clone()
        {
            var cloned = new AlphaTestMaterial();
            cloned.effect = this.effect;
            cloned.DepthAlphaEnabled = this.DepthAlphaEnabled;
            cloned.Alpha = this.Alpha;
            cloned.AlphaFunction = this.AlphaFunction;
            cloned.DiffuseColor = this.DiffuseColor;
            cloned.ReferenceAlpha = this.ReferenceAlpha;
            cloned.Texture = this.Texture;
            cloned.VertexColorEnabled = this.VertexColorEnabled;
            OnClone(cloned);
            return cloned;
        }
    }
    /// <summary>
    /// Effect instance for <c>BasicEffect</c>.
    /// </summary>
    [Nine.ContentSerializable()]
    [System.CodeDom.Compiler.GeneratedCode("Materials.tt", "1.1.0.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public partial class BasicMaterial : Nine.Graphics.Effects.Material
    {        
        /// <summary>
        /// Gets the underlying BasicMaterial.
        /// </summary>
        public override Microsoft.Xna.Framework.Graphics.Effect Effect { get { return effect; } }
        Microsoft.Xna.Framework.Graphics.BasicEffect effect;
        /// <summary>
        /// 
        /// </summary>
        public System.Single Alpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 DiffuseColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 EmissiveColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean LightingEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean PreferPerPixelLighting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 SpecularColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Single SpecularPower { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean TextureEnabled { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean VertexColorEnabled { get; set; }

        private BasicMaterial() { }

        /// <summary>
        /// Initializes a new instance of <c>BasicMaterial</c>.
        /// </summary>
        public BasicMaterial(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
            : this(Nine.Graphics.GraphicsResources<Microsoft.Xna.Framework.Graphics.BasicEffect>.GetInstance(graphics))
        {
            OnCreate();
        }

        /// <summary>
        /// Initializes a new instance of <c>BasicMaterial</c>.
        /// </summary>
        public BasicMaterial(Microsoft.Xna.Framework.Graphics.BasicEffect effect)
        {
            this.effect = effect;
            this.Alpha = effect.Alpha;
            this.DiffuseColor = effect.DiffuseColor;
            this.EmissiveColor = effect.EmissiveColor;
            this.LightingEnabled = effect.LightingEnabled;
            this.PreferPerPixelLighting = effect.PreferPerPixelLighting;
            this.SpecularColor = effect.SpecularColor;
            this.SpecularPower = effect.SpecularPower;
            this.Texture = effect.Texture;
            this.TextureEnabled = effect.TextureEnabled;
            this.VertexColorEnabled = effect.VertexColorEnabled;
        }
        
        partial void OnApply();
        partial void OnCreate();
        partial void OnClone(BasicMaterial cloned);

        /// <summary>
        /// Applys the parameter values to the underlying BasicMaterial.
        /// </summary>
        public override void Apply()
        {
            if (this.effect.Alpha != this.Alpha)
                this.effect.Alpha = this.Alpha;
            if (this.effect.DiffuseColor != this.DiffuseColor)
                this.effect.DiffuseColor = this.DiffuseColor;
            if (this.effect.EmissiveColor != this.EmissiveColor)
                this.effect.EmissiveColor = this.EmissiveColor;
            if (this.effect.LightingEnabled != this.LightingEnabled)
                this.effect.LightingEnabled = this.LightingEnabled;
            if (this.effect.PreferPerPixelLighting != this.PreferPerPixelLighting)
                this.effect.PreferPerPixelLighting = this.PreferPerPixelLighting;
            if (this.effect.SpecularColor != this.SpecularColor)
                this.effect.SpecularColor = this.SpecularColor;
            if (this.effect.SpecularPower != this.SpecularPower)
                this.effect.SpecularPower = this.SpecularPower;
            if (this.effect.Texture != this.Texture)
                this.effect.Texture = this.Texture;
            if (this.effect.TextureEnabled != this.TextureEnabled)
                this.effect.TextureEnabled = this.TextureEnabled;
            if (this.effect.VertexColorEnabled != this.VertexColorEnabled)
                this.effect.VertexColorEnabled = this.VertexColorEnabled;
      
        }
        
        /// <summary>
        /// Clones the parameter values to a new instance of BasicMaterial.
        /// </summary>
        public override Nine.Graphics.Effects.Material Clone()
        {
            var cloned = new BasicMaterial();
            cloned.effect = this.effect;
            cloned.DepthAlphaEnabled = this.DepthAlphaEnabled;
            cloned.Alpha = this.Alpha;
            cloned.DiffuseColor = this.DiffuseColor;
            cloned.EmissiveColor = this.EmissiveColor;
            cloned.LightingEnabled = this.LightingEnabled;
            cloned.PreferPerPixelLighting = this.PreferPerPixelLighting;
            cloned.SpecularColor = this.SpecularColor;
            cloned.SpecularPower = this.SpecularPower;
            cloned.Texture = this.Texture;
            cloned.TextureEnabled = this.TextureEnabled;
            cloned.VertexColorEnabled = this.VertexColorEnabled;
            OnClone(cloned);
            return cloned;
        }
    }
    /// <summary>
    /// Effect instance for <c>DualTextureEffect</c>.
    /// </summary>
    [Nine.ContentSerializable()]
    [System.CodeDom.Compiler.GeneratedCode("Materials.tt", "1.1.0.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public partial class DualTextureMaterial : Nine.Graphics.Effects.Material
    {        
        /// <summary>
        /// Gets the underlying DualTextureMaterial.
        /// </summary>
        public override Microsoft.Xna.Framework.Graphics.Effect Effect { get { return effect; } }
        Microsoft.Xna.Framework.Graphics.DualTextureEffect effect;
        /// <summary>
        /// 
        /// </summary>
        public System.Single Alpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 DiffuseColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean VertexColorEnabled { get; set; }

        private DualTextureMaterial() { }

        /// <summary>
        /// Initializes a new instance of <c>DualTextureMaterial</c>.
        /// </summary>
        public DualTextureMaterial(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
            : this(Nine.Graphics.GraphicsResources<Microsoft.Xna.Framework.Graphics.DualTextureEffect>.GetInstance(graphics))
        {
            OnCreate();
        }

        /// <summary>
        /// Initializes a new instance of <c>DualTextureMaterial</c>.
        /// </summary>
        public DualTextureMaterial(Microsoft.Xna.Framework.Graphics.DualTextureEffect effect)
        {
            this.effect = effect;
            this.Alpha = effect.Alpha;
            this.DiffuseColor = effect.DiffuseColor;
            this.Texture = effect.Texture;
            this.Texture2 = effect.Texture2;
            this.VertexColorEnabled = effect.VertexColorEnabled;
        }
        
        partial void OnApply();
        partial void OnCreate();
        partial void OnClone(DualTextureMaterial cloned);

        /// <summary>
        /// Applys the parameter values to the underlying DualTextureMaterial.
        /// </summary>
        public override void Apply()
        {
            if (this.effect.Alpha != this.Alpha)
                this.effect.Alpha = this.Alpha;
            if (this.effect.DiffuseColor != this.DiffuseColor)
                this.effect.DiffuseColor = this.DiffuseColor;
            if (this.effect.Texture != this.Texture)
                this.effect.Texture = this.Texture;
            if (this.effect.Texture2 != this.Texture2)
                this.effect.Texture2 = this.Texture2;
            if (this.effect.VertexColorEnabled != this.VertexColorEnabled)
                this.effect.VertexColorEnabled = this.VertexColorEnabled;
      
        }
        
        /// <summary>
        /// Clones the parameter values to a new instance of DualTextureMaterial.
        /// </summary>
        public override Nine.Graphics.Effects.Material Clone()
        {
            var cloned = new DualTextureMaterial();
            cloned.effect = this.effect;
            cloned.DepthAlphaEnabled = this.DepthAlphaEnabled;
            cloned.Alpha = this.Alpha;
            cloned.DiffuseColor = this.DiffuseColor;
            cloned.Texture = this.Texture;
            cloned.Texture2 = this.Texture2;
            cloned.VertexColorEnabled = this.VertexColorEnabled;
            OnClone(cloned);
            return cloned;
        }
    }
    /// <summary>
    /// Effect instance for <c>EnvironmentMapEffect</c>.
    /// </summary>
    [Nine.ContentSerializable()]
    [System.CodeDom.Compiler.GeneratedCode("Materials.tt", "1.1.0.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public partial class EnvironmentMapMaterial : Nine.Graphics.Effects.Material
    {        
        /// <summary>
        /// Gets the underlying EnvironmentMapMaterial.
        /// </summary>
        public override Microsoft.Xna.Framework.Graphics.Effect Effect { get { return effect; } }
        Microsoft.Xna.Framework.Graphics.EnvironmentMapEffect effect;
        /// <summary>
        /// 
        /// </summary>
        public System.Single Alpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 DiffuseColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 EmissiveColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.TextureCube EnvironmentMap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Single EnvironmentMapAmount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 EnvironmentMapSpecular { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Single FresnelFactor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }

        private EnvironmentMapMaterial() { }

        /// <summary>
        /// Initializes a new instance of <c>EnvironmentMapMaterial</c>.
        /// </summary>
        public EnvironmentMapMaterial(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
            : this(Nine.Graphics.GraphicsResources<Microsoft.Xna.Framework.Graphics.EnvironmentMapEffect>.GetInstance(graphics))
        {
            OnCreate();
        }

        /// <summary>
        /// Initializes a new instance of <c>EnvironmentMapMaterial</c>.
        /// </summary>
        public EnvironmentMapMaterial(Microsoft.Xna.Framework.Graphics.EnvironmentMapEffect effect)
        {
            this.effect = effect;
            this.Alpha = effect.Alpha;
            this.DiffuseColor = effect.DiffuseColor;
            this.EmissiveColor = effect.EmissiveColor;
            this.EnvironmentMap = effect.EnvironmentMap;
            this.EnvironmentMapAmount = effect.EnvironmentMapAmount;
            this.EnvironmentMapSpecular = effect.EnvironmentMapSpecular;
            this.FresnelFactor = effect.FresnelFactor;
            this.Texture = effect.Texture;
        }
        
        partial void OnApply();
        partial void OnCreate();
        partial void OnClone(EnvironmentMapMaterial cloned);

        /// <summary>
        /// Applys the parameter values to the underlying EnvironmentMapMaterial.
        /// </summary>
        public override void Apply()
        {
            if (this.effect.Alpha != this.Alpha)
                this.effect.Alpha = this.Alpha;
            if (this.effect.DiffuseColor != this.DiffuseColor)
                this.effect.DiffuseColor = this.DiffuseColor;
            if (this.effect.EmissiveColor != this.EmissiveColor)
                this.effect.EmissiveColor = this.EmissiveColor;
            if (this.effect.EnvironmentMap != this.EnvironmentMap)
                this.effect.EnvironmentMap = this.EnvironmentMap;
            if (this.effect.EnvironmentMapAmount != this.EnvironmentMapAmount)
                this.effect.EnvironmentMapAmount = this.EnvironmentMapAmount;
            if (this.effect.EnvironmentMapSpecular != this.EnvironmentMapSpecular)
                this.effect.EnvironmentMapSpecular = this.EnvironmentMapSpecular;
            if (this.effect.FresnelFactor != this.FresnelFactor)
                this.effect.FresnelFactor = this.FresnelFactor;
            if (this.effect.Texture != this.Texture)
                this.effect.Texture = this.Texture;
      
        }
        
        /// <summary>
        /// Clones the parameter values to a new instance of EnvironmentMapMaterial.
        /// </summary>
        public override Nine.Graphics.Effects.Material Clone()
        {
            var cloned = new EnvironmentMapMaterial();
            cloned.effect = this.effect;
            cloned.DepthAlphaEnabled = this.DepthAlphaEnabled;
            cloned.Alpha = this.Alpha;
            cloned.DiffuseColor = this.DiffuseColor;
            cloned.EmissiveColor = this.EmissiveColor;
            cloned.EnvironmentMap = this.EnvironmentMap;
            cloned.EnvironmentMapAmount = this.EnvironmentMapAmount;
            cloned.EnvironmentMapSpecular = this.EnvironmentMapSpecular;
            cloned.FresnelFactor = this.FresnelFactor;
            cloned.Texture = this.Texture;
            OnClone(cloned);
            return cloned;
        }
    }
    /// <summary>
    /// Effect instance for <c>SkinnedEffect</c>.
    /// </summary>
    [Nine.ContentSerializable()]
    [System.CodeDom.Compiler.GeneratedCode("Materials.tt", "1.1.0.0")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.Runtime.CompilerServices.CompilerGenerated()]
    [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
    public partial class SkinnedMaterial : Nine.Graphics.Effects.Material
    {        
        /// <summary>
        /// Gets the underlying SkinnedMaterial.
        /// </summary>
        public override Microsoft.Xna.Framework.Graphics.Effect Effect { get { return effect; } }
        Microsoft.Xna.Framework.Graphics.SkinnedEffect effect;
        /// <summary>
        /// 
        /// </summary>
        public System.Single Alpha { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 DiffuseColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 EmissiveColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Boolean PreferPerPixelLighting { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Vector3 SpecularColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Single SpecularPower { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Microsoft.Xna.Framework.Graphics.Texture2D Texture { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Int32 WeightsPerVertex { get; set; }

        private SkinnedMaterial() { }

        /// <summary>
        /// Initializes a new instance of <c>SkinnedMaterial</c>.
        /// </summary>
        public SkinnedMaterial(Microsoft.Xna.Framework.Graphics.GraphicsDevice graphics)
            : this(Nine.Graphics.GraphicsResources<Microsoft.Xna.Framework.Graphics.SkinnedEffect>.GetInstance(graphics))
        {
            OnCreate();
        }

        /// <summary>
        /// Initializes a new instance of <c>SkinnedMaterial</c>.
        /// </summary>
        public SkinnedMaterial(Microsoft.Xna.Framework.Graphics.SkinnedEffect effect)
        {
            this.effect = effect;
            this.Alpha = effect.Alpha;
            this.DiffuseColor = effect.DiffuseColor;
            this.EmissiveColor = effect.EmissiveColor;
            this.PreferPerPixelLighting = effect.PreferPerPixelLighting;
            this.SpecularColor = effect.SpecularColor;
            this.SpecularPower = effect.SpecularPower;
            this.Texture = effect.Texture;
            this.WeightsPerVertex = effect.WeightsPerVertex;
        }
        
        partial void OnApply();
        partial void OnCreate();
        partial void OnClone(SkinnedMaterial cloned);

        /// <summary>
        /// Applys the parameter values to the underlying SkinnedMaterial.
        /// </summary>
        public override void Apply()
        {
            if (this.effect.Alpha != this.Alpha)
                this.effect.Alpha = this.Alpha;
            if (this.effect.DiffuseColor != this.DiffuseColor)
                this.effect.DiffuseColor = this.DiffuseColor;
            if (this.effect.EmissiveColor != this.EmissiveColor)
                this.effect.EmissiveColor = this.EmissiveColor;
            if (this.effect.PreferPerPixelLighting != this.PreferPerPixelLighting)
                this.effect.PreferPerPixelLighting = this.PreferPerPixelLighting;
            if (this.effect.SpecularColor != this.SpecularColor)
                this.effect.SpecularColor = this.SpecularColor;
            if (this.effect.SpecularPower != this.SpecularPower)
                this.effect.SpecularPower = this.SpecularPower;
            if (this.effect.Texture != this.Texture)
                this.effect.Texture = this.Texture;
            if (this.effect.WeightsPerVertex != this.WeightsPerVertex)
                this.effect.WeightsPerVertex = this.WeightsPerVertex;
      
        }
        
        /// <summary>
        /// Clones the parameter values to a new instance of SkinnedMaterial.
        /// </summary>
        public override Nine.Graphics.Effects.Material Clone()
        {
            var cloned = new SkinnedMaterial();
            cloned.effect = this.effect;
            cloned.DepthAlphaEnabled = this.DepthAlphaEnabled;
            cloned.Alpha = this.Alpha;
            cloned.DiffuseColor = this.DiffuseColor;
            cloned.EmissiveColor = this.EmissiveColor;
            cloned.PreferPerPixelLighting = this.PreferPerPixelLighting;
            cloned.SpecularColor = this.SpecularColor;
            cloned.SpecularPower = this.SpecularPower;
            cloned.Texture = this.Texture;
            cloned.WeightsPerVertex = this.WeightsPerVertex;
            OnClone(cloned);
            return cloned;
        }
    }
}
#endif