<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoLivros.aspx.cs" Inherits="ProjetoLivraria.Livraria.GerenciamentoLivros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de novo Livro</h2>
        <table> 
            <tr style="display: grid;">
                <td>
                    <asp:Label ID="lblCadastroTituloLivro" runat="server" Font-Size="16pt" Text="Título: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroTituloLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroTituloLivro" Style="color: red;" ErrorMessage="* Digite o título do livro."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroAutor" runat="server" Font-Size="16pt" Text="Autor: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroAutor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCadastroAutor" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione o autor do livro."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroTipoLivro" runat="server" Font-Size="16pt" Text="Categoria: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroTipoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCadastroTipoLivro" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione a categoria do livro."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroEditor" runat="server" Font-Size="16pt" Text="Editor: "></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroEditor" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCadastroEditor" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione o editor do livro."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroPrecoLivro" runat="server" Font-Size="16pt" Text="Preço: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroPrecoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxCadastroPrecoLivro" Style="color: red;" ErrorMessage="* Digite o preço do livro."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" MinimumValue="0" MaximumValue="1000000" Type="Double" ErrorMessage="* O valor deve ser maior ou igual a 0." ControlToValidate="tbxCadastroPrecoLivro" runat="server" Style="color: red; display:block;" />
                </td>
                <td>
                    <asp:Label ID="lblCadastroRoyaltyLivro" runat="server" Font-Size="16pt" Text="Royalty: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxCadastroRoyaltyLivro" Style="color: red;" ErrorMessage="* Digite o royalty do livro."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator2" MinimumValue="0" MaximumValue="1000000" Type="Double" ErrorMessage="* O valor deve ser maior ou igual a 0." ControlToValidate="tbxCadastroRoyaltyLivro" runat="server" Style="color: red; display:block;" />
                </td>
                <td>
                    <asp:Label ID="lblCadastroResumoLivro" runat="server" Font-Size="16pt" Text="Resumo: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroResumoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxCadastroResumoLivro" Style="color: red;" ErrorMessage="* Informe o resumo do livro."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroEdicaoLivro" runat="server" Font-Size="16pt" Text="Número da edição: "></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroEdicaoLivro" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxCadastroEdicaoLivro" Style="color: red;" ErrorMessage="* Digite o número da edição do livro."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator3" MinimumValue="1" MaximumValue="1000000" Type="Integer" ErrorMessage="* O valor deve ser um inteiro maior que 0." ControlToValidate="tbxCadastroEdicaoLivro" runat="server" Style="color: red; display:block;" />
                </td>
                <td>
                    <asp:Button ID="btnNovoLivro" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoLivro_Click" />
                </td>
            </tr>            
        </table>
    </div>

    <div class="row" style="text-align: left;">
        <h3>Filtrar por Autor</h3>
        <asp:DropDownList ID="ddlFiltroAutor" runat="server" OnSelectedIndexChanged="filtroAutorAlterado" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control"  Width="250px">
            <asp:ListItem Text="TODOS" Value="All"></asp:ListItem>
        </asp:DropDownList>
    </div>
    

   <div class="row">
       <h2 style="text-align: center;">Lista de livros cadastrados</h2>           
       <asp:GridView ID="gvGerenciamentoLivros" runat="server" Width="100%" AutoGenerateColumns="false" Font-Size="14px" CellPadding="4" ForeColor="#333333" GridLines="None"
           OnRowCancelingEdit="gvGerenciamentoLivros_RowCancelingEdit" 
           OnRowEditing="gvGerenciamentoLivros_RowEditing" 
           OnRowUpdating="gvGerenciamentoLivros_RowUpdating" 
           OnRowDeleting="gvGerenciamentoLivros_RowDeleting"
           OnRowDataBound="gvGerenciamentoLivros_RowDataBound"
       >
          <Columns>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdLivro" runat="server" Text='<%# Eval("liv_id_livro") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdLivro" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdLivro" runat="server" Style="text-align: center;" Text='<%# Eval("liv_id_livro") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditTituloLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("liv_nm_titulo") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoTituloLivro" runat="server" Style="text-align: center;" Text="Título"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblTituloLivro" runat="server" Style="text-align: left;" Text='<%# Eval("liv_nm_titulo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdAutor" runat="server" Text='<%# Eval("aut_id_autor") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdAutor" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdAutor" runat="server" Style="text-align: center;" Text='<%# Eval("aut_id_autor") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                 <EditItemTemplate>
                     <asp:DropDownList ID="ddlEditAutor" runat="server" CssClass="form-control" Height="35px"></asp:DropDownList>
                 </EditItemTemplate>
                 <HeaderTemplate>
                     <asp:Label ID="lblTextoAutor" runat="server" Style="text-align: center;" Text="Autor"></asp:Label>                     
                 </HeaderTemplate>

                 <ItemTemplate>
                   <asp:Label ID="lblAutor" runat="server" Style="text-align: left;" Text='<%# Eval("aut_nm_nome") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

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
                     <asp:DropDownList ID="ddlEditTipoLivro" runat="server" CssClass="form-control" Height="35px"></asp:DropDownList>
                 </EditItemTemplate>
                 <HeaderTemplate>
                   <asp:Label ID="lblTextoTipoLivro" runat="server" Style="text-align: center;" Text="Categoria"></asp:Label>
                </HeaderTemplate>
                 <ItemTemplate>
                   <asp:Label ID="lblTipoLivro" runat="server" Style="text-align: left;" Text='<%# Eval("til_ds_descricao") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

              <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdEditor" runat="server" Text='<%# Eval("edi_id_editor") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdEditor" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdEditor" runat="server" Style="text-align: center;" Text='<%# Eval("edi_id_editor") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                 <EditItemTemplate>
                     <asp:DropDownList ID="ddlEditEditor" runat="server" CssClass="form-control" Height="35px"></asp:DropDownList>
                 </EditItemTemplate>
                 <HeaderTemplate>
                   <asp:Label ID="lblTextoEditor" runat="server" Style="text-align: center;" Text="Editor"></asp:Label>
                </HeaderTemplate>
                 <ItemTemplate>
                   <asp:Label ID="lblEditor" runat="server" Style="text-align: left;" Text='<%# Eval("edi_nm_editor") %>'></asp:Label>
                </ItemTemplate>
                 <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>             
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditPrecoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="45" Text='<%# Eval("liv_vl_preco") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoPrecoLivro" runat="server" Style="text-align: center;" Text="Preço"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblPrecoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("liv_vl_preco") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="400px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditRoyaltyLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("liv_pc_royalty") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoRoyaltyLivro" runat="server" Style="text-align: center;" Text="Royalty"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblRoyaltyLivro" runat="server" Style="text-align: center;" Text='<%# Eval("liv_pc_royalty") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditResumoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("liv_ds_resumo") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoResumoLivro" runat="server" Style="text-align: center;" Text="Resumo"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblResumoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("liv_ds_resumo") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditEdicaoLivro" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("liv_nu_edicao") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoEdicaoLivro" runat="server" Style="text-align: center;" Text="Edição"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblEdicaoLivro" runat="server" Style="text-align: center;" Text='<%# Eval("liv_nu_edicao") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              
             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />                   
                   <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:Button ID="btnEditarLivro" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />                   
                   <asp:Button ID="btnDeletarLivro" runat="server" CssClass="btn btn-danger"
                       Text="Deletar" CommandName="Delete" CausesValidation="false"
                       OnClientClick="return confirm('Tem certeza que deseja excluir esse livro?');"
                   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="500px"></HeaderStyle>
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
