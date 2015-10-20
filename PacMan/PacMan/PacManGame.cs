using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PacMan
{
    
    public class PacManGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        public enum GameState { StartMenu}
        GameState CurrentState = GameState.StartMenu;

        public PacManGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        
        protected override void Initialize()
        {
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            
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
            }

            

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (CurrentState)
            {
                case GameState.StartMenu:
                    break;
            }
            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
