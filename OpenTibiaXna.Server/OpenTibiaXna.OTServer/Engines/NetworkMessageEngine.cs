using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTibiaXna.OTServer.Items;
using OpenTibiaXna.Helpers.Cryptography;
using OpenTibiaXna.Helpers.Checksum;
using OpenTibiaXna.OTServer.Objects;
using OpenTibiaXna.OTServer.Engines;

namespace OpenTibiaXna.OTServer.Engines
{
    public class NetworkMessageEngine
    {
        #region Instance Variables

        private byte[] buffer;
        private int fposition, flength, fbufferSize = 16394;

        #endregion

        #region Properties

        public int Length
        {
            get { return flength; }
            set { flength = value; }
        }

        public int Position
        {
            get { return fposition; }
            set { fposition = value; }
        }

        public byte[] Buffer
        {
            get { return buffer; }
        }

        public int BufferSize
        {
            get { return fbufferSize; }
        }

        public byte[] GetData()
        {
            byte[] t = new byte[flength];
            Array.Copy(buffer, t, flength);
            return t;
        }

        #endregion

        #region Constructors

        public NetworkMessageEngine()
        {
            Reset();
        }

        public NetworkMessageEngine(int startingIndex)
        {
            Reset(startingIndex);
        }

        public NetworkMessageEngine(NetworkMessageEngine message)
            : this(0)
        {
            AddBytes(message.GetData());
        }

        public void Reset(int startingIndex)
        {
            buffer = new byte[fbufferSize];
            flength = startingIndex;
            fposition = startingIndex;
        }

        public void Reset()
        {
            Reset(8);
        }

        #endregion

        #region Get

        public byte GetByte()
        {
            if (fposition + 1 > flength)
                throw new IndexOutOfRangeException("NetworkMessage GetByte() out of range.");

            return buffer[fposition++];
        }

        public byte[] GetBytes(int count)
        {
            if (fposition + count > flength)
                throw new IndexOutOfRangeException("NetworkMessage GetBytes() out of range.");

            byte[] t = new byte[count];
            Array.Copy(buffer, fposition, t, 0, count);
            fposition += count;
            return t;
        }

        public string GetString()
        {
            int len = (int)GetUInt16();
            string t = System.Text.ASCIIEncoding.Default.GetString(buffer, fposition, len);
            fposition += len;
            return t;
        }

        public ushort GetUInt16()
        {
            return BitConverter.ToUInt16(GetBytes(2), 0);
        }

        public uint GetUInt32()
        {
            return BitConverter.ToUInt32(GetBytes(4), 0);
        }

        public LocationEngine GetLocation()
        {
            UInt16 x = GetUInt16();
            UInt16 y = GetUInt16();
            byte z = GetByte();

            return new LocationEngine(x, y, z);
        }

        private ushort GetPacketHeader()
        {
            return BitConverter.ToUInt16(buffer, 0);
        }

        public OutfitObject GetOutfit()
        {
            byte head, body, legs, feet, addons;
            ushort looktype = GetUInt16();

            if (looktype != 0)
            {
                head = GetByte();
                body = GetByte();
                legs = GetByte();
                feet = GetByte();
                addons = GetByte();

                return new OutfitObject(looktype, head, body, legs, feet, addons);
            }
            else
                return new OutfitObject(looktype, GetUInt16());
        }

        #endregion

        #region Add

        public void AddByte(byte value)
        {
            if (1 + flength > fbufferSize)
                throw new Exception("NetworkMessage buffer is full.");

            AddBytes(new byte[] { value });
        }

        public void AddBytes(byte[] value)
        {
            if (value.Length + flength > fbufferSize)
                throw new Exception("NetworkMessage buffer is full.");

            Array.Copy(value, 0, buffer, fposition, value.Length);
            fposition += value.Length;

            if (fposition > flength)
                flength = fposition;
        }

        public void AddString(string value)
        {
            AddUInt16((ushort)value.Length);
            AddBytes(System.Text.ASCIIEncoding.Default.GetBytes(value));
        }

        public void AddUInt16(ushort value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        public void AddUInt32(uint value)
        {
            AddBytes(BitConverter.GetBytes(value));
        }

        public void AddLocation(LocationEngine loc)
        {
            AddUInt16((ushort)loc.X);
            AddUInt16((ushort)loc.Y);
            AddByte((byte)loc.Z);
        }

        public void AddOutfit(OutfitObject outfit)
        {
            AddUInt16(outfit.LookType);
            if (outfit.LookType != 0)
            {
                AddByte(outfit.Head);
                AddByte(outfit.Body);
                AddByte(outfit.Legs);
                AddByte(outfit.Feet);
                AddByte(outfit.Addons);
            }
            else
            {
                AddUInt16(outfit.LookItem);
            }
        }

        public void AddCreature(CreatureObject creature, bool known, uint removeKnown)
        {
            if (known)
            {
                AddUInt16(0x62); // known
                AddUInt32(creature.Id);
            }
            else
            {
                AddUInt16(0x61); // unknown
                AddUInt32(removeKnown);
                AddUInt32(creature.Id);
                AddString(creature.Name);
            }

            AddByte(Convert.ToByte(creature.Health / creature.MaxHealth * 100)); // health bar
            AddByte((byte)creature.Direction);
            AddOutfit(creature.Outfit);
            AddByte(creature.LightLevel);
            AddByte(creature.LightColor);
            AddUInt16(creature.Speed);
            AddByte((byte)creature.Skull);
            AddByte((byte)creature.Party);
        }

        public void AddItem(ItemObject item)
        {
            AddUInt16(item.Id);

            if (item.Data.HasExtraByte)
            {
                AddByte(item.Extra);
            }
        }

        public void AddPaddingBytes(int count)
        {
            fposition += count;

            if (fposition > flength)
                flength = fposition;
        }

        #endregion

        #region Peek

        public byte PeekByte()
        {
            return buffer[fposition];
        }

        public byte[] PeekBytes(int count)
        {
            byte[] t = new byte[count];
            Array.Copy(buffer, fposition, t, 0, count);
            return t;
        }

        public ushort PeekUInt16()
        {
            return BitConverter.ToUInt16(PeekBytes(2), 0);
        }

        public uint PeekUInt32()
        {
            return BitConverter.ToUInt32(PeekBytes(4), 0);
        }

        public string PeekString()
        {
            int len = (int)PeekUInt16();
            return System.Text.ASCIIEncoding.ASCII.GetString(PeekBytes(len + 2), 2, len);
        }

        #endregion

        #region Replace

        public void ReplaceBytes(int index, byte[] value)
        {
            if (flength - index >= value.Length)
                Array.Copy(value, 0, buffer, index, value.Length);
        }

        #endregion

        #region Skip

        public void SkipBytes(int count)
        {
            if (fposition + count > flength)
                throw new IndexOutOfRangeException("NetworkMessage SkipBytes() out of range.");
            fposition += count;
        }

        #endregion

        #region Encryption

        public void RSADecrypt()
        {
            Rsa.Decrypt(ref buffer, fposition, flength);
        }

        public bool XteaDecrypt(uint[] key)
        {
            return Xtea.Decrypt(ref buffer, ref flength, 6, key);
        }

        public bool XteaEncrypt(uint[] key)
        {
            return Xtea.Encrypt(ref buffer, ref flength, 6, key);
        }

        #endregion

        #region Checksum

        public bool CheckAdler32()
        {
            return AdlerChecksum.Generate(ref buffer, 6, flength) == GetAdler32();
        }

        public void InsertAdler32()
        {
            Array.Copy(BitConverter.GetBytes(AdlerChecksum.Generate(ref buffer, 6, flength)), 0, buffer, 2, 4);
        }

        private uint GetAdler32()
        {
            return BitConverter.ToUInt32(buffer, 2);
        }

        #endregion

        #region Prepare

        private void InsertPacketLength()
        {
            Array.Copy(BitConverter.GetBytes((ushort)(flength - 8)), 0, buffer, 6, 2);
        }

        private void InsertTotalLength()
        {
            Array.Copy(BitConverter.GetBytes((ushort)(flength - 2)), 0, buffer, 0, 2);
        }

        public bool PrepareToSendWithoutEncryption()
        {
            InsertPacketLength();

            InsertAdler32();
            InsertTotalLength();

            return true;
        }

        public bool PrepareToSend(uint[] xteaKey)
        {
            // Must be before Xtea, because the packet length is encrypted as well
            InsertPacketLength();

            if (!XteaEncrypt(xteaKey))
                return false;

            // Must be after Xtea, because takes the checksum of the encrypted packet
            InsertAdler32();
            InsertTotalLength();

            return true;
        }

        public bool PrepareToRead(uint[] xteaKey)
        {
            if (!XteaDecrypt(xteaKey))
                return false;

            fposition = 8;
            return true;
        }

        #endregion
    }
}
