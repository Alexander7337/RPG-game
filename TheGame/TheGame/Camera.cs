using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 centre;


       


        public Camera(Viewport newView)
        {
            view = newView;
        }


        public void Update(GameTime gameTime, Hero hero, Game1 game1)
        {
            //centre = new Vector2((hero.Position.X + hero.Rectangle.Width / 2) - 400, (hero.Position.Y + hero.Rectangle.Height / 2) - 450);

            if ((hero.Position.X < game1.currentLevelWidth - 540) && (hero.Position.X > 260))
            {
                centre.X = (hero.Position.X + hero.Rectangle.Width / 2) - 280;
            }

            //if (hero.Position.Y < 360 && (hero.Position.Y > 100))
            //{
            //    centre.Y = (hero.Position.Y + hero.Rectangle.Height / 2) - 250;
            //}






            //if (ship.spritePosition.X > ship.GraphicsDevice.Viewport.Width - 200)
            //{
            //    centre.X = ship.GraphicsDevice.Viewport.Width - 800;
            //}

            transform =
                Matrix.CreateScale(new Vector3(1, 1, 0)) * (Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)));

        }





    }
}
