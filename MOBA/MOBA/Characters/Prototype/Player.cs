﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MOBA.Assets;
using MOBA.Characters.Classes.Spells;
using MOBA.World;
using MOBA.Input;
using MOBA.Math;

namespace MOBA.Characters.Prototype
{
    public class Player : BaseEntity
    {
        public const int WIDTH = 32;
        public const int HEIGHT = 64;

        private List<Projectile> autoAttack = new List<Projectile>();
        private Timer attackDelay;

        public int Level
        { get; protected set; }
        public int Exp 
        { get; protected set; }
        public string Name
        { get; protected set; }

        public Player()
        {
            Level = 1;
            Exp = 0;
            invisibilityLayer = 0;
            Bounds = new Rectangle(0, 0, 32, 64);
            light = new LightEmitter();

            attackDelay = new Timer(1);
        }

        public virtual void Load(AssetManager assMan)
        {
            
        }

        public void Username(string name)
        {
            //TODO: Name checks for unusable characters & length
            Name = name;
        }

        public void giveXp(int exp)
        {
            Exp += exp;
        }

        public override void Update(GameTime gameTime)
        {
            if (InputHandler.mouseHeld(MouseButton.Left))
            {
                attackDelay.Run();

                if(attackDelay.Tick)
                    autoAttack.Add(new Projectile(new Vector2(Main.WIDTH / 2, Main.HEIGHT / 2)));
            }

            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Shoot(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}
