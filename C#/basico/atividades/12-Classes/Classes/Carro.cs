using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public sealed class Carro : Veiculo
    {
        public int NumeroPortas { get; set; }
        private List<string> antigosDonos;

        public Carro()
        {
            this.antigosDonos = new List<string>();
            this.NumeroPortas = 4;
        }
        public Carro(string nomeCarro)
        {
            this.Nome = nomeCarro;
            this.Potencia = 707.14;
        }
        public Carro(string nomeMarca, int numeroPortas)
        {
            this.Marca = nomeMarca;
            this.NumeroPortas = numeroPortas;
        }
        public Carro(string marca, string nome, int qtdPortas = 4)
        {
            this.Marca = marca;
            this.Nome = nome;
            this.NumeroPortas = qtdPortas;
        }

        public void AdicionaAntigoDono (string nome)
        {
            antigosDonos.Add(nome);
        }
        public List<string> getAntigosDonos()
        {
            return antigosDonos;
        }

        public void ImprimeInfo()
        {
            Console.WriteLine(
                "Marca: " + this.Marca + ", Nome: " + this.Nome + 
                ", Portas: " + this.NumeroPortas + ", Potência: " + this.Potencia
            );
        }

        public override string Ligar()
        {
            return "O carro está ligando...";
        }
        public override string Desligar()
        {
            return "Desligando o carro...";
        }

        public override string Abastecer()
        {
            return "O carro está abastecido";
        }
    }
}
