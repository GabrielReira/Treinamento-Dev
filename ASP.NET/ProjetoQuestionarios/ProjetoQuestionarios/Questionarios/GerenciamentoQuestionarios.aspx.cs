using ProjetoQuestionarios.DAO;
using ProjetoQuestionarios.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoQuestionarios.Questionarios
{
    public partial class GerenciamentoQuestionarios : System.Web.UI.Page
    {
        QuestionarioDAO ioQuestionarioDAO = new QuestionarioDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregaDados();
        }

        // ViewState para armazenar a lista de questionários cadastrados
        public BindingList<Questionario> ListaQuestionarios
        {
            get
            {
                if ((BindingList<Questionario>)ViewState["ViewStateListaQuestionarios"] == null)
                    CarregaDados();
                return (BindingList<Questionario>)ViewState["ViewStateListaQuestionarios"];
            }
            set
            {
                ViewState["ViewStateListaQuestionarios"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaQuestionarios = ioQuestionarioDAO.BuscaQuestionarios();
                gvGerenciamentoQuestionarios.DataSource = ListaQuestionarios.OrderBy(n => n.qst_nm_questionario);
                this.gvGerenciamentoQuestionarios.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar questionários.');</script>");
            }
        }

        // Botão para cadastrar novo questionário
        protected void BtnNovoQuestionario_Click(object sender, EventArgs e)
        {
            try
            {
                // Utilizando LINQ para realizar a lógica de incremento da PK
                decimal ldcIdQuestionario = ListaQuestionarios.OrderByDescending(q => q.qst_id_questionario).First().qst_id_questionario + 1;
                string lsNomeQuestionario = tbxCadastroNomeQuestionario.Text;
                char lcTipoQuestionario = ddlCadastroTipoQuestionario.SelectedValue[0];
                string lsLinkInstrucoes = tbxCadastroLinkInstrucoes.Text;

                // Instanciando um objeto do tipo Questionário para ser adicionado
                Questionario loQuestionario = new Questionario(ldcIdQuestionario, lsNomeQuestionario, lcTipoQuestionario, lsLinkInstrucoes);
                // Inserindo o novo questionário no banco de dados
                ioQuestionarioDAO.InsereQuestionario(loQuestionario);

                // Atualizando a viewstate para exibir o novo questionário
                CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Questionário cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao cadastrar questionário.');</script>");
            }

            // Limpando os campos do formulário ao fim
            tbxCadastroNomeQuestionario.Text = String.Empty;
            tbxCadastroLinkInstrucoes.Text = String.Empty;
        }

        // Gerenciar exibição do botão editar
        protected void gvGerenciamentoQuestionarios_RowEditing(object sender, GridViewEditEventArgs e)
        {            
            gvGerenciamentoQuestionarios.EditIndex = e.NewEditIndex;
            CarregaDados();
        }

        // Gerenciar exibição do botão cancelar
        protected void gvGerenciamentoQuestionarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Indica que nenhuma linha está sendo editada
            gvGerenciamentoQuestionarios.EditIndex = -1;
            CarregaDados();
        }

        // Gerenciar edição dos dados de um questionário
        protected void gvGerenciamentoQuestionarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdQuestionario = Convert.ToDecimal((gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("lblEditIdQuestionario") as Label).Text);
            string lsNomeQuestionario = (gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("tbxEditNomeQuestionario") as TextBox).Text;
            char lcTipoQuestionario = (gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("ddlEditTipoQuestionario") as DropDownList).SelectedItem.Value[0];
            string lsLinkInstrucoes = (gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("tbxEditLinkInstrucoes") as TextBox).Text;

            // Validar se todos os campos foram preenchidos corretamente
            if (String.IsNullOrWhiteSpace(lsNomeQuestionario))
                HttpContext.Current.Response.Write("<script>alert('Informe o nome do questionário.');</script>");
            else if (String.IsNullOrWhiteSpace(lsLinkInstrucoes))
                HttpContext.Current.Response.Write("<script>alert('Informe o link de instruções do questionário.');</script>");
            else if (!(ValidaUrl(lsLinkInstrucoes)))
                HttpContext.Current.Response.Write("<script>alert('O link de instruções do questionário é uma url inválida.');</script>");
            else
            {
                try
                {
                    Questionario loQuestionario = new Questionario(ldcIdQuestionario, lsNomeQuestionario, lcTipoQuestionario, lsLinkInstrucoes);
                    ioQuestionarioDAO.AtualizaQuestionario(loQuestionario);
                    gvGerenciamentoQuestionarios.EditIndex = -1;  // indica que acabou a edição
                    CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Os dados do questionário foram atualizados com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do questionário.');</script>");
                }
            }
        }

        // Método para validar url
        bool ValidaUrl(string url)
        {
            Regex re = new Regex(@"^(Http:\/\/.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$");
            if (re.IsMatch(url))
                return true;
            else
                return false;
        }

        // Gerenciar remoção de um questionário
        protected void gvGerenciamentoQuestionarios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = gvGerenciamentoQuestionarios.Rows[e.RowIndex];
                decimal ldcIdQuestionario = Convert.ToDecimal((gvGerenciamentoQuestionarios.Rows[e.RowIndex].FindControl("lblIdQuestionario") as Label).Text);
                Questionario loQuestionario = ioQuestionarioDAO.BuscaQuestionarios(ldcIdQuestionario).FirstOrDefault();
                if (loQuestionario != null)
                {
                    PerguntaDAO loPerguntaDAO = new PerguntaDAO();
                    // Verificar se há perguntas associadas a este questionário
                    if (loPerguntaDAO.BuscaPerguntasPorQuestionario(loQuestionario).Count == 0)
                    {
                        ioQuestionarioDAO.RemoveQuestionario(loQuestionario);
                        CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Questionário removido com sucesso!');</script>");
                    }
                    else
                        HttpContext.Current.Response.Write("<script>alert('Este questionário não pode ser removido pois existem perguntas associadas a ele.');</script>");
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do questionário.');</script>");
            }
        }
    }
}