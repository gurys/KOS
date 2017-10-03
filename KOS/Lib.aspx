<%@ Page Title="Библиотека" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lib.aspx.cs" Inherits="KOS.Lib" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Библиотека</h3>
    <asp:ListView ID="Dir" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #98FB98">
                    <td id="Td1" runat="server">Название</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="LinkButton0" runat="server" PostBackUrl='<%# Eval("Url") %>'><%# Eval("Title") %></asp:LinkButton>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
   </asp:Content>
