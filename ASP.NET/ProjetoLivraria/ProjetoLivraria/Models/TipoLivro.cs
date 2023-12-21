using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class TipoLivro
    {
        public decimal til_id_tipo_livro { get; set; }
        public string til_ds_descricao { get; set; }

        public TipoLivro(decimal idTipoLivro, string descricaoTipoLivro)
        {
            this.til_id_tipo_livro = idTipoLivro;
            this.til_ds_descricao = descricaoTipoLivro;
        }
    }
}