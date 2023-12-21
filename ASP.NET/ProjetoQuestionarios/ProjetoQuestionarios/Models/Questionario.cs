using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoQuestionarios.Models
{
    [Serializable]
    public class Questionario
    {
        public decimal qst_id_questionario { get; set; }
        public string qst_nm_questionario { get; set; }
        public char qst_tp_questionario { get; set; }
        public string qst_ds_link_instrucoes { get; set; }

        public Questionario(decimal idQuestionario, string nomeQuestionario, char tipoQuestionario, string linkInstrucoes)
        {
            this.qst_id_questionario = idQuestionario;
            this.qst_nm_questionario = nomeQuestionario;
            this.qst_tp_questionario = tipoQuestionario;
            this.qst_ds_link_instrucoes = linkInstrucoes;
        }
    }
}