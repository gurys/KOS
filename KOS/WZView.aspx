<%@ Page Title="Заявка от механика" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WZView.aspx.cs" Inherits="KOS.WZView" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Механик <asp:Label ID="From" runat="server" Text="Label"></asp:Label>
    <br />
    Тип: <asp:Label ID="Type" runat="server" />
    <br />
    Лифт: <asp:Label ID="LiftId" runat="server" />
    <br />
    Отправлено <asp:Label ID="Date" runat="server" Text="Label"></asp:Label>
    <br />
    Принято: <asp:CheckBox ID="Readed" runat="server"/>
    <br />
    <asp:Label ID="Done" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:TextBox ID="Text" runat="server" Height="131px" Width="407px" TextMode="MultiLine" Columns="50" Rows="10" ReadOnly="true"></asp:TextBox><br />
    <br />
    <a href="WZView.aspx">WZView.aspx</a>
</asp:Content>
