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

            Demo.ConnectionTest();
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

        public static void ConnectionTest() {
            var Server = new StdServer();

            Server.Start("127.0.0.1", 8808);

            Console.WriteLine("End");
        }
    }

}
