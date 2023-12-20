using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetodosDeExtensao
{
    class Program
    {
        static void Main(string[] args)
        {
            string frase = Console.ReadLine();
            Console.WriteLine(frase.UpperLowerCase(false));
            Console.WriteLine(frase.UpperLowerCase(true));
            Console.ReadKey();
        }
    }
}
