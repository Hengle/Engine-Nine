﻿#region Copyright 2010 (c) Nightin Games
//=============================================================================
//
//  Copyright 2010 (c) Nightin Games. All Rights Reserved.
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
using Isles.Graphics.Vertices;
#endregion


namespace Isles.Graphics.ParticleEffects
{
    public sealed class PointSpriteBatch : IDisposable
    {
        #region Vertex
        internal struct Vertex
        {
            public Vector3 Position;
            public Color Color;
            public float Size;
            public float Rotation;

            public static int SizeInBytes = 4 * (3 + 1 + 1 + 1);
            public static VertexElement[] VertexElements = new VertexElement[]
            {
                new VertexElement(0, 0, VertexElementFormat.Vector3,
                    VertexElementMethod.Default, VertexElementUsage.Position, 0),
                new VertexElement(0, 12, VertexElementFormat.Color,
                    VertexElementMethod.Default, VertexElementUsage.Color, 0),
                new VertexElement(0, 16, VertexElementFormat.Single,
                    VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 0),
                new VertexElement(0, 20, VertexElementFormat.Single,
                    VertexElementMethod.Default, VertexElementUsage.TextureCoordinate, 1),
            };
        }
        #endregion


        #region Key
        internal struct Key
        {
            public Texture2D Texture;
            public EffectTechnique Technique;
        }
        #endregion


        private Matrix view;
        private Matrix projection;
        private bool hasBegin = false;
        private DynamicVertexBuffer vertices;
        private VertexDeclaration declaration;
        private Batch<Key, Vertex> batch;
        private Effect effect;
        private SpriteBlendMode blendMode;


        public GraphicsDevice GraphicsDevice { get; private set; }
        public bool IsDisposed { get; private set; }
        

        public PointSpriteBatch(GraphicsDevice graphics, int capacity)
        {
            if (capacity < 32)
                throw new ArgumentException("Capacity should be at least 32");

            GraphicsDevice = graphics;

            effect = new PointSpriteEffect(graphics);

            batch = new Batch<Key, Vertex>(capacity);

            vertices = new DynamicVertexBuffer(graphics, typeof(Vertex), capacity, BufferUsage.Points | BufferUsage.WriteOnly);

            declaration = new VertexDeclaration(graphics, Vertex.VertexElements);
        }


        public void Begin(SpriteBlendMode blendMode, Matrix view, Matrix projection)
        {
            if (IsDisposed)
                throw new ObjectDisposedException("PointSpriteBatch");

            hasBegin = true;

            this.view = view;
            this.projection = projection;
            this.blendMode = blendMode;

            batch.Clear();
        }


        public void Draw(Texture2D texture, Vector3 position, float size, float rotation, Color color)
        {
            if (!hasBegin)
                throw new InvalidOperationException("Begin must be called before end and draw calls");

            if (IsDisposed)
                throw new ObjectDisposedException("PointSpriteBatch");

            if (texture == null)
                throw new ArgumentNullException();


            Key key;
            Vertex vertex;

            key.Texture = texture;

            if (Math.Abs(rotation) <= float.Epsilon)
                key.Technique = effect.Techniques["NonRotatingParticles"];
            else
                key.Technique = effect.Techniques["RotatingParticles"];


            vertex.Position = position;
            vertex.Color = color;
            
            // Note that we scale texture coordinates in our shader
            vertex.Size = (float)(size * Math.Sqrt(2));
            vertex.Rotation = rotation;

            batch.Add(key, vertex);
        }


        public void End()
        {
            if (!hasBegin)
                throw new InvalidOperationException("Begin must be called before end and draw calls");

            if (IsDisposed)
                throw new ObjectDisposedException("PointSpriteBatch");

            hasBegin = false;

            if (batch.Count <= 0)
                return;

            RenderState renderState = GraphicsDevice.RenderState;

            // Enable point sprites.
            renderState.PointSpriteEnable = true;
            renderState.PointSizeMax = 256;

            // Set the alpha blend mode.
            renderState.SetSpriteBlendMode(blendMode);

            // Set the alpha test mode.
            renderState.AlphaTestEnable = true;
            renderState.AlphaFunction = CompareFunction.Greater;
            renderState.ReferenceAlpha = 0;

            // Enable the depth buffer (so particles will not be visible through
            // solid objects like the ground plane), but disable depth writes
            // (so particles will not obscure other particles).
            renderState.DepthBufferEnable = true;
            renderState.DepthBufferWriteEnable = false;

            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);
            effect.Parameters["ViewportHeight"].SetValue(GraphicsDevice.Viewport.Height);


            foreach (BatchItem<Key> batchItem in batch.Batches)
            {
                vertices.SetData<Vertex>(
                    batch.Values, batchItem.StartIndex, batchItem.Count, SetDataOptions.None);

                GraphicsDevice.Vertices[0].SetSource(vertices, 0, Vertex.SizeInBytes);
                GraphicsDevice.VertexDeclaration = declaration;

                effect.Parameters["Texture"].SetValue(batchItem.Key.Texture);

                effect.Begin();

                batchItem.Key.Technique.Passes[0].Begin();

                GraphicsDevice.DrawPrimitives(PrimitiveType.PointList, 0, batchItem.Count);
             
                batchItem.Key.Technique.Passes[0].End();

                effect.End();
                
                GraphicsDevice.Vertices[0].SetSource(null, 0, 0);
            }

            // Reset render states to default value
            renderState.PointSpriteEnable = false;
            renderState.AlphaBlendEnable = false;
            renderState.SourceBlend = Blend.SourceAlpha;
            renderState.DestinationBlend = Blend.InverseSourceAlpha;
            renderState.AlphaTestEnable = false;
            renderState.DepthBufferWriteEnable = true;
        }


        public void Dispose()
        {
            if (vertices != null)
                vertices.Dispose();

            if (declaration != null)
                declaration.Dispose();

            IsDisposed = true;
        }
    }
}