<%@ Page Title="Персонал" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Worker.aspx.cs" Inherits="KOS.Worker" %>
<%@ Register tagprefix="uc" tagname="Works" src="~/Controls/Works.ascx" %>
<%@ Register tagprefix="uc" tagname="MonthPlan" src="~/Controls/MonthPlan.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Персонал</h3>
    <table><tr>
        <td><asp:Button ID="ShowWorkTime" runat="server" Text="Часы работы" OnClick="ShowWorkTime_Click" /></td>
        <td><asp:Button ID="ShowConnections" runat="server" Text="Закрепления" OnClick="ShowConnections_Click" /></td>
        <td><asp:Button ID="ShowHollidays" runat="server" Text="Выходные" OnClick="ShowHollidays_Click" /></td>
    </tr></table>
    Механик: <asp:DropDownList ID="WorkerName" runat="server" OnSelectedIndexChanged="Worker_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    <asp:PlaceHolder ID="WorkTime" runat="server" Visible="false">
        <asp:Label runat="server" ID="Msg" ForeColor="Red"></asp:Label><br />
        <br />Рабочий день с <asp:TextBox ID="From" runat="server" Width="25px"></asp:TextBox> 
        по <asp:TextBox ID="To" runat="server" Width="25px"></asp:TextBox> 
        Перерыв на обед в  <asp:TextBox ID="Lunch" runat="server" Width="25px"></asp:TextBox>
        <br />
        <asp:Button ID="Save" runat="server" Text="Сохранить"  OnClick="Save_Click"/>
        <asp:ListView ID="Graf" runat="server">
            <LayoutTemplate>
                <table border="1" id="tbl1" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td id="Td1" runat="server">Механик</td>
                        <td id="Td2" runat="server">Начало</td>
                        <td id="Td3" runat="server">Конец</td>
                        <td id="Td4" runat="server">Обед</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                    <td>
                        <asp:Label ID="t1" runat="server" Text='<%# Eval("From") %>' />
                    </td>
                    <td>
                        <asp:Label ID="t2" runat="server" Text='<%# Eval("To") %>' />
                    </td>
                    <td>
                        <asp:Label ID="t3" runat="server"><%# Eval("Lunch") %></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="Connections" runat="server" Visible="false">
        Участок: <asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
        Маршрут: <asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
        <asp:ListView ID="IdL" runat="server">
            <LayoutTemplate>
                <table border="1" id="tbl1" runat="server">
                    <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                        <td id="Th1" runat="server">
                            <asp:CheckBox ID="SelectAll" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                        <td id="Th2" runat="server">№ лифта</td>
                        <td id="Td1" runat="server">Адрес</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td>
                        <asp:CheckBox ID="Select" Checked='<%# Eval("Checked") %>' runat="server" Enabled="True" />
                    </td>
                    <td>
                        <asp:Label ID="Title" runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>
                    <td>
                        <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <asp:Button ID="SaveConnections" runat="server" Text="Сохранить" OnClick="SaveConnections_Click" /><br />
        <asp:Button ID="ShowAllConnections" runat="server" Text="Показать все закрепления" OnClick="ShowAllConnections_Click" />
        <asp:PlaceHolder ID="AllConnections" runat="server" Visible="false">
            <asp:ListView ID="AllList" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                            <td id="Th1" runat="server">
                                <asp:CheckBox ID="SelectAll" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                            <td id="Th2" runat="server">№ лифта</td>
                            <td id="Td1" runat="server">Адрес</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:CheckBox ID="Select" Checked='<%# Eval("Checked") %>' runat="server" Enabled="True" />
                        </td>
                        <td>
                            <asp:Label ID="Title" runat="server" Text='<%# Eval("LiftId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <asp:Button ID="SaveAllConnections" runat="server" Text="Сохранить" OnClick="SaveAllConnections_Click" />
        </asp:PlaceHolder>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="phHolliday" Visible="false">
        <asp:DropDownList ID="HollidaysType" runat="server" OnSelectedIndexChanged="HollidaysType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SaveOrClear" runat="server" OnSelectedIndexChanged="SaveOrClear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:PlaceHolder runat="server" ID="phDaysHolliday" Visible="false">
            <asp:PlaceHolder runat="server" ID="DaysHollidayPeriodSetting" Visible="true">
                <table>
                    <tr>
                        <td colspan="2">Период</td>
                    </tr>
                    <tr>
                        <td>C
                            <asp:Calendar ID="DHStart" runat="server"></asp:Calendar>
                        </td>
                        <td>по
                            <asp:Calendar ID="DHEnd" runat="server"></asp:Calendar>
                        </td>
                    </tr>
                </table>
                <asp:Button runat="server" ID="DaysHollidayPeriodSet" Text="Выбрать" OnClick="DaysHollidayPeriodSet_Click" />
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="SetDaysHolliday" Visible="false">
                <asp:ListView ID="DaysHolliday" runat="server">
                    <LayoutTemplate>
                        <table border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style="background-color: #85a6d3">
                                <td id="Th1" runat="server"></td>
                                <td id="Th2" runat="server">Дата</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td>
                                <asp:CheckBox ID="Select" Checked='<%# Eval("Checked") %>' runat="server" Enabled="True" />
                            </td>
                            <td>
                                <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
            <asp:Button runat="server" ID="SaveDaysHolliday" Text="Сохранить" OnClick="SaveDaysHolliday_Click" />
            </asp:PlaceHolder>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phHollidayDaysOfWeek" Visible="false">
            <table>
                <tr>
                    <td colspan="2">Период</td>
                </tr>
                <tr>
                    <td>C
                        <asp:Calendar ID="WeekEndStart" runat="server"></asp:Calendar>
                    </td>
                    <td>по
                        <asp:Calendar ID="WeekEndEnd" runat="server"></asp:Calendar>
                    </td>
                </tr>
            </table>
            <asp:CheckBoxList ID="HollidayDaysOfWeek" runat="server">
                <asp:ListItem>понедельник</asp:ListItem>
                <asp:ListItem>вторник</asp:ListItem>
                <asp:ListItem>среда</asp:ListItem>
                <asp:ListItem>четверг</asp:ListItem>
                <asp:ListItem>пятница</asp:ListItem>
                <asp:ListItem>суббота</asp:ListItem>
                <asp:ListItem>воскресенье</asp:ListItem>
            </asp:CheckBoxList>
            <asp:Button ID="SaveWeekEnd" runat="server" Text="Сохранить" OnClick="SaveWeekEnd_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phHollidayPeriod" Visible="false">
            <table>
                <tr>
                    <td colspan="2">Период</td>
                </tr>
                <tr>
                    <td>C
                        <asp:Calendar ID="HollidayStart" runat="server"></asp:Calendar>
                    </td>
                    <td>по
                        <asp:Calendar ID="HollidayEnd" runat="server"></asp:Calendar>
                    </td>
                </tr>
            </table>
            <asp:Button ID="HollidayPeriodSave" runat="server" Text="Сохранить" OnClick="HollidayPeriodSave_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder ID="phClear" runat="server">
            <table>
                <tr>
                    <td colspan="2">Период</td>
                </tr>
                <tr>
                    <td>C
                        <asp:Calendar ID="ClearStart" runat="server"></asp:Calendar>
                    </td>
                    <td>по
                        <asp:Calendar ID="ClearEnd" runat="server"></asp:Calendar>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Clear" runat="server" Text="Очистить" OnClick="Clear_Click" />
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phHollidaysPeriod2" Visible="false">
            <table>
                <tr>
                    <td colspan="2">Период</td>
                </tr>
                <tr>
                    <td>C
                        <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                    </td>
                    <td>по
                        <asp:Calendar ID="Calendar2" runat="server"></asp:Calendar>
                    </td>
                </tr>
            </table>
            Первый <asp:Label ID="label1" runat="server"></asp:Label>
            <asp:Calendar ID="Calendar3" runat="server"></asp:Calendar>
            <asp:Button ID="H2" runat="server" Text="Установить" OnClick="H2_Click" />
        </asp:PlaceHolder>
    </asp:PlaceHolder>
    <br><br>
    <table>
        <tr style="background-color: #ffffff">
            <td onclick="doExpand(x101)" style="text-align:center; font-size: 18px; font-style: normal; color: #000080; font-weight: normal;">
                График работ на текущий год
            </td>
            </tr><tr><td></td></tr>
    </table>
    <div id="x101" style="display: none">
        <blockquote>
            <table>
                <tr>
                    <td>
                        <uc:MonthPlan ID="MonthPlan1" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan2" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan3" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc:MonthPlan ID="MonthPlan4" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan5" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan6" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc:MonthPlan ID="MonthPlan7" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan8" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan9" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc:MonthPlan ID="MonthPlan10" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan11" runat="server" />
                    </td>
                    <td>
                        <uc:MonthPlan ID="MonthPlan12" runat="server" />
                    </td>
                </tr>
            </table>
            <a onclick="doExpand(x101)" href="#">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align:center">спрятать</td>
                    </tr>
                </table>
            </a>
        </blockquote>
    </div>
</asp:Content>
