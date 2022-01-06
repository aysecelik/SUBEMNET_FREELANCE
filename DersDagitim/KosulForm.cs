using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class KosulForm : Form
  {
    private bool[,] kosulCikis;
    private bool[,] kosullar;
    private IContainer components;
    private Button button1;
    private Button button2;
    private Panel pnlUygun;
    private Label label1;
    private Label label2;
    private Panel pnlUygunDegil;

    public KosulForm(ref bool[,] kosulGiris, string baslik)
    {
      this.InitializeComponent();
      this.kosulCikis = kosulGiris;
      this.kosullar = araclar.diziKopyala(kosulGiris);
      this.Text = baslik;
    }

    private void KosulForm_Load(object sender, EventArgs e)
    {
      this.pnlUygun.BackColor = Color.Green;
      this.pnlUygunDegil.BackColor = Color.Red;
      kosulPanel kosulPanel = new kosulPanel(ref this.kosullar);
      kosulPanel.Location = new Point(10, 10);
      this.Controls.Add((Control) kosulPanel);
    }

    private void button4_Click(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
      araclar.diziKopyala(ref this.kosulCikis, this.kosullar);
      this.Close();
    }

    private void button2_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.button1 = new Button();
      this.button2 = new Button();
      this.pnlUygun = new Panel();
      this.label1 = new Label();
      this.label2 = new Label();
      this.pnlUygunDegil = new Panel();
      this.SuspendLayout();
      this.button1.Location = new Point(428, 377);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 0;
      this.button1.Text = "Tamam";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(509, 377);
      this.button2.Name = "button2";
      this.button2.Size = new Size(75, 23);
      this.button2.TabIndex = 1;
      this.button2.Text = "İptal";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.pnlUygun.BackColor = Color.Green;
      this.pnlUygun.Location = new Point(9, 368);
      this.pnlUygun.Name = "pnlUygun";
      this.pnlUygun.Size = new Size(42, 32);
      this.pnlUygun.TabIndex = 2;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(57, 377);
      this.label1.Name = "label1";
      this.label1.Size = new Size(38, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "Uygun";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(149, 377);
      this.label2.Name = "label2";
      this.label2.Size = new Size(65, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "Uygun Değil";
      this.pnlUygunDegil.BackColor = Color.Green;
      this.pnlUygunDegil.Location = new Point(101, 368);
      this.pnlUygunDegil.Name = "pnlUygunDegil";
      this.pnlUygunDegil.Size = new Size(42, 32);
      this.pnlUygunDegil.TabIndex = 4;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(596, 407);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.pnlUygunDegil);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.pnlUygun);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Name = nameof (KosulForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = nameof (KosulForm);
      this.Load += new EventHandler(this.KosulForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
