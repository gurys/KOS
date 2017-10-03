<%@ Page Title="Графики" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lifts.aspx.cs" Inherits="KOS.Lifts" %>
<%@ Register tagprefix="uc" tagname="LiftsReport" src="~/Controls/LiftsReport.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Графики</h3>
    <asp:PlaceHolder ID="Qst" runat="server">
        <asp:DropDownList ID="Type" runat="server"></asp:DropDownList>
        <table>
            <tr>
                <td colspan="2">Период</td>
            </tr>
            <tr>
                <td>C
                <asp:Calendar ID="PeriodBeg" runat="server"></asp:Calendar>
                </td>
                <td>по
                <asp:Calendar ID="PeriodEnd" runat="server"></asp:Calendar>
                </td>
            </tr>
        </table>
        Участок:
        <asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Маршрут:
        <asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Report" runat="server" Text="Показать" OnClick="Report_Click" Width="110px" /><br />
    Лифт: 
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
                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        
    </asp:PlaceHolder>
    <div id="dvHtml" runat="server">
    <asp:PlaceHolder ID="Out" runat="server" Visible="false">
        <asp:PlaceHolder ID="phGo" runat="server" Visible="false">
            <asp:HyperLink ID="PrevMonth" runat="server"></asp:HyperLink>
            <asp:HyperLink ID="NextMonth" runat="server"></asp:HyperLink>
        </asp:PlaceHolder>
        <uc:LiftsReport ID="LiftsRep" runat="server" />
    </asp:PlaceHolder>
        </div>
</asp:Content>
