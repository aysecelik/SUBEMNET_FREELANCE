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
    public partial class TEDARIKCILER : Form
    {
        public TEDARIKCILER()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ',t.Fax 'FAX',t.Adres 'ADRES' from Tedarikçiler t join Sube ş on t.sube=ş.ID where ş.Okulid='" + okulid + "' and t.Tedarikçi='" + textBox6.Text + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "TEDARİKÇİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "DETAY";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
            DataGridViewButtonColumn dgvbtn = new DataGridViewButtonColumn();
            dgvbtn.HeaderText = "ÖDEME AL";
            dgvbtn.Text = "ÖDEME AL";
            dgvbtn.UseColumnTextForButtonValue = true;
            dgvbtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn.Width = 70;
            dataGridView1.Columns.Add(dgvbtn);
        }
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;

        private void textBox6_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }
        void griddoldur()
        {

            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.TOPLAM,t.ÖDENEN,t.Fax 'FAX',t.Adres 'ADRES' from Tedarikçiler t join Sube ş on t.sube=ş.ID where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "TEDARİKÇİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "DETAY";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
            DataGridViewButtonColumn dgvbtn = new DataGridViewButtonColumn();
            dgvbtn.HeaderText = "ÖDEME AL";
            dgvbtn.Text = "ÖDEME AL";
            dgvbtn.UseColumnTextForButtonValue = true;
            dgvbtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn.Width = 70;
            dataGridView1.Columns.Add(dgvbtn);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
            button5.Visible = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
            button11.Visible = true;
            button5.Visible = false;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;

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
                for (int i = 0; i < dataGridView1.ColumnCount - 2; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
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
                panel1.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            comboBox1.Text = "";
            textBox15.Text = "";
            textBox13.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            richTextBox2.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "insert into  Tedarikçiler(";
                string values = "Values (";

                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Sube ";
                    values += "'" + subeid[comboBox1.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Tedarikçi ";
                    values += "'" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Email ";
                    values += "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Telefon ";
                    values += "'" + maskedTextBox1.Text.Replace(" ", "") + "'";
                    degisken = true;
                }
                if (maskedTextBox2.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IBAN ";
                    values += "'" + maskedTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " FAX ";
                    values += "'" + textBox13.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Adres ";
                    values += "'" + richTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " VergiNo ";
                    values += "'" + textBox1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " VergiDairesi ";
                    values += "'" + textBox7.Text + "'";
                    degisken = true;
                }
                filtre += ")";
                values += ")";
                filtre += values;
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                comboBox1.Text = "";
                textBox15.Text = "";
                textBox13.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                richTextBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
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
            try
            {
                dataGridView1.Columns.Clear();
                bool degisken = false;
                string filtre = "Select t.ID,t.Tedarikçi 'TEDARİKÇİ',t.TOPLAM,t.ÖDENEN,t.Fax 'FAX',t.Adres 'ADRES' from Tedarikçiler t join Sube ş on t.sube=ş.ID where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {

                    filtre += " AND ";

                    filtre += " t.Sube=" + "'" + subeid[comboBox1.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tedarikçi= " + "'" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Email= " + "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Telefon= " + "'" + maskedTextBox1.Text.Replace(" ", "") + "'";
                    degisken = true;
                }
                if (maskedTextBox2.MaskFull == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.IBAN= " + "'" + maskedTextBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox13.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.FAX= " + "'" + textBox13.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Adres= " + "'" + richTextBox2.Text + "'";
                    degisken = true;
                }

                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "TEDARİKÇİLER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
                dgvBtn3.HeaderText = "DETAY";
                dgvBtn3.Text = "DETAY";
                dgvBtn3.UseColumnTextForButtonValue = true;
                dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn3.Width = 70;
                dataGridView1.Columns.Add(dgvBtn3);
                DataGridViewButtonColumn dgvbtn = new DataGridViewButtonColumn();
                dgvbtn.HeaderText = "ÖDEME AL";
                dgvbtn.Text = "ÖDEME AL";
                dgvbtn.UseColumnTextForButtonValue = true;
                dgvbtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvbtn.Width = 70;
                dataGridView1.Columns.Add(dgvbtn);
                comboBox1.Text = "";
                textBox15.Text = "";
                textBox13.Text = "";
                textBox7.Text = "";
                textBox1.Text = "";
                textBox3.Text = "";
                richTextBox2.Text = "";
                maskedTextBox1.Text = "";
                maskedTextBox2.Text = "";
                panel1.Visible = false;




            }

            catch (Exception A)
            {
                baglan.Close();
                MessageBox.Show(A.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBox23.Text == "0" && textBox24.Text == "0")
            {
                MessageBox.Show("BU İŞLEM GERÇEKLEŞTİRİLEMEZ.");
            }
            else
            {
                //ÇEK ÖDEME ALMA
                if (dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString() == "ÇEK")
                {

                    komut = new SqlCommand("Select ÖDENEN,ödeme from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[1] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[1];
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)).ToString();
                        textBox6.Text = yenideger.ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle12 = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle12.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle12.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle12.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle22 = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle22.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle22.Parameters.AddWithValue("@p2", textBox23.Text + "." + textBox24.Text);
                        komutgüncelle22.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur();
                    baglan.Open();
                    SqlCommand komutgüncelle = new SqlCommand("update Borçlar set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                    komutgüncelle.Parameters.AddWithValue("@p2", textBox23.Text + "." + textBox24.Text);
                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();
                    griddoldur2();
                    panel9.Visible = false;
                    baglan.Open();
                    SqlCommand komutgüncelle3 = new SqlCommand("update ÇekSenet set Bozdur=@p1,ÇekDurumu=@p2 where tedarikçiid='" + dataGridView1.Rows[index].Cells[0].Value.ToString() + "' and miktar='" + textBox23.Text + "." + textBox24.Text + "'", baglan);
                    komutgüncelle3.Parameters.AddWithValue("@p1", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                    komutgüncelle3.Parameters.AddWithValue("@p2", "ÖDENDİ");
                    komutgüncelle3.ExecuteNonQuery();
                    baglan.Close();
                }
                else
                {
                    komut = new SqlCommand("Select ÖDENEN,ödeme from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[1] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[1];
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)).ToString();
                        textBox6.Text = yenideger.ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle12 = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle12.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle12.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle12.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle22 = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle22.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                        komutgüncelle22.Parameters.AddWithValue("@p2", textBox23.Text + "." + textBox24.Text);
                        komutgüncelle22.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur();
                    baglan.Open();
                    SqlCommand komutgüncelle = new SqlCommand("update Borçlar set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                    komutgüncelle.Parameters.AddWithValue("@p2", textBox23.Text + "." + textBox24.Text);
                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();
                    griddoldur2();
                    panel9.Visible = false;
                    baglan.Open();
                    SqlCommand komutgüncelle3 = new SqlCommand("update ÇekSenet set bozdur=@p1,ÇekDurumu=@p2 where tedarikçiid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "' and miktar='" + textBox23.Text + "." + textBox24.Text + "'", baglan);
                    komutgüncelle3.Parameters.AddWithValue("@p1", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                    komutgüncelle3.Parameters.AddWithValue("@p2", "ÖDENDİ");
                    komutgüncelle3.ExecuteNonQuery();
                    baglan.Close();
                }
                textBox9.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                griddoldur3();

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        List<int> subeid = new List<int>();
        SqlCommand komut;
        private void TEDARIKCILER_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            comboBox1.Items.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox1.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }

            baglan.Close();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
        }
        int index;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                baglan.Open();
                komut = new SqlCommand("select * from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    comboBox1.Text = comboBox1.Items[subeid.IndexOf((int)oku3[9])].ToString();
                    textBox15.Text = oku3[1].ToString();
                    textBox3.Text = oku3[4].ToString();
                    textBox13.Text = oku3[3].ToString();
                    richTextBox2.Text = oku3[6].ToString();
                    maskedTextBox1.Text = oku3[2].ToString();
                    maskedTextBox2.Text = oku3[5].ToString();
                    textBox7.Text = oku3[7].ToString();
                    textBox1.Text = oku3[8].ToString();

                }
                baglan.Close();
                panel1.Visible = true;
                panel7.Visible = true;
                button11.Visible = false;
                button5.Visible = false;
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {

                panel1.Visible = true;
                panel7.Visible = true;
                panel3.Visible = true;
                index = dataGridView1.CurrentCell.RowIndex;
                griddoldur2();
                griddoldur3();

            }
        }
        void griddoldur2()
        {
            dataGridView2.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select g.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ FİRMA',g.NO 'FATURA/FİŞ NO' ,g.GiderKalemi 'GİDER KALEMİ',g.Miktar 'TUTAR' ,g.ODENEN 'ÖDENEN', g.Açıklama 'AÇIKLAMA' from Giderler g join Sube ş on ş.ID=g.Sube join Tedarikçiler t on g.Tedarikçi=t.ID where  g.Tedarikçi='" + dataGridView1.Rows[index].Cells[0].Value.ToString() + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "GİDER-TEDARİKÇİ");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
            dgvbtn2.HeaderText = "ÖDEME YAP";
            dgvbtn2.Text = "ÖDEME YAP";
            dgvbtn2.UseColumnTextForButtonValue = true;
            dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn2.Width = 70;
            dataGridView2.Columns.Add(dgvbtn2);
        }
        void griddoldur3()
        {
            dataGridView3.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select g.ID,ş.ŞubeAdi,t.Tedarikçi 'TEDARİKÇİ FİRMA', g.GiderKalemi 'GİDER KALEMİ',g.ÖdemeTürü 'ÖDEME TÜRÜ',g.Miktar 'TUTAR' ,g.ODENEN 'ÖDENEN', g.Açıklama 'AÇIKLAMA' from Borçlar g join Sube ş on ş.ID=g.Şube join Tedarikçiler t on g.Tedarikçi=t.ID   where g.Tedarikçi='" + dataGridView1.Rows[index].Cells[0].Value.ToString() + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BORÇ-TEDARİKÇİ");
            dataGridView3.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "DETAY";
            dgvBtn3.Text = "DETAY";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView3.Columns.Add(dgvBtn3);
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 8)
            {
                panel9.Visible = true;
                panel11.Visible = true;
                panel12.Visible = false;
                textBox14.Text = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[5].Value.ToString();
                komut = new SqlCommand("Select tutar,ödenen from Giderler where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
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
                }
                baglan.Close();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //GİDER ÖDEME ALMA 

            if (string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(textBox5.Text) == true)
            {
                MessageBox.Show("HİÇ BİR ALAN BOŞ BIRAKILAMAZ.");
            }
            else
            {
                if (Convert.ToDecimal(textBox21.Text + "," + textBox22.Text) < Convert.ToDecimal(textBox16.Text + "," + textBox5.Text))
                {
                    MessageBox.Show("KALAN MİKTARDAN DAHA FAZLA ÖDEME ALMANIZ İMKANSIZDIR.");
                }
                else
                {
                    komut = new SqlCommand("Select ÖDENEN,ödeme from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[1] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[1];
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox16.Text + "," + textBox5.Text)).ToString();
                        textBox6.Text = yenideger.ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox5.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox16.Text + "." + textBox5.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur();
                    komut = new SqlCommand("Select ODENEN,ödenen from Giderler where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal a = 0;
                    bool bayrak2 = false;
                    SqlDataReader oku4 = komut.ExecuteReader();
                    while (oku4.Read())
                    {
                        if (oku4[1] == DBNull.Value)
                        {
                            bayrak2 = false;

                        }
                        else
                        {
                            a = (decimal)oku4[1];
                            bayrak2 = true;
                        }
                    }
                    baglan.Close();
                    string yeni;
                    if (bayrak2 == true)
                    {
                        yeni = (a + Convert.ToDecimal(textBox16.Text + "," + textBox5.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Giderler set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yeni)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yeni.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Giderler set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox5.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox16.Text + "." + textBox5.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur2();
                    panel9.Visible = false;
                    textBox14.Text = "";
                    textBox16.Text = "";
                    textBox5.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "";
                }

            }

        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView3.CurrentCell.ColumnIndex == 8)
            {
                panel9.Visible = true;
                if (dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString() == "NAKİT")
                {
                    panel11.Visible = true;
                    panel12.Visible = true;
                    panel2.Visible = true;

                    textBox20.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[5].Value.ToString();
                    komut = new SqlCommand("Select tutar,ödenen from Borçlar where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
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
                        textBox17.Text = i1.ToString();
                        textBox18.Text = i2.ToString();
                    }
                    baglan.Close();

                }
                if (dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString() == "ÇEK" || dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[4].Value.ToString() == "SENET")
                {
                    panel11.Visible = true;
                    panel12.Visible = true;
                    panel2.Visible = false;

                    textBox9.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[5].Value.ToString();
                    komut = new SqlCommand("Select tutar,ödenen from Borçlar where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
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
                    }
                    baglan.Close();

                }

                baglan.Close();




            }

        }


        private void button16_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox25.Text) == true || string.IsNullOrEmpty(textBox19.Text) == true)
            {
                MessageBox.Show("HİÇ BİR ALAN BOŞ BIRAKILAMAZ.");
            }
            else
            {
                if (Convert.ToDecimal(textBox17.Text + "," + textBox18.Text) < Convert.ToDecimal(textBox25.Text + "," + textBox19.Text))
                {
                    MessageBox.Show("KALAN MİKTARDAN DAHA FAZLA ÖDEME ALMANIZ İMKANSIZDIR.");
                }
                else
                {
                    komut = new SqlCommand("Select ÖDENEN,ödeme from Tedarikçiler where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal i = 0;
                    bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[1] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            i = (decimal)oku3[1];
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yenideger;
                    if (bayrak == true)
                    {
                        yenideger = (i + Convert.ToDecimal(textBox25.Text + "," + textBox19.Text)).ToString();
                        textBox6.Text = yenideger.ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set ÖDENEN=@p1,ödeme=@p2 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox25.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox25.Text + "." + textBox19.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur();
                    komut = new SqlCommand("Select ODENEN,ödenen from Borçlar where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    baglan.Open();
                    decimal a = 0;
                    bool bayrak2 = false;
                    SqlDataReader oku4 = komut.ExecuteReader();
                    while (oku4.Read())
                    {
                        if (oku4[1] == DBNull.Value)
                        {
                            bayrak2 = false;

                        }
                        else
                        {
                            a = (decimal)oku4[1];
                            bayrak2 = true;
                        }
                    }
                    baglan.Close();
                    string yeni;
                    if (bayrak2 == true)
                    {
                        yeni = (a + Convert.ToDecimal(textBox25.Text + "," + textBox19.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Borçlar set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yeni)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yeni.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Borçlar set ODENEN=@p1,ödenen=@p2 where ID='" + dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox25.Text + "," + textBox19.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox25.Text + "." + textBox19.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    griddoldur3();
                    panel9.Visible = false;
                    textBox20.Text = "";
                    textBox25.Text = "";
                    textBox19.Text = "";
                    textBox17.Text = "";
                    textBox18.Text = "";
                }

            }
        }
    }
}


