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
	public class Bubble : GameObject
	{
		public bool isLargeBubble;
		public Vector2 velocity;
		public float opacity;

		public Bubble(Vector2 Position, Vector2 Velocity) : base(Position, new Vector2(5), -1)
		{
			isLargeBubble = Game1.rand.Next(2) == 0 ? true : false;
			opacity = 1.0f;
			velocity = Velocity;
		}

		public override void Update(float DT)
		{
			opacity -= 1.0f * DT;
			if (opacity < 0.0f)
			{
				opacity = 0.0f;
				Game1.objectsToRemove.Add(this);
			}

			position += velocity * DT;
		}

		public override void Render()
		{
			RenderManager.DrawSprite(isLargeBubble ? "bubble_large" : "bubble_small", position, size, 0.0f, Color.White * opacity, 0.1f);
		}
	}
}
