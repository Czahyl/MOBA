using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MOBA.Input
{
    public static class InputHandler
    {
        private static KeyboardState oldstate = Keyboard.GetState();
        private static MouseState oldmouse = Mouse.GetState();
        private static KeyboardState keystate;
        private static MouseState mouse;

        public static Vector2 worldPosition
        { get; private set; }

        public static  MouseButton? EventButton
        { get; set; }

        public static Keys? EventKey
        { get; set; }

        public static int EventX;

        public static int EventY;

        public static void Listen()
        {
            keystate = Keyboard.GetState();
            mouse = Mouse.GetState();

            EventButton = MouseButton.None;

            worldPosition = Vector2.Transform(new Vector2(mouse.X, mouse.Y), Matrix.Invert(Main.cam.Transform));

            EventX = (int)worldPosition.X;
            EventY = (int)worldPosition.Y;


            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (keystate.IsKeyDown(key) && oldstate.IsKeyUp(key))
                {
                    EventKey = key;
                }
            }

            if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                EventButton = MouseButton.Left;
                EventX = (int)worldPosition.X;
                EventY = (int)worldPosition.Y;
            }

            if (mouse.RightButton == ButtonState.Pressed && oldmouse.RightButton == ButtonState.Released)
            {
                EventButton = MouseButton.Right;
                EventX = (int)worldPosition.X;
                EventY = (int)worldPosition.Y;
            }
        }

        public static bool keyPressed(Keys key)
        {
            return keystate.IsKeyDown(key) && oldstate.IsKeyUp(key);
        }

        public static bool mouseHeld(MouseButton button)
        {
            if (button == MouseButton.Left)
            {
                EventX = (int)worldPosition.X;
                EventY = (int)worldPosition.Y;

                return mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Pressed;
            }
            else if (button == MouseButton.Right)
            {
                EventX = (int)worldPosition.X;
                EventY = (int)worldPosition.Y;

                return mouse.RightButton == ButtonState.Pressed && oldmouse.RightButton == ButtonState.Pressed;
            }

            return false;
        }

        public static void Flush()
        {
            oldstate = keystate;
            oldmouse = mouse;
        }
    }

    public enum MouseButton { None, Middle, Left, Right }
}
