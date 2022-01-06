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
    public partial class İNDİRİMLİLER_LİSTE : Form
    {
        public İNDİRİMLİLER_LİSTE()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        int okulid = Form1.okulid;
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select g.ID,ş.ŞubeAdi 'ŞUBE',m.Adi + m.Soyadi 'ÖĞRENCİ',m.Snf 'SINIF',m.Devre 'DEVRE', g.İndirimTürü 'İNDİRİM TÜRÜ', g.İndirimMiktarı 'İNDİRİM MİKTARI',g.İndirimNedeni 'İNDİRİM NEDENİ'  from İndirimliler g join Sube ş on ş.ID=g.Şube join Ogrenci m on g.Öğrenci=m.ID   where (m.Adi+ m.Soyadi)='" + textBox6.Text + "' and ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "İNDİRİMLİLER");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

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

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount ; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
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
                panel1.Visible = false;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = true;
                string filtre = "Select g.ID,ş.ŞubeAdi,m.Adi + m.Soyadi 'ÖĞRENCİ',M.Snf 'SINIF',m.Devre 'DEVRE', g.İndirimTürü 'İNDİRİM TÜRÜ', g.İndirimMiktarı 'İNDİRİM MİKTARI',g.İndirimNedeni 'İNDİRİM NEDENİ'  from İndirimliler g join Sube ş on ş.ID=g.ŞUBE join Ogrenci m on g.Öğrenci=m.ID   where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {

                    filtre += " AND ";

                    filtre += " ş.ŞubeAdi=" + "'" + comboBox5.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Program=" + "'" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " g.İndirimTürü=" + "'" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Devre=" + "'" + comboBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.Snf=" + "'" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " g.İndirimNedeni=" + "'" + comboBox6.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " m.KayitTarihi between " + "'" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text) == false || textBox1.Text!="AD")
                {

                    filtre += " AND ";

                    filtre += " m.Adi=" + "'" + textBox1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false || textBox3.Text != "SOYAD")
                {

                    filtre += " AND ";

                    filtre += " m.Soyadi=" + "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (checkBox2.Checked==true)
                {

                    filtre += " AND ";

                    filtre += " m.Durum=" + "'" + 1 + "'";
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {

                    filtre += " AND ";

                    filtre += " m.Durum=" + "'" + 0 + "'";
                    degisken = true;
                }



                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "İNDİRİMLİLER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();            
                panel1.Visible = false;




            }

            catch (Exception A)
            {
                baglan.Close();
                MessageBox.Show(A.ToString());
            }
        }

        private void İNDİRİMLİLER_LİSTE_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("ÖN KAYITA ÖZEL İNDİRİMLİ");
            comboBox1.Items.Add("ÖZEL İNDİRİMLİ");
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox4.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox2.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi From Sube Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox5.Items.Add(oku4[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select distinct i.İndirimNedeni From İndirimliler i join Sube on ş.ID=i.Şube ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                comboBox6.Items.Add(oku5[0].ToString());

            }
            baglan.Close();
        }
    }
}
