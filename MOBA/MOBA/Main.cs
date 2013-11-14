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
using System.Diagnostics;
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
        public static List<MultiplayerController> Players = new List<MultiplayerController>(); // Connected players
        public static List<MinionController> Minions = new List<MinionController>();
       
        public static GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;

        public static AssetManager Assets;
        public static Math.Trig Trig;

        public Map map;
        public static Camera Cam;
        public static Viewport defaultViewport;
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

            Mouse.WindowHandle = Window.Handle;

            defaultViewport = new Viewport(0, 0, WIDTH, HEIGHT);
        }

        protected override void Initialize()
        {
            Assets = new AssetManager();
            Trig = new Math.Trig();
            map = new Map(this);

            Cam = new Camera(new Viewport(0, 0, WIDTH, HEIGHT));

            lightEngine = new LightEngine(this, WIDTH, HEIGHT);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Assets.storeFont(Content.Load<SpriteFont>("Font/Nameplate"));

            Assets.storeImage(Content.Load<Texture2D>("Misc/Rect"), new Rectangle(0, 0, 1, 1));    // ID 0
            Assets.storeImage(Content.Load<Texture2D>("Enviroment/Grass"), new Rectangle(0, 0, 64, 64)); // ID 1
            Assets.storeImage(Content.Load<Texture2D>("Enviroment/Tree"), new Rectangle(0, 0, 96, 192)); // ID 2
            Assets.storeImage(Content.Load<Texture2D>("Misc/WizAuto"), new Rectangle(0, 0, 27, 14)); // ID 3 
            Assets.storeImage(Content.Load<Texture2D>("Misc/testrect"), new Rectangle(0, 0, 1, 1));    // ID 4
            Assets.storeImage(Content.Load<Texture2D>("Wizard/Idle"), new Rectangle(0, 0, 48, 60)); // ID 5
            Assets.storeImage(Content.Load<Texture2D>("Wizard/Attack"), new Rectangle(0, 0, 48, 60)); // ID 6
            Assets.storeImage(Content.Load<Texture2D>("Interface/Nameplate"), new Rectangle(0, 0, 80, 20)); // ID 7

            font = Content.Load<SpriteFont>("Font/Arial");

            controller = new PlayerController(this);
            Players.Add(new MultiplayerController(this));
            Minions.Add(new MinionController(this));

            controller.plugEntity(new Wizard("Admin", 0));

            for (int i = 0; i < Minions.Count; i++) // TODO: Make "Spawner" class
                Minions[i].plugEntity(new Minion(1));

            for (int i = 0; i < Players.Count; i++)
                Players[i].plugEntity(new Player("Player " + (i + 1), 0));
        }

        protected override void UnloadContent()
        {
            Assets.Dispose();
            Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            InputHandler.Listen();

            map.Update();
            Cam.Update();

            controller.Update(gameTime);

            for (int i = 0; i < Minions.Count; i++)
                Minions[i].Update(gameTime);

            for (int i = 0; i < Players.Count; i++)
                Players[i].Update(gameTime);

            lightEngine.Update();

            InputHandler.Flush();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Cam.Transform);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GraphicsDevice.Viewport = Cam.viewport;

            map.Draw();

            spriteBatch.DrawString(font, "X - " + InputHandler.worldPosition.X + "\nY - " + InputHandler.worldPosition.Y, new Vector2(0, 0), Color.White);

            for (int i = 0; i < Minions.Count; i++)
            {
                if (lightEngine.isVisible(Minions[i].entity))
                    Minions[i].Draw();
            }

            for (int i = 0; i < Players.Count; i++)
            {
                if (lightEngine.isVisible(Players[i].player))
                    Players[i].Draw();
            }

            controller.Draw();
            spriteBatch.End();

            spriteBatch.Begin(); // Draw off-camera objects

            lightEngine.Draw();

            spriteBatch.DrawString(Assets.getFont(0), gameTime.ElapsedGameTime.TotalSeconds.ToString(), new Vector2(100, 400), Color.White);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public bool inScreen(BaseEntity e)
        {
            if (defaultViewport.Bounds.Contains(new Point((int)e.Position.X, (int)e.Position.Y)))
                return true;

            return false;
        }

        public static Player hitPlayer(Rectangle rect)
        {
            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].player.Bounds.Intersects(rect))
                    return Players[i].player;
            }

            return null;
        }

        public static Minion hitMinion(Rectangle rect)
        {
            for (int i = 0; i < Minions.Count; i++)
            {
                if (Minions[i].entity.Bounds.Intersects(rect))
                    return Minions[i].entity;
            }

            return null;
        }
    }
}
