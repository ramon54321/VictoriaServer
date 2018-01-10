using System;
using System.Collections.Generic;
using System.Text;

namespace VictoriaServer
{
    class Printer : SharpLogger.Printer
    {
        public override void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
