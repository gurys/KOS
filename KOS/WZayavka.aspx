 <%@ Page Title="Заявка механика" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WZayavka.aspx.cs" Inherits="KOS.WZayavka" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label runat="server" ID="Msg0" ForeColor="Blue" meta:resourcekey="MsgResource1"></asp:Label><br />
   <asp:PlaceHolder ID="phLift" runat="server" Visible ="false">   
   Адрес: <asp:DropDownList ID="Address" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="Address_TextChanged" BackColor="#ffffcc"></asp:DropDownList>
       <asp:Label ID="LiftVs" runat="server" Text="Лифт:"></asp:Label><asp:DropDownList ID="LiftId" runat="server"  BackColor="#ffccff" ForeColor="#000066"></asp:DropDownList> 
  </asp:PlaceHolder> 
    <asp:PlaceHolder ID="phIdUM" runat="server" Visible ="false">
   Склад №: <asp:Label ID="UM" runat="server"></asp:Label> 
  </asp:PlaceHolder><br />
    <asp:Button ID="ZamLift" runat="server" Text="Замечания по лифтам" Width="350px" BackColor="#993399" ForeColor="White" OnClick="ZamLift_Click"/><br />
    <asp:Button ID="ZapLift" runat="server" Text="Заявка на запчасти и материалы" Width="350px" BackColor="#339933" OnClick="ZapLift_Click"/><br />
    <asp:Button ID="Instrum" runat="server" Text="Заявка на инструменты и оборудование" Width="350px" BackColor="#99cc00" OnClick="Instrum_Click"/><br />
   <asp:DropDownList ID="Type" runat="server" AutoPostBack="true" OnTextChanged="PrimLift_TextChanged" Visible="false"></asp:DropDownList>
   <asp:PlaceHolder ID="PrimLift" runat="server" Visible="false">
    <asp:DropDownList ID="Category" runat="server" ></asp:DropDownList>
    <asp:DropDownList ID="To" runat="server"></asp:DropDownList>
   </asp:PlaceHolder>
    <asp:Label ID="Op" runat="server" Text="Описание:" Visible="false"></asp:Label><br /> 
    <asp:TextBox ID="Text" runat="server" Height="30px" Width="492px" BackColor="#ffccff" TextMode="MultiLine" Columns="50" Rows="10" Visible="false"></asp:TextBox>
    <asp:PlaceHolder ID="phZip" runat="server" Visible="false">
     ЗИП: <asp:CheckBox ID="Zip" runat="server" /> 
        </asp:PlaceHolder><br /> 
    <asp:PlaceHolder ID="phVzap" runat="server" Visible="false">
    наименование запчасти/оборуд.:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        каталог №:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        ID номер:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; кол-во:<br />
    <asp:TextBox ID="Text1" runat="server" Height="30px" Width="225px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="105px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text2" runat="server" Height="30px" Width="95px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
    <asp:TextBox ID="Text3" runat="server" Height="30px" Width="52px" BackColor="#ccff99" TextMode="MultiLine" Columns="50" Rows="10"></asp:TextBox>
        <asp:Button ID="Parts" runat="server" Text="+" BackColor="#ccff99" Height="45px" Width="45px" OnClick="Parts_Click" />
        <br />
   Выбор файла для записи фотографий в базу: <br />
   <asp:FileUpload id="FileUpload" runat="server">
    </asp:FileUpload>
        </asp:PlaceHolder>
        <br />
    <asp:Label runat="server" ID="Label2" ForeColor="Green" meta:resourcekey="MsgResource1"></asp:Label><br />
    <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
    <asp:PlaceHolder ID="phVdopzap" runat="server" Visible="false">
    <br />
    Запчасти/Оборудование:
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
   <br />
     <asp:TextBox ID="NumEvent" runat="server" Width="104px" Visible="false"></asp:TextBox>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phReg" runat="server" Visible="false" >
    <asp:Button ID="SavePin" runat="server" Text="Зарегистрировать заявку" OnClick="SavePin_Click" BackColor="#CC3300" ForeColor="White" />
        </asp:PlaceHolder>
    <asp:PlaceHolder ID="phPinClose" runat="server"  Visible="false">
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder><br /> 
    <asp:PlaceHolder ID="phAkt" runat="server" Visible ="false">
    Выбрать название акта из списка:
    <asp:DropDownList ID="TypeAkt" runat="server">
    </asp:DropDownList>
<br />    
    Ввести данные в поля ниже и записать акт в БД:<br />
    Работы по ремонту/ замене требуют более 1 чел. :<asp:DropDownList ID="Chel" runat="server">
    </asp:DropDownList>
    <br />
    Работы по ремонту/ замене требуют более 4 час. :<asp:DropDownList ID="Chas" runat="server">
    </asp:DropDownList>
    <br />
    Предст. заказчика: (должн./ФИО)&nbsp;&nbsp; Составил: <asp:Label ID="Label1" runat="server" ></asp:Label> <br />
    <asp:TextBox ID="FamZak" runat="server" TextMode="MultiLine" Width="184px" ></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <br />
     <asp:Button ID="Akt" runat="server" Text="Сформировать акт" OnClick="Akt_Click" BackColor="#99CCFF" /> 
 </asp:PlaceHolder>
    <br />
    <br />
 </asp:Content>

