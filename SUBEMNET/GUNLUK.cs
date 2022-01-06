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
    public partial class GUNLUK : Form
    {
        public GUNLUK()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        SqlCommand komut;
        List<int> subeid = new List<int>();
        DataTable dt;
        private void GUNLUK_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox1.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            comboBox1.Text=comboBox1.Items[0].ToString();
            baglan.Open();
            da = new SqlDataAdapter("Select ö.Sezon SEZON, ş.ŞubeAdi ŞUBE,t.Tür 'ÖDEME TÜRÜ',ö.SozNo 'SÖZ NO',ö.Adi+' ' +ö.Soyadi ÖĞRENCİ,ö.Devre 'DEVRE',t.Tarih TARİH,t.ÖDENEN,pm.ÖdemeŞekli 'ÖDEME ŞEKLİ'  from  ÖğrenciÖdemeDetay t join Ogrenci ö on ö.ID=t.Öğrenci join OgrenciOdeme pm on ö.ID=pm.OgrId join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ TAKSİTLERİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.ALINAN,t.VERİLEN,t.IslemTürü 'İŞLEM TÜRÜ',t.BankaHesabı 'BANKA HESAP ADI',t.Açıklama 'AÇIKLAMA'  from  Virman t  join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "VİRMAN");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,p.ID ,p.Adi + ' '+ p.Soyadi PERSONEL ,t.MAAŞ 'MAAŞ',t.MAAŞ ÖDENEN  from PersonelMaaş t join Personeller p on p.ID=t.Personel join Sube ş on p.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Ayyıl='" + DateTime.Now.Year + ' ' + DateTime.Now.ToString("MMMM") + "' and ÖdemeDurum=1", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MAAŞ ÖDEMELERİ");
            dataGridView3.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.GiderKalemi 'GİDER',t.Miktar 'VERECEK',t.ODENEN VERİLEN,t.No 'FATURA NO',t.Açıklama 'AÇIKLAMA'  from  Giderler t  join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GİDERLER");
            dataGridView4.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,p.Tedarikçi 'TEDARİKÇİ',t.GiderKalemi 'GİDER',t.Miktar 'VERECEK',t.ODENEN VERİLEN,t.ÖdemeTürü 'ÖDEME TÜRÜ',t.Açıklama 'AÇIKLAMA'  from  Borçlar t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Şube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView5.DataSource = ds.Tables[0];
            baglan.Close(); baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.Faaliyet,t.ÖdemeŞekli 'ÖDEME TÜRÜ',ö.SozNo 'SÖZ NO',ö.Adi+' ' +ö.Soyadi ÖĞRENCİ,ö.Devre 'DEVRE',t.Miktar 'ALACAK',t.ÖDENEN ALINAN from Faaliyet t join Ogrenci ö on ö.ID=t.Öğrenci join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "FAALİYETLER");
            dataGridView6.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.TARİH TARİH,m.AdSoyad MÜŞTERİ,t.DiğerGelirKalemi 'GELİR',t.MİKTAR 'ALACAK',t.ODENEN ALINAN,t.ÖdemeŞekli 'ÖDEME TÜRÜ',t.Açıklama 'AÇIKLAMA'  from  DiğerGelirler t  join Müşteri m on m.ID=t.Müşteri join Sube ş on t.ŞUBE=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GELİRLER");
            dataGridView7.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.Tür 'ÖDEME TÜRÜ',Sum(t.Ödeme)  from  ÖğrenciÖdemeDetay t join Ogrenci ö on ö.ID=t.Öğrenci join OgrenciOdeme pm on ö.ID=pm.OgrId join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and  t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'  group by t.Tür", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ TAKSİTLERİ");
            dataGridView8.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ÖdemeŞekli 'ÖDEME TÜRÜ',SUM(t.alınan) ALINAN from Faaliyet t join Ogrenci ö on ö.ID=t.Öğrenci join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "' group by t.ÖdemeŞekli", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "FAALİYETLER");
            dataGridView9.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.DiğerGelirKalemi 'GELİR',SUM(t.alınan)  from  DiğerGelirler t  join Müşteri m on m.ID=t.Müşteri join Sube ş on t.ŞUBE=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH='" + DateTime.Now.Date.ToString("yyyyMMdd") + "' group by  t.DiğerGelirKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GELİRLER");
            dataGridView13.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.GiderKalemi 'BORÇ',SUM(t.ödenen) ÖDENEN from  Borçlar t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Şube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH='" + DateTime.Now.Date.ToString("yyyyMMdd") + "' group by  t.GiderKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView10.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.GiderKalemi 'GİDER',SUM(t.ödenen) ÖDENEN from  Giderler t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH='" + DateTime.Now.Date.ToString("yyyyMMdd") + "' group by  t.GiderKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView11.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select sum(t.Tutar)  from PersonelMaaş t join Personeller p on p.ID=t.Personel join Sube ş on p.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "'and t.Ayyıl='" + DateTime.Now.Year + ' ' + DateTime.Now.ToString("MMMM") + "' and ÖdemeDurum=1", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MAAŞ ÖDEMELERİ");
            dataGridView12.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select sum(t.alacak) ALACAK,sum(t.verecek) VERECEK  from Virman t  join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "'and t.Tarih='" + DateTime.Now.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "VİRMAN");
            dataGridView14.DataSource = ds.Tables[0];
            baglan.Close();


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select ö.Sezon SEZON, ş.ŞubeAdi ŞUBE,t.Tür 'ÖDEME TÜRÜ',ö.SozNo 'SÖZ NO',ö.Adi+' ' +ö.Soyadi ÖĞRENCİ,ö.Devre 'DEVRE',t.Tarih TARİH,t.ÖDENEN,pm.ÖdemeŞekli 'ÖDEME ŞEKLİ'  from  ÖğrenciÖdemeDetay t join Ogrenci ö on ö.ID=t.Öğrenci join OgrenciOdeme pm on ö.ID=pm.OgrId join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '"+ dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ TAKSİTLERİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.ALINAN,t.VERİLEN,t.IslemTürü 'İŞLEM TÜRÜ',t.BankaHesabı 'BANKA HESAP ADI',t.Açıklama 'AÇIKLAMA'  from  Virman t  join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "VİRMAN");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,p.ID ,p.Adi + ' '+ p.Soyadi PERSONEL ,t.MAAŞ 'MAAŞ',t.MAAŞ ÖDENEN  from PersonelMaaş t join Personeller p on p.ID=t.Personel join Sube ş on p.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Ayyıl between '" + dateTimePicker1.Value.Year + ' ' + dateTimePicker1.Value.ToString("MMMM") + "' and '" + dateTimePicker2.Value.Year + ' ' + dateTimePicker2.Value.ToString("MMMM") + "'  and ÖdemeDurum=1", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MAAŞ ÖDEMELERİ");
            dataGridView3.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.GiderKalemi 'GİDER',t.Miktar 'VERECEK',t.ODENEN VERİLEN,t.No 'FATURA NO',t.Açıklama 'AÇIKLAMA'  from  Giderler t  join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GİDERLER");
            dataGridView4.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,p.Tedarikçi 'TEDARİKÇİ',t.GiderKalemi 'GİDER',t.Miktar 'VERECEK',t.ODENEN VERİLEN,t.ÖdemeTürü 'ÖDEME TÜRÜ',t.Açıklama 'AÇIKLAMA'  from  Borçlar t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Şube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView5.DataSource = ds.Tables[0];
            baglan.Close(); baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.Tarih TARİH,t.Faaliyet,t.ÖdemeŞekli 'ÖDEME TÜRÜ',ö.SozNo 'SÖZ NO',ö.Adi+' ' +ö.Soyadi ÖĞRENCİ,ö.Devre 'DEVRE',t.Miktar 'ALACAK',t.ÖDENEN ALINAN from Faaliyet t join Ogrenci ö on ö.ID=t.Öğrenci join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "FAALİYETLER");
            dataGridView6.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi ŞUBE,t.TARİH TARİH,m.AdSoyad MÜŞTERİ,t.DiğerGelirKalemi 'GELİR',t.MİKTAR 'ALACAK',t.ODENEN ALINAN,t.ÖdemeŞekli 'ÖDEME TÜRÜ',t.Açıklama 'AÇIKLAMA'  from  DiğerGelirler t  join Müşteri m on m.ID=t.Müşteri join Sube ş on t.ŞUBE=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GELİRLER");
            dataGridView7.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.Tür 'ÖDEME TÜRÜ',Sum(t.Ödeme) ALINAN from  ÖğrenciÖdemeDetay t join Ogrenci ö on ö.ID=t.Öğrenci join OgrenciOdeme pm on ö.ID=pm.OgrId join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and  t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'  group by t.Tür", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ TAKSİTLERİ");
            dataGridView8.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ÖdemeŞekli 'ÖDEME TÜRÜ',SUM(t.alınan) ALINAN from Faaliyet t join Ogrenci ö on ö.ID=t.Öğrenci join Sube ş on ö.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "' group by t.ÖdemeŞekli", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "FAALİYETLER");
            dataGridView9.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.DiğerGelirKalemi 'GELİR',SUM(t.alınan) ALINAN from  DiğerGelirler t  join Müşteri m on m.ID=t.Müşteri join Sube ş on t.ŞUBE=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "' group by  t.DiğerGelirKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GELİRLER");
            dataGridView13.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.GiderKalemi 'BORÇ',SUM(t.ödenen) ÖDENEN from  Borçlar t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Şube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "' group by  t.GiderKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView10.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select t.GiderKalemi 'GİDER',SUM(t.ödenen) ÖDENEN from  Giderler t join Tedarikçiler p on p.ID=t.Tedarikçi join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "' group by  t.GiderKalemi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇLAR");
            dataGridView11.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select sum(t.Tutar) ÖDENEN from PersonelMaaş t join Personeller p on p.ID=t.Personel join Sube ş on p.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "'and t.Ayyıl between '" + dateTimePicker1.Value.Year + ' ' + dateTimePicker1.Value.ToString("MMMM") + "' and '" +dateTimePicker2.Value.Year + ' ' + dateTimePicker2.Value.ToString("MMMM")+ "' and ÖdemeDurum=1", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "MAAŞ ÖDEMELERİ");
            dataGridView12.DataSource = ds.Tables[0];
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select SUM(t.alacak) ALACAK,SUM(t.verecek) VERECEK from Virman t join Sube ş on t.Sube=ş.ID where ş.Okulid='" + okulid + "' and ş.ŞubeAdi='" + comboBox1.Text + "' and t.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "VİRMAN");
            dataGridView14.DataSource = ds.Tables[0];
            baglan.Close();
        }
    }
}
