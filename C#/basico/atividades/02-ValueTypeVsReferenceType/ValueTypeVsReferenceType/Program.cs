using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValueTypeVsReferenceType
{
    class Program
    {
        static void Main(string[] args)
        {
            // Passagem por valor
            int num1 = 7;
            int num2 = num1;
            num1 = 14;
            Console.WriteLine("num1 = {0}, num2 = {1}", num1, num2);
            Console.ReadKey();

            // Passagem por referência
            Teste teste1 = new Teste();
            teste1.Valor = 7;
            Teste teste2 = teste1;
            teste1.Valor = 14;
            Console.WriteLine("teste1.Valor = {0}, teste2.Valor = {1}", teste1.Valor, teste2.Valor);
            Console.ReadKey();
        }
    }

    class Teste
    {
        public int Valor;
    }
}
