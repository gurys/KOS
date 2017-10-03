<%@ Page Title="Оборудование" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Enviroment.aspx.cs" Inherits="KOS.Enviroment" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Оборудование</h3>
    <asp:Button ID="Add" runat="server" Text="Добавить" OnClick="Add_Click" /><br />
    <asp:PlaceHolder ID="phAddRecord" runat="server" Visible="false">
        Состояние: <asp:DropDownList ID="AddType" runat="server"></asp:DropDownList><br />
        Наименование: <asp:TextBox ID="AddName" runat="server"></asp:TextBox><br />
        Адрес: <asp:DropDownList ID="AddAddress" runat="server"></asp:DropDownList><br />
        Примечание:<br /> <asp:TextBox ID="AddDescription" runat="server" Height="131px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
        <asp:Button ID="AddRecord" runat="server" Text="Сохранить" OnClick="AddRecord_Click" /><br />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEditRecord" runat="server" Visible="false">
        Состояние: <asp:DropDownList ID="EditType" runat="server"></asp:DropDownList><br />
        Наименование: <asp:TextBox ID="EditName" runat="server"></asp:TextBox><br />
        Адрес: <asp:DropDownList ID="EditAddress" runat="server"></asp:DropDownList><br />
        Примечание:<br /> <asp:TextBox ID="EditDescription" runat="server" Height="131px" Width="407px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox><br />
        <asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
        <asp:Button ID="Delete" runat="server" Text="Удалить" OnClick="Delete_Click" />
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="EnviromentTable" runat="server">
        <asp:ListView ID="Table" runat="server" DataSourceID="Select">
            <LayoutTemplate>
                <table border="1" id="tbl1" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td id="Th1" runat="server">№</td>
                        <td id="Td6" runat="server">наименование</td>
                        <td id="Th2" runat="server">состояние</td>
                        <td id="Td1" runat="server">адрес</td>
                        <td id="Td2" runat="server">примечание</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:HyperLink ID="Id" runat="server" NavigateUrl='<%# Eval("Url") %>'><%# Eval("Id") %></asp:HyperLink>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("Type") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text='<%# Eval("Address") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("Description") %>' />
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
            SelectCommand="select e.Id, '~/Enviroment.aspx?id='+CAST(e.Id as nvarchar) as Url, e.[Name], e.Type, ea.Address, e.[Description] from [Enviroment] e join EnviromentAddresses ea on ea.Id=e.AddressId order by e.Id"></asp:SqlDataSource>
    </asp:PlaceHolder>
</asp:Content>
