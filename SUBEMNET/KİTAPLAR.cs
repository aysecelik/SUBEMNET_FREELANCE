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
    public partial class KİTAPLAR : Form
    {
        public KİTAPLAR()
        {
            InitializeComponent();
        }
        private void KİTAPLAR_Load(object sender, EventArgs e)
        {
            comboBox21.Items.Clear();
            comboBox22.Items.Clear();

            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox5.Items.Add(oku[0].ToString());
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
                kütüphane.Add((int)oku2[1]);

            }
            baglan.Close();
            komut = new SqlCommand("Select k.Yayınevi from Yayınevi k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {

                comboBox18.Items.Add(oku3[0].ToString());
            }

            baglan.Close();
            komut = new SqlCommand("Select k.Yazar from Yazar k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox20.Items.Add(oku4[0].ToString());

            }

            baglan.Close();
            komut = new SqlCommand("Select k.Dil from Dil k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {

                comboBox1.Items.Add(oku5[0].ToString());


            }

            baglan.Close();
            komut = new SqlCommand("Select k.Tür from Tür k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku7 = komut.ExecuteReader();
            while (oku7.Read())
            {
                comboBox19.Items.Add(oku7[0].ToString());
            }

            baglan.Close();
            komut = new SqlCommand("Select k.Seri from Seri k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku8 = komut.ExecuteReader();
            while (oku8.Read())
            {
                comboBox17.Items.Add(oku8[0].ToString());
            }

            baglan.Close();
            komut = new SqlCommand("Select k.Seviye from Seviye k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku9 = komut.ExecuteReader();
            while (oku9.Read())
            {
                comboBox14.Items.Add(oku9[0].ToString());
            }

            baglan.Close();
            komut = new SqlCommand("Select k.Raf from Raf k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku10 = komut.ExecuteReader();
            while (oku10.Read())
            {
                comboBox15.Items.Add(oku10[0].ToString());
            }

            baglan.Close();
            comboBox2.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE SEÇİMİ YAPINIZ.");
            comboBox8.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox9.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox12.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox13.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox3.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox6.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox7.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox10.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");
            comboBox11.Items.Add("LÜTFEN ÖNCELİKLE ŞUBE VE KÜTÜPHANE SEÇİMİ YAPINIZ.");

            comboBox16.Items.Add("HASARLI");
            comboBox16.Items.Add("KAYIP");




        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "YAYINEVİ";
            }
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        List<int> subeid = new List<int>();
        List<int> kütüphaneid = new List<int>();
        List<int> kütüphane = new List<int>();


        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox8.Text) == true || string.IsNullOrEmpty(comboBox6.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true)
            {
                MessageBox.Show("ŞUBE, KÜTÜPHANE,YAZAR,YAYINEVİ, TÜR VE KİTAP ADI BİLGİLERİNİN GİRİLMESİ ZORUNLUDUR. NOT:BOŞ BIRAKILAN BARKOD KODLARINA OTOMATİK OLARAK ID ATANMAKTADIR.");
            }
            else
            {
                try
                {

                    bool degisken = false;
                    string filtre = "insert into  Kitaplar(";
                    string values = "Values (";

                    if (string.IsNullOrEmpty(comboBox5.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Sube ";
                        values += "'" + subeid[comboBox5.SelectedIndex] + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Kütüphane ";
                        values += "'" + kütüphaneid[comboBox2.SelectedIndex] + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " KitapAdı ";
                        values += "'" + textBox2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Yayınevi ";
                        values += "'" + comboBox8.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox9.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " İkinciYazar ";
                        values += "'" + comboBox9.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox4.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Doc ";
                        values += "'" + textBox4.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox1.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " BasımYılı ";
                        values += "'" + maskedTextBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox5.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " SayfaSayısı ";
                        values += "'" + textBox5.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox9.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Çeviren ";
                        values += "'" + textBox9.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Dil ";
                        values += "'" + comboBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox13.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Dolap ";
                        values += "'" + comboBox13.Text + "'";
                    }
                    if (string.IsNullOrEmpty(textBox14.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Sıra ";
                        values += "'" + textBox14.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Barkod ";
                        values += "'" + textBox3.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Tür ";
                        values += "'" + comboBox3.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Yazar ";
                        values += "'" + comboBox6.Text + "'";
                    }
                    if (string.IsNullOrEmpty(comboBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Seri ";
                        values += "'" + comboBox7.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " ISBN ";
                        values += "'" + textBox8.Text + "'";
                    }
                    if (string.IsNullOrEmpty(textBox11.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " BaskıSayısı ";
                        values += "'" + textBox11.Text + "'";
                    }
                    if (string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " YayınaHazırlayan ";
                        values += "'" + textBox10.Text + "'";
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Editör ";
                        values += "'" + textBox12.Text + "'";
                    }
                    if (string.IsNullOrEmpty(comboBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Seviye ";
                        values += "'" + comboBox10.Text + "'";
                    }
                    if (string.IsNullOrEmpty(comboBox11.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Raf ";
                        values += "'" + comboBox11.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox13.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Adet ";
                        values += "'" + textBox13.Text + "'";
                    }

                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Tarih,Kayıp,Hasarlı,TeslimDurum ";
                    values += "'" + DateTime.Now.ToString("dd.MM.yyyy") + "','" + Convert.ToBoolean(false) + "','" + Convert.ToBoolean(false) + "','" + Convert.ToBoolean(true) + "'";



                    filtre += ")";
                    values += ")";
                    filtre += values;
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    string baglanid = "";
                    if (string.IsNullOrEmpty(textBox3.Text) == true)
                    {
                        SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Kitaplar')", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            baglanid = oku.GetValue(0).ToString();
                        }
                        baglan.Close();
                        baglan.Open();
                        SqlCommand komut2 = new SqlCommand("update Kitaplar set Barkod=@a1 where ID=@a2", baglan);
                        komut2.Parameters.AddWithValue("@a1", baglanid);
                        komut2.Parameters.AddWithValue("@a2", baglanid);
                        komut2.ExecuteNonQuery();
                        baglan.Close();
                    }
                    comboBox5.Text = "";
                    comboBox2.Text = "";
                    comboBox8.Text = "";
                    comboBox9.Text = "";
                    comboBox12.Text = "";
                    comboBox13.Text = "";
                    comboBox23.Text = "";
                    comboBox3.Text = "";
                    comboBox6.Text = "";
                    comboBox7.Text = "";
                    comboBox10.Text = "";
                    comboBox11.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    maskedTextBox1.Text = "";
                    textBox5.Text = "";
                    textBox9.Text = "";
                    textBox14.Text = "";
                    textBox1.Text = "";
                    textBox15.Text = "";
                    textBox3.Text = "";
                    textBox8.Text = "";
                    textBox11.Text = "";
                    textBox10.Text = "";
                    textBox12.Text = "";
                    textBox13.Text = "";

                    panel2.Visible = false;



                }
                catch (Exception A)
                {
                    baglan.Close();
                    MessageBox.Show(A.ToString());
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            comboBox5.Text = "";
            comboBox2.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox12.Text = "";
            comboBox13.Text = "";
            comboBox23.Text = "";
            comboBox3.Text = "";
            comboBox6.Text = "";
            comboBox7.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            maskedTextBox1.Text = "";
            textBox5.Text = "";
            textBox9.Text = "";
            textBox14.Text = "";
            textBox1.Text = "";
            textBox15.Text = "";
            textBox3.Text = "";
            textBox8.Text = "";
            textBox11.Text = "";
            textBox10.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";



        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            comboBox21.Text = "";
            comboBox22.Text = "";
            comboBox18.Text = "";
            comboBox1.Text = "";
            comboBox4.Text = "";
            comboBox16.Text = "";
            comboBox19.Text = "";
            comboBox20.Text = "";
            comboBox17.Text = "";
            comboBox14.Text = "";
            comboBox15.Text = "";
            textBox24.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox18.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
        }
        int okulid = Form1.okulid;
        private void button8_Click(object sender, EventArgs e)
        {

            griddoldur();
        }
        void griddoldur()
        {
            try
            {

                bool degisken = true;
                string filtre = "Select t.ID,k.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Doc 'DDC/DOS',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Dolap DOLAP,t.Raf RAF,T.Seviye SEVİYE,t.Barkod 'BARKOD' from Kitaplar t join Sube ş on ş.ID=t.Sube join Kütüphane k on k.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";
                if (string.IsNullOrEmpty(textBox24.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.KitapAdı ='" + textBox24.Text + "'";
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
                if (string.IsNullOrEmpty(textBox23.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Barkod = " + textBox23.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox18.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.ISBN = '" + textBox18.Text + "'";
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
                if (string.IsNullOrEmpty(comboBox18.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Yayınevi = '" + comboBox18.Text + "'";
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
                if (string.IsNullOrEmpty(comboBox19.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tür = '" + comboBox19.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox20.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Yazar = '" + comboBox20.Text + "'";
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

                if (string.IsNullOrEmpty(comboBox21.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Sube =" + subeid[comboBox21.SelectedIndex];
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox22.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Kütüphane =" + kütüphane[comboBox22.SelectedIndex];
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Kayıp =" + Convert.ToBoolean(false);
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Hasarlı =" + Convert.ToBoolean(false);
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.TeslimDurum =" + Convert.ToBoolean(false);
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.TeslimDurum =" + Convert.ToBoolean(true);
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
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 10)
            {
                bool kayıp = false;
                komut = new SqlCommand("Select Hasarlı from Kitaplar where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    kayıp = (bool)oku[0];
                }
                baglan.Close();
                if (kayıp == false)
                {
                    DialogResult result = MessageBox.Show("HASARLI OLARAK GÜNCELLEME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "HASARLI OLARAK GÜNCELLE", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " KİTABINI HASARLI OLARAK GÜNCELLEMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "KİTAP HASARLI DURUM GÜNCELLEME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                baglan.Open();
                                SqlCommand degistir = new SqlCommand("update Kitaplar set Hasarlı=@a1,Durum=@a3,DurumTarih=@a4 where ID=@a2 ", baglan);
                                degistir.Parameters.AddWithValue("@a2", id);
                                degistir.Parameters.AddWithValue("@a1", Convert.ToBoolean(true));
                                degistir.Parameters.AddWithValue("@a3", "HASARLI");
                                degistir.Parameters.AddWithValue("@a4", DateTime.Now.ToString("dd.MM.yyyy"));

                                degistir.ExecuteNonQuery();
                                MessageBox.Show("KAYIP DURUMU GÜNCELLENDİ.");
                                baglan.Close();
                            }
                            catch (Exception a)
                            {
                                baglan.Close();
                                MessageBox.Show(a.ToString());
                            }


                        }

                    }
                }
                else
                {
                    MessageBox.Show("BU KİTAP İÇİN BİR HASAR KAYDI ZATEN OLUŞTURULMUŞ.");
                }


            }
            if (dataGridView1.CurrentCell.ColumnIndex == 11)
            {
                bool kayıp = false;
                komut = new SqlCommand("Select Kayıp from Kitaplar where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    kayıp = (bool)oku[0];
                }
                baglan.Close();
                if (kayıp == false)
                {
                    DialogResult result = MessageBox.Show("KAYIP GÜNCELLEME İŞLEMİNE DEVAM ETMEK İSTİYOR MUSUNUZ?", "KAYIP OLARAK GÜNCELLE", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " KİTABINI KAYIP OLARAK KAYDETMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "KİTAP KAYIP DURUMU GÜNCELLEME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                baglan.Open();
                                SqlCommand degistir = new SqlCommand("update Kitaplar set Kayıp=@a1,Durum=@a3,DurumTarih=@a4 where ID=@a2 ", baglan);
                                degistir.Parameters.AddWithValue("@a2", id);
                                degistir.Parameters.AddWithValue("@a1", Convert.ToBoolean(true));
                                degistir.Parameters.AddWithValue("@a3", "KAYIP");
                                degistir.Parameters.AddWithValue("@a4", DateTime.Now.ToString("dd.MM.yyyy"));
                                degistir.ExecuteNonQuery();
                                MessageBox.Show("KAYIP DURUMU GÜNCELLENDİ.");
                                baglan.Close();
                            }
                            catch (Exception a)
                            {
                                baglan.Close();
                                MessageBox.Show(a.ToString());
                            }

                        }
                    }
                }
                if (kayıp == true)
                {
                    DialogResult result = MessageBox.Show("BU KİTAP KAYIP. KİTAP BULUNDU MU", "KİTAP BULUNDU OLARAK GÜNCELLE", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " KİTABIN KAYIP KAYDINI SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "KİTAP BULUNDU OLARAK GÜNCELLE", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                baglan.Open();
                                SqlCommand degistir = new SqlCommand("update Kitaplar set Kayıp=@a1,Durum=@a3,DurumTarih=@a4 where ID=@a2 ", baglan);
                                degistir.Parameters.AddWithValue("@a2", id);
                                degistir.Parameters.AddWithValue("@a1", Convert.ToBoolean(false));
                                degistir.Parameters.AddWithValue("@a3", "");
                                degistir.Parameters.AddWithValue("@a4", "");
                                degistir.ExecuteNonQuery();
                                MessageBox.Show("KAYIP DURUMU GÜNCELLENDİ.");
                                baglan.Close();
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
            if (dataGridView1.CurrentCell.ColumnIndex == 12)
            {

                try
                {
                    komut = new SqlCommand("select * from Kitaplar where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();

                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        textBox16.Text = comboBox21.Items[subeid.IndexOf((int)oku[1])].ToString();
                        textBox17.Text = comboBox22.Items[kütüphane.IndexOf((int)oku[2])].ToString();
                        textBox2.Text = oku[3].ToString();
                        comboBox8.Text = oku[4].ToString();
                        if (oku[5] != DBNull.Value)
                        {
                            comboBox9.Text = oku[5].ToString();
                        }
                        if (oku[6] != DBNull.Value)
                        {
                            textBox4.Text = oku[6].ToString();
                        }

                        if (oku[7] != DBNull.Value)
                        {
                            maskedTextBox1.Text = oku[7].ToString();
                        }

                        if (oku[8] != DBNull.Value)
                        {
                            textBox5.Text = oku[8].ToString();
                        }
                        if (oku[9] != DBNull.Value)
                        {
                            textBox9.Text = oku[9].ToString();
                        }
                        if (oku[10] != DBNull.Value)
                        {
                            comboBox12.Text = oku[10].ToString();
                        }
                        if (oku[11] != DBNull.Value)
                        {
                            comboBox13.Text = oku[11].ToString();
                        }
                        if (oku[12] != DBNull.Value)
                        {
                            textBox14.Text = oku[12].ToString();
                        }
                        if (oku[13] != DBNull.Value)
                        {
                            textBox3.Text = oku[13].ToString();
                        }
                        if (oku[14] != DBNull.Value)
                        {
                            comboBox3.Text = oku[14].ToString();
                        }
                        if (oku[15] != DBNull.Value)
                        {
                            comboBox6.Text = oku[15].ToString();
                        }
                        if (oku[16] != DBNull.Value)
                        {
                            comboBox7.Text = oku[16].ToString();
                        }
                        if (oku[17] != DBNull.Value)
                        {
                            textBox8.Text = oku[17].ToString();
                        }
                        if (oku[18] != DBNull.Value)
                        {
                            textBox11.Text = oku[18].ToString();
                        }
                        if (oku[19] != DBNull.Value)
                        {
                            textBox10.Text = oku[19].ToString();
                        }
                        if (oku[20] != DBNull.Value)
                        {
                            textBox12.Text = oku[20].ToString();
                        }
                        if (oku[21] != DBNull.Value)
                        {
                            comboBox10.Text = oku[21].ToString();
                        }
                        if (oku[22] != DBNull.Value)
                        {
                            comboBox11.Text = oku[22].ToString();
                        }
                        if (oku[23] != DBNull.Value)
                        {
                            textBox13.Text = oku[23].ToString();
                        }
                        if (oku[26] != DBNull.Value)
                        {
                            comboBox23.Text = oku[26].ToString();
                        }
                        if (oku[27] != DBNull.Value)
                        {
                            textBox1.Text = oku[27].ToString();
                        }
                        if (oku[28] != DBNull.Value)
                        {
                            textBox15.Text = oku[28].ToString();
                        }

                    }
                    baglan.Close();
                    panel2.Visible = true;
                    panel1.Visible = true;
                    panel5.Visible = true;
                    panel6.Visible = true;
                    label61.Visible = false;
                    button11.Visible = false;
                    panel4.Enabled = false;
                    textBox16.Visible = true;
                    textBox17.Visible = true;
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());

                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 13)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "KİTAP SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " KİTABINI SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "KİTAP SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM Kitaplar WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", id);
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
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

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            label61.Visible = true;
            panel5.Visible = false;
            panel6.Visible = false;
            button11.Visible = true;
            panel4.Enabled = true;
            textBox16.Visible = false;
            textBox17.Visible = false;

        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
            textBox7.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "KÜTÜPHANE";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "YAZAR";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "TÜR";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "YAZAR";
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "SERİ";
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "DİL";
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "DOLAP";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "SEVİYE";
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE VE KÜTÜPHANE BİLGİSİ SEÇİLMELİDİR.");
            }
            else
            {
                panel7.Visible = true;
                label64.Text = "RAF";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == true)
            {
                MessageBox.Show("EKLEME YAPABİLMEK İÇİN VERİYİ GİRMENİZ ZORUNLUDUR.");
            }
            else
            {
                if (label64.Text == "KÜTÜPHANE")
                {
                    try
                    {
                        kütüphaneid.Clear();
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Kütüphane (Sube, Kütüphane) values (@p1, @p2)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");
                        comboBox2.Items.Clear();
                        komut = new SqlCommand("Select Kütüphane,ID from Kütüphane where Sube='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox2.Items.Add(oku[0].ToString());
                            kütüphaneid.Add((int)oku[1]);

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "YAZAR")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Yazar (Sube, Yazar,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox9.Items.Clear();
                        comboBox6.Items.Clear();
                        komut = new SqlCommand("Select Yazar from Yazar where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox9.Items.Add(oku[0].ToString());
                            comboBox6.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "DİL")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Dil (Sube, Dil,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox12.Items.Clear();
                        komut = new SqlCommand("Select Dil from Dil where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox12.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "DOLAP")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Dolap (Sube, Dolap,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox13.Items.Clear();
                        komut = new SqlCommand("Select Dolap from Dolap where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox13.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "SEVİYE")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Seviye (Sube, Seviye,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox10.Items.Clear();
                        komut = new SqlCommand("Select Seviye from Seviye where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox10.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "SERİ")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Seri (Sube, Seri,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox7.Items.Clear();
                        komut = new SqlCommand("Select Seri from Seri where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox7.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "RAF")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Raf (Sube, Raf,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox11.Items.Clear();
                        komut = new SqlCommand("Select Raf from Raf where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox11.Items.Add(oku[0].ToString());

                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "TÜR")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Tür (Sube, Tür,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox3.Items.Clear();
                        komut = new SqlCommand("Select Tür from Tür where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox3.Items.Add(oku[0].ToString());


                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
                if (label64.Text == "YAYINEVİ")
                {
                    try
                    {
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into Yayınevi (Sube, Yayınevi,Kütüphane) values (@p1, @p2,@p3)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                        komutkaydet.Parameters.AddWithValue("@p3", kütüphaneid[comboBox2.SelectedIndex]);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı");

                        comboBox8.Items.Clear();

                        komut = new SqlCommand("Select Yayınevi from Yayınevi where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            comboBox8.Items.Add(oku[0].ToString());


                        }
                        baglan.Close();
                        panel7.Visible = false;
                        textBox7.Text = "";

                    }
                    catch (Exception a)
                    {

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == false)
            {
                kütüphaneid.Clear();
                comboBox2.Items.Clear();
                komut = new SqlCommand("Select Kütüphane,ID from Kütüphane where Sube='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox2.Items.Add(oku[0].ToString());
                    kütüphaneid.Add((int)oku[1]);

                }
                baglan.Close();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == false && string.IsNullOrEmpty(comboBox2.Text) == false)
            {

                comboBox9.Items.Clear();
                comboBox6.Items.Clear();
                komut = new SqlCommand("Select Yazar from Yazar where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox9.Items.Add(oku[0].ToString());
                    comboBox6.Items.Add(oku[0].ToString());

                }
                baglan.Close();
                comboBox12.Items.Clear();
                komut = new SqlCommand("Select Dil from Dil where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku4 = komut.ExecuteReader();
                while (oku4.Read())
                {
                    comboBox12.Items.Add(oku4[0].ToString());

                }
                baglan.Close();
                comboBox13.Items.Clear();
                komut = new SqlCommand("Select Dolap from Dolap where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku5 = komut.ExecuteReader();
                while (oku5.Read())
                {
                    comboBox13.Items.Add(oku5[0].ToString());
                }
                baglan.Close();
                comboBox10.Items.Clear();
                komut = new SqlCommand("Select Seviye from Seviye where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku6 = komut.ExecuteReader();
                while (oku6.Read())
                {
                    comboBox10.Items.Add(oku6[0].ToString());

                }
                baglan.Close();
                comboBox7.Items.Clear();
                komut = new SqlCommand("Select Seri from Seri where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku7 = komut.ExecuteReader();
                while (oku7.Read())
                {
                    comboBox7.Items.Add(oku7[0].ToString());

                }
                baglan.Close();
                comboBox11.Items.Clear();
                komut = new SqlCommand("Select Raf from Raf where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku8 = komut.ExecuteReader();
                while (oku8.Read())
                {
                    comboBox11.Items.Add(oku8[0].ToString());

                }
                baglan.Close();
                comboBox3.Items.Clear();
                komut = new SqlCommand("Select Tür from Tür where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku9 = komut.ExecuteReader();
                while (oku9.Read())
                {
                    comboBox3.Items.Add(oku9[0].ToString());
                }
                baglan.Close();
                comboBox8.Items.Clear();
                komut = new SqlCommand("Select Yayınevi from Yayınevi where Sube='" + subeid[comboBox5.SelectedIndex] + "' and Kütüphane='" + kütüphaneid[comboBox2.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku10 = komut.ExecuteReader();
                while (oku10.Read())
                {
                    comboBox8.Items.Add(oku10[0].ToString());
                }
                baglan.Close();
            }
        }

        private void comboBox5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == false)
            {
                try
                {

                    bool degisken = true;
                    string filtre = "Select t.ID,k.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Doc 'DDC/DOS',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Dolap DOLAP,t.Raf RAF,T.Seviye SEVİYE,t.Barkod 'BARKOD' from Kitaplar t join Sube ş on ş.ID=t.Sube join Kütüphane k on k.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.KitapAdı ='" + textBox6.Text + "'";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 3);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount - 3; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount-3; j++)
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
                    title = new Paragraph(textBox7.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox11.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox4.Text, titleFont);
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

        private void button23_Click(object sender, EventArgs e)
        {
            try
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
                    PdfPTable pdfTable = new PdfPTable(1);

                    // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                    pdfTable.SpacingBefore = 20f;
                    pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                    pdfTable.WidthPercentage = 25; // hücre genişliği
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                    pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                    for (int i = 0; i < 1; i++)
                    {



                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[9].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable.AddCell(cell);

                    }
                    try
                    {

                        for (int i = 0; i < dataGridView1.RowCount; i++)
                        {

                            pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[9].Value.ToString(), fontTitle));

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

                        pdfDoc.Open();
                        pdfDoc.Add(pdfTable);
                        pdfDoc.Close();
                        stream.Close();
                    }
                }
            }
            catch
            {
                
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
