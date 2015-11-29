using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDivert.InteropServices;
using System.Runtime.InteropServices;

namespace SharpDivert.Helpers
{
    public class IcmpHeader : IStructPtr
    {
        #region IStructPtr Implementation
        protected IntPtr _unmanagedStructPtr;
        protected WINDIVERT_ICMPHDR _managedStruct;

        public bool IsValid()
        {
            return _unmanagedStructPtr != IntPtr.Zero;
        }

        public IntPtr GetStructureHandle(bool oriValues)
        {
            if (!oriValues)
            {
                Marshal.StructureToPtr(_managedStruct, _unmanagedStructPtr, true);
            }
            return _unmanagedStructPtr;
        }
        #endregion

        public byte Type
        {
            get
            {
                return _managedStruct.Type;
            }
            set
            {
                _managedStruct.Type = value;
            }
        }

        public byte Code
        {
            get
            {
                return _managedStruct.Code;
            }
            set
            {
                _managedStruct.Code = value;
            }
        }

        public ushort Checksum
        {
            get
            {
                return _managedStruct.Checksum;
            }
            set
            {
                _managedStruct.Checksum = value;
            }
        }

        public uint Body
        {
            get
            {
                return _managedStruct.Body;
            }
            set
            {
                _managedStruct.Body = value;
            }
        }
    }
}
