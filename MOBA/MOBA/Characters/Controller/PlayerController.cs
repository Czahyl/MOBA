﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MOBA.Characters.Prototype;
using MOBA.Input;

namespace MOBA.Characters.Controller
{
    /*Handles input and 'controls' the local player*/
    public class PlayerController : Controller
    {
        private InputHandler input = new InputHandler();

        public PlayerController(Main main) : base(main)
        {

        }

        public Player getPlayer()
        {
            return player;
        }

        public string info()
        {
            return player.Bounds.ToString();
        }

        public override void plugEntity(Player entity)
        {
            entity.setPosition((game.WIDTH / 2) - (Player.WIDTH / 2), (game.HEIGHT / 2) - (Player.HEIGHT / 2));
            entity.Lock();
            base.plugEntity(entity);
        }

        public override void Update(GameTime time)
        {
            input.Listen();
            player.Update();
            input.Flush();
            base.Update(time);
        }

    }
}
