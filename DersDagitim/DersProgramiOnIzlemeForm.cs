using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class DersProgramiOnIzlemeForm : Form
  {
    private bilesenTaban bilesen;
    private bool ilkacilis = true;
    private DataTable dtOgretmenler;
    private DataTable dtSiniflar;
    private DataTable dtDerslikler;
    private IContainer components;
    private ComboBox cmbOgretmenler;
    private ComboBox cmbSiniflar;
    private ComboBox cmbDerslikler;
    private Label label1;
    private Label label2;
    private Label label3;
    private PictureBox pbOnizleme;
    private GroupBox groupBox1;
    private Panel panel1;
    private Panel panel2;

    public DersProgramiOnIzlemeForm(bilesenTaban _bilesen = null)
    {
      this.InitializeComponent();
      this.bilesen = _bilesen;
    }

    public DataTable dtOlustur() => new DataTable()
    {
      Columns = {
        {
          "id",
          typeof (ushort)
        },
        {
          "adi",
          typeof (string)
        }
      }
    };

    private void listeyiOlustur()
    {
      this.dtOgretmenler = this.dtOlustur();
      this.dtDerslikler = this.dtOlustur();
      this.dtSiniflar = this.dtOlustur();
      foreach (bilesenOgretmen bilesenOgretmen in tanim.program.ogretmenler)
        this.dtOgretmenler.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi);
      foreach (bilesenSinif bilesenSinif in tanim.program.siniflar)
        this.dtSiniflar.Rows.Add((object) bilesenSinif.id, (object) bilesenSinif.adi);
      foreach (bilesenDerslik bilesenDerslik in tanim.program.derslikler)
        this.dtDerslikler.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi);
      this.cmbOgretmenler.DisplayMember = "adi";
      this.cmbOgretmenler.ValueMember = "id";
      this.cmbOgretmenler.DataSource = (object) this.dtOgretmenler;
      this.cmbDerslikler.DisplayMember = "adi";
      this.cmbDerslikler.ValueMember = "id";
      this.cmbDerslikler.DataSource = (object) this.dtDerslikler;
      this.cmbSiniflar.DisplayMember = "adi";
      this.cmbSiniflar.ValueMember = "id";
      this.cmbSiniflar.DataSource = (object) this.dtSiniflar;
    }

    private void DersProgramiOnIzlemeForm_Load(object sender, EventArgs e)
    {
      this.Height = 60 + ((int) tanim.program.gunlukDersSaatiSayisi + 1) * 70;
      this.Width = 120 * (int) tanim.program.haftalikGunSayisi + 70;
      this.Location = new Point(30, 30);
      this.listeyiOlustur();
      this.cmbSiniflar.SelectedIndex = -1;
      this.cmbOgretmenler.SelectedIndex = -1;
      this.cmbDerslikler.SelectedIndex = -1;
      this.ilkacilis = false;
      if (this.bilesen == null)
        return;
      Bitmap bitmap = araclar.dersProgramiCizelgesi(this.bilesen);
      this.Text = this.bilesen.adi;
      this.pbOnizleme.Image = (Image) bitmap;
      this.pbOnizleme.Width = bitmap.Width;
      this.pbOnizleme.Height = bitmap.Height;
    }

    private void cmbOgretmenler_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ilkacilis)
        return;
      Bitmap bitmap = araclar.dersProgramiCizelgesi((bilesenTaban) tanim.program.ogretmenGetir(Convert.ToUInt16(this.cmbOgretmenler.SelectedValue.ToString())));
      this.Text = "Öğretmen: " + this.cmbOgretmenler.Text;
      this.pbOnizleme.Image = (Image) bitmap;
      this.pbOnizleme.Width = bitmap.Width;
      this.pbOnizleme.Height = bitmap.Height;
      this.cmbSiniflar.Text = "";
      this.cmbDerslikler.Text = "";
    }

    private void cmbSiniflar_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ilkacilis)
        return;
      Bitmap bitmap = araclar.dersProgramiCizelgesi((bilesenTaban) tanim.program.sinifGetir(Convert.ToUInt16(this.cmbSiniflar.SelectedValue.ToString())));
      this.Text = "Sınıf: " + this.cmbSiniflar.Text;
      this.pbOnizleme.Image = (Image) bitmap;
      this.pbOnizleme.Width = bitmap.Width;
      this.pbOnizleme.Height = bitmap.Height;
      this.cmbOgretmenler.Text = "";
      this.cmbDerslikler.Text = "";
    }

    private void cmbDerslikler_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.ilkacilis)
        return;
      Bitmap bitmap = araclar.dersProgramiCizelgesi((bilesenTaban) tanim.program.derslikGetir(Convert.ToUInt16(this.cmbDerslikler.SelectedValue.ToString())));
      this.Text = "Derslik: " + this.cmbDerslikler.Text;
      this.pbOnizleme.Image = (Image) bitmap;
      this.pbOnizleme.Width = bitmap.Width;
      this.pbOnizleme.Height = bitmap.Height;
      this.cmbSiniflar.Text = "";
      this.cmbOgretmenler.Text = "";
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cmbOgretmenler = new ComboBox();
      this.cmbSiniflar = new ComboBox();
      this.cmbDerslikler = new ComboBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.pbOnizleme = new PictureBox();
      this.groupBox1 = new GroupBox();
      this.panel1 = new Panel();
      this.panel2 = new Panel();
      ((ISupportInitialize) this.pbOnizleme).BeginInit();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.cmbOgretmenler.FormattingEnabled = true;
      this.cmbOgretmenler.Location = new Point(6, 19);
      this.cmbOgretmenler.Name = "cmbOgretmenler";
      this.cmbOgretmenler.Size = new Size(200, 21);
      this.cmbOgretmenler.TabIndex = 0;
      this.cmbOgretmenler.Text = "Öğretmenler";
      this.cmbOgretmenler.SelectedIndexChanged += new EventHandler(this.cmbOgretmenler_SelectedIndexChanged);
      this.cmbSiniflar.FormattingEnabled = true;
      this.cmbSiniflar.Location = new Point(214, 19);
      this.cmbSiniflar.Name = "cmbSiniflar";
      this.cmbSiniflar.Size = new Size(200, 21);
      this.cmbSiniflar.TabIndex = 1;
      this.cmbSiniflar.Text = "Sınıflar";
      this.cmbSiniflar.SelectedIndexChanged += new EventHandler(this.cmbSiniflar_SelectedIndexChanged);
      this.cmbDerslikler.FormattingEnabled = true;
      this.cmbDerslikler.Location = new Point(424, 19);
      this.cmbDerslikler.Name = "cmbDerslikler";
      this.cmbDerslikler.Size = new Size(200, 21);
      this.cmbDerslikler.TabIndex = 2;
      this.cmbDerslikler.Text = "Derslikler";
      this.cmbDerslikler.SelectedIndexChanged += new EventHandler(this.cmbDerslikler_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(62, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(64, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Öğretmenler";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(292, 0);
      this.label2.Name = "label2";
      this.label2.Size = new Size(38, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Sınıflar";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(497, 0);
      this.label3.Name = "label3";
      this.label3.Size = new Size(50, 13);
      this.label3.TabIndex = 5;
      this.label3.Text = "Derslikler";
      this.pbOnizleme.Location = new Point(6, 3);
      this.pbOnizleme.Name = "pbOnizleme";
      this.pbOnizleme.Size = new Size(585, 394);
      this.pbOnizleme.TabIndex = 8;
      this.pbOnizleme.TabStop = false;
      this.groupBox1.Location = new Point(653, 717);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Padding = new Padding(0);
      this.groupBox1.Size = new Size(712, 62);
      this.groupBox1.TabIndex = 11;
      this.groupBox1.TabStop = false;
      this.panel1.AutoScroll = true;
      this.panel1.Controls.Add((Control) this.pbOnizleme);
      this.panel1.Dock = DockStyle.Fill;
      this.panel1.Location = new Point(0, 44);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(655, 606);
      this.panel1.TabIndex = 13;
      this.panel2.Controls.Add((Control) this.label1);
      this.panel2.Controls.Add((Control) this.cmbOgretmenler);
      this.panel2.Controls.Add((Control) this.label3);
      this.panel2.Controls.Add((Control) this.label2);
      this.panel2.Controls.Add((Control) this.cmbSiniflar);
      this.panel2.Controls.Add((Control) this.cmbDerslikler);
      this.panel2.Dock = DockStyle.Top;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(655, 44);
      this.panel2.TabIndex = 14;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(655, 650);
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.panel2);
      this.Name = nameof (DersProgramiOnIzlemeForm);
      this.StartPosition = FormStartPosition.Manual;
      this.Text = "Ders Programı Önizleme";
      this.Load += new EventHandler(this.DersProgramiOnIzlemeForm_Load);
      ((ISupportInitialize) this.pbOnizleme).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
