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
    public partial class EĞİTİM_DESTEK_TOPLU : Form
    {
        public EĞİTİM_DESTEK_TOPLU()
        {
            InitializeComponent();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EĞİTİM_DESTEK_TOPLU_Load(object sender, EventArgs e)
        {
            comboBox4.Items.Add("ŞUBAT TAKSİDİ");
            comboBox4.Items.Add("HAZİRAN TAKSİDİ");
            comboBox4.Items.Add("KASIM TAKSİDİ");
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());

                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select Sezon from Sezon where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox5.Items.Add(oku4[0].ToString());

            }
            baglan.Close();

            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());

            }
            baglan.Close();

        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> id = new List<int>();
        List<string> tut = new List<string>();
        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            if (string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true)
            {
                MessageBox.Show("SEZON VE TAKSİT BİLGİLERİNİ LÜTFEN SEÇİNİZ.");

            }
            else
            {
                try
                {
                  
                    if (comboBox4.Text == "ŞUBAT TAKSİDİ" && comboBox4.Text == "KASIM TAKSİDİ")
                    {
                        komut = new SqlCommand("Select pm.ID,pm.destek from OgrenciOdeme pm join Ogrenci p on p.ID=pm.OgrId Sube ş.ID=pm.Sube where ş.Okulid='" + okulid + "' and DestekVarYok=" + 1, baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            id.Add((int)oku[0]);
                            tut.Add(string.Format("{0:N}", decimal.Divide(decimal.Multiply((decimal)oku[1], 35), 100)));
                        }
                        baglan.Close();
                        for (int i = 0; i < id.Count; i++)
                        {
                            baglan.Open();
                            komut = new SqlCommand("update set OgrenciOdeme EğitimDestekTaksit2=@a2 where ID=@a1");
                            komut.Parameters.AddWithValue("@p1", tut[i]);
                            komut.Parameters.AddWithValue("@p2", id[i]);
                            komut.ExecuteNonQuery();
                            baglan.Close();
                        }
                      
                    }
                    if (comboBox4.Text == "HAZİRAN TAKSİDİ")
                    {
                        komut = new SqlCommand("Select pm.ID,pm.destek from OgrenciOdeme pm join Ogrenci p on p.ID=pm.OgrId Sube ş.ID=pm.Sube where ş.Okulid='" + okulid + "' and DestekVarYok=" + 1, baglan);
                        baglan.Open();
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            id.Add((int)oku[0]);
                            tut.Add(string.Format("{0:N}", decimal.Divide(decimal.Multiply((decimal)oku[1], 30), 100)));
                        }
                        baglan.Close();
                        for (int i = 0; i < id.Count; i++)
                        {
                            baglan.Open();
                            komut = new SqlCommand("update set OgrenciOdeme EğitimDestekTaksit2=@a2 where ID=@a1");
                            komut.Parameters.AddWithValue("@p1", tut[i]);
                            komut.Parameters.AddWithValue("@p2", id[i]);
                            komut.ExecuteNonQuery();
                            baglan.Close();
                        }

                    }
                  
                    bool degisken = true;
                    string filtre = "Select pm.ID,ş.ŞubeAdi 'ŞUBE',(p.Adi + p.Soyadi) 'ÖĞRENCİ',p.Devre 'DEVRE',p.Snf 'SINIF',pm.EgitimDestegi 'EĞİTİM DESTEĞİ',EğitimDestekTaksit2 'TAKSİT'";
                  
                    filtre += "from OgrenciOdeme pm join Ogrenci p on p.ID = pm.OgrId join Sube ş on ş.ID = pm.Şube where ş.Okulid = '" + okulid + "' and pm.DestekVarYok = " + 1;
                    if (string.IsNullOrEmpty(comboBox5.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " p.Sezon = '" + comboBox5.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " p.Devre = '" + comboBox1.Text + "'";
                        degisken = true;
                    }

                    if (string.IsNullOrEmpty(comboBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " p.Sınıf = '" + comboBox2.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " AND ";
                        }
                        filtre += " pm.Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                        degisken = true;
                    }


                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "EĞİTİM_DESTEK");
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

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
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
                    pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                    pdfTable.WidthPercentage = 100; // hücre genişliği
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                    pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
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
            catch
            {
                MessageBox.Show("hata");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
