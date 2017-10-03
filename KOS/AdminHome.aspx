<%@ Page Title="Домашняя страница Менеджера" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminHome.aspx.cs" Inherits="KOS.AdminHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>    
<%-- сюда вставляем скрипт--%>
    <style type="text/css">
        .auto-style1 {
            width: 290px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Интегратор Событий</h3>
    <asp:PlaceHolder ID="phWebPhone" runat="server" Visible ="false">
     <script src="https://api.yandex.mightycall.ru/api/v1/sdk/mightycall.webphone.sdk.js">
</script>
<script type="text/javascript">
    var mcConfig = { login: "f98f97c4-d153-4105-829c-bfdf0a9042f9", password: "100" };
    MightyCallWebPhone.ApplyConfig(mcConfig);

    MightyCallWebPhone.Phone.Init();
</script> 
<script type="text/javascript">
    MightyCallWebPhone.Phone.Call('+79264062614'); 
    MightyCallWebPhone.Phone.Focus();
</script> </asp:PlaceHolder> 

    <table class="tableAdminhome">
        <tr>
<td style="padding-left:20px; border:none;" class="auto-style1" ><br />
     <table border="1" id="Table1" runat="server">
         <tr id="Tr17" runat="server" style="background-color: #ffffff">
        <td colspan="2" style="text-align: center;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ОДС Эмика (МОНИТОРИНГ)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="WebPhone_Click" Text="WebPhone" ImageUrl="~/Images/telephone2.png" Height="50" Width="50"/> <br />   
            </td>
                </tr>
        <tr id="Tr18" runat="server" style="background-color: #ffffff">
            <td style="text-align: center;">В нормальной работе</td>            
            <td><asp:Label ID="Worked" runat="server"  style="color: #306b19; text-align: center;" ></asp:Label></td>
        </tr>
          <tr id="Tr19" runat="server" style="background-color: #ffffff">
           <td style="text-align: center;">ПРОСТОЙ</td>
           <td onclick="clickIt('Arc.aspx?t=5')"><asp:Label ID="Stopped" runat="server" style="color: #FF0000; text-align: center;"></asp:Label></td>
        </tr>
         <tr id="Tr20" runat="server" style="background-color: #ffffff">
          <td style="text-align: center;">admin (Замечания)</td>
          <td><asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/Arc.aspx?t=1" CssClass="float-center">[HyperLink1]</asp:HyperLink></td>
        </tr>
          <tr>
                        <td colspan="2">
                        <asp:Button ID="Lifts1"  runat="server" Text="РЕГИСТРАЦИЯ ЗАЯВОК" OnClick="Lifts1_Click" Width="290px"  BackColor="#CC0000" ForeColor="White" Height="30px" />
                  
                  <asp:Button ID="ArcButton"  runat="server" Text="АРХИВ (заявки ОДС Эмика)" OnClick="ArcButton_Click" Width="290px"  BackColor="#339966" ForeColor="White" Height="30px" />
                            </td>                  
                    	</tr>
          <tr id="Tr21" runat="server" style="background-color: #e2e9f6">
           <td colspan="2" style="text-align: center;"><asp:HyperLink ID="HyperLink30" runat="server" Text ="Интерфейс Manager" NavigateUrl="~/ManagerHome.aspx"></asp:HyperLink></td>
        </tr>
         <tr id="Tr22" runat="server" style="background-color: #e2e9f6">
           <td colspan="2" style="text-align: center;"><asp:Button ID="Lifts" runat="server" Text="ГРАФИКИ" OnClick="Lifts_Click"  Width="290px" BackColor="#CC6699" ForeColor="White" Height="30px" /></td>
        </tr>
        <tr id="Tr23" runat="server" style="background-color: #e2e9f6">
            <td colspan="2" style="text-align: center;">
             <asp:Button ID="WorkerZayavka" runat="server" Text="Заявка менеджеру (внутренняя)" OnClick="WorkerZayavka_Click"  Width="290px" BackColor="Tomato" ForeColor="White" Height="30px" /></td>
        </tr>
              
    </table>
       
            <td style="padding-left:20px; border:none;" class="auto-style1" ><br />
     <table border="1" id="tbl1" runat="server">
         <tr id="Tr2" runat="server" style="background-color: #ffffff">
                    <td id="Td1" runat="server">&nbsp;Этапы СОБЫТИЙ:</td>
                    <td id="Td2" runat="server">&nbsp;всего:</td>
                    <td id="Th2" runat="server">&nbsp;сроч:</td>
                </tr>
        <tr id="Tr3" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink1" runat="server" Text ="Активные События" NavigateUrl="~/Reg_wz.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Reg_wz.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink3" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
          <tr id="Tr4" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink4" runat="server" Text ="Запрос менеджеру" NavigateUrl="~/ZaprosMng.aspx"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink5" runat="server"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink6" runat="server" style="color: #FF0000; text-align: center;"></asp:HyperLink></td>
        </tr>
         <tr id="Tr5" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink7" runat="server" Text ="Запрос КП" NavigateUrl="~/ReportsTSG.aspx?t=2"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink8" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink9" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr6" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink10" runat="server" Text ="Анализ КП " NavigateUrl="~/ReportsTSG.aspx?t=3"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink11" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink12" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr7" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink13" runat="server" Text ="Ожидание счета на оплату" NavigateUrl="~/ReportsTSG.aspx?t=4"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink14" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink15" runat="server" style="color: #FF0000; text-align: center;"></asp:HyperLink></td>
        </tr>
        <tr id="Tr8" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink16" runat="server" Text ="Счета получены" NavigateUrl="~/ReportsTSG.aspx?t=5"></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink17" runat="server" ></asp:HyperLink></td>
            <td><asp:HyperLink ID="HyperLink18" runat="server" style="color: #FF0000; text-align: center;" ></asp:HyperLink></td>
        </tr>
         <tr id="Tr9" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink19" runat="server" Text ="Ожидание оплаты" NavigateUrl="~/ReportsTSG.aspx?t=6"></asp:HyperLink></td>
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
                <td><br>
              <td style="padding-left:1px; border:none;" class="auto-style1" ><br />  
                     <table border="1" id="tbl2" runat="server">
         <tr id="Tr1" runat="server" style="background-color: #98FB98">
                    <td id="Td3" runat="server">&nbsp;СПРАВОЧНАЯ информация</td>
                    
                </tr>
        <tr id="Tr10" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink22" runat="server" Text ="Лифты" NavigateUrl="~/BaseLifts.aspx"></asp:HyperLink></td>
        </tr>
                         <tr id="Tr24" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink32" runat="server" Text ="Администрирование участков" NavigateUrl="~/adminUM.aspx"></asp:HyperLink></td>
        </tr>
                          <tr id="Tr11" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink23" runat="server" Text ="Оборудование и запчасти для лифтов" NavigateUrl="~/Equipment.aspx"></asp:HyperLink></td>
        </tr>
                          <tr id="Tr12" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink24" runat="server" Text ="Инструменты и оборудование для ремонта" NavigateUrl="~/About.aspx?t=1"></asp:HyperLink></td>
        </tr>
                          <tr id="Tr13" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink25" runat="server" Text ="Справочная информация по лифтам" NavigateUrl="~/Lib.aspx"></asp:HyperLink></td>
        </tr>
                         <tr id="Tr14" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink26" runat="server" Text ="База документов по Событиям" NavigateUrl="~/BaseDoc.aspx"></asp:HyperLink></td>
        </tr>            
                         <tr id="Tr26" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink33" runat="server" Text ="База документов по маршрутам" NavigateUrl="~/BaseDocUM.aspx"></asp:HyperLink></td>
        </tr>
                           <tr id="Tr15" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink27" runat="server" Text ="Контрагенты" NavigateUrl="~/About.aspx?t=1"></asp:HyperLink></td>
        </tr>
                           <tr id="Tr16" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink28" runat="server" Text ="Контакты поставщиков" NavigateUrl="~/Suppliers.aspx?t=1"></asp:HyperLink></td>
        </tr>
                          
                           <tr id="Tr25" runat="server" style="background-color: #e2e9f6">
            <td><asp:HyperLink ID="HyperLink31" runat="server" Text ="Склады" NavigateUrl="~/Storage.aspx"></asp:HyperLink></td>
        </tr>
             </table> </tr>
                    </table> 
  
    </asp:Content>