using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SUBEMNET
{
    
    public partial class Form1 : Form
    {
        public static int okulid = 1;
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) == true || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("KULLANICI ADI VEYA ŞİFRE BOŞ BIRAKILAMAZ!!");
            }
            else
            {
                baglan.Open();
                SqlCommand komut = new SqlCommand("SELECT KULLANICI_ADI,  SIFRE from Admin WHERE KULLANICI_ADI='" + textBox1.Text + "' AND SIFRE='" + textBox2.Text + "'",baglan);
                SqlDataReader oku = komut.ExecuteReader();
                if (oku.Read())
                {
                    Menu fr = new Menu();
                    fr.Show();
                    this.Hide();
                   
                }
                else
                {
                    MessageBox.Show("KULLANICI ADI YA DA ŞİFRE YANLIŞ.");

                }
                baglan.Close();
                textBox1.Text = "";
                textBox2.Text = "";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
