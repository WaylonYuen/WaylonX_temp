using System;
using System.Net;
using System.Runtime.Remoting.Messaging;
using Waylong.Converter;
using Waylong.Users;

namespace Waylong.Packets {

    public struct StdNetPacket : INetPacket {

        #region Prop
        IUser INetPacket.GetUser { get => m_IUser; }
        public StdNetHeader Header { get; private set; }
        public byte[] GetData { get => mBys_data; }
        #endregion

        #region Local Values
        private IUser m_IUser;
        private byte[] mBys_data;

        #endregion

        #region Constructor

        /// <summary>
        /// 未指定發送目標
        /// </summary>
        /// <param name="header"></param>
        /// <param name="bys_data"></param>
        public StdNetPacket(StdNetHeader header, byte[] bys_data) {
            m_IUser = null;
            Header = header;
            mBys_data = bys_data;
        }

        /// <summary>
        /// 引用 Ori Header to instance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="header"></param>
        /// <param name="bys_data"></param>
        public StdNetPacket(IUser user, StdNetHeader header, byte[] bys_data) {
            m_IUser = user;
            Header = header;
            mBys_data = bys_data;
        }

        /// <summary>
        /// 未指定 加密方法 & 緊急程度
        /// </summary>
        /// <param name="user"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="bys_data"></param>
        public StdNetPacket(IUser user, Category category, Callback callback, byte[] bys_data) {
            m_IUser = user;
            Header = new StdNetHeader(user, Encryption.None, Emergency.None, category, callback, bys_data.Length);
            mBys_data = bys_data;
        }

        /// <summary>
        /// 標準封包 Instance
        /// </summary>
        /// <param name="user"></param>
        /// <param name="encryption"></param>
        /// <param name="emergency"></param>
        /// <param name="category"></param>
        /// <param name="callback"></param>
        /// <param name="bys_data"></param>
        public StdNetPacket(IUser user, Encryption encryption, Emergency emergency, Category category, Callback callback, byte[] bys_data) {
            m_IUser = user;
            Header = new StdNetHeader(user, encryption, emergency, category, callback, bys_data.Length);
            mBys_data = bys_data;          
        }
        #endregion

        #region Methods

        public byte[] ToPackup() {

            var bys_packet = new byte[StdNetHeader.SizeOf.Header + Header.GetDataLength];

            IBasicOfHeader stdNetHeader = Header;

            stdNetHeader.ToPackup().CopyTo(bys_packet, 0);
            mBys_data.CopyTo(bys_packet, StdNetHeader.IndexOf.Data);

            return bys_packet;
        }

        /// <summary>
        /// 解析bytes: Interface,
        /// </summary>
        /// <param name="user">Used for new STRUCT.Header(IUsers user, ...).</param>
        /// <param name="bys_netPacket">Complete bytes packet.</param>
        public void Unpack(IUser user, byte[] bys_netPacket) {

            if(bys_netPacket.Length < StdNetHeader.SizeOf.Header) {
                //Error: Header length is not incompatible.
                return;
            }

            Header = StdNetHeader.Unpack(user, bys_netPacket);
            mBys_data = Bytes.Extract(bys_netPacket, StdNetHeader.IndexOf.Data, Header.GetDataLength);
        }

        //Untested: NetPacket.UserEquals()
        //封包用戶比較器
        public bool UserEquals(IUser user) {
            IUser localUser = m_IUser;
            return (localUser.GetSocket == user.GetSocket) ? true : false;
        }

        #endregion

    }
}
