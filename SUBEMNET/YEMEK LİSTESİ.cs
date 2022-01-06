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
    public partial class YEMEK_LİSTESİ : Form
    {
        public YEMEK_LİSTESİ()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        SqlCommand komut;
        List<int> subeid = new List<int>();
       
        private void YEMEK_LİSTESİ_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("ÖNCELİKLE ŞUBE VE ÖĞÜN SEÇİLMELİDİR.");
            comboBox5.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");


            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox2.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            dataGridView1.RowTemplate.Height = 25;
          

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            komut = new SqlCommand("Select Öğün from Öğün where Sube='" + subeid[comboBox2.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox5.Items.Add(oku[0].ToString());


            }
            baglan.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text)==true)
            {
                MessageBox.Show("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            }
            else
            {
                comboBox1.Items.Clear();
                komut = new SqlCommand("Select distinct ayyıl from Yemeklistesi where Sube='" + subeid[comboBox2.SelectedIndex] + "' and Öğün='"+comboBox5.Text+"'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox1.Items.Add(oku[0].ToString());


                }
                baglan.Close();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "ŞUBE" || comboBox5.Text == "ÖĞÜN" || comboBox5.Text == "YEMEK LİSTESİ" || comboBox5.Text == "ÖNCELİKLE ŞUBE SEÇİLMELİDİR." || comboBox1.Text == "ÖNCELİKLE ŞUBE VE ÖĞÜN SEÇİLMELİDİR." || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true)
            {
                MessageBox.Show("LÜTFEN GÖRÜNTÜLEMEK İSTEDİĞİNİZ YEMEK LİSTESİNİN ÖĞÜN, ŞUBE VE YEMEK LİSTESİ BİLGİLERİNİ GİRİNİZ.");
            }
            else
            {
             
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter("Select t.TARİH, t.PAZARTESİ, t.SALI,t.ÇARŞAMBA, t.PERŞEMBE, t.CUMA, t.CUMARTESİ, t.PAZAR from YemekListesi t join Sube ş on t.Sube=ş.ID where t.Sube='" + subeid[comboBox2.SelectedIndex] + "' and t.Öğün='" + comboBox5.Text + "' and t.ayyıl='" + comboBox1.Text + "' order by t.Tarih", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "YEMEKLER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();

                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "SİL";
                dgvBtn2.Text = "SİL";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                comboBox2.Text = "ŞUBE";
                comboBox5.Text = "ÖĞÜN";
                comboBox5.Text = "YEMEK LİSTESİ";

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 1);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 100; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 8; j++)
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
    }
}
