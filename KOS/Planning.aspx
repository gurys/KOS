<%@ Page Title="Работы" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planning.aspx.cs" Inherits="KOS.Planning" %>
<%@ Register tagprefix="uc" tagname="MonthPlan" src="~/Controls/MonthPlan.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Работы</h3>
    <asp:Label runat="server" ID="Msg" ForeColor="Red"></asp:Label><br />
    <asp:Button ID="Zayavka" runat="server" Text="Заявка на внеплановые работы" OnClick="Zayavka_Click" /><br />
    Механик: <asp:DropDownList ID="Worker" runat="server" OnSelectedIndexChanged="Worker_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
    Тип работ: <asp:DropDownList ID="WorkType" runat="server" OnSelectedIndexChanged="WorkType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:DropDownList ID="SaveOrClear" runat="server" OnSelectedIndexChanged="SaveOrClear_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <br />Рабочее время с <asp:Label runat="server" ID="WorkFrom"></asp:Label> по <asp:Label runat="server" ID="WorkTo"></asp:Label>, обед <asp:Label runat="server" ID="WorkLunch"></asp:Label><br />
    <asp:PlaceHolder runat="server" ID="phSave" Visible="false">
        <asp:PlaceHolder runat="server" ID="phTp" Visible="false">
            <table>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label runat="server" ID="Year"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="Month" runat="server" OnSelectedIndexChanged="Month_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Ф.И.О</td>
                                <td><asp:Label runat="server" ID="Fio"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>№ участка</td>
                                <td>
                                    <asp:DropDownList ID="IdUM" runat="server" OnSelectedIndexChanged="IdUM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>№ лифта</td>
                                <td>
                                    <asp:DropDownList ID="ddlLiftId" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Дата</td>
                                <td>
                                    <asp:DropDownList ID="ddlDate" runat="server"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Время проведения работ</td>
                                <td>
                                    с <asp:TextBox ID="t1" runat="server" Width="25px"></asp:TextBox> по 
                                    <asp:TextBox ID="t2" runat="server" Width="25px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnSave" runat="server" Text="Установить" OnClick="btnSave_Click"  ForeColor="White" BackColor="DimGray"/></td>
                            </tr>
                        </table>
                    </td>
                    <td rowspan="2">
                        <asp:ListView ID="TpPlan" runat="server">
                            <LayoutTemplate>
                                <table border="1" id="tbl1" runat="server">
                                    <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                                      
                                        <td id="Td3" runat="server">Лифт</td>
                                        <td id="Td4" runat="server">Тип работ</td>
                                        <td id="Td5" runat="server">Дата</td>
                                        <td id="Td6" runat="server" style="width: 3px">8</td>
                                        <td id="Td7" runat="server" style="width: 3px">9</td>
                                        <td id="Td8" runat="server" style="width: 3px">10</td>
                                        <td id="Td9" runat="server" style="width: 3px">11</td>
                                        <td id="Td10" runat="server" style="width: 3px">12</td>
                                        <td id="Td11" runat="server" style="width: 3px">13</td>
                                        <td id="Td12" runat="server" style="width: 3px">14</td>
                                        <td id="Td13" runat="server" style="width: 3px">15</td>
                                        <td id="Td14" runat="server" style="width: 3px">16</td>
                                        <td id="Td1" runat="server" style="width: 3px">17</td>
                                        <td id="Td17" runat="server" style="width: 3px">18</td>
                                        <td id="Td15" runat="server" style="width: 3px">19</td>
                                        <td id="Td16" runat="server" style="width: 3px">20</td>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr1" runat="server">
                                   
                                    <td>
                                        <asp:Label ID="IdL" runat="server" Text='<%# Eval("LiftId") %>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="TpType" runat="server" Text='<%# Eval("TpId") %>' />
                                    </td>
                                    <td style="text-wrap:none">
                                        <asp:Label ID="Day" runat="server"><%# Eval("Date") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H08" runat="server"><%# Eval("H08") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H09" runat="server"><%# Eval("H09") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H10" runat="server"><%# Eval("H10") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H11" runat="server"><%# Eval("H11") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H12" runat="server"><%# Eval("H12") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H13" runat="server"><%# Eval("H13") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H14" runat="server"><%# Eval("H14") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H15" runat="server"><%# Eval("H15") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H16" runat="server"><%# Eval("H16") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H17" runat="server"><%# Eval("H17") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H18" runat="server"><%# Eval("H18") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H19" runat="server"><%# Eval("H19") %></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="H20" runat="server"><%# Eval("H20") %></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc:MonthPlan ID="MonthPlan13" runat="server" />
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="phWork" Visible="false">
            <asp:DropDownList ID="AddWorkSelector" runat="server" OnSelectedIndexChanged="AddWorkSelector_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:PlaceHolder runat="server" ID="phWork1">
                <asp:Calendar ID="WorkDay" runat="server"></asp:Calendar>
                Участок: <asp:DropDownList ID="IdU" runat="server" OnSelectedIndexChanged="IdU_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                Маршрут: <asp:DropDownList ID="IdM" runat="server" OnSelectedIndexChanged="IdM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                            <asp:ListView ID="IdL" runat="server">
                                <LayoutTemplate>
                                    <table border="1" id="tbl1" runat="server">
                                        <tr id="Tr2" runat="server" style="background-color: #98FB98">
                                            <td id="Th1" runat="server">
                                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                                            <td id="Th2" runat="server">№ лифта</td>
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
                Время: с <asp:TextBox ID="WorkTime" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                по <asp:TextBox ID="WorkDuration" runat="server" Width="50px"></asp:TextBox> ч.
                <br /><asp:Button ID="SaveWork" runat="server" Text="Сохранить" OnClick="SaveWork_Click" />
            </asp:PlaceHolder>
            <asp:PlaceHolder runat="server" ID="phWork2" Visible="false">
                <table>
                    <tr>
                        <td colspan="2">Период</td>
                    </tr>
                    <tr>
                        <td>C
                            <asp:Calendar ID="WorkPeriod" runat="server"></asp:Calendar>
                        </td>
                        <td>по
                            <asp:Calendar ID="WorkPeriodEnd" runat="server"></asp:Calendar>
                        </td>
                    </tr>
                </table>
                <asp:CheckBoxList ID="WorkDayOfWeek" runat="server">
                    <asp:ListItem>понедельник</asp:ListItem>
                    <asp:ListItem>вторник</asp:ListItem>
                    <asp:ListItem>среда</asp:ListItem>
                    <asp:ListItem>четверг</asp:ListItem>
                    <asp:ListItem>пятница</asp:ListItem>
                    <asp:ListItem>суббота</asp:ListItem>
                    <asp:ListItem>воскресенье</asp:ListItem>
                </asp:CheckBoxList>
                Участок: <asp:DropDownList ID="IdU2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="IdU2_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                Маршрут: <asp:DropDownList ID="IdM2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="IdM2_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;
                            <asp:ListView ID="IdL2" runat="server">
                                <LayoutTemplate>
                                    <table border="1" id="tbl1" runat="server">
                                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                                            <td id="Th1" runat="server">
                                                <asp:CheckBox ID="SelectAll" Checked="true" runat="server" Enabled="True" AutoPostBack="True" OnCheckedChanged="SelectAll_CheckedChanged" /></td>
                                            <td id="Th2" runat="server">№ лифта</td>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholder" />
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr id="Tr1" runat="server">
                                        <td>
                                            <asp:CheckBox ID="Select" Checked="true" runat="server" Enabled="True" />
                                        </td>
                                        <td>
                                            <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:ListView>
                Время: с <asp:TextBox ID="WorkTime2" runat="server" Width="50px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                по <asp:TextBox ID="WorkDuration2" runat="server" Width="50px"></asp:TextBox> ч.
                <br />
                <asp:Button ID="SaveWorkPeriod" runat="server" Text="Сохранить" OnClick="SaveWorkPeriod_Click" />
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
    <br><br>
    <table>
        <tr style="background-color: #ffffff">
            <td onclick="doExpand(x101)" style="font-size: 18px; color: #000000; background-color: #FFFFFF; font-weight: normal;">
                 Помесячный График ТР на текущий год       
            </td><td style="background-image: none; font-size: 10px;">  </td>
            <td onclick="doExpand(x102)" style="font-size: 18px; color: #000080; background-color: #FFFFFF; font-weight: normal;">
                 Общий График ТР на текущий год                   
            </td>
        </tr>
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
    <div id="x102" style="display: none">
        <blockquote>
            <asp:ListView ID="LiftsPlan" runat="server">
                <LayoutTemplate>
                    <table border="1" id="tbl1" runat="server">
                        <tr id="Tr2" runat="server" style="background-color: #8dadd0">
                            <td runat="server">Лифт</td>
                            <td runat="server">Адрес</td>
                            <td runat="server">Январь</td>
                            <td runat="server">Февраль</td>
                            <td runat="server">Март</td>
                            <td runat="server">Апрель</td>
                            <td runat="server">Май</td>
                            <td runat="server">Июнь</td>
                            <td runat="server">Июль</td>
                            <td runat="server">Август</td>
                            <td runat="server">Сентябрь</td>
                            <td runat="server">Октябрь</td>
                            <td runat="server">Ноябрь</td>
                            <td runat="server">Декабрь</td>
                        </tr>
                        <tr runat="server" id="itemPlaceholder" />
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr id="Tr1" runat="server">
                        <td>
                            <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                        </td>
                        <td>
                            <asp:Label ID="Address" runat="server" Text='<%# Eval("Address") %>' />
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton0" runat="server" PostBackUrl='<%# Eval("M01url") %>'><%# Eval("M01") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%# Eval("M02url") %>'><%# Eval("M02") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%# Eval("M03url") %>'><%# Eval("M03") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl='<%# Eval("M04url") %>'><%# Eval("M04") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl='<%# Eval("M05url") %>'><%# Eval("M05") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl='<%# Eval("M06url") %>'><%# Eval("M06") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl='<%# Eval("M07url") %>'><%# Eval("M07") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton7" runat="server" PostBackUrl='<%# Eval("M08url") %>'><%# Eval("M08") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton8" runat="server" PostBackUrl='<%# Eval("M09url") %>'><%# Eval("M09") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton9" runat="server" PostBackUrl='<%# Eval("M10url") %>'><%# Eval("M10") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton10" runat="server" PostBackUrl='<%# Eval("M11url") %>'><%# Eval("M11") %></asp:LinkButton>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton11" runat="server" PostBackUrl='<%# Eval("M12url") %>'><%# Eval("M12") %></asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <a onclick="doExpand(x102)" href="#">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align:center">спрятать</td>
                    </tr>
                </table>
            </a>
        </blockquote>
    </div>
</asp:Content>
