using System;
using System.Net;
using WaylonX.Converter;
using WaylonX.Loggers;
using WaylonX.Packets;

namespace WaylonX {

    class MainClass {
        public static void Main(string[] args) {

            Demo.LoggerTest();

            //Demo.PacketTest();

            //Demo.ConnectionTest();

            //Demo.PacketTest2();
        }
    }

    //Testing: test
    public static class Demo {

        public static void LoggerTest() {
            //建立記錄器
            var Logger = new StdLogger();

            Logger.Debug("Testing Error Logger output Format.");
            Logger.Info("Testing Error Logger output Format.");
            Logger.Warn("Testing Error Logger output Format.");
            Logger.Error("Testing Error Logger output Format.");

            var logs = Logger.GetContainer("Server Connection", Logger.ToString());

            logs.Add("IP", "127.0.0.1");
            logs.Add("Port", "8808");
            logs.Add("Test", "demo1");
            logs.Add("Test2", "demo2");
            logs.Excute();

            Logger.Testing();
        }

        public static void PacketTest() {

            //建立封包
            var pk = new StdPacket(Emergency.Level1, Encryption.Testing, Category.Emergency, Callback.SIZE, 123);   //使用者創建封包 & 參數為必備的封包資訊
            var bys_pk = pk.ToPackup(); //封包轉封包（用戶發送方法裡將自行封裝)

            //send 發送封包
            //.....省略發送過程

            //獲取完整封包長度
            var pkLen = Bytes.Splitter(out byte[] pk1, ref bys_pk, 0, BasicTypes.SizeOf.Int);
            Console.WriteLine($"pkLen : {IPAddress.NetworkToHostOrder(System.BitConverter.ToInt32(pkLen, 0))}");

            //獲取封包型態
            var pktype = Bytes.Splitter(out byte[] pk2, ref pk1, 0, BasicTypes.SizeOf.Short);
            Console.WriteLine($"pkType : {(PacketType)IPAddress.NetworkToHostOrder(System.BitConverter.ToInt16(pktype, 0))}");


            //通過型態創建封包
            var gPk = new StdPacket();

            gPk.Unpack(pk2);    //解析封包

            //Output
            Console.WriteLine(gPk.Header.ToString());
            Console.WriteLine(gPk.Body.ToString());
            Console.WriteLine(System.BitConverter.ToInt32(gPk.Body.Bys_data, 0));
        }

    }

}
