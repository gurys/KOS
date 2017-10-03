<%@ Page Title="Редактирование/Закрытие События" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CloseEvent.aspx.cs" Inherits="KOS.CloseEvent" %>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    Выбрано событие для редактирования/закрытия:<br />
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #77a3cc; color:#000;">

                        <td id="td1" runat="server" class="td5px">№ события:</td>
                        <td id="td10" runat="server" class="td5px">источник:</td>
                        <td id="td2" runat="server" class="td5px">дата/время:</td> 
                        <td id="td5" runat="server" class="td5px">лифт:</td>
                        <td id="td6" runat="server" class="td5px">событие:</td>
                        <td id="td7" runat="server" class="td5px">описание:</td>
                        <td id="td8" runat="server" class="td5px">назначен:</td>
                        <td id="td3" runat="server" class="td5px">дата/время:</td>
                        <td id="td9" runat="server" class="td5px">исполнил:</td>
                        <td id="td4" runat="server" class="td5px">комментарий:</td>
                        <td id="td20" runat="server" class="td5px">дата/время</td>
                        <td id="td14" runat="server" class="td5px">замечания:</td>                        
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="Id" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:Literal ID="Sourse" runat="server"></asp:Literal></td>
                        <td id="td13" runat="server" class="td5px"><asp:Literal ID="DataId" runat="server"></asp:Literal></td>
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="LiftId" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="TypeId" runat="server"></asp:Literal></td>
                        <td id="td21" runat="server" class="td5px"><asp:Literal ID="EventId" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="ToApp" runat="server"></asp:Literal></td>
                        <td id="td19" runat="server" class="td5px"><asp:Literal ID="DateToApp" runat="server"></asp:Literal></td>
                        <td id="td22" runat="server" class="td5px"><asp:Literal ID="Who" runat="server"></asp:Literal></td>
                        <td id="td23" runat="server" class="td5px"><asp:Literal ID="Comment" runat="server"></asp:Literal></td>
                        <td id="td24" runat="server" class="td5px"><asp:Literal ID="DateWho" runat="server"></asp:Literal></td>
                        <td id="td15" runat="server" class="td5px"><asp:Literal ID="Prim" runat="server"></asp:Literal></td>
                    </tr>
          <tr>
             <td id="td37" runat="server" >&nbsp;</td> 
          </tr>

           <tr id="Tr3" runat="server" style="background-color: #8fbdb1; color:#000;">

                        <td id="td25" runat="server" class="td5px"><asp:LinkButton ID ="Button3" runat ="server" OnClick = "ZayEdit_Click" Text ="обработчик события" /></td>
                        <td id="td27" runat="server" class="td5px"><asp:LinkButton ID ="PartList" runat ="server" OnClick = "PartList_Click" Text ="запчасти" /></td>
                        <td id="td26" runat="server" class="td5px"><asp:LinkButton ID ="LinkButton1" runat ="server" OnClick = "PartList_Click" Text ="запчасти" /></td>               
                        <td id="td32" runat="server" class="td5px"><asp:LinkButton ID ="DocE" runat ="server" OnClick = "DocE_Click" Text ="документы" /></td>
                        <td id="td28" runat="server" class="td5px"> КП:</td>
                        <td id="td29" runat="server" class="td5px">ожидание:</td>
                        <td id="td30" runat="server" class="td5px">получение:</td>
                        <td id="td33" runat="server" class="td5px">склад:</td>
                        <td id="td43" runat="server" class="td5px">перемещ.:</td>
                        <td id="td34" runat="server" class="td5px">списание:</td>
                        <td id="td35" runat="server" class="td5px"><asp:LinkButton ID ="Button4" runat ="server" OnClick = "Hist_Click" Text ="история" /></td>
                        <td id="td36" runat="server" class="td5px">тайминг:</td>                        
                        
                    </tr>
           <tr id="Tr4" runat="server" style="background-color: #FFF; color:#000;">
                        
               <td id="td39" runat="server" class="td5px"></td>
                        <td id="td38" runat="server" class="td5px"><asp:Literal ID="Literal3" runat="server" Text="необходимые" /></td>
                        <td id="td40" runat="server" class="td5px"><asp:Literal ID="Literal1" runat="server" Text="установленные" /></td>                        
                        <td id="td45" runat="server" class="td5px"><asp:Literal ID="Literal2" runat="server" Text="События" /></td>
                        <td id="td31" runat="server" class="td5px"><asp:Literal ID="ZapKp" runat="server"></asp:Literal></td>
                        <td id="td41" runat="server" class="td5px"><asp:Literal ID="Ogid" runat="server"></asp:Literal></td>
                        <td id="td42" runat="server" class="td5px"><asp:Literal ID="Poluch" runat="server"></asp:Literal></td>
                        <td id="td46" runat="server" class="td5px"><asp:Literal ID="Sklad" runat="server"></asp:Literal></td>
                        <td id="td44" runat="server" class="td5px"></td>
                        <td id="td47" runat="server" class="td5px"><asp:Literal ID="Spis" runat="server"></asp:Literal></td>
                        <td id="td48" runat="server" class="td5px"></td>
                        <td id="td49" runat="server" class="td5px"><asp:Literal ID="Timing" runat="server"></asp:Literal></td>
                    </tr>
               </table>
     <br /><asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label><br />
     <asp:PlaceHolder ID="HistL" runat="server">
<asp:ListView ID="ListView2" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #f39369; color: #000;">
                               
                                <td id="td1" runat="server" class="td5px">дата</td>
                                <td id="td3" runat="server" class="td5px">описание</td>                                                               
                                <td id="td5" runat="server" class="td5px">категория</td>                               
                                <td id="td6" runat="server" class="td5px">кто</td>                                
                                <td id="td7" runat="server" class="td5px">кому</td>
                                <td id="td51" runat="server" class="td5px">комментарий</td>
                                <td id="td52" runat="server" class="td5px">примечание</td>
                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >
                                <asp:Label ID="Date" runat="server" Text='<%# Eval("Date") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Text" runat="server" Text='<%# Eval("Text") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="Category" runat="server" Text='<%# Eval("Category") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="From" runat="server" Text='<%# Eval("From") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="To" runat="server" Text='<%# Eval("To") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Comment" runat="server" Text='<%# Eval("Comment") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="PrimHist" runat="server" Text='<%# Eval("PrimHist") %>' />
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
    </asp:PlaceHolder>
  <asp:PlaceHolder ID="PartL" runat="server">
<asp:ListView ID="ListView1" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #6da176; color: #000;">
                               
                                <td id="td1" runat="server" class="td5px">№</td>
                                <td id="td3" runat="server" class="td5px">имя фото</td>                                                               
                                <td id="td5" runat="server" class="td5px">наименование</td>                               
                                <td id="td6" runat="server" class="td5px">ID номер</td>                                
                                <td id="td7" runat="server" class="td5px">количество</td>
                                
                            </tr>
                            <tr runat="server" id="itemPlaceholder" />
                        </table>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >                                 
                                
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("namefoto") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("Name") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Status" runat="server" Text='<%# Eval("NumId") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Prim" runat="server" Text='<%# Eval("Kol") %>' />
                            </td>
                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="DocEv" runat="server">
<asp:ListView ID="Out" runat="server">
                    <LayoutTemplate>
                        <table  border="1" id="tbl1" runat="server">
                            <tr id="Tr2" runat="server" style ="background-color: #0094ff; color: #060f3d;">
                               
                                <td id="td1" runat="server" class="td5px">№</td><td id="td3" runat="server" class="td5px">документ:</td><td id="td5" runat="server" class="td5px">имя файла:</td><td id="td6" runat="server" class="td5px">статус:</td><td id="td7" runat="server" class="td5px">примеч.</td><td id="td50" runat="server" class="td5px">от кого:</td></tr><tr runat="server" id="itemPlaceholder" /></table></LayoutTemplate><ItemTemplate>
                        <tr id="Tr1" runat="server">
                            <td class="tdwhite" >                                 
                                
                                <asp:HyperLink ID="Url" runat="server" NavigateUrl='<%# Eval("Url") %>'>
                                <asp:Label ID="Id" runat="server" Text='<%# Eval("Id") %>' />
                                </asp:HyperLink></td><td class="tdwhite" >
                                <asp:Label ID="Name" runat="server" Text='<%# Eval("Name") %>' />
                            </td>                           
                            <td class="tdwhite" >
                                <asp:Label ID="NameFile" runat="server" Text='<%# Eval("NameFile") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Status" runat="server" Text='<%# Eval("Status") %>' />
                            </td>
                            <td class="tdwhite" >
                                <asp:Label ID="Prim" runat="server" Text='<%# Eval("Prim") %>' />
                            </td>
                             <td class="tdwhite" >
                                <asp:Label ID="Usr" runat="server" Text='<%# Eval("Usr") %>' />
                            </td>                           
                        </tr>
                    </ItemTemplate>
                </asp:ListView>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="false">
       участок/маршрут (склад) :<asp:Label ID="IdU" runat="server" Text="Label"></asp:Label>/ <asp:Label ID="IdM" runat="server" Text="Label"></asp:Label>Заявка ОДС : <asp:Label ID="Idz" runat="server" Text="Label"></asp:Label>,
        мех : <asp:Label ID="Idw" runat="server" Text="Label"></asp:Label><br /></asp:PlaceHolder><br /><br />
    Нажатием кнопки "Закрыть" событие переводится в архив!<br />
    <asp:Button ID ="Close" runat ="server" OnClick = "Close_Click" Text ="Закрыть событие" />
    <asp:Button ID ="Button1" runat ="server" OnClick = "Button1_Click" Text ="Вернуться" />    
       <br />
    </asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="HeadContent">
    
   </asp:Content>
