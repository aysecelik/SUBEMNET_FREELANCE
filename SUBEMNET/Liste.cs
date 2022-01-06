using iTextSharp.text;
using iTextSharp.text.pdf;
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
    public partial class Liste : Form
    {
        public string query;
        byte[] bytes;
        string path;
        bool cntrl = true;
        string drm = null;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        string yol = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        void ogrDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            baglan.Close();
        }

      
        void boyBedenDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView4.DataSource = dt;
            }
            baglan.Close();
        }
        void karneDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                dataGridView5.Columns.Add(chk);
                chk.HeaderText = "Seçim";
                chk.Name = "chk";
                dataGridView5.DataSource = dt;
            }
            baglan.Close();
        }
        void randevuDoldur()
        {
            baglan.Open();
            if (query != null)
            {
                da = new SqlDataAdapter(query, baglan);
                dt = new DataTable();
                cmdb = new SqlCommandBuilder(da);
                da.Fill(dt);
                dataGridView6.DataSource = dt;
            }
            baglan.Close();
        }
        void fotoDoldur()
        {
            baglan.Open();
            if (radioButton3.Checked == true)
                da = new SqlDataAdapter("Select Id, Cinsiyet, Adi as Ad, Soyadi as Soyad From Ogrenci where sube='" + subeid[cmb1.SelectedIndex] + "' and Snf='" + cmb2.Text + "' and Foto=00000000", baglan);
            else
                da = new SqlDataAdapter("Select Id, Cinsiyet, Adi as Ad, Soyadi as Soyad From Ogrenci where sube='" + subeid[cmb1.SelectedIndex] + "' and Snf='" + cmb2.Text + "and Foto!=00000000", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView7.DataSource = dt;
            baglan.Close();
            if (cntrl == true)
            {
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "FOTOĞRAF";
                dgvBtn.Text = "Fotoğraf";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
            }
        }
        public Liste()
        {
            InitializeComponent();
        }

        private void öğrenciListeleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                bool deg = false;
                string filtre = "Select OkulNo, SozNo, Adi, Soyadi";
                if (checkBoxSube.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Sube";
                    degisken = true;
                }
                if (checkBoxTCKN.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " TCKN";
                    degisken = true;
                }
                if (checkBoxCinsiyet.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Cinsiyet";
                    degisken = true;
                }
                if (checkBoxDT.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " DogumTarihi";
                    degisken = true;
                }
                if (checkBoxProgram.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Program";
                    degisken = true;
                }
                if (checkBoxDevre.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Devre";
                    degisken = true;
                }
                if (checkBoxKur.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Kur";
                    degisken = true;
                }
                if (checkBoxOkul.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Okul";
                    degisken = true;
                }
                if (checkBoxEposta.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Eposta";
                    degisken = true;
                }
                if (checkBoxEvTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " EvTel";
                    degisken = true;
                }
                if (checkBoxOgrCepTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OgrCepTel";
                    degisken = true;
                }
                if (checkBoxKaydeden.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Kaydeden";
                    degisken = true;
                }
                if (checkBoxKayitTar.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OlusturmaTarihi";
                    degisken = true;
                }
                if (checkBoxSehir.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Sehir";
                    degisken = true;
                }
                if (checkBoxIlce.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Ilce";
                    degisken = true;
                }
                if (checkBoxMah.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Mahalle";
                    degisken = true;
                }
                if (checkBoxAdres.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Adres";
                    degisken = true;
                }
                if (checkBoxVeliAdi.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliAdSoyad";
                    degisken = true;
                }
                if (checkBoxYakinlik.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Yakinlik";
                    degisken = true;
                }
                if (checkBoxMeslek.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliMeslek";
                    degisken = true;
                }
                if (checkBoxVeliCep.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " VeliCepTel";
                    degisken = true;
                }
                if (checkBoxMD.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " MaliDurum";
                    degisken = true;
                }
                if (checkBoxOB1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi1";
                    degisken = true;
                }
                if (checkBoxOB2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi2";
                    degisken = true;
                }
                if (checkBoxOB3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " OzelBilgi3";
                    degisken = true;
                }
                if (checkBoxAciklama.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " Aciklama";
                    degisken = true;
                }
                filtre += " From OnKayit Where ";

                if (cmbSube.SelectedIndex != -1)
                {
                    if (deg)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Sube = " + cmbSube.Text;
                    deg = true;
                }
                if (cmbDevre.SelectedIndex != -1)
                {
                    if (deg)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Devre = " + cmbDevre.Text;
                    deg = true;
                }
                if (cmbKur.SelectedIndex != -1)
                {
                    if (deg)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Kur = " + cmbKur.Text;
                    deg = true;
                }
                if (cmbSnf.SelectedIndex != -1)
                {
                    if (deg)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Snf = " + cmbSnf.Text;
                    deg = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (deg)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Program = " + cmbProgram.Text;
                    deg = true;
                }
                //if (cmbYetkiliAd.SelectedIndex != -1)
                //{
                //    if (deg)
                //    {
                //        filtre += " AND ";
                //    }
                //    filtre += cmbYetkili +" = " + cmbYetkiliAd.SelectedIndex;
                //    deg = true;
                //}
                if (radioButtonOlan.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Durum = 1";
                    degisken = true;
                }
                if (radioButtonOlmayan.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Durum = 0";
                    degisken = true;
                }

                if (dtKayTarBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " KayitTarihi between '" + dtKayTarBas.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dtKayTarBit.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel3.Visible = false;
                ogrDoldur();
                query = null;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }







     

       
        

        private void boyKiloVeBedenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel13.Visible = true;
            panel15.Visible = false;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            panel13.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.ŞubeAdi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Devre, ö.Snf as Sınıf, ö.Boy, ö.Kilo, ö.Beden from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ö.Snf='" + comboBox1.Text+"' and ş.okulid='"+okulid+"'";
            boyBedenDoldur();
            query = null;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.ŞubeAdi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Devre, ö.Snf as Sınıf, ö.Boy, ö.Kilo, ö.Beden from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ö.Devre='" + comboBox2.Text + "' and ş.okulid='" + okulid + "'";
            boyBedenDoldur();
            query = null;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            query = "Select ö.ID, ö.Sezon, ş.ŞubeAdi, ö.SozNo, ö.OkulNo, ö.Adi as Adı, ö.Soyadi as Soyadı, ö.Devre, ö.Snf as Sınıf, ö.Boy, ö.Kilo, ö.Beden from Ogrenci ö join Sube ş on ş.ID=ö.Sube Where  ş.okulid='" + okulid + "'";
            boyBedenDoldur();
            query = null;
        }

        private void button3_Click(object sender, EventArgs e)
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
            worksheet.Name = "Öğrenci Listesi";
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
            workbook.SaveAs(yol + "\\Ogrenci Listesi.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }
        int okulid = Form1.okulid;
        private void button14_Click_1(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select ö.ID, ö.OkulNo, ö.SozNo, ö.Adi as Ad, ö.Soyadi as Soyad";
                if (checkBoxSube.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ş.ŞubeAdi";
                    degisken = true;
                }
                if (checkBoxTCKN.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.TCKN";
                    degisken = true;
                }
                if (checkBoxCinsiyet.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Cinsiyet";
                    degisken = true;
                }
                if (checkBoxDT.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.DogumTarihi";
                    degisken = true;
                }
                if (checkBoxProgram.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Program";
                    degisken = true;
                }
                if (checkBoxDevre.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Devre";
                    degisken = true;
                }
                if (checkBoxKur.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Kur";
                    degisken = true;
                }
                if (checkBoxOkul.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Okul";
                    degisken = true;
                }
                if (checkBoxEposta.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Eposta";
                    degisken = true;
                }
                if (checkBoxEvTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.EvTel";
                    degisken = true;
                }
                if (checkBoxOgrCepTel.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OgrCepTel";
                    degisken = true;
                }
                if (checkBoxKaydeden.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Kaydeden";
                    degisken = true;
                }
                if (checkBoxKayitTar.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OlusturmaTarihi";
                    degisken = true;
                }
                if (checkBoxSehir.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Sehir";
                    degisken = true;
                }
                if (checkBoxIlce.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Ilce";
                    degisken = true;
                }
                if (checkBoxMah.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Mahalle";
                    degisken = true;
                }
                if (checkBoxAdres.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Adres";
                    degisken = true;
                }
                if (checkBoxVeliAdi.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliAdSoyad";
                    degisken = true;
                }
                if (checkBoxYakinlik.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Yakinlik";
                    degisken = true;
                }
                if (checkBoxMeslek.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliMeslek";
                    degisken = true;
                }
                if (checkBoxVeliCep.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.VeliCepTel";
                    degisken = true;
                }
                if (checkBoxMD.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.MaliDurum";
                    degisken = true;
                }
                if (checkBoxOB1.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi1";
                    degisken = true;
                }
                if (checkBoxOB2.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi2";
                    degisken = true;
                }
                if (checkBoxOB3.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.OzelBilgi3";
                    degisken = true;
                }
                if (checkBoxAciklama.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += ",";
                    }
                    filtre += " ö.Aciklama";
                    degisken = true;
                }
                filtre += " From Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.okulid='"+okulid+"'";

                if (cmbSube.SelectedIndex != -1)
                {
                    filtre += " AND ";

                    filtre += " ş.ŞubeAdi = '" + cmbSube.Text+"'";
                    degisken = true;
                }


                if (cmbDevre.SelectedIndex != -1)
                {
                    if (degisken==true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Devre = '" + cmbDevre.Text + "'";
                    degisken = true;
                }
                if (cmbSnf.SelectedIndex != -1)
                {
                    if (degisken==true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Snf = '" + cmbSnf.Text + "'";
                    degisken = true;
                }
                if (cmbKur.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Kur = '" + cmbKur.Text + "'";
                    degisken = true;
                }
                if (cmbProgram.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Program = '" + cmbProgram.Text + "'";
                    degisken = true;
                }
                if (radioButtonOlan.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Durum = 1";
                    degisken = true;
                }
                if (radioButtonOlmayan.Checked == true)
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.Durumu = 0";
                    degisken = true;
                }

                if (dtKayTarBas.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " ö.KayitTarihi between '" + dtKayTarBas.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dtKayTarBit.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel3.Visible = false;
                ogrDoldur();
                query = null;

            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show("HATA");
            }
        }

      

       

        private void button23_Click(object sender, EventArgs e)
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
            worksheet.Name = "Boy Kilo Beden Listesi";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView4.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView4.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView4.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView4.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView4.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  
            workbook.SaveAs(yol + "\\Boy Kilo Beden Listesi.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            panel15.Visible = false;
        }

        private void öğrenciKarnesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel15.Visible = true;
            panel19.Visible = false;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            panel17.Visible = true;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = false;
                string filtre = "Select ö.SozNo, ö.OkulNo, ö.Adi, ö.Soyadi, ö.Devre, ö.Snf From Ogrenci ö join Sube ş on ş.ID=ö.Sube Where ş.Okulid='"+okulid+"'";
                if (comboBox4.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Sube = '" + subeid[comboBox4.SelectedIndex]+"'";
                    degisken = true;
                }
                if (comboBox3.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Snf = '" + comboBox3.Text+"'";
                    degisken = true;
                }
                if (comboBox5.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Devre = '" + comboBox5.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.SozNo='" + textBox2.Text.TrimEnd()+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.OkulNo='" + textBox1.Text.TrimEnd()+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Adi='" + textBox3.Text.TrimEnd()+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " ö.Soyadi='" + textBox4.Text.TrimEnd()+"'";
                    degisken = true;
                }
                query = filtre;
                panel17.Visible = false;
                karneDoldur();
                query = null;

            }
            catch (Exception a)
            {
                MessageBox.Show("HATA");
            }
            finally
            {
                baglan.Close();
            }
        }

        private void veliRandevularıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel19.Visible = true;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            panel19.Visible = false;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            panel21.Visible = true;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            panel21.Visible = false;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {

                bool degisken = true;
                string filtre = "Select Ogrenci.Adi, Ogrenci.Soyadi, Ogrenci.Devre, Ogrenci.Snf, Veli.AdSoyad, Veli.Yakinlik as Görüşülen, Veli.CepTel, Randevu.Gorusen, Randevu.Tur, Randevu.GrsmTur, Randevu.Tarih, Randevu.Saat From" +
                    " Randevu Inner Join Ogrenci On Randevu.OgrId=Ogrenci.Id join Sube ş on ş.ID=Ogrenci.Sube join Veli on Veli.OgrId=Ogrenci.ID where ş.okulid='"+okulid+"' and Veli.VeliMi=1";
                if (cmbSubeV.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Ogrenci.Sube = '" + subeid[cmbSubeV.SelectedIndex]+"'";
                    degisken = true;
                }
                if (cmbSnfV.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Ogrenci.Snf = '" + cmbDevreV.Text + "'";
                    degisken = true;
                }
                if (cmbDevreV.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Ogrenci.Devre = '" + cmbDevreV.Text + "'";
                    degisken = true;
                }
              
                if (!string.IsNullOrEmpty(textBox6.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Randevu.GrsmTur ='" + textBox6.Text+"'";
                    degisken = true;
                }
                if (cmbOgrV.SelectedIndex != -1)
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Randevu.Gorusen ='" + cmbOgrV.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " And ";
                    }
                    filtre += " Randevu.Tur = '" + textBox5.Text+"'";
                    degisken = true;
                }
                if (!string.IsNullOrEmpty(txtNotV.Text))
                {
                    if (degisken == true)
                    {
                        filtre += " and ";
                    }
                    filtre += " Randevu.Not LIKE '%" + txtNotV.Text.TrimEnd() + "%'";
                    degisken = true;
                }
                if (dtBasV.Value.ToString("yyyyMMdd") != DateTime.Now.ToString("yyyyMMdd"))
                {
                    if (degisken == true)
                    {
                        filtre += " AND ";
                    }
                    filtre += " Tarih between '" + dtBasV.Value.ToString("yyyyMMdd HH:mm:ss") + "' and '" + dtBitV.Value.ToString("yyyyMMdd HH:mm:ss") + "'";
                    degisken = true;
                }
                query = filtre;
                panel21.Visible = false;
                randevuDoldur();
                query = null;

            }
            catch (Exception a)
            {
                
                MessageBox.Show(a.ToString());
            }
            finally
            {
                if (baglan.State == ConnectionState.Open)
                    baglan.Close();
            }
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
            worksheet.Name = "Randevu Listesi";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView6.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView6.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView6.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView6.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView6.Rows[i].Cells[j].Value.ToString();
                }
            }

            // save the application  
            workbook.SaveAs(yol + "\\Randevu Listesi.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);


            }
            // Read the file and convert it to Byte Array
            string filePath = path;
            string contenttype = String.Empty;

            contenttype = "application/jpg";

            if (contenttype != String.Empty)
            {
                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
            }
        }

        private void hızlıFotoğrafGirişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel33.Visible = true;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            panel33.Visible = false;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (cmb1.SelectedIndex != -1 && cmb2.SelectedIndex != -1)
            {
                fotoDoldur();
                cntrl = false;
            }
            else
            {
                MessageBox.Show("Şube ve Sınıf seçiniz.");
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                baglan.Open();
                string guncel = "Update Ogrenci set Foto=@a1 Where ID =" + drm;
                SqlCommand guncelle = new SqlCommand(guncel, baglan);
                guncelle.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;
                guncelle.ExecuteNonQuery();
                baglan.Close();
                cntrl = false;
                fotoDoldur();
            }
            else
            {
                MessageBox.Show("Fotoğraf seçiniz.");
            }

        }

        private void button34_Click(object sender, EventArgs e)
        {
            panel25.Visible = false;
        }

        private void dataGridView7_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                int secilen = dataGridView1.CurrentCell.RowIndex;
                drm = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                panel25.Visible = true;
            }
        }
        SqlCommand komut;
        List<int> subeid = new List<int>();

        private void Liste_Load(object sender, EventArgs e)
        {
            cmbKur.Items.Add("SAY");
            cmbKur.Items.Add("SOZ");
            cmbKur.Items.Add("EA");
            cmbKur.Items.Add("YDİL");
            cmbKur.Items.Add("MES");
            subeid.Clear();
            komut = new SqlCommand("Select ŞubeAdi,ID from Sube where Okulid='" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                cmbSube.Items.Add(oku[0].ToString());
                cmbSubeV.Items.Add(oku[0].ToString());
                comboBox4.Items.Add(oku[0].ToString());

                cmb1.Items.Add(oku[0].ToString());
                subeid.Add((int)oku[1]);
            }
            baglan.Close();
            komut = new SqlCommand("Select b.DEVRE From Sube ş join Devreler b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku8 = komut.ExecuteReader();
            while (oku8.Read())
            {
                cmbDevre.Items.Add(oku8[0].ToString());
                cmbDevreV.Items.Add(oku8[0].ToString());

                comboBox2.Items.Add(oku8[0].ToString());
                comboBox5.Items.Add(oku8[0].ToString());


            }
            baglan.Close();
            komut = new SqlCommand("Select b.SINIF From Sube ş join Sınıflar b on ş.ID = b.Sube where ş.Okulİd = '" + okulid + "'", baglan);
            baglan.Open();
            SqlDataReader oku2 = komut.ExecuteReader();
            while (oku2.Read())
            {
                cmbSnf.Items.Add(oku2[0].ToString());
                cmb2.Items.Add(oku2[0].ToString());
                cmbSnfV.Items.Add(oku2[0].ToString());
                comboBox1.Items.Add(oku2[0].ToString());
                comboBox3.Items.Add(oku2[0].ToString());



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
            komut = new SqlCommand("Select (p.Adi+' '+p.Soyadi) from Personeller p  join Sube ş on  ş.ID=p.Sube where ş.Okulid='" + okulid + "' and p.Pozisyon='ÖĞRETMEN'", baglan);
            baglan.Open();
            SqlDataReader oku11 = komut.ExecuteReader();
            while (oku11.Read())
            {
                cmbOgrV.Items.Add(oku11[0].ToString());
            }
            baglan.Close();
        }

        private void sınıfListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rehberlikNotuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel19.Visible = true;
            panel33.Visible = false;
        }

        private void button27_Click(object sender, EventArgs e)
        {

        }

       

      
        private void button10_Click(object sender, EventArgs e)
        {
            panel17.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            panel17.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.OverwritePrompt = false;
            save.Title = "PDF Dosyaları";
            save.DefaultExt = "pdf";
            save.Filter = "PDF Dosyaları (*.pdf)|*.pdf|Tüm Dosyalar(*.*)|*.*";
            if (save.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.pdf.BaseFont STF_Helvetica_Turkish = iTextSharp.text.pdf.BaseFont.CreateFont("Helvetica", "CP1254", iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
                iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(STF_Helvetica_Turkish, 12, iTextSharp.text.Font.NORMAL);
                PdfPTable pdfTable = new PdfPTable(dataGridView1.ColumnCount );

                // Bu alanlarla oynarak tasarımı iyileştirebilirsiniz.
                pdfTable.SpacingBefore = 20f;
                pdfTable.DefaultCell.Padding = 5; // hücre duvarı ve veri arasında mesafe
                pdfTable.WidthPercentage = 100; // hücre genişliği
                pdfTable.HorizontalAlignment = Element.ALIGN_LEFT; // yazı hizalaması
                pdfTable.DefaultCell.BorderWidth = 1; // kenarlık kalınlığı
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {



                    PdfPCell cell = new PdfPCell(new Phrase(dataGridView1.Columns[i].HeaderText, fontTitle));
                    cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240); // hücre arka plan rengi
                    pdfTable.AddCell(cell);

                }
                try
                {
                    for (int i = 0; i < dataGridView1.RowCount; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            pdfTable.AddCell(new Phrase(dataGridView1.Rows[i].Cells[j].Value.ToString(), fontTitle));

                        }
                    }


                }
                catch (NullReferenceException)
                {
                }
                using (FileStream stream = new FileStream(save.FileName + ".pdf", FileMode.Create))
                {

                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);// sayfa boyutu.
                    PdfWriter.GetInstance(pdfDoc, stream);
                    iTextSharp.text.Font titleFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 20, iTextSharp.text.Font.NORMAL);
                    iTextSharp.text.Font regularFont = new iTextSharp.text.Font(STF_Helvetica_Turkish, 15, iTextSharp.text.Font.NORMAL);
                    Paragraph title;
                    Paragraph text;
                    title = new Paragraph(textBox1.Text, titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    pdfDoc.Open();
                    

                    pdfDoc.Add(pdfTable);
                    pdfDoc.Close();
                    stream.Close();
                }
            }
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
