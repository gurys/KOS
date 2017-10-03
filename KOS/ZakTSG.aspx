<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZakTSG.aspx.cs" Inherits="KOS.ZakTSG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Редактирование/Закрытие События ОДС</title>
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
         <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/Reg_tsg.aspx" />
<br /><br />
     <h4> Событие № <asp:Literal ID="Id" runat="server"></asp:Literal>&nbsp;с момента регистрации прошло:&nbsp;
          <asp:Label ID="Timing" runat="server" ForeColor="Blue"></asp:Label>&nbsp;</h4>&nbsp;Редактировать/Закрыть: 
        <asp:Label ID="ZayavId" runat="server" Text="" Visible="false"></asp:Label><br />
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server">
                        <td id="td10" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">источник*</td>
                        <td id="td2" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">диспетчер*</td> 
                        <td id="td5" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">дата/время*</td>
                        <td id="td6" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">вид услуги</td>
                        <td id="td7" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">зона*</td>
                        <td id="td8" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">событие*</td>
                        <td id="td3" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">описание*</td>
                        <td id="td9" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">принял*</td>
                        <td id="td20" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">дата/время*</td>
                        <td id="td4" runat="server" class="td5px" style="background-color: #f3a8a8; color:#000;">исполнил*</td>
                        <td id="td14" runat="server" class="td5px" style="background-color: #f3a8a8; color:#000;">комментарий*</td>
                        <td id="td15" runat="server" class="td5px" style="background-color: #f3a8a8; color:#000;">дата/время*</td>
                        <td id="td30" runat="server" class="td5px" style="background-color: #0094ff; color:#000;">замечания*</td>                                            
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td28" runat="server" class="td5px"><asp:TextBox ID="Sourse" runat="server" TextMode="MultiLine" Height="80px" Width="90px"></asp:TextBox></td>
                        <td id="td12" runat="server" class="td5px"><asp:TextBox ID="IO" runat="server" TextMode="MultiLine" Height="80px" Width="85px"></asp:TextBox></td>
                        <td id="td13" runat="server" class="td5px"><asp:TextBox ID="DataId" runat="server" TextMode="MultiLine" ForeColor="Blue" Height="80px" Width="90px"></asp:TextBox></td>
                        <td id="td16" runat="server" class="td5px"><asp:TextBox ID="RegistrId" runat="server" Height="80px" TextMode="MultiLine" Width="90px"></asp:TextBox></td> 
                        <td id="td17" runat="server" class="td5px"><asp:TextBox ID="LiftId" runat="server" TextMode="MultiLine" Height="80px" Width="90px"></asp:TextBox></td>
                        <td id="td18" runat="server" class="td5px"><asp:TextBox ID="TypeId" runat="server" TextMode="MultiLine" Height="80px" Width="85px"></asp:TextBox></td>
                        <td id="td19" runat="server" class="td5px"><asp:TextBox ID="EventId" runat="server" TextMode="MultiLine" Height="80px" Width="105px"></asp:TextBox></td>
                        <td id="td22" runat="server" class="td5px"><asp:TextBox ID="ToApp" runat="server" TextMode="MultiLine" Height="80px" Width="85px"></asp:TextBox></td>
                        <td id="td23" runat="server" class="td5px"><asp:TextBox ID="DateToApp" runat="server" TextMode="MultiLine" ForeColor="Green" Height="80px" Width="90px"></asp:TextBox></td>
                        <td id="td24" runat="server" class="td5px"><asp:TextBox ID="Who" runat="server" TextMode="MultiLine" Height="80px" Width="85px"></asp:TextBox></td>
                        <td id="td25" runat="server" class="td5px"><asp:TextBox ID="Comment" runat="server" TextMode="MultiLine" Height="80px" Width="105px"></asp:TextBox></td>
                        <td id="td26" runat="server" class="td5px"><asp:TextBox ID="DateWho" runat="server" TextMode="MultiLine" ForeColor="#072a02" Height="80px" Width="90px"></asp:TextBox></td>
                        <td id="td29" runat="server" class="td5px"><asp:TextBox ID="Prim" runat="server" ForeColor="Red" TextMode="MultiLine" Height="80px" Width="105px"></asp:TextBox></td>
                        <td id="td27" runat="server" class="td5px"></td>                        
                        
                    </tr>
               </table>
         <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>&nbsp;&nbsp;&nbsp;<br />
         &nbsp;<asp:PlaceHolder ID="PinPl" runat="server" >
           Для редактирования/закрытия события введите пин-код:
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder><br />
        <asp:PlaceHolder ID="ActionPin" runat="server" Visible="false">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID ="Edit" runat ="server" OnClick = "Edit_Click" Text ="ПРИНЯТЬ ИЗМЕНЕНИЯ" BackColor="#0094ff" ForeColor="#000" Height="35px" Width="173px" /> 
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID ="Close" runat ="server" OnClick = "Close_Click" Text ="ЗАКРЫТЬ СОБЫТИЕ" BackColor="#f3a8a8" ForeColor="#000" Height="35px" Width="165px" /> 
        <br />
        &nbsp;&nbsp;<asp:PlaceHolder ID="DocPodp" runat="server">
        &nbsp;&nbsp;Документы на подпись:
<asp:ListView ID="Out1" runat="server">
                    <LayoutTemplate>
                        <table  id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #db6868; color: #060f3d;">
                                <td id="td3" runat="server" class="td5px">документ:</td>                                                               
                                <td id="td5" runat="server" class="td5px">имя файла:</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style ="background-color: #e1ebf2; color: #060f3d;">                           
                            <td class="tdwhite" >                                
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" > 
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("NameFile") %>' />
                                </asp:HyperLink></td></tr></ItemTemplate></asp:ListView></asp:PlaceHolder><br />
        &nbsp;&nbsp;<asp:PlaceHolder ID="DocVw" runat="server">
        &nbsp;&nbsp;Прикрепленные документы:
<asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                                <td id="td3" runat="server" class="td5px">документ:</td>                                                               
                                <td id="td5" runat="server" class="td5px">имя файла:</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style ="background-color: #e1ebf2; color: #060f3d;">                           
                            <td class="tdwhite" >                                
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" > 
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("NameFile") %>' />
                                </asp:HyperLink></td></tr></ItemTemplate></asp:ListView></asp:PlaceHolder><br />
        <br />
    <!--    &nbsp;&nbsp;Прикреплено Вами документов:&nbsp; <asp:Literal ID="UsrDoc" runat="server"></asp:Literal> -->
        &nbsp;&nbsp;Прикрепить документ:&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
        &nbsp;&nbsp;Введите название:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="DocName" runat="server">
        </asp:TextBox>
         &nbsp;&nbsp;&nbsp;<asp:Button ID ="Save" runat ="server" OnClick = "Save_Click" Text ="СОХРАНИТЬ" BackColor= "Wheat" ForeColor="#000" Height="35px" Width="110px" /> 
</asp:PlaceHolder>
        <br /><br /><br /><br /><br /><br /><br />
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
