using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class ElProgramiFrm : Form
  {
    private DataTable dtListe;
    private List<bilesenTaban> seciliNesneler = new List<bilesenTaban>();
    private int sayfa;
    private IContainer components;
    private Panel panel1;
    private RadioButton rbDerslik;
    private RadioButton rbOgretmen;
    private RadioButton rbSinif;
    private Panel panel2;
    private Panel panel3;
    private Button button3;
    private Button button2;
    private Button button1;
    private ListBox lstListe;
    private GroupBox groupBox1;
    private Label label1;
    private DateTimePicker dtpTeslimTarihi;
    private Panel pnlSerbest;
    private Panel pnlMeb;
    private RadioButton rbSerbest;
    private RadioButton rbMeb;
    private TextBox txtBaslik;
    private TextBox txtAltBilgi;
    private TextBox txtBilesenBilgi;
    private Label label4;
    private Label label3;
    private Label label2;
    private Label lblBilgiler;

    public ElProgramiFrm()
    {
      this.InitializeComponent();
      this.dtListe = new DataTable();
      this.dtListe.Columns.Add("id", typeof (ushort));
      this.dtListe.Columns.Add("adi", typeof (string));
      this.lstListe.DisplayMember = "adi";
      this.lstListe.ValueMember = "id";
      this.lstListe.DataSource = (object) this.dtListe;
    }

    private void radioButton2_CheckedChanged(object sender, EventArgs e)
    {
      this.dtListe.Rows.Clear();
      foreach (bilesenDerslik bilesenDerslik in tanim.program.derslikler)
        this.dtListe.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi);
    }

    private void rbOgretmen_CheckedChanged(object sender, EventArgs e)
    {
      this.dtListe.Rows.Clear();
      foreach (bilesenOgretmen bilesenOgretmen in tanim.program.ogretmenler)
        this.dtListe.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi);
    }

    private void rbSinif_CheckedChanged(object sender, EventArgs e)
    {
      this.dtListe.Rows.Clear();
      foreach (bilesenSinif bilesenSinif in tanim.program.siniflar)
        this.dtListe.Rows.Add((object) bilesenSinif.id, (object) bilesenSinif.adi);
    }

    private void button1_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.lstListe.Items.Count; ++index)
        this.lstListe.SetSelected(index, true);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      for (int index = 0; index < this.lstListe.Items.Count; ++index)
        this.lstListe.SetSelected(index, false);
    }

    private void button3_Click(object sender, EventArgs e)
    {
      this.seciliNesneler.Clear();
      if (this.rbOgretmen.Checked)
      {
        for (int index = 0; index < this.lstListe.Items.Count; ++index)
        {
          if (this.lstListe.GetSelected(index))
            this.seciliNesneler.Add((bilesenTaban) tanim.program.ogretmenGetir(Convert.ToUInt16(this.dtListe.Rows[index]["id"])));
        }
      }
      if (this.rbDerslik.Checked)
      {
        for (int index = 0; index < this.lstListe.Items.Count; ++index)
        {
          if (this.lstListe.GetSelected(index))
            this.seciliNesneler.Add((bilesenTaban) tanim.program.derslikGetir(Convert.ToUInt16(this.dtListe.Rows[index]["id"])));
        }
      }
      if (this.rbSinif.Checked)
      {
        for (int index = 0; index < this.lstListe.Items.Count; ++index)
        {
          if (this.lstListe.GetSelected(index))
            this.seciliNesneler.Add((bilesenTaban) tanim.program.sinifGetir(Convert.ToUInt16(this.dtListe.Rows[index]["id"])));
        }
      }
      if (this.seciliNesneler.Count <= 0)
        return;
      PrintDocument printDocument = new PrintDocument();
      printDocument.PrintPage += new PrintPageEventHandler(this.RaporPrint);
      printDocument.DocumentName = "Ders Programı raporu";
      PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
      printPreviewDialog.Document = printDocument;
      printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
      ((Form) printPreviewDialog).WindowState = FormWindowState.Maximized;
      this.sayfa = 0;
      int num = (int) printPreviewDialog.ShowDialog();
    }

    private string strFormatla(string str, bilesenTaban bilesen)
    {
      string[] strArray1 = new string[5]
      {
        "{0}",
        "{1}",
        "{2}",
        "{3}",
        "{4}"
      };
      string adi = bilesen.adi;
      string str1 = "";
      if (bilesen is bilesenOgretmen)
        str1 = "Öğretmen";
      if (bilesen is bilesenDerslik)
        str1 = "Derslik";
      if (bilesen is bilesenSinif)
        str1 = "Sınıf";
      string shortDateString = DateTime.Now.ToShortDateString();
      string[] strArray2 = new string[5]
      {
        str1,
        adi,
        shortDateString,
        tanim.program.okulAdi,
        tanim.program.ogretimYili
      };
      for (int index = 0; index < strArray1.Length; ++index)
        str = str.Replace(strArray1[index], strArray2[index]);
      return str;
    }

    private void RaporPrint(object nesne, PrintPageEventArgs e)
    {
      e.HasMorePages = true;
      int width = e.PageBounds.Width;
      int height = e.PageBounds.Height;
      Font font1 = new Font("Times New Roman", 11f, FontStyle.Bold);
      Font font2 = new Font("Times New Roman", 11.5f, FontStyle.Bold);
      Font font3 = new Font("Times New Roman", 12f, FontStyle.Italic);
      Font font4 = new Font("Times New Roman", 10f, FontStyle.Regular);
      Font font5 = new Font("Times New Roman", 10f, FontStyle.Italic);
      Font font6 = new Font("Times New Roman", 10f, FontStyle.Bold);
      Font font7 = new Font("Times New Roman", 14f, FontStyle.Bold);
      SolidBrush solidBrush = new SolidBrush(Color.Black);
      StringFormat stringFormat1 = new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center
      };
      StringFormat stringFormat2 = new StringFormat();
      StringFormat stringFormat3 = new StringFormat()
      {
        LineAlignment = StringAlignment.Far,
        Alignment = StringAlignment.Far
      };
      StringFormat stringFormat4 = new StringFormat()
      {
        LineAlignment = StringAlignment.Far,
        Alignment = StringAlignment.Near
      };
      StringFormat format = new StringFormat();
      format.LineAlignment = StringAlignment.Near;
      format.Alignment = StringAlignment.Center;
      StringFormat stringFormat5 = new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center,
        FormatFlags = StringFormatFlags.DirectionVertical
      };
      string s1 = "";
      if (this.rbMeb.Checked)
        s1 = string.Format("{0} MÜDÜRLÜĞÜ\n{1} EĞİTİM ÖĞRETİM YILI DERS PROGRAMI BİLGİSİ", (object) tanim.program.okulAdi, (object) tanim.program.ogretimYili);
      if (this.rbSerbest.Checked)
        s1 = this.strFormatla(this.txtBaslik.Text, this.seciliNesneler[this.sayfa]);
      e.Graphics.DrawString(s1, font1, (Brush) solidBrush, (RectangleF) new Rectangle(50, 50, width - 100, 100), format);
      if (this.rbMeb.Checked)
      {
        if (this.seciliNesneler[this.sayfa] is bilesenOgretmen)
        {
          string s2 = string.Format("Sayı\t\t:.........................................\nAdı Soyadı\t: {0}", (object) this.seciliNesneler[this.sayfa].adi);
          e.Graphics.DrawString(s2, font4, (Brush) solidBrush, (PointF) new Point(50, 100));
          string s3 = string.Format("Sınıf Öğretmenliği :..........................................\nEğitici Kulüp :..................................................\nNöbet Günü ve Yeri :.......................................");
          e.Graphics.DrawString(s3, font4, (Brush) solidBrush, (PointF) new Point(450, 95));
        }
        else
        {
          string str = "";
          if (this.seciliNesneler[this.sayfa] is bilesenDerslik)
            str = " DERSLİĞİ DERS PROGRAMI";
          if (this.seciliNesneler[this.sayfa] is bilesenSinif)
            str = " SINIFI DERS PROGRAMI";
          e.Graphics.DrawString(this.seciliNesneler[this.sayfa].adi + str, font7, (Brush) solidBrush, (RectangleF) new Rectangle(50, 100, width - 100, 50), format);
        }
      }
      if (this.rbSerbest.Checked)
      {
        string s2 = this.strFormatla(this.txtBilesenBilgi.Text, this.seciliNesneler[this.sayfa]);
        e.Graphics.DrawString(s2, font4, (Brush) solidBrush, (PointF) new Point(50, 100));
      }
      araclar.dersProgramiCizelgesi(this.seciliNesneler[this.sayfa], e.Graphics, 80, 150);
      if (this.rbMeb.Checked)
      {
        if (this.seciliNesneler[this.sayfa] is bilesenOgretmen)
        {
          string s2 = string.Format("Yukarıdaki dersler {0} tarihinde şahsınıza verilmiştir. Bilgilerinizi rica ederim.\n\n...../..../.........\nAslını Aldım.", (object) this.dtpTeslimTarihi.Value.ToShortDateString());
          e.Graphics.DrawString(s2, font4, (Brush) solidBrush, (PointF) new Point(50, 150 + ((int) tanim.program.gunlukDersSaatiSayisi + 1) * 60));
        }
        string s3 = string.Format("{0}\nOkul Müdürü", (object) tanim.program.okulMuduru);
        e.Graphics.DrawString(s3, font4, (Brush) solidBrush, (RectangleF) new Rectangle(width - 250, 250 + ((int) tanim.program.gunlukDersSaatiSayisi + 1) * 60, 150, 200), format);
      }
      if (this.rbSerbest.Checked)
      {
        string s2 = this.strFormatla(this.txtAltBilgi.Text, this.seciliNesneler[this.sayfa]);
        e.Graphics.DrawString(s2, font4, (Brush) solidBrush, (PointF) new Point(50, 150 + ((int) tanim.program.gunlukDersSaatiSayisi + 1) * 60));
      }
      if (++this.sayfa < this.seciliNesneler.Count)
        return;
      e.HasMorePages = false;
      this.sayfa = 0;
    }

    private void sitilSecimPanelDegistir(object sender, EventArgs e)
    {
      this.pnlMeb.Visible = this.rbMeb.Checked;
      this.pnlSerbest.Visible = this.rbSerbest.Checked;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.panel1 = new Panel();
      this.rbSinif = new RadioButton();
      this.rbDerslik = new RadioButton();
      this.rbOgretmen = new RadioButton();
      this.panel2 = new Panel();
      this.lstListe = new ListBox();
      this.panel3 = new Panel();
      this.groupBox1 = new GroupBox();
      this.pnlSerbest = new Panel();
      this.pnlMeb = new Panel();
      this.label1 = new Label();
      this.dtpTeslimTarihi = new DateTimePicker();
      this.rbSerbest = new RadioButton();
      this.rbMeb = new RadioButton();
      this.button3 = new Button();
      this.button2 = new Button();
      this.button1 = new Button();
      this.txtBaslik = new TextBox();
      this.txtBilesenBilgi = new TextBox();
      this.txtAltBilgi = new TextBox();
      this.label2 = new Label();
      this.label3 = new Label();
      this.label4 = new Label();
      this.lblBilgiler = new Label();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.pnlSerbest.SuspendLayout();
      this.pnlMeb.SuspendLayout();
      this.SuspendLayout();
      this.panel1.Controls.Add((Control) this.rbSinif);
      this.panel1.Controls.Add((Control) this.rbDerslik);
      this.panel1.Controls.Add((Control) this.rbOgretmen);
      this.panel1.Dock = DockStyle.Top;
      this.panel1.Location = new Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(557, 40);
      this.panel1.TabIndex = 1;
      this.rbSinif.AutoSize = true;
      this.rbSinif.Location = new Point(175, 12);
      this.rbSinif.Name = "rbSinif";
      this.rbSinif.Size = new Size(56, 17);
      this.rbSinif.TabIndex = 2;
      this.rbSinif.TabStop = true;
      this.rbSinif.Text = "Sınıflar";
      this.rbSinif.UseVisualStyleBackColor = true;
      this.rbSinif.CheckedChanged += new EventHandler(this.rbSinif_CheckedChanged);
      this.rbDerslik.AutoSize = true;
      this.rbDerslik.Location = new Point(101, 12);
      this.rbDerslik.Name = "rbDerslik";
      this.rbDerslik.Size = new Size(68, 17);
      this.rbDerslik.TabIndex = 1;
      this.rbDerslik.TabStop = true;
      this.rbDerslik.Text = "Derslikler";
      this.rbDerslik.UseVisualStyleBackColor = true;
      this.rbDerslik.CheckedChanged += new EventHandler(this.radioButton2_CheckedChanged);
      this.rbOgretmen.AutoSize = true;
      this.rbOgretmen.Location = new Point(12, 12);
      this.rbOgretmen.Name = "rbOgretmen";
      this.rbOgretmen.Size = new Size(82, 17);
      this.rbOgretmen.TabIndex = 0;
      this.rbOgretmen.TabStop = true;
      this.rbOgretmen.Text = "Öğretmenler";
      this.rbOgretmen.UseVisualStyleBackColor = true;
      this.rbOgretmen.CheckedChanged += new EventHandler(this.rbOgretmen_CheckedChanged);
      this.panel2.Controls.Add((Control) this.lstListe);
      this.panel2.Dock = DockStyle.Fill;
      this.panel2.Location = new Point(0, 40);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(240, 566);
      this.panel2.TabIndex = 2;
      this.lstListe.Dock = DockStyle.Fill;
      this.lstListe.FormattingEnabled = true;
      this.lstListe.Location = new Point(0, 0);
      this.lstListe.Name = "lstListe";
      this.lstListe.SelectionMode = SelectionMode.MultiSimple;
      this.lstListe.Size = new Size(240, 566);
      this.lstListe.TabIndex = 0;
      this.panel3.Controls.Add((Control) this.groupBox1);
      this.panel3.Controls.Add((Control) this.button3);
      this.panel3.Controls.Add((Control) this.button2);
      this.panel3.Controls.Add((Control) this.button1);
      this.panel3.Dock = DockStyle.Right;
      this.panel3.Location = new Point(240, 40);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(317, 566);
      this.panel3.TabIndex = 3;
      this.groupBox1.Controls.Add((Control) this.rbSerbest);
      this.groupBox1.Controls.Add((Control) this.rbMeb);
      this.groupBox1.Controls.Add((Control) this.pnlSerbest);
      this.groupBox1.Controls.Add((Control) this.pnlMeb);
      this.groupBox1.Dock = DockStyle.Bottom;
      this.groupBox1.Location = new Point(0, 75);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(317, 491);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Ayarlar";
      this.pnlSerbest.Controls.Add((Control) this.lblBilgiler);
      this.pnlSerbest.Controls.Add((Control) this.label4);
      this.pnlSerbest.Controls.Add((Control) this.label3);
      this.pnlSerbest.Controls.Add((Control) this.label2);
      this.pnlSerbest.Controls.Add((Control) this.txtAltBilgi);
      this.pnlSerbest.Controls.Add((Control) this.txtBilesenBilgi);
      this.pnlSerbest.Controls.Add((Control) this.txtBaslik);
      this.pnlSerbest.Location = new Point(6, 42);
      this.pnlSerbest.Name = "pnlSerbest";
      this.pnlSerbest.Size = new Size(307, 411);
      this.pnlSerbest.TabIndex = 5;
      this.pnlSerbest.Visible = false;
      this.pnlMeb.Controls.Add((Control) this.label1);
      this.pnlMeb.Controls.Add((Control) this.dtpTeslimTarihi);
      this.pnlMeb.Location = new Point(6, 42);
      this.pnlMeb.Name = "pnlMeb";
      this.pnlMeb.Size = new Size(307, 411);
      this.pnlMeb.TabIndex = 4;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(3, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(163, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Öğretmene Teslim Edilecek Tarih";
      this.dtpTeslimTarihi.Location = new Point(3, 44);
      this.dtpTeslimTarihi.Name = "dtpTeslimTarihi";
      this.dtpTeslimTarihi.Size = new Size(182, 20);
      this.dtpTeslimTarihi.TabIndex = 0;
      this.rbSerbest.AutoSize = true;
      this.rbSerbest.Location = new Point(94, 19);
      this.rbSerbest.Name = "rbSerbest";
      this.rbSerbest.Size = new Size(61, 17);
      this.rbSerbest.TabIndex = 3;
      this.rbSerbest.Text = "Serbest";
      this.rbSerbest.UseVisualStyleBackColor = true;
      this.rbSerbest.CheckedChanged += new EventHandler(this.sitilSecimPanelDegistir);
      this.rbMeb.AutoSize = true;
      this.rbMeb.Checked = true;
      this.rbMeb.Location = new Point(11, 19);
      this.rbMeb.Name = "rbMeb";
      this.rbMeb.Size = new Size(46, 17);
      this.rbMeb.TabIndex = 2;
      this.rbMeb.TabStop = true;
      this.rbMeb.Text = "Meb";
      this.rbMeb.UseVisualStyleBackColor = true;
      this.rbMeb.CheckedChanged += new EventHandler(this.sitilSecimPanelDegistir);
      this.button3.Location = new Point(225, 6);
      this.button3.Name = "button3";
      this.button3.Size = new Size(77, 38);
      this.button3.TabIndex = 2;
      this.button3.Text = "Rapor Al";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.button2.Location = new Point(89, 6);
      this.button2.Name = "button2";
      this.button2.Size = new Size(77, 38);
      this.button2.TabIndex = 1;
      this.button2.Text = "Seçimi Kaldır";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button1.Location = new Point(6, 6);
      this.button1.Name = "button1";
      this.button1.Size = new Size(77, 38);
      this.button1.TabIndex = 0;
      this.button1.Text = "Tümünü Seç";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.txtBaslik.Location = new Point(22, 39);
      this.txtBaslik.Multiline = true;
      this.txtBaslik.Name = "txtBaslik";
      this.txtBaslik.Size = new Size(274, 50);
      this.txtBaslik.TabIndex = 1;
      this.txtBaslik.Text = "{3}";
      this.txtBilesenBilgi.Location = new Point(22, 118);
      this.txtBilesenBilgi.Multiline = true;
      this.txtBilesenBilgi.Name = "txtBilesenBilgi";
      this.txtBilesenBilgi.Size = new Size(274, 50);
      this.txtBilesenBilgi.TabIndex = 3;
      this.txtBilesenBilgi.Text = "{0} : {1} ";
      this.txtAltBilgi.Location = new Point(22, 199);
      this.txtAltBilgi.Multiline = true;
      this.txtAltBilgi.Name = "txtAltBilgi";
      this.txtAltBilgi.Size = new Size(274, 64);
      this.txtAltBilgi.TabIndex = 5;
      this.txtAltBilgi.Text = "Program {2} tarihinde verilmiştir.\r\n";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(3, 23);
      this.label2.Name = "label2";
      this.label2.Size = new Size(152, 13);
      this.label2.TabIndex = 6;
      this.label2.Text = "Üst Başlık (Büyük, Kalın, Ortalı)";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(3, 102);
      this.label3.Name = "label3";
      this.label3.Size = new Size(203, 13);
      this.label3.TabIndex = 7;
      this.label3.Text = "Üst Açıklama (Başlık ile program arasında)";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(3, 183);
      this.label4.Name = "label4";
      this.label4.Size = new Size(147, 13);
      this.label4.TabIndex = 8;
      this.label4.Text = "Alt Açıklama (Program altında)";
      this.lblBilgiler.AutoSize = true;
      this.lblBilgiler.Location = new Point(19, 285);
      this.lblBilgiler.Name = "lblBilgiler";
      this.lblBilgiler.Size = new Size(185, 65);
      this.lblBilgiler.TabIndex = 9;
      this.lblBilgiler.Text = "{0} Bileşen Türü (Öğretmen, Derslik...)\r\n{1} Bileşen Adı (11B, Ahmet ...)\r\n{2} Tarih (23.02.2014)\r\n{3} Kurum Adı\r\n{4} Öğretim Yılı";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(557, 606);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel1);
      this.Name = nameof (ElProgramiFrm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "El Programı Raporlama";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel3.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.pnlSerbest.ResumeLayout(false);
      this.pnlSerbest.PerformLayout();
      this.pnlMeb.ResumeLayout(false);
      this.pnlMeb.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
