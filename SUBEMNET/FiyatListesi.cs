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
    public partial class FiyatListesi : Form
    {
        public FiyatListesi()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand ekle = new SqlCommand("insert into FiyatListe (Sube, Devre, Kur, Program, EgitimSaat, YazKursSaat, EgitimBas, EgitimBit, YazKursBas, YazKursBit, Indirim) " +
                    "values (@a1, @a2, @a3, @a4, @a5, @a6, @a7, @a8, @a9, @a10, @a11)", baglan);
                ekle.Parameters.AddWithValue("@a1", comboBoxSube.Text);
                ekle.Parameters.AddWithValue("@a2", comboBoxDevre.Text);
                ekle.Parameters.AddWithValue("@a3", comboBoxKur.Text);
                ekle.Parameters.AddWithValue("@a4", comboBoxProgram.Text);
                ekle.Parameters.AddWithValue("@a5", numericUpDownEgitim.Value.ToString());
                ekle.Parameters.AddWithValue("@a6", numericUpDownYazKurs.Value.ToString());
                ekle.Parameters.AddWithValue("@a7", dateTimePickerEgitimBas.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a8", dateTimePickerEgitimBit.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a9", dateTimePickerYazBas.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a9", dateTimePickerYazBit.Value.ToString("yyyyMMdd"));
                ekle.Parameters.AddWithValue("@a6", numericUpDownIndirim.Value.ToString());
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
                panel3.Visible = false;
                query = "select * from FiyatListe where Sube='"+comboBoxSube.Text+"'";

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }
        SqlCommand komut;
        int okulid = Form1.okulid;
        private void FiyatListesi_Load(object sender, EventArgs e)
        {
            comboBoxKur.Items.Add("SAY");
            comboBoxKur.Items.Add("SOZ");
            comboBoxKur.Items.Add("EA");
            comboBoxKur.Items.Add("YDİL");
            comboBoxKur.Items.Add("MES");

            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBoxDevre.Items.Add(oku[0].ToString());

            }
            baglan.Close();
           
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBoxProgram.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBoxSube.Items.Add(oku4[0].ToString());

            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
        public string query;
        private void button7_Click(object sender, EventArgs e)
        {
            query = "select * from FiyatListe";
            griddoldur();
        }
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);

            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
