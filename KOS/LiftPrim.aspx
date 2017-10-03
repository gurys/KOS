<%@ Page Title="Замечания" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LiftPrim.aspx.cs" Inherits="KOS.LiftPrim" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Замечания по участку/маршруту <asp:Label ID="UM" runat="server"></asp:Label></h3>
    <asp:ListView ID="Prim" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td>срочность</td>
                    <td>№ лифта</td>
                    <td>дата</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Index") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# Eval("Url") %>'><%# Eval("LiftId") %></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Date") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
