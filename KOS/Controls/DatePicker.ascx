<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="KOS.Controls.DatePicker" %>
<asp:DropDownList ID="Year" runat="server" OnSelectedIndexChanged="Year_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
<asp:DropDownList ID="Month" runat="server" OnSelectedIndexChanged="Month_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
<asp:DropDownList ID="Day" runat="server"></asp:DropDownList>