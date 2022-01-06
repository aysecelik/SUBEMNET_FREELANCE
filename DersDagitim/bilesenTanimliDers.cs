
using System;
using System.Collections.Generic;
using System.Linq;

namespace DersDagitim
{
  public class bilesenTanimliDers
  {
    public ushort id;
    public DersProgrami dersProgrami;
    public bilesenDers ders;
    public List<bilesenSinifGrup> sinifGruplar;
    public List<bilesenOgretmen> ogretmenler;
    public List<bilesenDerslik> derslikler;
    public string baslangicYerlesimi;
    public string yerlesimStr;
    public bilesenNode[] nodes;
    public bool[,] kosul;
    public List<bilesenTanimliDers.yerlesimOlasilik> olasiliklar = new List<bilesenTanimliDers.yerlesimOlasilik>();
    public bilesenTanimliDers.yerlesimOlasilik aktifYerlesim;
    public bilesenTanimliDers.yerlesimOlasilik eskiYerlesim;
    public List<bilesenTanimliDers> iliskiListesi;
    public int denemeSayac;

    public string aciklama
    {
      get
      {
        string str1 = this.ders.adi + "/";
        foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
          str1 = str1 + bilesenOgretmen.kisaAdi + " ";
        string str2 = str1 + "/";
        foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
          str2 = str2 + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
        string str3 = str2 + "/";
        foreach (bilesenDerslik bilesenDerslik in this.derslikler)
          str3 = str3 + bilesenDerslik.kisaAdi + " ";
        return str3;
      }
    }

    public static ushort[] yerlesimHesapla(string yerlesim)
    {
      string[] strArray = yerlesim.Split('+');
      ushort[] numArray = new ushort[strArray.Length];
      for (int index = 0; index < strArray.Length; ++index)
        numArray[index] = Convert.ToUInt16(strArray[index]);
      return numArray;
    }

    public ushort[] yerlesim => bilesenTanimliDers.yerlesimHesapla(this.yerlesimStr);

    public ushort toplamSaat
    {
      get
      {
        ushort num1 = 0;
        foreach (ushort num2 in bilesenTanimliDers.yerlesimHesapla(this.yerlesimStr))
          num1 += num2;
        return num1;
      }
    }

    public int boslukSay
    {
      get
      {
        int num = 0;
        for (int index1 = 0; index1 < this.kosul.GetLength(0); ++index1)
        {
          for (int index2 = 0; index2 < this.kosul.GetLength(1); ++index2)
          {
            if (this.kosul[index1, index2])
              ++num;
          }
        }
        return num;
      }
    }

    public bool yerlesirMi()
    {
      bool[,] dizi1 = araclar.diziBirlestir(araclar.diziOlustur(), this.kosul);
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
      {
        dizi1 = araclar.diziBirlestir(dizi1, bilesenOgretmen.yKosul);
        dizi1 = araclar.diziBirlestir(dizi1, bilesenOgretmen.kosul);
      }
      foreach (bilesenDerslik bilesenDerslik in this.derslikler)
      {
        dizi1 = araclar.diziBirlestir(dizi1, bilesenDerslik.yKosul);
        dizi1 = araclar.diziBirlestir(dizi1, bilesenDerslik.kosul);
      }
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
      {
        dizi1 = araclar.diziBirlestir(dizi1, bilesenSinifGrup.sinif.kosul);
        if (bilesenSinifGrup.grup.id != (ushort) 0)
        {
          dizi1 = araclar.diziBirlestir(dizi1, bilesenSinifGrup.sinif.grupGetir((ushort) 0).yKosul);
          dizi1 = araclar.diziBirlestir(dizi1, bilesenSinifGrup.grup.yKosul);
        }
        else
        {
          foreach (bilesenGrup bilesenGrup in bilesenSinifGrup.sinif.gruplar)
            dizi1 = araclar.diziBirlestir(dizi1, bilesenGrup.yKosul);
        }
      }
      bool flag = false;
      foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in this.olasiliklar)
      {
        yerlesimOlasilik.olumlu = true;
        for (int index1 = 0; index1 < yerlesimOlasilik.yerlesimler.Count && yerlesimOlasilik.olumlu; ++index1)
        {
          for (int index2 = 0; index2 < (int) this.nodes[index1].tSaat && yerlesimOlasilik.olumlu; ++index2)
          {
            if (!dizi1[(int) yerlesimOlasilik.yerlesimler[index1].gun, (int) yerlesimOlasilik.yerlesimler[index1].saat + index2])
              yerlesimOlasilik.olumlu = false;
          }
        }
        flag |= yerlesimOlasilik.olumlu;
      }
      return flag;
    }

    public bool olasilikSina(int olasilikno, bool id = false)
    {
      if (id)
      {
        for (int index = 0; index < this.olasiliklar.Count; ++index)
        {
          if (this.olasiliklar[index].id == olasilikno)
          {
            olasilikno = index;
            break;
          }
        }
      }
      return this.olasiliklar[olasilikno].olumlu;
    }

    public bool olasilikSina(bilesenTanimliDers.yerlesimOlasilik ol)
    {
      bool flag = true;
      bool[,] flagArray = araclar.diziOlustur();
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
        flagArray = araclar.diziBirlestir(flagArray, bilesenOgretmen.yKosul);
      foreach (bilesenDerslik bilesenDerslik in this.derslikler)
        flagArray = araclar.diziBirlestir(flagArray, bilesenDerslik.yKosul);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
      {
        if (bilesenSinifGrup.grup.id != (ushort) 0)
        {
          flagArray = araclar.diziBirlestir(flagArray, bilesenSinifGrup.sinif.grupGetir((ushort) 0).yKosul);
          flagArray = araclar.diziBirlestir(flagArray, bilesenSinifGrup.grup.yKosul);
        }
        else
        {
          foreach (bilesenGrup bilesenGrup in bilesenSinifGrup.sinif.gruplar)
            flagArray = araclar.diziBirlestir(flagArray, bilesenGrup.yKosul);
        }
      }
      for (int index = 0; index < this.nodes.Length; ++index)
      {
        flag &= this.nodes[index].nodeYerlesirmi(flagArray, ol.yerlesimler[index].gun, ol.yerlesimler[index].saat);
        if (!flag)
          break;
      }
      ol.aktif = flag;
      return flag;
    }

    public bool eskiyeYerles()
    {
      this.yerles(this.eskiYerlesim);
      return true;
    }

    public bool rastgeleYerles()
    {
      Random random = new Random();
      bool flag = false;
      List<bilesenTanimliDers.yerlesimOlasilik> list = this.olasiliklar.ToList<bilesenTanimliDers.yerlesimOlasilik>();
      while (list.Count > 0)
      {
        int index = random.Next(list.Count);
        if (this.olasilikSina(list[index]))
        {
          this.yerles(list[index]);
          flag = true;
          break;
        }
        list.RemoveAt(index);
      }
      return flag;
    }

    public double verimHesapla(bool[,] yerlesimKosul, bilesenTanimliDers yerlesecekDers)
    {
      if (araclar.catalmi(this, yerlesecekDers))
        return 100.0;
      List<bilesenTanimliDers.yerlesimOlasilik> yerlesimOlasilikList = new List<bilesenTanimliDers.yerlesimOlasilik>();
      if (!this.yerlesirMi())
        return 0.0;
      foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in this.olasiliklar)
      {
        if (yerlesimOlasilik.olumlu)
          yerlesimOlasilikList.Add(yerlesimOlasilik);
      }
      int count = yerlesimOlasilikList.Count;
      int num = 0;
      if (count == 0)
        return 0.0;
      foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in yerlesimOlasilikList)
      {
        bool flag = true;
        for (int index1 = 0; index1 < this.nodes.Length; ++index1)
        {
          for (int index2 = 0; index2 < (int) this.nodes[index1].tSaat; ++index2)
          {
            if (!yerlesimKosul[(int) yerlesimOlasilik.yerlesimler[index1].gun, (int) yerlesimOlasilik.yerlesimler[index1].saat + index2])
            {
              flag = false;
              ++num;
              break;
            }
            if (!flag)
              break;
          }
          if (!flag)
            break;
        }
      }
      return 100.0 * (double) (count - num) / (double) count;
    }

    public bilesenTanimliDers.yerlesimOlasilik[] yerlesilebilirOlasiliklar()
    {
      List<bilesenTanimliDers.yerlesimOlasilik> yerlesimOlasilikList = new List<bilesenTanimliDers.yerlesimOlasilik>();
      foreach (bilesenTanimliDers.yerlesimOlasilik ol in this.olasiliklar)
      {
        if (this.olasilikSina(ol))
          yerlesimOlasilikList.Add(ol);
      }
      return yerlesimOlasilikList.ToArray();
    }

    public bool enIyiyeYerles()
    {
      bool flag = false;
      double[] numArray = new double[this.olasiliklar.Count];
      for (int index = 0; index < this.olasiliklar.Count; ++index)
      {
        if (this.olasiliklar[index].olumlu)
        {
          double num1 = 0.0;
          double num2 = 0.0;
          foreach (bilesenTanimliDers d2 in this.iliskiListesi)
          {
            if (d2.aktifYerlesim == null || araclar.catalmi(this, d2))
            {
              num1 += d2.verimHesapla(this.olasiliklar[index].tablo, this);
              ++num2;
            }
          }
          numArray[index] = num1 / num2;
        }
        else
          numArray[index] = -1.0;
      }
      double num = -1.0;
      int olasilikno = -1;
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] > num)
        {
          num = numArray[index];
          olasilikno = index;
        }
      }
      if (num > -1.0 && olasilikno > -1)
      {
        this.yerles(olasilikno);
        flag = true;
      }
      return flag;
    }

    private void aktifOlasiligiIlkeGetir()
    {
      this.olasiliklar.Remove(this.aktifYerlesim);
      this.olasiliklar.Insert(0, this.aktifYerlesim);
    }

    public void yerles(int olasilikno, bool id = false)
    {
      if (id)
      {
        for (int index = 0; index < this.olasiliklar.Count; ++index)
        {
          if (this.olasiliklar[index].id == olasilikno)
          {
            olasilikno = index;
            break;
          }
        }
      }
      for (int index = 0; index < this.nodes.Length; ++index)
      {
        this.nodes[index].yerlesimGun = this.olasiliklar[olasilikno].yerlesimler[index].gun;
        this.nodes[index].yerlesimSaat = this.olasiliklar[olasilikno].yerlesimler[index].saat;
      }
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
        bilesenOgretmen.yKosul = araclar.diziBirlestir(bilesenOgretmen.yKosul, this.olasiliklar[olasilikno].tablo);
      foreach (bilesenDerslik bilesenDerslik in this.derslikler)
        bilesenDerslik.yKosul = araclar.diziBirlestir(bilesenDerslik.yKosul, this.olasiliklar[olasilikno].tablo);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
        bilesenSinifGrup.grup.yKosul = araclar.diziBirlestir(this.olasiliklar[olasilikno].tablo, bilesenSinifGrup.grup.yKosul);
      this.aktifYerlesim = this.olasiliklar[olasilikno];
      this.aktifOlasiligiIlkeGetir();
    }

    public void yerles(bilesenTanimliDers.yerlesimOlasilik ol)
    {
      if (!this.olasiliklar.Contains(ol))
        return;
      for (int index = 0; index < this.nodes.Length; ++index)
      {
        this.nodes[index].yerlesimGun = ol.yerlesimler[index].gun;
        this.nodes[index].yerlesimSaat = ol.yerlesimler[index].saat;
      }
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
        bilesenOgretmen.yKosul = araclar.diziBirlestir(bilesenOgretmen.yKosul, ol.tablo);
      foreach (bilesenDerslik bilesenDerslik in this.derslikler)
        bilesenDerslik.yKosul = araclar.diziBirlestir(bilesenDerslik.yKosul, ol.tablo);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
        bilesenSinifGrup.grup.yKosul = araclar.diziBirlestir(ol.tablo, bilesenSinifGrup.grup.yKosul);
      this.aktifYerlesim = ol;
      this.aktifOlasiligiIlkeGetir();
    }

    public void kaldir()
    {
      this.eskiYerlesim = this.aktifYerlesim;
      bool[,] flagArray = araclar.diziOlustur(false);
      for (int index1 = 0; index1 < this.nodes.Length; ++index1)
      {
        for (int index2 = 0; index2 < (int) this.nodes[index1].tSaat; ++index2)
          flagArray[(int) this.nodes[index1].yerlesimGun, (int) this.nodes[index1].yerlesimSaat + index2] = true;
      }
      foreach (bilesenOgretmen bilesenOgretmen in this.ogretmenler)
        bilesenOgretmen.yKosul = araclar.diziEkle(bilesenOgretmen.yKosul, flagArray);
      foreach (bilesenDerslik bilesenDerslik in this.derslikler)
        bilesenDerslik.yKosul = araclar.diziEkle(bilesenDerslik.yKosul, flagArray);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
        bilesenSinifGrup.grup.yKosul = araclar.diziEkle(flagArray, bilesenSinifGrup.grup.yKosul);
      this.aktifYerlesim = (bilesenTanimliDers.yerlesimOlasilik) null;
    }

    private void olasilikTara(
      bilesenTanimliDers.yerlesimOlasilik _yerlesimOlasilik = null,
      ushort _nodeSira = 0,
      ushort _olasilikSira = 0)
    {
      if (_yerlesimOlasilik == null)
        _yerlesimOlasilik = new bilesenTanimliDers.yerlesimOlasilik();
      bilesenTanimliDers.yerlesimOlasilik _yerlesimOlasilik1 = _yerlesimOlasilik.kopya();
      if ((int) _nodeSira >= this.nodes.Length)
        return;
      bilesenNode node = this.nodes[(int) _nodeSira];
      if ((int) _olasilikSira >= node.yerlesimYerleri.Length)
        return;
      bool flag = true;
      bilesenNode.yerlesimYeri yerlesimYeri = node.yerlesimYerleri[(int) _olasilikSira];
      for (int index = 0; index < (int) _nodeSira; ++index)
      {
        if ((int) this.nodes[index].tSaat == (int) node.tSaat && (int) _yerlesimOlasilik1.yerlesimler[index].gun > (int) yerlesimYeri.gun)
          flag = false;
        if ((int) _yerlesimOlasilik1.yerlesimler[index].gun == (int) yerlesimYeri.gun)
          flag = false;
      }
      if (flag)
      {
        _yerlesimOlasilik1.yerlesimler.Add(new bilesenTanimliDers.nodeOlasilik()
        {
          gun = yerlesimYeri.gun,
          saat = yerlesimYeri.saat
        });
        if (_yerlesimOlasilik1.yerlesimler.Count == this.nodes.Length)
          this.olasiliklar.Add(_yerlesimOlasilik1);
        else
          this.olasilikTara(_yerlesimOlasilik1, Convert.ToUInt16((int) _nodeSira + 1));
      }
      this.olasilikTara(_yerlesimOlasilik, _nodeSira, Convert.ToUInt16((int) _olasilikSira + 1));
    }

    private void olasiliklariOlustur()
    {
      this.olasiliklar = new List<bilesenTanimliDers.yerlesimOlasilik>();
      this.olasilikTara();
      foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in this.olasiliklar)
      {
        bool[,] flagArray = araclar.diziOlustur();
        for (int index1 = 0; index1 < this.nodes.Length; ++index1)
        {
          for (int index2 = 0; index2 < (int) this.nodes[index1].tSaat; ++index2)
            flagArray[(int) yerlesimOlasilik.yerlesimler[index1].gun, (int) yerlesimOlasilik.yerlesimler[index1].saat + index2] = false;
        }
        yerlesimOlasilik.tablo = flagArray;
      }
    }

    private void kosullariTopla()
    {
      this.kosul = araclar.diziKopyala(this.dersProgrami.kosullar);
      this.kosul = araclar.diziBirlestir(this.kosul, this.ders.kosul);
      foreach (bilesenSinifGrup bilesenSinifGrup in this.sinifGruplar)
        this.kosul = araclar.diziBirlestir(this.kosul, bilesenSinifGrup.sinif.kosul);
      foreach (bilesenTaban bilesenTaban in this.ogretmenler)
        this.kosul = araclar.diziBirlestir(this.kosul, bilesenTaban.kosul);
      foreach (bilesenTaban bilesenTaban in this.derslikler)
        this.kosul = araclar.diziBirlestir(this.kosul, bilesenTaban.kosul);
    }

    private void nodelariOlustur()
    {
      ushort[] yerlesim = this.yerlesim;
      this.nodes = new bilesenNode[yerlesim.Length];
      for (ushort _id = 0; (int) _id < yerlesim.Length; ++_id)
        this.nodes[(int) _id] = new bilesenNode(_id, this, yerlesim[(int) _id]);
    }

    public void cikarilacakVerimliDers(List<bilesenTanimliDers> cDers)
    {
      foreach (bilesenTanimliDers bilesenTanimliDers in this.iliskiListesi)
      {
        if (bilesenTanimliDers.aktifYerlesim != null)
        {
          bilesenTanimliDers.yerlesimOlasilik aktifYerlesim = bilesenTanimliDers.aktifYerlesim;
          foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in this.olasiliklar)
          {
            if (araclar.diziKesisiyormu(yerlesimOlasilik.tablo, aktifYerlesim.tablo))
            {
              cDers.Add(bilesenTanimliDers);
              break;
            }
          }
        }
      }
    }

    public void iliskileriOlustur()
    {
      for (int index1 = 0; index1 < this.iliskiListesi.Count; ++index1)
      {
        for (int index2 = index1; index2 < this.iliskiListesi.Count; ++index2)
        {
          if (this.iliskiListesi[index2].olasiliklar.Count > this.iliskiListesi[index1].olasiliklar.Count)
          {
            bilesenTanimliDers bilesenTanimliDers = this.iliskiListesi[index1];
            this.iliskiListesi[index1] = this.iliskiListesi[index2];
            this.iliskiListesi[index2] = bilesenTanimliDers;
          }
        }
      }
    }

    public void yerlesimeHazirla()
    {
      this.kosullariTopla();
      this.nodelariOlustur();
      foreach (bilesenNode node in this.nodes)
        node.hesapla(this.kosul);
      this.olasiliklariOlustur();
      int num = 0;
      foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in this.olasiliklar)
        yerlesimOlasilik.id = num++;
      this.iliskiListesi = new List<bilesenTanimliDers>();
      foreach (bilesenTanimliDers bilesenTanimliDers in this.dersProgrami.tanimliDersler)
      {
        bool flag = false;
        if (bilesenTanimliDers != this)
        {
          if (!this.iliskiListesi.Contains(bilesenTanimliDers))
          {
            foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
            {
              if (this.derslikler.Contains(bilesenDerslik))
                flag = true;
            }
            foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
            {
              if (this.ogretmenler.Contains(bilesenOgretmen))
                flag = true;
            }
            foreach (bilesenSinifGrup bilesenSinifGrup1 in bilesenTanimliDers.sinifGruplar)
            {
              foreach (bilesenSinifGrup bilesenSinifGrup2 in this.sinifGruplar)
              {
                if (bilesenSinifGrup1.sinif == bilesenSinifGrup2.sinif)
                  flag = true;
              }
            }
            if (flag)
              this.iliskiListesi.Add(bilesenTanimliDers);
          }
          else
            continue;
        }
        this.aktifYerlesim = (bilesenTanimliDers.yerlesimOlasilik) null;
        this.denemeSayac = 0;
      }
    }

    public bilesenTanimliDers(
      ushort _id,
      bilesenDers _ders,
      List<bilesenSinifGrup> _sinifGruplar,
      List<bilesenOgretmen> _ogretmenler,
      List<bilesenDerslik> _derslikler,
      string _yerlesimStr,
      DersProgrami _dpr)
    {
      this.dersProgrami = _dpr;
      this.id = _id;
      this.ders = _ders;
      this.sinifGruplar = _sinifGruplar;
      this.ogretmenler = _ogretmenler;
      this.derslikler = _derslikler;
      this.yerlesimStr = _yerlesimStr;
    }

    public int denemeXsaat
    {
      get
      {
        int num = 0;
        foreach (bilesenTanimliDers bilesenTanimliDers in this.iliskiListesi)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
            ++num;
        }
        return this.denemeSayac * (int) this.toplamSaat + num;
      }
    }

    public struct nodeOlasilik
    {
      public ushort gun;
      public ushort saat;
    }

    public class yerlesimOlasilik
    {
      public int id;
      public bool aktif;
      public bool olumlu = true;
      public List<bilesenTanimliDers.nodeOlasilik> yerlesimler = new List<bilesenTanimliDers.nodeOlasilik>();
      public bool[,] tablo;

      public string yerlesimStr
      {
        get
        {
          string str1 = "ABCDEFGHIJKLMNOPRSTUVYZXWQ0123456789*+&$/-";
          string str2 = "";
          for (int index = 0; index < this.yerlesimler.Count; ++index)
            str2 += string.Format("{0}{1}{2}", (object) str1[index], (object) str1[(int) this.yerlesimler[index].gun], (object) str1[(int) this.yerlesimler[index].saat]);
          return str2;
        }
      }

      public bilesenTanimliDers.yerlesimOlasilik kopya()
      {
        bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik = new bilesenTanimliDers.yerlesimOlasilik();
        for (int index = 0; index < this.yerlesimler.Count; ++index)
          yerlesimOlasilik.yerlesimler.Add(this.yerlesimler[index]);
        return yerlesimOlasilik;
      }
    }
  }
}
