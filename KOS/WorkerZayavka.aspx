<%@ Page Title="Заявка" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WorkerZayavka.aspx.cs" Inherits="KOS.WorkerZayavka" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
   Участок: <asp:DropDownList ID="IdU" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="IdU_SelectedIndexChanged"></asp:DropDownList>
   Маршрут: <asp:DropDownList ID="IdM" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="IdM_SelectedIndexChanged"></asp:DropDownList>   
   Лифт:<asp:DropDownList ID="LiftId" runat="server" AutoPostBack="true" OnTextChanged ="LiftId_SelectedIndexChanged"></asp:DropDownList>
 <!--   Адрес: <asp:DropDownList ID="Address" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="Address_TextChanged"></asp:DropDownList> -->
  
    <br><br>
    Тип работ:<asp:DropDownList ID="Type" runat="server" AutoPostBack="true" OnTextChanged="PrimLift_TextChanged"></asp:DropDownList>
    <asp:PlaceHolder ID="PrimLift" runat="server" Visible="false">
    Срочность:<asp:DropDownList ID="Category" runat="server"></asp:DropDownList>
    Кому:<asp:DropDownList ID="To" runat="server"></asp:DropDownList></asp:PlaceHolder>
    <br><br> 
    Описание характера работ или неисправности<br />
    <asp:TextBox ID="Text" runat="server" Height="41px" Width="511px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox><br />
    Наименование запчасти/оборуд.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Обозначение&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         IDномер&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; кол-во<br />
    <asp:TextBox ID="Text1" runat="server" Height="41px" Width="225px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="TextBox1" runat="server" Height="41px" Width="122px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text2" runat="server" Height="41px" Width="95px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text3" runat="server" Height="41px" Width="52px" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
        <br />
   Выбор файла для записи фотографий в базу: <br />
   <asp:FileUpload id="FileUpload" runat="server">
    </asp:FileUpload>       
    <br /><asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
    <asp:Button ID="Save" runat="server" Text="Зарегистрировать заявку" OnClick="Save_Click" BackColor="#CC3300" ForeColor="White" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" Text="На главную" OnClick="Button1_Click" BackColor="Silver" />
    <br />Для ввода в заявку запчастей и фото больше одного наименования наберите их 
    в соответствующих <br />полях и нажмите кнопку "Добавить запчасти" (при отсутствии № заявки - не нажимать!)<br />
    Запчасти:
        <table class="table" border ="1">
            <tr>
    <td>наименование:</td>
    <td>обозначение:</td>
    <td>ID номер:</td>
    <td>количество:</td><td></td>        
 </tr>
  <tr>
    <td><asp:Literal ID="Name1" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
      <td><asp:Literal ID="Obz1" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID1" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol1" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td>        
 </tr>
 <tr>
    <td><asp:Literal ID="Name2" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz2" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID2" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol2" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
 <tr>
    <td><asp:Literal ID="Name3" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz3" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID3" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol3" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
  <tr>
    <td><asp:Literal ID="Name4" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz4" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID4" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol4" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
  <tr>
    <td><asp:Literal ID="Name5" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Obz5" runat="server" meta:resourcekey="Name1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="ID5" runat="server" meta:resourcekey="ID1Resource1"></asp:Literal></td>
    <td><asp:Literal ID="Kol5" runat="server" meta:resourcekey="Kol1Resource1"></asp:Literal><td> </tr>
            </table>
    № текущей заявки:<br />
     <asp:TextBox ID="NumEvent" runat="server" Width="104px" ></asp:TextBox>&nbsp;&nbsp;
        <asp:Button ID="Parts" runat="server" Text="Добавить запчасти" OnClick="Parts_Click" BackColor="#99FFCC" />
    <br />
    Выбрать название акта из списка:
    <asp:DropDownList ID="TypeAkt" runat="server">
    </asp:DropDownList>
&nbsp;<br />
    Ввести данные в поля ниже и записать акт в БД:<br />
    Работы по ремонту/ замене требуют более 1 чел. :<asp:DropDownList ID="Chel" runat="server">
    </asp:DropDownList>
    <br />
    Работы по ремонту/ замене требуют более 4 час. :<asp:DropDownList ID="Chas" runat="server">
    </asp:DropDownList>
    <br />
    Предст. заказчика: (должн./ФИО)&nbsp; Составил: <asp:Label ID="Label1" runat="server" ></asp:Label> <br />
    <asp:TextBox ID="FamZak" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;<asp:PlaceHolder ID="phPinClose" runat="server" >
            Для подписи актов введите свой пин-код:
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder>
    &nbsp;<br />
    <br />
    &nbsp;&nbsp;&nbsp;
   <!--  Примечание:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Состав бригады: (дожн./ФИО, если была)<br />    
    <asp:TextBox ID="Prm" runat="server" TextMode="MultiLine"  Width="184px" ></asp:TextBox>
    <asp:TextBox ID="Work1" runat="server" TextMode="MultiLine"  Width="184px" ></asp:TextBox>
    <br />
    Для акта неисправности обязательно внести данные в поля:<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Выявлено:
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Заключение:
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Меры по устранению:<br />
    <asp:TextBox ID="Neis" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox><asp:TextBox ID="Zakl" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox>
    <asp:TextBox ID="Ustr" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox><br /> -->
     <asp:Button ID="Akt" runat="server" Text="Сформировать акт" OnClick="Akt_Click" BackColor="#99CCFF" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server" Text="На главную" OnClick="Button1_Click" BackColor="Silver" />
    <br />
    <br />

</asp:Content>
