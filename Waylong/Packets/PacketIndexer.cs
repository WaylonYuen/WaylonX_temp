using System;

namespace Waylong.Packets {

    public class PacketIndexer {

        public static object CreatePacket(PacketType packetType) {

            switch (packetType) {

                case PacketType.StdPacket:
                    return new StdPacket();

                default:
                    break;
            }

            return null;
        }

    }
}
