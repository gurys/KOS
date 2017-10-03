<%@ Page Title="Замечание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZPrimAdd.aspx.cs" Inherits="KOS.ZPrimAdd" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Замечание</h3>
    <asp:Label runat="server" ID="Msg" ForeColor="Red"></asp:Label><br />
    Участок: <asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Маршрут: <asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Лифт:  <asp:DropDownList ID="LiftId" runat="server" OnSelectedIndexChanged="LiftId_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Категория: <asp:DropDownList ID="Category" runat="server"></asp:DropDownList><br />
    Кому: <asp:DropDownList ID="To" runat="server"></asp:DropDownList><br />
    <asp:TextBox ID="Responсe" runat="server" Height="131px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
    <asp:Button ID="Save" runat="server" Text="Добавить" OnClick="Save_Click" />
</asp:Content>
