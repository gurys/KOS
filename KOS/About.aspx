<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="KOS.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

    <article>
        <p>        
            Доступ к этому сайту только для установленных пользователей.
            Сначала зарегистрируйтесь, а затем обратитесь к администратору для получения прав доступа к необходимым
            ресурсам.
        </p>
    </article>

    <aside>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/About.aspx">About</a></li>
            <li><a runat="server" href="mailto:gurus@emicatech.com">Admin</a></li>
        </ul>
    </aside>
</asp:Content>