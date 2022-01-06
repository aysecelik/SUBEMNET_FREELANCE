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
    public partial class Etut : Form
    {
        SqlConnection baglan = new SqlConnection(@"Data Source=.;Initial Catalog=SUBEMNET;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        string query;
        public Etut()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select EtutKodu, Sube, OgrAd, OgrSoyad, Ogretmen, from Etut", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

        }
        void temizle()
        {
            comboBox7.Text = "";
            comboBox6.Text = "";
            comboBox1.Text = "";
            comboBox4.Text = "";
            comboBox3.Text = "";
            textBox3.Text = "";
            textBox2.Text = "";
            textBox1.Text = "";
            dateTimePicker1.Value = DateTime.Now;
        }
        void temizle2()
        {
            comboBoxSube.Text = "";
            comboBoxDers.Text = "";
            comboBoxDerslik.Text = "";
            comboBoxOgretmen.Text = "";
            comboBoxSnf.Text = "";
            comboBoxTur.Text = "";
            comboBoxYoklama.Text = "";
            textBoxEK.Text = "";
            textBoxOgrAd.Text = "";
            textBoxOgrSoyad.Text = "";
            textBoxSozNo.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            temizle2();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = false;
                string filtre = "Select * From Etut Where";
                if (comboBoxSube.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "and";
                    }
                    filtre += " Sube=" + comboBoxSube.Text;
                    degisken = true;
                }
                if (comboBoxDers.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Ders=" + comboBoxDers.Text;
                    degisken = true;
                }
                if (comboBoxDerslik.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Derslik=" + comboBoxDerslik.Text;
                    degisken = true;
                }
                if (comboBoxOgretmen.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Ogretmen=" + comboBoxOgretmen.Text;
                    degisken = true;
                }
                if (comboBoxSnf.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Snf=" + comboBoxSnf.Text;
                    degisken = true;
                }
                if (comboBoxTur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Tur=" + comboBoxTur.Text;
                    degisken = true;
                }
                if (comboBoxYoklama.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Yoklama=" + comboBoxYoklama.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBoxEK.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " EtutKodu=" + textBoxEK.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBoxOgrAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrAd=" + textBoxOgrAd.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBoxOgrSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrSoyad=" + textBoxOgrSoyad.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBoxSozNo.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " SozNo=" + textBoxSozNo.Text;
                    degisken = true;
                }
                if (dateTimePickerBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Tarih between '" + dateTimePickerBas.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePickerBit.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel1.Visible = false;
                griddoldur();
                temizle2();

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            temizle2();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into Etut (Sube, Ders, Derslik, Snf, Tur, OgrAd, OgrSoyad, SozNo, Tarih) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9)", baglan);
                ekle.Parameters.AddWithValue("@a1", comboBox7.Text);
                ekle.Parameters.AddWithValue("@a2", comboBox6.Text);
                ekle.Parameters.AddWithValue("@a3", comboBox1.Text);
                ekle.Parameters.AddWithValue("@a4", comboBox4.Text);
                ekle.Parameters.AddWithValue("@a5", comboBox3.Text);
                ekle.Parameters.AddWithValue("@a6", textBox3.Text);
                ekle.Parameters.AddWithValue("@a7", textBox2.Text);
                ekle.Parameters.AddWithValue("@a8", textBox1.Text);
                ekle.Parameters.AddWithValue("@a9", dateTimePicker1.Value.ToString("yyyyMMdd"));
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
                temizle();
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void Etut_Load(object sender, EventArgs e)
        {

        }

        private void buttonSec_Click(object sender, EventArgs e)
        {

        }
    }
}
