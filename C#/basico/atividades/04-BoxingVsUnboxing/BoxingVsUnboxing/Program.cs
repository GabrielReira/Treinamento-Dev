using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxingVsUnboxing
{
    class Program
    {
        static void Main(string[] args)
        {
            // Boxing
            int inteiro1 = 7;
            object objeto = inteiro1;
            Console.WriteLine(objeto);
            Console.ReadKey();

            // Unboxing
            int inteiro2 = (int)objeto;
            Console.WriteLine(inteiro2);
            Console.ReadKey();
        }
    }
}
