using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDivert.InteropServices;
using System.Runtime.InteropServices;

namespace SharpDivert
{
    public class PacketAddress : IStructPtr
    {
        #region IStructPtr Implementation
        protected IntPtr _unmanagedStructPtr;
        protected WINDIVERT_ADDRESS _managedStruct;

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

        public uint InterfaceIndex
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.IfIdx;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IsValid())
                {
                    _managedStruct.IfIdx = value;
                }
            }
        }

        public uint SubInterfaceIndex
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.SubIfIdx;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IsValid())
                {
                    _managedStruct.SubIfIdx = value;
                }
            }
        }

        public PacketDirection Direction
        {
            get
            {
                if (IsValid())
                {
                    return (PacketDirection)_managedStruct.Direction;
                }
                else
                {
                    return PacketDirection.Inbound;
                }
            }
            set
            {
                if (IsValid())
                {
                    _managedStruct.Direction = (byte)value;
                }
            }
        }
    }
}
