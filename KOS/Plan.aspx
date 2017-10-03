<%@ Page Title="График Работ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Plan.aspx.cs" Inherits="KOS.Plan" %>
<%@ Register tagprefix="uc" tagname="MonthPlan" src="~/Controls/MonthPlan.ascx" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc:MonthPlan ID="MonthPlan13" runat="server" /><br />
    № участка <asp:DropDownList ID="IdUM" runat="server" BackColor="Window" OnSelectedIndexChanged="IdUM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList><br />
    <asp:ListView ID="TpPlan" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                   
                    <td id="Td3" runat="server">Лифт</td>
                    <td id="Td4" runat="server">Тип работ</td>
                    <td id="Td5" runat="server">Дата</td>
                    <td id="Td6" runat="server" style="width: 3px">8</td>
                    <td id="Td7" runat="server" style="width: 3px">9</td>
                    <td id="Td8" runat="server" style="width: 3px">10</td>
                    <td id="Td9" runat="server" style="width: 3px">11</td>
                    <td id="Td10" runat="server" style="width: 3px">12</td>
                    <td id="Td11" runat="server" style="width: 3px">13</td>
                    <td id="Td12" runat="server" style="width: 3px">14</td>
                    <td id="Td13" runat="server" style="width: 3px">15</td>
                    <td id="Td14" runat="server" style="width: 3px">16</td>
                    <td id="Td1" runat="server" style="width: 3px">17</td>
                    <td id="Td17" runat="server" style="width: 3px">18</td>
                    <td id="Td15" runat="server" style="width: 3px">19</td>
                    <td id="Td16" runat="server" style="width: 3px">20</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
               
                <td>
                    <asp:Label ID="IdL" runat="server" Text='<%# Eval("LiftId") %>' />
                </td>
                <td>
                    <asp:Label ID="TpType" runat="server" Text='<%# Eval("TpId") %>' />
                </td>
                <td style="text-wrap:none">
                    <asp:Label ID="Day" runat="server"><%# Eval("Date") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H08" runat="server"><%# Eval("H08") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H09" runat="server"><%# Eval("H09") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H10" runat="server"><%# Eval("H10") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H11" runat="server"><%# Eval("H11") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H12" runat="server"><%# Eval("H12") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H13" runat="server"><%# Eval("H13") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H14" runat="server"><%# Eval("H14") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H15" runat="server"><%# Eval("H15") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H16" runat="server"><%# Eval("H16") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H17" runat="server"><%# Eval("H17") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H18" runat="server"><%# Eval("H18") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H19" runat="server"><%# Eval("H19") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="H20" runat="server"><%# Eval("H20") %></asp:Label>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>     
            <h3><asp:Literal ID="Zayavky" runat="server"></asp:Literal></h3>
            <asp:ListView ID="ZayavkyList" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #ed9696">
                    <td id="Th1" runat="server">Лифт &nbsp;</td>
                    <td id="Td1" runat="server">событие</td>
                    <td id="Td2" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="Zayavka" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            <asp:ListView ID="ZayavkyNA" runat="server">
        <ItemTemplate>
            <table border="1" id="tbl1" runat="server">
           <tr id="Tr1" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Zayavka" runat="server" > <%# Eval("Idi") %></asp:Label>
                </td>
                 <td>
                    <asp:Label ID="LinkButton1" runat="server" > <%# Eval("Title") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LinkButton2" runat="server" > <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
    </asp:ListView>
      <h3><asp:Label ID="ReportTitle" runat="server"></asp:Label></h3> 
    <asp:ListView ID="List" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td id="Th1" runat="server">Лифт &nbsp;</td>
                    <td id="Td1" runat="server">событие</td>
                    <td id="Td2" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:LinkButton ID="Zayavka" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>   
            <asp:ListView ID="ListNA" runat="server">        
        <ItemTemplate>
            <table border="1" id="tbl1" runat="server">
            <tr id="Tr1" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Zayavka" runat="server"> <%# Eval("Idi") %></asp:Label>
                </td>
                 <td>
                    <asp:Label ID="LinkButton1" runat="server"> <%# Eval("Title") %></asp:Label>
                </td>
                <td>
                     <asp:Label ID="LinkButton2" runat="server"> <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
    </asp:ListView>
</asp:Content>
