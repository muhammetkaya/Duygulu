namespace DuyguluDogrulama
{
    partial class frmDogrulama
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnKaydet = new System.Windows.Forms.Button();
            this.txtArama = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBos = new System.Windows.Forms.Button();
            this.lblId = new System.Windows.Forms.Label();
            this.btnOlumlu = new System.Windows.Forms.Button();
            this.btnOlumsuz = new System.Windows.Forms.Button();
            this.btnIleri = new System.Windows.Forms.Button();
            this.btnGeri = new System.Windows.Forms.Button();
            this.lblTweet = new System.Windows.Forms.Label();
            this.lblDurum = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnKaydet
            // 
            this.btnKaydet.Location = new System.Drawing.Point(118, 12);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(55, 23);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = true;
            this.btnKaydet.Click += new System.EventHandler(this.btnKaydet_Click);
            this.btnKaydet.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnKaydet_KeyDown);
            // 
            // txtArama
            // 
            this.txtArama.Location = new System.Drawing.Point(12, 12);
            this.txtArama.Name = "txtArama";
            this.txtArama.Size = new System.Drawing.Size(100, 20);
            this.txtArama.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 38);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(748, 122);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CurrentCellChanged += new System.EventHandler(this.dataGridView1_CurrentCellChanged);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBos);
            this.groupBox1.Controls.Add(this.lblId);
            this.groupBox1.Controls.Add(this.btnOlumlu);
            this.groupBox1.Controls.Add(this.btnOlumsuz);
            this.groupBox1.Controls.Add(this.btnIleri);
            this.groupBox1.Controls.Add(this.btnGeri);
            this.groupBox1.Controls.Add(this.lblTweet);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 182);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(764, 202);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tweet Değerlendirme";
            // 
            // btnBos
            // 
            this.btnBos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBos.Location = new System.Drawing.Point(406, 18);
            this.btnBos.Name = "btnBos";
            this.btnBos.Size = new System.Drawing.Size(344, 72);
            this.btnBos.TabIndex = 4;
            this.btnBos.Text = "DEĞERLENDİRİLEMEZ  ( W )";
            this.btnBos.UseVisualStyleBackColor = true;
            this.btnBos.Click += new System.EventHandler(this.btnBos_Click);
            this.btnBos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBos_KeyDown);
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(13, 25);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(16, 13);
            this.lblId.TabIndex = 3;
            this.lblId.Text = "Id";
            // 
            // btnOlumlu
            // 
            this.btnOlumlu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOlumlu.Location = new System.Drawing.Point(580, 96);
            this.btnOlumlu.Name = "btnOlumlu";
            this.btnOlumlu.Size = new System.Drawing.Size(170, 100);
            this.btnOlumlu.TabIndex = 2;
            this.btnOlumlu.Text = "OLUMLU   ( E )";
            this.btnOlumlu.UseVisualStyleBackColor = true;
            this.btnOlumlu.Click += new System.EventHandler(this.btnOlumlu_Click);
            this.btnOlumlu.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOlumlu_KeyDown);
            // 
            // btnOlumsuz
            // 
            this.btnOlumsuz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOlumsuz.Location = new System.Drawing.Point(406, 96);
            this.btnOlumsuz.Name = "btnOlumsuz";
            this.btnOlumsuz.Size = new System.Drawing.Size(170, 100);
            this.btnOlumsuz.TabIndex = 2;
            this.btnOlumsuz.Text = "OLUMSUZ  ( Q )";
            this.btnOlumsuz.UseVisualStyleBackColor = true;
            this.btnOlumsuz.Click += new System.EventHandler(this.btnOlumsuz_Click);
            this.btnOlumsuz.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnOlumsuz_KeyDown);
            // 
            // btnIleri
            // 
            this.btnIleri.Location = new System.Drawing.Point(248, 18);
            this.btnIleri.Name = "btnIleri";
            this.btnIleri.Size = new System.Drawing.Size(150, 25);
            this.btnIleri.TabIndex = 1;
            this.btnIleri.Text = ">>>>>>>";
            this.btnIleri.UseVisualStyleBackColor = true;
            this.btnIleri.Click += new System.EventHandler(this.btnIleri_Click);
            // 
            // btnGeri
            // 
            this.btnGeri.Location = new System.Drawing.Point(92, 19);
            this.btnGeri.Name = "btnGeri";
            this.btnGeri.Size = new System.Drawing.Size(150, 24);
            this.btnGeri.TabIndex = 1;
            this.btnGeri.Text = "<<<<<<<";
            this.btnGeri.UseVisualStyleBackColor = true;
            this.btnGeri.Click += new System.EventHandler(this.btnGeri_Click);
            // 
            // lblTweet
            // 
            this.lblTweet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTweet.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTweet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTweet.Location = new System.Drawing.Point(12, 46);
            this.lblTweet.Name = "lblTweet";
            this.lblTweet.Size = new System.Drawing.Size(386, 153);
            this.lblTweet.TabIndex = 0;
            this.lblTweet.Text = "lblTweet";
            // 
            // lblDurum
            // 
            this.lblDurum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDurum.AutoSize = true;
            this.lblDurum.Location = new System.Drawing.Point(283, 12);
            this.lblDurum.Name = "lblDurum";
            this.lblDurum.Size = new System.Drawing.Size(68, 13);
            this.lblDurum.TabIndex = 4;
            this.lblDurum.Text = "Kayıt durumu";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(179, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Sil";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(230, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(47, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Sonuç";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // frmDogrulama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 384);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblDurum);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtArama);
            this.Controls.Add(this.btnKaydet);
            this.MaximumSize = new System.Drawing.Size(780, 1000);
            this.MinimumSize = new System.Drawing.Size(780, 39);
            this.Name = "frmDogrulama";
            this.Text = "Tweet Doğrulama";
            this.Load += new System.EventHandler(this.frmDogrulama_Load);
            this.SizeChanged += new System.EventHandler(this.frmDogrulama_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDogrulama_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnKaydet;
        private System.Windows.Forms.TextBox txtArama;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOlumlu;
        private System.Windows.Forms.Button btnOlumsuz;
        private System.Windows.Forms.Button btnIleri;
        private System.Windows.Forms.Button btnGeri;
        private System.Windows.Forms.Label lblTweet;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Button btnBos;
        private System.Windows.Forms.Label lblDurum;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

