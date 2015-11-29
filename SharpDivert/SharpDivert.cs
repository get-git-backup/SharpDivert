using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDivert.InteropServices;
using System.Runtime.InteropServices;

namespace SharpDivert
{
    public class SharpDivertMain : IDisposable
    {
        DivertSafeHandle _divert_handle = null;

        public void Open(string filter, DivertLayer layer, short priority, DivertFlags flags)
        {
            if (String.IsNullOrWhiteSpace(filter))
            {
                throw new ArgumentNullException("filter");
            }

            ulong m_flag = Convert.ToUInt64(flags);
            WINDIVERT_LAYER m_layer = (WINDIVERT_LAYER)layer;

            _divert_handle = NativeMethods.WinDivertOpen(filter, m_layer, priority, m_flag);
            int lastWin32Error = Marshal.GetLastWin32Error();

            if (_divert_handle.IsInvalid)
            {
                switch (lastWin32Error) {
                    case WinError.ERROR_FILE_NOT_FOUND:
                        throw new Exception("The driver files WinDivert32.sys or WinDivert64.sys were not found.");
                    case WinError.ERROR_ACCESS_DENIED:
                        throw new Exception("You don't have sufficent privilege to use the driver.");
                    case WinError.ERROR_INVALID_PARAMETER:
                        throw new Exception("Filter string, layer, priority, or flags parameters contain invalid values.");
                    case WinError.ERROR_INVALID_IMAGE_HASH:
                        throw new Exception("The WinDivert32.sys or WinDivert64.sys driver does not have a valid digital signature.");
                    case WinError.ERROR_DRIVER_BLOCKED:
                        throw new Exception("The driver is blocked from operating. This can happen for various reasons, such as interference from security software, or usage inside a virtualization environment that does not support drivers.");
                    default:
                        throw new Exception("Failed to open WinDivert safe handle. Cause is unknown. Win32 Error is " + lastWin32Error);
                }
            }
        }

        public bool Receive()
        {
            //Buffer pointer
            IntPtr bufferPtr = Marshal.AllocHGlobal(NativeConstants.WinDivertMaxBuffer);
            byte[] bufferArr = new byte[NativeConstants.WinDivertMaxBuffer];

            //Adderss pointer
            WINDIVERT_ADDRESS pAddress = new WINDIVERT_ADDRESS();
            IntPtr pAddressPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_ADDRESS)));
            Marshal.StructureToPtr(pAddress, pAddressPtr, true);

            //Read pointer
            IntPtr readPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(uint)));

            //Packet parsing
            IntPtr ipHdr = IntPtr.Zero;
            IntPtr ipv6Hdr = IntPtr.Zero;
            IntPtr icmpHdr = IntPtr.Zero;
            IntPtr icmpv6Hdr = IntPtr.Zero;
            IntPtr tcpHdr = IntPtr.Zero;
            IntPtr udpHdr = IntPtr.Zero;
            IntPtr ppData = IntPtr.Zero;
            IntPtr ppReadLen = IntPtr.Zero;

            try
            {
                //Invoke WinDivertRecv
                if (NativeMethods.WinDivertRecv(_divert_handle, bufferPtr, (uint)NativeConstants.WinDivertMaxBuffer, pAddressPtr, readPtr) == true)
                {
                    //We received a packet
                    pAddress = (WINDIVERT_ADDRESS)Marshal.PtrToStructure(pAddressPtr, typeof(WINDIVERT_ADDRESS));
                    
                    //This line is used to copy buffer data to managed Byte[]
                    //Marshal.Copy(bufferPtr, bufferArr, 0, bufferArr.Length);

                    //Packet length
                    uint readlen = (uint)Marshal.ReadInt32(readPtr);

                    //Try to parse data
                    ipHdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_IPHDR)));
                    ipv6Hdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_IPV6HDR)));
                    icmpHdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_ICMPHDR)));
                    icmpv6Hdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_ICMPV6HDR)));
                    tcpHdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_TCPHDR)));
                    udpHdr = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(WINDIVERT_UDPHDR)));
                    ppData = IntPtr.Zero; //Marshal.AllocHGlobal(NativeConstants.WinDivertMaxBuffer);
                    ppReadLen = IntPtr.Zero; //Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)));

                    //But WinDivertHelperParsePacket always return FALSE
                    //if (NativeMethods.WinDivertHelperParsePacket(bufferPtr, readlen, ref ipHdr, ref ipv6Hdr, ref icmpHdr, ref icmpv6Hdr, ref tcpHdr, ref udpHdr, ref ppData, ppReadLen) == true)
                    //{
                    NativeMethods.WinDivertHelperParsePacket(bufferPtr, readlen, ref ipHdr, ref ipv6Hdr, ref icmpHdr, ref icmpv6Hdr, ref tcpHdr, ref udpHdr, ref ppData, ppReadLen);
                        if (ipHdr != IntPtr.Zero)
                        {
                            Console.Write("IPV4 Src Address : ");
                            WINDIVERT_IPHDR dvIP = (WINDIVERT_IPHDR)Marshal.PtrToStructure(ipHdr, typeof(WINDIVERT_IPHDR));
                            Console.WriteLine(dvIP.SrcAddr);
                        }
                        if (ipv6Hdr != IntPtr.Zero)
                        {
                            Console.Write("IPV6 Src Address : ");
                            WINDIVERT_IPV6HDR dvIP = (WINDIVERT_IPV6HDR)Marshal.PtrToStructure(ipv6Hdr, typeof(WINDIVERT_IPV6HDR));
                            //Console.WriteLine(dvIP.SrcAddr);
                            byte[] ipArr = new byte[4];
                            ipArr[0] = (byte)dvIP.SrcAddr[0];
                            ipArr[1] = (byte)dvIP.SrcAddr[1];
                            ipArr[2] = (byte)dvIP.SrcAddr[2];
                            ipArr[3] = (byte)dvIP.SrcAddr[3];

                            System.Net.IPAddress ipAd = new System.Net.IPAddress(ipArr);
                            Console.WriteLine(ipAd.ToString());
                        }
                        if (icmpHdr != IntPtr.Zero)
                        {
                            Console.Write("ICMP Body : ");
                            WINDIVERT_ICMPHDR dvIP = (WINDIVERT_ICMPHDR)Marshal.PtrToStructure(ipv6Hdr, typeof(WINDIVERT_ICMPHDR));
                            Console.WriteLine(dvIP.Body);
                        }
                    //}

                    //Write to console
                    Console.WriteLine(String.Format(@"Direction : {0}, Idfx : {1}, Subidfx : {2}", pAddress.Direction, pAddress.IfIdx, pAddress.SubIfIdx));
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                //Release all resources
                Marshal.FreeHGlobal(pAddressPtr);
                Marshal.FreeHGlobal(bufferPtr);
                Marshal.FreeHGlobal(readPtr);
            }
        }



        #region Dispose Method
        void IDisposable.Dispose()
        {
            if (_divert_handle != null)
            {
                if (!_divert_handle.IsClosed) _divert_handle.Dispose();
            }
        }
        #endregion
    }
}
