using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class TakımÇalışması : Form
    {
        public TakımÇalışması()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //TÜMÜNÜ GÖSTER
            griddoldur();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ARA FİLTRELE
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLE
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

     
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ID,CalismaAdi 'ÇALIŞMA ADI',BaslamaTarihi 'BAŞLANGIÇ TARİHİ',BitisTarihi 'BİTİŞ TARİHİ',GorevTuru 'GÖREV TÜRÜ',Açıklama 'AÇIKLAMA'  from TakimCalismasi", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "TakimCalismasi");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "GÜNCELLE";
            dgvBtn.Text = "GÜNCELLE";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);

            DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
            dgvbtn2.HeaderText = "SİL";
            dgvbtn2.Text = "SİL";
            dgvbtn2.UseColumnTextForButtonValue = true;
            dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvbtn2.Width = 70;
            dataGridView1.Columns.Add(dgvbtn2);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle2();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Arama temizleme butonu
            temizle();

        }
        void temizle()
        {
            textBox1.Text = "";
            textBox6.Text = "";
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            comboBox1.Text = "";
            richTextBox1.Text = "";
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker6.Value = DateTime.Now;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }
        int okulid = Form1.okulid;
        private void TakımÇalışması_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker5.Format = DateTimePickerFormat.Custom;
            dateTimePicker5.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker6.Format = DateTimePickerFormat.Custom;
            dateTimePicker6.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            dateTimePicker8.Format = DateTimePickerFormat.Custom;
            dateTimePicker8.CustomFormat = "dd/MM/yyyy HH:mm:ss";
            
            griddoldur();
            comboBox1.Items.Add("");
            comboBox1.Items.Add("KONU ANALİZİ");
            comboBox1.Items.Add("ONLİNE SINAV SORU SEÇME");
            comboBox1.Items.Add("KİTAPÇIK SORU SEÇME");
            comboBox1.Items.Add("SORU DÜZENLE");
            comboBox1.Items.Add("METİN DÜZENLEME");
            comboBox1.Items.Add("KONU ANALİZİ VE CEVAP GİRİŞİ");
            comboBox1.Items.Add("DİĞER");
            comboBox3.Items.Add("KONU ANALİZİ");
            comboBox3.Items.Add("ONLİNE SINAV SORU SEÇME");
            comboBox3.Items.Add("KİTAPÇIK SORU SEÇME");
            comboBox3.Items.Add("SORU DÜZENLE");
            comboBox3.Items.Add("METİN DÜZENLEME");
            comboBox3.Items.Add("KONU ANALİZİ VE CEVAP GİRİŞİ");
            comboBox3.Items.Add("DİĞER");
            comboBox3.Items.Add("KONU ANALİZİ");
            comboBox2.Items.Add("ONLİNE SINAV SORU SEÇME");
            comboBox2.Items.Add("KİTAPÇIK SORU SEÇME");
            comboBox2.Items.Add("SORU DÜZENLE");
            comboBox2.Items.Add("METİN DÜZENLEME");
            comboBox2.Items.Add("KONU ANALİZİ VE CEVAP GİRİŞİ");
            comboBox2.Items.Add("DİĞER");

        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select ID,CalismaAdi 'ÇALIŞMA ADI',BaslamaTarihi 'BAŞLANGIÇ TARİHİ',BitisTarihi 'BİTİŞ TARİHİ',GorevTuru 'GÖREV TÜRÜ',Açıklama 'AÇIKLAMA' from TakimCalismasi where okulid='" + okulid+"'";
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    filtre += " AND ";

                    filtre += " ID = " + textBox1.Text;
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " CalismaAdi = '" + textBox6.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " GorevTuru = '" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BaslamaTarihi between '" + dateTimePicker3.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker4.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }

                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BitisTarihi between '" + dateTimePicker6.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker5.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(richTextBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Açıklama = " + richTextBox1.Text;
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox6.Text) && string.IsNullOrEmpty(comboBox1.Text) && string.IsNullOrEmpty(textBox1.Text) && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select ID,CalismaAdi 'ÇALIŞMA ADI',BaslamaTarihi 'BAŞLANGIÇ TARİHİ',BitisTarihi 'BİTİŞ TARİHİ',GorevTuru 'GÖREV TÜRÜ',Açıklama 'AÇIKLAMA' from TakimCalismasi where okulid='"+okulid+"";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "TakimCalismasi");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "GÜNCELLE";
                dgvBtn.Text = "GÜNCELLE";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);

                DataGridViewButtonColumn dgvbtn2 = new DataGridViewButtonColumn();
                dgvbtn2.HeaderText = "SİL";
                dgvbtn2.Text = "SİL";
                dgvbtn2.UseColumnTextForButtonValue = true;
                dgvbtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvbtn2.Width = 70;
                dataGridView1.Columns.Add(dgvbtn2);
                panel2.Visible = false;
                temizle();
            }
            catch (Exception a)
            {
                baglan.Close();
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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }
        void temizle2()
        {
            textBox2.Text = "";
            comboBox2.Text = "";
            richTextBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker7.Value = DateTime.Now;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //ekleme paneli temizle butou
            temizle2();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {


                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into TakimCalismasi (CalismaAdi, BaslamaTarihi, BitisTarihi,GorevTuru, Açıklama,okulid) values (@p1, @p2, @p3, @p4,@p5,@p6)", baglan);
                komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text.ToUpper());
                komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd HH:mm:ss"));
                komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                komutkaydet.Parameters.AddWithValue("@p4", comboBox2.Text);
                komutkaydet.Parameters.AddWithValue("@p5", richTextBox2.Text);
                komutkaydet.Parameters.AddWithValue("@p6", okulid);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                temizle2();
                griddoldur();
                panel2.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                if (string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
                {
                    MessageBox.Show("LÜTFEN TÜM BİLGİLERİ GİRİNİZ!!");
                }
                else
                    MessageBox.Show("HATA." + a.ToString());
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle3();
        }
        void temizle3()
        {
            textBox5.Text = "";
            textBox3.Text = "";
            comboBox3.Text = "";
            richTextBox3.Text = "";
            dateTimePicker8.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                panel2.Visible = true;
                panel1.Visible = true;
                panel5.Visible = true;
                yerlestir();
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "TAKIM ÇALIŞMASI SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString()  + " İSİMLİ TAKIM ÇALIŞMASINI TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "TAKIM ÇALIŞMASI SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE TakimCalismasi FROM  WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", id);
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            griddoldur();
                        }
                        catch (Exception )
                        {
                            baglan.Close();
                            MessageBox.Show("HATA");
                        }

                    }
                }
            }

        }
        void yerlestir()
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;


            textBox5.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[2].Value);
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[3].Value);
            richTextBox3.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            yerlestir();
        }
        SqlCommand komut;

      

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {

                baglan.Open();
                SqlCommand degistir = new SqlCommand("update TakimCalismasi set CalismaAdi=@a1, BaslamaTarihi=@a3, BitisTarihi=@a4, Açıklama=@a6 , GorevTuru=@a5 where ID=@a2 ", baglan);
                degistir.Parameters.AddWithValue("@a2", textBox5.Text);
                degistir.Parameters.AddWithValue("@a1", textBox3.Text.ToUpper());
                degistir.Parameters.AddWithValue("@a3", dateTimePicker8.Value.ToString("yyyyMMdd HH:mm:ss"));
                degistir.Parameters.AddWithValue("@a4", dateTimePicker2.Value.ToString("yyyyMMdd HH:mm:ss"));
                degistir.Parameters.AddWithValue("@a5", comboBox3.Text);
                degistir.Parameters.AddWithValue("@a6", richTextBox3.Text);

                degistir.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Güncellendi.");

                panel2.Visible = false;
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                if (string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(richTextBox3.Text) == true)
                {
                    MessageBox.Show("LÜTFEN TÜM BİLGİLERİ GİRİNİZ!!");
                }
                else
                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void textBox3_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
