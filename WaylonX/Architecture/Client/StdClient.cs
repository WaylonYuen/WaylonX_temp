using System;

namespace WaylonX.Architecture.Client {

    public class ClientInfoEventArgs : CSBaseInfoEventArgs {

    }

    /// <summary>
    /// 標準客戶端架構
    /// </summary>
    public abstract class StdClient : CSBase_Catalina {

        //Constructor
        public StdClient(string name) : base(name) {

        }

    }
}
