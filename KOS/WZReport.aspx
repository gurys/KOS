<%@ Page Title="Отчеты по Заявкам" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WZReport.aspx.cs" Inherits="KOS.WZReport" %>
<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Заявки</h3>
    <asp:PlaceHolder ID="Qst" runat="server">
        <asp:PlaceHolder ID="ph1" runat="server">
            <table style="width: 50%">
                <tr>
                    <td onclick="doExpand(x102)" style="text-align:center">
                        <h3>какие</h3>
                    </td>
                    <td onclick="doExpand(x103)" style="text-align:center">
                        <h3>от кого</h3>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ph2" runat="server">
            <table><tr>
                <td><asp:CheckBox ID="SelectDone" Checked="true" runat="server" Enabled="True" Text="прорабу" /></td>
                <td><asp:CheckBox ID="SelectNotDone" Checked="true" runat="server" Enabled="True" Text="механику"/></td>
            </tr></table>
        </asp:PlaceHolder>
        <table>
            <tr>
                <td colspan="2">Период</td>
            </tr>
            <tr>
                <td>C
                    <asp:Calendar ID="Calendar" runat="server"></asp:Calendar>
                </td>
                <td>по
                    <asp:Calendar ID="CalendarEnd" runat="server"></asp:Calendar>
                </td>
            </tr>
        </table>
        <div id="x102" style="display: none">
            <blockquote>
                <asp:ListView ID="Type" runat="server">
                    <LayoutTemplate>
                        <table border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                                <td id="Th1" runat="server">
                                    <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                                <td id="Th2" runat="server">какие</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td>
                                <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" />
                            </td>
                            <td>
                                <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <a onclick="doExpand(x102)" href="#">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align:center">спрятать</td>
                        </tr>
                    </table>
                </a>
            </blockquote>
        </div>
        <div id="x103" style="display: none">
            <blockquote>
                <asp:ListView ID="From" runat="server">
                    <LayoutTemplate>
                        <table border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                                <td id="Th1" runat="server">
                                    <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                                <td id="Th2" runat="server">от кого</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td>
                                <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" />
                            </td>
                            <td>
                                <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
                <a onclick="doExpand(x103)" href="#">
                    <table style="width: 100%">
                        <tr>
                            <td style="text-align:center">спрятать</td>
                        </tr>
                    </table>
                </a>
            </blockquote>
        </div>
        <asp:Button ID="Show" runat="server" Text="Показать" OnClick="Show_Click" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phReport" Visible="false" runat="server">
        Отправлена <asp:Label ID="lPeriod" runat="server"></asp:Label><br />
        Какие: <asp:Label ID="lType" runat="server"></asp:Label><br />
        От кого: <asp:Label ID="lFrom" runat="server"></asp:Label><br />
        <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table border="1" id="tblr" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td id="Th1" runat="server">От кого</td>
                        <td id="Th2" runat="server">Дата</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server" onclick='<%# Eval("Url") %>'>
                    <td>
                        <asp:Label ID="Fio" runat="server" Text='<%# Eval("Fio") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:PlaceHolder>
</asp:Content>
