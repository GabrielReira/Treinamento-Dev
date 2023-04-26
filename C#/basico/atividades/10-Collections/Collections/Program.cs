using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class Program
    {
        static void Main(string[] args)
        {
            // List
            List<int> listaNumeros = new List<int>();
            for (int i = 1; i < 10; i++)
            {
                listaNumeros.Add(i);
            }
            listaNumeros.Remove(7);
            listaNumeros.RemoveAt(0);
            foreach (int n in listaNumeros)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine(listaNumeros.IndexOf(9));
            Console.ReadKey();

            // Dictionary
            Dictionary<string, string> dicionario = new Dictionary<string, string>();
            dicionario.Add("Joy", "a feeling of great pleasure and happiness");
            dicionario.Add("Division", "the action of separating something into parts or the process of being separated");
            foreach (string k in dicionario.Keys)
            {
                Console.WriteLine("{0} means {1}", k, dicionario[k]);
            }
            Console.ReadKey();

            Dictionary<int, string> valores = new Dictionary<int, string>();
            valores.Add(7, "número ímpar");
            valores.Add(14, "número par");
            foreach (int k in valores.Keys)
            {
                Console.WriteLine("{0} é um {1}", k, valores[k]);
            }
            Console.ReadKey();
        }
    }
}
