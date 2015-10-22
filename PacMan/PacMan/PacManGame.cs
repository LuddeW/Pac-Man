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
        
        public enum GameState { StartMenu, HighScore}
        GameState CurrentState = GameState.StartMenu;

        StartMenu StartMenu;

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
            
            
        }

        protected void CreatScreens()
        {
            StartMenu = new StartMenu(this, menuFont);
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
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
