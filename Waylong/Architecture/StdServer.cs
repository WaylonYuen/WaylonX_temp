using System;
using Waylong.Net;

namespace Waylong.Architecture {

    public class StdServer : CSModel {


        #region Local Values
        //...
        #endregion

        #region Constructor

        public StdServer(string name, string IPAddress)
            : base(name, IPAddress, OperatingEnvironment.Terminal) {
            m_netMode = NetMode.Listen;
        }

        public StdServer(string name, string ip, int port)
            : base(name, ip, port, OperatingEnvironment.Terminal) {
            m_netMode = NetMode.Listen;
        }
        #endregion

        protected override void Initialize() {
            throw new NotImplementedException();
        }

        protected override void DataStruct() {
            throw new NotImplementedException();
        }

        protected override void Registered() {
            throw new NotImplementedException();
        }


        protected override void StartThread() {
            throw new NotImplementedException();
        }

        protected override void Execute_GeneralCallbackThread() {
            throw new NotImplementedException();
        }

    }
}
