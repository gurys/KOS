<%@ Page Language="C#"  AutoEventWireup="true" CodeBehind="PrimTSG.aspx.cs" Inherits="KOS.PrimTSG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8;" />
    <title>Aктивные события с замечаниями</title>
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
       
 <asp:PlaceHolder ID="phReport" runat="server">         
      <asp:Label ID="What" runat="server"></asp:Label></asp:PlaceHolder>
    <div id="dvHtml" runat="server">
       <h4> Активные События с Замечаниями:</h4>
        <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table border="1" id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">
                        <td id="td5" runat="server" class="td5px">№ соб.</td>
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
                    <td class="td5px">
                        <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                        <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' /></asp:HyperLink></td><td class="td5px">
                        <asp:Label ID="Sourse" runat="server" Text='<%# Eval("Sourse") %>' />
                    </td>
                    <td class="td5px">
                            <asp:Label ID="IO" runat="server" Text='<%# Eval("IO") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Date1" runat="server" Text='<%# Eval("Date1") %>' />
                    </td>                   
                    <td class="td5px">
                        <asp:Label ID="RegistrId" runat="server" SortExpression="RegistrId" Text='<%# Eval("RegistrId") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                        </td>
                        <td class="td5px">
                        <asp:Label ID="TypeId" runat="server" Text='<%# Eval("TypeId") %>' />                    
                    </td>
                    <td class="td5px">
                        <asp:Label ID="EventId" runat="server" Text='<%# Eval("EventId") %>' />
                    </td>                   
                    <td class="td5px">
                        <asp:Label ID="ToApp" runat="server" Text='<%# Eval("ToApp") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="DateToApp" runat="server" Text='<%# Eval("DateToApp") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Who" runat="server" Text='<%# Eval("Who") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Comment" runat="server" Text='<%# Eval("Comment") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Date2" runat="server" Text='<%# Eval("Date2") %>' />
                    </td>
                     <td class="td5px">
                        <asp:Label ID="Prim" runat="server"  style="color:red" Text='<%# Eval("Prim") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Timing" runat="server" Text='<%# Eval("Timing") %>' />
                    </td>
                   </tr>
            </ItemTemplate>

        </asp:ListView>
        <br /><br /><div class="content-wrapper">
                <div class="float-left">
                  <p> &copy; <%: DateTime.Now.Year %>- КОС</p></div></div><br /><br />

    </div>
    </form>
</body>
</html>
