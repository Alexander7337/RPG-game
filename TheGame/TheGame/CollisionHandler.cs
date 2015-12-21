using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
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

        public CollisionHandler(Hero hero)
        {
            this.Hero = hero;
            this.GameObjects = new List<GameObject>();
            this.PreviousPositions = new Dictionary<GameObject, Vector2>();
            this.GameCharacters = new List<Character>();
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
                    //Hero.IsCollided = false;
                    //gameObject.IsCollided = false;


                    //gameCharacters.Add(Hero);

                    Hero.IsCollided = false;

                    if (Hero.Rectangle.Intersects(gameObject.Rectangle))
                    {
                        Hero.Position = previousPositions[Hero];
                        gameObject.Position = previousPositions[gameObject];

                        Hero.IsCollided = true;
                        gameObject.IsCollided = true;

                        //if (Hero.isAttacking)
                        //{
                        //    gameObject.Health -= (int)Hero.Damage;
                        //}

                    }
                    else
                    {
                        
                        gameObject.IsCollided = false;
                    }

                }
                
            }


            //foreach (Character gameCharacter in gameCharacters)
            //{
            //    if (Hero != null)
            //    {
            //        //Hero.IsCollided = false;
            //        //gameCharacter.IsCollided = false;
            //        if (Hero.Rectangle.Intersects(gameCharacter.Rectangle) && Hero.IsAttacking)
            //        {
            //            gameCharacter.Health -= (int)Hero.Damage;
            //        }
            //        if (gameCharacter.Rectangle.Intersects(Hero.Rectangle) && gameCharacter.IsAttacking)
            //        {
            //            Hero.Health -= (int)gameCharacter.Damage;
            //        }
            //    }        
            //}










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
