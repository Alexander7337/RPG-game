using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    public enum Heroclass
    {
        CodeWizard,
        CodeJedi,
        Nakov
    }
    public abstract class Hero : Character
    {
        private Heroclass heroClass;
        //private Rectangle rectangle;

        public Hero(Texture2D newTexture, Vector2 position, string name, double damage, int moveSpeed, Heroclass heroclass, CollisionHandler collisionHandler)
            : base(newTexture, position, name, damage, moveSpeed,collisionHandler)
        {
            this.heroClass = heroClass;
            this.Rectangle = new Rectangle((int) this.Position.X, (int) this.Position.Y, 50, 100);
        }


        //public Rectangle Rectangle { get; set; }

        //public override void Update(KeyboardState presentKey, KeyboardState pastKey)
        //{
        //    //Move();
        //    //Animate();

        //}


    }
}
