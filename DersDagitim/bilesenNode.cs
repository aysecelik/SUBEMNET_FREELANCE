using System;
using System.Collections.Generic;

namespace DersDagitim
{
  public class bilesenNode
  {
    public ushort id;
    public bilesenTanimliDers tanimliDers;
    public ushort tSaat;
    public ushort yerlesimGun;
    public ushort yerlesimSaat;
    public bilesenNode.yerlesimYeri[] yerlesimYerleri;

    public ushort toplamYerlesim => Convert.ToUInt16(this.yerlesimYerleri.Length);

    public void hesapla(bool[,] kosul) => this.yerlesimYerleri = this.olasiliklariHesapla(kosul);

    public bool nodeYerlesirmi(bool[,] kosul, ushort gun, ushort saat)
    {
      bool flag = true;
      for (ushort index = 0; (int) index < (int) this.tSaat; ++index)
      {
        if ((int) saat + (int) index < kosul.GetLength(1))
        {
          if (!kosul[(int) gun, (int) saat + (int) index])
            flag = false;
        }
        else
          flag = false;
      }
      return flag;
    }

    private bilesenNode.yerlesimYeri[] olasiliklariHesapla(bool[,] kosul)
    {
      List<bilesenNode.yerlesimYeri> yerlesimYeriList = new List<bilesenNode.yerlesimYeri>();
      for (ushort gun = 0; (int) gun < kosul.GetLength(0); ++gun)
      {
        for (ushort saat = 0; (int) saat < kosul.GetLength(1); ++saat)
        {
          if (this.nodeYerlesirmi(kosul, gun, saat))
            yerlesimYeriList.Add(new bilesenNode.yerlesimYeri()
            {
              gun = gun,
              saat = saat
            });
        }
      }
      return yerlesimYeriList.ToArray();
    }

    public bilesenNode(
      ushort _id,
      bilesenTanimliDers _tanimliDers,
      ushort _toplamSaat,
      ushort _gun = 0,
      ushort _saat = 0)
    {
      this.id = _id;
      this.tanimliDers = _tanimliDers;
      this.tSaat = _toplamSaat;
      this.yerlesimGun = _gun;
      this.yerlesimSaat = _saat;
    }

    public struct yerlesimYeri
    {
      public ushort gun;
      public ushort saat;
    }
  }
}
