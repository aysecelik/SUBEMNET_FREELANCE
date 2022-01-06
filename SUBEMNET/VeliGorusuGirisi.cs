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
    public partial class VeliGorusuGirisi : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        public VeliGorusuGirisi()
        {
            InitializeComponent();
        }
        string ogrID;
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select Ogrenci.ID, Ogrenci.Sube AS Şube, Ogrenci.SozNo, Ogrenci.Adi AS Adı, Ogrenci.Soyadi AS Soyadı, Ogrenci.OgrCepTel, Veli.AdSoyad, Veli.CepTel," +
                    " Ogrenci.Devre, Ogrenci.Snf AS Sınıf, Ogrenci.KayitTarihi, Ogrenci.KayitSilinmeTarihi, Ogrenci.Aciklama AS Açıklama, Ogrenci.OlusturmaTarihi AS Tarih from Ogrenci" +
                    " INNER JOIN Veli on Ogrenci.ID=veli.OgrID join Sube ş on ş.ID=Ogrenci.Sube where velimi=1 and ş.Okulid='"+okulid+"'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "EKLE";
            dgvBtn.Text = "EKLE";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
            dgvBtn2.HeaderText = "PROFİL";
            dgvBtn2.Text = ">>";
            dgvBtn2.UseColumnTextForButtonValue = true;
            dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn2.Width = 70;
            dataGridView1.Columns.Add(dgvBtn2);
        }
        void formTemizle() {
            CheckBox[] chk = { checkBox1,checkBox2,checkBox3,checkBox4,checkBox5,checkBox6,checkBox7,checkBox8,
            checkBox9,checkBox10,checkBox11,checkBox12,checkBox13,checkBox14,checkBox15,checkBox16};
            TextBox[] tb = { textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8,
            textBox9,textBox10,textBox11,textBox12,textBox13,textBox14,textBox15,textBox16};
            for (int i = 0; i < 16; i++)
            {
                if (chk[i].Checked == true)                
                    chk[i].Checked = false;
                
                if (!string.IsNullOrEmpty(tb[i].Text))
                    tb[i].Text = "";
                
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 14)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                label4.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();   
                label3.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString() +  dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                label7.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                label5.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                label6.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
                ogrID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();

                panel1.Visible = true;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 15)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                OgrenciProfil prfl = new OgrenciProfil();
                prfl.OgrID = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                prfl.ShowDialog();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckBox[] chk = { checkBox1,checkBox2,checkBox3,checkBox4,checkBox5,checkBox6,checkBox7,checkBox8,
            checkBox9,checkBox10,checkBox11,checkBox12,checkBox13,checkBox14,checkBox15,checkBox16};
            TextBox[] tb = { textBox1,textBox2,textBox3,textBox4,textBox5,textBox6,textBox7,textBox8,
            textBox9,textBox10,textBox11,textBox12,textBox13,textBox14,textBox15,textBox16};
            Label[] lb = { lbl1,lbl2,lbl3,lbl4,lbl5,lbl6,lbl7,lbl8,
            lbl9,lbl10,lbl11,lbl12,lbl13,lbl14,lbl15,lbl16};
            for (int i = 0; i < 16; i++)
            {
                if (chk[i].Checked == true)
                {
                    if (baglan.State != System.Data.ConnectionState.Open)
                        baglan.Open();
                    SqlCommand ekle = new SqlCommand("insert into VeliGorusu (ogrId, GorusId, Gorus, Konu) values (@a1, @a2, @a3, @a4)", baglan);
                    ekle.Parameters.AddWithValue("@a2", i+1);
                    ekle.Parameters.AddWithValue("@a3", tb[i].Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a1", ogrID);
                    ekle.Parameters.AddWithValue("@a3", lb[i].Text); 
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                }
            }
            panel1.Visible = false;
            formTemizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            formTemizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = true;
                string filtre = "Select ö.ID, ö.SozNo 'SÖZ NO',ö.TCKN 'TC KİMLİK',ö.Adi+' '+ö.Soyadi ÖĞRENCİ,ö.Program 'PROGRAM',ö.Devre DEVRE,ö.Snf SINIF From Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.okulid='"+okulid+"'";
                if (comboBoxSube.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "and";
                    }
                    filtre += " ş.ŞubeAdi='" + comboBoxSube.Text+"'";
                    degisken = true;
                }
                if (comboBoxDevre.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Devre='" + comboBoxDevre.Text + "'";
                    degisken = true;
                }
                if (comboBoxProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Program='" + comboBoxProgram.Text + "'";
                    degisken = true;
                }
                if (comboBoxSnf.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Snf=" + comboBoxSnf.Text + "'";
                    degisken = true;
                }
               
                if (!string.IsNullOrEmpty(textBoxAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Adi='" + textBoxAd.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Soyadi='" + textBoxSoyad.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxSN.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.SozNo='" + textBoxSN.Text + "'";
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
                if (dateTimePickerBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.KayitTarihi between '" + dateTimePickerBas.Value.ToString("yyyyMMdd") + "' and '" + dateTimePickerBit.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                query = filtre;
                panel3.Visible = false;
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
        SqlCommand komut;
        private void VeliGorusuGirisi_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBoxDevre.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBoxSnf.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBoxProgram.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
          
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {

                comboBoxSube.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
        }
    }
}
