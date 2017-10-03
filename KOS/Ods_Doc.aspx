<%@ Page Title="Не подписанные Документы" Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Ods_Doc.aspx.cs" Inherits="KOS.Ods_Doc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="/Scripts/kos.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
      &nbsp;&nbsp;<h4>Документы на подпись:</h4><br /> 
   
<asp:ListView ID="Out1" runat="server">
                    <LayoutTemplate>
                        <table  id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #b59bed; color: #000000;">
                                <td id="td1" runat="server" class="td5px">№ события:</td>
                                <td id="td3" runat="server" class="td5px">документ:</td>                                                               
                                <td id="td5" runat="server" class="td5px">имя файла:</td>
                                <td id="td2" runat="server" class="td5px">кто прислал:</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style ="background-color: #e1ebf2; color: #060f3d;"> 
                             <td class="tdwhite" >                                
                                <asp:Label ID="Nev" runat="server" Text='<%# Eval("Nev") %>' />
                            </td>                          
                            <td class="tdwhite" >                                
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" > 
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("NameFile") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >                                
                                <asp:Label ID="Usr" runat="server" Text='<%# Eval("Usr") %>' />
                            </td>
                        </tr></ItemTemplate>
</asp:ListView><br />
<!--    По неисправностям обнаруженным механиком по лифтам:
    <asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #339933; color: #000000;">
                                <td id="td1" runat="server" class="td5px">№ события:</td>
                                <td id="td3" runat="server" class="td5px">документ:</td>                                                               
                                <td id="td5" runat="server" class="td5px">имя файла:</td>
                                <td id="td2" runat="server" class="td5px">кто прислал:</td>
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server" style ="background-color: #e1ebf2; color: #060f3d;"> 
                             <td class="tdwhite" >                                
                                <asp:Label ID="Nev" runat="server" Text='<%# Eval("Nev") %>' />
                            </td>                          
                            <td class="tdwhite" >                                
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" > 
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("NameFile") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >                                
                                <asp:Label ID="Usr" runat="server" Text='<%# Eval("Usr") %>' />
                            </td>
                        </tr></ItemTemplate>
</asp:ListView>  -->
    <br />
        &nbsp;&nbsp;<br />
</asp:Content>
