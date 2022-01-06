using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class KİTAPHAREKETLERİ : Form
    {
        public KİTAPHAREKETLERİ()
        {
            InitializeComponent();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            panel12.Visible = true;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            panel12.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox27.Text) == false)
            {
                try
                {

                    bool degisken = true;
                    string filtre = "Select t.ID,k.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Doc 'DDC/DOS',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Dolap DOLAP,t.Raf RAF,T.Seviye SEVİYE,t.Barkod 'BARKOD' from Kitaplar t join Sube ş on ş.ID=t.Sube join Kütüphane k on k.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";
                    if (string.IsNullOrEmpty(textBox27.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.KitapAdı ='" + textBox27.Text + "'";
                        degisken = true;
                    }
                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "KİTAPLAR");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.Close();
                    DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                    dgvBtn.HeaderText = "HASARLI";
                    dgvBtn.Text = "GÜNCELLE";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView1.Columns.Add(dgvBtn);
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.HeaderText = "KAYIP";
                    btn.Text = "GÜNCELLE";
                    btn.UseColumnTextForButtonValue = true;
                    btn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    btn.Width = 70;
                    dataGridView1.Columns.Add(btn);
                    DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
                    btn2.HeaderText = "DETAY";
                    btn2.Text = "GÖSTER";
                    btn2.UseColumnTextForButtonValue = true;
                    btn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                    btn2.Width = 70;
                    dataGridView1.Columns.Add(btn2);
                    DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                    dgvBtn2.HeaderText = "SİL";
                    dgvBtn2.Text = "SİL";
                    dgvBtn2.UseColumnTextForButtonValue = true;
                    dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn2.Width = 70;
                    dataGridView1.Columns.Add(dgvBtn2);
                    panel2.Visible = false;

                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
        }



        private void textBox27_Click(object sender, EventArgs e)
        {
            textBox27.Text = "";
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        List<int> subeid = new List<int>();
        List<int> kütüphane = new List<int>();
        private void KİTAPHAREKETLERİ_Load(object sender, EventArgs e)
        {
            comboBox21.Items.Clear();
            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox26.Items.Add(oku[0].ToString());
                comboBox21.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);

            }
            baglan.Close();
            kütüphane.Clear();
            komut = new SqlCommand("Select k.Kütüphane,k.ID from Kütüphane k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox22.Items.Add(oku2[0].ToString());
                comboBox27.Items.Add(oku2[0].ToString());

                kütüphane.Add((int)oku2[1]);

            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku22 = komut.ExecuteReader();
            while (oku22.Read())
            {
                comboBox2.Items.Add(oku22[0].ToString());
                comboBox20.Items.Add(oku22[0].ToString());

                cmbKur.Items.Add(oku22[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku23 = komut.ExecuteReader();
            while (oku23.Read())
            {
                comboBox3.Items.Add(oku23[0].ToString());
                comboBox18.Items.Add(oku23[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku7 = komut.ExecuteReader();
            while (oku7.Read())
            {
                cmbProgram.Items.Add(oku7[0].ToString());

            }
            baglan.Close();
            cmbCinsiyet.Items.Add("ERKEK");
            cmbCinsiyet.Items.Add("KIZ");
        }
        int okulid = Form1.okulid;
        private void button25_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox26.Text) == true || string.IsNullOrEmpty(comboBox27.Text) == true)
            {
                MessageBox.Show("ŞUBE VE KÜTÜPHANE BİLGİLERİNİN SEÇİLMESİ ZORUNLUDUR.");
            }
            else
            {
                try
                {

                    bool degisken = true;
                    string filtre = "Select t.ID,k.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Doc 'DDC/DOS',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Dolap DOLAP,t.Raf RAF,T.Seviye SEVİYE,t.Barkod 'BARKOD' from Kitaplar t join Sube ş on ş.ID=t.Sube join Kütüphane k on k.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";
                    if (string.IsNullOrEmpty(textBox26.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.KitapAdı ='" + textBox26.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox22.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Doc = '" + textBox22.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox25.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Barkod ='" + textBox25.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox21.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.ISBN = '" + textBox21.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Dil = '" + comboBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox19.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Yayınevi = '" + comboBox19.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox4.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Dolap = '" + comboBox4.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox16.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Durum = '" + comboBox16.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox24.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Tür = '" + comboBox24.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox25.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Yazar = '" + comboBox25.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox17.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Seri = '" + comboBox17.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox14.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Seviye = '" + comboBox14.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox15.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Raf = '" + comboBox15.Text + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(comboBox26.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Sube =" + subeid[comboBox26.SelectedIndex];
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox27.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Kütüphane =" + kütüphane[comboBox27.SelectedIndex];
                        degisken = true;
                    }
                    if (checkBox8.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Kayıp ='" + Convert.ToBoolean(false) + "'";
                        degisken = true;
                    }
                    if (checkBox5.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Hasarlı ='" + Convert.ToBoolean(false) + "'";
                        degisken = true;
                    }

                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "KİTAPLAR");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.Close();
                    DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                    dgvBtn.HeaderText = "SEÇ";
                    dgvBtn.Text = "SEÇ";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView1.Columns.Add(dgvBtn);
                    panel12.Visible = false;

                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = true;
                string filtre = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'";

                if (degisken == true)
                {
                    filtre += " and ";
                }
                filtre += " ş.ŞubeAdi='" + comboBox26.Text + "'";
                degisken = true;

                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Devre='" + cmbKur.Text + "'";
                    degisken = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Program='" + cmbProgram.Text + "'";
                    degisken = true;
                }
                if (cmbCinsiyet.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Cinsiyet='" + cmbCinsiyet.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Adi='" + txtAd.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Soyadi='" + txtSoyad.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSozno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.SozNo='" + txtSozno.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtTc.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.TCKN='" + txtTc.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtOkulno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.OkulNo='" + txtOkulno.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (radioButton4.Checked)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Durum=1";
                    degisken = true;
                }

                query = filtre;
                panel1.Visible = false;

                dataGridView1.Columns.Clear();
                baglan.Open();
                if (query != null)
                    da = new SqlDataAdapter(query, baglan);
           
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                query = null;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }
        public string query;

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "' and Snf = " + comboBox3.Text;
            dataGridView1.Columns.Clear();
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SEÇ";
            dgvBtn.Text = "SEÇ";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            query = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "' and Devre=" + comboBox2.Text;
            dataGridView1.Columns.Clear();
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SEÇ";
            dgvBtn.Text = "SEÇ";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            query = null;
        }
        string kitapid;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 10)
            {
                label19.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[2].Value.ToString();
                kitapid = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        string öğrenciid;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentCell.ColumnIndex == 9)
            {
                label16.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[3].Value.ToString() + " " + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString();
                öğrenciid = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }
        }
        SqlCommand komut;
        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(label19.Text) == true)
            {
                MessageBox.Show("ÖĞRENCİ VEYA KİTAP BİLGİSİ BOŞ BIRAKILAMAZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into  KitapHareketleri (Öğrenci,Kitap,Tarih,Tahmini,TeslimDurum,Sube,Kütüphane) values (@p1, @p2, @p3, @p4,@p5, @p7,@p8)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", kitapid);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", Convert.ToBoolean(false));
                    komutkaydet.Parameters.AddWithValue("@p7", subeid[comboBox26.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p8", kütüphane[comboBox27.SelectedIndex]);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;

                    komut = new SqlCommand("Select Alınma from Kitaplar where ID='" + kitapid + "'", baglan);
                    baglan.Open();
                    int i = 0;
                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[1] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (int)oku3[1];
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Kitaplar set Alınma=@p1,TeslimDurum=@p2 where ID='" + kitapid + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", i + 1);
                        komutgüncelle.Parameters.AddWithValue("@p2", Convert.ToBoolean(false));

                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Kitaplar set Alınma=@p1,TeslimDurum=@p2 where ID='" + kitapid + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", 1);
                        komutgüncelle.Parameters.AddWithValue("@p2", Convert.ToBoolean(false));

                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    label19.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("HATA");
                }
}
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            tabControl1.SelectedTab = tabPage1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //detay ve teslim durumu için oluştur
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("KİTAP ALIM İŞLEMİNE DEVAM ETMEK İSTİYOR MUSUNUZ?", "KİTAP DÖNÜŞ GÜNCELLE", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " KİTABININ ALINDIĞINI ONAYLIYOR MUSUNUZ?", "KİTAP DÖNÜŞ GÜNCELLE", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            baglan.Open();
                            SqlCommand degistir = new SqlCommand("update KitapHareketleri set TeslimDurum=@a1,Dönüş=@a4 where ID=@a2 ", baglan);
                            degistir.Parameters.AddWithValue("@a2", id);
                            degistir.Parameters.AddWithValue("@a1", Convert.ToBoolean(true));
                            degistir.Parameters.AddWithValue("@a4", DateTime.Now.ToString("yyyyMMdd"));
                            degistir.ExecuteNonQuery();
                            MessageBox.Show("KAYIP DURUMU GÜNCELLENDİ.");
                            baglan.Close();
                            komut = new SqlCommand("Select Kitap from KitapHareketleri where ID='" + id + "'", baglan);
                            baglan.Open();
                            int i = 0;
                            SqlDataReader oku3 = komut.ExecuteReader();
                            while (oku3.Read())
                            {
                              
                                    i = (int)oku3[1];
                                
                            }
                            baglan.Open();
                            SqlCommand degistir2 = new SqlCommand("update Kitaplar set TeslimDurum=@a1 where ID=@a2 ", baglan);
                            degistir2.Parameters.AddWithValue("@a2", i);
                            degistir2.Parameters.AddWithValue("@a1", Convert.ToBoolean(true));
                            baglan.Close();
                            griddoldur();
                        }
                        catch (Exception a)
                        {
                            baglan.Close();
                            MessageBox.Show(a.ToString());
                        }

                    }
                }
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox21.Text) == true || string.IsNullOrEmpty(comboBox22.Text) == true)
            {
                MessageBox.Show("ŞUBE VE KÜTÜPHANE BİLGİLERİNİN GİRİLMESİ ZORUNLUDUR.");
            }
            else
            {
                griddoldur();
            }
        }
        void griddoldur()
        {
            try
            {

                bool degisken = true;
                string filtre = "Select t.ID,k.KitapAdı 'KİTAP',o.OkulNo 'OKUL NO',(o.Adi + o.Soyadi) as ÖĞRENCİ,t.Tarih 'ALIŞ TARİHİ',t.Tahmini 'TAHMİNİ DÖNÜŞ TARİHİ',t.Dönüş 'DÖNÜŞ TARİHİ' from KitapHareketleri t join Kitaplar k on t.Kitap=k.ID join Ogrenci o on o.ID=t.Öğrenci join Sube ş on ş.ID=t.Sube join Kütüphane kü on kü.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";
                if (string.IsNullOrEmpty(textBox26.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " k.KitapAdı ='" + textBox26.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox25.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " k.Barkod = '" + textBox25.Text +"'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox21.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " k.ISBN = '" + textBox21.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox19.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " k.Yayınevi = '" + comboBox19.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox25.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " k.Yazar = '" + comboBox25.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox26.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Sube =" + subeid[comboBox26.SelectedIndex];
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox27.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Kütüphane =" + kütüphane[comboBox27.SelectedIndex];
                    degisken = true;
                }
                if (radioButton1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (radioButton2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tahmini between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (radioButton3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Dönüş between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tahmini ='" + DateTime.Now.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Dönüş> t.Tahmini";
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.TeslimDurum ='" + Convert.ToBoolean(false) + "'";
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.TeslimDurum ='" + Convert.ToBoolean(false) + "' and t.Tahmini<'" + DateTime.Now.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "KİTAPHAREKETLERİ");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "DÖNÜŞ";
                dgvBtn.Text = "ONAYLA";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                panel2.Visible = false;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 1);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontTitle));

                        }
                    }


                }
                catch (NullReferenceException)
                {
                }

                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);// sayfa boyutu.
                    PdfWriter.GetInstance(pdfDoc, stream);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 20, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font regularFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 15, iTextSharp.text.Font.NORMAL);
                    Paragraph title;
                    Paragraph text;
                    title = new Paragraph(textBox1.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox4.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox2.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable);
                    text = new Paragraph("NOT: " + richTextBox1.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel2.Visible = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }
    }
}

