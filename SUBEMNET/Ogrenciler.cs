using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class Ogrenciler : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        public Ogrenciler()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "Pofil";
            dgvBtn.Text = ">>";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
          
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.Şube, ö.KayitTarihi as KayıtTarihi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program, ö.Devre, Kur, ö.Snf as Sınıf from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ö.Snf='" + comboBox1.Text+"' and ş.okulid='"+okulid+"'";
            griddoldur();
            query = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.Şube, ö.KayitTarihi as KayıtTarihi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program, ö.Devre, Kur, ö.Snf as Sınıf from Ogrenci ö join Sube ş ş.ID=ö.SubeWhere ö.Devre='" + comboBox2.Text+"' and ş.okulid="+okulid+"'";
            griddoldur();
            query = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.Şube, ö.KayitTarihi as KayıtTarihi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program, ö.Devre, Kur, ö.Snf as Sınıf from Ogrenci ö join Sube ş on ş.ID=ö.Sube  Where ö.TCKN='" + textBox1.Text+"' and ş.okulid='"+okulid+"'";
            griddoldur();
            query = null;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                OgrenciProfil prfl = new OgrenciProfil();
                prfl.OgrID = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                prfl.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        int okulid = Form1.okulid;
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = true;
                string filtre = "Select ö.ID, ö.Sezon, ş.ŞubeAdi, ö.KayitTarihi as KayıtTarihi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program, ö.Devre, ö.Kur, ö.Snf as Sınıf from Ogrenci ö join Sube ş on ş.ID= ö.Sube Where ş.okulid='"+okulid+"'";
                if (cmbSezon.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "and";
                    }
                    filtre += " ö.Sezon=+" + cmbSezon.Text+"'";
                    degisken = true;
                }
                if (cmbSube.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "and";
                    }
                    filtre += " ş.ŞubeAdi=" + cmbSube.Text + "'";
                    degisken = true;
                }
                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Devre='" + cmbKur.Text + "'";
                    degisken = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
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
                    filtre += " ö.Adi=" + txtAd.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Soyadi=" + txtSoyad.Text.TrimEnd();
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
                if (string.IsNullOrEmpty(mskOgrCep.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";

                    }
                    filtre += " ö.OgrCepTel ='" + "'" + mskOgrCep.Text + "'";
                    degisken = true;
                }
                if (radioButton1.Checked)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Durum=0";
                    degisken = true;
                }
                if (radioButton2.Checked)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Durum=1";
                    degisken = true;
                }
                if (dtKayTarBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.KayitTarihi between '" + dtKayTarBas.Value.ToString("yyyyMMdd") + "' and '" + dtKayTarBit.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (dtSilTarBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.KayitSilinmeTarihi between '" + dtSilTarBas.Value.ToString("yyyyMMdd") + "' and '" + dtSilTarBit.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                query = filtre;
                panel1.Visible = false;
                griddoldur();
                query = null;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        private void Ogrenciler_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbSube.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbKur.Items.Add(oku[0].ToString());

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
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                cmbSube.Items.Add(oku4[0].ToString());
                subeid.Add((int)oku4[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select Sezon from Sezon where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                cmbSezon.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
