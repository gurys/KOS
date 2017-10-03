<%@  Page Title="События механика" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="AktWork.aspx.cs" Inherits="KOS.AktWork" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Все мои события (активные и в ожидании):
 <br /> <!-- заявки ОДС активные-->
    <asp:PlaceHolder ID="DayZayav" runat="server" Visible="false">
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
                    <asp:LinkButton ID="Zayavka" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton2" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
        <asp:ListView ID="ZayavkyNA" runat="server">
        <ItemTemplate>
            <table border="1" id="Table1" runat="server">
           <tr id="Tr3" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Label1" runat="server" Width="86">  <%# Eval("Idi") %>&nbsp;</asp:Label>
                </td>
                 <td>
                    <asp:Label ID="Label2" runat="server" Width="86">  <%# Eval("Title") %>&nbsp;&nbsp;</asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label3" runat="server" Width="506">  <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
    </asp:ListView>     
         </asp:PlaceHolder>
    <!-- заявки механика активные-->
        <asp:PlaceHolder ID="DayPrim" runat="server" Visible="false"> 
    <asp:ListView ID="List" runat="server">
        <LayoutTemplate>
            <table border="1" id="Table2" runat="server">
                <tr id="Tr4" runat="server" style="background-color: #85a6d3">
                    <td id="Td3" runat="server">Лифт &nbsp;</td>
                    <td id="Td4" runat="server">событие</td>
                    <td id="Td5" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />               
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr6" runat="server">
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton5" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            <asp:ListView ID="ListNA" runat="server">        
        <ItemTemplate>
            <table border="1" id="Table3" runat="server" style="grid-row-sizing:initial">
            <tr id="Tr7" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Label4" runat="server" Width="86"> <%# Eval("Idi") %>&nbsp;</asp:Label>
                </td>
                 <td>
                    <asp:Label ID="Label5" runat="server" Width="86"> <%# Eval("Title") %>&nbsp;&nbsp;</asp:Label>
                </td>
                <td>
                     <asp:Label ID="Label6" runat="server" Width="506"> <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
       </asp:ListView>
            <asp:ListView ID="ListZap" runat="server">
        <LayoutTemplate>
            <table border="1" id="Table2" runat="server">
                <tr id="Tr4" runat="server" style="background-color: #339933">
                    <td id="Td3" runat="server">Лифт &nbsp;</td>
                    <td id="Td4" runat="server">событие</td>
                    <td id="Td5" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />               
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr6" runat="server">
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton5" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            <asp:ListView ID="ListZapNA" runat="server">        
        <ItemTemplate>
            <table border="1" id="Table3" runat="server">
            <tr id="Tr7" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Label4" runat="server" Width="86"> <%# Eval("Idi") %>&nbsp;</asp:Label>
                </td>
                 <td>
                    <asp:Label ID="Label5" runat="server" Width="86"> <%# Eval("Title") %>&nbsp;&nbsp;</asp:Label>
                </td>
                <td>
                     <asp:Label ID="Label6" runat="server" Width="506"> <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
       </asp:ListView> 
            <asp:ListView ID="ListInst" runat="server">
        <LayoutTemplate>
            <table border="1" id="Table2" runat="server">
                <tr id="Tr4" runat="server" style="background-color: #99cc00">
                    <td id="Td3" runat="server">Лифт &nbsp;</td>
                    <td id="Td4" runat="server">событие</td>
                    <td id="Td5" runat="server">описание</td><td></td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />               
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr6" runat="server">
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Idi") %></asp:LinkButton>
                </td>
                 <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" Width="80" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Title") %></asp:LinkButton>
                </td>
                <td>
                     <asp:LinkButton ID="LinkButton5" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            <asp:ListView ID="ListInstNA" runat="server">        
        <ItemTemplate>
            <table border="1" id="Table3" runat="server">
            <tr id="Tr7" runat="server" style="background-color: #fff999">
                <td>
                    <asp:Label ID="Label4" runat="server" Width="86"> <%# Eval("Idi") %>&nbsp;</asp:Label>
                </td>
                 <td>
                    <asp:Label ID="Label5" runat="server" Width="86"> <%# Eval("Title") %>&nbsp;&nbsp;</asp:Label>
                </td>
                <td>
                     <asp:Label ID="Label6" runat="server" Width="506"> <%# Eval("Text1") %></asp:Label>
                </td><td></td> 
            </tr>
                </table>
        </ItemTemplate>
       </asp:ListView>                                
     </asp:PlaceHolder>
</asp:Content>
