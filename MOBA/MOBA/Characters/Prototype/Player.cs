using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.Assets;
using MOBA.World;

namespace MOBA.Characters.Prototype
{
    public class Player : BaseEntity
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 64;

        public int Level
        { get; protected set; }
        public int Exp 
        { get; protected set; }
        public string Name
        { get; protected set; }

        public Player(string name)
        {
            Name = name;
            Level = 1;
            Exp = 0;
            invisibilityLayer = 0;
            Bounds = new Rectangle(0, 0, 32, 64);
            light = new LightEmitter();
        }

        public virtual void Load(AssetManager assMan)
        {
            
        }

        public void giveXp(int exp)
        {
            Exp += exp;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
