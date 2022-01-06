using ExcelDataReader;
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
    public partial class DosyaSMS : Form
    {
        public DosyaSMS()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {

                byte[] byteData = null;

                using (SaveFileDialog savefile = new SaveFileDialog())
                {
                    savefile.Title = "Save File as";
                    savefile.CheckPathExists = true;
                    savefile.FileName = "ÖRNEK_MESAJ_DOSYASI_FORMATI.xlsx";


                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        baglan.Open();
                        komut = new SqlCommand("Select ÖRNEKMESAJDOSYASI from ÖRNEKLER where ID = '1'", baglan);
                        SqlDataReader oku = komut.ExecuteReader();
                        oku.Read();
                        byteData = (byte[])oku[0];
                        File.WriteAllBytes(savefile.FileName, byteData);
                        baglan.Close();
                    }
                }
            }
            catch (Exception A)
            {

                MessageBox.Show(A.ToString());
            }
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;
        int okulid = Form1.okulid;
        SqlCommand komut;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Title = "Lütfen Dosya Seçiniz";
                openFileDialog1.Filter = " (*.xlsx)|*.xlsx";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Multiselect = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string dosya_adres = openFileDialog1.FileName;
                    //Dosyanın okunacağı dizin
                    FileInfo fileinfo = new FileInfo(openFileDialog1.FileName);
                    textBox4.Text = fileinfo.Name;

                    //Dosyayı okuyacağımı ve gerekli izinlerin ayarlanması.
                    FileStream stream = File.Open(dosya_adres, FileMode.Open, FileAccess.Read);
                    //Encoding 1252 hatasını engellemek için;

                    ;

                    IExcelDataReader excelReader;
                    int counter = 0;

                    //Gönderdiğim dosya xls'mi xlsx formatında mı kontrol ediliyor.
                    if (Path.GetExtension(dosya_adres).ToUpper() == ".XLS")
                    {
                        //Reading from a binary Excel file ('97-2003 format; *.xls)
                        excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else
                    {
                        //Reading from a OpenXml Excel file (2007 format; *.xlsx)
                        excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }

                    /*yeni sürümlerde bu kaldırıldığı için kapatıldı.
                    //Datasete atarken ilk satırın başlık olacağını belirtiyor.
                    excelReader.IsFirstRowAsColumnNames = true;
                    DataSet result = excelReader.AsDataSet();*/

                    //Veriler okunmaya başlıyor.
                    while (excelReader.Read())
                    {
                        counter++;

                        if (counter > 1)
                        {
                            dataGridView2.Rows.Add(excelReader.GetDouble(0), excelReader.GetString(1));
                        }
                    }

                    excelReader.Close();
                    dataGridView2.Visible = true;

                }
            }
            catch (Exception)
            {
                MessageBox.Show("HATA");
            }
        }
        List<string> numaralar = new List<string>();
        List<string> mesajlar = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView2.RowCount - 1; i++)
            {

                numaralar.Add(dataGridView2.Rows[i].Cells[0].Value.ToString().Replace(" ", ""));
                mesajlar.Add(dataGridView2.Rows[i].Cells[1].Value.ToString().Replace(" ", ""));


            }
            for (int i = 0; i < numaralar.Count; i++)
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    try
                    {
                        string url = "https://api.iletimerkezi.com/v1/send-sms/get/?" +
                            "username=" + System.Web.HttpUtility.UrlEncode(textBox1.Text) + "&" +
                            "password=" + System.Web.HttpUtility.UrlEncode(textBox2.Text) + "&" +
                            "text=" + mesajlar[i] +
                            "&receipents=" + numaralar[i] + "&" +
                            "sender=" + textBox3.Text;
                        ;
                        string result = client.DownloadString(url);

                        MessageBox.Show("Mesajınız Gönderildi.", "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message, "Mesaj", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
