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
    public partial class MAAŞ_KONTROL : Form
    {
        public MAAŞ_KONTROL()
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
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select p.ID,p.Adi 'PERSONEL ADI' , p.Soyadi 'PERSONEL SOYADI',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ',p.MAAŞ  from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and Aktiflik='"+Convert.ToBoolean(true)+"'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "SEÇ";
            dgvBtn.Text = "SEÇ";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView1.Columns.Add(dgvBtn);
            DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
            dgvBtn2.HeaderText = "ÇIKAR";
            dgvBtn2.Text = "ÇIKAR";
            dgvBtn2.UseColumnTextForButtonValue = true;
            dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn2.Width = 70;
            dataGridView1.Columns.Add(dgvBtn2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox10.Text = "";
            textBox11.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            maskedTextBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";

            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            checkBox1.Checked = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select p.ID,p.Adi 'PERSONEL ADI' , p.Soyadi 'PERSONEL SOYADI',p.CepTel 'TELEFON',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ'  from Personeller p join Sube ş on ş.ID=p.Sube  where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox11.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
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
                    filtre += " p.Brans = '" + comboBox2.Text + "'";
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
               if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Aktiflik = '" + Convert.ToBoolean("true") + "'";
                    degisken = true;
               




                if (string.IsNullOrEmpty(textBox9.Text) == true && string.IsNullOrEmpty(textBox8.Text) == true && string.IsNullOrEmpty(textBox10.Text) == true && string.IsNullOrEmpty(textBox11.Text) == true && string.IsNullOrEmpty(comboBox1.Text) == true && string.IsNullOrEmpty(comboBox2.Text) == true && string.IsNullOrEmpty(comboBox3.Text) == true && string.IsNullOrEmpty(maskedTextBox1.Text.Replace(" ", "")) == true && checkBox1.Checked == false)
                {
                    filtre = "Select p.ID,p.Adi 'PERSONEL ADI' , p.Soyadi 'PERSONEL SOYADI',p.CepTel 'TELEFON',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ'  from Personeller p join Sube ş on ş.ID=p.Sube  where ş.Okulid='" + okulid +"' and Aktiflik='"+Convert.ToBoolean(true)+"'";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "ÇIKAR";
                dgvBtn2.Text = "ÇIKAR";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                panel2.Visible = false;
                textBox10.Text = "";
                textBox11.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                maskedTextBox1.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";

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
        List<int> secilenler = new List<int>();
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Selected = true;

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("AD", "AD");
            dataGridView2.Columns.Add("SOYAD", "SOYAD");
            dataGridView2.Columns.Add("ŞUBE", "ŞUBE");
            dataGridView2.Columns.Add("POZİSYON", "POZİSYON");
            dataGridView2.Columns.Add("BRANŞ", "BRANŞ");
            dataGridView2.Columns.Add("MAAŞ", "MAAŞ");

            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "ÇIKAR";
            dgvBtn.Text = "ÇIKAR";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView2.Columns.Add(dgvBtn);


            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = dataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                dataGridView2.Rows[index].Cells[3].Value = dataGridView1.SelectedRows[i].Cells[3].Value.ToString();
                dataGridView2.Rows[index].Cells[4].Value = dataGridView1.SelectedRows[i].Cells[4].Value.ToString();
                dataGridView2.Rows[index].Cells[5].Value = dataGridView1.SelectedRows[i].Cells[5].Value.ToString();
                dataGridView2.Rows[index].Cells[6].Value = dataGridView1.SelectedRows[i].Cells[6].Value.ToString();


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                if (dataGridView1.CurrentCell.RowIndex != dataGridView1.RowCount-1)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    secilenler.Add(secilen);
                }
                else
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    secilenler.Remove(secilen);
                    dataGridView1.Rows[secilen].Selected = false;
                }
             
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 8)
            {

                int secilen = dataGridView1.CurrentCell.RowIndex;
                secilenler.Remove(secilen);
                dataGridView1.Rows[secilen].Selected = false;

            }
            for (int i = 0; i < secilenler.Count; i++)
            {
                dataGridView1.Rows[secilenler[i]].Selected = true;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            secilenler.Clear();
            dataGridView1.ClearSelection();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == 7)
            {

                int secilen = dataGridView2.CurrentCell.RowIndex;
                DataGridViewRow dgvDelRow = dataGridView2.Rows[secilen];
                dataGridView2.Rows.Remove(dgvDelRow);

            }
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        private void MAAŞ_KONTROL_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            panel5.Visible = true;
            panel6.Visible = false;
            subeid.Clear();
            //dataGridView1.MultiSelect = true;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            panel2.Visible = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            griddoldur();
          
            dataGridView1.Rows[0].Selected = false;


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
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                panel5.Visible = true;
                panel6.Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                panel5.Visible = true;
                panel6.Visible = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN ZAM ORANINI GİRİNİZ.");
            }
            else
            {
                try
                {
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        komut = new SqlCommand("Select maasi from Personeller where ID='" + dataGridView2.Rows[i].Cells[0].Value.ToString() + "'", baglan);
                        baglan.Open();
                        decimal tutar = 0;
                        SqlDataReader oku3 = komut.ExecuteReader();
                        while (oku3.Read())
                        {
                            tutar = (decimal)oku3[0];
                        }
                        baglan.Close();
                        decimal result = Decimal.Divide(Decimal.Multiply(tutar, Convert.ToDecimal(textBox1.Text)), 100);
                        result = result + tutar;
                        string deger = result.ToString();
                        baglan.Open();
                        SqlCommand degistir = new SqlCommand("update Personeller set Maaş=@p1,maasi=@p2 where ID=@a2 ", baglan);
                        degistir.Parameters.AddWithValue("@a2", dataGridView2.Rows[i].Cells[0].Value.ToString());
                        degistir.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(deger)));
                        degistir.Parameters.AddWithValue("@p2", deger.Replace(",", "."));
                        degistir.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Güncellendi.");

                        panel2.Visible = false;
                        griddoldur();
                        textBox1.Text = "";
                    }
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("LÜTFEN YENİ MAAŞ BİLGİSİNİ GİRİNİZ.");
            }
            else
            {
                try
                {

                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        baglan.Open();
                        SqlCommand degistir = new SqlCommand("update Personeller set Maaş=@p1,maasi=@p2 where ID=@a2 ", baglan);
                        degistir.Parameters.AddWithValue("@a2", dataGridView2.Rows[i].Cells[0].Value.ToString());
                        degistir.Parameters.AddWithValue("@p1", string.Format("{0:N}", Convert.ToDecimal(textBox2.Text + "," + textBox3.Text)));
                        degistir.Parameters.AddWithValue("@p2", textBox2.Text + "." + textBox3.Text);
                        degistir.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Güncellendi.");

                        panel2.Visible = false;
                        griddoldur();
                        textBox2.Text = "";
                        textBox3.Text = "00";
                    }
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
                }
            }

        }
    }
}

