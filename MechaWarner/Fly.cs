using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MechaWarner.Framework;

namespace MechaWarner
{
    public class Fly : GameObject
    {
        public Vector2 velocity;
        public float angle;
        public float speed;
        public float lifespan;

        public Fly(Vector2 Position, Vector2 Size) : base(Position, Size, 2)
		{
            angle = (float)Game1.rand.NextDouble() * MathHelper.TwoPi;
            speed = (float)Game1.rand.Next(100, 300);
            velocity.X = (float) Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;
            lifespan = 2;
		}

		public override void Update(float DT)
		{
            if (position.X > 480 + (size.X / 2))
                position.X -= 480 + (size.X / 2);
            else if (position.X < 0)
                position.X += 480 + (size.X / 2);
            else if (position.Y > 270 + (size.Y / 2))
                position.Y -= 270 + (size.Y / 2);
            else if (position.Y < 0)
                position.Y += 270 + (size.Y / 2);
            else
                position += velocity * DT;

            if (lifespan > 0)
                lifespan -= DT;
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    float bs = (float)Game1.rand.Next(100) + 50.0f;
                    float ba = (float)Game1.rand.NextDouble() * MathHelper.TwoPi;
                    Vector2 bv = new Vector2((float)Math.Cos(ba), (float)Math.Sin(ba)) * bs;
                    Game1.objectsToAdd.Add(new Bubble(position, bv));
                }
                Game1.objectsToRemove.Add(this);
            }
		}

		public override void Render()
		{
            RenderManager.DrawSprite("fly_normal", position, size, angle + MathHelper.PiOver2, Color.White, 0.9f);
		}

        public void eat()
        {
            Game1.warner.health += 1;
            Game1.objectsToRemove.Add(this);
        }
    }
}
