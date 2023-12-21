using ProjetoQuestionarios.DAO;
using ProjetoQuestionarios.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace ProjetoQuestionarios.Questionarios
{
    public partial class ResponderQuestionario : System.Web.UI.Page
    {
        QuestionarioDAO ioQuestionarioDAO = new QuestionarioDAO();
        PerguntaDAO ioPerguntaDAO = new PerguntaDAO();
        OpcaoRespostaDAO ioOpcaoRespostaDAO = new OpcaoRespostaDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDados();
                CarregaDdlQuestionarios();
                GerarControlesDinamicos();
            }            
        }

        // ViewState para armazenar o questionário selecionado
        public BindingList<Questionario> QuestionarioSelecionado
        {
            get
            {
                if ((BindingList<Questionario>)ViewState["FiltroQuestionario"] == null)
                    QuestionarioSelecionado = ioQuestionarioDAO.BuscaQuestionarios(-1);
                return (BindingList<Questionario>)ViewState["FiltroQuestionario"];
            }
            set
            {
                ViewState["FiltroQuestionario"] = value;
            }
        }

        // ViewState para armazenar as perguntas do questionário selecionado
        public BindingList<Pergunta> ListaPerguntas
        {
            get
            {
                if ((BindingList<Pergunta>)ViewState["ViewStateListaPerguntas"] == null)
                    ListaPerguntas = ioPerguntaDAO.BuscaPerguntas(-1);
                return (BindingList<Pergunta>)ViewState["ViewStateListaPerguntas"];
            }
            set
            {
                ViewState["ViewStateListaPerguntas"] = value;
            }
        }

        // ViewState para armazenar as opções de resposta das perguntas
        public BindingList<OpcaoResposta> ListaOpcoesResposta
        {
            get
            {
                if ((BindingList<OpcaoResposta>)ViewState["ViewStateListaOpcoesResposta"] == null)
                    ListaOpcoesResposta = ioOpcaoRespostaDAO.BuscaOpcaoRespostas(-1);
                return (BindingList<OpcaoResposta>)ViewState["ViewStateListaOpcoesResposta"];
            }
            set
            {
                ViewState["ViewStateListaOpcoesResposta"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                if (ddlFiltroQuestionario.SelectedValue == "NENHUM")
                    QuestionarioSelecionado = null;
                else
                {
                    QuestionarioSelecionado = ioQuestionarioDAO.BuscaQuestionarios(Convert.ToDecimal(ddlFiltroQuestionario.SelectedValue));
                    ListaPerguntas = ioPerguntaDAO.BuscaPerguntasPorQuestionario(QuestionarioSelecionado.FirstOrDefault());
                    ListaPerguntas.OrderBy(p => p.per_nu_ordem);
                    GerarControlesDinamicos();
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar questionário selecionado.');</script>");
            }
        }

        // Filtro de seleção do questionário
        protected void filtroQuestionarioAlterado(object sender, EventArgs e)
        {
            DropDownList ddlFiltrarQuestionario = sender as DropDownList;
            ViewState["FiltroQuestionario"] = ddlFiltrarQuestionario.SelectedValue;
            CarregaDados();
            if (ddlFiltroQuestionario.SelectedValue != "NENHUM")          
                GerarTituloEBotaoEnviar();                
        }

        private void CarregaDdlQuestionarios()
        {
            try
            {
                ddlFiltroQuestionario.DataSource = ioQuestionarioDAO.BuscaQuestionarios().OrderBy(q => q.qst_nm_questionario);
                ddlFiltroQuestionario.DataValueField = "qst_id_questionario";
                ddlFiltroQuestionario.DataTextField = "qst_nm_questionario";
                ddlFiltroQuestionario.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar carregar o conteúdo das dropdowns do questionário.');</script>");
            }
        }

        // Gerenciamento dos controles
        private void GerarControlesDinamicos()
        {
            for (int i = 0; i < ListaPerguntas.Count(); i++)
            {
                // Criar um 'td' no formulário para cada pergunta
                Pergunta pergunta = ListaPerguntas[i];
                HtmlTableCell td = new HtmlTableCell();
                td.InnerText = i+1 + " - " + pergunta.per_ds_pergunta;
                td.Controls.Add(new LiteralControl("<BR>"));
                tr.Controls.Add(td);

                // Verificar quais são as opções de resposta daquela pergunta
                ListaOpcoesResposta = ioOpcaoRespostaDAO.BuscaOpcoesRespostaPorPergunta(pergunta);
                ListaOpcoesResposta.OrderBy(o => o.opr_nu_ordem);

                for (int j = 0; j < ListaOpcoesResposta.Count(); j++)
                {
                    OpcaoResposta opcaoDeResposta = ListaOpcoesResposta[j];

                    // Adcionar o input a depender do tipo de pergunta (única ou múltipla escolha)
                    if (pergunta.per_tp_pergunta == 'U')
                    {
                        HtmlInputRadioButton loOpcao = new HtmlInputRadioButton();
                        loOpcao.Attributes.Add("TYPE", "RADIO");
                        td.Controls.Add(loOpcao);
                    }
                    else if (pergunta.per_tp_pergunta == 'M')
                    {
                        HtmlInputCheckBox loOpcao = new HtmlInputCheckBox();
                        td.Controls.Add(loOpcao);
                    }
                    td.Controls.Add(new LiteralControl(" " + opcaoDeResposta.opr_ds_opcao_resposta));
                    td.Controls.Add(new LiteralControl("<BR>"));
                }
                td.Controls.Add(new LiteralControl("<BR>"));
            }            
        }

        // Gerar título para a div com perguntas e o botão de enviar
        private void GerarTituloEBotaoEnviar()
        {
            DivPrincipal.Controls.AddAt(0, new LiteralControl("<h3>Perguntas do questionário selecionado</h3>"));
            HtmlButton botaoEnviar = new HtmlButton();
            botaoEnviar.Controls.Add(new LiteralControl("Enviar"));
            DivPrincipal.Controls.Add(botaoEnviar);
        }
    }
}