<%@ Page Title="Интерфейс Базы Запчастей" Language="C#"MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Equipment.aspx.cs" Inherits="KOS.Equipment" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; База Оборудования Лифтов</h3>
   <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" 
         SelectCommand="SELECT [Id], [Manufacturer], [Model], [Area], [Node], [Pozition], [Name], [NumID] FROM [EquipmentBase]"
         UpdateCommand="UPDATE EquipmentBase SET Manufacturer=@Manufacturer, Model=@Model, Area=@Area, Node=@Node, Pozition=@Pozition, Name=@Name, NumID=@NumID
		 FROM EquipmentBase WHERE ID=@ID" />
        <asp:GridView ID="grid" runat="server" DataSourceID="SqlDataSource1" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" AllowSorting="True" >
            <Columns>
                <asp:CommandField ShowEditButton="True" />
                <asp:BoundField DataField="Id" HeaderText="№ " InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                <asp:BoundField DataField="Manufacturer" HeaderText="Марка" SortExpression="Manufacturer" />
                <asp:BoundField DataField="Model" HeaderText="Модель" SortExpression="Model" />
                <asp:BoundField DataField="Area" HeaderText="Зона" SortExpression="Area" />
                <asp:BoundField DataField="Node" HeaderText="Узел" SortExpression="Node" />
                <asp:BoundField DataField="Pozition" HeaderText="Позиция" SortExpression="Pozition" />
                <asp:BoundField DataField="Name" HeaderText="Наименование" SortExpression="Name" />
                <asp:BoundField DataField="NumID" HeaderText="ID" SortExpression="NumID" />
                
            </Columns>
    </asp:GridView>
    
     <h5>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Новая запись в Базу оборудования</h5>
    <br \>
     <div> 
    Участок:<asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    Маршрут:<asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    Лифт:<asp:DropDownList ID="LiftId" runat="server"></asp:DropDownList><br \>
    Производитель, Модель:<asp:Literal ID="Disp" runat="server" meta:resourcekey="DispResource1"></asp:Literal><br \><br \>
       Зона: <asp:DropDownList ID="Area" runat="server" ></asp:DropDownList>
       Узел:<asp:DropDownList ID="Node" runat="server"></asp:DropDownList>
       Позиция <asp:DropDownList ID="Pozition" runat="server"></asp:DropDownList>
         <br /><br \>
 Наименование: <asp:TextBox ID="Text4" runat="server" Height="20px" Width="275px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox>
ID: <asp:TextBox ID="Text5" runat="server" Height="20px" Width="275px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox><br />
        <asp:Button ID="Save" runat="server" class="buttonblue" Text="Внести в Базу" OnClick="Save_Click" BackColor="#666666" BorderStyle="Double" ForeColor="#FFFF99" Height="44px" Width="295px" meta:resourcekey="SaveResource1" />
        
    </div>

    </asp:Content>
