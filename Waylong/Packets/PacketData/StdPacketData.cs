using System;
using System.Net;
using Waylong.Converter;

namespace Waylong.Packets.PacketData {

    /// <summary>
    /// 標準資料封包架構
    /// </summary>
    public class StdPacketData : PacketDataBase, IPacketMethods {

        #region Property

        /// <summary>
        /// 主要資料 : Packet所要傳達的真正內容資訊
        /// </summary>
        public byte[] Data { get => mBys_data; }    //Bytes內容

        /// <summary>
        /// 封包型態 : 此packet的架構屬於何種型態
        /// </summary>
        public PacketDataType DataType { get => m_DataType; }

        /// <summary>
        /// 此結構大小
        /// </summary>
        public int StructSIZE => SIZE;

        

        #endregion

        #region Constructor

        /// <summary>
        /// 標準封包構造器
        /// </summary>
        /// <param name="bys_data">內容資料</param>
        public StdPacketData(byte[] bys_data) {
            mBys_data = bys_data;
        }

        #endregion

        #region Local values

        //Constants

        /// <summary>
        /// 封包Head長度 : 指的是此Packet對架構的描述所佔的長度, 此處的Head只屬於StdPacketData架構而非Header架構.
        /// </summary>
        public const int SIZE = IndexOf.Data;

        private const PacketDataType m_DataType = PacketDataType.StdPacketData;

        //Variables
        private byte[] mBys_data;

        #endregion

        #region Inner class

        public static class IndexOf {

            public const int packetType = 0;
            public const int DataLength = packetType + SizeOf.packetType;

            public const int Data = DataLength + SizeOf.DataLength;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public byte[] ToPackup() {

            //指定資料包尺寸
            var bys_packetData = new byte[SIZE + mBys_data.Length];

            //封裝資料
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder((short)m_DataType)).CopyTo(bys_packetData, IndexOf.packetType);
            BitConverter.GetBytes(IPAddress.HostToNetworkOrder(mBys_data.Length)).CopyTo(bys_packetData, IndexOf.DataLength);
            mBys_data.CopyTo(bys_packetData, IndexOf.Data);

            return bys_packetData;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packetData"></param>
        public void Unpack(byte[] bys_packetData) {

            if(bys_packetData.Length < SIZE) {
                return;
            }
            
            //解析
            //m_packetType = (PacketType)IPAddress.NetworkToHostOrder(BitConverter.ToInt16(Bytes.Extract(bys_packetData, IndexOf.packetType, SizeOf.packetType), 0));
            var len = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(Bytes.Extract(bys_packetData, IndexOf.DataLength, SizeOf.DataLength), 0));
            mBys_data = Bytes.Extract(bys_packetData, IndexOf.Data, len);

        }

        /// <summary>
        /// Information
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return
                "\n" + base.ToString() + ":\n"
                 + "----------------------------------------------\n"
                 + "DataLength\t" + mBys_data.Length + "\n"
                 + "End-------------------------------------------\n\n";
        }

        /// <summary>
        /// 測試用
        /// </summary>
        public static void Testing() {

            var packetData = new StdPacketData(BitConverter.GetBytes(1234));

            var bys_data = packetData.ToPackup();

            packetData.Unpack(bys_data);
            Console.WriteLine(packetData.ToString());

            Console.WriteLine($"data: {BitConverter.ToInt32(packetData.Data, 0)}");

        }

        #endregion
    }
}
