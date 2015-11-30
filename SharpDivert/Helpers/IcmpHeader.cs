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
        protected IntPtr _unmanagedStructPtrV6;
        protected WINDIVERT_ICMPV6HDR _managedStructV6;

        internal IcmpHeader(IntPtr structHandle, IntPtr structHandleV6)
        {
            _unmanagedStructPtr = structHandle;
            _managedStruct = (WINDIVERT_ICMPHDR)Marshal.PtrToStructure(structHandle, typeof(WINDIVERT_ICMPHDR));

            _unmanagedStructPtrV6 = structHandleV6;
            _managedStructV6 = (WINDIVERT_ICMPV6HDR)Marshal.PtrToStructure(structHandleV6, typeof(WINDIVERT_ICMPV6HDR));
        }

        public bool IsValid()
        {
            return _unmanagedStructPtr != IntPtr.Zero;
        }

        [Obsolete("Don't use this overload. Use the second overload instead.", true)]
        public IntPtr GetStructureHandle(bool oriValues)
        {
            throw new NotImplementedException();
        }

        public IntPtr GetStructureHandle(bool oriValues, bool icmpver6 = false)
        {
            if (!icmpver6)
            {
                if (!oriValues)
                {
                    Marshal.StructureToPtr(_managedStruct, _unmanagedStructPtr, true);
                }
                return _unmanagedStructPtr;
            }
            else
            {
                if (!oriValues)
                {
                    Marshal.StructureToPtr(_managedStructV6, _unmanagedStructPtrV6, true);
                }
                return _unmanagedStructPtrV6;
            }
        }
        #endregion

        #region Icmp version 4.0
        public byte IcmpType
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

        public byte IcmpCode
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

        public ushort IcmpChecksum
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

        public uint IcmpBody
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
        #endregion

        #region Icmp version 6.0
        public byte IcmpV6Type
        {
            get
            {
                return _managedStructV6.Type;
            }
            set
            {
                _managedStructV6.Type = value;
            }
        }

        public byte IcmpV6Code
        {
            get
            {
                return _managedStructV6.Code;
            }
            set
            {
                _managedStructV6.Code = value;
            }
        }

        public ushort IcmpV6Checksum
        {
            get
            {
                return _managedStructV6.Checksum;
            }
            set
            {
                _managedStructV6.Checksum = value;
            }
        }

        public uint IcmpV6Body
        {
            get
            {
                return _managedStructV6.Body;
            }
            set
            {
                _managedStructV6.Body = value;
            }
        }
        #endregion
    }
}
