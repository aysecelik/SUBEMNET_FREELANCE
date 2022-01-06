using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class DersTanimlamaForm : Form
  {
    private DataTable dtDersler;
    private DataTable dtOgretmenler;
    private DataTable dtDerslikler;
    private DataTable dtSiniflar;
    private DataTable dtGruplar;
    private bilesenTanimliDers duzeltilecekDers;
    private DataTable dtEOgretmenler;
    private DataTable dtEDerslikler;
    private DataTable dtESinifGruplar;
    private IContainer components;
    private ComboBox cmbDersler;
    private Label label1;
    private Panel panel1;
    private Panel panel2;
    private ListBox lstOgretmenler;
    private Label label2;
    private ComboBox cmbOgretmenler;
    private Panel panel3;
    private ListBox lstDerslikler;
    private Label label3;
    private ComboBox cmbDerslikler;
    private Panel panel4;
    private Label label6;
    private Label label5;
    private ComboBox cmbGruplar;
    private ListBox lstSinifGrup;
    private Label label4;
    private ComboBox cmbSiniflar;
    private Panel panel5;
    private TextBox txtYerlesimSekli;
    private Label label8;
    private TextBox txtToplamDersSaati;
    private Label label7;
    private Button btnTanimliDersEkle;
    private Button btnYardim;
    private Button btnOgretmenSil;
    private Button btnOgretmenEkle;
    private Button btnDerslikSil;
    private Button btnDerslikEkle;
    private Button btnSinifGrupSil;
    private Button btnSinifGrupEkle;
    private Button btnEkleDevam;

    private DataTable dataTableUret() => new DataTable()
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

    public DersTanimlamaForm()
    {
      this.InitializeComponent();
      this.bilgileriDoldur();
    }

    public DersTanimlamaForm(bilesenTaban bilesentaban)
    {
      this.InitializeComponent();
      this.bilgileriDoldur();
      if (bilesentaban is bilesenDers)
      {
        ushort uint16 = Convert.ToUInt16((bilesentaban as bilesenDers).id);
        for (int index = 0; index < this.dtDersler.Rows.Count; ++index)
        {
          if (this.dtDersler.Rows[index]["id"].ToString() == uint16.ToString())
            this.cmbDersler.SelectedIndex = index;
        }
      }
      if (bilesentaban is bilesenOgretmen)
      {
        bilesenOgretmen bilesenOgretmen = bilesentaban as bilesenOgretmen;
        this.dtEOgretmenler.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi);
      }
      if (bilesentaban is bilesenSinif)
      {
        bilesenSinif bilesenSinif = bilesentaban as bilesenSinif;
        this.dtESinifGruplar.Rows.Add((object) bilesenSinif.id, (object) (bilesenSinif.adi + " " + (bilesenSinif.gruplar[0] as bilesenGrup).adi), (object) 0);
      }
      if (!(bilesentaban is bilesenDerslik))
        return;
      bilesenDerslik bilesenDerslik = bilesentaban as bilesenDerslik;
      this.dtEDerslikler.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi);
    }

    public DersTanimlamaForm(ushort _id)
    {
      this.InitializeComponent();
      this.bilgileriDoldur();
      this.duzeltilecekDers = tanim.program.tanimliDersGetir(_id);
      this.btnTanimliDersEkle.Text = "Düzelt";
      this.btnEkleDevam.Visible = false;
      for (int index = 0; index < this.dtDersler.Rows.Count; ++index)
      {
        if (this.dtDersler.Rows[index]["id"].ToString() == this.duzeltilecekDers.ders.id.ToString())
          this.cmbDersler.SelectedIndex = index;
      }
      foreach (bilesenDerslik bilesenDerslik in this.duzeltilecekDers.derslikler)
        this.dtEDerslikler.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi);
      foreach (bilesenOgretmen bilesenOgretmen in this.duzeltilecekDers.ogretmenler)
        this.dtEOgretmenler.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.duzeltilecekDers.sinifGruplar)
        this.dtESinifGruplar.Rows.Add((object) bilesenSinifGrup.sinif.id, (object) (bilesenSinifGrup.sinif.kisaAdi + " " + bilesenSinifGrup.grup.kisaAdi), (object) bilesenSinifGrup.grup.id);
      this.txtToplamDersSaati.Text = this.duzeltilecekDers.toplamSaat.ToString();
      this.txtYerlesimSekli.Text = this.duzeltilecekDers.yerlesimStr;
    }

    public void bilgileriDoldur()
    {
      this.dtDersler = this.dataTableUret();
      for (int index = 0; index < tanim.program.dersler.Count; ++index)
      {
        bilesenDers bilesenDers = tanim.program.dersler[index];
        this.dtDersler.Rows.Add((object) bilesenDers.id, (object) bilesenDers.adi);
      }
      this.cmbDersler.DisplayMember = "adi";
      this.cmbDersler.ValueMember = "id";
      this.cmbDersler.DataSource = (object) this.dtDersler;
      this.dtOgretmenler = this.dataTableUret();
      for (int index = 0; index < tanim.program.ogretmenler.Count; ++index)
      {
        bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenler[index];
        this.dtOgretmenler.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi);
      }
      this.cmbOgretmenler.DisplayMember = "adi";
      this.cmbOgretmenler.ValueMember = "id";
      this.cmbOgretmenler.DataSource = (object) this.dtOgretmenler;
      this.dtSiniflar = this.dataTableUret();
      this.dtSiniflar.Columns.Add("kisaAdi", typeof (string));
      for (int index = 0; index < tanim.program.siniflar.Count; ++index)
      {
        bilesenSinif bilesenSinif = tanim.program.siniflar[index];
        this.dtSiniflar.Rows.Add((object) bilesenSinif.id, (object) bilesenSinif.adi, (object) bilesenSinif.kisaAdi);
      }
      this.cmbSiniflar.DisplayMember = "adi";
      this.cmbSiniflar.ValueMember = "id";
      this.cmbSiniflar.DataSource = (object) this.dtSiniflar;
      this.dtDerslikler = this.dataTableUret();
      for (int index = 0; index < tanim.program.derslikler.Count; ++index)
      {
        bilesenDerslik bilesenDerslik = tanim.program.derslikler[index];
        this.dtDerslikler.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi);
      }
      this.cmbDerslikler.DisplayMember = "adi";
      this.cmbDerslikler.ValueMember = "id";
      this.cmbDerslikler.DataSource = (object) this.dtDerslikler;
      this.dtEOgretmenler = this.dataTableUret();
      this.dtEDerslikler = this.dataTableUret();
      this.dtESinifGruplar = this.dataTableUret();
      this.dtESinifGruplar.Columns.Add("grupId", typeof (ushort));
      this.lstDerslikler.DisplayMember = this.lstOgretmenler.DisplayMember = this.lstSinifGrup.DisplayMember = "adi";
      this.lstDerslikler.ValueMember = this.lstOgretmenler.ValueMember = this.lstSinifGrup.ValueMember = "id";
      this.lstDerslikler.DataSource = (object) this.dtEDerslikler;
      this.lstOgretmenler.DataSource = (object) this.dtEOgretmenler;
      this.lstSinifGrup.DataSource = (object) this.dtESinifGruplar;
    }

    private void cmbSiniflar_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.cmbSiniflar.SelectedIndex == -1)
        return;
      this.dtGruplar = this.dataTableUret();
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(Convert.ToUInt16(this.cmbSiniflar.SelectedValue));
      for (int index = 0; index < bilesenSinif.gruplar.Count; ++index)
      {
        bilesenGrup bilesenGrup = bilesenSinif.gruplar[index] as bilesenGrup;
        this.dtGruplar.Rows.Add((object) bilesenGrup.id, (object) bilesenGrup.adi);
      }
      this.cmbGruplar.DisplayMember = "adi";
      this.cmbGruplar.ValueMember = "id";
      this.cmbGruplar.DataSource = (object) this.dtGruplar;
    }

    private void btnOgretmenEkle_Click(object sender, EventArgs e)
    {
      if (this.cmbOgretmenler.SelectedIndex == -1)
        return;
      bool flag = false;
      for (int index = 0; index < this.dtEOgretmenler.Rows.Count; ++index)
      {
        if (this.dtEOgretmenler.Rows[index]["id"].ToString() == this.cmbOgretmenler.SelectedValue.ToString())
          flag = true;
      }
      if (flag)
      {
        int num = (int) MessageBox.Show("Eklenmiş!!");
      }
      else
        this.dtEOgretmenler.Rows.Add(this.dtOgretmenler.Rows[this.cmbOgretmenler.SelectedIndex]["id"], this.dtOgretmenler.Rows[this.cmbOgretmenler.SelectedIndex]["adi"]);
    }

    private void btnOgretmenSil_Click(object sender, EventArgs e)
    {
      if (this.lstOgretmenler.SelectedIndex == -1)
        return;
      this.dtEOgretmenler.Rows.RemoveAt(this.lstOgretmenler.SelectedIndex);
    }

    private void btnDerslikEkle_Click(object sender, EventArgs e)
    {
      if (this.cmbDerslikler.SelectedIndex == -1)
        return;
      if (this.lstOgretmenler.Items.Count > this.lstDerslikler.Items.Count)
      {
        bool flag = false;
        for (int index = 0; index < this.dtEDerslikler.Rows.Count; ++index)
        {
          if (this.dtEDerslikler.Rows[index]["id"].ToString() == this.cmbDerslikler.SelectedValue.ToString())
            flag = true;
        }
        if (flag)
        {
          int num = (int) MessageBox.Show("Eklenmiş!!");
        }
        else
          this.dtEDerslikler.Rows.Add(this.dtDerslikler.Rows[this.cmbDerslikler.SelectedIndex]["id"], this.dtDerslikler.Rows[this.cmbDerslikler.SelectedIndex]["adi"]);
      }
      else
      {
        int num1 = (int) MessageBox.Show("Derslik sayısı öğretmen sayısını geçemez.\nÖğretmenin kullanacağı derslik ekleme sırasına göre belirlenir.\n(Örn:1.Sıradaki öğretmen 1. Sıradaki dersliği kullanır.");
      }
    }

    private void btnDerslikSil_Click(object sender, EventArgs e)
    {
      if (this.lstDerslikler.SelectedIndex == -1)
        return;
      this.dtEDerslikler.Rows.RemoveAt(this.lstDerslikler.SelectedIndex);
    }

    private void btnSinifGrupEkle_Click(object sender, EventArgs e)
    {
      if (this.cmbSiniflar.SelectedIndex == -1 || this.cmbGruplar.SelectedIndex == -1)
        return;
      bool flag = false;
      for (int index = 0; index < this.dtESinifGruplar.Rows.Count; ++index)
      {
        if (this.dtESinifGruplar.Rows[index]["id"].ToString() == this.cmbSiniflar.SelectedValue.ToString() && this.dtESinifGruplar.Rows[index]["grupId"].ToString() == this.cmbGruplar.SelectedValue.ToString())
          flag = true;
      }
      if (flag)
      {
        int num = (int) MessageBox.Show("Eklenmiş!!");
      }
      else
        this.dtESinifGruplar.Rows.Add((object) Convert.ToUInt16(this.cmbSiniflar.SelectedValue.ToString()), (object) (this.dtSiniflar.Rows[this.cmbSiniflar.SelectedIndex]["kisaAdi"].ToString() + " " + this.cmbGruplar.Text), (object) Convert.ToUInt16(this.cmbGruplar.SelectedValue.ToString()));
    }

    private void btnSinifGrupSil_Click(object sender, EventArgs e)
    {
      if (this.lstSinifGrup.SelectedIndex == -1)
        return;
      this.dtESinifGruplar.Rows.RemoveAt(this.lstSinifGrup.SelectedIndex);
    }

    private bool ekle()
    {
      bool flag = false;
      string text = "Hatalı bilgi girişi.\n";
      if (this.cmbDersler.SelectedIndex == -1)
      {
        text += "Ders seçilmemiş!\n";
        flag = true;
      }
      if (this.lstDerslikler.Items.Count == 0 && MessageBox.Show("Derslik önemsiz mi?", "Derslik Seçilmemiş", MessageBoxButtons.YesNo) != DialogResult.Yes)
      {
        text += "Derslik seçilmemiş!\n";
        flag = true;
      }
      if (this.lstOgretmenler.Items.Count == 0)
      {
        text += "Öğretmen seçilmemiş!\n";
        flag = true;
      }
      if (this.lstSinifGrup.Items.Count == 0)
      {
        text += "Sınıf seçilmemiş!\n";
        flag = true;
      }
      try
      {
        byte num1 = Convert.ToByte(this.txtToplamDersSaati.Text);
        string[] strArray = this.txtYerlesimSekli.Text.Split('+');
        int num2 = 0;
        for (int index = 0; index < strArray.Length; ++index)
          num2 += (int) Convert.ToByte(strArray[index]);
        if (num2 != (int) num1)
        {
          flag = true;
          text += "Tanımlanan toplam saat ile bölünme uyuşmuyor!";
        }
      }
      catch
      {
        flag = true;
        text += "Toplam Saat ve Yerleşim şekli girişi yanlış.\n(Örnek: Toplam Saat:4 Yerleşim Şekli:2+2)";
      }
      if (flag)
      {
        int num3 = (int) MessageBox.Show(text);
      }
      else
      {
        bilesenDers _ders = tanim.program.dersGetir(Convert.ToUInt16(this.cmbDersler.SelectedValue));
        List<bilesenOgretmen> _ogretmenler = new List<bilesenOgretmen>();
        for (int index = 0; index < this.dtEOgretmenler.Rows.Count; ++index)
        {
          bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenGetir(Convert.ToUInt16(this.dtEOgretmenler.Rows[index]["id"].ToString()));
          _ogretmenler.Add(bilesenOgretmen);
        }
        List<bilesenDerslik> _derslikler = new List<bilesenDerslik>();
        for (int index = 0; index < this.dtEDerslikler.Rows.Count; ++index)
        {
          bilesenDerslik bilesenDerslik = tanim.program.derslikGetir(Convert.ToUInt16(this.dtEDerslikler.Rows[index]["id"].ToString()));
          _derslikler.Add(bilesenDerslik);
        }
        List<bilesenSinifGrup> _sinifGruplar = new List<bilesenSinifGrup>();
        for (int index = 0; index < this.dtESinifGruplar.Rows.Count; ++index)
        {
          bilesenSinifGrup bilesenSinifGrup = new bilesenSinifGrup(tanim.program.sinifGetir(Convert.ToUInt16(this.dtESinifGruplar.Rows[index]["id"].ToString())), Convert.ToUInt16(this.dtESinifGruplar.Rows[index]["grupId"].ToString()));
          _sinifGruplar.Add(bilesenSinifGrup);
        }
        if (this.duzeltilecekDers == null)
        {
          bilesenTanimliDers bilesenTanimliDers = new bilesenTanimliDers(++tanim.program.idTanimliDersSon, _ders, _sinifGruplar, _ogretmenler, _derslikler, this.txtYerlesimSekli.Text, tanim.program);
          tanim.program.tanimliDersler.Add(bilesenTanimliDers);
          int num1 = (int) MessageBox.Show("Eklendi");
        }
        else
        {
          this.duzeltilecekDers.ders = _ders;
          this.duzeltilecekDers.ogretmenler = _ogretmenler;
          this.duzeltilecekDers.derslikler = _derslikler;
          this.duzeltilecekDers.sinifGruplar = _sinifGruplar;
          this.duzeltilecekDers.yerlesimStr = this.txtYerlesimSekli.Text;
          int num1 = (int) MessageBox.Show("Düzeltildi");
        }
      }
      return flag;
    }

    private void btnTanimliDersEkle_Click(object sender, EventArgs e)
    {
      if (this.ekle())
        return;
      this.Close();
    }

    private void button1_Click(object sender, EventArgs e) => this.ekle();

    private void btnTanimliDersDuzelt_Click(object sender, EventArgs e)
    {
    }

    private void btnYardim_Click(object sender, EventArgs e)
    {
      int num = (int) MessageBox.Show("1. Dersi seçiniz.\n2. Derse girecek öğretmen(ler)i seçerek ekle butonuna basınız.\n3. Ders için kullanılacak derslik(ler)i seçerek ekle butonuna basın. Derslik yoksa veya önemsizse boş bırakılabilir.\n3. Ders için sınıf ve grupları seçerek ekle butonuna basınız. Birden fazla sınıf veya grubu aynı ders altında toplayabilirsiniz.\n4. Toplam ders saati sayısını girin örn: 5\n5. Dersin yerleşim şeklini girin örn: 2+2+1\n\nEkle butonu ile tanımlı ders eklenir, pencere kapanır.\nEkle Devam ile tanımlı ders eklenir, pencere kapanmaz. Tanımlamalar üzerinde küçük değişiklikler yapılarak hızlı bir şekilde birden fazla ders eklenebilir.");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.cmbDersler = new ComboBox();
      this.label1 = new Label();
      this.panel1 = new Panel();
      this.panel2 = new Panel();
      this.btnOgretmenSil = new Button();
      this.btnOgretmenEkle = new Button();
      this.lstOgretmenler = new ListBox();
      this.label2 = new Label();
      this.cmbOgretmenler = new ComboBox();
      this.panel3 = new Panel();
      this.btnDerslikSil = new Button();
      this.btnDerslikEkle = new Button();
      this.lstDerslikler = new ListBox();
      this.label3 = new Label();
      this.cmbDerslikler = new ComboBox();
      this.panel4 = new Panel();
      this.btnSinifGrupSil = new Button();
      this.btnSinifGrupEkle = new Button();
      this.label6 = new Label();
      this.label5 = new Label();
      this.cmbGruplar = new ComboBox();
      this.lstSinifGrup = new ListBox();
      this.label4 = new Label();
      this.cmbSiniflar = new ComboBox();
      this.panel5 = new Panel();
      this.txtYerlesimSekli = new TextBox();
      this.label8 = new Label();
      this.txtToplamDersSaati = new TextBox();
      this.label7 = new Label();
      this.btnTanimliDersEkle = new Button();
      this.btnYardim = new Button();
      this.btnEkleDevam = new Button();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.panel3.SuspendLayout();
      this.panel4.SuspendLayout();
      this.panel5.SuspendLayout();
      this.SuspendLayout();
      this.cmbDersler.BackColor = Color.MistyRose;
      this.cmbDersler.FormattingEnabled = true;
      this.cmbDersler.Location = new Point(82, 15);
      this.cmbDersler.Name = "cmbDersler";
      this.cmbDersler.Size = new Size(463, 21);
      this.cmbDersler.TabIndex = 0;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(3, 18);
      this.label1.Name = "label1";
      this.label1.Size = new Size(61, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Dersi Seçin";
      this.panel1.BorderStyle = BorderStyle.FixedSingle;
      this.panel1.Controls.Add((Control) this.label1);
      this.panel1.Controls.Add((Control) this.cmbDersler);
      this.panel1.Location = new Point(12, 12);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(562, 54);
      this.panel1.TabIndex = 1;
      this.panel2.BorderStyle = BorderStyle.FixedSingle;
      this.panel2.Controls.Add((Control) this.btnOgretmenSil);
      this.panel2.Controls.Add((Control) this.btnOgretmenEkle);
      this.panel2.Controls.Add((Control) this.lstOgretmenler);
      this.panel2.Controls.Add((Control) this.label2);
      this.panel2.Controls.Add((Control) this.cmbOgretmenler);
      this.panel2.Location = new Point(12, 72);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(278, 160);
      this.panel2.TabIndex = 2;
      this.btnOgretmenSil.FlatStyle = FlatStyle.Flat;
      this.btnOgretmenSil.Location = new Point(186, 35);
      this.btnOgretmenSil.Name = "btnOgretmenSil";
      this.btnOgretmenSil.Size = new Size(75, 23);
      this.btnOgretmenSil.TabIndex = 2;
      this.btnOgretmenSil.Text = "Sil";
      this.btnOgretmenSil.UseVisualStyleBackColor = true;
      this.btnOgretmenSil.Click += new EventHandler(this.btnOgretmenSil_Click);
      this.btnOgretmenEkle.FlatStyle = FlatStyle.Flat;
      this.btnOgretmenEkle.Location = new Point(82, 35);
      this.btnOgretmenEkle.Name = "btnOgretmenEkle";
      this.btnOgretmenEkle.Size = new Size(75, 23);
      this.btnOgretmenEkle.TabIndex = 1;
      this.btnOgretmenEkle.Text = "Ekle";
      this.btnOgretmenEkle.UseVisualStyleBackColor = true;
      this.btnOgretmenEkle.Click += new EventHandler(this.btnOgretmenEkle_Click);
      this.lstOgretmenler.BackColor = Color.MistyRose;
      this.lstOgretmenler.FormattingEnabled = true;
      this.lstOgretmenler.Location = new Point(82, 64);
      this.lstOgretmenler.Name = "lstOgretmenler";
      this.lstOgretmenler.Size = new Size(179, 82);
      this.lstOgretmenler.TabIndex = 3;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(3, 14);
      this.label2.Name = "label2";
      this.label2.Size = new Size(53, 13);
      this.label2.TabIndex = 1;
      this.label2.Text = "Öğretmen";
      this.cmbOgretmenler.FormattingEnabled = true;
      this.cmbOgretmenler.Location = new Point(82, 11);
      this.cmbOgretmenler.Name = "cmbOgretmenler";
      this.cmbOgretmenler.Size = new Size(179, 21);
      this.cmbOgretmenler.TabIndex = 0;
      this.panel3.BorderStyle = BorderStyle.FixedSingle;
      this.panel3.Controls.Add((Control) this.btnDerslikSil);
      this.panel3.Controls.Add((Control) this.btnDerslikEkle);
      this.panel3.Controls.Add((Control) this.lstDerslikler);
      this.panel3.Controls.Add((Control) this.label3);
      this.panel3.Controls.Add((Control) this.cmbDerslikler);
      this.panel3.Location = new Point(296, 72);
      this.panel3.Name = "panel3";
      this.panel3.Size = new Size(278, 160);
      this.panel3.TabIndex = 3;
      this.btnDerslikSil.FlatStyle = FlatStyle.Flat;
      this.btnDerslikSil.Location = new Point(186, 35);
      this.btnDerslikSil.Name = "btnDerslikSil";
      this.btnDerslikSil.Size = new Size(75, 23);
      this.btnDerslikSil.TabIndex = 2;
      this.btnDerslikSil.Text = "Sil";
      this.btnDerslikSil.UseVisualStyleBackColor = true;
      this.btnDerslikSil.Click += new EventHandler(this.btnDerslikSil_Click);
      this.btnDerslikEkle.FlatStyle = FlatStyle.Flat;
      this.btnDerslikEkle.Location = new Point(82, 35);
      this.btnDerslikEkle.Name = "btnDerslikEkle";
      this.btnDerslikEkle.Size = new Size(75, 23);
      this.btnDerslikEkle.TabIndex = 1;
      this.btnDerslikEkle.Text = "Ekle";
      this.btnDerslikEkle.UseVisualStyleBackColor = true;
      this.btnDerslikEkle.Click += new EventHandler(this.btnDerslikEkle_Click);
      this.lstDerslikler.BackColor = Color.MistyRose;
      this.lstDerslikler.FormattingEnabled = true;
      this.lstDerslikler.Location = new Point(82, 64);
      this.lstDerslikler.Name = "lstDerslikler";
      this.lstDerslikler.Size = new Size(179, 82);
      this.lstDerslikler.TabIndex = 3;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(3, 14);
      this.label3.Name = "label3";
      this.label3.Size = new Size(39, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "Derslik";
      this.cmbDerslikler.FormattingEnabled = true;
      this.cmbDerslikler.Location = new Point(82, 11);
      this.cmbDerslikler.Name = "cmbDerslikler";
      this.cmbDerslikler.Size = new Size(179, 21);
      this.cmbDerslikler.TabIndex = 0;
      this.panel4.BorderStyle = BorderStyle.FixedSingle;
      this.panel4.Controls.Add((Control) this.btnSinifGrupSil);
      this.panel4.Controls.Add((Control) this.btnSinifGrupEkle);
      this.panel4.Controls.Add((Control) this.label6);
      this.panel4.Controls.Add((Control) this.label5);
      this.panel4.Controls.Add((Control) this.cmbGruplar);
      this.panel4.Controls.Add((Control) this.lstSinifGrup);
      this.panel4.Controls.Add((Control) this.label4);
      this.panel4.Controls.Add((Control) this.cmbSiniflar);
      this.panel4.Location = new Point(12, 238);
      this.panel4.Name = "panel4";
      this.panel4.Size = new Size(562, 120);
      this.panel4.TabIndex = 4;
      this.btnSinifGrupSil.FlatStyle = FlatStyle.Flat;
      this.btnSinifGrupSil.Location = new Point(186, 77);
      this.btnSinifGrupSil.Name = "btnSinifGrupSil";
      this.btnSinifGrupSil.Size = new Size(75, 23);
      this.btnSinifGrupSil.TabIndex = 3;
      this.btnSinifGrupSil.Text = "Sil";
      this.btnSinifGrupSil.UseVisualStyleBackColor = true;
      this.btnSinifGrupSil.Click += new EventHandler(this.btnSinifGrupSil_Click);
      this.btnSinifGrupEkle.FlatStyle = FlatStyle.Flat;
      this.btnSinifGrupEkle.Location = new Point(82, 77);
      this.btnSinifGrupEkle.Name = "btnSinifGrupEkle";
      this.btnSinifGrupEkle.Size = new Size(75, 23);
      this.btnSinifGrupEkle.TabIndex = 2;
      this.btnSinifGrupEkle.Text = "Ekle";
      this.btnSinifGrupEkle.UseVisualStyleBackColor = true;
      this.btnSinifGrupEkle.Click += new EventHandler(this.btnSinifGrupEkle_Click);
      this.label6.AutoSize = true;
      this.label6.Location = new Point(287, 23);
      this.label6.Name = "label6";
      this.label6.Size = new Size(53, 13);
      this.label6.TabIndex = 5;
      this.label6.Text = "Sınıf Grup";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(3, 53);
      this.label5.Name = "label5";
      this.label5.Size = new Size(30, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Grup";
      this.cmbGruplar.FormattingEnabled = true;
      this.cmbGruplar.Location = new Point(82, 50);
      this.cmbGruplar.Name = "cmbGruplar";
      this.cmbGruplar.Size = new Size(179, 21);
      this.cmbGruplar.TabIndex = 1;
      this.lstSinifGrup.BackColor = Color.MistyRose;
      this.lstSinifGrup.FormattingEnabled = true;
      this.lstSinifGrup.Location = new Point(366, 23);
      this.lstSinifGrup.Name = "lstSinifGrup";
      this.lstSinifGrup.Size = new Size(179, 82);
      this.lstSinifGrup.TabIndex = 4;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(3, 26);
      this.label4.Name = "label4";
      this.label4.Size = new Size(27, 13);
      this.label4.TabIndex = 1;
      this.label4.Text = "Sınıf";
      this.cmbSiniflar.FormattingEnabled = true;
      this.cmbSiniflar.Location = new Point(82, 23);
      this.cmbSiniflar.Name = "cmbSiniflar";
      this.cmbSiniflar.Size = new Size(179, 21);
      this.cmbSiniflar.TabIndex = 0;
      this.cmbSiniflar.SelectedIndexChanged += new EventHandler(this.cmbSiniflar_SelectedIndexChanged);
      this.panel5.BorderStyle = BorderStyle.FixedSingle;
      this.panel5.Controls.Add((Control) this.txtYerlesimSekli);
      this.panel5.Controls.Add((Control) this.label8);
      this.panel5.Controls.Add((Control) this.txtToplamDersSaati);
      this.panel5.Controls.Add((Control) this.label7);
      this.panel5.Location = new Point(12, 364);
      this.panel5.Name = "panel5";
      this.panel5.Size = new Size(562, 58);
      this.panel5.TabIndex = 5;
      this.txtYerlesimSekli.BackColor = Color.MistyRose;
      this.txtYerlesimSekli.Location = new Point(366, 17);
      this.txtYerlesimSekli.Name = "txtYerlesimSekli";
      this.txtYerlesimSekli.Size = new Size(100, 20);
      this.txtYerlesimSekli.TabIndex = 1;
      this.label8.AutoSize = true;
      this.label8.Location = new Point(287, 20);
      this.label8.Name = "label8";
      this.label8.Size = new Size(72, 13);
      this.label8.TabIndex = 0;
      this.label8.Text = "Yerleşim Şekli";
      this.txtToplamDersSaati.BackColor = Color.MistyRose;
      this.txtToplamDersSaati.Location = new Point(103, 17);
      this.txtToplamDersSaati.Name = "txtToplamDersSaati";
      this.txtToplamDersSaati.Size = new Size(100, 20);
      this.txtToplamDersSaati.TabIndex = 0;
      this.label7.AutoSize = true;
      this.label7.Location = new Point(3, 20);
      this.label7.Name = "label7";
      this.label7.Size = new Size(94, 13);
      this.label7.TabIndex = 0;
      this.label7.Text = "Toplam Ders Saati";
      this.btnTanimliDersEkle.Location = new Point(467, 428);
      this.btnTanimliDersEkle.Name = "btnTanimliDersEkle";
      this.btnTanimliDersEkle.Size = new Size(107, 38);
      this.btnTanimliDersEkle.TabIndex = 6;
      this.btnTanimliDersEkle.Text = "Ekle";
      this.btnTanimliDersEkle.UseVisualStyleBackColor = true;
      this.btnTanimliDersEkle.Click += new EventHandler(this.btnTanimliDersEkle_Click);
      this.btnYardim.Location = new Point(12, 428);
      this.btnYardim.Name = "btnYardim";
      this.btnYardim.Size = new Size(75, 38);
      this.btnYardim.TabIndex = 6;
      this.btnYardim.TabStop = false;
      this.btnYardim.Text = "Yardım";
      this.btnYardim.UseVisualStyleBackColor = true;
      this.btnYardim.Click += new EventHandler(this.btnYardim_Click);
      this.btnEkleDevam.Location = new Point(354, 428);
      this.btnEkleDevam.Name = "btnEkleDevam";
      this.btnEkleDevam.Size = new Size(107, 38);
      this.btnEkleDevam.TabIndex = 7;
      this.btnEkleDevam.Text = "Ekle Devam";
      this.btnEkleDevam.UseVisualStyleBackColor = true;
      this.btnEkleDevam.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(585, 475);
      this.Controls.Add((Control) this.btnEkleDevam);
      this.Controls.Add((Control) this.btnYardim);
      this.Controls.Add((Control) this.btnTanimliDersEkle);
      this.Controls.Add((Control) this.panel5);
      this.Controls.Add((Control) this.panel4);
      this.Controls.Add((Control) this.panel3);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel1);
      this.Name = nameof (DersTanimlamaForm);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Ders Tanımlama";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.panel2.ResumeLayout(false);
      this.panel2.PerformLayout();
      this.panel3.ResumeLayout(false);
      this.panel3.PerformLayout();
      this.panel4.ResumeLayout(false);
      this.panel4.PerformLayout();
      this.panel5.ResumeLayout(false);
      this.panel5.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
