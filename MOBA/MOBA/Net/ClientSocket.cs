using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MOBA.Net
{
    public class ClientSocket
    {
        private AddressFamily addrFamily;
        private ProtocolType protocol;
        private Socket sock;
        private PacketWriter writer;
        private PacketReader reader;

        public ClientSocket(AddressFamily family = AddressFamily.InterNetworkV6, ProtocolType protocol = ProtocolType.IPv6)
        {
            addrFamily = family;
            this.protocol = protocol;
        }

        public void Connect(string ip, int port)
        {
            sock = new Socket(addrFamily, SocketType.Stream, protocol);
            sock.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
        }

        public void Send(Packet packet)
        {
            writer = new PacketWriter();
            packet.Write(writer);
            sock.Send(writer.Buffer());
        }

        public void Read(Packet packet)
        {
            reader = new PacketReader(new MemoryStream(packet.Buffer()));
            packet.Process(reader);
        }
    }
}
