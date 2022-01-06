using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class ANKET : Form
    {
        public ANKET()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
          


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://accounts.google.com/ServiceLogin/signinchooser?service=wise&passive=1209600&continue=https%3A%2F%2Fdocs.google.com%2Fforms%2Fcreate%3Fhl%3Dtr&followup=https%3A%2F%2Fdocs.google.com%2Fforms%2Fcreate%3Fhl%3Dtr&ltmpl=forms&hl=tr&flowName=GlifWebSignIn&flowEntry=ServiceLogin");
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();



        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true ||  string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(richTextBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Anket (AnketAdı, Tarih,Açıklama,Sube,AnketLinki,CevapLinki,Bitis,Tür) values (@p1, @p2, @p3,@p5, @p6,@p7, @p8,@p9)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p3", richTextBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p6", textBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", textBox3.Text+ "#responses");
                    komutkaydet.Parameters.AddWithValue("@p8", dateTimePicker1.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p9", comboBox2.Text);




                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    panel2.Visible = false;
                    textBox2.Text = "";
                    textBox3.Text = "";
                    richTextBox1.Text = "";
                    comboBox5.Text = "";
                    dateTimePicker7.Value = DateTime.Now;
                    dateTimePicker1.Value = DateTime.Now;

                }
                catch (Exception a)
                {

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
            comboBox5.Text = "";
            dateTimePicker7.Value = DateTime.Now;


        }
        void griddoldur()
        {
            try
            {

                bool degisken = true;
                string filtre = "Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tür 'ANKET TÜRÜ', t.AnketAdı 'ANKET ADI',t.Tarih 'BAŞLANGIÇ TARİHİ',t.Bitis 'BİTİŞ TARİHİ',t.AnketLinki 'ANKET LİNKİ',t.CevapLinki 'CEVAP LİNKİ',t.Açıklama AÇIKLAMA from Anket t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'";

                if (string.IsNullOrEmpty(textBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.AnketAdı ='" + textBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tür ='" + comboBox3.Text + "'";
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

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Bitis between '" + dateTimePicker5.Value.ToString("yyyyMMdd") + "' and '" + dateTimePicker2.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Sube = " + subeid[comboBox1.SelectedIndex];
                    degisken = true;
                }

                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ANKET");
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
            catch (Exception)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
         
        }
        SqlCommand komut;
        private void button5_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            griddoldur();
            panel2.Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex==6)
            {
                string link = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[6].Value.ToString();
                System.Diagnostics.Process.Start(link);
                

            }
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                string link = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[7].Value.ToString();
                System.Diagnostics.Process.Start(link);

            }
            if (dataGridView1.CurrentCell.ColumnIndex == 9)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "ANKET SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + " İSİMLİ ANKET TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "ANKET SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM Anket WHERE ID=@id";
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

        private void ANKET_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox1.Items.Add(oku[0].ToString());
                comboBox5.Items.Add(oku[0].ToString());


                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            comboBox2.Items.Add("ONLİNE ANKET");
            comboBox2.Items.Add("REHBERLİK ANKETİ");
            comboBox2.Items.Add("FORM ANKETİ");
            comboBox2.Items.Add("DİĞER");
            comboBox3.Items.Add("ONLİNE ANKET");
            comboBox3.Items.Add("REHBERLİK ANKETİ");
            comboBox3.Items.Add("FORM ANKETİ");
            comboBox3.Items.Add("DİĞER");



        }


    }
}
