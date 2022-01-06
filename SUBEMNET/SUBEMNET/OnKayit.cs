using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class OnKayit : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=.;Initial Catalog=SUBEMNET;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        public OnKayit()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select TCKN, Adi, Soyadi, VeliAdSoyad, OgrCepTel, VeliCepTel from OnKayit", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();

        }
        void griddoldur2()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select*from Sehir", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            comboBoxSehir.ValueMember = "IL_ID";
            comboBoxSehir.DisplayMember = "IL_ADI";
            comboBoxSehir.DataSource = dt;
            baglan.Close();
        }
        void temizle()
        {
            textBoxTCKN.Text = "";
            comboBoxCinsiyet.Text = "Seçiniz";
            textBoxAd.Text = "";
            textBoxSoyad.Text = "";
            dateTimePickerDT.Value = DateTime.Now;
            comboBoxProgram.Text = "Seçiniz";
            comboBoxDevre.Text = "Seçiniz";
            comboBoxKur.Text = "Seçiniz";
            comboBoxOkul.Text = "Seçiniz";
            textBoxEposta.Text = "";
            textBoxEvTel.Text = "";
            textBoxOgrTel.Text = "";
            comboBoxSehir.Text = "Seçiniz";
            comboBoxIlce.Text = "Seçiniz";
            comboBoxMah.Text = "Seçiniz";
            textBoxAdres.Text = "";
            textBoxVeliAdSoyad.Text = "";
            textBoxVeliCep.Text = "";
            textBoxYakinlik.Text = "";
            textBoxMeslek.Text = "";
            comboBoxMD.Text = "Seçiniz";
            textBoxOB1.Text = "";
            textBoxOB2.Text = "";
            textBoxOB3.Text = "";
            textBoxAciklama.Text = "";
        }
        private void button7_Click(object sender, EventArgs e)
        {
            query = null;
            griddoldur();
        }

        //private void button12_Click(object sender, EventArgs e)
        //{
        //    panel1.Visible = false;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        //private void button5_Click(object sender, EventArgs e)
        //{

        //}

        //private void button3_Click(object sender, EventArgs e)
        //{
            
        //}

        //private void button8_Click(object sender, EventArgs e)
        //{
            
        //}

        private void onkayıt2_Load(object sender, EventArgs e)
        {
            griddoldur();
            griddoldur2();
            comboBoxCinsiyet.Items.Add("Erkek");
            comboBoxCinsiyet.Items.Add("Kız");
            comboBoxDevre2.Items.Add("2.Snf");
            comboBoxDevre2.Items.Add("10.Snf");
            comboBoxKur.Items.Add("1");
            comboBoxKur.Items.Add("2");
            comboBoxKur.Items.Add("3");
            comboBoxProgram.Items.Add("Test1");
            comboBoxProgram.Items.Add("Test2");
            comboBoxProgram.Items.Add("Test3");
            comboBoxMD.Items.Add("Test1");
            comboBoxMD.Items.Add("Test2");
            comboBoxMD.Items.Add("Test3");
            comboBoxDevre.Items.Add("2.Snf");
            comboBoxDevre.Items.Add("10.Snf");
            comboBoxSube.Items.Add("1");
            comboBoxSube.Items.Add("2");
            comboBoxKullanici.Items.Add("3");
        }


        private void buttonAra_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select TCKN, Adi, Soyadi, VeliAdSoyad, OgrCepTel, VeliCepTel from OnKayit Where TCKN =" + textBoxTC.Text, baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }

        private void buttonGetir_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into OnKayit (TCKN, Cinsiyet, Adi, Soyadi, DogumTarihi, Program, Devre, Kur, Okul, Eposta, EvTel, OgrCepTel, Sehir, Ilce, Mahalle, Adres," +
                    "VeliAdSoyad, VeliCepTel, Yakinlik, VeliMeslek, MaliDurum, OzelBilgi1, OzelBilgi2, OzelBilgi3,Aciklama, OlusturmaTarihi) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23, @a24, @a25, @a26)", baglan);
                ekle.Parameters.AddWithValue("@a1", textBoxTCKN.Text);
                ekle.Parameters.AddWithValue("@a2", comboBoxCinsiyet.Text);
                ekle.Parameters.AddWithValue("@a3", textBoxAd.Text);
                ekle.Parameters.AddWithValue("@a4", textBoxSoyad.Text);
                ekle.Parameters.AddWithValue("@a5", dateTimePickerDT.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a6", comboBoxProgram.Text);
                ekle.Parameters.AddWithValue("@a7", comboBoxDevre.Text);
                ekle.Parameters.AddWithValue("@a8", comboBoxKur.Text);
                ekle.Parameters.AddWithValue("@a9", comboBoxOkul.Text);
                ekle.Parameters.AddWithValue("@a10", textBoxEposta.Text);
                ekle.Parameters.AddWithValue("@a11", textBoxEvTel.Text);
                ekle.Parameters.AddWithValue("@a12", textBoxOgrTel.Text);
                ekle.Parameters.AddWithValue("@a13", comboBoxSehir.Text);
                ekle.Parameters.AddWithValue("@a14", comboBoxIlce.Text);
                ekle.Parameters.AddWithValue("@a15", comboBoxMah.Text);
                ekle.Parameters.AddWithValue("@a16", textBoxAdres.Text);
                ekle.Parameters.AddWithValue("@a17", textBoxVeliAdSoyad.Text);
                ekle.Parameters.AddWithValue("@a18", textBoxVeliCep.Text);
                ekle.Parameters.AddWithValue("@a19", textBoxYakinlik.Text);
                ekle.Parameters.AddWithValue("@a20", textBoxMeslek.Text);
                ekle.Parameters.AddWithValue("@a21", comboBoxMD.Text);
                ekle.Parameters.AddWithValue("@a22", textBoxOB1.Text);
                ekle.Parameters.AddWithValue("@a23", textBoxOB2.Text);
                ekle.Parameters.AddWithValue("@a24", textBoxOB3.Text);
                ekle.Parameters.AddWithValue("@a25", textBoxAciklama.Text);
                ekle.Parameters.AddWithValue("@a26", DateTime.Now);

                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
                temizle();
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                if (string.IsNullOrEmpty(textBoxAd.Text) == true || string.IsNullOrEmpty(textBoxSoyad.Text) == true || string.IsNullOrEmpty(textBoxVeliAdSoyad.Text) == true
                    || string.IsNullOrEmpty(textBoxVeliAdSoyad.Text) == true || string.IsNullOrEmpty(textBoxYakinlik.Text) == true || string.IsNullOrEmpty(textBoxVeliCep.Text) == true
                    || comboBoxCinsiyet.SelectedIndex == -1 || comboBoxDevre.SelectedIndex == -1 || comboBoxSehir.SelectedIndex == -1)
                {
                    MessageBox.Show("LÜTFEN İŞARETLİ BİLGİLERİ GİRİNİZ!!");
                }
                else
                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void comboBoxSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSehir.SelectedIndex != -1)
            {

                da = new SqlDataAdapter("Select*from Ilce where IL_ID = " + comboBoxSehir.SelectedValue, baglan);
                cmdb = new SqlCommandBuilder(da);
                dt = new DataTable();
                da.Fill(dt);
                comboBoxIlce.ValueMember = "ILCE_ID";
                comboBoxIlce.DisplayMember = "ILCE_ADI";
                comboBoxIlce.DataSource = dt;
                baglan.Close();
                comboBoxIlce.SelectedIndex = 0;
                comboBoxIlce.Text = "Seçiniz";
            }
        }

        private void comboBoxIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxIlce.SelectedIndex != -1)
            {

                da = new SqlDataAdapter("Select*from Mahalle where ILCE_ID = " + comboBoxIlce.SelectedValue, baglan);
                cmdb = new SqlCommandBuilder(da);
                dt = new DataTable();
                da.Fill(dt);
                comboBoxMah.ValueMember = "MAH_ID";
                comboBoxMah.DisplayMember = "MAHALLE_ADI";
                comboBoxMah.DataSource = dt;
                baglan.Close();
                comboBoxMah.SelectedIndex = 0;
                comboBoxMah.Text = "Seçiniz";
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select Adi, Soyadi";
                if (checkBoxSube.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Sube";
                    degisken = true;
                }
                if (checkBoxTCKN.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " TCKN";
                    degisken = true;
                }
                if (checkBoxCinsiyet.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Cinsiyet";
                    degisken = true;
                }
                if (checkBoxDT.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " DogumTarihi";
                    degisken = true;
                }
                if (checkBoxProgram.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Program";
                    degisken = true;
                }
                if (checkBoxDevre.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Devre";
                    degisken = true;
                }
                if (checkBoxKur.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Kur";
                    degisken = true;
                }
                if (checkBoxOkul.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Okul";
                    degisken = true;
                }
                if (checkBoxEposta.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Eposta";
                    degisken = true;
                }
                if (checkBoxEvTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " EvTel";
                    degisken = true;
                }
                if (checkBoxOgrCepTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OgrCepTel";
                    degisken = true;
                }
                if (checkBoxKaydeden.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Kaydeden";
                    degisken = true;
                }
                if (checkBoxKayitTar.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OlusturmaTarihi";
                    degisken = true;
                }
                if (checkBoxSehir.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Sehir";
                    degisken = true;
                }
                if (checkBoxIlce.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Ilce";
                    degisken = true;
                }
                if (checkBoxMah.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Mahalle";
                    degisken = true;
                }
                if (checkBoxAdres.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Adres";
                    degisken = true;
                }
                if (checkBoxVeliAdi.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliAdSoyad";
                    degisken = true;
                }
                if (checkBoxYakinlik.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Yakinlik";
                    degisken = true;
                }
                if (checkBoxMeslek.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliMeslek";
                    degisken = true;
                }
                if (checkBoxVeliCep.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliCepTel";
                    degisken = true;
                }
                if (checkBoxMD.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " MaliDurum";
                    degisken = true;
                }
                if (checkBoxOB1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi1";
                    degisken = true;
                }
                if (checkBoxOB2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi2";
                    degisken = true;
                }
                if (checkBoxOB3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi3";
                    degisken = true;
                }
                if (checkBoxAciklama.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Aciklama";
                    degisken = true;
                }
                filtre += " From OnKayit Where ";

                if (comboBoxSube.SelectedIndex != -1)
                {
                    filtre += " Sube = " + comboBoxSube.Text;
                    degisken = true;
                }


                if (comboBoxDevre.SelectedIndex != -1)
                {
                    if (comboBoxSube.SelectedIndex != -1)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Devre = '" + comboBoxDevre.Text + "'";
                    degisken = true;
                }
                if (comboBoxKullanici.SelectedIndex != -1)
                {
                    if (comboBoxDevre.SelectedIndex != -1 || comboBoxSube.SelectedIndex != -1)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Kullanici = '" + comboBoxKullanici.Text + "'";
                    degisken = true;
                }
                //if (radioButtonOlan.Checked == true)
                //{
                //    if (degisken == true)
                //    {
                //        filtre += " AND ";
                //    }
                //    filtre += " KayitDurumu = 1";
                //    degisken = true;
                //}
                //if (radioButtonOlmayan.Checked == true)
                //{
                //    if (degisken == true)
                //    {
                //        filtre += " AND ";
                //    }
                //    filtre += " KayitDurumu = 2";
                //    degisken = true;
                //}

                if (dateTimePickerBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " OlusturmaTarihi between '" + dateTimePickerBas.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePickerBit.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                if (radioButtonKGN.Checked == true)
                {
                    filtre += " OrderBy KGN ASC";
                }
                if (radioButtonAdi.Checked == true)
                {
                    filtre += " OrderBy Adi ASC";
                }
                if (radioButtonSoyadi.Checked == true)
                {
                    filtre += " OrderBy Soyadi ASC";
                }
                if (radioButtonDevre.Checked == true)
                {
                    filtre += " OrderBy Devre ASC";
                }
                query = filtre;
                panel3.Visible = false;
                griddoldur();

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            panel6.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void buttonSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel Dosyası |*.xlsx| Excel Dosyası|*.xls";
            file.FilterIndex = 2;
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "Excel Dosyası Seçiniz..";

            if (file.ShowDialog() == DialogResult.OK)
            {
                string DosyaYolu = file.FileName;
                string DosyaAdi = file.SafeFileName;
                textBoxDosya.Text = DosyaAdi;
                textBox1.Text = DosyaYolu;
            }
        }

        private void buttonYukle_Click(object sender, EventArgs e)
        {
            OleDbConnection bağlantıexcel = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox1.Text + "; Extended Properties='Excel 12.0 Xml;HDR=YES'");
            bağlantıexcel.Open();
            OleDbCommand cmd = new OleDbCommand("Select * from [OnKayit$]", bağlantıexcel);
            OleDbDataReader oku = cmd.ExecuteReader();
            while (oku.Read())
            {
                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into OnKayit (TCKN, Cinsiyet, Adi, Soyadi, DogumTarihi, Program, Devre, Kur, Okul, Eposta, EvTel, OgrCepTel, Sehir, Ilce, Mahalle, Adres," +
                    "VeliAdSoyad, VeliCepTel, Yakinlik, VeliMeslek, MaliDurum, OzelBilgi1, OzelBilgi2, OzelBilgi3,Aciklama, OlusturmaTarihi) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23, @a24, @a25, @a26)", baglan);
                ekle.Parameters.AddWithValue("@a1", oku["TCKN"]);
                ekle.Parameters.AddWithValue("@a2", oku["Cinsiyet"]);
                ekle.Parameters.AddWithValue("@a3", oku["Ad"]);
                ekle.Parameters.AddWithValue("@a4", oku["Soyad"]);
                ekle.Parameters.AddWithValue("@a5", oku["DogumTarihi"]);
                ekle.Parameters.AddWithValue("@a6", oku["Program"]);
                ekle.Parameters.AddWithValue("@a7", oku["Devre"]);
                ekle.Parameters.AddWithValue("@a8", oku["Kur"]);
                ekle.Parameters.AddWithValue("@a9", oku["Okul"]);
                ekle.Parameters.AddWithValue("@a10", oku["Eposta"]);
                ekle.Parameters.AddWithValue("@a11", oku["EvTel"]);
                ekle.Parameters.AddWithValue("@a12", oku["OgrTel"]);
                ekle.Parameters.AddWithValue("@a13", oku["Sehir"]);
                ekle.Parameters.AddWithValue("@a14", oku["Ilce"]);
                ekle.Parameters.AddWithValue("@a15", oku["Mahalle"]);
                ekle.Parameters.AddWithValue("@a16", oku["Adres"]);
                ekle.Parameters.AddWithValue("@a17", oku["VeliAdSoyad"]);
                ekle.Parameters.AddWithValue("@a18", oku["VeliCep"]);
                ekle.Parameters.AddWithValue("@a19", oku["Yakinlik"]);
                ekle.Parameters.AddWithValue("@a20", oku["Meslek"]);
                ekle.Parameters.AddWithValue("@a21", oku["MaliDurum"]);
                ekle.Parameters.AddWithValue("@a22", oku["OB1"]);
                ekle.Parameters.AddWithValue("@a23", oku["OB2"]);
                ekle.Parameters.AddWithValue("@a24", oku["OB3"]);
                ekle.Parameters.AddWithValue("@a25", oku["Aciklama"]);
                ekle.Parameters.AddWithValue("@a26", DateTime.Now);

                ekle.ExecuteNonQuery();
                baglan.Close();
                break;
            }
            bağlantıexcel.Close();
            MessageBox.Show("Kayıt Başarı ile Eklendi", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
