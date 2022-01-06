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
    public partial class GİDERLİSTE : Form
    {
        public GİDERLİSTE()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
            panel2.Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select g.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ FİRMA',g.NO 'FATURA/FİŞ NO' ,g.GiderKalemi 'GİDER KALEMİ',g.Miktar 'TUTAR' ,g.ODENEN 'ÖDENEN', g.Açıklama 'AÇIKLAMA' from Giderler g join Sube ş on ş.ID=g.Sube join Tedarikçiler t on g.Tedarikçi=t.ID where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GİDER");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "DETAY";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select g.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ FİRMA',g.NO 'FATURA/FİŞ NO', g.GiderKalemi 'GİDER KALEMİ',g.Miktar 'TUTAR' ,g.ODENEN 'ÖDENEN', g.Açıklama 'AÇIKLAMA' from Giderler g join Sube ş on ş.ID=g.Sube join Tedarikçiler t on g.Tedarikçi=t.ID   where t.Tedarikçi='" + textBox6.Text + "' and ş.Okulİd='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GELİR");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "DETAY";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 8)
            {
                baglan.Open();
                komut = new SqlCommand("select * from Giderler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    if (oku3[1] is DBNull)
                    {
                        textBox5.Text = "";
                        dateTimePicker3.Format = DateTimePickerFormat.Custom;
                        dateTimePicker3.CustomFormat = " ";
                    }
                    else
                    {
                        textBox5.Text = oku3[1].ToString();
                        dateTimePicker3.Value = Convert.ToDateTime(oku3[10]);
                    }
                    dateTimePicker5.Value= Convert.ToDateTime(oku3[2]);
                    comboBox1.Text = comboBox5.Items[subeid.IndexOf((int)oku3[9])].ToString();
                    comboBox10.Text = comboBox10.Items[Tedarikçi.IndexOf((int)oku3[3])].ToString();
                    comboBox9.Text = oku3[4].ToString();
                    comboBox8.Text = oku3[7].ToString();
                    textBox21.Text = (string.Format("{0:N}", (decimal)oku3[6])) + " TL";
                    richTextBox4.Text = oku3[7].ToString();

                }
                baglan.Close();
                panel1.Visible = true;
                panel7.Visible = true;
                panel2.Visible = true;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            comboBox5.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            dateTimePicker1.Value = DateTime.Now.Date;
            dateTimePicker2.Value = DateTime.Now.Date;
            checkBox1.Checked = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = true;
                string filtre = "Select g.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ FİRMA',g.No 'FATURA/FİŞ NO' g.GiderKalemi 'GİDER KALEMİ',g.Miktar 'TUTAR' ,g.ODENEN 'ÖDENEN', g.Açıklama 'AÇIKLAMA' from Giderler g join Sube ş on ş.ID=g.ŞUBE join Tedarikçiler t on g.Tedarikçi=t.ID  where ş.okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {

                    filtre += " AND ";

                    filtre += " ş.ŞubeAdi=" + "'" + comboBox5.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tedarikçi=" + "'" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " g.GiderKalemi=" + "'" + comboBox3.Text + "'";
                    degisken = true;
                }
              
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " g.Tarih between " + "'" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text) == false && string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " g.tutar between '" + textBox1.Text + ".00" + "' and '" + textBox3.Text + ".00" + "'";
                    degisken = true;
                }



                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "GİDER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
                dgvBtn3.HeaderText = "DETAY";
                dgvBtn3.Text = "DETAY";
                dgvBtn3.UseColumnTextForButtonValue = true;
                dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn3.Width = 70;
                dataGridView1.Columns.Add(dgvBtn3);
                textBox1.Text = "";
                textBox3.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox5.Text = "";
                dateTimePicker1.Value = DateTime.Now.Date;
                dateTimePicker2.Value = DateTime.Now.Date;

                checkBox1.Checked = false;
                panel1.Visible = false;




            }

            catch (Exception A)
            {
                baglan.Close();
                MessageBox.Show(A.ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            comboBox1.Text = "";
            comboBox10.Text = "";
            comboBox9.Text = "";
            comboBox8.Text = "";
            textBox21.Text = "";
            richTextBox4.Text = "";
            textBox5.Text = "";
            dateTimePicker3.Value = DateTime.Now.Date;
            dateTimePicker5.Value = DateTime.Now.Date;
        }
        List<int> Tedarikçi = new List<int>();
        List<int> tedarikçiid = new List<int>();
        List<int> subeid = new List<int>();
        private void GİDERLİSTE_Load(object sender, EventArgs e)
        {
            Tedarikçi.Clear();
            tedarikçiid.Clear();
            subeid.Clear();
           
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";

            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox5.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }
            baglan.Close();
            komut = new SqlCommand("Select t.ID,t.Tedarikçi from Tedarikçiler t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox10.Items.Add(oku2[1].ToString());
                Tedarikçi.Add((int)oku2[0]);


            }
            baglan.Close();
            comboBox2.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            comboBox3.Items.Clear();
            komut = new SqlCommand("Select GiderKalemi from GiderKalemi where okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            komut = new SqlCommand("Select ID,Tedarikçi from Tedarikçiler where sube='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[1].ToString());
                tedarikçiid.Add((int)oku2[0]);

            }
            baglan.Close();
        }

        private void textBox6_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }
    }
}

