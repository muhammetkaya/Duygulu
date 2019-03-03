using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Duygulu;

namespace DuyguluDogrulama
{
    public partial class Sonuc : Form
    {
        public Sonuc()
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

        private void Sonuc_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM tweettabletrue");
            dataGridView1.DataSource = dt;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM tweettabletrue WHERE RealStatus != 0");
            

            Duygulu.Duygulu DDuygulu = new Duygulu.Duygulu();
            DataTable dtAnaliz = new DataTable();
            dtAnaliz = DDuygulu.Analiz(dt,1);
            
            for (int i=0;i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["VirtualStatus"] = dtAnaliz.Rows[i]["Fark"].ToString(); 
            }
            dt.Columns.Remove("İyi");
            dt.Columns.Remove("Kötü");
            dt.Columns.Remove("Fark");
            dataGridView1.DataSource = dt;
            Raporla();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Raporla();
        }
        private void Raporla()
        {
            int Giyi = 0, Gkotu = 0, Aiyi = 0, Akotu = 0, ayniSonuc = 0, Gbos = 0, Abos = 0;
            int g, a;
            //for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            //    if (int.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString()) == 0 || int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) == 0)
            //        dataGridView1.Rows.Remove(dataGridView1.Rows[i]);

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                g = 0;
                a = 0;

                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "-1")
                {
                    Gkotu++;
                    g = -1;
                }
                else
                    if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "1")
                    {
                        Giyi++;
                        g = 1;
                    }

                if (int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) < 0)
                {
                    Akotu++;
                    a = -1;
                }
                else
                    if (int.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString()) > 0)
                    {
                        Aiyi++;
                        a = 1;
                    }
                if (g == a)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                    ayniSonuc++;
                }
                else
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;


            }
            label1.Text = "Gerçek Olumlu  :  " + Giyi.ToString() + "   Gerçek Olumsuz  :  " + Gkotu.ToString() + " Olumluluk Oranı : " + ((Giyi * 100) / (Giyi + Gkotu));
            label2.Text = "Analizdeki Olumlu  :  " + Aiyi.ToString() + "    Analizdeki Olumsuz  :  " + Akotu.ToString() + " Olumluluk Oranı : " + ((Aiyi * 100) / (Aiyi + Akotu)).ToString();
            label3.Text = "Doğru İsabet :  " + ayniSonuc.ToString() + "           Yanlış  :  " + (dataGridView1.Rows.Count - 1 - ayniSonuc).ToString() + "  Oran : " + (ayniSonuc * 100 / (dataGridView1.Rows.Count - 1)).ToString() + "%";
       
        }
    }
}
