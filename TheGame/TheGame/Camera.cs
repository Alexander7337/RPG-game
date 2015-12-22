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
            if ((hero.Position.X < game1.currentLevelWidth - 540) && (hero.Position.X > 260))
            {
                centre.X = (hero.Position.X + hero.Rectangle.Width / 2) - 280;
            }


            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * (Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0)));

        }





    }
}
