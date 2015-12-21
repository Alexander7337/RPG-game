using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TheGame
{
    public class Gravity
    {
        private const float GRAVITYVALUE = 1;
        private float gMultiplier;


        public Gravity()
        {
            this.GMultiplier = 0;
        }

        public float GMultiplier { get; set; }



        public void ApplyGravity(GameObject gameObject)
        {
            float gFactor = GRAVITYVALUE * GMultiplier;

            float lastXPosition = gameObject.Position.X;

            if (gameObject.Position.Y < 400)
            {
                if (gameObject.IsCollided == false)
                {
                    GMultiplier += 0.5f;


                    gameObject.Position = Vector2.Add(gameObject.Position, new Vector2(0, gFactor));

                    gameObject.Position = (gameObject.Position.Y >= 400)
                        ? new Vector2(lastXPosition, 400)
                        : gameObject.Position;
                }
                else
                {
                    gameObject.Position = Vector2.Add(gameObject.Position, new Vector2(0, 2));

                    //gameObject.Position = (gameObject.Position.Y >= 400)
                    //    ? new Vector2(lastXPosition, 400)
                    //    : gameObject.Position;
                }
            }
            else
            {
                GMultiplier = 0;
            }
        }


    }
}
