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
    public partial class İÇ_KAYIT : Form
    {
        public İÇ_KAYIT()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel12.Visible = false;
            textBox10.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel12.Visible = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage4;

        }
        public string query;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        SqlTransaction myTransaction = null;

        byte[] bytes;
        string path;
        public string OgrID;
        void doldur()
        {
            baglan.Open();
            komut = new SqlCommand("Select ö.ID,ö.SozNo,ö.TCKN,ö.Cinsiyet,ö.Adi,ö.Soyadi,ö.DogumTarihi,ö.Program,ö.Devre,ö.Kur,ö.Okul,ö.Eposta,ö.EvTel,ö.OgrCepTel,ö.Sehir,ö.Ilce,ö.Mahalle,ö.Adres,ö.MaliDurum,ö.OzelBilgi1," +
                "ö.OzelBilgi2,ö.OzelBilgi3,ö.Aciklama, ö.Durum, ö.KayitTarihi, ö.KayitSilinmeTarihi, ö.OlusturmaTarihi, ş.ŞubeAdi, ö.Snf,  ö.ServisId, ö.SabahOgleId, ö.RehberId, ö.SinifOgrId, ö.DanismanId, ö.KayitSilmeNedeni," +
                "ö.KanGrubu, ö.DogumYeri, ö.Hastalik, ö.AnneBabaAyri,ö.OkulNo,foto from Ogrenci ö. join Sube ş on ş.ID=ö.Sube where TCKN = '" + textBox10.Text + "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                OgrID = oku.GetValue(0).ToString();
                txtSN.Text = oku.GetValue(1).ToString();
                txtTC.Text = oku.GetValue(2).ToString();
                cmbCins.Text = oku.GetValue(3).ToString();
                txtAd.Text = oku.GetValue(4).ToString();
                txtSoyad.Text = oku.GetValue(5).ToString();
                dtDogumTar.Text = oku.GetValue(6).ToString();
                cmbProg.Text = oku.GetValue(7).ToString();
                cmbDevre.Text = oku.GetValue(8).ToString();
                cmbKur.Text = oku.GetValue(9).ToString();
                txtOkul.Text = oku.GetValue(10).ToString();
                txtEposta.Text = oku.GetValue(11).ToString();
                txtEvTel.Text = oku.GetValue(12).ToString();
                mskOgrCep.Text = oku.GetValue(13).ToString();
                comboBoxSehir.Text = oku.GetValue(14).ToString();
                comboBoxIlce.Text = oku.GetValue(15).ToString();
                comboBoxMah.Text = oku.GetValue(16).ToString();
                txtAdres.Text = oku.GetValue(17).ToString();
                txtMaliD.Text = oku.GetValue(18).ToString();
                txtOb1.Text = oku.GetValue(19).ToString();
                txtOb2.Text = oku.GetValue(20).ToString();
                txtOb3.Text = oku.GetValue(21).ToString();
                txtAciklama.Text = oku.GetValue(22).ToString();
                txtOkulNo.Text = oku.GetValue(40).ToString();
                dtKayTar.Text = oku.GetValue(24).ToString();

                cmbsube.Text = oku.GetValue(27).ToString();
                cmbSnf.Text = oku.GetValue(28).ToString();
                textBox11.Text = oku.GetValue(30).ToString();
                cmbSO.Text = oku.GetValue(31).ToString();
                cmbRO.Text = oku.GetValue(32).ToString();
                cmbSnfOgr.Text = oku.GetValue(33).ToString();
                cmbDns.Text = oku.GetValue(34).ToString();
                cmbKanG.Text = oku.GetValue(36).ToString();
                txtDogumYeri.Text = oku.GetValue(37).ToString();
                txtHasta.Text = oku.GetValue(38).ToString();
                if (oku.GetValue(39).ToString() == "1")
                    chkVeli.Checked = true;
                if (oku[41] == DBNull.Value)
                {
                    pictureBox1.Image = null;

                }
                else
                {
                    bytes = (byte[])oku[40];
                    MemoryStream mr = new MemoryStream(bytes);
                    pictureBox1.Image = Image.FromStream(mr);
                }
            }
            baglan.Close();
        }
        void doldur1()
        {
            baglan.Open();
            komut = new SqlCommand("Select Id,AdSoyad,TCKN,DogumTarihi,Meslek,CepTel,EvTel,IsTel,EvAdres,IsAdres,Eposta,Yakinlik from Veli where OgrID = '" + OgrID + "' and VeliMi=1", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtVeliAd.Text = oku.GetValue(1).ToString();
                txtVeliTc.Text = oku.GetValue(2).ToString();
                dtVeliDT.Text = oku.GetValue(3).ToString();
                txtVeliMeslek.Text = oku.GetValue(4).ToString();
                mskVeliCep.Text = oku.GetValue(5).ToString();
                txtVeliEvTel.Text = oku.GetValue(6).ToString();
                txtVeliIsTel.Text = oku.GetValue(7).ToString();
                txtVeliEvAdres.Text = oku.GetValue(8).ToString();
                txtVeliIsAdres.Text = oku.GetValue(9).ToString();
                txtVeliEposta.Text = oku.GetValue(10).ToString();
                txtVeliYakinlik.Text = oku.GetValue(11).ToString();
            }
            baglan.Close();
        }
        void doldur2()
        {
            baglan.Open();
            komut = new SqlCommand("Select Id,AdSoyad,TCKN,DogumTarihi,Meslek,CepTel,EvTel,IsTel,EvAdres,IsAdres,Eposta,Sag from Veli where OgrID = '" + OgrID + "' and veliTip=1", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtAnneAd.Text = oku.GetValue(1).ToString();
                txtAnneTc.Text = oku.GetValue(2).ToString();
                dtAnneDT.Text = oku.GetValue(3).ToString();
                txtAnneMeslek.Text = oku.GetValue(4).ToString();
                mskAnne.Text = oku.GetValue(5).ToString();
                txtAnneEvTel.Text = oku.GetValue(6).ToString();
                txtAnneIsTel.Text = oku.GetValue(7).ToString();
                txtAnneEvAdres.Text = oku.GetValue(8).ToString();
                txtAnneIsAdres.Text = oku.GetValue(9).ToString();
                txtAnneEposta.Text = oku.GetValue(10).ToString();
                if (oku.GetValue(11).ToString() == "1")
                    chkAnne.Checked = true;
            }
            baglan.Close();
        }
        void doldur3()
        {
            baglan.Open();
            komut = new SqlCommand("Select Id,AdSoyad,TCKN,DogumTarihi,Meslek,CepTel,EvTel,IsTel,EvAdres,IsAdres,Eposta,Sag from Veli where OgrID = '" + OgrID + "' and veliTip=2", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                txtBabaAd.Text = oku.GetValue(1).ToString();
                txtBabaTc.Text = oku.GetValue(2).ToString();
                dtBabaDT.Text = oku.GetValue(3).ToString();
                txtBabaMeslek.Text = oku.GetValue(4).ToString();
                mskBaba.Text = oku.GetValue(5).ToString();
                txtBabaEvTel.Text = oku.GetValue(6).ToString();
                txtBabaIsTel.Text = oku.GetValue(7).ToString();
                txtBabaEvAdres.Text = oku.GetValue(8).ToString();
                txtBabaIsAdres.Text = oku.GetValue(9).ToString();
                txtBabaEposta.Text = oku.GetValue(10).ToString();
                if (oku.GetValue(11).ToString() == "1")
                    chkBaba.Checked = true;

            }
            baglan.Close();
        }
        int okulid = Form1.okulid;
        void doldurSube()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select*from Sube where okulid='" + okulid + "'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            cmbsube.ValueMember = "ID";
            cmbsube.DisplayMember = "ŞubeAdi";
            cmbsube.DataSource = dt;

            SqlDataAdapter sezon = new SqlDataAdapter("Select Sezon from Sezon where okulid='" + okulid + "'", baglan);
            DataTable sezon1 = new DataTable();
            SqlCommandBuilder sezon2 = new SqlCommandBuilder(sezon);
            sezon.Fill(sezon1);
            comboBox2.DisplayMember = "Sezon";
            comboBox2.DataSource = sezon1;

            SqlDataAdapter dail = new SqlDataAdapter("Select*from Sehir", baglan);
            DataTable dtil = new DataTable();
            SqlCommandBuilder cmdbil = new SqlCommandBuilder(da);
            dail.Fill(dtil);
            comboBoxSehir.ValueMember = "IL_ID";
            comboBoxSehir.DisplayMember = "IL_ADI";
            comboBoxSehir.DataSource = dtil;
            baglan.Close();
        }
        private void button16_Click(object sender, EventArgs e)
        {
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
                bytes = br.ReadBytes((Int32)fs.Length);


            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbsube.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox20.Text) == true || string.IsNullOrEmpty(textBox27.Text) == true)
            {
                MessageBox.Show("ŞUBE,ÖDEME ŞEKLİ VE MİKTAR BİLGİLERİNİ GİRİNİZ.");
            }
            else
            {
                int vId = 0;
                baglan.Open();
                myTransaction = baglan.BeginTransaction();
                try
                {
                    string foto = "";
                    string fotoa = "";
                    if (pictureBox1.Image != null)
                    {
                        foto = "Foto,";
                        fotoa = "@a1";
                    }

                    SqlCommand ekle = new SqlCommand("insert into Ogrenci (" + foto + "Sezon,OkulNo, TCKN, Cinsiyet, Adi,Soyadi, DogumTarihi, Sube, Program, " +
                        "Devre, Kur, Snf,  ServisId, SabahOgleId, RehberId, SinifOgrId, DanismanId, KanGrubu, DogumYeri, Hastalik, Okul, Eposta, EvTel," +
                        " OgrCepTel, Sehir, Ilce, Mahalle, Adres, MaliDurum, OzelBilgi1, OzelBilgi2, OzelBilgi3, Boy, Kilo, Beden, AnneBabaAyri, Aciklama, Durum, KayitTarihi, OlusturmaTarihi, MdrYardId ) " +
                        "values (" + fotoa + " @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23, @a24, @a25, @a26, @a27, @a28, " +
                        "@a29, @a30, @a31, @a32, @a33, @a34, @a35, @a36, @a37, @a38, @a39, @a40, @a41, @a42, @a43)", baglan);
                    if (pictureBox1.Image != null)
                    {
                        ekle.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;
                    }
                    ekle.Parameters.AddWithValue("@a2", comboBox2.Text);
                    ekle.Parameters.AddWithValue("@a3", txtOkulNo.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a4", txtTC.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a5", cmbCins.Text);
                    ekle.Parameters.AddWithValue("@a6", txtAd.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a7", txtSoyad.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a8", dtDogumTar.Value.ToString("yyyy-MM-dd"));
                    ekle.Parameters.AddWithValue("@a9", cmbsube.SelectedValue);
                    ekle.Parameters.AddWithValue("@a10", cmbProg.Text);
                    ekle.Parameters.AddWithValue("@a11", cmbDevre.Text);
                    ekle.Parameters.AddWithValue("@a12", cmbKur.Text);
                    ekle.Parameters.AddWithValue("@a13", cmbSnf.Text);
                    ekle.Parameters.AddWithValue("@a15", textBox11.Text);
                    ekle.Parameters.AddWithValue("@a16", cmbSO.Text);
                    ekle.Parameters.AddWithValue("@a17", cmbRO.Text);
                    ekle.Parameters.AddWithValue("@a18", cmbSnfOgr.Text);
                    ekle.Parameters.AddWithValue("@a19", cmbDns.Text);
                    ekle.Parameters.AddWithValue("@a20", cmbKanG.Text);
                    ekle.Parameters.AddWithValue("@a21", txtDogumYeri.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a22", txtHasta.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a23", txtOkul.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a24", txtEposta.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a25", txtEvTel.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a26", mskOgrCep.Text.Replace(" ", ""));
                    ekle.Parameters.AddWithValue("@a27", comboBoxSehir.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a28", comboBoxIlce.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a29", comboBoxMah.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a30", txtAdres.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a31", txtMaliD.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a32", txtOb1.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a33", txtOb2.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a34", txtOb3.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a35", txtBoy.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a36", txtKilo.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a37", txtBeden.Text.TrimEnd());
                    if (chkVeli.Checked)
                        ekle.Parameters.AddWithValue("@a38", 1);
                    else
                        ekle.Parameters.AddWithValue("@a38", 0);
                    ekle.Parameters.AddWithValue("@a39", txtAciklama.Text.TrimEnd());
                    ekle.Parameters.AddWithValue("@a40", 1);
                    ekle.Parameters.AddWithValue("@a41", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    ekle.Parameters.AddWithValue("@a42", DateTime.Now.ToString("yyyy-MM-dd"));
                    ekle.Parameters.AddWithValue("@a43", cmbMdrY.Text);
                    ekle.Transaction = myTransaction;
                    ekle.ExecuteNonQuery();
                    ekle.CommandText = "Select MAX(ID) from Ogrenci";
                    object ogrId = ekle.ExecuteScalar();
                    int oId = Convert.ToInt32(ogrId);


                    SqlCommand ekleAnne = new SqlCommand("insert into Veli (OgrId,VeliTip, VeliMi, Sag, AdSoyad, TCKN, DogumTarihi, Meslek, CepTel, EvTel, IsTel, EvAdres, IsAdres, Eposta, Yakinlik, OlusturmaTarihi ) " +
                        "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16)", baglan);


                    ekleAnne.Parameters.AddWithValue("@a1", oId);
                    ekleAnne.Parameters.AddWithValue("@a2", 1);
                    if (txtAnneTc.Text == txtVeliTc.Text)
                        ekleAnne.Parameters.AddWithValue("@a3", 1);
                    else
                        ekleAnne.Parameters.AddWithValue("@a3", 0);
                    if (chkAnne.Checked)
                        ekleAnne.Parameters.AddWithValue("@a4", 1);
                    else
                        ekleAnne.Parameters.AddWithValue("@a4", 0);
                    ekleAnne.Parameters.AddWithValue("@a5", txtAnneAd.Text);
                    ekleAnne.Parameters.AddWithValue("@a6", txtAnneTc.Text);
                    ekleAnne.Parameters.AddWithValue("@a7", dtAnneDT.Value.ToString("yyyy-MM-dd"));
                    ekleAnne.Parameters.AddWithValue("@a8", txtAnneMeslek.Text);
                    ekleAnne.Parameters.AddWithValue("@a9", mskAnne.Text.Replace(" ", ""));
                    ekleAnne.Parameters.AddWithValue("@a10", txtAnneEvTel.Text);
                    ekleAnne.Parameters.AddWithValue("@a11", txtAnneIsTel.Text);
                    ekleAnne.Parameters.AddWithValue("@a12", txtAnneEvAdres.Text);
                    ekleAnne.Parameters.AddWithValue("@a13", txtAnneIsAdres.Text);
                    ekleAnne.Parameters.AddWithValue("@a14", txtAnneEposta.Text);
                    ekleAnne.Parameters.AddWithValue("@a15", "Anne");
                    ekleAnne.Parameters.AddWithValue("@a16", DateTime.Now.ToString("yyyy-MM-dd"));
                    ekleAnne.Transaction = myTransaction;
                    ekleAnne.ExecuteNonQuery();
                    ekleAnne.CommandText = "Select ID from veli where VeliTip=1 AND OgrId=" + oId;
                    object anneId = ekleAnne.ExecuteScalar();
                    int aId = Convert.ToInt32(anneId);


                    SqlCommand ekleBaba = new SqlCommand("insert into Veli (OgrId,VeliTip, VeliMi, Sag, AdSoyad, TCKN, DogumTarihi, Meslek, CepTel, EvTel, IsTel, EvAdres, IsAdres, Eposta, Yakinlik, OlusturmaTarihi ) " +
                       "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16)", baglan);


                    ekleBaba.Parameters.AddWithValue("@a1", oId);
                    ekleBaba.Parameters.AddWithValue("@a2", 2);
                    if (txtBabaTc.Text == txtVeliTc.Text)
                        ekleBaba.Parameters.AddWithValue("@a3", 1);
                    else
                        ekleBaba.Parameters.AddWithValue("@a3", 0);
                    if (chkBaba.Checked)
                        ekleBaba.Parameters.AddWithValue("@a4", 1);
                    else
                        ekleBaba.Parameters.AddWithValue("@a4", 0);
                    ekleBaba.Parameters.AddWithValue("@a5", txtBabaAd.Text);
                    ekleBaba.Parameters.AddWithValue("@a6", txtBabaTc.Text);
                    ekleBaba.Parameters.AddWithValue("@a7", dtBabaDT.Value.ToString("yyyy-MM-dd"));
                    ekleBaba.Parameters.AddWithValue("@a8", txtBabaMeslek.Text);
                    ekleBaba.Parameters.AddWithValue("@a9", mskBaba.Text.Replace(" ", ""));
                    ekleBaba.Parameters.AddWithValue("@a10", txtBabaEvTel.Text);
                    ekleBaba.Parameters.AddWithValue("@a11", txtBabaIsTel.Text);
                    ekleBaba.Parameters.AddWithValue("@a12", txtBabaEvAdres.Text);
                    ekleBaba.Parameters.AddWithValue("@a13", txtBabaIsAdres.Text);
                    ekleBaba.Parameters.AddWithValue("@a14", txtBabaEposta.Text);
                    ekleBaba.Parameters.AddWithValue("@a15", "Baba");
                    ekleBaba.Parameters.AddWithValue("@a16", DateTime.Now.ToString("yyyy-MM-dd"));
                    ekleBaba.Transaction = myTransaction;
                    ekleBaba.ExecuteNonQuery();
                    ekleBaba.CommandText = "Select ID from veli where VeliTip=2 AND OgrId=" + oId;
                    object babaId = ekleBaba.ExecuteScalar();
                    int bId = Convert.ToInt32(babaId);
                    if (txtBabaTc.Text == txtVeliTc.Text)
                        vId = bId;
                    if (txtAnneTc.Text == txtVeliTc.Text)
                        vId = aId;
                    if (txtBabaTc.Text != txtVeliTc.Text && txtAnneTc.Text != txtVeliTc.Text)
                    {
                        SqlCommand ekleVeli = new SqlCommand("insert into Veli (OgrId,VeliTip, VeliMi, Sag, AdSoyad, TCKN, DogumTarihi, Meslek, CepTel, EvTel, IsTel, EvAdres, IsAdres, Eposta, Yakinlik, OlusturmaTarih ) " +
                      "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16)", baglan);


                        ekleVeli.Parameters.AddWithValue("@a1", oId);
                        ekleVeli.Parameters.AddWithValue("@a2", 0);
                        ekleVeli.Parameters.AddWithValue("@a3", 1);
                        ekleVeli.Parameters.AddWithValue("@a4", 1);
                        ekleVeli.Parameters.AddWithValue("@a5", txtVeliAd.Text);
                        ekleVeli.Parameters.AddWithValue("@a6", txtVeliTc.Text);
                        ekleVeli.Parameters.AddWithValue("@a7", dtVeliDT.Value.ToString("yyyy-MM-dd"));
                        ekleVeli.Parameters.AddWithValue("@a8", txtVeliMeslek.Text);
                        ekleVeli.Parameters.AddWithValue("@a9", mskVeliCep.Text.Replace(" ", ""));
                        ekleVeli.Parameters.AddWithValue("@a10", txtVeliEvTel.Text);
                        ekleVeli.Parameters.AddWithValue("@a11", txtVeliIsTel.Text);
                        ekleVeli.Parameters.AddWithValue("@a12", txtVeliEvAdres.Text);
                        ekleVeli.Parameters.AddWithValue("@a13", txtVeliIsAdres.Text);
                        ekleVeli.Parameters.AddWithValue("@a14", txtVeliEposta.Text);
                        ekleVeli.Parameters.AddWithValue("@a15", txtVeliYakinlik.Text);
                        ekleVeli.Parameters.AddWithValue("@a16", DateTime.Now.ToString("yyyy-MM-dd"));
                        ekleVeli.Transaction = myTransaction;
                        ekleVeli.ExecuteNonQuery();
                        ekleVeli.CommandText = "Select ID from veli where VeliMi=1 AND OgrId=" + oId;
                        object veliId = ekleVeli.ExecuteScalar();
                        vId = Convert.ToInt32(veliId);
                    }


                    string guncel = "Update Ogrenci set VeliId=" + vId + ", AnneId=" + aId + ", BabaId=" + bId + "  Where ID =" + oId;
                    SqlCommand guncelle = new SqlCommand(guncel, baglan);
                    guncelle.Transaction = myTransaction;
                    guncelle.ExecuteNonQuery();

                    double miktar, destek = 0, ödenen = 0, ts = 1, taksit = 0;
                    miktar = Convert.ToDouble(textBox20.Text) + (Convert.ToDouble(textBox27.Text) * 0.1);

                    if (radioButton4.Checked)
                        ödenen = Convert.ToDouble(textBox4.Text) + (Convert.ToDouble(textBox6.Text) * 0.1);

                    if (radioButton1.Checked)
                        destek = Convert.ToDouble(textBox1.Text) + (Convert.ToDouble(textBox3.Text) * 0.1);

                    SqlCommand ekleOdeme = new SqlCommand("insert into OgrenciOdeme (OgrId, ÖdemeŞekli, MİKTAR, tutar, Açıklama, ŞUBE, Sahip, TaksitSayisi, TaksitGunu, Banka, Vade, ODENEN," +
                        " alınan, taksit, TARİH, NO, EgitimDestegi, destek, DestekVarYok, OnOdemeVarYok, BankaHesabı,OnÖdeme,EğitimDestekOdenen,EğitimDestektutar,TOPLAM,KALAN,kalantutar,toplamtutar,SonÖdemeTarihi  ) " +
                     "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11, @a12, @a13, @a14, @a15, @a16, @a17, @a18, @a19, @a20, @a21, @a22, @a23,@a24,@a25,@a26,@a27,@a28,@a29)", baglan);

                    ekleOdeme.Parameters.AddWithValue("@a1", oId);
                    ekleOdeme.Parameters.AddWithValue("@a2", comboBox4.Text);
                    ekleOdeme.Parameters.AddWithValue("@a4", textBox20.Text + "." + textBox27.Text);
                    ekleOdeme.Parameters.AddWithValue("@a3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    ekleOdeme.Parameters.AddWithValue("@a5", richTextBox6.Text);
                    ekleOdeme.Parameters.AddWithValue("@a6", cmbsube.SelectedValue);

                    if (comboBox4.Text == "ÇEK")
                    {
                        ekleOdeme.Parameters.AddWithValue("@a7", textBox5.Text);
                        ekleOdeme.Parameters.AddWithValue("@a8", 1);
                        ekleOdeme.Parameters.AddWithValue("@a9", 0);
                        ekleOdeme.Parameters.AddWithValue("@a11", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                        ekleOdeme.Parameters.AddWithValue("@a14", 0);
                        ekleOdeme.Parameters.AddWithValue("@a21", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a29", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));

                    }
                    else if (comboBox4.Text == "SENET")
                    {
                        ekleOdeme.Parameters.AddWithValue("@a7", textBox19.Text);
                        ekleOdeme.Parameters.AddWithValue("@a8", 1);
                        ekleOdeme.Parameters.AddWithValue("@a9", 0);
                        ekleOdeme.Parameters.AddWithValue("@a11", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                        ekleOdeme.Parameters.AddWithValue("@a14", 0);
                        ekleOdeme.Parameters.AddWithValue("@a21", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a29", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));

                    }
                    else if (comboBox4.Text == "BANKA")
                    {
                        ekleOdeme.Parameters.AddWithValue("@a7", textBox16.Text);
                        ekleOdeme.Parameters.AddWithValue("@a8", 0);
                        ekleOdeme.Parameters.AddWithValue("@a9", 0);
                        ekleOdeme.Parameters.AddWithValue("@a21", comboBox7.Text);
                        ekleOdeme.Parameters.AddWithValue("@a11", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a14", 0);
                        ekleOdeme.Parameters.AddWithValue("@a29", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));


                    }
                    else if (comboBox4.Text == "VİSA")
                    {
                        ekleOdeme.Parameters.AddWithValue("@a7", textBox23.Text);
                        ekleOdeme.Parameters.AddWithValue("@a8", numericUpDown1.Text);
                        ekleOdeme.Parameters.AddWithValue("@a9", numericUpDown21.Text);
                        ekleOdeme.Parameters.AddWithValue("@a21", comboBox6.Text);
                        ekleOdeme.Parameters.AddWithValue("@a11", DBNull.Value);
                        ts = Convert.ToDouble(numericUpDown1.Value);
                        taksit = (miktar - destek - ödenen) / ts;
                        decimal t = Convert.ToDecimal(taksit);
                        ekleOdeme.Parameters.AddWithValue("@a14", t);
                        ekleOdeme.Parameters.AddWithValue("@a29", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));


                    }
                    else
                    {
                        ekleOdeme.Parameters.AddWithValue("@a7", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a8", 1);
                        ekleOdeme.Parameters.AddWithValue("@a9", 0);
                        ekleOdeme.Parameters.AddWithValue("@a11", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a14", 0);
                        ekleOdeme.Parameters.AddWithValue("@a21", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a29", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));

                    }
                    ekleOdeme.Parameters.AddWithValue("@a15", DateTime.Now.ToString("yyyy-MM-dd"));
                    if (comboBox4.Text == "ÇEK")
                    {
                        ekleOdeme.Parameters.AddWithValue("@a16", textBox2.Text.TrimEnd());
                        ekleOdeme.Parameters.AddWithValue("@a10", textBox14.Text.TrimEnd());
                    }
                    else
                    {
                        ekleOdeme.Parameters.AddWithValue("@a16", DBNull.Value);
                        ekleOdeme.Parameters.AddWithValue("@a10", DBNull.Value);
                    }
                    if (radioButton1.Checked)
                    {
                        ekleOdeme.Parameters.AddWithValue("@a18", textBox1.Text + "." + textBox3.Text);
                        ekleOdeme.Parameters.AddWithValue("@a17", string.Format("{0:N}", Convert.ToDecimal(textBox1.Text + "," + textBox3.Text)));
                        ekleOdeme.Parameters.AddWithValue("@a19", 1);
                        ekleOdeme.Parameters.AddWithValue("@a23", "0,00 TL");
                        ekleOdeme.Parameters.AddWithValue("@a24", "0.00");


                    }
                    else
                    {
                        ekleOdeme.Parameters.AddWithValue("@a17", "0");
                        ekleOdeme.Parameters.AddWithValue("@a18", 0);
                        ekleOdeme.Parameters.AddWithValue("@a19", 0);
                        ekleOdeme.Parameters.AddWithValue("@a23", "0,00 TL");
                        ekleOdeme.Parameters.AddWithValue("@a24", "0.00");
                    }
                    decimal toplam = Convert.ToDecimal(textBox4.Text + "," + textBox6.Text) + Convert.ToDecimal(textBox20.Text + "," + textBox27.Text) + Convert.ToDecimal(textBox1.Text + "," + textBox3.Text);
                    decimal kalan = toplam - Convert.ToDecimal(textBox4.Text + "," + textBox6.Text);
                    if (radioButton4.Checked)
                    {
                        ekleOdeme.Parameters.AddWithValue("@a13", 0);
                        ekleOdeme.Parameters.AddWithValue("@a12", "0,00 TL");
                        ekleOdeme.Parameters.AddWithValue("@a20", 1);
                        ekleOdeme.Parameters.AddWithValue("@a22", string.Format("{0:N}", Convert.ToDecimal(textBox4.Text + "," + textBox6.Text)));
                        ekleOdeme.Parameters.AddWithValue("@a27", kalan);
                        ekleOdeme.Parameters.AddWithValue("@a28", toplam);



                    }
                    else
                    {
                        ekleOdeme.Parameters.AddWithValue("@a13", 0);
                        ekleOdeme.Parameters.AddWithValue("@a12", "0,00TL");
                        ekleOdeme.Parameters.AddWithValue("@a20", 0);
                        ekleOdeme.Parameters.AddWithValue("@a22", "0,00");
                        ekleOdeme.Parameters.AddWithValue("@a27", kalan);
                        ekleOdeme.Parameters.AddWithValue("@a28", toplam);



                    }

                    ekleOdeme.Parameters.AddWithValue("@a25", string.Format("{0:N}", toplam));
                    ekleOdeme.Parameters.AddWithValue("@a26", string.Format("{0:N}", kalan));

                    ekleOdeme.Transaction = myTransaction;
                    ekleOdeme.ExecuteNonQuery();
                    if (radioButton6.Checked)
                    {
                        SqlCommand komut = new SqlCommand("insert into İndirimliler (Öğrenci,Şube,İndirimTürü,İndirimMiktarı,İndirimNedeni) values (@p1, @p2, @p3, @p4,@p5)", baglan);
                        komut.Parameters.AddWithValue("@p1", oId);
                        komut.Parameters.AddWithValue("@p2", cmbsube.SelectedValue);
                        komut.Parameters.AddWithValue("@p3", comboBox1.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox7.Text + "," + textBox8.Text)));
                        komut.Parameters.AddWithValue("@p5", textBox9.Text);

                        komut.Transaction = myTransaction;
                        komut.ExecuteNonQuery();



                    }
                    if (radioButton4.Checked)
                    {
                        SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut.Parameters.AddWithValue("@p1", oId);
                        komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut.Parameters.AddWithValue("@p3", textBox4.Text + "." + textBox6.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox4.Text + "," + textBox6.Text)));
                        komut.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox4.Text + "," + textBox6.Text)));
                        komut.Parameters.AddWithValue("@p6", "ÖN ÖDEME");
                        komut.Parameters.AddWithValue("@p7", "0,00 TL");
                        komut.Parameters.AddWithValue("@p8", 0);

                        komut.Transaction = myTransaction;
                        komut.ExecuteNonQuery();
                    }
                    if (comboBox4.Text == "NAKİT")
                    {
                        SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                        kaydet3.Parameters.AddWithValue("@p1", oId);
                        kaydet3.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                        kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        kaydet3.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox27.Text);
                        kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                        kaydet3.Transaction = myTransaction;
                        kaydet3.ExecuteNonQuery();
                    }
                    if (comboBox4.Text == "BANKA")
                    {
                        SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                        kaydet3.Parameters.AddWithValue("@p1", oId);
                        kaydet3.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                        kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        kaydet3.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox27.Text);
                        kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                        kaydet3.Transaction = myTransaction;
                        kaydet3.ExecuteNonQuery();
                    }
                    if (comboBox4.Text == "VİSA")
                    {
                        for (int i = 0; i < numericUpDown1.Value; i++)
                        {
                            int month = DateTime.Now.Month;
                            month = month + i + 1;
                            if (month <= 12)
                            {
                                decimal t = Convert.ToDecimal(taksit);
                                SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                                kaydet3.Parameters.AddWithValue("@p1", oId);
                                kaydet3.Parameters.AddWithValue("@p2", DateTime.Now.Year + "-" + month + "-" + numericUpDown21.Value);
                                kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", t));
                                kaydet3.Parameters.AddWithValue("@p4", t);
                                kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                                kaydet3.Transaction = myTransaction;
                                kaydet3.ExecuteNonQuery();
                                tarih = DateTime.Now.Year + "-" + month + "-" + numericUpDown21.Value;


                            }
                            if (month > 12)
                            {

                                decimal t = Convert.ToDecimal(taksit);
                                SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                                kaydet3.Parameters.AddWithValue("@p1", oId);
                                kaydet3.Parameters.AddWithValue("@p2", (DateTime.Now.Year + 1) + "-" + (month - 12) + "-" + numericUpDown21.Value);
                                kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", t));
                                kaydet3.Parameters.AddWithValue("@p4", t);
                                kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                                kaydet3.Transaction = myTransaction;
                                kaydet3.ExecuteNonQuery();
                                tarih = (DateTime.Now.Year + 1) + "-" + (month - 12) + "-" + numericUpDown21.Value;
                            }
                        }

                    }
                    if (comboBox4.Text == "ÇEK")
                    {
                        SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,BANKA,ÇekNo,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10,@p11,@p12)", baglan);
                        komutkaydet2.Parameters.AddWithValue("@p2", "ÇEK");
                        komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                        komutkaydet2.Parameters.AddWithValue("@p11", oId);
                        komutkaydet2.Parameters.AddWithValue("@p1", cmbsube.SelectedValue);
                        komutkaydet2.Parameters.AddWithValue("@p3", textBox5.Text);
                        komutkaydet2.Parameters.AddWithValue("@p4", txtAd.Text + " " + txtSoyad.Text);
                        komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        komutkaydet2.Parameters.AddWithValue("@p10", textBox20.Text + "." + textBox27.Text);
                        komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                        komutkaydet2.Parameters.AddWithValue("@p9", textBox14.Text);
                        komutkaydet2.Parameters.AddWithValue("@p8", textBox2.Text);
                        komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                        komutkaydet2.Transaction = myTransaction;
                        komutkaydet2.ExecuteNonQuery();
                        SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                        kaydet3.Parameters.AddWithValue("@p1", oId);
                        kaydet3.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                        kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        kaydet3.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox27.Text);
                        kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                        kaydet3.Transaction = myTransaction;
                        kaydet3.ExecuteNonQuery();

                    }
                    if (comboBox4.Text == "SENET")
                    {
                        SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,BANKA,ÇekNo,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10,@p11,@p12)", baglan);
                        komutkaydet2.Parameters.AddWithValue("@p2", "SENET");
                        komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                        komutkaydet2.Parameters.AddWithValue("@p11", oId);
                        komutkaydet2.Parameters.AddWithValue("@p1", cmbsube.SelectedValue);
                        komutkaydet2.Parameters.AddWithValue("@p3", textBox5.Text);
                        komutkaydet2.Parameters.AddWithValue("@p4", txtAd.Text + " " + txtSoyad.Text);
                        komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        komutkaydet2.Parameters.AddWithValue("@p10", textBox20.Text + "." + textBox27.Text);
                        komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                        komutkaydet2.Parameters.AddWithValue("@p9", textBox14.Text);
                        komutkaydet2.Parameters.AddWithValue("@p8", "");
                        komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                        komutkaydet2.Transaction = myTransaction;
                        komutkaydet2.ExecuteNonQuery();
                        SqlCommand kaydet3 = new SqlCommand("insert into ÖdemePlanı (Öğrenci,SonÖdemeGünü,Miktar,tutar,ÖdendiDurum) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                        kaydet3.Parameters.AddWithValue("@p1", oId);
                        kaydet3.Parameters.AddWithValue("@p2", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                        kaydet3.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        kaydet3.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox27.Text);
                        kaydet3.Parameters.AddWithValue("@p5", "ÖDENMEDİ");

                        kaydet3.Transaction = myTransaction;
                        kaydet3.ExecuteNonQuery();
                        if (comboBox4.Text == "VİSA")
                        {
                            baglan.Open();
                            SqlCommand kaydet4 = new SqlCommand("update  OgrenciOdeme set SonÖdemeTarihi=@p1 where OgrId=" + oId, baglan);
                            kaydet4.Parameters.AddWithValue("@p1", tarih);
                            kaydet4.Transaction = myTransaction;

                            kaydet4.ExecuteNonQuery();
                            baglan.Close();



                        }
                    }

                    myTransaction.Commit();
                    MessageBox.Show("Kayıt Eklendi.");



                }
                catch (Exception a)
                {
                    myTransaction.Rollback();

                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
                }
                finally
                {
                    if (baglan.State == ConnectionState.Open)
                        baglan.Close();
                }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label181.Visible = true;
            label182.Visible = true;
            textBox1.Visible = true;
            textBox3.Visible = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label181.Visible = false;
            label182.Visible = false;
            textBox1.Visible = false;
            textBox3.Visible = false;
        }

        private void cmbsube_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbsube.SelectedIndex != -1)
            {
                da = new SqlDataAdapter("Select*from Personeller Where Pozisyon ='Öğretmen' And Sube=" + cmbsube.SelectedValue, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                cmbRO.ValueMember = "ID";
                cmbRO.DisplayMember = "Adi";
                cmbRO.DataSource = dt;
                cmbSnfOgr.ValueMember = "ID";
                cmbSnfOgr.DisplayMember = "Adi";
                cmbSnfOgr.DataSource = dt;
                cmbDns.ValueMember = "ID";
                cmbDns.DisplayMember = "Adi";
                cmbDns.DataSource = dt;

                SqlDataAdapter da2 = new SqlDataAdapter("Select*from Personeller Where Pozisyon ='Müdür Yardımcısı' And Sube=" + cmbsube.SelectedValue, baglan);
                DataTable dt2 = new DataTable();
                SqlCommandBuilder cmdb2 = new SqlCommandBuilder(da2);
                da2.Fill(dt2);
                cmbMdrY.ValueMember = "ID";
                cmbMdrY.DisplayMember = "Adi";
                cmbMdrY.DataSource = dt2;
            }
        }

        private void İÇ_KAYIT_Load(object sender, EventArgs e)
        {
            doldurSube();
            comboBox1.Items.Add("ÖN KAYITA ÖZEL İNDİRİMLİ");
            comboBox1.Items.Add("ÖZEL İNDİRİMLİ");

            comboBox4.Items.Add("NAKİT");
            comboBox4.Items.Add("VİSA");
            comboBox4.Items.Add("ÇEK");
            comboBox4.Items.Add("BANKA");
            comboBox4.Items.Add("SENET");

            cmbKanG.Items.Add("A Rh(+)");
            cmbKanG.Items.Add("A Rh(-)");
            cmbKanG.Items.Add("B Rh(+)");
            cmbKanG.Items.Add("B Rh(-)");
            cmbKanG.Items.Add("AB Rh(+)");
            cmbKanG.Items.Add("AB Rh(-)");
            cmbKanG.Items.Add("0 Rh(+)");
            cmbKanG.Items.Add("0 Rh(-)");

            cmbCins.Items.Add("ERKEK");
            cmbCins.Items.Add("KIZ");

            cmbSO.Items.Add("SABAH");
            cmbSO.Items.Add("ÖĞLE");

            cmbKur.Items.Add("SAY");
            cmbKur.Items.Add("SOZ");
            cmbKur.Items.Add("EA");
            cmbKur.Items.Add("YDİL");
            cmbKur.Items.Add("MES");

            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbDevre.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbSnf.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbProg.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='MÜDÜR YARDIMCISI'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                cmbMdrY.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='ÖĞRETMEN'", baglan);
            baglan.Open();
            SqlDataReader oku6 = komut.ExecuteReader();
            while (oku6.Read())
            {
                cmbSnfOgr.Items.Add(oku6[0].ToString());
                cmbRO.Items.Add(oku6[0].ToString());
            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='EĞİTİM DANIŞMANI'", baglan);
            baglan.Open();
            SqlDataReader oku7 = komut.ExecuteReader();
            while (oku7.Read())
            {
                cmbDns.Items.Add(oku7[0].ToString());
            }
            baglan.Close();
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
        string tarih = "";

        private void comboBoxMah_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox10.Text))
            {
                doldur();
                doldur1();
                doldur2();
                doldur3();

                panel12.Visible = false;
            }
        }
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label183.Visible = true;
            label192.Visible = true;
            textBox4.Visible = true;
            textBox6.Visible = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            label183.Visible = false;
            label192.Visible = false;
            textBox4.Visible = false;
            textBox6.Visible = false;
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked == true)
            {
                panel8.Visible = true;
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                panel8.Visible = false;
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox4.Text == "NAKİT")
            {
                panel5.Visible = true;
                panel9.Visible = false;
            }
            if (comboBox4.Text == "VİSA")
            {
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = false;

            }
            if (comboBox4.Text == "ÇEK")
            {
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = true;
                panel10.Visible = false;
            }
            if (comboBox4.Text == "BANKA")
            {
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;

            }
            if (comboBox4.Text == "SENET")
            {
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
