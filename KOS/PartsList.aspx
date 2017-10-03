<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PartsList.aspx.cs" Inherits="KOS.PartsList" %>
<%@ Register tagprefix="uc" tagname="DatePicker" src="~/Controls/DatePicker.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Отчеты по запчастям событий</title>
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/" />
        
         <br />
         <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Скачать отчет в pdf&nbsp;<asp:ImageButton ID="btnClick" runat="server" OnClick="btnClick_Click" AlternateText="pdf" ImageUrl="~/Images/pdf56.png" Width="30" /> 
       &nbsp;в excel&nbsp;<asp:ImageButton ID="Excel" runat="server" OnClick="Excel_Click" AlternateText="excel" ImageUrl="~/Images/excel50.png" Width="30" />
        <asp:HyperLink ID="Download" runat="server" Visible="true"></asp:HyperLink><br />
         <br />       
         <br /> 
    <div id="dvHtml" runat="server">
     
    <asp:PlaceHolder ID="Period" runat="server"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Выбирете период отчета:<br /><br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;С <uc:DatePicker ID="Beg" runat="server" />
        по <uc:DatePicker ID="End" runat="server" />
       <br />
        <br /> 
         <br />       
          <br /> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="DoIt" runat="server" Text="СФОРМИРОВАТЬ ОТЧЕТ" OnClick="PartList_Click" Height="35px" Width="250px" />
   </asp:PlaceHolder>
      <h4><asp:PlaceHolder ID="phReport" runat="server">         
          <asp:Label ID="What" runat="server"></asp:Label></asp:PlaceHolder></h4>
          <asp:ListView ID="ListView1" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #6da176; color: #000;">
                               
                                <td id="td1" runat="server" class="td5px">№ БД</td>
                                <td id="td2" runat="server" class="td5px">№ соб.</td>   
                                <td id="td5" runat="server" class="td5px">наименование</td> 
                                <td id="td4" runat="server" class="td5px">обозначение</td>                               
                                <td id="td6" runat="server" class="td5px">ID номер</td>                                
                                <td id="td7" runat="server" class="td5px">количество</td>
                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style="background-color:#FFF; border-color:#000; color:#000;">
                            <td class="tdwhite" >
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </asp:HyperLink></td>
                            <td class="tdwhite" >
                                 <asp:HyperLink ID="Url1" runat="server" NavigateUrl='<%# Eval("Url1") %>'>
                                <asp:Label ID="NumEvent" runat="server" Text='<%# Eval("NumEvent") %>' />
                                </asp:HyperLink></td>
                                                      
                            <td class="tdwhite" >
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                             <td class="tdwhite" >
                                <asp:Label ID="Obz" runat="server" Text='<%# Eval("Obz") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Status" runat="server" Text='<%# Eval("NumId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Prim" runat="server" Text='<%# Eval("Kol") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView><br /><br />
        <div class="content-wrapper">
                <div class="float-left">
                  <p> &copy; <%: DateTime.Now.Year %>- КОС</p></div><br /><br /></div>
    </div>
    </form>
</body>
</html>