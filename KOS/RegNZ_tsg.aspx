<%@Page Title="Регистрация событий" Language="C#" AutoEventWireup="true" CodeBehind="RegNZ_tsg.aspx.cs" Inherits="KOS.RegNZ_tsg" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
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

    <form id="form1" runat="server" style ="background-color:#ede8e8; font-family:'Segoe UI', Verdana, Helvetica, Sans-Serif; font-size: 13px;">
        <body onload="startTime()"><div id="txt" class="float-right"></div></body>
                        <asp:Label ID="Date" runat="server" class="float-right" CssClass="tddate"></asp:Label><br/>
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
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/Reg_tsg.aspx" />
<br /><br /> 

         <h4> <asp:Label ID="What" runat="server"></asp:Label></h4>
   
    
          &nbsp;<asp:ListView ID="Out1" runat="server">
            <LayoutTemplate>
                <table id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">
                        <td id="td5" runat="server" class="td5px">№ события</td>
                        <td id="td10" runat="server" class="td5px">источник</td>
                        <td id="td1" runat="server" class="td5px">диспетчер</td>
                        <td id="td2" runat="server" class="td5px">дата/время</td>                       
                        <td id="td4" runat="server" class="td5px">вид услуги</td>
                        <td id="td13" runat="server" class="td5px">зона</td>
                        <td id="td11" runat="server" class="td5px">событие</td>                                              
                        <td id="td7" runat="server" class="td5px">описание</td>
                        <td id="td8" runat="server" class="td5px">принял</td>
                        <td id="td6" runat="server" class="td5px">дата/время</td>
                        <td id="td12" runat="server" class="td5px">исполнил</td>
                        <td id="td9" runat="server" class="td5px">комментарий</td>
                        <td id="td14" runat="server" class="td5px">дата/время</td>
                        <td id="td3" runat="server" class="td5px">замечания</td>                        
                        <td id="td16" runat="server" class="td5px">тайминг</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server" style="background-color:#FFF; border-color:#000; color:#000;">
                    <td class="tdwhite">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' /></asp:HyperLink></td><td class="tdwhite">
                        <asp:Label ID="Sourse" runat="server" Text='<%# Eval("Sourse") %>' />
                    </td>
                    <td class="tdwhite">
                            <asp:Label ID="IO" runat="server" Text='<%# Eval("IO") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date1" runat="server" Text='<%# Eval("Date1") %>' />
                    </td>                   
                    <td class="tdwhite">
                        <asp:Label ID="RegistrId" runat="server" Text='<%# Eval("RegistrId") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                        </td>
                        <td class="tdwhite">
                        <asp:Label ID="TypeId" runat="server" Text='<%# Eval("TypeId") %>' />                    
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="EventId" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>                   
                    <td class="tdwhite">
                        <asp:Label ID="ToApp" runat="server" Text='<%# Eval("ToApp") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="DateToApp" runat="server" Text='<%# Eval("DateToApp") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Who" runat="server" Text='<%# Eval("Who") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Comment" runat="server" Text='<%# Eval("Comment") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date2" runat="server" Text='<%# Eval("Date2") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Prim") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Timing" runat="server" Text='<%# Eval("Timing") %>' />
                    </td>


                    </tr></ItemTemplate></asp:ListView><br /><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" Width="232px" Text="Регистрация новых событий"  OnClick="DiagrammODS_Click" BackColor="#CC0000" ForeColor="White" Height="65px"/>&nbsp; <br />
        <br /> <asp:ScriptManager runat="server">
            <Scripts>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="jquery.ui.combined" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>
         <asp:Timer ID="Timer1" runat="server" OnTick="InSmsText_Click" Interval="300000"></asp:Timer>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false"> 
             Ответные СМС от Дежурных служб: <br />Дата: <asp:Label ID="Creat" runat="server" Text=""></asp:Label>Телефон: <asp:Label ID="Sender" runat="server" Text=""></asp:Label>№ события: <asp:Label ID="NumEv" runat="server" Text=""></asp:Label>Статус: <asp:Label ID="Status" runat="server" Text=""></asp:Label></asp:PlaceHolder><br />   
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
