<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoPerguntas.aspx.cs" Inherits="ProjetoQuestionarios.Questionarios.GerenciamentoPerguntas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de nova pergunta</h2> 
        <table>
            <tr style="display: grid;"> 
                <td>
                    <asp:Label ID="lblCadastroQuestionario" runat="server" Font-Size="16pt" Text="Questionário:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroQuestionario" runat="server" CssClass="form-control" Height="35px" Width="300px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCadastroQuestionario" Style="color: red;" ErrorMessage="* Selecione o questionário."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroDescricaoPergunta" runat="server" Font-Size="16pt" Text="Descrição da pergunta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroDescricaoPergunta" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroDescricaoPergunta" InitialValue="0" Style="color: red;" ErrorMessage="* Informe a descrição da pergunta."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroTipoPergunta" runat="server" Font-Size="16pt" Text="Tipo da pergunta:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroTipoPergunta" runat="server" CssClass="form-control" Height="35px" Width="200px">
                        <asp:ListItem Text="Única escolha" Value="U"></asp:ListItem>
                        <asp:ListItem Text="Múltipla escolha" Value="M"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCadastroTipoPergunta" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione o tipo da pergunta."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroRespostaObrigatoria" runat="server" Font-Size="16pt" Text="Resposta obrigatória:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroRespostaObrigatoria" runat="server" CssClass="form-control" Height="35px" Width="200px">
                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCadastroTipoPergunta" InitialValue="0" Style="color: red;" ErrorMessage="* Selecione o tipo da pergunta."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroOrdemPergunta" runat="server" Font-Size="16pt" Text="Ordem da pergunta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroOrdemPergunta" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxCadastroOrdemPergunta" Style="color: red;" ErrorMessage="* Informe a ordem da pergunta no questionário."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" MinimumValue="1" MaximumValue="1000000" Type="Integer" ErrorMessage="* O valor deve ser um inteiro maior que 0." ControlToValidate="tbxCadastroOrdemPergunta" runat="server" Style="color: red; display:block;" />
                </td>

                <td>
                    <asp:Button ID="btnNovaPergunta" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovaPergunta_Click" />
                </td>
            </tr>
        </table>
    </div>

    <div class="row" style="text-align: left;">
        <h2>Selecione o questionário</h2>
        <asp:DropDownList ID="ddlFiltroQuestionario" runat="server" OnSelectedIndexChanged="filtroQuestionarioAlterado" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control"  Width="250px">
            <asp:ListItem Text="" Value="NENHUM"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="row">
       <h3 style="text-align: center;">Lista de perguntas cadastradas no questionário</h3>
       <asp:GridView ID="gvGerenciamentoPerguntas" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoPerguntas_RowCancelingEdit" OnRowEditing="gvGerenciamentoPerguntas_RowEditing" OnRowUpdating="gvGerenciamentoPerguntas_RowUpdating" OnRowDeleting="gvGerenciamentoPerguntas_RowDeleting">
          <Columns>
             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdPergunta" runat="server" Text='<%# Eval("per_id_pergunta") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdPergunta" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_id_pergunta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>

              <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdQuestionario" runat="server" Text='<%# Eval("per_id_questionario") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdQuestionario" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdQuestionario" runat="server" Style="text-align: center;" Text='<%# Eval("per_id_questionario") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>             

              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditDescricaoPergunta" runat="server" CssClass="form-control" Height="35px" MaxLength="100" Text='<%# Eval("per_ds_pergunta") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoDescricaoPergunta" runat="server" Style="text-align: center;" Text="Descrição da pergunta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblDescricaoPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_ds_pergunta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlEditTipoPergunta" runat="server" SelectedValue='<%# Eval("per_tp_pergunta") %>' CssClass="form-control" Height="35px" MaxLength="45">
                       <asp:ListItem Text="Única escolha" Value="U"></asp:ListItem>
                       <asp:ListItem Text="Múltipla escolha" Value="M"></asp:ListItem>
                   </asp:DropDownList>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoTipoPergunta" runat="server" Style="text-align: center;" Text="Tipo da pergunta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblTipoPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_tp_pergunta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              
              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlEditRespostaObrigatoria" runat="server" SelectedValue='<%# Eval("per_ch_resposta_obrigatoria") %>' CssClass="form-control" Height="35px" MaxLength="45">
                       <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                       <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                   </asp:DropDownList>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoRespostaObrigatoria" runat="server" Style="text-align: center;" Text="Resposta obrigatória"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblRespostaObrigatoria" runat="server" Style="text-align: center;" Text='<%# Eval("per_ch_resposta_obrigatoria") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>
              
              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditOrdemPergunta" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("per_nu_ordem") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoOrdemPergunta" runat="server" Style="text-align: center;" Text="Ordem da pergunta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblOrdemPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("per_nu_ordem") %>'></asp:Label>
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
                   <asp:Button ID="btnEditarPergunta" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnDeletarPergunta" runat="server" CssClass="btn btn-danger" 
                       Text="Deletar" CommandName="Delete" CausesValidation="false"
                       OnClientClick="return confirm('Tem certeza que deseja excluir essa pergunta?');"
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
