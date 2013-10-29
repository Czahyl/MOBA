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
        public static PlayerController controller; // Local player
        public static List<Controller> Players = new List<Controller>(); // Connected players
        public static List<MinionController> Minions = new List<MinionController>();
       
        public static GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public static AssetManager assets;

        public Map map;
        public static Camera cam;
        public static LightEngine lightEngine;

        public static int WIDTH, HEIGHT;

        SpriteFont font;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;

            WIDTH = graphics.PreferredBackBufferWidth;
            HEIGHT = graphics.PreferredBackBufferHeight;
        }

        protected override void Initialize()
        {
            assets = new AssetManager();
            map = new Map(this);

            cam = new Camera(new Viewport(0, 0, WIDTH, HEIGHT));

            lightEngine = new LightEngine(this, WIDTH, HEIGHT);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            assets.storeImage(Content.Load<Texture2D>("Misc/Rect"), new Rectangle(0, 0, 1, 1));    // ID 0
            assets.storeImage(Content.Load<Texture2D>("Enviroment/Grass"), new Rectangle(0, 0, 64, 64)); // ID 1
            assets.storeImage(Content.Load<Texture2D>("Enviroment/Tree"), new Rectangle(0, 0, 96, 192)); // ID 2
            assets.storeImage(Content.Load<Texture2D>("Misc/WizAuto"), new Rectangle(0, 0, 27, 14)); // ID 3 
            font = Content.Load<SpriteFont>("Font/Arial");

            controller = new PlayerController(this);
            Minions.Add(new MinionController(this));

            controller.plugEntity(new Wizard());

            for (int i = 0; i < Minions.Count; i++)
                Minions[i].plugEntity(new Minion());
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
            cam.Update();

            controller.Update(gameTime);

            for (int i = 0; i < Minions.Count; i++)
                Minions[i].Update(gameTime);

            lightEngine.Update();

            InputHandler.Flush();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, cam.Transform);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Viewport = cam.viewport;

            map.Draw();

            //if (lightEngine.inLight(controller.getPlayer().Rect(), controller.getPlayer().invisibilityLayer))
            spriteBatch.Draw(assets.getTexture(0).Texture, controller.getPlayer().Rect(), Color.White);

            spriteBatch.DrawString(font, "X - " + InputHandler.worldPosition.X + "\nY - " + InputHandler.worldPosition.Y, new Vector2(0, 0), Color.White);
            //spriteBatch.DrawString(font, 0 + Minions[0].getEntity().Health.ToString(), new Vector2(0, 50), Color.White);

            for (int i = 0; i < Minions.Count; i++)
                Minions[i].getEntity().Draw(spriteBatch);

            controller.Draw();
            spriteBatch.End();

            spriteBatch.Begin(); // Draw off-camera objects

            lightEngine.Draw();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
