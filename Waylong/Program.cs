using System;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Waylong.Architecture.Server;
using Waylong.Net;
using Waylong.Net.Protocol;
using Waylong.Packets;
using Waylong.Packets.Header;
using Waylong.Packets.PacketData;
using Waylong.Users;

namespace Waylong {

    class MainClass {
        public static void Main(string[] args) {

            Demo.PacketTest2();
        }
    }

    //Testing: test
    public static class Demo {

        public static void PacketTest2() {

            IUser user = new User();

            

            var packet = new StdPacket(user.VerificationCode, Emergency.Level3, Encryption.RES256, Category.Emergency, Callback.PacketHeaderSync, "Hello");

            var bys_packet = packet.ToPackup();

            user.Send(packet);

            var get_packet = new StdPacket();
            get_packet.Unpack(bys_packet);


            Console.WriteLine(get_packet.ToString());
            Console.WriteLine(Encoding.UTF8.GetString(get_packet.Data.Data));
        }

        public static void ConnectionTest() {
            var Server = new StdServer();

            Server.Start("127.0.0.1", 8808);

            Console.WriteLine("End");
        }
    }

}
