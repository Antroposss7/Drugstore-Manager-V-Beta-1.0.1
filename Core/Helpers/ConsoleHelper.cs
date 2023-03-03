using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class ConsoleHelper
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct ConsoleFont
        {
            public uint Index;
            public short SizeX, SizeY;
        }



        public static void WriteWithColor(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            Console.ResetColor();
        }

        public static void WriteWithCondition(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
            Console.ResetColor();
        }

       
    }

}



