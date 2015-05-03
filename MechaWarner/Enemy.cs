﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MechaWarner.Framework;

namespace MechaWarner
{
    public class Enemy : GameObject
    {
        public Vector2 velocity;
        public float angle;
        public float speed;
        public Enemy(Vector2 Position, Vector2 Size) : base(Position, Size, 2)
		{
            angle = (float)Game1.rand.NextDouble() * MathHelper.TwoPi;
            speed = (float)Game1.rand.Next(1, 200);
            velocity.X = (float) Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;
		}

		public override void Update(float DT)
		{
            position += velocity * DT;
		}

		public override void Render()
		{
            RenderManager.DrawSprite("turtle_normal", position, size, angle, Color.White, 0);
		}
    }
}