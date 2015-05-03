﻿using System;
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
		public const float ROT_SPEED = MathHelper.Pi * 1.5f;
		public const float ACC_SPEED = 150.0f;
		public const float MAX_SPEED = 200.0f;
		public Vector2 velocity;
		public float angle;
        public Boolean isInvincible;
        float timer;

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
				angle -= ROT_SPEED * DT;
			}
			else if (k.IsKeyDown(Keys.Right))
			{
				angle += ROT_SPEED * DT;
			}

			// Is acceleration
			bool isAccelerating = false;

			// Acceleration
			if (k.IsKeyDown(Keys.Up))
			{
				velocity += new Vector2((float)Math.Cos(angle) * ACC_SPEED, (float)Math.Sin(angle) * ACC_SPEED) * DT;
				isAccelerating = true;
			}

			// Cap the speed
			if (velocity.Length() > MAX_SPEED)
			{
				velocity.Normalize();
				velocity *= MAX_SPEED;
			}

			position += velocity * DT;

			// Wrap his bombin' position
			if (position.X > 480 + size.X / 2 || position.X < -(size.X / 2))
			{
				position.X -= Math.Sign(position.X) * (480 + size.X / 2);
			}
			if (position.Y > 270 + size.Y / 2 || position.Y < -(size.Y / 2))
			{
				position.Y -= Math.Sign(position.Y) * (270 + size.Y / 2);
			}

			// Create a bubble trail
			if (isAccelerating)
			{
				float addAngle = (float)Game1.rand.Next(-30, 30) / 90.0f;
				Vector2 bVel = new Vector2((float)Math.Cos(angle + addAngle + MathHelper.Pi), (float)Math.Sin(angle + addAngle + MathHelper.Pi)) * 100.0f;
				Vector2 bPos = position + new Vector2(16.0f * (float)Math.Cos(angle + MathHelper.Pi), 16.0f * (float)Math.Sin(angle + MathHelper.Pi));
				Game1.objectsToAdd.Add(new Bubble(bPos, bVel));
			}

            if(isInvincible)
            {
                timer -= DT;
                if (timer < 0)
                {
                    isInvincible = false;
                    timer = 1;
                }

            }

		}

		public override void Render()
		{
			RenderManager.DrawSprite("warner_normal", position, size, angle + MathHelper.PiOver2, Color.White, 0.0f);
		}
	}
}
