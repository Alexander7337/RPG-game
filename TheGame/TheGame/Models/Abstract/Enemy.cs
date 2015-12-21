using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame 
{
    public abstract class Enemy : Character
    {
        private Hero hero;
        private int range;

        protected Enemy(Texture2D newTexture, Vector2 position, string name, double damage, int moveSpeed, CollisionHandler collisionHandler)
            : base(newTexture, position, name, damage, moveSpeed,collisionHandler)
        {
            this.Lives = 1;
            
        }

        public Hero Hero
        {
            get { return this.hero; }
            set { this.hero = value; }
        }

        public int Range
        {
            get { return this.range; }
            set { this.range = value; }
        }




        
    }
}
