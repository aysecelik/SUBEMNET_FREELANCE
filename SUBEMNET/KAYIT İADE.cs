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
    public partial class KAYIT_İADE : Form
    {
        public KAYIT_İADE()
        {
            InitializeComponent();
        }

        private void textBox6_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel9.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        private void button21_Click(object sender, EventArgs e)
        {
            griddoldur();
            panel2.Visible = false;
        }
        void griddoldur()
        {

            try
            {
                dataGridView1.Columns.Clear();
                string filtre = "Select pm.ID,ş.ŞubeAdi 'ŞUBE',ö.ID 'ÖĞRENCİ ID',(ö.Adi + ö.Soyadi) 'ÖĞRENCİ',ö.Devre 'DEVRE',ö.Snf 'SINIF',pm.MİKTAR 'ÖDENECEK TUTAR', pm.ODENEN 'ÖDENEN',pm.Tarih TARİH,pm.Açıklama 'AÇIKLAMA'  from Kayıtiade pm join Ogrenci ö on ö.ID = pm.OgrId join Sube ş on ş.ID = pm.ŞUBE where ş.Okulid = '" + okulid + "'";

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
                    filtre += " pm.MİKTAR='" + textBox8.Text + "." + textBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false && string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " pm.tutar-pm.alınan='" + textBox5.Text + "." + textBox3.Text + "'";
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

                if (degisken == true)
                {
                    filtre += " and ";
                }
                filtre += " ö.Durum='" + 0 + "'";
                degisken = true;



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

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
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

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
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
                    komut = new SqlCommand("Select alınan from Kayıtiade where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
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
                        SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                        baglan.Open();

                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ODENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox16.Text + "," + textBox6.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p4", textBox16.Text + "." + textBox6.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();

                    }
                    panel2.Visible = false;
                    textBox14.Text = "";
                    textBox16.Text = "";
                    textBox5.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "";
                    griddoldur();

                }

            }
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

                komut = new SqlCommand("Select alınan from Kayıtiade where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
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
                    SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);

                    komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                    komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();
                }

                else
                {
                    baglan.Open();
                    SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ÖDENEN=@p3,alınan=@p4 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox23.Text + "," + textBox24.Text)));
                    komutgüncelle.Parameters.AddWithValue("@p4", textBox23.Text + "." + textBox24.Text);
                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();



                }

                panel2.Visible = false;
                baglan.Open();
                SqlCommand komutgüncelle3 = new SqlCommand("update ÇekSenet set Bozdur=@p1,ÇekDurumu=@p2 where ogrenciid='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString() + "' and miktar='" + textBox23.Text + "." + textBox24.Text + "'", baglan);
                komutgüncelle3.Parameters.AddWithValue("@p1", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                komutgüncelle3.Parameters.AddWithValue("@p2", "BOZDURULDU");
                komutgüncelle3.ExecuteNonQuery();
                baglan.Close();
            }

            textBox9.Text = "";
            dateTimePicker9.Value = DateTime.Now;
            griddoldur();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox18.Text == "0")
            {
                MessageBox.Show("DAHA FAZLA ÖDEME ALAMAZSINIZ.");
            }
            else
            {
                komut = new SqlCommand("Select alınan,TaksitSayisi from Kayıtiade where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
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
                    SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ÖDENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                    komutgüncelle.Parameters.AddWithValue("@p4", yenideger.Replace(",", "."));
                    komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);

                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();
                }
                else
                {
                    baglan.Open();
                    SqlCommand komutgüncelle = new SqlCommand("update Kayıtiade set ÖDENEN=@p3,alınan=@p4,TaksitSayisi=@p5 where ID='" + dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                    komutgüncelle.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox19.Text)));
                    komutgüncelle.Parameters.AddWithValue("@p4", textBox20.Text + "." + textBox19.Text);
                    komutgüncelle.Parameters.AddWithValue("@p5", taksit - 1);


                    komutgüncelle.ExecuteNonQuery();
                    baglan.Close();

                }

                panel2.Visible = false;
                textBox18.Text = "";
                textBox17.Text = "";
                textBox19.Text = "";
                textBox20.Text = "";
                griddoldur();
            }
        }
        int rows;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            rows = dataGridView1.CurrentCell.RowIndex;

            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {
                panel2.Visible = true;
                panel1.Visible = true;
                panel9.Visible = true;

                baglan.Open();
                string ödeme = "";
                komut = new SqlCommand("Select ÖdemeŞekli from Kayıtiade where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
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
                    komut = new SqlCommand("Select tutar,alınan from Kayıtiade where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
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
                    komut = new SqlCommand("Select tutar,alınan from Kayıtiade where ID='" + dataGridView1.Rows[rows].Cells[0].Value.ToString() + "'", baglan);
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

        private void KAYIT_İADE_Load(object sender, EventArgs e)
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
            komut = new SqlCommand("Select Sezon from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {
                comboBox1.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
        }
    }
}
