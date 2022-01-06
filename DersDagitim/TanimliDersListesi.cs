using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class TanimliDersListesi : Form
  {
    private DataTable dtTanimliDersler;
    private bilesenTaban tabanBilesen;
    private IContainer components;
    private DataGridView dgvTanimliDersler;
    private Button button1;
    private Button button2;
    private Button button3;
    private Panel panel1;
    private Panel panel2;
    private Button button4;
    private DataGridViewTextBoxColumn KolonId;
    private DataGridViewTextBoxColumn KolonDersAdi;
    private DataGridViewTextBoxColumn KolonOgretmenler;
    private DataGridViewTextBoxColumn KolonSiniflar;
    private DataGridViewTextBoxColumn KolonDerslikler;
    private DataGridViewTextBoxColumn KolonToplamDers;
    private DataGridViewTextBoxColumn KolonYerlesim;

    public TanimliDersListesi(bilesenTaban tabanBilesen = null)
    {
      this.InitializeComponent();
      this.tanimliDersleriYenile(tabanBilesen);
      this.tabanBilesen = tabanBilesen;
    }

    private void tanimliDersleriYenile(bilesenTaban tabanBilesen)
    {
      this.dtTanimliDersler = new DataTable();
      this.dtTanimliDersler.Columns.Add("id", typeof (ushort));
      this.dtTanimliDersler.Columns.Add("dersadi", typeof (string));
      this.dtTanimliDersler.Columns.Add("ogretmenler", typeof (string));
      this.dtTanimliDersler.Columns.Add("sinifgruplar", typeof (string));
      this.dtTanimliDersler.Columns.Add("derslikler", typeof (string));
      this.dtTanimliDersler.Columns.Add("toplamders", typeof (ushort));
      this.dtTanimliDersler.Columns.Add("yerlesim", typeof (string));
      for (int index1 = 0; index1 < tanim.program.tanimliDersler.Count; ++index1)
      {
        bilesenTanimliDers bilesenTanimliDers = tanim.program.tanimliDersler[index1];
        bool flag = false;
        if (tabanBilesen == null)
        {
          flag = true;
        }
        else
        {
          if (tabanBilesen is bilesenDers && (int) (tabanBilesen as bilesenDers).id == (int) bilesenTanimliDers.ders.id)
            flag = true;
          if (tabanBilesen is bilesenOgretmen)
          {
            bilesenOgretmen bilesenOgretmen = tabanBilesen as bilesenOgretmen;
            for (int index2 = 0; index2 < bilesenTanimliDers.ogretmenler.Count; ++index2)
            {
              if ((int) bilesenOgretmen.id == (int) bilesenTanimliDers.ogretmenler[index2].id)
                flag = true;
            }
          }
          if (tabanBilesen is bilesenDerslik)
          {
            bilesenDerslik bilesenDerslik = tabanBilesen as bilesenDerslik;
            for (int index2 = 0; index2 < bilesenTanimliDers.derslikler.Count; ++index2)
            {
              if ((int) bilesenDerslik.id == (int) bilesenTanimliDers.derslikler[index2].id)
                flag = true;
            }
          }
          if (tabanBilesen is bilesenSinif)
          {
            bilesenSinif bilesenSinif = tabanBilesen as bilesenSinif;
            for (int index2 = 0; index2 < bilesenTanimliDers.sinifGruplar.Count; ++index2)
            {
              if ((int) bilesenSinif.id == (int) bilesenTanimliDers.sinifGruplar[index2].sinif.id)
                flag = true;
            }
          }
        }
        if (flag)
        {
          ushort id = bilesenTanimliDers.id;
          string adi = bilesenTanimliDers.ders.adi;
          string str1 = "";
          for (int index2 = 0; index2 < bilesenTanimliDers.ogretmenler.Count; ++index2)
          {
            str1 += bilesenTanimliDers.ogretmenler[index2].adi;
            if (index2 < bilesenTanimliDers.ogretmenler.Count - 1)
              str1 += "\n";
          }
          string str2 = "";
          for (int index2 = 0; index2 < bilesenTanimliDers.sinifGruplar.Count; ++index2)
          {
            string kisaAdi1 = bilesenTanimliDers.sinifGruplar[index2].sinif.kisaAdi;
            string kisaAdi2 = bilesenTanimliDers.sinifGruplar[index2].grup.kisaAdi;
            str2 += string.Format(" {0}:{1} ", (object) kisaAdi1, (object) kisaAdi2);
          }
          string str3 = "";
          for (int index2 = 0; index2 < bilesenTanimliDers.derslikler.Count; ++index2)
          {
            str3 += bilesenTanimliDers.derslikler[index2].adi;
            if (index2 < bilesenTanimliDers.derslikler.Count - 1)
              str3 += " - ";
          }
          ushort toplamSaat = bilesenTanimliDers.toplamSaat;
          string yerlesimStr = bilesenTanimliDers.yerlesimStr;
          this.dtTanimliDersler.Rows.Add((object) id, (object) adi, (object) str1, (object) str2, (object) str3, (object) toplamSaat, (object) yerlesimStr);
        }
      }
      this.dgvTanimliDersler.DataSource = (object) this.dtTanimliDersler;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      int num = (int) new DersTanimlamaForm(this.tabanBilesen).ShowDialog();
      this.tanimliDersleriYenile(this.tabanBilesen);
    }

    private void button2_Click(object sender, EventArgs e)
    {
      if (this.dgvTanimliDersler.SelectedRows.Count <= 0)
        return;
      DersTanimlamaForm dersTanimlamaForm = new DersTanimlamaForm(Convert.ToUInt16(this.dgvTanimliDersler.SelectedRows[0].Cells[0].Value));
      bilesenTanimliDers bilesenTanimliDers = tanim.program.tanimliDersGetir(Convert.ToUInt16(this.dgvTanimliDersler.SelectedRows[0].Cells[0].Value));
      bool flag = false;
      if (bilesenTanimliDers.aktifYerlesim != null)
      {
        flag = true;
        bilesenTanimliDers.kaldir();
      }
      int num1 = (int) dersTanimlamaForm.ShowDialog();
      if (flag && !bilesenTanimliDers.eskiyeYerles())
      {
        int num2 = (int) MessageBox.Show("Ders üzerinde yapılan değişiklik ile eski yerleşimine yerleşemedi!");
      }
      int scrollingRowIndex = this.dgvTanimliDersler.FirstDisplayedScrollingRowIndex;
      int index = this.dgvTanimliDersler.SelectedRows[0].Index;
      this.tanimliDersleriYenile(this.tabanBilesen);
      if (index != -1 && scrollingRowIndex != -1 && this.dgvTanimliDersler.Rows.Count > scrollingRowIndex)
        this.dgvTanimliDersler.FirstDisplayedScrollingRowIndex = scrollingRowIndex;
      if (index == -1 || index >= this.dgvTanimliDersler.Rows.Count)
        return;
      this.dgvTanimliDersler.Rows[index].Selected = true;
    }

    private void button3_Click(object sender, EventArgs e)
    {
      if (this.dgvTanimliDersler.SelectedRows.Count <= 0 || MessageBox.Show("Seçili ders silinecek emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvTanimliDersler.SelectedRows[0].Cells[0].Value);
      for (int index = 0; index < tanim.program.tanimliDersler.Count; ++index)
      {
        if ((int) tanim.program.tanimliDersler[index].id == (int) uint16)
          tanim.program.tanimliDersler.RemoveAt(index);
      }
      this.tanimliDersleriYenile(this.tabanBilesen);
    }

    private void button4_Click(object sender, EventArgs e)
    {
      if (this.dgvTanimliDersler.SelectedRows.Count <= 0)
        return;
      ushort uint16 = Convert.ToUInt16(this.dgvTanimliDersler.SelectedRows[0].Cells[0].Value);
      bilesenTanimliDers _analizDers = tanim.program.tanimliDersGetir(uint16);
      if (_analizDers == null)
        return;
      int num = (int) new TanimliDersAnaliz(_analizDers).ShowDialog();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      this.dgvTanimliDersler = new DataGridView();
      this.button1 = new Button();
      this.button2 = new Button();
      this.button3 = new Button();
      this.panel1 = new Panel();
      this.button4 = new Button();
      this.panel2 = new Panel();
      this.KolonId = new DataGridViewTextBoxColumn();
      this.KolonDersAdi = new DataGridViewTextBoxColumn();
      this.KolonOgretmenler = new DataGridViewTextBoxColumn();
      this.KolonSiniflar = new DataGridViewTextBoxColumn();
      this.KolonDerslikler = new DataGridViewTextBoxColumn();
      this.KolonToplamDers = new DataGridViewTextBoxColumn();
      this.KolonYerlesim = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dgvTanimliDersler).BeginInit();
      this.panel1.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      this.dgvTanimliDersler.AllowUserToAddRows = false;
      this.dgvTanimliDersler.AllowUserToDeleteRows = false;
      this.dgvTanimliDersler.AllowUserToResizeColumns = false;
      this.dgvTanimliDersler.AllowUserToResizeRows = false;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle1.BackColor = SystemColors.Control;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 162);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.dgvTanimliDersler.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.dgvTanimliDersler.ColumnHeadersHeight = 30;
      this.dgvTanimliDersler.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgvTanimliDersler.Columns.AddRange((DataGridViewColumn) this.KolonId, (DataGridViewColumn) this.KolonDersAdi, (DataGridViewColumn) this.KolonOgretmenler, (DataGridViewColumn) this.KolonSiniflar, (DataGridViewColumn) this.KolonDerslikler, (DataGridViewColumn) this.KolonToplamDers, (DataGridViewColumn) this.KolonYerlesim);
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
      gridViewCellStyle2.BackColor = SystemColors.Window;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7.5f, FontStyle.Regular, GraphicsUnit.Point, (byte) 162);
      gridViewCellStyle2.ForeColor = SystemColors.ControlText;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.True;
      this.dgvTanimliDersler.DefaultCellStyle = gridViewCellStyle2;
      this.dgvTanimliDersler.Dock = DockStyle.Fill;
      this.dgvTanimliDersler.Location = new Point(0, 0);
      this.dgvTanimliDersler.MultiSelect = false;
      this.dgvTanimliDersler.Name = "dgvTanimliDersler";
      this.dgvTanimliDersler.ReadOnly = true;
      this.dgvTanimliDersler.RowHeadersVisible = false;
      this.dgvTanimliDersler.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dgvTanimliDersler.RowTemplate.Height = 45;
      this.dgvTanimliDersler.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dgvTanimliDersler.Size = new Size(742, 476);
      this.dgvTanimliDersler.TabIndex = 26;
      this.dgvTanimliDersler.TabStop = false;
      this.button1.Location = new Point(3, 3);
      this.button1.Name = "button1";
      this.button1.Size = new Size(106, 43);
      this.button1.TabIndex = 27;
      this.button1.Text = "Yeni Ders";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.Location = new Point(115, 3);
      this.button2.Name = "button2";
      this.button2.Size = new Size(106, 43);
      this.button2.TabIndex = 28;
      this.button2.Text = "Dersi Düzenle";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.Location = new Point(227, 3);
      this.button3.Name = "button3";
      this.button3.Size = new Size(106, 43);
      this.button3.TabIndex = 29;
      this.button3.Text = "Sil";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.panel1.Controls.Add((Control) this.button4);
      this.panel1.Controls.Add((Control) this.button1);
      this.panel1.Controls.Add((Control) this.button3);
      this.panel1.Controls.Add((Control) this.button2);
      this.panel1.Dock = DockStyle.Bottom;
      this.panel1.Location = new Point(0, 476);
      this.panel1.Name = "panel1";
      this.panel1.Size = new Size(742, 49);
      this.panel1.TabIndex = 30;
      this.button4.Location = new Point(369, 3);
      this.button4.Name = "button4";
      this.button4.Size = new Size(106, 43);
      this.button4.TabIndex = 30;
      this.button4.Text = "Analiz Et";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.Click += new EventHandler(this.button4_Click);
      this.panel2.Controls.Add((Control) this.dgvTanimliDersler);
      this.panel2.Dock = DockStyle.Fill;
      this.panel2.Location = new Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new Size(742, 476);
      this.panel2.TabIndex = 31;
      this.KolonId.DataPropertyName = "id";
      this.KolonId.HeaderText = "id";
      this.KolonId.Name = "KolonId";
      this.KolonId.ReadOnly = true;
      this.KolonId.Visible = false;
      this.KolonDersAdi.DataPropertyName = "dersadi";
      this.KolonDersAdi.HeaderText = "Ders Adı";
      this.KolonDersAdi.Name = "KolonDersAdi";
      this.KolonDersAdi.ReadOnly = true;
      this.KolonDersAdi.Width = 140;
      this.KolonOgretmenler.DataPropertyName = "ogretmenler";
      this.KolonOgretmenler.HeaderText = "Öğretmenler";
      this.KolonOgretmenler.Name = "KolonOgretmenler";
      this.KolonOgretmenler.ReadOnly = true;
      this.KolonOgretmenler.Width = 180;
      this.KolonSiniflar.DataPropertyName = "sinifgruplar";
      this.KolonSiniflar.HeaderText = "Sınıf Gruplar";
      this.KolonSiniflar.Name = "KolonSiniflar";
      this.KolonSiniflar.ReadOnly = true;
      this.KolonSiniflar.Width = 110;
      this.KolonDerslikler.DataPropertyName = "derslikler";
      this.KolonDerslikler.HeaderText = "Derslikler";
      this.KolonDerslikler.Name = "KolonDerslikler";
      this.KolonDerslikler.ReadOnly = true;
      this.KolonDerslikler.Width = 160;
      this.KolonToplamDers.DataPropertyName = "toplamders";
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.KolonToplamDers.DefaultCellStyle = gridViewCellStyle3;
      this.KolonToplamDers.HeaderText = "Saat";
      this.KolonToplamDers.Name = "KolonToplamDers";
      this.KolonToplamDers.ReadOnly = true;
      this.KolonToplamDers.Width = 40;
      this.KolonYerlesim.DataPropertyName = "yerlesim";
      gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
      this.KolonYerlesim.DefaultCellStyle = gridViewCellStyle4;
      this.KolonYerlesim.HeaderText = "Yerleşim";
      this.KolonYerlesim.Name = "KolonYerlesim";
      this.KolonYerlesim.ReadOnly = true;
      this.KolonYerlesim.Width = 90;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(742, 525);
      this.Controls.Add((Control) this.panel2);
      this.Controls.Add((Control) this.panel1);
      this.Name = nameof (TanimliDersListesi);
      this.StartPosition = FormStartPosition.CenterParent;
      this.Text = "Tanımlanmış Dersler Listesi";
      ((ISupportInitialize) this.dgvTanimliDersler).EndInit();
      this.panel1.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
