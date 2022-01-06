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
    public partial class AJANDA_GİRİŞ : Form
    {
        public AJANDA_GİRİŞ()
        {
            InitializeComponent();
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
            panel9.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel9.Visible = true;
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        List<int> subeid = new List<int>();
        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(textBox9.Text) == true)
            {
                MessageBox.Show("ŞUBE VE BAŞLIK BİLGİLERİNİN GİRİLMESİ ZORUNLUDUR.");

            }
            else
            {
                if (checkBox3.Checked == false && checkBox4.Checked == false && string.IsNullOrEmpty(comboBox12.Text) == false && string.IsNullOrEmpty(comboBox13.Text) == false && string.IsNullOrEmpty(comboBox8.Text) == false && string.IsNullOrEmpty(comboBox9.Text) == false)
                {
                    MessageBox.Show("KATILIM GÖSTERECEK KİŞİLERİ GİRMEK ZORUNLUDUR.");
                }
                else
                {
                    try
                    {
                        string baglanid="";
                        baglan.Open();
                        SqlCommand komutkaydet = new SqlCommand("insert into AkademikTakvim (Açıklama, Başlangıç, Bitiş,Başlık, Sube,Tür) values (@p1, @p2, @p3, @p4,@p5,@p6)", baglan);
                        komutkaydet.Parameters.AddWithValue("@p1", textBox14.Text);
                        komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker6.Value.ToString("yyyyMMdd HH:mm:ss"));
                        komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker5.Value.ToString("yyyyMMdd HH:mm:ss"));
                        komutkaydet.Parameters.AddWithValue("@p4", textBox9.Text);
                        komutkaydet.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                        komutkaydet.Parameters.AddWithValue("@p6", textBox1.Text);

                        komutkaydet.ExecuteNonQuery();
                        baglan.Close();
                        baglan.Open();
                        SqlCommand komut = new SqlCommand("SELECT IDENT_CURRENT('AkademikTakvim')", baglan);
                        SqlDataReader oku = komut.ExecuteReader();
                        while (oku.Read())
                        {
                            baglanid = oku.GetValue(0).ToString();
                        }
                        baglan.Close();
                        if (checkBox3.Checked == true)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "ÖĞRENCİLER");
                            kaydet2.Parameters.AddWithValue("@p3", "TÜM ÖĞRENCİLER");
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (checkBox4.Checked == true)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "PERSONEL");
                            kaydet2.Parameters.AddWithValue("@p3", "TÜM PERSONEL");
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (string.IsNullOrEmpty(comboBox12.Text)==false)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "DEVRE");
                            kaydet2.Parameters.AddWithValue("@p3", comboBox12.Text);
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (string.IsNullOrEmpty(comboBox13.Text) == false)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "SINIF");
                            kaydet2.Parameters.AddWithValue("@p3", comboBox13.Text);
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (string.IsNullOrEmpty(comboBox8.Text) == false)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "POZİSYON");
                            kaydet2.Parameters.AddWithValue("@p3", comboBox8.Text);
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (string.IsNullOrEmpty(comboBox9.Text) == false)
                        {
                            baglan.Open();
                            SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                            kaydet2.Parameters.AddWithValue("@p2", "BRANŞ");
                            kaydet2.Parameters.AddWithValue("@p3", comboBox9.Text);
                            kaydet2.Parameters.AddWithValue("@p4", baglanid);
                            kaydet2.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                            kaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }

                        MessageBox.Show("Kayıt Başarılı");
                        griddoldur();
                        panel2.Visible = false;
                        comboBox5.Text = "";
                        comboBox12.Text = "";
                        comboBox13.Text = "";
                        comboBox8.Text = "";
                        comboBox9.Text = "";
                        dateTimePicker6.Value = DateTime.Now;
                        dateTimePicker5.Value = DateTime.Now;
                        textBox14.Text = "";
                        textBox9.Text = "";
                    }
                    catch (Exception a)
                    {
                        baglan.Close();

                        MessageBox.Show("HATA." + a.ToString());
                    }
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            comboBox5.Text = "";
            comboBox12.Text = "";
            comboBox13.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            dateTimePicker6.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            textBox14.Text = "";
            textBox9.Text = "";
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                comboBox12.Text = "";
                comboBox13.Text = "";
                comboBox13.Enabled = false;
                comboBox12.Enabled = false;

            }
            if (checkBox3.Checked == true)
            {
              
                comboBox13.Enabled = true;
                comboBox12.Enabled = true;

            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                comboBox8.Text = "";
                comboBox9.Text = "";
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;

            }
            if (checkBox4.Checked == true)
            {

                comboBox8.Enabled = true;
                comboBox9.Enabled = true;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }
        int okulid = Form1.okulid;
        void griddoldur()
        {
            try
            {
                dataGridView1.Columns.Clear();

                bool degisken = false;
                string filtre = "Select v.ID,ş.ŞubeAdi ŞUBE, v.Tür 'OLAY TÜRÜ',v.başlık BAŞLIK,v.başlangıç BAŞLANGIÇ, v.bitiş as BİTİŞ,v.Açıklama As AÇIKLAMA   from AkademikTakvim v join Sube ş on ş.ID=v.Sube where ş.Okulid='" + okulid + "' and";


                if (string.IsNullOrEmpty(textBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " v.Tür = '" + textBox2.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox21.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " v.Sube = '" + subeid[comboBox21.SelectedIndex] + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox22.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " v.Açıklama  ='" + textBox22.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox24.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " v.Başlık  ='" + textBox24.Text + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "v.Başlangıç between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "v.Bitiş between '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
                dgvBtn3.HeaderText = "";
                dgvBtn3.Text = ">";
                dgvBtn3.UseColumnTextForButtonValue = true;
                dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn3.Width = 70;
                dataGridView1.Columns.Add(dgvBtn3);
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "AJANDA");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SİL";
                dgvBtn.Text = "X";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);

                panel2.Visible = false;
            }
            catch (Exception)
            {
                baglan.Close();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            griddoldur();
            }

        private void button21_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
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
                for (int i = 1; i < dataGridView1.ColumnCount - 1; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText,fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 1; j < dataGridView1.ColumnCount-1; j++)
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
                    title = new Paragraph(textBox20.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox21.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Add(title);
                    title = new Paragraph(textBox19.Text, titleFont);
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
        int id;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                id = dataGridView1.CurrentCell.RowIndex;
                label14.Text = dataGridView1.Rows[id].Cells[4].Value.ToString() + " " + dataGridView1.Rows[id].Cells[5].Value.ToString() + "-" + dataGridView1.Rows[id].Cells[6].Value.ToString();
                panel2.Visible = true;
                panel1.Visible = true;
                panel9.Visible = true;
                panel5.Visible = true;
                dataGridView2.Columns.Clear();
                string filtre = "Select v.ID,ş.ŞubeAdi ŞUBE,v.HedefGrubu 'HEDEF GRUBU',v.Hedef HEDEF   from Gruplar v join Sube ş on ş.ID=v.Sube where ş.Okulid='" + okulid + "' and v.başlık="+id;
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "GRUPLAR");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SİL";
                dgvBtn.Text = "X";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView2.Columns.Add(dgvBtn);

            }
            if (dataGridView1.CurrentCell.ColumnIndex == 8)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "OLAY SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[4].Value.ToString() + " İSİMLİ OLAYI TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "OLAY SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            string sql = "DELETE AkademikTakvim FROM  WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString()));
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            sql = "DELETE Gruplar FROM  WHERE başlık=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString()));
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

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        SqlCommand komut;
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "GRUP SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView2.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView2.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView2.Rows[secilen].Cells[2].Value.ToString() + " İSİMLİ GRUBU TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "GRUP SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            string sql = "DELETE Gruplar FROM  WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", Convert.ToInt32(dataGridView2.Rows[secilen].Cells[0].Value.ToString()));
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

        private void button10_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox7.Text = "";
            comboBox6.Text = "";
            checkBox6.Checked = false;
            checkBox5.Checked = false;

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked == true)
            {
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;

            }
            if (checkBox6.Checked == true)
            {

                comboBox3.Enabled = true;
                comboBox4.Enabled = true;

            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked == true)
            {
                comboBox7.Text = "";
                comboBox6.Text = "";
                comboBox7.Enabled = false;
                comboBox6.Enabled = false;

            }
            if (checkBox5.Checked == true)
            {

                comboBox7.Enabled = true;
                comboBox6.Enabled = true;

            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int sube = 0;
                komut = new SqlCommand("Select Sube from AkademikTakvim where ID="+id, baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    sube = (int)oku[0];


                }
                baglan.Close();
                if (checkBox6.Checked == true)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "ÖĞRENCİLER");
                    kaydet2.Parameters.AddWithValue("@p3", "TÜM ÖĞRENCİLER");
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }
                if (checkBox5.Checked == true)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "PERSONEL");
                    kaydet2.Parameters.AddWithValue("@p3", "TÜM PERSONEL");
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "DEVRE");
                    kaydet2.Parameters.AddWithValue("@p3", comboBox3.Text);
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }
                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "SINIF");
                    kaydet2.Parameters.AddWithValue("@p3", comboBox4.Text);
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }
                if (string.IsNullOrEmpty(comboBox7.Text) == false)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "POZİSYON");
                    kaydet2.Parameters.AddWithValue("@p3", comboBox7.Text);
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }
                if (string.IsNullOrEmpty(comboBox6.Text) == false)
                {
                    baglan.Open();
                    SqlCommand kaydet2 = new SqlCommand("insert into Gruplar (HedefGrubu, Hedef,başlık, Sube) values ( @p2, @p3, @p4,@p5)", baglan);
                    kaydet2.Parameters.AddWithValue("@p2", "BRANŞ");
                    kaydet2.Parameters.AddWithValue("@p3", comboBox6.Text);
                    kaydet2.Parameters.AddWithValue("@p4", id);
                    kaydet2.Parameters.AddWithValue("@p5", sube);
                    kaydet2.ExecuteNonQuery();
                    baglan.Close();
                }

                MessageBox.Show("Kayıt Başarılı");
                dataGridView2.Columns.Clear();
                string filtre = "Select v.ID,ş.ŞubeAdi ŞUBE,v.HedefGrubu 'HEDEF GRUBU',v.Hedef HEDEF   from Gruplar v join Sube ş on ş.ID=v.Sube where ş.Okulid='" + okulid + "' and v.başlık=" + id;
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "GRUPLAR");
                dataGridView2.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SİL";
                dgvBtn.Text = "X";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView2.Columns.Add(dgvBtn);
                panel6.Visible = false;
                comboBox3.Text = "";
                comboBox4.Text = "";
                comboBox7.Text = "";
                comboBox6.Text = "";
                checkBox6.Checked = false;
                checkBox5.Checked = false;

            }
            catch
            {

            }
        }

        private void AJANDA_GİRİŞ_Load(object sender, EventArgs e)
        {
          

            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());
                comboBox12.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox4.Items.Add(oku2[0].ToString());
                comboBox13.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select PozisyonAdi From Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox7.Items.Add(oku3[0].ToString());
                comboBox8.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select Branş From Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox6.Items.Add(oku4[0].ToString());
                comboBox9.Items.Add(oku4[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku5 = komut.ExecuteReader();
            while (oku5.Read())
            {

                comboBox21.Items.Add(oku5[0].ToString());
                comboBox5.Items.Add(oku5[0].ToString());
            }
            baglan.Close();
        }
    }
}
