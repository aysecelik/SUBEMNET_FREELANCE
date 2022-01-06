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
    public partial class ÖNKAYITSMS : Form
    {
        public ÖNKAYITSMS()
        {
            InitializeComponent();
        }


        List<int> secilenler = new List<int>();


        private void button5_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
            secilenler.Clear();
            dataGridView1.ClearSelection();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        private void button4_Click(object sender, EventArgs e)
        {
            string numaralar = "";
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {
                if (i == dataGridView2.RowCount - 2)
                {
                    numaralar += dataGridView2.Rows[i].Cells[5].Value.ToString().Replace(" ", "");
                }
                else
                    numaralar += dataGridView2.Rows[i].Cells[5].Value.ToString().Replace(" ", "") + ",";
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
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message, "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel1.Visible = true;
            dataGridView2.Columns.Add("ID", "ID");
            dataGridView2.Columns.Add("ŞUBE", "ŞUBE");
            dataGridView2.Columns.Add("DEVRE", "DEVRE");
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


            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                int index = dataGridView2.Rows.Add();
                dataGridView2.Rows[index].Cells[0].Value = dataGridView1.SelectedRows[i].Cells[0].Value.ToString();
                dataGridView2.Rows[index].Cells[1].Value = dataGridView1.SelectedRows[i].Cells[1].Value.ToString();
                dataGridView2.Rows[index].Cells[2].Value = dataGridView1.SelectedRows[i].Cells[2].Value.ToString();
                dataGridView2.Rows[index].Cells[3].Value = dataGridView1.SelectedRows[i].Cells[3].Value.ToString();
                dataGridView2.Rows[index].Cells[4].Value = dataGridView1.SelectedRows[i].Cells[4].Value.ToString();
                dataGridView2.Rows[index].Cells[5].Value = dataGridView1.SelectedRows[i].Cells[5].Value.ToString();


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Selected = true;

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }
        List<int> subeid = new List<int>();
        public string query;

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = true;
                string filtre = "Select ö.ID,ş.ŞubeAdi ŞUBE, ö.Devre 'DEVRE',ö.Adi+' '+ö.Soyadi as ÖĞRENCİ,ö.VeliAdSoyad 'VELİ',ö.VeliCepTel from  OnKayit ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='" + okulid + "'";
               
                if (cmbSube.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ş.ID='" + subeid[cmbSube.SelectedIndex]+"'";
                    degisken = true;
                }
                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Devre='" + cmbKur.Text + "'";
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
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Okul='" + textBox4.Text + "'";
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
            
                if (!string.IsNullOrEmpty(txtTc.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and";
                    }
                    filtre += " ö.TCKN='" + txtTc.Text.TrimEnd() + "'";
                    degisken = true;
                }

                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.DogumTarihi between '" + dtKayTarBas.Value.ToString("yyyyMMdd") + "' and '" + dtKayTarBit.Value.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.OlusturmaTarihi between '" + dtSilTarBas.Value.ToString("yyyyMMdd  HH:mm:ss") + "' and '" + dtSilTarBit.Value.ToString("yyyyMMdd  HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel2.Visible = false;
                dataGridView1.Columns.Clear();
                da = new SqlDataAdapter(query, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "ÖĞRENCİLER");
                dataGridView1.DataSource = ds.Tables[0];
                baglan.Close();
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "SEÇ";
                dgvBtn.Text = "SEÇ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
                DataGridViewButtonColumn dgvBtn2 = new DataGridViewButtonColumn();
                dgvBtn2.HeaderText = "ÇIKAR";
                dgvBtn2.Text = "ÇIKAR";
                dgvBtn2.UseColumnTextForButtonValue = true;
                dgvBtn2.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn2.Width = 70;
                dataGridView1.Columns.Add(dgvBtn2);
                baglan.Close();
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount-2)
            {

                if (dataGridView1.CurrentCell.RowIndex != dataGridView1.RowCount - 1)
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    secilenler.Add(secilen);
                }
                else
                {
                    int secilen = dataGridView1.CurrentCell.RowIndex;
                    secilenler.Remove(secilen);
                    dataGridView1.Rows[secilen].Selected = false;
                }

            }
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.ColumnCount - 1)
            {

                int secilen = dataGridView1.CurrentCell.RowIndex;
                secilenler.Remove(secilen);
                dataGridView1.Rows[secilen].Selected = false;

            }
            for (int i = 0; i < secilenler.Count; i++)
            {
                dataGridView1.Rows[secilenler[i]].Selected = true;

            }
        }
        SqlCommand komut;
        private void ÖNKAYITSMS_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                cmbSube.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbKur.Items.Add(oku[0].ToString());

            }
            baglan.Close();
            komut = new SqlCommand("Select b.PROGRAM From Sube ş join Programlar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbProgram.Items.Add(oku2[0].ToString());

            }
            baglan.Close();
        }
    }
}
