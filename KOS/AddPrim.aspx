<%@ Page Title="Добавление замечания" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPrim.aspx.cs" Inherits="KOS.AddPrim" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <thead><tr><td>Дата/время</td><td>Адрес</td><td>№ лифта</td><td>Вид работ</td><td>Срочность</td></tr></thead>
        <tr>
            <td>
                <asp:Label ID="Date" runat="server"></asp:Label></td>
            <td>
                <asp:Label ID="Address" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Lifts" runat="server"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="ReglamentTitle" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="Index" runat="server"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="Prim" runat="server" Height="131px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
    <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
    <br />
</asp:Content>
