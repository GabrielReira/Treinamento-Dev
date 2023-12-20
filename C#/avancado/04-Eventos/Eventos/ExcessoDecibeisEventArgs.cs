using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventos
{
    public class ExcessoDecibeisEventArgs : EventArgs
    {
        public int IntensidadeLatido { get; set; }
    }
}
