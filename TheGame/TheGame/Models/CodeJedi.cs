using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{
    class CodeJedi : Hero
    {
        private const int WIZARDSPEED = 4;
        private const int JUMPHEIGHT = -3;

        private Vector2 velocity;   // not needed ???

        private int jumpCounter;




        public CodeJedi(string name, Vector2 position, ContentManager Content, CollisionHandler collisionHandler)
            : base(
                Content.Load<Texture2D>("CodeJedi"), position, name,
                10, 10, Heroclass.CodeJedi,collisionHandler)
        {
            this.Health = 100;
            this.Lives = 10;
            this.JumpCounter = 0;
            this.CollisionGroup = CollisionGroup.CodeJedi;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            this.Damage = 10;

        }


        public int JumpCounter { get; set; }
        

        


        public override void Animate()
        {
            //throw new NotImplementedException();
        }

        public override void Move(KeyboardState presentKey, KeyboardState pastKey)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.Position = Vector2.Add(Position, new Vector2(this.MoveSpeed, 0));
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.Position = Vector2.Add(Position, new Vector2(-this.MoveSpeed, 0));
            }
            if (presentKey.IsKeyDown(Keys.Up) && pastKey.IsKeyUp(Keys.Up))
            {
                this.JumpCounter = 10;

            }
            if (presentKey.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                this.IsAttacking = true;
                Attack(CollisionHandler);

            }

            Jump();

            Gravity.ApplyGravity(this);

            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
        }

        private void Jump()
        {
            int jumpFactor = JUMPHEIGHT * JumpCounter;

            if (JumpCounter > 0)
            {

                this.Position = Vector2.Add(Position, new Vector2(0, jumpFactor));

                JumpCounter--;
            }



        }


        public override void Update(KeyboardState presentKey, KeyboardState pastKey)
        {
            Move(presentKey, pastKey);
        }

        public override void Load(ContentManager Content)
        {
            this.Texture = Content.Load<Texture2D>("CodeJedi");
            
        }

        public override void Attack(CollisionHandler collisionHandler)
        {
            foreach (Character character in collisionHandler.GameCharacters)
            {
            //    if (this.Rectangle.Intersects(character.Rectangle))
            //    {
            //        character.Health -= (int)this.Damage;
            //        this.IsAttacking = false;
            //    }

                if (this.IsCollided && character.IsCollided)
                {
                    character.Health -= (int) this.Damage;
                    this.IsAttacking = false;

                }

            }


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Rectangle = new Rectangle((int) Position.X, (int) Position.Y, 50, 100);
           spriteBatch.Draw(this.Texture, this.Rectangle, Color.White);
        }
    }
}
