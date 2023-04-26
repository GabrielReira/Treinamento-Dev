using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public sealed class Moto : Veiculo
    {
        public double CapacidadeTanque { get; set; }
        public string OuvirRonco()
        {
            if (VeiculoPotente)
            {
                return "Vrummmmmmmmmmm";
            }
            else
            {
                return "iajsias#*lpk(@doq#kl$abfz";
            }
        }
        public override string Ligar()
        {
            return "A moto está ligando...";
        }
        public override string Desligar()
        {
            return "Desligando a moto...";
        }

        public override string Abastecer()
        {
            return "Moto abastecida";
        }
    }
}
