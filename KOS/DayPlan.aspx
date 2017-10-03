<%@ Page Title="План на день" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DayPlan.aspx.cs" Inherits="KOS.DayPlan" %>
<%@ Register tagprefix="uc" tagname="DayPlan" src="~/Controls/DayPlan.ascx" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <uc:DayPlan runat="server" />
    <asp:PlaceHolder ID="UnplanBlock" runat="server" Visible ="false">
        <asp:ListView ID="Unplan" runat="server">
            <LayoutTemplate>
                <table border="1" id="tbl1" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td>срочность</td>
                        <td>№ лифта</td>
                        <td>описание</td>
                        <td>текст замечания</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:Label runat="server" Text='<%# Eval("Index") %>' />
                    </td>
                    <td>
                        <asp:Label runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>
                    <td>
                        <asp:LinkButton runat="server" PostBackUrl='<%# Eval("Url") %>'>замечание</asp:LinkButton>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" BackColor="Orange" Text='<%# Eval("Text") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:PlaceHolder>
     <br /> <!-- заявки ОДС активные-->
    <asp:PlaceHolder ID="DayZayav" runat="server" Visible ="false">     
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
                    <asp:LinkButton ID="Zayavka" runat="server" Width="85" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" Width="85" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton2" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
        <asp:ListView ID="ZayavkyNA" runat="server">
        <ItemTemplate>
            <table border="1" id="tbl1" runat="server">
           <tr id="Tr1" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Zayavka" runat="server" Width="91">  <%# Eval("Idi") %></asp:Label>
                </td>
                 <td>
                    <asp:Label ID="LinkButton1" runat="server" Width="91">  <%# Eval("Title") %></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LinkButton2" runat="server" Width="506">  <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
    </asp:ListView>     
         </asp:PlaceHolder>
    <!-- заявки механика активные-->
        <asp:PlaceHolder ID="DayPrim" runat="server" Visible ="false">       
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
                    <asp:LinkButton ID="Zayavka" runat="server" Width="85" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" Width="85" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton2" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            <asp:ListView ID="ListNA" runat="server">        
        <ItemTemplate>
            <table border="1" id="tbl1" runat="server">
            <tr id="Tr1" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Zayavka" runat="server" Width="91"> <%# Eval("Idi") %></asp:Label>
                </td>
                 <td>
                    <asp:Label ID="LinkButton1" runat="server" Width="91"> <%# Eval("Title") %></asp:Label>
                </td>
                <td>
                     <asp:Label ID="LinkButton2" runat="server" Width="506"> <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
    </asp:ListView>           
     </asp:PlaceHolder>
</asp:Content>
