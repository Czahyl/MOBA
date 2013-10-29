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
using MOBA.Input;

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

                Transform = Matrix.CreateTranslation(new Vector3(X, Y, 0));
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

            if (InputHandler.mouseX <= 10)
                Translate(2, 0);
            if (InputHandler.mouseX >= Main.WIDTH - 10)
                Translate(-2, 0);
            if (InputHandler.mouseY <= 10)
                Translate(0, 2);
            if (InputHandler.mouseY >= Main.HEIGHT - 10)
                Translate(0, -2);
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

        Main m;

        public Map(Main main)
        {
            m = main;
        }

        public void Update()
        {
            
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
