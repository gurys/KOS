<%@ Page Title="Редактирование/Закрытие Событий" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZakrytieTSG.aspx.cs" Inherits="KOS.ZakrytieTSG" %>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
         Выбрано Событие для редактирования/закрытия:<br />
      <table id="tblr" runat="server" cellpadding="2">
                    <tr id="Tr2" runat="server" style="background-color: #336699; color:#FFF;">

                        <td id="td1" runat="server" class="td5px">№</td>
                        <td id="td10" runat="server" class="td5px">источник</td>
                        <td id="td2" runat="server" class="td5px">диспетчер</td> 
                        <td id="td5" runat="server" class="td5px">дата/время</td>
                        <td id="td6" runat="server" class="td5px">вид услуги</td>
                        <td id="td7" runat="server" class="td5px">зона</td>
                        <td id="td8" runat="server" class="td5px">событие</td>
                        <td id="td3" runat="server" class="td5px">описание</td>
                        <td id="td9" runat="server" class="td5px">принял</td>
                        <td id="td20" runat="server" class="td5px">дата/время</td>
                        <td id="td4" runat="server" class="td5px">исполнил</td>
                        <td id="td14" runat="server" class="td5px">комментарий</td>
                        <td id="td15" runat="server" class="td5px">дата/время</td>
                        <td id="td30" runat="server" class="td5px">замечания</td>
                        <td id="td21" runat="server" class="td5px">тайминг</td>                        
                        
                    </tr>
                     <tr id="Tr1" runat="server" style="background-color: #FFF; color:#000;">
                        <td id="td11" runat="server" class="td5px"><asp:Literal ID="Id" runat="server"></asp:Literal></td>
                        <td id="td28" runat="server" class="td5px"><asp:Literal ID="Sourse" runat="server"></asp:Literal></td>
                        <td id="td12" runat="server" class="td5px"><asp:Literal ID="IO" runat="server"></asp:Literal></td>
                        <td id="td13" runat="server" class="td5px"><asp:Literal ID="DataId" runat="server"></asp:Literal></td>
                        <td id="td16" runat="server" class="td5px"><asp:Literal ID="RegistrId" runat="server"></asp:Literal></td>
                        <td id="td17" runat="server" class="td5px"><asp:Literal ID="LiftId" runat="server"></asp:Literal></td>
                        <td id="td18" runat="server" class="td5px"><asp:Literal ID="TypeId" runat="server"></asp:Literal></td>
                        <td id="td19" runat="server" class="td5px"><asp:Literal ID="EventId" runat="server"></asp:Literal></td>
                        <td id="td22" runat="server" class="td5px"><asp:Literal ID="ToApp" runat="server"></asp:Literal></td>
                        <td id="td23" runat="server" class="td5px"><asp:Literal ID="DateToApp" runat="server"></asp:Literal></td>
                        <td id="td24" runat="server" class="td5px"><asp:Literal ID="Who" runat="server"></asp:Literal></td>
                        <td id="td25" runat="server" class="td5px"><asp:Literal ID="Comment" runat="server"></asp:Literal></td>
                        <td id="td26" runat="server" class="td5px"><asp:Literal ID="DateWho" runat="server"></asp:Literal></td>
                        <td id="td29" runat="server" class="td5px"><asp:Literal ID="Prim" runat="server"></asp:Literal></td>
                        <td id="td27" runat="server" class="td5px"><asp:Literal ID="Timing" runat="server"></asp:Literal></td>                        
                        
                    </tr>
               </table>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Исполнил:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Диспетчер:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Зона:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Принял:<br />
    &nbsp;<asp:TextBox ID="Text1" runat="server" Height="22px" Width="200px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>
         &nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextBox5" runat="server" Height="22px" Width="105px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>
         &nbsp;<asp:PlaceHolder ID="Fdr" runat="server"><asp:DropDownList ID="FIO" runat="server" meta:resourcekey="FIOResource1" Height="25px"></asp:DropDownList></asp:PlaceHolder>
         &nbsp; &nbsp;<asp:TextBox ID="TextBox4" runat="server" Height="22px" Width="200px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>
         &nbsp;&nbsp; <asp:PlaceHolder ID="Wrk" runat="server"><asp:DropDownList ID="Workers" runat="server" meta:resourcekey="WorkerResource1" Height="25px"></asp:DropDownList></asp:PlaceHolder>
        <asp:TextBox ID="Text2" runat="server" Visible="false" Height="22px" Width="105px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>
         &nbsp;&nbsp;<br />
         Описание:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
        <asp:TextBox ID="Text" runat="server" Height="40px" Width="380px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
         <br />
       
          <asp:PlaceHolder ID="Treg" runat="server"> 
         <asp:Label runat="server" ID="Msg" ForeColor="Red" meta:resourcekey="MsgResource1"></asp:Label>
             
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
             
             дата/время Регистрации:<asp:DropDownList ID="DD" runat="server"  meta:resourcekey="DDResource1"></asp:DropDownList>
             <asp:DropDownList ID="MO" runat="server"  meta:resourcekey="MOResource1"></asp:DropDownList>
             <asp:DropDownList ID="YY" runat="server"  meta:resourcekey="YYResource1"></asp:DropDownList>/
             <asp:DropDownList ID="HH" runat="server"  meta:resourcekey="HHResource1"></asp:DropDownList>
             <asp:DropDownList ID="MM" runat="server"  meta:resourcekey="MMResource1"></asp:DropDownList><br />  
         
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              Принятия:<asp:DropDownList ID="DD1" runat="server"  meta:resourcekey="DD1Resource1"></asp:DropDownList>
             <asp:DropDownList ID="MO1" runat="server"  meta:resourcekey="MO1Resource1"></asp:DropDownList>
             <asp:DropDownList ID="YY1" runat="server"  meta:resourcekey="YY1Resource1"></asp:DropDownList>/
             <asp:DropDownList ID="HH1" runat="server"  meta:resourcekey="HH1Resource1"></asp:DropDownList>
             <asp:DropDownList ID="MM1" runat="server"  meta:resourcekey="MM1Resource1"></asp:DropDownList>

     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         </asp:PlaceHolder>
         <br />
       
         <asp:PlaceHolder ID="Wrk0" runat="server"><asp:DropDownList ID="Workers0" runat="server" meta:resourcekey="WorkerResource1" Height="25px"></asp:DropDownList></asp:PlaceHolder>
        <asp:TextBox ID="Text3" runat="server" Visible="false" Height="22px" Width="130px" TextMode="MultiLine" meta:resourcekey="TextResource1"></asp:TextBox>   
         &nbsp;Исполнил &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
         <asp:PlaceHolder ID="Tzak" runat="server">
     Исполнения:<asp:DropDownList ID="DD2" runat="server"  meta:resourcekey="DD2Resource1"></asp:DropDownList>
             <asp:DropDownList ID="MO2" runat="server"  meta:resourcekey="MO2Resource1"></asp:DropDownList>
             <asp:DropDownList ID="YY2" runat="server"  meta:resourcekey="YY2Resource1"></asp:DropDownList>/
             <asp:DropDownList ID="HH2" runat="server"  meta:resourcekey="HH2Resource1"></asp:DropDownList>
             <asp:DropDownList ID="MM2" runat="server"  meta:resourcekey="MM2Resource1"></asp:DropDownList></asp:PlaceHolder>
         &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         <br />
         Коментарий по Закрытию События:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Замечания:<br />
        <asp:TextBox ID="Text4" runat="server" Height="31px" Width="390px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp&nbsp;&nbsp;&nbsp;   
         <asp:TextBox ID="Text5" runat="server" Height="31px" Width="380px" TextMode="MultiLine" Columns="50" Rows="5" meta:resourcekey="TextResource1"></asp:TextBox>   
    <br />
   &nbsp;<asp:Button ID ="Edit" runat ="server" OnClick = "Edit_Click" Text ="ПРИНЯТЬ" BackColor="#003399" ForeColor="#CCFF99" Height="35px" Width="390px" /> 
       
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID ="Close" runat ="server" OnClick = "Close_Click" Text ="ЗАКРЫТЬ" BackColor="#CC0000" ForeColor="#CCFF99" Height="35px" Width="390px" /> <br />
    
      <br />
</asp:Content>
<asp:Content ID="Content4" runat="server" contentplaceholderid="FeaturedContent">
    <p>
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</p>
</asp:Content>

