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
    public partial class PersonelDevamTakip : Form
    {
        public PersonelDevamTakip()
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
            da = new SqlDataAdapter("Select p.PersonelID 'ID',MAX(p.Personel) 'PERSONEL', MAX() Sube, MAX(p.Pozisyon) POZİSYON, MAX(p.GeçKalmaSayaç) 'GEÇ KALMA', MAX(p.GelmediSayaç) 'GELMEDİ' from PersonelDevamTakip p join Sube ş on ş.ID=p.Sube where ş.Okulid='"+okulid+"' group by PersonelID", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "PersonelDevam");
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
            panel5.Visible = true;
            panel9.Visible = false;
            MessageBox.Show("DEVAM TAKİP İŞLEMİ YAPILACAK OLAN PERSONELİN ID'SİNE TIKLAYINIZ.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //YAZDIRMA İŞLEMİ YÖNLENDİRME
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; ;
            textBox11.Text = "";
            comboBox3.Text = "";
            comboBox10.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox4.Checked = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select p.PersonelID 'ID',MAX(p.Personel) 'PERSONEL', MAX() Sube, MAX(p.Pozisyon) POZİSYON, MAX(p.GeçKalmaSayaç) 'GEÇ KALMA', MAX(p.GelmediSayaç) 'GELMEDİ' from PersonelDevamTakip p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'  ";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";
                    filtre += " p.PersonelID = '" + textBox11.Text + "'";
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

                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Durum = 'GEÇ KALDI'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Personel  ='" + comboBox10.Text + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "p.Tarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }

                filtre += " group by PersonelID";

                if (string.IsNullOrEmpty(comboBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox3.Text) && checkBox4.Checked == false && checkBox1.Checked == false)
                {
                    filtre = "Select p.PersonelID 'ID',MAX(p.Personel) 'PERSONEL', MAX() Sube, MAX(p.Pozisyon) POZİSYON, MAX(p.GeçKalmaSayaç) 'GEÇ KALMA', MAX(p.GelmediSayaç) 'GELMEDİ' from PersonelDevamTakip p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' group by PersonelID";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "PersonelDevam");
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
                checkBox4.Checked = false;



            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
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
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
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

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',p.CepTel 'TELEFON',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
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

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',p.CepTel 'TELEFON',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    filtre += " AND ";
                    filtre += " p.ID = '" + textBox3.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox15.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Email = '" + textBox15.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Pozisyon = '" + comboBox5.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Brans = '" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox11.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Sube = '" + subeid[comboBox11.SelectedIndex] + "'";
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
                if (string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == false)
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
                    filtre = "Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',p.CepTel 'TELEFON',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
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
        SqlCommand komut;
        string id;
        bool aktiflik;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.CurrentCell.RowIndex;
            id = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
            baglan.Open();
            komut = new SqlCommand("Select Adi,Soyadi,Sube,Pozisyon,Aktiflik from Personeller where ID = '" + dataGridView2.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox12.Text= oku.GetValue(0).ToString() + " "+
                oku.GetValue(1).ToString();
                comboBox8.Text = oku.GetValue(2).ToString();
                comboBox13.Text = oku.GetValue(3).ToString();
               
                aktiflik = (bool)oku.GetValue(4);
            }
            button11.Visible = true;
            panel9.Visible = true;
            panel7.Visible = true;
            baglan.Close();
           
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
          
            comboBox8.Text = "";
            comboBox7.Text = ""; 
            comboBox13.Text = "";
            comboBox12.Text = "";
            dateTimePicker10.Value = DateTime.Now;
          
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                int secilen = dataGridView2.CurrentCell.RowIndex;
                int varid=0;
                bool var=false;
                MessageBox.Show(dataGridView1.RowCount.ToString());
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == id)
                    {
                        varid = i;
                        var= true;
                    }
                }
             
                bool degisken = false;
                string filtre = "insert into PersonelDevamTakip (";
                string values = "Values (";
              

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
                    filtre += " Personel  ";
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
                    filtre += "Durum ";
                    values += "'" + comboBox7.Text + "'";
                    degisken = true;
                }
                if(comboBox7.Text=="GEÇ KALDI")
                {
                    if (var == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GeçKalmaSayaç ";
                        values += "'" + (Convert.ToInt32(dataGridView1.Rows[varid].Cells[4].Value.ToString()) +1) + "'";
                        degisken = true;
                    }
                    else
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GeçKalmaSayaç ";
                        values += "'" + 1 + "'";
                       
                        degisken = true;
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GelmediSayaç ";
                        values += "'" + 0 + "'";

                        degisken = true;
                    }
                }
                if (comboBox7.Text == "GELMEDİ")
                {
                    if (var == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GelmediSayaç ";
                        values += "'" + (Convert.ToInt32(dataGridView1.Rows[varid].Cells[5].Value.ToString()) + 1) + "'";
                        degisken = true;
                    }
                    else
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GelmediSayaç ";
                        values += "'" + 1 + "'";
                        degisken = true;
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += "GeçKalmaSayaç ";
                        values += "'" + 0 + "'";
                        degisken = true;
                    }
                }
                filtre += ",PersonelID,Tarih)";
                values += ",'" + id + "', '" + dateTimePicker10.Value.Date.ToString("yyyyMMdd") +  "')";
                filtre += values;
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
              
                comboBox8.Text = "";
                comboBox7.Text = ""; 
                comboBox13.Text = "";
                comboBox12.Text = "";
                dateTimePicker10.Value = DateTime.Now;
               
                panel2.Visible = false;
                             
            }

            catch (Exception a)
            {
                MessageBox.Show(a.ToString());
            }
        }
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void PersonelDevamTakip_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            comboBox7.Items.Add("GEÇ KALDI");
            comboBox7.Items.Add("GELMEDİ");
            comboBox5.Items.Add("");
            comboBox4.Items.Add("");
            comboBox11.Items.Add("");
            comboBox3.Items.Add("");
            comboBox1.Items.Add("");
            komut = new SqlCommand("Select (Adi + ' '+ Soyadi) from Personeller order by Adi", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox10.Items.Add(oku[0].ToString());
               
            }
            baglan.Close();
            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox5.Items.Add(oku4[0].ToString());
                comboBox1.Items.Add(oku4[0].ToString());




            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox4.Items.Add(oku2[0].ToString());



            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube Where Sube.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox11.Items.Add(oku3[0].ToString());
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }
            baglan.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex==6)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
               
                label70.Text=dataGridView1.Rows[secilen].Cells[1].Value.ToString()+" DETAYLI DURUMU";
                panel2.Visible = true;
                panel1.Visible = true;
                panel5.Visible = true;
                panel9.Visible = true;
                panel7.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;
                baglan.Open();
                da = new SqlDataAdapter("Select ID,Durum,Tarih from PersonelDevamTakip where PersonelID='"+dataGridView1.Rows[secilen].Cells[0].Value.ToString()+"'", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "PersonelDetaylıDevam");
                dataGridView3.DataSource = ds.Tables[0];
                baglan.Close();
              




            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel11.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
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
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount);

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 3; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 80; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount ; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 3; j++)
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
                    title = new Paragraph(textBox8.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox9.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox7.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);

                    pdfDoc.Add(pdfTable);
                    text = new Paragraph("NOT: " + richTextBox2.Text, regularFont);
                    pdfDoc.Add(text);
                    pdfDoc.Close();
                    stream.Close();
                }
                panel2.Visible = false;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void comboBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}