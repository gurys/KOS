<%@ Page Title="Монитор Событий" Language="C#"MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reg_wz.aspx.cs" Inherits="KOS.Reg_wz" %>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
         <h5> &nbsp;Все события</h5>
  
 <asp:Label ID="What" runat="server"></asp:Label>

          &nbsp;<asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table id="tblr" runat="server" border="1">
                    <tr id="Tr2" runat="server" style="background-color:#85a6d3; color:#090909;">
                        <td id="td13" runat="server" class="td5px">№ </td>
                        <td id="td10" runat="server" class="td5px">описание</td>                        
                        <td id="td2" runat="server" class="td5px">дата/время</td>                        
                        <td id="td11" runat="server" class="td5px">источник</td>
                        <td id="td5" runat="server" class="td5px">отправил</td>
                        <td id="td14" runat="server" class="td5px">оператор</td>
                        <td id="td6" runat="server" class="td5px">событие</td>
                        <td id="td12" runat="server" class="td5px">участок</td>
                        <td id="td7" runat="server" class="td5px">маршрут</td>
                        <td id="td8" runat="server" class="td5px">лифт</td>
                        <td id="td1" runat="server" class="td5px">адрес</td>
                        
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td class="tdwhite">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                        </asp:HyperLink></td>
                    <td class="tdwhite">
                        <asp:Label ID="From" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>
                    
                    <td class="tdwhite">
                        <asp:Label ID="Time1" runat="server" Text='<%# Eval("DataId") %>' />
                    </td>
                  
                    <td class="tdwhite">
                        <asp:Label ID="Text" runat="server" Text='<%# Eval("Sourse") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Prinyal" runat="server" Text='<%# Eval("Family") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Vypolnil" runat="server" Text='<%# Eval("IO") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Couse" runat="server" Text='<%# Eval("TypeId") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date2" runat="server" Text='<%# Eval("IdU") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Time2" runat="server" Text='<%# Eval("IdM") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Prostoy" runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                    </td>
                </tr>
                 
            </ItemTemplate>
         </asp:ListView>
    
</asp:Content>
