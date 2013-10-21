using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MOBA.Net
{
    public class Headers
    {
        public static short HANDSHAKE = 0;
    }

    public class PacketWriter : BinaryWriter
    {
        private MemoryStream ms;

        public PacketWriter() : base(new MemoryStream())
        {
            ms = (MemoryStream)OutStream;
        }

        public byte[] Buffer()
        {
            return ms.ToArray();
        }
    }

    public class PacketReader : BinaryReader
    {
        public PacketReader(MemoryStream stream) : base(stream)
        {

        }
    }

    public abstract class Packet
    {
        public short Header
        { get; protected set; }

        public abstract void Process(PacketReader reader);
        public abstract void Write(PacketWriter writer);
        public abstract byte[] Buffer();
        public abstract void setBuffer(byte[] dat);
    }

    public class HandshakePacket : Packet
    {
        private byte[] dat;

        public HandshakePacket()
        {
            Header = Headers.HANDSHAKE;
        }

        public override void Process(PacketReader reader)
        {
            bool ok = reader.ReadBoolean();
        }

        public override void Write(PacketWriter writer)
        {
            writer.Write(Header);
        }

        public override byte[] Buffer()
        {
            return dat;
        }

        public override void setBuffer(byte[] dat)
        {
            this.dat = dat;
        }
    }
}
