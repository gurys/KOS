<%@ Page Title="Домашняя страница ОДС" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OdsHome.aspx.cs" Inherits="KOS.OdsHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
    <style type="text/css">
        .auto-style1 {
            height: 162px;
			vertical-align:top;
        }
    </style>
<%-- сюда вставляем скрипт--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <table class="tableodshome">
        <tr>
            <td style="padding-left:50px; border:none;" class="auto-style1"><br />
                <table cellpadding="0" cellspacing="5" style="border:none; width:300px;">
                    <tr>
                    	<td class="tdwhite" colspan="2">МОНИТОРИНГ</td>
                    </tr>
                	<tr>
                    	<td class="tdwhite">в нормальной работе</td>
                        <td class="tdwhite"><asp:Label ID="Worked" runat="server" style="color: #306b19; text-align: center;" CssClass="float-right"></asp:Label></td>
                    </tr>
                    <tr>
                    	<td class="tdwhite">простой</td>
                        <td class="tdwhite" onclick="clickIt('Arc.aspx?t=5')"><asp:Label ID="Stopped" runat="server" style="color: #FF0000; text-align: center;" CssClass="float-right"></asp:Label></td>
                    </tr>
                	<tr>
                    	<td class="tdwhite">АКТИВНЫЕ СОБЫТИЯ</td>
                        <td class="tdwhitea"><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Arc.aspx?t=1" style="width:100%; height:25px; display:block; padding-top:5px; text-align: right;" CssClass="float-right">[HyperLink1]</asp:HyperLink></td>

               </table>
            
             <td class="auto-style1"><br />
                <table cellpadding="0" cellspacing="5" style="border:none; width:292px;">
                        <tr>
                    	<td class="tdwhite" style ="color:red">ГОРЯЧАЯ ЛИНИЯ ЗАКАЗЧИКА ////</td>
<td class="tdwite">
    <script type="text/javascript">
        document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
        document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/4e7f6cc9-96fc-4e4b-88e6-f4c4dcd8d55f.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
    InitClick2Call("en");
</script>
                             </td>
                        </tr>
                        <tr>
                        <td colspan="2">
                        <asp:Button ID="Lifts1" class="buttonblue" runat="server" Text="РЕГИСТРАЦИЯ СОБЫТИЙ" OnClick="Lifts1_Click" Width="292px" BorderStyle="Outset" BackColor="#CC0000" ForeColor="White" />
                  <asp:Button ID="ZakrytieODS" class="buttonblue" runat="server" Text="ЗАКРЫТИЕ" OnClick="ZakrytieODS_Click" Width="292px" BorderStyle="Outset" BackColor="#336699" ForeColor="White" />
                  <asp:Button ID="ArcButton" class="buttonblue" runat="server" Text="АРХИВ" OnClick="ArcButton_Click" Width="292px" BorderStyle="Outset" BackColor="#339966" ForeColor="White" />
                            </td>
                  
                    	</tr>
                   </table>
 </td>
 </tr>
            <td style="padding-left:54px;" class="valignbottom">
                <table style="border:none; width:292px;">
                    <td>
</table>
            </td>
           <td class="valignbottom">
                <table style="border:none; width:292px;">
                    <tr><td style="border:none; width:200px;">
                       
                    </td></tr>
                </table>
            </td>
       </table>
</asp:Content>
