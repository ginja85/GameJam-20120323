using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Growth.Rendering;
using Growth.GameObjects;
using Growth.Input;
using Growth.Cameras;

namespace Growth
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class GrowthGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Renderer renderer;
        MouseWorldInput mouseInput;
        CameraStack cameraStack;
        Ship ship;

        public GrowthGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cameraStack = new CameraStack(new NullCamera(GraphicsDevice));
            renderer = new Renderer(GraphicsDevice, cameraStack);

            mouseInput = new MouseWorldInput(GraphicsDevice, cameraStack);

            ship = new Ship();            
            ship.Texture = Content.Load<Texture2D>("Sprites\\Ship");
            
            renderer.Ship = ship;
            cameraStack.PushCamera(new FollowCamera(GraphicsDevice) { Ship = ship });            
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            
            cameraStack.Update(gameTime.ElapsedGameTime.TotalSeconds);            
            ship.Update(gameTime.ElapsedGameTime.TotalSeconds, mouseInput);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            renderer.Render();

            base.Draw(gameTime);
        }
    }
}
