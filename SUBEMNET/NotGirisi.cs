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
    public partial class NotGirisi : Form
    {
        public NotGirisi()
        {
            InitializeComponent();
        }
        double ort, s1=0, s2=0, soz1=0, soz2=0,sayac=0;
        string ogrId, dersId;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da, da2;

        private void dataGridView7_Click(object sender, EventArgs e)
        {
            ogrId = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dersId = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString() + dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        DataSet ds;
        DataTable dt, dt2;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        SqlCommand komut;
        private void NotGirisi_Load(object sender, EventArgs e)
        {
            baglan.Open();
            da = new SqlDataAdapter("Select*from Sube where okulid='"+okulid+"'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            cmb1.ValueMember = "ID";
            cmb1.DisplayMember = "ŞubeAdi";
            cmb1.DataSource = dt;

            da2 = new SqlDataAdapter("Select*from Ders", baglan);
            dt2 = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da2.Fill(dt2);
            comboBox1.ValueMember = "ID";
            comboBox1.DisplayMember = "DersAd";
            comboBox1.DataSource = dt2;
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmb2.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand ekleNot = new SqlCommand("Update Notlar Set Sinav1=@a3,Sinav2=@a4,Sozlu1=@a5,Sozlu2=@a6,Ortalama=@a7 Where OgrId=" + ogrId + "and DersId=" + dersId, baglan);
                ekleNot.Parameters.AddWithValue("@a3", tSnv1.Text);
                ekleNot.Parameters.AddWithValue("@a4", tSnv2.Text);
                ekleNot.Parameters.AddWithValue("@a5", tSoz1.Text);
                ekleNot.Parameters.AddWithValue("@a6", tSoz2.Text);
                ekleNot.Parameters.AddWithValue("@a7", tOrt.Text);
                ekleNot.ExecuteNonQuery();

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

        private void button11_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(tSnv1.Text))
            {
                s1 = double.Parse(tSnv1.Text);
                sayac++;
            }
            if (!string.IsNullOrEmpty(tSnv2.Text))
            {
                s2 = double.Parse(tSnv2.Text);
                sayac++;
            }
            if (!string.IsNullOrEmpty(tSoz1.Text))
            {
                soz1 = double.Parse(tSoz1.Text);
                sayac++;
            }
            if (!string.IsNullOrEmpty(tSoz2.Text))
            {
                soz1 = double.Parse(tSoz2.Text);
                sayac++;
            }

            ort = (s1 + s2 + soz1 + soz2)/sayac;
            tOrt.Text = ort.ToString();
            s1 = 0; s2 = 0; soz1 = 0; soz2 = 0; sayac = 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (cmb1.SelectedIndex != -1 && cmb1.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
            {
                baglan.Open();
                da = new SqlDataAdapter("Select Ogrenci.Id, Ogrenci.Snf as Sınıf, Ogrenci.Adi as Ad, Ogrenci.Soyadi as Soyad, Ders.DersAd as Ders, Ders.Id as DersNo," +
                " Notlar.Sinav1, Notlar.Sinav2, Notlar.Sozlu1, Notlar.Sozlu2, Notlar.Ortalama From Ogrenci " +
                "Inner Join Notlar ON Notlar.OgrId=Ogrenci.Id Inner Join Ders on Notlar.DersId=" + comboBox1.SelectedValue + " and Ogrenci.sube=" + cmb1.SelectedValue + " and Ogrenci.Snf=" + cmb2.Text, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                baglan.Close();
            }
            else
            {
                MessageBox.Show("Tüm Alanlar Seçili Olmalı.");
            }
        }
    }
}

