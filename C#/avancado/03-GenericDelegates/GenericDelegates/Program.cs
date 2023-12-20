using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDelegates
{
    class Program
    {
        delegate T Somar<T>(T n1, T n2);
        static void Main(string[] args)
        {
            Program p = new Program();
            Somar<int> soma1 = new Somar<int>(p.SomarInteiro);
            Console.WriteLine(soma1(3, 7));
            InfoDelegate(soma1);

            Somar<double> soma2 = new Somar<double>(p.SomarDecimal);
            Console.WriteLine(soma2(3.5, 7.9));
            InfoDelegate(soma2);

            Console.ReadKey();

            // Utilizando Func e Action
            Func<int, int, int> soma3 = p.SomarInteiro;
            Action<float> imprime = p.ImprimirResultado;
            imprime(soma3(1, 11));
            Console.ReadKey();
        }

        private static void InfoDelegate<T>(Somar<T> s)
        {
            Console.WriteLine(s.Target);
            Console.WriteLine(s.Method);
        }

        void ImprimirResultado(float resultado)
        {
            Console.WriteLine(resultado);
        }

        int SomarInteiro(int n1, int n2)
        {
            return n1 + n2;
        }

        double SomarDecimal(double n1, double n2)
        {
            return n1 + n2;
        }
    }
}
