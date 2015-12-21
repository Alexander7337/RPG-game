using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TheGame.Models
{
    class R2D2
    {
        private Texture2D texture;
        private Vector2 position;
        private bool moveLeft;
        private ContentManager Content;
        public R2D2(Texture2D newTexture, Vector2 Position, ContentManager Content)
        {
            this.Texture = newTexture;
            position = Position;
            moveLeft = true;
            this.Position = Position;
        }
        public Texture2D Texture
        {
            get { return this.texture; }
            set { this.texture = value; }
        }
        public Vector2 Position
        {
            get { return this.position; }
            set { this.position = value; }
        }


        public void Move()
        {
            if (Position.X > 300)
            {
                Position = Vector2.Add(Position, new Vector2(-3, 0));


            }
        }

        public void Load()
        {
            this.Texture = Content.Load<Texture2D>("0");
            this.Texture = Content.Load<Texture2D>("1");
            this.Texture = Content.Load<Texture2D>("2");
            this.Texture = Content.Load<Texture2D>("3");
            this.Texture = Content.Load<Texture2D>("4");
            this.Texture = Content.Load<Texture2D>("5");
            this.Texture = Content.Load<Texture2D>("6");
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, 120, 220), Color.White);
        }
    }
}