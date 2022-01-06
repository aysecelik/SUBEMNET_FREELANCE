
namespace DersDagitim
{
  public class bilesenSinifGrup
  {
    public bilesenSinif sinif;
    public bilesenGrup grup;

    public bilesenSinifGrup(bilesenSinif _sinif, ushort _grupId)
    {
      this.sinif = _sinif;
      this.grup = this.sinif.grupGetir(_grupId);
    }
  }
}
