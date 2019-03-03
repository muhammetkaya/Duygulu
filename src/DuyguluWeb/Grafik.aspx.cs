using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Duygulu;
using System.Drawing;
using System.Data;

namespace DuyguluWeb
{
    public partial class Grafik : System.Web.UI.Page
    {
        Duygulu.Duygulu miner = new Duygulu.Duygulu();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["kullanici_id"] = 1;
        }

        protected void btnAnaliz_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();

            table = miner.Analiz(miner.VeriTabanindaAra(txtSearch.Text, int.Parse(Session["kullanici_id"].ToString()), CheckBox1.Checked), int.Parse(Session["kullanici_id"].ToString()));
            if (table.Rows.Count > 1)
            {
                table2.Columns.Add("CreatedDate");
                table2.Columns.Add("İyi");
                table2.Columns.Add("Kötü");
                table2.Columns.Add("Veri/Gün");

                table.Columns.Remove("SearchId");
                table.Columns.Remove("GeoCode");
                table.Columns.Remove("UserId");
                table.Columns.Remove("USerProfileText");
                table.Columns.Remove("HasPhoto");
                table.Columns.Remove("UserFollowerCount");
                table.Columns.Remove("UserFollowingCount");
                table.Columns.Remove("UserScreenName");
                table.Columns.Remove("IsReweet");
                table.Columns.Remove("Text2");

                DateTime t1;
                DateTime t2;
                t1 = DateTime.Parse(table.Rows[1]["CreatedDate"].ToString());
                t2 = DateTime.Parse(table.Rows[1]["CreatedDate"].ToString());
                for (int t = 1; t < table.Rows.Count; t++)
                {
                    if (t1 > DateTime.Parse(table.Rows[t]["CreatedDate"].ToString()))
                        t1 = DateTime.Parse(table.Rows[t]["CreatedDate"].ToString());
                    if (t2 < DateTime.Parse(table.Rows[t]["CreatedDate"].ToString()))
                        t2 = DateTime.Parse(table.Rows[t]["CreatedDate"].ToString());
                }

                table.DefaultView.Sort = "Fark desc";
                int gun = t2.DayOfYear - t1.DayOfYear;

                int derece, derece1 = 0, derece2 = 0, sayi1 = 0, sayi2 = 0;
                for (int i = 0; i <= gun; i++)
                {
                    for (int j = 1; j < table.Rows.Count; j++)
                    {
                        string a = t1.AddDays(i).ToShortDateString();
                        string b = DateTime.Parse(table.Rows[1]["CreatedDate"].ToString()).ToShortDateString();
                        if (DateTime.Parse(table.Rows[j]["CreatedDate"].ToString()).ToShortDateString() == t1.AddDays(i).ToShortDateString() && table.Rows[j]["Fark"].ToString() != "0")
                        {

                            if (int.Parse(table.Rows[j]["Fark"].ToString()) > 0)
                            {
                                sayi1++;
                                //derece1 += int.Parse(table.Rows[j]["İyi"].ToString());
                            }
                            else if (int.Parse(table.Rows[j]["Fark"].ToString()) < 0)
                            {
                                sayi2++;
                                //derece2 += int.Parse(table.Rows[j]["Kötü"].ToString());
                            }
                        }
                    }
                    if (sayi1 != 0 && sayi2 != 0)
                        table2.Rows.Add(t1.AddDays(i).ToShortDateString(), sayi1 * 100 / (sayi1 + sayi2), sayi2 * 100 / (sayi1 + sayi2));
                    derece1 += sayi1;
                    derece2 += sayi2;
                    sayi1 = 0;
                    sayi2 = 0;



                }



                Chart1.DataSource = table2;
                Chart1.Series[0].XValueMember = "CreatedDate";
                Chart1.Series[0].YValueMembers = "İyi";
                Chart1.Series[0].Color = Color.FromArgb(141,198,48);

                Chart1.Series.Add("1");
                Chart1.Series[1].XValueMember = "CreatedDate";
                Chart1.Series[1].YValueMembers = "Kötü";
                Chart1.Series[1].Color = Color.FromArgb(228, 81, 54);


                Chart1.DataBind();
                GridView1.DataSource = table;

                GridView1.DataBind();

                try
                {
                    for (int i = 0; i < GridView1.Rows.Count - 1; i++)
                    {
                        if (int.Parse(GridView1.Rows[i].Cells[9].Text) < 0)
                            GridView1.Rows[i].BackColor = Color.FromArgb(228, 81, 54);
                        else if (int.Parse(GridView1.Rows[i].Cells[9].Text) > 0)
                            GridView1.Rows[i].BackColor = Color.FromArgb(141,198,48);
                    }
                }
                catch { }

                lblDurum.Text = "Toplam veri sayısı : " + GridView1.Rows.Count.ToString() + " Olumlu veri sayısı : " + derece1.ToString() + " Olumsuz veri sayısı : " + derece2.ToString() + " Toplam : " + (derece1 + derece2).ToString();

            }
            else
                lblDurum.Text = "Aranılan kritere uygun veri bulunamadı.";
        }

        protected void btnTweetKaydet_Click(object sender, EventArgs e)
        {
            sbyte yetki = sbyte.Parse(Session["kullanici_yetki"].ToString());
            if (CheckBox1.Checked == false)
                yetki = 2;
            miner.TwitterdanKaydet(txtSearch.Text, DateTime.Parse(txtTarih.Text), int.Parse(txtn.Text), int.Parse(Session["kullanici_id"].ToString()));
      
        }
    }
}