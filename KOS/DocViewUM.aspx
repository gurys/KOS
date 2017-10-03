<%@ Page Title="Скачать/Просмотреть документ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DocViewUM.aspx.cs" Inherits="KOS.DocViewUM" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

      <table id="tblr" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #9face9; color:#000;">

                        <td id="td1" runat="server" class="td5px">№ док.</td>
                        <td id="td10" runat="server" class="td5px">наименование:</td>                        
                        <td id="td5" runat="server" class="td5px">имя файла:</td>
                        <td id="td6" runat="server" class="td5px">статус:</td>
                        <td id="td7" runat="server" class="td5px">примечание:</td>                        
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="Id" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:Literal ID="name" runat="server"></asp:Literal></td>                       
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="namefile" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="status" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="primm" runat="server"></asp:Literal></td>                        
                        
                    </tr>
               </table>

    &nbsp;<asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
      &nbsp;Просмотр документов в формате .PDF<br />
    <asp:Button ID ="Donl" runat ="server" OnClick = "Donl_Click" Text ="Просмотр документа" /> &nbsp;&nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID ="Foto" runat ="server" OnClick = "Foto_Click" Text ="Скачать документ" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID ="DelDoc" runat ="server" OnClick ="DelDoc_Click" Text ="Удалить документ" Visible="false"/><br />
    <asp:PlaceHolder ID="phPodp" runat="server" Visible="false">
    <asp:PlaceHolder ID="PinPl" runat="server" >
           Для подписания документа введите пин-код:
            <asp:TextBox ID="Pin" runat="server" TextMode ="Password" Width="50px"></asp:TextBox>
            <asp:Button ID ="Pinn" runat ="server" Text="Ввод" OnClick="Pinn_Click" Width="50px" />
              </asp:PlaceHolder><br />
    <asp:PlaceHolder ID="ActionPin" runat="server" Visible="false">
        Электронная подпись возможна только в документе формата .PDF<br />
      Введите должность/ФИО, если в документе нет или указана не Ваша:<br />
      <asp:TextBox ID="TextZak" runat="server" Width="300"></asp:TextBox><br />
      <asp:Button ID="Button1" runat="server" Text="Подписать документ" OnClick="Edit_Click"/>
    </asp:PlaceHolder>
    </asp:PlaceHolder>   
    
    </asp:Content>