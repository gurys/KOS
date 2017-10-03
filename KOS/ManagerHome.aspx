<%@ Page Title="Домашняя страница" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManagerHome.aspx.cs" Inherits="KOS.ManagerHome" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phWebPhone" runat="server" Visible ="false">
     <script src="https://api.yandex.mightycall.ru/api/v1/sdk/mightycall.webphone.sdk.js">
</script>
<script type="text/javascript">
    var mcConfig = { login: "f98f97c4-d153-4105-829c-bfdf0a9042f9", password: "100" };
    MightyCallWebPhone.ApplyConfig(mcConfig);

    MightyCallWebPhone.Phone.Init();
</script> 
<script type="text/javascript">
    MightyCallWebPhone.Phone.Call('+79269338001'); 
    MightyCallWebPhone.Phone.Focus();
</script> </asp:PlaceHolder> 
    Мониторинг Событий и интерфейсов: 
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="WebPhone_Click" Text="WebPhone" ImageUrl="~/Images/telephone2.png" Height="50" Width="50"/> 
    <br />
     <table border="1" id="tbl3" runat="server">
         <tr id="Tr31" runat="server" style="background-color: #ffffff">
                    <td id="Td3" runat="server">&nbsp;Этапы СОБЫТИЙ:</td>
                    <td id="Td4" runat="server">&nbsp;всего:</td>
                    <td id="Th3" runat="server">&nbsp;сроч:</td>
                </tr>
        <tr id="Tr3" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink46" runat="server" Text ="Активные События" NavigateUrl="~/Reg_wz.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink47" runat="server" NavigateUrl="~/Reg_wz.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink48" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
          <tr id="Tr4" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink49" runat="server" Text ="Запрос менеджеру" NavigateUrl="~/ZaprosMng.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink50" runat="server"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink51" runat="server" style="color: #FF0000; text-align: center;"></asp:HyperLink></td>
        </tr>
         <tr id="Tr5" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink52" runat="server" Text ="Запрос КП" NavigateUrl="~/ReportsTSG.aspx?t=2"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink53" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink54" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr6" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink55" runat="server" Text ="Анализ КП " NavigateUrl="~/ReportsTSG.aspx?t=3"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink56" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink57" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr7" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink58" runat="server" Text ="Ожидание счета на оплату" NavigateUrl="~/ReportsTSG.aspx?t=4"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink59" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink60" runat="server" style="color: #FF0000; text-align: center;"></asp:HyperLink></td>
        </tr>
        <tr id="Tr8" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink61" runat="server" Text ="Счета получены" NavigateUrl="~/ReportsTSG.aspx?t=5"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink62" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink63" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr9" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink64" runat="server" Text ="Ожидание оплаты" NavigateUrl="~/ReportsTSG.aspx?t=6"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink20" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink21" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
          <tr id="Tr27" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink34" runat="server" Text ="Ожидание доставки" NavigateUrl="~/ReportsTSG.aspx?t=7"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink35" runat="server"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink36" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr> <tr id="Tr28" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink37" runat="server" Text ="Приход" NavigateUrl="~/ReportsTSG.aspx?t=8"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink38" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink39" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr> <tr id="Tr29" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink40" runat="server" Text ="Ожидание акта ВР" NavigateUrl="~/ReportsTSG.aspx?t=9"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink41" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink42" runat="server" style="color: #FF0000; text-align: center;"></asp:HyperLink></td>
        </tr> <tr id="Tr30" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink43" runat="server" Text ="Списание" NavigateUrl="~/ReportsTSG.aspx?t=10"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink44" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink45" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
    </table>
    <br /> 
    &nbsp;&nbsp;
 <asp:HyperLink ID="HyperLink19" runat="server" Text =" ОТЧЕТЫ!" ForeColor="BlueViolet" NavigateUrl="~/Reports.aspx"></asp:HyperLink>
    <br />
<br />
    <asp:Button ID="MonInt" runat="server" Text="Монитор интерфейсов" BackColor="YellowGreen" OnClick="MonInt_Click"/>
    <asp:Button ID="Button3" runat="server" Text="SMS" BackColor="PaleTurquoise" OnClick="Button3_Click"/>
    <asp:Button ID="Button4" runat="server" Text="Очистить"  OnClick="Button4_Click" /><br />
    <asp:PlaceHolder ID="Monitor" runat="server" Visible="false">
       Монитор интефейсов: <asp:Label ID="Label1" runat="server" ></asp:Label>
    <table border="1" id="tbl2" runat="server">
        <tr>
            <td style="font-style: normal; font-family: 'Arial Black'">инциденты:</td>           
           
            <td>Застревания<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/ZReport.aspx?t=1"></asp:HyperLink></td>
            <td class="auto-style1">не закрытые<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Arc.aspx?t=2"></asp:HyperLink></td>        
           
            <td>Остановы&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/ZReport.aspx?t=3"></asp:HyperLink></td>
            <td>не закрытые&nbsp; <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Arc.aspx?t=3"></asp:HyperLink></td>
        </tr>   
        <tr>
            <td style="font-style: normal; font-family: 'Arial Black'">заявки от:&nbsp;&nbsp; </td>
            
            <td>Заказчика &nbsp;&nbsp; <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/ZReport.aspx?t=5"></asp:HyperLink></td>
            <td class="auto-style1">не закрытые<asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Arc.aspx?t=6"></asp:HyperLink></td>       
            
            <td>Менеджера <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/ZReport.aspx?t=7"></asp:HyperLink></td>
            <td>не закрытые&nbsp; <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/ZReport.aspx?t=8"></asp:HyperLink></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>Механика&nbsp;&nbsp;&nbsp; <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/ZReport.aspx?t=9"></asp:HyperLink></td>
            <td>не закрытые<asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/ZReport.aspx?t=10"></asp:HyperLink></td>        
           
            <td>ОДС планов.<asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/ZReport.aspx?t=11"></asp:HyperLink>&nbsp;</td>
            <td>внеплановые <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/ZReport.aspx?t=12"></asp:HyperLink>&nbsp;</td>
        </tr>
        </table>
    </asp:PlaceHolder>
    <br />
    <asp:Button ID="WorkerZayavka" runat="server" Text="Создание внутреннего События !" BackColor="Tomato" OnClick="WorkerZayavka_Click"/>    
    <br /><br />
    <asp:PlaceHolder ID="PhOutSms" runat="server" Visible ="false">
    <asp:Literal ID="SmsText" runat="server" Text="" Visible="false"></asp:Literal>
    <asp:Button ID="InSmsText" runat="server" Text="Чтение ответного СМС" OnClick="InSmsText_Click"/>
    <br />
 <!--   <asp:PlaceHolder ID="PhInSms" runat="server" Visible ="false">            
    <asp:Label ID="Label2" runat="server" Text="Входящие SMS за текущие сутки:"></asp:Label><br />
    &nbsp;<asp:Button ID="Button1" runat="server" Text="Проверить" OnClick="InSms_Click" BackColor="#6699ff"/>
        </asp:PlaceHolder>    -->
    Ответные sms за текущие сутки:<br /> <asp:Label ID="SmsCreat" runat="server" Text="..."></asp:Label>&nbsp;
  <!--  <asp:Label ID="SmsSend" runat="server" Text="."></asp:Label>&nbsp;
    <asp:Label ID="SmsText2" runat="server" Text="."></asp:Label> -->          
      <h4>  Отправка СМС </h4>
    Введите номер:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp; формат ввода: (например: 89261231212) <br />
    Текст сообщения:&nbsp;<asp:TextBox ID="TextBox2" runat="server" Width="400px"></asp:TextBox><br />
    длина одного SMS - 160 символов (En), 70 (Ru).<br />
    &nbsp;<asp:Button ID="Button2" runat="server" Text="Отправить"  OnClick="OutSms_Click"  BackColor="#00cc00"/><br />
        <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
    </asp:PlaceHolder>
    <asp:ListView ID="Lifts" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                    <td id="Td1" runat="server">участок/маршрут</td>
                    <td id="Td2" runat="server">НР</td>
                    <td id="Th2" runat="server">простой</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:Label ID="Title" runat="server" Text='<%# Eval("IdUM") %>' />
                </td>
                <td>
                    <asp:Label ID="HP" runat="server" Text='<%# Eval("Working") %>' />
                </td>
                <td>
                    <asp:HyperLink ID="Stopped" runat="server" NavigateUrl='<%# Eval("Url") %>' Text='<%# Eval("Stopped") %>'></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView>
   &nbsp;
    <h4>Замечания по лифтам</h4>
    <asp:ListView ID="LiftPrim" runat="server">
        <LayoutTemplate>
            <table border="1" id="tbl1" runat="server">
                <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                    <td id="Td1" runat="server">участок/маршрут</td>
                    <td id="Td2" runat="server">кол-во</td>
                </tr>
                <tr runat="server" id="itemPlaceholder" />
            </table>
        </LayoutTemplate>
        <ItemTemplate>
            <tr id="Tr1" runat="server">
                <td>
                    <asp:Label ID="Title" runat="server" Text='<%# Eval("IdUM") %>' />
                </td>
                <td>
                    <asp:HyperLink ID="N" runat="server" NavigateUrl='<%# Eval("Url") %>' Text='<%# Eval("N") %>'></asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
    </asp:ListView> 
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent"> 
    </asp:Content>

