<%@ Page Title="Регистрация событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reg_ods.aspx.cs" Inherits="KOS.Reg_ods" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Timer ID="Timer1" runat="server" Interval="600000" ontick="Timer1_Tick"></asp:Timer>
        <asp:Button ID="DoIt" runat="server" class="buttonblue" Visible="false" Text="Показать" OnClick="DoIt_Click" />
        <asp:Label ID="What" runat="server"></asp:Label><br />
        <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #CC0000; color:#FFF;">
                        <td id="td13" runat="server" class="td5px">№</td>
                        <td id="td10" runat="server" class="td5px">Отправил</td>
                        <td id="td1" runat="server" class="td5px">Дата</td>
                        <td id="td2" runat="server" class="td5px">Время</td>
                        <td id="td3" runat="server" class="td5px">Лифт</td>
                        <td id="td4" runat="server" class="td5px">Событие</td>
                        <td id="td11" runat="server" class="td5px">Описание</td>
                        <td id="td5" runat="server" class="td5px">Принял</td>
                        <td id="td6" runat="server" class="td5px">Назначен</td>
                        <td id="td12" runat="server" class="td5px">Комментарий</td>
                        <td id="td7" runat="server" class="td5px">Дата</td>
                        <td id="td8" runat="server" class="td5px">Время</td>
                        <td id="td9" runat="server" class="td5px">Простой</td>
                    </tr>
                    <tr runat="server" id="itemPlaceholder" />
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr id="Tr1" runat="server">
                    <td class="tdwhite">
                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                            <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                        </asp:HyperLink></td>
                    <td class="tdwhite">
                        <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                    </td>
                    <td class="tdwhite">
                            <asp:Label ID="Date1" runat="server" Text='<%# Eval("Date1") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Time1" runat="server" Text='<%# Eval("Time1") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="LiftId" runat="server" Text='<%# Eval("LiftId") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Category" runat="server" Text='<%# Eval("Category") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Text" runat="server" Text='<%# Eval("Text") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Prinyal" runat="server" Text='<%# Eval("Prinyal") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Vypolnil" runat="server" Text='<%# Eval("Vypolnil") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Couse" runat="server" Text='<%# Eval("Couse") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Date2" runat="server" Text='<%# Eval("Date2") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Time2" runat="server" Text='<%# Eval("Time2") %>' />
                    </td>
                    <td class="tdwhite">
                        <asp:Label ID="Prostoy" runat="server" Text='<%# Eval("Prostoy") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView><asp:Button runat="server" PageIndex ="~/KOS.Diagramm.aspx" Width="393px" Text="Регистрация новых событий"  OnClick="DiagrammODS_Click" BackColor="#CC0000" ForeColor="White"/><asp:Button runat="server" PageIndex ="~/KOS.ZakrytieODS.aspx" Width="336px" Text="К закрытию событий"  OnClick="ZakrytieODS_Click" BackColor="#000099" ForeColor="White"/><asp:Button runat="server" PageIndex ="~/KOS.OdsHome.aspx" Width="145px" Text="Домой"  OnClick="OdsHome_Click" BackColor="#666666" ForeColor="White"/></asp:Content>
