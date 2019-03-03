using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LinqToTwitter;
using MySql.Data.MySqlClient;
using Duygulu;

namespace DuyguluDogrulama
{
    public partial class frmDogrulama : Form
    {
        string ConsumerKey = "";
        string ConsumerSecret = "";
        string AccessToken = "";
        string AccessTokenSecret = "";
        DataTable TweetTablo;
        public frmDogrulama()
        {
            InitializeComponent();
        }
        private DataTable Oku(string Query)
        {
            MySqlConnection connention = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            connention.Open();

            DataTable dt = new DataTable();
            MySqlDataAdapter mySQLadapter = new MySqlDataAdapter(Query, connention);
            mySQLadapter.Fill(dt);
            connention.Close();
            return dt;
        }
        private void KomutCalistir(string Query)
        {

            MySqlConnection connention = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            connention.Open();
            MySqlCommand cmd = new MySqlCommand(Query, connention);
            cmd.ExecuteNonQuery();
            connention.Close();

        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TweetKaydet(txtArama.Text, 5);
            dataGridView1.DataSource = Oku("SELECT * FROM `tweettabletrue`");
            GridYenile(0);

        }
        public void TweetKaydet(string SearchText,int n)
        {
            DataTable dt = new DataTable();
            decimal MaxID=0;
            var auth = new SingleUserAuthorizer
            {
                Credentials = new SingleUserInMemoryCredentials
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    TwitterAccessToken = AccessToken,
                    TwitterAccessTokenSecret = AccessTokenSecret
                }
            };
            TwitterContext twitterContex = new TwitterContext(auth);

            for (int i = 0; i < n; i++)
            {
                var srch = (from search in twitterContex.Search
                            where search.Type == SearchType.Search &&
                                  search.MaxID == MaxID &&
                                  search.Query == SearchText &&
                                  search.SearchLanguage == "tr" &&
                                  search.Count == 100
                            select search).SingleOrDefault();

                foreach (var Tweet in srch.Statuses)
                {
                    dt = Oku("SELECT `Id` FROM `tweettabletrue` WHERE `Text` = '" + Tweet.Text.Replace("'"," ") + "'");
                    if (dt.Rows.Count == 0)
                    {
                        KomutCalistir("INSERT INTO `tweettabletrue`( `CreatedDate`, `UserId`, `Text`) VALUES ('" + Tweet.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss") + "'," + Tweet.User.Identifier.UserID.ToString() + ",'" + Tweet.Text.Replace("'", " ") + "')");
                    }
                    MaxID =decimal.Parse(Tweet.StatusID);

                }

            }   
        }
        private void TweetYaz(int index)
        {
            btnOlumlu.BackColor = Color.Silver;
            btnOlumsuz.BackColor = Color.Silver;
            btnBos.BackColor = Color.Silver;

            lblId.Text = dataGridView1.Rows[index].Cells["Id"].Value.ToString() ; //TweetTablo.Rows[no]["Id"].ToString();
            lblTweet.Text = dataGridView1.Rows[index].Cells["Text"].Value.ToString();
            if (dataGridView1.Rows[index].Cells["RealStatus"].Value.ToString()  == "-1")
                btnOlumsuz.BackColor = Color.Blue;
            if (dataGridView1.Rows[index].Cells["RealStatus"].Value.ToString() == "1")
                btnOlumlu.BackColor = Color.Blue;
            if (dataGridView1.Rows[index].Cells["RealStatus"].Value.ToString() == "0")
            {
                btnOlumlu.BackColor = Color.Silver ;
                btnOlumsuz.BackColor = Color.Silver;
                btnBos.BackColor = Color.Blue;
            }
            
        }
        private void GridYenile(int no)
        {
            int iyi=0, kotu = 0;
            TweetTablo = Oku("SELECT * FROM `tweettabletrue`");
            dataGridView1.DataSource = TweetTablo;
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells["RealStatus"].Value.ToString() == "-1")
                {
                    kotu++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                if (dataGridView1.Rows[i].Cells["RealStatus"].Value.ToString() == "0")
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                if (dataGridView1.Rows[i].Cells["RealStatus"].Value.ToString() == "1")
                {
                    iyi++;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
            }
            lblDurum.Text ="Toplam Tweet Sayısı : "+ (dataGridView1.Rows.Count-1).ToString()+"        Olumlu Tweet Sayısı : "+iyi.ToString()+"           Olumsuz Tweet Sayısı : "+kotu.ToString();
            dataGridView1.Rows[no].Cells["Id"].Selected = true;
        }

       
        private void btnIleri_Click(object sender, EventArgs e)
        {

            if (dataGridView1.CurrentRow.Index < dataGridView1.Rows.Count)
            dataGridView1.Rows[dataGridView1.CurrentRow.Index + 1].Cells["Id"].Selected = true;
            
        }
        
        private void frmDogrulama_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            GridYenile(0);
            dataGridView1.Width = this.Width - 20;
            dataGridView1.Height = this.Height - groupBox1.Height - 70;
          
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow.Index>0)
                dataGridView1.Rows[dataGridView1.CurrentRow.Index-1].Cells["Id"].Selected = true;
            
        }

        private void btnOlumsuz_Click(object sender, EventArgs e)
        {
            KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = "+lblId.Text); 
            GridYenile(dataGridView1.CurrentRow.Index + 1);
        }

        private void btnOlumlu_Click(object sender, EventArgs e)
        {
            KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
            GridYenile(dataGridView1.CurrentRow.Index + 1);
        }

        private void btnBos_Click(object sender, EventArgs e)
        {
            KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);

            GridYenile(dataGridView1.CurrentRow.Index + 1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            TweetYaz(e.RowIndex);
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                
                TweetYaz(dataGridView1.CurrentRow.Index);
            }
            catch{}
        }

        private void frmDogrulama_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width-20;
            dataGridView1.Height = this.Height - groupBox1.Height-70;
        }

        private void frmDogrulama_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if(e.KeyCode==Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if(e.KeyCode==Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void btnOlumsuz_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void btnOlumlu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void btnBos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void btnKaydet_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Q)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=-1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.W)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=0 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
            if (e.KeyCode == Keys.E)
            {
                KomutCalistir("UPDATE `tweettabletrue` SET `RealStatus`=1 WHERE `Id` = " + lblId.Text);
                GridYenile(dataGridView1.CurrentRow.Index + 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KomutCalistir("DELETE FROM `tweettabletrue` WHERE `Text` LIKE '%"+txtArama.Text+"%'");
            GridYenile(0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sonuc frm = new Sonuc();
            frm.Show();
        }

        
    }
}
