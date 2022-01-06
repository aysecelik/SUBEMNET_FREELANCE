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
    public partial class OgrenciYoklama : Form
    {
        public OgrenciYoklama()
        {
            InitializeComponent();
        }
        public string query;

        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        string yol = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        void dvmszlkDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "Yoklama";
                dgvBtn.Text = ">>";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            baglan.Close();
        }
        void bgnGlmynDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "Yoklama";
                dgvBtn.Text = ">>";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView4.Columns.Add(dgvBtn);
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView4.DataSource = dt;
            }
            baglan.Close();
        }
        private void button33_Click(object sender, EventArgs e)
        {
            panel21.Visible = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.Sheets["Sheet1"];
            worksheet = (Microsoft.Office.Interop.Excel._Worksheet)workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Devansızlık Listesi";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  
            workbook.SaveAs(yol + "\\Devamsızlık Listesi.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            bool deg = false;
            string filtre = "Select Ogrenci.Devre, Ogrenci.Snf, Ogrenci.SozNo, Ogrenci.OkulNo, Ogrenci.Adi, Ogrenci.Soyadi, sum(Devamsizlik.Saat)From Ogrenci Inner join Devamsizlik On Ogrenci.ID=Devamsizlik.OgrID";
            deg = true;
            if (cmbSube.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Sube ='" + cmbSube.SelectedValue+"'";
                deg = true;
            }
            if (cmbDevre.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Devre ='" + cmbDevre.Text + "'";
                deg = true;
            }
            if (cmbSnf.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Snf ='" + cmbSnf.Text + "'";
                deg = true;
            }
            if (cmbYTur.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Devamsizlik.Tur='" + cmbYTur.Text + "'";
                deg = true;
            }
            if (!string.IsNullOrEmpty(txtAd.Text))
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Adi='" + txtAd.Text.TrimEnd() + "'";
                deg = true;
            }
            if (!string.IsNullOrEmpty(txtSoyad.Text))
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Soyadi='" + txtSoyad.Text.TrimEnd() + "'";
                deg = true;
            }
            if (dtBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
            {
                if (deg == true)
                {
                    filtre += " AND ";
                }
                filtre += " Tarih between '" + dtBas.Value.ToString("yyyyMMdd") + "' and '" + dtBit.Value.ToString("yyyyMMdd") + "'";
            }
            query = filtre;
            panel21.Visible = false;
            dvmszlkDoldur();
            query = null;
        }

        private void devamsızlıkSayısıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel19.Visible = true;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            panel19.Visible = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex !=-1 && comboBox3.SelectedIndex != -1 && comboBox2.SelectedIndex != -1 && comboBox1.SelectedIndex != -1)
            {
                baglan.Open();
                da = new SqlDataAdapter("SELECT Id, OkulNo, Adi, Soyadi From Ogrenci Where Sube=" + comboBox4.SelectedValue + " And Snf=" + comboBox1.Text, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                baglan.Close();
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                dataGridView2.Columns.Add(chk);
                chk.HeaderText = "Gelmedi";
                chk.Name = "chk";
                DataGridViewCheckBoxColumn chk2= new DataGridViewCheckBoxColumn();
                dataGridView2.Columns.Add(chk2);
                chk2.HeaderText = "Geç Kaldı";
                chk2.Name = "chk2";
                DataGridViewCheckBoxColumn chk3 = new DataGridViewCheckBoxColumn();
                dataGridView2.Columns.Add(chk3);
                chk3.HeaderText = "İzinli";
                chk3.Name = "chk3";
                DataGridViewCheckBoxColumn chk4 = new DataGridViewCheckBoxColumn();
                dataGridView2.Columns.Add(chk4);
                chk4.HeaderText = "Raporlu";
                chk4.Name = "chk4";
            }
            else
            {
                MessageBox.Show("Tüm Alanları Seçin");
            }
        }
        List<int> subeid = new List<int>();
        private void button10_Click(object sender, EventArgs e)
        {
            List<string> selectedGelmedi = new List<string>();
            List<string> selectedGec = new List<string>();
            List<string> selectedIzin = new List<string>();
            List<string> selectedRapor = new List<string>();
            DataGridViewRow drow = new DataGridViewRow();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                drow = dataGridView2.Rows[i];
                if (Convert.ToBoolean(drow.Cells["chk"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedGelmedi.Add(id); //seçiliyse listeye ekle
                }
                if (Convert.ToBoolean(drow.Cells["chk2"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedGec.Add(id); //seçiliyse listeye ekle
                }
                if (Convert.ToBoolean(drow.Cells["chk3"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedIzin.Add(id); //seçiliyse listeye ekle
                }
                if (Convert.ToBoolean(drow.Cells["chk4"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedRapor.Add(id); //seçiliyse listeye ekle
                }
            }
            if (selectedGelmedi.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO Devamsizlik (OgrId, Ders, Snf, Tarih, Tur, TurId, Saat) VALUES(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", baglan);

                foreach (string a in selectedGelmedi)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", comboBox2.Text);
                    ekle.Parameters.AddWithValue("@a3", comboBox1.Text);
                    ekle.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-mm-dd"));
                    ekle.Parameters.AddWithValue("@a5", "Gelmedi");
                    ekle.Parameters.AddWithValue("@a6", 1);
                    ekle.Parameters.AddWithValue("@a7", comboBox3.Text);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
            if (selectedGec.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO Devamsizlik (OgrId, Ders, Snf, Tarih, Tur, TurId, Saat) VALUES(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", baglan);

                foreach (string a in selectedGec)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", comboBox2.Text);
                    ekle.Parameters.AddWithValue("@a3", comboBox1.Text);
                    ekle.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-mm-dd"));
                    ekle.Parameters.AddWithValue("@a5", "Geç Kaldı");
                    ekle.Parameters.AddWithValue("@a6", 2);
                    ekle.Parameters.AddWithValue("@a7", comboBox3.Text);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
            if (selectedIzin.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO Devamsizlik (OgrId, Ders, Snf, Tarih, Tur, TurId, Saat) VALUES(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", baglan);

                foreach (string a in selectedIzin)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", comboBox2.Text);
                    ekle.Parameters.AddWithValue("@a3", comboBox1.Text);
                    ekle.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-mm-dd"));
                    ekle.Parameters.AddWithValue("@a5", "İzinli");
                    ekle.Parameters.AddWithValue("@a6", 3);
                    ekle.Parameters.AddWithValue("@a7", comboBox3.Text);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
            if (selectedRapor.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO Devamsizlik (OgrId, Ders, Snf, Tarih, Tur, TurId, Saat) VALUES(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", baglan);

                foreach (string a in selectedRapor)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", comboBox2.Text);
                    ekle.Parameters.AddWithValue("@a3", comboBox1.Text);
                    ekle.Parameters.AddWithValue("@a4", dateTimePicker1.Value.ToString("yyyy-mm-dd"));
                    ekle.Parameters.AddWithValue("@a5", "Raporlu");
                    ekle.Parameters.AddWithValue("@a6", 4);
                    ekle.Parameters.AddWithValue("@a7", comboBox3.Text);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
        }

        private void ogrenciYoklamaGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void bugünGelmeyenlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool deg = false;
            string filtre = "Select Ogrenci.SozNo, Ogrenci.OkulNo, Ogrenci.Adi, Ogrenci.Soyadi, Ogrenci.Devre, Ogrenci.Program, Ogrenci.Snf, Veli.AdSoyad, Veli.CepTel, Devamsizlik.Tur" +
                " From Ogrenci Inner join Devamsizlik On Ogrenci.ID=Devamsizlik.OgrID Inner Join Veli on  Ogrenci.ID=Veli.OgrID where ";
            deg = true;
            if (cmbSubeB.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Sube = '" + subeid[cmbSubeB.SelectedIndex] +"'";
                deg = true;
            }
            if (cmbDevreB.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Devre ='" + cmbDevreB.Text + "'";
                deg = true;
            }
            if (cmbSnfB.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Snf ='" + cmbSnfB.Text + "'";
                deg = true;
            }
            if (cmbYTur.SelectedIndex != -1)
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Devamsizlik.Tur='" + cmbYTur.Text + "'";
                deg = true;
            }
            if (!string.IsNullOrEmpty(txtAdB.Text))
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Adi='" + txtAdB.Text.TrimEnd() + "'";
                deg = true;
            }
            if (!string.IsNullOrEmpty(txtSoyadB.Text))
            {
                if (deg)
                {
                    filtre += " AND ";
                }
                filtre += " Ogrenci.Soyadi='" + txtSoyadB.Text.TrimEnd() + "'";
                deg = true;
            }
            filtre += " AND Tarih='" + DateTime.Now.ToString("yyyy-mm-dd") + "'";
            query = filtre;
            panel7.Visible = false;
            bgnGlmynDoldur();
            query = null;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void yemekYoklamaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel4.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex != -1 && comboBox6.SelectedIndex != -1 && comboBox8.SelectedIndex != -1)
            {
                baglan.Open();
                da = new SqlDataAdapter("SELECT Id, OkulNo, Adi, Soyadi From Ogrenci Where Sube='" + subeid[comboBox8.SelectedIndex] + "' And Snf='" + comboBox5.Text+"'", baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                baglan.Close();
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                dataGridView2.Columns.Add(chk);
                chk.HeaderText = "Gelmedi";
                chk.Name = "chk";
            }
            else
            {
                MessageBox.Show("Tüm Alanları Seçin");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<string> selectedGelmedi = new List<string>();
            DataGridViewRow drow = new DataGridViewRow();
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                drow = dataGridView2.Rows[i];
                if (Convert.ToBoolean(drow.Cells["chk"].Value) == true) //checkbox seçiliyse 
                {
                    string id = drow.Cells["ID"].Value.ToString();
                    selectedGelmedi.Add(id); //seçiliyse listeye ekle
                }
            }
            if (selectedGelmedi.Count != 0)
            {
                if (baglan.State != System.Data.ConnectionState.Open)
                    baglan.Open();
                SqlCommand ekle = new SqlCommand("INSERT INTO YemekYoklama (OgrId, Snf, Ogun, Tarih, Tur, TurId) VALUES(@a1, @a2, @a3, @a4, @a5, @a6)", baglan);

                foreach (string a in selectedGelmedi)
                {
                    ekle.Parameters.AddWithValue("@a1", a);
                    ekle.Parameters.AddWithValue("@a2", comboBox5.Text);
                    ekle.Parameters.AddWithValue("@a3", comboBox6.Text);
                    ekle.Parameters.AddWithValue("@a4", dateTimePicker2.Value.ToString("yyyy-mm-dd"));
                    ekle.Parameters.AddWithValue("@a5", "Gelmedi");
                    ekle.Parameters.AddWithValue("@a6", 1);
                }
                ekle.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Eklendi.");
            }
        }

        private void idariİzinGirişToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel9.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel9.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (baglan.State != System.Data.ConnectionState.Open)
                baglan.Open();
            string ogrID=null;
            string ogrSnf = null;
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                SqlCommand com = new SqlCommand("SELECT*FROM Ogrenci WHERE TCKN='" + textBox1.Text + "'");
                com.Connection = baglan;
                SqlDataReader dr = com.ExecuteReader();
                while (dr.Read())
                {
                    ogrID = dr["ID"].ToString();
                    ogrSnf = dr["Snf"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(ogrID))
            {
                SqlCommand ekle = new SqlCommand("INSERT INTO Devamsizlik (OgrId, Ders, Snf, Tarih, Tur, TurId, Saat) VALUES(@a1, @a2, @a3, @a4, @a5, @a6, @a7)", baglan);
                ekle.Parameters.AddWithValue("@a1", ogrID);
                ekle.Parameters.AddWithValue("@a2", DBNull.Value);
                ekle.Parameters.AddWithValue("@a3", ogrSnf);
                ekle.Parameters.AddWithValue("@a4", dateTimePicker3.Value.ToString("yyyy-mm-dd"));
                ekle.Parameters.AddWithValue("@a5", "İzinli");
                ekle.Parameters.AddWithValue("@a6", 3);
                ekle.Parameters.AddWithValue("@a7", comboBox9.Text);

                ekle.ExecuteNonQuery();
                MessageBox.Show("Kayıt Eklendi");
            }
            else
            {
                MessageBox.Show("Öğrenci Kaydı Bulunamadı.");
            }
            baglan.Close();
        }
        SqlCommand komut;
        int okulid = Form1.okulid;

        private void OgrenciYoklama_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox1.Items.Add(oku2[0].ToString());
                comboBox5.Items.Add(oku2[0].ToString());

                cmbSnf.Items.Add(oku2[0].ToString());
                cmbSnfB.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            
            komut = new SqlCommand("Select DersAd From Ders", baglan);
            baglan.Open();
            SqlDataReader oku3= komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox2.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID From Sube where Okulid = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku4 = komut.ExecuteReader();
            while (oku4.Read())
            {
                comboBox4.Items.Add(oku4[0].ToString());
                comboBox8.Items.Add(oku4[0].ToString());

                cmbSubeB.Items.Add(oku4[0].ToString());

                subeid.Add((int)oku4[1]);


            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbDevre.Items.Add(oku[0].ToString());
                cmbDevreB.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku8 = komut.ExecuteReader();
            while (oku8.Read())
            {
                cmbProgramB.Items.Add(oku8[0].ToString());


            }
            baglan.Close();
            baglan.Open();
            da = new SqlDataAdapter("Select*from Sube where okulid='" + okulid + "'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            cmbSube.ValueMember = "ID";
            cmbSube.DisplayMember = "ŞubeAdi";
            cmbSube.DataSource = dt;

            baglan.Close();
            cmbYTur.Items.Add("Geç Kaldı");
            cmbYTur.Items.Add("Gelmedi");
            cmbYTur.Items.Add("Raporlu");
            cmbYTur.Items.Add("İzinli");
            cmbTurB.Items.Add("Geç Kaldı");
            cmbTurB.Items.Add("Gelmedi");
            cmbTurB.Items.Add("Raporlu");
            cmbTurB.Items.Add("İzinli");
          
        }

        private void comboBox4_Click(object sender, EventArgs e)
        {
          
        }

        private void comboBox8_Click(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            komut = new SqlCommand("Select Öğün from Öğün where Sube='" + subeid[comboBox8.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox6.Items.Add(oku[0].ToString());


            }
            baglan.Close();
        }
    }
}
