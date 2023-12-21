<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoCategorias.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoCategorias" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de nova Categoria</h2> 
        <table> 
            <tr style="display: grid;"> 
                <td>
                    <asp:Label ID="lblCadastroDescricaoTipoLivro" runat="server" Font-Size="16pt" Text="Categoria: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroDescricaoTipoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator" runat="server" ControlToValidate="tbxCadastroDescricaoTipoLivro" Style="color: red;" ErrorMessage="* Digite a descrição da categoria."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnNovoTipoLivro" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoTipoLivro_Click" />
                </td>
            </tr>
        </table>
    </div>

   <div class="row">
       <h2 style="text-align: center;">Lista de categorias cadastradas</h2>
       <asp:GridView ID="gvGerenciamentoCategorias" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoTipoLivro_RowCancelingEdit" OnRowEditing="gvGerenciamentoTipoLivro_RowEditing" OnRowUpdating="gvGerenciamentoTipoLivro_RowUpdating" OnRowDeleting="gvGerenciamentoTipoLivro_RowDeleting" OnRowCommand="gvGerenciamentoTipoLivro_RowCommand">
          <Columns>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdTipoLivro" runat="server" Text='<%# Eval("til_id_tipo_livro") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdTipoLivro" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdTipoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("til_id_tipo_livro") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditDescricaoTipoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("til_ds_descricao") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoDescricaoTipoLivro" runat="server" Style="text-align: center;" Text="Descrição"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblDecricaoTipoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("til_ds_descricao") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="250px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:Button ID="btnEditarTipoLivro" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnDeletarTipoLivro" runat="server" CssClass="btn btn-danger" 
                       Text="Deletar" CommandName="Delete" CausesValidation="false" 
                       OnClientClick="return confirm('Tem certeza que deseja excluir essa categoria?');"
                   />
                   &nbsp; 
                   <asp:Button ID="btnCarregaLivrosTipoLivro" runat="server" CssClass="btn btn-primary" Text="Livros" CommandName="CarregaLivrosTipoLivro" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" CausesValidation="false" />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="100px"></HeaderStyle>
             </asp:TemplateField>
          </Columns>
          <AlternatingRowStyle BackColor="White" />
          <EditRowStyle BackColor="#2461BF" Font-Size="14px"/>
          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <HeaderStyle HorizontalAlign="Center" Wrap="True" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
          <RowStyle HorizontalAlign="Center" BackColor="#EFF3FB" />
          <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" Font-Size="14px"/>
          <SortedAscendingCellStyle BackColor="#F5F7FB" />
          <SortedAscendingHeaderStyle BackColor="#6D95E1" />
          <SortedDescendingCellStyle BackColor="#E9EBEF" />
          <SortedDescendingHeaderStyle BackColor="#4870BE" />
       </asp:GridView>
    </div>
</asp:Content>
