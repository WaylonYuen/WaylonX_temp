using System;
using System.Net;
using System.Net.Sockets;

namespace Waylong.Net {

    
    
    public interface IConnection {

        #region Property

        /// <summary>
        /// Socket
        /// </summary>
        Socket Socket { get; }

        /// <summary>
        /// ip end point
        /// </summary>
        IPEndPoint IPEndPoint { get; }
        #endregion
    }

}
