<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="KOS.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Контакты.</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>телeфоны:</h3>
        </header>
        <p>
            <span class="label">Main:</span>
            <span>+7926.933.8001</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>+7926.406.2614</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Support@example.com">office@emicatech.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@example.com">gurus@emicatech.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@example.com">avikulov@emicatech.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            г.Москва<br />
            
        </p>
    </section>
</asp:Content>