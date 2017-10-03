<%@Page Title="Архив событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArcTsg.aspx.cs" Inherits="KOS.ArcTsg" %>

<%@ Register tagprefix="uc" tagname="DatePicker" src="~/Controls/DatePicker.ascx" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Архив событий</h3><br />
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
                        <td id="td5" runat="server" class="td5px">№ события</td>
                        <td id="td10" runat="server" class="td5px">источник</td>
                        <td id="td1" runat="server" class="td5px">диспетчер</td>
                        <td id="td2" runat="server" class="td5px">дата/время</td>                       
                        <td id="td4" runat="server" class="td5px">вид услуги</td>
                        <td id="td13" runat="server" class="td5px">зона</td>
                        <td id="td11" runat="server" class="td5px">событие</td>                                              
                        <td id="td7" runat="server" class="td5px">описание</td>
                        <td id="td8" runat="server" class="td5px">принял</td>
                        <td id="td6" runat="server" class="td5px">дата/время</td>
                        <td id="td12" runat="server" class="td5px">исполнил</td>
                        <td id="td9" runat="server" class="td5px">комментарий</td>
                        <td id="td14" runat="server" class="td5px">дата/время</td>                        
                        <td id="td16" runat="server" class="td5px">тайминг</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                    <td class="tdwhite" >                                          
                        <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Sourse" runat="server" Text='<%# Eval("Sourse") %>' />
                    </td>
                    <td class="tdwhite">
                            <asp:Label ID="IO" runat="server" Text='<%# Eval("IO") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date1" runat="server" Text='<%# Eval("Date1") %>' />
                    </td>                   
                    <td class="tdwhite">
                        <asp:Label ID="RegistrId" runat="server" Text='<%# Eval("RegistrId") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                        </td>
                        <td class="tdwhite">
                        <asp:Label ID="TypeId" runat="server" Text='<%# Eval("TypeId") %>' />                    
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="EventId" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>                   
                    <td class="tdwhite">
                        <asp:Label ID="ToApp" runat="server" Text='<%# Eval("ToApp") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="DateToApp" runat="server" Text='<%# Eval("DateToApp") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Who" runat="server" Text='<%# Eval("Who") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Comment" runat="server" Text='<%# Eval("Comment") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date2" runat="server" Text='<%# Eval("Date2") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Timing" runat="server" Text='<%# Eval("Timing") %>' />
                    </td>

                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            </asp:PlaceHolder>
            </div> 
         </body> 
      </html>
    
</asp:Content>
