<%@ Page Title="Поставщики" Language="C#"MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suppliers.aspx.cs" Inherits="KOS.Suppliers" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp  База  поставщиков</h3>

    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Id" DataSourceID="SqlDataSource1" GridLines="Vertical">
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="Name" HeaderText="Поставщик" SortExpression="Name" />
            <asp:BoundField DataField="Details" HeaderText="Описание" SortExpression="Details" />
            <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
            <asp:BoundField DataField="Site" HeaderText="Сайт" SortExpression="Site" />
            <asp:HyperLinkField DataNavigateUrlFields="Site" DataTextField="Site" DataTextFormatString="сайта" HeaderText="Просмотр" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" SortExpression="Phone" />
            <asp:BoundField DataField="Person" HeaderText="конт. лицо" SortExpression="Person" />
            <asp:BoundField DataField="EmailPerson" HeaderText="Email конт." SortExpression="EmailPerson" />
            <asp:BoundField DataField="Prim" HeaderText="Прим." SortExpression="Prim" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [Suppliers] WHERE [Id] = @Id" InsertCommand="INSERT INTO [Suppliers] ([Id], [Name], [Details], [Email], [Site], [Phone], [Person], [EmailPerson], [Prim]) VALUES (@Id, @Name, @Details, @Email, @Site, @Phone, @Person, @EmailPerson, @Prim)" SelectCommand="SELECT * FROM [Suppliers]" UpdateCommand="UPDATE [Suppliers] SET [Name] = @Name, [Details] = @Details, [Email] = @Email, [Site] = @Site, [Phone] = @Phone, [Person] = @Person, [EmailPerson] = @EmailPerson, [Prim] = @Prim WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Details" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Site" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Person" Type="String" />
            <asp:Parameter Name="EmailPerson" Type="String" />
            <asp:Parameter Name="Prim" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Details" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Site" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="Person" Type="String" />
            <asp:Parameter Name="EmailPerson" Type="String" />
            <asp:Parameter Name="Prim" Type="String" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

<br />
    </asp:Content>
