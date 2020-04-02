using System;
using System.Security.Cryptography.X509Certificates;
using Waylong.Packets;
using Waylong.User;

namespace Waylong {

    class MainClass {
        public static void Main(string[] args) {
            //Console.WriteLine("Hello World!");
            
            Demo.PacketTest();
          
        }
    }

    //Testing: test
    public static class Demo {

        public static int m_index = 0;

        public static void PacketTest() {

            //create
            var user = new User();
            var packet = new StdNetPacket(user, Category.General, Callback.PacketHeaderSync, BitConverter.GetBytes(2020));
            packet.Header.SetEncryptionType(Encryption.RES256);
            packet.Header.SetEmergencyType(Emergency.Level3);

            //byte[] packed = packet.ToPackup();  //封裝
            INetPacket netPacket = packet;
            byte[] packed = netPacket.ToPackup();

            //方法1
            //var getPacket = new NetPacket();
            //IStdNetHeader header = getPacket.Header;
            //header.Unpack(packed);

            //方法2
            //IStdNetHeader header = StdNetHeader.Unpack(user, packed);

            //方法3
            var resultPK = new StdNetPacket(); //新封包
            resultPK.Unpack(user, packed);   //解析
            IStdNetHeader header = resultPK.Header; //讀取Header
            var data = BitConverter.ToInt32(resultPK.GetData, 0);   //資料還原



            //Output
            Console.WriteLine($"data :\t {data}");

            Console.WriteLine($"Tesing : {++m_index}");
            Console.WriteLine($"callback\t { header.GetCallbackType}");
            Console.WriteLine($"category\t { header.GetCategoryType}");
            Console.WriteLine($"Verification\t { header.GetVerificationCode}");
            Console.WriteLine($"Encryption\t { header.GetEncryptionType}");
            Console.WriteLine($"Emergency\t { header.GetEmergencyType}");
            Console.WriteLine($"Data Length\t { header.GetDataLength}");
            Console.WriteLine("\n");

        }

    }

}
