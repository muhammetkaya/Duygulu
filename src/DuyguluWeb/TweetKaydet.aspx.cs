using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace DuyguluWeb
{
    public partial class TweetKaydet : System.Web.UI.Page
    {
        Duygulu.Duygulu miner = new Duygulu.Duygulu();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["kullanici_id"] = 1;
            Session["kullanici_yetki"] = 1;
            try
            {
                if (Session["kullanici_id"].ToString() != "0")
                {
                    if (Session["kullanici_yetki"].ToString() != "1")
                        Response.Redirect("Default.aspx");
                }
                else
                    Response.Redirect("Default.aspx");
            }
            catch { Response.Redirect("Login.aspx"); }

            SqlDataSourceSearch.SelectCommand = "SELECT searchtable.SearchId,searchtable.SearchDate,searchtable.SearchText,searchtable.Positiv,searchtable.Negativ, appuser.Name FROM searchtable INNER JOIN appuser ON searchtable.UserId=appuser.Id";
            gvSearchtable.DataBind();
          
        }
        protected void gvSearchtable_Load(object sender, EventArgs e)
        {
            if (gvSearchtable.Rows.Count > 0)
            {
                int iyi = 0, kotu = 0;
                for (int i = 0; i < gvSearchtable.Rows.Count; i++)
                {
                    iyi += int.Parse(gvSearchtable.Rows[i].Cells[2].Text);
                    kotu += int.Parse(gvSearchtable.Rows[i].Cells[3].Text);
                }
                gvSearchtable.Columns[2].HeaderText = "Olumlu (" + iyi.ToString() + ")";
                gvSearchtable.Columns[3].HeaderText = "Olumsuz (" + kotu.ToString() + ")";
            }
        }

        protected void btnTweetKaydet_Click(object sender, EventArgs e)
        {
            sbyte yetki = sbyte.Parse(Session["kullanici_yetki"].ToString());
            if (CheckBox1.Checked == false)
                yetki = 2;
           gvTweet.DataSource= miner.TwitterdanKaydet(txtSearch.Text, DateTime.Parse(txtTarih.Text), int.Parse(txtn.Text), int.Parse(Session["kullanici_id"].ToString()));
           

        }

    }
}