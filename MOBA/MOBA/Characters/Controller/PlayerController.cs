using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.Characters.Prototype;
using MOBA.World;
using MOBA.Input;
using MOBA.Characters.Classes.Spells;

namespace MOBA.Characters.Controller
{
    /*Handles input and 'controls' the local player*/
    public class PlayerController : Controller
    {
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
            player = e;

            player.setPosition(300, 200);
            player.light = new LightEmitter(Main.lightEngine, player.Position, 150, 0);
            Main.lightEngine.plugEmitter(player.light);
            targetPos = player.Position;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.mouseHeld(MouseButton.Right))
            {
                targetPos.X = (int)InputHandler.EventX;
                targetPos.Y = (int)InputHandler.EventY;
            }

            if (InputHandler.mouseHeld(MouseButton.Left) && player.canAttack)
            {
                if (player.attackDelay.Tick)
                {
                    player.currentAni = player.Animations["Attack"];
                    player.autoAttack.Add(new Autoattack(player.Position, player));
                }
            }
            else
                player.currentAni = player.Animations["Idle"];

            player.Pathfind((int)targetPos.X, (int)targetPos.Y);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            player.Draw(m.spriteBatch);
        }

    }
}
