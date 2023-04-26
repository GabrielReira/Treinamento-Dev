using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Program
    {
        static void Main(string[] args)
        {
            Carro carro = new Carro();
            carro.Marca = "Ford";
            Console.WriteLine(carro.Marca);
            carro.AdicionaAntigoDono("Oliver");
            carro.AdicionaAntigoDono("Blume");
            foreach (string dono in carro.getAntigosDonos())
            {
                Console.WriteLine(dono);
            }
            Console.ReadKey();


            Carro carro1 = new Carro("Gol");
            carro1.ImprimeInfo();
            Carro carro2 = new Carro();
            carro2.ImprimeInfo();
            Carro carro3 = new Carro("Ford", 3);
            carro3.ImprimeInfo();
            Carro carro4 = new Carro("GM", "Gol", 2);
            carro4.ImprimeInfo();
            Carro carro5 = new Carro(nome: "Polo", marca: "VW");
            carro5.ImprimeInfo();
            Carro carro6 = new Carro
            {
                Nome = "Astra",
                Marca = "Chevrolet",
                NumeroPortas = 4,
                Potencia = 111
            };
            carro6.ImprimeInfo();
            Console.ReadKey();

            Moto moto1 = new Moto();
            moto1.Nome = "Twister";
            moto1.CapacidadeTanque = 14;
            Console.WriteLine("{0}, {1}, {2}", moto1.Nome, moto1.CapacidadeTanque, moto1.Potencia);
            moto1.Potencia = 750;
            Console.WriteLine(moto1.OuvirRonco());
            Console.ReadKey();

            Moto moto2 = new Moto();
            moto2.Nome = "Twister";
            Console.WriteLine(moto2.Equals(moto1));
            Console.WriteLine(moto2.OuvirRonco());
            Console.ReadKey();

            Veiculo carro7 = new Carro();
            Console.WriteLine(carro7.Ligar());
            Console.WriteLine(carro7.Desligar());
            Console.ReadKey();

            Console.WriteLine(moto2.Abastecer());
            Console.WriteLine(carro7.Desabastecer());
            Console.ReadKey();

            MotoristaCaminhao motorista = new MotoristaCaminhao();
            motorista.Nome = "Bino";
            motorista.Idade = 70;
            Caminhao caminhao = new Caminhao();
            caminhao.AdicionaMotorista(motorista);
            foreach (MotoristaCaminhao m in caminhao.GetMotoristas())
            {
                Console.WriteLine(m.Nome);
            }
            Console.ReadKey();
        }
    }
}
