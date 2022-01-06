using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace DersDagitim
{
  internal class iyilestirmeliTaramaYap
  {
    private List<bilesenTanimliDers> yerlesenler = new List<bilesenTanimliDers>();
    private List<bilesenTanimliDers> yerlesmeyenler = new List<bilesenTanimliDers>();
    private List<int[]> enIyiYerlesimListesi = new List<int[]>();
    private DersProgrami dersprogrami;
    public object kilitle = new object();
    private Thread threadYerlestir;
    private Random rnd;
    private int toplamDersSaati;
    private int _enYuksekYuzde;
    private Stopwatch kronometre = new Stopwatch();
    public bool bitti;
    public ulong sayac = 1;

    public iyilestirmeliTaramaYap(DersProgrami dersprogrami, bool iyilestir = false)
    {
      this.rnd = new Random();
      this.dersprogrami = dersprogrami;
      dersprogrami.dagitimaHazirla();
      foreach (bilesenTanimliDers bilesenTanimliDers in dersprogrami.tanimliDersler)
        this.toplamDersSaati += (int) bilesenTanimliDers.toplamSaat;
      if (iyilestir)
      {
        foreach (bilesenTanimliDers bilesenTanimliDers in dersprogrami.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            if (bilesenTanimliDers.yerlesirMi())
            {
              if (bilesenTanimliDers.olasilikSina(bilesenTanimliDers.aktifYerlesim))
              {
                bilesenTanimliDers.yerles(bilesenTanimliDers.aktifYerlesim);
                this.yerlesenler.Add(bilesenTanimliDers);
              }
              else
                this.yerlesmeyenler.Add(bilesenTanimliDers);
            }
          }
          else
            this.yerlesmeyenler.Add(bilesenTanimliDers);
        }
      }
      else
      {
        List<bilesenTanimliDers> list1 = dersprogrami.tanimliDersler.ToList<bilesenTanimliDers>();
        for (int index1 = 0; index1 < list1.Count; ++index1)
        {
          for (int index2 = index1; index2 < list1.Count; ++index2)
          {
            if ((int) list1[index2].toplamSaat > (int) list1[index1].toplamSaat)
            {
              bilesenTanimliDers bilesenTanimliDers = list1[index1];
              list1[index1] = list1[index2];
              list1[index2] = bilesenTanimliDers;
            }
          }
        }
        this.yerlesmeyenler = list1.ToList<bilesenTanimliDers>();
        List<bilesenTanimliDers> list2 = this.yerlesmeyenler.ToList<bilesenTanimliDers>();
        int count = list2.Count;
        while (list2.Count > 0)
        {
          int index = 0;
          if (list2[index].rastgeleYerles())
          {
            this.yerlesenler.Add(list2[index]);
            this.yerlesmeyenler.Remove(list2[index]);
          }
          list2.RemoveAt(index);
        }
      }
      this.threadYerlestir = new Thread(new ThreadStart(this.baslat));
      this.threadYerlestir.Start();
    }

    public int enYuksekYuzde => this._enYuksekYuzde;

    public int yerlesenYuzde => this.yerlesenler.Count * 100 / (this.yerlesenler.Count + this.yerlesmeyenler.Count);

    public int yerlesmeyenSayisi => this.yerlesmeyenler.Count;

    public string gecenSure => string.Format("{0:00}:{1:00}:{2:00}:{3:00}", (object) this.kronometre.Elapsed.Days, (object) this.kronometre.Elapsed.Hours, (object) this.kronometre.Elapsed.Minutes, (object) this.kronometre.Elapsed.Seconds);

    public DataTable dtYerlesmeyenler()
    {
      DataTable dataTable = new DataTable();
      dataTable.Columns.Add("dersadi", typeof (string));
      dataTable.Columns.Add("sinifgrup", typeof (string));
      dataTable.Columns.Add("ogretmenler", typeof (string));
      dataTable.Columns.Add("derslikler", typeof (string));
      dataTable.Rows.Clear();
      List<bilesenTanimliDers> list = this.yerlesmeyenler.ToList<bilesenTanimliDers>();
      for (int index1 = 0; index1 < list.Count; ++index1)
      {
        for (int index2 = index1; index2 < list.Count; ++index2)
        {
          if (list[index2].denemeXsaat > list[index1].denemeXsaat)
          {
            bilesenTanimliDers bilesenTanimliDers = list[index1];
            list[index1] = list[index2];
            list[index2] = bilesenTanimliDers;
          }
        }
      }
      foreach (bilesenTanimliDers bilesenTanimliDers in list)
      {
        string adi = bilesenTanimliDers.ders.adi;
        string str1 = "";
        string str2 = "";
        string str3 = "";
        foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
          str1 = str1 + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
        if (bilesenTanimliDers.ogretmenler.Count == 1)
        {
          str2 = bilesenTanimliDers.ogretmenler[0].adi;
        }
        else
        {
          foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
            str2 = str2 + bilesenOgretmen.kisaAdi + " ";
        }
        foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
          str3 = str3 + bilesenDerslik.kisaAdi + " ";
        dataTable.Rows.Add((object) adi, (object) str1, (object) str2, (object) str3);
      }
      return dataTable;
    }

    public string[] enZorOnOgretmen
    {
      get
      {
        List<bilesenOgretmen> bilesenOgretmenList = new List<bilesenOgretmen>();
        foreach (bilesenOgretmen bilesenOgretmen in this.dersprogrami.ogretmenler)
          bilesenOgretmenList.Add(bilesenOgretmen);
        string[] strArray = new string[10];
        for (int index1 = 0; index1 < bilesenOgretmenList.Count; ++index1)
        {
          for (int index2 = index1 + 1; index2 < bilesenOgretmenList.Count; ++index2)
          {
            if (bilesenOgretmenList[index2].yerlesmemeSayisi > bilesenOgretmenList[index1].yerlesmemeSayisi)
            {
              bilesenOgretmen bilesenOgretmen = bilesenOgretmenList[index1];
              bilesenOgretmenList[index1] = bilesenOgretmenList[index2];
              bilesenOgretmenList[index2] = bilesenOgretmen;
            }
          }
        }
        for (int index = 0; index < bilesenOgretmenList.Count && index < 10; ++index)
          strArray[index] = bilesenOgretmenList[index].adi;
        return strArray;
      }
    }

    public void baslat()
    {
      int index1 = 0;
      byte num1 = 0;
      this.kronometre.Start();
      int num2 = this.yerlesenler.Count + this.yerlesmeyenler.Count;
      bilesenTanimliDers bilesenTanimliDers1 = (bilesenTanimliDers) null;
      List<bilesenTanimliDers> bilesenTanimliDersList1 = new List<bilesenTanimliDers>();
      List<bilesenTanimliDers> bilesenTanimliDersList2 = new List<bilesenTanimliDers>();
      List<bilesenTanimliDers> bilesenTanimliDersList3 = new List<bilesenTanimliDers>();
      List<iyilestirmeliTaramaYap.kaldirYerlestir> kaldirYerlestirList = new List<iyilestirmeliTaramaYap.kaldirYerlestir>();
      int num3 = num2 >= 150 ? 3 : 2;
      int num4 = num2 / num3;
      while (this.yerlesmeyenler.Count > 0)
      {
        lock (this.kilitle)
        {
          int num5 = 0;
          foreach (bilesenTanimliDers bilesenTanimliDers2 in this.yerlesenler)
            num5 += (int) bilesenTanimliDers2.toplamSaat;
          double num6 = (double) num5 / (double) this.toplamDersSaati;
          int num7 = (int) Math.Floor((double) (this.yerlesenler.Count * 100) / (double) (this.yerlesenler.Count + this.yerlesmeyenler.Count));
          if (num7 > this._enYuksekYuzde)
            this._enYuksekYuzde = num7;
          ++this.sayac;
          if (bilesenTanimliDers1 == null)
          {
            num1 = (byte) 0;
            int num8 = -1;
            for (int index2 = 0; index2 < this.yerlesmeyenler.Count; ++index2)
            {
              if (num8 < this.yerlesmeyenler[index2].denemeXsaat)
              {
                num8 = this.yerlesmeyenler[index2].denemeXsaat;
                index1 = index2;
              }
            }
            if (bilesenTanimliDers1 == null)
              bilesenTanimliDers1 = this.yerlesmeyenler[index1];
            bilesenTanimliDersList1.Add(bilesenTanimliDers1);
          }
          bool flag = false;
          ++bilesenTanimliDers1.denemeSayac;
          if (bilesenTanimliDersList1.Count > num2)
            bilesenTanimliDersList1.RemoveAt(0);
          int num9 = 0;
          foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDersList1)
          {
            if (bilesenTanimliDers2 == bilesenTanimliDers1)
              ++num9;
          }
          if (this.sayac % (ulong) num2 == 0UL && bilesenTanimliDersList1.Count == num2)
          {
            bilesenTanimliDersList2.Clear();
            foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDersList1)
            {
              if (!bilesenTanimliDersList2.Contains(bilesenTanimliDers2))
                bilesenTanimliDersList2.Add(bilesenTanimliDers2);
            }
            if ((double) bilesenTanimliDersList2.Count < Math.Pow((double) num2, 0.45))
            {
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDersList2)
              {
                for (int index2 = 0; (double) index2 < Math.Pow((double) bilesenTanimliDers2.iliskiListesi.Count, 0.5); ++index2)
                {
                  bilesenTanimliDers bilesenTanimliDers3 = (bilesenTanimliDers) null;
                  foreach (bilesenTanimliDers bilesenTanimliDers4 in bilesenTanimliDers2.iliskiListesi)
                  {
                    if (this.yerlesenler.Contains(bilesenTanimliDers4))
                    {
                      if (bilesenTanimliDers3 == null)
                        bilesenTanimliDers3 = bilesenTanimliDers4;
                      else if (bilesenTanimliDers3.denemeXsaat > bilesenTanimliDers4.denemeXsaat)
                        bilesenTanimliDers3 = bilesenTanimliDers4;
                    }
                  }
                  if (bilesenTanimliDers3 != null)
                  {
                    bilesenTanimliDers3.kaldir();
                    this.yerlesenler.Remove(bilesenTanimliDers3);
                    this.yerlesmeyenler.Add(bilesenTanimliDers3);
                    bilesenTanimliDersList1.Remove(bilesenTanimliDers2);
                    bilesenTanimliDersList1.Remove(bilesenTanimliDers3);
                  }
                }
              }
            }
          }
          if (bilesenTanimliDers1.yerlesirMi() && bilesenTanimliDers1.enIyiyeYerles())
          {
            bilesenTanimliDers.yerlesimOlasilik aktifYerlesim = bilesenTanimliDers1.aktifYerlesim;
            bilesenTanimliDers1.olasiliklar.Remove(aktifYerlesim);
            bilesenTanimliDers1.olasiliklar.Insert(0, aktifYerlesim);
            this.yerlesenler.Add(bilesenTanimliDers1);
            this.yerlesmeyenler.Remove(bilesenTanimliDers1);
            flag = true;
            bilesenTanimliDers1 = (bilesenTanimliDers) null;
          }
          if (!flag)
          {
            bilesenTanimliDersList3.Clear();
            kaldirYerlestirList.Clear();
            if (!flag && bilesenTanimliDers1 != null)
            {
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDers1.iliskiListesi)
              {
                if (this.yerlesenler.Contains(bilesenTanimliDers2))
                {
                  bilesenTanimliDers.yerlesimOlasilik aktifYerlesim = bilesenTanimliDers2.aktifYerlesim;
                  foreach (bilesenTanimliDers.yerlesimOlasilik yerlesimOlasilik in bilesenTanimliDers1.olasiliklar)
                  {
                    if (araclar.diziKesisiyormu(yerlesimOlasilik.tablo, aktifYerlesim.tablo))
                    {
                      bilesenTanimliDersList3.Add(bilesenTanimliDers2);
                      break;
                    }
                  }
                }
              }
            }
            if (!flag)
            {
              ++bilesenTanimliDers1.denemeSayac;
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDersList3)
              {
                bilesenTanimliDers2.kaldir();
                if (bilesenTanimliDers1.yerlesirMi())
                {
                  for (int index2 = 0; index2 < bilesenTanimliDers1.olasiliklar.Count; ++index2)
                  {
                    if (bilesenTanimliDers1.olasiliklar[index2].olumlu)
                    {
                      iyilestirmeliTaramaYap.kaldirYerlestir kaldirYerlestir = new iyilestirmeliTaramaYap.kaldirYerlestir()
                      {
                        dersler = new List<bilesenTanimliDers>()
                      };
                      kaldirYerlestir.dersler.Add(bilesenTanimliDers2);
                      kaldirYerlestirList.Add(kaldirYerlestir);
                      bilesenTanimliDers1.yerles(index2);
                      if (bilesenTanimliDers2.yerlesirMi())
                      {
                        for (int index3 = 0; index3 < bilesenTanimliDers2.olasiliklar.Count; ++index3)
                        {
                          if (bilesenTanimliDers2.olasiliklar[index3].olumlu)
                          {
                            bilesenTanimliDers2.yerles(index3);
                            this.yerlesenler.Add(bilesenTanimliDers1);
                            this.yerlesmeyenler.Remove(bilesenTanimliDers1);
                            flag = true;
                            bilesenTanimliDers1 = (bilesenTanimliDers) null;
                            break;
                          }
                        }
                      }
                      if (!flag)
                        bilesenTanimliDers1.kaldir();
                      else
                        break;
                    }
                  }
                }
                if (!flag)
                  bilesenTanimliDers2.eskiyeYerles();
                else
                  break;
              }
            }
            double num8 = 0.0;
            if (!flag)
            {
              int toplamSaat = (int) bilesenTanimliDers1.toplamSaat;
              int num10 = 0;
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDers1.iliskiListesi)
              {
                toplamSaat += (int) bilesenTanimliDers2.toplamSaat;
                if (this.yerlesenler.Contains(bilesenTanimliDers2))
                  num10 += (int) bilesenTanimliDers2.toplamSaat;
              }
              num8 = (double) num10 / (double) toplamSaat;
            }
            double num11 = (double) this.yerlesenler.Count / (double) this.dersprogrami.tanimliDersler.Count;
            if (!flag && num1 > (byte) 0 && (num8 > 0.85 && num11 > 0.8))
            {
              ++bilesenTanimliDers1.denemeSayac;
              List<bilesenTanimliDers> bilesenTanimliDersList4 = new List<bilesenTanimliDers>();
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDers1.iliskiListesi)
              {
                if (this.yerlesenler.Contains(bilesenTanimliDers2))
                  bilesenTanimliDersList4.Add(bilesenTanimliDers2);
              }
              for (int index2 = 0; index2 < bilesenTanimliDersList4.Count; ++index2)
              {
                for (int index3 = index2 + 1; index3 < bilesenTanimliDersList4.Count; ++index3)
                {
                  bilesenTanimliDers bilesenTanimliDers2 = bilesenTanimliDersList4[index2];
                  bilesenTanimliDers bilesenTanimliDers3 = bilesenTanimliDersList4[index3];
                  bilesenTanimliDers.yerlesimOlasilik aktifYerlesim1 = bilesenTanimliDers2.aktifYerlesim;
                  bilesenTanimliDers.yerlesimOlasilik aktifYerlesim2 = bilesenTanimliDers3.aktifYerlesim;
                  bilesenTanimliDers2.kaldir();
                  bilesenTanimliDers3.kaldir();
                  if (bilesenTanimliDers1.yerlesirMi())
                  {
                    iyilestirmeliTaramaYap.kaldirYerlestir kaldirYerlestir = new iyilestirmeliTaramaYap.kaldirYerlestir()
                    {
                      dersler = new List<bilesenTanimliDers>()
                    };
                    kaldirYerlestir.dersler.Add(bilesenTanimliDers2);
                    kaldirYerlestir.dersler.Add(bilesenTanimliDers3);
                    kaldirYerlestirList.Add(kaldirYerlestir);
                    for (int index4 = 0; index4 < bilesenTanimliDers1.olasiliklar.Count; ++index4)
                    {
                      if (bilesenTanimliDers1.olasiliklar[index4].olumlu)
                      {
                        bilesenTanimliDers1.yerles(index4);
                        if (bilesenTanimliDers2.yerlesirMi())
                        {
                          for (int index5 = 0; index5 < bilesenTanimliDers2.olasiliklar.Count; ++index5)
                          {
                            if (bilesenTanimliDers2.olasiliklar[index5].olumlu)
                            {
                              bilesenTanimliDers2.yerles(index5);
                              if (bilesenTanimliDers3.yerlesirMi())
                              {
                                for (int index6 = 0; index6 < bilesenTanimliDers3.olasiliklar.Count; ++index6)
                                {
                                  if (bilesenTanimliDers3.olasiliklar[index6].olumlu)
                                  {
                                    bilesenTanimliDers3.yerles(index6);
                                    this.yerlesenler.Add(bilesenTanimliDers1);
                                    this.yerlesmeyenler.Remove(bilesenTanimliDers1);
                                    flag = true;
                                    bilesenTanimliDers1 = (bilesenTanimliDers) null;
                                    break;
                                  }
                                }
                              }
                              if (!flag)
                                bilesenTanimliDers2.kaldir();
                              else
                                break;
                            }
                          }
                        }
                        if (!flag)
                          bilesenTanimliDers1.kaldir();
                        else
                          break;
                      }
                    }
                  }
                  if (!flag)
                  {
                    bilesenTanimliDers2.yerles(aktifYerlesim1);
                    bilesenTanimliDers3.yerles(aktifYerlesim2);
                  }
                  else
                    break;
                }
                if (flag)
                  break;
              }
              if (!flag)
                ++bilesenTanimliDers1.denemeSayac;
            }
            if (!flag && num1 > (byte) 1 && (num8 > 0.9 && num11 > 0.9))
            {
              ++bilesenTanimliDers1.denemeSayac;
              List<bilesenTanimliDers> bilesenTanimliDersList4 = new List<bilesenTanimliDers>();
              foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDers1.iliskiListesi)
              {
                if (this.yerlesenler.Contains(bilesenTanimliDers2))
                  bilesenTanimliDersList4.Add(bilesenTanimliDers2);
              }
              for (int index2 = 0; index2 < bilesenTanimliDersList4.Count; ++index2)
              {
                for (int index3 = index2 + 1; index3 < bilesenTanimliDersList4.Count; ++index3)
                {
                  for (int index4 = index3 + 1; index4 < bilesenTanimliDersList4.Count; ++index4)
                  {
                    bilesenTanimliDers bilesenTanimliDers2 = bilesenTanimliDersList4[index2];
                    bilesenTanimliDers bilesenTanimliDers3 = bilesenTanimliDersList4[index3];
                    bilesenTanimliDers bilesenTanimliDers4 = bilesenTanimliDersList4[index4];
                    bilesenTanimliDers.yerlesimOlasilik aktifYerlesim1 = bilesenTanimliDers2.aktifYerlesim;
                    bilesenTanimliDers.yerlesimOlasilik aktifYerlesim2 = bilesenTanimliDers3.aktifYerlesim;
                    bilesenTanimliDers.yerlesimOlasilik aktifYerlesim3 = bilesenTanimliDers4.aktifYerlesim;
                    bilesenTanimliDers2.kaldir();
                    bilesenTanimliDers3.kaldir();
                    bilesenTanimliDers4.kaldir();
                    if (bilesenTanimliDers1.yerlesirMi())
                    {
                      iyilestirmeliTaramaYap.kaldirYerlestir kaldirYerlestir = new iyilestirmeliTaramaYap.kaldirYerlestir()
                      {
                        dersler = new List<bilesenTanimliDers>()
                      };
                      kaldirYerlestir.dersler.Add(bilesenTanimliDers2);
                      kaldirYerlestir.dersler.Add(bilesenTanimliDers3);
                      kaldirYerlestir.dersler.Add(bilesenTanimliDers4);
                      kaldirYerlestirList.Add(kaldirYerlestir);
                      for (int index5 = 0; index5 < bilesenTanimliDers1.olasiliklar.Count; ++index5)
                      {
                        if (bilesenTanimliDers1.olasiliklar[index5].olumlu)
                        {
                          bilesenTanimliDers1.yerles(index5);
                          if (bilesenTanimliDers2.yerlesirMi())
                          {
                            for (int index6 = 0; index6 < bilesenTanimliDers2.olasiliklar.Count; ++index6)
                            {
                              if (bilesenTanimliDers2.olasiliklar[index6].olumlu)
                              {
                                bilesenTanimliDers2.yerles(index6);
                                if (bilesenTanimliDers3.yerlesirMi())
                                {
                                  for (int index7 = 0; index7 < bilesenTanimliDers3.olasiliklar.Count; ++index7)
                                  {
                                    if (bilesenTanimliDers3.olasiliklar[index7].olumlu)
                                    {
                                      bilesenTanimliDers3.yerles(index7);
                                      if (bilesenTanimliDers4.yerlesirMi())
                                      {
                                        for (int index8 = 0; index8 < bilesenTanimliDers4.olasiliklar.Count; ++index8)
                                        {
                                          if (bilesenTanimliDers4.olasiliklar[index8].olumlu)
                                          {
                                            bilesenTanimliDers4.yerles(index8);
                                            this.yerlesenler.Add(bilesenTanimliDers1);
                                            this.yerlesmeyenler.Remove(bilesenTanimliDers1);
                                            flag = true;
                                            bilesenTanimliDers1 = (bilesenTanimliDers) null;
                                            break;
                                          }
                                        }
                                      }
                                      if (!flag)
                                        bilesenTanimliDers3.kaldir();
                                      else
                                        break;
                                    }
                                  }
                                }
                                if (!flag)
                                  bilesenTanimliDers2.kaldir();
                                else
                                  break;
                              }
                            }
                          }
                          if (!flag)
                            bilesenTanimliDers1.kaldir();
                          else
                            break;
                        }
                      }
                    }
                    if (!flag)
                    {
                      bilesenTanimliDers2.yerles(aktifYerlesim1);
                      bilesenTanimliDers3.yerles(aktifYerlesim2);
                      bilesenTanimliDers4.yerles(aktifYerlesim3);
                    }
                    else
                      break;
                  }
                  if (flag)
                    break;
                }
                if (flag)
                  break;
              }
              if (!flag)
                ++bilesenTanimliDers1.denemeSayac;
            }
            if (!flag)
            {
              ++bilesenTanimliDers1.denemeSayac;
              if (bilesenTanimliDersList1.Count > 6)
              {
                for (int index2 = 0; index2 < kaldirYerlestirList.Count; ++index2)
                {
                  if (kaldirYerlestirList[index2].dersler.Count == 1)
                  {
                    int num10 = 0;
                    for (int index3 = bilesenTanimliDersList1.Count - 3; index3 < bilesenTanimliDersList1.Count; ++index3)
                    {
                      if (bilesenTanimliDersList1[index3] == kaldirYerlestirList[index2].dersler[0])
                        ++num10;
                    }
                    if (num10 > 0)
                    {
                      kaldirYerlestirList.RemoveAt(index2);
                      --index2;
                    }
                  }
                }
                for (int index2 = 0; index2 < bilesenTanimliDersList3.Count; ++index2)
                {
                  int num10 = 0;
                  for (int index3 = bilesenTanimliDersList1.Count - 3; index3 < bilesenTanimliDersList1.Count; ++index3)
                  {
                    if (bilesenTanimliDersList1[index3] == bilesenTanimliDersList3[index2])
                      ++num10;
                  }
                  if (num10 > 0)
                  {
                    bilesenTanimliDersList3.RemoveAt(index2);
                    --index2;
                  }
                }
              }
              if (kaldirYerlestirList.Count == 0 && bilesenTanimliDersList3.Count == 0)
              {
                foreach (bilesenTanimliDers bilesenTanimliDers2 in bilesenTanimliDers1.iliskiListesi)
                {
                  if (this.yerlesenler.Contains(bilesenTanimliDers2))
                    bilesenTanimliDersList3.Add(bilesenTanimliDers2);
                }
              }
              if (kaldirYerlestirList.Count > 0)
              {
                double toplamDenemeXsayac = kaldirYerlestirList[0].toplamDenemeXsayac;
                int index2 = 0;
                for (int index3 = 1; index3 < kaldirYerlestirList.Count; ++index3)
                {
                  if (kaldirYerlestirList[index3].toplamDenemeXsayac < toplamDenemeXsayac)
                  {
                    toplamDenemeXsayac = kaldirYerlestirList[index3].toplamDenemeXsayac;
                    index2 = index3;
                  }
                }
                foreach (bilesenTanimliDers bilesenTanimliDers2 in kaldirYerlestirList[index2].dersler)
                {
                  foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers2.ogretmenler)
                    bilesenOgretmen.yerlesmemeSayisi = Convert.ToInt32((double) bilesenOgretmen.yerlesmemeSayisi * 0.99);
                  bilesenTanimliDers2.kaldir();
                  this.yerlesenler.Remove(bilesenTanimliDers2);
                  this.yerlesmeyenler.Add(bilesenTanimliDers2);
                  if (bilesenTanimliDersList3.Contains(bilesenTanimliDers2))
                    bilesenTanimliDersList3.Remove(bilesenTanimliDers2);
                }
              }
              if (bilesenTanimliDersList3.Count > 0 && (num1 > (byte) 0 || num9 > 3 || kaldirYerlestirList.Count == 0))
              {
                int index2 = 0;
                int denemeXsaat = bilesenTanimliDersList3[0].denemeXsaat;
                for (int index3 = 0; index3 < bilesenTanimliDersList3.Count; ++index3)
                {
                  if (denemeXsaat > bilesenTanimliDersList3[index3].denemeXsaat)
                  {
                    denemeXsaat = bilesenTanimliDersList3[index3].denemeXsaat;
                    index2 = index3;
                  }
                }
                bilesenTanimliDers bilesenTanimliDers2 = bilesenTanimliDersList3[index2];
                if (this.yerlesenler.Contains(bilesenTanimliDers2))
                {
                  foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers2.ogretmenler)
                    bilesenOgretmen.yerlesmemeSayisi = Convert.ToInt32((double) bilesenOgretmen.yerlesmemeSayisi * 0.99);
                  bilesenTanimliDers2.kaldir();
                  this.yerlesenler.Remove(bilesenTanimliDers2);
                  this.yerlesmeyenler.Add(bilesenTanimliDers2);
                }
              }
              if (num1 > (byte) 2)
              {
                bilesenTanimliDers bilesenTanimliDers2 = (bilesenTanimliDers) null;
                int num10 = -1;
                foreach (bilesenTanimliDers bilesenTanimliDers3 in bilesenTanimliDers1.iliskiListesi)
                {
                  if (this.yerlesenler.Contains(bilesenTanimliDers3) && (num10 > bilesenTanimliDers3.denemeXsaat || num10 == -1))
                  {
                    num10 = bilesenTanimliDers3.denemeXsaat;
                    bilesenTanimliDers2 = bilesenTanimliDers3;
                  }
                }
                if (bilesenTanimliDers2 != null)
                {
                  foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers2.ogretmenler)
                    bilesenOgretmen.yerlesmemeSayisi = Convert.ToInt32((double) bilesenOgretmen.yerlesmemeSayisi * 0.99);
                  bilesenTanimliDers2.kaldir();
                  this.yerlesenler.Remove(bilesenTanimliDers2);
                  this.yerlesmeyenler.Add(bilesenTanimliDers2);
                }
              }
              if (!flag)
              {
                foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers1.ogretmenler)
                  bilesenOgretmen.yerlesmemeSayisi += (int) num1 + 2;
              }
            }
          }
          else
            continue;
        }
        ++num1;
      }
      foreach (bilesenTanimliDers bilesenTanimliDers2 in this.dersprogrami.tanimliDersler)
      {
        if (bilesenTanimliDers2.aktifYerlesim == null)
        {
          int num5 = (int) MessageBox.Show("Hatalı yerleşim");
        }
      }
      this.dersprogrami.dagitimaHazirla();
      foreach (bilesenTanimliDers bilesenTanimliDers2 in this.dersprogrami.tanimliDersler)
      {
        if (bilesenTanimliDers2.yerlesirMi())
        {
          if (bilesenTanimliDers2.olasilikSina(bilesenTanimliDers2.aktifYerlesim.id, true))
          {
            bilesenTanimliDers2.yerles(bilesenTanimliDers2.aktifYerlesim.id, true);
          }
          else
          {
            int num5 = (int) MessageBox.Show("Hata var");
          }
        }
        else
        {
          int num6 = (int) MessageBox.Show("Hata var");
        }
      }
      this.kronometre.Stop();
      Thread.Sleep(1000);
      this.bitti = true;
    }

    public void durdur() => this.threadYerlestir.Abort();

    private struct kaldirYerlestir
    {
      public List<bilesenTanimliDers> dersler;

      public double toplamDenemeXsayac
      {
        get
        {
          int num = 0;
          foreach (bilesenTanimliDers bilesenTanimliDers in this.dersler)
            num += bilesenTanimliDers.denemeXsaat;
          return (double) num;
        }
      }
    }
  }
}
