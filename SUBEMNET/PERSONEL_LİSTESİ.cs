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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Configuration;

namespace SUBEMNET
{
    public partial class PERSONEL_LİSTESİ : Form
    {
        public PERSONEL_LİSTESİ()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {//tümünü göster

            griddoldur();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;

        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        void griddoldur()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select ID,Adi as ADI,Soyadi as SOYADI,CepTel as TELEFON,Sube as ŞUBE,Pozisyon as POZİSYON,Brans as BRANŞ,SözleşmeDosya as SÖZLEŞME from Personeller", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();

        }
        SqlCommand komut;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void PERSONEL_LİSTESİ_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            griddoldur();

            comboBox1.Items.Add("");
            comboBox2.Items.Add("");


            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select p.ID,p.Adi as ADI,p.Soyadi as SOYADI,p.CepTel as TELEFON,ş.ŞubeAdi as ŞUBE,p.Pozisyon as POZİSYON,p.Brans as BRANŞ,p.SözleşmeDosya as SÖZLEŞME from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='"+okulid+"'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";

                    filtre += " p.ID = '" + textBox11.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Email = '" + textBox9.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Brans = '" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Sube = '" + comboBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Adi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Soyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (maskedTextBox1.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.CepTel = '" + maskedTextBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.IseBaslangıcTarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Aktiflik = '" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox9.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(comboBox3.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select p.ID,p.Adi as ADI,p.Soyadi as SOYADI,p.CepTel as TELEFON,ş.ŞubeAdi as ŞUBE,p.Pozisyon as POZİSYON,p.Brans as BRANŞ,p.SözleşmeDosya as SÖZLEŞME from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                maskedTextBox1.Text = "";
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

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox10.Text = "";
            textBox11.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            maskedTextBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox2.Checked = false;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 7)
                {
                    int secilen;
                    secilen = dataGridView1.SelectedCells[0].RowIndex;
                    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                    string fileExtension = fileInfo.Extension;
                    byte[] byteData = null;

                    using (SaveFileDialog savefile = new SaveFileDialog())
                    {
                        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                        savefile.Title = "Save File as";
                        savefile.CheckPathExists = true;
                        savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                        if (savefile.ShowDialog() == DialogResult.OK)
                        {
                            baglan.Open();
                            komut = new SqlCommand("Select Sözleşme from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                            SqlDataReader oku = komut.ExecuteReader();
                            oku.Read();
                            byteData = (byte[])oku[0];
                            File.WriteAllBytes(savefile.FileName, byteData);
                            baglan.Close();
                        }
                    }
                }
                //else if (dataGridView1.CurrentCell.ColumnIndex == 8)
                //{
                //    int secilen;
                //    secilen = dataGridView1.SelectedCells[0].RowIndex;
                //    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                //    string fileExtension = fileInfo.Extension;
                //    byte[] byteData = null;

                //    using (SaveFileDialog savefile = new SaveFileDialog())
                //    {
                //        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                //        savefile.Title = "Save File as";
                //        savefile.CheckPathExists = true;
                //        savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                //        if (savefile.ShowDialog() == DialogResult.OK)
                //        {
                //            baglan.Open();
                //            komut = new SqlCommand("Select SicilKaydı from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                //            SqlDataReader oku = komut.ExecuteReader();
                //            oku.Read();
                //            byteData = (byte[])oku[0];
                //            File.WriteAllBytes(savefile.FileName, byteData);
                //            baglan.Close();
                //        }
                //    }
                //}
                //if (dataGridView1.CurrentCell.ColumnIndex == 9)
                //{
                //    int secilen;
                //    secilen = dataGridView1.SelectedCells[0].RowIndex;
                //    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                //    string fileExtension = fileInfo.Extension;
                //    byte[] byteData = null;

                //    using (SaveFileDialog savefile = new SaveFileDialog())
                //    {
                //        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                //        savefile.Title = "Save File as";
                //        savefile.CheckPathExists = true;
                //        savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                //        if (savefile.ShowDialog() == DialogResult.OK)
                //        {
                //            baglan.Open();
                //            komut = new SqlCommand("Select SağlıkRaporu from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                //            SqlDataReader oku = komut.ExecuteReader();
                //            oku.Read();
                //            byteData = (byte[])oku[0];
                //            File.WriteAllBytes(savefile.FileName, byteData);
                //            baglan.Close();
                //        }
                //    }
                //}
                //if (dataGridView1.CurrentCell.ColumnIndex == 10)
                //{
                //    int secilen;
                //    secilen = dataGridView1.SelectedCells[0].RowIndex;
                //    FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                //    string fileExtension = fileInfo.Extension;
                //    byte[] byteData = null;

                //    using (SaveFileDialog savefile = new SaveFileDialog())
                //    {
                //        savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                //        savefile.Title = "Save File as";
                //        savefile.CheckPathExists = true;
                //        savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                //        if (savefile.ShowDialog() == DialogResult.OK)
                //        {
                //            baglan.Open();
                //            komut = new SqlCommand("Select SGKBelgesi from Personeller where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                //            SqlDataReader oku = komut.ExecuteReader();
                //            oku.Read();
                //            byteData = (byte[])oku[0];
                //            File.WriteAllBytes(savefile.FileName, byteData);
                //            baglan.Close();
                //        }
                //    }
                //}






            }
            catch (Exception A)
            {
                MessageBox.Show(A.ToString());
            }
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



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 7; j++)
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


    }
}
