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
using System.IO;

namespace MOBA
{
    public class Camera
    {
        public static int X { get; private set; }
        public static int Y { get; private set; }

        public Matrix Transform;
        public Viewport viewport;

        bool paused = false;

        public Camera(Viewport viewport)
        {
            X = 0;
            Y = 0;

            Transform = Matrix.Identity;
            this.viewport = viewport;
        }

        public void Translate(int x, int y)
        {
            if (!paused)
            {
                X += x;
                Y += y;
            }
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Translate(0, 2);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Translate(0, -2);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Translate(2, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Translate(-2, 0);

            Transform = Matrix.CreateTranslation(X, Y, 0);
        }

        public void Pause()
        {
            paused = true;
        }

        public void unPause()
        {
            paused = false;
        }
    }

    public class Map
    {
        const int width = 2048;
        const int height = 1280;

        public Camera cam;

        Main m;

        public Map(Main main)
        {
            m = main;

            cam = new Camera(new Viewport(0, 0, Main.WIDTH, Main.HEIGHT));
        }

        public void Update()
        {
            cam.Update();
        }

        public void Draw()
        {
            for (int x = 0 - width / 2; x < width / 2; x += 64)
            {
                for (int y = 0; y < height; y += 64)
                {
                    m.spriteBatch.Draw(Main.assets.getTexture(1).Texture, new Rectangle(x, y, 64, 64), Color.White);
                }
            }
        }
    }
}
