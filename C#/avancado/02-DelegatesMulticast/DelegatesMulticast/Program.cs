using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegatesMulticast
{
    class Program
    {
        delegate void VerificaIdade(int idade);
        static VerificaIdade verificadorIdade;

        static void Main(string[] args)
        {
            Console.Write("Digite seu sexo: ");
            string sexoUsuario = Console.ReadLine();
            Console.Write("Digite sua idade: ");
            int idadeUsuario = Convert.ToInt32(Console.ReadLine());

            if (sexoUsuario.Equals("H"))
            {
                verificadorIdade = new VerificaIdade(VerificaIdadeHomem);
                verificadorIdade += VerificaAposentadoriaHomem;
            }
            else
            {
                verificadorIdade = new VerificaIdade(VerificaIdadeMulher);
                verificadorIdade += VerificaAposentadoriaMulher;
            }

            verificadorIdade(idadeUsuario);
            Console.ReadKey();
        }

        static void VerificaIdadeHomem(int idade)
        {
            if (idade < 21)
            {
                Console.WriteLine("Você ainda está crescendo...");
            }
            else
            {
                Console.WriteLine("Você já cresceu!");
            }
        }
        static void VerificaAposentadoriaHomem(int idade)
        {
            if(idade > 65)
            {
                Console.WriteLine("Você já pode se aposentar.");
            }
            else
            {
                Console.WriteLine("Você não pode se aposentar.");
            }
        }
        static void VerificaIdadeMulher(int idade)
        {
            if (idade < 18)
            {
                Console.WriteLine("Você ainda está crescendo...");
            }
            else
            {
                Console.WriteLine("Você já cresceu!");
            }
        }
        static void VerificaAposentadoriaMulher(int idade)
        {
            if (idade > 62)
            {
                Console.WriteLine("Você já pode se aposentar.");
            }
            else
            {
                Console.WriteLine("Você não pode se aposentar.");
            }
        }
    }
}
