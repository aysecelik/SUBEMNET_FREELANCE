using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelDataReader;


namespace SUBEMNET
{
    public partial class ÖnKayıt : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        public ÖnKayıt()
        {
            InitializeComponent();
        }
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
            {
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }

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
            panel7.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

    

        private void onkayıt2_Load(object sender, EventArgs e)
        {
          
            
        }
        List<int> subeid = new List<int>();
        int okulid = Form1.okulid;
        private void buttonAra_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select ö.TCKN 'TC KİMLİK',ş.ŞubeAdi ŞUBE, ö.Adi ADI, ö.Soyadi SOYADI, ö.VeliAdSoyad VELİ, ö.OgrCepTel 'ÖĞRENCİ TELEFON', ö.VeliCepTel 'VELİ TELEFON' from OnKayit ö join Sube ş on ş.ID=ö.Sube  Where TCKN ='" + textBoxTC.Text+ "' and ş.okulid='"+okulid+"'", baglan);
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
                    SqlCommand ekle = new SqlCommand("insert into OnKayit (TCKN, Cinsiyet, Adi, Soyadi, DogumTarihi, Program, Devre, Okul, Eposta, EvTel, OgrCepTel, Sehir, Ilce, Mahalle, Adres," +
                        "VeliAdSoyad, VeliCepTel, Yakinlik, VeliMeslek, MaliDurum, OzelBilgi1, OzelBilgi2, OzelBilgi3,Aciklama, OlusturmaTarihi,Sube) " +
                        "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23, @a24, @a25, @a26,@a27)", baglan);
                    ekle.Parameters.AddWithValue("@a1", textBoxTCKN.Text);
                    ekle.Parameters.AddWithValue("@a2", comboBoxCinsiyet.Text);
                    ekle.Parameters.AddWithValue("@a3", textBoxAd.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a4", textBoxSoyad.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a5", dateTimePickerDT.Value.ToString("yyyyMMdd"));
                    ekle.Parameters.AddWithValue("@a6", comboBoxProgram.Text);
                    ekle.Parameters.AddWithValue("@a7", comboBoxDevre.Text);
                    ekle.Parameters.AddWithValue("@a9", textBox2.Text);
                    ekle.Parameters.AddWithValue("@a10", textBoxEposta.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a11", textBoxEvTel.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a12", textBoxOgrTel.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a13", comboBoxSehir.Text);
                    ekle.Parameters.AddWithValue("@a14", comboBoxIlce.Text);
                    ekle.Parameters.AddWithValue("@a15", comboBoxMah.Text);
                    ekle.Parameters.AddWithValue("@a16", textBoxAdres.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a17", textBoxVeliAdSoyad.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a18", textBoxVeliCep.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a19", textBoxYakinlik.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a20", textBoxMeslek.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a21", textBox3.Text);
                    ekle.Parameters.AddWithValue("@a22", textBoxOB1.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a23", textBoxOB2.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a24", textBoxOB3.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a25", textBoxAciklama.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a26", DateTime.Now);
                    ekle.Parameters.AddWithValue("@a27", subeid[cmbSube.SelectedIndex]);


                    ekle.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Eklendi.");
                    temizle();
                    griddoldur();
                }
                catch (Exception a)
                {
                    baglan.Close();
                    if (string.IsNullOrEmpty(cmbSube.Text) == true || string.IsNullOrEmpty(textBoxAd.Text) == true || string.IsNullOrEmpty(textBoxSoyad.Text) == true || string.IsNullOrEmpty(textBoxVeliAdSoyad.Text) == true
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

        private void button14_Click_1(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select ö.Adi, ö.Soyadi";
               
                if (checkBoxTCKN.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.TCKN";
                    degisken = true;
                }
                if (checkBoxCinsiyet.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Cinsiyet";
                    degisken = true;
                }
                if (checkBoxDT.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.DogumTarihi";
                    degisken = true;
                }
                if (checkBoxProgram.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Program";
                    degisken = true;
                }
                if (checkBoxDevre.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Devre";
                    degisken = true;
                }
                if (checkBoxKur.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Kur";
                    degisken = true;
                }
                if (checkBoxOkul.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Okul";
                    degisken = true;
                }
                if (checkBoxEposta.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Eposta";
                    degisken = true;
                }
                if (checkBoxEvTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.EvTel";
                    degisken = true;
                }
                if (checkBoxOgrCepTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OgrCepTel";
                    degisken = true;
                }
                if (checkBoxKaydeden.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Kaydeden";
                    degisken = true;
                }
                if (checkBoxKayitTar.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OlusturmaTarihi";
                    degisken = true;
                }
                if (checkBoxSehir.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Sehir";
                    degisken = true;
                }
                if (checkBoxIlce.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Ilce";
                    degisken = true;
                }
                if (checkBoxMah.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Mahalle";
                    degisken = true;
                }
                if (checkBoxAdres.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Adres";
                    degisken = true;
                }
                if (checkBoxVeliAdi.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliAdSoyad";
                    degisken = true;
                }
                if (checkBoxYakinlik.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Yakinlik";
                    degisken = true;
                }
                if (checkBoxMeslek.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliMeslek";
                    degisken = true;
                }
                if (checkBoxVeliCep.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliCepTel";
                    degisken = true;
                }
                if (checkBoxMD.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.MaliDurum";
                    degisken = true;
                }
                if (checkBoxOB1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi1";
                    degisken = true;
                }
                if (checkBoxOB2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi2";
                    degisken = true;
                }
                if (checkBoxOB3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi3";
                    degisken = true;
                }
                if (checkBoxAciklama.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Aciklama";
                    degisken = true;
                }
                filtre += " From OnKayit ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='"+okulid+"'";

                if (comboBoxSube.SelectedIndex != -1)
                {
                    
                        filtre += " AND ";
                    
                    filtre += " ö.Sube = '" + subeid[comboBoxSube.SelectedIndex]+"'";
                    degisken = true;
                }


                if (comboBoxDevre.SelectedIndex != -1)
                {
                    if (comboBoxSube.SelectedIndex != -1)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Devre = '" + comboBoxDevre.Text + "'";
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
                    filtre += " ö.OlusturmaTarihi between '" + dateTimePickerBas.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePickerBit.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                if (radioButtonKGN.Checked == true)
                {
                    filtre += " OrderBy ö.KGN ASC";
                }
                if (radioButtonAdi.Checked == true)
                {
                    filtre += " OrderBy ö.Adi ASC";
                }
                if (radioButtonSoyadi.Checked == true)
                {
                    filtre += " OrderBy ö.Soyadi ASC";
                }
                if (radioButtonDevre.Checked == true)
                {
                    filtre += " OrderBy ö.Devre ASC";
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

        private void button13_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
                
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
            if (string.IsNullOrEmpty(comboBox1.Text) == true)
            {
                MessageBox.Show("ŞUBE BİLGİSİNİN GİRİLMESİ ZORUNLUDUR.");
            }
            else
            {
                try
                {
                    OpenFileDialog openFileDialog1 = new OpenFileDialog();
                    openFileDialog1.Title = "Lütfen Dosya Seçiniz";
                    openFileDialog1.Filter = " (*.xlsx)|*.xlsx";
                    openFileDialog1.FilterIndex = 1;
                    openFileDialog1.Multiselect = true;
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string dosya_adres = openFileDialog1.FileName;
                        //Dosyanın okunacağı dizin

                        //Dosyayı okuyacağımı ve gerekli izinlerin ayarlanması.
                        FileStream stream = File.Open(dosya_adres, FileMode.Open, FileAccess.Read);
                        //Encoding 1252 hatasını engellemek için;

                        ;

                        IExcelDataReader excelReader;
                        int counter = 0;

                        //Gönderdiğim dosya xls'mi xlsx formatında mı kontrol ediliyor.
                        if (Path.GetExtension(dosya_adres).ToUpper() == ".XLS")
                        {
                            //Reading from a binary Excel file ('97-2003 format; *.xls)
                            excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else
                        {
                            //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                            excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }

                        /*yeni sürümlerde bu kaldırıldığı için kapatıldı.
                        //Datasete atarken ilk satırın başlık olacağını belirtiyor.
                        excelReader.IsFirstRowAsColumnNames = true;
                        DataSet result = excelReader.AsDataSet();*/

                        //Veriler okunmaya başlıyor.
                        while (excelReader.Read())
                        {
                            counter++;

                            //ilk satır başlık olduğu için 2.satırdan okumaya başlıyorum.
                            if (counter > 1)
                            {
                                baglan.Open();
                                SqlCommand ekle = new SqlCommand("insert into OnKayit (TCKN, Cinsiyet, Adi, Soyadi, DogumTarihi, Program, Devre, Okul, Eposta, EvTel, OgrCepTel, Sehir, Ilce, Mahalle, Adres," +
                                    "VeliAdSoyad, VeliCepTel, Yakinlik, VeliMeslek, MaliDurum, OzelBilgi1, OzelBilgi2, OzelBilgi3,Aciklama, OlusturmaTarihi,Sube) " +
                                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23, @a24, @a25, @a26,@a27)", baglan);
                                ekle.Parameters.AddWithValue("@a1", excelReader.GetString(0));
                                ekle.Parameters.AddWithValue("@a2", excelReader.GetString(1));
                                ekle.Parameters.AddWithValue("@a3", excelReader.GetString(2));
                                ekle.Parameters.AddWithValue("@a4", excelReader.GetString(3));
                                ekle.Parameters.AddWithValue("@a5", Convert.ToDateTime(excelReader.GetValue(4)).ToString("yyyyMMdd"));
                                ekle.Parameters.AddWithValue("@a6", excelReader.GetString(5));
                                ekle.Parameters.AddWithValue("@a7", excelReader.GetString(6));
                                ekle.Parameters.AddWithValue("@a9", excelReader.GetString(7));
                                ekle.Parameters.AddWithValue("@a10", excelReader.GetString(8));
                                ekle.Parameters.AddWithValue("@a11", excelReader.GetString(9));
                                ekle.Parameters.AddWithValue("@a12", excelReader.GetString(10));
                                ekle.Parameters.AddWithValue("@a13", excelReader.GetString(11));
                                ekle.Parameters.AddWithValue("@a14", excelReader.GetString(12));
                                ekle.Parameters.AddWithValue("@a15", excelReader.GetString(13));
                                ekle.Parameters.AddWithValue("@a16", excelReader.GetString(14));
                                ekle.Parameters.AddWithValue("@a17", excelReader.GetString(15));
                                ekle.Parameters.AddWithValue("@a18", excelReader.GetString(16));
                                ekle.Parameters.AddWithValue("@a19", excelReader.GetString(17));
                                ekle.Parameters.AddWithValue("@a20", excelReader.GetString(18));
                                ekle.Parameters.AddWithValue("@a21", excelReader.GetString(19));
                                ekle.Parameters.AddWithValue("@a22", excelReader.GetString(20));
                                ekle.Parameters.AddWithValue("@a23", excelReader.GetString(21));
                                ekle.Parameters.AddWithValue("@a24", excelReader.GetString(22));
                                ekle.Parameters.AddWithValue("@a25", excelReader.GetString(23));
                                ekle.Parameters.AddWithValue("@a26", DateTime.Now);
                                ekle.Parameters.AddWithValue("@a27", subeid[comboBox1.SelectedIndex]);


                                ekle.ExecuteNonQuery();
                                baglan.Close();




                            }

                            excelReader.Close();
                        }
                    }
                }
                catch
                {

                }
            }
        }
        SqlCommand komut;
        private void ÖnKayıt_Load(object sender, EventArgs e)
        {
           
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbSube.Items.Add(oku3[0].ToString());
                comboBoxSube.Items.Add(oku3[0].ToString());
                comboBox1.Items.Add(oku3[0].ToString());

                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulid = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBoxDevre.Items.Add(oku[0].ToString());
                comboBoxDevre2.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulid = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBoxProgram.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            griddoldur2();
            comboBoxCinsiyet.Items.Add("Erkek");
            comboBoxCinsiyet.Items.Add("Kız");
        }
    }
}

