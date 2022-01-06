using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class KontrolForm : Form
  {
    private IContainer components;
    private ListBox lstHatalar;
    private Label label2;
    private Button btnAkilliDagitim;

    public KontrolForm() => this.InitializeComponent();

    private void KontrolForm_Load(object sender, EventArgs e)
    {
      bool flag = false;
      ArrayList arrayList = new ArrayList();
      arrayList.AddRange((ICollection) tanim.program.ogretmenler);
      arrayList.AddRange((ICollection) tanim.program.derslikler);
      arrayList.AddRange((ICollection) tanim.program.siniflar);
      this.lstHatalar.Items.Add((object) "Toplam saat ile uygun saat kontrol ediliyor!");
      this.lstHatalar.Refresh();
      for (int index = 0; index < arrayList.Count; ++index)
      {
        bilesenTaban bilesen = arrayList[index] as bilesenTaban;
        int num = (int) tanim.program.bilesenDersSayisi(bilesen);
        if (tanim.program.uygunDersSaatiSay(bilesen) < num)
        {
          this.lstHatalar.Items.Add((object) (bilesen.adi + " Uygun olduğu saat yetersiz"));
          flag = true;
        }
      }
      this.lstHatalar.Items.Add((object) "Tanimli derslerin yerleşim olasılıkları kontrol ediliyor!");
      this.lstHatalar.Refresh();
      for (int index = 0; index < tanim.program.tanimliDersler.Count; ++index)
      {
        bilesenTanimliDers bilesenTanimliDers = tanim.program.tanimliDersler[index];
        bilesenTanimliDers.yerlesimeHazirla();
        if (bilesenTanimliDers.olasiliklar.Count == 0)
        {
          flag = true;
          this.lstHatalar.Items.Add((object) (bilesenTanimliDers.aciklama + " Tanımlı dersin yerleştirilmesi imkansız"));
        }
      }
      foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        bilesenTanimliDers.iliskileriOlustur();
      if (flag)
      {
        this.lstHatalar.Items.Add((object) "Hatalar var! Dağıtım yapılamaz!");
      }
      else
      {
        this.lstHatalar.Items.Add((object) "Hata Yok!! Ders Dağıtımı yapılabilir");
        this.btnAkilliDagitim.Enabled = true;
      }
    }

    private void btnAkilliDagitim_Click(object sender, EventArgs e)
    {
      GC.Collect();
      GC.WaitForPendingFinalizers();
      this.Hide();
      AkilliDagitimForm akilliDagitimForm = new AkilliDagitimForm();
      akilliDagitimForm.Owner = this.Owner;
      int num = (int) akilliDagitimForm.ShowDialog();
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
      this.lstHatalar = new ListBox();
      this.label2 = new Label();
      this.btnAkilliDagitim = new Button();
      this.SuspendLayout();
      this.lstHatalar.FormattingEnabled = true;
      this.lstHatalar.Location = new Point(15, 26);
      this.lstHatalar.Name = "lstHatalar";
      this.lstHatalar.Size = new Size(429, 303);
      this.lstHatalar.TabIndex = 0;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(12, 9);
      this.label2.Name = "label2";
      this.label2.Size = new Size(41, 13);
      this.label2.TabIndex = 3;
      this.label2.Text = "Hatalar";
      this.btnAkilliDagitim.Enabled = false;
      this.btnAkilliDagitim.Location = new Point(134, 335);
      this.btnAkilliDagitim.Name = "btnAkilliDagitim";
      this.btnAkilliDagitim.Size = new Size(196, 44);
      this.btnAkilliDagitim.TabIndex = 5;
      this.btnAkilliDagitim.Text = "Ders Dağıtımını Başlat";
      this.btnAkilliDagitim.UseVisualStyleBackColor = true;
      this.btnAkilliDagitim.Click += new EventHandler(this.btnAkilliDagitim_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(453, 385);
      this.Controls.Add((Control) this.btnAkilliDagitim);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.lstHatalar);
      this.Name = nameof (KontrolForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Ders Programı Kontrol";
      this.Load += new EventHandler(this.KontrolForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
