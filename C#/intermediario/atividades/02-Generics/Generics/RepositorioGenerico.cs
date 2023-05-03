using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class RepositorioGenerico<TTipo>
    {
        private List<TTipo> lista;

        public RepositorioGenerico()
        {
            lista = new List<TTipo>();
        }

        public List<TTipo> Get()
        {
            return lista;
        }

        public void Insert(TTipo obj)
        {
            lista.Add(obj);
        }
    }
}
