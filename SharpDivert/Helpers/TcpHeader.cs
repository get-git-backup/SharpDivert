using SharpDivert.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;

namespace SharpDivert.Helpers
{
    public class TcpHeader : IStructurePtr
    {
        protected IntPtr _unmanagedStructPtr;
        protected WINDIVERT_TCPHDR _managedStruct;

        public bool IsValid()
        {
            return _unmanagedStructPtr != IntPtr.Zero;
        }

        public bool GetStructureHandle(bool oriHandle, out IntPtr handle)
        {
            if (!oriHandle)
            {
                Marshal.StructureToPtr(_managedStruct, _unmanagedStructPtr, true);
            }
            handle = _unmanagedStructPtr;
            return true;
        }

      
        public ushort SourcePort
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)IPAddress.NetworkToHostOrder(_managedStruct.SrcPort);
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
                    _managedStruct.SrcPort = (ushort)IPAddress.HostToNetworkOrder(value);
                }
            }
        }

        public ushort DestinationPort
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)IPAddress.NetworkToHostOrder(_managedStruct.DstPort);
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
                    _managedStruct.DstPort = (ushort)IPAddress.HostToNetworkOrder(value);
                }
            }
        }

        public uint SequenceNumber
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.SeqNum;
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
                    _managedStruct.SeqNum = value;
                }
            }
        }

        public uint AcknowledgmentNumber
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.AckNum;
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
                    _managedStruct.AckNum = value;
                }
            }
        }

        public ushort Reserved1
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)_managedStruct.Reserved1;
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
                    _managedStruct.Reserved1 = value;
                }
            }
        }
    }
}
