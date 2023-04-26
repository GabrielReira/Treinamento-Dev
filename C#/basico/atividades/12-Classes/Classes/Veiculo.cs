using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public abstract class Veiculo : IVeiculo
    {
        private string marca;
        private double potencia;
        public string Nome { get; set; }
        public string Marca
        {
            get
            {
                return marca;
            }
            set
            {
                if (value == "Ford")
                {
                    marca = "VW";
                }
                else
                {
                    marca = value;
                }
            }
        }
        public double Potencia {
            get
            {
                return potencia;
            }
            set
            {
                if (value >= 700)
                {
                    VeiculoPotente = true;
                }
                else
                {
                    VeiculoPotente = false;
                }
                potencia = value;
            }
        }
        protected bool VeiculoPotente { get; set; }

        public override bool Equals(object obj)
        {
            Veiculo veiculo = (Veiculo)obj;
            return (veiculo.Nome == this.Nome && veiculo.Marca == this.Marca);
        }

        public virtual string Ligar()
        {
            return "O veículo está ligando...";
        }
        public abstract string Desligar();

        public abstract string Abastecer();

        public string Desabastecer()
        {
            return "O veículo foi desabastecido.";
        }
    }
}
