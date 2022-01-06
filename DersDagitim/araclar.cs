using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Windows.Forms;

namespace DersDagitim
{
  public static class araclar
  {
    public const string SITE = "";

    public static string versiyon()
    {
      Version version = Assembly.GetExecutingAssembly().GetName().Version;
      return string.Format("v{0}.{1}.{2} ({3})", (object) version.Major, (object) version.Minor, (object) version.Build, (object) version.Revision);
    }

    public static bool diziKesisiyormu(bool[,] dizi1, bool[,] dizi2)
    {
      bool flag = false;
      for (int index1 = 0; index1 < dizi1.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < dizi1.GetLength(1); ++index2)
        {
          if (!dizi1[index1, index2] && !dizi2[index1, index2])
          {
            flag = true;
            break;
          }
          if (flag)
            break;
        }
      }
      return flag;
    }

    public static bool[,] diziKopyala(bool[,] kopyalanacak)
    {
      int length1 = kopyalanacak.GetLength(0);
      int length2 = kopyalanacak.GetLength(1);
      bool[,] flagArray = new bool[length1, length2];
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
          flagArray[index1, index2] = kopyalanacak[index1, index2];
      }
      return flagArray;
    }

    public static bool[,] diziBirlestir(bool[,] dizi1, bool[,] dizi2)
    {
      bool[,] flagArray = araclar.diziOlustur();
      for (int index1 = 0; index1 < dizi2.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < dizi2.GetLength(1); ++index2)
          flagArray[index1, index2] = dizi2[index1, index2] && dizi1[index1, index2];
      }
      return flagArray;
    }

    public static bool[,] diziEkle(bool[,] dizi1, bool[,] dizi2)
    {
      bool[,] flagArray = araclar.diziOlustur();
      for (int index1 = 0; index1 < dizi2.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < dizi2.GetLength(1); ++index2)
          flagArray[index1, index2] = dizi2[index1, index2] || dizi1[index1, index2];
      }
      return flagArray;
    }

    public static void diziKopyala(ref bool[,] hedef, bool[,] kaynak)
    {
      int length1 = kaynak.GetLength(0);
      int length2 = kaynak.GetLength(1);
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          if (index1 < hedef.GetLength(0) && index2 < hedef.GetLength(1))
            hedef[index1, index2] = kaynak[index1, index2];
        }
      }
    }

    public static string diziKodla(bool[,] dizi)
    {
      int length1 = dizi.GetLength(0);
      int length2 = dizi.GetLength(1);
      string str = "";
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
          str = !dizi[index1, index2] ? str + "0" : str + "1";
      }
      return str;
    }

    public static bool[,] diziKodCoz(string str, int gen, int yuk)
    {
      bool[,] flagArray = new bool[gen, yuk];
      int num = 0;
      for (int index1 = 0; index1 < gen; ++index1)
      {
        for (int index2 = 0; index2 < yuk; ++index2)
        {
          string str1 = str;
          int index3 = num++;
          flagArray[index1, index2] = str1[index3] == '1';
        }
      }
      return flagArray;
    }

    public static bool[,] diziOlustur(bool durum = true)
    {
      bool[,] flagArray = new bool[(int) tanim.program.haftalikGunSayisi, (int) tanim.program.gunlukDersSaatiSayisi];
      for (int index1 = 0; index1 < flagArray.GetLength(0); ++index1)
      {
        for (int index2 = 0; index2 < flagArray.GetLength(1); ++index2)
          flagArray[index1, index2] = durum;
      }
      return flagArray;
    }

    public static Bitmap kosulResim(bool[,] dizi, bool buyukResim = false)
    {
      double num1 = (double) (99 / dizi.GetLength(1));
      double num2 = (double) (39 / dizi.GetLength(0));
      Bitmap sourceBMP = new Bitmap(100, 40);
      Graphics graphics = Graphics.FromImage((Image) sourceBMP);
      for (int index1 = 0; index1 < dizi.GetLength(1); ++index1)
      {
        for (int index2 = 0; index2 < dizi.GetLength(0); ++index2)
        {
          if (dizi[index2, index1])
            graphics.FillRectangle((Brush) new SolidBrush(Color.Green), new Rectangle((int) ((double) index1 * num1), (int) ((double) index2 * num2), (int) num1, (int) num2));
          else
            graphics.FillRectangle((Brush) new SolidBrush(Color.Red), new Rectangle((int) ((double) index1 * num1), (int) ((double) index2 * num2), (int) num1, (int) num2));
          graphics.DrawRectangle(new Pen(Color.White), new Rectangle((int) ((double) index1 * num1), (int) ((double) index2 * num2), (int) num1, (int) num2));
        }
      }
      if (buyukResim)
        sourceBMP = araclar.resimBuyult(sourceBMP);
      return sourceBMP;
    }

    private static Bitmap resimBuyult(Bitmap sourceBMP)
    {
      int width = sourceBMP.Width * 2;
      int height = sourceBMP.Height * 2;
      Bitmap bitmap = new Bitmap(width, height);
      using (Graphics graphics = Graphics.FromImage((Image) bitmap))
        graphics.DrawImage((Image) sourceBMP, 0, 0, width, height);
      return bitmap;
    }

    public static void marioMelodiCal() => Console.Beep(2250, 200);

    public static Bitmap dersProgramiCizelgesi(bilesenTaban bilesen)
    {
      int haftalikGunSayisi = (int) tanim.program.haftalikGunSayisi;
      int gunlukDersSaatiSayisi = (int) tanim.program.gunlukDersSaatiSayisi;
      Bitmap bitmap = new Bitmap(30 + 120 * haftalikGunSayisi + 1, 30 + 70 * gunlukDersSaatiSayisi + 1);
      Graphics graphics = Graphics.FromImage((Image) bitmap);
      Pen pen1 = new Pen((Brush) new SolidBrush(Color.Black), 1.5f);
      Pen pen2 = new Pen((Brush) new SolidBrush(Color.Gray), 1.5f);
      pen2.DashStyle = DashStyle.Dash;
      Font font1 = new Font("Arial", 7f, FontStyle.Regular);
      Font font2 = new Font("Arial", 10f, FontStyle.Bold);
      Font font3 = new Font("Arial", 9f, FontStyle.Regular);
      Font font4 = new Font("Arial", 9f, FontStyle.Bold);
      Font font5 = new Font("Arial", 8.5f, FontStyle.Bold);
      SolidBrush solidBrush = new SolidBrush(Color.Black);
      StringFormat format1 = new StringFormat();
      format1.Alignment = StringAlignment.Center;
      format1.LineAlignment = StringAlignment.Center;
      StringFormat format2 = new StringFormat();
      StringFormat format3 = new StringFormat();
      format3.LineAlignment = StringAlignment.Far;
      format3.Alignment = StringAlignment.Far;
      StringFormat format4 = new StringFormat();
      format4.LineAlignment = StringAlignment.Far;
      format4.Alignment = StringAlignment.Near;
      StringFormat stringFormat = new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center,
        FormatFlags = StringFormatFlags.DirectionVertical
      };
      Rectangle rect1 = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
      Rectangle[] rectangleArray1 = new Rectangle[haftalikGunSayisi];
      Rectangle[] rectangleArray2 = new Rectangle[gunlukDersSaatiSayisi];
      Rectangle[,] rectangleArray3 = new Rectangle[haftalikGunSayisi, gunlukDersSaatiSayisi];
      List<Rectangle> rectangleList = new List<Rectangle>();
      for (int index = 0; index < haftalikGunSayisi; ++index)
        rectangleArray1[index] = new Rectangle(30 + index * 120, 0, 120, 30);
      for (int index = 0; index < gunlukDersSaatiSayisi; ++index)
        rectangleArray2[index] = new Rectangle(0, 30 + index * 70, 30, 70);
      for (int index1 = 0; index1 < haftalikGunSayisi; ++index1)
      {
        for (int index2 = 0; index2 < gunlukDersSaatiSayisi; ++index2)
        {
          rectangleArray3[index1, index2] = new Rectangle(30 + index1 * 120, 30 + index2 * 70, 120, 70);
          rectangleList.Add(rectangleArray3[index1, index2]);
        }
      }
      rectangleList.AddRange((IEnumerable<Rectangle>) rectangleArray1);
      rectangleList.AddRange((IEnumerable<Rectangle>) rectangleArray2);
      graphics.FillRectangle((Brush) new SolidBrush(Color.White), rect1);
      foreach (Rectangle rect2 in rectangleArray1)
        graphics.FillRectangle((Brush) new SolidBrush(Color.LightGray), rect2);
      foreach (Rectangle rect2 in rectangleList)
        graphics.DrawRectangle(new Pen(Color.Black), rect2);
      for (int index = 0; index < rectangleArray2.Length; ++index)
      {
        graphics.TranslateTransform((float) rectangleArray2[index].X, (float) (rectangleArray2[index].Height + rectangleArray2[index].Y));
        graphics.RotateTransform(270f);
        Rectangle rect2 = new Rectangle(0, 0, rectangleArray2[index].Height, rectangleArray2[index].Width);
        string s = (index + 1).ToString() + "\n" + tanim.program.derssaatleri[index];
        graphics.FillRectangle((Brush) new SolidBrush(Color.LightGray), rect2);
        graphics.DrawString(s, font5, (Brush) solidBrush, (RectangleF) rect2, format1);
        graphics.ResetTransform();
      }
      for (int index = 0; index < rectangleArray1.Length; ++index)
        graphics.DrawString(tanim.program.gunler[index], font2, (Brush) solidBrush, (RectangleF) rectangleArray1[index], format1);
      if (bilesen is bilesenOgretmen)
      {
        bilesenOgretmen bilesenOgretmen1 = bilesen as bilesenOgretmen;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenOgretmen bilesenOgretmen2 in bilesenTanimliDers.ogretmenler)
            {
              if (bilesenOgretmen2 == bilesenOgretmen1)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.derslikler = dersYerlesim2.derslikler + bilesenDerslik.kisaAdi + " ";
                    }
                    foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                    {
                      if (bilesenSinifGrup.grup.id != (ushort) 0)
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
                      }
                      else
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + " ";
                      }
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              graphics.DrawRectangle(pen2, rectangleArray4[index3]);
              graphics.DrawString(dersYerlesimList2[index3].dersAdi, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              graphics.DrawString(dersYerlesimList2[index3].sinifGruplar, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              graphics.DrawString(dersYerlesimList2[index3].derslikler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format3);
            }
          }
        }
      }
      if (bilesen is bilesenSinif)
      {
        bilesenSinif bilesenSinif = bilesen as bilesenSinif;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
            {
              if (bilesenSinifGrup.sinif == bilesenSinif)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    if (bilesenSinifGrup.grup.id != (ushort) 0)
                    {
                      dersYerlesim1.grupAdi = bilesenSinifGrup.grup.kisaAdi;
                      dersYerlesim1.dersAdi = bilesenTanimliDers.ders.kisaAdi;
                    }
                    else
                      dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.ogretmenler = dersYerlesim2.ogretmenler + bilesenOgretmen.kisaAdi + " ";
                    }
                    foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.derslikler = dersYerlesim2.derslikler + bilesenDerslik.kisaAdi + " ";
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              graphics.DrawRectangle(pen2, rectangleArray4[index3]);
              graphics.DrawString(dersYerlesimList2[index3].grupAdi, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              graphics.DrawString(dersYerlesimList2[index3].dersAdi, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              graphics.DrawString(dersYerlesimList2[index3].ogretmenler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format4);
              graphics.DrawString(dersYerlesimList2[index3].derslikler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format3);
            }
          }
        }
      }
      if (bilesen is bilesenDerslik)
      {
        bilesenDerslik bilesenDerslik1 = bilesen as bilesenDerslik;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenDerslik bilesenDerslik2 in bilesenTanimliDers.derslikler)
            {
              if (bilesenDerslik2 == bilesenDerslik1)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.ogretmenler = dersYerlesim2.ogretmenler + bilesenOgretmen.kisaAdi + " ";
                    }
                    foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                    {
                      if (bilesenSinifGrup.grup.id != (ushort) 0)
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
                      }
                      else
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + " ";
                      }
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              graphics.DrawRectangle(pen2, rectangleArray4[index3]);
              graphics.DrawString(dersYerlesimList2[index3].sinifGruplar, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              graphics.DrawString(dersYerlesimList2[index3].dersAdi, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              graphics.DrawString(dersYerlesimList2[index3].ogretmenler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format4);
            }
          }
        }
      }
      string str = "";
      foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
      {
        if (bilesenTanimliDers.aktifYerlesim == null)
          str = str + "[" + bilesenTanimliDers.aciklama + "] ";
      }
      if (str != "")
      {
        int num = (int) MessageBox.Show("Aşağıdaki Dersler Yerleşmemiştir!!\n" + str);
      }
      foreach (Rectangle rect2 in rectangleList)
        graphics.DrawRectangle(new Pen(Color.Black), rect2);
      return bitmap;
    }

    public static void dersProgramiCizelgesi(
      bilesenTaban bilesen,
      Graphics gr,
      int xBOS,
      int YBOS)
    {
      int width = 600 / (int) tanim.program.haftalikGunSayisi;
      int haftalikGunSayisi = (int) tanim.program.haftalikGunSayisi;
      int gunlukDersSaatiSayisi = (int) tanim.program.gunlukDersSaatiSayisi;
      gr.TranslateTransform((float) xBOS, (float) YBOS);
      Pen pen1 = new Pen((Brush) new SolidBrush(Color.Black), 1.5f);
      Pen pen2 = new Pen((Brush) new SolidBrush(Color.Gray), 0.7f);
      pen2.DashStyle = DashStyle.Dash;
      Font font1 = new Font("Arial", 7f, FontStyle.Regular);
      Font font2 = new Font("Arial", 10f, FontStyle.Bold);
      Font font3 = new Font("Arial", 9f, FontStyle.Regular);
      Font font4 = new Font("Arial", 9f, FontStyle.Bold);
      Font font5 = new Font("Arial", 7f, FontStyle.Bold);
      SolidBrush solidBrush = new SolidBrush(Color.Black);
      StringFormat format1 = new StringFormat();
      format1.Alignment = StringAlignment.Center;
      format1.LineAlignment = StringAlignment.Center;
      StringFormat format2 = new StringFormat();
      StringFormat format3 = new StringFormat();
      format3.LineAlignment = StringAlignment.Far;
      format3.Alignment = StringAlignment.Far;
      StringFormat format4 = new StringFormat();
      format4.LineAlignment = StringAlignment.Far;
      format4.Alignment = StringAlignment.Near;
      StringFormat stringFormat = new StringFormat()
      {
        Alignment = StringAlignment.Center,
        LineAlignment = StringAlignment.Center,
        FormatFlags = StringFormatFlags.DirectionVertical
      };
      Rectangle rect1 = new Rectangle(0, 0, 30 + width * haftalikGunSayisi + 1, 30 + 60 * gunlukDersSaatiSayisi + 1);
      Rectangle[] rectangleArray1 = new Rectangle[haftalikGunSayisi];
      Rectangle[] rectangleArray2 = new Rectangle[gunlukDersSaatiSayisi];
      Rectangle[,] rectangleArray3 = new Rectangle[haftalikGunSayisi, gunlukDersSaatiSayisi];
      List<Rectangle> rectangleList = new List<Rectangle>();
      for (int index = 0; index < haftalikGunSayisi; ++index)
        rectangleArray1[index] = new Rectangle(30 + index * width, 0, width, 30);
      for (int index = 0; index < gunlukDersSaatiSayisi; ++index)
        rectangleArray2[index] = new Rectangle(0, 30 + index * 60, 30, 60);
      for (int index1 = 0; index1 < haftalikGunSayisi; ++index1)
      {
        for (int index2 = 0; index2 < gunlukDersSaatiSayisi; ++index2)
        {
          rectangleArray3[index1, index2] = new Rectangle(30 + index1 * width, 30 + index2 * 60, width, 60);
          rectangleList.Add(rectangleArray3[index1, index2]);
        }
      }
      rectangleList.AddRange((IEnumerable<Rectangle>) rectangleArray1);
      rectangleList.AddRange((IEnumerable<Rectangle>) rectangleArray2);
      gr.FillRectangle((Brush) new SolidBrush(Color.White), rect1);
      foreach (Rectangle rect2 in rectangleList)
        gr.DrawRectangle(new Pen(Color.Black), rect2);
      foreach (Rectangle rect2 in rectangleArray1)
        gr.FillRectangle((Brush) new SolidBrush(Color.LightGray), rect2);
      for (int index = 0; index < rectangleArray2.Length; ++index)
      {
        gr.TranslateTransform((float) rectangleArray2[index].X, (float) (rectangleArray2[index].Height + rectangleArray2[index].Y));
        gr.RotateTransform(270f);
        Rectangle rect2 = new Rectangle(0, 0, rectangleArray2[index].Height, rectangleArray2[index].Width);
        string s = (index + 1).ToString() + "\n" + tanim.program.derssaatleri[index];
        gr.FillRectangle((Brush) new SolidBrush(Color.LightGray), rect2);
        gr.DrawString(s, font5, (Brush) solidBrush, (RectangleF) rect2, format1);
        gr.ResetTransform();
        gr.TranslateTransform((float) xBOS, (float) YBOS);
      }
      for (int index = 0; index < rectangleArray1.Length; ++index)
        gr.DrawString(tanim.program.gunler[index], font2, (Brush) solidBrush, (RectangleF) rectangleArray1[index], format1);
      if (bilesen is bilesenOgretmen)
      {
        bilesenOgretmen bilesenOgretmen1 = bilesen as bilesenOgretmen;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenOgretmen bilesenOgretmen2 in bilesenTanimliDers.ogretmenler)
            {
              if (bilesenOgretmen2 == bilesenOgretmen1)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.derslikler = dersYerlesim2.derslikler + bilesenDerslik.kisaAdi + " ";
                    }
                    foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                    {
                      if (bilesenSinifGrup.grup.id != (ushort) 0)
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
                      }
                      else
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + " ";
                      }
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              gr.DrawRectangle(pen2, rectangleArray4[index3]);
              gr.DrawString(dersYerlesimList2[index3].dersAdi, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              gr.DrawString(dersYerlesimList2[index3].sinifGruplar, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              gr.DrawString(dersYerlesimList2[index3].derslikler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format3);
            }
          }
        }
      }
      if (bilesen is bilesenSinif)
      {
        bilesenSinif bilesenSinif = bilesen as bilesenSinif;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
            {
              if (bilesenSinifGrup.sinif == bilesenSinif)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    if (bilesenSinifGrup.grup.id != (ushort) 0)
                    {
                      dersYerlesim1.grupAdi = bilesenSinifGrup.grup.kisaAdi;
                      dersYerlesim1.dersAdi = bilesenTanimliDers.ders.kisaAdi;
                    }
                    else
                      dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.ogretmenler = dersYerlesim2.ogretmenler + bilesenOgretmen.kisaAdi + " ";
                    }
                    foreach (bilesenDerslik bilesenDerslik in bilesenTanimliDers.derslikler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.derslikler = dersYerlesim2.derslikler + bilesenDerslik.kisaAdi + " ";
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              gr.DrawRectangle(pen2, rectangleArray4[index3]);
              gr.DrawString(dersYerlesimList2[index3].grupAdi, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              gr.DrawString(dersYerlesimList2[index3].dersAdi, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              gr.DrawString(dersYerlesimList2[index3].ogretmenler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format4);
              gr.DrawString(dersYerlesimList2[index3].derslikler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format3);
            }
          }
        }
      }
      if (bilesen is bilesenDerslik)
      {
        bilesenDerslik bilesenDerslik1 = bilesen as bilesenDerslik;
        List<bilesenTanimliDers> bilesenTanimliDersList = new List<bilesenTanimliDers>();
        List<araclar.dersYerlesim> dersYerlesimList1 = new List<araclar.dersYerlesim>();
        foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
        {
          if (bilesenTanimliDers.aktifYerlesim != null)
          {
            foreach (bilesenDerslik bilesenDerslik2 in bilesenTanimliDers.derslikler)
            {
              if (bilesenDerslik2 == bilesenDerslik1)
              {
                for (int index1 = 0; index1 < bilesenTanimliDers.nodes.Length; ++index1)
                {
                  for (int index2 = 0; index2 < (int) bilesenTanimliDers.nodes[index1].tSaat; ++index2)
                  {
                    araclar.dersYerlesim dersYerlesim1 = new araclar.dersYerlesim();
                    dersYerlesim1.gun = (int) bilesenTanimliDers.nodes[index1].yerlesimGun;
                    dersYerlesim1.saat = (int) bilesenTanimliDers.nodes[index1].yerlesimSaat + index2;
                    dersYerlesim1.dersAdi = bilesenTanimliDers.ders.adi;
                    foreach (bilesenOgretmen bilesenOgretmen in bilesenTanimliDers.ogretmenler)
                    {
                      araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                      dersYerlesim2.ogretmenler = dersYerlesim2.ogretmenler + bilesenOgretmen.kisaAdi + " ";
                    }
                    foreach (bilesenSinifGrup bilesenSinifGrup in bilesenTanimliDers.sinifGruplar)
                    {
                      if (bilesenSinifGrup.grup.id != (ushort) 0)
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + "-" + bilesenSinifGrup.grup.kisaAdi + " ";
                      }
                      else
                      {
                        araclar.dersYerlesim dersYerlesim2 = dersYerlesim1;
                        dersYerlesim2.sinifGruplar = dersYerlesim2.sinifGruplar + bilesenSinifGrup.sinif.kisaAdi + " ";
                      }
                    }
                    dersYerlesimList1.Add(dersYerlesim1);
                  }
                }
              }
            }
          }
        }
        for (int index1 = 0; index1 < (int) tanim.program.haftalikGunSayisi; ++index1)
        {
          for (int index2 = 0; index2 < (int) tanim.program.gunlukDersSaatiSayisi; ++index2)
          {
            List<araclar.dersYerlesim> dersYerlesimList2 = new List<araclar.dersYerlesim>();
            foreach (araclar.dersYerlesim dersYerlesim in dersYerlesimList1)
            {
              if (dersYerlesim.gun == index1 && dersYerlesim.saat == index2)
                dersYerlesimList2.Add(dersYerlesim);
            }
            int count = dersYerlesimList2.Count;
            Rectangle rectangle = rectangleArray3[index1, index2];
            Rectangle[] rectangleArray4 = new Rectangle[count];
            Font font6 = new Font("Arial", (float) (8 - count), FontStyle.Regular);
            Font font7 = new Font("Arial", (float) (10 - count), FontStyle.Bold);
            for (int index3 = 0; index3 < dersYerlesimList2.Count; ++index3)
            {
              rectangleArray4[index3] = new Rectangle(rectangle.X, rectangle.Y + index3 * rectangle.Height / count, rectangle.Width, rectangle.Height / count);
              gr.DrawRectangle(pen2, rectangleArray4[index3]);
              gr.DrawString(dersYerlesimList2[index3].sinifGruplar, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format2);
              gr.DrawString(dersYerlesimList2[index3].dersAdi, font7, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format1);
              gr.DrawString(dersYerlesimList2[index3].ogretmenler, font6, (Brush) solidBrush, (RectangleF) rectangleArray4[index3], format4);
            }
          }
        }
      }
      string str = "";
      foreach (bilesenTanimliDers bilesenTanimliDers in tanim.program.tanimliDersler)
      {
        if (bilesenTanimliDers.aktifYerlesim == null)
          str = str + "[" + bilesenTanimliDers.aciklama + "]";
      }
      foreach (Rectangle rect2 in rectangleList)
        gr.DrawRectangle(new Pen(Color.Black), rect2);
      gr.ResetTransform();
    }

    public static bool catalmi(bilesenTanimliDers d1, bilesenTanimliDers d2)
    {
      foreach (bilesenOgretmen bilesenOgretmen1 in d1.ogretmenler)
      {
        foreach (bilesenOgretmen bilesenOgretmen2 in d2.ogretmenler)
        {
          if (bilesenOgretmen1 == bilesenOgretmen2)
            return false;
        }
      }
      foreach (bilesenDerslik bilesenDerslik1 in d1.derslikler)
      {
        foreach (bilesenDerslik bilesenDerslik2 in d2.derslikler)
        {
          if (bilesenDerslik1 == bilesenDerslik2)
            return false;
        }
      }
      foreach (bilesenSinifGrup bilesenSinifGrup1 in d1.sinifGruplar)
      {
        foreach (bilesenSinifGrup bilesenSinifGrup2 in d2.sinifGruplar)
        {
          if (bilesenSinifGrup1.sinif == bilesenSinifGrup2.sinif && bilesenSinifGrup1.grup == bilesenSinifGrup2.grup)
            return false;
        }
      }
      return true;
    }

    public static byte[] Zip(string str)
    {
      byte[] bytes = araclar.GetBytes(str);
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (GZipStream gzipStream = new GZipStream((Stream) memoryStream, CompressionMode.Compress, true))
          gzipStream.Write(bytes, 0, bytes.Length);
        return memoryStream.ToArray();
      }
    }

    public static string unZip(byte[] gzip)
    {
      using (GZipStream gzipStream = new GZipStream((Stream) new MemoryStream(gzip), CompressionMode.Decompress))
      {
        byte[] buffer = new byte[4096];
        using (MemoryStream memoryStream = new MemoryStream())
        {
          int count;
          do
          {
            count = gzipStream.Read(buffer, 0, 4096);
            if (count > 0)
              memoryStream.Write(buffer, 0, count);
          }
          while (count > 0);
          return araclar.GetString(memoryStream.ToArray());
        }
      }
    }

    public static byte[] GetBytes(string str)
    {
      byte[] numArray = new byte[str.Length * 2];
      Buffer.BlockCopy((Array) str.ToCharArray(), 0, (Array) numArray, 0, numArray.Length);
      return numArray;
    }

    public static string GetString(byte[] bytes)
    {
      char[] chArray = new char[bytes.Length / 2];
      Buffer.BlockCopy((Array) bytes, 0, (Array) chArray, 0, bytes.Length);
      return new string(chArray);
    }

    private class dersYerlesim
    {
      public int gun;
      public int saat;
      public string dersAdi = "";
      public string ogretmenler = "";
      public string derslikler = "";
      public string sinifGruplar = "";
      public string grupAdi = "";
    }
  }
}
