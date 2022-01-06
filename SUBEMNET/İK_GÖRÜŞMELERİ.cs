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
using Microsoft.VisualBasic;

namespace SUBEMNET
{
    public partial class İK_GÖRÜŞMELERİ : Form
    {
        public İK_GÖRÜŞMELERİ()
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
            da = new SqlDataAdapter("Select i.ID,ş.ŞubeAdi ŞUBE,i.TCKimlikNo TC, i.Adı ADI,i.Soyadı 'SOYADI',i.Brans BRANŞ,i.Pozisyon POZİSYON, i.IsDeneyimi 'İŞ DENEYİMİ',i.Puan 'DEĞERLENDİRME PUANI',i.GörüşmeTarihi 'GÖRÜŞME TARİHİ',i.Aktiflik 'AKTİFLİK'  from IK_Görüsmeleri i join Sube ş on ş.ID=i.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "IK");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "PUAN";
            dgvBtn.Text = "DEĞERLENDİR";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);

        }
        SqlCommand komut;

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select i.ID,ş.ŞubeAdi ŞUBE,i.TCKimlikNo TC, i.Adı ADI,i.Soyadı 'SOYADI',i.Brans BRANŞ,i.Pozisyon POZİSYON, i.IsDeneyimi 'İŞ DENEYİMİ',i.Puan 'DEĞERLENDİRME PUANI',i.GörüşmeTarihi 'GÖRÜŞME TARİHİ',i.Aktiflik 'AKTİFLİK'  from IK_Görüsmeleri i join Sube ş on ş.ID=i.Sube where ş.Okulid='"+okulid+"'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";
                    filtre += " i.ID = '" + textBox11.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.TCKimlikNo = '" + textBox9.Text + "'";
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
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Adı = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Soyadı = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.GörüşmeTarihi between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
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
                if (string.IsNullOrEmpty(textBox12.Text) == false && string.IsNullOrEmpty(textBox14.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.IsDeneyimi between '" + textBox12.Text + "' and '" + textBox14.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false && string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Puan between '" + textBox7.Text + "' and '" + textBox13.Text + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox12.Text) == true && string.IsNullOrEmpty(textBox14.Text) == true && string.IsNullOrEmpty(textBox7.Text) == true && string.IsNullOrEmpty(textBox13.Text) == true && string.IsNullOrEmpty(textBox9.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(comboBox3.Text) && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select i.ID,ş.ŞubeAdi ŞUBE,i.TCKimlikNo TC, i.Adı ADI,i.Soyadı 'SOYADI',i.Brans BRANŞ,i.Pozisyon POZİSYON, i.IsDeneyimi 'İŞ DENEYİMİ',i.Puan 'DEĞERLENDİRME PUANI',i.GörüşmeTarihi 'GÖRÜŞME TARİHİ',i.Aktiflik 'AKTİFLİK'  from IK_Görüsmeleri i join Sube ş on ş.ID=i.Sube where ş.Okulid='" + okulid + "'";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "IK");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "PUAN";
                dgvBtn.Text = "DEĞERLENDİR";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                panel2.Visible = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox12.Text = "";
                textBox14.Text = "";
                textBox7.Text = "";
                textBox13.Text = "";

                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";

                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                checkBox1.Checked = false;
                checkBox2.Checked = false;


            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();

        private void İK_GÖRÜŞMELERİ_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            panel10.Visible = false;
            panel2.Visible = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            dateTimePicker6.Format = DateTimePickerFormat.Custom;
            dateTimePicker6.CustomFormat = "dd/MM/yyyy";
            comboBox1.Items.Add("");
            comboBox2.Items.Add("");
            comboBox4.Items.Add("");
            comboBox5.Items.Add("");

            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());
                comboBox5.Items.Add(oku[0].ToString());
                comboBox7.Items.Add(oku[0].ToString());



            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());
                comboBox4.Items.Add(oku2[0].ToString());
                comboBox6.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                comboBox8.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Ekleme platformu
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            panel9.Visible = false;
            MessageBox.Show("LÜTFEN GÖRÜŞME YAPMAK İSTEDİĞİNİZ KİŞİNİN ID'SİNİN ÜZERİNE TIKLAYINIZ.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount - 3);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 100; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 10; j++)
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

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select ID,Email 'E-POSTA',Adi ADI,Soyadi SOYADI ,CepTel TELEFON,Pozisyon POZİSYON,Branş BRANŞ ,CVDosya CV, SicilDosya 'SİCİL KAYDI', SaglikRaporDosya 'SAĞLIK RAPORU'from IsBasvuru where okulid='"+okulid+"'", baglan);

            cmdb = new SqlCommandBuilder(da);

            ds = new DataSet();
            da.Fill(ds, "IsBasvuru");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView2.CurrentCell.ColumnIndex == 7)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select CV from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }
                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 8)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select Sicil from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }

                }
                else if (dataGridView2.CurrentCell.ColumnIndex == 9)
                {
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView2.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView2.Rows[secilen].Cells[0].Value.ToString() + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + dataGridView2.Rows[secilen].Cells[3].Value.ToString() + ".pdf";

                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select SaglikRapor from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }


                }
                else
                {
                    textBox19.Text = "";
                    textBox18.Text = "";
                    textBox17.Text = "";
                    textBox16.Text = "";
                    comboBox8.Text = "";
                    comboBox7.Text = "";
                    comboBox6.Text = "";
                    dateTimePicker6.Value = DateTime.Now;
                    int secilen;
                    secilen = dataGridView2.CurrentCell.RowIndex;                   
                    panel2.Visible = true;
                    panel1.Visible = true;
                    panel5.Visible = true;
                    panel9.Visible = true;
                    panel7.Visible = true;
                    baglan.Open();
                    komut = new SqlCommand("Select Adi,Soyadi,Pozisyon,Branş,Is_Deneyimi,TcKimlikNo from IsBasvuru where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        textBox18.Text = oku.GetValue(0).ToString();
                        textBox17.Text = oku.GetValue(1).ToString();
                        comboBox7.Text = oku.GetValue(2).ToString();
                        comboBox6.Text = oku.GetValue(3).ToString();
                        textBox16.Text = oku.GetValue(4).ToString();
                        textBox19.Text= oku.GetValue(5).ToString();
                    }
                    baglan.Close();
                    
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false; 
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
            panel7.Visible = false;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select ID,Email 'E-POSTA',Adi ADI,Soyadi SOYADI ,CepTel TELEFON,Pozisyon POZİSYON,Branş BRANŞ ,CVDosya CV, SicilDosya 'SİCİL KAYDI', SaglikRaporDosya 'SAĞLIK RAPORU'from IsBasvuru where okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    filtre += " AND ";

                    filtre += " ID = '" + textBox3.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Email = '" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Pozisyon = '" + comboBox5.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Branş = '" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Adi = '" + textBox6.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Soyadi = '" + textBox5.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " CepTel = '" + maskedTextBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BasvuruTarihi between '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox3.Text) == true && string.IsNullOrEmpty(textBox15.Text) == true && string.IsNullOrEmpty(textBox6.Text) == true && string.IsNullOrEmpty(textBox5.Text) == true && string.IsNullOrEmpty(comboBox5.Text) == true && string.IsNullOrEmpty(comboBox4.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox3.Checked == false)
                {
                    filtre = "Select ID,Email 'E-POSTA',Adi ADI,Soyadi SOYADI ,CepTel TELEFON,Pozisyon POZİSYON,Branş BRANŞ ,CVDosya CV, SicilDosya 'SİCİL KAYDI', SaglikRaporDosya 'SAĞLIK RAPORU'from IsBasvuru where okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "IsBasvuru");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                textBox3.Text = "";
                textBox15.Text = "";
                textBox6.Text = "";
                textBox5.Text = "";
                maskedTextBox1.Text = "";
                comboBox5.Text = "";
                comboBox4.Text = "";
                dateTimePicker4.Value = DateTime.Now;
                dateTimePicker3.Value = DateTime.Now;
                checkBox3.Checked = false;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            
                try
                {
                    bool degisken = false;
                    string filtre = "insert into IK_Görüsmeleri (";
                    string values = "Values (";
                    if (string.IsNullOrEmpty(textBox19.Text) == false)
                    {
                        filtre += " TCKimlikNo ";
                        values += "'" + textBox19.Text + "'";
                        degisken = true;
                    }


                    if (string.IsNullOrEmpty(comboBox7.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Pozisyon ";
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
                        filtre += " Brans  ";
                        values += "'" + comboBox6.Text + "'";
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

                    if (string.IsNullOrEmpty(textBox18.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Adı ";
                        values += "'" + textBox18.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox17.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Soyadı ";
                        values += "'" + textBox17.Text.ToUpper() + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox16.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " IsDeneyimi ";
                        values += "'" + textBox16.Text + "'";
                        degisken = true;
                    }
                    filtre += ",GörüşmeTarihi)";
                    values += ", '" + dateTimePicker6.Value.Date.ToString("yyyyMMdd") + "')";
                    filtre += values;
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    textBox19.Text = "";
                    textBox18.Text = "";
                    textBox17.Text = "";
                    textBox16.Text = "";
                    comboBox8.Text = "";
                    comboBox7.Text = "";
                    comboBox6.Text = "";
                    dateTimePicker6.Value = DateTime.Now;



                    panel2.Visible = false;
                }

                catch (Exception a)
                {
                    MessageBox.Show("hata");
                }
            }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex==11)
            {

                int secilen = dataGridView1.CurrentCell.RowIndex;
                panel10.Visible = true;
                label38.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "  " + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[4].Value.ToString() + " DEĞERLENDİRME";
               
            }
        }

        private void panel10_LocationChanged(object sender, EventArgs e)
        {
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            int secilen = dataGridView1.CurrentCell.RowIndex;
            baglan.Open();
            SqlCommand degistir = new SqlCommand("update IK_Görüsmeleri set Puan=@a1, Aktiflik=@a3 where ID=@a2 ", baglan);
            degistir.Parameters.AddWithValue("@a2", dataGridView1.Rows[secilen].Cells[0].Value.ToString());
            degistir.Parameters.AddWithValue("@a3", Convert.ToBoolean("true"));
            degistir.Parameters.AddWithValue("@a1", Convert.ToInt16(textBox20.Text));
            degistir.ExecuteNonQuery();
            baglan.Close();
            MessageBox.Show("Kayıt Güncellendi.");
            griddoldur();
            panel10.Visible = false;
            textBox20.Text = "";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
            textBox20.Text = "";
        }
    }
}
