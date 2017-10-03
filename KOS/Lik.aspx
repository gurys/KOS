<%@ Page Title="Лик" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lik.aspx.cs" Inherits="KOS.Lik" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr style="background-color: #85a6d3"><td><a id="Journal" runat="server" href="~/Journal.aspx" visible="false">Журнал сообщений</a></tr>
        <tr style="background-color: #85a6d3"><td><a id="Enviroment" runat="server" href="~/Enviroment.aspx">Оборудование и запачасти</a></tr>
    </table>
</asp:Content>
