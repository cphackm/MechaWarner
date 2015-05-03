using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MechaWarner.Framework
{
	public class RenderManager
	{
		public static SpriteBatch sb;
		public static ContentManager cm;
		public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, SpriteFont> fonts;

		public static void Initialize(GraphicsDevice GD, ContentManager CM)
		{
			sb = new SpriteBatch(GD);
			textures = new Dictionary<string,Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
			cm = CM;
		}

		public static void LoadTexture(string Path, string Key)
		{
			textures.Add(Key, cm.Load<Texture2D>(Path));
		}

        public static void LoadFont(string Path, string Key)
        {
            fonts.Add(Key, cm.Load<SpriteFont>(Path));
        }

		public static void DrawSprite(string Key, Vector2 Position, Vector2 Size, float Angle, Color CColor, float Depth)
		{
			Texture2D tex = textures[Key];
			sb.Draw(
				tex,
				new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y),
				new Rectangle(0, 0, tex.Width, tex.Height),
				CColor,
				Angle,
				new Vector2((float)tex.Width / 2, (float)tex.Height / 2),
				SpriteEffects.None,
				Depth);
		}

        public static void DrawFont(string Key, Vector2 Position, String Message, Color CColor, float Depth, float Scale)
        {
            SpriteFont font = fonts[Key];
            sb.DrawString(
                font, 
                Message, 
                Position, 
                CColor, 
                0, 
                Vector2.Zero, 
                Scale, 
                SpriteEffects.None, 
                Depth);
        }
	}
}
