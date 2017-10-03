<%@ Page Title="Запрос КП" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZaprosKP.aspx.cs" Inherits="KOS.ZaprosKP" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server"> 
    <h4>События для запроса КП</h4>
   
 <table>
      <tr>
        <td rowspan="2">
                        <asp:ListView ID="Out" runat="server">
                            <LayoutTemplate>
                                <table border="1" id="tbl1" runat="server">
                                    <tr id="Tr2" runat="server" style="background-color: #7b99ae">
                                        <td id="Td2" runat="server">№ события </td>
                                        <td id="Td3" runat="server">дата/время</td>
                                        <td id="Td4" runat="server">участок</td>
                                        <td id="Td5" runat="server">маршрут</td>
                                        <td id="Td6" runat="server">лифт</td>
                                        <td id="Td7" runat="server">источник</td>
                                        <td id="Td1" runat="server">категория</td>                                       
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr1" runat="server">
                                    <td><asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                        <asp:Label ID="zId" runat="server" Text='<%# Eval("zId") %>' /></asp:HyperLink></td><td>
                                        <asp:Label ID="DataId" runat="server" Text='<%# Eval("DataId") %>' />
                                    </td>
                                      <td>
                                        <asp:Label ID="IdU" runat="server"><%# Eval("IdU") %></asp:Label></td><td>
                                        <asp:Label ID="IdM" runat="server"><%# Eval("IdM") %></asp:Label></td><td>
                                        <asp:Label ID="LiftId" runat="server"><%# Eval("LiftId") %></asp:Label></td><td>
                                        <asp:Label ID="Sourse" runat="server"><%# Eval("Sourse") %></asp:Label></td><td>
                                        <asp:Label ID="TypeId" runat="server"><%# Eval("TypeId") %></asp:Label></td><td></td>
                                   
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>  
          </tr>      
                </table>
    <asp:Button ID ="Label4" runat ="server" OnClick = "Page_Load" Text ="Отправить запрос"/>
</asp:Content>