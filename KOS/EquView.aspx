<%@Page Title="Перемещение/Списание запчастей на складе" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EquView.aspx.cs" Inherits="KOS.EquView" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br /><h4>Карточка запчасти/детали/узла:</h4>
    <br />
&nbsp;&nbsp;
    &nbsp;<br /><br />
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #9face9; color:#000;">

                        <td id="td1" runat="server" class="td5px">Дата пост.</td>
                        <td id="td10" runat="server" class="td5px">№ документа:</td>                        
                        <td id="td5" runat="server" class="td5px">№ склада:</td>
                        <td id="td6" runat="server" class="td5px">закрепление:</td>
                        <td id="td31" runat="server" class="td5px">принял:</td>
                        <td id="td7" runat="server" class="td5px">наименование:</td>
                        <td id="td4" runat="server" class="td5px">обозначение:</td>
                        <td id="td2" runat="server" class="td5px">ID номер:</td>
                                                  
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="DataPost" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:Literal ID="NumDoc" runat="server"></asp:Literal></td>                       
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="NumSklada" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="Zakreplen" runat="server"></asp:Literal></td>
                         <td id="td32" runat="server" class="td5px"><asp:Literal ID="Prinyal" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="Name" runat="server"></asp:Literal></td>
                         <td id="td30" runat="server" class="td5px"><asp:Literal ID="Obz" runat="server"></asp:Literal></td>
                        <td id="td8" runat="server" class="td5px"><asp:Literal ID="NumID" runat="server"></asp:Literal></td>
                                              
                        
                    </tr>
               </table><br /><br />
     <table id="Table1" runat="server" cellpadding="2">
                    <tr id="Tr3" runat="server" style="background-color: #9face9; color:#000;">
                        <td id="td3" runat="server" class="td5px">количество:</td> 
                        <td id="td9" runat="server" class="td5px">цена:</td>
                        <td id="td13" runat="server" class="td5px">источник:</td>                        
                        <td id="td14" runat="server" class="td5px">Дата списания:</td>
                        <td id="td15" runat="server" class="td5px">№ документа:</td>
                        <td id="td19" runat="server" class="td5px">количество*</td>
                        <td id="td20" runat="server" class="td5px">примечание:</td>
                        <td id="td21" runat="server" class="td5px">остаток*</td>                           
                        
                    </tr>
                     <tr id="Tr4" runat="server" style="background-color: #FFF; color:#000;">
                         <td id="td29" runat="server" class="td5px"><asp:Literal ID="TheNum" runat="server"></asp:Literal></td>  
                        <td id="td22" runat="server" class="td5px"><asp:TextBox ID="Price" runat="server" Height="41px" Width="65px"></asp:TextBox></td>
                        <td id="td23" runat="server" class="td5px"><asp:TextBox ID="Source" runat="server"  TextMode="MultiLine" Height="41px" Width="90px"></asp:TextBox></td>                       
                        <td id="td24" runat="server" class="td5px"><asp:TextBox ID="DataSpisaniya" runat="server" Height="41px" Width="67px"></asp:TextBox></td>
                        <td id="td25" runat="server" class="td5px"><asp:TextBox ID="NumDocSpisan" runat="server" Height="41px" Width="74px"></asp:TextBox></td>
                        <td id="td26" runat="server" class="td5px"><asp:TextBox ID="TheNumSpisan" runat="server" Height="41px" Width="65px"></asp:TextBox></td>
                        <td id="td27" runat="server" class="td5px"><asp:TextBox ID="PrimZ" runat="server"  TextMode="MultiLine" Height="41px" Width="190px"></asp:TextBox></td>
                        <td id="td28" runat="server" class="td5px"><asp:TextBox ID="Ostatok" runat="server" Height="41px" Width="55px"></asp:TextBox></td>                        
                        
                    </tr>
               </table>

    &nbsp;<asp:PlaceHolder ID="phSpisanie" runat="server" Visible="false">
        Для списания запчасти/детали/узла ввести <br />
        Пин-код:&nbsp;&nbsp; <asp:TextBox ID="PinSpis" runat="server"></asp:TextBox>
          </asp:PlaceHolder> 
    <br />
    Для списания перемещения внести данные в карточку (* - количество и остаток обязательно)<br />
    <asp:Button ID="Button1" runat="server" Text="Списать" OnClick="Button1_Click"/>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server" Text="Перемещение" OnClick="Button3_Click"/>
    <br />
      &nbsp;<asp:PlaceHolder ID="phPeremesh" runat="server" Visible="false">
          Если данные для перемещения (количество и остаток в карточке) внесены,<br />
         Выберите склад:&nbsp; <asp:DropDownList ID="NumSklad" runat="server"></asp:DropDownList>
         Закрепить за:&nbsp; <asp:DropDownList ID="WorkSklad" runat="server"></asp:DropDownList>         
         Нажать после выбора:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="Button2" runat="server" Text="Переместить"  OnClick="Button2_Click"/> 
            </asp:PlaceHolder> <br />   
      &nbsp;
    </asp:Content>