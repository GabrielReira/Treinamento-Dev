using ProjetoLivraria.DAO;
using ProjetoLivraria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoLivraria.Livraria
{
    public partial class GerenciamentoLivros : System.Web.UI.Page
    {
        LivrosDAO ioLivrosDAO = new LivrosDAO();
        AutoresDAO ioAutoresDAO = new AutoresDAO();
        TipoLivroDAO ioTipoLivroDAO = new TipoLivroDAO();
        EditoresDAO ioEditoresDAO = new EditoresDAO();
        LivroAutorDAO ioLivroAutorDAO = new LivroAutorDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                CarregaDropDowns();
                this.CarregaDados();
            }
        }

        // ViewState para armazenar a lista de livros cadastrados
        public BindingList<Livros> ListaLivros
        {
            get
            {
                if ((BindingList<Livros>)ViewState["ViewStateListaLivros"] == null)
                    this.CarregaDados();
                return (BindingList<Livros>)ViewState["ViewStateListaLivros"];
            }
            set
            {
                ViewState["ViewStateListaLivros"] = value;
            }
        }

        // ViewState para armazenar o autor selecionado do filtro
        public BindingList<Autores> ListaAutores
        {
            get
            {
                if ((BindingList<Autores>)ViewState["FiltroAutor"] == null)
                    this.ListaAutores = ioAutoresDAO.BuscaAutores();
                return (BindingList<Autores>)ViewState["FiltroAutor"];
            }
            set
            {
                ViewState["FiltroAutor"] = value;
            }
        }

        private void CarregaDados()
        {
            try
            {
                // Verificar se existe algum filtro ou sessão
                if (ddlFiltroAutor.SelectedValue != "All")
                    this.ListaLivros = this.ioLivrosDAO.BuscaLivrosPorAutorCustom(ioAutoresDAO.BuscaAutores(Convert.ToDecimal(ViewState["FiltroAutor"])).FirstOrDefault());
                else if (Session["SessionAutorSelecionado"] != null)
                {
                    this.ListaLivros = this.ioLivrosDAO.BuscaLivrosPorAutorCustom((Autores)Session["SessionAutorSelecionado"]);
                    ddlFiltroAutor.SelectedValue = ((Autores)Session["SessionAutorSelecionado"]).aut_id_autor.ToString();
                }
                else if (Session["SessionTipoLivroSelecionado"] != null)
                    this.ListaLivros = this.ioLivrosDAO.BuscarLivrosPorCategoriaCustom((TipoLivro)Session["SessionTipoLivroSelecionado"]);
                else if (Session["SessionEditorSelecionado"] != null)
                    this.ListaLivros = this.ioLivrosDAO.BuscarLivrosPorEditorCustom((Editores)Session["SessionEditorSelecionado"]);
                else
                    this.ListaLivros = this.ioLivrosDAO.BuscaLivrosCustom();                             

                this.gvGerenciamentoLivros.DataSource = this.ListaLivros.OrderBy(l => l.liv_nm_titulo).ToList();
                this.gvGerenciamentoLivros.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar livros.');</script>");
            }
        }

        // DropDown que carrega os dados de autores, categorias e editores
        private void CarregaDropDowns()
        {
            try
            {
                // Autores
                this.ddlCadastroAutor.DataSource = ioAutoresDAO.BuscaAutores().OrderBy(a => a.aut_nm_nome);
                this.ddlCadastroAutor.DataTextField = "aut_nm_nome";
                this.ddlCadastroAutor.DataValueField = "aut_id_autor";
                this.ddlCadastroAutor.DataBind();
                ListItem autorItem = new ListItem("", "0", true);
                autorItem.Selected = true;
                this.ddlCadastroAutor.Items.Insert(0, autorItem);

                // Categorias
                this.ddlCadastroTipoLivro.DataSource = ioTipoLivroDAO.BuscaCategorias().OrderBy(c => c.til_ds_descricao);
                this.ddlCadastroTipoLivro.DataTextField = "til_ds_descricao";
                this.ddlCadastroTipoLivro.DataValueField = "til_id_tipo_livro";
                this.ddlCadastroTipoLivro.DataBind();
                ListItem tipoLivroItem = new ListItem("", "0", true);
                tipoLivroItem.Selected = true;
                this.ddlCadastroTipoLivro.Items.Insert(0, tipoLivroItem);

                // Editores
                this.ddlCadastroEditor.DataSource = ioEditoresDAO.BuscaEditores().OrderBy(e => e.edi_nm_editor);
                this.ddlCadastroEditor.DataTextField = "edi_nm_editor";
                this.ddlCadastroEditor.DataValueField = "edi_id_editor";
                this.ddlCadastroEditor.DataBind();
                ListItem editorItem = new ListItem("", "0", true);
                editorItem.Selected = true;
                this.ddlCadastroEditor.Items.Insert(0, editorItem);

                // Carregar dropdown do filtro de autores
                ddlFiltroAutor.DataSource = ioAutoresDAO.BuscaAutores().OrderBy(a => a.aut_nm_nome);
                ddlFiltroAutor.DataTextField = "aut_nm_nome";
                ddlFiltroAutor.DataValueField = "aut_id_autor";
                ddlFiltroAutor.DataBind();
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao tentar buscar o conteúdo das dropdowns de autores/categorias/editores.');</script>");
            }
        }

        // Botão para cadastrar novo livro
        protected void BtnNovoLivro_Click(object sender, EventArgs e)
        {
            try
            {
                decimal ldcIdLivro = this.ListaLivros.OrderByDescending(l => l.liv_id_livro).First().liv_id_livro + 1;
                // Pegar os IDs das FKs de autor, categoria e editor
                decimal ldcIdAutor = Convert.ToDecimal(this.ddlCadastroAutor.SelectedValue);
                decimal ldcIdTipoLivro = Convert.ToDecimal(this.ddlCadastroTipoLivro.SelectedValue);
                decimal ldcIdEditorLivro = Convert.ToDecimal(this.ddlCadastroEditor.SelectedValue);
                // Pegar os demais campos
                string lsTitulo = this.tbxCadastroTituloLivro.Text;
                decimal ldcPreco = Convert.ToDecimal(this.tbxCadastroPrecoLivro.Text);
                decimal ldcRoyalty = Convert.ToDecimal(this.tbxCadastroRoyaltyLivro.Text);
                string lsResumo = this.tbxCadastroResumoLivro.Text;
                int liEdicao = Convert.ToInt32(this.tbxCadastroEdicaoLivro.Text);

                // Instanciando um objeto do tipo Livros para ser adicionado
                Livros loLivro = new Livros(
                    ldcIdLivro, ldcIdTipoLivro, ldcIdEditorLivro, lsTitulo, ldcPreco, ldcRoyalty, lsResumo, liEdicao
                );

                // Associar o livro a um autor através da tabela LIA_LIVRO_AUTOR
                LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyalty);

                // Inserindo valores no banco de dados
                this.ioLivrosDAO.InsereLivro(loLivro);
                this.ioLivroAutorDAO.InsereLivroAutor(loLivroAutor);

                // Atualizando a viewstate para exibir o novo livro
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro cadastrado com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Falha ao cadastrar novo livro.');</script>");
            }

            // Limpando os campos do formulário ao fim
            this.tbxCadastroTituloLivro.Text = String.Empty;
            this.ddlCadastroAutor.SelectedValue = "0";
            this.ddlCadastroTipoLivro.SelectedValue = "0";
            this.ddlCadastroEditor.SelectedValue = "0";
            this.tbxCadastroPrecoLivro.Text = String.Empty;
            this.tbxCadastroRoyaltyLivro.Text = String.Empty;
            this.tbxCadastroResumoLivro.Text = String.Empty;
            this.tbxCadastroEdicaoLivro.Text = String.Empty;
        }

        // Método para gerenciar o Botão Editar
        protected void gvGerenciamentoLivros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = e.NewEditIndex;
            this.CarregaDados();
        }

        // Método para gerenciar o Botão Cancelar Edição
        protected void gvGerenciamentoLivros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.gvGerenciamentoLivros.EditIndex = -1;
            this.CarregaDados();
        }

        // Método para editar os dados de um livro
        protected void gvGerenciamentoLivros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblEditIdLivro") as Label).Text);
                string lsTituloLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditTituloLivro") as TextBox).Text;
                decimal ldcPrecoLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditPrecoLivro") as TextBox).Text);
                decimal ldcRoyaltyLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditRoyaltyLivro") as TextBox).Text);
                string lsResumoLivro = (this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditResumoLivro") as TextBox).Text;
                int liEdicaoLivro = Convert.ToInt32((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("tbxEditEdicaoLivro") as TextBox).Text);
                // Capturar os IDs de autor, categoria e editor da dropdown
                decimal ldcIdAutor = Convert.ToDecimal((gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditAutor") as DropDownList).SelectedItem.Value);
                decimal ldcIdTipoLivro = Convert.ToDecimal((gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditTipoLivro") as DropDownList).SelectedItem.Value);
                decimal ldcIdEditor = Convert.ToDecimal((gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("ddlEditEditor") as DropDownList).SelectedItem.Value);

                // Verificar se todos os campos foram preenchidos corretamente
                if (String.IsNullOrWhiteSpace(lsTituloLivro))
                    HttpContext.Current.Response.Write("<script>alert('Informe o título do livro.');</script>");
                else if (ldcPrecoLivro < 0)
                    HttpContext.Current.Response.Write("<script>alert('O preço do livro deve ser maior ou igual a zero.');</script>");
                else if (ldcRoyaltyLivro < 0)
                    HttpContext.Current.Response.Write("<script>alert('O royalty do livro deve ser maior ou igual zero.');</script>");
                else if (String.IsNullOrWhiteSpace(lsResumoLivro))
                    HttpContext.Current.Response.Write("<script>alert('Informe o resumo do livro.');</script>");
                else if (liEdicaoLivro <= 0)
                    HttpContext.Current.Response.Write("<script>alert('O número da edição do livro deve ser maior que zero.');</script>");
                else
                {
                    try
                    {
                        Livros loLivro = new Livros(ldcIdLivro, ldcIdTipoLivro, ldcIdEditor, lsTituloLivro, ldcPrecoLivro, ldcRoyaltyLivro, lsResumoLivro, liEdicaoLivro);
                        LivroAutor loLivroAutor = new LivroAutor(ldcIdAutor, ldcIdLivro, ldcRoyaltyLivro);
                        this.ioLivrosDAO.AtualizaLivro(loLivro);
                        this.ioLivroAutorDAO.AtualizaLivroAutor(loLivroAutor);
                        this.gvGerenciamentoLivros.EditIndex = -1;
                        this.CarregaDados();
                        HttpContext.Current.Response.Write("<script>alert('Os dados do livro foram atualizados com sucesso!');</script>");
                    }
                    catch
                    {
                        HttpContext.Current.Response.Write("<script>alert('Erro na atualização do livro.');</script>");
                    }
                }
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Informe todos os campos para realizar a edição.');</script>");
            }

        }

        // Método para deletar um livro
        protected void gvGerenciamentoLivros_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow loGridViewRow = this.gvGerenciamentoLivros.Rows[e.RowIndex];
                decimal ldcIdLivro = Convert.ToDecimal((this.gvGerenciamentoLivros.Rows[e.RowIndex].FindControl("lblIdLivro") as Label).Text);
                // Remover livro da tabela Livro-Autor
                LivroAutor loLivroAutor = this.ioLivroAutorDAO.BuscaLivroAutor(ldcIdLivro).FirstOrDefault();
                this.ioLivroAutorDAO.RemoveLivroAutor(loLivroAutor);
                // Remover livro da tabela Livro
                Livros loLivro = this.ioLivrosDAO.BuscaLivros(ldcIdLivro).FirstOrDefault();
                this.ioLivrosDAO.RemoveLivro(loLivro);                

                // Atualizar gridview
                this.CarregaDados();
                HttpContext.Current.Response.Write("<script>alert('Livro removido com sucesso!');</script>");
            }
            catch
            {
                HttpContext.Current.Response.Write("<script>alert('Erro na remoção do livro.');</script>");
            }
        }

        protected void gvGerenciamentoLivros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate))
                {                    
                    DropDownList ddlEditAutorControl = (e.Row.FindControl("ddlEditAutor") as DropDownList);
                    ddlEditAutorControl.DataSource = ioAutoresDAO.BuscaAutores().OrderBy(a => a.aut_nm_nome);
                    ddlEditAutorControl.DataValueField = "aut_id_autor";
                    ddlEditAutorControl.DataTextField = "aut_nm_nome";
                    ddlEditAutorControl.DataBind();
                    ListItem editAutorItem = new ListItem("", null, true);
                    ddlEditAutorControl.Items.Insert(0, editAutorItem);
                    // Fazer com que a dropdown deixe selecionado o valor correto ao clicar em editar
                    ddlEditAutorControl.SelectedValue = (e.Row.DataItem as Livros).aut_id_autor.Value.ToString();

                    DropDownList ddlEditTipoLivroControl = (e.Row.FindControl("ddlEditTipoLivro") as DropDownList);
                    ddlEditTipoLivroControl.DataSource = ioTipoLivroDAO.BuscaCategorias().OrderBy(c => c.til_ds_descricao);
                    ddlEditTipoLivroControl.DataValueField = "til_id_tipo_livro";
                    ddlEditTipoLivroControl.DataTextField = "til_ds_descricao";
                    ddlEditTipoLivroControl.DataBind();
                    ListItem editCategoriaItem = new ListItem("", null, true);
                    ddlEditTipoLivroControl.Items.Insert(0, editCategoriaItem);
                    // Fazer com que a dropdown deixe selecionado o valor correto ao clicar em editar
                    ddlEditTipoLivroControl.SelectedValue = (e.Row.DataItem as Livros).til_id_tipo_livro.Value.ToString();

                    DropDownList ddlEditEditorControl = (e.Row.FindControl("ddlEditEditor") as DropDownList);
                    ddlEditEditorControl.DataSource = ioEditoresDAO.BuscaEditores().OrderBy(ed => ed.edi_nm_editor);
                    ddlEditEditorControl.DataValueField = "edi_id_editor";
                    ddlEditEditorControl.DataTextField = "edi_nm_editor";
                    ddlEditEditorControl.DataBind();
                    ListItem editEditorItem = new ListItem("", null, true);
                    ddlEditEditorControl.Items.Insert(0, editEditorItem);
                    // Fazer com que a dropdown deixe selecionado o valor correto ao clicar em editar
                    ddlEditEditorControl.SelectedValue = (e.Row.DataItem as Livros).edi_id_editor.Value.ToString();
                }
            }
        }

        // Filtro para autores
        protected void filtroAutorAlterado(object sender, EventArgs e)
        {
            DropDownList ddlFiltroAutor = sender as DropDownList;
            ViewState["FiltroAutor"] = ddlFiltroAutor.SelectedValue;
            this.CarregaDados();
        }
    }
}
