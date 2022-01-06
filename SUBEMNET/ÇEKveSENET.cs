using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class ÇEKveSENET : Form
    {
        public ÇEKveSENET()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView2.Visible = true;
            griddoldur();


        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        void griddoldur()
        {
            dataGridView2.Visible = true;
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ç.ID,ş.ŞubeAdi ŞUBE,ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.EvrakTipi 'EVRAK TİPİ',ç.AlınanEvrakAdı 'EVRAK ADI',ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR,ç.Vade as VADE,ç.Bozdur 'BOZDURMA TARİH'  from ÇekSenet ç join Sube ş on ş.ID=ç.Sube WHERE DURUM='ALACAK' and ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÇekveSenetAlınan");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
        
            dataGridView2.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ç.ID,ş.ŞubeAdi ŞUBE,ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR,ç.Vade as VADE,ç.Bozdur 'ÖDEME TARİH'  from ÇekSenet ç join Sube ş on ş.ID=ç.Sube where DURUM='BORÇ' and ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ÇekveSenetBorç");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
          
          



        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
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
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
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
                        for (int j = 0; j < 12; j++)
                        {
                            pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontTitle));

                        }
                    }


                }
                catch (NullReferenceException)
                {
                }
                PdfPTable pdfTable2 = new PdfPTable(dataGridView2.ColumnCount - 1);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable2.SpacingBefore = 20f;
                pdfTable2.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable2.WidthPercentage = 80; // hücre genişliği
                pdfTable2.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable2.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView2.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView2.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable2.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            pdfTable2.AddCell(new Phrase(dataGridView2.Rows[i].Cells[j].Value.ToString(), fontTitle));

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
                    title = new Paragraph("ALACAK", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable);
                    title = new Paragraph("BORÇ", titleFont);
                    title.Alignment = Element.ALIGN_LEFT;
                    pdfDoc.Add(title);
                    pdfDoc.Add(pdfTable2);
                    text = new Paragraph("NOT: " + richTextBox1.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel2.Visible = false;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox11.Text = "";
            textBox3.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox10.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView2.Columns.Clear();

                bool degisken = false;
                string filtre = "Select ç.ID,ş.ŞubeAdi ŞUBE,ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.EvrakTipi 'EVRAK TİPİ',ç.AlınanEvrakAdı 'EVRAK ADI',ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR,ç.Vade as VADE,ç.Bozdur 'BOZDURMA TARİH'  from ÇekSenet ç join Sube ş on ş.ID=ç.Sube where ş.Okulid='"+okulid+"' and ";
                string filtre2 = "Select ç.ID,ş.ŞubeAdi ŞUBE, ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR, ç.Vade as VADE,ç.Bozdur 'ÖDEME TARİH'  from ÇekSenet ç join Sube ş on ş.ID=ç.Sube where ş.Okulid='"+okulid+"' and ";
                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " ID = '" + textBox11.Text + "'";
                    filtre2 += " ID = '" + textBox11.Text + "'";

                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " EvrakTürü = '" + comboBox1.Text + "'";
                    filtre2 += " EvrakTürü = '" + comboBox1.Text + "'";

                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    filtre2 += " Sube = '" + subeid[comboBox3.SelectedIndex] + "'";

                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " EvrakTipi  ='" + comboBox10.Text + "'";
                    filtre2 += " EvrakTipi  ='" + comboBox10.Text + "'";
                    dataGridView2.Visible = false;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " AlınanEvrakAdı  ='" + textBox3.Text + "'";
                    filtre2 += " AlınanEvrakAdı  ='" + textBox3.Text + "'";

                    dataGridView2.Visible = false;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " Tedarikçi  ='" + textBox5.Text + "'";
                    filtre2 += " Tedarikçi  ='" + textBox5.Text + "'";

                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " Sahibi  ='" + textBox6.Text + "'";
                    filtre2 += " Sahibi  ='" + textBox6.Text + "'";

                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    if (comboBox2.SelectedIndex == 1)
                    {
                        filtre += " Çekdurumu  ='" + "BOZDURULDU" + "'";
                        filtre2 += " Çekdurumu  ='" + "ÖDENDİ" + "'";
                    }
                    else
                    {
                        filtre += " Çekdurumu  ='" + "BOZDURULMADI" + "'";
                        filtre2 += " Çekdurumu  ='" + "ÖDENMEDİ" + "'";
                    }

                    dataGridView2.Visible = false;
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false && string.IsNullOrEmpty(textBox8.Text)==false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " miktar between '" + textBox7.Text + ".00" + "' and '" + textBox8.Text + ".00" + "'";
                    filtre2 += " miktar between '" + textBox7.Text + ".00" + "' and '" + textBox8.Text + ".00" + "'";

                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                        filtre2 += " AND ";

                    }
                    filtre += " Vade between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    filtre2 += " Vade between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";

                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox10.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(textBox3.Text) == true && string.IsNullOrEmpty(textBox7.Text) == true && string.IsNullOrEmpty(textBox5.Text) == true && string.IsNullOrEmpty(textBox6.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox3.Text) && checkBox1.Checked == false)
                {
                    filtre = "Select ç.ID,ş.ŞubeAdi ŞUBE,ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.EvrakTipi 'EVRAK TİPİ',ç.AlınanEvrakAdı 'EVRAK ADI',ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR,ç.Vade as VADE,ç.Bozdur 'BOZDURMA TARİH'  from ÇekSenet ç join Sube ş on ç.Sube=ş.ID where ş.Okulid='"+okulid+"' and ";
                    filtre2 = "Select ç.ID,ş.ŞubeAdi ŞUBE, ç.EvrakTürü 'EVRAK TÜRÜ',ç.Sahibi 'SAHİBİ',ç.BANKA,ç.ÇekNo 'ÇEK NO',ç.Tedarikçi 'TEDARİKÇİ',ç.Tutar TUTAR, ç.Vade as VADE,ç.Bozdur 'ÖDEME TARİH'  from ÇekSenet ç join Sube ş on ş.ID=ç.Sube where ş.Okulid='"+okulid+"' and ";


                }
                if (degisken == true)
                {
                    filtre += " AND ";
                    filtre2 += " AND ";

                }
                filtre += " DURUM='ALACAK'";
                filtre2 += " DURUM='BORÇ'";

                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÇekveSenetAlınan");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
             
                baglan.Open();
                da = new SqlDataAdapter(filtre2, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÇekveSenetBorç");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
            
                panel2.Visible = false;
               


            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        private void ÇEKveSENET_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
         
            comboBox3.Items.Add("");
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
             
                subeid.Add((int)oku3[1]);



            }
          


          
            comboBox10.Items.Add("ÖĞRENCİ");
            comboBox10.Items.Add("MÜŞTERİ");
          
            comboBox1.Items.Add("ÇEK");
            comboBox1.Items.Add("SENET");
         
            comboBox2.Items.Add("BOZDURULMUŞ EVRAKLAR");
            comboBox2.Items.Add("BOZDURULMAMIŞ EVRAKLAR");
            baglan.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
        }

       
       

       
        

      

      

       

        private void comboBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox20_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
               

        }
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
