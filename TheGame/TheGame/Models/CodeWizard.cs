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
        //private static Texture2D codeWizardTexture;

        
        //private static Rectangle codeWizardRectangle;
        //private Vector2 codeWizardPosition;
        //private static int damage = 10;
        //private static int moveSpeed = 10;



        //public CodeWizard(string name)
        //    : base(codeWizardTexture, CodeWizardRectangle, name, damage, moveSpeed, Heroclass.CodeWizard)
        //{
        //    this.Health = 100;
        //    this.Lives = 10;
        //}

        private const int WIZARDSPEED = 4;
        private const int JUMPHEIGHT = -3;

        private Vector2 velocity;   // not needed ???

        private int jumpCounter;







        public CodeWizard(string name, Vector2 position, ContentManager Content, CollisionHandler collisionHandler)
            : base(
                Content.Load<Texture2D>("CodeWizard"), position, name,
                10, WIZARDSPEED, Heroclass.CodeWizard,collisionHandler)
        {
            this.Health = 100;
            this.Lives = 10;
            //this.Position = position;
            this.JumpCounter = 0;
            this.CollisionGroup = CollisionGroup.CodeWizard;
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            this.Damage = 10;


        }

        public Vector2 Velocity { get; set; }
        public int JumpCounter { get; set; }

        






        public override void Move(KeyboardState presentKey, KeyboardState pastKey)
        {
           // this.Velocity = new Vector2(0,0);

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

        //private void Accelerate(Vector2 direction, float force)
        //{
        //    Velocity += force*Vector2.Normalize(direction);
        //}

        public override void Update(KeyboardState presentKey, KeyboardState pastKey)
        {
            Move(presentKey, pastKey);

        }

        //private void ApplyGravity()
        //{
        //    if (this.Position.Y < 400)
        //    {
        //        //Accelerate(new Vector2(0,1),30 );
        //        this.Position = Vector2.Add(Position, new Vector2(0, 100));
        //    }

        //}

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
            this.IsAttacking = true;

            foreach (Character character in collisionHandler.GameCharacters)
            {
                //if (this.Rectangle.Intersects(character.Rectangle) && !character.Equals(this))
                //{
                //    character.Health -= (int)this.Damage;
                //    this.IsAttacking = false;

                //}
                if (this.IsCollided && character.IsCollided)
                {
                    character.Health -= (int)this.Damage;
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
