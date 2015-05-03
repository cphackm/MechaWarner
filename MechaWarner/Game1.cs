#region Using Statements
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
    /// 
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
        public static Random rand = new Random();
        double oldTurtleTime;
        Boolean oldTurtleTimeSet = false;
        double oldFlyTime;
        Boolean oldFlyTimeSet = false;

		// Delta time - used for framerate independence.
		float dt;

		// List of game objects to keep track of
		public static List<GameObject> gameObjects;
		public static List<GameObject> objectsToAdd;
		public static List<GameObject> objectsToRemove;
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
			objectsToAdd = new List<GameObject>();
			objectsToRemove = new List<GameObject>();

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
            RenderManager.LoadTexture("turtle", "turtle_normal");
            RenderManager.LoadTexture("fly", "fly_normal");
			RenderManager.LoadTexture("matingcall", "horny_beam");
			RenderManager.LoadTexture("mechatongue", "mechatongue");
            RenderManager.LoadTexture("mechasand", "mechasand");
            RenderManager.LoadTexture("mecharocks1", "mecharocks1");
            RenderManager.LoadTexture("mecharocks2", "mecharocks2");
            RenderManager.LoadTexture("mecharocks3", "mecharocks3");
            RenderManager.LoadTexture("mecharocks4", "mecharocks4");
            RenderManager.LoadTexture("mecharocks5", "mecharocks5");
            RenderManager.LoadFont("pixelFont", "pixel_font");
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
			foreach (GameObject g in objectsToAdd)
			{
				gameObjects.Add(g);
			}
			foreach (GameObject g in objectsToRemove)
			{
				gameObjects.Remove(g);
			}
			objectsToAdd.Clear();

            //create turtles
            if (!oldTurtleTimeSet)
            {
                oldTurtleTime = gameTime.TotalGameTime.TotalMilliseconds;
                oldTurtleTimeSet = true;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds - oldTurtleTime > 1000)
            {
                Vector2[] corners = { new Vector2(0, 0), new Vector2(0, 270), new Vector2(480, 0), new Vector2(480, 270) };
                Enemy e = new Enemy(corners[rand.Next(0, 3)], new Vector2(64, 64));
                gameObjects.Add(e);
                oldTurtleTime = gameTime.TotalGameTime.TotalMilliseconds;
            }

            if (!oldFlyTimeSet)
            {
                oldFlyTime = gameTime.TotalGameTime.TotalMilliseconds;
                oldFlyTimeSet = true;
            }
            if (gameTime.TotalGameTime.TotalMilliseconds - oldFlyTime > 10000)
            {
                Vector2[] corners = { new Vector2(0, 0), new Vector2(0, 270), new Vector2(480, 0), new Vector2(480, 270) };
                Fly f = new Fly(corners[rand.Next(0, 3)], new Vector2(9, 9));
                gameObjects.Add(f);
                oldFlyTime = gameTime.TotalGameTime.TotalMilliseconds;
            }


			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Begin the sprite batch operation
			GraphicsDevice.SetRenderTarget(smallTarget);
            GraphicsDevice.Clear(Color.White);

			RenderManager.sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null);

            //Draw background
            for (int i = 0; i <= (480 / 32) + 1; i++)
            {
                for (int j = 0; j <= (270 / 32) + 1; j++)
                {
                    RenderManager.DrawSprite("mechasand", new Vector2(i * 32, j * 32), new Vector2(32, 32), 0, (Color.CornflowerBlue)*0.75f, 1);
                }
            }

            RenderManager.DrawSprite("mecharocks1", new Vector2(30, 30), new Vector2(64, 64), 0, Color.White, 0.99f);
            RenderManager.DrawSprite("mecharocks2", new Vector2(100, 250), new Vector2(128, 128), 0, Color.White, 0.99f);
            RenderManager.DrawSprite("mecharocks3", new Vector2(346, 20), new Vector2(128, 128), 0, Color.White, 0.99f);
            RenderManager.DrawSprite("mecharocks4", new Vector2(259, 102), new Vector2(32, 32), 0, Color.White, 0.99f);
            RenderManager.DrawSprite("mecharocks5", new Vector2(380, 240), new Vector2(64, 64), 0, Color.White, 0.99f);

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
			for (int i = 0; i < 135; i++)
			{
				int ripple = (int)(Math.Cos((i + gameTime.TotalGameTime.TotalMilliseconds / 20.0) / 2.0) * 4);
				RenderManager.sb.Draw(smallTarget, new Rectangle(ripple, i * 4, 960, 4), new Rectangle(0, i * 2, 480, 2), Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                if (warner.health < 1)
                    gameOver();
			}
			//RenderManager.sb.Draw(smallTarget, new Rectangle(0, 0, 960, 540), Color.White);
			RenderManager.DrawFont("pixel_font", Vector2.Zero, "Warner's Health: " + warner.health, Color.White, 0, 1.0f);
			RenderManager.sb.End();

			base.Draw(gameTime);
		}

        public static void gameOver()
        {
            Vector2 size = (RenderManager.fonts["pixel_font"].MeasureString("GAME OVER"))*2;
            RenderManager.DrawFont("pixel_font", new Vector2((960 / 2) - (size.X / 2), (540 / 2) - (size.Y / 2)), "GAME OVER", Color.White, 0, 2.0f);
        }
	}
}
