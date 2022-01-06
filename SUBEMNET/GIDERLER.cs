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
    public partial class GIDERLER : Form
    {
        public GIDERLER()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox21.Text) == true || string.IsNullOrEmpty(textBox22.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Giderler (Tarih,Tedarikçi,GiderKalemi,Durum,Miktar,tutar,Açıklama,Sube,NO,FaturaTarih) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8, @p9,@p10)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", tedarikçiid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox21.Text + "." + textBox22.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    komut = new SqlCommand("Select TOPLAM,tutar from Tedarikçiler where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set TOPLAM=@p1,tutar=@p2 where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set TOPLAM=@p1,tutar=@p2 where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox21.Text + "." + textBox22.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    richTextBox4.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "00";
                    checkBox1.Checked = false;


                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

            private void button3_Click(object sender, EventArgs e)
            {
                panel1.Visible = false;
                textBox4.Text = "";
            }
            Form1 Form1 = new Form1();
            int okulid = Form1.okulid;
            SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
            SqlDataAdapter da;
            DataSet ds;
            SqlCommandBuilder cmdb;
            SqlCommand komut;
            private void button4_Click(object sender, EventArgs e)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into GiderKalemi (GiderKalemi,Okulid) values (@p1,@p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", okulid);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox4.Text = "";
                    panel1.Visible = false;
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show("HATA." + a.ToString());
                }
                comboBox3.Items.Clear();
                komut = new SqlCommand("Select GiderKalemi from GiderKalemi", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox3.Items.Add(oku[0].ToString());

                }
                baglan.Close();
            }
        List<int> tedarikçiid = new List<int>();
        List<int> subeid = new List<int>();
        private void GIDERLER_Load(object sender, EventArgs e)
            {


            tedarikçiid.Clear();
            subeid.Clear();
            comboBox4.Items.Add("ÖDENDİ");
            comboBox4.Items.Add("ÖDENMEDİ");
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
                comboBox1.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);


            }
            baglan.Close();
            comboBox2.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            comboBox3.Items.Clear();
            komut = new SqlCommand("Select GiderKalemi from GiderKalemi where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());

            }
            baglan.Close();

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
                tedarikçiid.Clear();
                comboBox2.Items.Clear();
                komut = new SqlCommand("Select ID,Tedarikçi from Tedarikçiler where Sube='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
                baglan.Open();
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    comboBox2.Items.Add(oku2[1].ToString());
                    tedarikçiid.Add((int)oku2[0]);

                }
                baglan.Close();

            }
                catch (Exception A)
                {
                    baglan.Close();
                    MessageBox.Show(A.ToString());
                }
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

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            tedarikçiid.Clear();
            comboBox2.Items.Clear();
            komut = new SqlCommand("Select ID,Tedarikçi from Tedarikçiler where Sube='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[1].ToString());
                tedarikçiid.Add((int)oku2[0]);

            }
            baglan.Close();
        }

 

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox21.Text) == true || string.IsNullOrEmpty(textBox22.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Giderler (Tarih,Tedarikçi,GiderKalemi,Durum,Miktar,tutar,Açıklama,Sube) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", tedarikçiid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox21.Text + "." + textBox22.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    komut = new SqlCommand("Select TOPLAM,tutar from Tedarikçiler where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set TOPLAM=@p1,tutar=@p2 where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Tedarikçiler set TOPLAM=@p1,tutar=@p2 where ID='" + tedarikçiid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox21.Text + "." + textBox22.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    richTextBox4.Text = "";
                    textBox21.Text = "";
                    textBox22.Text = "00";
                    checkBox1.Checked = false;


                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked == true)
            {
                panel2.Visible = false;
            }
            else
            {
                panel2.Visible = true;
            }
        }
    }
}
