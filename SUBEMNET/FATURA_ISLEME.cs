using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class FATURA_ISLEME : Form
    {
        public FATURA_ISLEME()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //RAPORLAMA İŞLEMLERİ
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GÜNCELLEME YÖNLENDİRME
            MessageBox.Show("LÜTFEN GÜNCELLEMEK VEYA SİLMEK İSTEDİĞİNİZ FATURANIN ÜZERİNE ÇİFT TIKLAYINIZ!!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLEME YÖNLENDİRME
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ARAMA FİLTRELEME YÖNLENDİRME

            panel2.Visible = true;
            panel1.Visible = false;
            panel5.Visible = false;


        }

        private void FATURA_ISLEME_Load(object sender, EventArgs e)
        {
            panel2.Visible = false;
            griddoldur();
            Sil();
            comboBox3.Items.Add("");
            comboBox3.Items.Add("ÖDENDİ");
            comboBox3.Items.Add("ÖDENMEDİ");
            comboBox2.Items.Add("ÖDENDİ");
            comboBox2.Items.Add("ÖDENMEDİ");
            comboBox5.Items.Add("ÖDENDİ");
            comboBox5.Items.Add("ÖDENMEDİ");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Arama pop-up kapat
            panel2.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ARAMA seçim temizle
            temizle();

        }
        void temizle()
        {
            textBox1.Clear();
            textBox6.Text = "";
            comboBox3.Text = "";
            textBox3.Text = "";
            textBox7.Text = "";
            dateTimePicker6.Value = DateTime.Now;
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            checkBox1.Checked = false;
            checkBox2.Checked = false;

        }
        TimeSpan fark;
        double gunfark;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        void griddoldur()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select * from Faturalar", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Faturalar");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();



            renklendir();
        }
        SqlCommand komut;
        void Sil()
        {
            int id;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                fark = Convert.ToDateTime(DateTime.Now.ToShortDateString()) - Convert.ToDateTime(dataGridView1.Rows[i].Cells["SonOdemeTarihi"].Value.ToString());
                gunfark = fark.TotalDays;
                string odeme = dataGridView1.Rows[i].Cells["OdemeDurumu"].Value.ToString();
                if (gunfark >= 365 && odeme == "ÖDENDİ")
                {
                    id = Convert.ToInt32(dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    string sql = "DELETE FROM Faturalar WHERE ID=@id";
                    komut = new SqlCommand(sql, baglan);
                    komut.Parameters.AddWithValue("@id", id);
                    baglan.Open();
                    komut.ExecuteNonQuery();
                    baglan.Close();
                }
            }
            griddoldur();
        }
        void renklendir()
        {

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {

                fark = Convert.ToDateTime(dataGridView1.Rows[i].Cells["SonOdemeTarihi"].Value.ToString()) - Convert.ToDateTime(DateTime.Now.ToShortDateString());
                gunfark = fark.TotalDays;
                string odeme = dataGridView1.Rows[i].Cells["OdemeDurumu"].Value.ToString();
                if (odeme == "ÖDENDİ")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (gunfark <= 3 && odeme == "ÖDENMEDİ")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.DarkRed;
                }
                else if (gunfark > 3 && gunfark < 7 && odeme == "ÖDENMEDİ")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

                else

                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            //arama filtreleme işlemleri


            try
            {

                bool degisken = false;
                string filtre = "Select * from Faturalar where ";
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    filtre += " ID = " + textBox1.Text;
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox3.Text) == false && string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    if (Convert.ToInt32(textBox3.Text) < Convert.ToInt32(textBox7.Text))
                    {
                        filtre += " Tutar between '" + textBox3.Text + "' and '" + textBox7.Text + "'";
                    }
                    if (Convert.ToInt32(textBox3.Text) > Convert.ToInt32(textBox7.Text))
                    {
                        filtre += " Tutar between '" + textBox7.Text + "' and '" + textBox3.Text + "'";
                    }
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " FaturaAdi = '" + textBox6.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " OdemeDurumu = '" + comboBox3.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " OdemeTarihi between '" + dateTimePicker3.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker4.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }

                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " SonOdemeTarihi between '" + dateTimePicker6.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker5.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(textBox6.Text) && string.IsNullOrEmpty(comboBox3.Text) && string.IsNullOrEmpty(textBox1.Text) && string.IsNullOrEmpty(textBox3.Text) && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select * from Faturalar";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Faturalar");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                renklendir();
                panel2.Visible = false;
                temizle();


            }

            catch (Exception a)
            {
                baglan.Close();
                if(String.IsNullOrEmpty(textBox3.Text)==true && string.IsNullOrEmpty(textBox7.Text)==false)
                {
                    MessageBox.Show("Tutar kısmında bir aralık belirtmek zorunludur. Lütfen iki kutucuğu da doldurunuz.");
                }
                if (String.IsNullOrEmpty(textBox3.Text) == false && string.IsNullOrEmpty(textBox7.Text)==true)
                {
                    MessageBox.Show("Tutar kısmında bir aralık belirtmek zorunludur. Lütfen iki kutucuğu da doldurunuz.");
                }

                else
                    MessageBox.Show("HATA.. Lütfen girdiğiniz bilgileri kontrol ediniz.");

                
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ID sadece sayı alı
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            //tutar kısmı sayı alımı
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);




        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            temizle2();
        }
        void temizle2()
        {
            textBox8.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker7.Value = DateTime.Now;
            comboBox2.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox2.Text == "ÖDENDİ")
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faturalar (FaturaAdi, Tutar, SonOdemeTarihi,OdemeTarihi, OdemeDurumu) values (@p1, @p2, @p3, @p4,@p5)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox8.Text.ToUpper());
                    komutkaydet.Parameters.AddWithValue("@p2", Convert.ToDecimal(textBox2.Text));
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker1.Value.Date);
                    komutkaydet.Parameters.AddWithValue("@p5", comboBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker7.Value.Date);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                }
                else
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Faturalar (FaturaAdi, Tutar, SonOdemeTarihi, OdemeDurumu) values (@p1, @p2, @p3, @p4)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox8.Text.ToUpper());
                    komutkaydet.Parameters.AddWithValue("@p2", Convert.ToDecimal(textBox2.Text));
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker1.Value.Date);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox2.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                }


                Sil();
                temizle2();
                panel2.Visible = false;
            }
            catch (Exception a)
            {
                if (string.IsNullOrEmpty(textBox8.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true)
                {
                    MessageBox.Show("Lütfen tüm bilgileri giriniz!!");
                }
                else
                    MessageBox.Show("HATA.Lütfen girdiğiniz bilgileri kontrol ediniz.");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "ÖDENDİ")
            {
                dateTimePicker7.Enabled = true;
            }
            else
            {
                dateTimePicker7.Enabled = false;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ekleme tutar kontrol
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)&& e.KeyChar !=',' && e.KeyChar != '.';

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (!char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Bu kısımda sadece alfabetik harfler kullanılabilir.");
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (!char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Bu kısımda sadece alfabetik harfler kullanılabilir.");
            }
        }



        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
            if (!char.IsLetter(e.KeyChar))
            {
                MessageBox.Show("Bu kısımda sadece alfabetik harfler kullanılabilir.");
            }
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.';
           
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text == "ÖDENDİ")
            {
                dateTimePicker8.Enabled = true;

            }
            else
            {
                dateTimePicker8.Enabled = false;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;


            textBox5.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox9.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[5].Value);
            comboBox5.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();


        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;


            textBox5.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox9.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[5].Value);
            comboBox5.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            if (comboBox5.Text == "ÖDENDİ")
            {
                dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[4].Value.ToString());
            }
            else
            {
                dateTimePicker8.Enabled = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        void KayitSil(int id)
        {
            string sql = "DELETE FROM Faturalar WHERE ID=@id";
            komut = new SqlCommand(sql, baglan);
            komut.Parameters.AddWithValue("@id", id);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int secilen;
                secilen = dataGridView1.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                KayitSil(id);
                panel2.Visible = false;
                griddoldur();
            }
            catch (Exception a)
            {
                MessageBox.Show("HATA...");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {

            try
            {
                if (comboBox5.Text == "ÖDENMEDİ")
                {
                    baglan.Open();
                    SqlCommand degistir = new SqlCommand("update Faturalar set FaturaAdi=@a1, SonOdemeTarihi=@a4, Tutar=@a3, OdemeDurumu=@a5 where ID=@a2 ", baglan);
                    degistir.Parameters.AddWithValue("@a2", textBox5.Text);
                    degistir.Parameters.AddWithValue("@a1", textBox9.Text.ToUpper());
                    degistir.Parameters.AddWithValue("@a3", Convert.ToDecimal(textBox4.Text));
                    degistir.Parameters.AddWithValue("@a4", dateTimePicker2.Value.Date);
                    degistir.Parameters.AddWithValue("@a5", comboBox5.Text);
                    degistir.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi.");
                }
                else
                {
                    baglan.Open();
                    SqlCommand degistir = new SqlCommand("update Faturalar set FaturaAdi=@a1, SonOdemeTarihi=@a4, Tutar=@a3, OdemeDurumu=@a5,OdemeTarihi=@a6 where ID=@a2 ", baglan);
                    degistir.Parameters.AddWithValue("@a2", textBox5.Text);
                    degistir.Parameters.AddWithValue("@a1", textBox9.Text.ToUpper());
                    degistir.Parameters.AddWithValue("@a3", Convert.ToDecimal(textBox4.Text));
                    degistir.Parameters.AddWithValue("@a4", dateTimePicker2.Value.Date);
                    degistir.Parameters.AddWithValue("@a5", comboBox5.Text);
                    degistir.Parameters.AddWithValue("@a6", dateTimePicker8.Value.Date);
                    degistir.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi.");
                }
                panel2.Visible = false;
                Sil();
            }
            catch (Exception a)
            {
                if (string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox9.Text) == true || string.IsNullOrEmpty(textBox4.Text) == true)
                {
                    MessageBox.Show("Lütfen tüm bilgileri giriniz!!");
                }
                else
                    MessageBox.Show("HATA..");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
