<%@ Page Title="Архив" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Arc.aspx.cs" Inherits="KOS.Arc" %>
<%@ Register tagprefix="uc" tagname="DatePicker" src="~/Controls/DatePicker.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Архив</h3><br />
    <asp:PlaceHolder ID="Period" runat="server">
        С <uc:DatePicker ID="Beg" runat="server" />
        <span style="padding-left:15px;">по</span> <uc:DatePicker ID="End" runat="server" /> <br /><br />
        <asp:Button ID="DoIt" runat="server" Text="Показать" OnClick="DoIt_Click" />         
    </asp:PlaceHolder>
   <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br /><br />
     Скачать pdf<asp:ImageButton ID="btnClick" runat="server" OnClick="btnClick_Click" AlternateText="pdf" ImageUrl="~/Images/pdf56.png" Width="30" />
    excel<asp:ImageButton ID="Excel" runat="server" OnClick="Excel_Click" AlternateText="excel" ImageUrl="~/Images/excel50.png" Width="30" />
    <asp:HyperLink ID="Download" runat="server" Visible="true"></asp:HyperLink>
    
     <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
       <html  xmlns = "http://www.w3.org/1999/xhtml"> 
           <body> 
        
        <div id="dvHtml" runat="server">
            
    <asp:PlaceHolder ID="phReport" runat="server">
                
        <br />       
        <asp:Label ID="What" runat="server"></asp:Label><br />
           
                <asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                                <td id="td13" runat="server" class="td5px">№</td>
                                <td id="td10" runat="server" class="td5px">Отправил</td>
                                <td id="td1" runat="server" class="td5px">Когда</td>
                                <td id="td3" runat="server" class="td5px">Лифт</td>
                                <td id="td4" runat="server" class="td5px">Событие</td>
                                <td id="td11" runat="server" class="td5px">Описание</td>
                                <td id="td5" runat="server" class="td5px">Принял</td>
                                <td id="td14" runat="server" class="td5px">Когда</td>
                                <td id="td6" runat="server" class="td5px">Выполнил</td>
                                <td id="td12" runat="server" class="td5px">Коммент.</td>
                                <td id="td7" runat="server" class="td5px">Когда вып.</td>
                                <td id="td9" runat="server" class="td5px">Простой</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="Date1" runat="server" Text='<%# Eval("Date1") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Category" runat="server" Text='<%# Eval("Category") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Text" runat="server" Text='<%# Eval("Text") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="Prinyal" runat="server" Text='<%# Eval("Prinyal") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="StartPrinyal" runat="server" Text='<%# Eval("StartPrinyal") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Vypolnil" runat="server" Text='<%# Eval("Vypolnil") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Couse" runat="server" Text='<%# Eval("Couse") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Date2" runat="server" Text='<%# Eval("Date2") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="Prostoy" runat="server" Text='<%# Eval("Prostoy") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </asp:PlaceHolder>
            </div> 
         </body> 
      </html>
    
</asp:Content>
