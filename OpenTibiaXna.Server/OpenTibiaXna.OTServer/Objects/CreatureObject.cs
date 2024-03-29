﻿using System;
using System.Collections.Generic;
using OpenTibiaXna.OTServer.Objects;

namespace OpenTibiaXna.OTServer.Objects
{
    public class CreatureObject : ThingObject
    {
        #region Private Variables


        #endregion

        #region Constructor

        public CreatureObject()
        {
            Health = 100;
            MaxHealth = 100;
            Mana = 100;
            MaxMana = 100;
            Health = 100;
            MaxHealth = 100;
            Outfit = new OutfitObject(128, 0);
            Direction = Direction.North;
            LightLevel = 0;
            LightColor = 0;
            Skull = Skull.None;
            Party = Party.None;
            Speed = 200;
        }

        #endregion

        #region Overrides

        public override string ToString()
        {
            return Name + " [" + Id + "]";
        }

        #endregion

        #region Properties

        public uint Id { get; set; }
        public string Name { get; set; }

        public ushort Health { get; set; }

        public ushort MaxHealth { get; set; }

        public ushort Mana { get; set; }
        public ushort MaxMana { get; set; }

        public OutfitObject Outfit { get; set; }
        public Direction Direction { get; set; }
        public byte LightLevel { get; set; }
        public byte LightColor { get; set; }
        public Skull Skull { get; set; }
        public Party Party { get; set; }
        public ushort Speed { get; set; }
        public TileObject Tile { get; set; }
        public GameObject Game { get; set; }

        public bool IsPlayer
        {
            get { return Id > 0x40000000; }
        }

        public byte HealthPercent
        {
            get
            {
                return Convert.ToByte(Math.Floor((double)(Health / MaxHealth) * 100));
            }
        }

        protected override ushort GetThingId()
        {
            return 0x63;
        }

        #endregion

        #region Methods

        public uint GetStepDuration()
        {
            uint duration = 0;
            if (Tile.Ground != null)
            {
                uint groundSpeed = Tile.Ground.Info.Speed;
                if (groundSpeed <= 0)
                    groundSpeed = 100;
                uint stepSpeed = Speed;
                if (stepSpeed > 0)
                {
                    duration = (1000 * groundSpeed) / stepSpeed;
                }
            }

            return duration; // TODO: * lastStepCost
        }

        public override string GetLookAtString()
        {
            return "You see " + Name + ".";
        }

        public void Say(string text)
        {
            this.Game.CreatureSpeech(this, new SpeechObject() { Type = SpeechType.Say, Message = text });
        }

        public void Yell(string text)
        {
            this.Game.CreatureSpeech(this, new SpeechObject() { Type = SpeechType.Yell, Message = text });
        }

        public void Whisper(string text)
        {
            this.Game.CreatureSpeech(this, new SpeechObject() { Type = SpeechType.Whisper, Message = text });
        }

        public void Step(Direction dir)
        {
            this.Game.CreatureMove(this, dir);
        }

        public void Turn(Direction dir)
        {
            this.Game.CreatureTurn(this, dir);
        }

        #endregion
    }
}