#region Copyright 2009 - 2010 (c) Engine Nine
//=============================================================================
//
//  Copyright 2009 - 2010 (c) Engine Nine. All Rights Reserved.
//
//=============================================================================
#endregion

#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nine;
using Nine.Graphics;
using Nine.Graphics.Effects;
using Nine.Graphics.ScreenEffects;
#endregion

namespace ShadowMapping
{
    /// <summary>
    /// Sample game showing how to draw shadows using shadow mapping.
    /// </summary>
    public class ShadowMappingGame : Microsoft.Xna.Framework.Game
    {
        SpriteBatch spriteBatch;

        Model model;
        Model terrain;
        Model skyBox;
        ModelBatch modelBatch;

        ModelViewerCamera camera;

        DepthEffect depth;
        ShadowEffect shadow;
        SkyBoxEffect skyBoxEffect;
        ShadowMap shadowMap;


        public ShadowMappingGame()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this);

            graphics.SynchronizeWithVerticalRetrace = false;
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            IsFixedTimeStep = false;
            Components.Add(new FrameRate(this, "Consolas"));
            Components.Add(new InputComponent(Window.Handle));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            modelBatch = new ModelBatch(GraphicsDevice);

            // Create a model viewer camera to help us visualize the scene
            camera = new ModelViewerCamera(GraphicsDevice);

            // Load our model assert.
            model = Content.Load<Model>("dude");
            terrain = Content.Load<Model>("Terrain");

            // Create skybox.
            skyBox = Content.Load<Model>("cube");
            skyBoxEffect = new SkyBoxEffect(GraphicsDevice);
            skyBoxEffect.Texture = Content.Load<TextureCube>("SkyCubeMap");


            // Create shadow map related effects, depth is used to generate shadow maps,
            // shadow is used to draw a shadow receiver with a shadow map.
            shadowMap = new ShadowMap(GraphicsDevice, 256);
            depth = new DepthEffect(GraphicsDevice);
            shadow = new ShadowEffect(GraphicsDevice);
            shadow.EnableDefaultLighting();


            // Enable shadowmap bluring, this will produce smoothed shadow edge.
            // You can increase the shadow quality by using a larger shadowmap resolution (like 1024 or 2048),
            // and increase the blur samples.
            //shadowMap.Blur.SampleCount = BlurEffect.MaxSampleCount;
            shadowMap.BlurEnabled = true;
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        protected override void Draw(GameTime gameTime)
        {
            // Light view and light projection defines a light frustum.
            // Shadows will be projected based on this frustom.
            shadow.LightView = Matrix.CreateLookAt(new Vector3(10, 10, 30), Vector3.Zero, Vector3.UnitZ);
            shadow.LightProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.Pi / 2.5f, 1, 8.0f, 80.0f);


            // These two matrices are to adjust the position of the two models
            Matrix worldModel = Matrix.CreateScale(0.3f) * Matrix.CreateRotationX(MathHelper.PiOver2);
            Matrix worldTerrain = Matrix.CreateTranslation(-5, 0, 1) * Matrix.CreateScale(10f) * Matrix.CreateRotationX(MathHelper.PiOver2);


            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;


            // We need to draw the shadow casters on to a render target.
            shadowMap.Begin();

            // Clear everything to white. This is required.
            GraphicsDevice.Clear(Color.White);

            GraphicsDevice.BlendState = BlendState.Opaque;

            // Draw shadow casters using depth effect with the matrices set to light view and projection.
            modelBatch.Begin(shadow.LightView, shadow.LightProjection);
            modelBatch.Draw(model, worldModel, depth);
            modelBatch.End();

            // We got a shadow map rendered.
            shadow.ShadowMap = shadowMap.End();


            // Now we begin drawing real scene.
            GraphicsDevice.Clear(Color.DarkSlateGray);

            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            // This value needs to be tweaked
            shadow.DepthBias = 0.005f;

            modelBatch.Begin(camera.View, camera.Projection);

            // Draw skybox
            //modelBatch.Draw(skyBox, Matrix.Identity, skyBoxEffect);

            // Draw all shadow receivers with the shadow effect
            modelBatch.Draw(model, worldModel, shadow);
            modelBatch.Draw(terrain, worldTerrain, shadow);
            modelBatch.End();


            // Draw shadow map
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                spriteBatch.Begin();
                spriteBatch.Draw(shadow.ShadowMap, Vector2.Zero, Color.White);
                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
