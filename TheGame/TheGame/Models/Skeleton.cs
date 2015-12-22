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
                Content.Load<Texture2D>("skelet0"), position, name,
                10, 10, collisionHandler)
        {
            this.Health = 100;
            this.Lives = 10;
            this.CollisionGroup = CollisionGroup.Skeleton;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 75);
            this.Name = name;
            this.MoveSpeed = 1;
            this.Range = SkeletonRange;


        }

        public override void Update(KeyboardState presentKey, KeyboardState pastKey, GameTime gameTime)
        {
            if (this.Health <= 0)
            {
                this.Exists = false;

                this.Rectangle = new Rectangle(0, 0, 0, 0);
            }

            Move(presentKey, pastKey, gameTime);
        }


        public override void Move(KeyboardState presentKey, KeyboardState pastkey, GameTime gameTime)
        {
            Ai(gameTime);
        }



        public void Ai(GameTime gameTime)
        {
            if (Math.Abs(this.Position.X - Hero.Position.X) < this.Range)
            {
                if (Hero.Position.X > this.Position.X)
                {
                    SkeletonMoveRight();
                    this.CurrentAnim = MoveRight;
                    Animate(gameTime);



                }
                else if (Hero.Position.X < this.Position.X)
                {
                    SkeletonMoveLeft();
                    this.CurrentAnim = MoveLeft;
                    Animate(gameTime);
                }
                else
                {
                    this.SourceRectangle = new Rectangle(0, 0, 50, 75);
                }

                if (Math.Abs(this.Position.X - Hero.Position.X) < 5)
                {
                    Attack(this.CollisionHandler);
                }
            }

            else
            {
                this.SourceRectangle = new Rectangle(0, 0, 50, 75);
            }


            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 75);


        }

        private void SkeletonMoveLeft()
        {
            this.Position = Vector2.Add(this.Position, (new Vector2(-(this.MoveSpeed), 0)));
        }

        private void SkeletonMoveRight()
        {
            this.Position = Vector2.Add(this.Position, (new Vector2(this.MoveSpeed, 0)));
        }

        public override void Animate(GameTime gameTime)
        {
            this.Elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.Elapsed >= this.Delay)
            {
                if (this.Frames >= 9)
                {
                    this.Frames = 0;
                }
                else
                {
                    this.Frames++;
                }

                this.Elapsed = 0;

            }

            SourceRectangle = new Rectangle(50 * this.Frames, 0, 50, 75);
        }

        public override void Load(ContentManager Content)
        {
            this.MoveRight = Content.Load<Texture2D>("skelet3");
            this.MoveLeft = Content.Load<Texture2D>("skelet4");

            this.CurrentAnim = this.MoveRight;
        }

        public override void Attack(CollisionHandler collisionHandler)
        {
            // throw new NotImplementedException();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 75);

            spriteBatch.Draw(this.CurrentAnim, this.Rectangle, this.SourceRectangle, Color.White);
        }
    }
}
