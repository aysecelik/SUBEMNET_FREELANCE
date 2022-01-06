using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class TanimliDersAnaliz : Form
  {
    private bilesenTanimliDers analizDers;
    private IContainer components;
    private PictureBox pbYerlesim;
    private Label label1;
    private Label label2;
    private Label lblOlasilikToplami;

    public TanimliDersAnaliz(bilesenTanimliDers _analizDers)
    {
      this.InitializeComponent();
      this.analizDers = _analizDers;
    }

    private void TanimliDersAnaliz_Load(object sender, EventArgs e)
    {
      if (this.analizDers.aktifYerlesim == null)
      {
        this.Text = this.analizDers.aciklama;
        this.analizDers.yerlesimeHazirla();
        this.pbYerlesim.Image = (Image) araclar.kosulResim(this.analizDers.kosul, true);
        this.lblOlasilikToplami.Text = this.analizDers.olasiliklar.Count.ToString();
      }
      else
      {
        int num = (int) MessageBox.Show("Yerleşmiş ders analizi yapılmaz!!");
        this.Close();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.pbYerlesim = new PictureBox();
      this.label1 = new Label();
      this.label2 = new Label();
      this.lblOlasilikToplami = new Label();
      ((ISupportInitialize) this.pbYerlesim).BeginInit();
      this.SuspendLayout();
      this.pbYerlesim.Location = new Point(130, 12);
      this.pbYerlesim.Name = "pbYerlesim";
      this.pbYerlesim.Size = new Size(200, 80);
      this.pbYerlesim.TabIndex = 0;
      this.pbYerlesim.TabStop = false;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(11, 42);
      this.label1.Name = "label1";
      this.label1.Size = new Size(113, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Yerleşebileceği Alanlar";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 115);
      this.label2.Name = "label2";
      this.label2.Size = new Size(80, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "Olasılık Toplamı";
      this.lblOlasilikToplami.AutoSize = true;
      this.lblOlasilikToplami.Location = new Point((int) sbyte.MaxValue, 115);
      this.lblOlasilikToplami.Name = "lblOlasilikToplami";
      this.lblOlasilikToplami.Size = new Size(13, 13);
      this.lblOlasilikToplami.TabIndex = 3;
      this.lblOlasilikToplami.Text = "0";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(402, 192);
      this.Controls.Add((Control) this.lblOlasilikToplami);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.pbYerlesim);
      this.Name = nameof (TanimliDersAnaliz);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Tanımlı Ders Analiz";
      this.Load += new EventHandler(this.TanimliDersAnaliz_Load);
      ((ISupportInitialize) this.pbYerlesim).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
