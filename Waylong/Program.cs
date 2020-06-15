using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Waylong.Architecture;
using Waylong.Net;
using Waylong.Net.Protocol;
using Waylong.Packets;
using Waylong.Packets.Header;
using Waylong.Packets.PacketData;
using Waylong.Users;

namespace Waylong {

    class MainClass {
        public static void Main(string[] args) {

            //StdPacketHeader.Testing();
            //StdPacketData.Testing();
            //PackagedDemo.Testing();

            //Demo.PacketTest();

            Demo.StdServerTest();
        }
    }

    //Testing: test
    public static class Demo {

        public static void PacketTest() {

            var user = new User();

            var packetObj = new StdPacket(user, "Test StdPacket");
            packetObj.SetHeader(Emergency.Level2, Encryption.RES256, Category.General, Callback.PacketHeaderSync);

            var bys_packet = packetObj.ToPackup();

            var newPacketObj = StdPacket.Unpack(user, bys_packet);

            Console.WriteLine(newPacketObj.ToString());
            Console.WriteLine(Encoding.UTF8.GetString(newPacketObj.Data.Data));
        }

        //Test
        public static void TcpConnectionTest() {

            var Conn = new TcpConnection(NetworkMode.Listen, "127.0.0.1", 8808);
            var Management = new NetworkManagement();

            bool isConn = Management.Connect(Conn);

        }

        public static void UdpConnectionTest() {

            var Conn = new UdpConnection(NetworkMode.Listen, "127.0.0.1", 8809);
            var Management = new NetworkManagement();

            bool isConn = Management.Connect(Conn);
           
        }

        public static void StdServerTest() {

            var ServerTest = new StdServer();

            ServerTest.Name = "Test Server";
            ServerTest.Start("127.0.0.1", 8088);

        }
    }

}
