using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DersDagitim
{
  public class AnaForm : Form
  {
    //private AnaForm.yeniVersiyonBilgileri yeniVersiyon;
    private AnaForm.hucreBilgi[,] bilgiler;
    private string[] basliklar;
    private string baslikAdi = "";
    private PrintAction prntAction;
    private int sayfaSayac;
    private IContainer components;
    private MenuStrip menuStrip1;
    private ToolStripMenuItem dosyaToolStripMenuItem;
    private ToolStripMenuItem açToolStripMenuItem;
    private ToolStripMenuItem kaydetToolStripMenuItem;
    private ToolStripMenuItem farklıKaydetToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem çıkışToolStripMenuItem;
    private ToolStripMenuItem yeniToolStripMenuItem;
    private ToolStripMenuItem bilgilerToolStripMenuItem;
    private ToolStripMenuItem genelBilgilerToolStripMenuItem;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem dersliklerToolStripMenuItem;
    private ToolStripMenuItem derslerToolStripMenuItem;
    private ToolStripMenuItem sınıflarToolStripMenuItem;
    private ToolStripMenuItem öğretmenlerToolStripMenuItem;
    private ToolStripMenuItem dersTanımlamaToolStripMenuItem;
    private ToolStripMenuItem yeniDersTanımlamaToolStripMenuItem;
    private ToolStripMenuItem tümTanımlıDerslerToolStripMenuItem;
    private ToolStripMenuItem dersDağıtımıToolStripMenuItem;
    private ToolStripMenuItem kontrolEtToolStripMenuItem;
    private ToolStripMenuItem dersProgramıToolStripMenuItem;
    private ToolStripMenuItem önizlemeToolStripMenuItem;
    private ToolStripMenuItem raporlarToolStripMenuItem;
    private ToolStripMenuItem elProgramlarıToolStripMenuItem;
    private ToolStripMenuItem programıİyileştirToolStripMenuItem;
    private ToolStripMenuItem yardımToolStripMenuItem;
    private ToolStripMenuItem hakkındaToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripMenuItem yerleşimleriKontrolEtToolStripMenuItem;
    private ToolStripMenuItem yardımKonularıToolStripMenuItem;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripMenuItem çarşafListeToolStripMenuItem;
    private ToolStripMenuItem sınıflarToolStripMenuItem1;
    private ToolStripMenuItem öğretmenlerToolStripMenuItem1;
    private ToolStripMenuItem dersliklerToolStripMenuItem1;

    public AnaForm() => this.InitializeComponent();

    private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
    {
      bool flag = true;
      if (tanim.program != null && MessageBox.Show("Açık ders programı var!!\nYeni ders programı oluşturulsun mu?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.No)
        flag = false;
      if (!flag)
        return;
      this.tumPencereleriKapat();
      tanim.program = new DersProgrami();
      this.adYenile();
      int num = (int) new GenelAyarlar().ShowDialog();
    }

    //private void versiyonIndir()
    //{
    //  try
    //  {
    //    string end = new StreamReader(new WebClient().OpenRead("http://bilgsoft.com/dagitmatik/dagitmatik.xml")).ReadToEnd();
    //    XmlDocument xmlDocument = new XmlDocument();
    //    xmlDocument.LoadXml(end);
    //    string innerText = xmlDocument.SelectSingleNode("versiyon/no").InnerText;
    //    this.yeniVersiyon = new AnaForm.yeniVersiyonBilgileri();
    //    this.yeniVersiyon.versiyon = innerText;
    //    this.yeniVersiyon.baglanti = xmlDocument.SelectSingleNode("versiyon/baglanti").InnerText;
    //    XmlNodeList xmlNodeList = xmlDocument.SelectNodes("versiyon/yenilikler/yenilik");
    //    this.yeniVersiyon.yenilikler = new List<string>();
    //    for (int i = 0; i < xmlNodeList.Count; ++i)
    //      this.yeniVersiyon.yenilikler.Add(xmlNodeList[i].InnerText);
    //    ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
    //    if (araclar.versiyon() != this.yeniVersiyon.versiyon)
    //    {
    //      toolStripMenuItem.Text = "Yeni versiyon bulundu. Tıklayın";
    //      toolStripMenuItem.Click += new EventHandler(this.yeniVersiyonIndir);
    //    }
    //    else
    //      toolStripMenuItem.Text = "Güncel versiyon kullanılıyor..";
    //    toolStripMenuItem.ForeColor = Color.Red;
    //    this.menuStrip1.Items.Add((ToolStripItem) toolStripMenuItem);
    //  }
    //  catch
    //  {
    //  }
    //  string requestUriString = "http://bilgsoft.com/dagitmatik/dagitmatikKullanici.php";
    //  string s = "";
    //  string[] strArray1 = new string[3]
    //  {
    //    "pass",
    //    "versiyon",
    //    "tarihsaat"
    //  };
    //  string[] strArray2 = new string[3]
    //  {
    //    "dagitmatikKullanici",
    //    araclar.versiyon(),
    //    string.Format("{0:yyyy-MM-dd HH:mm:ss}", (object) DateTime.Now)
    //  };
    //  for (int index = 0; index < strArray1.Length; ++index)
    //  {
    //    s = s + strArray1[index] + "=" + strArray2[index];
    //    if (index + 1 != strArray1.Length)
    //      s += "&";
    //  }
    //  string empty = string.Empty;
    //  try
    //  {
    //    byte[] bytes = Encoding.ASCII.GetBytes(s);
    //    HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestUriString);
    //    httpWebRequest.Method = "POST";
    //    httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
    //    httpWebRequest.ContentLength = (long) bytes.Length;
    //    using (Stream requestStream = httpWebRequest.GetRequestStream())
    //      requestStream.Write(bytes, 0, bytes.Length);
    //    using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
    //    {
    //      using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
    //        streamReader.ReadToEnd();
    //    }
    //  }
    //  catch
    //  {
    //  }
    //}

    //private void yeniVersiyonIndir(object nesne, EventArgs e)
    //{
    //  string str = string.Format("Mevcut versiyon \t: {0}\nYeni versiyon\t: {1}\n\nYenilikler:\n", (object) araclar.versiyon(), (object) this.yeniVersiyon.versiyon);
    //  for (int index = 0; index < this.yeniVersiyon.yenilikler.Count; ++index)
    //    str = str + "* " + this.yeniVersiyon.yenilikler[index] + "\n";
    //  if (MessageBox.Show(str + "\nİndirmek ister misiniz?", "Yeni Versiyon Bulundu", MessageBoxButtons.YesNo) != DialogResult.Yes)
    //    return;
    //  Process.Start(this.yeniVersiyon.baglanti);
    //}

    private void tumPencereleriKapat()
    {
      foreach (Form mdiChild in this.MdiChildren)
        mdiChild.Close();
    }

    private void AnaForm_Load(object sender, EventArgs e)
    {
      //Version version = Assembly.GetExecutingAssembly().GetName().Version;
      //this.Text = string.Format("DağıtMatik Ders Dağıtım Programı v{0}.{1}.{2} ({3})", (object) version.Major, (object) version.Minor, (object) version.Build, (object) version.Revision);
      //new Thread(new ThreadStart(this.versiyonIndir)).Start();
      //formHakkinda formHakkinda = new formHakkinda(true);
      //formHakkinda.FormBorderStyle = FormBorderStyle.None;
      //formHakkinda.StartPosition = FormStartPosition.Manual;
      //formHakkinda.MdiParent = (Form) this;
      //formHakkinda.Location = new Point(this.Width / 2 - formHakkinda.Width / 2, this.Height / 2 - formHakkinda.Height / 2);
      //formHakkinda.Show();
    }

    private void genelBilgilerToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program != null)
      {
        int num = (int) new GenelAyarlar().ShowDialog();
        foreach (Form mdiChild in this.MdiChildren)
        {
          if (mdiChild is BilgiGirisForm)
            (mdiChild as BilgiGirisForm).bilgileriYenile();
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Açık ders programı yok!");
      }
    }

    private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program != null)
      {
        tanim.program.kaydet();
        this.adYenile();
      }
      else
      {
        int num = (int) MessageBox.Show("Ders programı oluşturulmamış!!");
      }
    }

    private void farklıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program != null)
      {
        tanim.program.kaydet(true);
        this.adYenile();
      }
      else
      {
        int num = (int) MessageBox.Show("Ders programı oluşturulmamış!!");
      }
    }

    private void adYenile()
    {
      if (tanim.program.dosyaAdi != null)
        this.Text = tanim.program.dosyaAdi;
      else
        this.Text = "Kaydedilmemiş Ders Programı!";
    }

    private void açToolStripMenuItem_Click(object sender, EventArgs e)
    {
      bool flag = true;
      if (tanim.program != null && MessageBox.Show("Açık olan ders programı kapatılacaktır.\nOnaylıyor musunuz?", "Uyarı", MessageBoxButtons.YesNo) != DialogResult.Yes)
        flag = false;
      if (!flag)
        return;
      this.tumPencereleriKapat();
      DersProgrami dersProgrami = new DersProgrami(false);
      if (!dersProgrami.ac())
        return;
      tanim.program = dersProgrami;
      tanim.program.dagitimaHazirla();
      foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        bilesenTanimliDers.yerlesimeHazirla();
      for (int index1 = 0; index1 < tanim.program.tanimliDersler.Count; ++index1)
      {
        bilesenTanimliDers bilesenTanimliDers = tanim.program.tanimliDersler[index1];
        for (int index2 = 0; index2 < bilesenTanimliDers.olasiliklar.Count; ++index2)
        {
          bilesenTanimliDers.yerlesimOlasilik ol = bilesenTanimliDers.olasiliklar[index2];
          if (ol.yerlesimStr == bilesenTanimliDers.baslangicYerlesimi && bilesenTanimliDers.olasilikSina(ol))
            bilesenTanimliDers.yerles(ol);
        }
      }
      this.adYenile();
      this.bilgiFormGoster((ushort) 0);
    }

    private void bilgiFormGoster(ushort tabNo)
    {
      if (tanim.program != null)
      {
        bool flag = false;
        foreach (Form mdiChild in this.MdiChildren)
        {
          if (mdiChild is BilgiGirisForm)
          {
            mdiChild.Activate();
            (mdiChild as BilgiGirisForm).tbBilgiGirisleri.SelectedIndex = (int) tabNo;
            flag = true;
          }
        }
        if (flag)
          return;
        BilgiGirisForm bilgiGirisForm = new BilgiGirisForm(tabNo);
        bilgiGirisForm.MdiParent = (Form) this;
        bilgiGirisForm.Show();
      }
      else
      {
        int num = (int) MessageBox.Show("Açık ders programı yok!");
      }
    }

    private void formGoster(int frmNo, bool ozgur = true)
    {
      if (tanim.program != null)
      {
        Form form = (Form) null;
        if (frmNo == 1)
          form = (Form) new DersTanimlamaForm();
        if (frmNo == 2)
          form = (Form) new TanimliDersListesi();
        if (frmNo == 3)
          form = (Form) new KontrolForm();
        if (frmNo == 4)
          form = (Form) new DersProgramiOnIzlemeForm();
        if (frmNo == 5)
          form = (Form) new ElProgramiFrm();
        form.Owner = (Form) this;
        if (ozgur)
        {
          form.MdiParent = (Form) this;
          form.Show();
        }
        else
        {
          int num = (int) form.ShowDialog();
        }
      }
      else
      {
        int num1 = (int) MessageBox.Show("Açık ders programı yok!");
      }
    }

    private void dersliklerToolStripMenuItem_Click(object sender, EventArgs e) => this.bilgiFormGoster((ushort) 2);

    private void derslerToolStripMenuItem_Click(object sender, EventArgs e) => this.bilgiFormGoster((ushort) 0);

    private void öğretmenlerToolStripMenuItem_Click(object sender, EventArgs e) => this.bilgiFormGoster((ushort) 1);

    private void sınıflarToolStripMenuItem_Click(object sender, EventArgs e) => this.bilgiFormGoster((ushort) 3);

    private void yeniDersTanımlamaToolStripMenuItem_Click(object sender, EventArgs e) => this.formGoster(1, false);

    private void tümTanımlıDerslerToolStripMenuItem_Click(object sender, EventArgs e) => this.formGoster(2, false);

    private void kontrolEtToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.tumPencereleriKapat();
      if (tanim.program == null)
      {
        int num1 = (int) MessageBox.Show("Açık ders programı yok!!");
      }
      else if (tanim.program.tanimliDersler.Count == 0)
      {
        int num2 = (int) MessageBox.Show("Tanımlı Ders Yok");
      }
      else
      {
        bool flag1 = true;
        bool flag2 = false;
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim == null)
            flag1 = false;
        }
        if (flag1)
        {
          if (MessageBox.Show("Yerleşmiş ders dağılımı silinecek emin misiniz?", "Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            this.formGoster(3, false);
            flag2 = true;
          }
        }
        else
        {
          this.formGoster(3, false);
          flag2 = true;
        }
        if (!flag2)
          return;
        bool flag3 = true;
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim == null)
            flag3 = false;
        }
        if (!flag3)
          return;
        this.formGoster(4);
      }
    }

    private void önizlemeToolStripMenuItem_Click(object sender, EventArgs e) => this.formGoster(4);

    private void elProgramlarıToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program == null)
        return;
      if (tanim.program.tumuYerlesmis())
      {
        this.formGoster(5);
      }
      else
      {
        int num = (int) MessageBox.Show("Yerleşmeyen dersler var!");
      }
    }

    private void programıİyileştirToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program == null || tanim.program.tanimliDersler.Count <= 0)
        return;
      AkilliDagitimForm akilliDagitimForm = new AkilliDagitimForm(true);
      akilliDagitimForm.MdiParent = (Form) this;
      akilliDagitimForm.Show();
    }

    private void hakkındaToolStripMenuItem_Click(object sender, EventArgs e)
    {
      int num = (int) new formHakkinda().ShowDialog();
    }

    private void çıkışToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

    private void AnaForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (tanim.program == null)
        return;
      if (MessageBox.Show("Açık ders programı var. Çıkmak istiyor musunuz?", "Dikkat", MessageBoxButtons.YesNo) == DialogResult.No)
      {
        e.Cancel = true;
      }
      else
      {
        if (tanim.program.dosyaAdi != null || MessageBox.Show("Ders programı hiç kaydedilmemiş. Emin misiniz?", "Dikkat", MessageBoxButtons.YesNo) != DialogResult.No)
          return;
        e.Cancel = true;
      }
    }

    private void yerleşimleriKontrolEtToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (tanim.program != null)
      {
        int num1 = 0;
        int num2 = 0;
        string str = "";
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.nodes != null)
            num1 += bilesenTanimliDers.nodes.Length;
          num2 += (int) bilesenTanimliDers.toplamSaat;
          if (bilesenTanimliDers.aktifYerlesim == null)
            str = str + "[" + bilesenTanimliDers.aciklama + "] ";
        }
        if (str == "")
        {
          int num3 = (int) MessageBox.Show(string.Format("Tüm dersler yerleşmiştir:\nToplam Tanımlı Ders: {0}\nToplam Blok Sayısı: {1}\nToplam Ders Saati: {2}", (object) tanim.program.tanimliDersler.Count, (object) num1, (object) num2));
        }
        else
        {
          int num4 = (int) MessageBox.Show("Yerleşmeyen dersler var:\n" + str);
        }
      }
      else
      {
        int num = (int) MessageBox.Show("Açık ders programı yok!");
      }
    }

    private void carsafListe(int carsafNo, AnaForm anaForm)
    {
            if (tanim.program == null)
            {
                int num1 = (int)MessageBox.Show("Açık ders programı yok!");
            }
            else if (!tanim.program.tumuYerlesmis())
            {
                int num2 = (int)MessageBox.Show("Yerleşmeyen dersler var!");
            }
            else
            {
                int num3;
                switch (carsafNo)
                {
                    case 1:
                        num3 = tanim.program.siniflar.Count;
                        break;
                    case 2:
                        num3 = tanim.program.ogretmenler.Count;
                        break;
                    case 3:
                        num3 = tanim.program.derslikler.Count;
                        break;
                    default:
                        num3 = 0;
                        break;
                }
                int length = num3;
                if (length == 0)
                    return;
                this.basliklar = new string[length];
                this.bilgiler = new AnaForm.hucreBilgi[length, (int)tanim.program.haftalikGunSayisi * (int)tanim.program.gunlukDersSaatiSayisi];
                for (int index1 = 0; index1 < this.bilgiler.GetLength(0); ++index1)
                {
                    for (int index2 = 0; index2 < this.bilgiler.GetLength(1); ++index2)
                        this.bilgiler[index1, index2].b1 = this.bilgiler[index1, index2].b2 = this.bilgiler[index1, index2].b3 = "";
                }
                if (carsafNo == 1)
                {
                    this.baslikAdi = "SINIFLAR";
                    for (int index1 = 0; index1 < tanim.program.siniflar.Count; ++index1)
                    {
                        bilesenSinif bilesenSinif = tanim.program.siniflar[index1];
                        this.basliklar[index1] = bilesenSinif.adi;
                        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
                        {
                            foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                            {
                                if (bilesenSinif == bilesenSinifGrup.sinif)
                                {
                                    bool[,] tablo = bilesenTanimliDers.aktifYerlesim.tablo;
                                    for (int index2 = 0; index2 < tablo.GetLength(0); ++index2)
                                    {
                                        for (int index3 = 0; index3 < tablo.GetLength(1); ++index3)
                                        {
                                            if (!tablo[index2, index3])
                                            {
                                                int index4 = index2 * (int)tanim.program.gunlukDersSaatiSayisi + index3;
                                                this.bilgiler[index1, index4].b1 += bilesenTanimliDers.ders.kisaAdi;
                                                foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                                                {
                                                    ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                    local.b2 = local.b2 + bilesenOgretmen.kisaAdi + " ";
                                                }
                                                this.bilgiler[index1, index4].b2 = this.bilgiler[index1, index4].b2.Remove(this.bilgiler[index1, index4].b2.Length - 1, 1);
                                                foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                                                {
                                                    ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                    local.b3 = local.b3 + bilesenDerslik.kisaAdi + " ";
                                                }
                                                if (this.bilgiler[index1, index4].b3.Length > 1)
                                                    this.bilgiler[index1, index4].b3 = this.bilgiler[index1, index4].b3.Remove(this.bilgiler[index1, index4].b3.Length - 1, 1);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (carsafNo == 2)
                {
                    this.baslikAdi = "ÖĞRETMENLER";
                    for (int index1 = 0; index1 < tanim.program.ogretmenler.Count; ++index1)
                    {
                        bilesenOgretmen bilesenOgretmen = tanim.program.ogretmenler[index1];
                        this.basliklar[index1] = bilesenOgretmen.adi;
                        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
                        {
                            if (bilesenTanimliDers.ogretmenler.Contains(bilesenOgretmen))
                            {
                                bool[,] tablo = bilesenTanimliDers.aktifYerlesim.tablo;
                                for (int index2 = 0; index2 < tablo.GetLength(0); ++index2)
                                {
                                    for (int index3 = 0; index3 < tablo.GetLength(1); ++index3)
                                    {
                                        if (!tablo[index2, index3])
                                        {
                                            int index4 = index2 * (int)tanim.program.gunlukDersSaatiSayisi + index3;
                                            this.bilgiler[index1, index4].b1 += bilesenTanimliDers.ders.kisaAdi;
                                            foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                                            {
                                                ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                local.b2 = local.b2 + bilesenSinifGrup.sinif.kisaAdi + " ";
                                            }
                                            this.bilgiler[index1, index4].b2 = this.bilgiler[index1, index4].b2.Remove(this.bilgiler[index1, index4].b2.Length - 1, 1);
                                            foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                                            {
                                                ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                local.b3 = local.b3 + bilesenDerslik.kisaAdi + " ";
                                            }
                                            if (this.bilgiler[index1, index4].b3.Length > 1)
                                                this.bilgiler[index1, index4].b3 = this.bilgiler[index1, index4].b3.Remove(this.bilgiler[index1, index4].b3.Length - 1, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (carsafNo == 3)
                {
                    this.baslikAdi = "DERSLİKLER";
                    for (int index1 = 0; index1 < tanim.program.derslikler.Count; ++index1)
                    {
                        bilesenDerslik bilesenDerslik = tanim.program.derslikler[index1];
                        this.basliklar[index1] = bilesenDerslik.adi;
                        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
                        {
                            if (bilesenTanimliDers.derslikler.Contains(bilesenDerslik))
                            {
                                bool[,] tablo = bilesenTanimliDers.aktifYerlesim.tablo;
                                for (int index2 = 0; index2 < tablo.GetLength(0); ++index2)
                                {
                                    for (int index3 = 0; index3 < tablo.GetLength(1); ++index3)
                                    {
                                        if (!tablo[index2, index3])
                                        {
                                            int index4 = index2 * (int)tanim.program.gunlukDersSaatiSayisi + index3;
                                            this.bilgiler[index1, index4].b1 += bilesenTanimliDers.ders.kisaAdi;
                                            foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                                            {
                                                ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                local.b2 = local.b2 + bilesenSinifGrup.sinif.kisaAdi + " ";
                                            }
                                            this.bilgiler[index1, index4].b2 = this.bilgiler[index1, index4].b2.Remove(this.bilgiler[index1, index4].b2.Length - 1, 1);
                                            foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                                            {
                                                ref AnaForm.hucreBilgi local = ref this.bilgiler[index1, index4];
                                                local.b3 = local.b3 + bilesenOgretmen.kisaAdi + " ";
                                            }
                                            this.bilgiler[index1, index4].b3 = this.bilgiler[index1, index4].b3.Remove(this.bilgiler[index1, index4].b3.Length - 1, 1);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                PrintDocument printDocument = new PrintDocument();
                printDocument.BeginPrint += (PrintEventHandler)((nesne, ev) => this.prntAction = ev.PrintAction);
                printDocument.PrintPage += new PrintPageEventHandler(this.CarsafListeRapor);
                printDocument.DefaultPageSettings.Landscape = true;
                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
                printPreviewDialog.Document = printDocument;
                printPreviewDialog.PrintPreviewControl.Zoom = 1.0;
                ((Form)printPreviewDialog).WindowState = FormWindowState.Maximized;
                this.sayfaSayac = 0;
                int num4 = (int)printPreviewDialog.ShowDialog();
            }
        }

    private void CarsafListeRapor(object nesne, PrintPageEventArgs e)
    {
      e.HasMorePages = true;
      Graphics graphics = e.Graphics;
      int x = 20;
      int num1 = 20;
      if (this.prntAction != PrintAction.PrintToPreview)
      {
        x -= (int) e.PageSettings.HardMarginX;
        num1 -= (int) e.PageSettings.HardMarginY;
      }
      int width1 = e.PageBounds.Width - x - 20;
      int num2 = e.PageBounds.Height - num1 - 20;
      int haftalikGunSayisi = (int) tanim.program.haftalikGunSayisi;
      int gunlukDersSaatiSayisi = (int) tanim.program.gunlukDersSaatiSayisi;
      Brush brush = (Brush) new SolidBrush(Color.Black);
      Pen pen1 = new Pen(brush, 1.4f);
      Pen pen2 = new Pen(brush, 0.5f);
      Font font1 = new Font("Times New Roman", 10f, FontStyle.Bold);
      Font font2 = new Font("Times New Roman", 7f, FontStyle.Regular);
      StringFormat format1 = new StringFormat();
      format1.Alignment = StringAlignment.Center;
      format1.LineAlignment = StringAlignment.Center;
      StringFormat format2 = new StringFormat();
      format2.Alignment = StringAlignment.Near;
      format2.LineAlignment = StringAlignment.Center;
      StringFormat stringFormat = new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Near
      };
      int y1 = num1;
      string s1 = string.Format("{0} {1} EĞİTİM ÖĞRETİM YILI {2} DERS PROGRAMI LİSTESİ", (object) tanim.program.okulAdi, (object) tanim.program.ogretimYili, (object) this.baslikAdi);
      graphics.DrawString(s1, font1, brush, (RectangleF) new Rectangle(x, y1, width1, 20), format1);
      int y2 = y1 + 20;
      int y3 = y2;
      int num3 = haftalikGunSayisi * gunlukDersSaatiSayisi;
      int width2 = (width1 - 80) / num3;
      for (int index = 0; index < haftalikGunSayisi; ++index)
      {
        string s2 = tanim.program.gunler[index];
        Rectangle rect = new Rectangle(x + 80 + index * width2 * gunlukDersSaatiSayisi, y2, width2 * gunlukDersSaatiSayisi, 20);
        graphics.DrawRectangle(pen1, rect);
        graphics.DrawString(s2, font1, brush, (RectangleF) rect, format1);
      }
      int y4 = y2 + 20;
      for (int index = 0; index < gunlukDersSaatiSayisi * haftalikGunSayisi; ++index)
      {
        Font font3 = new Font("Times New Roman", 5f, FontStyle.Regular);
        string s2 = string.Format("{0}\n{1}", (object) (index % gunlukDersSaatiSayisi + 1), (object) tanim.program.derssaatleri[index % gunlukDersSaatiSayisi]).Replace('-', '\n');
        Rectangle rect = new Rectangle(x + 80 + index * width2, y4, width2, 25);
        graphics.DrawRectangle(pen2, rect);
        graphics.DrawString(s2, font3, brush, (RectangleF) rect, format1);
      }
      int y5 = y4 + 25;
      float emSize = 8f;
      Font font4;
      SizeF sizeF;
      do
      {
        font4 = new Font("Times New Roman", emSize, FontStyle.Regular);
        sizeF = e.Graphics.MeasureString("AAAAA", font4);
        emSize -= 0.01f;
      }
      while ((double) sizeF.Width > (double) width2);
      for (int index1 = 0; index1 < this.basliklar.Length; ++index1)
      {
        Rectangle rect1 = new Rectangle(x, y5, 80, 36);
        graphics.DrawRectangle(pen2, rect1);
        graphics.DrawString(this.basliklar[this.sayfaSayac], font2, brush, (RectangleF) rect1, format2);
        for (int index2 = 0; index2 < haftalikGunSayisi * gunlukDersSaatiSayisi; ++index2)
        {
          string s2 = string.Format("{0}\n{1}\n{2}", (object) this.bilgiler[this.sayfaSayac, index2].b1, (object) this.bilgiler[this.sayfaSayac, index2].b2, (object) this.bilgiler[this.sayfaSayac, index2].b3);
          Rectangle rect2 = new Rectangle(x + 80 + index2 * width2, y5, width2, 36);
          graphics.DrawRectangle(pen2, rect2);
          graphics.DrawString(s2, font4, brush, (RectangleF) rect2, format1);
        }
        if (++this.sayfaSayac == this.basliklar.Length)
        {
          e.HasMorePages = false;
          this.sayfaSayac = 0;
          break;
        }
        y5 += 36;
        if (y5 >= num2 - 36)
        {
          y5 -= 36;
          break;
        }
      }
      for (int index = 0; index < haftalikGunSayisi; ++index)
      {
        Rectangle rect = new Rectangle(x + 80 + index * width2 * gunlukDersSaatiSayisi, y3, width2 * gunlukDersSaatiSayisi, y5 - y3 + 36);
        graphics.DrawRectangle(pen1, rect);
      }
    }

    private void sınıflarToolStripMenuItem1_Click(object sender, EventArgs e) => this.carsafListe(1, this);

    private void öğretmenlerToolStripMenuItem1_Click(object sender, EventArgs e) => this.carsafListe(2, this);

    private void dersliklerToolStripMenuItem1_Click(object sender, EventArgs e) => this.carsafListe(3, this);

    //private void yardımKonularıToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("http://bilgsoft.com/dagitmatik/yardim/");

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (AnaForm));
      this.menuStrip1 = new MenuStrip();
      this.dosyaToolStripMenuItem = new ToolStripMenuItem();
      this.yeniToolStripMenuItem = new ToolStripMenuItem();
      this.açToolStripMenuItem = new ToolStripMenuItem();
      this.kaydetToolStripMenuItem = new ToolStripMenuItem();
      this.farklıKaydetToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem1 = new ToolStripSeparator();
      this.çıkışToolStripMenuItem = new ToolStripMenuItem();
      this.bilgilerToolStripMenuItem = new ToolStripMenuItem();
      this.genelBilgilerToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripMenuItem2 = new ToolStripSeparator();
      this.derslerToolStripMenuItem = new ToolStripMenuItem();
      this.öğretmenlerToolStripMenuItem = new ToolStripMenuItem();
      this.dersliklerToolStripMenuItem = new ToolStripMenuItem();
      this.sınıflarToolStripMenuItem = new ToolStripMenuItem();
      this.dersTanımlamaToolStripMenuItem = new ToolStripMenuItem();
      this.yeniDersTanımlamaToolStripMenuItem = new ToolStripMenuItem();
      this.tümTanımlıDerslerToolStripMenuItem = new ToolStripMenuItem();
      this.dersDağıtımıToolStripMenuItem = new ToolStripMenuItem();
      this.kontrolEtToolStripMenuItem = new ToolStripMenuItem();
      this.programıİyileştirToolStripMenuItem = new ToolStripMenuItem();
      this.dersProgramıToolStripMenuItem = new ToolStripMenuItem();
      this.önizlemeToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator1 = new ToolStripSeparator();
      this.yerleşimleriKontrolEtToolStripMenuItem = new ToolStripMenuItem();
      this.raporlarToolStripMenuItem = new ToolStripMenuItem();
      this.elProgramlarıToolStripMenuItem = new ToolStripMenuItem();
      this.çarşafListeToolStripMenuItem = new ToolStripMenuItem();
      this.sınıflarToolStripMenuItem1 = new ToolStripMenuItem();
      this.öğretmenlerToolStripMenuItem1 = new ToolStripMenuItem();
      this.dersliklerToolStripMenuItem1 = new ToolStripMenuItem();
      //this.yardımToolStripMenuItem = new ToolStripMenuItem();
      //this.yardımKonularıToolStripMenuItem = new ToolStripMenuItem();
      this.toolStripSeparator2 = new ToolStripSeparator();
      this.hakkındaToolStripMenuItem = new ToolStripMenuItem();
      this.menuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.menuStrip1.Items.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.dosyaToolStripMenuItem,
        (ToolStripItem) this.bilgilerToolStripMenuItem,
        (ToolStripItem) this.dersTanımlamaToolStripMenuItem,
        (ToolStripItem) this.dersDağıtımıToolStripMenuItem,
        (ToolStripItem) this.dersProgramıToolStripMenuItem,
        (ToolStripItem) this.raporlarToolStripMenuItem
      });
      this.menuStrip1.Location = new Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new Size(868, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      this.dosyaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.yeniToolStripMenuItem,
        (ToolStripItem) this.açToolStripMenuItem,
        (ToolStripItem) this.kaydetToolStripMenuItem,
        (ToolStripItem) this.farklıKaydetToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem1,
        (ToolStripItem) this.çıkışToolStripMenuItem
      });
      this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
      this.dosyaToolStripMenuItem.Size = new Size(51, 20);
      this.dosyaToolStripMenuItem.Text = "Dosya";
      this.yeniToolStripMenuItem.Name = "yeniToolStripMenuItem";
      this.yeniToolStripMenuItem.Size = new Size(152, 22);
      this.yeniToolStripMenuItem.Text = "Yeni";
      this.yeniToolStripMenuItem.Click += new EventHandler(this.yeniToolStripMenuItem_Click);
      this.açToolStripMenuItem.Name = "açToolStripMenuItem";
      this.açToolStripMenuItem.Size = new Size(152, 22);
      this.açToolStripMenuItem.Text = "Aç";
      this.açToolStripMenuItem.Click += new EventHandler(this.açToolStripMenuItem_Click);
      this.kaydetToolStripMenuItem.Name = "kaydetToolStripMenuItem";
      this.kaydetToolStripMenuItem.Size = new Size(152, 22);
      this.kaydetToolStripMenuItem.Text = "Kaydet";
      this.kaydetToolStripMenuItem.Click += new EventHandler(this.kaydetToolStripMenuItem_Click);
      this.farklıKaydetToolStripMenuItem.Name = "farklıKaydetToolStripMenuItem";
      this.farklıKaydetToolStripMenuItem.Size = new Size(152, 22);
      this.farklıKaydetToolStripMenuItem.Text = "Farklı Kaydet";
      this.farklıKaydetToolStripMenuItem.Click += new EventHandler(this.farklıKaydetToolStripMenuItem_Click);
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new Size(149, 6);
      this.çıkışToolStripMenuItem.Name = "çıkışToolStripMenuItem";
      this.çıkışToolStripMenuItem.Size = new Size(152, 22);
      this.çıkışToolStripMenuItem.Text = "Çıkış";
      this.çıkışToolStripMenuItem.Click += new EventHandler(this.çıkışToolStripMenuItem_Click);
      this.bilgilerToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
      {
        (ToolStripItem) this.genelBilgilerToolStripMenuItem,
        (ToolStripItem) this.toolStripMenuItem2,
        (ToolStripItem) this.derslerToolStripMenuItem,
        (ToolStripItem) this.öğretmenlerToolStripMenuItem,
        (ToolStripItem) this.dersliklerToolStripMenuItem,
        (ToolStripItem) this.sınıflarToolStripMenuItem
      });
      this.bilgilerToolStripMenuItem.Name = "bilgilerToolStripMenuItem";
      this.bilgilerToolStripMenuItem.Size = new Size(55, 20);
      this.bilgilerToolStripMenuItem.Text = "Bilgiler";
      this.genelBilgilerToolStripMenuItem.Name = "genelBilgilerToolStripMenuItem";
      this.genelBilgilerToolStripMenuItem.Size = new Size(143, 22);
      this.genelBilgilerToolStripMenuItem.Text = "Genel Bilgiler";
      this.genelBilgilerToolStripMenuItem.Click += new EventHandler(this.genelBilgilerToolStripMenuItem_Click);
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new Size(140, 6);
      this.derslerToolStripMenuItem.Name = "derslerToolStripMenuItem";
      this.derslerToolStripMenuItem.Size = new Size(143, 22);
      this.derslerToolStripMenuItem.Text = "Dersler";
      this.derslerToolStripMenuItem.Click += new EventHandler(this.derslerToolStripMenuItem_Click);
      this.öğretmenlerToolStripMenuItem.Name = "öğretmenlerToolStripMenuItem";
      this.öğretmenlerToolStripMenuItem.Size = new Size(143, 22);
      this.öğretmenlerToolStripMenuItem.Text = "Öğretmenler";
      this.öğretmenlerToolStripMenuItem.Click += new EventHandler(this.öğretmenlerToolStripMenuItem_Click);
      this.dersliklerToolStripMenuItem.Name = "dersliklerToolStripMenuItem";
      this.dersliklerToolStripMenuItem.Size = new Size(143, 22);
      this.dersliklerToolStripMenuItem.Text = "Derslikler";
      this.dersliklerToolStripMenuItem.Click += new EventHandler(this.dersliklerToolStripMenuItem_Click);
      this.sınıflarToolStripMenuItem.Name = "sınıflarToolStripMenuItem";
      this.sınıflarToolStripMenuItem.Size = new Size(143, 22);
      this.sınıflarToolStripMenuItem.Text = "Sınıflar";
      this.sınıflarToolStripMenuItem.Click += new EventHandler(this.sınıflarToolStripMenuItem_Click);
      this.dersTanımlamaToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.yeniDersTanımlamaToolStripMenuItem,
        (ToolStripItem) this.tümTanımlıDerslerToolStripMenuItem
      });
      this.dersTanımlamaToolStripMenuItem.Name = "dersTanımlamaToolStripMenuItem";
      this.dersTanımlamaToolStripMenuItem.Size = new Size(98, 20);
      this.dersTanımlamaToolStripMenuItem.Text = "Tanımlı Dersler";
      this.yeniDersTanımlamaToolStripMenuItem.Name = "yeniDersTanımlamaToolStripMenuItem";
      this.yeniDersTanımlamaToolStripMenuItem.Size = new Size(186, 22);
      this.yeniDersTanımlamaToolStripMenuItem.Text = "Yeni Ders Tanımlama";
      this.yeniDersTanımlamaToolStripMenuItem.Click += new EventHandler(this.yeniDersTanımlamaToolStripMenuItem_Click);
      this.tümTanımlıDerslerToolStripMenuItem.Name = "tümTanımlıDerslerToolStripMenuItem";
      this.tümTanımlıDerslerToolStripMenuItem.Size = new Size(186, 22);
      this.tümTanımlıDerslerToolStripMenuItem.Text = "Tüm Tanımlı Dersler";
      this.tümTanımlıDerslerToolStripMenuItem.Click += new EventHandler(this.tümTanımlıDerslerToolStripMenuItem_Click);
      this.dersDağıtımıToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.kontrolEtToolStripMenuItem,
        (ToolStripItem) this.programıİyileştirToolStripMenuItem
      });
      this.dersDağıtımıToolStripMenuItem.Name = "dersDağıtımıToolStripMenuItem";
      this.dersDağıtımıToolStripMenuItem.Size = new Size(90, 20);
      this.dersDağıtımıToolStripMenuItem.Text = "Ders Dağıtımı";
      this.kontrolEtToolStripMenuItem.Name = "kontrolEtToolStripMenuItem";
      this.kontrolEtToolStripMenuItem.Size = new Size(165, 22);
      this.kontrolEtToolStripMenuItem.Text = "Yeni Dağıtım Yap";
      this.kontrolEtToolStripMenuItem.Click += new EventHandler(this.kontrolEtToolStripMenuItem_Click);
      this.programıİyileştirToolStripMenuItem.Name = "programıİyileştirToolStripMenuItem";
      this.programıİyileştirToolStripMenuItem.Size = new Size(165, 22);
      this.programıİyileştirToolStripMenuItem.Text = "Programı İyileştir";
      this.programıİyileştirToolStripMenuItem.Visible = false;
      this.programıİyileştirToolStripMenuItem.Click += new EventHandler(this.programıİyileştirToolStripMenuItem_Click);
      this.dersProgramıToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.önizlemeToolStripMenuItem,
        (ToolStripItem) this.toolStripSeparator1,
        (ToolStripItem) this.yerleşimleriKontrolEtToolStripMenuItem
      });
      this.dersProgramıToolStripMenuItem.Name = "dersProgramıToolStripMenuItem";
      this.dersProgramıToolStripMenuItem.Size = new Size(94, 20);
      this.dersProgramıToolStripMenuItem.Text = "Ders Programı";
      this.önizlemeToolStripMenuItem.Name = "önizlemeToolStripMenuItem";
      this.önizlemeToolStripMenuItem.Size = new Size(190, 22);
      this.önizlemeToolStripMenuItem.Text = "Önizleme";
      this.önizlemeToolStripMenuItem.Click += new EventHandler(this.önizlemeToolStripMenuItem_Click);
      this.toolStripSeparator1.Name = "toolStripSeparator1";
      this.toolStripSeparator1.Size = new Size(187, 6);
      this.yerleşimleriKontrolEtToolStripMenuItem.Name = "yerleşimleriKontrolEtToolStripMenuItem";
      this.yerleşimleriKontrolEtToolStripMenuItem.Size = new Size(190, 22);
      this.yerleşimleriKontrolEtToolStripMenuItem.Text = "Yerleşimleri Kontrol Et";
      this.yerleşimleriKontrolEtToolStripMenuItem.Click += new EventHandler(this.yerleşimleriKontrolEtToolStripMenuItem_Click);
      this.raporlarToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.elProgramlarıToolStripMenuItem,
        (ToolStripItem) this.çarşafListeToolStripMenuItem
      });
      this.raporlarToolStripMenuItem.Name = "raporlarToolStripMenuItem";
      this.raporlarToolStripMenuItem.Size = new Size(63, 20);
      this.raporlarToolStripMenuItem.Text = "Raporlar";
      this.elProgramlarıToolStripMenuItem.Name = "elProgramlarıToolStripMenuItem";
      this.elProgramlarıToolStripMenuItem.Size = new Size(148, 22);
      this.elProgramlarıToolStripMenuItem.Text = "El Programları";
      this.elProgramlarıToolStripMenuItem.Click += new EventHandler(this.elProgramlarıToolStripMenuItem_Click);
      this.çarşafListeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      {
        (ToolStripItem) this.sınıflarToolStripMenuItem1,
        (ToolStripItem) this.öğretmenlerToolStripMenuItem1,
        (ToolStripItem) this.dersliklerToolStripMenuItem1
      });
      this.çarşafListeToolStripMenuItem.Name = "çarşafListeToolStripMenuItem";
      this.çarşafListeToolStripMenuItem.Size = new Size(148, 22);
      this.çarşafListeToolStripMenuItem.Text = "Çarşaf Liste";
      this.sınıflarToolStripMenuItem1.Name = "sınıflarToolStripMenuItem1";
      this.sınıflarToolStripMenuItem1.Size = new Size(141, 22);
      this.sınıflarToolStripMenuItem1.Text = "Sınıflar";
      this.sınıflarToolStripMenuItem1.Click += new EventHandler(this.sınıflarToolStripMenuItem1_Click);
      this.öğretmenlerToolStripMenuItem1.Name = "öğretmenlerToolStripMenuItem1";
      this.öğretmenlerToolStripMenuItem1.Size = new Size(141, 22);
      this.öğretmenlerToolStripMenuItem1.Text = "Öğretmenler";
      this.öğretmenlerToolStripMenuItem1.Click += new EventHandler(this.öğretmenlerToolStripMenuItem1_Click);
      this.dersliklerToolStripMenuItem1.Name = "dersliklerToolStripMenuItem1";
      this.dersliklerToolStripMenuItem1.Size = new Size(141, 22);
      this.dersliklerToolStripMenuItem1.Text = "Derslikler";
      this.dersliklerToolStripMenuItem1.Click += new EventHandler(this.dersliklerToolStripMenuItem1_Click);
      //this.yardımToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
      //{
      //  (ToolStripItem) this.yardımKonularıToolStripMenuItem,
      //  (ToolStripItem) this.toolStripSeparator2,
      //  (ToolStripItem) this.hakkındaToolStripMenuItem
      //});
      //this.yardımToolStripMenuItem.Name = "yardımToolStripMenuItem";
      //this.yardımToolStripMenuItem.Size = new Size(57, 20);
      //this.yardımToolStripMenuItem.Text = "Yardım";
      //this.yardımKonularıToolStripMenuItem.Name = "yardımKonularıToolStripMenuItem";
      //this.yardımKonularıToolStripMenuItem.Size = new Size(159, 22);
      //this.yardımKonularıToolStripMenuItem.Text = "Yardım Konuları";
      //this.yardımKonularıToolStripMenuItem.Click += new EventHandler(this.yardımKonularıToolStripMenuItem_Click);
      this.toolStripSeparator2.Name = "toolStripSeparator2";
      this.toolStripSeparator2.Size = new Size(156, 6);
      this.hakkındaToolStripMenuItem.Name = "hakkındaToolStripMenuItem";
      this.hakkındaToolStripMenuItem.Size = new Size(159, 22);
      this.hakkındaToolStripMenuItem.Text = "Hakkında";
      this.hakkındaToolStripMenuItem.Click += new EventHandler(this.hakkındaToolStripMenuItem_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(868, 722);
      this.Controls.Add((Control) this.menuStrip1);
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.IsMdiContainer = true;
      this.MainMenuStrip = this.menuStrip1;
      this.Name = nameof (AnaForm);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Ders Dağıtım Ekranı";
      this.WindowState = FormWindowState.Maximized;
      this.FormClosing += new FormClosingEventHandler(this.AnaForm_FormClosing);
      this.Load += new EventHandler(this.AnaForm_Load);
      this.menuStrip1.ResumeLayout(false);
      this.menuStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    //private struct yeniVersiyonBilgileri
    //{
    //  public string versiyon;
    //  public string baglanti;
    //  public List<string> yenilikler;
    //}

    private struct hucreBilgi
    {
      public string b1;
      public string b2;
      public string b3;
    }
  }
}
