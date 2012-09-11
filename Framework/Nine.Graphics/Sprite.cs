﻿namespace Nine.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Markup;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Nine.Graphics.Drawing;
    using Nine.Graphics.Materials;
    
    /// <summary>
    /// Defines a 2D textured sprite.
    /// </summary>
    [ContentProperty("Material")]
    public class Sprite : Transformable, ISprite
    {
        #region Properties
        /// <summary>
        /// Gets the underlying graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Gets or sets whether this sprite is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the material of this sprite.
        /// </summary>
        public Material Material
        {
            get { return material; }
            set { material = value; }
        }
        private Material material;

        /// <summary>
        /// Gets or sets the alpha of this sprite.
        /// </summary>
        public float Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }
        private float alpha = 1;

        /// <summary>
        /// Gets or sets the color of this sprite.
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
        private Color color = Color.White;

        /// <summary>
        /// Gets or sets the blend state of this sprite. The default value is BlendState.AlphaBlend.
        /// </summary>
        public BlendState BlendState { get; set; }

        /// <summary>
        /// Gets or sets the sampler state of this sprite. The default value is SamplerState.LinearClamp.
        /// </summary>
        public SamplerState SamplerState { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether this sprite
        /// will be flipped on the x axis.
        /// </summary>
        public bool FlipX
        {
            get { return flipX; }
            set { flipX = value; }
        }
        private bool flipX;

        /// <summary>
        /// Gets or sets a value indicating whether this sprite
        /// will be flipped on the x axis.
        /// </summary>
        public bool FlipY
        {
            get { return flipY; }
            set { flipY = value; }
        }
        private bool flipY;

        /// <summary>
        /// Gets or sets the z order of this sprite.
        /// </summary>
        public int ZOrder
        {
            get { return zOrder; }
            set { zOrder = value; }
        }
        private int zOrder;
        #endregion

        #region Transform
        /// <summary>
        /// Gets the bounds of this sprite in world space.
        /// </summary>
        public BoundingRectangle BoundingBox
        {
            get { return boundingBox; }
        }
        private BoundingRectangle boundingBox;

        /// <summary>
        /// Gets or sets the position of this sprite in local space.
        /// </summary>
        public Vector2 Position 
        {
            get { return position; }
            set 
            {
                position = value;
                transform.M41 = value.X;
                transform.M42 = value.Y;
                transformChanging = true;
                NotifyTransformChanged();
                transformChanging = false;
            }
        }
        private Vector2 position;
        
        /// <summary>
        /// Gets or sets the scale of this sprite in local space.
        /// </summary>
        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; UpdateScaleAndRotation(); }
        }
        private Vector2 scale = Vector2.One;

        /// <summary>
        /// Gets or sets the rotation of this sprite in local space.
        /// </summary>
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; UpdateScaleAndRotation(); }
        }
        private float rotation;

        /// <summary>
        /// Gets or sets the optional target size of this sprite in screen space independent of the scale factor.
        /// </summary>
        public Vector2? Size
        {
            get { return size; }
            set { size = value; UpdateScaleAndRotation(); anchorPointNeedsUpdate = true; }
        }
        private Vector2? size;

        /// <summary>
        /// Gets or sets the percentage based anchor point of this sprite.
        /// </summary>
        public Vector2 Anchor 
        {
            get { return anchor; }
            set { anchor = value; UpdateScaleAndRotation(); anchorPointNeedsUpdate = true; }
        }
        private Vector2 anchor = new Vector2(0.5f, 0.5f);
        private Vector2 anchorPoint;
        private bool anchorPointNeedsUpdate = true;

        /// <summary>
        /// Gets or sets the texture of this sprite.
        /// </summary>
        public Texture2D Texture
        {
            get { return texture; }
            set { if (texture != value) { texture = value; UpdateScaleAndRotation(); anchorPointNeedsUpdate = true; } }
        }
        private Texture2D texture;
        
        /// <summary>
        /// Gets or sets the rectangular region in the source texture.
        /// </summary>
        public Rectangle? SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; UpdateScaleAndRotation(); anchorPointNeedsUpdate = true; }
        }
        private Rectangle? sourceRectangle;
        #endregion

        #region Fields
        /// <summary>
        /// The vertex buffer and index buffers are shared between sprites.
        /// </summary>
        private DynamicVertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private bool transformChanging;
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of Sprite.
        /// </summary>
        public Sprite(GraphicsDevice graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            Visible = true;
            GraphicsDevice = graphics;
        }

        /// <summary>
        /// Gets the vertex and index buffer for drawing sprites.
        /// </summary>
        private static void GetBuffers(GraphicsDevice graphics, out DynamicVertexBuffer vertexBuffer, out IndexBuffer indexBuffer)
        {
            KeyValuePair<DynamicVertexBuffer, IndexBuffer> sharedBuffer;

            if (SharedBuffers == null)
                SharedBuffers = new Dictionary<GraphicsDevice, KeyValuePair<DynamicVertexBuffer, IndexBuffer>>();

            if (!SharedBuffers.TryGetValue(graphics, out sharedBuffer))
            {
                sharedBuffer = new KeyValuePair<DynamicVertexBuffer, IndexBuffer>(
                    new DynamicVertexBuffer(graphics, typeof(VertexPositionColorTexture), 4, BufferUsage.WriteOnly)
                  , new IndexBuffer(graphics, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly));

                sharedBuffer.Value.SetData<ushort>(new ushort[] { 0, 1, 2, 0, 2, 3 });
                SharedBuffers.Add(graphics, sharedBuffer);
            }

            vertexBuffer = sharedBuffer.Key;
            indexBuffer = sharedBuffer.Value;
        }
        private static Dictionary<GraphicsDevice, KeyValuePair<DynamicVertexBuffer, IndexBuffer>> SharedBuffers;
        private static VertexPositionColorTexture[] SharedVertices = new VertexPositionColorTexture[4];

#if !WINDOWS_PHONE
        private static BasicEffect GetBasicEffect(GraphicsDevice graphics)
        {
            if (SharedEffects == null)
                SharedEffects = new Dictionary<GraphicsDevice, BasicEffect>();
            
            BasicEffect result;
            if (!SharedEffects.TryGetValue(graphics, out result))
                SharedEffects.Add(graphics, result = new BasicEffect(graphics) { LightingEnabled = false, VertexColorEnabled = true, TextureEnabled = true });
            return result;
        }
        private static Dictionary<GraphicsDevice, BasicEffect> SharedEffects;
        private BasicEffect basicEffect;
#endif
        
        /// <summary>
        /// Draws this sprite with the specified material.
        /// </summary>
        public void Draw(DrawingContext context, Material material)
        {   
            float width, height;
            float left, right, top, bottom;
            float textureWidth, textureHeight;

            if (texture != null)
            {
                textureWidth = (float)texture.Width;
                textureHeight = (float)texture.Height;
            }
            else if (size != null)
            {
                textureWidth = size.Value.X;
                textureHeight = size.Value.Y;
            }
            else
            {
                return;
            }

            if (sourceRectangle.HasValue)
            {
                var rect = sourceRectangle.Value;
                left = rect.X / textureWidth; right = (rect.X + rect.Width) / textureWidth;
                top = rect.Y / textureHeight; bottom = (rect.Y + rect.Height) / textureHeight;

                width = rect.Width;
                height = rect.Height;
            }
            else
            {
                width = textureWidth;
                height = textureHeight;

                left = 0f; right = 1f;
                top = 0f; bottom = 1f;
            }

            if (flipX)
            {
                var temp = left; left = right; right = temp;
            }
            if (flipY)
            {
                var temp = top; top = bottom; bottom = temp;
            }

            UpdateAnchorPoint();

            SharedVertices[0].Position = new Vector3(-anchorPoint.X, -anchorPoint.Y, 0);
            SharedVertices[1].Position = new Vector3(width - anchorPoint.X, -anchorPoint.Y, 0);
            SharedVertices[2].Position = new Vector3(width - anchorPoint.X, height - anchorPoint.Y, 0);
            SharedVertices[3].Position = new Vector3(-anchorPoint.X, height - anchorPoint.Y, 0);

            SharedVertices[0].TextureCoordinate = new Vector2(left, top);
            SharedVertices[1].TextureCoordinate = new Vector2(right, top);
            SharedVertices[2].TextureCoordinate = new Vector2(right, bottom);
            SharedVertices[3].TextureCoordinate = new Vector2(left, bottom);

            SharedVertices[0].Color = SharedVertices[1].Color = SharedVertices[2].Color = SharedVertices[3].Color = color * alpha;

            if (vertexBuffer == null)
                GetBuffers(context.graphics, out vertexBuffer, out indexBuffer);
            if (basicEffect == null)
                basicEffect = GetBasicEffect(context.graphics);
                        
            context.SetVertexBuffer(null, 0);
            vertexBuffer.SetData(SharedVertices);

            // Apply a basic effect in case the specified material does not have a vertex shader
#if !WINDOWS_PHONE
            basicEffect.World = AbsoluteTransform;
            basicEffect.View = context.matrices.view;
            basicEffect.Projection = context.matrices.projection;
            basicEffect.CurrentTechnique.Passes[0].Apply();
#endif

            bool replaceTexture;
            if (replaceTexture = (material.texture == null))
                material.texture = texture;

            material.world = AbsoluteTransform;
            material.BeginApply(context);
            
            context.SetVertexBuffer(vertexBuffer, 0);
            context.graphics.Indices = indexBuffer;
            context.graphics.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            
            material.EndApply(context);

            if (replaceTexture)
                material.texture = null;
        }

        /// <summary>
        /// Draws this sprite using the specified sprite batch.
        /// </summary>
        public void Draw(DrawingContext context, SpriteBatch spriteBatch)
        {
            if (texture != null)
            {
                var spriteEffects = SpriteEffects.None;
                if (flipX)
                    spriteEffects |= SpriteEffects.FlipHorizontally;
                if (flipY)
                    spriteEffects |= SpriteEffects.FlipVertically;

                float worldRotation;
                Vector2 worldScale;
                Vector2 worldPosition;
                Matrix worldTransform = AbsoluteTransform;
                MatrixHelper.Decompose(ref worldTransform, out worldScale, out worldRotation, out worldPosition);

                UpdateAnchorPoint();

                spriteBatch.Draw(texture, worldPosition, sourceRectangle, color * alpha, worldRotation, anchorPoint, worldScale, spriteEffects, 0);
            }
        }

        /// <summary>
        /// Called when local or absolute transform changed.
        /// </summary>
        protected override void OnTransformChanged()
        {
            if (!transformChanging)
                MatrixHelper.Decompose(ref transform, out scale, out rotation, out position);
        }

        private void UpdateScaleAndRotation()
        {
            var scale = this.scale;
            ApplyScaleFactor(ref scale);

            if (rotation == 0)
            {
                transform.M11 = scale.X; transform.M12 = 0;
                transform.M21 = 0; transform.M22 = scale.Y;
            }
            else
            {
                var cos = (float)Math.Cos(rotation);
                var sin = (float)Math.Sin(rotation);

                transform.M11 = cos * scale.X; transform.M12 = sin * scale.X;
                transform.M21 = -sin * scale.Y; transform.M22 = cos * scale.Y;
            }

            transformChanging = true;
            NotifyTransformChanged();
            transformChanging = false;
        }

        private void ApplyScaleFactor(ref Vector2 scale)
        {
            if (size != null)
            {
                if (sourceRectangle == null)
                {
                    if (texture != null)
                    {
                        scale.X *= size.Value.X / texture.Width;
                        scale.Y *= size.Value.Y / texture.Height;
                    }
                    else
                    {
                        scale.X *= size.Value.X;
                        scale.Y *= size.Value.Y;
                    }
                }
                else
                {
                    scale.X *= size.Value.X / sourceRectangle.Value.Width;
                    scale.Y *= size.Value.Y / sourceRectangle.Value.Height;
                }
            }
        }

        private void UpdateAnchorPoint()
        {
            if (anchorPointNeedsUpdate)
            {
                if (sourceRectangle != null)
                {
                    var r = sourceRectangle.Value;
                    anchorPoint.X = r.X + r.Width * anchor.X;
                    anchorPoint.Y = r.Y + r.Height * anchor.Y;
                }
                else if (texture != null)
                {
                    anchorPoint.X = texture.Width * anchor.X;
                    anchorPoint.Y = texture.Height * anchor.Y;
                }
                else
                {
                    anchorPoint = anchor;
                }
                anchorPointNeedsUpdate = false;
            }
        }
        #endregion
    }
}
