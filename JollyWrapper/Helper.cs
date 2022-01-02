using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JollyWrapper
{
    public static partial class Database
    {
        private static void OutputError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("JollyWrapper: ");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
