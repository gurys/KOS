<%@Page Title="Администрирование" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="adminUM.aspx.cs" Inherits="KOS.adminUM" %>
<%@ Register tagprefix="uc" tagname="DatePicker" src="~/Controls/DatePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h4>Выбор данных для формирования графиков и приказов:</h4><br />
   Год:<asp:DropDownList ID="Year" runat="server"></asp:DropDownList>
   Месяц:<asp:DropDownList ID="Month" runat="server" OnSelectedIndexChanged="Month_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
   День:<asp:DropDownList ID="Day" runat="server"></asp:DropDownList> 
    <br /><br />
   Участок: <asp:DropDownList ID="IdU" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="IdU_SelectedIndexChanged"></asp:DropDownList>
   Маршрут: <asp:DropDownList ID="IdM" Height="25px" runat="server" AutoPostBack="true" OnTextChanged="IdM_SelectedIndexChanged"></asp:DropDownList> 
   Эскалаторы уч.2/2:<asp:CheckBox ID="CheckBox1" runat="server" /><br />
<!--   Лифт:<asp:DropDownList ID="LiftId" runat="server" AutoPostBack="true" OnTextChanged ="LiftId_SelectedIndexChanged"></asp:DropDownList> -->
   Электромеханик:<asp:DropDownList ID="To" runat="server"></asp:DropDownList>
   Мастер участка: <asp:DropDownList ID="ddProrab" runat="server"></asp:DropDownList>
   Главный инженер:<asp:DropDownList ID="ddGlIng" runat="server"></asp:DropDownList>
    <br />
    <br />
  <!--  Ввести номер приказа вручную:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br /> -->
    <asp:Button ID="PrWork" runat="server" Text="Приказ о закреплении электромеханика"  OnClick="PrWork_Click"/>&nbsp;&nbsp;
    <asp:Button ID="PrProrab" runat="server" Text="Приказ об организации ТО"  OnClick="PrProrab_Click" />
    <br /><br />
    Прежде, чем формировать план-графики, необходимо спланировать работы на все лифты участка.
    <br />
    <asp:Button ID="Button1" runat="server" Text="Показать спланированные ТР на месяц"  OnClick="Button1_Click"/>&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server" Text="Показать Годовой план ТР" OnClick="Button2_Click"/><br />
    <br />
    <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
    <asp:Button ID="MonthSchedule" runat="server" Text="Сформировать План-График на месяц" OnClick="MonthSchedule_Click" />&nbsp;&nbsp;
    <asp:Button ID="YearSchedule" runat="server" Text="Сформировать График ТР на год" OnClick="YearSchedule_Click" /><br />
    <asp:PlaceHolder ID="phOut" runat="server" Visible="false">
        Данные для план-графика:
     <asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                                <td id="td13" runat="server" class="td5px">Лифт</td>
                                <td id="td1" runat="server" class="td5px">ТРы</td>
                                <td id="td3" runat="server" class="td5px">Дата нач.</td>
                                <td id="td4" runat="server" class="td5px">Дата окон.</td>
                                <td id="td11" runat="server" class="td5px">Выполнил</td>
                                <td id="td5" runat="server" class="td5px">Принял</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >
                                <asp:Label ID="Lift" runat="server" Text='<%# Eval("LiftId") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="TpId" runat="server" Text='<%# Eval("TpId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="DateEnd" runat="server" Text='<%# Eval("DateEnd") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Vyp" runat="server" Text='<%# Eval("Vyp") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="Prn" runat="server" Text='<%# Eval("Prn") %>' />
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
        </asp:PlaceHolder>
     <asp:PlaceHolder ID="phOut1" runat="server" Visible="false">
         Годовой ТР-план:
     <asp:ListView ID="Out1" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #00ff21; color: #060f3d;">
                                <td id="td13" runat="server" class="td5px">Лифт</td>
                                <td id="td1" runat="server" class="td5px">ТРы</td>
                                <td id="td3" runat="server" class="td5px">Дата</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >
                                <asp:Label ID="Lift" runat="server" Text='<%# Eval("LiftId") %>' />
                            </td>
                            <td class="tdwhite">
                                <asp:Label ID="TpId" runat="server" Text='<%# Eval("TpId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
        </asp:PlaceHolder>
    </asp:Content>