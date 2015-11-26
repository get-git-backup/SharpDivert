using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert.Helpers
{
    /// <summary>
    /// Abstract header methods.
    /// </summary>
    public interface IStructPtr
    {
        bool IsValid();
        bool GetStructureHandle(bool oriHandle, out IntPtr handle);
    }
}
