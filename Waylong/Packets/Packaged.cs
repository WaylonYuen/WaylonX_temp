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

        public User User { get => m_user; } // 線程分發封包時需要
        public T Header { get => m_header; }
        public U Data { get => m_data; }

        public PacketHeaderType HeaderType { get => m_packetHeaderType; }
        public PacketType PacketType { get => m_packetType; }

        #endregion

        #region Constructor

        /// <summary>
        /// 創建標準封包組合器
        /// </summary>
        /// <param name="user"></param>
        /// <param name="packetHeader"></param>
        /// <param name="packetData"></param>
        public Packaged(User user, T packetHeader, U packetData) {
            m_user = user;
            m_header = packetHeader;
            m_data = packetData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        protected Packaged(User user, U packetData) {
            m_user = user;
            m_data = packetData;
        }

        #endregion

        #region Local values

        protected readonly User m_user;
        protected T m_header;
        protected U m_data;

        private PacketHeaderType m_packetHeaderType;
        private PacketType m_packetType;

        #endregion

        #region Methods

        /// <summary>
        /// 封裝
        /// </summary>
        /// <returns></returns>
        public byte[] ToPackup() {

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
        public void Unpack(byte[] bys_packet) {

            //分割資料: Splitter返回提取內容, out剩餘內容
            m_header.Unpack(Bytes.Splitter(out byte[] bys_data, ref bys_packet, 0, m_header.PacketHeadDescriptionLength));
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

    public class PackagedDemo {

        public static void Testing() {

            //create
            var user = new User();
#if Test
            var header = new StdPacketHeader(user.VerificationCode, Emergency.Level2, Encryption.RES256, Category.General, Callback.PacketHeaderSync);
#else
            var header = new StdPacketHeader(user.VerificationCode, Category.General, Callback.PacketHeaderSync);
#endif

            //標準寫法
            var Test = new Packaged<StdPacketHeader, StdPacketData>(user,
                new StdPacketHeader(user.VerificationCode, Category.General, Callback.PacketHeaderSync),
                new StdPacketData(BitConverter.GetBytes(67548)));

            //另外的寫法
            //var Test2 = new StdPacket(user, Emergency.Level2, Encryption.RES256, Category.General, Callback.PacketHeaderSync, BitConverter.GetBytes(4584));
            //var Test3 = new StdPacket(user, Emergency.Level3, Encryption.Testing, Category.Emergency, Callback.Testing, true);

            var Test4 = new StdPacket(user, "Testing");
            Test4.SetHeader(Emergency.Level2, Encryption.RES256, Category.None, Callback.PacketHeaderSync);

            var data = new StdPacketData(BitConverter.GetBytes(456));
            var packet = new Packaged<StdPacketHeader, StdPacketData>(user, header, data);

            //packup
#if Test
            var bys_packet = packet.ToPackup();
#else
            var bys_packet = Test4.ToPackup();
#endif

            //create
            var getHeader = new StdPacketHeader(user.VerificationCode, Emergency.None, Encryption.None, Category.None, Callback.None);
            var getData = new StdPacketData(BitConverter.GetBytes(56));
            var newPacket = new Packaged<StdPacketHeader, StdPacketData>(user, getHeader, getData);

            //unpack
            newPacket.Unpack(bys_packet);

            //show
            Console.WriteLine(newPacket.ToString());

            //可優化: 利用接口來指定不同型態由Bit回覆指定型態,再利用packetData中添加指定型態enum來取決於用什麼方法來回覆
            //Console.WriteLine(BitConverter.ToInt32(newPacket.Data.Data, 0));
            Console.WriteLine(Encoding.UTF8.GetString(newPacket.Data.Data));
        }
    }

}
