using DersDagitim.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DersDagitim
{
  public class BilgiGirisForm : Form
  {
    private DataTable dtDersler;
    private DataTable dtSiniflar;
    private DataTable dtDerslikler;
    private DataTable dtOgretmenler;
    private DataTable dtGruplar;
    private IContainer components;
    public TabControl tbBilgiGirisleri;
    private TabPage tpDersler;
    private TabPage tpOgretmenler;
    private TabPage tpDerslikler;
    private TabPage tpSiniflar;
    private DataGridView dgvDersler;
    private Button btnDersSil;
    private Button btnDersDuzelt;
    private Button btnDersEkle;
    private Label label2;
    private TextBox txtDersKisaAdi;
    private Label label1;
    private TextBox txtDersAdi;
    private Button btnDersKosullar;
    private Button btnOgretmenKosullar;
    private Button btnOgretmenSil;
    private Button btnOgretmenDuzelt;
    private Button btnOgretmenEkle;
    private Label label3;
    private TextBox txtOgretmenKisaAdi;
    private Label label4;
    private TextBox txtOgretmenAdi;
    private DataGridView dgvOgretmenler;
    private Button btnDerslikKosullar;
    private Button btnDerslikSil;
    private Button btnDerslikDuzelt;
    private Button btnDerslikEkle;
    private Label label5;
    private TextBox txtDerslikKisaAdi;
    private Label label6;
    private TextBox txtDerslikAdi;
    private DataGridView dgvDerslikler;
    private Button btnSinifKosullar;
    private Button btnSinifSil;
    private Button btnSinifDuzelt;
    private Button btnSinifEkle;
    private Label label7;
    private TextBox txtSinifKisaAdi;
    private Label label8;
    private TextBox txtSinifAdi;
    private DataGridView dgvSiniflar;
    private DataGridView dgvSinifGruplar;
    private GroupBox groupBox1;
    private Label label9;
    private TextBox txtGrupAdi;
    private TextBox txtGrupKisaAdi;
    private Label label10;
    private Button btnGrupSil;
    private Button btnGrupDuzelt;
    private Button btnGrupEkle;
    private Button button1;
    private Button button2;
    private Button button4;
    private Button button3;
    private Panel panel1;
    private DataGridViewTextBoxColumn DersKolonID;
    private DataGridViewTextBoxColumn KolonDersAdi;
    private DataGridViewTextBoxColumn KolonDersKisaAd;
    private DataGridViewTextBoxColumn KolonDersToplamDers;
    private DataGridViewImageColumn KolonDersKosul;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewImageColumn dataGridViewImageColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private DataGridViewImageColumn dataGridViewImageColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    private DataGridViewImageColumn dataGridViewImageColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
    private Button button5;
    private Button button6;
    private Button button7;
    private ContextMenuStrip siniflarAcilirMenu;
    private ToolStripMenuItem sinifYukariTasıKomutu;
    private ToolStripMenuItem sinifAsagiTasiKomutu;
    private ContextMenuStrip dersliklerAcilirMenu;
    private ToolStripMenuItem toolStripMenuItem1;
    private ToolStripMenuItem toolStripMenuItem2;
    private ContextMenuStrip ogretmenlerAcilirMenu;
    private ToolStripMenuItem toolStripMenuItem3;
    private ToolStripMenuItem toolStripMenuItem4;
    private ToolStripSeparator toolStripMenuItem5;
    private ToolStripMenuItem ogretmenAlfabetikSıralaToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem6;
    private ToolStripMenuItem derslikAlfabetikSirala;
    private ToolStripSeparator toolStripMenuItem7;
    private ToolStripMenuItem sinifAlfabetikSirala;
    private ContextMenuStrip derslerAcilirMenu;
    private ToolStripMenuItem derslerYukariTasi;
    private ToolStripMenuItem derslerAsagiTasi;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem derslerAlfabetikSirala;
    private Button button8;
    private Button button9;
    private Button button10;
    private Button button11;
    private Button button12;
    private Button button13;
    private Button button14;
    private Button button15;
    private Button button16;
    private Button button17;
    private Button button18;
    private Button button19;

    public BilgiGirisForm(ushort tabNo)
    {
      this.InitializeComponent();
      this.tbBilgiGirisleri.SelectedIndex = (int) tabNo;
    }

    public void bilgileriYenile()
    {
      tanim.program.temizle();
      this.dersleriGetir();
      this.ogretmenleriGetir();
      this.derslikleriGetir();
      this.siniflariGetir();
    }

    private void BilgiGirisForm_Load(object sender, EventArgs e) => this.bilgileriYenile();

    private void dersleriGetir()
    {
      int index1 = -1;
      int num = -1;
      if (this.dgvDersler.Rows.Count > 0)
        num = this.dgvDersler.FirstDisplayedScrollingRowIndex;
      if (this.dgvDersler.SelectedRows.Count > 0)
        index1 = this.dgvDersler.SelectedRows[0].Index;
      this.dtDersler = new DataTable();
      this.dtDersler.Columns.Add("id", typeof (ushort));
      this.dtDersler.Columns.Add("adi", typeof (string));
      this.dtDersler.Columns.Add("kisaadi", typeof (string));
      this.dtDersler.Columns.Add("derssayisi", typeof (ushort));
      this.dtDersler.Columns.Add("kosul", typeof (Bitmap));
      for (int index2 = 0; index2 < tanim.program.dersler.Count; ++index2)
      {
        bilesenDers bilesenDers = tanim.program.dersler[index2];
        this.dtDersler.Rows.Add((object) bilesenDers.id, (object) bilesenDers.adi, (object) bilesenDers.kisaAdi, (object) tanim.program.bilesenDersSayisi((bilesenTaban) bilesenDers), (object) araclar.kosulResim(bilesenDers.kosul));
      }
      this.dgvDersler.DataSource = (object) this.dtDersler;
      if (index1 > this.dgvDersler.Rows.Count - 1)
        --index1;
      if (index1 != -1)
        this.dgvDersler.Rows[index1].Selected = true;
      if (num >= this.dgvDersler.Rows.Count || num == -1)
        return;
      this.dgvDersler.FirstDisplayedScrollingRowIndex = num;
    }

    private void btnDersEkle_Click(object sender, EventArgs e)
    {
      if (!(this.txtDersAdi.Text != "") || !(this.txtDersKisaAdi.Text != ""))
        return;
      bilesenDers bilesenDers = new bilesenDers(++tanim.program.idDersSon, araclar.diziOlustur(), this.txtDersAdi.Text, this.txtDersKisaAdi.Text);
      tanim.program.dersler.Add(bilesenDers);
      this.dersleriGetir();
      this.txtDersAdi.Text = this.txtDersKisaAdi.Text = "";
      this.txtDersAdi.Focus();
    }

    private void btnDersKosullar_Click(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDersler.SelectedRows[0].Cells[0].Value);
      bilesenDers bilesenDers = tanim.program.dersGetir(uint16);
      int num = (int) new KosulForm(ref bilesenDers.kosul, bilesenDers.adi).ShowDialog();
      this.dersleriGetir();
    }

    private void dgvDersler_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0 || this.dgvDersler.Rows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDersler.SelectedRows[0].Cells[0].Value);
      bilesenDers bilesenDers = tanim.program.dersGetir(uint16);
      this.txtDersAdi.Text = bilesenDers.adi;
      this.txtDersKisaAdi.Text = bilesenDers.kisaAdi;
    }

    private void btnDersDuzelt_Click(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDersler.SelectedRows[0].Cells[0].Value);
      bilesenDers bilesenDers = tanim.program.dersGetir(uint16);
      bilesenDers.adi = this.txtDersAdi.Text;
      bilesenDers.kisaAdi = this.txtDersKisaAdi.Text;
      this.dersleriGetir();
    }

    private void btnDersSil_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Seçili olan silinecek! Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes || this.dgvDersler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDersler.SelectedRows[0].Cells[0].Value);
      bilesenDers bilesenDers = tanim.program.dersGetir(uint16);
      for (int index = 0; index < tanim.program.dersler.Count; ++index)
      {
        if ((int) bilesenDers.id == (int) tanim.program.dersler[index].id)
          tanim.program.dersler.RemoveAt(index);
      }
      this.bilgileriYenile();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDersler.SelectedRows[0].Cells[0].Value);
      int num = (int) new TanimliDersListesi((bilesenTaban) tanim.program.dersGetir(uint16)).ShowDialog();
      this.bilgileriYenile();
    }

    private void ogretmenleriGetir()
    {
      int index1 = -1;
      int num = -1;
      if (this.dgvOgretmenler.SelectedRows.Count > 0)
        num = this.dgvOgretmenler.FirstDisplayedScrollingRowIndex;
      if (this.dgvOgretmenler.SelectedRows.Count > 0)
        index1 = this.dgvOgretmenler.SelectedRows[0].Index;
      this.dtOgretmenler = new DataTable();
      this.dtOgretmenler.Columns.Add("id", typeof (ushort));
      this.dtOgretmenler.Columns.Add("adisoyadi", typeof (string));
      this.dtOgretmenler.Columns.Add("kisaadi", typeof (string));
      this.dtOgretmenler.Columns.Add("derssayisi", typeof (ushort));
      this.dtOgretmenler.Columns.Add("kosul", typeof (Bitmap));
      for (int index2 = 0; index2 < tanim.program.ogretmenler.Count; ++index2)
      {
        bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenler[index2];
        this.dtOgretmenler.Rows.Add((object) bilesenOgretmen.id, (object) bilesenOgretmen.adi, (object) bilesenOgretmen.kisaAdi, (object) tanim.program.bilesenDersSayisi((bilesenTaban) bilesenOgretmen), (object) araclar.kosulResim(bilesenOgretmen.kosul));
      }
      this.dgvOgretmenler.DataSource = (object) this.dtOgretmenler;
      if (index1 > this.dgvOgretmenler.Rows.Count - 1)
        --index1;
      if (index1 != -1)
        this.dgvOgretmenler.Rows[index1].Selected = true;
      if (num >= this.dgvOgretmenler.Rows.Count || num == -1)
        return;
      this.dgvOgretmenler.FirstDisplayedScrollingRowIndex = num;
    }

    private void btnOgretmenEkle_Click(object sender, EventArgs e)
    {
      if (!(this.txtOgretmenAdi.Text != "") || !(this.txtOgretmenKisaAdi.Text != ""))
        return;
      bilesenOgretmen bilesenOgretmen = new bilesenOgretmen(++tanim.program.idOgretmenSon, araclar.diziOlustur(), this.txtOgretmenAdi.Text, this.txtOgretmenKisaAdi.Text);
      tanim.program.ogretmenler.Add(bilesenOgretmen);
      this.ogretmenleriGetir();
      this.txtOgretmenAdi.Text = this.txtOgretmenKisaAdi.Text = "";
      this.txtOgretmenAdi.Focus();
    }

    private void btnOgretmenKosullar_Click(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenGetir(uint16);
      int num = (int) new KosulForm(ref bilesenOgretmen.kosul, bilesenOgretmen.adi).ShowDialog();
      this.ogretmenleriGetir();
    }

    private void dgvOgretmenler_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0 || this.dgvOgretmenler.Rows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenGetir(uint16);
      this.txtOgretmenAdi.Text = bilesenOgretmen.adi;
      this.txtOgretmenKisaAdi.Text = bilesenOgretmen.kisaAdi;
    }

    private void btnOgretmenDuzelt_Click(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenGetir(uint16);
      bilesenOgretmen.adi = this.txtOgretmenAdi.Text;
      bilesenOgretmen.kisaAdi = this.txtOgretmenKisaAdi.Text;
      this.ogretmenleriGetir();
    }

    private void btnOgretmenSil_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Seçili olan silinecek! Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes || this.dgvOgretmenler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenGetir(uint16);
      for (int index = 0; index < tanim.program.ogretmenler.Count; ++index)
      {
        if ((int) bilesenOgretmen.id == (int) tanim.program.ogretmenler[index].id)
          tanim.program.ogretmenler.RemoveAt(index);
      }
      this.bilgileriYenile();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      int num = (int) new TanimliDersListesi((bilesenTaban) tanim.program.ogretmenGetir(uint16)).ShowDialog();
      this.bilgileriYenile();
    }

    private void derslikleriGetir()
    {
      int index1 = -1;
      int num = -1;
      if (this.dgvDerslikler.SelectedRows.Count > 0)
        num = this.dgvDerslikler.FirstDisplayedScrollingRowIndex;
      if (this.dgvDerslikler.SelectedRows.Count > 0)
        index1 = this.dgvDerslikler.SelectedRows[0].Index;
      this.dtDerslikler = new DataTable();
      this.dtDerslikler.Columns.Add("id", typeof (ushort));
      this.dtDerslikler.Columns.Add("adi", typeof (string));
      this.dtDerslikler.Columns.Add("kisaadi", typeof (string));
      this.dtDerslikler.Columns.Add("Derssayisi", typeof (ushort));
      this.dtDerslikler.Columns.Add("kosul", typeof (Bitmap));
      for (int index2 = 0; index2 < tanim.program.derslikler.Count; ++index2)
      {
        bilesenDerslik bilesenDerslik = tanim.program.derslikler[index2];
        this.dtDerslikler.Rows.Add((object) bilesenDerslik.id, (object) bilesenDerslik.adi, (object) bilesenDerslik.kisaAdi, (object) tanim.program.bilesenDersSayisi((bilesenTaban) bilesenDerslik), (object) araclar.kosulResim(bilesenDerslik.kosul));
      }
      this.dgvDerslikler.DataSource = (object) this.dtDerslikler;
      if (index1 > this.dgvDerslikler.Rows.Count - 1)
        --index1;
      if (index1 != -1)
        this.dgvDerslikler.Rows[index1].Selected = true;
      if (num >= this.dgvDerslikler.Rows.Count || num == -1)
        return;
      this.dgvDerslikler.FirstDisplayedScrollingRowIndex = num;
    }

    private void btnDerslikEkle_Click(object sender, EventArgs e)
    {
      if (!(this.txtDerslikAdi.Text != "") || !(this.txtDerslikKisaAdi.Text != ""))
        return;
      bilesenDerslik bilesenDerslik = new bilesenDerslik(++tanim.program.idDerslikSon, araclar.diziOlustur(), this.txtDerslikAdi.Text, this.txtDerslikKisaAdi.Text);
      tanim.program.derslikler.Add(bilesenDerslik);
      this.derslikleriGetir();
      this.txtDerslikAdi.Text = this.txtDerslikKisaAdi.Text = "";
      this.txtDerslikAdi.Focus();
    }

    private void btnDerslikKosullar_Click(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      bilesenDerslik bilesenDerslik = tanim.program.derslikGetir(uint16);
      int num = (int) new KosulForm(ref bilesenDerslik.kosul, bilesenDerslik.adi).ShowDialog();
      this.derslikleriGetir();
    }

    private void dgvDerslikler_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0 || this.dgvDerslikler.Rows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      bilesenDerslik bilesenDerslik = tanim.program.derslikGetir(uint16);
      this.txtDerslikAdi.Text = bilesenDerslik.adi;
      this.txtDerslikKisaAdi.Text = bilesenDerslik.kisaAdi;
    }

    private void btnDerslikDuzelt_Click(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      bilesenDerslik bilesenDerslik = tanim.program.derslikGetir(uint16);
      bilesenDerslik.adi = this.txtDerslikAdi.Text;
      bilesenDerslik.kisaAdi = this.txtDerslikKisaAdi.Text;
      this.derslikleriGetir();
    }

    private void btnDerslikSil_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Seçili olan silinecek! Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes || this.dgvDerslikler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      bilesenDerslik bilesenDerslik = tanim.program.derslikGetir(uint16);
      for (int index = 0; index < tanim.program.derslikler.Count; ++index)
      {
        if ((int) bilesenDerslik.id == (int) tanim.program.derslikler[index].id)
          tanim.program.derslikler.RemoveAt(index);
      }
      this.bilgileriYenile();
    }

    private void button3_Click_1(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      int num = (int) new TanimliDersListesi((bilesenTaban) tanim.program.derslikGetir(uint16)).ShowDialog();
      this.bilgileriYenile();
    }

    private void siniflariGetir()
    {
      int index1 = -1;
      int num = -1;
      if (this.dgvSiniflar.SelectedRows.Count > 0)
        num = this.dgvSiniflar.FirstDisplayedScrollingRowIndex;
      if (this.dgvSiniflar.SelectedRows.Count > 0)
        index1 = this.dgvSiniflar.SelectedRows[0].Index;
      this.dtSiniflar = new DataTable();
      this.dtSiniflar.Columns.Add("id", typeof (ushort));
      this.dtSiniflar.Columns.Add("adi", typeof (string));
      this.dtSiniflar.Columns.Add("kisaadi", typeof (string));
      this.dtSiniflar.Columns.Add("Derssayisi", typeof (ushort));
      this.dtSiniflar.Columns.Add("kosul", typeof (Bitmap));
      for (int index2 = 0; index2 < tanim.program.siniflar.Count; ++index2)
      {
        bilesenSinif bilesenSinif = tanim.program.siniflar[index2];
        this.dtSiniflar.Rows.Add((object) bilesenSinif.id, (object) bilesenSinif.adi, (object) bilesenSinif.kisaAdi, (object) tanim.program.bilesenDersSayisi((bilesenTaban) bilesenSinif), (object) araclar.kosulResim(bilesenSinif.kosul));
      }
      this.dgvSiniflar.DataSource = (object) this.dtSiniflar;
      if (index1 > this.dgvSiniflar.Rows.Count - 1)
        --index1;
      if (index1 != -1)
        this.dgvSiniflar.Rows[index1].Selected = true;
      if (num >= this.dgvSiniflar.Rows.Count || num == -1)
        return;
      this.dgvSiniflar.FirstDisplayedScrollingRowIndex = num;
    }

    private void btnSinifEkle_Click(object sender, EventArgs e)
    {
      if (!(this.txtSinifAdi.Text != "") || !(this.txtSinifKisaAdi.Text != ""))
        return;
      bilesenSinif bilesenSinif = new bilesenSinif(++tanim.program.idSinifSon, araclar.diziOlustur(), this.txtSinifAdi.Text, this.txtSinifKisaAdi.Text, new ArrayList());
      tanim.program.siniflar.Add(bilesenSinif);
      this.siniflariGetir();
      this.txtSinifAdi.Text = this.txtSinifKisaAdi.Text = "";
      this.txtSinifAdi.Focus();
    }

    private void btnSinifKosullar_Click(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      int num = (int) new KosulForm(ref bilesenSinif.kosul, bilesenSinif.adi).ShowDialog();
      this.siniflariGetir();
    }

    private void gruplariGetir(ushort sinifId)
    {
      int index1 = -1;
      if (this.dgvSinifGruplar.SelectedRows.Count > 0 && this.dgvSinifGruplar.Rows.Count > 0)
        index1 = this.dgvSinifGruplar.SelectedRows[0].Index;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      this.dtGruplar = new DataTable();
      this.dtGruplar.Columns.Add("id", typeof (ushort));
      this.dtGruplar.Columns.Add("adi", typeof (string));
      this.dtGruplar.Columns.Add("kisaadi", typeof (string));
      this.dtGruplar.Columns.Add("Derssayisi", typeof (ushort));
      for (int index2 = 0; index2 < bilesenSinif.gruplar.Count; ++index2)
      {
        bilesenGrup bilesenGrup = bilesenSinif.gruplar[index2] as bilesenGrup;
        this.dtGruplar.Rows.Add((object) bilesenGrup.id, (object) bilesenGrup.adi, (object) bilesenGrup.kisaAdi, (object) tanim.program.bilesenDersSayisi((bilesenTaban) bilesenSinif, (bilesenTaban) bilesenGrup));
      }
      this.dgvSinifGruplar.DataSource = (object) this.dtGruplar;
      if (index1 > this.dgvSinifGruplar.Rows.Count - 1)
        --index1;
      if (index1 == -1 || index1 >= this.dgvSinifGruplar.Rows.Count)
        return;
      this.dgvSinifGruplar.Rows[index1].Selected = true;
    }

    private void dgvSiniflar_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0 || this.dgvSiniflar.Rows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      this.txtSinifAdi.Text = bilesenSinif.adi;
      this.txtSinifKisaAdi.Text = bilesenSinif.kisaAdi;
      this.gruplariGetir(uint16);
    }

    private void btnSinifDuzelt_Click(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      bilesenSinif.adi = this.txtSinifAdi.Text;
      bilesenSinif.kisaAdi = this.txtSinifKisaAdi.Text;
      this.siniflariGetir();
    }

    private void btnSinifSil_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Seçili olan silinecek! Emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes || this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      for (int index = 0; index < tanim.program.siniflar.Count; ++index)
      {
        if ((int) bilesenSinif.id == (int) tanim.program.siniflar[index].id)
          tanim.program.siniflar.RemoveAt(index);
      }
      this.bilgileriYenile();
    }

    private void btnGrupEkle_Click(object sender, EventArgs e)
    {
      if (!(this.txtGrupAdi.Text != "") || !(this.txtGrupKisaAdi.Text != "") || this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16);
      bilesenGrup bilesenGrup = new bilesenGrup(++bilesenSinif.grupIdSon, this.txtGrupAdi.Text, this.txtGrupKisaAdi.Text);
      bilesenSinif.gruplar.Add((object) bilesenGrup);
      this.gruplariGetir(uint16);
      this.txtGrupAdi.Text = this.txtGrupKisaAdi.Text = "";
      this.txtGrupAdi.Focus();
    }

    private void btnGrupDuzelt_Click(object sender, EventArgs e)
    {
      if (this.dgvSinifGruplar.SelectedRows.Count <= 0 || this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16_1 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16_1);
      ushort uint16_2 = Convert.ToUInt16(this.dgvSinifGruplar.SelectedRows[0].Cells[0].Value);
      bilesenGrup bilesenGrup = bilesenSinif.grupGetir(uint16_2);
      if (uint16_2 == (ushort) 0)
        return;
      bilesenGrup.adi = this.txtGrupAdi.Text;
      bilesenGrup.kisaAdi = this.txtGrupKisaAdi.Text;
      this.gruplariGetir(uint16_1);
    }

    private void btnGrupSil_Click(object sender, EventArgs e)
    {
      if (this.dgvSinifGruplar.SelectedRows.Count <= 0 || this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16_1 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenSinif bilesenSinif = tanim.program.sinifGetir(uint16_1);
      ushort uint16_2 = Convert.ToUInt16(this.dgvSinifGruplar.SelectedRows[0].Cells[0].Value);
      for (int index = 0; index < bilesenSinif.gruplar.Count; ++index)
      {
        if ((int) (bilesenSinif.gruplar[index] as bilesenGrup).id == (int) uint16_2 && uint16_2 != (ushort) 0)
          bilesenSinif.gruplar.RemoveAt(index);
      }
      this.bilgileriYenile();
    }

    private void dgvSinifGruplar_SelectionChanged(object sender, EventArgs e)
    {
      if (this.dgvSinifGruplar.SelectedRows.Count <= 0 || this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      bilesenGrup bilesenGrup = tanim.program.sinifGetir(uint16).grupGetir(Convert.ToUInt16(this.dgvSinifGruplar.SelectedRows[0].Cells[0].Value));
      if (bilesenGrup.id == (ushort) 0)
        return;
      this.txtGrupAdi.Text = bilesenGrup.adi;
      this.txtGrupKisaAdi.Text = bilesenGrup.kisaAdi;
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      int num = (int) new TanimliDersListesi((bilesenTaban) tanim.program.sinifGetir(uint16)).ShowDialog();
      this.bilgileriYenile();
    }

    private void button5_Click(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvOgretmenler.SelectedRows[0].Cells[0].Value);
      DersProgramiOnIzlemeForm programiOnIzlemeForm = new DersProgramiOnIzlemeForm((bilesenTaban) tanim.program.ogretmenGetir(uint16));
      programiOnIzlemeForm.MdiParent = this.MdiParent;
      programiOnIzlemeForm.Show();
    }

    private void button6_Click(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvDerslikler.SelectedRows[0].Cells[0].Value);
      DersProgramiOnIzlemeForm programiOnIzlemeForm = new DersProgramiOnIzlemeForm((bilesenTaban) tanim.program.derslikGetir(uint16));
      programiOnIzlemeForm.MdiParent = this.MdiParent;
      programiOnIzlemeForm.Show();
    }

    private void button7_Click(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvSiniflar.SelectedRows[0].Cells[0].Value);
      DersProgramiOnIzlemeForm programiOnIzlemeForm = new DersProgramiOnIzlemeForm((bilesenTaban) tanim.program.sinifGetir(uint16));
      programiOnIzlemeForm.MdiParent = this.MdiParent;
      programiOnIzlemeForm.Show();
    }

    private void sinifYukari(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0 || this.dgvSiniflar.SelectedRows[0].Index <= 0)
        return;
      int index1 = this.dgvSiniflar.SelectedRows[0].Index;
      bilesenSinif bilesenSinif = tanim.program.siniflar[index1];
      tanim.program.siniflar.Remove(bilesenSinif);
      int index2;
      tanim.program.siniflar.Insert(index2 = index1 - 1, bilesenSinif);
      this.bilgileriYenile();
      this.dgvSiniflar.Rows[index2].Selected = true;
    }

    private void sinifAsagi(object sender, EventArgs e)
    {
      if (this.dgvSiniflar.SelectedRows.Count <= 0 || this.dgvSiniflar.SelectedRows[0].Index >= this.dgvSiniflar.Rows.Count - 1)
        return;
      int index1 = this.dgvSiniflar.SelectedRows[0].Index;
      bilesenSinif bilesenSinif = tanim.program.siniflar[index1];
      tanim.program.siniflar.Remove(bilesenSinif);
      int index2;
      tanim.program.siniflar.Insert(index2 = index1 + 1, bilesenSinif);
      this.bilgileriYenile();
      this.dgvSiniflar.Rows[index2].Selected = true;
    }

    private void derslikYukari(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0 || this.dgvDerslikler.SelectedRows[0].Index <= 0)
        return;
      int index1 = this.dgvDerslikler.SelectedRows[0].Index;
      bilesenDerslik bilesenDerslik = tanim.program.derslikler[index1];
      tanim.program.derslikler.Remove(bilesenDerslik);
      int index2;
      tanim.program.derslikler.Insert(index2 = index1 - 1, bilesenDerslik);
      this.bilgileriYenile();
      this.dgvDerslikler.Rows[index2].Selected = true;
    }

    private void derslikAsagi(object sender, EventArgs e)
    {
      if (this.dgvDerslikler.SelectedRows.Count <= 0 || this.dgvDerslikler.SelectedRows[0].Index >= this.dgvDerslikler.Rows.Count - 1)
        return;
      int index1 = this.dgvDerslikler.SelectedRows[0].Index;
      bilesenDerslik bilesenDerslik = tanim.program.derslikler[index1];
      tanim.program.derslikler.Remove(bilesenDerslik);
      int index2;
      tanim.program.derslikler.Insert(index2 = index1 + 1, bilesenDerslik);
      this.bilgileriYenile();
      this.dgvDerslikler.Rows[index2].Selected = true;
    }

    private void ogretmenYukari(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0 || this.dgvOgretmenler.SelectedRows[0].Index <= 0)
        return;
      int index1 = this.dgvOgretmenler.SelectedRows[0].Index;
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenler[index1];
      tanim.program.ogretmenler.Remove(bilesenOgretmen);
      int index2;
      tanim.program.ogretmenler.Insert(index2 = index1 - 1, bilesenOgretmen);
      this.bilgileriYenile();
      this.dgvOgretmenler.Rows[index2].Selected = true;
    }

    private void ogretmenAsagi(object sender, EventArgs e)
    {
      if (this.dgvOgretmenler.SelectedRows.Count <= 0 || this.dgvOgretmenler.SelectedRows[0].Index >= this.dgvOgretmenler.Rows.Count - 1)
        return;
      int index1 = this.dgvOgretmenler.SelectedRows[0].Index;
      bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenler[index1];
      tanim.program.ogretmenler.Remove(bilesenOgretmen);
      int index2;
      tanim.program.ogretmenler.Insert(index2 = index1 + 1, bilesenOgretmen);
      this.bilgileriYenile();
      this.dgvOgretmenler.Rows[index2].Selected = true;
    }

    private void dersYukari(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0 || this.dgvDersler.SelectedRows[0].Index <= 0)
        return;
      int index1 = this.dgvDersler.SelectedRows[0].Index;
      bilesenDers bilesenDers = tanim.program.dersler[index1];
      tanim.program.dersler.Remove(bilesenDers);
      int index2;
      tanim.program.dersler.Insert(index2 = index1 - 1, bilesenDers);
      this.bilgileriYenile();
      this.dgvDersler.Rows[index2].Selected = true;
    }

    private void dersAsagi(object sender, EventArgs e)
    {
      if (this.dgvDersler.SelectedRows.Count <= 0 || this.dgvDersler.SelectedRows[0].Index >= this.dgvDersler.Rows.Count - 1)
        return;
      int index1 = this.dgvDersler.SelectedRows[0].Index;
      bilesenDers bilesenDers = tanim.program.dersler[index1];
      tanim.program.dersler.Remove(bilesenDers);
      int index2;
      tanim.program.dersler.Insert(index2 = index1 + 1, bilesenDers);
      this.bilgileriYenile();
      this.dgvDersler.Rows[index2].Selected = true;
    }

    private void ogretmenAlfabetikSirala(object sender, EventArgs e)
    {
      List<bilesenOgretmen> list = tanim.program.ogretmenler.OrderBy<bilesenOgretmen, string>((Func<bilesenOgretmen, string>) (o => o.adi)).ToList<bilesenOgretmen>();
      tanim.program.ogretmenler = list;
      this.bilgileriYenile();
    }

    private void derslikAlfabetikSirala_Click(object sender, EventArgs e)
    {
      List<bilesenDerslik> list = tanim.program.derslikler.OrderBy<bilesenDerslik, string>((Func<bilesenDerslik, string>) (o => o.adi)).ToList<bilesenDerslik>();
      tanim.program.derslikler = list;
      this.bilgileriYenile();
    }

    private void sinifAlfabetikSirala_Click(object sender, EventArgs e)
    {
      List<bilesenSinif> list = tanim.program.siniflar.OrderBy<bilesenSinif, string>((Func<bilesenSinif, string>) (o => o.adi)).ToList<bilesenSinif>();
      tanim.program.siniflar = list;
      this.bilgileriYenile();
    }

    private void derslerAlfabetikSirala_Click(object sender, EventArgs e)
    {
      List<bilesenDers> list = tanim.program.dersler.OrderBy<bilesenDers, string>((Func<bilesenDers, string>) (o => o.adi)).ToList<bilesenDers>();
      tanim.program.dersler = list;
      this.bilgileriYenile();
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
      this.tbBilgiGirisleri = new TabControl();
      this.tpDersler = new TabPage();
      this.txtDersAdi = new TextBox();
      this.button1 = new Button();
      this.dgvDersler = new DataGridView();
      this.DersKolonID = new DataGridViewTextBoxColumn();
      this.KolonDersAdi = new DataGridViewTextBoxColumn();
      this.KolonDersKisaAd = new DataGridViewTextBoxColumn();
      this.KolonDersToplamDers = new DataGridViewTextBoxColumn();
      this.KolonDersKosul = new DataGridViewImageColumn();
      this.label1 = new Label();
      this.btnDersKosullar = new Button();
      this.btnDersEkle = new Button();
      this.txtDersKisaAdi = new TextBox();
      this.btnDersDuzelt = new Button();
      this.btnDersSil = new Button();
      this.label2 = new Label();
      this.tpOgretmenler = new TabPage();
      this.button5 = new Button();
      this.button2 = new Button();
      this.btnOgretmenKosullar = new Button();
      this.btnOgretmenSil = new Button();
      this.btnOgretmenDuzelt = new Button();
      this.btnOgretmenEkle = new Button();
      this.label3 = new Label();
      this.txtOgretmenKisaAdi = new TextBox();
      this.label4 = new Label();
      this.txtOgretmenAdi = new TextBox();
      this.dgvOgretmenler = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewImageColumn1 = new DataGridViewImageColumn();
      this.ogretmenlerAcilirMenu = new ContextMenuStrip(this.components);
      this.toolStripMenuItem3 = new ToolStripMenuItem();
      this.toolStripMenuItem4 = new ToolStripMenuItem();
      this.toolStripMenuItem5 = new ToolStripSeparator();
      this.ogretmenAlfabetikSıralaToolStripMenuItem = new ToolStripMenuItem();
      this.tpDerslikler = new TabPage();
      this.button6 = new Button();
      this.button3 = new Button();
      this.btnDerslikKosullar = new Button();
      this.btnDerslikSil = new Button();
      this.btnDerslikDuzelt = new Button();
      this.btnDerslikEkle = new Button();
      this.label5 = new Label();
      this.txtDerslikKisaAdi = new TextBox();
      this.label6 = new Label();
      this.txtDerslikAdi = new TextBox();
      this.dgvDerslikler = new DataGridView();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      this.dataGridViewImageColumn2 = new DataGridViewImageColumn();
      this.dersliklerAcilirMenu = new ContextMenuStrip(this.components);
      this.toolStripMenuItem1 = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripMenuItem();
      this.tpSiniflar = new TabPage();
      this.button7 = new Button();
      this.panel1 = new Panel();
      this.dgvSiniflar = new DataGridView();
      this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
      this.dataGridViewImageColumn3 = new DataGridViewImageColumn();
      this.siniflarAcilirMenu = new ContextMenuStrip(this.components);
      this.sinifYukariTasıKomutu = new ToolStripMenuItem();
      this.sinifAsagiTasiKomutu = new ToolStripMenuItem();
      this.button4 = new Button();
      this.groupBox1 = new GroupBox();
      this.btnGrupSil = new Button();
      this.dgvSinifGruplar = new DataGridView();
      this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
      this.label9 = new Label();
      this.btnGrupDuzelt = new Button();
      this.txtGrupAdi = new TextBox();
      this.txtGrupKisaAdi = new TextBox();
      this.btnGrupEkle = new Button();
      this.label10 = new Label();
      this.btnSinifKosullar = new Button();
      this.btnSinifSil = new Button();
      this.btnSinifDuzelt = new Button();
      this.btnSinifEkle = new Button();
      this.label7 = new Label();
      this.txtSinifKisaAdi = new TextBox();
      this.label8 = new Label();
      this.txtSinifAdi = new TextBox();
      this.toolStripMenuItem6 = new ToolStripSeparator();
      this.derslikAlfabetikSirala = new ToolStripMenuItem();
      this.toolStripMenuItem7 = new ToolStripSeparator();
      this.sinifAlfabetikSirala = new ToolStripMenuItem();
      this.derslerAcilirMenu = new ContextMenuStrip(this.components);
      this.derslerYukariTasi = new ToolStripMenuItem();
      this.derslerAsagiTasi = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.derslerAlfabetikSirala = new ToolStripMenuItem();
      this.button10 = new Button();
      this.button9 = new Button();
      this.button8 = new Button();
      this.button11 = new Button();
      this.button12 = new Button();
      this.button13 = new Button();
      this.button14 = new Button();
      this.button15 = new Button();
      this.button16 = new Button();
      this.button17 = new Button();
      this.button18 = new Button();
      this.button19 = new Button();
      this.tbBilgiGirisleri.SuspendLayout();
      this.tpDersler.SuspendLayout();
      ((ISupportInitialize) this.dgvDersler).BeginInit();
      this.tpOgretmenler.SuspendLayout();
      ((ISupportInitialize) this.dgvOgretmenler).BeginInit();
      this.ogretmenlerAcilirMenu.SuspendLayout();
      this.tpDerslikler.SuspendLayout();
      ((ISupportInitialize) this.dgvDerslikler).BeginInit();
      this.dersliklerAcilirMenu.SuspendLayout();
      this.tpSiniflar.SuspendLayout();
      this.panel1.SuspendLayout();
      ((ISupportInitialize) this.dgvSiniflar).BeginInit();
      this.siniflarAcilirMenu.SuspendLayout();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.dgvSinifGruplar).BeginInit();
      this.derslerAcilirMenu.SuspendLayout();
      this.SuspendLayout();
      this.tbBilgiGirisleri.Controls.Add((Control) this.tpDersler);
      this.tbBilgiGirisleri.Controls.Add((Control) this.tpOgretmenler);
      this.tbBilgiGirisleri.Controls.Add((Control) this.tpDerslikler);
      this.tbBilgiGirisleri.Controls.Add((Control) this.tpSiniflar);
      this.tbBilgiGirisleri.Dock = DockStyle.Fill;
      this.tbBilgiGirisleri.Location = new Point(0, 0);
      this.tbBilgiGirisleri.Name = "tbBilgiGirisleri";
      this.tbBilgiGirisleri.SelectedIndex = 0;
      this.tbBilgiGirisleri.Size = new Size(735, 534);
      this.tbBilgiGirisleri.TabIndex = 0;
      this.tbBilgiGirisleri.TabStop = false;
      this.tpDersler.Controls.Add((Control) this.button10);
      this.tpDersler.Controls.Add((Control) this.button9);
      this.tpDersler.Controls.Add((Control) this.button8);
      this.tpDersler.Controls.Add((Control) this.txtDersAdi);
      this.tpDersler.Controls.Add((Control) this.button1);
      this.tpDersler.Controls.Add((Control) this.dgvDersler);
      this.tpDersler.Controls.Add((Control) this.label1);
      this.tpDersler.Controls.Add((Control) this.btnDersKosullar);
      this.tpDersler.Controls.Add((Control) this.btnDersEkle);
      this.tpDersler.Controls.Add((Control) this.txtDersKisaAdi);
      this.tpDersler.Controls.Add((Control) this.btnDersDuzelt);
      this.tpDersler.Controls.Add((Control) this.btnDersSil);
      this.tpDersler.Controls.Add((Control) this.label2);
      this.tpDersler.Location = new Point(4, 22);
      this.tpDersler.Name = "tpDersler";
      this.tpDersler.Padding = new Padding(3);
      this.tpDersler.Size = new Size(727, 508);
      this.tpDersler.TabIndex = 2;
      this.tpDersler.Text = "Dersler";
      this.tpDersler.UseVisualStyleBackColor = true;
      this.txtDersAdi.Location = new Point(534, 13);
      this.txtDersAdi.Name = "txtDersAdi";
      this.txtDersAdi.Size = new Size(173, 20);
      this.txtDersAdi.TabIndex = 0;
      this.button1.Location = new Point(534, 340);
      this.button1.Name = "button1";
      this.button1.Size = new Size(173, 35);
      this.button1.TabIndex = 6;
      this.button1.TabStop = false;
      this.button1.Text = "Atanmış Dersler";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.dgvDersler.AllowUserToAddRows = false;
      this.dgvDersler.AllowUserToDeleteRows = false;
      this.dgvDersler.AllowUserToResizeColumns = false;
      this.dgvDersler.AllowUserToResizeRows = false;
      this.dgvDersler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDersler.Columns.AddRange((DataGridViewColumn) this.DersKolonID, (DataGridViewColumn) this.KolonDersAdi, (DataGridViewColumn) this.KolonDersKisaAd, (DataGridViewColumn) this.KolonDersToplamDers, (DataGridViewColumn) this.KolonDersKosul);
      this.dgvDersler.ContextMenuStrip = this.derslerAcilirMenu;
      this.dgvDersler.Dock = DockStyle.Left;
      this.dgvDersler.Location = new Point(3, 3);
      this.dgvDersler.MultiSelect = false;
      this.dgvDersler.Name = "dgvDersler";
      this.dgvDersler.ReadOnly = true;
      this.dgvDersler.RowHeadersVisible = false;
      this.dgvDersler.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvDersler.RowTemplate.Height = 45;
      this.dgvDersler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvDersler.Size = new Size(457, 502);
      this.dgvDersler.TabIndex = 0;
      this.dgvDersler.TabStop = false;
      this.dgvDersler.SelectionChanged += new EventHandler(this.dgvDersler_SelectionChanged);
      this.DersKolonID.DataPropertyName = "id";
      this.DersKolonID.HeaderText = "id";
      this.DersKolonID.Name = "DersKolonID";
      this.DersKolonID.ReadOnly = true;
      this.DersKolonID.Visible = false;
      this.KolonDersAdi.DataPropertyName = "adi";
      this.KolonDersAdi.HeaderText = "Adı";
      this.KolonDersAdi.Name = "KolonDersAdi";
      this.KolonDersAdi.ReadOnly = true;
      this.KolonDersAdi.Width = 180;
      this.KolonDersKisaAd.DataPropertyName = "kisaadi";
      this.KolonDersKisaAd.HeaderText = "Kısa Adı";
      this.KolonDersKisaAd.Name = "KolonDersKisaAd";
      this.KolonDersKisaAd.ReadOnly = true;
      this.KolonDersKisaAd.Width = 70;
      this.KolonDersToplamDers.DataPropertyName = "derssayisi";
      this.KolonDersToplamDers.HeaderText = "Toplam Saat";
      this.KolonDersToplamDers.Name = "KolonDersToplamDers";
      this.KolonDersToplamDers.ReadOnly = true;
      this.KolonDersToplamDers.Width = 60;
      this.KolonDersKosul.DataPropertyName = "kosul";
      this.KolonDersKosul.HeaderText = "Koşul";
      this.KolonDersKosul.Name = "KolonDersKosul";
      this.KolonDersKosul.ReadOnly = true;
      this.KolonDersKosul.Width = 110;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(471, 16);
      this.label1.Name = "label1";
      this.label1.Size = new Size(22, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Adı";
      this.btnDersKosullar.Location = new Point(534, 242);
      this.btnDersKosullar.Name = "btnDersKosullar";
      this.btnDersKosullar.Size = new Size(173, 35);
      this.btnDersKosullar.TabIndex = 5;
      this.btnDersKosullar.TabStop = false;
      this.btnDersKosullar.Text = "Koşullar";
      this.btnDersKosullar.UseVisualStyleBackColor = true;
      this.btnDersKosullar.Click += new EventHandler(this.btnDersKosullar_Click);
      this.btnDersEkle.Location = new Point(534, 86);
      this.btnDersEkle.Name = "btnDersEkle";
      this.btnDersEkle.Size = new Size(173, 35);
      this.btnDersEkle.TabIndex = 2;
      this.btnDersEkle.Text = "Ekle";
      this.btnDersEkle.UseVisualStyleBackColor = true;
      this.btnDersEkle.Click += new EventHandler(this.btnDersEkle_Click);
      this.txtDersKisaAdi.Location = new Point(534, 39);
      this.txtDersKisaAdi.MaxLength = 5;
      this.txtDersKisaAdi.Name = "txtDersKisaAdi";
      this.txtDersKisaAdi.Size = new Size(100, 20);
      this.txtDersKisaAdi.TabIndex = 1;
      this.btnDersDuzelt.Location = new Point(534, 160);
      this.btnDersDuzelt.Name = "btnDersDuzelt";
      this.btnDersDuzelt.Size = new Size(173, 35);
      this.btnDersDuzelt.TabIndex = 3;
      this.btnDersDuzelt.TabStop = false;
      this.btnDersDuzelt.Text = "Düzelt";
      this.btnDersDuzelt.UseVisualStyleBackColor = true;
      this.btnDersDuzelt.Click += new EventHandler(this.btnDersDuzelt_Click);
      this.btnDersSil.Location = new Point(534, 201);
      this.btnDersSil.Name = "btnDersSil";
      this.btnDersSil.Size = new Size(173, 35);
      this.btnDersSil.TabIndex = 4;
      this.btnDersSil.TabStop = false;
      this.btnDersSil.Text = "Sil";
      this.btnDersSil.UseVisualStyleBackColor = true;
      this.btnDersSil.Click += new EventHandler(this.btnDersSil_Click);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(471, 42);
      this.label2.Name = "label2";
      this.label2.Size = new Size(45, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Kısa Adı";
      this.tpOgretmenler.Controls.Add((Control) this.button11);
      this.tpOgretmenler.Controls.Add((Control) this.button12);
      this.tpOgretmenler.Controls.Add((Control) this.button13);
      this.tpOgretmenler.Controls.Add((Control) this.button5);
      this.tpOgretmenler.Controls.Add((Control) this.button2);
      this.tpOgretmenler.Controls.Add((Control) this.btnOgretmenKosullar);
      this.tpOgretmenler.Controls.Add((Control) this.btnOgretmenSil);
      this.tpOgretmenler.Controls.Add((Control) this.btnOgretmenDuzelt);
      this.tpOgretmenler.Controls.Add((Control) this.btnOgretmenEkle);
      this.tpOgretmenler.Controls.Add((Control) this.label3);
      this.tpOgretmenler.Controls.Add((Control) this.txtOgretmenKisaAdi);
      this.tpOgretmenler.Controls.Add((Control) this.label4);
      this.tpOgretmenler.Controls.Add((Control) this.txtOgretmenAdi);
      this.tpOgretmenler.Controls.Add((Control) this.dgvOgretmenler);
      this.tpOgretmenler.Location = new Point(4, 22);
      this.tpOgretmenler.Name = "tpOgretmenler";
      this.tpOgretmenler.Padding = new Padding(3);
      this.tpOgretmenler.Size = new Size(727, 508);
      this.tpOgretmenler.TabIndex = 1;
      this.tpOgretmenler.Text = "Öğretmenler";
      this.tpOgretmenler.UseVisualStyleBackColor = true;
      this.button5.Location = new Point(534, 381);
      this.button5.Name = "button5";
      this.button5.Size = new Size(173, 35);
      this.button5.TabIndex = 16;
      this.button5.Text = "Ders Programı";
      this.button5.UseVisualStyleBackColor = true;
      this.button5.Click += new EventHandler(this.button5_Click);
      this.button2.Location = new Point(534, 340);
      this.button2.Name = "button2";
      this.button2.Size = new Size(173, 35);
      this.button2.TabIndex = 15;
      this.button2.TabStop = false;
      this.button2.Text = "Atanmış Dersler";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.btnOgretmenKosullar.Location = new Point(534, 242);
      this.btnOgretmenKosullar.Name = "btnOgretmenKosullar";
      this.btnOgretmenKosullar.Size = new Size(173, 35);
      this.btnOgretmenKosullar.TabIndex = 14;
      this.btnOgretmenKosullar.TabStop = false;
      this.btnOgretmenKosullar.Text = "Koşullar";
      this.btnOgretmenKosullar.UseVisualStyleBackColor = true;
      this.btnOgretmenKosullar.Click += new EventHandler(this.btnOgretmenKosullar_Click);
      this.btnOgretmenSil.Location = new Point(534, 201);
      this.btnOgretmenSil.Name = "btnOgretmenSil";
      this.btnOgretmenSil.Size = new Size(173, 35);
      this.btnOgretmenSil.TabIndex = 12;
      this.btnOgretmenSil.TabStop = false;
      this.btnOgretmenSil.Text = "Sil";
      this.btnOgretmenSil.UseVisualStyleBackColor = true;
      this.btnOgretmenSil.Click += new EventHandler(this.btnOgretmenSil_Click);
      this.btnOgretmenDuzelt.Location = new Point(534, 160);
      this.btnOgretmenDuzelt.Name = "btnOgretmenDuzelt";
      this.btnOgretmenDuzelt.Size = new Size(173, 35);
      this.btnOgretmenDuzelt.TabIndex = 11;
      this.btnOgretmenDuzelt.TabStop = false;
      this.btnOgretmenDuzelt.Text = "Düzelt";
      this.btnOgretmenDuzelt.UseVisualStyleBackColor = true;
      this.btnOgretmenDuzelt.Click += new EventHandler(this.btnOgretmenDuzelt_Click);
      this.btnOgretmenEkle.Location = new Point(534, 86);
      this.btnOgretmenEkle.Name = "btnOgretmenEkle";
      this.btnOgretmenEkle.Size = new Size(173, 35);
      this.btnOgretmenEkle.TabIndex = 9;
      this.btnOgretmenEkle.Text = "Ekle";
      this.btnOgretmenEkle.UseVisualStyleBackColor = true;
      this.btnOgretmenEkle.Click += new EventHandler(this.btnOgretmenEkle_Click);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(471, 42);
      this.label3.Name = "label3";
      this.label3.Size = new Size(45, 13);
      this.label3.TabIndex = 13;
      this.label3.Text = "Kısa Adı";
      this.txtOgretmenKisaAdi.Location = new Point(534, 39);
      this.txtOgretmenKisaAdi.MaxLength = 5;
      this.txtOgretmenKisaAdi.Name = "txtOgretmenKisaAdi";
      this.txtOgretmenKisaAdi.Size = new Size(100, 20);
      this.txtOgretmenKisaAdi.TabIndex = 8;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(471, 16);
      this.label4.Name = "label4";
      this.label4.Size = new Size(57, 13);
      this.label4.TabIndex = 10;
      this.label4.Text = "Adı Soyadı";
      this.txtOgretmenAdi.Location = new Point(534, 13);
      this.txtOgretmenAdi.Name = "txtOgretmenAdi";
      this.txtOgretmenAdi.Size = new Size(173, 20);
      this.txtOgretmenAdi.TabIndex = 6;
      this.dgvOgretmenler.AllowUserToAddRows = false;
      this.dgvOgretmenler.AllowUserToDeleteRows = false;
      this.dgvOgretmenler.AllowUserToResizeColumns = false;
      this.dgvOgretmenler.AllowUserToResizeRows = false;
      this.dgvOgretmenler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvOgretmenler.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.dataGridViewTextBoxColumn3, (DataGridViewColumn) this.dataGridViewTextBoxColumn4, (DataGridViewColumn) this.dataGridViewImageColumn1);
      this.dgvOgretmenler.ContextMenuStrip = this.ogretmenlerAcilirMenu;
      this.dgvOgretmenler.Dock = DockStyle.Left;
      this.dgvOgretmenler.Location = new Point(3, 3);
      this.dgvOgretmenler.MultiSelect = false;
      this.dgvOgretmenler.Name = "dgvOgretmenler";
      this.dgvOgretmenler.ReadOnly = true;
      this.dgvOgretmenler.RowHeadersVisible = false;
      this.dgvOgretmenler.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvOgretmenler.RowTemplate.Height = 45;
      this.dgvOgretmenler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvOgretmenler.Size = new Size(457, 502);
      this.dgvOgretmenler.TabIndex = 7;
      this.dgvOgretmenler.TabStop = false;
      this.dgvOgretmenler.SelectionChanged += new EventHandler(this.dgvOgretmenler_SelectionChanged);
      this.dataGridViewTextBoxColumn1.DataPropertyName = "id";
      this.dataGridViewTextBoxColumn1.HeaderText = "id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Visible = false;
      this.dataGridViewTextBoxColumn2.DataPropertyName = "adisoyadi";
      this.dataGridViewTextBoxColumn2.HeaderText = "Adı Soyadı";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 180;
      this.dataGridViewTextBoxColumn3.DataPropertyName = "kisaadi";
      this.dataGridViewTextBoxColumn3.HeaderText = "Kısa Adı";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 70;
      this.dataGridViewTextBoxColumn4.DataPropertyName = "derssayisi";
      this.dataGridViewTextBoxColumn4.HeaderText = "Toplam Saat";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.Width = 60;
      this.dataGridViewImageColumn1.DataPropertyName = "kosul";
      this.dataGridViewImageColumn1.HeaderText = "Koşul";
      this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
      this.dataGridViewImageColumn1.ReadOnly = true;
      this.dataGridViewImageColumn1.Width = 110;
      this.ogretmenlerAcilirMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.toolStripMenuItem3,
        (ToolStripItem) this.toolStripMenuItem4,
        (ToolStripItem) this.toolStripMenuItem5,
        (ToolStripItem) this.ogretmenAlfabetikSıralaToolStripMenuItem
      });
      this.ogretmenlerAcilirMenu.Name = "acilirMenu1";
      this.ogretmenlerAcilirMenu.Size = new Size(153, 76);
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new Size(152, 22);
      this.toolStripMenuItem3.Text = "Yukarı Taşı";
      this.toolStripMenuItem3.Click += new EventHandler(this.ogretmenYukari);
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new Size(152, 22);
      this.toolStripMenuItem4.Text = "Aşağı Taşı";
      this.toolStripMenuItem4.Click += new EventHandler(this.ogretmenAsagi);
      this.toolStripMenuItem5.Name = "toolStripMenuItem5";
      this.toolStripMenuItem5.Size = new Size(149, 6);
      this.ogretmenAlfabetikSıralaToolStripMenuItem.Name = "ogretmenAlfabetikSıralaToolStripMenuItem";
      this.ogretmenAlfabetikSıralaToolStripMenuItem.Size = new Size(152, 22);
      this.ogretmenAlfabetikSıralaToolStripMenuItem.Text = "Alfabetik Sırala";
      this.ogretmenAlfabetikSıralaToolStripMenuItem.Click += new EventHandler(this.ogretmenAlfabetikSirala);
      this.tpDerslikler.Controls.Add((Control) this.button14);
      this.tpDerslikler.Controls.Add((Control) this.button15);
      this.tpDerslikler.Controls.Add((Control) this.button16);
      this.tpDerslikler.Controls.Add((Control) this.button6);
      this.tpDerslikler.Controls.Add((Control) this.button3);
      this.tpDerslikler.Controls.Add((Control) this.btnDerslikKosullar);
      this.tpDerslikler.Controls.Add((Control) this.btnDerslikSil);
      this.tpDerslikler.Controls.Add((Control) this.btnDerslikDuzelt);
      this.tpDerslikler.Controls.Add((Control) this.btnDerslikEkle);
      this.tpDerslikler.Controls.Add((Control) this.label5);
      this.tpDerslikler.Controls.Add((Control) this.txtDerslikKisaAdi);
      this.tpDerslikler.Controls.Add((Control) this.label6);
      this.tpDerslikler.Controls.Add((Control) this.txtDerslikAdi);
      this.tpDerslikler.Controls.Add((Control) this.dgvDerslikler);
      this.tpDerslikler.Location = new Point(4, 22);
      this.tpDerslikler.Name = "tpDerslikler";
      this.tpDerslikler.Padding = new Padding(3);
      this.tpDerslikler.Size = new Size(727, 508);
      this.tpDerslikler.TabIndex = 3;
      this.tpDerslikler.Text = "Derslikler";
      this.tpDerslikler.UseVisualStyleBackColor = true;
      this.button6.Location = new Point(534, 381);
      this.button6.Name = "button6";
      this.button6.Size = new Size(173, 35);
      this.button6.TabIndex = 25;
      this.button6.Text = "Ders Programı";
      this.button6.UseVisualStyleBackColor = true;
      this.button6.Click += new EventHandler(this.button6_Click);
      this.button3.Location = new Point(534, 340);
      this.button3.Name = "button3";
      this.button3.Size = new Size(173, 35);
      this.button3.TabIndex = 24;
      this.button3.TabStop = false;
      this.button3.Text = "Atanmış Dersler";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click_1);
      this.btnDerslikKosullar.Location = new Point(534, 242);
      this.btnDerslikKosullar.Name = "btnDerslikKosullar";
      this.btnDerslikKosullar.Size = new Size(173, 35);
      this.btnDerslikKosullar.TabIndex = 23;
      this.btnDerslikKosullar.TabStop = false;
      this.btnDerslikKosullar.Text = "Koşullar";
      this.btnDerslikKosullar.UseVisualStyleBackColor = true;
      this.btnDerslikKosullar.Click += new EventHandler(this.btnDerslikKosullar_Click);
      this.btnDerslikSil.Location = new Point(534, 201);
      this.btnDerslikSil.Name = "btnDerslikSil";
      this.btnDerslikSil.Size = new Size(173, 35);
      this.btnDerslikSil.TabIndex = 21;
      this.btnDerslikSil.TabStop = false;
      this.btnDerslikSil.Text = "Sil";
      this.btnDerslikSil.UseVisualStyleBackColor = true;
      this.btnDerslikSil.Click += new EventHandler(this.btnDerslikSil_Click);
      this.btnDerslikDuzelt.Location = new Point(534, 160);
      this.btnDerslikDuzelt.Name = "btnDerslikDuzelt";
      this.btnDerslikDuzelt.Size = new Size(173, 35);
      this.btnDerslikDuzelt.TabIndex = 20;
      this.btnDerslikDuzelt.TabStop = false;
      this.btnDerslikDuzelt.Text = "Düzelt";
      this.btnDerslikDuzelt.UseVisualStyleBackColor = true;
      this.btnDerslikDuzelt.Click += new EventHandler(this.btnDerslikDuzelt_Click);
      this.btnDerslikEkle.Location = new Point(534, 86);
      this.btnDerslikEkle.Name = "btnDerslikEkle";
      this.btnDerslikEkle.Size = new Size(173, 35);
      this.btnDerslikEkle.TabIndex = 18;
      this.btnDerslikEkle.Text = "Ekle";
      this.btnDerslikEkle.UseVisualStyleBackColor = true;
      this.btnDerslikEkle.Click += new EventHandler(this.btnDerslikEkle_Click);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(471, 42);
      this.label5.Name = "label5";
      this.label5.Size = new Size(45, 13);
      this.label5.TabIndex = 22;
      this.label5.Text = "Kısa Adı";
      this.txtDerslikKisaAdi.Location = new Point(534, 39);
      this.txtDerslikKisaAdi.MaxLength = 5;
      this.txtDerslikKisaAdi.Name = "txtDerslikKisaAdi";
      this.txtDerslikKisaAdi.Size = new Size(100, 20);
      this.txtDerslikKisaAdi.TabIndex = 17;
      this.label6.AutoSize = true;
      this.label6.Location = new Point(471, 16);
      this.label6.Name = "label6";
      this.label6.Size = new Size(57, 13);
      this.label6.TabIndex = 19;
      this.label6.Text = "Derslik Adı";
      this.txtDerslikAdi.Location = new Point(534, 13);
      this.txtDerslikAdi.Name = "txtDerslikAdi";
      this.txtDerslikAdi.Size = new Size(173, 20);
      this.txtDerslikAdi.TabIndex = 15;
      this.dgvDerslikler.AllowUserToAddRows = false;
      this.dgvDerslikler.AllowUserToDeleteRows = false;
      this.dgvDerslikler.AllowUserToResizeColumns = false;
      this.dgvDerslikler.AllowUserToResizeRows = false;
      this.dgvDerslikler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvDerslikler.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn5, (DataGridViewColumn) this.dataGridViewTextBoxColumn6, (DataGridViewColumn) this.dataGridViewTextBoxColumn7, (DataGridViewColumn) this.dataGridViewTextBoxColumn8, (DataGridViewColumn) this.dataGridViewImageColumn2);
      this.dgvDerslikler.ContextMenuStrip = this.dersliklerAcilirMenu;
      this.dgvDerslikler.Dock = DockStyle.Left;
      this.dgvDerslikler.Location = new Point(3, 3);
      this.dgvDerslikler.MultiSelect = false;
      this.dgvDerslikler.Name = "dgvDerslikler";
      this.dgvDerslikler.ReadOnly = true;
      this.dgvDerslikler.RowHeadersVisible = false;
      this.dgvDerslikler.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvDerslikler.RowTemplate.Height = 45;
      this.dgvDerslikler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvDerslikler.Size = new Size(457, 502);
      this.dgvDerslikler.TabIndex = 16;
      this.dgvDerslikler.TabStop = false;
      this.dgvDerslikler.SelectionChanged += new EventHandler(this.dgvDerslikler_SelectionChanged);
      this.dataGridViewTextBoxColumn5.DataPropertyName = "id";
      this.dataGridViewTextBoxColumn5.HeaderText = "id";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.Visible = false;
      this.dataGridViewTextBoxColumn6.DataPropertyName = "adi";
      this.dataGridViewTextBoxColumn6.HeaderText = "Derslik Adı";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn6.ReadOnly = true;
      this.dataGridViewTextBoxColumn6.Width = 180;
      this.dataGridViewTextBoxColumn7.DataPropertyName = "kisaadi";
      this.dataGridViewTextBoxColumn7.HeaderText = "Kısa Adı";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn7.ReadOnly = true;
      this.dataGridViewTextBoxColumn7.Width = 70;
      this.dataGridViewTextBoxColumn8.DataPropertyName = "derssayisi";
      this.dataGridViewTextBoxColumn8.HeaderText = "Toplam Saat";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn8.ReadOnly = true;
      this.dataGridViewTextBoxColumn8.Width = 60;
      this.dataGridViewImageColumn2.DataPropertyName = "kosul";
      this.dataGridViewImageColumn2.HeaderText = "Koşul";
      this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
      this.dataGridViewImageColumn2.ReadOnly = true;
      this.dataGridViewImageColumn2.Width = 110;
      this.dersliklerAcilirMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.toolStripMenuItem6,
        (ToolStripItem) this.derslikAlfabetikSirala
      });
      this.dersliklerAcilirMenu.Name = "acilirMenu1";
      this.dersliklerAcilirMenu.Size = new Size(153, 76);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(152, 22);
      this.toolStripMenuItem1.Text = "Yukarı Taşı";
      this.toolStripMenuItem1.Click += new EventHandler(this.derslikYukari);
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(152, 22);
      this.toolStripMenuItem2.Text = "Aşağı Taşı";
      this.toolStripMenuItem2.Click += new EventHandler(this.derslikAsagi);
      this.tpSiniflar.Controls.Add((Control) this.button17);
      this.tpSiniflar.Controls.Add((Control) this.button18);
      this.tpSiniflar.Controls.Add((Control) this.button19);
      this.tpSiniflar.Controls.Add((Control) this.button7);
      this.tpSiniflar.Controls.Add((Control) this.panel1);
      this.tpSiniflar.Controls.Add((Control) this.button4);
      this.tpSiniflar.Controls.Add((Control) this.groupBox1);
      this.tpSiniflar.Controls.Add((Control) this.btnSinifKosullar);
      this.tpSiniflar.Controls.Add((Control) this.btnSinifSil);
      this.tpSiniflar.Controls.Add((Control) this.btnSinifDuzelt);
      this.tpSiniflar.Controls.Add((Control) this.btnSinifEkle);
      this.tpSiniflar.Controls.Add((Control) this.label7);
      this.tpSiniflar.Controls.Add((Control) this.txtSinifKisaAdi);
      this.tpSiniflar.Controls.Add((Control) this.label8);
      this.tpSiniflar.Controls.Add((Control) this.txtSinifAdi);
      this.tpSiniflar.Location = new Point(4, 22);
      this.tpSiniflar.Name = "tpSiniflar";
      this.tpSiniflar.Padding = new Padding(3);
      this.tpSiniflar.Size = new Size(727, 508);
      this.tpSiniflar.TabIndex = 4;
      this.tpSiniflar.Text = "Sınıflar";
      this.tpSiniflar.UseVisualStyleBackColor = true;
      this.button7.Location = new Point(541, 272);
      this.button7.Name = "button7";
      this.button7.Size = new Size(170, 35);
      this.button7.TabIndex = 41;
      this.button7.Text = "Ders Programı";
      this.button7.UseVisualStyleBackColor = true;
      this.button7.Click += new EventHandler(this.button7_Click);
      this.panel1.Controls.Add((Control) this.dgvSiniflar);
      this.panel1.Dock = DockStyle.Left;
      this.panel1.Location = new Point(3, 3);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(461, 311);
      this.panel1.TabIndex = 40;
      this.dgvSiniflar.AllowUserToAddRows = false;
      this.dgvSiniflar.AllowUserToDeleteRows = false;
      this.dgvSiniflar.AllowUserToResizeColumns = false;
      this.dgvSiniflar.AllowUserToResizeRows = false;
      this.dgvSiniflar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSiniflar.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn9, (DataGridViewColumn) this.dataGridViewTextBoxColumn10, (DataGridViewColumn) this.dataGridViewTextBoxColumn11, (DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewImageColumn3);
      this.dgvSiniflar.ContextMenuStrip = this.siniflarAcilirMenu;
      this.dgvSiniflar.Dock = DockStyle.Fill;
      this.dgvSiniflar.Location = new Point(0, 0);
      this.dgvSiniflar.MultiSelect = false;
      this.dgvSiniflar.Name = "dgvSiniflar";
      this.dgvSiniflar.ReadOnly = true;
      this.dgvSiniflar.RowHeadersVisible = false;
      this.dgvSiniflar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvSiniflar.RowTemplate.Height = 45;
      this.dgvSiniflar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvSiniflar.Size = new Size(461, 311);
      this.dgvSiniflar.TabIndex = 25;
      this.dgvSiniflar.TabStop = false;
      this.dgvSiniflar.SelectionChanged += new EventHandler(this.dgvSiniflar_SelectionChanged);
      this.dataGridViewTextBoxColumn9.DataPropertyName = "id";
      this.dataGridViewTextBoxColumn9.HeaderText = "id";
      this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      this.dataGridViewTextBoxColumn9.ReadOnly = true;
      this.dataGridViewTextBoxColumn9.Visible = false;
      this.dataGridViewTextBoxColumn10.DataPropertyName = "adi";
      this.dataGridViewTextBoxColumn10.HeaderText = "Sınıf Adı";
      this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
      this.dataGridViewTextBoxColumn10.ReadOnly = true;
      this.dataGridViewTextBoxColumn10.Width = 180;
      this.dataGridViewTextBoxColumn11.DataPropertyName = "kisaadi";
      this.dataGridViewTextBoxColumn11.HeaderText = "Kısa Adı";
      this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
      this.dataGridViewTextBoxColumn11.ReadOnly = true;
      this.dataGridViewTextBoxColumn11.Width = 70;
      this.dataGridViewTextBoxColumn12.DataPropertyName = "derssayisi";
      this.dataGridViewTextBoxColumn12.HeaderText = "Toplam Saat";
      this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
      this.dataGridViewTextBoxColumn12.ReadOnly = true;
      this.dataGridViewTextBoxColumn12.Width = 60;
      this.dataGridViewImageColumn3.DataPropertyName = "kosul";
      this.dataGridViewImageColumn3.HeaderText = "Koşul";
      this.dataGridViewImageColumn3.Name = "dataGridViewImageColumn3";
      this.dataGridViewImageColumn3.ReadOnly = true;
      this.dataGridViewImageColumn3.Width = 110;
      this.siniflarAcilirMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.sinifYukariTasıKomutu,
        (ToolStripItem) this.sinifAsagiTasiKomutu,
        (ToolStripItem) this.toolStripMenuItem7,
        (ToolStripItem) this.sinifAlfabetikSirala
      });
      this.siniflarAcilirMenu.Name = "acilirMenu1";
      this.siniflarAcilirMenu.Size = new Size(153, 76);
      this.sinifYukariTasıKomutu.Name = "sinifYukariTasıKomutu";
      this.sinifYukariTasıKomutu.Size = new Size(152, 22);
      this.sinifYukariTasıKomutu.Text = "Yukarı Taşı";
      this.sinifYukariTasıKomutu.Click += new EventHandler(this.sinifYukari);
      this.sinifAsagiTasiKomutu.Name = "sinifAsagiTasiKomutu";
      this.sinifAsagiTasiKomutu.Size = new Size(152, 22);
      this.sinifAsagiTasiKomutu.Text = "Aşağı Taşı";
      this.sinifAsagiTasiKomutu.Click += new EventHandler(this.sinifAsagi);
      this.button4.Location = new Point(541, 231);
      this.button4.Name = "button4";
      this.button4.Size = new Size(170, 35);
      this.button4.TabIndex = 39;
      this.button4.TabStop = false;
      this.button4.Text = "Atanmış Dersler";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.groupBox1.Controls.Add((Control) this.btnGrupSil);
      this.groupBox1.Controls.Add((Control) this.dgvSinifGruplar);
      this.groupBox1.Controls.Add((Control) this.label9);
      this.groupBox1.Controls.Add((Control) this.btnGrupDuzelt);
      this.groupBox1.Controls.Add((Control) this.txtGrupAdi);
      this.groupBox1.Controls.Add((Control) this.txtGrupKisaAdi);
      this.groupBox1.Controls.Add((Control) this.btnGrupEkle);
      this.groupBox1.Controls.Add((Control) this.label10);
      this.groupBox1.Dock = DockStyle.Bottom;
      this.groupBox1.Location = new Point(3, 314);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(721, 191);
      this.groupBox1.TabIndex = 38;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Gruplar";
      this.btnGrupSil.Location = new Point(341, 147);
      this.btnGrupSil.Name = "btnGrupSil";
      this.btnGrupSil.Size = new Size(212, 35);
      this.btnGrupSil.TabIndex = 40;
      this.btnGrupSil.TabStop = false;
      this.btnGrupSil.Text = "Sil";
      this.btnGrupSil.UseVisualStyleBackColor = true;
      this.btnGrupSil.Click += new EventHandler(this.btnGrupSil_Click);
      this.dgvSinifGruplar.AllowUserToAddRows = false;
      this.dgvSinifGruplar.AllowUserToDeleteRows = false;
      this.dgvSinifGruplar.AllowUserToResizeColumns = false;
      this.dgvSinifGruplar.AllowUserToResizeRows = false;
      this.dgvSinifGruplar.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvSinifGruplar.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn13, (DataGridViewColumn) this.dataGridViewTextBoxColumn14, (DataGridViewColumn) this.dataGridViewTextBoxColumn15, (DataGridViewColumn) this.dataGridViewTextBoxColumn16);
      this.dgvSinifGruplar.Location = new Point(6, 19);
      this.dgvSinifGruplar.MultiSelect = false;
      this.dgvSinifGruplar.Name = "dgvSinifGruplar";
      this.dgvSinifGruplar.ReadOnly = true;
      this.dgvSinifGruplar.RowHeadersVisible = false;
      this.dgvSinifGruplar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvSinifGruplar.RowTemplate.Height = 30;
      this.dgvSinifGruplar.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvSinifGruplar.Size = new Size(324, 163);
      this.dgvSinifGruplar.TabIndex = 33;
      this.dgvSinifGruplar.TabStop = false;
      this.dgvSinifGruplar.SelectionChanged += new EventHandler(this.dgvSinifGruplar_SelectionChanged);
      this.dataGridViewTextBoxColumn13.DataPropertyName = "id";
      this.dataGridViewTextBoxColumn13.HeaderText = "id";
      this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
      this.dataGridViewTextBoxColumn13.ReadOnly = true;
      this.dataGridViewTextBoxColumn13.Visible = false;
      this.dataGridViewTextBoxColumn14.DataPropertyName = "adi";
      this.dataGridViewTextBoxColumn14.HeaderText = "Grup Adı";
      this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
      this.dataGridViewTextBoxColumn14.ReadOnly = true;
      this.dataGridViewTextBoxColumn14.Width = 140;
      this.dataGridViewTextBoxColumn15.DataPropertyName = "kisaadi";
      this.dataGridViewTextBoxColumn15.HeaderText = "Kısa Adı";
      this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
      this.dataGridViewTextBoxColumn15.ReadOnly = true;
      this.dataGridViewTextBoxColumn15.Width = 70;
      this.dataGridViewTextBoxColumn16.DataPropertyName = "derssayisi";
      this.dataGridViewTextBoxColumn16.HeaderText = "Toplam Saat";
      this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
      this.dataGridViewTextBoxColumn16.ReadOnly = true;
      this.dataGridViewTextBoxColumn16.Width = 60;
      this.label9.AutoSize = true;
      this.label9.Location = new Point(338, 48);
      this.label9.Name = "label9";
      this.label9.Size = new Size(45, 13);
      this.label9.TabIndex = 37;
      this.label9.Text = "Kısa Adı";
      this.btnGrupDuzelt.Location = new Point(341, 109);
      this.btnGrupDuzelt.Name = "btnGrupDuzelt";
      this.btnGrupDuzelt.Size = new Size(212, 35);
      this.btnGrupDuzelt.TabIndex = 39;
      this.btnGrupDuzelt.TabStop = false;
      this.btnGrupDuzelt.Text = "Düzelt";
      this.btnGrupDuzelt.UseVisualStyleBackColor = true;
      this.btnGrupDuzelt.Click += new EventHandler(this.btnGrupDuzelt_Click);
      this.txtGrupAdi.Location = new Point(401, 19);
      this.txtGrupAdi.Name = "txtGrupAdi";
      this.txtGrupAdi.Size = new Size(152, 20);
      this.txtGrupAdi.TabIndex = 34;
      this.txtGrupKisaAdi.Location = new Point(401, 45);
      this.txtGrupKisaAdi.MaxLength = 5;
      this.txtGrupKisaAdi.Name = "txtGrupKisaAdi";
      this.txtGrupKisaAdi.Size = new Size(79, 20);
      this.txtGrupKisaAdi.TabIndex = 35;
      this.btnGrupEkle.Location = new Point(341, 71);
      this.btnGrupEkle.Name = "btnGrupEkle";
      this.btnGrupEkle.Size = new Size(212, 35);
      this.btnGrupEkle.TabIndex = 38;
      this.btnGrupEkle.Text = "Ekle";
      this.btnGrupEkle.UseVisualStyleBackColor = true;
      this.btnGrupEkle.Click += new EventHandler(this.btnGrupEkle_Click);
      this.label10.AutoSize = true;
      this.label10.Location = new Point(338, 22);
      this.label10.Name = "label10";
      this.label10.Size = new Size(48, 13);
      this.label10.TabIndex = 36;
      this.label10.Text = "Grup Adı";
      this.btnSinifKosullar.Location = new Point(541, 190);
      this.btnSinifKosullar.Name = "btnSinifKosullar";
      this.btnSinifKosullar.Size = new Size(170, 35);
      this.btnSinifKosullar.TabIndex = 32;
      this.btnSinifKosullar.TabStop = false;
      this.btnSinifKosullar.Text = "Koşullar";
      this.btnSinifKosullar.UseVisualStyleBackColor = true;
      this.btnSinifKosullar.Click += new EventHandler(this.btnSinifKosullar_Click);
      this.btnSinifSil.Location = new Point(541, 149);
      this.btnSinifSil.Name = "btnSinifSil";
      this.btnSinifSil.Size = new Size(170, 35);
      this.btnSinifSil.TabIndex = 30;
      this.btnSinifSil.TabStop = false;
      this.btnSinifSil.Text = "Sil";
      this.btnSinifSil.UseVisualStyleBackColor = true;
      this.btnSinifSil.Click += new EventHandler(this.btnSinifSil_Click);
      this.btnSinifDuzelt.Location = new Point(541, 108);
      this.btnSinifDuzelt.Name = "btnSinifDuzelt";
      this.btnSinifDuzelt.Size = new Size(170, 35);
      this.btnSinifDuzelt.TabIndex = 29;
      this.btnSinifDuzelt.TabStop = false;
      this.btnSinifDuzelt.Text = "Düzelt";
      this.btnSinifDuzelt.UseVisualStyleBackColor = true;
      this.btnSinifDuzelt.Click += new EventHandler(this.btnSinifDuzelt_Click);
      this.btnSinifEkle.Location = new Point(541, 67);
      this.btnSinifEkle.Name = "btnSinifEkle";
      this.btnSinifEkle.Size = new Size(170, 35);
      this.btnSinifEkle.TabIndex = 27;
      this.btnSinifEkle.Text = "Ekle";
      this.btnSinifEkle.UseVisualStyleBackColor = true;
      this.btnSinifEkle.Click += new EventHandler(this.btnSinifEkle_Click);
      this.label7.AutoSize = true;
      this.label7.Location = new Point(481, 47);
      this.label7.Name = "label7";
      this.label7.Size = new Size(45, 13);
      this.label7.TabIndex = 31;
      this.label7.Text = "Kısa Adı";
      this.txtSinifKisaAdi.Location = new Point(541, 41);
      this.txtSinifKisaAdi.MaxLength = 5;
      this.txtSinifKisaAdi.Name = "txtSinifKisaAdi";
      this.txtSinifKisaAdi.Size = new Size(100, 20);
      this.txtSinifKisaAdi.TabIndex = 26;
      this.label8.AutoSize = true;
      this.label8.Location = new Point(481, 21);
      this.label8.Name = "label8";
      this.label8.Size = new Size(45, 13);
      this.label8.TabIndex = 28;
      this.label8.Text = "Sınıf Adı";
      this.txtSinifAdi.Location = new Point(541, 15);
      this.txtSinifAdi.Name = "txtSinifAdi";
      this.txtSinifAdi.Size = new Size(173, 20);
      this.txtSinifAdi.TabIndex = 24;
      this.toolStripMenuItem6.Name = "toolStripMenuItem6";
      this.toolStripMenuItem6.Size = new Size(149, 6);
      this.derslikAlfabetikSirala.Name = "derslikAlfabetikSirala";
      this.derslikAlfabetikSirala.Size = new Size(152, 22);
      this.derslikAlfabetikSirala.Text = "Alfabetik Sırala";
      this.derslikAlfabetikSirala.Click += new EventHandler(this.derslikAlfabetikSirala_Click);
      this.toolStripMenuItem7.Name = "toolStripMenuItem7";
      this.toolStripMenuItem7.Size = new Size(149, 6);
      this.sinifAlfabetikSirala.Name = "sinifAlfabetikSirala";
      this.sinifAlfabetikSirala.Size = new Size(152, 22);
      this.sinifAlfabetikSirala.Text = "Alfabetik Sırala";
      this.sinifAlfabetikSirala.Click += new EventHandler(this.sinifAlfabetikSirala_Click);
      this.derslerAcilirMenu.Items.AddRange(new ToolStripItem[4]
      {
        (ToolStripItem) this.derslerYukariTasi,
        (ToolStripItem) this.derslerAsagiTasi,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.derslerAlfabetikSirala
      });
      this.derslerAcilirMenu.Name = "acilirMenu1";
      this.derslerAcilirMenu.Size = new Size(153, 76);
      this.derslerYukariTasi.Name = "derslerYukariTasi";
      this.derslerYukariTasi.Size = new Size(152, 22);
      this.derslerYukariTasi.Text = "Yukarı Taşı";
      this.derslerYukariTasi.Click += new EventHandler(this.dersYukari);
      this.derslerAsagiTasi.Name = "derslerAsagiTasi";
      this.derslerAsagiTasi.Size = new Size(152, 22);
      this.derslerAsagiTasi.Text = "Aşağı Taşı";
      this.derslerAsagiTasi.Click += new EventHandler(this.dersAsagi);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(149, 6);
      this.derslerAlfabetikSirala.Name = "derslerAlfabetikSirala";
      this.derslerAlfabetikSirala.Size = new Size(152, 22);
      this.derslerAlfabetikSirala.Text = "Alfabetik Sırala";
      this.derslerAlfabetikSirala.Click += new EventHandler(this.derslerAlfabetikSirala_Click);
      this.button10.FlatAppearance.BorderSize = 0;
      this.button10.FlatStyle = FlatStyle.Flat;
      this.button10.Image = (Image) Resources.alfabetik;
      this.button10.Location = new Point(474, 188);
      this.button10.Name = "button10";
      this.button10.Size = new Size(45, 45);
      this.button10.TabIndex = 9;
      this.button10.UseVisualStyleBackColor = true;
      this.button10.Click += new EventHandler(this.derslerAlfabetikSirala_Click);
      this.button9.FlatAppearance.BorderSize = 0;
      this.button9.FlatStyle = FlatStyle.Flat;
      this.button9.Image = (Image) Resources.asagi;
      this.button9.Location = new Point(474, 137);
      this.button9.Name = "button9";
      this.button9.Size = new Size(45, 45);
      this.button9.TabIndex = 8;
      this.button9.UseVisualStyleBackColor = true;
      this.button9.Click += new EventHandler(this.dersAsagi);
      this.button8.FlatAppearance.BorderSize = 0;
      this.button8.FlatStyle = FlatStyle.Flat;
      this.button8.Image = (Image) Resources.yukari;
      this.button8.Location = new Point(474, 86);
      this.button8.Name = "button8";
      this.button8.Size = new Size(45, 45);
      this.button8.TabIndex = 7;
      this.button8.UseVisualStyleBackColor = true;
      this.button8.Click += new EventHandler(this.dersYukari);
      this.button11.FlatAppearance.BorderSize = 0;
      this.button11.FlatStyle = FlatStyle.Flat;
      this.button11.Image = (Image) Resources.alfabetik;
      this.button11.Location = new Point(474, 188);
      this.button11.Name = "button11";
      this.button11.Size = new Size(45, 45);
      this.button11.TabIndex = 19;
      this.button11.UseVisualStyleBackColor = true;
      this.button11.Click += new EventHandler(this.ogretmenAlfabetikSirala);
      this.button12.FlatAppearance.BorderSize = 0;
      this.button12.FlatStyle = FlatStyle.Flat;
      this.button12.Image = (Image) Resources.asagi;
      this.button12.Location = new Point(474, 137);
      this.button12.Name = "button12";
      this.button12.Size = new Size(45, 45);
      this.button12.TabIndex = 18;
      this.button12.UseVisualStyleBackColor = true;
      this.button12.Click += new EventHandler(this.ogretmenAsagi);
      this.button13.FlatAppearance.BorderSize = 0;
      this.button13.FlatStyle = FlatStyle.Flat;
      this.button13.Image = (Image) Resources.yukari;
      this.button13.Location = new Point(474, 86);
      this.button13.Name = "button13";
      this.button13.Size = new Size(45, 45);
      this.button13.TabIndex = 17;
      this.button13.UseVisualStyleBackColor = true;
      this.button13.Click += new EventHandler(this.ogretmenYukari);
      this.button14.FlatAppearance.BorderSize = 0;
      this.button14.FlatStyle = FlatStyle.Flat;
      this.button14.Image = (Image) Resources.alfabetik;
      this.button14.Location = new Point(474, 188);
      this.button14.Name = "button14";
      this.button14.Size = new Size(45, 45);
      this.button14.TabIndex = 28;
      this.button14.UseVisualStyleBackColor = true;
      this.button14.Click += new EventHandler(this.derslikAlfabetikSirala_Click);
      this.button15.FlatAppearance.BorderSize = 0;
      this.button15.FlatStyle = FlatStyle.Flat;
      this.button15.Image = (Image) Resources.asagi;
      this.button15.Location = new Point(474, 137);
      this.button15.Name = "button15";
      this.button15.Size = new Size(45, 45);
      this.button15.TabIndex = 27;
      this.button15.UseVisualStyleBackColor = true;
      this.button15.Click += new EventHandler(this.derslikAsagi);
      this.button16.FlatAppearance.BorderSize = 0;
      this.button16.FlatStyle = FlatStyle.Flat;
      this.button16.Image = (Image) Resources.yukari;
      this.button16.Location = new Point(474, 86);
      this.button16.Name = "button16";
      this.button16.Size = new Size(45, 45);
      this.button16.TabIndex = 26;
      this.button16.UseVisualStyleBackColor = true;
      this.button16.Click += new EventHandler(this.derslikYukari);
      this.button17.FlatAppearance.BorderSize = 0;
      this.button17.FlatStyle = FlatStyle.Flat;
      this.button17.Image = (Image) Resources.alfabetik;
      this.button17.Location = new Point(481, 169);
      this.button17.Name = "button17";
      this.button17.Size = new Size(45, 45);
      this.button17.TabIndex = 44;
      this.button17.UseVisualStyleBackColor = true;
      this.button17.Click += new EventHandler(this.sinifAlfabetikSirala_Click);
      this.button18.FlatAppearance.BorderSize = 0;
      this.button18.FlatStyle = FlatStyle.Flat;
      this.button18.Image = (Image) Resources.asagi;
      this.button18.Location = new Point(481, 118);
      this.button18.Name = "button18";
      this.button18.Size = new Size(45, 45);
      this.button18.TabIndex = 43;
      this.button18.UseVisualStyleBackColor = true;
      this.button18.Click += new EventHandler(this.sinifAsagi);
      this.button19.FlatAppearance.BorderSize = 0;
      this.button19.FlatStyle = FlatStyle.Flat;
      this.button19.Image = (Image) Resources.yukari;
      this.button19.Location = new Point(481, 67);
      this.button19.Name = "button19";
      this.button19.Size = new Size(45, 45);
      this.button19.TabIndex = 42;
      this.button19.UseVisualStyleBackColor = true;
      this.button19.Click += new EventHandler(this.sinifYukari);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(735, 534);
      this.Controls.Add((Control) this.tbBilgiGirisleri);
      this.Name = nameof (BilgiGirisForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Bilgi Giriş Formu";
      this.Load += new EventHandler(this.BilgiGirisForm_Load);
      this.tbBilgiGirisleri.ResumeLayout(false);
      this.tpDersler.ResumeLayout(false);
      this.tpDersler.PerformLayout();
      ((ISupportInitialize) this.dgvDersler).EndInit();
      this.tpOgretmenler.ResumeLayout(false);
      this.tpOgretmenler.PerformLayout();
      ((ISupportInitialize) this.dgvOgretmenler).EndInit();
      this.ogretmenlerAcilirMenu.ResumeLayout(false);
      this.tpDerslikler.ResumeLayout(false);
      this.tpDerslikler.PerformLayout();
      ((ISupportInitialize) this.dgvDerslikler).EndInit();
      this.dersliklerAcilirMenu.ResumeLayout(false);
      this.tpSiniflar.ResumeLayout(false);
      this.tpSiniflar.PerformLayout();
      this.panel1.ResumeLayout(false);
      ((ISupportInitialize) this.dgvSiniflar).EndInit();
      this.siniflarAcilirMenu.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((ISupportInitialize) this.dgvSinifGruplar).EndInit();
      this.derslerAcilirMenu.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
