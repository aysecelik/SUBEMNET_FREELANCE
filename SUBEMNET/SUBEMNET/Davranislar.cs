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
    public partial class Davranislar : Form
    {
        public Davranislar()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=.;Initial Catalog=SUBEMNET;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        string query;
        private void Davranislar_Load(object sender, EventArgs e)
        {
            griddoldur();
        }
        void griddoldur()
        {
            //baglan.Open();
            //if (query != null)
            //    da = new SqlDataAdapter(query, baglan);
            //else
            //    da = new SqlDataAdapter("Select Sube, Ad, Aciklama, Baslangic, Bitis from Davranis", baglan);
            //dt = new DataTable();
            //cmdb = new SqlCommandBuilder(da);
            //da.Fill(dt);
            //dataGridView1.DataSource = dt;
            //baglan.Close();

        }
        void temizle()
        {
            comboBoxSube.Text = "";
            comboBoxTur.Text = "";
            comboBoxAralik.Text = "";
            textBoxAciklama.Text = "";
            textBoxAd.Text = "";
            textBoxYazar.Text = "";
            checkBoxPuan.Checked = false;
            checkBoxZorunlu.Checked = false;
            dateTimePickerBas.Value = DateTime.Now;
            dateTimePickerBit.Value = DateTime.Now;
        }
        void temizle2()
        {
            comboBox2.Text = "";
            comboBoxDav.Text = "";
            comboBoxDavAd.Text = "";
            comboBox1.Text = "";
            comboBoxSnf.Text = "";
            comboBoxDevre.Text = "";
            comboBoxOgretmenAd.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into Davranis (Sube, Ad, Aciklama, HedefTur, TarihAraligi, Zorunluluk, PuanVerme, Yazar, Baslangic, Bitis) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9)", baglan);
                ekle.Parameters.AddWithValue("@a1", comboBoxSube.Text);
                ekle.Parameters.AddWithValue("@a2", textBoxAd.Text);
                ekle.Parameters.AddWithValue("@a3", textBoxAciklama.Text);
                ekle.Parameters.AddWithValue("@a4", comboBoxTur.Text);
                ekle.Parameters.AddWithValue("@a5", comboBoxAralik.Text);
                if(checkBoxZorunlu.Checked==true)
                    ekle.Parameters.AddWithValue("@a6", 1);
                else
                    ekle.Parameters.AddWithValue("@a6", 0);
                if (checkBoxPuan.Checked == true)
                    ekle.Parameters.AddWithValue("@a7", 1);
                else
                    ekle.Parameters.AddWithValue("@a7", 0);
                ekle.Parameters.AddWithValue("@a8", textBoxYazar.Text);
                ekle.Parameters.AddWithValue("@a9", dateTimePickerBas.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a10", dateTimePickerBit.Value.ToString("yyyyMMdd"));
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
                panel3.Visible = false;
                temizle();
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
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

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = false;
                string filtre = "Select * From Davranis Where";
                if (comboBox2.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += "and";
                    }
                    filtre += " Sube=" + comboBox2.Text;
                    degisken = true;
                }
                if (comboBoxDav.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Davranis=" + comboBoxDav.Text;
                    degisken = true;
                }
                if (comboBoxDavAd.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " DavranisAd=" + comboBoxDavAd.Text;
                    degisken = true;
                }
                if (comboBox1.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Aralik=" + comboBox1.Text;
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
                if (comboBoxDevre.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Devre=" + comboBoxDevre.Text;
                    degisken = true;
                }
                if (comboBoxOgretmenAd.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgretmenAd=" + comboBoxOgretmenAd.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrAd=" + textBox1.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox2.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrSoyad=" + textBox2.Text;
                    degisken = true;
                }
                if (dateTimePickerBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Tarih between '" + dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel3.Visible = false;
                griddoldur();
                temizle2();

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
            temizle();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            temizle2();
        }

        private void buttonSec_Click(object sender, EventArgs e)
        {
            //sestanim
            panel5.Visible = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }
    }
}
