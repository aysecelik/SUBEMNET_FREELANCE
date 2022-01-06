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
    public partial class KİTAPLAR_RAPOR : Form
    {
        public KİTAPLAR_RAPOR()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        private void KİTAPLAR_RAPOR_Load(object sender, EventArgs e)
        {
            comboBox18.Items.Add("EN ÇOK OKUNAN KİTAP");
            comboBox18.Items.Add("EN ÇOK OKUNAN YAZAR");
            comboBox18.Items.Add("EN ÇOK OKUNAN YAYINEVİ");
            comboBox18.Items.Add("EN ÇOK OKUNAN TÜR");
            comboBox18.Items.Add("EN ÇOK OKUNAN KİTAP (KİTAP TÜRÜNE GÖRE)");
            comboBox18.Items.Add("EN ÇOK OKUNAN YAZAR (KİTAP TÜRÜNE GÖRE)");
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox21.Items.Add(oku3[0].ToString());

                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select k.Kütüphane,k.ID from Kütüphane k join Sube ş on ş.ID=k.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox22.Items.Add(oku2[0].ToString());

            }
            baglan.Close();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible=false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //YAZDIRMA İŞLEMLERİ
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount );

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount ; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontTitle));

                        }
                    }


                }
                catch (NullReferenceException)
                {
                }

                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);// sayfa boyutu.
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
                    pdfDoc.Add(pdfTable);
                    text = new Paragraph("NOT: " + richTextBox1.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel2.Visible = false;
            }

        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        List<int> subeid = new List<int>();
        List<int> kütüphane = new List<int>();
        int okulid = Form1.okulid;
        private void button8_Click(object sender, EventArgs e)
        {
           
            if (comboBox18.SelectedIndex == 0 || comboBox18.SelectedIndex == 1 || comboBox18.SelectedIndex == 2 || comboBox18.SelectedIndex == 3)
            {
                try
                {

                    bool degisken = true;
                    string filtre = "Select t.ID,kü.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Tür  TÜR from Kitaplar t join KitapHareketleri k on k.Kitap=t.ID join Sube ş on ş.ID=t.Sube join Kütüphane kÜ on kü.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";


                    if (string.IsNullOrEmpty(comboBox21.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Sube =" + subeid[comboBox21.SelectedIndex];
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox22.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Kütüphane =" + kütüphane[comboBox22.SelectedIndex];
                        degisken = true;
                    }
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Alınma= (SELECT MAX(Alınma) FROM Kitaplar)";

                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "KİTAPLAR");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.Close();
                    panel2.Visible = false;


                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
            if (comboBox18.SelectedIndex == 4 || comboBox18.SelectedIndex == 5)
            {
                try
                {

                    bool degisken = true;
                    string filtre = "Select t.ID,kü.Kütüphane 'KÜTÜPHANE', t.KitapAdı 'KİTAP ADI',t.Yayınevi 'YAYIN EVİ',t.Yazar 'YAZAR',t.Tür TÜR from Kitaplar t join KitapHareketleri k on k.Kitap=t.ID join Sube ş on ş.ID=t.Sube join Kütüphane kÜ on kü.ID=t.Kütüphane where ş.Okulid='" + okulid + "'";


                    if (string.IsNullOrEmpty(comboBox21.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Sube =" + subeid[comboBox21.SelectedIndex];
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox22.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " t.Kütüphane =" + kütüphane[comboBox22.SelectedIndex];
                        degisken = true;
                    }
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Alınma= (SELECT MAX(Alınma) FROM Kitaplar group by Tür)";

                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "KİTAPLAR");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.Close();
                    panel2.Visible = false;


                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            
        }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
