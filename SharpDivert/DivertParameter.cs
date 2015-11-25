using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert
{
    /// <summary>
    /// Specifies WinDivert parameter for use in 
    /// </summary>
    /// <remarks>https://reqrypt.org/windivert-doc.html#divert_set_param</remarks>
    public enum DivertParameter
    {
        /// <summary>
        /// Sets the maximum length of the packet queue for WinDivertRecv(). Currently the
        /// default value is 512, the minimum is 1, and the maximum is 8192.
        /// </summary>
        QueueLength = 0,
        /// <summary>
        /// Sets the minimum time, in milliseconds, a packet can be queued before it is
        /// automatically dropped. Packets cannot be queued indefinitely, and ideally, packets
        /// should be processed by the application as soon as is possible. Note that this sets
        /// the minimum time a packet can be queued before it can be dropped. The actual time
        /// may be exceed this value. Currently the default value is 512, the minimum is 128,
        /// and the maximum is 2048.
        /// </summary>
        QueueTime = 1,
    }
}
