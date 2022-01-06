
namespace DersDagitim
{
  public class bilesenDers : bilesenTaban
  {
    public bilesenDers(ushort _id, bool[,] _kosul, string _adi, string _kisaAdi)
    {
      this.id = _id;
      this.kosul = _kosul;
      this.adi = _adi;
      this.kisaAdi = _kisaAdi;
    }
  }
}
