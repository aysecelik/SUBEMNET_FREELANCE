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
using System.Configuration;
using System.IO;

namespace SUBEMNET
{
    public partial class İnsanKaynkaları : Form
    {
        public İnsanKaynkaları()
        {
            InitializeComponent();
        }
        int okulid = Form1.okulid;
        private void İnsanKaynkaları_Load(object sender, EventArgs e)
        {
            tabPage1.Text = "GENEL BİLGİLER";
            tabPage2.Text = "EĞİTİM BİLGİLERİ";
            tabPage3.Text = "İŞ DENEYİMİ";
            tabPage4.Text = "EK BELGELER";
          
            datetimeDogumtarih.Format = DateTimePickerFormat.Custom;
            datetimeDogumtarih.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            datemezuntarih.Format = DateTimePickerFormat.Custom;
            datemezuntarih.CustomFormat = "dd/MM/yyyy";


            griddoldur();
            panel2.Visible = false;
            cmbişdeneyimidurumu.Items.Add("VAR");
            cmbişdeneyimidurumu.Items.Add("YOK");
            cmbeğitim.Items.Add("İLKOKUL");
            cmbeğitim.Items.Add("ORTAOKUL");
            cmbeğitim.Items.Add("LİSE");
            cmbeğitim.Items.Add("ÖNLİSANS");
            cmbeğitim.Items.Add("LİSANS");
            cmbeğitim.Items.Add("YÜKSEKLİSANS");
            cmbeğitim.Items.Add("DOKTORA");
            cmbeğitim.Items.Add("DİPLOMASI YOK");
            cmbcinsiyet.Items.Add("KADIN");
            cmbcinsiyet.Items.Add("ERKEK");
            cmbcinsiyet.Items.Add("LGBT");
            cmbcinsiyet.Items.Add("BELİRTMEK İSTEMİYORUM");
            cmbkan.Items.Add("");
            cmbkan.Items.Add("AB Rh+");
            cmbkan.Items.Add("AB Rh-");
            cmbkan.Items.Add("A Rh+");
            cmbkan.Items.Add("A Rh-");
            cmbkan.Items.Add("B Rh+");
            cmbkan.Items.Add("B Rh-");
            cmbkan.Items.Add("0 Rh+");
            cmbkan.Items.Add("0 Rh-");
            cmbcalismadurum.Items.Add("");
            cmbcalismadurum.Items.Add("ÇALIŞIYOR");
            cmbcalismadurum.Items.Add("ÇALIŞMIYOR");
            cmbmedeni.Items.Add("");
            cmbmedeni.Items.Add("BEKAR");
            cmbmedeni.Items.Add("EVLİ");

            cmbaskerlik.Items.Add("");
            cmbaskerlik.Items.Add("YAPILDI");
            cmbaskerlik.Items.Add("YAPILMADI");
            cmbaskerlik.Items.Add("MUAF");
            comboBox1.Items.Add("");
            comboBox2.Items.Add("");
            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbpozisyon.Items.Add(oku[0].ToString());
                comboBox1.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbBranş.Items.Add(oku2[0].ToString());
                comboBox2.Items.Add(oku2[0].ToString());


            }
            baglan.Close();

        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        byte[] bytes;
        byte[] bytes2;
        byte[] bytes3;
        byte[] bytes4;


        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ID,Email 'E-POSTA',Adi ADI,Soyadi SOYADI,CepTel TELEFON,Pozisyon POZİSYON,Branş BRANŞ,CVDosya CV, SicilDosya 'SİCİL KAYDI', SaglikRaporDosya 'SAĞLIK RAPORU' from IsBasvuru where okulid='"+okulid+"'", baglan);

            cmdb = new SqlCommandBuilder(da);

            ds = new DataSet();
            da.Fill(ds, "IsBasvuru");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "DETAY";
            dgvBtn.Text = "DETAY";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);

            DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
            dgvbtn2.HeaderText = "SİL";
            dgvbtn2.Text = "SİL";
            dgvbtn2.UseColumnTextForButtonValue = true;
            dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn2.Width = 70;
            dataGridView1.Columns.Add(dgvbtn2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Ekleme Kısım Açılış
            panel2.Visible = true;
            panel1.Visible = true;
            tabControl1.SelectedTab = tabPage1;
            currentPage = tabPage1;
            button14.Visible = true;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            //fotoğraf ekleme
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(open.FileName);
                path = open.FileName;
                string filePath = path;

                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes4 = br.ReadBytes((Int32)fs.Length);



            }            
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //eğitimdurumu açılış
            if (string.IsNullOrEmpty(cmbcinsiyet.Text) == true || string.IsNullOrEmpty(txteposta.Text) == true || string.IsNullOrEmpty(cmbpozisyon.Text) == true || string.IsNullOrEmpty(cmbBranş.Text) == true || string.IsNullOrEmpty(txtAd.Text) == true || string.IsNullOrEmpty(mskceptel.Text.Replace(" ", "")) == true || string.IsNullOrEmpty(txtSoyad.Text) == true)
            {
                MessageBox.Show("LÜTFEN ZORUNLU KISIMLARI DOLDURUNUZ.");
            }
            else
            {
                tabControl1.SelectedTab = tabPage2;
                currentPage = tabPage2;


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
            currentPage = tabPage1;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;     
            temizle();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
            currentPage = tabPage2;

        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;
            currentPage = tabPage4;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (cmbeğitim.Text == "")
            {
                MessageBox.Show("EĞİTİM DURUMU SEÇİLMEDEN BİR SONRAKİ AŞAMAYA GEÇEMEZSİNİZ.");
            }
            else
            {
                tabControl1.SelectedTab = tabPage3;
                currentPage = tabPage3;

                panel9.Visible = false;


            }
        }

        private void mskevtel_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            currentPage = tabPage3;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox7.Text == "" || textBox6.Text == "")
            {
                MessageBox.Show("BELGELERİN YÜKLENMESİ ZORUNLUDUR. AKSİ TAKTİRDE KAYDINIZ YAPILMAYACAKTIR.");
            }
            else
            {
                try
                {
                    bool degisken = false;
                    string filtre = "insert into IsBasvuru (";
                    string values = "Values (";
                    if (string.IsNullOrEmpty(txteposta.Text) == false)
                    {
                        filtre += " Email ";
                        values += "'" + txteposta.Text + "'";
                        degisken = true;
                    }


                    if (string.IsNullOrEmpty(cmbpozisyon.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Pozisyon ";
                        values += "'" + cmbpozisyon.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbBranş.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Branş  ";
                        values += "'" + cmbBranş.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbcinsiyet.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Cinsiyet ";
                        values += "'" + cmbcinsiyet.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtTC.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " TcKimlikNo ";
                        values += "'" + txtTC.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtAd.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Adi ";
                        values += "'" + txtAd.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtSoyad.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Soyadi ";
                        values += "'" + txtSoyad.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (datetimeDogumtarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " DogumTarihi ";
                        values += "'" + datetimeDogumtarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtdogumyeri.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " DogumYeri ";
                        values += "'" + txtdogumyeri.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskevtel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " EvTel ";
                        values += "'" + mskevtel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " CepTel ";
                        values += "'" + mskceptel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel2.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " CepTel2 ";
                        values += "'" + mskceptel2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Ev_Adres_il ";
                        values += "'" + txtevil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevilce.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Ev_Adres_ilce ";
                        values += "'" + txtevilce.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevmahalle.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Ev_Adres_Mahalle ";
                        values += "'" + txtevmahalle.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevadres.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Ev_Adres ";
                        values += "'" + txtevadres.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtisdeneyim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Is_Deneyimi ";
                        values += txtisdeneyim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbaskerlik.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Askerlik_Durumu ";
                        values += "'" + cmbaskerlik.Text + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(cmbmedeni.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Medeni_Hal ";
                        values += "'" + cmbmedeni.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbcalismadurum.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Calisma_Durumu ";
                        values += "'" + cmbcalismadurum.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtbabaadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Baba_Adi ";
                        values += "'" + txtbabaadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtanneadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Anne_Adi ";
                        values += "'" + txtanneadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Nüfus_Kayıt_il ";
                        values += "'" + txtnüfusil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusilçe.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Nüfus_Kayıt_ilce ";
                        values += "'" + txtnüfusilçe.Text.ToUpper() + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(cmbkan.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " KanGrubu ";
                        values += "'" + cmbkan.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtciltno.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Cilt_No ";
                        values += "'" + txtciltno.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtailesırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Aile_Sıra_No ";
                        values += "'" + txtailesırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtsırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Sıra_No ";
                        values += "'" + txtsırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtengeldurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " EngelDurumuYüzde ";
                        values += "'" + txtengeldurumu.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbeğitim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Egitim_Durumu ";
                        values += "'" + cmbeğitim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtegitimdurum.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mezun_Olunan_Kurum ";
                        values += "'" + txtegitimdurum.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtFakülte.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mezun_Fakulte ";
                        values += "'" + txtFakülte.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtBölüm.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mezun_Bölüm ";
                        values += "'" + txtBölüm.Text + "'";
                        degisken = true;
                    }
                    if (datemezuntarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mezun_Olunan_Tarih ";
                        values += "'" + datemezuntarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbişdeneyimidurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " IsDeneyimiDurum ";
                        values += "'" + cmbişdeneyimidurumu.Text + "'";
                        degisken = true;
                    }
                    if (cmbişdeneyimidurumu.Text == "VAR")
                    {
                        if (string.IsNullOrEmpty(txtkurum1.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi1 ";
                            values += "'" + txtkurum1.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox2.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi2 ";
                            values += "'" + textBox2.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox3.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi3 ";
                            values += "'" + textBox3.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox4.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi4 ";
                            values += "'" + textBox4.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox5.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi5 ";
                            values += "'" + textBox5.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox11.MaskFull==true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi1Tarih ";
                            values += "'" + maskedTextBox11.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox12.MaskFull==true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi2Tarih ";
                            values += "'" + maskedTextBox12.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox13.MaskFull==true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi3Tarih ";
                            values += "'" + maskedTextBox13.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox14.MaskFull==true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi4Tarih ";
                            values += "'" + maskedTextBox14.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox15.MaskFull==true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";
                                values += " , ";
                            }
                            filtre += " IsDeneyimi5Tarih ";
                            values += "'" + maskedTextBox15.Text + "'";
                            degisken = true;
                        }
                    }
                    if (pictureBox1.Image != null)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " foto ";
                        values += "@a4";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " CV, CVDosya ";
                        values += "@a1 , '" + CVfilename + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Sicil, SicilDosya ";
                        values += "@a2, '" + SicilFilename + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " SaglikRapor, SaglikRaporDosya ";
                        values += " @a3 , '" + Saglikfilename + "'";
                        degisken = true;
                    }

                    filtre += ",BasvuruTarihi,okulid)";
                    values += ", '" + DateTime.Now.Date.ToString("yyyyMMdd") + "','"+okulid+"')";
                    filtre += values;
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;
                    komutkaydet.Parameters.AddWithValue("@a2", SqlDbType.VarBinary).Value = bytes2;
                    komutkaydet.Parameters.AddWithValue("@a3", SqlDbType.VarBinary).Value = bytes3;
                    if (pictureBox1.Image != null)
                    {
                        komutkaydet.Parameters.AddWithValue("@a4", SqlDbType.VarBinary).Value = bytes4;
                    }

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    panel2.Visible = false;
                    temizle();

                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());

                }
            }
        }
        string path;
        //fileUpload kısımı
        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                   path = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/pdf";
            if (path != null)
            {
                if (contenttype != String.Empty)
                {
                    Stream fs = File.OpenRead(filePath);
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    CVfilename = fileinfo.Name;
                    textBox1.Text = fileinfo.Name;

                }
            }
        }
        string CVfilename;
        string SicilFilename;
        string Saglikfilename;
        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    path= open.FileName;

                }
            }

            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/pdf";
            if (path != null)
            {
                if (contenttype != String.Empty)
                {
                    Stream fs = File.OpenRead(filePath);
                    BinaryReader br = new BinaryReader(fs);
                    bytes2 = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    SicilFilename = fileinfo.Name;
                    textBox6.Text = fileinfo.Name;
                }
            }
        }
        SqlCommand komut;
        private void button19_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                   path= open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/pdf";
            if (path != null)
            {
                if (contenttype != String.Empty)
                {
                    Stream fs = File.OpenRead(filePath);
                    BinaryReader br = new BinaryReader(fs);
                    bytes3 = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    Saglikfilename = fileinfo.Name;
                    textBox7.Text = fileinfo.Name;


                }
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 7)
                {
                    int secilen;
                    secilen = dataGridView1.SelectedCells[0].RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select CV from IsBasvuru where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }
                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 8)
                {
                    int secilen;
                    secilen = dataGridView1.SelectedCells[0].RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                       savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select Sicil from IsBasvuru where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }

                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 9)
                {
                    int secilen;
                    secilen = dataGridView1.SelectedCells[0].RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                       savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select SaglikRapor from IsBasvuru where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }


                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 10)
                {
                    temizle();
                    int i = 0;
                    int secilen;
                    secilen = dataGridView1.CurrentCell.RowIndex;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    baglan.Open();
                    komut = new SqlCommand("Select Email,Pozisyon,Branş,Cinsiyet,TcKimlikNo,Adi,Soyadi,DogumYeri,EvTel,DogumTarihi,CepTel,CepTel2,Ev_Adres_il, Ev_Adres_ilce" +
                        ",Ev_Adres_Mahalle,Ev_Adres,Is_Deneyimi,Askerlik_Durumu,Medeni_Hal,Baba_Adi,Anne_Adi,Nufus_Kayıt_il,Nufus_Kayıt_ilce,KanGrubu,Cilt_No," +
                        "Aile_Sıra_No,SıraNo,EngelDurumuYüzde,Egitim_Durumu,Mezun_Olunan_Kurum,Mezun_Fakulte,Mezun_Bölüm,Mezun_Olunan_Tarih,IsDeneyimiDurum,IsDeneyimi1," +
                        "IsDeneyimi2,IsDeneyimi3,IsDeneyimi4,IsDeneyimi5,IsDeneyimi1Tarih,IsDeneyimi2Tarih,IsDeneyimi3Tarih,IsDeneyimi4Tarih,IsDeneyimi5Tarih, " +
                        " Sicil, SicilDosya, SaglikRapor, SaglikRaporDosya,foto,CV,CvDosya from IsBasvuru where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        txteposta.Text = oku.GetValue(0).ToString();
                        cmbpozisyon.Text = oku.GetValue(1).ToString();
                        cmbBranş.Text = oku.GetValue(2).ToString();
                        cmbcinsiyet.Text = oku.GetValue(3).ToString();
                        txtTC.Text = oku.GetValue(4).ToString();
                        txtAd.Text = oku.GetValue(5).ToString();
                        txtSoyad.Text = oku.GetValue(6).ToString();
                        txtdogumyeri.Text = oku.GetValue(7).ToString();
                        mskevtel.Text = oku.GetValue(8).ToString();
                        if (string.IsNullOrEmpty(oku.GetValue(9).ToString()) == false)
                        {
                            datetimeDogumtarih.Value = Convert.ToDateTime(oku.GetValue(9));
                        }
                        mskceptel.Text = oku.GetValue(10).ToString();
                        mskceptel2.Text = oku.GetValue(11).ToString();
                        txtevil.Text = oku.GetValue(12).ToString();
                        txtevilce.Text = oku.GetValue(13).ToString();
                        txtevmahalle.Text = oku.GetValue(14).ToString();
                        txtevadres.Text = oku.GetValue(15).ToString();
                        txtisdeneyim.Text = oku.GetValue(16).ToString();
                        cmbaskerlik.Text = oku.GetValue(17).ToString();
                        cmbmedeni.Text = oku.GetValue(18).ToString();
                        txtbabaadi.Text = oku.GetValue(19).ToString();
                        txtanneadi.Text = oku.GetValue(20).ToString();
                        txtnüfusil.Text = oku.GetValue(21).ToString();
                        txtnüfusilçe.Text = oku.GetValue(22).ToString();
                        cmbkan.Text = oku.GetValue(23).ToString();
                        txtciltno.Text = oku.GetValue(24).ToString();
                        txtailesırano.Text = oku.GetValue(25).ToString();
                        txtsırano.Text = oku.GetValue(26).ToString();
                        txtengeldurumu.Text = oku.GetValue(27).ToString();
                        cmbeğitim.Text = oku.GetValue(28).ToString();
                        txtegitimdurum.Text = oku.GetValue(29).ToString();
                        txtFakülte.Text = oku.GetValue(30).ToString();
                        txtBölüm.Text = oku.GetValue(31).ToString();
                        if (string.IsNullOrEmpty(oku.GetValue(32).ToString()) == false)
                        {
                            datemezuntarih.Value = Convert.ToDateTime(oku.GetValue(32));
                        }
                        cmbişdeneyimidurumu.Text = oku.GetValue(33).ToString();
                        txtkurum1.Text = oku.GetValue(34).ToString();
                        textBox2.Text = oku.GetValue(35).ToString();
                        textBox3.Text = oku.GetValue(36).ToString();
                        textBox4.Text = oku.GetValue(37).ToString();
                        textBox5.Text = oku.GetValue(38).ToString();
                        maskedTextBox11.Text = oku.GetValue(39).ToString();
                        maskedTextBox12.Text = oku.GetValue(40).ToString();
                        maskedTextBox13.Text = oku.GetValue(41).ToString();
                        maskedTextBox14.Text = oku.GetValue(42).ToString();
                        maskedTextBox15.Text = oku.GetValue(43).ToString();
                        bytes2 = (byte[])oku[44];
                        textBox6.Text = oku.GetValue(45).ToString();
                        bytes3 = (byte[])oku[46];
                        textBox7.Text = oku.GetValue(47).ToString();
                        if (oku[48] != DBNull.Value)
                        {
                            bytes4 = (byte[])oku[48];
                            MemoryStream mr = new MemoryStream(bytes4);
                            pictureBox1.Image = Image.FromStream(mr);
                        }
                        bytes = (byte[])oku[49];
                        textBox1.Text = oku.GetValue(50).ToString();

                    }
                    baglan.Close();
                 
                    panel2.Visible = true;
                    panel1.Visible = true;
                    tabControl1.SelectedTab = tabPage1;
                    currentPage = tabPage1;

                    button14.Visible = false;

                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 11)
                {
                    DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "İŞ BAŞVURUSU SİLME", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " İSİMLİ KİŞİNİN İŞ BAŞVURUSUNU TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "İŞ BAŞVURU SİLME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                string sql = "DELETE IsBasvuru FROM  WHERE ID=@id";
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
                                MessageBox.Show("HATA");
                            }

                        }
                    }
                }
            }
            catch(Exception a)
            {
                MessageBox.Show("HATA.");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = false;
                string filtre = "Select  ID,Email,Adi,Soyadi,CepTel,Pozisyon,Branş,CVDosya, SicilDosya, SaglikRaporDosya from IsBasvuru where ";
            
                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " ID = '" + textBox11.Text+ "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Email = '" + textBox9.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }
               
                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Branş = '" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Adi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Soyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " CepTel = '" + maskedTextBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BasvuruTarihi between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox9.Text)==true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox1.Checked == false)
                {
                    filtre = "Select  ID,Email,Adi,Soyadi,CepTel,Pozisyon,Branş,CVDosya, SicilDosya, SaglikRaporDosya from IsBasvuru";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "IsBasvuru");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                temizle();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void cmbişdeneyimidurumu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbişdeneyimidurumu.Text == "VAR")
                panel9.Visible = true;
            else
                panel9.Visible = false;
        }

        private void cmbişdeneyimidurumu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true ;
        }

        private void cmbeğitim_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtegitimdurum_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtAd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }
        void temizle()
        {
            maskedTextBox1.Text = "";
            checkBox1.Checked = false;
            textBox1.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            txtAd.Text = "";
            txtailesırano.Text = "";
            txtanneadi.Text = "";
            txtbabaadi.Text = "";
            txtBölüm.Text = "";
            txtciltno.Text = "";
            txtdogumyeri.Text = "";
            txtegitimdurum.Text = "";
            txtengeldurumu.Text = "";
            txteposta.Text = "";
            txtevadres.Text = "";
            txtevil.Text = "";
            txtevilce.Text = "";
            txtevmahalle.Text = "";
            txtFakülte.Text = "";
            txtisdeneyim.Text = "";
            txtkurum1.Text = "";
            txtnüfusil.Text = "";
            txtnüfusilçe.Text = "";
            txtSoyad.Text = "";
            txtsırano.Text = "";
            txtTC.Text = "";
            cmbaskerlik.Text = "";
            cmbBranş.Text = "";
            cmbcalismadurum.Text = "";
            cmbcinsiyet.Text = "";
            cmbeğitim.Text = "";
            cmbişdeneyimidurumu.Text = "";
            cmbkan.Text = "";
            cmbmedeni.Text = "";
            cmbpozisyon.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            mskceptel.Text = "";
            mskceptel2.Text = "";
            mskevtel.Text = "";
            maskedTextBox11.Text = "";
            maskedTextBox12.Text = "";
            maskedTextBox13.Text = "";
            maskedTextBox14.Text = "";
            maskedTextBox15.Text = "";

            datetimeDogumtarih.Value = DateTime.Now;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            datemezuntarih.Value = DateTime.Now;
            pictureBox1.Image = null;




        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }
        TabPage currentPage;
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            tabControl1.SelectedTab = currentPage;
        }
    }
    }

