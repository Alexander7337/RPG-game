using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame
{
    public abstract class Character : GameObject
    {
        private string name;
        
        private double damage;
        private int moveSpeed;
        private int lives;

        private Texture2D moveLeft;
        private Texture2D moveRight;

        private float elapsed;
        private float delay = 200;
        private int frames = 0;

        private Texture2D currentAnim;
        private Rectangle sourceRectangle;

        private bool isAttacking;


        protected Character(Texture2D newTexture, Vector2 position, string name, double damage, int moveSpeed, CollisionHandler collisionHandler)
            : base(newTexture, position, collisionHandler)
        {
            this.Name = name;
            this.Damage = damage;
            this.MoveSpeed = moveSpeed;
            this.IsAttacking = false;
            
        }


        public string Name { get; set; }
        
        public double Damage { get; set; }
        public int MoveSpeed { get; set; }
        public int Lives { get; set; }
        public bool IsAttacking { get; set; }

        public int Frames
        {
            get { return this.frames; }
            set { this.frames = value; }
        }

        public float Delay
        {
            get { return this.delay; }
            set { this.delay = value; }
        }

        public float Elapsed
        {
            get { return this.elapsed; }
            set { this.elapsed = value; }
        }
        

        public Texture2D MoveLeft
        {
            get { return this.moveLeft; }
            set { this.moveLeft = value; }
        }

        public Texture2D MoveRight
        {
            get { return this.moveRight; }
            set { this.moveRight = value; }
        }

        public Texture2D CurrentAnim
        {
            get { return this.currentAnim; }
            set { this.currentAnim = value; }
        }

        public Rectangle SourceRectangle
        {
            get { return this.sourceRectangle; }
            set { this.sourceRectangle = value; }
        }




        public abstract void Attack(CollisionHandler collisionHandler);

        public abstract void Move(KeyboardState presentKey, KeyboardState pastKey, GameTime gameTime);
        public abstract void Animate(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);



        public override void Update(KeyboardState presentKey, KeyboardState pastKey, GameTime gameTime)
        {
            CheckHealth();
        }

        private void CheckHealth()
        {
            if (this.Health <= 0)
            {
                CollisionHandler.GameObjects.Remove(this);
            }
        }

       
    }
}
