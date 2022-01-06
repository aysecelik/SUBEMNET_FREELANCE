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
    public partial class MESAİ_DÜZENLE : Form
    {
        public MESAİ_DÜZENLE()
        {
            InitializeComponent();
        }
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void MESAİ_DÜZENLE_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            panel2.Visible = false;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            griddoldur();


            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "MESAİ";
            dgvBtn.Text = "DÜZENLE";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);


            comboBox1.Items.Add("");
            comboBox2.Items.Add("");


            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
            tabPage1.Text = "MESAİ";
            tabPage2.Text = "YEMEK";
            tabPage3.Text = "GÖREV";
           

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
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID=p.Sube where Aktiflik= '"+Convert.ToBoolean("true")+"' and ş.Okulid='"+okulid+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();

        }
        SqlCommand komut;
        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID=p.Sube where Aktiflik= '" + Convert.ToBoolean("true") + "' and ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    filtre += " AND ";

                    filtre += " p.ID = '" + textBox11.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox9.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Email = '" + textBox9.Text + "'";
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

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += "p.Brans = '" + comboBox2.Text + "'";
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
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Adi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Soyadi = '" + textBox10.Text.ToUpper() + "'";
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
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.IseBaslangıcTarih between '" + dateTimePicker1.Value.Date.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
              




                if (string.IsNullOrEmpty(textBox9.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(comboBox3.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox1.Checked == false )
                {
                    filtre = "Select p.ID,p.Adi 'PERSONEL ADI',p.Soyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ' from Personeller p join Sube ş on ş.ID = p.Sube where Aktiflik = '"+Convert.ToBoolean("true")+"' and ş.Okulid = '"+okulid+"'";
                }
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                panel2.Visible = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                maskedTextBox1.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                checkBox1.Checked = false;

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

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            temizle();
        }

      

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.CurrentCell.RowIndex;
             if (dataGridView1.CurrentCell.ColumnIndex == 6)
            {
                panel2.Visible = true;
                panel1.Visible = true;
                label50.Text= dataGridView1.Rows[secilen].Cells[0].Value.ToString()+ " " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " "+ dataGridView1.Rows[secilen].Cells[2].Value.ToString() + " MESAİ SAATİ DÜZENLEME";
                label14.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString()+ " YEMEK SAATİ DÜZENLEME";
                label27.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString()+ " GÖREV DÜZENLEME";
                label40.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " " + dataGridView1.Rows[secilen].Cells[2].Value.ToString()+ " RANDEVU DÜZENLEME";
                baglan.Open();
                komut = new SqlCommand("Select Baslangic,Bitis,Baslangic2,Bitis2,Baslangic3,Bitis3,Baslangic4,Bitis4,Baslangic5,Bitis5 from Mesai where PersonelID= '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='MESAİ'", baglan);
                SqlDataReader oku2 = komut.ExecuteReader();
                while (oku2.Read())
                {
                    maskedTextBox2.Text = oku2.GetValue(0).ToString();
                    maskedTextBox3.Text = oku2.GetValue(1).ToString();
                    maskedTextBox5.Text = oku2.GetValue(2).ToString();
                    maskedTextBox4.Text = oku2.GetValue(3).ToString();
                    maskedTextBox7.Text = oku2.GetValue(4).ToString();
                    maskedTextBox6.Text = oku2.GetValue(5).ToString();
                    maskedTextBox9.Text = oku2.GetValue(6).ToString();
                    maskedTextBox8.Text = oku2.GetValue(7).ToString();
                    maskedTextBox11.Text = oku2.GetValue(8).ToString();
                    maskedTextBox10.Text = oku2.GetValue(9).ToString();


                }
                baglan.Close();
                baglan.Open();
                komut = new SqlCommand("Select Baslangic,Bitis,Baslangic2,Bitis2,Baslangic3,Bitis3,Baslangic4,Bitis4,Baslangic5,Bitis5 from Mesai where PersonelID= '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='YEMEK'", baglan);
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    maskedTextBox21.Text = oku.GetValue(0).ToString();
                    maskedTextBox20.Text = oku.GetValue(1).ToString();
                    maskedTextBox19.Text = oku.GetValue(2).ToString();
                    maskedTextBox18.Text = oku.GetValue(3).ToString();
                    maskedTextBox17.Text = oku.GetValue(4).ToString();
                    maskedTextBox16.Text = oku.GetValue(5).ToString();
                    maskedTextBox15.Text = oku.GetValue(6).ToString();
                    maskedTextBox14.Text = oku.GetValue(7).ToString();
                    maskedTextBox13.Text = oku.GetValue(8).ToString();
                    maskedTextBox12.Text = oku.GetValue(9).ToString();


                }
                baglan.Close();
                baglan.Open();

                komut = new SqlCommand("Select Baslangic,Bitis,Baslangic2,Bitis2,Baslangic3,Bitis3,Baslangic4,Bitis4,Baslangic5,Bitis5 from Mesai where PersonelID= '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='GÖREV'", baglan);
                SqlDataReader oku3 = komut.ExecuteReader();
                while (oku3.Read())
                {
                    maskedTextBox31.Text = oku3.GetValue(0).ToString();
                    maskedTextBox30.Text = oku3.GetValue(1).ToString();
                    maskedTextBox29.Text = oku3.GetValue(2).ToString();
                    maskedTextBox28.Text = oku3.GetValue(3).ToString();
                    maskedTextBox27.Text = oku3.GetValue(4).ToString();
                    maskedTextBox26.Text = oku3.GetValue(5).ToString();
                    maskedTextBox25.Text = oku3.GetValue(6).ToString();
                    maskedTextBox24.Text = oku3.GetValue(7).ToString();
                    maskedTextBox23.Text = oku3.GetValue(8).ToString();
                    maskedTextBox22.Text = oku3.GetValue(9).ToString();


                }
                baglan.Close();
                baglan.Open();

                komut = new SqlCommand("Select Baslangic,Bitis,Baslangic2,Bitis2,Baslangic3,Bitis3,Baslangic4,Bitis4,Baslangic5,Bitis5 from Mesai where PersonelID= '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='RANDEVU'", baglan);
                SqlDataReader oku4 = komut.ExecuteReader();
                while (oku4.Read())
                {
                    maskedTextBox41.Text = oku4.GetValue(0).ToString();
                    maskedTextBox40.Text = oku4.GetValue(1).ToString();
                    maskedTextBox39.Text = oku4.GetValue(2).ToString();
                    maskedTextBox38.Text = oku4.GetValue(3).ToString();
                    maskedTextBox37.Text = oku4.GetValue(4).ToString();
                    maskedTextBox36.Text = oku4.GetValue(5).ToString();
                    maskedTextBox35.Text = oku4.GetValue(6).ToString();
                    maskedTextBox34.Text = oku4.GetValue(7).ToString();
                    maskedTextBox33.Text = oku4.GetValue(8).ToString();
                    maskedTextBox32.Text = oku4.GetValue(9).ToString();


                }
                baglan.Close();
            }

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            //YEMEK
            try
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("Select ID from Mesai where PersonelID='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='YEMEK'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                string a = "";
                while (oku.Read())
                {
                    a = oku[0].ToString();
                }
                baglan.Close();
                if (string.IsNullOrEmpty(a))
                {
                    bool degisken = true;
                    string filtre = "insert into Mesai  (PersonelID, Adi";
                    string values = " Values ('" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "', 'YEMEK'";

                    if (maskedTextBox21.MaskFull == true && maskedTextBox20.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün, Baslangic, Bitis ";
                        values += "'PAZARTESİ'" + ",'" + maskedTextBox21.Text + "'" + ", '" + maskedTextBox20.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox19.MaskFull == true && maskedTextBox18.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün2, Baslangic2, Bitis2 ";
                        values += "'SALI', '" + maskedTextBox19.Text + "'" + ", '" + maskedTextBox18.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox17.MaskFull == true && maskedTextBox16.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün3, Baslangic3, Bitis3 ";
                        values += "'ÇARŞAMBA', '" + maskedTextBox17.Text + "'" + ", '" + maskedTextBox16.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox15.MaskFull == true && maskedTextBox14.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün4, Baslangic4, Bitis4 ";
                        values += "'PERŞEMBE', '" + maskedTextBox15.Text + "'" + ", '" + maskedTextBox14.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox13.MaskFull == true && maskedTextBox12.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün5, Baslangic5, Bitis5 ";
                        values += "'CUMA', '" + maskedTextBox13.Text + "'" + ", '" + maskedTextBox12.Text + "'";
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
                    panel2.Visible = false;
                    temizle2();

                }
                else
                {

                    bool degisken = false;
                    string filtre = "update Mesai set " ;

                    if (maskedTextBox21.MaskFull == true && maskedTextBox20.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün= 'PAZARTESİ' , Baslangic=" + "'" + maskedTextBox21.Text + "'" + ", Bitis =" + "'" + maskedTextBox20.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox19.MaskFull == true && maskedTextBox18.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün2= 'SALI' , Baslangic2=" + "'" + maskedTextBox19.Text + "'" + ", Bitis2 =" + "'" + maskedTextBox18.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox17.MaskFull == true && maskedTextBox16.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün3= 'ÇARŞAMBA' , Baslangic3=" + "'" + maskedTextBox17.Text + "'" + ", Bitis3 =" + "'" + maskedTextBox16.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox15.MaskFull == true && maskedTextBox14.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün4= 'PERŞEMBE' , Baslangic4=" + "'" + maskedTextBox15.Text + "'" + ", Bitis4 =" + "'" + maskedTextBox14.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox13.MaskFull == true && maskedTextBox12.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün5= 'CUMA' , Baslangic5=" + "'" + maskedTextBox13.Text + "'" + ", Bitis5 =" + "'" + maskedTextBox12.Text + "'";
                        degisken = true;
                    }
                    filtre += " where PersonelID = " + "'" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi = 'YEMEK'";
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi");
                    panel2.Visible = false;
                    temizle2();

                }
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());

            }
        }
        void temizle2()
        {
            maskedTextBox12.Text = "";
            maskedTextBox13.Text = "";
            maskedTextBox14.Text = "";
            maskedTextBox15.Text = "";
            maskedTextBox16.Text = "";
            maskedTextBox17.Text = "";
            maskedTextBox18.Text = "";
            maskedTextBox19.Text = "";
            maskedTextBox20.Text = "";
            maskedTextBox21.Text = "";


        }
        private void button3_Click(object sender, EventArgs e)
        {
            //MESAİ
            try
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("Select ID from Mesai where PersonelID='"+dataGridView1.Rows[secilen].Cells[0].Value.ToString()+"' and Adi='MESAİ'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                string a="";
                while (oku.Read())
                {
                    a = oku[0].ToString();
                }
                baglan.Close();
                if (string.IsNullOrEmpty(a)==true)
                {
                    bool degisken = true;
                    string filtre = "insert into Mesai  (PersonelID, Adi";
                    string values = " Values ('" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "', 'MESAİ'";

                    if (maskedTextBox2.MaskFull == true && maskedTextBox3.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün, Baslangic, Bitis ";
                        values += "'PAZARTESİ'" + ",'" + maskedTextBox2.Text + "'" + ", '" + maskedTextBox3.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox5.MaskFull == true && maskedTextBox4.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün2, Baslangic2, Bitis2 ";
                        values += "'SALI', '" + maskedTextBox5.Text + "'" + ", '" + maskedTextBox4.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox7.MaskFull == true && maskedTextBox6.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün3, Baslangic3, Bitis3 ";
                        values += "'ÇARŞAMBA', '" + maskedTextBox7.Text + "'" + ", '" + maskedTextBox6.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox9.MaskFull == true && maskedTextBox8.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün4, Baslangic4, Bitis4 ";
                        values += "'PERŞEMBE', '" + maskedTextBox9.Text + "'" + ", '" + maskedTextBox8.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox11.MaskFull == true && maskedTextBox10.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün5, Baslangic5, Bitis5 ";
                        values += "'CUMA', '" + maskedTextBox11.Text + "'" + ", '" + maskedTextBox10.Text + "'";
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
                    panel2.Visible = false;
                    temizle();

                }
                else
                {
                   
                    bool degisken = false;
                    string filtre = "update Mesai set ";

                    if (maskedTextBox2.MaskFull == true && maskedTextBox3.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; 

                        }
                        filtre += " Gün= 'PAZARTESİ' , Baslangic=" + "'" + maskedTextBox2.Text + "'"+ ", Bitis =" + "'" + maskedTextBox3.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox5.MaskFull == true && maskedTextBox4.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; 

                        }
                        filtre += " Gün2= 'SALI' , Baslangic2=" + "'" + maskedTextBox5.Text + "'" + ", Bitis2 =" + "'" + maskedTextBox4.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox7.MaskFull == true && maskedTextBox6.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün3= 'ÇARŞAMBA' , Baslangic3=" + "'" + maskedTextBox7.Text + "'" + ", Bitis3 =" + "'" + maskedTextBox6.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox9.MaskFull == true && maskedTextBox8.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; 

                        }
                        filtre += " Gün4= 'PERŞEMBE' , Baslangic4=" + "'" + maskedTextBox9.Text + "'" + ", Bitis4 =" + "'" + maskedTextBox8.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox11.MaskFull == true && maskedTextBox10.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün5= 'CUMA' , Baslangic5=" + "'" + maskedTextBox11.Text + "'" + ", Bitis5 =" + "'" + maskedTextBox10.Text + "'";
                        degisken = true;
                    }
                    filtre += " where PersonelID = " + "'" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi = 'MESAİ'";
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi");
                    panel2.Visible = false;
                    temizle();
                    
                }
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());

            }
        }
        void temizle()
        {
            maskedTextBox2.Text = "";
            maskedTextBox3.Text = "";
            maskedTextBox4.Text = "";
            maskedTextBox5.Text = "";
            maskedTextBox6.Text = "";
            maskedTextBox7.Text = "";
            maskedTextBox8.Text = "";
            maskedTextBox9.Text = "";
            maskedTextBox10.Text = "";
            maskedTextBox11.Text = "";



        }
        private void button5_Click(object sender, EventArgs e)
        {
            //GÖREV DÜZENLEME
            try
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("Select ID from Mesai where PersonelID='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='GÖREV'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                string a = "";
                while (oku.Read())
                {
                    a = oku[0].ToString();
                }
                baglan.Close();
                if (string.IsNullOrEmpty(a))
                {
                    bool degisken = true;
                    string filtre = "insert into Mesai  (PersonelID, Adi";
                    string values = " Values ('" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "', 'GÖREV'";

                    if (maskedTextBox31.MaskFull == true && maskedTextBox30.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün, Baslangic, Bitis ";
                        values += "'PAZARTESİ'" + ",'" + maskedTextBox31.Text + "'" + ", '" + maskedTextBox30.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox29.MaskFull == true && maskedTextBox28.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün2, Baslangic2, Bitis2 ";
                        values += "'SALI', '" + maskedTextBox29.Text + "'" + ", '" + maskedTextBox28.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox27.MaskFull == true && maskedTextBox26.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün3, Baslangic3, Bitis3 ";
                        values += "'ÇARŞAMBA', '" + maskedTextBox27.Text + "'" + ", '" + maskedTextBox26.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox25.MaskFull == true && maskedTextBox24.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün4, Baslangic4, Bitis4 ";
                        values += "'PERŞEMBE', '" + maskedTextBox25.Text + "'" + ", '" + maskedTextBox24.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox23.MaskFull == true && maskedTextBox22.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün5, Baslangic5, Bitis5 ";
                        values += "'CUMA', '" + maskedTextBox23.Text + "'" + ", '" + maskedTextBox22.Text + "'";
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
                    panel2.Visible = false;
                    temizle4();

                }
                else
                {

                    bool degisken = false;
                    string filtre = "update Mesai set ";

                    if (maskedTextBox31.MaskFull == true && maskedTextBox30.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün= 'PAZARTESİ' , Baslangic=" + "'" + maskedTextBox31.Text + "'" + ", Bitis =" + "'" + maskedTextBox30.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox29.MaskFull == true && maskedTextBox28.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün2= 'SALI' , Baslangic2=" + "'" + maskedTextBox29.Text + "'" + ", Bitis2 =" + "'" + maskedTextBox28.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox27.MaskFull == true && maskedTextBox26.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün3= 'ÇARŞAMBA' , Baslangic3=" + "'" + maskedTextBox27.Text + "'" + ", Bitis3 =" + "'" + maskedTextBox26.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox25.MaskFull == true && maskedTextBox24.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün4= 'PERŞEMBE' , Baslangic4=" + "'" + maskedTextBox25.Text + "'" + ", Bitis4 =" + "'" + maskedTextBox24.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox23.MaskFull == true && maskedTextBox22.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün5= 'CUMA' , Baslangic5=" + "'" + maskedTextBox23.Text + "'" + ", Bitis5 =" + "'" + maskedTextBox22.Text + "'";
                        degisken = true;
                    }
                    filtre += " where PersonelID = " + "'" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi = 'GÖREV'";
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi");
                    panel2.Visible = false;
                    temizle4();

                }
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());

            }


        }
        void temizle4()
        {
            maskedTextBox41.Text = "";
            maskedTextBox40.Text = "";
            maskedTextBox39.Text = "";
            maskedTextBox38.Text = "";
            maskedTextBox37.Text = "";
            maskedTextBox36.Text = "";
            maskedTextBox35.Text = "";
            maskedTextBox34.Text = "";
            maskedTextBox33.Text = "";
            maskedTextBox32.Text = "";



        }
        private void button6_Click(object sender, EventArgs e)
        {
            //RANDEVU DÜZENLEME
            try
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                komut = new SqlCommand("Select ID from Mesai where PersonelID='" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi='RANDEVU'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                string a = "";
                while (oku.Read())
                {
                    a = oku[0].ToString();
                }
                baglan.Close();
                if (string.IsNullOrEmpty(a))
                {
                    bool degisken = true;
                    string filtre = "insert into Mesai  (PersonelID, Adi";
                    string values = " Values ('" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "', 'RANDEVU'";

                    if (maskedTextBox41.MaskFull == true && maskedTextBox40.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün, Baslangic, Bitis ";
                        values += "'PAZARTESİ'" + ",'" + maskedTextBox41.Text + "'" + ", '" + maskedTextBox40.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox39.MaskFull == true && maskedTextBox38.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün2, Baslangic2, Bitis2 ";
                        values += "'SALI', '" + maskedTextBox39.Text + "'" + ", '" + maskedTextBox38.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox37.MaskFull == true && maskedTextBox36.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün3, Baslangic3, Bitis3 ";
                        values += "'ÇARŞAMBA', '" + maskedTextBox37.Text + "'" + ", '" + maskedTextBox36.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox35.MaskFull == true && maskedTextBox34.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün4, Baslangic4, Bitis4 ";
                        values += "'PERŞEMBE', '" + maskedTextBox35.Text + "'" + ", '" + maskedTextBox34.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox33.MaskFull == true && maskedTextBox32.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , "; values += " , ";

                        }
                        filtre += " Gün5, Baslangic5, Bitis5 ";
                        values += "'CUMA', '" + maskedTextBox33.Text + "'" + ", '" + maskedTextBox32.Text + "'";
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
                    panel2.Visible = false;
                    temizle4();

                }
                else
                {

                    bool degisken = false;
                    string filtre = "update Mesai set ";

                    if (maskedTextBox41.MaskFull == true && maskedTextBox40.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün= 'PAZARTESİ' , Baslangic=" + "'" + maskedTextBox41.Text + "'" + ", Bitis =" + "'" + maskedTextBox40.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox39.MaskFull == true && maskedTextBox38.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün2= 'SALI' , Baslangic2=" + "'" + maskedTextBox39.Text + "'" + ", Bitis2 =" + "'" + maskedTextBox38.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox37.MaskFull == true && maskedTextBox36.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün3= 'ÇARŞAMBA' , Baslangic3=" + "'" + maskedTextBox37.Text + "'" + ", Bitis3 =" + "'" + maskedTextBox36.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox35.MaskFull == true && maskedTextBox34.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün4= 'PERŞEMBE' , Baslangic4=" + "'" + maskedTextBox35.Text + "'" + ", Bitis4 =" + "'" + maskedTextBox34.Text + "'";
                        degisken = true;
                    }
                    if (maskedTextBox33.MaskFull == true && maskedTextBox32.MaskFull == true)
                    {
                        if (degisken == true)
                        {
                            filtre += " , ";

                        }
                        filtre += " Gün5= 'CUMA' , Baslangic5=" + "'" + maskedTextBox33.Text + "'" + ", Bitis5 =" + "'" + maskedTextBox32.Text + "'";
                        degisken = true;
                    }
                    filtre += " where PersonelID = " + "'" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "' and Adi = 'RANDEVU'";
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Güncellendi");
                    panel2.Visible = false;
                    temizle4();

                }
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());

            }
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);

        }
    }
}
