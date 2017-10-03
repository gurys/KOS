<%@ Page Title="Скачать/Просмотреть документ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="PartView.aspx.cs" Inherits="KOS.PartView" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #6da176; color:#000;">

                        <td id="td1" runat="server" class="td5px">№ события</td>
                        <td id="td10" runat="server" class="td5px">имя файла:</td>                        
                        <td id="td5" runat="server" class="td5px">наименование:</td>
                        <td id="td6" runat="server" class="td5px">обозначение:</td>
                        <td id="td7" runat="server" class="td5px">ID номер:</td>
                        <td id="td3" runat="server" class="td5px">количество:</td>                         
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="NumEvent" runat="server"></asp:Literal></td>
                        <td id="td2" runat="server" class="td5px"><asp:Literal ID="NameFile" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:TextBox ID="Name" runat="server" TextMode="MultiLine" Height="55px" Width="150px"></asp:TextBox></td>                       
                        <td id="td16" runat="server" class="td5px"><asp:TextBox ID="Obz" runat="server" TextMode="MultiLine" Height="55px" Width="100px"></asp:TextBox></td>
                        <td id="td17" runat="server" class="td5px"><asp:TextBox ID="NumID" runat="server" TextMode="MultiLine" Height="55px" Width="100px"></asp:TextBox></td>
                        <td id="td18" runat="server" class="td5px"><asp:TextBox ID="Kol" runat="server" TextMode="MultiLine" Height="55px" Width="80px"></asp:TextBox></td>                        
                        
                    </tr>
               </table>
    &nbsp; <asp:Label ID="Msg" runat="server" Text="" ForeColor="Red"></asp:Label><br />
      &nbsp;<br />
    <asp:Button ID ="Donl" runat ="server" OnClick = "Donl_Click" Text ="Просмотр фото запчасти" />
     &nbsp; &nbsp; <asp:Button ID ="Foto" runat ="server" OnClick = "Foto_Click" Text ="Скачать фото запчасти" />
      <br />
      <br />
      <asp:Button ID ="Button2" runat ="server" OnClick = "EditPart_Click" Text ="Редактировать запись"  ForeColor="Green"/>     
    &nbsp; &nbsp; <asp:Button ID ="Button1" runat ="server" OnClick = "DelPart_Click" Text ="Удалить запись!" Visible="false"  ForeColor="Red"/>
    
      &nbsp;
      &nbsp;
    </asp:Content>