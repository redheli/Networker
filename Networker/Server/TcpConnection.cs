﻿using System;
using System.Net.Sockets;
using Networker.Common;
using Networker.Interfaces;

namespace Networker.Server
{
    public class TcpConnection : INetworkerConnection
    {
        public TcpConnection(Socket socket)
        {
            this.Socket = socket;
            this.Serializer = new PacketSerializer();
            this.Identifier = Guid.NewGuid()
                                  .ToString();
        }

        public string Identifier { get; }
        public PacketSerializer Serializer { get; }
        public Socket Socket { get; }

        public void Close()
        {
            this.Socket.Shutdown(SocketShutdown.Both);
        }

        public void Send<T>(T packet)
            where T: NetworkerPacketBase
        {
            this.Socket.Send(this.Serializer.Serialize(packet));
        }
    }
}