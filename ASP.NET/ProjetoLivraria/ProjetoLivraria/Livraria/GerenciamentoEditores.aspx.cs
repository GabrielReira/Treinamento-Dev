using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoEditores : System.Web.UI.Page
    {
        EditoresDAO ioEditoresDAO = new EditoresDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CarregaDados();
                Session.Clear();  // limpar o conteúdo da sessão
            }
        }

        public BindingList<Editores> ListaEditores
        {
            get
            {
                if ((BindingList<Editores>)ViewState["ViewStateListaEditores"] == null)
                {
                    this.CarregaDados();
                }
                return (BindingList<Editores>)ViewState["ViewStateListaEditores"];
            }
            set
            {
                ViewState["ViewStateListaEditores"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaEditores = this.ioEditoresDAO.BuscaEditores();
                this.gvGerenciamentoEditores.DataSource = this.ListaEditores.OrderBy(e => e.edi_nm_editor);
                this.gvGerenciamentoEditores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar editores.');</script>");
            }
        }

        // Botão para cadastrar Editor
        protected void BtnNovoEditor_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdEditor = this.ListaEditores.OrderByDescending(ed => ed.edi_id_editor).First().edi_id_editor + 1;
                string lsNomeEditor = this.tbxCadastroNomeEditor.Text;
                string lsEmailEditor = this.tbxCadastroEmailEditor.Text;
                string lsUrlEditor = this.tbxCadastroUrlEditor.Text;

                Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                this.ioEditoresDAO.InsereEditor(loEditor);

                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Editor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao cadastrar editor.');</script>");
            }
            this.tbxCadastroNomeEditor.Text = String.Empty;
            this.tbxCadastroEmailEditor.Text = String.Empty;
            this.tbxCadastroUrlEditor.Text = String.Empty;
        }

        // Método para gerenciar exibição do botão Editar
        protected void gvGerenciamentoEditores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoEditores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        // Método para gerenciar exibição do botão Cancelar
        protected void gvGerenciamentoEditores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoEditores.EditIndex = -1;
            this.CarregaDados();
        }

        // Método para gerenciar botão Editar
        protected void gvGerenciamentoEditores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblEditIdEditor") as Label).Text);
            string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditNomeEditor") as TextBox).Text;
            string lsEmailEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditEmailEditor") as TextBox).Text;
            string lsUrlEditor = (this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("tbxEditUrlEditor") as TextBox).Text;

            if (String.IsNullOrWhiteSpace(lsNomeEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Informe o nome do editor.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsEmailEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Informe o email do editor.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsUrlEditor))
            {
                HttpContext.Current.Response.Write("<script>alert('Informe o site do editor.');</script>");
            }
            else if (!(UrlValida(lsUrlEditor)))
            {
                HttpContext.Current.Response.Write("<script>alert('A url informada é inválida.');</script>");
            }
            else if (!(EmailValido(lsEmailEditor)))
            {
                HttpContext.Current.Response.Write("<script>alert('O email informado é inválido.');</script>");
            }
            else
            {
                try
                {
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                    this.ioEditoresDAO.AtualizaEditor(loEditor);
                    this.gvGerenciamentoEditores.EditIndex = -1;  // indicar que acabou a edição
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Os dados do editor foram atualizados com sucesso!');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do editor.');</script>");
                }
            }
        }

        // Método para validar url
        public bool UrlValida(string url)
        {
            Regex re = new Regex(@"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$");
            if (re.IsMatch(url))
                return true;
            else
                return false;
        }
        // Método para validar email
        public bool EmailValido(string email)
        {
            Regex re = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            if (re.IsMatch(email))
                return true;
            else
                return false;
        }

        // Método para gerenciar botão Deletar
        protected void gvGerenciamentoEditores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = this.gvGerenciamentoEditores.Rows[e.RowIndex];
                decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[e.RowIndex].FindControl("lblIdEditor") as Label).Text);
                Editores loEditor = this.ioEditoresDAO.BuscaEditores(ldcIdEditor).FirstOrDefault();
                if (loEditor != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    // Verificar se há livros associados ao Editor
                    if (loLivrosDAO.BuscarLivrosPorEditor(loEditor).Count == 0)
                    {
                        this.ioEditoresDAO.RemoveEditor(loEditor);
                        this.CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Editor removido com sucesso!');</script>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o editor pois existem livros associados a ele.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do editor.');</script>");

            }
        }

        // Salvar editor na sessão
        public Editores EditorSessao
        {
            get { return (Editores)Session["SessionEditorSelecionado"]; }
            set { Session["SessionEditorSelecionado"] = value; }
        }

        protected void gvGerenciamentoEditores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosEditor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdEditor = Convert.ToDecimal((this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblIdEditor") as Label).Text);
                    string lsNomeEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblNomeEditor") as Label).Text;
                    string lsEmailEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblEmailEditor") as Label).Text;
                    string lsUrlEditor = (this.gvGerenciamentoEditores.Rows[liRowIndex].FindControl("lblUrlEditor") as Label).Text;
                    Editores loEditor = new Editores(ldcIdEditor, lsNomeEditor, lsEmailEditor, lsUrlEditor);
                    this.EditorSessao = loEditor;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }
    }
}