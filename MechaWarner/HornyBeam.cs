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
		}

		public override void Render()
		{
			RenderManager.DrawSprite("horny_beam", position, size, angle + MathHelper.PiOver2, Color.White, 0.0f);
		}
	}
}
