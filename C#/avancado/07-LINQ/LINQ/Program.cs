using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Pessoa> pessoas = CarregarListaPessoa();
            // Todas as pessoas com mais de 10 irmãos
                // 1a forma
            var pessoasMaisDezIrmaos1 = from pessoa in pessoas
                                       where pessoa.QtdIrmaos > 10
                                       select pessoa;
                // 2a forma
            IEnumerable<Pessoa> pessoasMaisDezIrmaos2 = pessoas.Where(p => p.QtdIrmaos > 10);

            foreach (Pessoa p in pessoasMaisDezIrmaos2)
            {
                Console.WriteLine("{0} {1} {2}", p.Nome, p.Idade, p.QtdIrmaos);
            }


            // Nome das pessoas maiores de idade
                // 1a forma
            var pessoasMaioresIdade1 = from pessoa in pessoas
                                      where pessoa.Idade >= 18
                                      orderby pessoa.Idade descending
                                      select new { pessoa.Nome };
                // 2a forma
            var pessoasMaioresIdade2 = pessoas.Where(p => p.Idade >= 18)
                                                              .OrderByDescending(p => p.Idade)
                                                              .Select(p => new { p.Nome });

            foreach (var p in pessoasMaioresIdade2)
            {
                Console.WriteLine(p);
            }

            Console.ReadKey();
        }

        static List<Pessoa> CarregarListaPessoa()
        {
            List<Pessoa> pessoas = new List<Pessoa>();
            pessoas.Add(new Pessoa
            {
                Nome = "Ademário",
                Idade = 55,
                QtdIrmaos = 9
            });
            pessoas.Add(new Pessoa
            {
                Nome = "Bertolina",
                Idade = 66,
                QtdIrmaos = 13
            });
            return pessoas;
        }
    }

    class Pessoa
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int QtdIrmaos { get; set; }
    }
}
