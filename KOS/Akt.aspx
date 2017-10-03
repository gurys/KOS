<%@ Page Title="Просмотр Моих Заявок" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Akt.aspx.cs" Inherits="KOS.Akt" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
    
    <h3>&nbsp; <asp:Label ID="What" runat="server"></asp:Label></h3> 
   
     &nbsp;<asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
               <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td id="td13" runat="server" class="td5px">№ </td>
                        <td id="td10" runat="server" class="td5px">дата</td>                        
                        <td id="td2" runat="server" class="td5px">лифт</td>
                        <td id="td6" runat="server" class="td5px">событие</td>                        
                        <td id="td8" runat="server" class="td5px">статус</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                        </asp:HyperLink></td>
                    <td>
                        <asp:Label ID="DataId" runat="server" Text='<%# Eval("DataId") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>  
                    <td>
                        <asp:Label ID="EventId" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>' />
                    </td>
                </tr>                 
            </ItemTemplate>      
         </asp:ListView>
     
    <asp:ListView ID="OutNa" runat="server">
            <LayoutTemplate>
                <table id="tbl1" runat="server" border="1">
                    <tr id="Tr2" runat="server" style="background-color:#fff999; color:#090909;">
                        <td id="td13" runat="server" class="td5px">№ </td>
                        <td id="td10" runat="server" class="td5px">дата</td>                        
                        <td id="td2" runat="server" class="td5px">лифт</td>
                        <td id="td6" runat="server" class="td5px">событие</td>                        
                        <td id="td8" runat="server" class="td5px">статус</td> 
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server" style="background-color: #fff999">
                    <td>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                    <td>
                        <asp:Label ID="DataId" runat="server" Text='<%# Eval("DataId") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>  
                    <td>
                        <asp:Label ID="EventId" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>' />
                    </td>                    
                </tr>                 
            </ItemTemplate>
         </asp:ListView>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<!--    <asp:ImageButton ID="btnClick" runat="server" OnClick="btnClick_Click" AlternateText="pdf" ImageUrl="~/Images/pdf56.png" Width="30" />
    <asp:HyperLink ID="Download" runat="server" Visible="false"></asp:HyperLink> -->
    <br />
    <br />
    </asp:Content>
