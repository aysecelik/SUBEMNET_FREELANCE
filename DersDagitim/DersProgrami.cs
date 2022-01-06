using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace DersDagitim
{
  public class DersProgrami
  {
    public string okulAdi;
    public string okulMuduru;
    public string okulMudurYrd;
    public bool mudurYrdBas;
    public string ogretimYili;
    public byte gunlukDersSaatiSayisi;
    public byte haftalikGunSayisi;
    public string[] gunler;
    public string[] derssaatleri;
    public bool[,] kosullar;
    public string dosyaAdi;
    public ushort idDerslikSon;
    public ushort idOgretmenSon;
    public ushort idSinifSon;
    public ushort idDersSon;
    public ushort idTanimliDersSon;
    public List<bilesenDers> dersler = new List<bilesenDers>();
    public List<bilesenOgretmen> ogretmenler = new List<bilesenOgretmen>();
    public List<bilesenSinif> siniflar = new List<bilesenSinif>();
    public List<bilesenDerslik> derslikler = new List<bilesenDerslik>();
    public List<bilesenTanimliDers> tanimliDersler = new List<bilesenTanimliDers>();

    public bilesenDers dersGetir(ushort id)
    {
      bilesenDers bilesenDers = (bilesenDers) null;
      for (int index = 0; index < this.dersler.Count; ++index)
      {
        if ((int) this.dersler[index].id == (int) id)
          bilesenDers = this.dersler[index];
      }
      return bilesenDers;
    }

    public bilesenOgretmen ogretmenGetir(ushort id)
    {
      bilesenOgretmen bilesenOgretmen = (bilesenOgretmen) null;
      for (int index = 0; index < this.ogretmenler.Count; ++index)
      {
        if ((int) this.ogretmenler[index].id == (int) id)
          bilesenOgretmen = this.ogretmenler[index];
      }
      return bilesenOgretmen;
    }

    public bilesenDerslik derslikGetir(ushort id)
    {
      bilesenDerslik bilesenDerslik = (bilesenDerslik) null;
      for (int index = 0; index < this.derslikler.Count; ++index)
      {
        if ((int) this.derslikler[index].id == (int) id)
          bilesenDerslik = this.derslikler[index];
      }
      return bilesenDerslik;
    }

    public bilesenSinif sinifGetir(ushort id)
    {
      bilesenSinif bilesenSinif = (bilesenSinif) null;
      for (int index = 0; index < this.siniflar.Count; ++index)
      {
        if ((int) this.siniflar[index].id == (int) id)
          bilesenSinif = this.siniflar[index];
      }
      return bilesenSinif;
    }

    public bilesenTanimliDers tanimliDersGetir(ushort id)
    {
      bilesenTanimliDers bilesenTanimliDers = (bilesenTanimliDers) null;
      for (int index = 0; index < this.tanimliDersler.Count; ++index)
      {
        if ((int) this.tanimliDersler[index].id == (int) id)
          bilesenTanimliDers = this.tanimliDersler[index];
      }
      return bilesenTanimliDers;
    }

    public bool tumuYerlesmis()
    {
      foreach (bilesenTanimliDers bilesenTanimliDers in this.tanimliDersler)
      {
        if (bilesenTanimliDers.aktifYerlesim == null)
          return false;
      }
      return true;
    }

    public ushort bilesenDersSayisi(bilesenTaban bilesen, bilesenTaban grupSay = null)
    {
      ushort num1 = 0;
      if (bilesen is bilesenDers)
      {
        ushort id = (bilesen as bilesenDers).id;
        for (int index = 0; index < this.tanimliDersler.Count; ++index)
        {
          bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index];
          if ((int) id == (int) bilesenTanimliDers.ders.id)
            num1 += bilesenTanimliDers.toplamSaat;
        }
      }
      if (bilesen is bilesenDerslik)
      {
        ushort id = (bilesen as bilesenDerslik).id;
        for (int index = 0; index < this.tanimliDersler.Count; ++index)
        {
          bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index];
          foreach (bilesenTaban bilesenTaban in bilesenTanimliDers.derslikler)
          {
            if ((int) bilesenTaban.id == (int) id)
              num1 += bilesenTanimliDers.toplamSaat;
          }
        }
      }
      if (bilesen is bilesenSinif)
      {
        ushort num2 = 0;
        bilesenSinif bilesenSinif = bilesen as bilesenSinif;
        ushort id1 = bilesenSinif.id;
        ushort[,] numArray = new ushort[2, bilesenSinif.gruplar.Count];
        for (int index = 0; index < bilesenSinif.gruplar.Count; ++index)
        {
          bilesenGrup bilesenGrup = bilesenSinif.gruplar[index] as bilesenGrup;
          numArray[0, index] = bilesenGrup.id;
        }
        for (int index1 = 0; index1 < this.tanimliDersler.Count; ++index1)
        {
          bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index1];
          foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
          {
            if ((int) bilesenSinifGrup.sinif.id == (int) id1)
            {
              for (int index2 = 0; index2 < bilesenSinif.gruplar.Count; ++index2)
              {
                ushort id2 = (bilesenSinif.gruplar[index2] as bilesenGrup).id;
                if ((int) bilesenSinifGrup.grup.id == (int) id2)
                  numArray[1, index2] += bilesenTanimliDers.toplamSaat;
              }
            }
          }
        }
        if (grupSay != null)
        {
          ushort id2 = (grupSay as bilesenGrup).id;
          for (int index = 0; index < numArray.GetLength(1); ++index)
          {
            if ((int) id2 == (int) numArray[0, index])
              num2 = numArray[1, index];
          }
        }
        else
        {
          ushort num3 = numArray[1, 0];
          ushort num4 = 0;
          for (int index = 1; index < numArray.GetLength(1); ++index)
          {
            if ((int) numArray[1, index] > (int) num4)
              num4 = numArray[1, index];
          }
          num2 = (ushort) ((uint) num3 + (uint) num4);
        }
        return num2;
      }
      if (bilesen is bilesenOgretmen)
      {
        ushort id = (bilesen as bilesenOgretmen).id;
        for (int index = 0; index < this.tanimliDersler.Count; ++index)
        {
          bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index];
          foreach (bilesenTaban bilesenTaban in bilesenTanimliDers.ogretmenler)
          {
            if ((int) bilesenTaban.id == (int) id)
              num1 += bilesenTanimliDers.toplamSaat;
          }
        }
      }
      return num1;
    }

    public void temizle()
    {
      for (int index1 = 0; index1 < this.tanimliDersler.Count; ++index1)
      {
        bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index1];
        if (this.dersGetir(bilesenTanimliDers.ders.id) == null)
        {
          this.tanimliDersler.RemoveAt(index1--);
        }
        else
        {
          for (int index2 = 0; index2 < bilesenTanimliDers.sinifGruplar.Count; ++index2)
          {
            bilesenSinifGrup bilesenSinifGrup = bilesenTanimliDers.sinifGruplar[index2];
            if (this.sinifGetir(bilesenSinifGrup.sinif.id) == null)
              bilesenTanimliDers.sinifGruplar.RemoveAt(index2--);
            else if (this.sinifGetir(bilesenSinifGrup.sinif.id).grupGetir(bilesenSinifGrup.grup.id) == null)
              bilesenTanimliDers.sinifGruplar.RemoveAt(index2--);
          }
          if (bilesenTanimliDers.sinifGruplar.Count == 0)
          {
            this.tanimliDersler.RemoveAt(index1--);
          }
          else
          {
            for (int index2 = 0; index2 < bilesenTanimliDers.ogretmenler.Count; ++index2)
            {
              if (this.ogretmenGetir(bilesenTanimliDers.ogretmenler[index2].id) == null)
                bilesenTanimliDers.ogretmenler.RemoveAt(index2--);
            }
            if (bilesenTanimliDers.ogretmenler.Count == 0)
            {
              this.tanimliDersler.RemoveAt(index1--);
            }
            else
            {
              for (int index2 = 0; index2 < bilesenTanimliDers.derslikler.Count; ++index2)
              {
                if (this.derslikGetir(bilesenTanimliDers.derslikler[index2].id) == null)
                  bilesenTanimliDers.derslikler.RemoveAt(index2--);
              }
            }
          }
        }
      }
    }

    public int uygunDersSaatiSay(bilesenTaban bilesen)
    {
      int num = 0;
      bool[,] flagArray = araclar.diziBirlestir(this.kosullar, bilesen.kosul);
      for (int index1 = 0; index1 < flagArray.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < flagArray.GetLength(1); ++index2)
        {
          if (flagArray[index1, index2])
            ++num;
        }
      }
      return num;
    }

    public double yerlesimYuzde
    {
      get
      {
        double num = 0.0;
        foreach (bilesenTanimliDers bilesenTanimliDers in this.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
            ++num;
        }
        return 100.0 * num / (double) this.tanimliDersler.Count;
      }
    }

    public DersProgrami(bool ornekVeri = true)
    {
      if (!ornekVeri)
        return;
      this.idDerslikSon = this.idOgretmenSon = this.idSinifSon = this.idDersSon = this.idTanimliDersSon = (ushort) 0;
      this.haftalikGunSayisi = (byte) 5;
      this.mudurYrdBas = false;
      this.gunlukDersSaatiSayisi = (byte) 8;
      this.ogretimYili = string.Format("{0}-{1}", (object) DateTime.Now.Year, (object) (DateTime.Now.Year + 1));
      this.gunler = new string[5]
      {
        "Pazartesi",
        "Salı",
        "Çarşamba",
        "Perşembe",
        "Cuma"
      };
      this.derssaatleri = new string[8]
      {
        "08:30-09:10",
        "09:20-10:00",
        "10:10-10:50",
        "11:00-11:40",
        "11:50-12:30",
        "13:30-14:10",
        "14:20-15:00",
        "15:10-15:50"
      };
      this.kosullar = new bool[(int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi];
      for (int index1 = 0; index1 < (int) this.haftalikGunSayisi; ++index1)
      {
        for (int index2 = 0; index2 < (int) this.gunlukDersSaatiSayisi; ++index2)
          this.kosullar[index1, index2] = true;
      }
    }

    public void dagitimaHazirla()
    {
      ArrayList arrayList = new ArrayList();
      arrayList.AddRange((ICollection) this.ogretmenler);
      arrayList.AddRange((ICollection) this.derslikler);
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
        bilesenOgretmen.yerlesmemeSayisi = 0;
      for (int index1 = 0; index1 < this.tanimliDersler.Count; ++index1)
      {
        for (int index2 = 0; index2 < arrayList.Count; ++index2)
          (arrayList[index2] as bilesenTaban).yKosul = araclar.diziOlustur();
        foreach (bilesenSinif bilesenSinif in this.siniflar)
        {
          foreach (bilesenTaban bilesenTaban in bilesenSinif.gruplar)
            bilesenTaban.yKosul = araclar.diziOlustur();
        }
      }
    }

    public void kaydet(bool farkliKaydet = false)
    {
      bool flag = true;
      if (this.dosyaAdi == null || farkliKaydet)
      {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "(Ders Programı Dosyası)|*.dprg";
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
          this.dosyaAdi = saveFileDialog.FileName;
        else
          flag = false;
      }
      if (!(this.dosyaAdi != "") || !flag)
        return;
      XmlDocument xmlDocument = new XmlDocument();
      XmlNode xmlDeclaration = (XmlNode) xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", (string) null);
      xmlDocument.AppendChild(xmlDeclaration);
      XmlNode element1 = (XmlNode) xmlDocument.CreateElement(nameof (DersProgrami));
      xmlDocument.AppendChild(element1);
      XmlNode element2 = (XmlNode) xmlDocument.CreateElement("GenelAyarlar");
      element1.AppendChild(element2);
      XmlNode element3 = (XmlNode) xmlDocument.CreateElement("OkulAdi");
      element3.InnerText = this.okulAdi;
      element2.AppendChild(element3);
      XmlNode element4 = (XmlNode) xmlDocument.CreateElement("OkulMuduru");
      element4.InnerText = this.okulMuduru;
      element2.AppendChild(element4);
      XmlNode element5 = (XmlNode) xmlDocument.CreateElement("OkulMudurYrd");
      XmlAttribute attribute1 = xmlDocument.CreateAttribute("Bas");
      if (this.mudurYrdBas)
        attribute1.Value = "1";
      else
        attribute1.Value = "0";
      element5.Attributes.Append(attribute1);
      element5.InnerText = this.okulMudurYrd;
      element2.AppendChild(element5);
      XmlNode element6 = (XmlNode) xmlDocument.CreateElement("OgretimYili");
      element6.InnerText = this.ogretimYili;
      element2.AppendChild(element6);
      XmlNode element7 = (XmlNode) xmlDocument.CreateElement("GunlukDersSaatiSayisi");
      element7.InnerText = this.gunlukDersSaatiSayisi.ToString();
      element2.AppendChild(element7);
      XmlNode element8 = (XmlNode) xmlDocument.CreateElement("HaftalikGunSayisi");
      element8.InnerText = this.haftalikGunSayisi.ToString();
      element2.AppendChild(element8);
      XmlNode element9 = (XmlNode) xmlDocument.CreateElement("Gunler");
      element2.AppendChild(element9);
      for (int index = 0; index < this.gunler.Length; ++index)
      {
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Gun");
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Sira");
        attribute2.Value = index.ToString();
        element10.Attributes.Append(attribute2);
        element10.InnerText = this.gunler[index];
        element9.AppendChild(element10);
      }
      XmlNode element11 = (XmlNode) xmlDocument.CreateElement("Saatler");
      element2.AppendChild(element11);
      for (int index = 0; index < this.derssaatleri.Length; ++index)
      {
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Saat");
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Sira");
        attribute2.Value = index.ToString();
        element10.Attributes.Append(attribute2);
        element10.InnerText = this.derssaatleri[index];
        element11.AppendChild(element10);
      }
      XmlNode element12 = (XmlNode) xmlDocument.CreateElement("Kosullar");
      element12.InnerText = araclar.diziKodla(this.kosullar);
      element2.AppendChild(element12);
      XmlNode element13 = (XmlNode) xmlDocument.CreateElement("Dersler");
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("DerslerIdSon");
      attribute3.Value = this.idDersSon.ToString();
      element13.Attributes.Append(attribute3);
      element1.AppendChild(element13);
      for (int index = 0; index < this.dersler.Count; ++index)
      {
        bilesenDers bilesenDers = this.dersler[index];
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Ders");
        element13.AppendChild(element10);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
        attribute2.Value = bilesenDers.id.ToString();
        element10.Attributes.Append(attribute2);
        XmlNode element14 = (XmlNode) xmlDocument.CreateElement("Adi");
        element10.AppendChild(element14);
        element14.InnerText = bilesenDers.adi;
        XmlNode element15 = (XmlNode) xmlDocument.CreateElement("KisaAdi");
        element10.AppendChild(element15);
        element15.InnerText = bilesenDers.kisaAdi;
        XmlNode element16 = (XmlNode) xmlDocument.CreateElement("Kosul");
        element10.AppendChild(element16);
        element16.InnerText = araclar.diziKodla(bilesenDers.kosul);
      }
      XmlNode element17 = (XmlNode) xmlDocument.CreateElement("Ogretmenler");
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("OgretmenlerIdSon");
      attribute4.Value = this.idOgretmenSon.ToString();
      element17.Attributes.Append(attribute4);
      element1.AppendChild(element17);
      for (int index = 0; index < this.ogretmenler.Count; ++index)
      {
        bilesenOgretmen bilesenOgretmen = this.ogretmenler[index];
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Ogretmen");
        element17.AppendChild(element10);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
        attribute2.Value = bilesenOgretmen.id.ToString();
        element10.Attributes.Append(attribute2);
        XmlNode element14 = (XmlNode) xmlDocument.CreateElement("Adi");
        element10.AppendChild(element14);
        element14.InnerText = bilesenOgretmen.adi;
        XmlNode element15 = (XmlNode) xmlDocument.CreateElement("KisaAdi");
        element10.AppendChild(element15);
        element15.InnerText = bilesenOgretmen.kisaAdi;
        XmlNode element16 = (XmlNode) xmlDocument.CreateElement("Kosul");
        element10.AppendChild(element16);
        element16.InnerText = araclar.diziKodla(bilesenOgretmen.kosul);
      }
      XmlNode element18 = (XmlNode) xmlDocument.CreateElement("Derslikler");
      XmlAttribute attribute5 = xmlDocument.CreateAttribute("DersliklerIdSon");
      attribute5.Value = this.idDerslikSon.ToString();
      element18.Attributes.Append(attribute5);
      element1.AppendChild(element18);
      for (int index = 0; index < this.derslikler.Count; ++index)
      {
        bilesenDerslik bilesenDerslik = this.derslikler[index];
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Derslik");
        element18.AppendChild(element10);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
        attribute2.Value = bilesenDerslik.id.ToString();
        element10.Attributes.Append(attribute2);
        XmlNode element14 = (XmlNode) xmlDocument.CreateElement("Adi");
        element10.AppendChild(element14);
        element14.InnerText = bilesenDerslik.adi;
        XmlNode element15 = (XmlNode) xmlDocument.CreateElement("KisaAdi");
        element10.AppendChild(element15);
        element15.InnerText = bilesenDerslik.kisaAdi;
        XmlNode element16 = (XmlNode) xmlDocument.CreateElement("Kosul");
        element10.AppendChild(element16);
        element16.InnerText = araclar.diziKodla(bilesenDerslik.kosul);
      }
      XmlNode element19 = (XmlNode) xmlDocument.CreateElement("Siniflar");
      XmlAttribute attribute6 = xmlDocument.CreateAttribute("SiniflarIdSon");
      attribute6.Value = this.idSinifSon.ToString();
      element19.Attributes.Append(attribute6);
      element1.AppendChild(element19);
      for (int index1 = 0; index1 < this.siniflar.Count; ++index1)
      {
        bilesenSinif bilesenSinif = this.siniflar[index1];
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("Sinif");
        element19.AppendChild(element10);
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
        attribute2.Value = bilesenSinif.id.ToString();
        element10.Attributes.Append(attribute2);
        XmlAttribute attribute7 = xmlDocument.CreateAttribute("GruplarIdSon");
        attribute7.Value = bilesenSinif.grupIdSon.ToString();
        element10.Attributes.Append(attribute7);
        XmlNode element14 = (XmlNode) xmlDocument.CreateElement("Adi");
        element10.AppendChild(element14);
        element14.InnerText = bilesenSinif.adi;
        XmlNode element15 = (XmlNode) xmlDocument.CreateElement("KisaAdi");
        element10.AppendChild(element15);
        element15.InnerText = bilesenSinif.kisaAdi;
        XmlNode element16 = (XmlNode) xmlDocument.CreateElement("Kosul");
        element10.AppendChild(element16);
        element16.InnerText = araclar.diziKodla(bilesenSinif.kosul);
        XmlNode element20 = (XmlNode) xmlDocument.CreateElement("Gruplar");
        element10.AppendChild(element20);
        for (int index2 = 0; index2 < bilesenSinif.gruplar.Count; ++index2)
        {
          bilesenGrup bilesenGrup = bilesenSinif.gruplar[index2] as bilesenGrup;
          XmlNode element21 = (XmlNode) xmlDocument.CreateElement("Grup");
          element20.AppendChild(element21);
          XmlAttribute attribute8 = xmlDocument.CreateAttribute("Id");
          attribute8.Value = bilesenGrup.id.ToString();
          element21.Attributes.Append(attribute8);
          XmlNode element22 = (XmlNode) xmlDocument.CreateElement("Adi");
          element22.InnerText = bilesenGrup.adi;
          element21.AppendChild(element22);
          XmlNode element23 = (XmlNode) xmlDocument.CreateElement("KisaAdi");
          element23.InnerText = bilesenGrup.kisaAdi;
          element21.AppendChild(element23);
        }
      }
      XmlNode element24 = (XmlNode) xmlDocument.CreateElement("TanimliDersler");
      XmlAttribute attribute9 = xmlDocument.CreateAttribute("TanimliDersIdSon");
      attribute9.Value = this.idTanimliDersSon.ToString();
      element24.Attributes.Append(attribute9);
      element1.AppendChild(element24);
      for (int index1 = 0; index1 < this.tanimliDersler.Count; ++index1)
      {
        XmlNode element10 = (XmlNode) xmlDocument.CreateElement("TanimliDers");
        element24.AppendChild(element10);
        bilesenTanimliDers bilesenTanimliDers = this.tanimliDersler[index1];
        XmlAttribute attribute2 = xmlDocument.CreateAttribute("Id");
        attribute2.Value = bilesenTanimliDers.id.ToString();
        element10.Attributes.Append(attribute2);
        XmlAttribute attribute7 = xmlDocument.CreateAttribute("DersId");
        attribute7.Value = bilesenTanimliDers.ders.id.ToString();
        element10.Attributes.Append(attribute7);
        XmlAttribute attribute8 = xmlDocument.CreateAttribute("Yerlesim");
        attribute8.Value = bilesenTanimliDers.yerlesimStr;
        element10.Attributes.Append(attribute8);
        string str1 = "";
        for (int index2 = 0; index2 < bilesenTanimliDers.ogretmenler.Count; ++index2)
        {
          str1 += bilesenTanimliDers.ogretmenler[index2].id.ToString();
          if (index2 < bilesenTanimliDers.ogretmenler.Count - 1)
            str1 += ",";
        }
        XmlAttribute attribute10 = xmlDocument.CreateAttribute("Ogretmenler");
        attribute10.Value = str1;
        element10.Attributes.Append(attribute10);
        string str2 = "";
        for (int index2 = 0; index2 < bilesenTanimliDers.derslikler.Count; ++index2)
        {
          str2 += bilesenTanimliDers.derslikler[index2].id.ToString();
          if (index2 < bilesenTanimliDers.derslikler.Count - 1)
            str2 += ",";
        }
        XmlAttribute attribute11 = xmlDocument.CreateAttribute("Derslikler");
        attribute11.Value = str2;
        element10.Attributes.Append(attribute11);
        string str3 = "";
        for (int index2 = 0; index2 < bilesenTanimliDers.sinifGruplar.Count; ++index2)
        {
          string str4 = bilesenTanimliDers.sinifGruplar[index2].sinif.id.ToString();
          string str5 = bilesenTanimliDers.sinifGruplar[index2].grup.id.ToString();
          str3 = str3 + str4 + ":" + str5;
          if (index2 < bilesenTanimliDers.sinifGruplar.Count - 1)
            str3 += ",";
        }
        XmlAttribute attribute12 = xmlDocument.CreateAttribute("SinifGruplar");
        attribute12.Value = str3;
        element10.Attributes.Append(attribute12);
        if (bilesenTanimliDers.aktifYerlesim != null)
        {
          XmlAttribute attribute13 = xmlDocument.CreateAttribute("YerlesimStr");
          attribute13.Value = bilesenTanimliDers.aktifYerlesim.yerlesimStr;
          element10.Attributes.Append(attribute13);
        }
      }
      File.WriteAllBytes(this.dosyaAdi, araclar.Zip(xmlDocument.OuterXml));
    }

    public bool ac()
    {
      bool flag = true;
      OpenFileDialog openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = " (Ders Programı Dosyası)|*.dprg";
      if (openFileDialog.ShowDialog() == DialogResult.OK)
      {
        this.dosyaAdi = openFileDialog.FileName;
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
          byte[] gzip = File.ReadAllBytes(this.dosyaAdi);
          xmlDocument.LoadXml(araclar.unZip(gzip));
          this.okulAdi = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/OkulAdi").InnerText;
          this.okulMuduru = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMuduru").InnerText;
          this.okulMudurYrd = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMudurYrd").InnerText;
          this.mudurYrdBas = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/OkulMudurYrd").Attributes["Bas"].Value.ToString() == "1";
          this.ogretimYili = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/OgretimYili").InnerText;
          this.gunlukDersSaatiSayisi = Convert.ToByte(xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/GunlukDersSaatiSayisi").InnerText);
          this.haftalikGunSayisi = Convert.ToByte(xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/HaftalikGunSayisi").InnerText);
          this.haftalikGunSayisi = Convert.ToByte(xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/HaftalikGunSayisi").InnerText);
          XmlNodeList xmlNodeList1 = xmlDocument.SelectNodes("DersProgrami/GenelAyarlar/Gunler/Gun");
          if (xmlNodeList1.Count != (int) this.haftalikGunSayisi)
            throw new Exception();
          this.gunler = new string[(int) this.haftalikGunSayisi];
          for (int i = 0; i < (int) this.haftalikGunSayisi; ++i)
            this.gunler[i] = xmlNodeList1[i].InnerText;
          XmlNodeList xmlNodeList2 = xmlDocument.SelectNodes("DersProgrami/GenelAyarlar/Saatler/Saat");
          if (xmlNodeList2.Count != (int) this.gunlukDersSaatiSayisi)
            throw new Exception();
          this.derssaatleri = new string[(int) this.gunlukDersSaatiSayisi];
          for (int i = 0; i < (int) this.gunlukDersSaatiSayisi; ++i)
            this.derssaatleri[i] = xmlNodeList2[i].InnerText;
          string innerText1 = xmlDocument.SelectSingleNode("DersProgrami/GenelAyarlar/Kosullar").InnerText;
          if (innerText1.Length != (int) this.haftalikGunSayisi * (int) this.gunlukDersSaatiSayisi)
            throw new Exception();
          this.kosullar = araclar.diziKodCoz(innerText1, (int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi);
          this.idDersSon = Convert.ToUInt16(xmlDocument.SelectSingleNode("DersProgrami/Dersler").Attributes["DerslerIdSon"].Value);
          XmlNodeList xmlNodeList3 = xmlDocument.SelectNodes("DersProgrami/Dersler/Ders");
          for (int i = 0; i < xmlNodeList3.Count; ++i)
          {
            XmlNode xmlNode = xmlNodeList3[i];
            ushort uint16 = Convert.ToUInt16(xmlNode.Attributes["Id"].Value);
            string innerText2 = xmlNode.SelectSingleNode("Adi").InnerText;
            string innerText3 = xmlNode.SelectSingleNode("KisaAdi").InnerText;
            bool[,] _kosul = araclar.diziKodCoz(xmlNode.SelectSingleNode("Kosul").InnerText, (int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi);
            this.dersler.Add(new bilesenDers(uint16, _kosul, innerText2, innerText3));
          }
          this.idOgretmenSon = Convert.ToUInt16(xmlDocument.SelectSingleNode("DersProgrami/Ogretmenler").Attributes["OgretmenlerIdSon"].Value);
          XmlNodeList xmlNodeList4 = xmlDocument.SelectNodes("DersProgrami/Ogretmenler/Ogretmen");
          for (int i = 0; i < xmlNodeList4.Count; ++i)
          {
            XmlNode xmlNode = xmlNodeList4[i];
            ushort uint16 = Convert.ToUInt16(xmlNode.Attributes["Id"].Value);
            string innerText2 = xmlNode.SelectSingleNode("Adi").InnerText;
            string innerText3 = xmlNode.SelectSingleNode("KisaAdi").InnerText;
            bool[,] _kosul = araclar.diziKodCoz(xmlNode.SelectSingleNode("Kosul").InnerText, (int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi);
            this.ogretmenler.Add(new bilesenOgretmen(uint16, _kosul, innerText2, innerText3));
          }
          this.idDerslikSon = Convert.ToUInt16(xmlDocument.SelectSingleNode("DersProgrami/Derslikler").Attributes["DersliklerIdSon"].Value);
          XmlNodeList xmlNodeList5 = xmlDocument.SelectNodes("DersProgrami/Derslikler/Derslik");
          for (int i = 0; i < xmlNodeList5.Count; ++i)
          {
            XmlNode xmlNode = xmlNodeList5[i];
            ushort uint16 = Convert.ToUInt16(xmlNode.Attributes["Id"].Value);
            string innerText2 = xmlNode.SelectSingleNode("Adi").InnerText;
            string innerText3 = xmlNode.SelectSingleNode("KisaAdi").InnerText;
            bool[,] _kosul = araclar.diziKodCoz(xmlNode.SelectSingleNode("Kosul").InnerText, (int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi);
            this.derslikler.Add(new bilesenDerslik(uint16, _kosul, innerText2, innerText3));
          }
          this.idSinifSon = Convert.ToUInt16(xmlDocument.SelectSingleNode("DersProgrami/Siniflar").Attributes["SiniflarIdSon"].Value);
          XmlNodeList xmlNodeList6 = xmlDocument.SelectNodes("DersProgrami/Siniflar/Sinif");
          for (int i1 = 0; i1 < xmlNodeList6.Count; ++i1)
          {
            XmlNode xmlNode1 = xmlNodeList6[i1];
            ushort uint16_1 = Convert.ToUInt16(xmlNode1.Attributes["Id"].Value);
            ushort uint16_2 = Convert.ToUInt16(xmlNode1.Attributes["GruplarIdSon"].Value);
            string innerText2 = xmlNode1.SelectSingleNode("Adi").InnerText;
            string innerText3 = xmlNode1.SelectSingleNode("KisaAdi").InnerText;
            bool[,] _kosul = araclar.diziKodCoz(xmlNode1.SelectSingleNode("Kosul").InnerText, (int) this.haftalikGunSayisi, (int) this.gunlukDersSaatiSayisi);
            ArrayList _gruplar = new ArrayList();
            XmlNodeList xmlNodeList7 = xmlNode1.SelectNodes("Gruplar/Grup");
            for (int i2 = 0; i2 < xmlNodeList7.Count; ++i2)
            {
              XmlNode xmlNode2 = xmlNodeList7[i2];
              bilesenGrup bilesenGrup = new bilesenGrup(Convert.ToUInt16(xmlNode2.Attributes["Id"].Value), xmlNode2.SelectSingleNode("Adi").InnerText, xmlNode2.SelectSingleNode("KisaAdi").InnerText);
              _gruplar.Add((object) bilesenGrup);
            }
            this.siniflar.Add(new bilesenSinif(uint16_1, _kosul, innerText2, innerText3, _gruplar, uint16_2));
          }
          this.idTanimliDersSon = Convert.ToUInt16(xmlDocument.SelectSingleNode("DersProgrami/TanimliDersler").Attributes["TanimliDersIdSon"].Value);
          this.tanimliDersler = new List<bilesenTanimliDers>();
          XmlNodeList xmlNodeList8 = xmlDocument.SelectNodes("DersProgrami/TanimliDersler/TanimliDers");
          for (int i = 0; i < xmlNodeList8.Count; ++i)
          {
            XmlNode xmlNode = xmlNodeList8[i];
            ushort uint16 = Convert.ToUInt16(xmlNode.Attributes["Id"].Value);
            bilesenDers _ders = this.dersGetir(Convert.ToUInt16(xmlNode.Attributes["DersId"].Value));
            string _yerlesimStr = xmlNode.Attributes["Yerlesim"].Value;
            string str1 = xmlNode.Attributes["Ogretmenler"].Value;
            List<bilesenOgretmen> _ogretmenler = new List<bilesenOgretmen>();
            string str2 = str1;
            char[] chArray1 = new char[1]{ ',' };
            foreach (string str3 in str2.Split(chArray1))
            {
              bilesenOgretmen bilesenOgretmen = this.ogretmenGetir(Convert.ToUInt16(str3));
              _ogretmenler.Add(bilesenOgretmen);
            }
            List<bilesenDerslik> _derslikler = new List<bilesenDerslik>();
            string[] strArray1 = xmlNode.Attributes["Derslikler"].Value.Split(',');
            for (int index = 0; index < strArray1.Length; ++index)
            {
              if (!(strArray1[index] == ""))
              {
                bilesenDerslik bilesenDerslik = this.derslikGetir(Convert.ToUInt16(strArray1[index]));
                _derslikler.Add(bilesenDerslik);
              }
            }
            string str4 = xmlNode.Attributes["SinifGruplar"].Value;
            List<bilesenSinifGrup> _sinifGruplar = new List<bilesenSinifGrup>();
            string str5 = str4;
            char[] chArray2 = new char[1]{ ',' };
            foreach (string str3 in str5.Split(chArray2))
            {
              char[] chArray3 = new char[1]{ ':' };
              string[] strArray2 = str3.Split(chArray3);
              bilesenSinifGrup bilesenSinifGrup = new bilesenSinifGrup(this.sinifGetir(Convert.ToUInt16(strArray2[0])), Convert.ToUInt16(strArray2[1]));
              _sinifGruplar.Add(bilesenSinifGrup);
            }
            string str6 = "";
            if (xmlNode.Attributes["YerlesimStr"] != null)
              str6 = xmlNode.Attributes["YerlesimStr"].Value;
            bilesenTanimliDers bilesenTanimliDers = new bilesenTanimliDers(uint16, _ders, _sinifGruplar, _ogretmenler, _derslikler, _yerlesimStr, this);
            this.tanimliDersler.Add(bilesenTanimliDers);
            bilesenTanimliDers.baslangicYerlesimi = str6;
          }
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show("Dosya okumada hata oluştu!!\n" + ex.Message);
          flag = false;
        }
      }
      else
        flag = false;
      return flag;
    }
  }
}
