using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class FİNANS_RAPORU : Form
    {
        public FİNANS_RAPORU()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        PdfPTable pdfTable13;
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {

                iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 100; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable2 = new PdfPTable(dataGridView2.ColumnCount);
                pdfTable2.SpacingBefore = 20f;
                pdfTable2.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable2.WidthPercentage = 100; // hücre genişliği
                pdfTable2.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable2.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable3 = new PdfPTable(dataGridView10.ColumnCount);
                pdfTable3.SpacingBefore = 20f;
                pdfTable3.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable3.WidthPercentage = 100; // hücre genişliği
                pdfTable3.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable3.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                PdfPTable pdfTable4 = new PdfPTable(dataGridView3.ColumnCount);
                pdfTable4.SpacingBefore = 20f;
                pdfTable4.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable4.WidthPercentage = 100; // hücre genişliği
                pdfTable4.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable4.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable5 = new PdfPTable(dataGridView4.ColumnCount);
                pdfTable5.SpacingBefore = 20f;
                pdfTable5.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable5.WidthPercentage = 100; // hücre genişliği
                pdfTable5.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable5.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable6 = new PdfPTable(dataGridView5.ColumnCount);
                pdfTable6.SpacingBefore = 20f;
                pdfTable6.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable6.WidthPercentage = 100; // hücre genişliği
                pdfTable6.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable6.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable7 = new PdfPTable(dataGridView9.ColumnCount);
                pdfTable7.SpacingBefore = 20f;
                pdfTable7.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable7.WidthPercentage = 100; // hücre genişliği
                pdfTable7.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable7.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı

                PdfPTable pdfTable9 = new PdfPTable(dataGridView8.ColumnCount);
                pdfTable9.SpacingBefore = 20f;
                pdfTable9.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable9.WidthPercentage = 100; // hücre genişliği
                pdfTable9.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable9.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                PdfPTable pdfTable10 = new PdfPTable(dataGridView11.ColumnCount);
                pdfTable10.SpacingBefore = 20f;
                pdfTable10.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable10.WidthPercentage = 100; // hücre genişliği
                pdfTable10.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable10.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                PdfPTable pdfTable11 = new PdfPTable(dataGridView7.ColumnCount);
                pdfTable11.SpacingBefore = 20f;
                pdfTable11.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable11.WidthPercentage = 100; // hücre genişliği
                pdfTable11.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable11.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                PdfPTable pdfTable12 = new PdfPTable(dataGridView6.ColumnCount);
                pdfTable12.SpacingBefore = 20f;
                pdfTable12.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable12.WidthPercentage = 100; // hücre genişliği
                pdfTable12.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable12.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                if (comboBox3.Text == "GİDER KALEMİ")
                {
                    pdfTable13 = new PdfPTable(dataGridView12.ColumnCount);
                    pdfTable13.SpacingBefore = 20f;
                    pdfTable13.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                    pdfTable13.WidthPercentage = 100; // hücre genişliği
                    pdfTable13.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                    pdfTable13.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                }
                try
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            pdfTable.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView1.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }






                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView2.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable2.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView2.ColumnCount; j++)
                        {
                            pdfTable2.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView2.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }



                    for (int i = 0; i < dataGridView10.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView10.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable3.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView10.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView10.ColumnCount; j++)
                        {
                            pdfTable3.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView10.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }




                    for (int i = 0; i < dataGridView3.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView3.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable4.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView3.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView3.ColumnCount; j++)
                        {
                            pdfTable4.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView3.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }



                    for (int i = 0; i < dataGridView4.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView4.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable5.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView4.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView4.ColumnCount; j++)
                        {
                            pdfTable5.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView4.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }



                    for (int i = 0; i < dataGridView5.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView5.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable6.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView5.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView5.ColumnCount; j++)
                        {
                            pdfTable6.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView5.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }



                    for (int i = 0; i < dataGridView9.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView9.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable7.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView9.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView9.ColumnCount; j++)
                        {
                            pdfTable7.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView9.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }







                    for (int i = 0; i < dataGridView8.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView8.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable9.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView8.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView8.ColumnCount; j++)
                        {
                            pdfTable9.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView8.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }

                    for (int i = 0; i < dataGridView11.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView11.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable10.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView11.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView11.ColumnCount; j++)
                        {
                            pdfTable10.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView11.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }

                    for (int i = 0; i < dataGridView7.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView7.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable11.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView7.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView7.ColumnCount; j++)
                        {
                            pdfTable11.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView7.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }

                    for (int i = 0; i < dataGridView6.ColumnCount; i++)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(dataGridView6.Columns[i].HeaderText, fontTitle));
                        cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                        pdfTable12.AddCell(cell);
                    }

                    for (int i = 0; i < dataGridView6.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView6.ColumnCount; j++)
                        {
                            pdfTable12.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView6.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                        }
                    }
                    if (comboBox3.Text == "GİDER KALEMİ")
                    {
                        for (int i = 0; i < dataGridView12.ColumnCount; i++)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(dataGridView12.Columns[i].HeaderText, fontTitle));
                            cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                            pdfTable13.AddCell(cell);
                        }

                        for (int i = 0; i < dataGridView12.RowCount; i++)
                        {
                            for (int j = 0; j < dataGridView12.ColumnCount; j++)
                            {
                                pdfTable13.AddCell(new Phrase(string.Format("{0:N}", Convert.ToDecimal(dataGridView12.Rows[i].Cells[j].Value.ToString()), fontTitle)));

                            }
                        }
                    }


                }
                catch (NullReferenceException)
                {
                }
                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);// sayfa boyutu.
                    PdfWriter.GetInstance(pdfDoc, stream);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 20, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font regularFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 15, iTextSharp.text.Font.NORMAL);
                    Paragraph title;
                    Paragraph text;
                    title = new Paragraph(textBox1.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox4.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox2.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph("GELİRLER", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    title = new Paragraph("ÖĞRENCİ TAKSİTLERİ", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable);
                    title = new Paragraph("EĞİTİM DESTEK", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable2);
                    title = new Paragraph("ÖN ÖDEME", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable3);
                    title = new Paragraph("EKSTRA ÖĞRENCİ GELİR", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable4);
                    title = new Paragraph("KESİNTİ", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable5);
                    title = new Paragraph(tabPage11.Text, titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable6);
                    title = new Paragraph("GİDERLER", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    title = new Paragraph(tabPage3.Text, titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable7);
                    title = new Paragraph("ÖDENMİŞ MAAŞLAR", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable9);
                    title = new Paragraph("ÖDENMEMİŞ MAAŞLAR", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable10);
                    title = new Paragraph("PRİM", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable11);
                    title = new Paragraph("ÖĞRENCİ KAYIT İADE", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable12);
                    if (comboBox3.Text == "GİDER KALEMİ")
                    {
                        title = new Paragraph("BORÇLAR", titleFont);
                        title.Alignment = Element.ALIGN_LEFT;
                        pdfDoc.Add(title);
                        pdfDoc.Add(pdfTable13);
                    }
                    text = new Paragraph("NOT: " + richTextBox1.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel2.Visible = false;
            }

        }
        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        int okulid = Form1.okulid;
        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void FİNANS_RAPORU_Load(object sender, EventArgs e)
        {
            comboBox3.Text = "TEDARİKÇİLER";
            comboBox3.Items.Add("TEDARİKÇİLER");
            comboBox1.Items.Add("GİDER KALEMİ");
            comboBox1.Items.Add("MÜŞTERİLER");

            comboBox3.Items.Add("DİĞER GELİR KALEMİ");



            comboBox1.Text = "DİĞER GELİR KALEMİ";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //arama işlemi
            try
            {

                bool degisken = true;
                string filtreönödeme = "Select SUM(öd.Ödeme) ALACAK,Sum(öd.Ödeme) TAHSLİAT,sum(öd.ÖDEME)-SUM(öd.ÖDEME) KALAN from ÖğrenciÖdemeDetay öd join Ogrenci ö on ö.ID=öd.Öğrenci join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' and öd.Tür='ÖN ÖDEME'";
                string filtreeğitimödemesi = "Select SUM(pm.tutar) ALACAK,Sum(pm.alınan) TAHSLİAT,sum(pm.tutar)-SUM(pm.alınan) KALAN from ÖğrenciÖdemeDetay öd join Ogrenci ö on ö.ID=öd.Öğrenci join OgrenciOdeme pm on pm.OgrId=ö.ID join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "'";
                string filtreeğitimdestek = "Select SUM(pm.destek) ALACAK,Sum(pm.EğitimDestektutar) TAHSLİAT,sum(pm.destek)-SUM(pm.EğitimDestektutar) KALAN from ÖğrenciÖdemeDetay öd join Ogrenci ö on ö.ID=öd.Öğrenci join OgrenciOdeme pm on pm.OgrId=ö.ID join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "'";
                string filtreücretlifaaliyet = "Select SUM(pm.tutar) ALACAK,Sum(pm.alınan) TAHSLİAT,sum(pm.tutar)-SUM(pm.alınan) KALAN from Faaliyet pm join Ogrenci ö on ö.ID=pm.Öğrenci join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' group by Faaliyet";
                string filtrekesinti = "Select SUM(pm.tutar) ALACAK,Sum(pm.tutar) TAHSLİAT,sum(pm.tutar)-SUM(pm.tutar) KALAN from PrimKesinti pm join Personeller ö on ö.ID=pm.Personel join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' and pm.PrimKesinti='KESİNTİ'";
                string filtrediğergelirler = " from DiğerGelirler pm join Müşteri m on m.ID=pm.Müşteri join Sube ş on ş.ID=m.sube where ş.okulid='" + okulid + "'";
                string filtregider = " from Giderler pm join Tedarikçiler m on m.ID=pm.Tedarikçi join Sube ş on ş.ID=m.Sube where ş.okulid='" + okulid + "'";
                string filtreprim = "Select SUM(pm.tutar) VERECEK,Sum(pm.tutar) VERİLEN,sum(pm.tutar)-SUM(pm.tutar) KALAN from PrimKesinti pm join Personeller ö on ö.ID=pm.Personel join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' and pm.PrimKesinti='PRİM'";
                string filtremaaş = "Select SUM(pm.tutar) VERECEK,Sum(pm.tutar) VERİLEN,sum(pm.tutar)-SUM(pm.tutar) KALAN from PersonelMaaş pm join Personeller ö on ö.ID=pm.Personel join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' and ÖdemeDurum='" + Convert.ToBoolean(true) + "'";
                string filtremaaş2 = "Select SUM(pm.tutar) VERECEK,sum(pm.tutar)-SUM(pm.tutar) VERİLEN,Sum(pm.tutar) KALAN from PersonelMaaş pm join Personeller ö on ö.ID=pm.Personel join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "' and ÖdemeDurum='" + Convert.ToBoolean(false) + "'";
                string filtrekayitiade = "Select SUM(pm.tutar) VERECEK,sum(pm.alınan) VERİLEN,Sum(pm.tutar)-SUM(pm.alınan) KALAN from Kayıtiade pm join Ogrenci ö on ö.ID=pm.Öğrenci join Sube ş on ş.ID=ö.Sube where ş.okulid='" + okulid + "'";
                string filtreborç = " from Borçlar pm join Tedarikçiler m on m.ID=pm.Tedarikçi join Sube ş on ş.ID=m.Sube where ş.okulid='" + okulid + "'";




                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtreeğitimdestek += " AND ";
                        filtreeğitimödemesi += " AND ";
                        filtreönödeme += " AND ";
                        filtreücretlifaaliyet += " AND ";
                        filtrekesinti += " AND ";
                        filtrediğergelirler += " AND ";
                        filtregider += " AND ";
                        filtreprim += " AND ";
                        filtremaaş += " AND ";
                        filtremaaş2 += " AND ";
                        filtrekayitiade += " AND ";
                        filtreborç += " AND ";


                    }
                    filtreeğitimdestek += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtreeğitimödemesi += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtreönödeme += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtrekesinti += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtreücretlifaaliyet += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtrediğergelirler += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtregider += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtreprim += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtremaaş += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtremaaş2 += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtrekayitiade += " ş.ŞubeAdi = '" + comboBox2.Text + "'";
                    filtreborç += " ş.ŞubeAdi = '" + comboBox2.Text + "'";






                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtreeğitimdestek += " AND ";
                        filtreeğitimödemesi += " AND ";
                        filtreönödeme += " AND ";
                        filtreücretlifaaliyet += " AND ";
                        filtrekesinti += " AND ";
                        filtrediğergelirler += " AND ";
                        filtregider += " AND ";
                        filtreprim += " AND ";
                        filtremaaş += " AND ";
                        filtremaaş2 += " AND ";
                        filtrekayitiade += " AND ";
                        filtreborç += " AND ";


                    }
                    filtreeğitimdestek += " öd.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtreeğitimödemesi += " öd.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtreönödeme += " öd.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtreücretlifaaliyet += " pm.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtrekesinti += " pm.Vade between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtrediğergelirler += " pm.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtregider += " pm.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtreprim += " pm.Vade between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtremaaş += " pm.Ayyıl between '" + dateTimePicker1.Value.Year + " " + dateTimePicker1.Value.Month.ToString("MMMM") + "' and '" + dateTimePicker2.Value.Year + " " + dateTimePicker2.Value.Month.ToString("MMMM") + "'";
                    filtremaaş2 += " pm.Ayyıl between '" + dateTimePicker1.Value.Year + " " + dateTimePicker1.Value.Month.ToString("MMMM") + "' and '" + dateTimePicker2.Value.Year + " " + dateTimePicker2.Value.Month.ToString("MMMM") + "'";
                    filtrekayitiade += " pm.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtreborç += " pm.TARİH between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";

                    degisken = true;
                }
                if (comboBox1.Text == "DİĞER GELİR KALEMİ")
                {
                    tabPage11.Text = "DİĞER GELİRLER";
                    filtrediğergelirler += " group by pm.DiğerGelirKalemi";
                    filtrediğergelirler = "Select pm.DiğerGelirKalemi 'DİĞER GELİR KALEMİ',SUM(pm.tutar) ALACAK,Sum(pm.alınan) TAHSLİAT,sum(pm.tutar)-SUM(pm.alınan) KALAN " + filtrediğergelirler;
                }
                if (comboBox1.Text == "MÜŞTERİLER")
                {
                    tabPage11.Text = "MÜŞTERİLER";

                    filtrediğergelirler += " group by pm.Müşteri";
                    filtrediğergelirler = "Select m.AdSoyad MÜŞTERİ,SUM(pm.tutar) ALACAK,Sum(pm.alınan) TAHSLİAT,sum(pm.tutar)-SUM(pm.alınan) KALAN " + filtrediğergelirler;
                }
                filtreborç += " group by pm.GiderKalemi";
                filtreborç = "Select pm.GiderKalemi 'GİDER KALEMİ',SUM(pm.tutar) VERECEK,Sum(pm.ödenen) VERİLEN,sum(pm.tutar)-SUM(pm.ödenen) KALAN " + filtreborç;

                if (comboBox3.Text == "GİDER KALEMİ")
                {

                    tabPage3.Text = "GİDERLER";
                    filtregider += " group by pm.GiderKalemi";

                    filtregider = "Select pm.GiderKalemi 'GİDER KALEMİ',SUM(pm.tutar) VERECEK,Sum(pm.ödenen) VERİLEN,sum(pm.tutar)-SUM(pm.ödenen) KALAN " + filtregider;

                }
                if (comboBox3.Text == "TEDARİKÇİLER")
                {
                    tabControl2.TabPages.Remove(tabPage6);
                    tabPage3.Text = "TEDARİKÇİLER";

                    filtregider += " group by pm.Tedarikçi";
                    filtregider = "Select MAX(m.Tedarikçi) TEDARİKÇİ,SUM(pm.tutar) VERECEK,Sum(pm.ödenen) VERİLEN,sum(pm.tutar)-SUM(pm.ödenen) KALAN " + filtregider;
                }

                baglan.Open();
                da = new SqlDataAdapter(filtreeğitimödemesi, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtreeğitimdestek, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtreönödeme, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView10.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtrediğergelirler, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView5.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtrekesinti, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView4.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtreücretlifaaliyet, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView3.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtregider, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView9.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtreprim, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView7.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtremaaş, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView8.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtremaaş2, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView11.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                baglan.Open();
                da = new SqlDataAdapter(filtrekayitiade, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView6.DataSource = ds.Tables[0];
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter(filtreborç, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ_ÖDEMELELERİ");
                dataGridView12.DataSource = ds.Tables[0];
                baglan.Close();

                panel2.Visible = false;


            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = panel1.Visible = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text == "TEDARİKÇİLER")
                tabControl2.TabPages.Remove(tabPage6);
            else
            {
                if (!tabControl2.Controls.Contains(tabPage6))
                    tabControl2.TabPages.Add(tabPage6);


            }
        }
    }
}
