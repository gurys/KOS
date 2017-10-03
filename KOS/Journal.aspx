<%@ Page Title="Журнал" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Journal.aspx.cs" Inherits="KOS.Journal" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Журнал</h3>
    <asp:Button ID="Add" runat="server" Text="Добавить" OnClick="Add_Click" BackColor= "Wheat" ForeColor="#000" /><br />
    <asp:PlaceHolder ID="phAddRecord" runat="server" Visible="false">
        Кому: <asp:DropDownList ID="AddTo" runat="server"></asp:DropDownList><br />
        Тема:<br /> <asp:TextBox ID="AddRole" runat="server"></asp:TextBox><br />
        Документ:<br /> <asp:TextBox ID="AddPage" runat="server"></asp:TextBox><br />
        Прикрепить документ:<br />
        <asp:FileUpload ID="FileUpload1" runat="server" /><br />
        Сообщение:<br /> <asp:TextBox ID="AddDescription" runat="server" Height="50px" Width="400px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
        <asp:Button ID="AddRecord" runat="server" Text="Сохранить" OnClick="AddRecord_Click" BackColor= "Wheat" ForeColor="#000"/><br />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEditRecord" runat="server" Visible="false">
        От кого:<br /> <asp:TextBox ID="EditFrom" runat="server"></asp:TextBox><br />
        Кому:<br /> <asp:TextBox ID="EditTo" runat="server"></asp:TextBox><br />
        Тема:<br /> <asp:TextBox ID="EditRole" runat="server"></asp:TextBox><br />        
        Документ:&nbsp;<asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br /> <asp:TextBox ID="EditPage" runat="server"></asp:TextBox>
        &nbsp;<asp:Button ID="Donl" runat="server" Text="Смотреть" OnClick="Donl_Click" BackColor= "Wheat" ForeColor="#000"/>
        &nbsp;<asp:Button ID="Foto" runat="server" Text="Скачать" OnClick="Foto_Click" BackColor= "Wheat" ForeColor="#000" />
        <br />
        Сообщение<br /> <asp:TextBox ID="EditDescription" runat="server" Height="50px" Width="400px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
        Примечание:<br /> <asp:TextBox ID="EditPrim" runat="server" Height="50px" Width="400px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
        <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" BackColor= "Wheat" ForeColor="#000" />
        <asp:Button ID="Delete" runat="server" Text="Удалить" OnClick="Delete_Click" BackColor= "Wheat" ForeColor="#000" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="JournalTable" runat="server">
        <asp:ListView ID="Table" runat="server" DataSourceID="Select">
            <LayoutTemplate>
                <table border="1" id="tbl1" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #86b5d8">
                        <td id="Th1" runat="server">№</td>
                        <td id="Td6" runat="server">от кого</td>
                        <td id="Th2" runat="server">дата</td>
                        <td id="Td1" runat="server">тема</td>
                        <td id="Td2" runat="server">документ</td>
                        <td id="Td3" runat="server">сообщение</td>
                        <td id="Td4" runat="server">принято</td>
                        <td id="Td5" runat="server">примечание</td>
                        <td id="Td7" runat="server">кому</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td class="tdwhite">
                        <asp:HyperLink ID="Id" runat="server" NavigateUrl='<%# Eval("Url") %>'><%# Eval("Id") %></asp:HyperLink>
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("FromFIO") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Begin" runat="server" Text='<%# Eval("Begin") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Role") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Page") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("End") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label5" runat="server" Text='<%# Eval("Prim") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Label7" runat="server" Text='<%# Eval("ToFIO") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="Table" PageSize="25">
            <Fields>
                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="true" ShowNextPageButton="false" ShowLastPageButton="false" ShowPreviousPageButton="false" />
                <asp:NumericPagerField ButtonCount="10" />
                <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="true" ShowFirstPageButton="false" ShowNextPageButton="false" ShowPreviousPageButton="false" />
            </Fields>
        </asp:DataPager>
        <asp:SqlDataSource ID="Select" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand="select j.*, uf.Family+' '+uf.IO as FromFIO, ut.Family+' '+ut.IO as ToFIO, '~/Journal.aspx?id='+CAST(j.Id as nvarchar) as Url from [Journal] j left join UserInfo uf on uf.UserId=j.[From] left join UserInfo ut on ut.UserId=j.[To] order by j.[Begin] desc"></asp:SqlDataSource>
    </asp:PlaceHolder>
</asp:Content>
