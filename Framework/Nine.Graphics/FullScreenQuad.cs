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
    /// Represents a full screen quad.
    /// </summary>
    [ContentSerializable]
    [RuntimeNameProperty("Name")]
    [DictionaryKeyProperty("Name")]
    [ContentProperty("Material")]
    public class FullScreenQuad : Nine.Object, IDrawableObject
    {
        /// <summary>
        /// Gets the graphics device.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }

        /// <summary>
        /// Gets whether this object is visible.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets the material of the object.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// The vertex buffer and index buffers are shared between FullScreenQuads.
        /// </summary>
        private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private static Dictionary<GraphicsDevice, KeyValuePair<VertexBuffer, IndexBuffer>> SharedBuffers;

        /// <summary>
        /// Always use this pass through material as the vertex shader when drawing fullscreen quads.
        /// </summary>
        private VertexPassThroughMaterial vertexPassThrough;

        /// <summary>
        /// Initializes a new instance of the <see cref="FullScreenQuad"/> class.
        /// </summary>
        public FullScreenQuad(GraphicsDevice graphics)
        {
            if (graphics == null)
                throw new ArgumentNullException("graphics");

            Visible = true;
            GraphicsDevice = graphics;
            vertexPassThrough = new VertexPassThroughMaterial(graphics);

            KeyValuePair<VertexBuffer, IndexBuffer> sharedBuffer;     
                   
            if (SharedBuffers == null)
                SharedBuffers = new Dictionary<GraphicsDevice, KeyValuePair<VertexBuffer, IndexBuffer>>();

            if (!SharedBuffers.TryGetValue(graphics, out sharedBuffer))
            {
                sharedBuffer = new KeyValuePair<VertexBuffer, IndexBuffer>(
                    new VertexBuffer(graphics, typeof(VertexPositionTexture), 4, BufferUsage.WriteOnly)
                  , new IndexBuffer(graphics, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly));

                sharedBuffer.Key.SetData(new[] 
                {
                    new VertexPositionTexture() { Position = new Vector3(-1, 1, 0), TextureCoordinate = new Vector2(0, 0) },
                    new VertexPositionTexture() { Position = new Vector3(1, 1, 0), TextureCoordinate = new Vector2(1, 0) },
                    new VertexPositionTexture() { Position = new Vector3(1, -1, 0), TextureCoordinate = new Vector2(1, 1) },
                    new VertexPositionTexture() { Position = new Vector3(-1, -1, 0), TextureCoordinate = new Vector2(0, 1) },
                });

                sharedBuffer.Value.SetData<ushort>(new ushort[] { 0, 1, 2, 0, 2, 3 });
                SharedBuffers.Add(graphics, sharedBuffer);
            }

            vertexBuffer = sharedBuffer.Key;
            indexBuffer = sharedBuffer.Value;
        }

        public void Draw(DrawingContext context, Material material)
        {
            material.BeginApply(context);

            context.SetVertexBuffer(vertexBuffer, 0);
            GraphicsDevice.Indices = indexBuffer;

            // Apply a vertex pass through material in case the specified material does
            // not have a vertex shader.
            //
            // NOTE: There is no legal way to determine the current shader profile. If you are mix using a 
            //       2_0 vs and 3_0 ps, DrawIndexedPrimitives is going to throw an InvalidOperationException,
            //       in that case, catch that exception and try to draw with 3_0 vs.
            try
            {
                // Use vs 2_0
                vertexPassThrough.Apply(0);
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }
            catch (InvalidOperationException)
            {
                if (context.GraphicsDevice.GraphicsProfile != GraphicsProfile.HiDef)
                    throw;

                // Use vs 3_0
                vertexPassThrough.Apply(1);
                GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 4, 0, 2);
            }

            material.EndApply(context);
        }

        void IDrawableObject.BeginDraw(DrawingContext context) { }
        void IDrawableObject.EndDraw(DrawingContext context) { }
    }
}