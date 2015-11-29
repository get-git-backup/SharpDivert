using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


namespace SharpDivert.InteropServices
{
    /// <summary>
    /// Provides safe handle to WinDivert capture handle.
    /// </summary>
    public class DivertSafeHandle : SafeHandleMinusOneIsInvalid
    {
        public DivertSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.WinDivertClose(this.handle);
        }
    }
}
