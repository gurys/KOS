<%@ Page Title="Закрытие" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZakrytieODS.aspx.cs" Inherits="KOS.ZakrytieODS" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <asp:Button ID="DoIt" runat="server" class="buttonblue" Visible="false" Text="Показать" OnClick="DoIt_Click" />
 Дежурная служба:
    <script type="text/javascript">
        var str = '<%= User.Identity.Name%>';
        if (str === "ODS21" || str === "ODS22" || str === "ODS23" || str === "ODS24" || str === "ODS31" || str === "ODS32") {
            document.write("уч. 2., 3..");
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/5c336450-e07a-49fc-a3b6-22f3319265c6.js' type='text/javascript'%3E%3C/script%3E"));
        }
        else if (str === "ODS13") {
            document.write("уч. 1.3");
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/8c8f5b64-0cd9-4c85-9031-ef69020da832.js' type='text/javascript'%3E%3C/script%3E"));
        }
        else if (str === "ODS14") {
            document.write("уч. 1.4");
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/6387a5e8-3b95-4b9e-9bc7-bde50ac40658.js' type='text/javascript'%3E%3C/script%3E"));
        }
        else if (str === "ODS42" || str === "ODS41" || str === "ODS_test") {
            document.write("уч. 4.2, 4.1");
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/5602f514-bd81-42ba-a520-f71ed5c44cdd.js' type='text/javascript'%3E%3C/script%3E"));
        }
        else if (str === "ODS11" || str === "ODS12" || str === "ODS15" || str === "ODS17") {
            document.write("уч. 1.1, 1.2, 1.5, 1.7");
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/dd9cca3e-dbe6-498b-94e5-59192b61b975.js' type='text/javascript'%3E%3C/script%3E"));
        }
        else {
            document.write(str.toUpperCase());
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//yandex.mightycall.ru/c2c/js/MightyCallC2C_5.4.js' type='text/javascript'%3E%3C/script%3E"));
            document.write(unescape("%3Cscript src='" + ((document.location.protocol.indexOf("file") !== -1) ? "http:" : document.location.protocol) + "//mightycallstorage.blob.core.windows.net/c2cjss/4e7f6cc9-96fc-4e4b-88e6-f4c4dcd8d55f.js' type='text/javascript'%3E%3C/script%3E"));
        }
</script>
<script type="text/javascript">
    InitClick2Call("en");
</script>
    <br />
        <asp:Label ID="What" runat="server"></asp:Label><br />
        <asp:ListView ID="Out" runat="server">
            <LayoutTemplate>
                <table id="tblr" runat="server" cellpadding="3">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">
                        <td id="td13" runat="server" class="td5px">№</td>
                        <td id="td10" runat="server" class="td5px">Отправил</td>
                        <td id="td1" runat="server" class="td5px">Дата</td>
                        <td id="td2" runat="server" class="td5px">Время</td>
                        <td id="td3" runat="server" class="td5px">Лифт</td>
                        <td id="td4" runat="server" class="td5px">Событие</td>
                        <td id="td11" runat="server" class="td5px">Описание</td>
                        <td id="td5" runat="server" class="td5px">Принял</td>
                        <td id="td14" runat="server" class="td5px">Дата/время</td>
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
                        </asp:HyperLink>
                    </td>
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
                        <asp:Label ID="StartPrinyal" runat="server" Text='<%# Eval("StartPrinyal") %>' />
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
        </asp:ListView>
</asp:Content>
