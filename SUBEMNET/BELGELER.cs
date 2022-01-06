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
    public partial class BELGELER : Form
    {
        public BELGELER()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();

        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();



        void griddoldur()
        {
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tema 'TEMA', t.BelgeAdi 'BELGE ADI',t.Tarih 'TARİH',t.belgesi BELGE,t.Açıklama AÇIKLAMA from Belgeler t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "BELGELER");
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
        private string path;

        private void button2_Click(object sender, EventArgs e)
        {
            //ARMA 
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EKL
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox3.Text = "";
            textBox4.Text = "";
            dateTimePicker3.Value = DateTime.Now.Date;
            dateTimePicker4.Value = DateTime.Now.Date;
            checkBox1.Checked = false;
            panel2.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tema 'TEMA', t.BelgeAdi 'BELGE ADI',t.Tarih 'TARİH',t.belgesi BELGE,t.Açıklama AÇIKLAMA from Belgeler t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'";
            
                if (string.IsNullOrEmpty(textBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.BelgeAdi ='" + textBox4.Text+ "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Tema ='" + comboBox3.Text + "'";
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


                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " t.Sube = " + subeid[comboBox1.SelectedIndex];
                    degisken = true;
                }




                if (string.IsNullOrEmpty(textBox3.Text) && string.IsNullOrEmpty(comboBox1.Text) && string.IsNullOrEmpty(textBox4.Text) && checkBox1.Checked == false)
                {
                    filtre = "Select t.ID,ş.ŞubeAdi 'ŞUBE',t.Tema 'TEMA', t.BelgeAdi 'BELGE ADI',t.Tarih 'TARİH',t.belgesi BELGE,t.Açıklama AÇIKLAMA from Belgeler t join Sube ş on ş.ID=t.Sube where ş.Okulid='" + okulid + "'";
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "BELGELER");
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
                textBox4.Text = "";
                dateTimePicker3.Value = DateTime.Now;
                dateTimePicker4.Value = DateTime.Now;
                comboBox1.Text = "";
                comboBox3.Text = "";
                checkBox1.Checked = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

        private void BELGELER_Load(object sender, EventArgs e)
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
            komut = new SqlCommand("Select TemaAdı from Temalar where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {

                comboBox3.Items.Add(oku2[0].ToString());
                comboBox2.Items.Add(oku2[0].ToString());


            }
            baglan.Close();

            griddoldur();
            dateTimePicker3.Format = DateTimePickerFormat.Custom;
            dateTimePicker3.CustomFormat = "dd/MM/yyyy";
            dateTimePicker4.Format = DateTimePickerFormat.Custom;
            dateTimePicker4.CustomFormat = "dd/MM/yyyy";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox6.Text = "";
            panel5.Visible = false;

        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand("insert into Temalar (TemaAdı,Okulid) values (@p1,@p2)", baglan);
                komutkaydet.Parameters.AddWithValue("@p1", textBox6.Text);
                komutkaydet.Parameters.AddWithValue("@p2", okulid);
                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                textBox6.Text = "";
                panel5.Visible = false;
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA." + a.ToString());
            }
            comboBox3.Items.Clear();
            comboBox2.Items.Clear();

            komut = new SqlCommand("Select TemaAdı from Temalar where Okulid='"+okulid+"'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox3.Items.Add(oku[0].ToString());
                comboBox2.Items.Add(oku[0].ToString());


            }
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel5.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            comboBox5.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            richTextBox1.Text = "";
            dateTimePicker7.Value = DateTime.Now.Date;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(comboBox5.Text)==true || string.IsNullOrEmpty(comboBox2.Text) == true || string.IsNullOrEmpty(textBox2.Text) == true || string.IsNullOrEmpty(textBox3.Text) == true || string.IsNullOrEmpty(richTextBox1.Text) == true)
            {
                MessageBox.Show("LÜTFEN TÜM BİLGİLERİ DOLDURUNUZ.");
            }
            else
            {
                try
                {
                    baglan.Open();
                    SqlCommand komutkaydet = new SqlCommand("insert into Belgeler (BelgeAdi, Tarih,Açıklama, Tema, Sube,belgesi,Belge) values (@p1, @p2, @p3, @p4,@p5, @p6,@p7)", baglan);
                    komutkaydet.Parameters.AddWithValue("@p1", textBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p2", dateTimePicker7.Value.ToString("yyyyMMdd"));
                    komutkaydet.Parameters.AddWithValue("@p3", richTextBox1.Text);
                    komutkaydet.Parameters.AddWithValue("@p4", comboBox2.Text);
                    komutkaydet.Parameters.AddWithValue("@p5", subeid[comboBox5.SelectedIndex]);
                    komutkaydet.Parameters.AddWithValue("@p6", textBox3.Text);
                    komutkaydet.Parameters.AddWithValue("@p7", SqlDbType.VarBinary).Value = bytes;

                    komutkaydet.ExecuteNonQuery();
                    baglan.Close();
                    MessageBox.Show("Kayıt Başarılı");
                    griddoldur();
                    panel2.Visible = false;
                }
                catch (Exception a)
                {
                 
                        MessageBox.Show("HATA." + a.ToString());
                }
            }
        }
        byte[] bytes;
        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    path = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/pdf";
            if (path != null)
            {
                if (contenttype != String.Empty)
                {
                    Stream fs = File.OpenRead(filePath);
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                    FileInfo fileinfo = new FileInfo(open.FileName);
                    textBox3.Text = fileinfo.Name;

                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
            {
                int secilen;
                secilen = dataGridView1.SelectedCells[0].RowIndex;
                FileInfo fileInfo = new FileInfo(dataGridView1.Rows[secilen].Cells[7].ToString());
                string fileExtension = fileInfo.Extension;
                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Filter = "Files (*" + fileExtension + ")|*" + fileExtension;
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = dataGridView1.Rows[secilen].Cells[0].Value.ToString() + dataGridView1.Rows[secilen].Cells[2].Value.ToString() + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + ".pdf";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        baglan.Open();
                        komut = new SqlCommand("Select Belge from Belgeler where ID = '" + dataGridView1.Rows[secilen].Cells[0].Value.ToString() + "'", baglan);
                        SqlDataReader oku = komut.ExecuteReader();
                        oku.Read();
                        byteData = (byte[])oku[0];
                        File.WriteAllBytes(savefile.FileName, byteData);
                        baglan.Close();
                    }
                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 7)
            {
                DialogResult result = MessageBox.Show("SİLME İŞLEMİ HİÇ BİR ŞEKİLDE GERİ ALINAMAMAKTADIR. DEVAM ETMEK İSTİYOR MUSUNUZ?", "BELGE SİLME", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    DialogResult dialogResult = MessageBox.Show(dataGridView1.Rows[secilen].Cells[0].Value.ToString() + " ID numarasına sahip " + dataGridView1.Rows[secilen].Cells[3].Value.ToString() + " İSİMLİ BELGEYİ TAMAMEN SİLMEK İSTEDİĞİNİZE EMİN MİSİNİZ^?", "İŞ BAŞVURU SİLME", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            int id = Convert.ToInt32(dataGridView1.Rows[secilen].Cells[0].Value.ToString());
                            string sql = "DELETE  FROM Belgeler WHERE ID=@id";
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
