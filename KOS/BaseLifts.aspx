<%@ Page Title="Интерфейс Базы Лифтов" Language="C#"MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BaseLifts.aspx.cs" Inherits="KOS.BaseLifts" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp  База  Лифтов</h3>
        &nbsp; Поля помеченные * не редактировать т.к. это приведет к ошибке программы! <br />
        &nbsp; Номера договоров/модель/марка: <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True" CellPadding="4" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" PageSize="10" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="LiftID" HeaderText="№ лифта *" SortExpression="LiftID" />           
            <asp:BoundField DataField="PlantNum" HeaderText="№ заводской" SortExpression="PlantNum" />
            <asp:BoundField DataField="RegisrerNum" HeaderText="№ регистр." SortExpression="RegisrerNum" />
            <asp:BoundField DataField="ContractNumServise" HeaderText="№ дог.серв." SortExpression="ContractNumServise" />
            <asp:BoundField DataField="NumSupContract" HeaderText="№ контр. пост." SortExpression="NumSupContract" />
            <asp:BoundField DataField="Manufacturer" HeaderText="Марка" SortExpression="Manufacturer" />
            <asp:BoundField DataField="Model" HeaderText="Модель" SortExpression="Model" />
            <asp:BoundField DataField="Id" HeaderText="№ базы *" SortExpression="Id" />
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
        </asp:GridView>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
            SelectCommand="SELECT * FROM [PhisicalAddr]"
            UpdateCommand="UPDATE [PhisicalAddr] SET [LiftID] = @LiftID, [PlantNum] = @PlantNum, [RegisrerNum] = @RegisrerNum, [ContractNumServise] = @ContractNumServise, [NumSupContract] = @NumSupContract, [Manufacturer] = @Manufacturer, [Model] = @Model WHERE [Id] = @Id"
            DeleteCommand="DELETE FROM [PhisicalAddr] WHERE [Id] = @Id" 
            InsertCommand="INSERT INTO [PhisicalAddr] ([LiftID], [PlantNum], [RegisrerNum], [ContractNumServise], [NumSupContract], [Manufacturer], [Model]) VALUES (@LiftID, @PlantNum, @RegisrerNum, @ContractNumServise, @NumSupContract, @Manufacturer, @Model)" >
            <DeleteParameters>
                <asp:Parameter Name="Id" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="LiftID" Type="String" />
                <asp:Parameter Name="PlantNum" Type="String" />
                <asp:Parameter Name="RegisrerNum" Type="String" />
                <asp:Parameter Name="ContractNumServise" Type="String" />
                <asp:Parameter Name="NumSupContract" Type="String" />
                <asp:Parameter Name="Manufacturer" Type="String" />
                <asp:Parameter Name="Model" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="LiftID" Type="String" />
                <asp:Parameter Name="PlantNum" Type="String" />
                <asp:Parameter Name="RegisrerNum" Type="String" />
                <asp:Parameter Name="ContractNumServise" Type="String" />
                <asp:Parameter Name="NumSupContract" Type="String" />
                <asp:Parameter Name="Manufacturer" Type="String" />
                <asp:Parameter Name="Model" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
    </asp:SqlDataSource>
    &nbsp; Адрес установки/этаж/скорость/дата последнего освидетельствования: <br />
    <asp:GridView ID="GridView2" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource2" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" AllowSorting="True">
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="LiftID" HeaderText="№ лифта *" SortExpression="LiftID" />
            <asp:BoundField DataField="Street" HeaderText=" улица" SortExpression="Street" />
            <asp:BoundField DataField="House" HeaderText="№ дома" SortExpression="House" />
            <asp:BoundField DataField="Cas" HeaderText=" корпус" SortExpression="Case" />
            <asp:BoundField DataField="Entrance" HeaderText="№ подъезда" SortExpression="Entrance" />
            <asp:BoundField DataField="Floor" HeaderText=" этаж" SortExpression="Floor" />
            <asp:BoundField DataField="Speed" HeaderText=" скорость" SortExpression="Speed" />
            <asp:BoundField DataField="DateOsvid" HeaderText=" посл. освидет." SortExpression="DateOsvid" />
            <asp:BoundField DataField="Id" HeaderText="№ базы *" SortExpression="Id" />
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
        DeleteCommand="DELETE FROM [PhisicalAddr] WHERE [Id] = @Id" 
        InsertCommand="INSERT INTO [PhisicalAddr] ([LiftID], [PlantNum], [RegisrerNum], [ContractNumServise], [NumSupContract], [Manufacturer], [Model], [Street], [House], [Cas], [Entrance], [Floor], [Speed], [DateOsvid]) VALUES (@LiftID, @PlantNum, @RegisrerNum, @ContractNumServise, @NumSupContract, @Manufacturer, @Model, @Street, @House, @Cas, @Entrance, @Floor, @Speed, @DateOsvid)"
        SelectCommand="SELECT * FROM [PhisicalAddr]" 
        UpdateCommand="UPDATE [PhisicalAddr] SET [LiftID] = @LiftID, [Street] = @Street, [House] = @House, [Cas] = @Cas, [Entrance] = @Entrance, [Floor] = @Floor, [Speed] = @Speed, [DateOsvid] = @DateOsvid WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="LiftID" Type="String" />
            <asp:Parameter Name="PlantNum" Type="String" />
            <asp:Parameter Name="RegisrerNum" Type="String" />
            <asp:Parameter Name="ContractNumServise" Type="String" />
            <asp:Parameter Name="NumSupContract" Type="String" />
            <asp:Parameter Name="Manufacturer" Type="String" />
            <asp:Parameter Name="Model" Type="String" />
            <asp:Parameter Name="Street" Type="String" />
            <asp:Parameter Name="House" Type="String" />
            <asp:Parameter Name="Cas" Type="String" />
            <asp:Parameter Name="Entrance" Type="String" />
            <asp:Parameter Name="Floor" Type="String" />
            <asp:Parameter Name="Speed" Type="String" />
            <asp:Parameter Name="DateOsvid" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="LiftID" Type="String" />
            <asp:Parameter Name="PlantNum" Type="String" />
            <asp:Parameter Name="RegisrerNum" Type="String" />
            <asp:Parameter Name="ContractNumServise" Type="String" />
            <asp:Parameter Name="NumSupContract" Type="String" />
            <asp:Parameter Name="Manufacturer" Type="String" />
            <asp:Parameter Name="Model" Type="String" />
            <asp:Parameter Name="Street" Type="String" />
            <asp:Parameter Name="House" Type="String" />
            <asp:Parameter Name="Cas" Type="String" />
            <asp:Parameter Name="Entrance" Type="String" />
            <asp:Parameter Name="Floor" Type="String" />
            <asp:Parameter Name="Speed" Type="String" />
            <asp:Parameter Name="DateOsvid" Type="String" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <br />
    </asp:Content>