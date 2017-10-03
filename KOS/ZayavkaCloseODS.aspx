<%@ Page Title="Назначение заявки" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZayavkaCloseODS.aspx.cs" Inherits="KOS.ZayavkaCloseODS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     Дежурная служба:
   <script type="text/javascript">
       var str = '<%= User.Identity.Name%>';
       if (str === "ODS21" || str === "ODS22" || str === "ODS23" || str === "ODS24" || str === "ODS31" || str === "ODS32") {
           document.write("уч. 2., 3..");
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/5c336450-e07a-49fc-a3b6-22f3319265c6.js' type='text/javascript'%3E%3C/script%3E"));
       }
       else if (str === "ODS13") {
           document.write("уч. 1.3");
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/8c8f5b64-0cd9-4c85-9031-ef69020da832.js' type='text/javascript'%3E%3C/script%3E"));
       }
       else if (str === "ODS14") {
           document.write("уч. 1.4");
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/6387a5e8-3b95-4b9e-9bc7-bde50ac40658.js' type='text/javascript'%3E%3C/script%3E"));
       }
       else if (str === "ODS42" || str === "ODS41" || str === "ODS_test") {
           document.write("уч. 4.2, 4.1");
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/5602f514-bd81-42ba-a520-f71ed5c44cdd.js' type='text/javascript'%3E%3C/script%3E"));
       }
       else if (str === "ODS11" || str === "ODS12" || str === "ODS15" || str === "ODS17") {
           document.write("уч. 1.1, 1.2, 1.5, 1.7");
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/dd9cca3e-dbe6-498b-94e5-59192b61b975.js' type='text/javascript'%3E%3C/script%3E"));
       }
       else {
           document.write(str.toUpperCase());
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
           document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/4e7f6cc9-96fc-4e4b-88e6-f4c4dcd8d55f.js' type='text/javascript'%3E%3C/script%3E"));
       }
</script>
<script type="text/javascript">
    InitClick2Call("en");
</script>
                
                <table id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #808080; color:#FFF;">
                        <td id="td1" runat="server" class="td5px">Лифт:</td>
                        <td id="td10" runat="server" class="td5px">Адрес:</td>
                        <td id="td2" runat="server" class="td5px">Событие:</td>
                        <td id="td3" runat="server" class="td5px">Отправил:</td>
                        <td id="td4" runat="server" class="td5px">Источник:</td>
                        <td id="td5" runat="server" class="td5px">Отправлено:</td>
                        <td id="td6" runat="server" class="td5px">Принял:</td>
                        <td id="td7" runat="server" class="td5px">Когда:</td>
                        <td id="td8" runat="server" class="td5px">Устранил:</td>
                        <td id="td12" runat="server" class="td5px">Когда:<br /></td>
                        <td id="td9" runat="server" class="td5px">Статус:</td>
                        <td id="td11" runat="server" class="td5px">ПРОСТОЙ:</td>
                    </tr>
                    <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td24" runat="server" class="td5px"><asp:Literal ID="Lift" runat="server"></asp:Literal></td>
                        <td id="td13" runat="server" class="td5px"><asp:Literal ID="Address" runat="server"></asp:Literal></td>
                        <td id="td14" runat="server" class="td5px"><asp:Literal ID="Category" runat="server"></asp:Literal></td>
                        <td id="td15" runat="server" class="td5px"><asp:Literal ID="Disp" runat="server"></asp:Literal></td>
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="From" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="Start" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="Prinyal" runat="server"></asp:Literal></td>
                        <td id="td19" runat="server" class="td5px"><asp:Literal ID="StartPrinyal" runat="server"></asp:Literal> </td>
                        <td id="td20" runat="server" class="td5px"><asp:Literal ID="_worker" runat="server"></asp:Literal></td>
                        <td id="td21" runat="server" class="td5px"><asp:Literal ID="Finish" runat="server"></asp:Literal></td>
                        <td id="td22" runat="server" class="td5px"><asp:Literal ID="Status" runat="server"></asp:Literal></td>
                        <td id="td23" runat="server" class="td5px"><asp:Literal ID="Prostoy" runat="server"></asp:Literal></td>
                    </tr>
                    </table>
            

&nbsp;Описание события:
    <br />
    <asp:TextBox ID="Text" runat="server" Height="60px" Width="576px" TextMode="MultiLine" Columns="50" Rows="5" ReadOnly="true"></asp:TextBox><br />
&nbsp;Комментарий:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Выбор механика: <asp:DropDownList ID="Worker" runat="server"></asp:DropDownList>
    <br /><asp:TextBox ID="Couse" runat="server" Height="60px" Width="580px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox>
    <asp:Button ID="Save" runat="server" Text="Закрыть" OnClick="Save_Click" />
</asp:Content>
