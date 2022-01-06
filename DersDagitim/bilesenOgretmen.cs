
namespace DersDagitim
{
  public class bilesenOgretmen : bilesenTaban
  {
    public int yerlesmemeSayisi;

    public bilesenOgretmen(ushort _id, bool[,] _kosul, string _adisoyadi, string _kisaAdi)
    {
      this.id = _id;
      this.kosul = _kosul;
      this.adi = _adisoyadi;
      this.kisaAdi = _kisaAdi;
    }
  }
}
