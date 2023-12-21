using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class LivroAutor
    {
        public decimal lia_id_autor { get; set; }
        public decimal lia_id_livro { get; set; }
        public decimal lia_pc_royalty { get; set; }

        public LivroAutor(decimal idAutor, decimal idLivro, decimal royaltyLivroAutor)
        {
            this.lia_id_autor = idAutor;
            this.lia_id_livro = idLivro;
            this.lia_pc_royalty = royaltyLivroAutor;
        }
    }
}