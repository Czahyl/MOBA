using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MOBA.Input
{
    public class InputHandler
    {
        private KeyboardState oldstate = Keyboard.GetState();
        private MouseState oldmouse = Mouse.GetState();
        private KeyboardState keystate;
        private MouseState mouse;

        public MouseButton? EventButton
        { get; protected set; }

        public Keys? EventKey
        { get; protected set; }

        public int? EventX
        { get; protected set; }

        public int? EventY
        { get; protected set; }

        public void Listen()
        {
            keystate = Keyboard.GetState();
            mouse = Mouse.GetState();

            EventButton = MouseButton.None;
            EventKey = null;
            EventX = null;
            EventY = null;

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
                EventX = mouse.X;
                EventY = mouse.Y;
            }

            if (mouse.RightButton == ButtonState.Pressed && oldmouse.RightButton == ButtonState.Released)
            {
                EventButton = MouseButton.Right;
                EventX = mouse.X;
                EventY = mouse.Y;
            }
        }

        public bool keyPressed(Keys key)
        {
            return keystate.IsKeyDown(key) && oldstate.IsKeyUp(key);
        }

        public void Flush()
        {
            oldstate = keystate;
            oldmouse = mouse;
        }
    }

    public enum MouseButton { None, Middle, Left, Right }
}
