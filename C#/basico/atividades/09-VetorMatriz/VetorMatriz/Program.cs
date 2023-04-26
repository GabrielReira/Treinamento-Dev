using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VetorMatriz
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vetores
            int[] vetor = new int[10];
            for (int i = -1; i < 9; i++)
            {
                vetor[i+1] = i + 1;
            }
            foreach (int n in vetor)
            {
                Console.WriteLine(n);
            }
            Console.WriteLine("Tamanho do vetor: {0}", vetor.Length);
            Console.ReadKey();

            // Matrizes
            int[,] matriz = new int[2, 4];
            int valor = 1;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    matriz[i, j] = valor;
                    valor *= 3;
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("Matriz[{0}, {1}] = {2}", i, j, matriz[i, j]);
                }
            }
            Console.WriteLine("Dimensão da matriz: {0}", matriz.Rank);
            Console.WriteLine("Tamanho da matriz: {0}", matriz.Length);
            Console.ReadKey();
        }
    }
}
