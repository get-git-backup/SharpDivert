using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using SharpDivert.Native;
using System.Runtime.InteropServices;

namespace SharpDivert.Helpers
{
    public class TcpHeader
    {
        IntPtr _structPtr = IntPtr.Zero;
        WINDIVERT_TCPHDR _structReal;

        internal TcpHeader(IntPtr handle)
        {
            _structReal = (WINDIVERT_TCPHDR)Marshal.PtrToStructure(handle, typeof(WINDIVERT_TCPHDR));
            _structPtr = handle;
        }

        public bool IsValid
        {
            get
            {
                return _structPtr != IntPtr.Zero;
            }
        }

        public ushort SourcePort
        {
            get
            {
                if (IsValid)
                {
                    return (ushort)IPAddress.NetworkToHostOrder(_structReal.SrcPort);
                } else {
                    return 0;
                }
            }
            set
            {
                if (IsValid)
                {
                    _structReal.SrcPort = (ushort)IPAddress.HostToNetworkOrder(value);
                }
            }
        }

        public ushort DestinationPort
        {
            get
            {
                if (IsValid)
                {
                    return (ushort)IPAddress.NetworkToHostOrder(_structReal.DstPort);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IsValid)
                {
                    _structReal.DstPort = (ushort)IPAddress.HostToNetworkOrder(value);
                }
            }
        }

        public uint SequenceNumber
        {
            get
            {
                if (IsValid)
                {
                    return (uint)_structReal.DstPort;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (IsValid)
                {
                    _structReal.SeqNum = (uint)value;
                }
            }
        }


    }
}
