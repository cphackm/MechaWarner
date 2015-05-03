using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MechaWarner.Framework;

namespace MechaWarner
{
	public class Warner : GameObject
	{
		public const float ROT_SPEED = MathHelper.Pi;
		public Vector2 velocity;
		public float angle;

		public Warner(Vector2 Position) : base(Position, new Vector2(33, 33), 5)
		{
			velocity = Vector2.Zero;
			angle = 0.0f;
		}

		public override void Update(float DT)
		{
			// Grab the keyboard state
			KeyboardState k = Keyboard.GetState();

			// Rotation
			if (k.IsKeyDown(Keys.Left))
			{
				angle += 
			}

			position += velocity * DT;
		}

		public override void Render()
		{
			RenderManager.DrawSprite("warner_normal", position, size, angle + MathHelper.PiOver2, Color.White, 0.0f);
		}
	}
}
