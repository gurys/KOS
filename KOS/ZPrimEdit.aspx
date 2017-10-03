<%@ Page Title="Замечание" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZPrimEdit.aspx.cs" Inherits="KOS.ZPrimEdit" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h4>Замечание</h4>
    <asp:Label runat="server" ID="Msg" ForeColor="Red"></asp:Label>
    <br />
    Событие №:&nbsp; <asp:Label ID="NumEvent" runat="server"></asp:Label>    
    Лифт №&nbsp; <asp:Label runat="server" ID="LiftId"></asp:Label>&nbsp;<asp:Label runat="server" ID="DateAndWho"></asp:Label>&nbsp;Кому: <asp:Label runat="server" ID="To"></asp:Label>&nbsp;Категория <asp:Label runat="server" ID="Category"></asp:Label><br />
    <asp:TextBox ID="Responсe" runat="server" Height="75px" Width="485px" TextMode="MultiLine" Columns="50" Rows="5" ReadOnly="true"></asp:TextBox><br />Дополнение/Комментарий:<br />
    <asp:TextBox ID="Replay" runat="server" Height="75px" Width="485px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
    <asp:CheckBox ID="Done" runat="server" /> устранил<br />
    <asp:Button ID="Save" runat="server" Text="Сохранить и выйти" OnClick="Save_Click" />&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="Далее к обработке замечания" OnClick="Save_Click1" />
</asp:Content>
