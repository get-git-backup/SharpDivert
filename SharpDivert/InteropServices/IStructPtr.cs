using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDivert.InteropServices
{
    /// <summary>
    /// Abstract header methods..
    /// </summary>
    public interface IStructPtr
    {
        bool IsValid();
        IntPtr GetStructureHandle(bool oriValues);
    }
}
