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
        public const float AttRange = 0.5f;

        public List<Projectile> autoAttack { get; private set; }
        private Timer attackDelay;

        public int Team
        { get; private set; }
        public int Mana
        { get; protected set; }
        public int Level
        { get; protected set; }
        public int Exp 
        { get; protected set; }
        public string Name
        { get; protected set; }
        public float AttackSpeed
        { get; protected set; }
        public int Attack
        { get; protected set; }
        public int SpellPower
        { get; protected set; }

        public Player(string Username, int TeamID)
        {
            if (Username.Length > 8 /* or allowed length */) // TODO: Make a checkname() method in server class
            {
                for (int i = 0; i < 8; i++)
                {
                    Name += Username[i]; //Cap the username at 8 characters
                }
            }
            else
            {
                Name = Username;
            }

            Team = TeamID;
            Level = 1;
            Exp = 0;
            visionLayer = 0;
            defaultLayer = visionLayer;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, 32, 64);
            light = new LightEmitter();

            autoAttack = new List<Projectile>();

            moveSpeed = 1;
            AttackSpeed = 0.5f;

            attackDelay = new Timer(AttackSpeed, false);
        }

        public virtual void Load(AssetManager assMan)
        {
            
        }

        public void giveXp(int exp)
        {
            Exp += exp;
        }

        public override void Update(GameTime gameTime)
        {
            attackDelay.Run();
            setPosition((int)Position.X, (int)Position.Y);

            if (InputHandler.mouseHeld(MouseButton.Left))
            {
                if(attackDelay.Tick)
                    autoAttack.Add(new Projectile(new Vector2(Position.X, Position.Y), this));
            }

            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Shoot(gameTime);

            light.Update();

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Draw(spriteBatch);

            spriteBatch.Draw(Main.Assets.getTexture(0).Texture, Bounds, new Color(255, 255, 255, Alpha));
            spriteBatch.DrawString(Main.Assets.getFont(0), Name, new Vector2(Bounds.X, Bounds.Y - 15), Color.White);

            base.Draw(spriteBatch);
        }

        public void changeVisibility(int x)
        {
            if (x <= visionLayer)
                visionLayer = visionLayer;
            else if (x == -1)
                visionLayer = defaultLayer;
            else
                visionLayer = x;
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
