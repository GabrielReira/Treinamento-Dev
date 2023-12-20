using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovarianceVsContravariance
{
    // Contravariante [in]: converte tipos genéricos (pai) para tipos específicos (filho)
    interface IArmazenador<in T>
    {
        void Armazenar(T item);
    }
}
