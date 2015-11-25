using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert
{
    /// <summary>
    /// Specifies WinDivert capture flags.
    /// </summary>
    /// <remarks>https://reqrypt.org/windivert-doc.html#divert_helper_parse_packet</remarks>
    public enum DivertFlags
    {
        /// <summary>
        /// By default WinDivert ensures that each diverted packet has a valid checksum. If the
        /// checksum is missing (e.g. with TCP checksum offloading), WinDivert will calculate it
        /// before passing the packet to the user application. This flag disables this behavior.
        /// </summary>
        [Obsolete(@"As of WinDivert 1.2, the NoChecksum attribute is deprecated, because the default behavior of the library is now to no longer automatically calculate checksums.")]
        NoChecksum = 0,

        /// <summary>
        /// This flag opens the WinDivert handle in packet sniffing mode. In packet sniffing
        /// mode the original packet is not dropped-and-diverted (the default) but
        /// copied-and-diverted. This mode is useful for implementing packet sniffing tools
        /// similar to those applications that currently use Winpcap.
        /// </summary>
        Sniff = 1,

        /// <summary>
        /// This flag indicates that the user application does not intend to read matching
        /// packets with WinDivertRecv(), instead the packets should be silently dropped. This
        /// is useful for implementing simple packet filters using the WinDivert filter
        /// language.
        /// </summary>
        Drop = 2
    }
}
