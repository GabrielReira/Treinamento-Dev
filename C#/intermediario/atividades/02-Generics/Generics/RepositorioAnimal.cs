using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{
    public class RepositorioAnimal
    {
        private List<Animal> animais;

        public RepositorioAnimal()
        {
            animais = new List<Animal>();
        }

        public List<Animal> Get()
        {
            return animais;
        }

        public void Insert(Animal animal)
        {
            animais.Add(animal);
        }
    }
}
