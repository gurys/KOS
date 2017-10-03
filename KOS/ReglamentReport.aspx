<%@ Page Title="Плановые работы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReglamentReport.aspx.cs" Inherits="KOS.ReglamentReport" %>
<%@ Register tagprefix="uc" tagname="MonthPlan" src="~/Controls/MonthPlan.ascx" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h3>Плановые работы</h3>
<asp:PlaceHolder ID="Qst" runat="server">

    <table style="width: 10%" border="1">
        <asp:PlaceHolder ID="ph1" runat="server">
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="Button1" runat="server" Text="участок" OnClick="Button1_Click" Height="30px" Width="150px" />
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button2" runat="server" Text="маршрут" OnClick="Button2_Click" Height="30px" Width="150px" />
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button3" runat="server" Text="лифт" OnClick="Button3_Click" Height="30px" Width="150px" />
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button4" runat="server" Text="адрес" OnClick="Button4_Click" Height="30px" Width="150px" />
                </td>
                <td>
                </td>
            </tr>
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="ph2" runat="server">
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="Button5" runat="server" Text="вид работ" OnClick="Button5_Click" Height="30px" Width="150px"/>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button6" runat="server" Text="выполнил" OnClick="Button6_Click" Height="30px" Width="150px" />
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button7" runat="server" Text="статус" OnClick="Button7_Click" Height="30px" Width="150px"/>
                </td>
                <td>
                </td>
            </tr>
        </asp:PlaceHolder>
    </table>
    <table>
        <tr>
            <td colspan="2" style="text-align:center">Период</td>
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

    <div id="x102">
        <blockquote>
            <asp:ListView ID="IdU" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">№ участка</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x103">
        <blockquote>
            <asp:ListView ID="IdM" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">№ маршрута</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x104">
        <blockquote>
            <asp:ListView ID="IdL" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">№ лифта</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x105">
        <blockquote>
            <asp:ListView ID="Address" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Адрес установки</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x106">
        <blockquote>
            <asp:ListView ID="WorkType" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Вид работ</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x107">
        <blockquote>
            <asp:ListView ID="Worker" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Выполнил</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <div id="x108">
        <blockquote>
            <asp:ListView ID="Done" runat="server" Visible="false">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Выполнено</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" OnCheckedChanged="Select_CheckedChanged" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </blockquote>
    </div>

    <asp:Button ID="DoIt" runat="server" Text="Отчёт" OnClick="DoIt_Click" /><br />

</asp:PlaceHolder>

<asp:PlaceHolder ID="phReport" Visible="false" runat="server">
    Период <asp:Label ID="lPrinyalDate" runat="server"></asp:Label><br />
    Участок <asp:Label ID="lU" runat="server"></asp:Label><br />
    Маршрут <asp:Label ID="lM" runat="server"></asp:Label><br />
    Лифт <asp:Label ID="lL" runat="server"></asp:Label><br />
    Адрес установки <asp:Label ID="lAddress" runat="server"></asp:Label><br />
    Вид работ <asp:Label ID="lWorkType" runat="server"></asp:Label><br />
    Выполнил <asp:Label ID="lWorker" runat="server"></asp:Label><br />
    Выполнено <asp:Label ID="lDone" runat="server"></asp:Label><br />

    <asp:ListView ID="Out" runat="server">
        <LayoutTemplate>
            <table border="1" id="tblr" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                    <td id="Th1" runat="server">Лифт</td>
                    <td id="Th2" runat="server">Адрес</td>
                    <td id="Th3" runat="server">Вид работ</td>
                    <td id="Th4" runat="server">Выполнил</td>
                    <td id="Th5" runat="server">Дата/время</td>
                    <td id="Th6" runat="server">Замечания</td>
                    <td id="Th7" runat="server">Выполнено</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                </td>
                <td>
                    <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                </td>
                <td>
                    <asp:HyperLink ID="TpId" runat="server" NavigateUrl='<%# Eval("planId") %>'><%# Eval("TpId") %></asp:HyperLink>
                </td>
                <td>
                    <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                </td>
                <td>
                    <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                </td>
                <td>
                    <asp:LinkButton ID="Url" runat="server" PostBackUrl='<%# Eval("Url") %>'><%# Eval("UrlTitle") %></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="Done" runat="server" Text='<%# Eval("Done") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:PlaceHolder>

</asp:Content>
