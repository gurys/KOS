<%@ Page Title="Отчёт" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="KOS.Reports" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h4>Отчеты и документы</h4>
    <table border="1" id="Table1" runat="server">
        <tr style="background-color: #c5cdf8"><td><a id="A3" runat="server" href="~/ReglamentReport.aspx">Плановые работы</a></td><td><a id="DocumView" runat="server" href="~/BaseDoc.aspx" visible="false">Документы привязанные к Событию</a></td></tr>
        <tr style="background-color: #c5cdf8"><td><a id="A1" runat="server" href="~/IncidentReport.aspx">Инциденты</a></td><td><a id="DocViewUM" runat="server" href="~/BaseDocUM.aspx" visible="false">Документы привязанные к маршруту</a></td></tr>
        <tr style="background-color: #c5cdf8"><td><a id="A2" runat="server" href="~/PrimReport.aspx">Замечания</a></td><td><a id="AdminUM" runat="server" href="~/AdminUM.aspx" visible="false">Приказы, план-графики в .PDF</a></td></tr>
        <tr style="background-color: #c5cdf8"><td><a id="WZReport" runat="server" href="~/WZReport.aspx" visible="false">Заявки</a></td><td><a id="ReportsTSG" runat="server" href="~/ReportsTSG.aspx?t=1" visible="false">Закрытые События за период</a></td></tr>
        <tr style="background-color: #c5cdf8"><td><a id="Lifts" runat="server" href="~/Lifts.aspx" visible="false">Графики</a></td><td><a id="PartsList" runat="server" href="~/PartsList.aspx" visible="false">Запчасти Событий за период</a></td></tr>
        <tr style="background-color: #c5cdf8"></tr>
        <tr style="background-color: #c5cdf8"></tr>
        <tr style="background-color: #c5cdf8"></tr>
    </table>
    
</asp:Content>
