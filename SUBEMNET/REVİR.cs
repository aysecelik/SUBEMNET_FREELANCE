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
    public partial class REVİR : Form
    {
        public REVİR()
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
        int okulid = Form1.okulid;
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tür 'TÜR', t.AdıSoyadı 'ADI-SOYADI',t.Durum 'DURUM',t.Tarih 'TARİH',t.Açıklama 'AÇIKLAMA' from REVİR t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "REVİR");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
            dgvBtn2.HeaderText = "SİL";
            dgvBtn2.Text = "SİL";
            dgvBtn2.UseColumnTextForButtonValue = true;
            dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn2.Width = 70;
            dataGridView1.Columns.Add(dgvBtn2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKLE BUTON
            panel2.Visible = true;
            panel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ARA/FİLTRELE BUTON  
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //ARAMA KAPAT BUTONU
            panel2.Visible = false;
            textBox1.Text = "";
            textBox6.Text = "";
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            comboBox1.Text = "";
            comboBox4.Text = "";
            comboBox6.Text = "";
        }
        SqlCommand komut;
        private void REVİR_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            comboBox1.Items.Add("ÖĞRENCİ");
            comboBox1.Items.Add("PERSONEL");
            comboBox6.Items.Add("TEŞHİS YAPILDI");
            comboBox6.Items.Add("HASTANEYE SEVK");
            comboBox2.Items.Add("ÖĞRENCİ");
            comboBox2.Items.Add("PERSONEL");
            comboBox3.Items.Add("TEŞHİS YAPILDI");
            comboBox3.Items.Add("HASTANEYE SEVK");
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
            dateTimePicker7.Format = DateTimePickerFormat.Custom;
            dateTimePicker7.CustomFormat = "dd/MM/yyyy";
            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox4.Items.Add(oku[0].ToString());
                comboBox5.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();

        }
        List<int> subeid = new List<int>();
        private void button8_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tür 'TÜR', t.AdıSoyadı 'ADI-SOYADI',t.Durum 'DURUM',t.Tarih 'TARİH',t.Açıklama 'AÇIKLAMA' from REVİR t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'";
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.ID = " + textBox1.Text;
                    degisken = true;
                }


                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.AdıSoyadı = '" + textBox6.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tür='" + comboBox1.Text + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tarih between '" + dateTimePicker3.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker4.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Sube =" + subeid[comboBox4.SelectedIndex];
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Durum = '" + comboBox6.Text+"'";
                    degisken = true;
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "REVİR");
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
                textBox1.Text = "";
                textBox6.Text = "";
                dateTimePicker3.Value = DateTime.Now;
                dateTimePicker4.Value = DateTime.Now;
                comboBox1.Text = "";
                comboBox4.Text = "";
                comboBox6.Text = "";
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox2.Text = "";
            comboBox2.Text = "";
            comboBox5.Text = "";
            comboBox3.Text = "";
            dateTimePicker7.Value = DateTime.Now;
            richTextBox1.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //EKLMEE
            if (string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(richTextBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Revir (AdıSoyadı, Tarih, Durum,Tür, Sube,Açıklama) values (@p1, @p2, @p3, @p4,@p5,@p6)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p6", richTextBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    textBox2.Text = "";
                    comboBox2.Text = "";
                    comboBox5.Text = "";
                    comboBox3.Text = "";
                    dateTimePicker7.Value = DateTime.Now;
                    richTextBox1.Text = "";
                    panel2.Visible = false;
                }
                catch (Exception a)
                {
                    baglan.Close();

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "REVİR KAYIT SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + " KİŞİYE AİT REVİR KAYDINI SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "REVİR KAYIT SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM REVİR WHERE ID=@id";
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
                            MessageBox.Show(a.ToString());
                        }

                    }
                }
            }
        }
    }
}
