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
    public partial class HAZIRSMS : Form
    {
        public HAZIRSMS()
        {
            InitializeComponent();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }
        public string query;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        List<int> subeid = new List<int>();
        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == false)
            {
                try
                {
                    bool degisken = true;
                    string filtre = "Select ö.ID,ş.ŞubeAdi ŞUBE, ö.Devre 'DEVRE',  ö.Snf as SINIF , ö.Adi+' '+ö.Soyadi as ÖĞRENCİ,v.AdSoyad 'VELİ',v.CepTel from Ogrenci ö join Veli v on v.OgrId=ö.ID join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "' and v.VeliMi=1";

                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ş.ŞubeAdi='" + comboBox5.Text + "'";
                    degisken = true;

                    if (cmbKur.SelectedIndex != -1)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Kur='" + cmbKur.Text + "'";
                        degisken = true;
                    }
                    if (cmbProgram.SelectedIndex != -1)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Program='" + cmbProgram.Text + "'";
                        degisken = true;
                    }
                    if (comboBox1.SelectedIndex != -1)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Devre='" + comboBox1.Text + "'";
                        degisken = true;
                    }
                    if (comboBox4.SelectedIndex != -1)
                    {
                        if (degisken == true)
                        {
                            filtre += " and ";
                        }
                        filtre += " ö.Snf='" + comboBox4.Text + "'";
                        degisken = true;
                    }
                    if (cmbCinsiyet.SelectedIndex != -1)
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.Cinsiyet='" + cmbCinsiyet.Text + "'";
                        degisken = true;
                    }
                    if (!string.IsNullOrEmpty(txtAd.Text))
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.Adi='" + txtAd.Text.TrimEnd() + "'";
                        degisken = true;
                    }
                    if (!string.IsNullOrEmpty(txtSoyad.Text))
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.Soyadi='" + txtSoyad.Text.TrimEnd() + "'";
                        degisken = true;
                    }
                    if (!string.IsNullOrEmpty(txtSozno.Text))
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.SozNo='" + txtSozno.Text.TrimEnd() + "'";
                        degisken = true;
                    }
                    if (!string.IsNullOrEmpty(txtTc.Text))
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.TCKN='" + txtTc.Text.TrimEnd() + "'";
                        degisken = true;
                    }
                    if (!string.IsNullOrEmpty(txtOkulno.Text))
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.OkulNo='" + txtOkulno.Text.TrimEnd() + "'";
                        degisken = true;
                    }
                    if (radioButton4.Checked)
                    {
                        if (degisken == true)
                        {
                            filtre += " and";
                        }
                        filtre += " ö.Durum=1";
                        degisken = true;
                    }

                    query = filtre;

                    dataGridView3.Columns.Clear();
                    baglan.Open();
                    if (query != null)
                        da = new SqlDataAdapter(query, baglan);

                    cmdb = new SqlCommandBuilder(da);
                    ds = new DataSet();
                    da.Fill(ds, "ÖĞRENCİ");
                    dataGridView3.DataSource = ds.Tables[0];
                    baglan.Close();
                    DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                    dgvBtn.HeaderText = "SEÇ";
                    dgvBtn.Text = "SEÇ";
                    dgvBtn.UseColumnTextForButtonValue = true;
                    dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn.Width = 70;
                    dataGridView3.Columns.Add(dgvBtn);
                    DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                    dgvBtn2.HeaderText = "ÇIKAR";
                    dgvBtn2.Text = "ÇIKAR";
                    dgvBtn2.UseColumnTextForButtonValue = true;
                    dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                    dgvBtn2.Width = 70;
                    dataGridView3.Columns.Add(dgvBtn2);
                    baglan.Close();
                    query = null;
                    panel7.Visible = false;
                }
                catch (Exception a)
                {
                    baglan.Close();
                    MessageBox.Show("HATA");
                }
            }
            else
                MessageBox.Show("LÜTFEN ŞUBE SEÇİMİ YAPINIZ.");
        }
        SqlCommand komut;
        private void HAZIRSMS_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {      
                comboBox5.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku8 = komut.ExecuteReader();
            while (oku8.Read())
            {
                comboBox1.Items.Add(oku8[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox4.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbProgram.Items.Add(oku3[0].ToString());

            }
            baglan.Close();
          
       
        }



        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }
        List<int> secilenler = new List<int>();

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text) == false)
            {
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter("Select b.Başlık BAŞLIK,b.Metin 'SMS METNİ' From Sube ş join HazırSMS b on ş.ID=b.Sube where ş.Okulİd='" + okulid + "' and ş.ID='" + subeid[comboBox5.SelectedIndex] + "'", baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "HAZIRSMS");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                if (dataGridView3.CurrentCell.ColumnIndex == dataGridView3.ColumnCount - 2)
                {

                    if (dataGridView3.CurrentCell.RowIndex != dataGridView3.RowCount - 1)
                    {
                        int secilen = dataGridView3.CurrentCell.RowIndex;
                        secilenler.Add(secilen);
                    }
                    else
                    {
                        int secilen = dataGridView3.CurrentCell.RowIndex;
                        secilenler.Remove(secilen);
                        dataGridView3.Rows[secilen].Selected = false;
                    }

                }
                if (dataGridView3.CurrentCell.ColumnIndex == dataGridView3.ColumnCount - 1)
                {

                    int secilen = dataGridView3.CurrentCell.RowIndex;
                    secilenler.Remove(secilen);
                    dataGridView3.Rows[secilen].Selected = false;

                }
                for (int i = 0; i < secilenler.Count; i++)
                {
                    dataGridView3.Rows[secilenler[i]].Selected = true;

                }
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.ColumnIndex == dataGridView2.ColumnCount-1)
            {

                int secilen = dataGridView2.CurrentCell.RowIndex;
                DataGridViewRow dgvDelRow = dataGridView2.Rows[secilen];
                dataGridView2.Rows.Remove(dgvDelRow);

            }
            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("ŞUBE", "ŞUBE");
            dataGridView2.Columns.Add("DEVRE", "DEVRE");
            dataGridView2.Columns.Add("SINIF", "SINIF");
            dataGridView2.Columns.Add("ÖĞRENCİ", "ÖĞRENCİ");
            dataGridView2.Columns.Add("VELİ", "VELİ");
            dataGridView2.Columns.Add("CEP TEL", "CEP TEL");
            DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
            dgvBtn.HeaderText = "ÇIKAR";
            dgvBtn.Text = "ÇIKAR";
            dgvBtn.UseColumnTextForButtonValue = true;
            dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
            dgvBtn.Width = 70;
            dataGridView2.Columns.Add(dgvBtn);


            for (int i = 0; i < dataGridView3.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = dataGridView3.SelectedRows[i].Cells[0].Value.ToString();
                dataGridView2.Rows[index].Cells[1].Value = dataGridView3.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = dataGridView3.SelectedRows[i].Cells[2].Value.ToString();
                dataGridView2.Rows[index].Cells[3].Value = dataGridView3.SelectedRows[i].Cells[3].Value.ToString();
                dataGridView2.Rows[index].Cells[4].Value = dataGridView3.SelectedRows[i].Cells[4].Value.ToString();
                dataGridView2.Rows[index].Cells[5].Value = dataGridView3.SelectedRows[i].Cells[5].Value.ToString();
                dataGridView2.Rows[index].Cells[6].Value = dataGridView3.SelectedRows[i].Cells[6].Value.ToString();


            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentCell.ColumnIndex == dataGridView3.ColumnCount - 1)
            {
                richTextBox1.Text = dataGridView3.Rows[dataGridView3.CurrentCell.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string numaralar = "";
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (i == dataGridView2.RowCount - 2)
                {
                    numaralar += dataGridView2.Rows[i].Cells[6].Value.ToString().Replace(" ", "");
                }
                else
                    numaralar += dataGridView2.Rows[i].Cells[6].Value.ToString().Replace(" ", "") + ",";
            }

            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                try
                {
                    string url = "https://api.iletimerkezi.com/v1/send-sms/get/?" +
                        "username=" + System.Web.HttpUtility.UrlEncode(textBox1.Text) + "&" +
                        "password=" + System.Web.HttpUtility.UrlEncode(textBox2.Text) + "&" +
                        "text=" + richTextBox1.Text +
                        "&receipents=" + numaralar + "&" +
                        "sender=" + textBox3.Text;
                    ;
                    string result = client.DownloadString(url);

                    MessageBox.Show("Mesajınız Gönderildi.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                    richTextBox1.Text = "";
                    secilenler.Clear();
                    dataGridView1.ClearSelection();
                    dataGridView3.ClearSelection();

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
