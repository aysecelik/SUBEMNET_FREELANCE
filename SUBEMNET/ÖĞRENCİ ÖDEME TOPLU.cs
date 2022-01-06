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
    public partial class ÖĞRENCİ_ÖDEME_TOPLU : Form
    {
        public ÖĞRENCİ_ÖDEME_TOPLU()
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

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            griddoldur();
            panel2.Visible = false;
        }
        void griddoldur()
        {
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                try
                {
                    dataGridView1.Columns.Clear();
                    string filtre = "Select pm.ID,ş.ŞubeAdi 'ŞUBE',ö.ID 'ÖĞRENCİ ID',(ö.Adi + ö.Soyadi) 'ÖĞRENCİ',ö.Devre 'DEVRE',ö.Snf 'SINIF', pm.Miktar 'KAYIT FİYATI', pm.ODENEN ALINAN,pm.EgitimDestegi 'EĞİTİM DESTEk',pm.EğitimDestekOdenen 'EĞİTİM DESTEK ALINAN',pm.OnÖdeme 'ÖN ÖDEME',pm.TOPLAM, pm.KALAN from OgrenciOdeme pm join Ogrenci ö on ö.ID = pm.OgrId join Sube ş on ş.ID = pm.ŞUBE where ş.Okulid = '" + okulid + "'";

                    bool degisken = true;

                    if (string.IsNullOrEmpty(comboBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ş.ŞubeAdi='" + comboBox2.Text + "'";
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
                    if (string.IsNullOrEmpty(comboBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Program='" + comboBox6.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false && string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.tutar='" + textBox8.Text + "." + textBox10.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox5.Text) == false && string.IsNullOrEmpty(textBox3.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.kalantutar='" + textBox5.Text + "." + textBox3.Text + "'";
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
                        filtre += " ö.KayitSilinmeTarihi between '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (checkBox6.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.KayitTarihi between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
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
                    dgvBtn.HeaderText = "ÖDEME AL";
                    dgvBtn.Text = "SEÇ";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView1.Columns.Add(dgvBtn);
                    DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
            if (comboBox3.Text == "FAALİYET")
            {
                dataGridView1.Columns.Clear();
                string filtre = "Select MAX(pm.ID) ID,MAX(ş.ŞubeAdi) 'ŞUBE',MAX(ö.ID) 'ÖĞRENCİ ID',MAX((ö.Adi + ö.Soyadi)) 'ÖĞRENCİ',MAX(ö.Devre) 'DEVRE',MAX(ö.Snf) 'SINIF'  from Faaliyet pm join Ogrenci ö on ö.ID = pm.Öğrenci join Sube ş on ş.ID = ö.Sube where ş.Okulid = '" + okulid + "'";
                try
                {
                    bool degisken = true;

                    if (string.IsNullOrEmpty(comboBox2.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ş.ŞubeAdi='" + comboBox2.Text + "'";
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
                    if (string.IsNullOrEmpty(comboBox6.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Program=" + comboBox6.Text;
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false && string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " pm.tutar='" + textBox8.Text + "." + textBox10.Text + "'";
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
                        filtre += " ö.KayitSilinmeTarihi between '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";
                        degisken = true;
                    }
                    if (checkBox6.Checked == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.KayitTarihi between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
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
                    filtre += " group by ö.ID";
                    dataGridView1.Columns.Clear();
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEME");
                    dataGridView1.DataSource = ds.Tables[0];
                    baglan.Close();
                    DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                    dgvBtn.HeaderText = "ÖDEME AL";
                    dgvBtn.Text = "SEÇ";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView1.Columns.Add(dgvBtn);
                    DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                    panel2.Visible = false;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show(a.ToString());
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void ÖĞRENCİ_ÖDEME_TOPLU_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox5.Items.Add(oku[0].ToString());

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
            komut = new SqlCommand("Select Sezon from Sezon where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                comboBox1.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
            comboBox3.Items.Add("TOPLAM (EĞİTİM DESTEKLİ)");
            comboBox3.Items.Add("FAALİYET");
            güncelle();



        }
        List<int> id = new List<int>();
        List<int> ogrid = new List<int>();

        List<decimal> tut = new List<decimal>();
        List<decimal> odenen = new List<decimal>();
        List<decimal> kalantutar = new List<decimal>();
        List<decimal> toplam = new List<decimal>();


        void güncelle()
        {
            if (DateTime.Now.Month == 11)
            {

                id.Clear();
                tut.Clear();
                ogrid.Clear();
                kalantutar.Clear();
                toplam.Clear();
                var baseDate = new DateTime(DateTime.Now.Year, 11, 7);
                var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek + 1);
                if (DateTime.Now == thisWeekStart)
                {
                    komut = new SqlCommand("Select pm.ID,pm.destek,pm.kalantutar,pm.TOPLAM,p.ID from OgrenciOdeme pm join Ogrenci p on p.ID=pm.OgrId join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and pm.DestekVarYok=1 and p.Sezon='" + DateTime.Now.Year + "-" + (DateTime.Now.Year + 1) + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {

                    id.Add((int)oku[0]);
                    ogrid.Add((int)oku[4]);

                    tut.Add(decimal.Divide(decimal.Multiply((decimal)oku[1], 35), 100));
                    kalantutar.Add((decimal)oku[2]);
                    toplam.Add((decimal)oku[1]);


                }
                baglan.Close();

                for (int i = 0; i < id.Count; i++)
                {
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("update OgrenciOdeme set EğitimDestekOdenen=@a1, EğitimDestektutar=@a2,KALAN=@p4,kalantutar=@p5 where ID=@a3", baglan);
                    komut.Parameters.AddWithValue("@a1", string.Format("{0:N}", tut[i]));
                    komut.Parameters.AddWithValue("@a2", tut[i]);
                    komut.Parameters.AddWithValue("@a3", id[i]);
                    komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", kalantutar[i] - (tut[i])));
                    komut.Parameters.AddWithValue("@p5", kalantutar[i] - (tut[i]));

                    komut.ExecuteNonQuery();
                    baglan.Close();


                    baglan.Open();
                    SqlCommand komut2 = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                    komut2.Parameters.AddWithValue("@p1", ogrid[i]);
                    komut2.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                    komut2.Parameters.AddWithValue("@p3", tut[i]);
                    komut2.Parameters.AddWithValue("@p4", string.Format("{0:N}", tut[i]));
                    komut2.Parameters.AddWithValue("@p5", string.Format("{0:N}", toplam[i]));
                    komut2.Parameters.AddWithValue("@p6", "EĞİTİM DESTEK");
                    komut2.Parameters.AddWithValue("@p7", string.Format("{0:N}", toplam[i] - tut[i]));
                    komut2.Parameters.AddWithValue("@p8", toplam[i] - tut[i]);
                    komut2.ExecuteNonQuery();
                    baglan.Close();
                }
                }
            }


            if (DateTime.Now.Month == 2)
            {

                var baseDate = new DateTime(DateTime.Now.Year, 2, 7);
                var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek + 1);
                id.Clear();
                tut.Clear();
                ogrid.Clear();
                kalantutar.Clear();
                toplam.Clear();
                odenen.Clear();
                if (DateTime.Now == thisWeekStart)
                {
                    komut = new SqlCommand("Select pm.ID,pm.destek,pm.EğitimDestektutar,pm.kalantutar,pm.TOPLAM,p.ID from OgrenciOdeme pm join Ogrenci p on p.ID=pm.OgrId join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and pm.DestekVarYok=1 and p.Sezon='" + (DateTime.Now.Year-1 ) + "-" + DateTime.Now.Year + "'", baglan);
                    baglan.Open();
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {

                        odenen.Add((decimal)oku[2]);
                        id.Add((int)oku[0]);
                        ogrid.Add((int)oku[5]);
                        tut.Add(decimal.Divide(decimal.Multiply((decimal)oku[1], 35), 100));
                        kalantutar.Add((decimal)oku[3]);
                        toplam.Add((decimal)oku[1]);

                    }
                    baglan.Close();

                    for (int i = 0; i < id.Count; i++)
                    {
                    MessageBox.Show("geldi");

                    baglan.Open();
                        komut = new SqlCommand("update OgrenciOdeme set EğitimDestekOdenen=@a1, EğitimDestektutar=@a2,KALAN=@p4,kalantutar=@p5 where ID=@a3", baglan);
                        komut.Parameters.AddWithValue("@a1", string.Format("{0:N}", tut[i] + odenen[i]));
                        komut.Parameters.AddWithValue("@a2", tut[i] + odenen[i]);
                        komut.Parameters.AddWithValue("@a3", id[i]);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", kalantutar[i] - (tut[i])));
                        komut.Parameters.AddWithValue("@p5", kalantutar[i] - (tut[i]));
                        komut.ExecuteNonQuery();
                        baglan.Close();
                        baglan.Open();
                        SqlCommand komut2 = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut2.Parameters.AddWithValue("@p1", ogrid[i]);
                        komut2.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut2.Parameters.AddWithValue("@p3", tut[i]);
                        komut2.Parameters.AddWithValue("@p4", string.Format("{0:N}", tut[i]));
                        komut2.Parameters.AddWithValue("@p5", string.Format("{0:N}", toplam[i]));
                        komut2.Parameters.AddWithValue("@p6", "EĞİTİM DESTEK");
                        komut2.Parameters.AddWithValue("@p7", string.Format("{0:N}", toplam[i] - (tut[i] + odenen[i])));
                        komut2.Parameters.AddWithValue("@p8", toplam[i] - (tut[i] + odenen[i]));
                        komut2.ExecuteNonQuery();
                        baglan.Close();
                    }
                }
            }
            if (DateTime.Now.Month == 6)
            {

                var baseDate = new DateTime(DateTime.Now.Year, 6, 7);
                var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek + 1);
                id.Clear();
                tut.Clear();
                ogrid.Clear();
                kalantutar.Clear();
                toplam.Clear();
                odenen.Clear();
                if (DateTime.Now == thisWeekStart)
                {
                    komut = new SqlCommand("Select pm.ID,pm.destek,pm.EğitimDestektutar,pm.kalantutar,pm.TOPLAM,p.ID from OgrenciOdeme pm join Ogrenci p on p.ID=pm.OgrId join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and pm.DestekVarYok=" + 1 + "  and p.Sezon='" + (DateTime.Now.Year - 1) + "-" + DateTime.Now.Year + "'", baglan);
                    baglan.Open();
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {

                        odenen.Add((decimal)oku[2]);
                        id.Add((int)oku[0]);
                        ogrid.Add((int)oku[5]);
                        tut.Add(decimal.Divide(decimal.Multiply((decimal)oku[1], 30), 100));
                        kalantutar.Add((decimal)oku[3]);
                        toplam.Add((decimal)oku[1]);

                    }
                    baglan.Close();

                    for (int i = 0; i < id.Count; i++)
                    {
                        baglan.Open();
                        komut = new SqlCommand("update OgrenciOdeme set EğitimDestekOdenen=@a1, EğitimDestektutar=@a2,KALAN=@p4,kalantutar=@p5 where ID=@a3", baglan);
                        komut.Parameters.AddWithValue("@a1", string.Format("{0:N}", tut[i] + odenen[i]));
                        komut.Parameters.AddWithValue("@a2", tut[i] + odenen[i]);
                        komut.Parameters.AddWithValue("@a3", id[i]);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", kalantutar[i] - (tut[i])));
                        komut.Parameters.AddWithValue("@p5", kalantutar[i] - (tut[i]));
                        komut.ExecuteNonQuery();
                        baglan.Close();
                        baglan.Open();
                        SqlCommand komut2 = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut2.Parameters.AddWithValue("@p1", ogrid[i]);
                        komut2.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut2.Parameters.AddWithValue("@p3", tut[i]);
                        komut2.Parameters.AddWithValue("@p4", string.Format("{0:N}", tut[i]));
                        komut2.Parameters.AddWithValue("@p5", string.Format("{0:N}", toplam[i]));
                        komut2.Parameters.AddWithValue("@p6", "EĞİTİM DESTEK");
                        komut2.Parameters.AddWithValue("@p7", string.Format("{0:N}", toplam[i] - (tut[i] + odenen[i])));
                        komut2.Parameters.AddWithValue("@p8", toplam[i] - (tut[i] + odenen[i]));
                        komut2.ExecuteNonQuery();
                        baglan.Close();
                    }
                }
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
            panel9.Visible = false;

        }
        string öğrenciid;
        int rows;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rows = dataGridView1.CurrentCell.RowIndex;
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                label24.Text = "ÖDEME PLANI";
                dataGridView3.Visible = true;
                dataGridView2.Size = new Size(943, 251);
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
                {
                    öğrenciid = dataGridView1.Rows[rows].Cells[2].Value.ToString();
                    panel2.Visible = true;
                    panel1.Visible = true;
                    panel5.Visible = true;
                    label56.Text = dataGridView1.Rows[rows].Cells[3].Value.ToString() + " ADLI ÖĞRENCİNİN ÖDEME DETAYI";
                    baglan.Open();
                    string ödeme = "";
                    komut = new SqlCommand("Select ÖdemeŞekli from OgrenciOdeme where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {

                        ödeme = oku3[0].ToString();
                    }
                    baglan.Close();
                    if (ödeme == "NAKİT" || ödeme == "BANKA")
                    {
                        panel11.Visible = true;
                        panel12.Visible = false;
                        komut = new SqlCommand("Select tutar,alınan from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            string s = (tutar - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox21.Text = i1.ToString();
                            textBox22.Text = i2.ToString();
                            textBox14.Text = textBox21.Text + "." + textBox22.Text;
                        }
                        baglan.Close();
                    }
                    if (ödeme == "ÇEK" || ödeme == "SENET")
                    {
                        panel11.Visible = true;
                        panel12.Visible = true;
                        panel13.Visible = false;
                        komut = new SqlCommand("Select tutar,alınan from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            string s = (tutar - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox23.Text = i1.ToString();
                            textBox24.Text = i2.ToString();
                            textBox9.Text = textBox23.Text + "." + textBox24.Text;
                        }
                        baglan.Close();
                    }
                    if (ödeme == "VİSA")
                    {
                        komut = new SqlCommand("Select tutar,alınan,TaksitSayisi from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            textBox17.Text = tutar.ToString();
                            string s = (tutar - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox25.Text = i1.ToString();
                            textBox26.Text = i2.ToString();

                            string s1 = decimal.Divide(tutar - c, (decimal)oku[2]).ToString();
                            string[] parts2 = s1.Split(',');
                            int i = Convert.ToInt32(parts2[0]);
                            int i3 = Convert.ToInt32(parts2[1]);
                            textBox20.Text = i.ToString();
                            textBox19.Text = i3.ToString();
                            textBox18.Text = Convert.ToInt32((decimal)oku[2]).ToString();

                        }
                        baglan.Close();
                        panel11.Visible = true;
                        panel12.Visible = true;
                        panel13.Visible = true;
                    }
                    baglan.Close();
                    string filtre = "Select ID,Tarih,TOPLAM,KALAN,ÖDENEN,TÜR from ÖğrenciÖdemeDetay where Öğrenci='" + öğrenciid + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEME");
                    dataGridView2.DataSource = ds.Tables[0];
                    baglan.Close();
                    dataGridView3.Columns.Clear();
                    string filtre2 = "Select ID,SonÖdemeGünü 'SON ÖDEME GÜNÜ',Miktar MİKTAR,ÖdendiDurum 'ÖDEME DURUM' from ÖdemePlanı where Öğrenci='" + öğrenciid + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre2, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEMEPLANI");
                    dataGridView3.DataSource = ds.Tables[0];
                    baglan.Close();



                }
            }
            if (comboBox3.Text == "FAALİYET")
            {
                label24.Text = "";
                dataGridView3.Visible = false;
                dataGridView2.Size = new Size(943, 502);
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
                {
                    panel2.Visible = true;
                    panel1.Visible = true;
                    panel5.Visible = true;
                    label56.Text = dataGridView1.Rows[rows].Cells[3].Value.ToString() + " ADLI ÖĞRENCİNİN FAALİYETLERİ";

                    dataGridView2.Columns.Clear();
                    string filtre = "Select ID,Faaliyet,Tarih,Miktar as TOPLAM,ÖDENEN,SonÖdemeTarihi 'SON ÖDEME TARİHİ' from Faaliyet where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEME");
                    dataGridView2.DataSource = ds.Tables[0];
                    baglan.Close();
                    DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                    dgvBtn.HeaderText = "ÖDEME AL";
                    dgvBtn.Text = "SEÇ";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView2.Columns.Add(dgvBtn);
                    DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                }

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {



        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                öğrenciid = dataGridView1.Rows[rows].Cells[2].Value.ToString();

                label56.Text = dataGridView1.Rows[rows].Cells[3].Value.ToString() + " ADLI ÖĞRENCİNİN ÖDEME DETAYI";
                baglan.Open();
                string ödeme = "";
                komut = new SqlCommand("Select ÖdemeŞekli from OgrenciOdeme where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {

                    ödeme = oku3[0].ToString();
                }
                baglan.Close();
                if (ödeme == "NAKİT" || ödeme == "BANKA")
                {
                    panel11.Visible = true;
                    panel12.Visible = false;
                    komut = new SqlCommand("Select tutar,alınan from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal c;
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        decimal tutar = (decimal)oku[0];
                        if (oku[1] == DBNull.Value)
                        {

                            c = (decimal)0;
                        }
                        else
                        {
                            c = (decimal)oku[1];
                        }
                        string s = (tutar - c).ToString();
                        string[] parts = s.Split(',');
                        int i1 = Convert.ToInt32(parts[0]);
                        int i2 = Convert.ToInt32(parts[1]);
                        textBox21.Text = i1.ToString();
                        textBox22.Text = i2.ToString();
                        textBox14.Text = textBox21.Text + "." + textBox22.Text;
                    }
                    baglan.Close();
                }
                if (ödeme == "ÇEK" || ödeme == "SENET")
                {
                    panel11.Visible = true;
                    panel12.Visible = true;
                    panel13.Visible = false;
                    komut = new SqlCommand("Select tutar,alınan from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal c;
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        decimal tutar = (decimal)oku[0];
                        if (oku[1] == DBNull.Value)
                        {

                            c = (decimal)0;
                        }
                        else
                        {
                            c = (decimal)oku[1];
                        }
                        string s = (tutar - c).ToString();
                        string[] parts = s.Split(',');
                        int i1 = Convert.ToInt32(parts[0]);
                        int i2 = Convert.ToInt32(parts[1]);
                        textBox23.Text = i1.ToString();
                        textBox24.Text = i2.ToString();
                        textBox9.Text = textBox23.Text + "." + textBox24.Text;
                    }
                    baglan.Close();
                }
                if (ödeme == "VİSA")
                {
                    komut = new SqlCommand("Select tutar,alınan,TaksitSayisi from OgrenciOdeme where OgrId='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal c;
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        decimal tutar = (decimal)oku[0];
                        if (oku[1] == DBNull.Value)
                        {

                            c = (decimal)0;
                        }
                        else
                        {
                            c = (decimal)oku[1];
                        }
                        textBox17.Text = tutar.ToString();
                        string s = (tutar - c).ToString();
                        string[] parts = s.Split(',');
                        int i1 = Convert.ToInt32(parts[0]);
                        int i2 = Convert.ToInt32(parts[1]);
                        textBox25.Text = i1.ToString();
                        textBox26.Text = i2.ToString();

                        string s1 = decimal.Divide(tutar - c, (decimal)oku[2]).ToString();
                        string[] parts2 = s1.Split(',');
                        int i = Convert.ToInt32(parts2[0]);
                        int i3 = Convert.ToInt32(parts2[1]);
                        textBox20.Text = i.ToString();
                        textBox19.Text = i3.ToString();
                        textBox18.Text = Convert.ToInt32((decimal)oku[2]).ToString();

                    }
                    baglan.Close();
                    panel11.Visible = true;
                    panel12.Visible = true;
                    panel13.Visible = true;
                }
            }


        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                //ÇEK BOZDURMA
                if (textBox23.Text == "0" && textBox24.Text == "0")
                {
                    MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMEZ.");
                }
                else
                {
                    //ÇEK ÖDEME ALMA

                    komut = new SqlCommand("Select alınan,kalantutar,tutar from OgrenciOdeme where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    decimal kalantutar = 0;
                    decimal tutar = 0;


                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        kalantutar = (decimal)oku3[1];
                        tutar = (decimal)oku3[2];

                        if (oku3[0] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[0];

                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))));
                        komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                        if (tutar == i + Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))
                        {
                            baglan.Open();
                            SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "'", baglan);
                            komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                            komu.ExecuteNonQuery();
                            baglan.Close();
                        }
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut.Parameters.AddWithValue("@p1", öğrenciid);
                        komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut.Parameters.AddWithValue("@p3", textBox23.Text + "." + textBox24.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komut.Parameters.AddWithValue("@p5", tutar);
                        komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                        komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - i - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))));
                        komut.Parameters.AddWithValue("@p8", tutar - i - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))));
                        komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p4", textBox23.Text + "." + textBox24.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                        if (tutar == Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))
                        {
                            baglan.Open();
                            SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "'", baglan);
                            komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                            komu.ExecuteNonQuery();
                            baglan.Close();
                        }
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut.Parameters.AddWithValue("@p1", öğrenciid);
                        komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut.Parameters.AddWithValue("@p3", textBox23.Text + "." + textBox24.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komut.Parameters.AddWithValue("@p5", tutar);
                        komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                        komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text))));
                        komut.Parameters.AddWithValue("@p8", tutar - (Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komut.ExecuteNonQuery();
                        baglan.Close();


                    }

                    panel9.Visible = false;
                    baglan.Open();
                    SqlCommand komutgüncelle3 = new SqlCommand("update ÇekSenet set Bozdur=@p1,ÇekDurumu=@p2 where ogrenciid='" + öğrenciid + "' and miktar='" + textBox23.Text + "." + textBox24.Text + "'", baglan);
                    komutgüncelle3.Parameters.AddWithValue("@p1", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                    komutgüncelle3.Parameters.AddWithValue("@p2", "BOZDURULDU");
                    komutgüncelle3.ExecuteNonQuery();
                    baglan.Close();
                }

                textBox9.Text = "";
                dateTimePicker9.Value = DateTime.Now;
                string filtre = "Select ID,Tarih,TOPLAM,KALAN,ÖDENEN,TÜR from ÖğrenciÖdemeDetay where Öğrenci='" + öğrenciid + "'";
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖDEME");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
                dataGridView3.Columns.Clear();
                string filtre2 = "Select ID,SonÖdemeGünü 'SON ÖDEME GÜNÜ',Miktar MİKTAR,ÖdendiDurum 'ÖDEME DURUM' from ÖdemePlanı where Öğrenci='" + öğrenciid + "'";
                baglan.Open();
                da = new SqlDataAdapter(filtre2, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖDEMEPLANI");
                dataGridView3.DataSource = ds.Tables[0];
                baglan.Close();

            }
            if (comboBox3.Text == "FAALİYET")
            {
                if (textBox23.Text == "0" && textBox24.Text == "0")
                {
                    MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMEZ.");
                }
                else
                {
                    //ÇEK ÖDEME ALMA

                    komut = new SqlCommand("Select alınan from Faaliyet where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;



                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {

                        if (oku3[0] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[0];

                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);

                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }

                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p4", textBox23.Text + "." + textBox24.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                        baglan.Open();



                    }

                    panel9.Visible = false;
                    baglan.Open();
                    SqlCommand komutgüncelle3 = new SqlCommand("update ÇekSenet set Bozdur=@p1,ÇekDurumu=@p2 where ogrenciid='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "' and miktar='" + textBox23.Text + "." + textBox24.Text + "'", baglan);
                    komutgüncelle3.Parameters.AddWithValue("@p1", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                    komutgüncelle3.Parameters.AddWithValue("@p2", "BOZDURULDU");
                    komutgüncelle3.ExecuteNonQuery();
                    baglan.Close();
                }

                textBox9.Text = "";
                dateTimePicker9.Value = DateTime.Now;
                string filtre = "Select ID,Faaliyet,Tarih,Miktar as TOPLAM,ÖDENEN,SonÖdemeTarihi 'SON ÖDEME TARİHİ' from Faaliyet where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖDEME");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
            }
            griddoldur();


        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                if (textBox18.Text == "0")
                {
                    MessageBox.Show("DAHA FAZLA ÖDEME ALAMAZSINIZ.");
                }
                else
                {
                    komut = new SqlCommand("Select alınan,kalantutar,tutar,TaksitSayisi from OgrenciOdeme where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    decimal kalantutar = 0;
                    decimal tutar = 0;
                    decimal taksit = 1;


                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        kalantutar = (decimal)oku3[1];
                        tutar = (decimal)oku3[2];
                        taksit = (decimal)oku3[3];

                        if (oku3[0] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[0];

                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text))));
                        komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                        komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();

                        string id = "";
                        for (int j = 0; j < dataGridView3.RowCount - 1; j++)
                        {
                            if (dataGridView3.Rows[j].Cells[3].Value.ToString() == "ÖDENMEDİ")
                            {
                                id = dataGridView3.Rows[j].Cells[0].Value.ToString();
                                break;
                            }

                        }
                        baglan.Open();
                        SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "' and ID='" + id + "'", baglan);
                        komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                        komu.ExecuteNonQuery();
                        baglan.Close();

                        baglan.Open();
                        SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut.Parameters.AddWithValue("@p1", dataGridView1.Rows[rows].Cells[2].Value.ToString());
                        komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut.Parameters.AddWithValue("@p3", textBox20.Text + "." + textBox19.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komut.Parameters.AddWithValue("@p5", tutar);
                        komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                        komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - i - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text))));
                        komut.Parameters.AddWithValue("@p8", tutar - i - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komut.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text))));
                        komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox19.Text);
                        komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();

                        baglan.Open();
                        SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "' and ID='" + dataGridView3.Rows[0].Cells[0].Value.ToString() + "'", baglan);
                        komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                        komu.ExecuteNonQuery();
                        baglan.Close();

                        baglan.Open();
                        SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                        komut.Parameters.AddWithValue("@p1", dataGridView1.Rows[rows].Cells[2].Value.ToString());
                        komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                        komut.Parameters.AddWithValue("@p3", textBox20.Text + "." + textBox19.Text);
                        komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komut.Parameters.AddWithValue("@p5", tutar);
                        komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                        komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text))));
                        komut.Parameters.AddWithValue("@p8", tutar - (Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komut.ExecuteNonQuery();
                        baglan.Close();


                    }

                    panel9.Visible = false;
                    textBox18.Text = "";
                    textBox17.Text = "";
                    textBox19.Text = "";
                    textBox20.Text = "";
                    string filtre = "Select ID,Tarih,TOPLAM,KALAN,ÖDENEN,TÜR from ÖğrenciÖdemeDetay where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEME");
                    dataGridView2.DataSource = ds.Tables[0];
                    baglan.Close();
                    dataGridView3.Columns.Clear();
                    string filtre2 = "Select ID,SonÖdemeGünü 'SON ÖDEME GÜNÜ',Miktar MİKTAR,ÖdendiDurum 'ÖDEME DURUM' from ÖdemePlanı where Öğrenci='" + öğrenciid + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre2, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEMEPLANI");
                    dataGridView3.DataSource = ds.Tables[0];
                    baglan.Close();
                }
            }
            if (comboBox3.Text == "FAALİYET")
            {
                if (textBox18.Text == "0")
                {
                    MessageBox.Show("DAHA FAZLA ÖDEME ALAMAZSINIZ.");
                }
                else
                {
                    komut = new SqlCommand("Select alınan,TaksitSayisi from Faaliyet where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    decimal taksit = 1;


                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {

                        taksit = (decimal)oku3[1];

                        if (oku3[0] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[0];

                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                        komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);

                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox19.Text);
                        komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);


                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();

                    }

                    panel9.Visible = false;
                    textBox18.Text = "";
                    textBox17.Text = "";
                    textBox19.Text = "";
                    textBox20.Text = "";
                    string filtre = "Select ID,Faaliyet,Tarih,Miktar as TOPLAM,ÖDENEN,SonÖdemeTarihi 'SON ÖDEME TARİHİ' from Faaliyet where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                    baglan.Open();
                    da = new SqlDataAdapter(filtre, baglan);
                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖDEME");
                    dataGridView2.DataSource = ds.Tables[0];
                    baglan.Close();
                }
            }
            griddoldur();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.Text == "TOPLAM (EĞİTİM DESTEKLİ)")
            {
                if (string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(textBox6.Text) == true)
                {
                    MessageBox.Show("HİÇ BİR ALAN BOŞ BIRAKILAMAZ.");
                }
                else
                {
                    if (Convert.ToDecimal(textBox21.Text + "," + textBox22.Text) < Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))
                    {
                        MessageBox.Show("KALAN MİKTARDAN DAHA FAZLA ÖDEME ALMANIZ İMKANSIZDIR.");
                    }
                    else
                    {
                        komut = new SqlCommand("Select alınan,kalantutar,tutar from OgrenciOdeme where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal i = 0;
                        decimal kalantutar = 0;
                        decimal tutar = 0;


                        bool bayrak = false;
                        SqlDataReader oku3 = komut.ExecuteReader();
                        while (oku3.Read())
                        {
                            kalantutar = (decimal)oku3[1];
                            tutar = (decimal)oku3[2];

                            if (oku3[0] == DBNull.Value)
                            {
                                bayrak = false;

                            }
                            else
                            {
                                i = (decimal)oku3[0];

                                bayrak = true;
                            }
                        }
                        baglan.Close();
                        string yenideger;
                        if (bayrak == true)
                        {
                            yenideger = (i + Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)).ToString();
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))));
                            komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                            komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                            if (tutar == (i + Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)))
                            {
                                baglan.Open();
                                SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "'", baglan);
                                komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                                komu.ExecuteNonQuery();
                                baglan.Close();
                            }
                            baglan.Open();
                            SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                            komut.Parameters.AddWithValue("@p1", dataGridView1.Rows[rows].Cells[2].Value.ToString());
                            komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                            komut.Parameters.AddWithValue("@p3", textBox16.Text + "." + textBox6.Text);
                            komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komut.Parameters.AddWithValue("@p5", tutar);
                            komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                            komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - i - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))));
                            komut.Parameters.AddWithValue("@p8", tutar - i - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komut.ExecuteNonQuery();
                            baglan.Close();
                        }
                        else
                        {
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update OgrenciOdeme set KALAN=@p1,kalantutar=@p2,ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", kalantutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))));
                            komutgüncelle.Parameters.AddWithValue("@p2", kalantutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komutgüncelle.Parameters.AddWithValue("@p4", textBox16.Text + "." + textBox6.Text);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                            if (tutar == Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))
                            {
                                baglan.Open();
                                SqlCommand komu = new SqlCommand("update ÖdemePlanı set ÖdendiDurum=@p1 where Öğrenci='" + öğrenciid + "'", baglan);
                                komu.Parameters.AddWithValue("@p1", "ÖDENDİ");
                                komu.ExecuteNonQuery();
                                baglan.Close();
                            }
                            baglan.Open();
                            SqlCommand komut = new SqlCommand("insert into ÖğrenciÖdemeDetay (Öğrenci,Tarih,Ödeme,ÖDENEN,TOPLAM,TÜR,KALAN,kalantutar) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7, @p8)", baglan);
                            komut.Parameters.AddWithValue("@p1", dataGridView1.Rows[rows].Cells[2].Value.ToString());
                            komut.Parameters.AddWithValue("@p2", DateTime.Now.ToString("yyyyMMdd"));
                            komut.Parameters.AddWithValue("@p3", textBox16.Text + "." + textBox6.Text);
                            komut.Parameters.AddWithValue("@p4", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komut.Parameters.AddWithValue("@p5", tutar);
                            komut.Parameters.AddWithValue("@p6", "EĞİTİM ÖDEMESİ");
                            komut.Parameters.AddWithValue("@p7", string.Format("{0:N}", tutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))));
                            komut.Parameters.AddWithValue("@p8", tutar - (Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komut.ExecuteNonQuery();
                            baglan.Close();


                        }
                        panel9.Visible = false;
                        textBox14.Text = "";
                        textBox16.Text = "";
                        textBox6.Text = "";
                        textBox21.Text = "";
                        textBox22.Text = "";
                        string filtre = "Select ID,Tarih,TOPLAM,KALAN,ÖDENEN,TÜR from ÖğrenciÖdemeDetay where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                        baglan.Open();
                        da = new SqlDataAdapter(filtre, baglan);
                        cmdb = new SqlCommandBuilder(da);
                        ds = new DataSet();
                        da.Fill(ds, "ÖDEME");
                        dataGridView2.DataSource = ds.Tables[0];
                        baglan.Close();
                        dataGridView3.Columns.Clear();
                        string filtre2 = "Select ID,SonÖdemeGünü 'SON ÖDEME GÜNÜ',Miktar MİKTAR,ÖdendiDurum 'ÖDEME DURUM' from ÖdemePlanı where Öğrenci='" + öğrenciid + "'";
                        baglan.Open();
                        da = new SqlDataAdapter(filtre2, baglan);
                        cmdb = new SqlCommandBuilder(da);
                        ds = new DataSet();
                        da.Fill(ds, "ÖDEMEPLANI");
                        dataGridView3.DataSource = ds.Tables[0];
                        baglan.Close();
                    }

                }
            }

            if (comboBox3.Text == "FAALİYET")
            {
                if (string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(textBox6.Text) == true)
                {
                    MessageBox.Show("HİÇ BİR ALAN BOŞ BIRAKILAMAZ.");
                }
                else
                {
                    if (Convert.ToDecimal(textBox21.Text + "," + textBox22.Text) < Convert.ToDecimal(textBox16.Text + "," + textBox6.Text))
                    {
                        MessageBox.Show("KALAN MİKTARDAN DAHA FAZLA ÖDEME ALMANIZ İMKANSIZDIR.");
                    }
                    else
                    {
                        komut = new SqlCommand("Select alınan from Faaliyet where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal i = 0;


                        bool bayrak = false;
                        SqlDataReader oku3 = komut.ExecuteReader();
                        while (oku3.Read())
                        {

                            if (oku3[0] == DBNull.Value)
                            {
                                bayrak = false;

                            }
                            else
                            {
                                i = (decimal)oku3[0];

                                bayrak = true;
                            }
                        }
                        baglan.Close();
                        string yenideger;
                        if (bayrak == true)
                        {
                            yenideger = (i + Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)).ToString();
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                            komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                            baglan.Open();

                        }
                        else
                        {
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update Faaliyet set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                            komutgüncelle.Parameters.AddWithValue("@p4", textBox16.Text + "." + textBox6.Text);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();

                        }
                        panel9.Visible = false;
                        textBox14.Text = "";
                        textBox16.Text = "";
                        textBox5.Text = "";
                        textBox21.Text = "";
                        textBox22.Text = "";
                        string filtre = "Select ID,Faaliyet,Tarih,Miktar as TOPLAM,ÖDENEN,SonÖdemeTarihi 'SON ÖDEME TARİHİ' from Faaliyet where Öğrenci='" + dataGridView1.Rows[rows].Cells[2].Value.ToString() + "'";
                        baglan.Open();
                        da = new SqlDataAdapter(filtre, baglan);
                        cmdb = new SqlCommandBuilder(da);
                        ds = new DataSet();
                        da.Fill(ds, "ÖDEME");
                        dataGridView2.DataSource = ds.Tables[0];
                        baglan.Close();

                    }

                }
            }
            griddoldur();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox3.Text == "FAALİYET")
            {
                if (dataGridView2.CurrentCell.ColumnIndex == dataGridView2.ColumnCount - 1)
                {
                    panel9.Visible = true;
                    baglan.Open();
                    string ödeme = "";
                    komut = new SqlCommand("Select ÖdemeŞekli from Faaliyet where ID='" + dataGridView2.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {

                        ödeme = oku3[0].ToString();
                    }
                    baglan.Close();
                    if (ödeme == "NAKİT" || ödeme == "BANKA")
                    {
                        panel11.Visible = true;
                        panel12.Visible = false;
                        komut = new SqlCommand("Select tutar,alınan from Faaliyet where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            string s = (tutar - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox21.Text = i1.ToString();
                            textBox22.Text = i2.ToString();
                            textBox14.Text = textBox21.Text + "." + textBox22.Text;

                        }
                        baglan.Close();
                    }
                    if (ödeme == "ÇEK" || ödeme == "SENET")
                    {
                        panel11.Visible = true;
                        panel12.Visible = true;
                        panel13.Visible = false;
                        textBox9.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[5].Value.ToString();
                        komut = new SqlCommand("Select tutar,alınan from Faaliyet where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            string s = (tutar - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox23.Text = i1.ToString();
                            textBox24.Text = i2.ToString();
                            textBox9.Text = textBox23.Text + "." + textBox24.Text;

                        }
                        baglan.Close();
                    }
                    if (ödeme == "VİSA")
                    {
                        komut = new SqlCommand("Select tutar,alınan,taksit,TaksitSayisi from Faaliyet where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal c;
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            decimal tutar = (decimal)oku[0];
                            if (oku[1] == DBNull.Value)
                            {

                                c = (decimal)0;
                            }
                            else
                            {
                                c = (decimal)oku[1];
                            }
                            textBox17.Text = (Decimal.Multiply(tutar, (decimal)oku[2])).ToString();
                            string s = (Decimal.Multiply(tutar, (decimal)oku[2]) - c).ToString();
                            string[] parts = s.Split(',');
                            int i1 = Convert.ToInt32(parts[0]);
                            int i2 = Convert.ToInt32(parts[1]);
                            textBox25.Text = i1.ToString();
                            textBox26.Text = i2.ToString();

                            string s1 = (tutar).ToString();
                            string[] parts2 = s1.Split(',');
                            int i = Convert.ToInt32(parts2[0]);
                            int i3 = Convert.ToInt32(parts2[1]);
                            textBox20.Text = i.ToString();
                            textBox19.Text = i3.ToString();
                            textBox18.Text = Convert.ToInt32((decimal)oku[3]).ToString();

                        }
                        baglan.Close();
                        panel11.Visible = true;
                        panel12.Visible = true;
                        panel13.Visible = true;

                    }
                    baglan.Close();
                }
            }
        }
    }
}