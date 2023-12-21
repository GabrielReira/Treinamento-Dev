using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoLivraria.Models
{
    [Serializable]
    public class Editores
    {
        public decimal edi_id_editor { get; set; }
        public string edi_nm_editor { get; set; }
        public string edi_ds_email { get; set; }
        public string edi_ds_url { get; set; }

        public Editores(decimal idEditor, string nomeEditor, string emailEditor, string urlEditor)
        {
            this.edi_id_editor = idEditor;
            this.edi_nm_editor = nomeEditor;
            this.edi_ds_email = emailEditor;
            this.edi_ds_url = urlEditor;
        }
    }
}