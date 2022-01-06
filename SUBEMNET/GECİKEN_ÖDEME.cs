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
    public partial class GECİKEN_ÖDEME : Form
    {
        public GECİKEN_ÖDEME()
        {
            InitializeComponent();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                panel2.Visible = true;
                panel1.Visible = true;
                panel5.Visible = true;
                comboBox3.Items.Clear();
                richTextBox2.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " ADLI ÖĞRENCİNİN\n KAYIT FİYATI :" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString() + "\n ÖDENEN: "+ dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[8].Value.ToString();
                komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.ŞubeAdi='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "' and p.Pozisyon='AVUKAT'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox3.Items.Add(oku[0].ToString());
                }
                baglan.Close();
            }
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
            id.Clear();
            gecikmegünü.Clear();
            komut = new SqlCommand("Select pm.ID,pm.SonÖdemeGünü from ÖdemePlanı pm join Ogrenci p on p.ID=pm.Öğrenci join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and pm.ÖdendiDurum='ÖDENMEDİ'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                id.Add((int)oku[0]);
                TimeSpan kalangün = DateTime.Now.Date - Convert.ToDateTime(oku[1]);
                gecikmegünü.Add(Convert.ToInt32(kalangün.TotalDays));
            }
            for (int i = 0; i < id.Count; i++)
            {
                SqlCommand komutgüncelle = new SqlCommand("update ÖdemePlanı set Gecikme=@p1 where ID='" + id[i] + "'", baglan);
                komutgüncelle.Parameters.AddWithValue("@p1", gecikmegünü[i]);

            }
            baglan.Close();
          
                dataGridView1.Columns.Clear();
                string filtre = "Select pm.ID,ş.ŞubeAdi 'ŞUBE',ö.ID 'ÖĞRENCİ ID',(ö.Adi+ ' '+ ö.Soyadi) 'ÖĞRENCİ',ö.Devre 'DEVRE',ö.Snf 'SINIF',ay.SonÖdemeGünü 'SON ÖDEME TARİH',pm.Miktar 'KAYIT FİYATI', pm.ODENEN ALINAN,pm.TOPLAM, pm.KALAN from OgrenciOdeme pm join Ogrenci ö on ö.ID = pm.OgrId join ÖdemePlanı ay on ay.Öğrenci=ö.ID join Sube ş on ş.ID = pm.ŞUBE where ş.Okulid = '" + okulid + "' and pm.tutar-pm.alınan>0 ";
                try
                {
                    bool degisken = true;

                    if (string.IsNullOrEmpty(comboBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ş.ŞubeAdi='" + comboBox2.Text+"'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                    filtre += " ö.Sezon='" + comboBox1.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Program='" + comboBox6.Text+"'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Adi='" + textBox7.Text+"'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox9.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Soyadi='" +textBox9.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Snf='" + comboBox7.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(comboBox5.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Devre='" + comboBox5.Text + "'";
                        degisken = true;
                    }
                 
                    if (string.IsNullOrEmpty(textBox8.Text) == false )
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.TaksitSayisi>" + textBox8.Text ;
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.TaksitSayisi<" + textBox6.Text;
                        degisken = true;
                    }
                    if (numericUpDown1.Value!=0)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ay.Gecikme=" + numericUpDown1.Value;
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox5.Text) == false || string.IsNullOrEmpty(textBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.tutar-pm.alınan > " + textBox5.Text + '.' + textBox3.Text;
                        degisken = true;
                    }
                    if (checkBox9.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.Vade between '" + dateTimePicker8.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker7.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (checkBox7.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ay.SonÖdemeGünü between '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                  
                    if (checkBox3.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Durum='" + 0 + "'";
                        degisken = true;
                    }
            
                    if (checkBox5.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Durum='" + 1 + "'";
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
                    MessageBox.Show(a.ToString());
                }
            
           
            
        }

        private void GECİKEN_ÖDEME_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.okulid='" + okulid+ "' and p.Pozisyon='AVUKAT'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox4.Items.Add(oku[0].ToString());
            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku9 = komut.ExecuteReader();
            while (oku9.Read())
            {
                comboBox5.Items.Add(oku9[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox7.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox6.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox2.Items.Add(oku4[0].ToString());
                subeid.Add((int)oku4[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select Sezon from SEZON where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                comboBox1.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into  AvukataVer (Avukat,Öğrenci,Tarih,Açıklama) values (@p1, @p2, @p3, @p4)", baglan);
                komutkaydet.Parameters.AddWithValue("@p1", comboBox3.Text);
                komutkaydet.Parameters.AddWithValue("@p2", dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString());
                komutkaydet.Parameters.AddWithValue("@p3", DateTime.Now.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@p4", richTextBox3.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                dateTimePicker3.Value = DateTime.Now;
                dateTimePicker4.Value = DateTime.Now;
              comboBox3.Text = "";
                richTextBox3.Text = "";

            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
    }
}
