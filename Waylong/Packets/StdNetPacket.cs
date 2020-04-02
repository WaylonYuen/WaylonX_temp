using System;
using System.Net;
using System.Runtime.Remoting.Messaging;
using Waylong.Converter;
using Waylong.Users;

namespace Waylong.Packets {

    public struct NetPacket : INetPacket {

        #region Prop
        public StdNetHeader Header { get; private set; }
        public byte[] GetData { get => mBys_data; }
        #endregion

        #region Local Values
        private readonly IUsers m_IUser;
        private byte[] mBys_data;

        #endregion

        #region Constructor

        public NetPacket(IUsers user, StdNetHeader header, byte[] bys_data) {
            m_IUser = user;
            Header = header;
            mBys_data = bys_data;
        }

        public NetPacket(IUsers user, Category category, Callback callback, byte[] bys_data) {
            m_IUser = user;
            Header = new StdNetHeader(user, Encryption.None, Emergency.None, category, callback, bys_data.Length);
            mBys_data = bys_data;
        }

        public NetPacket(IUsers user, Encryption encryption, Emergency emergency, Category category, Callback callback, byte[] bys_data) {
            m_IUser = user;
            Header = new StdNetHeader(user, encryption, emergency, category, callback, bys_data.Length);
            mBys_data = bys_data;          
        }
        #endregion

        #region Methods

        //Bug: NetPaket.ToPackup()
        public byte[] ToPackup() {

            var bys_packet = new byte[StdNetHeader.SizeOf.Header + Header.GetDataLength];

            IBasicOfHeader stdNetHeader = Header;

            stdNetHeader.ToPackup().CopyTo(bys_packet, 0);
            mBys_data.CopyTo(bys_packet, StdNetHeader.IndexOf.Data);

            return bys_packet;
        }

        public void Unpack(IUsers user, byte[] bys_netPacket) {

            if(bys_netPacket.Length < StdNetHeader.SizeOf.Header) {
                //Error: Header length is not incompatible.
                return;
            }

            Header = StdNetHeader.Unpack(user, bys_netPacket);
            mBys_data = Bytes.Extract(bys_netPacket, StdNetHeader.IndexOf.Data, Header.GetDataLength);
        }

        //Untested: NetPacket.UserEquals()
        //封包用戶比較器
        public bool UserEquals(IUsers user) {
            IUsers localUser = m_IUser;
            return (localUser.GetSocket == user.GetSocket) ? true : false;
        }

        #endregion

    }
}
