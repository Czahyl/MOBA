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
using MOBA.Interface;

namespace MOBA.Characters.Prototype
{
    public class Player : BaseEntity
    {
        public const float AttRange = 0.5f;

        public List<Autoattack> autoAttack { get; private set; }
        public bool canAttack = true;
        public Timer attackDelay;

        public Nameplate nameplate
        { get; protected set; }
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
        public bool Friendly
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

            Width = 60;
            Height = 100;

            Animations = new Dictionary<string, Animation>();

            currentAni = new Animation(5);

            currentAni.buffer.Add(Main.Assets.getTexture(0));

            Team = TeamID;
            Level = 1;
            Exp = 0;
            Health = 100;
            maxHealth = 100;
            visionLayer = 0;
            defaultLayer = visionLayer;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            light = new LightEmitter();

            nameplate = new Nameplate(this);

            autoAttack = new List<Autoattack>();

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

            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Shoot(gameTime);

            light.Update(Position);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Draw(spriteBatch);

            spriteBatch.Draw(currentAni.Frame(), Bounds, Color.FromNonPremultiplied(255, 255, 255, (int)Alpha)); //TODO Add image support to draw
            nameplate.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public bool isFriendly()
        {
            return (Team - Main.controller.player.Team) == 0;
        }

        public void changeInvisibility(int x)
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
