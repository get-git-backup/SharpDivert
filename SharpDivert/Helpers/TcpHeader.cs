using SharpDivert.InteropServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.InteropServices;

namespace SharpDivert.Helpers
{
    public class TcpHeader : IStructPtr
    {
        #region IStructPtr Implementation
        protected IntPtr _unmanagedStructPtr;
        protected WINDIVERT_TCPHDR _managedStruct;

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

        public uint HeaderLength
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.HdrLength;
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
                    _managedStruct.HdrLength = value;
                }
            }
        }
        
        public uint Fin
        {
            get
            {
                if (IsValid())
                {
                    return _managedStruct.Fin;
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
                    _managedStruct.Fin = value;
                }
            }
        }

        public uint Syn
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Syn;
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
                    _managedStruct.Syn = value;
                }
            }
        }

        public uint Rst
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Rst;
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
                    _managedStruct.Rst = value;
                }
            }
        }

        public uint Psh
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Psh;
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
                    _managedStruct.Psh = value;
                }
            }
        }

        public uint Ack
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Ack;
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
                    _managedStruct.Ack = value;
                }
            }
        }

        public uint Urg
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Urg;
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
                    _managedStruct.Urg = value;
                }
            }
        }

        public uint Reserved2
        {
            get
            {
                if (IsValid())
                {
                    return (uint)_managedStruct.Reserved2;
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
                    _managedStruct.Reserved2 = value;
                }
            }
        }

        public ushort WindowSize
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)_managedStruct.Window;
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
                    _managedStruct.Window = value;
                }
            }
        }

        public ushort Checksum
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)_managedStruct.Checksum;
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
                    _managedStruct.Checksum = value;
                }
            }
        }

        public ushort UrgentPointer
        {
            get
            {
                if (IsValid())
                {
                    return (ushort)_managedStruct.UrgPtr;
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
                    _managedStruct.UrgPtr = value;
                }
            }
        }
    }
}
