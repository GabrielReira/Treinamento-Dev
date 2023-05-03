using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class RepositorioPessoa
    {
        private List<Pessoa> pessoas;

        public RepositorioPessoa()
        {
            pessoas = new List<Pessoa>();
        }

        public List<Pessoa> Get()
        {
            return pessoas;
        }

        public void Insert(Pessoa pessoa)
        {
            pessoas.Add(pessoa);
        }
    }
}
