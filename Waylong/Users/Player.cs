using System;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.Users {

    public class Player : IPlayer {

        public Socket GetSocket =>
            throw new NotImplementedException();

        public int VerificationCode {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public bool Send(INetPacket netPacket) {
            throw new NotImplementedException();
        }
    }

}
