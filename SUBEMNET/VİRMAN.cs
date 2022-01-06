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
    public partial class VİRMAN : Form
    {
        public VİRMAN()
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
            da = new SqlDataAdapter("Select v.ID,v.IslemTürü 'İŞLEM TÜRÜ',ş.ŞubeAdi ŞUBE,  v.BankaHesabı 'BANKA HESABI',v.ALINAN,v.VERİLEN, v.Tarih as TARİH,v.Açıklama As AÇIKLAMA  from Virman v join Sube ş on ş.ID=v.Sube where ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Virman");
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
            //ARAMA İŞLMEİ YÖNLENDİRME
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLEME İŞLMEİ YÖNLENDİRME
            panel2.Visible = true;
            panel1.Visible = true;
            panel7.Visible = true;
            comboBox8.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //YAZDIRMA İŞLEMİ YÖNLENDİRME
            panel2.Visible = true;
            panel1.Visible = true;
            panel7.Visible = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox11.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";
            comboBox10.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            richTextBox3.Text = "";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Columns.Clear();

                bool degisken = false;
                string filtre = "Select v.ID, v.IslemTürü 'İŞLEM TÜRÜ',ş.ŞubeAdi ŞUBE, v.BankaHesabı 'BANKA HESABI',v.ALINAN,v.VERİLEN, v.Tarih as TARİH,v.Açıklama As AÇIKLAMA   from Virman v join Sube ş on ş.ID=v.Sube where ş.Okulid='"+okulid+"' and";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " ID = '" + textBox11.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " IslemTürü = '" + comboBox1.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    degisken = true;
                }
               
                if (string.IsNullOrEmpty(comboBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BankaHesabı  ='" + comboBox10.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Açıklama  ='" + richTextBox3.Text + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox3.Text) && checkBox1.Checked == false)
                {
                    filtre = "Select v.ID, v.IslemTürü 'İŞLEM TÜRÜ',ş.ŞubeAdi ŞUBE, v.BankaHesabı 'BANKA HESABI',v.ALINAN,v.VERİLEN, v.Tarih as TARİH,v.Açıklama As AÇIKLAMA from Virman v join Sube ş on ş.ID=v.Sube where Okulid='"+okulid+"'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Virman");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
                dgvBtn3.HeaderText = "DETAY";
                dgvBtn3.Text = "DETAY";
                dgvBtn3.UseColumnTextForButtonValue = true;
                dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn3.Width = 70;
                dataGridView1.Columns.Add(dgvBtn3);

                panel2.Visible = false;
                textBox11.Text = "";
                comboBox1.Text = "";
                comboBox3.Text = "";
                comboBox10.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                checkBox1.Checked = false;
                richTextBox3.Text = "";


            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
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
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText));
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

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;
        SqlCommand komut;
        List<int> subeid = new List<int>();
        private void VİRMAN_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            panel5.Visible = false;
            panel6.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel12.Visible = false;
            panel13.Visible = false;
            comboBox8.Text = "";
            comboBox8.Items.Add("ŞUBE İÇİ BANKADAN NAKİT KASAYA VİRMAN");
            comboBox8.Items.Add("ŞUBE İÇİ NAKİT KASADAN BANKAYA VİRMAN");
            comboBox8.Items.Add("ŞUBE İÇİ VİSADAN BANKAYA VİRMAN");
            comboBox8.Items.Add("ŞUBE İÇİ BANKADAN BANKAYA VİRMAN");
            comboBox8.Items.Add("ŞUBELER ARASI NAKİT VİRMAN");
            comboBox8.Items.Add("ŞUBELER ARASI BANKADAN BANKAYA VİRMAN");
            comboBox8.Items.Add("ŞUBELER ARASI BANKADAN NAKİT KASAYA VİRMAN");
            comboBox8.Items.Add("ŞUBELER ARASI NAKİT KASADAN BANKAYA VİRMAN");
            comboBox8.Items.Add("ŞUBELER ARASI ÇEK VİRMAN");
            comboBox1.Items.Add("");
            comboBox1.Items.Add("BANKADAN NAKİT KASAYA VİRMAN");
            comboBox1.Items.Add("NAKİT KASADAN BANKAYA VİRMAN");
            comboBox1.Items.Add("VİSADAN BANKAYA VİRMAN");
            comboBox1.Items.Add("BANKADAN BANKAYA VİRMAN");
            comboBox1.Items.Add("NAKİT VİRMAN");
            comboBox1.Items.Add("ÇEK VİRMAN");

            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                comboBox11.Items.Add(oku3[0].ToString());
                comboBox2.Items.Add(oku3[0].ToString());
                comboBox21.Items.Add(oku3[0].ToString());
                comboBox7.Items.Add(oku3[0].ToString());
                comboBox6.Items.Add(oku3[0].ToString());
                comboBox9.Items.Add(oku3[0].ToString());
                comboBox17.Items.Add(oku3[0].ToString());
                comboBox15.Items.Add(oku3[0].ToString());
                comboBox20.Items.Add(oku3[0].ToString());
                comboBox18.Items.Add(oku3[0].ToString());
                comboBox25.Items.Add(oku3[0].ToString());
                comboBox23.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select b.BankaHesabı from BankaHesabı b join Sube ş on ş.ID=b.Sube  where ş.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox10.Items.Add(oku[0].ToString());
        
            }
            baglan.Close();





        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel6.Visible = false;
            panel9.Visible = false;
            panel10.Visible = false;
            panel11.Visible = false;
            panel12.Visible = false;
            panel13.Visible = false;

            if (comboBox8.SelectedIndex == 0||  comboBox8.SelectedIndex == 1 || comboBox8.SelectedIndex == 2)
            {
                panel5.Visible = true;
                panel6.Visible = false;

            }
            if (comboBox8.SelectedIndex == 3)
            {
                panel5.Visible = false;
                panel6.Visible = true;
                panel9.Visible = false;

            }
            if (comboBox8.SelectedIndex == 4)
            {
                panel5.Visible =false;
                panel6.Visible = true;
                panel9.Visible = true;
                panel10.Visible = false;

            }
            if (comboBox8.SelectedIndex == 5)
            {
                panel5.Visible =false;
                panel6.Visible = true;
                panel9.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;

            }
            if (comboBox8.SelectedIndex == 6)
            {
                panel5.Visible = false;
                panel6.Visible = true;
                panel9.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
                panel12.Visible = false;



            }
            if (comboBox8.SelectedIndex == 7)
            {
                panel5.Visible =false;
                panel6.Visible = true;
                panel9.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
                panel12.Visible = true;
                panel13.Visible = false;

            }
            if (comboBox8.SelectedIndex == 8)
            {
                panel5.Visible = false;
                panel6.Visible = true;
                panel9.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
                panel12.Visible = true;
                panel13.Visible = true;

            }
        }
        string baglanid = "";
        private void button11_Click(object sender, EventArgs e)
        {
            //şube içi ilk üçü için işlem
            if (comboBox8.SelectedIndex == 0)
            {
                try
                {
                  
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                    komutkaydet.Parameters.AddWithValue("@a1","BANKADAN NAKİT KASAYA VİRMAN");
                    komutkaydet.Parameters.AddWithValue("@a2",DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@a4","0.00 TL");
                    komutkaydet.Parameters.AddWithValue("@a5",string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text))+ " TL");
                    komutkaydet.Parameters.AddWithValue("@a6",subeid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@a7",comboBox19.Text);
                    komutkaydet.Parameters.AddWithValue("@a8", 0);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        baglanid = oku.GetValue(0).ToString();
                    }
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                    komut2.Parameters.AddWithValue("@a1", baglanid);
                    komut2.Parameters.AddWithValue("@a2", baglanid);
                    komut2.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@a1", "BANKADAN NAKİT KASAYA VİRMAN");
                    komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet2.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text)) + " TL");
                    komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                    komutkaydet2.Parameters.AddWithValue("@a6",subeid[comboBox2.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@a7", "");
                    komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@a10", 0);

                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    richTextBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "00";
                    comboBox2.Text = "";
                    comboBox19.Text = "";
                    panel5.Visible = false;
                    panel2.Visible = false;


                }
                catch (Exception a)
                {

                    MessageBox.Show(a.ToString());
                }

            }
            if (comboBox8.SelectedIndex == 1)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                    komutkaydet.Parameters.AddWithValue("@a1", "NAKİT KASADAN BANKAYA VİRMAN");
                    komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                    komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text)) + " TL");
                    komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@a7", "");
                    komutkaydet.Parameters.AddWithValue("@a8", 0);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        baglanid = oku.GetValue(0).ToString();
                    }
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                    komut2.Parameters.AddWithValue("@a1", baglanid);
                    komut2.Parameters.AddWithValue("@a2", baglanid);
                    komut2.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@a1", "NAKİT KASADAN BANKAYA VİRMAN");
                    komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet2.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text)) + " TL");
                    komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                    komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox2.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@a7", comboBox19.Text);
                    komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@a10", 0);

                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    richTextBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "00";
                    comboBox2.Text = "";
                    comboBox19.Text = "";
                    panel5.Visible = false;
                    panel2.Visible = false;

                }
                catch (Exception)
                {

                    MessageBox.Show("HATA");
                }
            }
            if (comboBox8.SelectedIndex == 2)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                    komutkaydet.Parameters.AddWithValue("@a1", "VİSADAN BANKAYA VİRMAN");
                    komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                    komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text)) + " TL");
                    komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@a7", "");
                    komutkaydet.Parameters.AddWithValue("@a8", 0);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        baglanid = oku.GetValue(0).ToString();
                    }
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                    komut2.Parameters.AddWithValue("@a1", baglanid);
                    komut2.Parameters.AddWithValue("@a2", baglanid);
                    komut2.ExecuteNonQuery();
                    baglan.Close();
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@a1", "VİSADAN BANKAYA VİRMAN");
                    komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                    komutkaydet2.Parameters.AddWithValue("@a3", richTextBox2.Text);
                    komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox5.Text)) + " TL");
                    komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                    komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox2.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@a7", comboBox19.Text);
                    komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                    komutkaydet.Parameters.AddWithValue("@a9", textBox3.Text + "." + textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@a10", 0);



                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    richTextBox2.Text = "";
                    textBox3.Text = "";
                    textBox5.Text = "00";
                    comboBox2.Text = "";
                    comboBox19.Text = "";
                    panel5.Visible = false;
                    panel2.Visible = false;

                }
                catch (Exception)
                {

                    MessageBox.Show("HATA");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //şubeiçi banka banka
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "BANKADAN BANKAYA VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox4.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox7.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox21.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", comboBox4.Text);
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox6.Text + "." + textBox7.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "BANKADAN BANKAYA VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox4.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox7.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox21.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", comboBox5.Text);
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox6.Text + "." + textBox7.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox4.Text = "";
                textBox6.Text = "";
                textBox7.Text = "00";
                comboBox21.Text = "";
                comboBox5.Text = "";
                comboBox4.Text = "";
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;


            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //şube şube nakit
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "NAKİT VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox5.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox8.Text + "," + textBox9.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox7.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", "");
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox8.Text + "." + textBox9.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "NAKİT VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox5.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox8.Text + "," + textBox9.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox6.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", "");
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox8.Text + "." + textBox9.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox5.Text = "";
                textBox8.Text = "";
                textBox9.Text = "00";
                comboBox7.Text = "";
                comboBox6.Text = "";
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //şubebank şubebank
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "BANKADAN BANKAYA VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox6.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox10.Text + "," + textBox12.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox11.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", comboBox13.Text);
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox10.Text + "." + textBox12.Text);

                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "BANKADAN BANKAYA VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox6.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox10.Text + "," + textBox12.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox9.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", comboBox12.Text);
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox10.Text + "." + textBox12.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox6.Text = "";
                textBox10.Text = "";
                textBox12.Text = "00";
                comboBox11.Text = "";
                comboBox9.Text = "";
                comboBox13.Text = "";
                comboBox12.Text = "";
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //şubabank şube
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "BANKADAN NAKİT KASAYA VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox7.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox13.Text + "," + textBox14.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox17.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", comboBox16.Text);
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox13.Text + "." + textBox14.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "BANKADAN NAKİT KASAYA VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox7.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox13.Text + "," + textBox14.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox15.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", "");
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox13.Text + "." + textBox14.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox7.Text = "";
                textBox13.Text = "";
                textBox14.Text = "00";
                comboBox17.Text = "";
                comboBox16.Text = "";
                comboBox15.Text = "";   
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //şube şubebank
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "NAKİT KASADAN BANKAYA VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox8.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox15.Text + "," + textBox16.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox20.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", "");
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox15.Text + "." + textBox16.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "NAKİT KASADAN BANKAYA VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox8.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox15.Text + "," + textBox16.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox18.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", comboBox14.Text);
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox15.Text + "." + textBox16.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox8.Text = "";
                textBox15.Text = "";
                textBox16.Text = "00";
                comboBox20.Text = "";
                comboBox18.Text = "";
                comboBox14.Text = "";
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //şubeçek şube
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9)", baglan);
                komutkaydet.Parameters.AddWithValue("@a1", "ÇEK VİRMAN");
                komutkaydet.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet.Parameters.AddWithValue("@a3", richTextBox10.Text);
                komutkaydet.Parameters.AddWithValue("@a4", "0.00 TL");
                komutkaydet.Parameters.AddWithValue("@a5", string.Format("{0:N}", Convert.ToDecimal(textBox19.Text + "," + textBox20.Text)) + " TL");
                komutkaydet.Parameters.AddWithValue("@a6", subeid[comboBox25.SelectedIndex]);
                komutkaydet.Parameters.AddWithValue("@a7", "");
                komutkaydet.Parameters.AddWithValue("@a8", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox19.Text + "." + textBox20.Text);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
              
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('Virman')", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                SqlCommand komut2 = new SqlCommand("update Virman set Bağla=@a1 where ID=@a2", baglan);
                komut2.Parameters.AddWithValue("@a1", baglanid);
                komut2.Parameters.AddWithValue("@a2", baglanid);
                komut2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                SqlCommand komutkaydet2 = new SqlCommand("insert into Virman (IslemTürü, Tarih, Açıklama, ALINAN,VERİLEN,Sube,BankaHesabı,Bağla,alacak,verecek) VALUES (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8,@a9,@a10)", baglan);
                komutkaydet2.Parameters.AddWithValue("@a1", "ÇEK VİRMAN");
                komutkaydet2.Parameters.AddWithValue("@a2", DateTime.Now.Date.ToString("yyyyMMdd"));
                komutkaydet2.Parameters.AddWithValue("@a3", richTextBox10.Text);
                komutkaydet2.Parameters.AddWithValue("@a4", string.Format("{0:N}", Convert.ToDecimal(textBox19.Text + "," + textBox20.Text)) + " TL");
                komutkaydet2.Parameters.AddWithValue("@a5", "0.00 TL");
                komutkaydet2.Parameters.AddWithValue("@a6", subeid[comboBox23.SelectedIndex]);
                komutkaydet2.Parameters.AddWithValue("@a7", "");
                komutkaydet2.Parameters.AddWithValue("@a8", baglanid);
                komutkaydet.Parameters.AddWithValue("@a10", 0);
                komutkaydet.Parameters.AddWithValue("@a9", textBox19.Text + "." + textBox20.Text);

                komutkaydet2.ExecuteNonQuery();
                baglan.Close();
                baglan.Open();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                richTextBox10.Text = "";
                textBox19.Text = "";
                textBox20.Text = "00";
                comboBox25.Text = "";
                comboBox23.Text = "";
                comboBox24.Text = "";
                panel5.Visible = false;
                panel6.Visible = false;
                panel2.Visible = false;

            }
            catch (Exception)
            {

                MessageBox.Show("HATA");
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 8)
            {
                panel14.Visible = true;
                label85.Text = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + " ID NUMARASINA SAHİP İŞLEMİN DETAYI";
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT Bağla from Virman where ID='"+ dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() + "'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    baglanid = oku.GetValue(0).ToString();
                }
                baglan.Close();
                baglan.Open();
                da = new SqlDataAdapter("Select v.ID,v.IslemTürü 'İŞLEM TÜRÜ',ş.ŞubeAdi ŞUBE,  v.BankaHesabı 'BANKA HESABI',v.ALINAN,v.VERİLEN, v.Tarih as TARİH,v.Açıklama As AÇIKLAMA  from Virman v join Sube ş on ş.ID=v.Sube where Bağla='"+baglanid+"'", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Virman");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel14.Visible = false;
        }

        private void comboBox18_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox18.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox14.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox17_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox17.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox16.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox11.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox13.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox9.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox12.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox21_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox21.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox4.Items.Add(oku[0].ToString());
                comboBox5.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + comboBox2.Text + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox19.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox10.Items.Clear();
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" +subeid[comboBox3.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox10.Items.Add(oku[0].ToString());

            }
            baglan.Close();


        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBox19.Items.Clear();
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='" + subeid[comboBox2.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox19.Items.Add(oku[0].ToString());

            }
            baglan.Close();
        }
    }
}
