<%@ Page Title="Лифты в останове" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Stopped.aspx.cs" Inherits="KOS.Stopped" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Лифты в останове</h3>
    <asp:ListView ID="Stop" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                    <td id="Th2" runat="server">№ лифта</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="Lift" runat="server" Text='<%# Eval("LiftId") %>' PostBackUrl='<%# Eval("Url") %>'></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
