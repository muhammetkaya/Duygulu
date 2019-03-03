<%@ Page Title="" Language="C#" MasterPageFile="~/Tema.Master" AutoEventWireup="true" CodeBehind="TweetKaydet.aspx.cs" Inherits="DuyguluWeb.TweetKaydet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="UstMenu" runat="server">
      <table style="width: 100%; height: 100%;text-align:center" cellpadding="0" cellspacing="0">
			<tr  style=" background-color: #A5D7A4">
				<td  width:25%"  align="center" onclick="location.href='/Grafik.aspx'" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'" >
				<img alt="Grafik"  height="128" src="images/Chart.png" width="128" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/Harita.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Harita" height="95" src="images/map2.png" width="90" /></td>
				<td align="center" onclick="location.href='/TweetKaydet.aspx'" style="background-color: #6DC46B; width:25%" onclick="location.href='/Harita.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
                <img alt="Kaydet" height="128" src="images/database.png" width="128" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/KontrolPanel.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Ayarlar" height="128" src="images/control2.png" width="128" /></td>
				<td align="center" style=" width:25%" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="" height="128" src="images/loginicon.png" width="128" />&nbsp;</td>
			</tr>
		</table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Icerik" runat="server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td style="width:30%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <legend>Aramalar</legend>
                                <div id="SearchTable">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="Panel1" runat="server" Height="400px" ScrollBars="Vertical">
                                                    <asp:GridView ID="gvSearchtable" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CssClass="mGrid" DataKeyNames="SearchId" DataSourceID="SqlDataSourceSearch" Font-Size="X-Small" OnLoad="gvSearchtable_Load">
                                                        <Columns>
                                                            <asp:BoundField DataField="SearchDate" HeaderText="Tarih" SortExpression="SearchDate" />
                                                            <asp:BoundField DataField="SearchText" HeaderText="Arama" SortExpression="SearchText" />
                                                            <asp:BoundField DataField="Positiv" HeaderText=" + " SortExpression="Positiv" />
                                                            <asp:BoundField DataField="Negativ" HeaderText=" - " SortExpression="Negativ" />
                                                        </Columns>
                                                        <HeaderStyle BorderColor="White" ForeColor="White" />
                                                    </asp:GridView>
                                                    <asp:SqlDataSource ID="SqlDataSourceSearch" runat="server" ConnectionString="<%$ ConnectionStrings:twitterdbConnectionString %>" ProviderName="<%$ ConnectionStrings:twitterdbConnectionString.ProviderName %>" SelectCommand="SELECT searchtable.SearchId,searchtable.SearchDate,searchtable.SearchText,searchtable.Positiv,searchtable.Negativ, appuser.Name FROM searchtable INNER JOIN appuser ON searchtable.UserId=appuser.Id"></asp:SqlDataSource>
                                                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                    </asp:ScriptManager>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width:50%">
                        <asp:GridView ID="gvTweet" runat="server" CssClass="mGrid">
                        </asp:GridView>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearch" runat="server" CssClass="txt2" placeholder="Aranacak İfade"></asp:TextBox>
                        <asp:TextBox ID="txtn" runat="server" CssClass="txt2" placeholder="Kaç Yüz Tweet ?" TextMode="Number"></asp:TextBox>
                        <asp:TextBox ID="txtTarih" runat="server" CssClass="txt2" placeholder="Hangi Tarihten Geriye ?" TextMode="DateTime"></asp:TextBox>
                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="True" Text="Özel Analiz" />
                        <asp:Button ID="btnTweetKaydet" runat="server" CssClass="btn2" OnClick="btnTweetKaydet_Click" Text="Tweet Kaydet" Width="100%" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
