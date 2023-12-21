using ProjetoQuestionarios.DAO;
using ProjetoQuestionarios.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoQuestionarios.Questionarios
{
    public partial class GerenciamentoOpcoesResposta : System.Web.UI.Page
    {
        QuestionarioDAO ioQuestionarioDAO = new QuestionarioDAO();
        PerguntaDAO ioPerguntaDAO = new PerguntaDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDados();
                CarregaDropDowns();
            }
        }

        // ViewState para armazenar a lista de questionários cadastrados
        public BindingList<Questionario> ListaQuestionarios
        {
            get
            {
                if ((BindingList<Questionario>)ViewState["FiltroQuestionarios"] == null)
                    ListaQuestionarios = ioQuestionarioDAO.BuscaQuestionarios(-1);
                return (BindingList<Questionario>)ViewState["FiltroQuestionarios"];
            }
            set
            {
                ViewState["FiltroQuestionarios"] = value;
            }
        }

        // ViewState para armazenar a lista de perguntas cadastradas no questionário
        public BindingList<Pergunta> ListaPerguntas
        {
            get
            {
                if ((BindingList<Pergunta>)ViewState["FiltroPerguntas"] == null)
                    ListaPerguntas = ioPerguntaDAO.BuscaPerguntas(-1);
                return (BindingList<Pergunta>)ViewState["FiltroPerguntas"];
            }
            set
            {
                ViewState["FiltroPerguntas"] = value;
            }
        }

        private void CarregaDados()
        {

        }

        private void CarregaDropDowns()
        {

        }

        protected void BtnNovaOpcaoResposta_Click(object sender, EventArgs e)
        {

        }

        protected void gvGerenciamentoOpcaoResposta_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGerenciamentoOpcoesResposta.EditIndex = e.NewEditIndex;
            CarregaDados();
        }

        protected void gvGerenciamentoOpcaoResposta_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvGerenciamentoOpcoesResposta.EditIndex = -1;
            CarregaDados();
        }

        protected void gvGerenciamentoOpcaoResposta_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvGerenciamentoOpcaoResposta_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        // Filtro para alterar as perguntas a depender do questionário selecionado
        protected void filtroQuestionarioAlterado(object sender, EventArgs e)
        {
            
        }

        // Filtro para alterar as oções de resposta a depender da pergunta selecionada
        protected void filtroPerguntaAlterado(object sender, EventArgs e)
        {

        }
    }
}