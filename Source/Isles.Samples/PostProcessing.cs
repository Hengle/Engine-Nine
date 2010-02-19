﻿#region Copyright 2009 (c) Nightin Games
//=============================================================================
//
//  Copyright 2009 (c) Nightin Games. All Rights Reserved.
//
//=============================================================================
#endregion


#region Using Directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Isles.Components;
using Isles.Graphics;
using Isles.Graphics.Cameras;
using Isles.Graphics.Primitives;
using Isles.Graphics.Landscape;
using Isles.Graphics.Models;
using Isles.Graphics.Filters;
#endregion


namespace Isles.Samples
{
    [SampleClass]
    public class PostProcessingGame : BasicModelViewerGame
    {
        public FilterCollection PostEffects { get; set; }
        public IFilter Filter { get; set; }

        SpriteBatch sprite;
        Texture2D texture;

        FFTWave wave;

        protected override void LoadContent()
        {
            // Chainning post processing effects
            PostEffects = new FilterCollection();


            //Filter = new SaturationFilter(1.0f);            
            Filter = new HeightmapFilter();


            sprite = new SpriteBatch(GraphicsDevice);
            //texture = Content.Load<Texture2D>("Textures/glacier");

            wave = new FFTWave(GraphicsDevice, 128, 1, 1.0f, Vector2.One, 6.0f, SurfaceFormat.Color);
            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            GraphicsDevice.Clear(Color.Black);

            wave.Update(gameTime);

            sprite.Begin();
            sprite.Draw(wave.Heightmap, Vector2.Zero, Color.White);
            //sprite.Draw(wave.Heightmap, GraphicsDevice.Viewport.TitleSafeArea, Color.White);
            sprite.End();

            //Filter.Draw(GraphicsDevice, wave.Heightmap, null);
        }
    
        [SampleMethod("Post Processing Sample", IsStartup=true)]
        public static void Test()
        {
            using (PostProcessingGame game = new PostProcessingGame())
            {
                game.Run();
            }
        }
    }
}
