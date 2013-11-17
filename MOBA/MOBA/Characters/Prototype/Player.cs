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
using MOBA.Characters.Classes.StatusEffects;
using System.Diagnostics;

namespace MOBA.Characters.Prototype
{
    public class Player : BaseEntity
    {
        public const float AttRange = 0.5f;

        public List<Ability> ability { get; protected set; }
        public List<Autoattack> autoAttack { get; private set; }
        public bool canAttack = true;
        public Timer attackDelay;

        public List<Debuff> debuffList
        { get; protected set; }
        public List<Buff> buffList
        { get; protected set; }

        public Nameplate nameplate
        { get; protected set; }
        private Timer Regen;
        public int Team
        { get; private set; }
        public int HP5
        { get; private set; }
        public int MP5
        { get; private set; }
        public int Mana
        { get; protected set; }
        public int BaseMana 
        { get; protected set; }
        public int maxMana 
        { get; protected set; }
        public int ManaStat
        { get; protected set; }
        public int Level
        { get; protected set; }
        public int Exp 
        { get; protected set; }
        public string Name
        { get; protected set; }
        public float MoveSpeed
        { get; set; }
        public float AttackSpeed
        { get; protected set; }
        public int Attack
        { get; protected set; }
        public int AttackPower
        { get; protected set; }
        public int SpellPower
        { get; protected set; }
        public int HealthStat
        { get; protected set; }
        public bool Friendly
        { get; protected set; }

        public Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
        public SpriteEffects spe;

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

            ability = new List<Ability>();

            debuffList = new List<Debuff>();
            buffList = new List<Buff>();

            Regen = new Timer(5, false);

            currentAni = new Animation(5);

            currentAni.buffer.Add(Main.Assets.getTexture(0));

            Team = TeamID;
            Level = 1;
            Exp = 0;
            MoveSpeed = 1f;
            Health = 100;
            BaseHealth = 100;
            maxHealth = (BaseHealth * Level) + HealthStat;
            HP5 = 3;
            Mana = 100;
            BaseMana = 100;
            maxMana = (BaseMana * Level) + ManaStat;
            MP5 = 3;
            visionLayer = 0;
            defaultLayer = visionLayer;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
            light = new LightEmitter();

            nameplate = new Nameplate(this);

            autoAttack = new List<Autoattack>();

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
            Regen.Run();

            if (Regen.Tick)
            {
                if(Health < maxHealth)
                    Health += HP5;
                if(Mana < maxMana)
                    Mana += MP5;
            }

            maxHealth = (BaseHealth * Level) + HealthStat;
            maxMana = (BaseMana * Level) + ManaStat;

            attackDelay.Run();
            setPosition((int)Position.X, (int)Position.Y);

            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Shoot(gameTime);

            for (int i = 0; i < debuffList.Count; i++)
                debuffList[i].Update(gameTime);

            for (int i = 0; i < buffList.Count; i++)
                buffList[i].Update(gameTime);

            light.Update(Position);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            currentFrame = currentAni.Frame();

            for (int i = 0; i < autoAttack.Count; i++)
                autoAttack[i].Draw(spriteBatch);

            for (int i = 0; i < debuffList.Count; i++)
                debuffList[i].Draw(spriteBatch);

            for (int i = 0; i < buffList.Count; i++)
                buffList[i].Draw(spriteBatch);

            spriteBatch.Draw(currentFrame.Texture, Bounds, currentFrame.sRect, Color.FromNonPremultiplied(255, 255, 255, (int)Alpha), 0f, new Vector2(0, 0), spe, 0f); //TODO Add image support to draw
            nameplate.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }

        public bool isFriendly()
        {
            return (Team - Main.controller.player.Team) == 0;
        }

        public void removeProjectile(Autoattack a)
        {
            autoAttack.Remove(a);
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

        public void Drain(int manaAmount)
        {
            Mana -= manaAmount;
        }

        public void RefundResource(int amount)
        {
            Mana += amount;
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

                Position.X += MoveSpeed * xSpeed;
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

                Position.Y += MoveSpeed * ySpeed;
            }
        }
    }
}
