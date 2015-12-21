using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{
    

    class CodeWizard : Hero
    {
        
        private const int WIZARDSPEED = 4;
        private const int JUMPHEIGHT = -3;
        private int jumpCounter;
        Texture2D rightWalk, leftWalk, upWalk, currentAnim;
        Rectangle sourceRectangle;
        Rectangle destRecangle = new Rectangle(100, 100, 32, 47);
        float elapsed;
        float delay = 200f;
        int frames = 0;
        private KeyboardState ks;
        public CodeWizard(string name, Vector2 position, ContentManager Content, CollisionHandler collisionHandler)
            : base(
                Content.Load<Texture2D>("wizard1"), position, name,
                10, WIZARDSPEED, Heroclass.CodeWizard,collisionHandler)
        {
            this.Health = 100;
            this.Lives = 10;
            this.JumpCounter = 0;
            this.CollisionGroup = CollisionGroup.CodeWizard;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            this.Damage = 10;


        }

        public int JumpCounter { get; set; }

        
        public override void Move(KeyboardState presentKey, KeyboardState pastKey,GameTime gameTime)
        {
            ks = Keyboard.GetState();
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this.Position = Vector2.Add(Position, new Vector2(+2f, 0));
                currentAnim = rightWalk;
                Animate(gameTime);
               
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this.Position = Vector2.Add(Position, new Vector2(-2f, 0));
                currentAnim = leftWalk;
                Animate(gameTime);
            }
            if (presentKey.IsKeyDown(Keys.Up) && pastKey.IsKeyUp(Keys.Up))
            {
                currentAnim = upWalk;
                Animate(gameTime);
                this.JumpCounter = 10;
               
            }
            if (presentKey.IsKeyDown(Keys.Space) && pastKey.IsKeyUp(Keys.Space))
            {
                this.IsAttacking = true;
                Attack(CollisionHandler);

            }
            else
            {
                sourceRectangle = new Rectangle(0, 0, 32, 47);
            }
            
             
            Jump();

            Gravity.ApplyGravity(this);

            


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

        private void Animate(GameTime gameTime)
        {
            elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                if (frames > 3)
                {
                    frames = 0;
                }
                else
                {
                    frames++;
                }
                elapsed = 0;
            }

            sourceRectangle = new Rectangle(32 * frames, 0, 32, 47);
        }
        public override void Animate()
        {
            //throw new NotImplementedException();
        }


        


        public override void Load(ContentManager Content)
        {
            rightWalk = Content.Load<Texture2D>("wizard4");
            leftWalk = Content.Load<Texture2D>("wizard4");
            upWalk = Content.Load<Texture2D>("wizard3");
            currentAnim = Content.Load<Texture2D>("wizard1");
        }





        public override void Attack(CollisionHandler collisionHandler)
        {
            this.IsAttacking = true;

            foreach (Character character in collisionHandler.GameCharacters)
            {
               
                if (this.IsCollided && character.IsCollided)
                {
                    character.Health -= (int)this.Damage;
                    this.IsAttacking = false;

                }
            }


        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(currentAnim, destRecangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
