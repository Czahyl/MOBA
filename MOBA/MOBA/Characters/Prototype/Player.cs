using System;
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

        public List<Projectile> autoAttack { get; private set; }
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

            autoAttack = new List<Projectile>();

            moveSpeed = 1;

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
            setPosition((int)Position.X, (int)Position.Y);

            if (InputHandler.mouseHeld(MouseButton.Left))
            {
                attackDelay.Run();

                if(attackDelay.Tick)
                    autoAttack.Add(new Projectile(new Vector2(Position.X, Position.Y), this));
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

        public void Pathfind(int x, int y)
        {
            int xSpeed, ySpeed;
            int difX = (int)Position.X - x;
            int difY = (int)Position.Y - y;
            bool isPositiveX = difX > 0;
            bool isPositiveY = difY > 0;

            xSpeed = 0;
            ySpeed = 0;

            if(difX != 0)
            {
                if (isPositiveX)
                {
                    xSpeed = -1;
                }
                else if (!isPositiveX)
                {
                    xSpeed = 1;
                }

                Position.X += moveSpeed * xSpeed;
            }
            if (difY != 0)
            {
                if (isPositiveY)
                {
                    ySpeed = -1;
                }
                else if (!isPositiveY)
                {
                    ySpeed = 1;
                }

                Position.Y += moveSpeed * ySpeed;
            }
        }
    }
}
