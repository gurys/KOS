<%@ Page Title="Страница Управления ТСЖ" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="ManagerTSG.aspx.cs" Inherits="KOS.ManagerTSG" %>

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
    <table class="tableManagerTSG">
        <tr>
            <td style="padding-left:50px; border:none;" class="auto-style1"><br />
                <table cellpadding="0" cellspacing="5" style="border:none; width:350px;">
                    <tr>
                    	<td class="tdwhite" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; МОНИТОРИНГ</td>
                    </tr>
                	<tr>
                    	<td class="tdwhite"><asp:Button ID="Button2" runat="server" Text="АКТИВНЫЕ СОБЫТИЯ" OnClick="EventsTSG_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2"><asp:HyperLink ID="HyperLink2" runat="server" style="width:100%; display:block; padding-top:5px; text-align: center;" Width="62px" >[HyperLink2]</asp:HyperLink></td>
                    </tr>
                    <tr>
                    	<td class="tdwhite"><asp:Button ID="Button1" runat="server" Text="ПРЕВЫШЕНИЕ НОРМАТИВОВ" OnClick="ArcButton_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2" ><asp:HyperLink ID="HyperLink1" runat="server" style="width:100%; height:25px; display:block; padding-top:5px; text-align: center;" >[HyperLink1]</asp:HyperLink></td>
                    </tr>
                	<tr>
                    	<td class="tdwhite"><asp:Button ID="PrimTSG" runat="server" Text="ЗАМЕЧАНИЯ ПО ЗАЯВКАМ" OnClick="PrimTSG_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" /></td>
                        <td class="auto-style2"><asp:HyperLink ID="HyperLink3" runat="server" style="width:100%; height:25px; display:block; padding-top:5px; text-align: center;" >[HyperLink3]</asp:HyperLink></td>
                      <tr>
                    	<td class="tdwhite" colspan="2"><asp:Button ID="AdminTSG" runat="server" Text="Администрирование" OnClick="AdminTSG_Click" Width="270px" BorderStyle="None" BackColor="White" ForeColor="#333333" Height="30px" />&nbsp;</td>
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
                <td colspan="2"> <asp:Button ID="Reports" runat="server" Text="ОТЧЕТЫ" OnClick="ReportsTSG_Click" Width="277px"  BackColor="White" ForeColor="#333333" Height="30px" /></td>
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

      
</asp:Content>
