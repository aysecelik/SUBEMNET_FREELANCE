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
    public partial class AYARLAR : Form
    {
        public AYARLAR()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        SqlCommand komut;
        Form1 Form1 = new Form1();
        int okulid = Form1.okulid;
        void SubeDoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ID,ŞubeAdi 'ŞUBELER' From Sube where Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "ŞUBELER");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "SİL";
            dgvBtn3.Text = "SİL";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView1.Columns.Add(dgvBtn3);
        }
        void BankaDoldur()
        {
            dataGridView2.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select b.ID,ş.ŞubeAdi 'ŞUBE',b.BankaHesabı 'BANKA HESABI',b.IBAN,b.HESAPNO 'HESAP NO' From Sube ş join BankaHesabı b on ş.ID=b.Sube where ş.Okulİd='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BANKAHESAPLARI");
            dataGridView2.DataSource = ds.Tables[0];
            baglan.Close();
            DataGridViewButtonColumn dgvBtn3 = new DataGridViewButtonColumn();
            dgvBtn3.HeaderText = "SİL";
            dgvBtn3.Text = "SİL";
            dgvBtn3.UseColumnTextForButtonValue = true;
            dgvBtn3.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn3.Width = 70;
            dataGridView2.Columns.Add(dgvBtn3);
        }
        void ProgramDoldur()
        {
            dataGridView4.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi 'ŞUBE',b.PROGRAM From Sube ş join Programlar b on ş.ID=b.Sube where ş.Okulİd='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "PROGRAMLAR");
            dataGridView4.DataSource = ds.Tables[0];
            baglan.Close();
           
        }
        void DevreDoldur()
        {
            dataGridView5.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi 'ŞUBE',b.DEVRE From Sube ş join Devreler b on ş.ID=b.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "DEVRELER");
            dataGridView5.DataSource = ds.Tables[0];
            baglan.Close();

        }
        void SınıfDoldur()
        {
            dataGridView6.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi 'ŞUBE',b.SINIF SINIFLAR From Sube ş join Sınıflar b on ş.ID=b.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "SINIFLAR");
            dataGridView6.DataSource = ds.Tables[0];
            baglan.Close();

        }
        void HazırSMSDoldur()
        {
            dataGridView7.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi 'ŞUBE',b.Başlık BAŞLIK,b.Metin 'SMS METNİ' From Sube ş join HazırSMS b on ş.ID=b.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "HAZIRSMS");
            dataGridView7.DataSource = ds.Tables[0];
            baglan.Close();

        }
        void dersdoldur()
        {
            dataGridView3.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select Id,DersAd 'DERSLER' from Ders", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "DERSLER");
            dataGridView3.DataSource = ds.Tables[0];
            baglan.Close();
          
        }
        List<int> subeid = new List<int>();
       
        private void AYARLAR_Load(object sender, EventArgs e)

        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox1.Items.Add(oku[0].ToString());
                comboBox2.Items.Add(oku[0].ToString());
                comboBox3.Items.Add(oku[0].ToString());
                comboBox4.Items.Add(oku[0].ToString());
                comboBox5.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            try
            {
                SubeDoldur();
                BankaDoldur();
                subeid.Clear();
                DevreDoldur();
                ProgramDoldur();
                SınıfDoldur();
                dersdoldur();
                HazırSMSDoldur();
              
            }
            catch(Exception a)
            {
                MessageBox.Show(a.ToString());
                baglan.Close()
                    ;
            }
          
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text) == false)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Sube (ŞubeAdi,Okulid) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", okulid);

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    SubeDoldur();
                }
                catch { baglan.Close(); }


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount-1)
            {
                MessageBox.Show("ŞUBE SİLME İŞLEMİ GERÇEKLEŞTİĞİNDE O ŞUBEYE AİT HİÇ BİR VERİYE ULAŞAMAYACAĞINIZI BELİRTMEK İSTERİZ.", "ŞUBE SİLME", MessageBoxButtons.OK);
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "ŞUBE SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " İSİMLİ ŞUBEYİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "ŞUBE SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[1].Value.ToString());
                            string sql = "DELETE  FROM Sube WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", id);
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            SubeDoldur();
                        }
                        catch (Exception)
                        {
                            baglan.Close();
                        }

                    }
                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == dataGridView2.ColumnCount - 1)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "BANKA HESABI SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView2.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView2.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView2.Rows[secilen].Cells[1].Value.ToString() + " İSİMLİ BANKA HESABINI TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "ŞUBE SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView2.Rows[secilen].Cells[1].Value.ToString());
                            string sql = "DELETE  FROM BankaHesabı WHERE ID=@id";
                            komut = new SqlCommand(sql, baglan);
                            komut.Parameters.AddWithValue("@id", id);
                            baglan.Open();
                            komut.ExecuteNonQuery();
                            baglan.Close();
                            MessageBox.Show("İŞLEM BAŞARILI");
                            SubeDoldur();
                        }
                        catch (Exception)
                        {
                            baglan.Close();
                        }

                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox4.Text) == false && string.IsNullOrEmpty(textBox1.Text) == false && string.IsNullOrEmpty(comboBox1.Text) == false && maskedTextBox2.MaskFull==true )
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into BankaHesabı (Sube,BankaHesabı,IBAN,HESAPNO) values (@p1, @p2,@p3, @p4)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox1.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox4.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", maskedTextBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", textBox1.Text);



                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                   BankaDoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text) == false)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Ders (DersAd) values (@p1)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text);

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    dersdoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox5.Text) == false &&  string.IsNullOrEmpty(comboBox2.Text) == false )
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Programlar (Sube,PROGRAM) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox2.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox5.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    ProgramDoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == false && string.IsNullOrEmpty(comboBox3.Text) == false)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Devreler (Sube,DEVRE) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox3.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox6.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    DevreDoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text) == false && string.IsNullOrEmpty(comboBox4.Text) == false)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Sınıflar (Sube,SINIF) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox7.Text);
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    SınıfDoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox8.Text) == false && string.IsNullOrEmpty(comboBox5.Text) == false)
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into HazırSMS (Sube,Başlık,Metin) values (@p1, @p2, @p3)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox9.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", textBox8.Text);

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    HazırSMSDoldur();
                }
                catch
                {
                    baglan.Close();
                }
            }
        }
    }
}
