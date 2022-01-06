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
    public partial class KAYIT_SİLME_İŞLEMLERİ : Form
    {
        public KAYIT_SİLME_İŞLEMLERİ()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        public string OgrID;
        public string subeID;


        private void button8_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                baglan.Open();
                komut = new SqlCommand("Select ID,SozNo,TCKN,Cinsiyet,Adi,Soyadi,DogumTarihi,Program,Devre,Kur,Okul,Eposta,EvTel,OgrCepTel,Sehir,Ilce,Mahalle,Adres,MaliDurum,OzelBilgi1," +
                    "OzelBilgi2,OzelBilgi3, Aciklama, Durum, KayitTarihi, KayitSilinmeTarihi, OlusturmaTarihi, Sube, Snf, Kaydeden, ServisId, SabahOgleId, RehberId, SinifOgrId, DanismanId, KayitSilmeNedeni," +
                    "KanGrubu, DogumYeri, Hastalik, AnneBabaAyri from Ogrenci where TCKN = '" + textBox1.Text + "'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                { 
                    panel1.Visible = true;

                    OgrID = oku.GetValue(0).ToString();
                    textBox3.Text = oku.GetValue(4).ToString() + " " + oku.GetValue(5).ToString();
                    dateTimePicker1.Value = Convert.ToDateTime(oku.GetValue(24));
                    subeID = oku.GetValue(27).ToString();


                }
                baglan.Close();
                baglan.Open();
                komut = new SqlCommand("Select MİKTAR,ODENEN,EgitimDestegi,EğitimDestekOdenen,OnÖdeme from OgrenciOdeme where OgrId = '" + OgrID + "'", baglan);
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    textBox8.Text = oku.GetValue(0).ToString();
                    textBox10.Text = oku.GetValue(1).ToString();
                    textBox7.Text = oku.GetValue(2).ToString();
                    textBox12.Text = oku.GetValue(3).ToString();
                    textBox9.Text = oku.GetValue(4).ToString();



                }
                baglan.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(textBox21.Text) == true || string.IsNullOrEmpty(textBox22.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    komut = new SqlCommand("update Ogrenci set (KayitSilmeNedeni,KayitSilinmeTarihi,Durum) values (@a1,@a2,0) where ID='" + OgrID + "'", baglan);
                    komut.Parameters.AddWithValue("@p1", textBox4.Text);
                    komut.Parameters.AddWithValue("@p2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Kayıtiade (TARİH,Öğrenci,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE) values (@p1, @p2, @p4,@p5, @p6, @p7,@p8)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", OgrID);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox21.Text + "." + textBox22.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeID);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("KAYIT SİLME İŞLEMİ BAŞARILI, MUHASEBE KISMINDAN ÖDEME İŞLEMLERİNİ GERÇEKLEŞTİREBİLİRSİNİZ.");
                    panel6.Visible = false;
                    dateTimePicker1.Value = DateTime.Now;
                    textBox4.Text = "";
                    textBox9.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    textBox10.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    comboBox4.Text = "";
                    richTextBox4.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "00";


                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }

        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "NAKİT")
            {
                panel6.Visible = true;
                panel9.Visible = false;
            }
            if (comboBox4.Text == "VİSA")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = false;

            }
            if (comboBox4.Text == "ÇEK")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = false;
            }
            if (comboBox4.Text == "BANKA")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;

            }
            if (comboBox4.Text == "SENET")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //visa ekleme
            if (string.IsNullOrEmpty(textBox17.Text) == true || string.IsNullOrEmpty(textBox18.Text) == true  || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(textBox23.Text) == true || string.IsNullOrEmpty(comboBox6.Text) == true || string.IsNullOrEmpty(textBox25.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    komut = new SqlCommand("update Ogrenci set (KayitSilmeNedeni,KayitSilinmeTarihi,Durum) values (@a1,@a2,0) where ID='" + OgrID + "'", baglan);
                    komut.Parameters.AddWithValue("@p1", textBox4.Text);
                    komut.Parameters.AddWithValue("@p2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Kayıtiade (TARİH,Öğrenci ,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,TaksitSayisi,NO,BankaHesabı,taksit) values (@p1, @p2, @p4,@p5, @p6, @p7,@p8,@p9, @p10, @p11,@p12,@p13)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", OgrID);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox17.Text + "," + textBox18.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox17.Text + "." + textBox18.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeID);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox23.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", numericUpDown1.Text);
                    komutkaydet.Parameters.AddWithValue("@p11", textBox25.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p13", numericUpDown1.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("KAYIT SİLME İŞLEMİ BAŞARILI, MUHASEBE KISMINDAN ÖDEME İŞLEMLERİNİ GERÇEKLEŞTİREBİLİRSİNİZ.");
                    panel6.Visible = false;
                  
                    dateTimePicker1.Value = DateTime.Now;
                    textBox4.Text = "";
                    textBox9.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    textBox10.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    comboBox4.Text = "";
                    richTextBox5.Text = "";
                    textBox17.Text = "";
                    textBox18.Text = "00";
                    textBox23.Text = "";
                    numericUpDown1.Text = "1";
                    textBox25.Text = "";
                    comboBox6.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ÇEK
            if (string.IsNullOrEmpty(textBox6.Text) == true || string.IsNullOrEmpty(textBox11.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true  || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox14.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    komut = new SqlCommand("update Ogrenci set (KayitSilmeNedeni,KayitSilinmeTarihi,Durum) values (@a1,@a2,0) where ID='" + OgrID + "'", baglan);
                    komut.Parameters.AddWithValue("@p1", textBox4.Text);
                    komut.Parameters.AddWithValue("@p2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komut.ExecuteNonQuery();
                    baglan.Close();

                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Kayıtiade (TARİH,Öğrenci,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,Vade,NO,Banka) values (@p1, @p2, @p4,@p5, @p6, @p7,@p8,@p9, @p10, @p11,@p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", OgrID);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox6.Text + "." + textBox11.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeID);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p11", textBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", textBox14.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("KAYIT SİLME İŞLEMİ BAŞARILI, MUHASEBE KISMINDAN ÖDEME İŞLEMLERİNİ GERÇEKLEŞTİREBİLİRSİNİZ.");
                    panel6.Visible = false;
                   

                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,BANKA,ÇekNo,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p2", "ÇEK");
                    komutkaydet2.Parameters.AddWithValue("@p12", "BORÇ");
                    komutkaydet2.Parameters.AddWithValue("@p11", OgrID);
                    komutkaydet2.Parameters.AddWithValue("@p1", subeID);
                    komutkaydet2.Parameters.AddWithValue("@p3", textBox3.Text);
                    komutkaydet2.Parameters.AddWithValue("@p4", textBox3.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox6.Text + "." + textBox11.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                    komutkaydet2.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet2.Parameters.AddWithValue("@p8", textBox14.Text);
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    textBox4.Text = "";
                    textBox9.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    textBox10.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox4.Text = "";
                    richTextBox1.Text = "";
                    textBox6.Text = "";
                    textBox11.Text = "00";
                    textBox25.Text = "";
                    dateTimePicker2.Value = DateTime.Now;
                    textBox2.Text = "";
                    textBox14.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //BANKA
            if (string.IsNullOrEmpty(textBox24.Text) == true || string.IsNullOrEmpty(textBox26.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true  || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(comboBox7.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    komut = new SqlCommand("update Ogrenci set (KayitSilmeNedeni,KayitSilinmeTarihi,Durum) values (@a1,@a2,0) where ID='" + OgrID + "'", baglan);
                    komut.Parameters.AddWithValue("@p1", textBox4.Text);
                    komut.Parameters.AddWithValue("@p2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Kayıtiade (TARİH,Öğrenci ,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,BankaHesabı) values (@p1, @p2, @p4,@p5, @p6, @p7,@p8,@p9, @p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", OgrID);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox24.Text + "," + textBox26.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox24.Text + "." + textBox26.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeID);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox16.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox7.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("KAYIT SİLME İŞLEMİ BAŞARILI, MUHASEBE KISMINDAN ÖDEME İŞLEMLERİNİ GERÇEKLEŞTİREBİLİRSİNİZ.");
                    panel6.Visible = false;
                   
                    dateTimePicker1.Value = DateTime.Now;
                    textBox4.Text = "";
                    textBox9.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    textBox10.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    comboBox4.Text = "";
                    richTextBox3.Text = "";
                    textBox24.Text = "";
                    textBox26.Text = "00";
                    textBox25.Text = "";
                    textBox16.Text = "";

                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //SENET
            if (string.IsNullOrEmpty(textBox20.Text) == true || string.IsNullOrEmpty(textBox27.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox19.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    komut = new SqlCommand("update Ogrenci set (KayitSilmeNedeni,KayitSilinmeTarihi,Durum) values (@a1,@a2,0) where ID='" + OgrID + "'", baglan);
                    komut.Parameters.AddWithValue("@p1", textBox4.Text);
                    komut.Parameters.AddWithValue("@p2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komut.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Kayıtiade (TARİH,Öğrenci ,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,Vade) values (@p1, @p2,  @p4,@p5, @p6, @p7,@p8,@p9, @p10)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", OgrID);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox20.Text + "." + textBox27.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeID);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox19.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
               
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p1", subeID);
                    komutkaydet2.Parameters.AddWithValue("@p2", "SENET");
                    komutkaydet2.Parameters.AddWithValue("@p12", "BORÇ");
                    komutkaydet2.Parameters.AddWithValue("@p11", OgrID);
                    komutkaydet2.Parameters.AddWithValue("@p3", textBox19.Text);
                    komutkaydet2.Parameters.AddWithValue("@p4", textBox3.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox20.Text + "." + textBox27.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                    dateTimePicker1.Value = DateTime.Now;
                    textBox4.Text = "";
                    textBox9.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    textBox10.Text = "";
                    textBox3.Text = "";
                    textBox1.Text = "";
                    comboBox4.Text = "";
                    richTextBox1.Text = "";
                    textBox20.Text = "";
                    textBox27.Text = "00";
                    textBox25.Text = "";
                    dateTimePicker3.Value = DateTime.Now;
                    textBox2.Text = "";
                    textBox14.Text = "";
                    textBox19.Text = "";





                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }
    }
}


