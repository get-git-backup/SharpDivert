using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert
{
    /// <summary>
    /// General implementation of received or transmitted packet data using WinDivert.
    /// </summary>
    public interface IPacket
    {
        byte[] DivertRawData { get; }

        object TcpHeader { get; }
        object UdpHeader { get; }
        object IcmpHeader { get; }
        object IcmpV6Header { get; }
        object IpHeader { get; }
        object IpV6Header { get; }

        byte[] PacketData { get; }
        object PacketAddress { get; }
        uint PacketLength { get; }
    }
}
