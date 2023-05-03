using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    class Program
    {
        static void Main(string[] args)
        {
            Pessoa p1 = new Pessoa
            {
                Id = 1,
                Nome = "Pessoa 1"
            };
            Animal a1 = new Animal
            {
                Id = 1,
                Especie = "Cachorro"
            };
            RepositorioPessoa repositorioPessoa1 = new RepositorioPessoa();
            RepositorioAnimal repositorioAnimal1 = new RepositorioAnimal();
            repositorioPessoa1.Insert(p1);
            repositorioAnimal1.Insert(a1);
          
            foreach (Pessoa p in repositorioPessoa1.Get())
            {
                Console.WriteLine(p.Nome);
            }
            foreach (Animal a in repositorioAnimal1.Get())
            {
                Console.WriteLine(a.Especie);
            }

            // Utilizando generics
            Pessoa p2 = new Pessoa
            {
                Id = 2,
                Nome = "Pessoa 2"
            };
            Animal a2 = new Animal
            {
                Id = 2,
                Especie = "Gato"
            };
            RepositorioGenerico<Pessoa> repositorioPessoa2 = new RepositorioGenerico<Pessoa>();
            RepositorioGenerico<Animal> repositorioAnimal2 = new RepositorioGenerico<Animal>();
            repositorioPessoa2.Insert(p2);
            repositorioAnimal2.Insert(a2);

            foreach (Pessoa p in repositorioPessoa2.Get())
            {
                Console.WriteLine(p.Nome);
            }
            foreach (Animal a in repositorioAnimal2.Get())
            {
                Console.WriteLine(a.Especie);
            }
            
           Console.ReadKey();
        }
    }
}
