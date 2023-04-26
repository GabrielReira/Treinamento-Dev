using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    class Caminhao : Veiculo
    {
        public Caminhao()
        {
            this.motoristas = new List<MotoristaCaminhao>();
        }

        public override string Abastecer()
        {
            return "Abastecendo o caminhão........";
        }

        public override string Desligar()
        {
            return "O caminhão está desligado";
        }

        private List<MotoristaCaminhao> motoristas;

        public void AdicionaMotorista(MotoristaCaminhao motorista)
        {
            motoristas.Add(motorista);
        }

        public List<MotoristaCaminhao> GetMotoristas()
        {
            return motoristas;
        }
    }
}
