using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlProject.Helpers.Extentions
{
    public static class ConsoleExtention
    {
        public static void WriteConsole(this ConsoleColor color,string text)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);

            Console.ResetColor();
        }

    }
}
