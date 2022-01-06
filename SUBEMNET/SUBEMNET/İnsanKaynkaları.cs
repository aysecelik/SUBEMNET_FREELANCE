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
using System.Configuration;
using System.IO;

namespace SUBEMNET
{
    public partial class İnsanKaynkaları : Form
    {
        public İnsanKaynkaları()
        {
            InitializeComponent();
        }

        private void İnsanKaynkaları_Load(object sender, EventArgs e)
        {
            griddoldur();
            panel2.Visible = false;
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        byte[] bytes;
        byte[] bytes2;
        byte[] bytes3;

        void griddoldur()
        {
            baglan.Open();
            da = new SqlDataAdapter("Select ID,Email,Adi,Soyadi,CepTel,Pozisyon,Branş,CVDosya, SicilDosya, SaglikRaporDosya from IsBasvuru", baglan);

            cmdb = new SqlCommandBuilder(da);
            
            ds = new DataSet();
            da.Fill(ds, "IsBasvuru");
            dataGridView1.DataSource = ds.Tables[0];
            baglan.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Ekleme Kısım Açılış
            panel2.Visible = true;
            panel1.Visible = true;
            panel5.Visible = false;
            panel7.Visible = false;
            panel10.Visible = false;
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            //fotoğraf ekleme
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
               
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;

        }

        private void button11_Click(object sender, EventArgs e)
        {
            //eğitimdurumu açılış
            panel5.Visible = true;
            panel7.Visible = false;
            panel10.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel7.Visible = true;
            panel10.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            panel7.Visible = false;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel7.Visible = true;
            panel10.Visible = false;
        }

        private void mskevtel_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            try
            {
                bool degisken = false;
                string filtre = "insert into IsBasvuru (";
                string values = "Values (";
                if (string.IsNullOrEmpty(txteposta.Text) == false)
                {
                    filtre += " Email ";
                    values += "'" + txteposta.Text + "'";
                    degisken = true;
                }


                if (string.IsNullOrEmpty(cmbpozisyon.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Pozisyon ";
                    values += "'" + cmbpozisyon.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbBranş.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Branş  ";
                    values += "'" + cmbBranş.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbcinsiyet.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Cinsiyet ";
                    values += "'" + cmbcinsiyet.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtTC.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " TcKimlikNo ";
                    values += "'" + txtTC.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtAd.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Adi ";
                    values += "'" + txtAd.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtSoyad.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Soyadi ";
                    values += "'" + txtSoyad.Text + "'";
                    degisken = true;
                }
                if (datetimeDogumtarih.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " DogumTarihi ";
                    values += "'" + datetimeDogumtarih.Value.Date + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtdogumyeri.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " DogumYeri ";
                    values += "'" + txtdogumyeri.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(mskevtel.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " EvTel ";
                    values += "'" + mskevtel.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(mskceptel.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " CepTel ";
                    values += "'" + mskceptel.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(mskceptel2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " CepTel2 ";
                    values += "'" + mskceptel2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtevil.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Ev_Adres_il ";
                    values += "'" + txtevil.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtevilce.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Ev_Adres_ilce ";
                    values += "'" + txtevilce.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtevmahalle.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Ev_Adres_Mahalle ";
                    values += "'" + txtevmahalle.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtevadres.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Ev_Adres ";
                    values += "'" + txtevadres.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtisdeneyim.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Is_Deneyimi ";
                    values += txtisdeneyim.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbaskerlik.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Askerlik_Durumu ";
                    values += "'" + cmbaskerlik.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(cmbmedeni.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Medeni_Hal ";
                    values += "'" + cmbmedeni.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbcalismadurum.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Calisma_Durumu ";
                    values += "'" + cmbcalismadurum.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtbabaadi.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Baba_Adi ";
                    values += "'" + txtbabaadi.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtanneadi.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Anne_Adi ";
                    values += "'" + txtanneadi.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtnüfusil.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Nüfus_Kayıt_ilce ";
                    values += "'" + txtnüfusil.Text + "'";
                    degisken = true;
                }

                if (string.IsNullOrEmpty(cmbkan.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " KanGrubu ";
                    values += "'" + cmbkan.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtciltno.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Cilt_No ";
                    values += "'" + txtciltno.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtailesırano.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Aile_Sıra_No ";
                    values += "'" + txtailesırano.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtsırano.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Sıra_No ";
                    values += "'" + txtsırano.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtengeldurumu.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                    }
                    filtre += " EngelDurumuYüzde ";
                    values += "'" + txtengeldurumu.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbeğitim.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Egitim_Durumu ";
                    values += "'" + cmbeğitim.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtegitimdurum.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Mezun_Olunan_Kurum ";
                    values += "'" + txtegitimdurum.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtFakülte.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Mezun_Fakulte ";
                    values += "'" + txtFakülte.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtBölüm.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Mezun_Bölüm ";
                    values += "'" + txtBölüm.Text + "'";
                    degisken = true;
                }
                if (datemezuntarih.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Mezun_Olunan_Tarih ";
                    values += "'" + datemezuntarih.Value.Date + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(cmbişdeneyimidurumu.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimiDurum ";
                    values += "'" + cmbişdeneyimidurumu.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(txtkurum1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi1 ";
                    values += "'" + txtkurum1.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox2.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi2 ";
                    values += "'" + textBox2.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox3.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi3 ";
                    values += "'" + textBox3.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox4.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimii4 ";
                    values += "'" + textBox4.Text + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox5.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimii5 ";
                    values += "'" + textBox5.Text + "'";
                    degisken = true;
                }
                if (date1kurum1.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi1Tarih ";
                    values += "'" + date1kurum1.Value.Date.ToString("yyyyMMdd") + " - " + date2kurum1.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (date1kurum2.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi2Tarih ";
                    values += "'" + date1kurum2.Value.Date.ToString("yyyyMMdd") + " - " + date2kurum2.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (date1kurum3.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi3Tarih ";
                    values += "'" + date1kurum3.Value.Date.ToString("yyyyMMdd") + " - " + date2kurum3.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (date1kurum4.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi4Tarih ";
                    values += "'" + date1kurum4.Value.Date.ToString("yyyyMMdd") + " - " + date2kurum4.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (date1kurum1.Value != DateTime.Now)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " IsDeneyimi5Tarih ";
                    values += "'" + date1kurum5.Value.Date.ToString("yyyyMMdd") + " - " + date2kurum5.Value.Date.ToString("yyyyMMdd") + "'";
                    degisken = true;
                }
                if (pictureBox1.Image != null)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " foto ";
                    values += "'" + pictureBox1.Image + "'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox1.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " CV, CVDosya ";
                    values += "@a1 , '"+ CVfilename+"'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox6.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " Sicil, SicilDosya ";
                    values += "@a2, '"+ SicilFilename +"'";
                    degisken = true;
                }
                if (string.IsNullOrEmpty(textBox7.Text) == false)
                {
                    if (degisken == true)
                    {
                        filtre += " , ";
                        values += " , ";
                    }
                    filtre += " SaglikRapor, SaglikRaporDosya ";
                    values += " @a3 , '"+ Saglikfilename + "'";
                    degisken = true;
                }
                filtre += ")";
                values += ")";
                filtre += values;
                baglan.Open();
                SqlCommand komutkaydet = new SqlCommand(filtre, baglan);
                komutkaydet.Parameters.AddWithValue("@a1", SqlDbType.VarBinary).Value = bytes;
                komutkaydet.Parameters.AddWithValue("@a2", SqlDbType.VarBinary).Value = bytes2;
                komutkaydet.Parameters.AddWithValue("@a3", SqlDbType.VarBinary).Value = bytes3;

                komutkaydet.ExecuteNonQuery();
                baglan.Close();
                MessageBox.Show("Kayıt Başarılı");
                griddoldur();
                panel2.Visible = false;
                
            }
            catch (Exception a)
            {
                baglan.Close();
                MessageBox.Show(a.ToString());

            }
        }
        //fileUpload kısımı
        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    textBox1.Text = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = textBox1.Text;
            string contenttype = String.Empty;

            contenttype = "application/pdf";

            if (contenttype != String.Empty)
            {
                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes = br.ReadBytes((Int32)fs.Length);
                FileInfo fileinfo = new FileInfo(open.FileName);
                CVfilename = fileinfo.Name;
            }
        }
        string CVfilename;
        string SicilFilename;
        string Saglikfilename;
        private void button18_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    textBox6.Text = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = textBox6.Text;
            string contenttype = String.Empty;

            contenttype = "application/pdf";

            if (contenttype != String.Empty)
            {
                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes2 = br.ReadBytes((Int32)fs.Length);
                FileInfo fileinfo = new FileInfo(open.FileName);
               SicilFilename = fileinfo.Name;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "PDF Files | *.pdf";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.FileName.Length > 0)
                {
                    textBox7.Text = open.FileName;
                }
            }

            // Read the file and convert it to Byte Array
            string filePath = textBox7.Text;
            string contenttype = String.Empty;

            contenttype = "application/pdf";

            if (contenttype != String.Empty)
            {
                Stream fs = File.OpenRead(filePath);
                BinaryReader br = new BinaryReader(fs);
                bytes3 = br.ReadBytes((Int32)fs.Length);
                FileInfo fileinfo = new FileInfo(open.FileName);
                Saglikfilename = fileinfo.Name;

            }
        }
    }
    }

