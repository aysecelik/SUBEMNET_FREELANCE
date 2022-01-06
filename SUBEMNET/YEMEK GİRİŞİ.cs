using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBEMNET
{
    public partial class YEMEK_GİRİŞİ : Form
    {
        public YEMEK_GİRİŞİ()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox4.Text) == true || string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox1.Text) == true )
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Yemek (Sube, Öğün,Yemek,Tarih,Gün,ayyıl) values (@p1, @p2, @p3, @p4,@p5, @p6)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", comboBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p5", dateTimePicker7.Value.ToString("dddd"));
                    komutkaydet.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year +" "+ dateTimePicker7.Value.ToString("MMMM"));

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    var thisWeekStart = dateTimePicker7.Value.AddDays(-(int)(dateTimePicker7.Value.DayOfWeek - 1));
                    var thisWeekEnd = thisWeekStart.AddDays(6).AddSeconds(-1);
                    komut = new SqlCommand("Select * from YemekListesi where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy")+"-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                    baglan.Open();
                MessageBox.Show("GELDİ");

                bool bayrak = false;
                    SqlDataReader oku3 = komut.ExecuteReader();
                    while (oku3.Read())
                    {
                        if (oku3[9] == DBNull.Value)
                        {
                            bayrak = false;

                        }
                        else
                        {
                            bayrak = true;
                        }
                    }
                    baglan.Close();
                    string yemek="";
                MessageBox.Show(bayrak.ToString());
                    if (bayrak == true)
                    {
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PAZARTESİ")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set PAZARTESİ=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();

                    }
                    if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "SALI")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set SALI=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "ÇARŞAMBA")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set ÇARŞAMBA=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PERŞEMBE")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set PERŞEMBE=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "CUMA")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set CUMA=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "CUMARTESİ")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set CUMARTESİ=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PAZAR")
                        {
                            for (int i = 0; i < dataGridView1.RowCount-1; i++)
                            {
                                yemek += dataGridView1.Rows[i].Cells[3].Value.ToString();
                                yemek += Environment.NewLine;

                            }
                            baglan.Open();
                            SqlCommand komutgüncelle = new SqlCommand("update YemekListesi set PAZAR=@p1 where TARİH='" + thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy") + "'", baglan);
                            komutgüncelle.Parameters.AddWithValue("@p1", yemek);
                            komutgüncelle.ExecuteNonQuery();
                            baglan.Close();
                        }

                    }
                    else
                    {
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PAZARTESİ")
                        {
                        MessageBox.Show("GELDİ");

                        baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,PAZARTESİ,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "SALI")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,SALI,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "ÇARŞAMBA")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,ÇARŞAMBA,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PERŞEMBE")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,PERŞEMBE,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "CUMA")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,CUMA,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "CUMARTESİ")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,CUMARTESİ,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                        if (dateTimePicker7.Value.ToString("dddd").ToUpper() == "PAZAR")
                        {
                            baglan.Open();
                            SqlCommand komutkaydet2 = new SqlCommand("insert into YemekListesi (Sube, Öğün,PAZAR,TARİH,ayyıl) values (@p1, @p2, @p3, @p4, @p6)", baglan);
                            komutkaydet2.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                            komutkaydet2.Parameters.AddWithValue("@p2", comboBox3.Text);
                            komutkaydet2.Parameters.AddWithValue("@p3", comboBox1.Text);
                            komutkaydet2.Parameters.AddWithValue("@p4", thisWeekStart.ToString("dd/MM/yyyy") + "-" + thisWeekEnd.ToString("dd/MM/yyyy"));
                            komutkaydet2.Parameters.AddWithValue("@p6", dateTimePicker7.Value.Year + " " + dateTimePicker7.Value.ToString("MMMM"));

                            komutkaydet2.ExecuteNonQuery();
                            baglan.Close();
                        }
                    }
                    comboBox1.Text = "";

            }
                catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA." + a.ToString());
            }
        }
    }
        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select ş.ŞubeAdi,t.ID,t.Öğün, t.Yemek,t.Tarih from Yemek t join Sube ş on ş.ID=t.Sube where ş.ID='" + subeid[comboBox4.SelectedIndex] + "' and t.Öğün='"+comboBox3.Text+"' and t.Tarih='"+ dateTimePicker7.Value.ToString("yyyyMMdd") + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "YEMEKLER");
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
        SqlCommand komut;
        private void YEMEK_GİRİŞİ_Load(object sender, EventArgs e)
        {
            comboBox4.Text = dateTimePicker7.Value.ToString("dddd").ToUpper();
            comboBox1.Items.Add("ÖNCELİKLE ŞUBE VE ÖĞÜN SEÇİLMELİDİR.");
            comboBox3.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");
            comboBox5.Items.Add("ÖNCELİKLE ŞUBE SEÇİLMELİDİR.");


            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {

                comboBox4.Items.Add(oku[0].ToString());
                comboBox2.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox3.Text) == true || string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN ÖNCELİKLE ÖĞÜN VE ŞUBE SEÇİNİZ.");
            }
            else
            {
                panel5.Visible = true;
                panel1.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                MessageBox.Show("LÜTFEN ÖNCELİKLE ŞUBE SEÇİNİZ.");
            }
            else
            {
                panel5.Visible = true;
                panel1.Visible = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            textBox6.Visible = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox6.Text) == true )
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Yemekler (Sube, Öğün,Yemek) values (@p1, @p2, @p3)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", comboBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p3", textBox6.Text);
                
                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox6.Text = "";
                    panel5.Visible = false;
                    comboBox1.Items.Clear();
                    komut = new SqlCommand("Select Yemek from Yemekler where Sube='" + subeid[comboBox4.SelectedIndex] + "' and Öğün='"+comboBox3.Text+"'", baglan);
                    baglan.Open();
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        comboBox1.Items.Add(oku[0].ToString());


                    }
                    baglan.Close();
                }
                catch (Exception a)
                {

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            panel5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Öğün (Sube, Öğün) values (@p1, @p2)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", subeid[comboBox4.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p2", textBox1.Text);

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    textBox1.Text = "";
                    panel5.Visible = false;
                    comboBox3.Items.Clear();
                    komut = new SqlCommand("Select Öğün from Öğün where Sube='" + subeid[comboBox4.SelectedIndex] + "'", baglan);
                    baglan.Open();
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        comboBox3.Items.Add(oku[0].ToString());


                    }
                    baglan.Close();

                }
                catch (Exception a)
                {

                    MessageBox.Show("HATA." + a.ToString());
                }
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();
            komut = new SqlCommand("Select Öğün from Öğün where Sube='" + subeid[comboBox4.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());


            }
            baglan.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox4.Text) == true)
            {
                comboBox1.Items.Add("ÖNCELİKLE ŞUBE VE ÖĞÜN SEÇİLMELİDİR.");
            }
            else
            {
                comboBox1.Items.Clear();
                komut = new SqlCommand("Select Yemek from Yemekler where Sube='" + subeid[comboBox4.SelectedIndex] + "' and Öğün='" + comboBox3.Text + "'", baglan);
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    comboBox1.Items.Add(oku[0].ToString());


                }
                baglan.Close();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "YEMEK SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[1].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + " İSİMLİ YEMEĞİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ?", "YEMEK SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[1].Value.ToString());
                            string sql = "DELETE  FROM Yemek WHERE ID=@id";
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

        private void button6_Click(object sender, EventArgs e)
        {
            if(comboBox2.Text=="ŞUBE" || comboBox5.Text=="ÖĞÜN" || comboBox5.Text== "ÖNCELİKLE ŞUBE SEÇİLMELİDİR." || string.IsNullOrEmpty(comboBox2.Text)==true || string.IsNullOrEmpty(comboBox5.Text)==true)
            {
                MessageBox.Show("LÜTFEN GÖRÜNTÜLEMEK İSTEDİĞİNİZ YEMEK LİSTESİNİN ÖĞÜN VE ŞUBE BİLGİLERİNİ GİRİNİZ.");               
            }
            else
            {
                comboBox4.Text = comboBox2.Text;
                comboBox4.SelectedIndex = comboBox2.SelectedIndex;
                comboBox3.Text = comboBox5.Text;
                dateTimePicker7.Value = dateTimePicker1.Value;
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter("Select ş.ŞubeAdi,t.ID,t.Öğün, t.Yemek,t.Tarih from Yemek t join Sube ş on ş.ID=t.Sube where ş.ID='" + subeid[comboBox2.SelectedIndex] + "' and t.Öğün='" + comboBox5.Text + "' and t.Tarih='" + dateTimePicker1.Value.ToString("yyyyMMdd") + "'", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "YEMEKLER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();

                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "SİL";
                dgvBtn2.Text = "SİL";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                comboBox2.Text = "ŞUBE";
                comboBox5.Text = "ÖĞÜN";
                dateTimePicker1.Value = DateTime.Now;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox5.Items.Clear();
            komut = new SqlCommand("Select Öğün from Öğün where Sube='" + subeid[comboBox2.SelectedIndex] + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox5.Items.Add(oku[0].ToString());


            }
            baglan.Close();
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {


        }

       
    }
}
