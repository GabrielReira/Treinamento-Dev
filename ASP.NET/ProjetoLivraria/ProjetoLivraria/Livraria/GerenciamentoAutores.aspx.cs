using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoAutores : System.Web.UI.Page
    {
        AutoresDAO ioAutoresDAO = new AutoresDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CarregaDados();
                Session.Clear();  // limpar o conteúdo da sessão
            }
        }

        // ViewState para armazenar a lista de autores cadastrados
        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["ViewStateListaAutores"] == null)
                {
                    this.CarregaDados();
                }
                return (BindingList<Autores>)ViewState["ViewStateListaAutores"];
            }
            set
            {
                ViewState["ViewStateListaAutores"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaAutores = this.ioAutoresDAO.BuscaAutores();
                // Indica onde o GridView deve buscar os valores
                this.gvGerenciamentoAutores.DataSource = this.ListaAutores.OrderBy(a => a.aut_nm_nome);
                this.gvGerenciamentoAutores.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar autores.');</script>");
            }
        }

        // Botão para cadastrar novo autor
        protected void BtnNovoAutor_Click(object sender, EventArgs e)
        {
            try
            {
                // Utilizando LINQ para realizar a lógica de incremento da PK
                decimal ldcIdAutor = this.ListaAutores.OrderByDescending(a => a.aut_id_autor).First().aut_id_autor + 1;

                // Salvando os valores que o usuário preencheu em cada campo do formulário
                string lsNomeAutor = this.tbxCadastroNomeAutor.Text;
                string lsSobrenomeAutor = this.tbxCadastroSobrenomeAutor.Text;
                string lsEmailAutor = this.tbxCadastroEmailAutor.Text;

                // Instanciando um objeto do tipo Autores para ser adicionado
                Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);

                // Inserindo o novo autor no banco de dados
                this.ioAutoresDAO.InsereAutor(loAutor);

                // Atualizando a viewstate para exibir o novo autor
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Autor cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao cadastrar autor.');</script>");
            }

            // Limpando os campos do formulário ao fim
            this.tbxCadastroNomeAutor.Text = String.Empty;
            this.tbxCadastroSobrenomeAutor.Text = String.Empty;
            this.tbxCadastroEmailAutor.Text = String.Empty;
        }

        // Método para gerenciar exibição do Botão Editar
        protected void gvGerenciamentoAutores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoAutores.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        // Método para gerenciar exibição do Botão Cancelar Edição
        protected void gvGerenciamentoAutores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Indica que nenhuma linha está sendo editada
            this.gvGerenciamentoAutores.EditIndex = -1;
            this.CarregaDados();
        }

        // Método para editar os dados de um autor
        protected void gvGerenciamentoAutores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Capturando os dados que foram inseridos
            decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblEditIdAutor") as Label).Text);
            string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditNomeAutor") as TextBox).Text;
            string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditSobrenomeAutor") as TextBox).Text;
            string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("tbxEditEmailAutor") as TextBox).Text;
            
            // Validar se todos os campos foram preenchidos
            if (String.IsNullOrWhiteSpace(lsNomeAutor))
            {
                HttpContext.Current.Response.Write("<script>alert('Informe o nome do autor.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsSobrenomeAutor)) {
                HttpContext.Current.Response.Write("<script>alert('Informe o sobrenome do autor.');</script>");
            }
            else if (String.IsNullOrWhiteSpace(lsEmailAutor)) {
                HttpContext.Current.Response.Write("<script>alert('Informe o email do autor.');</script>");
            }
            else
            {
                try
                {
                    System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(lsEmailAutor);  // validar email
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                    this.ioAutoresDAO.AtualizaAutor(loAutor);
                    this.gvGerenciamentoAutores.EditIndex = -1;  // indicar que acabou a edição
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Os dados do autor foram atualizados com sucesso!');</script>");
                }
                catch (FormatException)
                {
                    HttpContext.Current.Response.Write("<script>alert('O email informado é inválido.');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro na atualização do autor.');</script>");
                }
            }
        }

        // Método para deletar um autor
        protected void gvGerenciamentoAutores_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = this.gvGerenciamentoAutores.Rows[e.RowIndex];
                decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[e.RowIndex].FindControl("lblIdAutor") as Label).Text);
                Autores loAutor = this.ioAutoresDAO.BuscaAutores(ldcIdAutor).FirstOrDefault();
                if (loAutor != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    // Verificar se há livros associados ao autor
                    if (loLivrosDAO.BuscarLivrosPorAutor(loAutor).Count == 0)
                    {
                        this.ioAutoresDAO.RemoveAutor(loAutor);
                        this.CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Autor removido com sucesso!');</script>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover o autor pois existem livros associados a ele.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do autor.');</script>");

            }
        }

        // Salvar autor na sessão
        public Autores AutorSessao
        {
            get { return (Autores)Session["SessionAutorSelecionado"]; }
            set { Session["SessionAutorSelecionado"] = value; }
        }

        protected void gvGerenciamentoAutores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosAutor":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdAutor = Convert.ToDecimal((this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblIdAutor") as Label).Text);
                    string lsNomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblNomeAutor") as Label).Text;
                    string lsSobrenomeAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblSobrenomeAutor") as Label).Text;
                    string lsEmailAutor = (this.gvGerenciamentoAutores.Rows[liRowIndex].FindControl("lblEmailAutor") as Label).Text;
                    Autores loAutor = new Autores(ldcIdAutor, lsNomeAutor, lsSobrenomeAutor, lsEmailAutor);
                    this.AutorSessao = loAutor;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }
    }
}
