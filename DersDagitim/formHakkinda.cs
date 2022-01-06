using DersDagitim.Properties;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class formHakkinda : Form
  {
    private IContainer components;
    private TextBox textBox1;
    private Label label1;
    private Label lblBilgi;
    private Label label3;
    private Label lblVersion;
    private PictureBox pictureBox1;
    private Label label4;
    private Timer timer1;

    public formHakkinda(bool _ilkEkran = false)
    {
      this.InitializeComponent();
      this.timer1.Enabled = _ilkEkran;
    }

    private void label3_Click(object sender, EventArgs e) => Process.Start("http://dagitmatik.blogspot.com");

    private void formHakkinda_Load(object sender, EventArgs e)
    {
      this.lblVersion.Text = string.Format("Version : {0}", (object) araclar.versiyon());
      this.lblBilgi.Text = "Murat AKAR\nBilişim Teknolojileri\nÖğretmeni\n\nIsparta Meryem Albayrak\nMesleki ve Teknik Anadolu Lisesi";
    }

    private void lblVersion_DoubleClick(object sender, EventArgs e) => Clipboard.SetText(araclar.versiyon());

    private void timer1_Tick(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (formHakkinda));
      this.textBox1 = new TextBox();
      this.label1 = new Label();
      this.lblBilgi = new Label();
      this.label3 = new Label();
      this.lblVersion = new Label();
      this.label4 = new Label();
      this.pictureBox1 = new PictureBox();
      this.timer1 = new Timer(this.components);
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.SuspendLayout();
      this.textBox1.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 162);
      this.textBox1.Location = new Point(3, 316);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(513, 40);
      this.textBox1.TabIndex = 0;
      this.textBox1.Text = "murat.akar@outlook.com";
      this.textBox1.TextAlign = HorizontalAlignment.Center;
      this.label1.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 162);
      this.label1.Location = new Point(264, 5);
      this.label1.Name = "label1";
      this.label1.Size = new Size(252, 101);
      this.label1.TabIndex = 1;
      this.label1.Text = componentResourceManager.GetString("label1.Text");
      this.lblBilgi.Font = new Font("Microsoft Sans Serif", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 162);
      this.lblBilgi.Location = new Point(264, 115);
      this.lblBilgi.Name = "lblBilgi";
      this.lblBilgi.Size = new Size(211, 109);
      this.lblBilgi.TabIndex = 2;
      this.lblBilgi.Text = "Murat AKAR           Bilişim Teknolojileri Öğretmeni             Isparta Meryem Alb. TML";
      this.label3.BackColor = SystemColors.ActiveCaptionText;
      this.label3.Cursor = Cursors.Hand;
      this.label3.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 162);
      this.label3.ForeColor = SystemColors.HotTrack;
      this.label3.Location = new Point(3, 268);
      this.label3.Name = "label3";
      this.label3.Size = new Size(513, 37);
      this.label3.TabIndex = 3;
      this.label3.Text = "dagitmatik.blogspot.com";
      this.label3.TextAlign = ContentAlignment.MiddleCenter;
      this.label3.Click += new EventHandler(this.label3_Click);
      this.lblVersion.AutoSize = true;
      this.lblVersion.Location = new Point(264, 225);
      this.lblVersion.Name = "lblVersion";
      this.lblVersion.Size = new Size(35, 13);
      this.lblVersion.TabIndex = 4;
      this.lblVersion.Text = "label4";
      this.lblVersion.DoubleClick += new EventHandler(this.lblVersion_DoubleClick);
      this.label4.AutoSize = true;
      this.label4.Location = new Point(264, 244);
      this.label4.Name = "label4";
      this.label4.Size = new Size(106, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Derleme: 08.08.2015";
      this.pictureBox1.ErrorImage = (Image) null;
      this.pictureBox1.Image = (Image) Resources.untitled;
      this.pictureBox1.InitialImage = (Image) null;
      this.pictureBox1.Location = new Point(-3, -2);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(261, 262);
      this.pictureBox1.TabIndex = 5;
      this.pictureBox1.TabStop = false;
      this.timer1.Interval = 50000;
      this.timer1.Tick += new EventHandler(this.timer1_Tick);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(528, 368);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.lblVersion);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.lblBilgi);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.textBox1);
      this.MaximizeBox = false;
      this.Name = nameof (formHakkinda);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Hakkında";
      this.Load += new EventHandler(this.formHakkinda_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
