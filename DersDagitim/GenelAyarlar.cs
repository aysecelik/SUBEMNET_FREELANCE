using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class GenelAyarlar : Form
  {
    private IContainer components;
    private GroupBox groupBox1;
    private TextBox txtOkulAdi;
    private Label label2;
    private Label label1;
    private TextBox txtOkulMudurYrd;
    private Label label3;
    private TextBox txtOkulMuduru;
    private Label label4;
    private GroupBox groupBox2;
    private Label label6;
    private Label label5;
    private ComboBox cmbGunlukDersSay;
    private ComboBox cmbHaftalikGunSay;
    private Button button4;
    private Button button5;
    private Button button6;
    private ListBox lstGunler;
    private GroupBox groupBox3;
    private TextBox txtGunAdi;
    private Button button9;
    private Button button8;
    private Button button7;
    private GroupBox groupBox4;
    private Button button10;
    private Button button11;
    private Button button12;
    private ListBox lstDersSaatleri;
    private MaskedTextBox txtDersSaati;
    private Button button2;
    private Button button1;
    private Button button14;
    private Button button13;
    private MaskedTextBox txtOgretimYili;
    private Button button15;
    private CheckBox chkBasYardimci;

    public GenelAyarlar() => this.InitializeComponent();

    private void GenelAyarlar_Load(object sender, EventArgs e)
    {
      this.txtOkulAdi.Text = tanim.program.okulAdi;
      this.txtOkulMuduru.Text = tanim.program.okulMuduru;
      this.txtOkulMudurYrd.Text = tanim.program.okulMudurYrd;
      this.chkBasYardimci.Checked = tanim.program.mudurYrdBas;
      this.txtOgretimYili.Text = tanim.program.ogretimYili;
      this.cmbGunlukDersSay.SelectedIndex = this.cmbGunlukDersSay.Items.IndexOf((object) tanim.program.gunlukDersSaatiSayisi.ToString());
      this.cmbHaftalikGunSay.SelectedIndex = this.cmbHaftalikGunSay.Items.IndexOf((object) tanim.program.haftalikGunSayisi.ToString());
      for (int index = 0; index < tanim.program.gunler.Length; ++index)
        this.lstGunler.Items.Add((object) tanim.program.gunler[index]);
      for (int index = 0; index < tanim.program.derssaatleri.Length; ++index)
        this.lstDersSaatleri.Items.Add((object) tanim.program.derssaatleri[index]);
    }

    private void lstGunler_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lstGunler.SelectedIndex == -1)
        return;
      this.txtGunAdi.Text = this.lstGunler.SelectedItem.ToString();
    }

    private void button7_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.cmbHaftalikGunSay.SelectedItem) > this.lstGunler.Items.Count)
      {
        if (this.txtGunAdi.Text.Length > 0)
        {
          this.lstGunler.Items.Add((object) this.txtGunAdi.Text);
        }
        else
        {
          int num1 = (int) MessageBox.Show("Boş girilemez!");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("Haftalık gün sayısı doldu!");
      }
    }

    private void cmbGunlukDersSay_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void cmbHaftalikGunSay_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbHaftalikGunSay.SelectedIndex == -1)
        return;
      int num;
      for (int index1 = Convert.ToInt32(this.cmbHaftalikGunSay.SelectedItem); index1 < this.lstGunler.Items.Count; index1 = num + 1)
      {
        ListBox.ObjectCollection items = this.lstGunler.Items;
        int index2 = index1;
        num = index2 - 1;
        items.RemoveAt(index2);
      }
    }

    private void button8_Click(object sender, EventArgs e)
    {
      if (this.lstGunler.SelectedIndex == -1)
        return;
      this.lstGunler.Items.RemoveAt(this.lstGunler.SelectedIndex);
    }

    private void button9_Click(object sender, EventArgs e)
    {
      if (this.lstGunler.SelectedIndex == -1)
        return;
      this.lstGunler.Items[this.lstGunler.SelectedIndex] = (object) this.txtGunAdi.Text;
    }

    private void groupBox3_Enter(object sender, EventArgs e)
    {
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.lstDersSaatleri.SelectedIndex == -1)
        return;
      this.txtDersSaati.Text = this.lstDersSaatleri.SelectedItem.ToString();
    }

    private void button11_Click(object sender, EventArgs e)
    {
    }

    private void button12_Click(object sender, EventArgs e)
    {
      if (Convert.ToInt32(this.cmbGunlukDersSay.SelectedItem) > this.lstDersSaatleri.Items.Count)
      {
        if (this.txtDersSaati.Text != "  :  -  :")
        {
          this.lstDersSaatleri.Items.Add((object) this.txtDersSaati.Text);
        }
        else
        {
          int num1 = (int) MessageBox.Show("Boş girilemez");
        }
      }
      else
      {
        int num2 = (int) MessageBox.Show("Ders Saati Sayısı Doldu!");
      }
    }

    private void button11_Click_1(object sender, EventArgs e)
    {
      if (this.lstDersSaatleri.SelectedIndex == -1)
        return;
      this.lstDersSaatleri.Items.RemoveAt(this.lstDersSaatleri.SelectedIndex);
    }

    private void button10_Click(object sender, EventArgs e)
    {
      if (this.lstDersSaatleri.SelectedIndex == -1)
        return;
      this.lstDersSaatleri.Items[this.lstDersSaatleri.SelectedIndex] = (object) this.txtDersSaati.Text;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.lstGunler.SelectedIndex <= 0)
        return;
      int selectedIndex = this.lstGunler.SelectedIndex;
      string str = this.lstGunler.Items[selectedIndex - 1].ToString();
      this.lstGunler.Items[selectedIndex - 1] = this.lstGunler.Items[selectedIndex];
      this.lstGunler.Items[selectedIndex] = (object) str;
      this.lstGunler.SelectedIndex = selectedIndex - 1;
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.lstGunler.SelectedIndex >= this.lstGunler.Items.Count - 1 || this.lstGunler.SelectedIndex <= -1)
        return;
      int selectedIndex = this.lstGunler.SelectedIndex;
      string str = this.lstGunler.Items[selectedIndex + 1].ToString();
      this.lstGunler.Items[selectedIndex + 1] = this.lstGunler.Items[selectedIndex];
      this.lstGunler.Items[selectedIndex] = (object) str;
      this.lstGunler.SelectedIndex = selectedIndex + 1;
    }

    private void button13_Click(object sender, EventArgs e)
    {
      if (this.lstDersSaatleri.SelectedIndex <= 0)
        return;
      int selectedIndex = this.lstDersSaatleri.SelectedIndex;
      string str = this.lstDersSaatleri.Items[selectedIndex - 1].ToString();
      this.lstDersSaatleri.Items[selectedIndex - 1] = this.lstDersSaatleri.Items[selectedIndex];
      this.lstDersSaatleri.Items[selectedIndex] = (object) str;
      this.lstDersSaatleri.SelectedIndex = selectedIndex - 1;
    }

    private void button14_Click(object sender, EventArgs e)
    {
      if (this.lstDersSaatleri.SelectedIndex >= this.lstDersSaatleri.Items.Count - 1 || this.lstDersSaatleri.SelectedIndex <= -1)
        return;
      int selectedIndex = this.lstDersSaatleri.SelectedIndex;
      string str = this.lstDersSaatleri.Items[selectedIndex + 1].ToString();
      this.lstDersSaatleri.Items[selectedIndex + 1] = this.lstDersSaatleri.Items[selectedIndex];
      this.lstDersSaatleri.Items[selectedIndex] = (object) str;
      this.lstDersSaatleri.SelectedIndex = selectedIndex + 1;
    }

    private void button6_Click(object sender, EventArgs e)
    {
      bool flag = true;
      if ((int) Convert.ToByte(this.cmbGunlukDersSay.SelectedItem) != (int) tanim.program.gunlukDersSaatiSayisi || (int) Convert.ToByte(this.cmbHaftalikGunSay.SelectedItem) != (int) tanim.program.haftalikGunSayisi)
      {
        DialogResult dialogResult = MessageBox.Show("Yapılan değişiklikler uygulansın mı?", "Uyarı", MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes)
        {
          try
          {
            this.uygula();
            flag = true;
          }
          catch
          {
            flag = false;
          }
        }
        if (dialogResult == DialogResult.No)
          flag = false;
      }
      if (!flag)
        return;
      int num = (int) new KosulForm(ref tanim.program.kosullar, "Okul Genel Koşulları").ShowDialog();
    }

    private void uygula()
    {
      bool flag = true;
      string text = "";
      if (Convert.ToInt32(this.cmbHaftalikGunSay.SelectedItem) != this.lstGunler.Items.Count)
      {
        text += "Haftalık gün sayısı ile günler uyuşmuyor!\n";
        flag = false;
      }
      if (Convert.ToInt32(this.cmbGunlukDersSay.SelectedItem) != this.lstDersSaatleri.Items.Count)
      {
        text += "Günlük ders saati sayısı ile ders saatleri uyuşmuyor!";
        flag = false;
      }
      if (flag)
      {
        tanim.program.okulAdi = this.txtOkulAdi.Text;
        tanim.program.okulMuduru = this.txtOkulMuduru.Text;
        tanim.program.okulMudurYrd = this.txtOkulMudurYrd.Text;
        tanim.program.mudurYrdBas = this.chkBasYardimci.Checked;
        tanim.program.ogretimYili = this.txtOgretimYili.Text;
        tanim.program.haftalikGunSayisi = Convert.ToByte(this.cmbHaftalikGunSay.SelectedItem);
        tanim.program.gunlukDersSaatiSayisi = Convert.ToByte(this.cmbGunlukDersSay.SelectedItem);
        tanim.program.gunler = new string[(int) tanim.program.haftalikGunSayisi];
        tanim.program.derssaatleri = new string[(int) tanim.program.gunlukDersSaatiSayisi];
        for (int index = 0; index < (int) tanim.program.haftalikGunSayisi; ++index)
          tanim.program.gunler[index] = this.lstGunler.Items[index].ToString();
        for (int index = 0; index < (int) tanim.program.gunlukDersSaatiSayisi; ++index)
          tanim.program.derssaatleri[index] = this.lstDersSaatleri.Items[index].ToString();
        bool[,] hedef1 = new bool[(int) tanim.program.haftalikGunSayisi, (int) tanim.program.gunlukDersSaatiSayisi];
        for (int index1 = 0; index1 < hedef1.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < hedef1.GetLength(1); ++index2)
            hedef1[index1, index2] = true;
        }
        araclar.diziKopyala(ref hedef1, tanim.program.kosullar);
        tanim.program.kosullar = hedef1;
        ArrayList arrayList = new ArrayList();
        arrayList.AddRange((ICollection) tanim.program.dersler);
        arrayList.AddRange((ICollection) tanim.program.ogretmenler);
        arrayList.AddRange((ICollection) tanim.program.derslikler);
        arrayList.AddRange((ICollection) tanim.program.siniflar);
        for (int index = 0; index < arrayList.Count; ++index)
        {
          bilesenTaban bilesenTaban = arrayList[index] as bilesenTaban;
          bool[,] hedef2 = araclar.diziOlustur();
          araclar.diziKopyala(ref hedef2, bilesenTaban.kosul);
          bilesenTaban.kosul = hedef2;
        }
      }
      else
      {
        int num = (int) MessageBox.Show(text);
        throw new Exception();
      }
    }

    private void button15_Click(object sender, EventArgs e)
    {
      try
      {
        this.uygula();
      }
      catch
      {
      }
    }

    private void button4_Click(object sender, EventArgs e) => this.Close();

    private void button5_Click(object sender, EventArgs e)
    {
      try
      {
        this.uygula();
        this.Close();
      }
      catch
      {
      }
    }

    private void cmbGunlukDersSay_SelectedIndexChanged_1(object sender, EventArgs e)
    {
      if (this.cmbGunlukDersSay.SelectedIndex == -1)
        return;
      int num;
      for (int index1 = Convert.ToInt32(this.cmbGunlukDersSay.SelectedItem); index1 < this.lstDersSaatleri.Items.Count; index1 = num + 1)
      {
        ListBox.ObjectCollection items = this.lstDersSaatleri.Items;
        int index2 = index1;
        num = index2 - 1;
        items.RemoveAt(index2);
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.groupBox1 = new GroupBox();
      this.chkBasYardimci = new CheckBox();
      this.txtOkulMudurYrd = new TextBox();
      this.label3 = new Label();
      this.txtOkulMuduru = new TextBox();
      this.txtOkulAdi = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.label4 = new Label();
      this.groupBox2 = new GroupBox();
      this.txtOgretimYili = new MaskedTextBox();
      this.button6 = new Button();
      this.label6 = new Label();
      this.label5 = new Label();
      this.cmbGunlukDersSay = new ComboBox();
      this.cmbHaftalikGunSay = new ComboBox();
      this.button4 = new Button();
      this.button5 = new Button();
      this.lstGunler = new ListBox();
      this.groupBox3 = new GroupBox();
      this.txtGunAdi = new TextBox();
      this.button9 = new Button();
      this.button8 = new Button();
      this.button2 = new Button();
      this.button7 = new Button();
      this.button1 = new Button();
      this.groupBox4 = new GroupBox();
      this.txtDersSaati = new MaskedTextBox();
      this.button10 = new Button();
      this.button11 = new Button();
      this.button14 = new Button();
      this.button12 = new Button();
      this.lstDersSaatleri = new ListBox();
      this.button13 = new Button();
      this.button15 = new Button();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      this.groupBox1.Controls.Add((Control) this.chkBasYardimci);
      this.groupBox1.Controls.Add((Control) this.txtOkulMudurYrd);
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.txtOkulMuduru);
      this.groupBox1.Controls.Add((Control) this.txtOkulAdi);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Controls.Add((Control) this.label1);
      this.groupBox1.Location = new Point(12, 12);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(424, 117);
      this.groupBox1.TabIndex = 0;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Okul Bilgileri";
      this.chkBasYardimci.AutoSize = true;
      this.chkBasYardimci.Location = new Point(94, 82);
      this.chkBasYardimci.Name = "chkBasYardimci";
      this.chkBasYardimci.Size = new Size(44, 17);
      this.chkBasYardimci.TabIndex = 6;
      this.chkBasYardimci.Text = "Baş";
      this.chkBasYardimci.UseVisualStyleBackColor = true;
      this.txtOkulMudurYrd.Location = new Point(139, 80);
      this.txtOkulMudurYrd.Name = "txtOkulMudurYrd";
      this.txtOkulMudurYrd.Size = new Size(184, 20);
      this.txtOkulMudurYrd.TabIndex = 5;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(6, 83);
      this.label3.Name = "label3";
      this.label3.Size = new Size(87, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "Müdür Yardımcısı";
      this.txtOkulMuduru.Location = new Point(139, 54);
      this.txtOkulMuduru.Name = "txtOkulMuduru";
      this.txtOkulMuduru.Size = new Size(184, 20);
      this.txtOkulMuduru.TabIndex = 3;
      this.txtOkulAdi.Location = new Point(139, 28);
      this.txtOkulAdi.Name = "txtOkulAdi";
      this.txtOkulAdi.Size = new Size(277, 20);
      this.txtOkulAdi.TabIndex = 2;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(6, 57);
      this.label2.Name = "label2";
      this.label2.Size = new Size(68, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Okul Müdürü";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(6, 31);
      this.label1.Name = "label1";
      this.label1.Size = new Size(47, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "Okul Adı";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(6, 25);
      this.label4.Name = "label4";
      this.label4.Size = new Size(59, 13);
      this.label4.TabIndex = 7;
      this.label4.Text = "Öğretim Yılı";
      this.groupBox2.Controls.Add((Control) this.txtOgretimYili);
      this.groupBox2.Controls.Add((Control) this.button6);
      this.groupBox2.Controls.Add((Control) this.label6);
      this.groupBox2.Controls.Add((Control) this.label5);
      this.groupBox2.Controls.Add((Control) this.cmbGunlukDersSay);
      this.groupBox2.Controls.Add((Control) this.cmbHaftalikGunSay);
      this.groupBox2.Controls.Add((Control) this.label4);
      this.groupBox2.Location = new Point(12, 135);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(195, 143);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Ders Programı Bilgileri";
      this.txtOgretimYili.Location = new Point(105, 25);
      this.txtOgretimYili.Mask = "0000-0000";
      this.txtOgretimYili.Name = "txtOgretimYili";
      this.txtOgretimYili.Size = new Size(63, 20);
      this.txtOgretimYili.TabIndex = 6;
      this.button6.FlatStyle = FlatStyle.Flat;
      this.button6.Location = new Point(6, 102);
      this.button6.Name = "button6";
      this.button6.Size = new Size(162, 23);
      this.button6.TabIndex = 13;
      this.button6.Text = "Koşullar";
      this.button6.UseVisualStyleBackColor = true;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(6, 78);
      this.label6.Name = "label6";
      this.label6.Size = new Size(96, 13);
      this.label6.TabIndex = 11;
      this.label6.Text = "Günlük Ders Sayısı";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(6, 51);
      this.label5.Name = "label5";
      this.label5.Size = new Size(96, 13);
      this.label5.TabIndex = 10;
      this.label5.Text = "Haftalık Gün Sayısı";
      this.cmbGunlukDersSay.FormattingEnabled = true;
      this.cmbGunlukDersSay.Items.AddRange(new object[20]
      {
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7",
        (object) "8",
        (object) "9",
        (object) "10",
        (object) "11",
        (object) "12",
        (object) "13",
        (object) "14",
        (object) "15",
        (object) "16",
        (object) "17",
        (object) "18",
        (object) "19",
        (object) "20"
      });
      this.cmbGunlukDersSay.Location = new Point(105, 75);
      this.cmbGunlukDersSay.Name = "cmbGunlukDersSay";
      this.cmbGunlukDersSay.Size = new Size(63, 21);
      this.cmbGunlukDersSay.TabIndex = 9;
      this.cmbGunlukDersSay.SelectedIndexChanged += new EventHandler(this.cmbGunlukDersSay_SelectedIndexChanged_1);
      this.cmbHaftalikGunSay.FormattingEnabled = true;
      this.cmbHaftalikGunSay.Items.AddRange(new object[7]
      {
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7"
      });
      this.cmbHaftalikGunSay.Location = new Point(105, 48);
      this.cmbHaftalikGunSay.Name = "cmbHaftalikGunSay";
      this.cmbHaftalikGunSay.Size = new Size(63, 21);
      this.cmbHaftalikGunSay.TabIndex = 8;
      this.cmbHaftalikGunSay.SelectedIndexChanged += new EventHandler(this.cmbHaftalikGunSay_SelectedIndexChanged);
      this.button4.Location = new Point(500, 339);
      this.button4.Name = "button4";
      this.button4.Size = new Size(75, 23);
      this.button4.TabIndex = 2;
      this.button4.Text = "İptal";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.button5.Location = new Point(338, 339);
      this.button5.Name = "button5";
      this.button5.Size = new Size(75, 23);
      this.button5.TabIndex = 3;
      this.button5.Text = "Tamam";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.lstGunler.FormattingEnabled = true;
      this.lstGunler.Location = new Point(6, 19);
      this.lstGunler.Name = "lstGunler";
      this.lstGunler.Size = new Size(88, 108);
      this.lstGunler.TabIndex = 4;
      this.lstGunler.SelectedIndexChanged += new EventHandler(this.lstGunler_SelectedIndexChanged);
      this.groupBox3.Controls.Add((Control) this.txtGunAdi);
      this.groupBox3.Controls.Add((Control) this.button9);
      this.groupBox3.Controls.Add((Control) this.button8);
      this.groupBox3.Controls.Add((Control) this.button2);
      this.groupBox3.Controls.Add((Control) this.button7);
      this.groupBox3.Controls.Add((Control) this.lstGunler);
      this.groupBox3.Controls.Add((Control) this.button1);
      this.groupBox3.Location = new Point(213, 138);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(178, 140);
      this.groupBox3.TabIndex = 5;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Günleri Düzenle";
      this.groupBox3.Enter += new EventHandler(this.groupBox3_Enter);
      this.txtGunAdi.Location = new Point(100, 19);
      this.txtGunAdi.Name = "txtGunAdi";
      this.txtGunAdi.Size = new Size(69, 20);
      this.txtGunAdi.TabIndex = 7;
      this.button9.FlatStyle = FlatStyle.Flat;
      this.button9.Location = new Point(119, 101);
      this.button9.Name = "button9";
      this.button9.Size = new Size(50, 23);
      this.button9.TabIndex = 6;
      this.button9.Text = "Düzelt";
      this.button9.UseVisualStyleBackColor = true;
      this.button9.Click += new EventHandler(this.button9_Click);
      this.button8.FlatStyle = FlatStyle.Flat;
      this.button8.Location = new Point(119, 72);
      this.button8.Name = "button8";
      this.button8.Size = new Size(50, 23);
      this.button8.TabIndex = 6;
      this.button8.Text = "Sil";
      this.button8.UseVisualStyleBackColor = true;
      this.button8.Click += new EventHandler(this.button8_Click);
      this.button2.FlatStyle = FlatStyle.Flat;
      this.button2.Location = new Point(98, 87);
      this.button2.Name = "button2";
      this.button2.Size = new Size(18, 40);
      this.button2.TabIndex = 5;
      this.button2.Text = "A";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button7.FlatStyle = FlatStyle.Flat;
      this.button7.Location = new Point(119, 45);
      this.button7.Name = "button7";
      this.button7.Size = new Size(50, 23);
      this.button7.TabIndex = 5;
      this.button7.Text = "Ekle";
      this.button7.UseVisualStyleBackColor = true;
      this.button7.Click += new EventHandler(this.button7_Click);
      this.button1.FlatStyle = FlatStyle.Flat;
      this.button1.Location = new Point(98, 45);
      this.button1.Name = "button1";
      this.button1.Size = new Size(18, 40);
      this.button1.TabIndex = 5;
      this.button1.Text = "Y";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.groupBox4.Controls.Add((Control) this.txtDersSaati);
      this.groupBox4.Controls.Add((Control) this.button10);
      this.groupBox4.Controls.Add((Control) this.button11);
      this.groupBox4.Controls.Add((Control) this.button14);
      this.groupBox4.Controls.Add((Control) this.button12);
      this.groupBox4.Controls.Add((Control) this.lstDersSaatleri);
      this.groupBox4.Controls.Add((Control) this.button13);
      this.groupBox4.Location = new Point(397, 138);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(178, 140);
      this.groupBox4.TabIndex = 5;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Ders Saatlerini Düzenle";
      this.groupBox4.Enter += new EventHandler(this.groupBox3_Enter);
      this.txtDersSaati.Location = new Point(100, 19);
      this.txtDersSaati.Mask = "00:00-00:00";
      this.txtDersSaati.Name = "txtDersSaati";
      this.txtDersSaati.Size = new Size(69, 20);
      this.txtDersSaati.TabIndex = 6;
      this.button10.FlatStyle = FlatStyle.Flat;
      this.button10.Location = new Point(119, 104);
      this.button10.Name = "button10";
      this.button10.Size = new Size(50, 23);
      this.button10.TabIndex = 6;
      this.button10.Text = "Düzelt";
      this.button10.UseVisualStyleBackColor = true;
      this.button10.Click += new EventHandler(this.button10_Click);
      this.button11.FlatStyle = FlatStyle.Flat;
      this.button11.Location = new Point(119, 75);
      this.button11.Name = "button11";
      this.button11.Size = new Size(50, 23);
      this.button11.TabIndex = 6;
      this.button11.Text = "Sil";
      this.button11.UseVisualStyleBackColor = true;
      this.button11.Click += new EventHandler(this.button11_Click_1);
      this.button14.FlatStyle = FlatStyle.Flat;
      this.button14.Location = new Point(98, 87);
      this.button14.Name = "button14";
      this.button14.Size = new Size(18, 40);
      this.button14.TabIndex = 5;
      this.button14.Text = "A";
      this.button14.UseVisualStyleBackColor = true;
      this.button14.Click += new EventHandler(this.button14_Click);
      this.button12.FlatStyle = FlatStyle.Flat;
      this.button12.Location = new Point(119, 48);
      this.button12.Name = "button12";
      this.button12.Size = new Size(50, 23);
      this.button12.TabIndex = 5;
      this.button12.Text = "Ekle";
      this.button12.UseVisualStyleBackColor = true;
      this.button12.Click += new EventHandler(this.button12_Click);
      this.lstDersSaatleri.FormattingEnabled = true;
      this.lstDersSaatleri.Location = new Point(6, 19);
      this.lstDersSaatleri.Name = "lstDersSaatleri";
      this.lstDersSaatleri.Size = new Size(88, 108);
      this.lstDersSaatleri.TabIndex = 4;
      this.lstDersSaatleri.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
      this.button13.FlatStyle = FlatStyle.Flat;
      this.button13.Location = new Point(98, 45);
      this.button13.Name = "button13";
      this.button13.Size = new Size(18, 40);
      this.button13.TabIndex = 5;
      this.button13.Text = "Y";
      this.button13.UseVisualStyleBackColor = true;
      this.button13.Click += new EventHandler(this.button13_Click);
      this.button15.Location = new Point(419, 339);
      this.button15.Name = "button15";
      this.button15.Size = new Size(75, 23);
      this.button15.TabIndex = 6;
      this.button15.Text = "Uygula";
      this.button15.UseVisualStyleBackColor = true;
      this.button15.Click += new EventHandler(this.button15_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(583, 374);
      this.Controls.Add((Control) this.button15);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.button5);
      this.Controls.Add((Control) this.button4);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = nameof (GenelAyarlar);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Genel Bilgiler";
      this.Load += new EventHandler(this.GenelAyarlar_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
