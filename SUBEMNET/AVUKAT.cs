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
    public partial class AVUKAT : Form
    {
        public AVUKAT()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
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
                            for (int j = 0; j < dataGridView1.ColumnCount - 1; j++)
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

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        List<int> gecikmegünü = new List<int>();
        List<int> id = new List<int>();


        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        private void button21_Click(object sender, EventArgs e)
        {
            griddoldur();
        }
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            string filtre = "Select pm.ID,ş.ŞubeAdi 'ŞUBE',ö.ID,(ö.Adi+ ' '+ ö.Soyadi) 'ÖĞRENCİ',v.AdSoyad 'VELİ',v.TCKN 'VELİ TC', v.CepTel 'TELEFON',v.Eposta 'E-POSTA',öd.Miktar 'KAYIT FİYATI', öd.ODENEN ALINAN,pm.Tarih 'TARİH',pm.Açıklama 'AÇIKLAMA' from AvukataVer pm join Ogrenci ö on ö.ID = pm.OgrId join OgrenciOdeme öd on öd.OgrId=ö.ID join Veli v on v.OgrId=ö.ID join Sube ş on ş.ID = pm.ŞUBE where ş.Okulid = '" + okulid + "' and v.VeliMi=1";
            try
            {
                bool degisken = true;

                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ş.ŞubeAdi=" + comboBox3.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " pm.Avukat=" + comboBox5.Text;
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Adi=" + textBox6.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Soyadi=" + textBox7.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " v.TCKN=" + textBox3.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.SozNo=" + textBox5.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " v.AdSoyad LİKE '%" + textBox8.Text + "%'";
                    degisken = true;
                }


                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " pm.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖDEME");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "AVUKATA VER";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                panel2.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount-1)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "AVUKAT İŞLEMİ SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID NUMARASINA SAHİP AVUKAT İŞLEMİNİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "AVUKAT İŞLEMİ SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[1].Value.ToString());
                            string sql = "DELETE  FROM AvukataVer WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", id);
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            griddoldur();
                        }
                        catch (Exception a)
                        {
                            baglan.Close();
                            MessageBox.Show(a.ToString());
                        }

                    }
                }
            }
        }

        private void AVUKAT_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {


                comboBox3.Items.Add(oku[0].ToString());

                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='AVUKAT'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox5.Items.Add(oku2[0].ToString());
            }
            baglan.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
