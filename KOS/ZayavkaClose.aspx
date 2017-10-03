<%@ Page Title="Закрытие заявки" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZayavkaClose.aspx.cs" Inherits="KOS.ZayavkaClose" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
     Дежурная служба:
    <script type="text/javascript">
        var str = '<%= User.Identity.Name%>';
        if (str === "ODS21" || str === "ODS22" || str === "ODS23" || str === "ODS24" || str === "ODS31" || str === "ODS32") {
            document.write("уч. 2., 3., 4.");
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
        else if (str === "ODS11" || str === "ODS12" || str === "ODS15") {
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
    Адрес: <asp:Literal ID="Address" runat="server"></asp:Literal>
    <br />
    № лифта: <asp:Literal ID="Lift" runat="server"></asp:Literal>
    <br />
    Категория: <asp:Literal ID="Category" runat="server"></asp:Literal>
    <br />
    Инцидент: 
    <br />
    <asp:TextBox ID="Text" runat="server" Height="79px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5" ReadOnly="true"></asp:TextBox><br />
    Отправил: <asp:Literal ID="Disp" runat="server"></asp:Literal>
    <br />
    Источник: <asp:Literal ID="From" runat="server"></asp:Literal>
    <br />
    Отправлено: <asp:Literal ID="Start" runat="server"></asp:Literal>
    <br />
    Принял: <asp:Literal ID="Prinyal" runat="server"></asp:Literal>
    <br />
    Когда: <asp:Literal ID="StartPrinyal" runat="server"></asp:Literal>
    <br />
    Статус: <asp:Literal ID="Status" runat="server"></asp:Literal>
    <br />
    Устранил: <asp:Literal ID="Worker" runat="server"></asp:Literal>
    <br />
    Когда: <asp:Literal ID="Finish" runat="server"></asp:Literal>
    <br />
    Причина: 
    <br />
    <asp:TextBox ID="Couse" runat="server" Height="80px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
    Простой: <asp:Literal ID="Prostoy" runat="server"></asp:Literal>
    <br />
    <asp:Button ID="Save" runat="server" Text="Выполнил" OnClick="Save_Click" />
</asp:Content>
