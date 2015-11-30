using SharpDivert.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpDivert.Helpers
{
    public class IcmpV6Header  : IStructPtr
    {
        #region IStructPtr Implementation
        protected IntPtr _unmanagedStructPtr;
        protected WINDIVERT_ICMPV6HDR _managedStruct;

        internal IcmpV6Header(IntPtr structHandle)
        {
            _unmanagedStructPtr = structHandle;
            _managedStruct = (WINDIVERT_ICMPV6HDR)Marshal.PtrToStructure(structHandle, typeof(WINDIVERT_ICMPV6HDR));
        }

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
