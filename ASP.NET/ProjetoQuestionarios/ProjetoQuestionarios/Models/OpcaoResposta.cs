using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoQuestionarios.Models
{
    [Serializable]
    public class OpcaoResposta
    {
        public decimal opr_id_opcao_resposta { get; set; }
        public decimal opr_id_pergunta { get; set; }
        public string opr_ds_opcao_resposta { get; set; }
        public char opr_ch_resposta_correta { get; set; }
        public int opr_nu_ordem { get; set; }

        public OpcaoResposta(decimal idOpcaoResposta, decimal idPergunta, string descricaoOpcaoResposta, char respostaCorreta, int ordemOpcaoResposta)
        {
            this.opr_id_opcao_resposta = idOpcaoResposta;
            this.opr_id_pergunta = idPergunta;
            this.opr_ds_opcao_resposta = descricaoOpcaoResposta;
            this.opr_ch_resposta_correta = respostaCorreta;
            this.opr_nu_ordem = ordemOpcaoResposta;
        }
    }
}