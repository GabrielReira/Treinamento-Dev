<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResponderQuestionario.aspx.cs" Inherits="ProjetoQuestionarios.Questionarios.ResponderQuestionario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="text-align: left;">
        <h2>Página para responder um questionário</h2>

        <h3>Selecione o questionário</h3>
        <asp:DropDownList ID="ddlFiltroQuestionario" runat="server" OnSelectedIndexChanged="filtroQuestionarioAlterado" AutoPostBack="true" AppendDataBoundItems="true" CssClass="form-control"  Width="250px">
            <asp:ListItem Text="" Value="NENHUM"></asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="row" style="text-align: left;">
        <div id="DivPrincipal" runat="server">
            <table id="formDinamico" runat="server">
                <tr id="tr" runat="server" style="display: grid;"></tr>
            </table>
        </div>
    </div>
</asp:Content>
