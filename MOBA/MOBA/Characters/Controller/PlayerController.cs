using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MOBA.Characters.Prototype;

namespace MOBA.Characters.Controller
{
    public class PlayerController : Controller
    {
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
            player.Update();
            base.Update(time);
        }

    }
}
