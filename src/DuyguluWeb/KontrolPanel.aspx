<%@ Page Title="" Language="C#" MasterPageFile="~/Tema.Master" AutoEventWireup="true" CodeBehind="KontrolPanel.aspx.cs" Inherits="DuyguluWeb.KontrolPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="UstMenu" runat="server">
       <table style="width: 100%; height: 100%;text-align:center" cellpadding="0" cellspacing="0">
			<tr  style=" background-color: #A5D7A4">
				<td  style=" width:25%"  align="center" onclick="location.href='/Grafik.aspx'" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'" >
				<img alt="Grafik"  height="128" src="images/Chart.png" width="128" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/Harita.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Harita" height="78" src="images/map2.png" width="73" /></td>
                <td align="center" onclick="location.href='/TweetKaydet.aspx'" style="background-color: #6DC46B; width:25%" onclick="location.href='/Harita.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
                <img alt="Kaydet" height="128" src="images/database.png" width="128" /></td>
				<td align="center" style="background-color: #6DC46B; width:25%" onclick="location.href='/KontrolPanel.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Ayarlar" height="128" src="images/control2.png" width="128" /></td>
				<td align="center" style=" width:25%" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="" height="128" src="images/loginicon.png" width="128" />&nbsp;</td>
			</tr>
		</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Icerik" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
    <link href="styles/smart_tab_vertical.css" rel="stylesheet" type="text/css"></link>
<script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
<script type="text/javascript" src="js/jquery.smartTab.js"></script>

        <style>
        span.reference{
            position:fixed;
            left:5px;
            top:5px;
            font-size:10px;
            text-shadow:1px 1px 1px #fff;
        }
        span.reference a{
            color:#555;
            text-decoration:none;
			text-transform:uppercase;
        }
        span.reference a:hover{
            color:#000;
            
        }
        h1{
            color:#ccc;
            font-size:36px;
            text-shadow:1px 1px 1px #fff;
            padding:20px;
        }

            </style>
  <script type="text/javascript">
      $(document).ready(function () {
          // Smart Tab
          $('#tabs').smartTab({ autoProgress: false, stopOnFocus: true, transitionEffect: 'vSlide' });
      });
</script>
    <table align="center" border="0" cellpadding="0" cellspacing="0">
<tr>
  <td valign="top">
<!-- Tabs -->
  		<div id="tabs" >
  			<ul>
  				<li><a href="#tabs-1">ARAMALAR<br />
                <small>Arama Kayıtlar</small>
            </a></li>
  				<li><a href="#tabs-2">KULLANICILAR<br />
                <small>Kayıtlı Kullanıcı Listesi</small>
            </a></li>
  				<li><a href="#tabs-3">TEST ANALİZİ<br />
                <small>Tek Bir Metin Analizi</small>
             </a></li>
  				<li><a href="#tabs-4">SÖZLÜK<br />
                <small>Analizde Kullanılan Sözlük</small>
            </a></li>
                  <li><a href="#tabs-5">ÖZEL SÖZLÜK<br />
                <small>Kişisel Analiz Sözlüğü</small>
            </a></li>
                  <li><a href="#tabs-6">ANALİZ AYARI<br />
                <small>Analizdeki Algoritma Seçimi</small>
            </a></li>
  			</ul>
  			<div id="tabs-1" style="width:95%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <legend>Aramalar</legend>
                                    <div id="SearchTable" >
                              
                                        <table style="width: 100%;">
                                            <tr>
                                                 <td class="auto-style1">
                                                    <asp:TextBox ID="txts1" placeholder="Aranacak İfade"  runat="server" AutoPostBack="True" Width="90%" CssClass="textbox"></asp:TextBox>
                                                </td>
                                                 <td rowspan="2">
                                                     <asp:ImageButton ID="ImageButton1"  runat="server" OnClick="Button1_Click" ImageUrl="~/Images/search1.png" Height="144px" Width="130px" />
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">
                                                    <asp:TextBox ID="txtt1" runat="server" TextMode="Date" Width="90%" CssClass="textbox"></asp:TextBox>
                                                    <asp:TextBox ID="txtt2" runat="server" TextMode="Date" Width="90%" CssClass="textbox"></asp:TextBox>
                                                </td>
                                        
                                                
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical">
                                                        <asp:GridView ID="gvSearchtable" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="SearchId" DataSourceID="SqlDataSourceSearch" OnDataBound="gvSearchtable_DataBound" OnLoad="gvSearchtable_Load" OnSelectedIndexChanged="gvSearchtable_SelectedIndexChanged">
                                                            <Columns>
                                                                <asp:BoundField DataField="SearchId" HeaderText="SearchId" ReadOnly="True" SortExpression="SearchId" />
                                                                <asp:BoundField DataField="SearchDate" HeaderText="SearchDate" SortExpression="SearchDate" />
                                                                <asp:BoundField DataField="SearchText" HeaderText="SearchText" SortExpression="SearchText" />
                                                                <asp:BoundField DataField="Positiv" HeaderText="Positiv" SortExpression="Positiv" />
                                                                <asp:BoundField DataField="Negativ" HeaderText="Negativ" SortExpression="Negativ" />
                                                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                                            </Columns>
                                                            <HeaderStyle BorderColor="White" ForeColor="White" />
                                                        </asp:GridView>
                                                        <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" ConnectionString="<%$ ConnectionStrings:twitterdbConnectionString %>" ProviderName="<%$ ConnectionStrings:twitterdbConnectionString.ProviderName %>" SelectCommand="SELECT searchtable.SearchId,searchtable.SearchDate,searchtable.SearchText,searchtable.Positiv,searchtable.Negativ, appuser.Name FROM searchtable INNER JOIN appuser ON searchtable.UserId=appuser.Id"></asp:SqlDataSource>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            
        </div>
  			<div id="tabs-2" style="width:95%">
                   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                            <legend>Kullanıcılar</legend>
                             <div id="UserTable" >
            <table style="width:100%;">
                <tr>
                    <td>&nbsp;</td>


                </tr>
                <tr>
                    <td > <asp:TextBox ID="txtName" placeholder="Kullanıcı Adı" runat="server" AutoPostBack="True" Width="100%" CssClass="textbox" ></asp:TextBox>
                    <asp:Button ID="btnUserSearch" runat="server" OnClick="btnUserSearch_Click" Text="ARA" Width="100%" CssClass="btn" />
                        </td>
                </tr>
                <tr>
                    <td><asp:GridView ID="gvUser" runat="server" CssClass="CSSTable" OnSelectedIndexChanged="gvUser_SelectedIndexChanged" OnLoad="gvUser_Load" AutoGenerateColumns="False" DataKeyNames="Id" DataSourceID="SqlDataSourceUser">
                        <Columns>
                            <asp:CommandField SelectText="Seç" ShowSelectButton="True" />
                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            <asp:BoundField DataField="Mail" HeaderText="Mail" SortExpression="Mail" />
                            <asp:BoundField DataField="Positiv" HeaderText="Positiv" SortExpression="Positiv" />
                            <asp:BoundField DataField="Negativ" HeaderText="Negativ" SortExpression="Negativ" />
                            <asp:BoundField DataField="Authorize" HeaderText="Authorize" SortExpression="Authorize" />
                        </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSourceUser" runat="server" ConnectionString="<%$ ConnectionStrings:twitterdbConnectionString %>" ProviderName="<%$ ConnectionStrings:twitterdbConnectionString.ProviderName %>" SelectCommand="SELECT Id, Name, Mail, Authorize, Positiv, Negativ FROM appuser"></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div></ContentTemplate>
                            </asp:UpdatePanel>    

        </div>                      
  			<div id="tabs-3" style="width:95%;">
                            				  
                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                            <legend>Test Analizi</legend>
                             <div id="TestDiv" >
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:TextBox ID="txtMetin" runat="server" placeholder="Metin Girin iz.." TextMode="MultiLine" Width="100%" CssClass="textbox"></asp:TextBox></td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAnaliz" runat="server" Text="Analiz Et" Width="100%" OnClick="btnAnaliz_Click" CssClass="btn" /></td>

            </tr>
            <tr>
                <td><asp:TextBox ID="txtHazirlik"  Width="100%" runat="server" CssClass="textbox" ></asp:TextBox></td>

            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvAnaliz" runat="server" CssClass="mGrid" OnLoad="gvAnaliz_Load"></asp:GridView>
                </td>

            </tr>
        </table>
    </div></ContentTemplate>
                            </asp:UpdatePanel>        
        </div>
  			<div id="tabs-4" style="width:95%">
                            			<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                           <div id="WordTableDiv">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtKelimeAra" Width="90%" runat="server" CssClass="textbox"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnKelimeAra" runat="server" Width="100%" CssClass="btn" OnClick="btnKelimeAra_Click" Text="Kelime Ara" Height="50px" />
                                        </td>

                                    </tr>
                                   
                                    <tr>
                                        <td  colspan="2">
                                            <asp:GridView ID="gvKelime" runat="server" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" DataSourceID="SqlDataSourceWord" >
                                                <Columns>
                                                    <asp:CommandField CancelText="İptal" DeleteText="Sil" EditText="Düzenle" InsertText="Ekle" NewText="Yeni" SelectText="Seç" ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" ShowSelectButton="True" UpdateText="Güncelle" />
                                                    <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                                                    <asp:BoundField DataField="Word" HeaderText="Word" SortExpression="Word" />
                                                    <asp:BoundField DataField="Positiv" HeaderText="Positiv" SortExpression="Positiv" />
                                                    <asp:BoundField DataField="Negativ" HeaderText="Negativ" SortExpression="Negativ" />
                                                    <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                </Columns>
                                                <HeaderStyle ForeColor="White" />
                                            </asp:GridView>
  
                                            <asp:SqlDataSource ID="SqlDataSourceWord" runat="server" ConnectionString="<%$ ConnectionStrings:twitterdbConnectionString %>" ProviderName="<%$ ConnectionStrings:twitterdbConnectionString.ProviderName %>" ></asp:SqlDataSource>
  
                                        </td>
                                        <td></td>

                                    </tr>
                                </table>

                            </div>
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                       
        </div>
              <div id="tabs-5" style="width:95%">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div id="UserWordTableDiv">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtKelimeUser" runat="server" CssClass="textbox"></asp:TextBox>
                                                    <asp:Button ID="btnKelimeAraUser" runat="server" OnClick="btnKelimeAraUser_Click" Text="Kelime Ara" CssClass="btn" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblKelimeUserId" runat="server" Text="0"></asp:Label>
                                                    <asp:TextBox ID="txtWordUser" placeholder="Kelime"  runat="server" Width="176px" CssClass="textbox"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlPositiv" runat="server"   AutoPostBack="True" OnSelectedIndexChanged="ddlPositiv_SelectedIndexChanged" CssClass="wrapper-dropdown-5">
                                                        
                                                        <asp:ListItem>Olumluluk 0</asp:ListItem>
                                                        <asp:ListItem>1</asp:ListItem>
                                                        <asp:ListItem>2</asp:ListItem>
                                                        <asp:ListItem>3</asp:ListItem>
                                                        <asp:ListItem>4</asp:ListItem>
                                                        <asp:ListItem>5</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlNegativ" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNegativ_SelectedIndexChanged" CssClass="wrapper-dropdown-5">
                
                                                        <asp:ListItem>Olumsuzluk 0</asp:ListItem>
                                                        <asp:ListItem>1</asp:ListItem>
                                                        <asp:ListItem>2</asp:ListItem>
                                                        <asp:ListItem>3</asp:ListItem>
                                                        <asp:ListItem>4</asp:ListItem>
                                                        <asp:ListItem>5</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnKaydet" runat="server" Width="100%" OnClick="btnKaydet_Click" Text="Kaydet" CssClass="btn" />
                                                    <asp:Button ID="btnKelimeUserSil" runat="server" Width="100%" OnClick="btnKelimeUserSil_Click" Text="Sil" CssClass="btn" />
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvKelimeUser" runat="server" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="Id" DataSourceID="SqlDataSourceWordUser" OnSelectedIndexChanged="gvKelimeUser_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:CommandField CancelText="İptal" DeleteText="Sil" EditText="Düzenle" InsertText="Ekle" NewText="Yeni" SelectText="Seç" ShowSelectButton="True" UpdateText="Güncelle" />
                                                            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
                                                            <asp:BoundField DataField="Word" HeaderText="Word" SortExpression="Word" />
                                                            <asp:BoundField DataField="Positiv" HeaderText="Positiv" SortExpression="Positiv" />
                                                            <asp:BoundField DataField="Negativ" HeaderText="Negativ" SortExpression="Negativ" />
                                                            <asp:BoundField DataField="Type" HeaderText="Type" SortExpression="Type" />
                                                            
                                                        </Columns>
                                                        <HeaderStyle ForeColor="White" />
                                                    </asp:GridView>

                                                    <asp:SqlDataSource ID="SqlDataSourceWordUser" runat="server" ConnectionString="<%$ ConnectionStrings:twitterdbConnectionString %>" ProviderName="<%$ ConnectionStrings:twitterdbConnectionString.ProviderName %>"></asp:SqlDataSource>

                                                </td>

                                            </tr>
                                        </table>

                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            			
        </div>
              <div id="tabs-6" style="width:95%">
               <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <div id="Div1">


                                        <table style="width: 100%;">
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnPreprocessing" runat="server" Width="100%" OnClick="btnPreprocessing_Click" Text="Preprocessing" CssClass="btn" />
                                                </td>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnStopWord" runat="server" Width="100%" OnClick="btnStopWord_Click" Text="Stop Word" CssClass="btn" />


                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSpelling" runat="server" Width="100%" OnClick="btnSpelling_Click" Text="Spelling" CssClass="btn" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="Label1" runat="server" Text="Jaro-Winkler Benzerlik Oranı :"></asp:Label>
                                                    <asp:DropDownList ID="ddlSimilarity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSimilarity_SelectedIndexChanged" OnTextChanged="ddlSimilarity_TextChanged">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>


                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            			
        </div>
          
  	</div>  	
  </td>
</tr>
</table>  		
    
</asp:Content>
