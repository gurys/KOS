<%@ Page Title="Обработка внутренних событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BaseDoc.aspx.cs" Inherits="KOS.BaseDoc" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<br/> База документов: <asp:Label ID="Date" runat="server"></asp:Label><br />
    <asp:PlaceHolder ID="phVse" runat="server" Visible ="false">
        Список Всех документов в БД:
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" GridLines="None" Width="903px" CellSpacing="1" PageSize="20">
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="NumEvent" DataNavigateUrlFormatString="~/EventView.aspx?zId={0}" DataTextField="NumEvent" FooterText="NumEvent" HeaderText="номер события" SortExpression="NumEvent" />
                <asp:BoundField DataField="Name" HeaderText="Название документа" SortExpression="Name" />
                <asp:BoundField DataField="NameFile" HeaderText="имя файла документа" SortExpression="NameFile" />
                <asp:HyperLinkField DataNavigateUrlFields="Id" HeaderText="документ" SortExpression="Image" DataNavigateUrlFormatString="~/DocumView.aspx?zId={0}" DataTextField="Image" DataTextFormatString="просмотр" FooterText="просмотр"/>
                <asp:BoundField DataField="Status" HeaderText="статус документа" SortExpression="Status" />
                <asp:BoundField DataField="Prim" HeaderText="примечание" SortExpression="Prim" />
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Id], [Name], [NumEvent], [Image], [NameFile], [Status], [Prim] FROM [Documents]"></asp:SqlDataSource>
   </asp:PlaceHolder>
     <br />                 <asp:Label  ID="Msg" runat="server" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
    <asp:PlaceHolder ID="Mes1" runat="server"> <asp:Label  ID="Msg1" runat="server" ForeColor="Green" meta:resourcekey="MsgResource1"></asp:Label>
    </asp:PlaceHolder>
    <br />
    <asp:Button ID="VseDoc" runat="server" Text="Список всех" OnClick="VseDoc_Click" Width="151px" />
&nbsp;&nbsp;
    <asp:Button ID="Poisk" runat="server" Text="Поиск по параметрам" OnClick="Poisk_Click" Width="176px" />
&nbsp;&nbsp;
    <asp:Button ID="ZapDoc" runat="server" Text="Запись в базу с локального ПК" OnClick="ZapDoc_Click" Width="245px" />
    <br />
    <asp:PlaceHolder ID="phPoisk" runat="server" Visible="false">
    Найти документы по:&nbsp; № события*:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; названию&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; статусу<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:TextBox ID="TextBox5" runat="server" Width="62px"></asp:TextBox>
&nbsp;&nbsp;
   <asp:DropDownList ID="Ndoc" runat="server"></asp:DropDownList>
&nbsp;
    <asp:DropDownList ID="Sdoc" runat="server"></asp:DropDownList>
    <br />
    <br />
&nbsp;<asp:Button ID="PoiskParam" runat="server" Text="Найти документ" OnClick="PoiskParam_Click" Width="208px" />
    <br />
    <br />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phZap" runat="server" Visible="false">
                        Для записи документа в базу&nbsp; заполнить поля, выбрать его файл из папки на своем компьютереи нажать Сохранить.<br />
                        Первые два поля обязательны для заполнения!<br />
                        № события: <asp:TextBox ID="TextBox1" runat="server" Width="42px"></asp:TextBox>
        &nbsp; название:
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
           &nbsp; cтатус:
                        <asp:TextBox ID="TextBox3" runat="server" Width="86px"></asp:TextBox>
              &nbsp; прим.:
                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                        &nbsp;
                        <br />
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button1" runat="server" OnClick="Save_Click" Text="СОХРАНИТЬ" Width="183px" />
    </asp:PlaceHolder>
    <br />
      <asp:PlaceHolder ID="phView" runat="server">
 <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table border="0" id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">
                        <td id="td15" runat="server" class="td5px">№ в БД</td>
                        <td id="td5" runat="server" class="td5px">№ События</td>
                        <td id="td10" runat="server" class="td5px">название</td>
                        <td id="td7" runat="server" class="td5px">имя файла </td>
                        <td id="td1" runat="server" class="td5px">статус</td>
                        <td id="td3" runat="server" class="td5px">примечание</td>
                        <td id="td2" runat="server" class="td5px">создал</td>
                       
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
                        <asp:Label ID="DataPost" runat="server" Text='<%# Eval("IdE") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="NumDoc" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td class="td5px">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("nameFile") %>' />
                    </td>
                     <td class="td5px">
                        <asp:Label ID="Zakreplen" runat="server" Text='<%# Eval("Status") %>' />
                    </td>
                    <td class="td5px">
                            <asp:Label ID="Name" runat="server" Text='<%# Eval("PrimDoc") %>' />
                    </td>
                     <td class="td5px">
                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("Usr") %>' />
                    </td>
                    
                   </tr>
            </ItemTemplate>
        </asp:ListView>
   </asp:PlaceHolder>
</asp:Content>