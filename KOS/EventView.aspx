<%@ Page Title="Редактирование События" Language="C#"  AutoEventWireup="true" CodeBehind="EventView.aspx.cs" Inherits="KOS.EventView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Редактирование События</title>
    <style type="text/css">


    .content-wrapper {
    margin: 0 auto;
    max-width: 960px;
}


    .content-wrapper {
        padding-right: 10px;
        padding-left: 10px;
    }

    .float-left {
    float: left;
}

    </style>
</head>
<body>

    <form id="form1" runat="server" style ="background-color:#FFF; font-family:'Segoe UI', Verdana, Helvetica, Sans-Serif; font-size: 13px;">
        <body onload="startTime()"><div id="txt" class="float-right"></div></body>
                        <asp:Label ID="Date" runat="server" CssClass="tddate"></asp:Label><br/>
                        <script type="text/javascript">
                            function startTime() {
                                var today = new Date();
                                var h = today.getHours();
                                var m = today.getMinutes();
                                var s = today.getSeconds();
                                // добавить нуль перед числами <10
                                m = checkTime(m);
                                s = checkTime(s);
                                document.getElementById('txt').innerHTML = "МСК - " + h + " : " + m + " : " + s + " , ";
                                t = setTimeout('startTime()', 500);
                            }
                            function checkTime(i) {
                                if (i < 10) {
                                    i = "0" + i;
                                }
                                return i;
                            }
                         </script>
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/" />
<br /><br />
    <asp:Label ID="Msg" runat="server" Text="" ForeColor="Red"></asp:Label>
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #85b9f8; color:#000;">

                        <td id="td1" runat="server" class="td5px">№:</td>
                        <td id="td10" runat="server" class="td5px">событие:</td>
                        <td id="td2" runat="server" class="td5px">дата/время:</td> 
                        <td id="td5" runat="server" class="td5px">лифт:</td>
                        <td id="td6" runat="server" class="td5px">источник:</td>
                        <td id="td7" runat="server" class="td5px">кто:</td>
                        <td id="td8" runat="server" class="td5px">тип:</td>
                        <td id="td3" runat="server" class="td5px">принял:</td>
                        <td id="td9" runat="server" class="td5px">дата:</td>
                        <td id="td20" runat="server" class="td5px">исполнил:</td>
                        <td id="td21" runat="server" class="td5px">дата:</td>
                        <td id="td26" runat="server" class="td5px" style="background-color: #f3a8a8; color:#000;">комментарий:</td>
                        <td id="td27" runat="server" class="td5px" style="background-color: #f3a8a8; color:#000;">замечания:</td>
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="Id" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:TextBox ID="EventId" runat="server" TextMode="MultiLine" Height="55px" Width="145px"></asp:TextBox></td>
                        <td id="td13" runat="server" class="td5px"><asp:TextBox ID="DataId" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td16" runat="server" class="td5px"><asp:TextBox ID="LiftId" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td17" runat="server" class="td5px"><asp:TextBox ID="Sourse" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td18" runat="server" class="td5px"><asp:TextBox ID="Family" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td19" runat="server" class="td5px"><asp:TextBox ID="TypeId" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td22" runat="server" class="td5px"><asp:TextBox ID="ToApp" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td23" runat="server" class="td5px"><asp:TextBox ID="DateToApp" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td24" runat="server" class="td5px"><asp:TextBox ID="Who" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                        <td id="td25" runat="server" class="td5px"><asp:TextBox ID="DateWho" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>
                          <td id="td37" runat="server" class="td5px"><asp:TextBox ID="Comment" runat="server" TextMode="MultiLine" Height="55px" Width="130px"></asp:TextBox></td>
                        <td id="td38" runat="server" class="td5px"><asp:TextBox ID="Prim" runat="server" TextMode="MultiLine" Height="55px" Width="130px"></asp:TextBox></td>
                        
                    </tr>
               </table>
        Индикатор/Переключатель состояния события:<br />
        
    <table id="Table2" runat="server" cellpadding="2">
                    <tr id="Tr5" runat="server" style="background-color: #de2edb; color:#fff;">

                        <td id="td28" runat="server" class="td5px">запрос мен.</td>
                        <td id="td35" runat="server" class="td5px"><asp:LinkButton ID ="ZapKP" runat ="server" OnClick ="ZapKP_Click" Text ="запрос КП"  BackColor="White"/></td>
                        <td id="td46" runat="server" class="td5px">ожидание</td> 
                        <td id="td47" runat="server" class="td5px">запрос счета</td>
                        <td id="td48" runat="server" class="td5px">получ. счета</td>
                        <td id="td49" runat="server" class="td5px">оплата счета</td>
                        <td id="td53" runat="server" class="td5px">доставка</td>
                        <td id="td54" runat="server" class="td5px">приход:</td>
                        <td id="td55" runat="server" class="td5px">перемещ.</td>
                        <td id="td56" runat="server" class="td5px">акт вып. р-т</td>
                        <td id="td57" runat="server" class="td5px">списание</td>
                        <td id="td69" runat="server" class="td5px">закрытие</td>
                        
                    </tr>
                     <tr id="Tr6" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td58" runat="server" class="td5px"><asp:CheckBox ID="ZaprosMng" runat="server" AutoPostBack="true" OnCheckedChanged="ZaprosMng_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td59" runat="server" class="td5px"><asp:CheckBox ID="ZaprosKP" runat="server"  AutoPostBack="true" OnCheckedChanged="ZaprosKP_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td60" runat="server" class="td5px"><asp:CheckBox ID="KP" runat="server" AutoPostBack="true" OnCheckedChanged="KP_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td61" runat="server" class="td5px"><asp:CheckBox ID="ZapBill" runat="server" AutoPostBack="true" OnCheckedChanged="ZapBill_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td62" runat="server" class="td5px"><asp:CheckBox ID="Bill" runat="server" AutoPostBack="true" OnCheckedChanged="Bill_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td63" runat="server" class="td5px"><asp:CheckBox ID="Payment" runat="server" AutoPostBack="true" OnCheckedChanged="Payment_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td64" runat="server" class="td5px"><asp:CheckBox ID="Dostavka" runat="server" AutoPostBack="true" OnCheckedChanged="Dostavka_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td65" runat="server" class="td5px"><asp:CheckBox ID="Prihod" runat="server" AutoPostBack="true" OnCheckedChanged="Prihod_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td66" runat="server" class="td5px"><asp:CheckBox ID="Peremeshenie" runat="server" AutoPostBack="true" OnCheckedChanged="Peremeshenie_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td67" runat="server" class="td5px"><asp:CheckBox ID="AktVR" runat="server" AutoPostBack="true" OnCheckedChanged="AktVR_CheckedChanged" TextAlign="Left"/></td>
                        <td id="td68" runat="server" class="td5px"><asp:CheckBox ID="Spisanie" runat="server" AutoPostBack="true" OnCheckedChanged="Spisanie_CheckedChanged" TextAlign="Left"/></td>
                         <td id="td70" runat="server" class="td5px"><asp:CheckBox ID="Cansel" runat="server" AutoPostBack="true" OnCheckedChanged="Cansel_CheckedChanged" TextAlign="Left"/></td>
                        
                    </tr>
               </table>

     <table id="Table1" runat="server" cellpadding="2">
                    <tr id="Tr3" runat="server" style="background-color: #c2bdbd; color:#000;">
                        
                        <td id="td32" runat="server" class="td5px"><asp:LinkButton ID ="Button3" runat ="server" OnClick = "ZayEdit_Click" Text ="обработчик" /></td> 
                        <td id="td29" runat="server" class="td5px"><asp:LinkButton ID ="Button4" runat ="server" OnClick = "Hist_Click" Text ="история" /></td>
                        <td id="td30" runat="server" class="td5px"><asp:LinkButton ID ="DocE" runat ="server" OnClick = "DocE_Click" Text ="документы" /></td>
                        <td id="td36" runat="server" class="td5px"><asp:LinkButton ID ="LinkButton1" runat ="server" OnClick = "PartList_Click" Text ="запчасти" /></td>
                        <td id="td31" runat="server" class="td5px"><asp:LinkButton ID ="PartList" runat ="server" OnClick = "PartList_Click" Text ="запчасти" /></td>
                        <td id="td33" runat="server" class="td5px">склад:</td>
                        <td id="td34" runat="server" class="td5px">тайминг:</td>
                    </tr>
                     <tr id="Tr4" runat="server" style="background-color: #a7cad7; color:#000;">
                       
                        <td id="td39" runat="server" class="td5px">События</td>
                        <td id="td40" runat="server" class="td5px">События</td>
                        <td id="td41" runat="server" class="td5px">События</td>
                        <td id="td42" runat="server" class="td5px"><asp:Literal ID="Literal1" runat="server" Text="для заказа" /></td>
                        <td id="td43" runat="server" class="td5px"><asp:Literal ID="Literal2" runat="server" Text="установленные" /></td>
                        <td id="td44" runat="server" class="td5px">Выбор запчастей</td>
                        <td id="td45" runat="server" class="td5px"><asp:Literal ID="Timing" runat="server"></asp:Literal></td>
                    </tr>
               </table>
    
    <asp:PlaceHolder ID="HistL" runat="server">
<asp:ListView ID="ListView2" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #f39369; color: #000;">
                               
                                <td id="td1" runat="server" class="td5px">дата</td>
                                <td id="td3" runat="server" class="td5px">описание</td>                                                               
                                <td id="td5" runat="server" class="td5px">категория</td>                               
                                <td id="td6" runat="server" class="td5px">кто</td>                                
                                <td id="td7" runat="server" class="td5px">кому</td>
                                <td id="td51" runat="server" class="td5px">комментарий</td>
                                <td id="td52" runat="server" class="td5px">примечание</td>
                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >
                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Text" runat="server" Text='<%# Eval("Text") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="Category" runat="server" Text='<%# Eval("Category") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="To" runat="server" Text='<%# Eval("To") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Comment" runat="server" Text='<%# Eval("Comment") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="PrimHist" runat="server" Text='<%# Eval("PrimHist") %>' />
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
    </asp:PlaceHolder>
  <asp:PlaceHolder ID="PartL" runat="server">
<asp:ListView ID="ListView1" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #6da176; color: #000;">
                               
                                <td id="td1" runat="server" class="td5px">№</td>
                                <td id="td3" runat="server" class="td5px">имя фото</td>                                                               
                                <td id="td5" runat="server" class="td5px">наименование</td>                               
                                <td id="td6" runat="server" class="td5px">ID номер</td>                                
                                <td id="td7" runat="server" class="td5px">количество</td>
                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >                                 
                                
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("namefoto") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Status" runat="server" Text='<%# Eval("NumId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Prim" runat="server" Text='<%# Eval("Kol") %>' />
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="DocEv" runat="server" Visible="false">
       <asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                               
                                <td id="td1" runat="server" class="td5px">№</td><td id="td3" runat="server" class="td5px">документ:</td><td id="td5" runat="server" class="td5px">имя файла:</td><td id="td6" runat="server" class="td5px">статус:</td><td id="td7" runat="server" class="td5px">примеч.</td><td id="td50" runat="server" class="td5px">от кого:</td></tr><tr runat="server" id="itemPlaceholder" /></table></LayoutTemplate><ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >                                 
                                
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("NameFile") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Prim" runat="server" Text='<%# Eval("Prim") %>' />
                            </td>
                             <td class="tdwhite" >
                                <asp:Label ID="Usr" runat="server" Text='<%# Eval("Usr") %>' />
                            </td>                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
        Добавить документ к событию:<br/>
        <asp:FileUpload ID="FileUpload1" runat="server" /><br/>
        Название документа:<br/>
        <asp:TextBox ID="TextBox2" runat="server" Text="документ"></asp:TextBox><asp:Button ID="AddDoc" runat="server" Text="Добавить!"  OnClick="AddDoc_Click"/>

    </asp:PlaceHolder><br/>
    <asp:Label ID="Idz" runat="server" Text="Zayavka" Visible="false"></asp:Label><asp:Label ID="Idwz" runat="server" Text="WZayavka" Visible="false"></asp:Label><br/>
<!--   <asp:Button ID ="Foto" runat ="server" OnClick = "Foto_Click" Text ="Просмотр фото" /> -->
    <asp:PlaceHolder ID="zKP" runat="server" Visible="false">
        Внесите данные для заказа запчасти:<br/>
        <asp:TextBox ID="Email2" runat="server" Text="Email"></asp:TextBox><asp:TextBox ID="Message" runat="server" Text="Здравствуйте! Вышлите пожалуйста коммерческое предложение на:" Width="590px"></asp:TextBox><br />
        <asp:TextBox ID="Text1" runat="server" Text="Наименование" Width="400px"></asp:TextBox><asp:TextBox ID="TextBox1" runat="server" Text="Обозначение"></asp:TextBox>
        <asp:TextBox ID="Text2" runat="server" Text="ID"></asp:TextBox><asp:TextBox ID="Text3" runat="server" Text="Кол." Width="35px"></asp:TextBox><br />
         Выбор файла фотографии: <br />
   <asp:FileUpload id="FileUpload" runat="server">
    </asp:FileUpload> <br />
         Выбор поставщиков из списка для отправки запроса на КП запчастей из события:<br />
     <asp:ListView ID="Post" runat="server" DataSourceID="Select">
                            <LayoutTemplate>
                                <table border="1" id="tbl1" runat="server">
                                    <tr id="Tr2" runat="server" style="background-color: #98FB98">
                                        <td id="Td4" runat="server">№:</td>
                                        <td id="Td2" runat="server">поставщик:</td>
                                        <td id="Td14" runat="server">е-mail:</td>
                                         <td id="Td71" runat="server">сайт:</td>
                                        <td id="Td15" runat="server">категория:</td>
                                        <td id="Td1" runat="server"><asp:CheckBox ID="SelectAll" runat="server" Enabled="True" /></td>
                                        <td></td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr1" runat="server">
                                     <td>
                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Id") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Email") %>' />
                                    </td>
                                     <td>
                                        <asp:LinkButton ID="Label4" runat="server"  Text='<%# Eval("Site") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Details") %>' />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="Select" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged"/>
                                    </td><td></td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
           <asp:SqlDataSource ID="Select" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand= "select Id, Name, Email, Site, Details from Suppliers"></asp:SqlDataSource>
   <br /><br />
        <asp:Button ID ="Email" runat ="server" OnClick = "Email_Click" Text ="Запрос нa КП по запчастям события" Height="35px" />
        <asp:Button ID ="Button1" runat ="server" OnClick ="Arbitrary_Query_Click" Text ="Запрос нa КП по введённым данным" Height="35px" />
    </asp:PlaceHolder>
<asp:Button ID="Button5" runat="server" Text="Редактировать Событие"  BackColor="#c2bdbd" ForeColor="000" OnClick="Edit_Click" Height="35px" /><br /><br /><br /><br /><br /><br /><br />
        <br />
        <br />
        <br />    
            <div class="content-wrapper"> 
                <div class="float-left">
                  <p> &copy; <%: DateTime.Now.Year %> - КОС</p> 
                  </div>
            </div>
        <br />
      <br />
    </form>    
</body>
</html>



