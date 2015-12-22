using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheGame.Models;

namespace TheGame
{
    public enum CollisionGroup
    {
        CodeWizard,
        CodeJedi,
        Skeleton,
        Ground,
        Other
    }


    public class CollisionHandler
    {
        List<GameObject> gameObjects;
        private List<Character> gameCharacters;
        private Dictionary<GameObject, Vector2> previousPositions;
        private Hero hero;
        private Game1 game1;

        public CollisionHandler(Hero hero, Game1 game1)
        {
            this.Hero = hero;
            this.GameObjects = new List<GameObject>();
            this.PreviousPositions = new Dictionary<GameObject, Vector2>();
            this.GameCharacters = new List<Character>();
            this.Game1 = game1;
        }

        public List<GameObject> GameObjects
        {
            get { return this.gameObjects; }
            set { this.gameObjects = value; }
        }

        public List<Character> GameCharacters
        {
            get { return this.gameCharacters; }
            set { this.gameCharacters = value; }
        }

        public Game1 Game1
        {
            get { return this.game1; }
            set { this.game1 = value; }
        }

        public Dictionary<GameObject, Vector2> PreviousPositions
        {
            get { return this.previousPositions; }
            set { this.previousPositions = value; }
        }

        public Hero Hero
        {
            get { return this.hero; }
            set { this.hero = value; }
        }


        public void HandleCollisions(Hero hero)
        {
            this.Hero = hero;

            if (Hero != null)
            {
                if (Hero.Position.X < 0 || Hero.Position.X > Game1.currentLevelWidth - 40)
                {
                    Hero.Position = PreviousPositions[Hero];
                }
            }


            foreach (GameObject gameObject in GameObjects)
            {
                if (gameCharacters.Count > 0)
                {
                    gameCharacters.Clear();
                }

                if (gameObject is Character)
                {
                    gameCharacters.Add(gameObject as Character);
                }


                if (Hero != null)
                {
                    Hero.IsCollided = false;


                    if (Hero.Rectangle.Intersects(gameObject.Rectangle))
                    {
                        Hero.Position = previousPositions[Hero];
                        gameObject.Position = previousPositions[gameObject];

                        Hero.IsCollided = true;
                        gameObject.IsCollided = true;
                    }
                    else
                    {

                        gameObject.IsCollided = false;
                    }

                }

            }


            









        }

        public void GetCurrentPositions(Hero hero)
        {
            this.Hero = hero;

            foreach (GameObject gameObject in GameObjects)
            {
                //if ((gameObject is R2D2))
                //{
                //    foreach (GameObject magic in gameObjects)
                //    {
                //        //TODO: ...
                //    }
                //}


                
                if (gameObject != null && Hero != null)
                {
                    if (!(gameObject.Rectangle.Intersects(Hero.Rectangle)))
                    {                       
                        previousPositions[gameObject] = gameObject.Position;
                    }

                    if (!(Hero.Rectangle.Intersects(gameObject.Rectangle)))
                    {
                        previousPositions[Hero] = Hero.Position;
                    }
                    
                }

                
            }

            
        }
    }
}
