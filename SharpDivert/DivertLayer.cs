using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert
{
    /// <summary>
    /// Specifies netowork layer capture.
    /// </summary>
    public enum DivertLayer
    {
        /// <summary>
        /// Capture standard network card.
        /// </summary>
        Network = 0,
        /// <summary>
        /// Capture network loopback (localhost).
        /// </summary>
        NetworkForward = 1,
    }
}
