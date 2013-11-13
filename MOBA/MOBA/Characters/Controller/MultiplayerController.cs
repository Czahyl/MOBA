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
using MOBA.Characters.Prototype;
using MOBA.World;

namespace MOBA.Characters.Controller
{
    public class MultiplayerController : Controller
    {
        public MultiplayerController(Main m) : base(m)
        {
        }

        public override void plugEntity(Player p)
        {
            base.plugEntity(p);
            player.setPosition(200, 200);
            player.light = new LightEmitter(Main.lightEngine, player.Position, 150, 0);

            if (player.isFriendly())
                Main.lightEngine.plugEmitter(player.light);
        }

        public override void Update(GameTime gameTime)
        {
            //movement logic thru server
            base.Update(gameTime);
        }

        public override void Draw()
        {

            base.Draw();
        }
    }
}
