using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MOBA.Input
{
    public delegate void keyPressHandle(Keys k);
    public delegate void keyUpHandle(Keys k);
    public delegate void clickHandle(MouseButton button, int x, int y);

    public class InputHandler
    {
        private KeyboardState oldstate = Keyboard.GetState();
        private MouseState oldmouse = Mouse.GetState();

        public event keyPressHandle handleKeypress;
        public event keyUpHandle handleKeyup;
        public event clickHandle handleClick;

        public void Listen()
        {
            KeyboardState keystate = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (keystate.IsKeyDown(key) && oldstate.IsKeyUp(key))
                {
                    onKeypress(key);
                }

                if (keystate.IsKeyUp(key) && oldstate.IsKeyDown(key))
                {
                    onKeyup(key);
                }
            }

            if (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released)
            {
                onClick(MouseButton.Left, mouse.X, mouse.Y);
            }

            if (mouse.RightButton == ButtonState.Pressed && oldmouse.RightButton == ButtonState.Released)
            {
                onClick(MouseButton.Right, mouse.X, mouse.Y);
            }

            oldstate = keystate;
            oldmouse = mouse;
        }

        protected virtual void onKeyup(Keys key)
        {

        }

        protected virtual void onKeypress(Keys key)
        {

        }

        protected virtual void onClick(MouseButton button, int x, int y)
        {
            
        }
    }

    public enum MouseButton { Left, Right }
}
