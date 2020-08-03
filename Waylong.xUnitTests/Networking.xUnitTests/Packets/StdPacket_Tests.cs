using System;
using System.Text;
using Waylong.Packets;
using Waylong.Users;
using Xunit;

namespace Networking.xUnitTests.Packets {

    public class StdPacket_Tests {

        //[Fact]
        //public void StdPacket_Unpack_RefBys_RangeTest_LessThan_OK() {
        //    //測試 : 解析參數內容小於封包架構時,應回傳Null

        //    object expected = null;
        //    var actual = StdPacket.Unpack(new User(), new byte[10]);    //解析

        //    Assert.Equal(expected, actual);
        //}

        //[Fact]
        //public void StdPacket_Unpack_FunTest_OK() {

        //    var expected = "Unit Test";

        //    //創建
        //    var user = new User();
        //    var packet = new StdPacket(user, Emergency.Level3, Encryption.RES256, Category.Emergency, Callback.PacketHeaderSync, expected);

        //    //封裝
        //    var bys_packet = packet.ToPackup();

        //    //解析
        //    var newPacketObj = StdPacket.Unpack(user, bys_packet);

        //    var actual = Encoding.UTF8.GetString(newPacketObj.Data.Data);

        //    Assert.Equal(expected, actual);

        //}

    }

}
