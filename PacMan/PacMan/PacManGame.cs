using PacMan.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacMan
{
    
    public class PacManGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public enum GameState { StartMenu, HighScore, GameScreen}
        GameState CurrentState = GameState.GameScreen;

        StartMenu startMenu;
        GameScreen gameScreen;

        SpriteFont menuFont;

        public PacManGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 500;
            graphics.PreferredBackBufferHeight = 515;
            graphics.ApplyChanges();
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadFonts();
            CreatScreens();
            gameScreen.LoadPictures();
            
            
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

            switch (CurrentState)
            {
                case GameState.StartMenu:

                    break;

                case GameState.HighScore:

                    break;

                case GameState.GameScreen:
                    gameScreen.Update(gameTime);
                    break;
            }

            

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            switch (CurrentState)
            {
                case GameState.StartMenu:
                    
                    break;

                case GameState.HighScore:

                    break;

                case GameState.GameScreen:
                    gameScreen.Draw(spriteBatch);
                    break;
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
