using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strings
{
    class Program
    {
        static void Main(string[] args)
        {
            string string1 = "ABC";
            string string2 = "XYZ";
            Console.WriteLine(string1 + " " + string2);
            Console.ReadKey();

            // String Builder
            StringBuilder sb = new StringBuilder();
            sb.Append(string1);
            sb.Append(" ");
            sb.Append(string2);
            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }
    }
}
