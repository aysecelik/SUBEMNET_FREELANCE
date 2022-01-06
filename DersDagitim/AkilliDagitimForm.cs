using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
  public class AkilliDagitimForm : Form
  {
    private bool iyilestir;
    private iyilestirmeliTaramaYap akilliDagitim;
    private int sayac;
    private int yuzde;
    private int maxYuzde;
    private string strYerlesmeyenSayisi;
    private bool bitti;
    private ulong cevirim;
    private string aciklama;
    private string[] enZorOgretmenler;
    private DataTable dtYerlesmeyenler;
    private Thread thrYenile;
    private IContainer components;
    private ProgressBar prbYerlesmeYuzdesi;
    private Label label1;
    private Label lblYerlesmeYuzdesi;
    private System.Windows.Forms.Timer timer1;
    private Label label2;
    private Label lblYerlesmeyenSayisi;
    private ListBox lstEnZorOgretmenler;
    private Label label3;
    private Label label4;
    private Button button2;
    private Label lblMaxYuzde;
    private Label label6;
    private DataGridView dgvYerlesmeyenler;
    private DataGridViewTextBoxColumn KolonDersAdi;
    private DataGridViewTextBoxColumn KolonSinifGruplar;
    private DataGridViewTextBoxColumn KolonOgretmenler;
    private DataGridViewTextBoxColumn KolonDerslikler;
    private Panel panel1;
    private Panel panel2;

    public AkilliDagitimForm(bool iyilestir = false)
    {
      this.InitializeComponent();
      this.iyilestir = iyilestir;
    }

    private void AkilliDagitimForm_Load(object sender, EventArgs e)
    {
      this.akilliDagitim = new iyilestirmeliTaramaYap(tanim.program, this.iyilestir);
      this.timer1.Enabled = true;
      this.thrYenile = new Thread(new ThreadStart(this.bilgileriYenile));
      this.thrYenile.Start();
    }

    private void bilgileriYenile()
    {
      lock (this.akilliDagitim.kilitle)
      {
        this.yuzde = this.akilliDagitim.yerlesenYuzde;
        this.maxYuzde = this.akilliDagitim.enYuksekYuzde;
        this.strYerlesmeyenSayisi = this.akilliDagitim.yerlesmeyenSayisi.ToString();
        this.dtYerlesmeyenler = this.akilliDagitim.dtYerlesmeyenler();
        this.cevirim = this.akilliDagitim.sayac;
        this.bitti = this.akilliDagitim.bitti;
        this.aciklama = "Süre: " + this.akilliDagitim.gecenSure + "  Çevirim: " + this.cevirim.ToString();
        if (this.sayac % 5 != 0)
          return;
        this.enZorOgretmenler = this.akilliDagitim.enZorOnOgretmen;
      }
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      if (this.sayac++ > 600)
      {
        this.sayac = 0;
        GC.Collect();
      }
      if (this.thrYenile.ThreadState == ThreadState.Stopped)
      {
        this.dgvYerlesmeyenler.DataSource = (object) this.dtYerlesmeyenler;
        this.lblYerlesmeyenSayisi.Text = this.strYerlesmeyenSayisi;
        if (this.sayac % 5 == 0 && this.enZorOgretmenler != null)
        {
          this.lstEnZorOgretmenler.Items.Clear();
          for (int index = 0; index < this.enZorOgretmenler.Length; ++index)
          {
            if (this.enZorOgretmenler[index] != null)
              this.lstEnZorOgretmenler.Items.Add((object) this.enZorOgretmenler[index]);
          }
        }
        this.Text = this.aciklama;
        this.prbYerlesmeYuzdesi.Value = this.yuzde;
        this.lblYerlesmeYuzdesi.Text = "%" + this.yuzde.ToString();
        this.lblMaxYuzde.Text = "%" + this.maxYuzde.ToString();
        this.thrYenile = new Thread(new ThreadStart(this.bilgileriYenile));
        this.thrYenile.Start();
      }
      if (!this.bitti)
        return;
      this.timer1.Enabled = false;
      this.thrYenile.Abort();
      araclar.marioMelodiCal();
      new Thread(new ThreadStart(this.istatistikGonder)).Start();
      int num = (int) MessageBox.Show((IWin32Window) this, string.Format("Yerleşim Gerçekleştirildi.\nHesaplama Süresi: {0}\nÇevirim: {1}", (object) this.akilliDagitim.gecenSure, (object) this.akilliDagitim.sayac));
      this.Close();
    }

    private void istatistikGonder()
    {
      if (tanim.program.okulMuduru == "TEST")
        return;
      string requestUriString = "http://bilgsoft.com/dagitmatik/dagitmatikLog.php";
      string s = "";
      string[] strArray1 = new string[9]
      {
        "pass",
        "versiyon",
        "okuladi",
        "mudur",
        "ogretmensay",
        "tanimliderssay",
        "sure",
        "cevirim",
        "tarihsaat"
      };
      string[] strArray2 = new string[9]
      {
        "dagitmatikOkulKayit",
        araclar.versiyon(),
        tanim.program.okulAdi,
        tanim.program.okulMuduru,
        tanim.program.ogretmenler.Count.ToString(),
        tanim.program.tanimliDersler.Count.ToString(),
        this.akilliDagitim.gecenSure,
        this.akilliDagitim.sayac.ToString(),
        string.Format("{0:yyyy-MM-dd HH:mm:ss}", (object) DateTime.Now)
      };
      for (int index = 1; index < strArray2.Length; ++index)
      {
        strArray2[index] = strArray2[index].ToUpper();
        strArray2[index] = strArray2[index].Replace('Ü', 'U');
        strArray2[index] = strArray2[index].Replace('Ö', 'O');
        strArray2[index] = strArray2[index].Replace('Ş', 'S');
        strArray2[index] = strArray2[index].Replace('İ', 'I');
        strArray2[index] = strArray2[index].Replace('Ç', 'C');
        strArray2[index] = strArray2[index].Replace('Ğ', 'G');
      }
      for (int index = 0; index < strArray1.Length; ++index)
      {
        s = s + strArray1[index] + "=" + strArray2[index];
        if (index + 1 != strArray1.Length)
          s += "&";
      }
      string empty = string.Empty;
      try
      {
        byte[] bytes = Encoding.ASCII.GetBytes(s);
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
        httpWebRequest.Method = "POST";
        httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
        httpWebRequest.ContentLength = (long) bytes.Length;
        using (Stream requestStream = httpWebRequest.GetRequestStream())
          requestStream.Write(bytes, 0, bytes.Length);
        using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
        {
          using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            streamReader.ReadToEnd();
        }
      }
      catch
      {
      }
    }

    private void AkilliDagitimForm_FormClosing(object sender, FormClosingEventArgs e) => this.akilliDagitim.durdur();

    private void button1_Click(object sender, EventArgs e)
    {
    }

    private void btnSifirla_Click(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.akilliDagitim.bitti || MessageBox.Show("Tarama sonlandırılacak emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.prbYerlesmeYuzdesi = new ProgressBar();
      this.label1 = new Label();
      this.lblYerlesmeYuzdesi = new Label();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.label2 = new Label();
      this.lblYerlesmeyenSayisi = new Label();
      this.lstEnZorOgretmenler = new ListBox();
      this.label3 = new Label();
      this.label4 = new Label();
      this.button2 = new Button();
      this.lblMaxYuzde = new Label();
      this.label6 = new Label();
      this.dgvYerlesmeyenler = new DataGridView();
      this.KolonDersAdi = new DataGridViewTextBoxColumn();
      this.KolonSinifGruplar = new DataGridViewTextBoxColumn();
      this.KolonOgretmenler = new DataGridViewTextBoxColumn();
      this.KolonDerslikler = new DataGridViewTextBoxColumn();
      this.panel1 = new Panel();
      this.panel2 = new Panel();
      ((ISupportInitialize) this.dgvYerlesmeyenler).BeginInit();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.prbYerlesmeYuzdesi.Location = new Point(3, 25);
      this.prbYerlesmeYuzdesi.Name = "prbYerlesmeYuzdesi";
      this.prbYerlesmeYuzdesi.Size = new Size(443, 23);
      this.prbYerlesmeYuzdesi.TabIndex = 0;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(3, 5);
      this.label1.Name = "label1";
      this.label1.Size = new Size(59, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Yerleşme : ";
      this.lblYerlesmeYuzdesi.AutoSize = true;
      this.lblYerlesmeYuzdesi.Location = new Point(57, 5);
      this.lblYerlesmeYuzdesi.Name = "lblYerlesmeYuzdesi";
      this.lblYerlesmeYuzdesi.Size = new Size(21, 13);
      this.lblYerlesmeYuzdesi.TabIndex = 2;
      this.lblYerlesmeYuzdesi.Text = "%0";
      this.timer1.Interval = 750;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(326, 3);
      this.label2.Name = "label2";
      this.label2.Size = new Size(101, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Yerleşmeyen Ders : ";
      this.lblYerlesmeyenSayisi.AutoSize = true;
      this.lblYerlesmeyenSayisi.Location = new Point(425, 3);
      this.lblYerlesmeyenSayisi.Name = "lblYerlesmeyenSayisi";
      this.lblYerlesmeyenSayisi.Size = new Size(13, 13);
      this.lblYerlesmeyenSayisi.TabIndex = 5;
      this.lblYerlesmeyenSayisi.Text = "0";
      this.lstEnZorOgretmenler.FormattingEnabled = true;
      this.lstEnZorOgretmenler.Location = new Point(6, 22);
      this.lstEnZorOgretmenler.Name = "lstEnZorOgretmenler";
      this.lstEnZorOgretmenler.Size = new Size(187, 147);
      this.lstEnZorOgretmenler.TabIndex = 7;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(3, 58);
      this.label3.Name = "label3";
      this.label3.Size = new Size(103, 13);
      this.label3.TabIndex = 8;
      this.label3.Text = "Yerleşmeyen Dersler";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(3, 6);
      this.label4.Name = "label4";
      this.label4.Size = new Size(103, 13);
      this.label4.TabIndex = 9;
      this.label4.Text = "En Zor 10 Öğretmen";
      this.button2.Location = new Point(6, 175);
      this.button2.Name = "button2";
      this.button2.Size = new Size(124, 66);
      this.button2.TabIndex = 11;
      this.button2.Text = "İptal/Çık";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.lblMaxYuzde.AutoSize = true;
      this.lblMaxYuzde.Location = new Point(206, 5);
      this.lblMaxYuzde.Name = "lblMaxYuzde";
      this.lblMaxYuzde.Size = new Size(21, 13);
      this.lblMaxYuzde.TabIndex = 13;
      this.lblMaxYuzde.Text = "%0";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(178, 5);
      this.label6.Name = "label6";
      this.label6.Size = new Size(33, 13);
      this.label6.TabIndex = 12;
      this.label6.Text = "Max :";
      this.dgvYerlesmeyenler.AllowUserToAddRows = false;
      this.dgvYerlesmeyenler.AllowUserToDeleteRows = false;
      this.dgvYerlesmeyenler.AllowUserToResizeColumns = false;
      this.dgvYerlesmeyenler.AllowUserToResizeRows = false;
      this.dgvYerlesmeyenler.BorderStyle = BorderStyle.None;
      this.dgvYerlesmeyenler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvYerlesmeyenler.Columns.AddRange((DataGridViewColumn) this.KolonDersAdi, (DataGridViewColumn) this.KolonSinifGruplar, (DataGridViewColumn) this.KolonOgretmenler, (DataGridViewColumn) this.KolonDerslikler);
      this.dgvYerlesmeyenler.Enabled = false;
      this.dgvYerlesmeyenler.EnableHeadersVisualStyles = false;
      this.dgvYerlesmeyenler.Location = new Point(3, 74);
      this.dgvYerlesmeyenler.MultiSelect = false;
      this.dgvYerlesmeyenler.Name = "dgvYerlesmeyenler";
      this.dgvYerlesmeyenler.ReadOnly = true;
      this.dgvYerlesmeyenler.RowHeadersVisible = false;
      this.dgvYerlesmeyenler.RowTemplate.Height = 18;
      this.dgvYerlesmeyenler.ScrollBars = ScrollBars.Vertical;
      this.dgvYerlesmeyenler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvYerlesmeyenler.Size = new Size(443, 483);
      this.dgvYerlesmeyenler.TabIndex = 14;
      this.KolonDersAdi.DataPropertyName = "dersadi";
      this.KolonDersAdi.HeaderText = "Ders Adı";
      this.KolonDersAdi.Name = "KolonDersAdi";
      this.KolonDersAdi.ReadOnly = true;
      this.KolonDersAdi.Width = 130;
      this.KolonSinifGruplar.DataPropertyName = "sinifgrup";
      this.KolonSinifGruplar.HeaderText = "Sınıf Gruplar";
      this.KolonSinifGruplar.Name = "KolonSinifGruplar";
      this.KolonSinifGruplar.ReadOnly = true;
      this.KolonSinifGruplar.Width = 80;
      this.KolonOgretmenler.DataPropertyName = "ogretmenler";
      this.KolonOgretmenler.HeaderText = "Öğretmen(ler)";
      this.KolonOgretmenler.Name = "KolonOgretmenler";
      this.KolonOgretmenler.ReadOnly = true;
      this.KolonOgretmenler.Width = 130;
      this.KolonDerslikler.DataPropertyName = "derslikler";
      this.KolonDerslikler.HeaderText = "Derslik(ler)";
      this.KolonDerslikler.Name = "KolonDerslikler";
      this.KolonDerslikler.ReadOnly = true;
      this.KolonDerslikler.Width = 80;
      this.panel1.BorderStyle = BorderStyle.FixedSingle;
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Controls.Add((Control) this.dgvYerlesmeyenler);
      this.panel1.Controls.Add((Control) this.prbYerlesmeYuzdesi);
      this.panel1.Controls.Add((Control) this.lblMaxYuzde);
      this.panel1.Controls.Add((Control) this.lblYerlesmeYuzdesi);
      this.panel1.Controls.Add((Control) this.label6);
      this.panel1.Controls.Add((Control) this.label2);
      this.panel1.Controls.Add((Control) this.lblYerlesmeyenSayisi);
      this.panel1.Controls.Add((Control) this.label3);
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(453, 557);
      this.panel1.TabIndex = 15;
      this.panel2.Controls.Add((Control) this.label4);
      this.panel2.Controls.Add((Control) this.lstEnZorOgretmenler);
      this.panel2.Controls.Add((Control) this.button2);
      this.panel2.Location = new Point(458, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(200, 557);
      this.panel2.TabIndex = 16;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(662, 562);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel1);
      this.Name = nameof (AkilliDagitimForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (AkilliDagitimForm);
      this.FormClosing += new FormClosingEventHandler(this.AkilliDagitimForm_FormClosing);
      this.Load += new EventHandler(this.AkilliDagitimForm_Load);
      ((ISupportInitialize) this.dgvYerlesmeyenler).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
