using System;

namespace Waylong.Packets {

    public delegate void CallbackHandler(User user, NetPacketHeader netPacketHeader, byte[] bys_Data);

}
