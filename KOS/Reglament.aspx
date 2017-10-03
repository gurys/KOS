<%@ Page Title="Регламент работ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reglament.aspx.cs" Inherits="KOS.Reglament" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
    <asp:Label ID="Msg" runat="server" ForeColor="Red"></asp:Label><br />
    № лифтa: <asp:Literal ID="LiftsId" runat="server"></asp:Literal> Вид работ: <asp:Literal ID="TpId" runat="server"></asp:Literal>
    <asp:ListView ID="ReglamentWorks" runat="server">
        <LayoutTemplate>
          <table cellpadding="2" border="1" ID="tbl1" runat="server">
            <tr id="Tr2" runat="server" style="background-color: #8dadd0">
              <td id="Th1" runat="server">Регламент работ</td>
            </tr>
            <tr runat="server" id="itemPlaceholder" /> 
          </table>
        </LayoutTemplate>
        <ItemTemplate>
          <tr id="Tr1" runat="server">
            <td>
              <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />
            </td>
          </tr>
        </ItemTemplate>
    </asp:ListView>
    <!--                <asp:ListView ID="Out" runat="server">
                            <LayoutTemplate>
                                <table border="1" id="tbl1" runat="server">
                                    <tr id="Tr2" runat="server" style="background-color: #98FB98">
                                        <td id="Td2" runat="server">Описание внеплановых работ</td>
                                        </tr>
                                    <tr runat="server" id="itemPlaceholder" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr id="Tr1" runat="server">
                                    <td>
                                        <asp:Label ID="Title" runat="server" Text='<%# Eval("Title") %>' />                                        <td></td>
                                   </td>
                                </tr>                              
                            </ItemTemplate>
                        </asp:ListView> -->
   <asp:ListView ID="ZPrim" runat="server">
        <LayoutTemplate>
          <table  border="1" ID="tbl1" runat="server">
            <tr id="Tr2" runat="server" style="background-color: #8dadd0">
              <td id="Th1" runat="server">Замечания</td>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
          </table>
        </LayoutTemplate>
        <ItemTemplate>
          <tr id="Tr1" runat="server">
            <td>
              <asp:LinkButton ID="Title" runat="server" Text="замечание" PostBackUrl='<%# Eval("Url") %>' />
            </td>
          </tr>
        </ItemTemplate>
    </asp:ListView> 

                                   
    <asp:CheckBox ID="Done" runat="server" Text="Выполнил" /><asp:CheckBox ID="Prin" runat="server" Text="Принял" Visible="false" />
    <br />
    <asp:Label ID="L1" runat="server" Text="Ввелите описание ВР (блок не более 256 символов!) и нажмите +, если больше введите следующий блок:" Visible="false" ></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server" Width="700"  Height="30" TextMode="MultiLine" BackColor="#ccccff" Visible="false"  ></asp:TextBox>
    <asp:Button ID="AddBP" runat="server" Text="+" Width="40" Height="40" BackColor="#ccccff" OnClick="AddBP_Click" Visible="false" />
    <br /> <br />
    <asp:Button ID="AddZPrim" runat="server" Text="Добавить замечание" BackColor="#ffcc99" OnClick="AddZPrim_Click" />
    &nbsp;&nbsp;<asp:Button ID="Save" runat="server" Text="Сохранить" OnClick="Save_Click" />
    &nbsp;&nbsp;<asp:Button ID="btnPrin" runat="server" Text="Принять" BackColor="#99ffcc" OnClick="Prin_Click" Visible="false" />
    &nbsp;&nbsp;<asp:Button ID="Grafik" runat="server" Text="График" OnClick="Grafik_Click" />
</asp:Content>
