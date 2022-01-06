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
    public partial class PRİM_KESİNTİ_KONTROL : Form
    {
        public PRİM_KESİNTİ_KONTROL()
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
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();

        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select pm.ID,(p.Adi + ' ' +p.Soyadi) 'PERSONEL',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ',pm.PrimKesinti 'DURUM',pm.MİKTAR,pm.Vade 'VADE',pm.Açıklama 'AÇIKLAMA'  from PrimKesinti pm join Personeller p on p.ID=pm.Personel join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Aktiflik='" + Convert.ToBoolean(true) + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "PRİM-KESİNTİ");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SİL";
            dgvBtn.Text = "SİL";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            panel9.Visible = false;
            MessageBox.Show("İZİN ALACAK OLAN PERSONELİN ID'SİNE TIKLAYINIZ.");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }
        SqlCommand komut;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 9)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "PRİM-KESİNTİ SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID'Lİ " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " PERSONELE AİT OLAN PRİM-KESİNTİYİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "PRİM-KESİNTİ SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE FROM PrimKesinti WHERE ID=@id";
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

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false; ;
            textBox10.Text = "";
            textBox8.Text = "";
            richTextBox2.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox10.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox4.Checked = false;

        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select pm.ID,(p.Adi + ' ' +p.Soyadi) 'PERSONEL',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ',p.PrimKesinti 'DURUM',p.MİKTAR,pm.Vade 'VADE',pm.Açıklama 'AÇIKLAMA'  from PrimKesinti pm join Personeller p on p.ID=pm.Personel join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Aktiflik='" + Convert.ToBoolean(true) + "'";


                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.Aciklama = '" + richTextBox2.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Branş = '" + comboBox2.Text + "'";
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
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.PrimKesinti veren  = '" + comboBox10.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.PersonelAdi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.PersonelSoyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.Vade between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
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
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " i.Aktiflik='" + Convert.ToBoolean(false) + "'";
                    degisken = true;
                }



                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "PRİM-KESİNTİ");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "SİL";
                dgvBtn2.Text = "SİL";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                panel2.Visible = false;
                textBox10.Text = "";
                textBox8.Text = "";

                richTextBox2.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox10.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
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



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < 9; j++)
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


        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox7.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(textBox11.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ GİRİNİZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into PrimKesinti (Personel, PrimKesinti, MİKTAR,tutar, Açıklama,Vade) values (@p1, @p2, @p3, @p4,@p5,@p6)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox7.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", string.Format("{0:N}", Convert.ToDecimal(textBox3.Text + "," + textBox11.Text)) + " TL");
                    komutkaydet.Parameters.AddWithValue("@p4", textBox3.Text + "." + textBox11.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", richTextBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p6", dateTimePicker10.Value.Year + " " + dateTimePicker10.Value.ToString("MMMM"));
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    panel2.Visible = false;
                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
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
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş ş.ID=p.Sube where ş.Okulid='" + okulid + "'";



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
                if (maskedTextBox1.MaskFull == true)
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





                if (string.IsNullOrEmpty(textBox15.Text) == true && string.IsNullOrEmpty(textBox6.Text) == true && string.IsNullOrEmpty(textBox5.Text) == true && string.IsNullOrEmpty(comboBox5.Text) == true && string.IsNullOrEmpty(comboBox4.Text) == true && string.IsNullOrEmpty(comboBox11.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox3.Checked == false)
                {
                    filtre = "Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş ş.ID=p.Sube where ş.Okulid='" + okulid + "'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel9.Visible = false;
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

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
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

        private void button5_Click(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi ADI, p.Soyadi SOYADI,p.CepTel TELEFON,ş.ŞubeAdi ŞUBE , p.Pozisyon POZİSYON ,p.Brans BRANŞ  from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
        }
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
            }
            button11.Visible = true;
            panel9.Visible = true;
            panel7.Visible = true;
            baglan.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox9.Text = "";

            textBox7.Text = "";
            comboBox8.Text = "";
            comboBox7.Text = "";
            comboBox13.Text = "";
            comboBox12.Text = "";
            dateTimePicker10.Value = DateTime.Now;
            richTextBox3.Text = "";
        }

        private void PRİM_KESİNTİ_KONTROL_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            griddoldur();

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
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
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

            comboBox7.Items.Add("PRİM");
            comboBox10.Items.Add("PRİM");
            comboBox7.Items.Add("KESİNTİ");
            comboBox10.Items.Add("KESİNTİ");
            dateTimePicker10.CustomFormat = "MMMM yyyy";
            dateTimePicker10.Format = DateTimePickerFormat.Custom;



        }




    }
}
