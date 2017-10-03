<%@ Page Title="Закрепления" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Connections.aspx.cs" Inherits="KOS.Connections" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Закрепления</h3>
    Механик: <asp:DropDownList ID="Worker" runat="server" OnSelectedIndexChanged="Worker_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Участок: <asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Маршрут: <asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    <asp:ListView ID="IdL" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                    <td id="Th1" runat="server">
                        <asp:CheckBox ID="SelectAll" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                    <td id="Th2" runat="server">№ лифта</td>
                    <td id="Td1" runat="server">Адрес</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:CheckBox ID="Select" Checked='<%# Eval("Checked") %>' runat="server" Enabled="True" />
                </td>
                <td>
                    <asp:Label ID="Title" runat="server" Text='<%# Eval("LiftId") %>' />
                </td>
                <td>
                    <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
</asp:Content>
