using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Activation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TheGame.Models;

namespace TheGame
{
    public enum GameState
    {
        Start,
        MainMenu,
        CutScene,
        Options,
        Level1,
        Level2,
        Level3,
        Level4,
        BossBattle,
        EndMenu,
        OtherMenu
    }

    enum ChosenClass
    {
        CharacterSelect,
        CodeWizard,
        CodeJedi
    }
    
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Character class & Game state...
        GameState currentGameState = GameState.Start;
        private ChosenClass chooseClass;
        private int gameStartDelay = 300;
        

        //Screen Adjustment
        public   int screenWidth = 800;
        public   int screenHeight = 600;

        private CButton playButton;
        private CButton btnStart;
        private ChoseClassButton btnWizard;
        private ChoseClassButton btnJedi;

        public int currentLevelWidth;

        private Camera camera;

        //Keyboard
        public KeyboardState presentKey;
        public KeyboardState pastKey;

        //Sounds
        public SoundEffect introSong;
        private Song mainSong;
        private Song mainSongAlternate;

        //Characters
        private Hero hero;
        private CodeJedi codeJedi;
        private CodeWizard codeWizard;
        private Vector2 currentPosition;
        private Vector2 R2D2Position;
        private R2D2 r2d2;
        private SpriteFont font;
        bool isTimerOn = false;
        decimal counter = 5000; 

        private Skeleton skeleton1;
        private Skeleton skeleton2;
        private Skeleton skeleton3;
        private Skeleton skeleton4;
        private Skeleton skeleton5;
        private Skeleton skeleton6;
        //private Skeleton skeleton7;
        //private Skeleton skeleton8;
        //private Skeleton skeleton9;
        //private Skeleton skeleton10;

        private CollisionHandler collisionHandler;
        struct DisplayMessage
        {
            public string Message;
            public TimeSpan DisplayTime;
            public int CurrentIndex;
            public Vector2 Position;
            public string DrawnMessage;
            public Color DrawColor;

            public DisplayMessage(string message, TimeSpan displayTime, Vector2 position, Color color)
            {
                Message = message;
                DisplayTime = displayTime;
                CurrentIndex = 0;
                Position = position;
                DrawnMessage = string.Empty;
                DrawColor = color;
            }
        }
        List<DisplayMessage> messages = new List<DisplayMessage>();


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //codeWizard = new CodeWizard("Pesho", new Vector2(100, 400), Content);//////

            camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Screen
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();
          
            IsMouseVisible = true;

            introSong = Content.Load<SoundEffect>("login");
            introSong.Play();
            mainSong = Content.Load<Song>("ImperialMarch");
            mainSongAlternate = Content.Load<Song>("REMIX");

            btnStart = new CButton(Content.Load<Texture2D>("Begin"), graphics.GraphicsDevice);
            btnStart.setPosition(new Vector2(330, 210));
            btnStart.size = new Vector2(150,150);
            playButton = new CButton(Content.Load<Texture2D>("play_button"),graphics.GraphicsDevice);
            playButton.setPosition(new Vector2(300, 370));
            btnWizard = new ChoseClassButton(Content.Load<Texture2D>("CodeWizard"), graphics.GraphicsDevice);
            btnWizard.setPosition(new Vector2(10, 120));
            btnJedi = new ChoseClassButton(Content.Load<Texture2D>("CodeJedi"), graphics.GraphicsDevice);
            btnJedi.setPosition(new Vector2(560, 130));
            currentPosition = new Vector2(100,400);
            R2D2Position = new Vector2(750, 320);
            font = Content.Load<SpriteFont>("font");

            r2d2 = new R2D2(Content.Load<Texture2D>("0"), R2D2Position, Content);
            skeleton1 = new Skeleton("Jim", new Vector2(300, 400), Content, collisionHandler);
            skeleton2 = new Skeleton("John", new Vector2(600, 400), Content, collisionHandler);
            skeleton3 = new Skeleton("Chuck", new Vector2(1200, 400), Content, collisionHandler);
            skeleton4 = new Skeleton("Arnold", new Vector2(1800, 400), Content, collisionHandler);
            //skeleton5 = new Skeleton("Silvester", new Vector2(2400, 400), Content, collisionHandler);
            //skeleton6 = new Skeleton("Jason", new Vector2(3000, 400), Content, collisionHandler);
            //skeleton = new Skeleton("Jet", new Vector2(600, 400), Content, collisionHandler);
            //skeleton = new Skeleton("Dolph", new Vector2(600, 400), Content, collisionHandler);
            //skeleton = new Skeleton("bob", new Vector2(600, 400), Content, collisionHandler);



            collisionHandler = new CollisionHandler(hero);

            collisionHandler.GameObjects.Add(skeleton1);
            collisionHandler.GameObjects.Add(skeleton2);


            collisionHandler.GameObjects.Add(skeleton3);

            collisionHandler.GameObjects.Add(skeleton4);
            //collisionHandler.GameObjects.Add(skeleton5);
            //collisionHandler.GameObjects.Add(skeleton6);

            

            


            //if (currentGameState == GameState.Level1)
            //{
                
            //}

            
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            collisionHandler.GetCurrentPositions(hero);

            float elapsed = (float) gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouse = Mouse.GetState();
            presentKey = Keyboard.GetState();


            switch (currentGameState)
            {
                case GameState.Start:
                    if (gameStartDelay <= 0)
                    {
                        if (btnStart.isClicked)
                        {
                            //MediaPlayer.Play(mainSong);
                            MediaPlayer.Play(mainSongAlternate);
                            currentGameState = GameState.MainMenu;
                        }
                        btnStart.Update(mouse);
                    }
                    gameStartDelay--;
                    break;
                case GameState.MainMenu:
                   
                    MainMenuUpdate(mouse);
                    break;
                case GameState.CutScene:
                    if (r2d2 != null)
                    {
                        r2d2.Move();
                    }

                    InputManager.Update();
                    if (Content.Load<Texture2D>("DialogBox") != null)
                    {
                        messages.Add(new DisplayMessage("Help me student!\nEnemies have infested\nSoftuni.\nYou must defeat them!", TimeSpan.FromSeconds(60.0), new Vector2(356, 215), Color.Lime));
                    }
                    UpdateMessages(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        currentGameState = GameState.Options;
                    break;
                case GameState.Options:
                    if (btnWizard.isClicked == true)
                    {
                        chooseClass = ChosenClass.CodeWizard;
                        hero = new CodeWizard("Pesho", currentPosition, Content,collisionHandler);
                        //skeleton1.Hero = hero;
                        //skeleton2.Hero = hero;
                        //skeleton3.Hero = hero;
                        //skeleton4.Hero = hero;
                        foreach (Skeleton gameObject in collisionHandler.GameObjects)
                        {
                            gameObject.Hero = hero;
                        }
                        currentGameState = GameState.Level1;
                    }
                    
                    if (btnJedi.isClicked == true)
                    {
                        chooseClass = ChosenClass.CodeJedi;
                        hero = new CodeJedi("Gosho", currentPosition, Content, collisionHandler);
                        //skeleton1.Hero = hero;
                        //skeleton2.Hero = hero;
                        //skeleton3.Hero = hero;
                        //skeleton4.Hero = hero;
                        foreach (Skeleton gameObject in collisionHandler.GameObjects)
                        {
                            gameObject.Hero = hero;
                        }
                        currentGameState = GameState.Level1;
                    }

                    btnWizard.Update(mouse);
                    btnJedi.Update(mouse);
                    break;


                case GameState.Level1:
                    Level1Update(elapsed);
                    break;
            }


            if (hero != null)
            {
                camera.Update(gameTime, hero, this);
            }
            //if (codeWizard != null)
            //{
            //    camera.Update(gameTime, codeWizard, this);
            //}
            //else if (codeJedi != null)
            //{
            //    camera.Update(gameTime, codeJedi, this);
            //}


            collisionHandler.HandleCollisions(hero);

            pastKey = presentKey;
            base.Update(gameTime);
        }

        private void Level1Update(float elapsed)
        {
            // LevelWidth -> Important for camera. 
            // If lvl texture is changed, it needs to be changed here as well
            currentLevelWidth = Content.Load<Texture2D>("map").Width + Content.Load<Texture2D>("map").Width;

            //if (codeWizard == null)
            //{
            //    codeWizard = new CodeWizard("Pesho", currentPosition, Content);///////////////
            //}

            //codeWizard.Position = currentPosition;   // not needed

            hero.Update(presentKey, pastKey);

           
            DecideUpdate(skeleton1);
            DecideUpdate(skeleton2);
            DecideUpdate(skeleton3);
            DecideUpdate(skeleton4);



            ////// THE LIST ...
            for (int i = 0; i < collisionHandler.GameObjects.Count; i++)
            {
                GameObject gameObject = collisionHandler.GameObjects[i];

                if (gameObject.Exists)
                {
                    gameObject.Update(presentKey, pastKey);
                }
                else
                {
                    collisionHandler.GameObjects.Remove(gameObject);
                }
            }


            //foreach (GameObject gameObject in collisionHandler.GameObjects)
            //{
            //    if (gameObject.Exists)
            //    {
            //        gameObject.Update(presentKey, pastKey);

            //    }
            //    else
            //    {
            //        collisionHandler.GameObjects.Remove(gameObject);
            //    }

            //}


            
            
        }

        private void DecideUpdate(Skeleton skeleton)
        {
            if (skeleton.Exists)
            {
                skeleton.Update(presentKey, pastKey);
            }
        }



        private void MainMenuUpdate(MouseState mouse)
        {
            if (playButton.isClicked == true)
            {
                currentGameState = GameState.CutScene;
            }
            playButton.Update(mouse);

        }

        

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
           // spriteBatch.Begin();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            switch (currentGameState)
            {
                case GameState.Start:
                    spriteBatch.Begin();
                    if (gameStartDelay <= 0)
                    {
                        btnStart.Draw(spriteBatch);
                    }
                    break;
                case GameState.MainMenu:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("otherbackground"),
                        new Rectangle(0, 0, screenWidth, screenHeight),
                        Color.White);
                    playButton.Draw(spriteBatch);
                    break;
                case GameState.CutScene:
                    spriteBatch.Begin();
                    CutSceneDraw();
                    if (r2d2 == null)
                    {
                        isTimerOn = true;

                    }
                    if (isTimerOn)
                    {
                        counter -= gameTime.ElapsedGameTime.Milliseconds;
                        if (counter <= 5000)
                            spriteBatch.Draw(Content.Load<Texture2D>("0"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 4500)
                            spriteBatch.Draw(Content.Load<Texture2D>("1"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 4200)
                            spriteBatch.Draw(Content.Load<Texture2D>("2"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 3900)
                            spriteBatch.Draw(Content.Load<Texture2D>("3"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 3700)
                            spriteBatch.Draw(Content.Load<Texture2D>("4"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 3400)
                            spriteBatch.Draw(Content.Load<Texture2D>("5"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if (counter <= 3100)
                            spriteBatch.Draw(Content.Load<Texture2D>("6"), new Rectangle(300, 320, 120, 220),
                                Color.White);
                        if(counter <=2600)
                            spriteBatch.Draw(Content.Load<Texture2D>("DialogBox"), new Rectangle(350, 200, 300, 150),
                                Color.White);
                        if (counter <= 2000)
                        DrawMessages();

                    }
                    break;
                case GameState.Options:
                    spriteBatch.Begin();
                    spriteBatch.Draw(Content.Load<Texture2D>("minimal-class"),
                        new Rectangle(0, 0, screenWidth, screenHeight), Color.White);

                    btnWizard.Draw(spriteBatch);
                    // spriteBatch.Draw(Content.Load<Texture2D>("CodeWizard"), new Rectangle(0, 100, 100, 150), Color.White);

                    btnJedi.Draw(spriteBatch);
                    //spriteBatch.Draw(Content.Load<Texture2D>("CodeJedi"), new Rectangle(101, 100, 100, 150), Color.White);

                    break;


                case GameState.Level1:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
                    Level1Draw();
                    
                    break;
                default:
                    spriteBatch.Begin();
                    break;
            }



            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void CutSceneDraw()
        {

            spriteBatch.Draw(Content.Load<Texture2D>("CutSceneRoom"), new Rectangle(0, 0, Content.Load<Texture2D>("CutSceneRoom").Width, screenHeight), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("starec"), new Rectangle(110, 295, 200, 270), Color.White);
            if (r2d2 != null && 300 < r2d2.Position.X)
            {
                r2d2.Draw(spriteBatch);
            }
            if (r2d2 != null && 300 == r2d2.Position.X)
            {
                r2d2 = null;
            }

        }

        private void Level1Draw()
        {
            spriteBatch.Draw(Content.Load<Texture2D>("map"), new Rectangle(0, 0, Content.Load<Texture2D>("map").Width*2, Content.Load<Texture2D>("map").Height), Color.White);
            
            if (hero != null)
            {
                hero.Draw(spriteBatch);
            }


            //DecideDraw(skeleton1);
            //DecideDraw(skeleton2);
            //DecideDraw(skeleton3);
            //DecideDraw(skeleton4);


            foreach (GameObject gameObject in collisionHandler.GameObjects)
            {
                if (gameObject.Exists)
                {
                    gameObject.Draw(spriteBatch);
                }

            }
        }

        private void DecideDraw(Skeleton skeleton)
        {
            if (skeleton.Exists)
            {
                skeleton.Draw(spriteBatch);
            }
        }

        void UpdateMessages(GameTime gameTime)
        {
            if (messages.Count > 0)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    DisplayMessage dm = messages[i];
                    dm.DisplayTime -= gameTime.ElapsedGameTime;
                    if (dm.DisplayTime <= TimeSpan.Zero)
                    {
                        messages.RemoveAt(i);
                    }
                    else
                    {
                        messages[i] = dm;
                    }
                }
            }
        }
        void DrawMessages()
        {
            if (messages.Count > 0)
            {
                for (int i = 0; i < messages.Count; i++)
                {
                    DisplayMessage dm = messages[i];
                    dm.DrawnMessage += dm.Message[dm.CurrentIndex].ToString();
                    spriteBatch.DrawString(font, dm.DrawnMessage, dm.Position, dm.DrawColor);
                    if (dm.CurrentIndex != dm.Message.Length - 1)
                    {
                        dm.CurrentIndex++;
                        messages[i] = dm;
                    }
                }
            }
        }
    }
}