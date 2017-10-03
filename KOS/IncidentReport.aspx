<%@ Page Title="Инциденты" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="IncidentReport.aspx.cs" Inherits="KOS.IncidentReport" %>
<%@ Register tagprefix="uc" tagname="SelectHours" src="~/Controls/SelectHours.ascx" %>
<%@ Register tagprefix="uc" tagname="SelectMinutes" src="~/Controls/SelectMinutes.ascx" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h3>Инциденты</h3>
<asp:PlaceHolder ID="Qst" runat="server">
    <asp:PlaceHolder ID="ph1" runat="server">
        <table style="width: 50%" border="1">
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="Button4" runat="server" Text="участок" OnClick="Button4_Click" Height="30px" Width="150px"/>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button5" runat="server" Text="маршрут" OnClick="Button5_Click" Height="30px" Width="150px"/>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button6" runat="server" Text="лифт" OnClick="Button6_Click" Height="30px" Width="150px"/>
                </td>
            </tr>
            <tr>
                <td style="text-align:center">
                    <asp:Button ID="Button7" runat="server" Text="адрес " OnClick="Button7_Click" Height="30px" Width="150px"/>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button8" runat="server" Text="категория" OnClick="Button8_Click" Height="30px" Width="150px"/>
                </td>
                <td style="text-align:center">
                    <asp:Button ID="Button9" runat="server" Text="источник" OnClick="Button9_Click" Height="30px" Width="150px"/>
                </td>
            </tr>
        </table>
    </asp:PlaceHolder>
    <table>
        <tr>
            <td colspan="2">Отправлено</td>
        </tr>
        <tr>
            <td>C
                <asp:Calendar ID="CalendarStart" runat="server"></asp:Calendar>
            </td>
            <td>по
                <asp:Calendar ID="CalendarStartEnd" runat="server"></asp:Calendar>
            </td>
        </tr>
    </table>

    <asp:PlaceHolder ID="PlaceHolder1" Visible="false" runat="server">
    <div id="x98">
        <blockquote>
            <uc:SelectHours runat="server" ID="HourStart" />
            <uc:SelectMinutes runat="server" ID="MinuteStart" />
        </blockquote>
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder2" Visible="false" runat="server">
    <div id="x102">
        <blockquote>
            <table>
                <tr>
                    <td colspan="2">Выполнил</td>
                </tr>
                <tr>
                    <td>C
                        <asp:Calendar ID="CalendarFinish" runat="server"></asp:Calendar>
                    </td>
                    <td>по
                        <asp:Calendar ID="CalendarFinishEnd" runat="server"></asp:Calendar>
                    </td>
                </tr>
            </table>
        </blockquote>
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder3" Visible="false" runat="server">
    <div id="x99">
        <blockquote>
            <uc:SelectHours runat="server" ID="HourFinish" />
            <uc:SelectMinutes runat="server" ID="MinuteFinish" />
        </blockquote>
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder4" Visible="false" runat="server">
    <div id="x103">
        <blockquote>
            <asp:ListView ID="IdU" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder5" Visible="false" runat="server">
    <div id="x104">
        <blockquote>
            <asp:ListView ID="IdM" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder6" Visible="false" runat="server">
    <div id="x105">
        <blockquote>
            <asp:ListView ID="IdL" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder7" Visible="false" runat="server">
    <div id="x106">
        <blockquote>
            <asp:ListView ID="Address" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder8" Visible="false" runat="server">
    <div id="x107">
        <blockquote>
            <asp:ListView ID="Category" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Категория</td>
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder9" Visible="false" runat="server">
    <div id="x108">
        <blockquote>
            <asp:ListView ID="From" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Источник</td>
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder10" Visible="false" runat="server">
    <div id="x109">
        <blockquote>
            <asp:ListView ID="Prinyal" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Принял</td>
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
    </div></asp:PlaceHolder>

    <asp:PlaceHolder ID="PlaceHolder11" Visible="false" runat="server">
    <div id="x110">
        <blockquote>
            <asp:ListView ID="Worker" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">Устранил</td>
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
    </div></asp:PlaceHolder>

    <asp:Button ID="DoIt" runat="server" Text="Отчёт" OnClick="DoIt_Click" /><br />

</asp:PlaceHolder>

<asp:PlaceHolder ID="phReport" Visible="false" runat="server">
    Принял: <asp:Label ID="lPrinyalDate" runat="server"></asp:Label>
    Часы <asp:Label ID="lPHours" runat="server"></asp:Label>
    Минуты <asp:Label ID="lPMinutes" runat="server"></asp:Label><br />
    Выполнил: <asp:Label ID="lVypolnilDate" runat="server"></asp:Label>
    Часы <asp:Label ID="lVHours" runat="server"></asp:Label>
    Минуты <asp:Label ID="lVMinutes" runat="server"></asp:Label><br />
    Участок: <asp:Label ID="lU" runat="server"></asp:Label>
    Маршрут: <asp:Label ID="lM" runat="server"></asp:Label>
    Лифт: <asp:Label ID="lL" runat="server"></asp:Label>
    Адрес установки: <asp:Label ID="lAddress" runat="server"></asp:Label><br />
    Категория: <asp:Label ID="lCategory" runat="server"></asp:Label>
    Источник: <asp:Label ID="lSource" runat="server"></asp:Label>
    Принял: <asp:Label ID="lPrinyal" runat="server"></asp:Label><br />
    Устранил: <asp:Label ID="lWorker" runat="server"></asp:Label><br />
    
    <asp:ListView ID="Out" runat="server">
        <LayoutTemplate>
            <table border="1" id="tblr" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td id="Th1" runat="server">Лифт</td>
                    <td id="Th2" runat="server">Адрес</td>
                    <td id="Th3" runat="server">Инцидент</td>
                    <td id="Th4" runat="server">Источник</td>
                    <td id="Th5" runat="server">Отправлено</td>
                    <td id="Th6" runat="server">Принял</td>
                    <td id="Th7" runat="server">Устранено</td>
                    <td id="Th8" runat="server">Устранил</td>
                    <td id="Th9" runat="server">Простой</td>
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
                    <asp:LinkButton ID="Incident" runat="server" PostBackUrl='<%# Eval("Url") %>'><%# Eval("Category") %></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                </td>
                <td>
                    <asp:Label ID="Start" runat="server" Text='<%# Eval("Start") %>' />
                </td>
                <td>
                    <asp:Label ID="Prinyal" runat="server" Text='<%# Eval("Prinyal") %>' />
                </td>
                <td>
                    <asp:Label ID="Finish" runat="server" Text='<%# Eval("Finish") %>' />
                </td>
                <td>
                    <asp:Label ID="Worker" runat="server" Text='<%# Eval("Worker") %>' />
                </td>
                <td>
                    <asp:Label ID="Prostoy" runat="server" Text='<%# Eval("Prostoy") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:PlaceHolder>

</asp:Content>
