<%@Page Title="Регистрация событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DiagrammTSG.aspx.cs" Inherits="KOS.DiagrammTSG" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h4> Регистрация событий:</h4>
    <br /> Дежурная служба:
   <script type="text/javascript">
       var str = '<%= User.Identity.Name%>';
       if (str === "ODS21" || str === "ODS22" || str === "ODS23" || str === "ODS24" || str === "ODS25" || str === "ODS26" || str === "ODS31") {
           document.write("уч. 2.1-2.6,3.1 ");
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
       else if (str === "ODS42" || str === "ODS41") {
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
    <br />
    <br />
    <div>
       Вид услуги:<asp:DropDownList ID="Uslugy" runat="server" AutoPostBack="true" OnTextChanged="Uslugy_TextChanged" Height="25px"></asp:DropDownList>
       <asp:PlaceHolder ID="Cat" runat="server"><span style="padding-left:1px;">Вид работ:</span><asp:DropDownList ID="Category" Height="25px" runat="server" meta:resourcekey="CategoryResource1"></asp:DropDownList></asp:PlaceHolder>
       Адрес:<asp:DropDownList ID="Address" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="Address_TextChanged"></asp:DropDownList>
       <br />Зона:
        <asp:Label runat="server" ID="NLift" ForeColor="Blue" meta:resourcekey="NLiftResource1"></asp:Label>
        <asp:PlaceHolder ID="TA" runat="server"><asp:TextBox ID="TextArea" runat="server" Height="21px" Width="230px" TextMode="MultiLine" meta:resourcekey="TextAreaResource1"></asp:TextBox></asp:PlaceHolder>         
         <asp:DropDownList ID="Lift" runat="server" Visible="false" meta:resourcekey="LiftResource1" Height="25px"></asp:DropDownList><br />        
        <br />Источник:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Диспетчер:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; Принял:<br />
        <asp:TextBox ID="Text1" runat="server" Height="23px" Width="239px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>
        &nbsp;&nbsp; &nbsp;&nbsp;<asp:PlaceHolder ID="Fdr" runat="server"><asp:DropDownList ID="FIO" runat="server" meta:resourcekey="FIOResource1" Height="25px"></asp:DropDownList></asp:PlaceHolder>
        &nbsp;&nbsp; &nbsp;&nbsp; <asp:PlaceHolder ID="Wrk" runat="server"><asp:DropDownList ID="Workers" runat="server" meta:resourcekey="WorkerResource1" Height="25px"></asp:DropDownList></asp:PlaceHolder>
        <asp:TextBox ID="Text2" runat="server" Visible="false" Height="23px" Width="130px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>   
        
        <br />   
        <br />
        Описание:<br />
        <asp:TextBox ID="Text" runat="server" Height="45px" Width="661px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox><br />
        <asp:Button ID="Save" runat="server" class="buttonblue" Text="РЕГИСТРАЦИЯ СОБЫТИЙ" OnClick="Save_Click" BackColor="#CC0000" BorderStyle="Double" ForeColor="#CCFF99" Height="35px" Width="670px" meta:resourcekey="SaveResource1" />
        
    </div>
     
    Отправил: <asp:Literal ID="Disp" runat="server" meta:resourcekey="DispResource1"></asp:Literal> <br /> 
      <asp:Label ID="Msg" runat="server" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
      <asp:Label ID="SmsText" runat="server" Text=""></asp:Label><br />
      <asp:Button ID="Button1" runat="server" Text="К списку Активных Событий"  OnClick="Button1_Click"/>

</asp:Content>
