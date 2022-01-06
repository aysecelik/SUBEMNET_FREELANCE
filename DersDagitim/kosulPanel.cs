using System.Drawing;
using System.Windows.Forms;

namespace DersDagitim
{
  public class kosulPanel : Control
  {
    private const int BTNGEN = 70;
    private const int BTNYUK = 30;
    public bool[,] kosullar;
    private int gunSay;
    private int saatSay;
    private int aktifSatir;
    private int aktifSutun;
    private Pen KalinCizgi = new Pen((Brush) new SolidBrush(Color.Black));
    private SolidBrush renkYesil = new SolidBrush(Color.Green);
    private SolidBrush renkSari = new SolidBrush(Color.Yellow);
    private SolidBrush renkSiyah = new SolidBrush(Color.Black);
    private SolidBrush renkKirmizi = new SolidBrush(Color.Red);
    private Font fontYazilar = new Font("Arial", 9f, FontStyle.Bold);
    private StringFormat formatDikeyOrtali = new StringFormat();
    private StringFormat formatYatayDikeyOrtali = new StringFormat();
    private int GEN = 570;
    private int YUK = 330;
    private int secGen;
    private int secYuk;
    private Rectangle[,] recSecimler;
    private Rectangle[] recGunler;
    private Rectangle[] recSaatler;

    public kosulPanel(ref bool[,] kosulGirdi)
    {
      this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer, true);
      this.gunSay = kosulGirdi.GetLength(0);
      this.saatSay = kosulGirdi.GetLength(1);
      this.kosullar = kosulGirdi;
      this.GEN = 70 + this.saatSay * ((this.GEN - 70) / this.saatSay);
      this.YUK = 30 + this.gunSay * ((this.YUK - 30) / this.gunSay);
      this.secGen = (this.GEN - 70) / this.saatSay;
      this.secYuk = (this.YUK - 30) / this.gunSay;
      this.recGunler = new Rectangle[this.gunSay];
      for (int index = 0; index < this.gunSay; ++index)
        this.recGunler[index] = new Rectangle(new Point(0, this.secYuk * index + 30), new Size(70, this.secYuk));
      this.recSaatler = new Rectangle[this.saatSay];
      for (int index = 0; index < this.saatSay; ++index)
        this.recSaatler[index] = new Rectangle(new Point(70 + this.secGen * index, 0), new Size(this.secGen, 30));
      this.recSecimler = new Rectangle[this.gunSay, this.saatSay];
      for (int index1 = 0; index1 < this.gunSay; ++index1)
      {
        for (int index2 = 0; index2 < this.saatSay; ++index2)
          this.recSecimler[index1, index2] = new Rectangle(new Point(index2 * this.secGen + 70, index1 * this.secYuk + 30), new Size(this.secGen, this.secYuk));
      }
      this.Width = this.GEN + 1;
      this.Height = this.YUK + 1;
      this.formatDikeyOrtali.LineAlignment = StringAlignment.Center;
      this.formatYatayDikeyOrtali.LineAlignment = StringAlignment.Center;
      this.formatYatayDikeyOrtali.Alignment = StringAlignment.Center;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      for (int index1 = 0; index1 < this.gunSay; ++index1)
      {
        for (int index2 = 0; index2 < this.saatSay; ++index2)
        {
          Rectangle rect = new Rectangle(new Point(this.recSecimler[index1, index2].X + 3, this.recSecimler[index1, index2].Y + 3), new Size(this.secGen - 6, this.secYuk - 6));
          if (this.kosullar[index1, index2])
            e.Graphics.FillRectangle((Brush) this.renkYesil, rect);
          else
            e.Graphics.FillRectangle((Brush) this.renkKirmizi, rect);
          e.Graphics.DrawRectangle(this.KalinCizgi, this.recSecimler[index1, index2]);
        }
      }
      for (int index = 0; index < this.gunSay; ++index)
      {
        if (index == this.aktifSatir)
          e.Graphics.FillRectangle((Brush) this.renkSari, this.recGunler[index]);
        e.Graphics.DrawRectangle(this.KalinCizgi, this.recGunler[index]);
        e.Graphics.DrawString(tanim.program.gunler[index], this.fontYazilar, (Brush) this.renkSiyah, (RectangleF) this.recGunler[index], this.formatDikeyOrtali);
      }
      for (int index = 0; index < this.saatSay; ++index)
      {
        if (this.aktifSutun == index)
          e.Graphics.FillRectangle((Brush) this.renkSari, this.recSaatler[index]);
        e.Graphics.DrawRectangle(this.KalinCizgi, this.recSaatler[index]);
        e.Graphics.DrawString((index + 1).ToString(), this.fontYazilar, (Brush) this.renkSiyah, (RectangleF) this.recSaatler[index], this.formatYatayDikeyOrtali);
      }
      base.OnPaint(e);
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      this.aktifSatir = (e.Y - 30) / this.secYuk;
      this.aktifSutun = (e.X - 70) / this.secGen;
      if (e.Y > 30 && e.X > 70 && (e.Y < 30 + this.secYuk * this.gunSay && e.X < 70 + this.secGen * this.saatSay))
        this.kosullar[this.aktifSatir, this.aktifSutun] = !this.kosullar[this.aktifSatir, this.aktifSutun];
      if (e.Y < 30 && e.X > 70)
      {
        bool flag = !this.kosullar[0, this.aktifSutun];
        for (int index = 0; index < this.gunSay; ++index)
          this.kosullar[index, this.aktifSutun] = flag;
      }
      if (e.X < 70 && e.Y > 30)
      {
        bool flag = !this.kosullar[this.aktifSatir, 0];
        for (int index = 0; index < this.saatSay; ++index)
          this.kosullar[this.aktifSatir, index] = flag;
      }
      this.Invalidate();
      base.OnMouseClick(e);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      this.aktifSatir = (e.Y - 30) / this.secYuk;
      this.aktifSutun = (e.X - 70) / this.secGen;
      this.Invalidate();
      base.OnMouseMove(e);
    }
  }
}
