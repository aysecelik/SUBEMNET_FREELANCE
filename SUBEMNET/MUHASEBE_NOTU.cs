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
    public partial class MUHASEBE_NOTU : Form
    {
        public MUHASEBE_NOTU()
        {
            InitializeComponent();
        }
        public string query;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "' and Snf = " + comboBox3.Text;
            dataGridView1.Columns.Clear();
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SEÇ";
            dgvBtn.Text = "SEÇ";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            query = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "' and Devre=" + comboBox2.Text;
            dataGridView1.Columns.Clear();
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÖĞRENCİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SEÇ";
            dgvBtn.Text = "SEÇ";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            query = null;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;

        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = true;
                string filtre = "Select ö.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Program 'PROGRAM', ö.Devre 'DEVRE', ö.Kur 'KUR', ö.Snf as SINIF from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'";

                if (degisken == true)
                {
                    filtre += " and ";
                }
                filtre += " ş.ŞubeAdi=" + comboBox22.Text;
                degisken = true;

                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Kur=" + cmbKur.Text;
                    degisken = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Program=" + cmbProgram.Text;
                    degisken = true;
                }
                if (cmbCinsiyet.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Cinsiyet=" + cmbCinsiyet.Text;
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Adi=" + txtAd.Text.TrimEnd();
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Soyadi=" + txtSoyad.Text.TrimEnd();
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSozno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.SozNo=" + txtSozno.Text.TrimEnd();
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtTc.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.TCKN=" + txtTc.Text.TrimEnd();
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtOkulno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.OkulNo=" + txtOkulno.Text.TrimEnd();
                    degisken = true;
                }
                if (radioButton4.Checked)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Durum=1";
                    degisken = true;
                }

                query = filtre;
                panel1.Visible = false;

                dataGridView1.Columns.Clear();
                baglan.Open();
                if (query != null)
                    da = new SqlDataAdapter(query, baglan);
                else
                    da = new SqlDataAdapter("Select ID, Sezon, Sube, KayitTarihi as KayıtTarihi, SozNo, OkulNo, Adi as Adı, Soyadi as Soyadı, Program, Devre, Kur, Snf as Sınıf from Ogrenci", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                query = null;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string öğrenciid;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentCell.ColumnIndex == 9)
            {
                label16.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[3].Value.ToString() + " " + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString();
                öğrenciid = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true)
            {
                MessageBox.Show("ÖĞRENCİ BİLGİSİ BOŞ BIRAKILAMAZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into  MuhasebeNotu (Öğrenci,Not,Tarih,SözVermeTarihi) values (@p1, @p2, @p3, @p4)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", richTextBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    richTextBox2.Text = "";
                   
                }
                catch (Exception)
                {
                    MessageBox.Show("HATA");
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;

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
                    PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount-1);

                    // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                    pdfTable.SpacingBefore = 20f;
                    pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                    pdfTable.WidthPercentage = 100; // hücre genişliği
                    pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                    pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                    for (int i = 0; i < dataGridView1.ColumnCount-1; i++)
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

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            griddoldur();

        }
        void griddoldur()
        {
            try
            {
                bool degisken = true;
                string filtre = "Select f.ID, ö.SozNo 'SÖZ NO', ö.Adi + ö.Soyadı ÖĞRENCİ,ö.Devre 'DEVRE', f.Tarih 'TARİH' , f.Not 'MUHASEBE NOTU',f.SözVermeTarihi 'SÖZ VERME TARİHİ'  from MuhasebeNotu f join Ogrenci ö on ö.ID=f.Öğrenci join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(comboBox22.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ş.ŞubeAdi=" + comboBox22.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " f.MuhasebeNotu=" + textBox3.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox18.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Snf=" + comboBox18.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox20.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Devre=" + comboBox20.Text;
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " f.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " f.SözVermeTarihi between '" + dateTimePicker6.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker5.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                query = filtre;
                panel1.Visible = false;

                dataGridView1.Columns.Clear();
                baglan.Open();
                if (query != null)
                    da = new SqlDataAdapter(query, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "MUHASEBENOTU");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SİL";
                dgvBtn.Text = "SİL";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                query = null;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }
        SqlCommand komut;

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "MUHASEBE NOTU SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip  MUHASEBE NOTUNU TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "MUHASEBE NOTU SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM MuhasebeNotu WHERE ID=@id";
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

        private void MUHASEBE_NOTU_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox22.Items.Add(oku3[0].ToString());

                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku[0].ToString());
                comboBox20.Items.Add(oku[0].ToString());

                cmbKur.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox3.Items.Add(oku2[0].ToString());
                comboBox18.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku7 = komut.ExecuteReader();
            while (oku7.Read())
            {
                cmbProgram.Items.Add(oku7[0].ToString());

            }
            baglan.Close();
            cmbCinsiyet.Items.Add("ERKEK");
            cmbCinsiyet.Items.Add("KIZ");
        }
    }
}
