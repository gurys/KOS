<%@ Page Title="Инцидент" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZayavkaView.aspx.cs" Inherits="KOS.ZayavkaView" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Адрес: <asp:Literal ID="Address" runat="server"></asp:Literal>
    № лифта: <asp:Literal ID="Lift" runat="server"></asp:Literal>
    <br />
    Категория: <asp:Literal ID="Category" runat="server"></asp:Literal>
    Статус: <asp:Literal ID="Status" runat="server"></asp:Literal>
    <br />
    Инцидент: 
    <br />
    <asp:TextBox ID="Text" runat="server" Height="81px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5" ReadOnly="true"></asp:TextBox><br />
    <br />
    Отправил: <asp:Literal ID="Disp" runat="server"></asp:Literal>
    
 <!--   Источник: <asp:Literal ID="From" runat="server"></asp:Literal> -->
    
    - <asp:Literal ID="Start" runat="server"></asp:Literal>
    <br />    
    Принял: <asp:Literal ID="Prinyal" runat="server"></asp:Literal>
    
    - <asp:Literal ID="Time" runat="server"></asp:Literal>
    
    <br />
    Устранил: <asp:Literal ID="Worker" runat="server"></asp:Literal>
    
    - <asp:Literal ID="Finish" runat="server"></asp:Literal>
    <br />
    Причина: 
    <br />
    <asp:TextBox ID="Couse" runat="server" Height="81px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5" ReadOnly="true"></asp:TextBox><br />
    <br />
    Простой: <asp:Literal ID="Prostoy" runat="server"></asp:Literal>
    <br />
 <!--   Оповещены: <asp:Literal ID="Sended" runat="server"></asp:Literal> -->
</asp:Content>
