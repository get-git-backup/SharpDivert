using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace SharpDivert.Native
{
    public partial class NativeConstants
    {
        public const int WINDIVERT_DIRECTION_OUTBOUND = 0;
        public const int WINDIVERT_DIRECTION_INBOUND = 1;

        public const int WINDIVERT_FLAG_SNIFF = 1;
        public const int WINDIVERT_FLAG_DROP = 2;
        [Obsolete(@"As of WinDivert 1.2, the NoChecksum attribute is deprecated, because the default behavior of the library is now to no longer automatically calculate checksums.")]
        public const int WINDIVERT_FLAG_NO_CHECKSUM = 1024;

        public const WINDIVERT_PARAM WINDIVERT_PARAM_MAX = WINDIVERT_PARAM.WINDIVERT_PARAM_QUEUE_TIME;
        public const int WinDivertMaxBuffer = 65535;
    }

    public partial class NativeMethods
    {
        public const string DivertDllPath = @"WinDivert.dll";

        [DllImport(DivertDllPath, EntryPoint = "WinDivertOpen", CallingConvention = CallingConvention.Cdecl)]
        public static extern DivertSafeHandle WinDivertOpen([InAttribute(), MarshalAs(UnmanagedType.LPStr)] string filter, 
                                                            WINDIVERT_LAYER layer, short priority, ulong flags);

        [DllImport(DivertDllPath, EntryPoint = "WinDivertRecv", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertRecv([InAttribute()] DivertSafeHandle handle, IntPtr pPacket, uint packetLen, 
                                                IntPtr pAddr, IntPtr readLen);

        [DllImport(DivertDllPath, EntryPoint = "WinDivertSend", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertSend([InAttribute()] DivertSafeHandle handle, [InAttribute()] IntPtr pPacket, 
                                                uint packetLen, [InAttribute()] ref WINDIVERT_ADDRESS pAddr, IntPtr writeLen);

        [DllImport(DivertDllPath, EntryPoint = "WinDivertClose", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertClose([InAttribute()] IntPtr handle);

        [DllImport(DivertDllPath, EntryPoint = "WinDivertSetParam", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertSetParam([InAttribute()] DivertSafeHandle handle, WINDIVERT_PARAM param, ulong value);

        [DllImport(DivertDllPath, EntryPoint = "WinDivertGetParam", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertGetParam([InAttribute()] DivertSafeHandle handle, WINDIVERT_PARAM param, 
                                                    [OutAttribute()] out ulong pValue);
        
        [DllImport(DivertDllPath, EntryPoint = "WinDivertHelperParsePacket", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WinDivertHelperParsePacket([InAttribute()] IntPtr pPacket, uint packetLen,
                                                             ref IntPtr ppIpHdr, ref IntPtr ppIpv6Hdr, ref IntPtr ppIcmpHdr,
                                                             ref IntPtr ppIcmpv6Hdr, ref IntPtr ppTcpHdr, ref IntPtr ppUdpHdr,
                                                             ref IntPtr ppData, IntPtr pDataLen);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WINDIVERT_ADDRESS
    {
        public uint IfIdx;
        public uint SubIfIdx;
        public byte Direction;
    }

    public enum WINDIVERT_LAYER
    {
        WINDIVERT_LAYER_NETWORK = 0,
        WINDIVERT_LAYER_NETWORK_FORWARD = 1,
    }

    public enum WINDIVERT_PARAM
    {
        WINDIVERT_PARAM_QUEUE_LEN = 0,
        WINDIVERT_PARAM_QUEUE_TIME = 1,
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_ICMPHDR
    {
        public byte Type;
        public byte Code;
        public ushort Checksum;
        public uint Body;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_ICMPV6HDR
    {
        public byte Type;
        public byte Code;
        public ushort Checksum;
        public uint Body;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_TCPHDR
    {
        public ushort SrcPort;
        public ushort DstPort;
        public uint SeqNum;
        public uint AckNum;

        /// Reserved1 : 4
        ///HdrLength : 4
        ///Fin : 1
        ///Syn : 1
        ///Rst : 1
        ///Psh : 1
        ///Ack : 1
        ///Urg : 1
        ///Reserved2 : 2
        public uint bitvector1;

        /// UINT16->unsigned short
        public ushort Window;

        /// UINT16->unsigned short
        public ushort Checksum;

        /// UINT16->unsigned short
        public ushort UrgPtr;

        public uint Reserved1
        {
            get
            {
                return ((uint)((this.bitvector1 & 15u)));
            }
            set
            {
                this.bitvector1 = ((uint)((value | this.bitvector1)));
            }
        }

        public uint HdrLength
        {
            get
            {
                return ((uint)(((this.bitvector1 & 240u)
                            / 16)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 16)
                            | this.bitvector1)));
            }
        }

        public uint Fin
        {
            get
            {
                return ((uint)(((this.bitvector1 & 256u)
                            / 256)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 256)
                            | this.bitvector1)));
            }
        }

        public uint Syn
        {
            get
            {
                return ((uint)(((this.bitvector1 & 512u)
                            / 512)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 512)
                            | this.bitvector1)));
            }
        }

        public uint Rst
        {
            get
            {
                return ((uint)(((this.bitvector1 & 1024u)
                            / 1024)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 1024)
                            | this.bitvector1)));
            }
        }

        public uint Psh
        {
            get
            {
                return ((uint)(((this.bitvector1 & 2048u)
                            / 2048)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 2048)
                            | this.bitvector1)));
            }
        }

        public uint Ack
        {
            get
            {
                return ((uint)(((this.bitvector1 & 4096u)
                            / 4096)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 4096)
                            | this.bitvector1)));
            }
        }

        public uint Urg
        {
            get
            {
                return ((uint)(((this.bitvector1 & 8192u)
                            / 8192)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 8192)
                            | this.bitvector1)));
            }
        }

        public uint Reserved2
        {
            get
            {
                return ((uint)(((this.bitvector1 & 49152u)
                            / 16384)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 16384)
                            | this.bitvector1)));
            }
        }
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_UDPHDR
    {
        public ushort SrcPort;
        public ushort DstPort;
        public ushort Length;
        public ushort Checksum;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_IPV6HDR
    {

        /// TrafficClass0 : 4
        ///Version : 4
        ///FlowLabel0 : 4
        ///TrafficClass1 : 4
        public uint bitvector1;

        /// UINT16->unsigned short
        public ushort FlowLabel1;

        /// UINT16->unsigned short
        public ushort Length;

        /// UINT8->unsigned char
        public byte NextHdr;

        /// UINT8->unsigned char
        public byte HopLimit;

        /// UINT32[4]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] SrcAddr;

        /// UINT32[4]
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.U4)]
        public uint[] DstAddr;

        public uint TrafficClass0
        {
            get
            {
                return ((uint)((this.bitvector1 & 15u)));
            }
            set
            {
                this.bitvector1 = ((uint)((value | this.bitvector1)));
            }
        }

        public uint Version
        {
            get
            {
                return ((uint)(((this.bitvector1 & 240u)
                            / 16)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 16)
                            | this.bitvector1)));
            }
        }

        public uint FlowLabel0
        {
            get
            {
                return ((uint)(((this.bitvector1 & 3840u)
                            / 256)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 256)
                            | this.bitvector1)));
            }
        }

        public uint TrafficClass1
        {
            get
            {
                return ((uint)(((this.bitvector1 & 61440u)
                            / 4096)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 4096)
                            | this.bitvector1)));
            }
        }
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct WINDIVERT_IPHDR
    {

        /// HdrLength : 4
        ///Version : 4
        public uint bitvector1;

        /// UINT8->unsigned char
        public byte TOS;

        /// UINT16->unsigned short
        public ushort Length;

        /// UINT16->unsigned short
        public ushort Id;

        /// UINT16->unsigned short
        public ushort FragOff0;

        /// UINT8->unsigned char
        public byte TTL;

        /// UINT8->unsigned char
        public byte Protocol;

        /// UINT16->unsigned short
        public ushort Checksum;

        /// UINT32->unsigned int
        public uint SrcAddr;

        /// UINT32->unsigned int
        public uint DstAddr;

        public uint HdrLength
        {
            get
            {
                return ((uint)((this.bitvector1 & 15u)));
            }
            set
            {
                this.bitvector1 = ((uint)((value | this.bitvector1)));
            }
        }

        public uint Version
        {
            get
            {
                return ((uint)(((this.bitvector1 & 240u)
                            / 16)));
            }
            set
            {
                this.bitvector1 = ((uint)(((value * 16)
                            | this.bitvector1)));
            }
        }
    }

}
