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
    public partial class HİZMET_EKLE : Form
    {
        public HİZMET_EKLE()
        {
            InitializeComponent();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        string öğrenciid;
        string şubeid;

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentCell.ColumnIndex == 9)
            {
                label16.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[3].Value.ToString() + " " + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString();
                öğrenciid = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString();
                komut = new SqlCommand("Select Sube from Ogrenci where ID='" + öğrenciid + "'", baglan);
                baglan.Open();
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    şubeid = oku3[0].ToString();
                }
                baglan.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }
        public string query;
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
                filtre += " ş.ŞubeAdi='" + comboBox5.Text+"'";
                degisken = true;

                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Kur='" + cmbKur.Text+"'";
                    degisken = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Program='" + cmbProgram.Text + "'";
                    degisken = true;
                }
                if (cmbCinsiyet.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Cinsiyet='" + cmbCinsiyet.Text + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtAd.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Adi='" + txtAd.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSoyad.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.Soyadi='" + txtSoyad.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtSozno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.SozNo='" + txtSozno.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtTc.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.TCKN='" + txtTc.Text.TrimEnd() + "'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtOkulno.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.OkulNo='" + txtOkulno.Text.TrimEnd() + "'";
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

                dataGridView3.Columns.Clear();
                baglan.Open();
                if (query != null)
                    da = new SqlDataAdapter(query, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİ");
                dataGridView3.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView3.Columns.Add(dgvBtn);
                query = null;
                panel7.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
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

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void HİZMET_EKLE_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("YEMEK");
            comboBox1.Items.Add("KIYAFET");
            comboBox1.Items.Add("SORU BANKASI");
            comboBox1.Items.Add("KİTAP");
            comboBox1.Items.Add("KULÜP ÜCRETİ");
            comboBox21.Items.Add("YEMEK");
            comboBox21.Items.Add("KIYAFET");
            comboBox21.Items.Add("SORU BANKASI");
            comboBox21.Items.Add("KİTAP");
            comboBox21.Items.Add("KULÜP ÜCRETİ");
            panel8.Visible = false;
            comboBox4.Items.Add("NAKİT");
            comboBox4.Items.Add("VİSA");
            comboBox4.Items.Add("ÇEK");
            comboBox4.Items.Add("BANKA");
            comboBox4.Items.Add("SENET");
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox5.Items.Add(oku3[0].ToString());
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

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
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
                string filtre = "Select f.ID, ö.SozNo 'SÖZ NO', ö.OkulNo 'OKUL NO', ö.Adi +' ' +ö.Soyadi ÖĞRENCİ, f.Faaliyet FAALİYET,f.Miktar 'ÜCRET', f.Tarih 'TARİH'  from Faaliyet f join Ogrenci ö on ö.ID=f.Öğrenci join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(comboBox22.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ş.ŞubeAdi=" + comboBox22.Text;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox21.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " f.Faaliyet=" + comboBox21.Text;
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
                query = filtre;
                panel1.Visible = false;

                dataGridView1.Columns.Clear();
                baglan.Open();
                if (query != null)
                    da = new SqlDataAdapter(query, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "FAALİYET");
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
                panel2.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "FAALİYET SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[4].Value.ToString() + " İSİMLİ FAALİYETİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "FAALİYET SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[1].Value.ToString());
                            string sql = "DELETE  FROM Faaliyet WHERE ID=@id";
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

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox4.Text == "NAKİT")
            {
                panel8.Visible = true;
                panel9.Visible = false;
            }
            if (comboBox4.Text == "VİSA")
            {
                panel8.Visible = true;
                panel9.Visible = true;
                panel3.Visible = false;

            }
            if (comboBox4.Text == "ÇEK")
            {
                panel8.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = false;
            }
            if (comboBox4.Text == "BANKA")
            {
                panel8.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;

            }
            if (comboBox4.Text == "SENET")
            {
                panel8.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
            }

        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox21.Text) == true || string.IsNullOrEmpty(textBox22.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true )
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {

                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into  Faaliyet (Öğrenci,Faaliyet,Tarih,SonÖdemeTarihi,Miktar,tutar,ÖdemeŞekli) values (@p1, @p2, @p3, @p4,@p5, @p8,@p9)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)) + " TL");
                    komutkaydet.Parameters.AddWithValue("@p8", textBox21.Text + "." + textBox22.Text);
                    komutkaydet.Parameters.AddWithValue("@p9", "NAKİT");

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    comboBox1.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "00";
                    panel8.Visible = false;


                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox17.Text) == true || string.IsNullOrEmpty(textBox18.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(comboBox6.Text) == true | string.IsNullOrEmpty(textBox23.Text) == true  || string.IsNullOrEmpty(textBox25.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faaliyet (Öğrenci,Faaliyet,Tarih,SonÖdemeTarihi,Miktar,tutar,ÖdemeŞekli,Sahip,TaksitSayisi,NO,BankaHesabı,taksit) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p9, @p10, @p11,@p12,@p13)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox17.Text + "," + textBox18.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox17.Text + "." + textBox18.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", "VİSA");
                    komutkaydet.Parameters.AddWithValue("@p9", textBox23.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", numericUpDown1.Text);
                    komutkaydet.Parameters.AddWithValue("@p11", textBox25.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p13", numericUpDown1.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel8.Visible = false;
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text = "";
                    textBox17.Text = "";
                    textBox18.Text = "00";
                    textBox23.Text = "";
                    numericUpDown1.Text = "1";
                    textBox25.Text = "";
                    comboBox6.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //ÇEK
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox6.Text) == true || string.IsNullOrEmpty(textBox11.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox14.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faaliyet (Öğrenci,Faaliyet,Tarih,SonÖdemeTarihi,Miktar,tutar,ÖdemeŞekli,Sahip,Vade,NO,Banka) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p9, @p10, @p11,@p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox6.Text + "." + textBox11.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", "ÇEK");
                    komutkaydet.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker6.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p11", textBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", textBox14.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel8.Visible = false;
                  

                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,BANKA,ÇekNo,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p2", "ÇEK");
                    komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                    komutkaydet2.Parameters.AddWithValue("@p11", öğrenciid);
                    komutkaydet2.Parameters.AddWithValue("@p1", şubeid);
                    komutkaydet2.Parameters.AddWithValue("@p3", textBox5.Text);
                    komutkaydet2.Parameters.AddWithValue("@p4", label16.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox6.Text + "." + textBox11.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                    komutkaydet2.Parameters.AddWithValue("@p9", textBox3.Text);
                    komutkaydet2.Parameters.AddWithValue("@p8", textBox14.Text);
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker6.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text = "";
                    textBox6.Text = "";
                    textBox11.Text = "00";
                    textBox5.Text = "";
                    dateTimePicker6.Value = DateTime.Now;
                    textBox3.Text = "";
                    textBox14.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox24.Text) == true || string.IsNullOrEmpty(textBox26.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(comboBox7.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faaliyet (Öğrenci,Faaliyet,Tarih,SonÖdemeTarihi,Miktar,tutar,ÖdemeŞekli,Sahip,BankaHesabı) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p9, @p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox24.Text + "," + textBox26.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox24.Text + "." + textBox26.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", "BANKA");
                    komutkaydet.Parameters.AddWithValue("@p9", textBox16.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox7.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel8.Visible = false;
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text = "";
                    comboBox7.Text = "";
                    textBox24.Text = "";
                    textBox26.Text = "00";
                    textBox16.Text = "";

                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(label16.Text) == true || string.IsNullOrEmpty(textBox20.Text) == true || string.IsNullOrEmpty(textBox27.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox19.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faaliyet (Öğrenci,Faaliyet,Tarih,SonÖdemeTarihi,Miktar,tutar,ÖdemeŞekli,Sahip,Vade) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p9, @p10)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", öğrenciid);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker4.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox20.Text + "." + textBox27.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", "SENET");
                    komutkaydet.Parameters.AddWithValue("@p9", textBox19.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker5.Value.Date.ToString("yyyyMMdd"));

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,miktar,ogrenciid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p1", şubeid);
                    komutkaydet2.Parameters.AddWithValue("@p2", "SENET");
                    komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                    komutkaydet2.Parameters.AddWithValue("@p11", öğrenciid);
                    komutkaydet2.Parameters.AddWithValue("@p3", label16.Text);
                    komutkaydet2.Parameters.AddWithValue("@p4", textBox19.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox20.Text + "." + textBox27.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "ÖĞRENCİ");
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker5.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel8.Visible = false;
                    dateTimePicker3.Value = DateTime.Now;
                    dateTimePicker4.Value = DateTime.Now;
                    label16.Text = "";
                    comboBox1.Text = "";
                    comboBox4.Text = "";
                    textBox20.Text = "";
                    textBox27.Text = "00";
                    dateTimePicker5.Value = DateTime.Now;
                    textBox19.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }
    }
}
