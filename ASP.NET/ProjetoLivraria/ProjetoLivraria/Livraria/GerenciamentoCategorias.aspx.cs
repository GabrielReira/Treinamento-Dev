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
    public partial class GerenciamentoCategorias : System.Web.UI.Page
    {
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.CarregaDados();
                Session.Clear();  // limpar o conteúdo da sessão
            }
        }

        // ViewState para armazenar a lista de categorias cadastradas
        public BindingList<TipoLivro> ListaTipoLivro
        {
            get
            {
                if ((BindingList<TipoLivro>)ViewState["ViewStateListaTipoLivro"] == null)
                {
                    this.CarregaDados();
                }
                return (BindingList<TipoLivro>)ViewState["ViewStateListaTipoLivro"];
            }
            set
            {
                ViewState["ViewStateListaTipoLivro"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                this.ListaTipoLivro = this.ioTipoLivroDAO.BuscaCategorias();
                // Indica onde o GridView deve buscar os valores
                this.gvGerenciamentoCategorias.DataSource = this.ListaTipoLivro.OrderBy(c => c.til_ds_descricao);
                this.gvGerenciamentoCategorias.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar categorias.');</script>");
            }
        }

        // Botão para cadastrar nova categoria
        protected void BtnNovoTipoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdCategoria = this.ListaTipoLivro.OrderByDescending(c => c.til_id_tipo_livro).First().til_id_tipo_livro + 1;
                string lsDescricao = this.tbxCadastroDescricaoTipoLivro.Text;
                TipoLivro loCategoria = new TipoLivro(ldcIdCategoria, lsDescricao);
                this.ioTipoLivroDAO.InsereCategoria(loCategoria);

                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Categoria cadastrada com sucesso.');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar cadastrar categoria.');</script>");
            }

            this.tbxCadastroDescricaoTipoLivro.Text = String.Empty;
        }

        // Método para gerenciar exibição do botão Editar
        protected void gvGerenciamentoTipoLivro_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoCategorias.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        // Método para gerenciar exibição do botão Cancelar
        protected void gvGerenciamentoTipoLivro_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoCategorias.EditIndex = -1;
            this.CarregaDados();
        }

        // Método para Editar os dados de uma categoria
        protected void gvGerenciamentoTipoLivro_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            decimal ldcIdTipoLivro = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("lblEditIdTipoLivro") as Label).Text);
            string lsDescricao = (this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("tbxEditDescricaoTipoLivro") as TextBox).Text;
            // Verificar se o campo descrição foi preenchido
            if (String.IsNullOrWhiteSpace(lsDescricao))
            {
                HttpContext.Current.Response.Write("<script>alert('Informe a descrição da categoria.');</script>");
            }
            else
            {
                try
                {
                    TipoLivro loCategoria = new TipoLivro(ldcIdTipoLivro, lsDescricao);
                    this.ioTipoLivroDAO.AtualizaCategoria(loCategoria);
                    this.gvGerenciamentoCategorias.EditIndex = -1;  // indica que acabou a edição
                    this.CarregaDados();
                    HttpContext.Current.Response.Write("<script>alert('Categoria atualizada com sucesso.');</script>");
                }
                catch
                {
                    HttpContext.Current.Response.Write("<script>alert('Erro ao atualizar categoria.');</script>");
                }
            }
        }

        // Método para deletar uma categoria
        protected void gvGerenciamentoTipoLivro_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = this.gvGerenciamentoCategorias.Rows[e.RowIndex];
                decimal ldcIdTipoLivro = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[e.RowIndex].FindControl("lblIdTipoLivro") as Label).Text);
                TipoLivro loTipoLivro = this.ioTipoLivroDAO.BuscaCategorias(ldcIdTipoLivro).FirstOrDefault();
                if (loTipoLivro != null)
                {
                    LivrosDAO loLivrosDAO = new LivrosDAO();
                    // Verificar se há livros associados à categoria
                    if (loLivrosDAO.BuscarLivrosPorCategoria(loTipoLivro).Count == 0)
                    {
                        this.ioTipoLivroDAO.RemoveCategoria(loTipoLivro);
                        this.CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Categoria removida com sucesso!');</script>");
                    }
                    else
                    {
                        HttpContext.Current.Response.Write("<script>alert('Não é possível remover a categoria pois existem livros associados a ela.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro ao remover categoria.');</script>");
            }
        }

        // Salvar categoria na sessão
        public TipoLivro TipoLivroSessao
        {
            get { return (TipoLivro)Session["SessionTipoLivroSelecionado"]; }
            set { Session["SessionTipoLivroSelecionado"] = value; }
        }

        protected void gvGerenciamentoTipoLivro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "CarregaLivrosTipoLivro":
                    int liRowIndex = Convert.ToInt32(e.CommandArgument);
                    decimal ldcIdTipoLivro = Convert.ToDecimal((this.gvGerenciamentoCategorias.Rows[liRowIndex].FindControl("lblIdTipoLivro") as Label).Text);
                    string lsDescricao = (this.gvGerenciamentoCategorias.Rows[liRowIndex].FindControl("lblDecricaoTipoLivro") as Label).Text;
                    TipoLivro loCategoria = new TipoLivro(ldcIdTipoLivro, lsDescricao);
                    this.TipoLivroSessao = loCategoria;
                    Response.Redirect("/Livraria/GerenciamentoLivros");
                    break;
                default:
                    break;
            }
        }
    }
}
