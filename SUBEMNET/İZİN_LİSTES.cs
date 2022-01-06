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
    public partial class İZİN_LİSTES : Form
    {
        public İZİN_LİSTES()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        void griddoldur()
        {

            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select i.ID,i.PersonelAdi 'PERSONEL ADI',i.PersonelSoyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',i.Pozisyon 'POZİSYON',i.Branş 'BRANŞ',i.İzinTürü 'İZİN TÜRÜ',i.BaslangicTarih 'BAŞLANGIÇ TARİHİ',i.BitisTarih 'BİTİŞ TARİHİ',i.Aciklama 'AÇIKLAMA',i.İzinVeren 'İZİN VEREN' from İzinler i join Sube ş on ş.ID=i.Sube where ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "İzinler");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "DÜZENLE";
            dgvBtn.Text = "DÜZENLE";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
            dgvBtn2.HeaderText = "SİL";
            dgvBtn2.Text = "SİL";
            dgvBtn2.UseColumnTextForButtonValue = true;
            dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn2.Width = 70;
            dataGridView1.Columns.Add(dgvBtn2);
          
          

        }
        SqlCommand komut;

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select i.ID,i.PersonelAdi 'PERSONEL ADI',i.PersonelSoyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',i.Pozisyon 'POZİSYON',i.Branş 'BRANŞ',i.İzinTürü 'İZİN TÜRÜ',i.BaslangicTarih 'BAŞLANGIÇ TARİHİ',i.BitisTarih 'BİTİŞ TARİHİ',i.Aciklama 'AÇIKLAMA',i.İzinVeren 'İZİN VEREN' from İzinler i join Sube ş on ş.ID=i.Sube where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";
                    filtre += " i.ID = '" + textBox11.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Aciklama = '" + richTextBox2.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Branş = '" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.İzin veren  = '" + comboBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.İzinTürü = '" + comboBox9.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.PersonelAdi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.PersonelSoyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.BaslangicTarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.BitisTarih between '" + dateTimePicker7.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker5.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Aktiflik='" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox10.Text) == true && string.IsNullOrEmpty(comboBox9.Text) == true && string.IsNullOrEmpty(richTextBox2.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(comboBox3.Text) && checkBox4.Checked == false && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select i.ID,i.PersonelAdi 'PERSONEL ADI',i.PersonelSoyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',i.Pozisyon 'POZİSYON',i.Branş 'BRANŞ',i.İzinTürü 'İZİN TÜRÜ',i.BaslangicTarih 'BAŞLANGIÇ TARİHİ',i.BitisTarih 'BİTİŞ TARİHİ',i.Aciklama 'AÇIKLAMA',i.İzinVeren 'İZİN VEREN' from İzinler i join Sube ş on ş.ID=i.Sube where ş.Okulid='" + okulid + "'";

                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "İzinler");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "DÜZENLE";
                dgvBtn.Text = "DÜZENLE";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "SİL";
                dgvBtn2.Text = "SİL";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                panel2.Visible = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox8.Text = "";

                richTextBox2.Text ="";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox10.Text = "";
                comboBox9.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                dateTimePicker7.Value = DateTime.Now;
                dateTimePicker5.Value = DateTime.Now;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;



            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; ;
            textBox10.Text = "";
            textBox11.Text = "";
            textBox8.Text = "";

            richTextBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox10.Text = "";
            comboBox9.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker7.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
           
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 2);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount-2; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 11; j++)
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 11)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                comboBox8.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
                comboBox13.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
                comboBox12.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
                comboBox7.Text = dataGridView1.Rows[secilen].Cells[10].Value.ToString();
                comboBox6.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
                textBox9.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
                textBox7.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
                dateTimePicker10.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[7].Value);
                dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[8].Value);
                richTextBox3.Text = dataGridView1.Rows[secilen].Cells[9].Value.ToString();
           
               


                panel2.Visible = true;
                panel1.Visible = true;
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = true;
                button11.Visible = false;
                button14.Visible = true;



            }
            if (dataGridView1.CurrentCell.ColumnIndex == 12)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "İZİN SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID'Lİ " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " PERSONELE AİT OLAN İZİNİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "İZİN SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE FROM İzinler WHERE ID=@id";
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
                            MessageBox.Show("HATA");
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            panel9.Visible = false;
            MessageBox.Show("İZİN ALACAK OLAN PERSONELİN ID'SİNE TIKLAYINIZ.");

        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
            textBox3.Text = "";
            textBox15.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            maskedTextBox1.Text = "";
            comboBox5.Text = "";
            comboBox4.Text = "";
            comboBox11.Text = "";

            dateTimePicker4.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            checkBox3.Checked = false;
            checkBox5.Checked = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
            panel7.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    filtre += " P.ID = '" + textBox3.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " P.Email = '" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " P.Pozisyon = '" + comboBox5.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " P.Brans = '" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox11.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " P.Sube = '" + subeid[comboBox11.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Adi = '" + textBox6.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Soyadi = '" + textBox5.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (maskedTextBox1.MaskFull==true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.CepTel = '" + maskedTextBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.IseBaslangıcTarih between '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox5.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Aktiflik = '" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox3.Text) == true && string.IsNullOrEmpty(textBox15.Text) == true && string.IsNullOrEmpty(textBox6.Text) == true && string.IsNullOrEmpty(textBox5.Text) == true && string.IsNullOrEmpty(comboBox5.Text) == true && string.IsNullOrEmpty(comboBox4.Text) == true && string.IsNullOrEmpty(comboBox11.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox3.Checked == false && checkBox5.Checked == false)
                {
                    filtre = "Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel9.Visible = false;
                textBox3.Text = "";
                textBox15.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                maskedTextBox1.Text = "";
                comboBox5.Text = "";
                comboBox4.Text = "";
                comboBox11.Text = "";

                dateTimePicker4.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
                checkBox3.Checked = false;
                checkBox5.Checked = false;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox9.Text = "";

            textBox7.Text = "";
            comboBox8.Text = "";
            comboBox7.Text = ""; comboBox6.Text = "";
            comboBox13.Text = "";
            comboBox12.Text = "";
            dateTimePicker10.Value = DateTime.Now;
            dateTimePicker8.Value = DateTime.Now;
            richTextBox3.Text = "";
        }
        bool aktiflik;
        string id;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.CurrentCell.RowIndex;
            id = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            baglan.Open();
            komut = new SqlCommand("Select Adi,Soyadi,Sube,Pozisyon,Brans,Aktiflik from Personeller where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                textBox9.Text = oku.GetValue(0).ToString();
                textBox7.Text = oku.GetValue(1).ToString();
                comboBox8.Text = comboBox8.Items[subeid.IndexOf((int)oku[2])].ToString();
                comboBox13.Text = oku.GetValue(3).ToString();
                comboBox12.Text = oku.GetValue(4).ToString();
                aktiflik = (bool)oku.GetValue(5);
            }
            button14.Visible = false;
            button11.Visible = true;
            panel9.Visible = true;
            panel7.Visible = true;
            baglan.Close();
         

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = false;
                string filtre = "insert into İzinler (";
                string values = "Values (";
                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " PersonelAdi ";
                    values += "'" + textBox9.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " PersonelSoyadi ";
                    values += "'" + textBox7.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Pozisyon ";
                    values += "'" + comboBox13.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox12.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Branş  ";
                    values += "'" + comboBox12.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Sube ";
                    values += "'" + subeid[comboBox8.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += "İzinVeren ";
                    values += "'" + comboBox7.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " İzinTürü ";
                    values += "'" + comboBox6.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Aciklama ";
                    values += "'" + richTextBox3.Text + "'";
                    degisken = true;
                }


                filtre += ",PersonelID,BaslangicTarih,BitisTarih)";
                values += ",'"+id+ "', '" + dateTimePicker10.Value.Date.ToString("yyyyMMdd") + "', '" + dateTimePicker8.Value.Date.ToString("yyyyMMdd") + "')";
                filtre += values;
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                textBox9.Text = "";
             
                textBox7.Text = "";
                comboBox8.Text = "";
                comboBox7.Text = ""; comboBox6.Text = "";
                comboBox13.Text = "";
                comboBox12.Text = "";
                dateTimePicker10.Value = DateTime.Now;
                dateTimePicker8.Value = DateTime.Now;
                richTextBox3.Text = "";




                panel2.Visible = false;
            }

            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void İZİN_LİSTES_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            button14.Visible = false;
            griddoldur();
            int i = 0;
            string[,] dizi = new string[dataGridView1.RowCount-1, 7];
            baglan.Open();

            komut = new SqlCommand("Select İzinler.PersonelID,Personeller.Aktiflik,İzinler.PersonelAdi,İzinler.PersonelSoyadi,İzinler.Pozisyon,İzinler.Branş from Personeller join İzinler on İzinler.PersonelID=Personeller.ID", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                dizi[i, 0] = oku.GetValue(0).ToString();
                dizi[i, 1] = oku.GetValue(1).ToString();
                dizi[i, 2] = oku.GetValue(1).ToString();
                dizi[i, 3] = oku.GetValue(1).ToString();
                dizi[i, 4] = oku.GetValue(1).ToString();
                dizi[i, 5] = oku.GetValue(1).ToString();
                i++; 

            }
            
            baglan.Close();
            for (int j = 0; j < dataGridView1.RowCount-1; j++)
            {
                baglan.Open();
                SqlCommand degistir2 = new SqlCommand("update İzinler set Aktiflik=@a1,PersonelAdi=@a3,PersonelSoyadi=@a4,Pozisyon=@a5,Branş=@a6 where PersonelID=@a2 ", baglan);
                degistir2.Parameters.AddWithValue("@a2", Convert.ToInt32(dizi[j, 0]));
                degistir2.Parameters.AddWithValue("@a1", Convert.ToBoolean(dizi[j, 1]));
                degistir2.Parameters.AddWithValue("@a3", dizi[j, 2]);
                degistir2.Parameters.AddWithValue("@a4", dizi[j, 3]);
                degistir2.Parameters.AddWithValue("@a5", dizi[j, 4]);
                degistir2.Parameters.AddWithValue("@a6", dizi[j, 5]);


                degistir2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show(dizi[j, 0] + dizi[j, 1]);
            }

         
            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox1.Items.Add(oku4[0].ToString());
                comboBox5.Items.Add(oku4[0].ToString());



            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());
                comboBox4.Items.Add(oku2[0].ToString());



            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                comboBox11.Items.Add(oku3[0].ToString());
                comboBox8.Items.Add(oku3[0].ToString());

                subeid.Add((int)oku3[1]);



            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi + ' '+ p.Soyadi) PERSONEL from Personeller p join Sube ş on ş.ID=p.Sube where p.Pozisyon='YÖNETİM KURULU' and ş.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                comboBox7.Items.Add(oku5[0].ToString());
                comboBox10.Items.Add(oku5[0].ToString());



            }
            baglan.Close();
            comboBox6.Items.Add("YILLLIK İZİN");
            comboBox6.Items.Add("İDARİ İZİN");
            comboBox6.Items.Add("RAPORLU");
            comboBox9.Items.Add("YILLLIK İZİN");
            comboBox9.Items.Add("İDARİ İZİN");
            comboBox9.Items.Add("RAPORLU");


        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                baglan.Open();
                SqlCommand degistir = new SqlCommand("update İzinler set İzinTürü=@a7,BaslangicTarih=@a8,BitisTarih=@a9,Aciklama=@a10,İzinVeren=@a11 where ID=@a2 ", baglan);
                degistir.Parameters.AddWithValue("@a2", dataGridView1.Rows[secilen].Cells[0].Value.ToString());             
                degistir.Parameters.AddWithValue("@a8", dateTimePicker10.Value.Date);
                degistir.Parameters.AddWithValue("@a9", dateTimePicker8.Value.Date);
                degistir.Parameters.AddWithValue("@a7", comboBox6.Text);
                degistir.Parameters.AddWithValue("@a11", comboBox7.Text);
                degistir.Parameters.AddWithValue("@a10", richTextBox3.Text);
                degistir.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Güncellendi.");
                button14.Visible = false;
                button11.Visible = true;
                panel2.Visible = false;
                griddoldur();
                textBox9.Text = "";

                textBox7.Text = "";
                comboBox8.Text = "";
                comboBox7.Text = ""; comboBox6.Text = "";
                comboBox13.Text = "";
                comboBox12.Text = "";
                dateTimePicker10.Value = DateTime.Now;
                dateTimePicker8.Value = DateTime.Now;
                richTextBox3.Text = "";
           


            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }

        private void comboBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }
    }
}
