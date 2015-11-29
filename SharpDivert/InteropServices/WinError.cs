using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert.InteropServices
{
    /// <summary>
    /// Contains Windows's error codes.
    /// </summary>
    internal class WinError
    {
        public const int ERROR_SUCCESS = 0x0;

        public const int ERROR_FILE_NOT_FOUND = 0x2;
        public const int ERROR_ACCESS_DENIED = 0x5;
        public const int ERROR_INVALID_HANDLE = 0x6;
        public const int ERROR_INVALID_PARAMETER = 0x57;
        public const int ERROR_INVALID_IMAGE_HASH = 0x241;
        public const int ERROR_DRIVER_BLOCKED = 0x4FB;
    }
}
