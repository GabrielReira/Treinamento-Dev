using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovarianceVsContravariance
{
    // Covariante [out]: converte tipos específicos (filho) para tipos genéricos (pai)
    interface IRecuperador<out T>
    {
        T Recuperar(int codigo);
    }
}
