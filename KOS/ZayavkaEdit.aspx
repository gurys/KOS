<%@ Page Title="Обработка заявки ОДС " Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZayavkaEdit.aspx.cs" Inherits="KOS.ZayavkaPrinyal" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<h4>Обработка События</h4>
    <table class="table" border ="1">
        <tr><td class="tdwhite">&nbsp;Событие:</td><td class="tdwhite"><asp:Literal ID="Category" runat="server"></asp:Literal></td><td class="tdwhite"><asp:Label ID="NumEvent" runat="server"></asp:Label></td></tr>
        <tr><td class="tdwhite">&nbsp;Адрес:</td><td class="tdwhite"></td><td class="tdwhite"><asp:Literal ID="Address" runat="server"></asp:Literal></td></tr>
        <tr><td class="tdwhite">&nbsp;№ лифта:&nbsp;</td><td class="tdwhite"></td><td class="tdwhite"><asp:Literal ID="Lift" runat="server"></asp:Literal></td></tr>
        <tr><td class="tdwhite">&nbsp;Статус:</td><td class="tdwhite"></td><td class="tdwhite"><asp:Literal ID="Status" runat="server"></asp:Literal></td></tr>
        <tr><td class="tdwhite">&nbsp;Простой:</td><td class="tdwhite"><td class="tdwhite"><asp:Literal ID="Prostoy" runat="server"></asp:Literal></td></tr>
        <tr><td class="tdwhite"><asp:LinkButton ID="Button5" runat="server" Text="Принял:" OnClick="DonePrn_Click" /></td><td class="tdwhite"><asp:Literal ID="Prinyal" runat="server"></asp:Literal></td><td class="tdwhite"><asp:Literal ID="StartPrinyal" runat="server"></asp:Literal></td></tr>
                </table>
    <br /><asp:Literal ID="Disp" runat="server" Visible="false"></asp:Literal>
    <table class="table" border ="1">
            <tr><td class="tdwhite">Описание:&nbsp;</td><td class="tdwhite"><asp:Label ID="Text" runat="server" ForeColor="Blue"></asp:Label></td></tr>
        <tr><td class="tdwhite">Комментарий:&nbsp;</td><td class="tdwhite"><asp:Label ID="Comm" runat="server" ForeColor="Blue"></asp:Label></td></tr>
                </table>
    <asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                                <td id="td3" runat="server" class="td5px">дата:</td>
                                <td id="td1" runat="server" class="td5px">автор:</td>                                                               
                                <td id="td5" runat="server" class="td5px">описание:</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style ="background-color: #e1ebf2; color: #060f3d;">                           
                            <td class="tdwhite" >                                
                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td class="tdwhite" > 
                                <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                                </td>
                            <td class="tdwhite" >
                            <asp:Label ID="Text" runat="server" Text='<%# Eval("Text") %>' />
                                </td>
                        </tr></ItemTemplate></asp:ListView><br />
 <!--   <asp:Button ID="Save" runat="server" Text="Принял" OnClick="DonePrn_Click" /> -->
    <br />
    <asp:Label ID="Msg" runat="server" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
        <br />
    <asp:PlaceHolder ID="phPoleVvoda" runat="server" Visible="false">
    Поле ввода информации:<br />
    <asp:TextBox ID="Text1" runat="server"  Height="31px" Width="545px" BackColor="#ffccff" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
        </asp:PlaceHolder>
    <br />
   <asp:PlaceHolder ID="phCloseEvent" runat="server" Visible="false">
       <asp:Button ID="Button4" runat="server" Visible="false" Text="Закрыть Событие" Onclick ="Done_Click" Height="30px" Width="250px" BackColor="Red" ForeColor="#FFFFFF"/><br /> 
       </asp:PlaceHolder>
    <asp:PlaceHolder ID="phAction" runat="server">
       <asp:Button ID="OpisNi" runat="server" Text="Ввод Комментария" Onclick="OpisNi_Click" Height="30px" Width="250px" BackColor="OliveDrab" ForeColor="#FFFFFF"/>
    <!--   <asp:Button ID="DopMex" runat="server" Text="Дополнительный механик" OnClick="DopMex_Click" Height="30px" Width="250px" BackColor="Blue" ForeColor="#FFFFFF"  /><br /> 
       <asp:Button ID="ZakZap" runat="server" Text="Заказать запчасти" OnClick="ZakZap_Click" Height="30px" Width="250px" BackColor="#85a6d3" ForeColor="#000000"  /><br />
       <asp:Button ID="ReoPnr" runat="server" Text="ПНР/РЭО" OnClick="ReoPnr_Click" Height="30px" Width="250px" BackColor="#9555CC" ForeColor="#FFFFFF"  /><br /> 
       <asp:Button ID="DopZap1" runat="server" Text="Добавить запчасти в акты" OnClick="DopZap_Click" Height="30px" Width="250px" BackColor="#99FFCC" ForeColor="#000000"  /><br />      
       <asp:Button ID="FormAkt" runat="server" Text="Сформировать акты" OnClick="FormAkt_Click" Height="30px" Width="250px" BackColor="Silver" ForeColor="#000000"  /><br />   -->
        </asp:PlaceHolder>
    <br />
        <asp:Button ID="ChangeCat" runat="server" Text="Перевести в останов" Onclick="ChangeCat_Click" Height="30px" Width="250px" BackColor="Thistle" ForeColor="Blue" Visible="false"/> <br />
        <asp:Button ID="ZkEv" runat="server" Visible="true" Text="Закрыть Событие" Onclick ="ZkEv_Click" Height="30px" Width="250px" BackColor="Thistle" ForeColor="Red"/>

    <asp:PlaceHolder ID="phPinClose" runat="server" >
            ПИН-код:
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder>
    <asp:PlaceHolder ID="phPodp" runat="server" Visible ="false">   
    Ваша подпись: &nbsp; <asp:Label ID="Label1" runat="server" ForeColor="Green"></asp:Label><br />
        </asp:PlaceHolder>
    <asp:PlaceHolder ID="phCheckBox" runat="server" Visible="false">
        <br />
        Для устранения неисправности требуется:
         <table class="table" border ="0">
             <tr><td><asp:CheckBox ID="CheckBox2" runat="server" /></td><td> - более 4 часов</td><td><asp:CheckBox ID="CheckBox1" runat="server" /></td><td> - нарушение правил эксплуатации</td></tr>
             <tr><td><asp:CheckBox ID="CheckBox3" runat="server" /></td><td> - более 1 человека</td></tr>
             <tr><td><asp:CheckBox ID="CheckBox4" runat="server" /></td><td> - требуются запчасти</td></tr>
             <tr><td><asp:CheckBox ID="CheckBox5" runat="server" /></td><td> - ПНР/РЭО</td></tr>
        <!--     <tr><td><asp:CheckBox ID="CheckBox6" runat="server" /></td><td> - нарушение правил эксплуфтации</td></tr> -->
                </table>
        <asp:Button ID="Button7" runat="server" Text="Сформировать документ"  OnClick="Button7_Click"/><br />
    </asp:PlaceHolder><br />
    <asp:PlaceHolder ID="ActZNa" runat="server" Visible ="false">
        <asp:Button ID="Button6" runat="server" OnClick="Button6_Click" Text="Активировать у механика" Height="30px" Width="255px" BackColor="PapayaWhip" ForeColor="#000000" /> <br />
        <asp:Button ID="Vnr" runat="server" Text="Перейти к планированию" OnClick="Vnr_Click" Height="30px" Width="255px" BackColor="#ed9696" ForeColor="#FFFFFF"  /><br />
    </asp:PlaceHolder>       
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
    <td><asp:Literal ID="Name" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz" runat="server" meta:resourcekey="Obz1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID0" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td>        
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
            <tr></tr>
        </table>
         <asp:Button ID="Button2" runat="server" Text="Заказать запчасти из таблицы менеджеру" OnClick="ZakParts_Click" Height="30px" BackColor="#99FFCC" />
    </asp:PlaceHolder>  <br />
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
     Требуется ПНР/РЭО :&nbsp;<asp:DropDownList ID="Pnr" runat="server">
    </asp:DropDownList>
    <br />
    Предст. заказчика: (должн./ФИО) 
    <asp:TextBox ID="FamZak" runat="server" TextMode="MultiLine" Height="30px" Width="300px" ></asp:TextBox><br />        
 <!--  <asp:TextBox ID="Prm" runat="server" TextMode="MultiLine"  Width="184px" ></asp:TextBox>
    <asp:TextBox ID="Work1" runat="server" TextMode="MultiLine"  Width="184px" ></asp:TextBox>
    <br />
    Для акта неисправности обязательно внести данные в поля:<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Выявлено:
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Заключение:
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Меры по устранению:<br />
    <asp:TextBox ID="Neis" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox><asp:TextBox ID="Zakl" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox>
    <asp:TextBox ID="Ustr" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox> -->
    <br />
        При формировании актов используются запчасти из таблицы: <br />   
     <asp:Button ID="Akt" runat="server" Text="Запись в базу и просмотр акта" OnClick="Akt_Click" BackColor="#99CCFF" /></asp:PlaceHolder>
    <br /> <br /> 
    &nbsp;&nbsp;&nbsp;&nbsp;
  <!--  <asp:Button ID="Button1" runat="server" Text="Спрятать окна ввода" OnClick="Button1_Click" BackColor="#fff999" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="На главную" OnClick="Button3_Click" BackColor="Silver" /> -->
    <br /><br />
    </asp:Content>
