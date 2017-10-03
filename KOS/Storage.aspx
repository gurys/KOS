<%@Page Title="Склады" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Storage.aspx.cs" Inherits="KOS.Storage" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<h4>Склады</h4><br />
    Выбрать номер склада:
    <asp:DropDownList ID="DdlSklad" runat="server"></asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
 <asp:Button ID="Button1" runat="server" Text="Просмотр" OnClick ="Sklsdd_Click"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:PlaceHolder ID="phPost" runat="server">
    <asp:Button ID="Button2" runat="server" Text="Поступление" OnClick ="Vvod_Click"/></asp:PlaceHolder> 
    <br /> 
    <br />
    <asp:PlaceHolder ID="phVvod" runat="server" Visible="false">
    Ввод поступивших на склады запчастей:<br />
    Склад:
    <asp:label ID="SkladVvod" runat="server"></asp:label>
&nbsp;№ документа:
    <asp:TextBox ID="TextBox1" runat="server" Width="99px"></asp:TextBox>
&nbsp;Закрепить за:
    <asp:DropDownList ID="DdlSklad1" runat="server"></asp:DropDownList>
&nbsp;<br /><br />
    Наименование:
    <asp:TextBox ID="TextBox2" runat="server" Width="400px"></asp:TextBox>
    Обозначение:<asp:TextBox ID="TextBox8" runat="server" Width="100px"></asp:TextBox>
    <br /><br />
    ID номер:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
&nbsp;кол.:
    <asp:TextBox ID="TextBox4" runat="server" Width="40px"></asp:TextBox>
&nbsp;цена:
    <asp:TextBox ID="TextBox5" runat="server" Width="40px"></asp:TextBox>
&nbsp;источник:
    <asp:TextBox ID="TextBox6" runat="server" Width="175px"></asp:TextBox><br /><br />
        <asp:Button ID="Button4" runat="server" Text="Записать поступление" OnClick ="Button2_Click"/> 
    </asp:PlaceHolder>
    &nbsp;&nbsp;
    <asp:Button ID="Button5" runat="server" Text="Очистить зону ввода" OnClick ="Button5_Click"/>
    <br />
    <asp:PlaceHolder ID="phSpPr" runat="server" Visible="false">
        Для перемещения/списания запчасти перейдите в её карточку:
        Введите номер в БД: <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
        или выберите название: <asp:DropDownList ID="DplBdEqu" runat="server"></asp:DropDownList>
        <asp:Button ID="Button6" runat="server" Text="Перейти в карточку" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phView" runat="server">
 <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table border="0" id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">
                        <td id="td15" runat="server" class="td5px">№ в БД</td>
                        <td id="td5" runat="server" class="td5px">Дата поступ.</td>
                        <td id="td10" runat="server" class="td5px">№ док.</td>
                        <td id="td7" runat="server" class="td5px">№ склада</td>
                        <td id="td1" runat="server" class="td5px">Закрепление</td>
                        <td id="td17" runat="server" class="td5px">Принял</td>
                        <td id="td3" runat="server" class="td5px">наменование</td>
                        <td id="td16" runat="server" class="td5px">обознач.</td>
                        <td id="td2" runat="server" class="td5px">ID номер</td>                       
                        <td id="td4" runat="server" class="td5px">кол-во</td>
                        <td id="td13" runat="server" class="td5px">цена</td>
                        <td id="td11" runat="server" class="td5px">источник</td>
                        <td id="td8" runat="server" class="td5px">Дата списания</td>
                        <td id="td6" runat="server" class="td5px">№ док.</td>
                        <td id="td12" runat="server" class="td5px">кол-во</td>
                        <td id="td9" runat="server" class="td5px">примеч.</td>
                        <td id="td14" runat="server" class="td5px">ост.</td>                       
                        
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server" style="background-color:#FFF; border-color:#000; color:#000;">
                    <td class="td5px">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                        </asp:HyperLink></td>
                    <td class="td5px">                      
                        <asp:Label ID="DataPost" runat="server" Text='<%# Eval("DataPost") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="NumDoc" runat="server" Text='<%# Eval("NumDoc") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("NumSklada") %>' />
                    </td>
                     <td class="td5px">
                        <asp:Label ID="Zakreplen" runat="server" Text='<%# Eval("Zakreplen") %>' />
                          <td class="td5px">
                    </td>
                        <asp:Label ID="Prinyal" runat="server" Text='<%# Eval("Prinyal") %>' />
                    </td>
                    <td class="td5px">
                            <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                     <td class="td5px">
                            <asp:Label ID="Obz" runat="server" Text='<%# Eval("Obz") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="NumID" runat="server" Text='<%# Eval("NumID") %>' />
                    </td>                   
                    <td class="td5px">
                        <asp:Label ID="TheNum" runat="server"  Text='<%# Eval("TheNum") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Price" runat="server" Text='<%# Eval("Price") %>' />
                        </td>
                        <td class="td5px">
                        <asp:Label ID="Source" runat="server" Text='<%# Eval("Source") %>' />                    
                    </td>
                    <td class="td5px">
                        <asp:Label ID="DataSpisaniya" runat="server" Text='<%# Eval("DataSpisaniya") %>' />
                    </td>                   
                    <td class="td5px">
                        <asp:Label ID="NumDocSpisan" runat="server" Text='<%# Eval("NumDocSpisan") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="TheNumSpisan" runat="server" Text='<%# Eval("TheNumSpisan") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Prim" runat="server" Text='<%# Eval("Prim") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Ostatok" runat="server" Text='<%# Eval("Ostatok") %>' />
                    </td>
                   </tr>
            </ItemTemplate>
        </asp:ListView>
   </asp:PlaceHolder>
</asp:Content>