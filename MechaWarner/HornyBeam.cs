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
	public class HornyBeam : GameObject
	{
		public Vector2 velocity;
		public float angle;

		public HornyBeam(Vector2 Position, float Angle) : base(Position, new Vector2(15), -1)
		{
			angle = Angle;
			velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * 500.0f;
		}

		public override void Update(float DT)
		{
			position += velocity * DT;

			// Collisions
			foreach (GameObject g in Game1.gameObjects)
			{
				if (typeof(Enemy).Equals(g.GetType()))
				{
					if (Vector2.Distance(position, g.position) < (g.size.X / 2 - 6))
					{
						(g as Enemy).attack();
						Game1.objectsToRemove.Add(this);
						break;
					}
				}
			}

            if (position.X > 480 || position.Y > 270)
                Game1.objectsToRemove.Add(this);
		}

		public override void Render()
		{
			RenderManager.DrawSprite("horny_beam", position, size, angle + MathHelper.PiOver2, Color.White, 0.0f);
		}
	}
}
