using System;
using System.Text;
using Waylong.Converter;
using Waylong.Packets.Header;
using Waylong.Users;

namespace Waylong.Packets.PacketData {

    /// <summary>
    /// 封包組合器
    /// </summary>
    /// <typeparam name="T">Packet Header</typeparam>
    /// <typeparam name="U">Packet Data</typeparam>
    public class Packaged<T, U>
        where T : IPacketMethods
        where U : IPacketMethods {

        #region Property

        /// <summary>
        /// 封包描述: Header中包含所有對此封包的描述資料
        /// </summary>
        public T Header { get => m_header; }

        /// <summary>
        /// 封包內容: 封包主體內容
        /// </summary>
        public U Body { get => m_data; }

        #endregion

        #region Constructor

        /// <summary>
        /// 創建標準封包組合器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="packetHeader"></param>
        /// <param name="packetData"></param>
        public Packaged(T packetHeader, U packetData) {
            m_header = packetHeader;
            m_data = packetData;
        }


        #endregion

        #region Local values

        protected T m_header;
        protected U m_data;

        #endregion

        #region Methods

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public virtual byte[] ToPackup() {

            //封裝
            var bys_header = m_header.ToPackup();
            var bys_data = m_data.ToPackup();

            //返回組合封裝
            return Bytes.ToPackup(ref bys_header, ref bys_data);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="bys_packet"></param>
        public virtual void Unpack(byte[] bys_packet) {

            //分割資料: Splitter返回提取內容, out剩餘內容
            m_header.Unpack(Bytes.Splitter(out byte[] bys_data, ref bys_packet, 0, m_header.StructSIZE));
            m_data.Unpack(bys_data);
        }

        /// <summary>
        /// Information
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return
                base.ToString() + "\n"
                 + m_header.ToString()
                 + m_data.ToString();
        }

        /// <summary>
        /// 測試用
        /// </summary>
        public void GTest() {
            Console.WriteLine("Testing");
            Console.WriteLine(m_header.ToString());
            Console.WriteLine(m_data.ToString());
        }

        #endregion
    }

}
