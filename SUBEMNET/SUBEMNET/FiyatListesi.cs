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
        SqlConnection baglan = new SqlConnection(@"Data Source=.;Initial Catalog=SUBEMNET;Integrated Security=True");
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
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA. LÜTFEN TEKRAR DENEYİNİZ." + a.ToString());
            }
        }

        private void FiyatListesi_Load(object sender, EventArgs e)
        {

        }
    }
}
