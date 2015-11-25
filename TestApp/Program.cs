using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDivert;

namespace TestApp
{
    class Program
    {
        static SharpDivertMain dvr = new SharpDivertMain();
        
        static void Main(string[] args)
        {
            Console.CancelKeyPress += Console_CancelKeyPress;
                        
            dvr.Open(@"true", DivertLayer.Network, 0, DivertFlags.Sniff);
            
            while (true)
            {
                dvr.Receive();
            }
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            
        }
    }
}
