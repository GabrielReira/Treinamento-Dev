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
    public partial class GerenciamentoPerguntas : System.Web.UI.Page
    {
        PerguntaDAO ioPerguntaDAO = new PerguntaDAO();
        QuestionarioDAO ioQuestionarioDAO = new QuestionarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaDados();
                CarregaDropDowns();
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
            try
            {
                if (ddlFiltroQuestionario.SelectedValue == "NENHUM")
                    ListaPerguntas = null;
                else
                    ListaPerguntas = ioPerguntaDAO.BuscaPerguntasPorQuestionario(ioQuestionarioDAO.BuscaQuestionarios(Convert.ToDecimal(ddlFiltroQuestionario.SelectedValue)).ToList().FirstOrDefault());
                gvGerenciamentoPerguntas.DataSource = ListaPerguntas.OrderBy(p => p.per_nu_ordem).ToList();
                gvGerenciamentoPerguntas.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar perguntas.');</script>");
            }
        }

        private void CarregaDropDowns()
        {
            try
            {
                // Dropdown de questionários no cadastro de pergunta
                ddlCadastroQuestionario.DataSource = ioQuestionarioDAO.BuscaQuestionarios().OrderBy(q => q.qst_nm_questionario);
                ddlCadastroQuestionario.DataValueField = "qst_id_questionario";
                ddlCadastroQuestionario.DataTextField = "qst_nm_questionario";                
                ddlCadastroQuestionario.DataBind();

                // Dropdown do filtro de questionários
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

        // Botão para cadastrar nova pergunta
        protected void BtnNovaPergunta_Click(object sender, EventArgs e)
        {
            try
            {
                // Utilizando LINQ para realizar a lógica de incremento da PK
                decimal ldcIdPergunta = ioPerguntaDAO.BuscaPerguntas().OrderByDescending(p => p.per_id_pergunta).First().per_id_pergunta + 1;
                decimal ldcIdQuestionario = Convert.ToDecimal(ddlCadastroQuestionario.SelectedValue);
                string lsDescricaoPergunta = tbxCadastroDescricaoPergunta.Text;
                char lcTipoPergunta = ddlCadastroTipoPergunta.SelectedValue[0];
                char lcRespostaObrigatoria = ddlCadastroRespostaObrigatoria.SelectedValue[0];
                int liOrdemPergunta = Convert.ToInt32(tbxCadastroOrdemPergunta.Text);

                // Instanciando um objeto do tipo Pergunta para ser adicionado
                Pergunta loPergunta = new Pergunta(ldcIdPergunta, ldcIdQuestionario, lsDescricaoPergunta, lcTipoPergunta, lcRespostaObrigatoria, liOrdemPergunta);

                // Verificar se há pergunta no questionário com a mesma informação de ordem
                if (ioPerguntaDAO.BuscaPerguntaPorOrdem(loPergunta).Count != 0)
                    HttpContext.Current.Response.Write("<script>alert('Já existe pergunta neste questionário com a ordem indicada.');</script>");
                // Verificar se o questionário é de AVALIAÇÃO, se for, não pode conter pergunta de múltipla escolha
                else if (
                    (ioQuestionarioDAO.BuscaQuestionarios(ldcIdQuestionario).FirstOrDefault().qst_tp_questionario == 'A') &&
                    (lcTipoPergunta == 'M')
                )
                    HttpContext.Current.Response.Write("<script>alert('Questionário de avalição não pode conter pergunta de múltipla escolha.');</script>");
                else
                {
                    ioPerguntaDAO.InserePergunta(loPergunta);
                    CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Pergunta cadastrada com sucesso!');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao cadastrar pergunta.');</script>");
            }

            // Limpando os campos do formulário ao fim
            tbxCadastroDescricaoPergunta.Text = String.Empty;
            tbxCadastroOrdemPergunta.Text = String.Empty;
        }

        // Gerenciar exibição do botão editar
        protected void gvGerenciamentoPerguntas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvGerenciamentoPerguntas.EditIndex = e.NewEditIndex;
            CarregaDados();
        }

        // Gerenciar exibição do botão cancelar edição
        protected void gvGerenciamentoPerguntas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Indica que nenhuma linha está sendo editada
            gvGerenciamentoPerguntas.EditIndex = -1;
            CarregaDados();
        }

        // Gerenciar edição dos dados de uma pergunta
        protected void gvGerenciamentoPerguntas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {        
            decimal ldcIdPergunta = Convert.ToDecimal((gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblEditIdPergunta") as Label).Text);
            decimal ldcIdQuestionario = Convert.ToDecimal((gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblEditIdQuestionario") as Label).Text);
            string lsDescricaoPergunta = (gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("tbxEditDescricaoPergunta") as TextBox).Text;
            char lcTipoPergunta = (gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("ddlEditTipoPergunta") as DropDownList).SelectedItem.Value[0];
            char lcRespostaObrigatoria = (gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("ddlEditRespostaObrigatoria") as DropDownList).SelectedItem.Value[0];
            int liOrdemPergunta = Convert.ToInt32((gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("tbxEditOrdemPergunta") as TextBox).Text);


            // Validar se todos os campos foram preenchidos corretamente
            if (String.IsNullOrWhiteSpace(lsDescricaoPergunta))
                HttpContext.Current.Response.Write("<script>alert('Informe a descrição da pergunta.');</script>");
            else if (liOrdemPergunta <= 0)
                HttpContext.Current.Response.Write("<script>alert('A ordem da pergunta deve ser um inteiro maior que zero.');</script>");
            
            // TO DO: verificar se já existe pergunta na ordem informada (se eu tivesse mais tempo conseguiria)
            // É preciso salvar a ordem da pergunta ao clicar em editar
            //else if ((ioPerguntaDAO.BuscaPerguntaPorOrdem(loPergunta).Count != 0))
            //  HttpContext.Current.Response.Write("<script>alert('Já existe pergunta neste questionário com a ordem indicada.');</script>");

            // Verificar se o questionário é de AVALIAÇÃO, se for, não pode conter pergunta de múltipla escolha
            else if (
                (ioQuestionarioDAO.BuscaQuestionarios(ldcIdQuestionario).FirstOrDefault().qst_tp_questionario == 'A') &&
                (lcTipoPergunta == 'M')
            )
                HttpContext.Current.Response.Write("<script>alert('Questionário de avalição não pode conter pergunta de múltipla escolha.');</script>");
            else
            {
                try
                {
                    Pergunta loPergunta = new Pergunta(ldcIdPergunta, ldcIdQuestionario, lsDescricaoPergunta, lcTipoPergunta, lcRespostaObrigatoria, liOrdemPergunta);
                    ioPerguntaDAO.AtualizaPergunta(loPergunta);
                    gvGerenciamentoPerguntas.EditIndex = -1;  // indica que acabou a edição
                    CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Os dados da pergunta foram atualizadas com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização da pergunta.');</script>");
                }
            }
        }

        // Gerenciar remoção de uma pergunta
        protected void gvGerenciamentoPerguntas_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = gvGerenciamentoPerguntas.Rows[e.RowIndex];
                decimal ldcIdPergunta = Convert.ToDecimal((gvGerenciamentoPerguntas.Rows[e.RowIndex].FindControl("lblIdPergunta") as Label).Text);
                Pergunta loPergunta = ioPerguntaDAO.BuscaPerguntas(ldcIdPergunta).FirstOrDefault();
                if (loPergunta != null)
                {
                    OpcaoRespostaDAO loOpcaoRespostaDAO = new OpcaoRespostaDAO();
                    // Verificar se há opções de resposta associadas à pergunta
                    if (loOpcaoRespostaDAO.BuscaOpcoesRespostaPorPergunta(loPergunta).Count == 0)
                    {
                        ioPerguntaDAO.RemovePergunta(loPergunta);
                        CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Pergunta removida com sucesso!');</script>");
                    }
                    else
                        HttpContext.Current.Response.Write("<script>alert('Esta pergunta não pode ser removida pois existe(m) opção de resposta associada a ela.');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção da pergunta.');</script>");
            }
        }

        // Filtro para alterar as perguntas a depender do questionário selecionado
        protected void filtroQuestionarioAlterado(object sender, EventArgs e)
        {
            DropDownList ddlFiltroPerguntas = sender as DropDownList;
            ViewState["FiltroPerguntas"] = ddlFiltroPerguntas.SelectedValue;
            CarregaDados();
        }
    }
}