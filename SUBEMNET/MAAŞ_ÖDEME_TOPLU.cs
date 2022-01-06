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
    public partial class MAAŞ_ÖDEME_TOPLU : Form
    {
        public MAAŞ_ÖDEME_TOPLU()
        {
            InitializeComponent();
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();
        private void MAAŞ_ÖDEME_TOPLU_Load(object sender, EventArgs e)
        {
            secilenler.Clear();
            dataGridView1.ClearSelection();
            subeid.Clear();
            //dataGridView1.MultiSelect = true;
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            panel2.Visible = false;


            griddoldur();


            dataGridView1.Rows[0].Selected = false;
            komut = new SqlCommand("Select distinct pm.Ayyıl from PersonelMaaş pm join Personeller p on p.ID=pm.Personel join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader ok = komut.ExecuteReader();
            while (ok.Read())
            {
                comboBox4.Items.Add(ok[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select PozisyonAdi from Pozisyonlar", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select Branş from Branşlar", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                comboBox2.Items.Add(oku2[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Sube.Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                comboBox3.Items.Add(oku3[0].ToString());
                subeid.Add((int)oku3[1]);

            }
            baglan.Close();
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
        List<int> ödeme = new List<int>();
        List<int> id = new List<int>();

        List<string> durum = new List<string>();
        List<string> vade = new List<string>();

        List<decimal> artış = new List<decimal>();
        List<decimal> tutar = new List<decimal>();


        void griddoldur()
        {
            ödeme.Clear();
            id.Clear();
            durum.Clear();
            vade.Clear();
            artış.Clear();
            tutar.Clear();
            komut = new SqlCommand("Select p.ID,p.maaştarih,p.MAAŞ,p.maasi from Personeller p join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Aktiflik='" + Convert.ToBoolean(true) + "'", baglan);
            baglan.Open();

            SqlDataReader oku3 = komut.ExecuteReader();
            while (oku3.Read())
            {
                id.Add((int)oku3[0]);
                ödeme.Add((int)oku3[1]);
                tutar.Add((decimal)oku3[3]);
            }
            baglan.Close();
            for (int i = 0; i < id.Count; i++)
            {


                komut = new SqlCommand("Select ID from PersonelMaaş where Personel='" + id[i] + "' and Ayyıl='" + DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM") + "'", baglan);
                bool bayrak = true;
                baglan.Open();
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {

                    bayrak = false;

                }
                baglan.Close();
                MessageBox.Show("1");
                if (bayrak == true)
                {
                    if (ödeme[i] == (int)DateTime.Now.Day)
                    {
                        //EKLMEE
                        try
                        {
                            komut = new SqlCommand("Select tutar,PrimKesinti,Vade from PrimKesinti where Personel='" + id[i] + "' and Vade='" + DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM") + "'", baglan);
                            baglan.Open();
                            SqlDataReader oku2 = komut.ExecuteReader();
                            bool eklemeli = false;
                            while (oku2.Read())
                            {
                                durum.Add(oku2[1].ToString());
                                artış.Add((decimal)oku2[0]);
                                vade.Add(oku2[2].ToString());
                                MessageBox.Show("2-1");
                                eklemeli = true;


                            }
                            baglan.Close();
                            MessageBox.Show("2");
                            if (eklemeli == true)
                                for (int j = 0; j < vade.Count; j++)
                                {


                                    if (vade[j] == DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM"))
                                    {
                                        komut = new SqlCommand("Select ID,tutar from PersonelMaaş where Personel='" + id[i] + "'", baglan);
                                        baglan.Open();
                                        SqlDataReader oku5 = komut.ExecuteReader();
                                        bool update = false;
                                        decimal tutar2 = 0;
                                        while (oku5.Read())
                                        {
                                            update = true;
                                            tutar2 = (decimal)oku5[1];



                                        }
                                        baglan.Close();
                                        if (update == false)
                                        {
                                            if (durum[j] == "PRİM")
                                            {
                                                baglan.Open();
                                                SqlCommand komutkaydet2 = new SqlCommand("insert into PersonelMaaş (Personel,Ayyıl,ÖdemeDurum,MAAŞ,tutar) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                                                komutkaydet2.Parameters.AddWithValue("@p1", id[i]);
                                                komutkaydet2.Parameters.AddWithValue("@p2", DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM"));
                                                komutkaydet2.Parameters.AddWithValue("@p3", Convert.ToBoolean(false));
                                                komutkaydet2.Parameters.AddWithValue("@p4", (string.Format("{0:N}", tutar[i] + artış[j]) + " TL"));
                                                komutkaydet2.Parameters.AddWithValue("@p5", tutar[i] + artış[j]);
                                                komutkaydet2.ExecuteNonQuery();
                                                baglan.Close();
                                                MessageBox.Show("3");

                                            }
                                            if (durum[j] == "KESİNTİ")
                                            {
                                                baglan.Open();
                                                SqlCommand komutkaydet3 = new SqlCommand("insert into PersonelMaaş (Personel,Ayyıl,ÖdemeDurum,MAAŞ,tutar) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                                                komutkaydet3.Parameters.AddWithValue("@p1", id[i]);
                                                komutkaydet3.Parameters.AddWithValue("@p2", DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM"));
                                                komutkaydet3.Parameters.AddWithValue("@p3", Convert.ToBoolean(false));
                                                komutkaydet3.Parameters.AddWithValue("@p4", (string.Format("{0:N}", tutar[i] - artış[j])) + " TL");
                                                komutkaydet3.Parameters.AddWithValue("@p5", tutar[i] - artış[j]);
                                                komutkaydet3.ExecuteNonQuery();
                                                baglan.Close();
                                                MessageBox.Show("4");
                                            }

                                        }
                                        else
                                        {
                                            if (durum[j] == "PRİM")
                                            {
                                                baglan.Open();
                                                SqlCommand komutkaydet2 = new SqlCommand("update PersonelMaaş set MAAŞ=@p4,tutar=@p5 where Personel='"+id[i]+"' and Ayyıl='" + DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM") + "'", baglan);
                                                komutkaydet2.Parameters.AddWithValue("@p4", (string.Format("{0:N}", tutar2 + artış[j]) + " TL"));
                                                komutkaydet2.Parameters.AddWithValue("@p5", tutar2 + artış[j]);
                                                komutkaydet2.ExecuteNonQuery();
                                                baglan.Close();
                                                MessageBox.Show("3");

                                            }
                                            if (durum[j] == "KESİNTİ")
                                            {
                                                baglan.Open();
                                                SqlCommand komutkaydet3 = new SqlCommand(" update PersonelMaaş set MAAŞ=@p4,tutar=@p5 where Personel='"+id[i]+"' and Ayyıl='" + DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM") + "'", baglan);
                                                komutkaydet3.Parameters.AddWithValue("@p4", (string.Format("{0:N}", tutar2 - artış[j])) + " TL");
                                                komutkaydet3.Parameters.AddWithValue("@p5", tutar2 - artış[j]);
                                                komutkaydet3.ExecuteNonQuery();
                                                baglan.Close();
                                                MessageBox.Show("4");
                                            }
                                        }
                                    }
                                }
                            else
                            {
                                baglan.Open();
                                SqlCommand komutkaydet = new SqlCommand("insert into PersonelMaaş (Personel,Ayyıl,ÖdemeDurum,MAAŞ,tutar) values (@p1, @p2, @p3, @p4, @p5)", baglan);
                                komutkaydet.Parameters.AddWithValue("@p1", id[i]);
                                komutkaydet.Parameters.AddWithValue("@p2", DateTime.Now.Year + " " + DateTime.Now.ToString("MMMM"));
                                komutkaydet.Parameters.AddWithValue("@p3", Convert.ToBoolean(false));
                                komutkaydet.Parameters.AddWithValue("@p4", (string.Format("{0:N}", tutar[i])) + " TL");
                                komutkaydet.Parameters.AddWithValue("@p5", tutar[i]);
                                komutkaydet.ExecuteNonQuery();
                                baglan.Close();
                                MessageBox.Show("5");
                            }
                        }
                        catch (Exception a)
                        {
                            baglan.Close();
                            MessageBox.Show("HATA." + a.ToString());
                        }
                    }
                }
            }
            dataGridView1.Columns.Clear();
            baglan.Open();
            da = new SqlDataAdapter("Select pm.ID,(p.Adi + ' ' +p.Soyadi) 'PERSONEL',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ',pm.MAAŞ,pm.Ayyıl 'VADE',pm.ÖdemeDurum 'ÖDEME DURUMU'  from PersonelMaaş pm join Personeller p on p.ID=pm.Personel join Sube ş on ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Aktiflik='" + Convert.ToBoolean(true) + "'", baglan);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Personeller");
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
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                string odeme = dataGridView1.Rows[i].Cells[7].Value.ToString();
                if (odeme == "True")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                }
                else if (odeme == "False")
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

            }

        }
        List<int> secilenler = new List<int>();
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 8)
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
            if (dataGridView1.CurrentCell.ColumnIndex == 9)
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

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                dataGridView1.Rows[i].Selected = true;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            textBox10.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;



            checkBox1.Checked = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //ARAMA İŞLEMİ
            try
            {

                bool degisken = true;
                string filtre = "Select pm.ID,pm.Personel 'PERSONEL',ş.ŞubeAdi 'ŞUBE',p.Pozisyon 'POZİSYON',p.Brans 'BRANŞ',p.MAAŞ,pm.Ayyıl 'VADE',pm.ÖdemeDurum 'ÖDEME DURUMU'  from PersonelMaaş pm join Personeller p on p.ID = pm.Personel join Sube ş on ş.ID = p.Sube where ş.Okulid = '" + okulid + "'";


                if (string.IsNullOrEmpty(comboBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Pozisyon = '" + comboBox1.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(comboBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Brans = '" + comboBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.Ayyıl = '" + comboBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(comboBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Sube = '" + subeid[comboBox3.SelectedIndex] + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox8.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Adi = '" + textBox8.Text.ToUpper() + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox10.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Soyadi = '" + textBox10.Text.ToUpper() + "'";
                    degisken = true;
                }

                if (checkBox2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Aktiflik = '" + Convert.ToBoolean("true") + "'";
                    degisken = true;
                }
                if (checkBox3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " p.Aktiflik = '" + Convert.ToBoolean(false) + "'";
                    degisken = true;
                }
                if (checkBox4.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.ÖdemeDurum = '" + Convert.ToBoolean(true) + "'";
                    degisken = true;
                }
                if (checkBox1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " pm.ÖdemeDurum = '" + Convert.ToBoolean(false) + "'";
                    degisken = true;
                }
                dataGridView1.Columns.Clear();
                baglan.Open();
                da = new SqlDataAdapter(filtre, baglan);
                cmdb = new SqlCommandBuilder(da);
                ds = new DataSet();
                da.Fill(ds, "Personeller");
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
                panel2.Visible = false;
                textBox10.Text = "";
                textBox8.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox4.Text = "";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;


                checkBox1.Checked = false;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    string odeme = dataGridView1.Rows[i].Cells[7].Value.ToString();
                    if (odeme == "True")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (odeme == "False")
                    {
                        dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }

                }


            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ÖDEME YAPMA 
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
            {
                baglan.Open();
                SqlCommand degistir = new SqlCommand("update PersonelMaaş set ÖdemeDurum=@a5,Tarih=@a6 where ID=@a2 ", baglan);
                degistir.Parameters.AddWithValue("@a2", dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
                degistir.Parameters.AddWithValue("@a5", Convert.ToBoolean(true));
                degistir.Parameters.AddWithValue("@a6", DateTime.Now.Date);

                degistir.ExecuteNonQuery();
                baglan.Close();

            }
            MessageBox.Show("İŞLEM TAMAMLANDI.");
            secilenler.Clear();
            dataGridView1.ClearSelection();
            griddoldur();


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label127_Click(object sender, EventArgs e)
        {

        }

        private void label128_Click(object sender, EventArgs e)
        {

        }

        private void label117_Click(object sender, EventArgs e)
        {

        }

        private void label118_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void label111_Click(object sender, EventArgs e)
        {

        }

        private void label114_Click(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label105_Click(object sender, EventArgs e)
        {

        }

        private void label109_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label99_Click(object sender, EventArgs e)
        {

        }

        private void label100_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
