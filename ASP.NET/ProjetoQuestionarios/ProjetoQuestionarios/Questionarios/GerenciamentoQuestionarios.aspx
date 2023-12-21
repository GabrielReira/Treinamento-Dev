<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoQuestionarios.aspx.cs" Inherits="ProjetoQuestionarios.Questionarios.GerenciamentoQuestionarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de novo questionário</h2> 
        <table>
            <tr style="display: grid;"> 
                <td>
                    <asp:Label ID="lblCadastroNomeQuestionario" runat="server" Font-Size="16pt" Text="Nome do questionário:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroNomeQuestionario" runat="server" CssClass="form-control" Height="35px" Width="300px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCadastroNomeQuestionario" Style="color: red;" ErrorMessage="* Informe o nome do questionário."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroTipoQuestionario" runat="server" Font-Size="16pt" Text="Tipo do questionário:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroTipoQuestionario" runat="server" InitialValue="0" CssClass="form-control" Height="35px" Width="200px">
                        <asp:ListItem Text="Pesquisa" Value="P"></asp:ListItem>
                        <asp:ListItem Text="Avaliação" Value="A"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCadastroTipoQuestionario" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione o tipo do questionário."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroLinkInstrucoes" runat="server" Font-Size="16pt" Text="Link com página de instruções:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroLinkInstrucoes" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroLinkInstrucoes" Style="color: red;" ErrorMessage="* Informe o link de instruções."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxCadastroLinkInstrucoes" ErrorMessage="* Insira uma url válida." Style="color: red; display:block;" ValidationExpression="^(Https?:\/\/.)[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$"></asp:RegularExpressionValidator>
                </td>

                <td>
                    <asp:Button ID="btnNovoQuestionario" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovoQuestionario_Click" />
                </td>
            </tr>
        </table>
    </div>

   <div class="row">
       <h2 style="text-align: center;">Lista de questionários cadastrados</h2>
       <asp:GridView ID="gvGerenciamentoQuestionarios" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoQuestionarios_RowCancelingEdit" OnRowEditing="gvGerenciamentoQuestionarios_RowEditing" OnRowUpdating="gvGerenciamentoQuestionarios_RowUpdating" OnRowDeleting="gvGerenciamentoQuestionarios_RowDeleting">
          <Columns>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdQuestionario" runat="server" Text='<%# Eval("qst_id_questionario") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdQuestionario" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_id_questionario") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditNomeQuestionario" runat="server" CssClass="form-control" Height="35px" MaxLength="15" Text='<%# Eval("qst_nm_questionario") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoNomeQuestionario" runat="server" Style="text-align: center;" Text="Nome"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblNomeQuestionario" runat="server" Style="text-align: left;" Text='<%# Eval("qst_nm_questionario") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="250px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlEditTipoQuestionario" runat="server" SelectedValue='<%# Eval("qst_tp_questionario") %>' CssClass="form-control" Height="35px" MaxLength="45">
                       <asp:ListItem Text="Pesquisa" Value="P"></asp:ListItem>
                       <asp:ListItem Text="Avaliação" Value="A"></asp:ListItem>
                   </asp:DropDownList>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoTipoQuestionario" runat="server" Style="text-align: center;" Text="Tipo do questionário"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblTipoQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("qst_tp_questionario") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditLinkInstrucoes" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("qst_ds_link_instrucoes") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoLinkInstrucoes" runat="server" Style="text-align: center;" Text="Link de instruções"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblLinkInstrucoes" runat="server" Style="text-align: center;" Text='<%# Eval("qst_ds_link_instrucoes") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-success" Text="Atualizar" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnCancelar" runat="server" CommandName="Cancel" CssClass="btn btn-danger" Text="Cancelar" CausesValidation="false" />
                </EditItemTemplate>
                <ItemTemplate>
                   <asp:Button ID="btnEditarQuestionario" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnDeletarQuestionario" runat="server" CssClass="btn btn-danger" 
                       Text="Deletar" CommandName="Delete" CausesValidation="false"
                       OnClientClick="return confirm('Tem certeza que deseja excluir esse questionário?');"
                   />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="250px"></HeaderStyle>
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
