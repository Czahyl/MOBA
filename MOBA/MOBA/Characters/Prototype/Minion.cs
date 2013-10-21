using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.World;

namespace MOBA.Characters.Prototype
{
    public class Minion : BaseEntity
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 32;
        public const int WORTH = 10;

        public Minion()
        {
            //Add preset animation to ani buffer
            //Maxhealth = Base amount * Towers dead
            Center = new Vector2(150, 150);
            Bounds = new Rectangle((int)Center.X, (int)Center.Y, WIDTH, HEIGHT);
            light = new LightEmitter();
        }

        public override void Update()
        {
            Center = new Vector2(150 + Camera.X, 150 + Camera.Y);
            base.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
