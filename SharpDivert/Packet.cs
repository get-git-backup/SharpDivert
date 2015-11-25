using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDivert.Native;

namespace SharpDivert
{
    public class Packet 
    {
        string rd = "";

        internal Packet(byte[] dataBuffer, WINDIVERT_ADDRESS address, uint length)
        {
            
        }

        public string Rawdata
        {
            get
            {
                return rd;
            }
        }

        public string IPData;
    }
}
