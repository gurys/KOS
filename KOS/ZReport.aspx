<%@ Page Title="Заявки" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZReport.aspx.cs" Inherits="KOS.ZReport" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3><asp:Label ID="ReportTitle" runat="server"></asp:Label></h3>
    <asp:ListView ID="List" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td id="Th1" runat="server">№ заявки</td>
                    <td id="Td1" runat="server">кто отправил от: дата и время</td>
                    <td id="Td2" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="Zayavka" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Id") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:Label Text='<%# Eval("Title") %>' runat="server" />
                </td>
                <td>
                    <asp:Label Text='<%# Eval("Text") %>' runat="server" />
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
