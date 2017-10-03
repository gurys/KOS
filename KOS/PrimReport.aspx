<%@ Page Title="Замечания" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrimReport.aspx.cs" Inherits="KOS.PrimReport" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h3>Замечания</h3>
<asp:PlaceHolder ID="Qst" runat="server">
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
                <asp:Button ID="Button7" runat="server" Text="адрес" OnClick="Button7_Click" Height="30px" Width="150px"/>
            </td>
            <td style="text-align:center">
                <asp:Button ID="Button8" runat="server" Text="категория" OnClick="Button8_Click" Height="30px" Width="150px"/>
            </td>
            <td style="text-align:center">
                <asp:Button ID="Button9" runat="server" Text="выполнено" OnClick="Button9_Click" Height="30px" Width="150px"/>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td colspan="2">Период</td>
        </tr>
        <tr>
            <td>C
                <asp:Calendar ID="Start" runat="server"></asp:Calendar>
            </td>
            <td>по
                <asp:Calendar ID="End" runat="server"></asp:Calendar>
            </td>
        </tr>
    </table>
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
    <div id="Div1">
        <blockquote>
            <asp:ListView ID="Done" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
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
    </div></asp:PlaceHolder>
    <asp:Button ID="DoIt" runat="server" Text="Отчёт" OnClick="DoIt_Click" /><br />
</asp:PlaceHolder>
<asp:PlaceHolder ID="phReport" Visible="false" runat="server">
    Участок <asp:Label ID="lU" runat="server"></asp:Label><br />
    Маршрут <asp:Label ID="lM" runat="server"></asp:Label><br />
    Лифт <asp:Label ID="lL" runat="server"></asp:Label><br />
    Адрес установки <asp:Label ID="lAddress" runat="server"></asp:Label><br />
    Категория <asp:Label ID="lCategory" runat="server"></asp:Label><br />
    Выполнено <asp:Label ID="lDone" runat="server"></asp:Label><br />
    Период <asp:Label ID="lDate" runat="server"></asp:Label><br />
    
    <asp:ListView ID="Out" runat="server">
        <LayoutTemplate>
            <table border="1" id="tblr" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td id="Th1" runat="server">Лифт</td>
                    <td id="Th2" runat="server">Адрес</td>
                    <td id="Th3" runat="server">Категория</td>
                    <td id="Th4" runat="server">Выполнение</td>
                    <td id="Th5" runat="server">Отправлено</td>
                    <td id="Th6" runat="server">Дата</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="LiftId" runat="server" PostBackUrl='<%# Eval("Url") %>'><%# Eval("LiftId") %></asp:LinkButton>
                </td>
                <td>
                    <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                </td>
                <td>
                    <asp:Label ID="Category" runat="server" Text='<%# Eval("Category") %>' />
                </td>
                <td>
                    <asp:Label ID="Done" runat="server" Text='<%# Eval("Done") %>' />
                </td>
                <td>
                    <asp:Label ID="UserName" runat="server" Text='<%# Eval("UserName") %>' />
                </td>
                <td>
                    <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
</asp:PlaceHolder>
</asp:Content>
