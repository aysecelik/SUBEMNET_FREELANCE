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
    public partial class OnKayitGorusmeler : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        public OnKayitGorusmeler()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
           
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

        }
        private void buttonAra_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
              
                string filtre = "Select ş.ŞubeAdi, OnKayit.Adi, OnKayit.Soyadi, OnKayit.TCKN, OnKayitGorusme.Gorusen, OnKayitGorusme.GrsmTur, OnKayitGorusme.Tarih from" +
                    " OnKayitGorusme inner join OnKayit on OnKayit.ID=OnKayitGorusme.ogrId join Sube  ş on ş.ID=OnKayit.Sube Where ş.Okulid='" + okulid + "'";                
              
                if (comboBoxSube.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " ş.ŞubeAdi = '" + comboBoxSube.Text+"'";
                    degisken = true;
                }
                if (comboBoxDevre.SelectedIndex != -1)
                {
                    if (comboBoxSube.SelectedIndex != -1)
                    {
                        filtre += " AND ";
                    }
                    filtre += " OnKayit.Devre = '" + comboBoxDevre.Text + "'";
                    degisken = true;
                }
              
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " OnKayitGorusme.GrsmTuru= '" +textBox1.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " OnKayit.Adi = '" + textBoxAd.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " OnKayit.Soyadi = '" + textBoxSoyad.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxTC.Text))
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " OnKayit.TCKN = '" + textBoxTC.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxKgno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += "AND";
                    }
                    filtre += " OnKayit.KGNO = '" + textBoxKgno.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (dateTimePicker1.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "  OnKayitGorusme.Tarih between '" + dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }             
                query = filtre;
                panel5.Visible = false;
                griddoldur();

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
        List<int> subeid = new List<int>();

        SqlCommand komut;
        int okulid = Form1.okulid;
        private void OnKayitGorusmeler_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBoxSube.Items.Add(oku4[0].ToString());

                subeid.Add((int)oku4[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBoxDevre2.Items.Add(oku[0].ToString());
                comboBoxDevre.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            query = null;
            griddoldur();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into OnKayitGorusme (ogrId,  GrsmTur, Tarih) " +
                    "values (@a1,  @a3, @a4)", baglan);
                ekle.Parameters.AddWithValue("@a3", textBox2.Text);
                ekle.Parameters.AddWithValue("@a4", dateTimePicker3.Value.ToString("yyyyMMdd HH:mm:ss"));                
                ekle.Parameters.AddWithValue("@a1", textBoxTCKN2.Text);

                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
                panel1.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            query = "Select ş.ŞubeAdi, OnKayit.Adi, OnKayit.Soyadi, OnKayit.TCKN, OnKayitGorusme.Gorusen, OnKayitGorusme.GrsmTur, OnKayitGorusme.Tarih from" +
                    " OnKayitGorusme inner join OnKayit on OnKayit.ID=OnKayitGorusme.ogrId join Sube  ş on ş.ID=OnKayit.Sube Where ş.Okulid='" + okulid + "' and OnKayitGorusme.Tarih='" +DateTime.Now.ToString("yyyyMMdd HH:mm:ss")+ "'";

            griddoldur();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
