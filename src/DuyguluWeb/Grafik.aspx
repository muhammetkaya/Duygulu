<%@ Page Title="" Language="C#" MasterPageFile="~/Tema.Master" AutoEventWireup="true" CodeBehind="Grafik.aspx.cs" Inherits="DuyguluWeb.Grafik" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="UstMenu">
    <table style="width: 100%; height: 100%;text-align:center" cellpadding="0" cellspacing="0">
			<tr  style=" background-color: #A5D7A4">
				<td  style="background-color: #6DC46B; width:25%"  align="center" onclick="location.href='/Grafik.aspx'" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'" >
				<img alt="Grafik"  height="128" src="images/Chart.png" width="128" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/Harita.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Harita" height="95" src="images/map2.png" width="90" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/TweetKaydet.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
                <img alt="Kayıt" height="128" src="images/database.png" width="128" /></td>
				<td align="center" style=" width:25%" onclick="location.href='/KontrolPanel.aspx'"  onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="Ayarlar" height="128" src="images/control2.png" width="128" /></td>
				<td align="center" style=" width:25%" onMouseOver="this.bgColor='#6DC46B'" OnMouseOut="this.bgColor='#A5D7A4'">
				<img alt="" height="128" src="images/loginicon.png" width="128" />&nbsp;</td>
			</tr>
		</table>
</asp:Content>

<asp:Content ID="Content2" runat="server" contentplaceholderid="Icerik">
    <table style="width:100%;">
        <tr>
            <td style="background-color: #6DC46B; width: 80%">
                <asp:Chart ID="Chart1" runat="server">
                    <series>
                        <asp:Series Name="Series1">
                        </asp:Series>
                    </series>
                    <chartareas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </chartareas>
                </asp:Chart>
                <br />
                <asp:Label ID="lblDurum" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <table style="background-color: #6DC46B; width:100%;">
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSearch" placeholder="Aranacak İfade" runat="server" CssClass="txt2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtn" runat="server" placeholder="Kaç Yüz Tweet ?" TextMode="Number" CssClass="txt2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtTarih" runat="server" placeholder="Hangi Tarihten Geriye ?" TextMode="DateTime" CssClass="txt2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:Button ID="btnAnaliz" runat="server" OnClick="btnAnaliz_Click" Text="Analiz" Width="100%" CssClass="btn2" />
                            <br />
                            <asp:CheckBox ID="CheckBox1" runat="server" Text="Özel Analiz" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="98%" Height="400px">
                    <asp:GridView ID="GridView1" runat="server"  Width="100%" CssClass="mGrid" Font-Size="X-Small">
                    </asp:GridView>
                    <br />
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>


