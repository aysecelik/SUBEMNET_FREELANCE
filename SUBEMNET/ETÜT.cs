using iTextSharp.text;
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
                da = new SqlDataAdapter("Select ID, Sube from Etut", baglan);
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
            textBox5.Text = "";
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
                    filtre += " Sube='" + comboBoxSube.Text+"'";
                    degisken = true;
                }
                if (comboBoxDers.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Ders='" + comboBoxDers.Text+"'";
                    degisken = true;
                }
                if (comboBoxDerslik.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Derslik='" + comboBoxDerslik.Text+"'";
                    degisken = true;
                }
                if (comboBoxOgretmen.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Ogretmen='" + comboBoxOgretmen.Text+"'";
                    degisken = true;
                }
                if (comboBoxSnf.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Snf='" + comboBoxSnf.Text+"'";
                    degisken = true;
                }
                if (comboBoxTur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Tur='" + comboBoxTur.Text+"'";
                    degisken = true;
                }
                if (comboBoxYoklama.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " Yoklama='" + comboBoxYoklama.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxEK.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " EtutKodu='" + textBoxEK.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxOgrAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrAd='" + textBoxOgrAd.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxOgrSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " OgrSoyad='" + textBoxOgrSoyad.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBoxSozNo.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " SozNo='" + textBoxSozNo.Text+"'";
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
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into Etut (Sube, Ders, Derslik, Snf, Tur, OgrAd, OgrSoyad, SozNo, Tarih) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9)", baglan);
                ekle.Parameters.AddWithValue("@a1", comboBox7.Text);
                ekle.Parameters.AddWithValue("@a2", comboBox6.Text);
                ekle.Parameters.AddWithValue("@a3", textBox5.Text);
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
            panel3.Visible = false;
        }


        private void buttonSec_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int SeciliID = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand com = new SqlCommand("SELECT*FROM Etut WHERE ID=" + SeciliID + " ");
                com.Connection = baglan;
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    labelSube.Text = dr["Sube"].ToString();
                    labelEtutKodu.Text = dr["ID"].ToString();
                    labelTur.Text = dr["Tur"].ToString();
                    labelDerslik.Text = dr["Derslik"].ToString();
                    labelSnf.Text = dr["Snf"].ToString();
                    labelDers.Text = dr["Ders"].ToString();
                }
                panel5.Visible = true;
                dr.Close();
                da = new SqlDataAdapter("Select ID, Ad, Soyad from Ogrenci where ID=(Select OgrId from OgrenciEtut Where EtutId=" + SeciliID + ")", baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridViewOgr.DataSource = dt;
                baglan.Close();
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                dataGridViewOgr.Columns.Add(chk);
                chk.HeaderText = "Seç";
                chk.Name = "chk";

            }
            else
                MessageBox.Show("HATA. LÜTFEN LİSTEDEN ETÜT SEÇİNİZ.");
        }

        private void Etut_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox4.Text);
            textBox4.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int secim = listBox1.SelectedIndex;
            if (secim != -1)
            {
                listBox1.Items.RemoveAt(secim);
            }
            else
            {
                MessageBox.Show("Seçim Yapın!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO EtutYoklama (OgrNumara, EtutID, Tarih) VALUES(@a1, @a2, @a3)", baglan);

                foreach (string a in listBox1.Items)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", labelEtutKodu.Text);
                    ekle.Parameters.AddWithValue("@a3", dateTimePicker2.Value);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
            else
            {
                MessageBox.Show("Listbox' ta Veri Yok");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            listBox1.Items.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            List<string> selectedItem = new List<string>();
            DataGridViewRow drow = new DataGridViewRow();            
            for (int i = 0; i < dataGridViewOgr.Rows.Count; i++)
            {              
                drow = dataGridViewOgr.Rows[i];
                if (Convert.ToBoolean(drow.Cells["chk"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedItem.Add(id); //seçiliyse listeye ekle
                }

            }
            foreach(string s in selectedItem)
            {
                listBox1.Items.Add(s);
            }
            panel7.Visible = false;
        }
    }
}