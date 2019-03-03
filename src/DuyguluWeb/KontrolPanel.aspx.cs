using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Duygulu;


namespace DuyguluWeb
{
    public partial class KontrolPanel : System.Web.UI.Page
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
            SqlDataSourceUser.SelectCommand = "SELECT Id, Name, Mail, Authorize, Positiv, Negativ FROM `appuser`";
            gvUser.DataBind();

            SqlDataSourceWord.SelectCommand = "SELECT * FROM wordtable WHERE UserId=0";
            gvKelime.DataBind();

            SqlDataSourceWordUser.SelectCommand = "SELECT * FROM wordtable WHERE UserId=" + Session["kullanici_id"].ToString();
            gvKelimeUser.DataBind();
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM appuser WHERE Id=" + Session["kullanici_id"].ToString());
            if (dt.Rows[0]["Preprocessing"].ToString() == "0")
                btnPreprocessing.BackColor = Color.Red;
            else
                btnPreprocessing.BackColor = Color.Green;
            if (dt.Rows[0]["StopWord"].ToString() == "0")
                btnStopWord.BackColor = Color.Red;
            else
                btnStopWord.BackColor = Color.Green;
            if (dt.Rows[0]["Spelling"].ToString() == "0")
                btnSpelling.BackColor = Color.Red;
            else
                btnSpelling.BackColor = Color.Green;

            if (!IsPostBack)
            {
                ddlSimilarity.Items.Clear();
                for (int i = 1; i < 101; i++) // benzerlik oranı yüzdesi
                    ddlSimilarity.Items.Add(i.ToString());

                ddlSimilarity.SelectedIndex = int.Parse(dt.Rows[0]["Similarity"].ToString()) - 1;
            }
        }
        private DataTable VeriGetir(string Query)
        {
            DataTable table = new DataTable();

            MySqlConnection connention = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            connention.Open();
            MySqlDataAdapter mySQLadapter = new MySqlDataAdapter(Query, connention);
            mySQLadapter.Fill(table);
            return table;
        }
        private DataTable Oku(string Query)
        {
            MySqlConnection connention = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            connention.Open();

            DataTable dt = new DataTable();
            MySqlDataAdapter mySQLadapter = new MySqlDataAdapter(Query, connention);
            mySQLadapter.Fill(dt);
            return dt;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            SqlDataSourceSearch.SelectCommand = "SELECT searchtable.SearchId,searchtable.SearchDate,searchtable.SearchText,searchtable.Positiv,searchtable.Negativ, appuser.Name FROM searchtable INNER JOIN appuser ON searchtable.UserId=appuser.Id WHERE SearchText LIKE '%" + txts1.Text + "%' OR Name LIKE '%" + txts1.Text + "%'";
            gvSearchtable.DataBind();

        }

        protected void btnUserSearch_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "")
                SqlDataSourceUser.SelectCommand = "SELECT Id, Name, Mail, Authorize, Positiv, Negativ FROM `appuser` WHERE `Name` LIKE '%" + txtName.Text + "%'";
            else
                SqlDataSourceUser.SelectCommand = "SELECT Id, Name, Mail, Authorize, Positiv, Negativ FROM `appuser`";
            gvUser.DataBind();
        }

        protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
        {

            SqlDataSourceSearch.SelectCommand = "SELECT searchtable.SearchId,searchtable.SearchDate,searchtable.SearchText,searchtable.Positiv,searchtable.Negativ, appuser.Name FROM searchtable INNER JOIN appuser ON searchtable.UserId=appuser.Id WHERE  Name LIKE '" + gvUser.SelectedRow.Cells[2].Text + "'";
            gvSearchtable.DataBind();
        }

        protected void btnAnaliz_Click(object sender, EventArgs e)
        {
            txtHazirlik.Text = miner.Hazirla(txtMetin.Text, int.Parse(Session["kullanici_id"].ToString()));
            gvAnaliz.DataSource = miner.TekAnaliz(txtMetin.Text, int.Parse(Session["kullanici_id"].ToString()));
            gvAnaliz.DataBind();
        }

        protected void btnKelimeAra_Click(object sender, EventArgs e)
        {
            if (txtKelimeAra.Text != "")
                SqlDataSourceWord.SelectCommand = "SELECT * FROM wordtable WHERE  UserId=0 AND Word LIKE '%" + txtKelimeAra.Text + "%'";
            else
                SqlDataSourceWord.SelectCommand = "SELECT * FROM wordtable WHERE UserId=0";

            gvKelime.DataBind();
        }

        protected void gvSearchtable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvSearchtable_DataBound(object sender, EventArgs e)
        {

        }

        protected void gvSearchtable_Load(object sender, EventArgs e)
        {
            if (gvSearchtable.Rows.Count > 0)
            {
                int iyi = 0, kotu = 0;
                for (int i = 0; i < gvSearchtable.Rows.Count; i++)
                {
                    iyi += int.Parse(gvSearchtable.Rows[i].Cells[3].Text);
                    kotu += int.Parse(gvSearchtable.Rows[i].Cells[4].Text);
                }
                gvSearchtable.Columns[3].HeaderText = "Olumlu (" + iyi.ToString() + ")";
                gvSearchtable.Columns[4].HeaderText = "Olumsuz (" + kotu.ToString() + ")";
            }
        }

        protected void gvUser_Load(object sender, EventArgs e)
        {
            if (gvUser.Rows.Count > 0)
            {
                int iyi = 0, kotu = 0;
                for (int i = 0; i < gvUser.Rows.Count; i++)
                {
                    iyi += int.Parse(gvUser.Rows[i].Cells[4].Text);
                    kotu += int.Parse(gvUser.Rows[i].Cells[5].Text);
                }
                gvUser.Columns[4].HeaderText = "Olumlu (" + iyi.ToString() + ")";
                gvUser.Columns[5].HeaderText = "Olumsuz (" + kotu.ToString() + ")";
            }
        }

        protected void gvAnaliz_Load(object sender, EventArgs e)
        {
            try
            {
                if (gvAnaliz.Rows.Count > 0)
                {
                    int iyi = 0, kotu = 0;
                    for (int i = 0; i < gvAnaliz.Rows.Count; i++)
                    {
                        iyi += int.Parse(gvAnaliz.Rows[i].Cells[2].Text);
                        kotu += int.Parse(gvAnaliz.Rows[i].Cells[3].Text);
                    }
                    gvAnaliz.Columns[2].HeaderText += " (" + iyi.ToString() + ")";
                    gvAnaliz.Columns[3].HeaderText += " (" + kotu.ToString() + ")";
                }
            }
            catch { }
        }

        protected void Button1_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void btnKelimeAraUser_Click(object sender, EventArgs e)
        {
            if (txtKelimeUser.Text != "")
                SqlDataSourceWordUser.SelectCommand = "SELECT * FROM wordtable WHERE UserId=" + Session["kullanici_id"].ToString() + " AND Word LIKE '%" + txtKelimeUser.Text + "%'";
            else
                SqlDataSourceWordUser.SelectCommand = "SELECT * FROM wordtable WHERE UserId=" + Session["kullanici_id"].ToString();
            gvKelimeUser.DataBind();
        }

        protected void ddlPositiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPositiv.SelectedIndex != 0)
                ddlNegativ.SelectedIndex = 0;
        }

        protected void ddlNegativ_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNegativ.SelectedIndex != 0)
                ddlPositiv.SelectedIndex = 0;
        }

        protected void btnKaydet_Click(object sender, EventArgs e)
        {
            if (lblKelimeUserId.Text == "0" && txtWordUser.Text.Length > 2 && (ddlNegativ.SelectedIndex > 0 || ddlPositiv.SelectedIndex > 0))
                KomutCalistir("INSERT INTO `wordtable`( `Word`, `Positiv`, `Negativ`, `Type`, `UserId`) VALUES ('" + txtWordUser.Text + "'," + ddlPositiv.SelectedIndex + "," + ddlNegativ.SelectedIndex + ",0," + Session["kullanici_id"].ToString() + ")");
            if (lblKelimeUserId.Text != "0" && txtWordUser.Text.Length > 2 && (ddlNegativ.SelectedIndex > 0 || ddlPositiv.SelectedIndex > 0))
                KomutCalistir("UPDATE `wordtable` SET `Word`='" + txtWordUser.Text + "',`Positiv`=" + ddlPositiv.SelectedIndex.ToString() + ",`Negativ`=" + ddlNegativ.SelectedIndex.ToString() + " WHERE `Id`=" + lblKelimeUserId.Text);
            gvKelimeUser.DataBind();
            lblKelimeUserId.Text = "0";
            txtWordUser.Text = "";
            ddlNegativ.SelectedIndex = 0;
            ddlPositiv.SelectedIndex = 0;

        }
        protected void KelimeSil(int KelimeId)
        {
            KomutCalistir("DELETE FROM `wordtable` WHERE `Id`=" + KelimeId.ToString());
            gvKelimeUser.DataBind();
            lblKelimeUserId.Text = "0";
            txtWordUser.Text = "";
            ddlNegativ.SelectedIndex = 0;
            ddlPositiv.SelectedIndex = 0;
        }

        protected void KomutCalistir(string komut)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(komut, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void gvKelimeUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblKelimeUserId.Text = gvKelimeUser.SelectedRow.Cells[1].Text;
            txtWordUser.Text = gvKelimeUser.SelectedRow.Cells[2].Text;
            ddlPositiv.SelectedIndex = int.Parse(gvKelimeUser.SelectedRow.Cells[3].Text);
            ddlNegativ.SelectedIndex = int.Parse(gvKelimeUser.SelectedRow.Cells[4].Text);
        }

        protected void btnKelimeUserSil_Click(object sender, EventArgs e)
        {
            KelimeSil(int.Parse(lblKelimeUserId.Text));
            lblKelimeUserId.Text = "0";
        }

        protected void btnPreprocessing_Click(object sender, EventArgs e)
        {
            if (btnPreprocessing.BackColor == Color.Green)
            {
                KomutCalistir("UPDATE `appuser` SET `Preprocessing`=0 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnPreprocessing.BackColor = Color.Red;
                btnSpelling.Visible = false;
                btnStopWord.Visible = false;

            }
            else
            {
                KomutCalistir("UPDATE `appuser` SET `Preprocessing`=1 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnPreprocessing.BackColor = Color.Green;
                btnSpelling.Visible = true;
                btnStopWord.Visible = true;
            }
        }

        protected void btnStopWord_Click(object sender, EventArgs e)
        {
            if (btnStopWord.BackColor == Color.Green)
            {
                KomutCalistir("UPDATE `appuser` SET `StopWord`=0 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnStopWord.BackColor = Color.Red;
            }
            else
            {
                KomutCalistir("UPDATE `appuser` SET `StopWord`=1 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnStopWord.BackColor = Color.Green;
            }
        }

        protected void btnSpelling_Click(object sender, EventArgs e)
        {
            if (btnSpelling.BackColor == Color.Green)
            {
                KomutCalistir("UPDATE `appuser` SET `Spelling`=0 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnSpelling.BackColor = Color.Red;
            }
            else
            {
                KomutCalistir("UPDATE `appuser` SET `Spelling`=1 WHERE `Id`=" + Session["kullanici_id"].ToString());
                btnSpelling.BackColor = Color.Green;
            }
        }

        protected void ddlSimilarity_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void ddlSimilarity_TextChanged(object sender, EventArgs e)
        {
            KomutCalistir("UPDATE `appuser` SET `Similarity`=" + ddlSimilarity.Text+ " WHERE `Id`=" + Session["kullanici_id"].ToString());

        }


     

    }
}