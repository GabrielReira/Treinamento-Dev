<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GerenciamentoOpcoesResposta.aspx.cs" Inherits="ProjetoQuestionarios.Questionarios.GerenciamentoOpcoesResposta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Cadastro de nova opção de resposta</h2> 
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
                    <asp:Label ID="lblCadastroPergunta" runat="server" Font-Size="16pt" Text="Pergunta:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroPergunta" runat="server" CssClass="form-control" Height="35px" Width="300px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlCadastroPergunta" Style="color: red;" ErrorMessage="* Selecione a pergunta."></asp:RequiredFieldValidator>
                </td>

                <td>
                    <asp:Label ID="lblCadastroDescricaoOpcaoResposta" runat="server" Font-Size="16pt" Text="Descrição da opção de resposta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroDescricaoOpcaoResposta" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCadastroDescricaoOpcaoResposta" InitialValue="0" Style="color: red;" ErrorMessage="* Informe a descrição da opção de resposta."></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblCadastroRespostaCorreta" runat="server" Font-Size="16pt" Text="Resposta correta:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCadastroRespostaCorreta" runat="server" CssClass="form-control" Height="35px" Width="200px">
                        <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                        <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </td>                
                <td>
                    <asp:Label ID="lblCadastroOrdemOpcaoResposta" runat="server" Font-Size="16pt" Text="Ordem da opção de resposta:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="tbxCadastroOrdemOpcaoResposta" runat="server" CssClass="form-control" Height="35px" Width="400px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxCadastroOrdemOpcaoResposta" Style="color: red;" ErrorMessage="* Informe a ordem da opção de resposta na pergunta."></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" MinimumValue="1" MaximumValue="1000000" Type="Integer" ErrorMessage="* O valor deve ser um inteiro maior que 0." ControlToValidate="tbxCadastroOrdemOpcaoResposta" runat="server" Style="color: red; display:block;" />
                </td>

                <td>
                    <asp:Button ID="btnNovaOpcaoResposta" runat="server" CssClass="btn btn-success" Style="margin-top: 10px" Text="Salvar" OnClick="BtnNovaOpcaoResposta_Click" />
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
    <div class="row" style="text-align: left;">
        <h2>Selecione a pergunta</h2>
        <asp:DropDownList ID="ddlFiltroPergunta" runat="server" OnSelectedIndexChanged="filtroPerguntaAlterado" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control"  Width="250px">
            <asp:ListItem Text="" Value="NENHUM"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="row">
       <h3 style="text-align: center;">Lista de opções de resposta cadastradas na pergunta</h3>
       <asp:GridView ID="gvGerenciamentoOpcoesResposta" runat="server" Width="100%" AutoGenerateColumns="False" Font-Size="14px" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="gvGerenciamentoOpcaoResposta_RowCancelingEdit" OnRowEditing="gvGerenciamentoOpcaoResposta_RowEditing" OnRowUpdating="gvGerenciamentoOpcaoResposta_RowUpdating" OnRowDeleting="gvGerenciamentoOpcaoResposta_RowDeleting">
          <Columns>
              <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdOpcaoResposta" runat="server" Text='<%# Eval("opr_id_opcao_resposta") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdOpcaoResposta" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdOpcaoResposta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_id_opcao_resposta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField Visible="false">
                <EditItemTemplate>
                   <asp:Label ID="lblEditIdPergunta" runat="server" Text='<%# Eval("opr_id_pergunta") %>'></asp:Label>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoIdPergunta" runat="server" Style="width: 100%" Text="ID"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblIdPergunta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_id_pergunta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" Width="50px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
             </asp:TemplateField>                           

              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditDescricaoOpcaoResposta" runat="server" CssClass="form-control" Height="35px" MaxLength="100" Text='<%# Eval("opr_ds_opcao_resposta") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoDescricaoOpcaoResposta" runat="server" Style="text-align: center;" Text="Descrição da opção de resposta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblDescricaoOpcaoResposta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_ds_opcao_resposta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="450px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>

             <asp:TemplateField>
                <EditItemTemplate>
                   <asp:DropDownList ID="ddlEditRespostaCorreta" runat="server" SelectedValue='<%# Eval("opr_ch_resposta_correta") %>' CssClass="form-control" Height="35px" MaxLength="45">
                       <asp:ListItem Text="Sim" Value="S"></asp:ListItem>
                       <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                   </asp:DropDownList>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoRespostaCorreta" runat="server" Style="text-align: center;" Text="Resposta correta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblRespostaCorreta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_ch_resposta_correta") %>'></asp:Label>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Left" Width="150px"></HeaderStyle>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
             </asp:TemplateField>            
                            
              <asp:TemplateField>
                <EditItemTemplate>
                   <asp:TextBox ID="tbxEditOrdemResposta" runat="server" CssClass="form-control" Height="35px" MaxLength="50" Text='<%# Eval("opr_nu_ordem") %>'></asp:TextBox>
                </EditItemTemplate>
                <HeaderTemplate>
                   <asp:Label ID="lblTextoOrdemResposta" runat="server" Style="text-align: center;" Text="Ordem da resposta"></asp:Label>
                </HeaderTemplate>
                <ItemTemplate>
                   <asp:Label ID="lblOrdemResposta" runat="server" Style="text-align: center;" Text='<%# Eval("opr_nu_ordem") %>'></asp:Label>
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
                   <asp:Button ID="btnEditarOpcaoResposta" runat="server" CssClass="btn btn-success" Text="Editar" CommandName="Edit" CausesValidation="false" />
                   &nbsp; 
                   <asp:Button ID="btnDeletarOpcaoResposta" runat="server" CssClass="btn btn-danger" 
                       Text="Deletar" CommandName="Delete" CausesValidation="false"
                       OnClientClick="return confirm('Tem certeza que deseja excluir essa opção de resposta?');"
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
