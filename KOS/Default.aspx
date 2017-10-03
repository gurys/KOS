<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="KOS._Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <table class="table">
        <tr><td>
            <asp:Literal ID="Date" runat="server"></asp:Literal>
        </td></tr>
        <tr><td onclick="clickIt('Plan.aspx')">
            &nbsp;</td></tr>
        <tr><td onclick="clickIt('DayPlan.aspx')">
            <asp:Button ID="Plan" runat="server" Text="График работ на" OnClick="Plan_Click" Width="250px" BackColor="#999999" BorderColor="White" BorderStyle="Outset" ForeColor="White"  /><br>
            <asp:Button ID="DayPlan" runat="server" Text="План работ на" OnClick="DayPlan_Click" Width="250px" BackColor="SlateBlue" BorderColor="White" BorderStyle="Outset" ForeColor="White" /><br>
            <asp:Button ID="WorkerZayavka" runat="server" Text="ЗАЯВКА" OnClick="WorkerZayavka_Click"  Width="251px" BorderColor="White" BackColor="Red" BorderStyle="Outset" ForeColor="White" /><br>
            <asp:Button ID="Akt" runat="server" Text="Мои События" OnClick="Akt_Click" Width="251px" BackColor="Tomato" BorderColor="White" BorderStyle="Outset" ForeColor="White" /><br>
            <asp:Button ID="Sklad" runat="server" Text="Мой Склад" OnClick="Sklad_Click" Width="251px" BackColor="Olive" BorderColor="White" BorderStyle="Outset" ForeColor="White" />
        
        </td></tr>
        <tr><td onclick="clickIt('Lib.aspx')">
            <asp:Button ID="Info" runat="server" Text="Справка" OnClick="Info_Click" BackColor="#009933" BorderColor="#FFFFCC" BorderStyle="Outset" ForeColor="White" />
        </td></tr>
        <tr><td onclick="clickIt('WorkerZayavka.aspx')">
            &nbsp;</td></tr>
        <tr><td onclick="clickIt('Zayavka.aspx')">
                &nbsp;</td></tr>
        <tr><td>
             </td></tr>
    </table>
            <asp:PlaceHolder ID="ZvODS" runat="server" Visible = "false">
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
                     <asp:LinkButton ID="LinkButton2" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="ZvREO" runat="server" Visible = "false">
            <h3><asp:Literal ID="ZayavkyREO" runat="server"></asp:Literal></h3>
             <asp:ListView ID="ZayavkyListREO" runat="server">
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
                     <asp:LinkButton ID="LinkButton2" runat="server" Width="500" PostBackUrl='<%# Eval("Url") %>'> <%# Eval("Text1") %></asp:LinkButton>
                </td><td></td> 
            </tr>
        </ItemTemplate>
    </asp:ListView>
            </asp:PlaceHolder>
       
</asp:Content>
