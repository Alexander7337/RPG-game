using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{
    public class Skeleton : Enemy
    {
        private const int SkeletonRange = 400;

        public Skeleton(string name, Vector2 position, ContentManager Content, CollisionHandler collisionHandler)
            : base(
                Content.Load<Texture2D>("character"), position, name,
                10, 10,collisionHandler)
        {
            this.Health = 100;
            this.Lives = 10;
            this.CollisionGroup = CollisionGroup.Skeleton;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            this.Name = name;
            this.CollisionHandler = collisionHandler;
            this.MoveSpeed = 1;
            this.Range = SkeletonRange;

        }


        //public static Texture2D SkeletonTexture { get; set; }
        //public static Rectangle SkeletonRectangle { get; set; }


         public override void Update(KeyboardState presentKey, KeyboardState pastKey)
        {
             if (this.Health <= 0)
             {             
                 this.Exists = false;

                 this.Rectangle = new Rectangle(0,0,0,0);
             }

             Ai();
        }


        public override void Move(KeyboardState presentKey, KeyboardState pastkey)
        {
            Ai();
        }



        public void Ai()
        {
            if (Math.Abs(this.Position.X - Hero.Position.X) < this.Range)
            {
                if (Hero.Position.X > this.Position.X)
                {
                    MoveRight();

                    

                }
                if (Hero.Position.X < this.Position.X)
                {
                    MoveLeft();
                    
                }

                if (Math.Abs(this.Position.X - Hero.Position.X) < 5)
                {
                    Attack(this.CollisionHandler);
                }
            } 

           


        }

        private void MoveLeft()
        {
            this.Position = Vector2.Add(this.Position, (new Vector2(-(this.MoveSpeed), 0)));
        }

        private void MoveRight()
        {
            this.Position = Vector2.Add(this.Position, (new Vector2(this.MoveSpeed, 0)));
        }

        public override void Animate()
        {
            //throw new NotImplementedException();
        }

        public override void Load(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>("character");
        }

        public override void Attack(CollisionHandler collisionHandler)
        {
           // throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }
    }
}
