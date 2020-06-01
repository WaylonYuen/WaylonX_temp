using System;
using System.Net.Sockets;

namespace Waylong.Net {

    public class Networking {

        public static void TcpConnection(NetMode netMode, Socket socket) {

        }

        public static void UdpConnection(NetMode netMode, Socket socket) {

        }

    }

    public enum NetMode {
        Connect,
        Listen,
    }

}
