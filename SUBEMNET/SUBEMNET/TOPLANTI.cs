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
    public partial class TOPLANTI : Form
    {
        public TOPLANTI()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //TÜMÜNÜ GÖSTER BUTON
            griddoldur();

        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        void griddoldur()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select * from Toplanti", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Toplanti");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            //ARA/FİLTRELE BUTON  
            panel2.Visible = true;
            panel1.Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLE BUTON
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //gÜNCELLEME VE SİLME BUTON
            MessageBox.Show("LÜTFEN DEĞİŞTİRMEK YA DA SİLMEK İSTEDİĞİNİZ TAKIM ÇALIŞMASININ ÜSTÜNE ÇİFT TIKLAYINIZ.");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = true;
            yerlestir();
        }
        void yerlestir()
        {
            int secilen;
            secilen = dataGridView1.SelectedCells[0].RowIndex;


            textBox5.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            comboBox3.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            dateTimePicker8.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[4].Value);
            dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[secilen].Cells[5].Value);
            comboBox6.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ARAMA KAPAT BUTONU
            panel2.Visible = false;
            temizle();
        }
        void temizle()
        {
            textBox1.Text = "";
            textBox6.Text = "";
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            comboBox1.Text = "";
            comboBox4.Text = "";
            dateTimePicker5.Value = DateTime.Now;
            dateTimePicker6.Value = DateTime.Now;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //ARAMA TEMİZLEME BUTONU
            temizle();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = false;
                string filtre = "Select * from Toplanti where ";
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    filtre += " ID = " + textBox1.Text;
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ToplantıAdı = '" + textBox6.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ToplantıTürü = '" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BaslangicTarih between '" + dateTimePicker3.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker4.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }

                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " BitisTarig between '" + dateTimePicker6.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dateTimePicker5.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Sube = " + comboBox4.Text;
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox6.Text) && string.IsNullOrEmpty(comboBox1.Text) && string.IsNullOrEmpty(comboBox4.Text) && string.IsNullOrEmpty(textBox1.Text) && checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    filtre = "Select * from Toplanti";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Toplanti");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                temizle();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void TOPLANTI_Load(object sender, EventArgs e)
        {
            comboBox6.Items.Add("ÖĞRETMENLER");
            comboBox6.Items.Add("YÖNETİM KURULU");
            comboBox6.Items.Add("REHBERLİK");
            comboBox6.Items.Add("MUHASEBE");
            comboBox6.Items.Add("MÜDÜRLER");
            comboBox6.Items.Add("ZÜMRE");
            comboBox6.Items.Add("İDARİ");
            comboBox6.Items.Add("DİĞER");
            comboBox2.Items.Add("ÖĞRETMENLER");
            comboBox2.Items.Add("YÖNETİM KURULU");
            comboBox2.Items.Add("REHBERLİK");
            comboBox2.Items.Add("MUHASEBE");
            comboBox2.Items.Add("MÜDÜRLER");
            comboBox2.Items.Add("ZÜMRE");
            comboBox2.Items.Add("İDARİ");
            comboBox2.Items.Add("DİĞER");
            comboBox1.Items.Add("");
            comboBox1.Items.Add("ÖĞRETMENLER");
            comboBox1.Items.Add("YÖNETİM KURULU");
            comboBox1.Items.Add("REHBERLİK");
            comboBox1.Items.Add("MUHASEBE");
            comboBox1.Items.Add("MÜDÜRLER");
            comboBox1.Items.Add("ZÜMRE");
            comboBox1.Items.Add("İDARİ");
            comboBox1.Items.Add("DİĞER");
            griddoldur();
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
            


        }
        void temizle2()
        {
            textBox2.Text = "";
            comboBox2.Text = "";
            comboBox5.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker7.Value = DateTime.Now;
        }
        private void button10_Click(object sender, EventArgs e)
        {
            //EKLEME TEMİZLE BUTONU
            temizle2();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //EKLEME KAPAT BUTOUN
            panel2.Visible = false;
            temizle2();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ARAMA ID
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void comboBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //EKLMEE
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Toplanti (ToplantıAdı, BaslangicTarih, BitisTarig,ToplantıTürü, Sube) values (@p1, @p2, @p3, @p4,@p5)", baglan);
                komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text.ToUpper());
                komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd HH:mm:ss"));
                komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker1.Value.ToString("yyyyMMdd HH:mm:ss"));
                komutkaydet.Parameters.AddWithValue("@p4", comboBox2.Text);
                komutkaydet.Parameters.AddWithValue("@p5", comboBox5.Text);
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

        private void button13_Click(object sender, EventArgs e)
        {
            //değişiklik geri al 
            yerlestir();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //güncelleme kapat butonu
            panel2.Visible = false;
        }
        SqlCommand komut;

        void KayitSil(int id)
        {
            string sql = "DELETE FROM Toplanti WHERE ID=@id";
            komut = new SqlCommand(sql, baglan);
            komut.Parameters.AddWithValue("@id", id);
            baglan.Open();
            komut.ExecuteNonQuery();
            baglan.Close();
        }
        private void button15_Click(object sender, EventArgs e)
        {
            //silme işlemi
            try
            {
                int secilen;
                secilen = dataGridView1.SelectedCells[0].RowIndex;
                int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                KayitSil(id);
                panel2.Visible = false;
                griddoldur();
                MessageBox.Show("KAYIT SİLİNDİ");
                panel2.Visible = false;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("KAYIT SİLİNEMEDİ. LÜTFEN TEKRAR DENEYİNİZ.");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //güncelleme işlemi
            try
            {

                baglan.Open();
                SqlCommand degistir = new SqlCommand("update Toplanti set ToplantıAdı=@a1, BaslangicTarih=@a3, BitisTarig=@a4, Sube=@a6 , ToplantıTürü=@a5 where ID=@a2 ", baglan);
                degistir.Parameters.AddWithValue("@a2", textBox5.Text);
                degistir.Parameters.AddWithValue("@a1", textBox3.Text.ToUpper());
                degistir.Parameters.AddWithValue("@a3", dateTimePicker8.Value.ToString("yyyyMMdd HH:mm:ss"));
                degistir.Parameters.AddWithValue("@a4", dateTimePicker2.Value.ToString("yyyyMMdd HH:mm:ss"));
                degistir.Parameters.AddWithValue("@a5", comboBox6.Text);
                degistir.Parameters.AddWithValue("@a6", comboBox3.Text);

                degistir.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Güncellendi.");

                panel2.Visible = false;
                griddoldur();
            }
            catch (Exception a)
            {
                baglan.Close();
                if (string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true )
                {
                    MessageBox.Show("LÜTFEN TÜM BİLGİLERİ GİRİNİZ!!");
                }
                else
                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
    }
}
