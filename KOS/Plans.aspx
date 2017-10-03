<%@ Page Title="План" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Plans.aspx.cs" Inherits="KOS.Plans" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr style="background-color: #85a6d3"><td><a id="Planning" runat="server" href="~/Planning.aspx" visible="false">Работы</a></tr>
        <tr style="background-color: #85a6d3"><td><a id="Worker" runat="server" href="~/Worker.aspx" visible="false">Персонал</a></td></tr>
    </table>
</asp:Content>
