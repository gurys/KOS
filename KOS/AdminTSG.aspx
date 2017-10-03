<%@ Page Title="Администрирование" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminTSG.aspx.cs" Inherits="KOS.AdminTSG" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <p><br /> <asp:Label runat="server" ID="Text" ForeColor="Blue" meta:resourcekey="MsgResource1"></asp:Label><br />       
        <asp:PlaceHolder ID = "Vpin" runat ="server">
       <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
        Введите пароль:<br />           
        <asp:TextBox ID="Pn" runat="server" TextMode ="Password" Width ="60px"></asp:TextBox>        
        
        <asp:Button ID="Button1" runat="server" OnClick ="Button1_Click" Text="ВВОД" Width ="65px" /></asp:PlaceHolder>
        <br />
        <asp:PlaceHolder ID="PinAdmin" runat="server" Visible ="false">
         Поле "роль" не править!<br />
         ТАБЛИЦА пользователей, имеющих право на внесение замечаний: <br />          
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="surname" HeaderText="Фамилия:" SortExpression="surname" />
                <asp:BoundField DataField="name" HeaderText="Имя:" SortExpression="name" />
                <asp:BoundField DataField="midlename" HeaderText="Отчество:" SortExpression="midlename" />
                <asp:BoundField DataField="education" HeaderText="пин-код:" SortExpression="education" />                                
                <asp:BoundField DataField="specialty" HeaderText="роль:" SortExpression="specialty" />
                
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
              
             InsertCommand="INSERT INTO [People] ([Id], [surname], [name], [midlename], [birthday], [phones], [specialty], [education], [comments]) VALUES (@Id, @surname, @name, @midlename, @birthday, @phones, @specialty, @education, @comments)"
             SelectCommand="SELECT * FROM [People] WHERE ([comments] = @comments)"
             UpdateCommand="UPDATE [People] SET [surname] = @surname, [name] = @name, [midlename] = @midlename, [birthday] = @birthday, [phones] = @phones, [specialty] = @specialty, [education] = @education WHERE [Id] = @Id">
            
            <InsertParameters>
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="Корона_1" Name="comments" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
</asp:PlaceHolder>
    </p>
    
    <p> <asp:PlaceHolder ID="PinDisp" runat="server" Visible ="false">
        ТАБЛИЦА данных диспетчеров ОДС:
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="Id" DataSourceID="SqlDataSource2" GridLines="None">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="surname" HeaderText="Фамилия:" SortExpression="surname" />
                <asp:BoundField DataField="name" HeaderText="Имя:" SortExpression="name" />
                <asp:BoundField DataField="midlename" HeaderText="Отчество:" SortExpression="midlename" />
                <asp:BoundField DataField="education" HeaderText="пин-код:" SortExpression="education" />                              
                <asp:BoundField DataField="specialty" HeaderText="роль:" SortExpression="specialty" />
                
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
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
             
             InsertCommand="INSERT INTO [People] ([Id], [surname], [name], [midlename], [birthday], [phones], [specialty], [education], [comments]) VALUES (@Id, @surname, @name, @midlename, @birthday, @phones, @specialty, @education, @comments)"
             SelectCommand="SELECT * FROM [People] WHERE ([comments] = @comments)"
             UpdateCommand="UPDATE [People] SET [surname] = @surname, [name] = @name, [midlename] = @midlename, [birthday] = @birthday, [phones] = @phones, [specialty] = @specialty, [education] = @education WHERE [Id] = @Id">
            
            <InsertParameters>
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="ODS14" Name="comments" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
        </asp:PlaceHolder>
        </p>
       <p>
        <asp:PlaceHolder ID="PinAdmin1" runat="server" Visible ="false">      
         Поле "роль" не править!<br />
         ТАБЛИЦА пользователей, имеющих право на внесение замечаний: <br />          
         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSource3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="surname" HeaderText="Фамилия:" SortExpression="surname" />
                <asp:BoundField DataField="name" HeaderText="Имя:" SortExpression="name" />
                <asp:BoundField DataField="midlename" HeaderText="Отчество:" SortExpression="midlename" />
                <asp:BoundField DataField="education" HeaderText="пин-код:" SortExpression="education" />                                
                <asp:BoundField DataField="specialty" HeaderText="роль:" SortExpression="specialty" />
                
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"
              
             InsertCommand="INSERT INTO [People] ([Id], [surname], [name], [midlename], [birthday], [phones], [specialty], [education], [comments]) VALUES (@Id, @surname, @name, @midlename, @birthday, @phones, @specialty, @education, @comments)"
             SelectCommand="SELECT * FROM [People] WHERE ([comments] = @comments)"
             UpdateCommand="UPDATE [People] SET [surname] = @surname, [name] = @name, [midlename] = @midlename, [birthday] = @birthday, [phones] = @phones, [specialty] = @specialty, [education] = @education WHERE [Id] = @Id">
            
            <InsertParameters>
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="Миракс_Парк" Name="comments" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
</asp:PlaceHolder>
    </p>
    
    <p>
        <asp:PlaceHolder ID="PinDisp1" runat="server" Visible ="false">
        ТАБЛИЦА данных диспетчеров ОДС:
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" DataKeyNames="Id" DataSourceID="SqlDataSource4" GridLines="None">
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="surname" HeaderText="Фамилия:" SortExpression="surname" />
                <asp:BoundField DataField="name" HeaderText="Имя:" SortExpression="name" />
                <asp:BoundField DataField="midlename" HeaderText="Отчество:" SortExpression="midlename" />
                <asp:BoundField DataField="education" HeaderText="пин-код:" SortExpression="education" />                              
                <asp:BoundField DataField="specialty" HeaderText="роль:" SortExpression="specialty" />
                
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
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
             
             InsertCommand="INSERT INTO [People] ([Id], [surname], [name], [midlename], [birthday], [phones], [specialty], [education], [comments]) VALUES (@Id, @surname, @name, @midlename, @birthday, @phones, @specialty, @education, @comments)"
             SelectCommand="SELECT * FROM [People] WHERE ([comments] = @comments)"
             UpdateCommand="UPDATE [People] SET [surname] = @surname, [name] = @name, [midlename] = @midlename, [birthday] = @birthday, [phones] = @phones, [specialty] = @specialty, [education] = @education WHERE [Id] = @Id">
            
            <InsertParameters>
                <asp:Parameter Name="Id" Type="Int32" />
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
            </InsertParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="ODS13" Name="comments" Type="String" />
            </SelectParameters>
            <UpdateParameters>
                <asp:Parameter Name="surname" Type="String" />
                <asp:Parameter Name="name" Type="String" />
                <asp:Parameter Name="midlename" Type="String" />
                <asp:Parameter DbType="Date" Name="birthday" />
                <asp:Parameter Name="phones" Type="String" />
                <asp:Parameter Name="specialty" Type="String" />
                <asp:Parameter Name="education" Type="String" />
                <asp:Parameter Name="comments" Type="String" />
                <asp:Parameter Name="Id" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>
       </asp:PlaceHolder> 
    </p>

    </asp:Content>