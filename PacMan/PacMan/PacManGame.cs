using PacMan.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacMan
{
    
    public class PacManGame : Game
    {
        public const int TILE_SIZE = 40;
        const int HUD_HEIGHT = 40;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public enum GameState { StartMenu, HighScore, GameScreen, LevelEditor}
        GameState CurrentState = GameState.StartMenu;
        
        StartMenu startMenu;
        GameScreen gameScreen;

        SpriteFont menuFont;

        //KeyboardState prevKeyState;

        public PacManGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = TILE_SIZE * 15;
            graphics.PreferredBackBufferHeight = TILE_SIZE * 15 + HUD_HEIGHT;
            graphics.ApplyChanges();
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadFonts();
            CreatScreens();
            gameScreen.Load();
            startMenu.LoadPictures();
        }

        protected void CreatScreens()
        {
            startMenu = new StartMenu(this, menuFont);
            gameScreen = new GameScreen(this);
        }

        protected void LoadFonts()
        {
            menuFont = Content.Load<SpriteFont>(@"MenuFont");
        }
 
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //KeyboardState keyboardState = Keyboard.GetState();
            switch (CurrentState)
            {
                case GameState.StartMenu:
                    startMenu.Update();
                    break;

                case GameState.HighScore:

                    break;

                case GameState.GameScreen:
                    gameScreen.Update(gameTime);
                    break;
                case GameState.LevelEditor:

                    break;
            }
            //prevKeyState = keyboardState;
            

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (CurrentState)
            {
                case GameState.StartMenu:
                    startMenu.Draw(spriteBatch);
                    break;

                case GameState.HighScore:

                    break;

                case GameState.GameScreen:
                    gameScreen.Draw(spriteBatch);
                    break;
                case GameState.LevelEditor:

                    break;
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }

        public void SetScreen(GameState GameState)
        {
            CurrentState = GameState;
        }
    }
}
