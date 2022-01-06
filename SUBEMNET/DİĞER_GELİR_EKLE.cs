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
    public partial class DİĞER_GELİR_EKLE : Form
    {
        public DİĞER_GELİR_EKLE()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        private void button10_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            comboBox1.Text = "";
            textBox15.Text = "";
            maskedTextBox3.Text = "";
            textBox10.Text = "";
            textBox13.Text = "";
            textBox7.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
            richTextBox2.Text = "";
            textBox8.Text = "";
            textBox12.Text = "";
            maskedTextBox1.Text = "";
            maskedTextBox2.Text = "";
            panel5.Visible = false;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButton1.Checked == true)
                {
                    bool degisken = false;
                    string filtre = "insert into  Müşteri(";
                    string values = "Values (";

                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " sube ";
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
                        filtre += " AdSoyad ";
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
                        filtre += " Fax ";
                        values += "'" + textBox13.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " İl ";
                        values += "'" + textBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " İlçe ";
                        values += "'" + textBox10.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mahalle ";
                        values += "'" + textBox8.Text + "'";
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
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " KişiTip ";
                    values += "'TÜZEL KİŞİ'";



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
                    maskedTextBox3.Text = "";
                    textBox10.Text = "";
                    textBox13.Text = "";
                    textBox7.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                    richTextBox2.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    panel5.Visible = false;
                    panel1.Visible = false;


                }
                else if (radioButton2.Checked == true)
                {
                    bool degisken = false;
                    string filtre = "insert into Müşteri (";
                    string values = "Values (";

                    if (string.IsNullOrEmpty(comboBox1.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " sube";
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
                        filtre += " AdSoyad ";
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
                        filtre += " Fax ";
                        values += "'" + textBox13.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox12.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " İl ";
                        values += "'" + textBox12.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox10.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " İlçe ";
                        values += "'" + textBox10.Text + "'";
                        degisken = true;
                    }
                    if (string.IsNullOrEmpty(textBox8.Text) == false)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " Mahalle ";
                        values += "'" + textBox8.Text + "'";
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
                    if (maskedTextBox3.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";
                            values += " , ";
                        }
                        filtre += " TcKimlik ";
                        values += "'" + maskedTextBox3.Text + "'";
                        degisken = true;
                    }

                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " KişiTip ";
                    values += "'GERÇEK KİŞİ'";



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
                    maskedTextBox3.Text = "";
                    textBox10.Text = "";
                    textBox13.Text = "";
                    textBox7.Text = "";
                    textBox1.Text = "";
                    textBox3.Text = "";
                    richTextBox2.Text = "";
                    textBox8.Text = "";
                    textBox12.Text = "";
                    maskedTextBox1.Text = "";
                    maskedTextBox2.Text = "";
                    panel5.Visible = false;
                    panel1.Visible = false;

                }
                müşteriid.Clear();
                comboBox2.Items.Clear();
                komut = new SqlCommand("Select m.ID,m.AdSoyad from Müşteri m join Sube ş  on m.sube=ş.ID where ş.Okulid='" + okulid + "'", baglan);
                baglan.Open();
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    comboBox2.Items.Add(oku2[1].ToString());
                    müşteriid.Add((int)oku2[0]);

                }
                baglan.Close();
                panel1.Visible = false;
            }
            catch (Exception A)
            {
                baglan.Close();
                MessageBox.Show(A.ToString());
            }
            
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            textBox4.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into GelirKalemi (GelirKalemii,okulid) values (@p1,@p2)", baglan);
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
            komut = new SqlCommand("Select GelirKalemii from GelirKalemi where okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());
              
            }
            baglan.Close();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel7.Visible = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                label30.Text = "FİRMA ADI";
                panel5.Visible = true;
                panel2.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                label30.Text = "ADI SOYADI";
                panel2.Visible = true;
                panel5.Visible = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //NAKİT EKLEME
            if (string.IsNullOrEmpty(textBox21.Text) == true || string.IsNullOrEmpty(textBox22.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true )
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into DiğerGelirler (TARİH,Müşteri , DiğerGelirKalemi,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1",dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox21.Text + "," + textBox22.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox21.Text + "." + textBox22.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                
                    komut = new SqlCommand("Select TOPLAM,GELECEK from Müşteri where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                    baglan.Open();
                    decimal i=0;
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
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",","."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
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


                }
                catch (Exception a)
                {
                    baglan.Close();
                   
                        MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            //visa ekleme
            if (string.IsNullOrEmpty(textBox17.Text) == true || string.IsNullOrEmpty(textBox18.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox23.Text) == true || string.IsNullOrEmpty(comboBox6.Text) == true || string.IsNullOrEmpty(textBox25.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into DiğerGelirler (TARİH,Müşteri , DiğerGelirKalemi,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,TaksitSayisi,NO,BankaHesabı,taksit) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10, @p11,@p12,@p13)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox17.Text + "," + textBox18.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox17.Text + "." + textBox18.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox23.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", numericUpDown1.Text);
                    komutkaydet.Parameters.AddWithValue("@p11", textBox25.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p13", numericUpDown1.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                    komut = new SqlCommand("Select TOPLAM,GELECEK from Müşteri where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Decimal.Multiply(Convert.ToDecimal(textBox17.Text + "," + textBox18.Text),Convert.ToDecimal(numericUpDown1.Text))).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Decimal.Multiply(Convert.ToDecimal(textBox17.Text + "," + textBox18.Text), Convert.ToDecimal(numericUpDown1.Text))));
                        komutgüncelle.Parameters.AddWithValue("@p2", Decimal.Multiply(Convert.ToDecimal(textBox17.Text + "," + textBox18.Text), Convert.ToDecimal(numericUpDown1.Text)));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    richTextBox5.Text = "";
                    textBox17.Text = "";
                    textBox18.Text = "00";
                    textBox23.Text = "";
                    numericUpDown1.Text = "1";
                    textBox25.Text = "";
                    comboBox6.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ÇEK
            if (string.IsNullOrEmpty(textBox6.Text) == true || string.IsNullOrEmpty(textBox11.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox5.Text) == true || string.IsNullOrEmpty(textBox14.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into DiğerGelirler (TARİH,Müşteri , DiğerGelirKalemi,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,Vade,NO,Banka) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10, @p11,@p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox6.Text + "." + textBox11.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p11", textBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", textBox14.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                    komut = new SqlCommand("Select TOPLAM,GELECEK from Müşteri where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox6.Text + "." + textBox11.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }

                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,BANKA,ÇekNo,miktar,musteriid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p2", "ÇEK");
                    komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                    komutkaydet2.Parameters.AddWithValue("@p11", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@p3", comboBox2.Text );
                    komutkaydet2.Parameters.AddWithValue("@p4", comboBox2.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox6.Text + "," + textBox11.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox6.Text + "." + textBox11.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "MÜŞTERİ");
                    komutkaydet2.Parameters.AddWithValue("@p9", textBox5.Text);
                    komutkaydet2.Parameters.AddWithValue("@p8", textBox14.Text);
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker2.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    richTextBox1.Text = "";
                    textBox6.Text = "";
                    textBox11.Text = "00";
                    textBox25.Text = "";
                    dateTimePicker2.Value = DateTime.Now;
                    textBox2.Text = "";
                    textBox14.Text = "";




                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //BANKA
            if (string.IsNullOrEmpty(textBox24.Text) == true || string.IsNullOrEmpty(textBox26.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox16.Text) == true || string.IsNullOrEmpty(comboBox7.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into DiğerGelirler (TARİH,Müşteri , DiğerGelirKalemi,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,BankaHesabı) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p12)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox24.Text + "," + textBox26.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox24.Text + "." + textBox26.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox16.Text);
                    komutkaydet.Parameters.AddWithValue("@p12", comboBox7.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                    komut = new SqlCommand("Select TOPLAM,GELECEK from Müşteri where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Convert.ToDecimal(textBox24.Text + "," + textBox26.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox24.Text + "," + textBox26.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox24.Text + "." + textBox26.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    richTextBox3.Text = "";
                    textBox24.Text = "";
                    textBox26.Text = "00";
                    textBox25.Text = "";
                    textBox16.Text = "";
                  
                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //SENET
            if (string.IsNullOrEmpty(textBox20.Text) == true || string.IsNullOrEmpty(textBox27.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(textBox19.Text) == true )
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into DiğerGelirler (TARİH,Müşteri , DiğerGelirKalemi,ÖdemeŞekli, MİKTAR,tutar,Açıklama,ŞUBE,Sahip,Vade) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7,@p8,@p9, @p10)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", dateTimePicker1.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p2", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet.Parameters.AddWithValue("@p6", textBox20.Text + "." + textBox27.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", richTextBox6.Text);
                    komutkaydet.Parameters.AddWithValue("@p8", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p9", textBox19.Text);
                    komutkaydet.Parameters.AddWithValue("@p10", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                 
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    komut = new SqlCommand("Select TOPLAM,GELECEK from Müşteri where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
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
                        yenideger = (i + Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)).ToString();
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(yenideger)));
                        komutgüncelle.Parameters.AddWithValue("@p2", yenideger.Replace(",", "."));
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    else
                    {
                        baglan.Open();
                        SqlCommand komutgüncelle = new SqlCommand("update Müşteri set TOPLAM=@p1,GELECEK=@p2 where ID='" + müşteriid[comboBox2.SelectedIndex] + "'", baglan);
                        komutgüncelle.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                        komutgüncelle.Parameters.AddWithValue("@p2", textBox20.Text + "." + textBox27.Text);
                        komutgüncelle.ExecuteNonQuery();
                        baglan.Close();
                    }
                    baglan.Open();
                    SqlCommand komutkaydet2 = new SqlCommand("insert into ÇekSenet (Sube,EvrakTürü,Sahibi,AlınanEvrakAdı,EvrakTipi,Tutar,Vade,miktar,musteriid,DURUM) values (@p1, @p2, @p3, @p4,@p5, @p6, @p7, @p10,@p11,@p12)", baglan);
                    komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@p2", "SENET");
                    komutkaydet2.Parameters.AddWithValue("@p12", "ALACAK");
                    komutkaydet2.Parameters.AddWithValue("@p11", müşteriid[comboBox2.SelectedIndex]);
                    komutkaydet2.Parameters.AddWithValue("@p3", textBox19.Text);
                    komutkaydet2.Parameters.AddWithValue("@p4", comboBox2.Text);
                    komutkaydet2.Parameters.AddWithValue("@p6", string.Format("{0:N}", Convert.ToDecimal(textBox20.Text + "," + textBox27.Text)));
                    komutkaydet2.Parameters.AddWithValue("@p10", textBox20.Text + "." + textBox27.Text);
                    komutkaydet2.Parameters.AddWithValue("@p5", "MÜŞTERİ");
                    komutkaydet2.Parameters.AddWithValue("@p7", dateTimePicker3.Value.Date.ToString("yyyyMMdd"));
                    komutkaydet2.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    panel6.Visible = false;
                    comboBox5.Text = "";
                    dateTimePicker1.Value = DateTime.Now;
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                    comboBox4.Text = "";
                    comboBox5.Text = "";
                    richTextBox1.Text = "";
                    textBox20.Text = "";
                    textBox27.Text = "00";
                    textBox25.Text = "";
                    dateTimePicker3.Value = DateTime.Now;
                    textBox2.Text = "";
                    textBox14.Text = "";
                    textBox19.Text = "";





                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }
       
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox4.Text == "NAKİT")
            {
                panel6.Visible = true;
                panel9.Visible = false;
            }
            if (comboBox4.Text == "VİSA")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = false;

            }
            if (comboBox4.Text == "ÇEK")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible =false;
            }
            if (comboBox4.Text == "BANKA")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = false;

            }
            if (comboBox4.Text == "SENET")
            {
                panel6.Visible = true;
                panel9.Visible = true;
                panel3.Visible = true;
                panel10.Visible = true;
                panel11.Visible = true;
            }
        }
        SqlCommand komut;
        List<int> müşteriid = new List<int>();
        List<int> subeid = new List<int>();
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;
        private void DİĞER_GELİR_EKLE_Load(object sender, EventArgs e)
        {
            
            
            müşteriid.Clear();
            panel6.Visible = false;
            subeid.Clear();
            comboBox4.Items.Add("NAKİT");
            comboBox4.Items.Add("VİSA");
            comboBox4.Items.Add("ÇEK");
            comboBox4.Items.Add("BANKA");
            comboBox4.Items.Add("SENET");
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='"+okulid+"'", baglan);
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
            comboBox6.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            comboBox7.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            comboBox3.Items.Clear();
            komut = new SqlCommand("Select GelirKalemii from GelirKalemi where okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());

            }
            baglan.Close();



        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            müşteriid.Clear();
            comboBox6.Items.Clear();
            komut = new SqlCommand("Select BankaHesabı from BankaHesabı where Sube='"+subeid[comboBox5.SelectedIndex]+"'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox6.Items.Add(oku[0].ToString());
                comboBox7.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            comboBox2.Items.Clear();
            komut = new SqlCommand("Select ID,AdSoyad from Müşteri where sube='"+subeid[comboBox5.SelectedIndex]+"'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[1].ToString());
                müşteriid.Add((int)oku2[0]);

            }
            baglan.Close();
           

        }
    }
}
