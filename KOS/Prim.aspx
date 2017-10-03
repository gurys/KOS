<%@ Page Title="Замечание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Prim.aspx.cs" Inherits="KOS.Prim" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="Prime" runat="server" Height="74px" Width="407px" TextMode="MultiLine" Columns="50" Rows="10" ReadOnly="true"></asp:TextBox>    
    <br />
    <asp:TextBox ID="Comments" runat="server" Height="74px" Width="407px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <br />
    <asp:CheckBox ID="Done" runat="server" Text="выполнено" />
    <br />
    <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
</asp:Content>
