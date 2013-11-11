using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOBA.Characters.Prototype;
using MOBA.Input;
using MOBA.World;

namespace MOBA.Characters.Controller
{
    /*Handles input and 'controls' the local player*/
    public class PlayerController : Controller
    {
        public Player entity;
        private Vector2 targetPos;

        public PlayerController(Main main) : base(main)
        {
        }

        public string info()
        {
            return player.Bounds.ToString();
        }

        public override void plugEntity(Player e)
        {
            entity = e;

            entity.setPosition(300, 200);
            entity.light = new LightEmitter(Main.lightEngine, entity.Position, 150, 0);
            Main.lightEngine.plugEmitter(entity.light);
            targetPos = entity.Position;

            base.plugEntity(entity);
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.mouseHeld(MouseButton.Right))
            {
                targetPos.X = (int)InputHandler.EventX;
                targetPos.Y = (int)InputHandler.EventY;
            }

            player.Pathfind((int)targetPos.X, (int)targetPos.Y);

            player.Update(gameTime);
            base.Update(gameTime);
        }

        public void Draw()
        {
            player.Draw(main.spriteBatch);
        }

    }
}
