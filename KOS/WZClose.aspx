<%@ Page Title="Обработка внутренних событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WZClose.aspx.cs" Inherits="KOS.WZClose" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h4>Обработка События:&nbsp; &nbsp;</h4>
    <asp:PlaceHolder ID="phLift" runat="server">
        <table class="table" border ="1">
            <tr><td class="tdwhite">Событие:&nbsp;</td><td class="tdwhite"><asp:Label ID="Type" runat="server" /></td><td class="tdwhite"><asp:Label ID="NumEvent" runat="server" /></td></tr>
            <tr><td class="tdwhite">Адрес:&nbsp;</td><td class="tdwhite"></td><td class="tdwhite"><asp:Label ID="Address" runat="server" /></td></tr>
            <tr><td class="tdwhite">№ лифта:&nbsp;</td><td class="tdwhite"></td><td class="tdwhite"><asp:Label ID="LiftId" runat="server" /></td></tr>
            <tr><td class="tdwhite">Статус:&nbsp;</td><td class="tdwhite"></td><td class="tdwhite"><asp:Label ID="Status" runat="server" /></td></tr>
            <tr><td class="tdwhite">Простой:&nbsp;</td><td class="tdwhite"></td><td class="tdwhite"><asp:Label ID="Prostoy" runat="server" /></td></tr>
            <tr><td class="tdwhite">Создал:&nbsp;</td><td class="tdwhite"><asp:Label ID="From" runat="server" /></td><td class="tdwhite"><asp:Label ID="DateFrom" runat="server" /></td></tr>
           <tr></tr>
                </table>
    </asp:PlaceHolder>
    <br />
    <table class="table" border ="1">
            <tr><td class="tdwhite">Описание:&nbsp;</td><td class="tdwhite"><asp:Label ID="Text" runat="server" ForeColor="Blue"></asp:Label></td></tr>
        <tr><td class="tdwhite">Комментарий:&nbsp;</td><td class="tdwhite"><asp:Label ID="Comm" runat="server" ForeColor="Blue"></asp:Label></td></tr>
                </table>
    <br />
    <asp:PlaceHolder ID="Zp" runat="server"  Visible="false">
        Запчасти:
        <table class="table" border ="1">
    <tr>
    <td>наименование:</td>
    <td>обозначение:</td>
    <td>ID номер:</td>
    <td>количество:</td> 
    <td>имя файла:</td><td></td>       
 </tr><tr>
     <td><asp:Label ID="Name" runat="server" /></td>
     <td><asp:Label ID="Obz" runat="server" /></td>
     <td><asp:Label ID="NumID" runat="server" /></td>
     <td><asp:Label ID="Kol" runat="server" /></td>  
     <td><asp:Label ID="namefoto" runat="server" /></td><td></td> 
    </tr><tr><td></td><td></td><td></td><td></td><td></td></tr>
         </table>
    <asp:Button ID = "Foto" runat="server" Text="просмотр фото" OnClick="Foto_Click" Height="30px" />        
    </asp:PlaceHolder>
    <br />
    Поле ввода информации:<br />
    <asp:TextBox ID="Text1" runat="server" Height="31px" Width="545px" BackColor="#ffccff" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
      <br />  
    <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
     <br />
   <asp:PlaceHolder ID="phPinClose" runat="server">
            Введите свой пин-код:
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder>
    <asp:PlaceHolder ID="phPodp" runat="server" Visible ="false">   
    Ваша подпись: &nbsp; <asp:Label ID="Label1" runat="server" ForeColor="Green"></asp:Label><br />
        </asp:PlaceHolder>
    <asp:PlaceHolder ID="CloseEv" runat="server" Visible ="false">
        
        Внимание! Действия по обработке события с введенным пин-кодом:
    </asp:PlaceHolder>
    <br />
     <asp:PlaceHolder ID="phCloseEvent" runat="server" Visible="false">
       <asp:Button ID="Button4" runat="server" Visible="false" Text="Закрыть Событие" Onclick ="Done_Click" Height="30px" Width="250px" BackColor="Red" ForeColor="#FFFFFF"/><br /> 
       </asp:PlaceHolder> 
    <asp:PlaceHolder ID="ActionWZ" runat="server" Visible ="true">   
     <table class="table" border ="1">
            <tr><td>
     <asp:Button ID="OpisNi" runat="server" Text="Описание неисправности" Onclick="OpisNi_Click" Height="30px" Width="250px" BackColor="OliveDrab" ForeColor="#FFFFFF"/><br />  
     <asp:Button ID="DopMex" runat="server" Text="Дополнительный механик" OnClick="DopMex_Click" Height="30px" Width="250px" BackColor="Blue" ForeColor="#FFFFFF"  /><br /> 
     <asp:Button ID="ZakZap" runat="server" Text="Заказать запчасти" OnClick="ZakZap_Click" Height="30px" Width="250px" BackColor="#85a6d3" ForeColor="#000000"  /><br /> 
     <asp:Button ID="ReoPnr" runat="server" Text="ПНР/РЭО" OnClick="ReoPnr_Click" Height="30px" Width="250px" BackColor="#9555CC" ForeColor="#FFFFFF"  /><br /> 
    <!--   <asp:Button ID="DopZap1" runat="server" Text="Добавить запчасти в акты" OnClick="DopZap_Click" Height="30px" Width="250px" BackColor="#99FFCC" ForeColor="#000000"  /><br /> -->       
       <asp:Button ID="FormAkt" runat="server" Text="Сформировать акты" OnClick="FormAkt_Click" Height="30px" Width="250px" BackColor="Silver" ForeColor="#000000"  /><br />      
     </td><td></td>
            </tr>
                </table> </asp:PlaceHolder> 
    <asp:PlaceHolder ID="ActWZNa" runat="server" Visible ="false">
        <asp:Button ID="Vnr" runat="server" Text="Внеплановый ремонт" OnClick="Vnr_Click" Height="30px" Width="255px" BackColor="#ed9696" ForeColor="#FFFFFF"  /><br />
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Активировать у механика" Height="30px" Width="255px" BackColor="PapayaWhip" ForeColor="#000000"/>
    </asp:PlaceHolder> <br />       
    <asp:PlaceHolder ID="phDopZap" runat="server" Visible ="false">
       Наименование запчасти/оборуд.&nbsp;&nbsp;&nbsp;&nbsp;
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       Каталог №&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       ID номер&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
       Кол.<br />
    <asp:TextBox ID="Text4" runat="server" Height="30px" Width="225" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text7" runat="server" Height="30px" Width="115px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text2" runat="server" Height="30px" Width="95px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text3" runat="server" Height="30px" Width="52px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:Button ID="Parts" runat="server" Text="+" OnClick="Parts_Click" BackColor="#ccff99" Height="45px" Width="45px" />
        <br />прикрепить фото:
        <asp:FileUpload id="FileUpload" runat="server">
    </asp:FileUpload>
        <br />   
               Запчасти события:
        <table class="table" border ="1">
            <tr>
    <td>наименование:</td>
    <td>обозначение:</td>
    <td>ID номер:</td>
    <td>количество:</td><td></td>        
 </tr>
  <tr>
    <td><asp:Literal ID="Name1" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz1" runat="server" meta:resourcekey="Obz1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID1" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol1" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td>        
 </tr>
 <tr>
    <td><asp:Literal ID="Name2" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz2" runat="server" meta:resourcekey="Obz2Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID2" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol2" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
 <tr>
    <td><asp:Literal ID="Name3" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz3" runat="server" meta:resourcekey="Obz3Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID3" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol3" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
  <tr>
    <td><asp:Literal ID="Name4" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz4" runat="server" meta:resourcekey="Obz4Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID4" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol4" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
            <tr>
    <td><asp:Literal ID="Name5" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz5" runat="server" meta:resourcekey="Obz4Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID5" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol5" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
            <tr></tr>
        </table>
         <asp:Button ID="Button2" runat="server" Text="Заказать запчасти из таблицы менеджеру" OnClick="ZakParts_Click" Height="30px" BackColor="#99FFCC" />
    </asp:PlaceHolder>     
    <br />
    <asp:PlaceHolder ID="phAktZap" runat="server" Visible ="false">
        Выбрать название акта из списка:&nbsp; <asp:DropDownList ID="VidAkta" runat="server"></asp:DropDownList><br />
        Ввести данные в поля ниже и записать акт в БД:<br />
         Работы по ремонту/ замене требуют более 1 чел. :&nbsp;<asp:DropDownList ID="Chel" runat="server"></asp:DropDownList>
        &nbsp; Или (этот же список) для :<br />
        Актов установки и выполнения внеплановых работ деталь/узел сдан Владельцу? &nbsp;
    <br />
    Работы по ремонту/ замене требуют более 4 час. :&nbsp;<asp:DropDownList ID="Chas" runat="server">
    </asp:DropDownList>
    <br />
    Предст. заказчика: (должн./ФИО) 
    <asp:TextBox ID="FamZak" runat="server" TextMode="MultiLine" Height="30px" Width="190px" Columns="50" Rows="10"></asp:TextBox><br />        
  <br />
   При формировании актов используются запчасти из таблицы:    <br /> 
     <asp:Button ID="Akt" runat="server" Text="Запись в базу и просмотр акта" OnClick="Akt_Click" BackColor="#99CCFF" /></asp:PlaceHolder>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
  <!--  <asp:Button ID="Button1" runat="server" Text="Спрятать окна ввода" OnClick="Button1_Click" BackColor="#fff999" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="На главную" OnClick="Button3_Click" BackColor="Silver" /> -->
    <br /><br />
     <!--   <asp:Button ID="Save" runat="server" Text="Принято" OnClick="Save_Click" />
    <asp:Button ID="Done" runat="server" Text="Выполнено" OnClick="Done_Click" /> -->
    <asp:Button ID="ZkEv" runat="server" Visible="true" Text="Закрыть Событие" Onclick ="ZkEv_Click" Height="30px" Width="250px" BackColor="Thistle" ForeColor="Red"/>
</asp:Content>
