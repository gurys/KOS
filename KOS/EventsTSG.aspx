<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EventsTSG.aspx.cs" Inherits="KOS.EventsTSG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>События ТСЖ с превышением нормативного времени</title>
</head>
<body>
    <form id="form1" runat="server">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/logo.png" PostBackUrl="~/ManagerTSG.aspx" />
    <br /><h4>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;События ТСЖ с превышением нормативного времени</h4>
    <div> 
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" AllowPaging="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="№" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="EventId" HeaderText="описание" SortExpression="EventId" />
                <asp:BoundField DataField="DataId" HeaderText="дата/время" SortExpression="DataId" />
                <asp:BoundField DataField="IO" HeaderText="диспетчер" SortExpression="IO" />
                <asp:BoundField DataField="RegistrId" HeaderText="вид услуг" SortExpression="RegistrId" />
                <asp:BoundField DataField="Sourse" HeaderText="источник" SortExpression="Sourse" />
                <asp:BoundField DataField="TypeId" HeaderText="событие" SortExpression="TypeId" />
                <asp:BoundField DataField="LiftId" HeaderText="зона" SortExpression="LiftId" />
                <asp:BoundField DataField="ToApp" HeaderText="назначен" SortExpression="ToApp" />
                <asp:BoundField DataField="DateToApp" HeaderText="дата/время" SortExpression="DateToApp" />
                <asp:BoundField DataField="Comment" HeaderText="Comment" SortExpression="Comment" />
                <asp:BoundField DataField="Who" HeaderText="исполнил" SortExpression="Who" />
                <asp:BoundField DataField="DateWho" HeaderText="дата/время" SortExpression="DateWho" />
                <asp:BoundField DataField="Prim" HeaderText="замечания" SortExpression="Prim" >
                <ItemStyle ForeColor="#CC0000" />
                </asp:BoundField>
                <asp:BoundField DataField="Timing" HeaderText="тайминг" SortExpression="Timing" />
                <asp:BoundField DataField="Address" HeaderText="адрес" SortExpression="Address" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
            SelectCommand="SELECT [Id], [EventId], [RegistrId], [DataId], [Sourse], [IO], [TypeId], [LiftId], [Who], [ToApp], [DateWho], [DateToApp], [Comment], [Timing], [Address], [Prim] FROM [Events] WHERE (([Cansel] = @Cansel) AND ([Family] = @Family) AND ([Timing] &gt;= @Timing))">
            <SelectParameters>
                <asp:Parameter DefaultValue="true" Name="Cansel" Type="Boolean" />
                <asp:Parameter DefaultValue="ODS13" Name="Family" Type="String" />
                <asp:Parameter DefaultValue="0 2:00" Name="Timing" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    </div>
    </form>     
</body>
</html>
