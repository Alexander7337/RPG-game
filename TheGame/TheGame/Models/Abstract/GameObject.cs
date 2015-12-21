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
    public abstract class  GameObject //: Game1
    {
        private Texture2D texture;
        private Vector2 position;
        private Rectangle rectangle;
        private Gravity gravity;
        private CollisionGroup collisionGroup;
        private CollisionHandler collisionHandler;
        private bool isCollided;
        private int health;
        public int Health { get; set; }
        public bool exists;

        


        public GameObject(Texture2D newTexture, Vector2 position, CollisionHandler collisionHandler)
        {
            this.Texture = newTexture;
            this.Position = position;

            this.Gravity = new Gravity();
            this.CollisionGroup = CollisionGroup.Other;   // Other -> Default collision group. Actual must be specified in descendant classes
            this.IsCollided = false;
            this.CollisionHandler = collisionHandler;
            this.Exists = true;
        }

        public Gravity Gravity { get; set; }

        public bool IsCollided { get; set; }

        public CollisionHandler CollisionHandler
        {
            get { return this.collisionHandler; }
            set { this.collisionHandler = value; }
        }

        public CollisionGroup CollisionGroup
        {
            get { return this.collisionGroup; }
            set { this.collisionGroup = value; }
        }

        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }

        public Rectangle Rectangle
        {
            get { return this.rectangle; }
            set { this.rectangle = value; }
        }

        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public bool Exists
        {
            get { return this.exists; }
            set { this.exists = value; }
        }




        public abstract void Update(KeyboardState presentKey, KeyboardState pastKey);

        public abstract void Load(ContentManager Content);
        

        public void Draw(SpriteBatch spriteBatch)
        {
            this.Rectangle = new Rectangle((int)Position.X, (int)Position.Y, 50, 100);
            spriteBatch.Draw(this.Texture, new Rectangle((int)Position.X, (int)Position.Y, 50, 100), Color.White);
            
            
        }


    }
}
