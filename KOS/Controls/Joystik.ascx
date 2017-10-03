<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Joystik.ascx.cs" Inherits="KOS.Controls.Joystik" %>

<table border="0">
    <tr><td></td><td>
        <asp:ImageButton ID="Up" runat="server" ImageUrl="~/Images/joyUp.png" /></td><td></td></tr>
    <tr>
        <td><asp:ImageButton ID="Left" runat="server" ImageUrl="~/Images/joyLeft.png" /></td>
        <td></td>
        <td><asp:ImageButton ID="Right" runat="server" ImageUrl="~/Images/joyRight.png" /></td>
    </tr>
    <tr>
        <td><asp:ImageButton ID="Home" runat="server" ImageUrl="~/Images/joyHome.png" PostBackUrl="~/Default.aspx" /></td>
        <td><asp:ImageButton ID="Down" runat="server" ImageUrl="~/Images/joyDown.png" /></td>
        <td></td>
    </tr>
</table>