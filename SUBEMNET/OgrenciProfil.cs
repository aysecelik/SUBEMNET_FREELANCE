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
    public partial class OgrenciProfil : Form
    {
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        byte[] bytes;
        string path;
        public string OgrID;
        public OgrenciProfil()
        {
            InitializeComponent();
        }

        void doldur()
        {           
            baglan.Open();
            komut = new SqlCommand("Select ö.ID,ö.SozNo,ö.TCKN,ö.Cinsiyet,ö.Adi,ö.Soyadi,ö.DogumTarihi,ö.Program,ö.Devre,ö.Kur,ö.Okul,ö.Eposta,ö.EvTel,ö.OgrCepTel,ö.Sehir,ö.Ilce,ö.Mahalle,ö.Adres,ö.MaliDurum,ö.OzelBilgi1," +
                "ö.OzelBilgi2,ö.OzelBilgi3, ö.Aciklama, ö.Durum, ö.KayitTarihi, ö.KayitSilinmeTarihi, ö.OlusturmaTarihi, ş.ŞubeAdi, ö.Snf, ö.Kaydeden, ö.ServisId, ö.SabahOgleId, ö.RehberId, ö.SinifOgrId, ö.DanismanId, ö.KayitSilmeNedeni," +
                "ö.KanGrubu, ö.DogumYeri, ö.Hastalik, ö.AnneBabaAyri, ö.Foto,ö.OkulNo from Ogrenci ö join Sube ş on ş.ID=ö.Sube where ö.ID = '" + OgrID + "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
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
                txtSehir.Text = oku.GetValue(14).ToString();
                txtIlce.Text = oku.GetValue(15).ToString();
                txtMah.Text = oku.GetValue(16).ToString();
                txtAdres.Text = oku.GetValue(17).ToString();
                txtMaliD.Text = oku.GetValue(18).ToString();
                txtOb1.Text = oku.GetValue(19).ToString();
                txtOb2.Text = oku.GetValue(20).ToString();
                txtOb3.Text = oku.GetValue(21).ToString();
                txtAciklama.Text = oku.GetValue(22).ToString();
                txtOkulNo.Text = oku.GetValue(41).ToString();
                dtKayTar.Text = oku.GetValue(24).ToString();
                dtKST.Text = oku.GetValue(25).ToString();
                txtSube.Text = oku.GetValue(27).ToString();
                cmbSnf.Text = oku.GetValue(28).ToString();
                cmbServis.Text = oku.GetValue(30).ToString();
                cmbSO.Text = oku.GetValue(31).ToString();
                cmbRO.Text = oku.GetValue(32).ToString();
                cmbSnfOgr.Text = oku.GetValue(33).ToString();
                cmbDns.Text = oku.GetValue(34).ToString();
                cmbKSN.Text = oku.GetValue(35).ToString();
                cmbKanG.Text = oku.GetValue(36).ToString();
                txtDogumYeri.Text = oku.GetValue(37).ToString();
                txtHasta.Text = oku.GetValue(38).ToString();
                if (oku.GetValue(39).ToString() == "1")
                    chkVeli.Checked = true;
                if (oku[40] == DBNull.Value)
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
        private void OgrenciProfil_Load(object sender, EventArgs e)
        {
            doldur();
            doldur1();
            doldur2();
            doldur3();
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
        int okulid = Form1.okulid;
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
            try
            {
                //Güncelleme işlemi

                bool degisken = false;
                string filtre = "update Ogrenci set";
                if (string.IsNullOrEmpty(txtSN.Text) == false)
                {
                    filtre += " SozNo= " + "'" + txtSN.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtOkulNo.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " OkulNo =" + "'" + txtOkulNo.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(txtKgNo.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " KGNo= " + "'" + txtKgNo.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtSenetNo.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " SenetNo = " + "'" + txtSenetNo.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtMuhNo.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " MuhasebeNo =" + "'" + txtMuhNo.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtTC.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " TCKN= " + "'" + txtTC.Text + "'";
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
                if (string.IsNullOrEmpty(cmbCins.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Cinsiyet= " + "'" + cmbCins.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(mskOgrCep.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " OgrCepTel =" + "'" + mskOgrCep.Text + "'";
                    degisken = true;
                }
                //if (string.IsNullOrEmpty(mskceptel.Text.Replace(" ", "")) == false)
                //{
                //    if (degisken == true)
                //    {
                //        filtre += " , ";

                //    }
                //    filtre += " CepTel= " + "'" + mskceptel.Text + "'";
                //    degisken = true;
                //}
                //if (string.IsNullOrEmpty(mskceptel2.Text.Replace(" ", "")) == false)
                //{
                //    if (degisken == true)
                //    {
                //        filtre += " , ";

                //    }
                //    filtre += " CepTel2= " + "'" + mskceptel2.Text + "'";
                //    degisken = true;
                //}
               
                if (string.IsNullOrEmpty(cmbProg.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Program= " + "'" + cmbProg.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbDevre.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Devre= " + "'" + cmbDevre.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbKur.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Kur= '" + cmbKur.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbSnf.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Snf= " + "'" + cmbSnf.Text + "'";
                    degisken = true;
                }
              
                if (string.IsNullOrEmpty(cmbServis.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " ServisID =" + "'" + cmbServis.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbSO.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " SabahOgleId =" + "'" + cmbSO.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbRO.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " RehberId= " + "'" + cmbRO.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbSnfOgr.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " SinifOgrId =" + "'" + cmbSnfOgr.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbDns.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " DanismanId =" + "'" + cmbDns.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbKanG.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " KanGrubu =" + "'" + cmbKanG.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtDogumYeri.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " DogumYeri =" + "'" + txtDogumYeri.Text+ "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtHasta.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Hastalik = " + "'" + txtHasta.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtOkul.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " Okul =" + "'" + txtOkul.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtEposta.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Eposta =" + "'" + txtEposta.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtEvTel.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " EvTel =" + "'" + txtEvTel.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtSehir.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Sehir =" + "'" + txtSehir.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtIlce.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Ilce =" + "'" + txtIlce.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtMah.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Mahalle =" + "'" + txtMah.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtAdres.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Adres =" + "'" + txtAdres.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtOb1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " OzelBilgi1 =" + "'" + txtOb1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtOb2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " OzelBilgi2 =" + "'" + txtOb2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtOb3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " OzelBilgi3 =" + "'" + txtOb3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtAciklama.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " Aciklama =" + "'" + txtAciklama.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbKSN.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " KayitSilmeNedeni =" + "'" + cmbKSN.Text + "'";
                    degisken = true;
                }
                if (chkVeli.Checked == false)
                {
                    filtre += " ,AnneBabaAyri =0";
                    degisken = true;
                }
                if (chkVeli.Checked == true)
                {
                    filtre += " ,AnneBabaAyri =1";
                    degisken = true;
                }
                if (pictureBox1.Image != null)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";

                    }
                    filtre += " foto=@a1 ";
                    degisken = true;
                }
                filtre += " ,SonDegistirme=GETDATE()";
                filtre += " ,DogumTarihi= " + "'" + dtDogumTar.Value.Date.ToString("yyyy-MM-dd") + "'";
                filtre += " ,KayitTarihi =" + "'" + dtKayTar.Value.Date.ToString("yyyy-MM-dd") + "'";

                filtre += "  where ID = " + OgrID;
                baglan.Open();
                SqlCommand degistir = new SqlCommand(filtre, baglan);
                if (pictureBox1.Image != null)
                {
                    degistir.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;
                }

                degistir.ExecuteNonQuery();
                baglan.Close();

                //////////////////////////////////////////////
                
                bool degisken2 = false;
                string filtre2 = "update Veli set";
                if (string.IsNullOrEmpty(txtAnneAd.Text) == false)
                {
                    filtre2 += " AdSoyad= " + "'" + txtAnneAd.Text.ToUpper() + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneTc.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " TCKN =" + "'" + txtAnneTc.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneMeslek.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " Meslek =" + "'" + txtAnneMeslek.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(mskAnne.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " CepTel =" + "'" + mskAnne.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneEvTel.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " EvTel =" + "'" + txtAnneEvTel.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneIsTel.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " IsTel =" + "'" + txtAnneIsTel.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneEvAdres.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " EvAdres =" + "'" + txtAnneEvAdres.Text + "'";
                    degisken2 = true;
                }
                if (string.IsNullOrEmpty(txtAnneIsAdres.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " IsAdres =" + "'" + txtAnneIsAdres.Text + "'";
                    degisken2 = true;
                }
                if (chkAnne.Checked == false)
                {
                    filtre += " ,Sag=0";
                    degisken = true;
                }
                if (chkAnne.Checked == true)
                {
                    filtre += " ,Sag =1";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtAnneEposta.Text) == false)
                {
                    if (degisken2 == true)
                    {
                        filtre2 += " , ";

                    }
                    filtre2 += " Eposta =" + "'" + txtAnneEposta.Text + "'";
                    degisken2 = true;
                }
                filtre2 += " ,SonDegistirme=GETDATE()";
                filtre2 += ", DogumTarihi =" + "'" + dtAnneDT.Value.Date.ToString("yyyy-MM-dd") + "'";
                filtre2 += " where OgrId = " + OgrID + "And VeliTip=1";
                baglan.Open();
                SqlCommand degistir2 = new SqlCommand(filtre2, baglan);
                degistir2.ExecuteNonQuery();
                baglan.Close();

                ///////////////////////////////////////////////

                bool degisken3 = false;
                string filtre3 = "update Veli set";
                if (string.IsNullOrEmpty(txtBabaAd.Text) == false)
                {
                    filtre3 += " AdSoyad= " + "'" + txtBabaAd.Text.ToUpper() + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaTc.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " TCKN =" + "'" + txtBabaTc.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaMeslek.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " Meslek =" + "'" + txtBabaMeslek.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(mskBaba.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " CepTel =" + "'" + mskBaba.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaEvTel.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " EvTel =" + "'" + txtBabaEvTel.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaIsTel.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " IsTel =" + "'" + txtBabaIsTel.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaEvAdres.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " EvAdres =" + "'" + txtBabaEvAdres.Text + "'";
                    degisken3 = true;
                }
                if (string.IsNullOrEmpty(txtBabaIsAdres.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " IsAdres =" + "'" + txtBabaIsAdres.Text + "'";
                    degisken3 = true;
                }
                if (chkBaba.Checked == false)
                {
                    filtre += " ,Sag=0";
                    degisken = true;
                }
                if (chkBaba.Checked == true)
                {
                    filtre += " ,Sag =1";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtBabaEposta.Text) == false)
                {
                    if (degisken3 == true)
                    {
                        filtre3 += " , ";

                    }
                    filtre3 += " Eposta =" + "'" + txtBabaEposta.Text + "'";
                    degisken3 = true;
                }
                filtre3 += " ,SonDegistirme=GETDATE()";
                filtre3 += " ,DogumTarihi =" + "'" + dtBabaDT.Value.Date.ToString("yyyy-MM-dd") + "'";
                filtre3 += "  where OgrId = " + OgrID + "And VeliTip=2";
                baglan.Open();
                SqlCommand degistir3 = new SqlCommand(filtre3, baglan);
                degistir3.ExecuteNonQuery();
                baglan.Close();

                ////////////////////////////////////////

                bool degisken1 = false;
                string filtre1 = "update Veli set";
                if (string.IsNullOrEmpty(txtVeliAd.Text) == false)
                {
                    filtre1 += " AdSoyad= " + "'" + txtVeliAd.Text.ToUpper() + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliTc.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " TCKN =" + "'" + txtVeliTc.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliMeslek.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " Meslek =" + "'" + txtVeliMeslek.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(mskVeliCep.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " CepTel =" + "'" + mskVeliCep.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliEvTel.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " EvTel =" + "'" + txtVeliEvTel.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliIsTel.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " IsTel =" + "'" + txtVeliIsTel.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliEvAdres.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " EvAdres =" + "'" + txtVeliEvAdres.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliIsAdres.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " IsAdres =" + "'" + txtVeliIsAdres.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliEposta.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " Eposta =" + "'" + txtVeliEposta.Text + "'";
                    degisken1 = true;
                }
                if (string.IsNullOrEmpty(txtVeliYakinlik.Text) == false)
                {
                    if (degisken1 == true)
                    {
                        filtre1 += " , ";

                    }
                    filtre1 += " Yakinlik =" + "'" + txtVeliYakinlik.Text + "'";
                    degisken1 = true;
                }
                filtre1 += " ,SonDegistirme=GETDATE()";
                filtre1 += " ,DogumTarihi =" + "'" + dtVeliDT.Value.Date.ToString("yyyy-MM-dd") + "'";
                filtre1 += "  where OgrId = " + OgrID + "And Velimi=1";
                baglan.Open();
                SqlCommand degistir1 = new SqlCommand(filtre1, baglan);
                degistir1.ExecuteNonQuery();
                baglan.Close();

                MessageBox.Show("Kayıt Güncellendi.");
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }        
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

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
