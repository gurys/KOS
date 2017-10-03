<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrimEditTsg.aspx.cs" Inherits="KOS.PrimEditTsg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Внесение/Редактирование замечаний по Событию</title>
    <style type="text/css">


    .content-wrapper {
        padding-right: 10px;
        padding-left: 10px;
    }

    .content-wrapper {
    margin: 0 auto;
    max-width: 960px;
}

.float-left {
    float: left;
}

    </style>
</head>  
<body>
     
    <form id="form1" runat="server" style ="background-color:#ede8e8; font-family:'Segoe UI', Verdana, Helvetica, Sans-Serif; font-size: 13px;">
 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/ManagerTSG.aspx" />
    <div>
        <br />
        Выбрано Событие для внесения/редактирования Замечаний:<br />
        <br />
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">

                        <td id="td1" runat="server" class="td5px">№</td>
                        <td id="td10" runat="server" class="td5px">источник</td>
                        <td id="td2" runat="server" class="td5px">диспетчер</td> 
                        <td id="td5" runat="server" class="td5px">дата/время</td>
                        <td id="td6" runat="server" class="td5px">вид услуги</td>
                        <td id="td7" runat="server" class="td5px">зона</td>
                        <td id="td8" runat="server" class="td5px">событие</td>
                        <td id="td3" runat="server" class="td5px">описание</td>
                        <td id="td9" runat="server" class="td5px">принял</td>
                        <td id="td20" runat="server" class="td5px">дата/время</td>
                        <td id="td4" runat="server" class="td5px">исполнил</td>
                        <td id="td14" runat="server" class="td5px">комментарий</td>
                        <td id="td15" runat="server" class="td5px">дата/время</td>
                        <td id="td29" runat="server" class="td5px">замечание</td> 
                        <td id="td21" runat="server" class="td5px">тайминг</td>                        
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="Id" runat="server"></asp:Literal></td>
                        <td id="td28" runat="server" class="td5px"><asp:Literal ID="Sourse" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:Literal ID="IO" runat="server"></asp:Literal></td>
                        <td id="td13" runat="server" class="td5px"><asp:Literal ID="DataId" runat="server"></asp:Literal></td>
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="RegistrId" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="LiftId" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="TypeId" runat="server"></asp:Literal></td>
                        <td id="td19" runat="server" class="td5px"><asp:Literal ID="EventId" runat="server"></asp:Literal></td>
                        <td id="td22" runat="server" class="td5px"><asp:Literal ID="ToApp" runat="server"></asp:Literal></td>
                        <td id="td23" runat="server" class="td5px"><asp:Literal ID="DateToApp" runat="server"></asp:Literal></td>
                        <td id="td24" runat="server" class="td5px"><asp:Literal ID="Who" runat="server"></asp:Literal></td>
                        <td id="td25" runat="server" class="td5px"><asp:Literal ID="Comment" runat="server"></asp:Literal></td>
                        <td id="td26" runat="server" class="td5px"><asp:Literal ID="DateWho" runat="server"></asp:Literal></td>
                        <td id="td30" runat="server" class="td5px"><asp:Literal ID="Prim" runat="server"></asp:Literal></td>
                        <td id="td27" runat="server" class="td5px"><asp:Literal ID="Timing" runat="server"></asp:Literal></td>                        
                        
                    </tr>
               </table>
        <br />
        &nbsp;
        &nbsp; <br />
        <asp:PlaceHolder ID = "Vpin" runat ="server">
       <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
        пин-код:<br />           
        <asp:TextBox ID="Pn" runat="server" TextMode ="Password" Width ="60px"></asp:TextBox>        
        
        <asp:Button ID="Button1" runat="server" OnClick ="Button1_Click" Text="ВВОД" Width ="65px" /></asp:PlaceHolder>
        <br />
        <br />
        Описание/редактирование замечаний по Событию:<br />
        <asp:TextBox ID="Text" runat="server" Height="45px" Width="661px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox>   
    <br />
        <asp:PlaceHolder ID = "PIN" runat ="server">
   <asp:Button ID ="Edit" runat ="server" OnClick = "Edit_Click" Text ="ВНЕСТИ" BackColor="#CC0000" ForeColor="#CCFF99" Height="35px" Width="670px" /> <br />
   </asp:PlaceHolder> 
        <br />
        <br />
    
            <div class="content-wrapper">
                <div class="float-left">
                  <p> &copy; <%: DateTime.Now.Year %> - КОС</p> 
                   
                </div>
            </div>    
        <br />
      <br />
    </div>
    </form>
</body>
</html>
