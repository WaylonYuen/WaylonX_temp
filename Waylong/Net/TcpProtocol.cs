using System;
using System.Net.Sockets;

namespace Waylong.Net {

    public class TcpProtocol : IConnection {

        public string GetIP { get => m_ip; }
        public int GetPort { get => m_prot; }
        public ProtocolType GetProtocolType { get => m_protocolType; }

        private readonly string m_ip;
        private readonly int m_prot;
        private readonly ProtocolType m_protocolType;

        public TcpProtocol() {

        }



        public bool Connection() {
            throw new NotImplementedException();
        }
    }
}
