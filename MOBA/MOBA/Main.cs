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
using MOBA.Assets;
using MOBA.Characters.Controller;
using MOBA.Characters.Classes;
using MOBA.Characters.Prototype;
using MOBA.World;
using MOBA.Input;

namespace MOBA
{
    public class Main : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public static AssetManager assets;

        public PlayerController controller;
        public MinionController testMinion;

        public Map map;
        public LightEngine lightEngine;

        public static int WIDTH, HEIGHT;

        SpriteFont font;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;

            WIDTH = graphics.PreferredBackBufferWidth;
            HEIGHT = graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            assets = new AssetManager();
            map = new Map(this);
            lightEngine = new LightEngine(this);
            controller = new PlayerController(this);
            testMinion = new MinionController(this);
            controller.plugEntity(new Wizard());
            testMinion.plugEntity(new Minion());

            //lightEngine.plugEmitter(new LightEmitter(lightEngine, WIDTH / 2, HEIGHT / 2, 100, true, 0));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            assets.storeTexture(Content.Load<Texture2D>("Misc/Rect"), new Rectangle(0, 0, 1, 1));    // ID 0
            assets.storeTexture(Content.Load<Texture2D>("Enviroment/Grass"), new Rectangle(0, 0, 64, 64)); // ID 1
            assets.storeTexture(Content.Load<Texture2D>("Enviroment/Tree"), new Rectangle(0, 0, 96, 192)); // ID 2
            assets.storeTexture(Content.Load<Texture2D>("Misc/WizAuto"), new Rectangle(0, 0, 27, 14)); // ID 3 
            font = Content.Load<SpriteFont>("Font/Arial");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            InputHandler.Listen();

            map.Update();

            controller.Update(gameTime);
            testMinion.Update(gameTime);

            lightEngine.Update();

            InputHandler.Flush();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            map.Draw();

            if (lightEngine.checkAllEmitters(controller.getPlayer().Bounds, controller.getPlayer().invisibilityLayer))
                spriteBatch.Draw(assets.getTexture(0).Texture, controller.getPlayer().Bounds, Color.White);

            spriteBatch.DrawString(font, "MainW - "+Main.WIDTH+"\nMainH - "+Main.HEIGHT+"\nCamX - "+Camera.X+"\nCamY - "+Camera.Y, new Vector2(0, 0), Color.White);

            spriteBatch.Draw(assets.getTexture(0).Texture, testMinion.getEntity().Bounds, Color.White);

            controller.Draw();

            lightEngine.Draw();
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
