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
    public partial class VeliRandevuOlustur : Form
    {
        public VeliRandevuOlustur()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        private void button30_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbOgr.Text) == true)
            {
                MessageBox.Show("ÖĞRENCİ SEÇİLMESİ ZORUNLUDUR.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand ekleVeli = new SqlCommand("insert into Veli (OgrId,Gorusen,GrsmTur,Tur,Tarih,Saat) " +
                     "values (@a1, @a2, @a3, @a4, @a5, @a6)", baglan);

                    ekleVeli.Parameters.AddWithValue("@a1", ogrenci[cmbOgr.SelectedIndex]);
                    ekleVeli.Parameters.AddWithValue("@a2", cmbOgrV.Text);
                    ekleVeli.Parameters.AddWithValue("@a3", textBox1.Text);
                    ekleVeli.Parameters.AddWithValue("@a4", textBox2.Text);
                    ekleVeli.Parameters.AddWithValue("@a5", dtBasV.Value.ToString("yyyy-mm-dd"));
                    ekleVeli.Parameters.AddWithValue("@a6", dtBitV.Value.ToString("hh:mm"));
                    ekleVeli.ExecuteNonQuery();

                }
                catch (Exception a)
                {
                    MessageBox.Show("Hata.");
                }
                finally
                {
                    baglan.Close();
                }
            }
        }
        SqlCommand komut;
        int okulid = Form1.okulid;
        private void VeliRandevuOlustur_Load(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select*from Sube where Okulid='"+okulid+"'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            cmbSubeV.ValueMember = "ID";
            cmbSubeV.DisplayMember = "ŞubeAdi";
            cmbSubeV.DataSource = dt;
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbSnfV.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='ÖĞRETMEN'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbOgrV.Items.Add(oku3[0].ToString());
            }
            baglan.Close();
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi),p.ID from Ogrenci p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                cmbOgr.Items.Add(oku4[0].ToString());
                ogrenci.Add((int)oku4[1]);
            }
            baglan.Close();
           
        }
        List<int> ogrenci = new List<int>();
        private void cmbSnfV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubeV.SelectedIndex != -1 && cmbSnfV.SelectedIndex!=-1)
            {
                baglan.Open();
                da = new SqlDataAdapter("Select*from Ogrenci where Sube = " + cmbSubeV.SelectedValue + "And Snf='" + cmbSnfV.Text + "'", baglan);              
                cmdb = new SqlCommandBuilder(da);
                dt = new DataTable();
                da.Fill(dt);
                cmbOgr.ValueMember = "ID";
                cmbOgr.DisplayMember = "AdSoyad";
                cmbOgr.DataSource = dt;
                baglan.Close();
            }
        }
    }
}
