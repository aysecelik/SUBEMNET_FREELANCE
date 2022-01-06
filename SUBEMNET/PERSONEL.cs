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
    public partial class PERSONEL : Form
    {
        public PERSONEL()
        {
            InitializeComponent();
        }
        void temizle()
        {
            dateTimePicker7.Value = DateTime.Now;
            comboBox3.Text = "";
            textBox12.Text = "";
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker6.Value = DateTime.Now;
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
            cmbsube.Text = "";
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
            textBox16.Text = "";
            textBox15.Text = "";
            textBox13.Text = "";
            textBox14.Text = "00";
            numericUpDown1.Value = 1;



        }
        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("GÜNCELLEMEK YA DA SİLMEK İSTEDİĞİNİZ PERSONEL BİLGİSİNİN ID'SİNE ÇİFT TIKLAYINIZ.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //tümünü göster

            griddoldur();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi as ADI,p.Soyadi as SOYADI,p.CepTel as TELEFON,ş.ŞubeAdi as ŞUBE,p.Pozisyon as POZİSYON,p.Brans as BRANŞ,p.SözleşmeDosya as SÖZLEŞME from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();

            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "GÜNCELLE";
            dgvBtn.Text = "GÜNCELLE";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);

            DataGridViewButtonColumn dgvbtn3 = new DataGridViewButtonColumn();
            dgvbtn3.HeaderText = "AYRILDI";
            dgvbtn3.Text = "AYRILDI";
            dgvbtn3.UseColumnTextForButtonValue = true;
            dgvbtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn3.Width = 70;
            dataGridView1.Columns.Add(dgvbtn3);

            DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
            dgvbtn2.HeaderText = "SİL";
            dgvbtn2.Text = "SİL";
            dgvbtn2.UseColumnTextForButtonValue = true;
            dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn2.Width = 70;
            dataGridView1.Columns.Add(dgvbtn2);



        }
        private void button2_Click(object sender, EventArgs e)
        {
            panel21.Visible = true;
            panel23.Visible = true;
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLEME PLATFORMU

            DialogResult dialogResult = MessageBox.Show("İŞ BAŞVURUSU YAPMIŞ BİR KİŞİYİ Mİ EKLEYECEKSİNİZ?", "PERSONEL EKLEME", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                panel21.Visible = true;
                panel23.Visible = false;
                MessageBox.Show("LÜTFEN PERSONEL OLACAK KİŞİNİN ID'SİNİN ÜSTÜNE TIKLAYINIZ.");
                button14.Visible = true;
                button22.Visible = false;
                button26.Visible = false;
                button25.Visible = false;
                button24.Visible = false;
                button23.Visible = false;
                button29.Visible = false;
                button27.Visible = false;


            }
            else if (dialogResult == DialogResult.No)
            {
                panel21.Visible = true;
                panel23.Visible = true;
                panel2.Visible = true;
                panel1.Visible = true;
                tabControl1.SelectedTab = tabPage1;
                currentPage = tabPage1;
                button14.Visible = true;
                button22.Visible = false;
                button26.Visible = false;
                button25.Visible = false;
                button24.Visible = false;
                button23.Visible = false;
                button29.Visible = false;
                button27.Visible = false;
            }
        }
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //BELGE İNDİRMEK İÇİN
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
                            komut = new SqlCommand("Select Sözleşme from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
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

                    int i = 0;
                    int secilen;
                    secilen = dataGridView1.CurrentCell.RowIndex;
                    panel21.Visible = true;
                    panel23.Visible = true;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    baglan.Open();
                    komut = new SqlCommand("Select Email,Pozisyon,Brans,Cinsiyet,TcKimlikNo,Adi,Soyadi,DogumYeri,EvTel,DogumTarihi,CepTel,CepTel2,Ev_Adres_il, Ev_Adres_ilce" +
                        ",Ev_Adres_Mahalle,EvAdres,IsDeneyimi,Askerlik_Durumu,Medeni_Hal,Baba_Adi,Anne_Adi,NÜfus_Kayıt_il,NÜfus_Kayıt_ilce,KanGrubu,Cilt_No," +
                        "Aile_Sıra_No,SıraNo,EngelDurumuYüzde,Egitim_Durumu,Mezun_Olunan_Kurum,Mezun_Fakulte,Mezun_Bölüm,Mezun_Olunan_Tarih,IsDeneyimDurum,IsDeneyim1," +
                        "IsDeneyim2,IsDeneyimi3,IsDeneyimi4,IsDeneyimi5,IsDeneyim1Tarih,IsDeneyim2Tarih,IsDeneyimi3Tarih,IsDeneyimi4Tarih,IsDeneyimi5Tarih, " +
                        " SicilKaydı, SicilKaydıDosya, SağlıkRaporu, SağlıkRaporuDosya,foto,SGKBelgesi, SGKBelgesiDosya, Sözleşme,SözleşmeDosya,Sube,SozlesmeBaslangicTarih,SozlesmeBitisTarih" +
                        ", SGKBaslangic,SGKBitis,IseBaslangıcTarih,ikametgahDosya,ikametgah,KimlikFotokopi,kimlik,maasi,maaştarih from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
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
                        if (oku[44] != DBNull.Value)
                        {
                            bytes2 = (byte[])oku[44];
                            textBox6.Text = oku.GetValue(45).ToString();
                        }
                        if (oku[46] != DBNull.Value)
                        {
                            bytes3 = (byte[])oku[46];
                            textBox7.Text = oku.GetValue(47).ToString();
                        }
                        if (oku[49] != DBNull.Value)
                        {
                            bytes4 = (byte[])oku[49];
                            textBox12.Text = oku.GetValue(50).ToString();
                        }
                        if (oku[51] != DBNull.Value)
                        {
                            bytes = (byte[])oku[51];
                            textBox1.Text = oku.GetValue(52).ToString();
                        }
                        if (oku[59] != DBNull.Value)
                        {
                            bytes6 = (byte[])oku[59];
                            textBox16.Text = oku.GetValue(60).ToString();
                        }
                        if (oku[61] != DBNull.Value)
                        {
                            bytes7 = (byte[])oku[61];
                            textBox15.Text = oku.GetValue(62).ToString();
                        }
                        string s = ((decimal)oku.GetValue(63)).ToString();
                        string[] parts = s.Split(',');
                        int i1 = Convert.ToInt32(parts[0]);
                        int i2 = Convert.ToInt32(parts[1]);
                        textBox13.Text = i1.ToString();
                        textBox14.Text = i2.ToString();
                        numericUpDown1.Value = (int)oku.GetValue(64);



                        cmbsube.Text = cmbsube.Items[subeid.IndexOf((int)oku[53])].ToString();
                        dateTimePicker3.Value = Convert.ToDateTime(oku.GetValue(54));
                        dateTimePicker4.Value = Convert.ToDateTime(oku.GetValue(55));
                        dateTimePicker5.Value = Convert.ToDateTime(oku.GetValue(56));
                        dateTimePicker6.Value = Convert.ToDateTime(oku.GetValue(57));
                        dateTimePicker7.Value = Convert.ToDateTime(oku.GetValue(58));

                        if (oku[48] != DBNull.Value)
                        {
                            bytes5 = (byte[])oku[48];
                            MemoryStream mr = new MemoryStream(bytes5);
                            pictureBox1.Image = Image.FromStream(mr);
                        }
                        else
                        {
                            pictureBox1.Image = null;
                        }







                    }
                    baglan.Close();
                    panel21.Visible = true;
                    panel23.Visible = true;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    tabControl1.SelectedTab = tabPage1;
                    currentPage = tabPage1;
                    button22.Visible = true;
                    button26.Visible = true;
                    button25.Visible = true;
                    button24.Visible = true;
                    button23.Visible = true;
                    button29.Visible = true;
                    button27.Visible = true;

                }
                else if (dataGridView1.CurrentCell.ColumnIndex == 9)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;

                    DialogResult result = MessageBox.Show("AYRILDI İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "PERSONEL AYRILMA", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " İSİMLİ PERSONELİN DURUMUNU AYRILDI OLARAK DEĞİŞTİRMEK İSTEDİĞİNİZDEN EMİN MİSİNİZ^?", "PERSONEL AYRILMA", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                baglan.Open();
                                SqlCommand degistir = new SqlCommand("update Personeller set Aktiflik=@a1, AyrılmaTarih=@a3 where ID=@a2 ", baglan);
                                degistir.Parameters.AddWithValue("@a2", dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                degistir.Parameters.AddWithValue("@a1", Convert.ToBoolean("false"));
                                degistir.Parameters.AddWithValue("@a3", DateTime.Now.Date);
                                degistir.ExecuteNonQuery();
                                baglan.Close();
                                MessageBox.Show("Kayıt Güncellendi.");
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

                else if (dataGridView1.CurrentCell.ColumnIndex == 10)
                {
                    DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "PERSONEL SİLME", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        int secilen = dataGridView1.CurrentCell.RowIndex;
                        DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " İSİMLİ PERSONELİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "PERSONEL SİLME", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            try
                            {
                                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                                string sql = "DELETE FROM Personeller WHERE ID=@id";
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
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //GÜNCELLEME İÇİN
        }

        private void PERSONEL_Load(object sender, EventArgs e)
        {
            panel21.Visible = false;
            tabPage1.Text = "GENEL BİLGİLER";
            tabPage2.Text = "EĞİTİM BİLGİLERİ";
            tabPage3.Text = "İŞ DENEYİMİ";
            tabPage4.Text = "EK BELGELER";
            subeid.Clear();

            datetimeDogumtarih.Format = DateTimePickerFormat.Custom;
            datetimeDogumtarih.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            dateTimePicker5.Format = DateTimePickerFormat.Custom;
            dateTimePicker5.CustomFormat = "dd/MM/yyyy";
            dateTimePicker6.Format = DateTimePickerFormat.Custom;
            dateTimePicker6.CustomFormat = "dd/MM/yyyy";
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy";
            dateTimePicker26.Format = DateTimePickerFormat.Custom;
            dateTimePicker26.CustomFormat = "dd/MM/yyyy";
            dateTimePicker25.Format = DateTimePickerFormat.Custom;
            dateTimePicker25.CustomFormat = "dd/MM/yyyy";
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
            cmbmedeni.Items.Add("");
            cmbmedeni.Items.Add("BEKAR");
            cmbmedeni.Items.Add("EVLİ");

            cmbaskerlik.Items.Add("");
            cmbaskerlik.Items.Add("YAPILDI");
            cmbaskerlik.Items.Add("YAPILMADI");
            cmbaskerlik.Items.Add("MUAF");
            comboBox1.Items.Add("");
            comboBox2.Items.Add("");
            comboBox14.Items.Add("");
            comboBox13.Items.Add("");
            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbpozisyon.Items.Add(oku[0].ToString());
                comboBox1.Items.Add(oku[0].ToString());
                comboBox14.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbBranş.Items.Add(oku2[0].ToString());
                comboBox2.Items.Add(oku2[0].ToString());
                comboBox13.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbsube.Items.Add(oku3[0].ToString());
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }
            baglan.Close();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
            temizle();
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select p.ID,p.Adi as ADI,p.Soyadi as SOYADI,p.CepTel as TELEFON,ş.ŞubeAdi as ŞUBE,p.Pozisyon as POZİSYON,p.Brans as BRANŞ,p.SözleşmeDosya as SÖZLEŞME from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";
                    filtre += " p.ID = '" + textBox11.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Email = '" + textBox9.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Brans = '" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Adi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Soyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.CepTel = '" + maskedTextBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.IseBaslangıcTarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox9.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox1.Checked == false)
                {
                    filtre = "Select p.ID,p.Adi as ADI,p.Soyadi as SOYADI,p.CepTel as TELEFON,ş.ŞubeAdi as ŞUBE,p.Pozisyon as POZİSYON,p.Brans as BRANŞ,p.SözleşmeDosya as SÖZLEŞME from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "GÜNCELLE";
                dgvBtn.Text = "GÜNCELLE";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);

                DataGridViewButtonColumn dgvbtn3 = new DataGridViewButtonColumn();
                dgvbtn3.HeaderText = "AYRILDI";
                dgvbtn3.Text = "AYRILDI";
                dgvbtn3.UseColumnTextForButtonValue = true;
                dgvbtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvbtn3.Width = 70;
                dataGridView1.Columns.Add(dgvbtn3);

                DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
                dgvbtn2.HeaderText = "SİL";
                dgvbtn2.Text = "SİL";
                dgvbtn2.UseColumnTextForButtonValue = true;
                dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvbtn2.Width = 70;
                dataGridView1.Columns.Add(dgvbtn2);
                panel21.Visible = false;
                temizle();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
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

        private void button9_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
            temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            temizle();
            panel21.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            temizle();
            panel21.Visible = false;
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

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
            currentPage = tabPage3;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            temizle();
            panel21.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox13.Text) == true && string.IsNullOrEmpty(textBox14.Text) == true)
            {
                MessageBox.Show("MAAŞ BİLGİSİNİN GİRİLMESİ ZORUNLUDUR.");
            }
            else
            {
                try
                {
                    bool degisken = false;
                    string filtre = "insert into Personeller (";
                    string values = "Values (";
                    if (string.IsNullOrEmpty(txteposta.Text) == false)
                    {
                        filtre += " Email ";
                        values += "'" + txteposta.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbsube.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";


                        }
                        filtre += " Sube ";
                        values += "'" + subeid[cmbsube.SelectedIndex] + "'";
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
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Brans  ";
                        values += "'" + cmbBranş.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbcinsiyet.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Cinsiyet ";
                        values += "'" + cmbcinsiyet.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtTC.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " TcKimlikNo ";
                        values += "'" + txtTC.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtAd.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Adi ";
                        values += "'" + txtAd.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtSoyad.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Soyadi ";
                        values += "'" + txtSoyad.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (degisken == true)
                    {
                        filtre += " , "; values += " , ";

                    }
                    filtre += " Aktiflik ";
                    values += "'" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                    if (datetimeDogumtarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " DogumTarihi ";
                        values += "'" + datetimeDogumtarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtdogumyeri.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " DogumYeri ";
                        values += "'" + txtdogumyeri.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskevtel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " EvTel ";
                        values += "'" + mskevtel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " CepTel ";
                        values += "'" + mskceptel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel2.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " CepTel2 ";
                        values += "'" + mskceptel2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Ev_Adres_il ";
                        values += "'" + txtevil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevilce.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Ev_Adres_ilce ";
                        values += "'" + txtevilce.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevmahalle.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Ev_Adres_Mahalle ";
                        values += "'" + txtevmahalle.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevadres.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Ev_Adres ";
                        values += "'" + txtevadres.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtisdeneyim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " IsDeneyimi ";
                        values += txtisdeneyim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbaskerlik.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Askerlik_Durumu ";
                        values += "'" + cmbaskerlik.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbmedeni.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Medeni_Hal ";
                        values += "'" + cmbmedeni.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtbabaadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Baba_Adi ";
                        values += "'" + txtbabaadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtanneadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Anne_Adi ";
                        values += "'" + txtanneadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Nüfus_Kayıt_il ";
                        values += "'" + txtnüfusil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusilçe.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Nüfus_Kayıt_ilce ";
                        values += "'" + txtnüfusilçe.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbkan.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " KanGrubu ";
                        values += "'" + cmbkan.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtciltno.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Cilt_No ";
                        values += "'" + txtciltno.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtailesırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Aile_Sıra_No ";
                        values += "'" + txtailesırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtsırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " SıraNo ";
                        values += "'" + txtsırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtengeldurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";
                        }
                        filtre += " EngelDurumuYüzde ";
                        values += "'" + txtengeldurumu.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbeğitim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Egitim_Durumu ";
                        values += "'" + cmbeğitim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtegitimdurum.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Mezun_Olunan_Kurum ";
                        values += "'" + txtegitimdurum.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtFakülte.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Mezun_Fakulte ";
                        values += "'" + txtFakülte.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtBölüm.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Mezun_Bölüm ";
                        values += "'" + txtBölüm.Text + "'";
                        degisken = true;
                    }
                    if (datemezuntarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Mezun_Olunan_Tarih ";
                        values += "'" + datemezuntarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbişdeneyimidurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " IsDeneyimDurum ";
                        values += "'" + cmbişdeneyimidurumu.Text + "'";
                        degisken = true;
                    }
                    if (cmbişdeneyimidurumu.Text == "VAR")
                    {
                        if (string.IsNullOrEmpty(txtkurum1.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyim1 ";
                            values += "'" + txtkurum1.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox2.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyim2 ";
                            values += "'" + textBox2.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox3.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyimi3 ";
                            values += "'" + textBox3.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox4.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyimi4 ";
                            values += "'" + textBox4.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox5.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyimi5 ";
                            values += "'" + textBox5.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox11.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyim1Tarih ";
                            values += "'" + maskedTextBox11.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox12.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyim2Tarih ";
                            values += "'" + maskedTextBox12.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox13.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyimi3Tarih ";
                            values += "'" + maskedTextBox13.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox14.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

                            }
                            filtre += " IsDeneyimi4Tarih ";
                            values += "'" + maskedTextBox14.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox15.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , "; values += " , ";

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
                            filtre += " , "; values += " , ";

                        }
                        filtre += " foto ";
                        values += "@a5";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Sözleşme, SözleşmeDosya ";
                        values += "@a1, '" + textBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " SicilKaydı, SicilKaydıDosya ";
                        values += "@a2, '" + textBox6.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " SağlıkRaporu, SağlıkRaporuDosya ";
                        values += "@a3, '" + textBox7.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " SGKBelgesi, SGKBelgesiDosya ";
                        values += " @a4 , '" + textBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox16.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " ikametgah, ikametgahDosya ";
                        values += " @a6 , '" + textBox16.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox15.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " KimlikFotokopi, kimlik ";
                        values += " @a7 , '" + textBox15.Text + "'";
                        degisken = true;
                    }
                    filtre += " , "; values += " , ";


                    filtre += " SozlesmeBaslangicTarih ";
                    values += "'" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";

                    filtre += " , "; values += " , ";


                    filtre += " SozlesmeBitisTarih ";
                    values += "'" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "'";
                    filtre += " , "; values += " , ";


                    filtre += " SGKBaslangic ";
                    values += "'" + dateTimePicker5.Value.Date.ToString("yyyyMMdd") + "'";

                    filtre += " , "; values += " , ";

                    filtre += " MAAŞ ";
                    values += "'" + string.Format("{0:N}", Convert.ToDecimal(textBox13.Text + "," + textBox14.Text)) + "'";
                    filtre += " , "; values += " , ";
                    filtre += " maasi ";
                    values += "'" + textBox13.Text + "." + textBox14.Text + "'";
                    filtre += " , "; values += " , ";
                    filtre += " maaştarih ";
                    values += "'" + numericUpDown1.Value + "'";
                    filtre += " , "; values += " , ";


                    filtre += " SGKBitis ";
                    values += "'" + dateTimePicker6.Value.Date.ToString("yyyyMMdd") + "'";


                    filtre += ", IseBaslangıcTarih )";
                    values += ", '" + dateTimePicker7.Value.Date.ToString("yyyyMMdd") + "')";
                    filtre += values;
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;

                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a2", SqlDbType.VarBinary).Value = bytes2;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a3", SqlDbType.VarBinary).Value = bytes3;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a4", SqlDbType.VarBinary).Value = bytes4;
                    }
                    if (string.IsNullOrEmpty(textBox16.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a6", SqlDbType.VarBinary).Value = bytes6;
                    }
                    if (string.IsNullOrEmpty(textBox15.Text) == false)
                    {
                        komutkaydet.Parameters.AddWithValue("@a7", SqlDbType.VarBinary).Value = bytes7;
                    }
                    if (pictureBox1.Image != null)
                    {
                        komutkaydet.Parameters.AddWithValue("@a5", SqlDbType.VarBinary).Value = bytes5;
                    }
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    panel21.Visible = false;

                    temizle();
                    button14.Visible = false;


                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());

                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            tabControl1.SelectedTab = tabPage3;
            currentPage = tabPage3;

        }

        private void cmbişdeneyimidurumu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbişdeneyimidurumu.Text.Replace(" ", "") == "" || cmbişdeneyimidurumu.Text == "YOK")
            {
                panel9.Visible = false;
            }
            else
            {
                panel9.Visible = true;
            }
        }
        string sözleşmefilename;
        string SicilFilename;
        string Saglikfilename;
        string SGKfilename;

        byte[] bytes;
        byte[] bytes2;
        byte[] bytes3;
        byte[] bytes4;
        byte[] bytes5;
        byte[] bytes6;
        byte[] bytes7;



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
                    if (filePath != null)
                    {
                        Stream fs = File.OpenRead(filePath);
                        BinaryReader br = new BinaryReader(fs);
                        bytes = br.ReadBytes((Int32)fs.Length);
                        FileInfo fileinfo = new FileInfo(open.FileName);
                        sözleşmefilename = fileinfo.Name;
                        textBox1.Text = fileinfo.Name;
                    }
                }
            }
        }
        string path;
        private void button18_Click(object sender, EventArgs e)
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
                    bytes2 = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    SicilFilename = fileinfo.Name;
                    textBox6.Text = fileinfo.Name;

                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
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
                    bytes3 = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    Saglikfilename = fileinfo.Name;
                    textBox7.Text = fileinfo.Name;

                }
            }
        }

        private void btnsgk_Click(object sender, EventArgs e)
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
                    bytes4 = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    SGKfilename = fileinfo.Name;
                    textBox12.Text = fileinfo.Name;

                }
            }
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
                path = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);


            }
            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/jpg";

            if (contenttype != String.Empty)
            {
                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes5 = br.ReadBytes((Int32)fs.Length);


            }
        }

        private void button40_Click(object sender, EventArgs e)
        {

            baglan.Open();
            da = new SqlDataAdapter("Select ID,Email as 'E-POSTA',Adi as ADI,Soyadi as SOYADI,CepTel as TELEFON,Pozisyon as POZİSYON,Branş AS BRANŞ,CVDosya AS CV, SicilDosya AS SİCİL, SaglikRaporDosya AS 'SAĞLIK RAPORU'from IsBasvuru where okulid='" + okulid + "'", baglan);

            cmdb = new SqlCommandBuilder(da);

            ds = new DataSet();
            da.Fill(ds, "IsBasvuru");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();

        }
        string[] dizi;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (dataGridView2.CurrentCell.ColumnIndex == 7)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select CV from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }
                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 8)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select Sicil from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }

                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 9)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select SaglikRapor from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }


                }
                else
                {
                    temizle();
                    int i = 0;
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    panel21.Visible = true;
                    panel23.Visible = true;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    baglan.Open();
                    komut = new SqlCommand("Select Email,Pozisyon,Branş,Cinsiyet,TcKimlikNo,Adi,Soyadi,DogumYeri,EvTel,DogumTarihi,CepTel,CepTel2,Ev_Adres_il, Ev_Adres_ilce" +
                        ",Ev_Adres_Mahalle,Ev_Adres,Is_Deneyimi,Askerlik_Durumu,Medeni_Hal,Baba_Adi,Anne_Adi,Nufus_Kayıt_il,Nufus_Kayıt_ilce,KanGrubu,Cilt_No," +
                        "Aile_Sıra_No,SıraNo,EngelDurumuYüzde,Egitim_Durumu,Mezun_Olunan_Kurum,Mezun_Fakulte,Mezun_Bölüm,Mezun_Olunan_Tarih,IsDeneyimiDurum,IsDeneyimi1," +
                        "IsDeneyimi2,IsDeneyimi3,IsDeneyimi4,IsDeneyimi5,IsDeneyimi1Tarih,IsDeneyimi2Tarih,IsDeneyimi3Tarih,IsDeneyimi4Tarih,IsDeneyimi5Tarih, " +
                        " Sicil, SicilDosya, SaglikRapor, SaglikRaporDosya,foto from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
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
                            bytes5 = (byte[])oku[48];
                            MemoryStream mr = new MemoryStream(bytes5);
                            pictureBox1.Image = Image.FromStream(mr);
                        }


                    }
                    baglan.Close();
                    panel21.Visible = true;
                    panel23.Visible = true;
                    panel2.Visible = true;
                    panel1.Visible = true;
                    tabControl1.SelectedTab = tabPage1;
                    currentPage = tabPage1;



                }
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            panel23.Visible = true;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel23.Visible = false;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            //arama işlemi
            try
            {

                bool degisken = true;
                string filtre = "Select ID,Email as 'E-POSTA',Adi as ADI,Soyadi as SOYADI,CepTel as TELEFON,Pozisyon as POZİSYON,Branş AS BRANŞ,CVDosya AS CV, SicilDosya AS SİCİL, SaglikRaporDosya AS 'SAĞLIK RAPORU'from IsBasvuru where okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox42.Text) == false)
                {
                    filtre += " AND ";

                    filtre += " ID = '" + textBox42.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox45.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Email = '" + textBox45.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox14.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Pozisyon = '" + comboBox14.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Branş = '" + comboBox13.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox44.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Adi = '" + textBox44.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox43.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Soyadi = '" + textBox43.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox5.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " CepTel = '" + maskedTextBox5.Text + "'";
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BasvuruTarihi between '" + dateTimePicker26.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker25.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox42.Text) == true && string.IsNullOrEmpty(textBox45.Text) == true && string.IsNullOrEmpty(textBox43.Text) == true && string.IsNullOrEmpty(textBox44.Text) == true && string.IsNullOrEmpty(comboBox13.Text) == true && string.IsNullOrEmpty(comboBox14.Text) == true && string.IsNullOrEmpty(maskedTextBox5.Text.Replace(" ", "")) == true && checkBox2.Checked == false)
                {
                    filtre = "Select ID,Email as E-POSTA,Adi as ADI,Soyadi as SOYADI,CepTel as TELEFON,Pozisyon as POZİSYON,Branş AS BRANŞ,CVDosya AS CV, SicilDosya AS SİCİL, SaglikRaporDosya AS 'SAĞLIK RAPORU'from IsBasvuru where okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "IsBasvuru");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
                panel23.Visible = false;
                textBox42.Text = "";
                textBox45.Text = "";
                textBox43.Text = "";
                textBox44.Text = "";
                maskedTextBox5.Text = "";
                comboBox13.Text = "";
                comboBox14.Text = "";
                dateTimePicker26.Value = DateTime.Now;
                dateTimePicker25.Value = DateTime.Now;
                checkBox2.Checked = false;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox13.Text) == true && string.IsNullOrEmpty(textBox14.Text) == true)
            {
                MessageBox.Show("MAAŞ BİLGİSİNİN GİRİLMESİ ZORUNLUDUR.");
            }
            else
            {
                try
                {
                    //Güncelleme işlemi

                    bool degisken = false;
                    string filtre = "update Personeller set";
                    if (string.IsNullOrEmpty(txteposta.Text) == false)
                    {
                        filtre += " Email= " + "'" + txteposta.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbsube.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Sube =" + "'" + subeid[cmbsube.SelectedIndex] + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(cmbpozisyon.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Pozisyon= " + "'" + cmbpozisyon.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbBranş.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Brans = " + "'" + cmbBranş.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbcinsiyet.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Cinsiyet =" + "'" + cmbcinsiyet.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtTC.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " TcKimlikNo= " + "'" + txtTC.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtAd.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Adi =" + "'" + txtAd.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtSoyad.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Soyadi= " + "'" + txtSoyad.Text.ToUpper() + "'";
                        degisken = true;
                    }

                    filtre += " , Aktiflik =" + "'" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                    if (datetimeDogumtarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " DogumTarihi= " + "'" + datetimeDogumtarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtdogumyeri.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " DogumYeri= " + "'" + txtdogumyeri.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskevtel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " EvTel =" + "'" + mskevtel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " CepTel= " + "'" + mskceptel.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(mskceptel2.Text.Replace(" ", "")) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " CepTel2= " + "'" + mskceptel2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Ev_Adres_il= " + "'" + txtevil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevilce.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Ev_Adres_ilce =" + "'" + txtevilce.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevmahalle.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Ev_Adres_Mahalle= " + "'" + txtevmahalle.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtevadres.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Ev_Adres= " + "'" + txtevadres.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtisdeneyim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " IsDeneyimi= '" + txtisdeneyim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbaskerlik.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Askerlik_Durumu= " + "'" + cmbaskerlik.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbmedeni.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Medeni_Hal= " + "'" + cmbmedeni.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtbabaadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Baba_Adi =" + "'" + txtbabaadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtanneadi.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Anne_Adi =" + "'" + txtanneadi.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusil.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Nüfus_Kayıt_il= " + "'" + txtnüfusil.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtnüfusilçe.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Nüfus_Kayıt_ilce =" + "'" + txtnüfusilçe.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbkan.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " KanGrubu =" + "'" + cmbkan.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtciltno.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Cilt_No =" + "'" + txtciltno.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtailesırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Aile_Sıra_No =" + "'" + txtailesırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtsırano.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " SıraNo = " + "'" + txtsırano.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtengeldurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " EngelDurumuYüzde =" + "'" + txtengeldurumu.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbeğitim.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Egitim_Durumu =" + "'" + cmbeğitim.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtegitimdurum.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Mezun_Olunan_Kurum =" + "'" + txtegitimdurum.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtFakülte.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Mezun_Fakulte =" + "'" + txtFakülte.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(txtBölüm.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Mezun_Bölüm =" + "'" + txtBölüm.Text + "'";
                        degisken = true;
                    }
                    if (datemezuntarih.Value.Date != DateTime.Now.Date)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Mezun_Olunan_Tarih =" + "'" + datemezuntarih.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(cmbişdeneyimidurumu.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " IsDeneyimDurum =" + "'" + cmbişdeneyimidurumu.Text + "'";
                        degisken = true;
                    }
                    if (cmbişdeneyimidurumu.Text == "VAR")
                    {
                        if (string.IsNullOrEmpty(txtkurum1.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyim1 =" + "'" + txtkurum1.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox2.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyim2= " + "'" + textBox2.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox3.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi3 =" + "'" + textBox3.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox4.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi4 =" + "'" + textBox4.Text + "'";
                            degisken = true;
                        }
                        if (string.IsNullOrEmpty(textBox5.Text) == false)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi5 =" + "'" + textBox5.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox11.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyim1Tarih =" + "'" + maskedTextBox11.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox12.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyim2Tarih =" + "'" + maskedTextBox12.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox13.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi3Tarih =" + "'" + maskedTextBox13.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox14.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi4Tarih =" + "'" + maskedTextBox14.Text + "'";
                            degisken = true;
                        }
                        if (maskedTextBox15.MaskFull == true)
                        {
                            if (degisken == true)
                            {
                                filtre += " , ";

                            }
                            filtre += " IsDeneyimi5Tarih =" + "'" + maskedTextBox15.Text + "'";
                            degisken = true;


                        }

                    }
                    if (pictureBox1.Image != null)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " foto=@a5 ";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Sözleşme=@a1, SözleşmeDosya =" + "'" + textBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " SicilKaydı=@a2, SicilKaydıDosya =" + "'" + textBox6.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " SağlıkRaporu=@a3, SağlıkRaporuDosya= " + "'" + textBox7.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                        }
                        filtre += " SGKBelgesi = @a4 , SGKBelgesiDosya= " + "'" + textBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox16.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " ikametgah=@a6, ikametgahDosya= " + "'" + textBox16.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox15.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " KimlikFotokopi=@a7, kimlik=" + "'" + textBox15.Text + "'";
                        degisken = true;
                    }
                    filtre += " , ";

                    filtre += " SozlesmeBaslangicTarih = " + "'" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";

                    filtre += " , ";

                    filtre += " SozlesmeBitisTarih =" + "'" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "'";
                    filtre += " , ";

                    filtre += " SGKBaslangic =" + "'" + dateTimePicker5.Value.Date.ToString("yyyyMMdd") + "'";

                    filtre += " , ";

                    filtre += " SGKBitis = " + "'" + dateTimePicker6.Value.Date.ToString("yyyyMMdd") + "'";
                    filtre += " , ";
                    filtre += " MAAŞ=" + "'" + string.Format("{0:N}", Convert.ToDecimal(textBox13.Text + "," + textBox14.Text)) + "'";
                    filtre += " , ";
                    filtre += "maasi="+"'" + textBox13.Text + "." + textBox14.Text + "'";
                    filtre += " , ";
                    filtre += "maaştarih=" + "'" + numericUpDown1.Value+ "'";
                    filtre += ", IseBaslangıcTarih = '" + dateTimePicker7.Value.Date.ToString("yyyyMMdd") + "'";
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    filtre += "  where ID = " + dataGridView1.Rows[secilen].Cells[0].Value.ToString();

                    baglan.Open();
                    SqlCommand degistir = new SqlCommand(filtre, baglan);
                    if (string.IsNullOrEmpty(textBox1.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;

                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a2", SqlDbType.VarBinary).Value = bytes2;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a3", SqlDbType.VarBinary).Value = bytes3;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a4", SqlDbType.VarBinary).Value = bytes4;
                    }
                    if (string.IsNullOrEmpty(textBox16.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a6", SqlDbType.VarBinary).Value = bytes6;
                    }
                    if (string.IsNullOrEmpty(textBox15.Text) == false)
                    {
                        degistir.Parameters.AddWithValue("@a7", SqlDbType.VarBinary).Value = bytes7;
                    }
                    if (pictureBox1.Image != null)
                    {
                        degistir.Parameters.AddWithValue("@a5", SqlDbType.VarBinary).Value = bytes5;
                    }

                    degistir.ExecuteNonQuery();
                    temizle();
                    button22.Visible = false;
                    button26.Visible = false;
                    button25.Visible = false;
                    button24.Visible = false;
                    button23.Visible = false;

                    baglan.Close();
                    panel21.Visible = false;

                    MessageBox.Show("Kayıt Güncellendi.");

                    griddoldur();
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select Sözleşme from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select SicilKaydı from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void button24_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select SağlıkRaporu from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select SGKBelgesi from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void button30_Click(object sender, EventArgs e)
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
                    if (filePath != null)
                    {
                        Stream fs = File.OpenRead(filePath);
                        BinaryReader br = new BinaryReader(fs);
                        bytes6 = br.ReadBytes((Int32)fs.Length);
                        FileInfo fileinfo = new FileInfo(open.FileName);
                        sözleşmefilename = fileinfo.Name;
                        textBox16.Text = fileinfo.Name;
                    }
                }
            }
        }

        private void button28_Click(object sender, EventArgs e)
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
                    if (filePath != null)
                    {
                        Stream fs = File.OpenRead(filePath);
                        BinaryReader br = new BinaryReader(fs);
                        bytes7 = br.ReadBytes((Int32)fs.Length);
                        FileInfo fileinfo = new FileInfo(open.FileName);
                        sözleşmefilename = fileinfo.Name;
                        textBox15.Text = fileinfo.Name;
                    }
                }
            }
        }

        private void button29_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select ikametgah from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select KimlikFotokopi from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    oku.Read();
                    byteData = (byte[])oku[0];
                    File.WriteAllBytes(savefile.FileName, byteData);
                    baglan.Close();
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void tabControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        TabPage currentPage;
        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
       
                tabControl1.SelectedTab = currentPage;
             
        
        }
    }
}
