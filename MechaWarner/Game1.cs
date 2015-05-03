﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using MechaWarner.Framework;
#endregion

namespace MechaWarner
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		// Delta time - used for framerate independence.
		float dt;

		// List of game objects to keep track of
		public static List<GameObject> gameObjects;
		public static Warner warner;

		// Render target for scaling
		RenderTarget2D smallTarget;

		public Game1() : base()
		{
			// Create the graphics and content
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 960;
			graphics.PreferredBackBufferHeight = 540;
			Content.RootDirectory = "Content";

			// Create the list of game objects
			gameObjects = new List<GameObject>();

			// Create warner
			warner = new Warner(new Vector2(240, 135));
			gameObjects.Add(warner);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here
			RenderManager.Initialize(GraphicsDevice, this.Content);

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// Create the small target
			smallTarget = new RenderTarget2D(GraphicsDevice, 480, 270);

			// Load content here
			RenderManager.LoadTexture("mechawarner", "warner_normal");
			RenderManager.LoadTexture("sprite_bubblelarge", "bubble_large");
			RenderManager.LoadTexture("sprite_bubblesmall", "bubble_small");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			// Update the delta time
			dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

			// Update all game objects
			foreach (GameObject g in gameObjects)
			{
				g.Update(dt);
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			// Begin the sprite batch operation
			GraphicsDevice.SetRenderTarget(smallTarget);
			GraphicsDevice.Clear(Color.DarkBlue);
			RenderManager.sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

			// Draw all game objects
			foreach (GameObject g in gameObjects)
			{
				g.Render();
			}

			// End the sprite batch operation
			RenderManager.sb.End();

			// Draw the small target scaled up onto the back buffer
			GraphicsDevice.SetRenderTarget(null);
			RenderManager.sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);
			RenderManager.sb.Draw(smallTarget, new Rectangle(0, 0, 960, 540), Color.White);
			RenderManager.sb.End();

			base.Draw(gameTime);
		}
	}
}
