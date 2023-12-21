using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Autores
    {
        public decimal aut_id_autor { get; set; }
        public string aut_nm_nome { get; set; }
        public string aut_nm_sobrenome { get; set; }
        public string aut_ds_email { get; set; }

        public Autores(decimal idAutor, string nomeAutor, string sobrenomeAutor, string emailAutor)
        {
            this.aut_id_autor = idAutor;
            this.aut_nm_nome = nomeAutor;
            this.aut_nm_sobrenome = sobrenomeAutor;
            this.aut_ds_email = emailAutor;
        }
    }
}