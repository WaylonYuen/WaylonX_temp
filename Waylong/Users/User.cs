using System;
using System.Net.Sockets;
using Waylong.Packets;

namespace Waylong.Users {

    public class User : IUser {

        Socket IUsers.GetSocket { get => m_socket; }

        int IUsers.VerificationCode {
            get { return this.GetHashCode(); }
            set { m_VerificationCode = value; }
        }

        private Socket m_socket;
        private int m_VerificationCode;

        public bool Send(INetPacket netPacket) {
            throw new NotImplementedException();
        }
    }

}
