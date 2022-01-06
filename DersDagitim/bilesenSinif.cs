
using System.Collections;

namespace DersDagitim
{
  public class bilesenSinif : bilesenTaban
  {
    public ArrayList gruplar;
    public ushort grupIdSon;

    public bilesenSinif(
      ushort _id,
      bool[,] _kosul,
      string _adi,
      string _kisaAdi,
      ArrayList _gruplar,
      ushort _grupIdSon = 1)
    {
      this.id = _id;
      this.kosul = _kosul;
      this.adi = _adi;
      this.kisaAdi = _kisaAdi;
      this.gruplar = _gruplar;
      this.grupIdSon = _grupIdSon;
      if (this.grupGetir((ushort) 0) != null)
        return;
      this.gruplar.Add((object) new bilesenGrup((ushort) 0, "Tüm Sınıf", "Tümü"));
    }

    public bilesenGrup grupGetir(ushort grupId)
    {
      bilesenGrup bilesenGrup1 = (bilesenGrup) null;
      for (int index = 0; index < this.gruplar.Count; ++index)
      {
        bilesenGrup bilesenGrup2 = this.gruplar[index] as bilesenGrup;
        if ((int) bilesenGrup2.id == (int) grupId)
          bilesenGrup1 = bilesenGrup2;
      }
      return bilesenGrup1;
    }
  }
}
