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
using DersDagitim;


namespace SUBEMNET
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        private void Menu_Load(object sender, EventArgs e)
        {
            if (DateTime.Now == Convert.ToDateTime("01.01."+DateTime.Now.Year))
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Sezon (Sezon, okulid) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", DateTime.Now.Year+"-"+(DateTime.Now.Year+1));
                    komutkaydet.Parameters.AddWithValue("@p2", okulid);
                 

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();

                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void gÜNLÜKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUNLUK fr = new GUNLUK();
            fr.Show();
        }

        private void vİRMANToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VİRMAN FR = new VİRMAN();
            FR.Show() ;
        }

        private void gECİKENÖDEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GECİKEN_ÖDEME FR = new GECİKEN_ÖDEME();
            FR.Show();
        }

        private void öĞRENCİÖDEMEDETAYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÖĞRENCİ_ÖDEME_TOPLU FR = new ÖĞRENCİ_ÖDEME_TOPLU();
            FR.Show();
        }

        private void iNDİRİMLERLİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İNDİRİMLİLER_LİSTE FR = new İNDİRİMLİLER_LİSTE();
            FR.Show();
        }

        private void mAAŞKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MAAŞ_KONTROL FR = new MAAŞ_KONTROL();
            FR.Show();
        }


        private void tEDARİKÇİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TEDARIKCILER fr = new TEDARIKCILER();
            fr.Show();
        }

        private void bORÇLARKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BORÇLAR fr = new BORÇLAR();
            fr.Show();
        }

        private void kREDİKARTIÖDEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void dİĞERGELİREKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DİĞER_GELİR_EKLE fr = new DİĞER_GELİR_EKLE();
            fr.Show();
        }

        private void mÜŞTERİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MÜŞTERİLER FR = new MÜŞTERİLER();
            FR.Show();
        }

        private void fATURAİŞLEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FATURA_ISLEME fr = new FATURA_ISLEME();
            fr.Show();
        }

        private void tOPLUFATURAİŞLEMLERİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fATURAİSTATİSTİKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gİDERBULToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GIDERLER fr = new GIDERLER();
            fr.Show();
        }

        private void tAKIMÇALIŞMASIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakımÇalışması fr = new TakımÇalışması();
            fr.Show();
        }

        private void tOPLANTILARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TOPLANTI fr = new TOPLANTI();
            fr.Show();
        }

        private void iNSANKAYNAKLARIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            İnsanKaynkaları fr = new İnsanKaynkaları();
            fr.Show();
        }

        private void iNSANKAYNAKLARILİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İnsanKaynaklarıListe fr = new İnsanKaynaklarıListe();
            fr.Show();
        }

        private void sMSVeBİLDİRİMLERToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pERSONELToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            PERSONEL fr = new PERSONEL();
            fr.Show();
        }

        private void pERSONELLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PERSONEL_LİSTESİ fr = new PERSONEL_LİSTESİ();
            fr.Show();
        }

        private void mESAİDÜZENLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MESAİ_DÜZENLE fr = new MESAİ_DÜZENLE();
            fr.Show();
        }

        private void iKGÖRÜŞMELERİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İK_GÖRÜŞMELERİ fr = new İK_GÖRÜŞMELERİ();
            fr.Show();
        }

        private void iZİNLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İZİN_LİSTES FR = new İZİN_LİSTES();
            FR.Show();
        }

        private void pERSONELATAMALİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelAtamaListesi fr = new PersonelAtamaListesi();
            fr.Show();
        }

        private void pERSONELDEVAMTAKİPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelDevamTakip fr = new PersonelDevamTakip();
            fr.Show();
        }

        private void mESAİKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MesaiKontrol fr = new MesaiKontrol();
            fr.Show();
        }

        private void pERSONELÖZELSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonelÖzelSMS fr = new PersonelÖzelSMS();
            fr.Show();

        }

        private void bİLDİRİMGÖNDERToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void dOSYADANSMSToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DosyaSMS fr = new DosyaSMS();
            fr.Show();
        }

        private void iKSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IKSMS fr = new IKSMS();
            fr.Show();
        }

        private void çEKVeSENETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÇEKveSENET fr = new ÇEKveSENET();
            fr.Show();
        }

        private void mUHASEBENOTUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MUHASEBE_NOTU fr = new MUHASEBE_NOTU();
            fr.Show();
        }

        private void fİNANSRAPORUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FİNANS_RAPORU FR = new FİNANS_RAPORU();
            FR.Show();
        }

        private void mAAŞÖDEMETOPLUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MAAŞ_ÖDEME_TOPLU FR = new MAAŞ_ÖDEME_TOPLU();
            FR.Show();
        }

        private void eĞİTİMDESTEKTOPLUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EĞİTİM_DESTEK_TOPLU FR = new EĞİTİM_DESTEK_TOPLU();
            FR.Show();
        }

        private void pRİMKESİNTİKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PRİM_KESİNTİ_KONTROL FR=new PRİM_KESİNTİ_KONTROL();
            FR.Show();
        }

        private void dERSPROGRAMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DersDagitim.AnaForm af = new DersDagitim.AnaForm();
            af.Show();
        }

        private void eTÜTToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
           
        }

        private void öNKAYITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÖnKayıt fr = new ÖnKayıt();
            fr.Show();
        }

        private void fİYATLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FiyatListesi fr = new FiyatListesi();
            fr.Show();
        }

        private void dAVRANIŞLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Davranışlar fr = new Davranışlar();
            fr.Show();
        }

        private void dİĞERGELİRLERLİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DİĞERGELİRLİSTE fr = new DİĞERGELİRLİSTE();
            fr.Show();
        }

        private void gİDERLİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GİDERLİSTE FR = new GİDERLİSTE();
            FR.Show();
        }

        private void bORÇEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BORÇ_EKLE fr = new BORÇ_EKLE();
            fr.Show();
        }

        private void eTKİNLİKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void eTKLİNLİKARAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETKİNLİK_EKLE FR = new ETKİNLİK_EKLE();
            FR.Show();
        }

        private void eTKİNLİKLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ETKİNLİKLER fr = new ETKİNLİKLER();
            fr.Show();
        }

     

        private void bÜLTENVeBELGELERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BELGELER fr = new BELGELER();
            fr.Show();
        }

        private void yEMEKGİRİŞİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YEMEK_GİRİŞİ fr = new YEMEK_GİRİŞİ();
            fr.Show();
        }

        private void yEMEKLİSTESİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            YEMEK_LİSTESİ FR = new YEMEK_LİSTESİ();
            FR.Show();
        }

      

        private void öNKAYITRANDEVUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnKayitGorusmeler FR = new OnKayitGorusmeler();
            FR.Show();
        }

        private void eTÜTARAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Etut fr = new Etut();
            fr.Show();
        }

        private void öNKAYITToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ÖnKayıt fr = new ÖnKayıt();
            fr.Show();
        }

        private void vELİGÖRÜŞÜGİRİŞİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hALKLAİLİŞKİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
      
        }

        private void öĞRENCİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ogrenciler fr = new Ogrenciler();
            fr.Show();
        }

        private void kİTAPLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KİTAPLAR FR = new KİTAPLAR();
            FR.Show();
        }

        private void kİTAPHAREKETLERİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KİTAPHAREKETLERİ FR = new KİTAPHAREKETLERİ();
            FR.Show();
        }

        private void rAPORLARToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            KİTAPLAR_RAPOR FR = new KİTAPLAR_RAPOR();
            FR.Show();
        }

        private void sETTANIMLARIToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void rEVİRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            REVİR FR = new REVİR();
            FR.Show();
        }

        private void aJANDAGİRİŞToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AJANDA_GİRİŞ FR = new AJANDA_GİRİŞ();
            FR.Show();
        }

        private void hİZMETEKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void hİZMETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HİZMET_EKLE FR = new HİZMET_EKLE();
            FR.Show();
        }

        private void öĞRENCİKAYITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OgrenciKayit fr = new OgrenciKayit();
            fr.Show();
        }

        private void lOGToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aVUKATToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AVUKAT FR = new AVUKAT();
            FR.Show();
        }

        private void öNKAYITSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÖNKAYITSMS FR = new ÖNKAYITSMS();
            FR.Show();
        }

        private void öĞRENCİÖZELSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ÖĞRENCİÖZELSMS FR = new ÖĞRENCİÖZELSMS();
            FR.Show();

        }

        private void dOSYADANSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOSYADAN_SMS FR = new DOSYADAN_SMS();
            FR.Show();
        }

        private void hAZIRSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HAZIRSMS FR = new HAZIRSMS();
            FR.Show();
        }

        private void aNKETToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ANKET FR = new ANKET();
            FR.Show();
        }

        private void aNKETToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ANKET FR = new ANKET();
            FR.Show();
        }

        private void hIZLIKESİNKAYITToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void öĞRENCİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yOKLAMAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OgrenciYoklama fr = new OgrenciYoklama();
            fr.Show();
        }

        private void vELİGÖRÜŞGİRİŞİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VeliGorusuGirisi FR = new VeliGorusuGirisi();
            FR.Show();
        }

        private void sORUNÇÖZÜMLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SorunCozumleri fr = new SorunCozumleri();
            fr.Show();
        }

        private void tANIMLARToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void lİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Liste fr = new Liste();
            fr.Show();
        }

        private void fİYATLİSTESİToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FiyatListesi fr = new FiyatListesi();
            fr.Show();
        }

        private void öNKAYITLİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iÇKAYITToolStripMenuItem_Click(object sender, EventArgs e)
        {
            İÇ_KAYIT FR=new İÇ_KAYIT();
            FR.Show();
        }

        private void pERSONELToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void kAYITSİLMEİŞLEMLERİToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KAYIT_SİLME_İŞLEMLERİ FR = new KAYIT_SİLME_İŞLEMLERİ();
            FR.Show();
        }

        private void sINAVLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotGirisi fr = new NotGirisi();
            fr.Show();
        }

        private void kAYITİADEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KAYIT_İADE FR = new KAYIT_İADE();
            FR.Show();
        }

        private void aYARLARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AYARLAR FR = new AYARLAR();
            FR.Show();
        }

        private void vELİRANDEVUOLUŞTURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VeliRandevuOlustur FR = new VeliRandevuOlustur();
            FR.Show();
        }
    }
}
