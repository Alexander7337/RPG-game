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




        public abstract void Attack(CollisionHandler collisionHandler);

        public abstract void Move(KeyboardState presentKey, KeyboardState pastKey,GameTime gameTime);
        public abstract void Animate();
        public abstract void Draw(SpriteBatch spriteBatch);



        public override void Update(KeyboardState presentKey, KeyboardState pastKey)
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
