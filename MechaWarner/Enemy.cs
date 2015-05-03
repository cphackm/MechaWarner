using System;
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
        public int health;
        public bool alreadySplit = false;
        public Enemy(Vector2 Position, Vector2 Size) : base(Position, Size, 2)
		{
            angle = (float)Game1.rand.NextDouble() * MathHelper.TwoPi;
            speed = (float)Game1.rand.Next(1, 200);
            velocity.X = (float) Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;
            health = 2;
		}

        public Enemy(Vector2 Position, Vector2 Size, bool alreadySplit)
            : base(Position, Size, 2)
        {
            angle = (float)Game1.rand.NextDouble() * MathHelper.TwoPi;
            speed = (float)Game1.rand.Next(1, 200);
            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = (float)Math.Sin(angle) * speed;
            health = 2;
            this.alreadySplit = alreadySplit;
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

            if(Vector2.Distance(position, Game1.warner.position) < 30)
            {
                if (!Game1.warner.isInvincible)
                {
                    Game1.warner.health -= 1;
                    Game1.warner.isInvincible = true;
                }
            }
		}

		public override void Render()
		{
            RenderManager.DrawSprite("turtle_normal", position, size, angle + MathHelper.PiOver2, Color.White, 1);
		}

        public void attack()
        {
            health -= 1;
            if(health < 0)
            {
                if (!alreadySplit)
                {
                    Game1.objectsToAdd.Add(new Enemy(position, size / 2, true));
                    Game1.objectsToAdd.Add(new Enemy(position, size / 2, true));
                    Game1.objectsToAdd.Add(new Enemy(position, size / 2, true));
                    Game1.objectsToRemove.Add(this);
                }
                else
                    Game1.objectsToRemove.Add(this);
            }
        }
    }
}
