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
    public partial class ETKİNLİK_EKLE : Form
    {
        public ETKİNLİK_EKLE()
        {
            InitializeComponent();
        }
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        private void ETKİNLİK_EKLE_Load(object sender, EventArgs e)
        {
            subeid.Clear();
            comboBox2.Items.Add("GEZİ");
            comboBox2.Items.Add("MÜZİK");
            comboBox2.Items.Add("OYUN");
            comboBox2.Items.Add("DRAMA");
            comboBox2.Items.Add("UZAKTAN EĞİTİM");
            comboBox2.Items.Add("RESİM");
            comboBox2.Items.Add("SPOR");
            comboBox2.Items.Add("KUTLAMA");
            comboBox2.Items.Add("TÖREN");
            comboBox2.Items.Add("EĞLENCE");
            comboBox2.Items.Add("SEMİNER");
            comboBox2.Items.Add("PİKNİK");
            comboBox2.Items.Add("REHBERLİK");
            comboBox2.Items.Add("SINIF İÇİ ETKİNLİK");

            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox5.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);
            }
            baglan.Close();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";



        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(textBox1.Text) == true || string.IsNullOrEmpty(richTextBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ GİRİNİZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Etkinlik (EtkinlikTürü, EtkinlikAdı, Tarih,Açıklama, Sube) values (@p1, @p2, @p3, @p4,@p5)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", comboBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", dateTimePicker1.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p4", richTextBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    comboBox5.Text = "";
                    comboBox2.Text = "";
                    textBox1.Text = "";
                    richTextBox4.Text = "";
                    dateTimePicker1.Value = DateTime.Now.Date;
              
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

    }
}
