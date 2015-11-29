using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert
{
    /// <summary>
    /// Specifies packet direction.
    /// </summary>
    public enum PacketDirection : byte
    {
        Outbound = 0,
        Inbound = 1,
    }
}
