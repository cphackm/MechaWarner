using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MechaWarner.Framework
{
	public abstract class GameObject
	{
		public Vector2 position;
		public Vector2 size;
		public int health;

		public GameObject(Vector2 Position, Vector2 Size, int Health)
		{
			this.position = Position;
			this.size = Size;
			this.health = Health;
		}

		public abstract void Update(float DT);

		public abstract void Render();
	}
}
