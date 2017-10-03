<%@ Page Title="Домашняя страница ОДС ТСЖ" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="ODS_TSG.aspx.cs" Inherits="KOS.ODS_TSG" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 162px;
			vertical-align:top;
        }           
        .auto-style2 {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 5px 10px;
            background-color: #ffffff;
            width: 125px;
        }
    </style>
<%-- сюда вставляем скрипт--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tableodshome">
        <tr>
            <td style="padding-left:50px; border:none;" class="auto-style1"><br />
                <table cellpadding="0" cellspacing="5" style="border:none; width:350px;">
                    <tr>
                    	<td class="tdwhite" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; МОНИТОРИНГ</td>
                    </tr>
                	<tr>
                    	<td class="tdwhite"><asp:Button ID="Button2" runat="server" Text="АКТИВНЫЕ СОБЫТИЯ" OnClick="RegTSG_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2"><asp:HyperLink ID="HyperLink2" runat="server" style="width:100%; display:block; padding-top:5px; text-align: center;" Width="62px" >[HyperLink2]</asp:HyperLink></td>
                    </tr>
                    <tr>
                    	<td class="tdwhite"><asp:Button ID="Button1" runat="server" Text="НЕ НАЗНАЧЕННЫЕ СОБЫТИЯ" OnClick="RegNZ_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2" ><asp:HyperLink ID="HyperLink1" runat="server" style="width:100%; height:25px; display:block; padding-top:5px; text-align: center;" >[HyperLink1]</asp:HyperLink></td>
                    </tr>
                    <tr>
                    	<td class="tdwhite"><asp:Button ID="Button3" runat="server" Text="Документы на подпись" OnClick="Button3_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2" ><asp:HyperLink ID="HyperLink3" runat="server" style="width:100%; height:25px; display:block; padding-top:5px; text-align: center;" >[HyperLink3]</asp:HyperLink></td>
                    </tr>
                	<tr>
                    	<td class="tdwhite" colspan="2"><asp:Button ID="DiagrammTSG" runat="server" Text="РЕГИСТРАЦИЯ" style="color: #FF0000;" OnClick="DiagrammTSG_Click" Width="277px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                      <tr>
                    	<td class="tdwhite" colspan="2"><asp:Button ID="ArcButton" runat="server" Text="АРХИВ" OnClick="ArcButton_Click" Width="277px"  BackColor="White" ForeColor="#333333" Height="30px" /></td>
                    </tr>                       	
               </table>
            
             <td class="auto-style1"><br />
                <table cellpadding="0" cellspacing="5" style="border:none; width:292px;">
                        <tr>
                    <td> &nbsp;</td>
                   <td class="tdwite">    &nbsp;</td>         
                        </tr>
                        <tr>
                <td colspan="2"> &nbsp;</td>
                    	</tr>
                     <tr>
                <td colspan="2"> &nbsp;</td>
                    	</tr>
                     <tr>
                <td colspan="2"> &nbsp;</td>
                    	</tr>
                     <tr>
                <td colspan="2"> &nbsp;</td>
                    	</tr>
                   </table>
 </td>
 </tr>              
</table>
    <asp:Timer ID="Timer1" runat="server" OnTick="InSmsText_Click" Interval="300000"></asp:Timer>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false"> 
             Ответные СМС от Дежурных служб: <br />
    Дата: <asp:Label ID="Creat" runat="server" Text=""></asp:Label>
    Телефон: <asp:Label ID="Sender" runat="server" Text=""></asp:Label>
    № события: <asp:Label ID="NumEv" runat="server" Text=""></asp:Label> 
    Статус: <asp:Label ID="Status" runat="server" Text=""></asp:Label>
            </asp:PlaceHolder>
</asp:Content>
