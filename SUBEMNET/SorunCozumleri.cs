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
    public partial class SorunCozumleri : Form
    {
        public string query;
        bool cntrl =true;
        SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-H17HO6C2\SQLEXPRESS;Initial Catalog=SUBEMNETDATABASE;Integrated Security=True");
        SqlDataAdapter da;
        DataSet ds;
        DataTable dt;
        SqlCommandBuilder cmdb;
        public SorunCozumleri()
        {
            InitializeComponent();
        }
        int okulid = Form1.okulid;
        void griddoldur()
        {
            baglan.Open();
            if (query != null)
                da = new SqlDataAdapter(query, baglan);
            else
                da = new SqlDataAdapter("Select VeliGorusu.ID, ş.ŞubeAdi AS Şube, Ogrenci.Adi AS Adı, Ogrenci.Soyadi AS Soyadı, Ogrenci.OgrCepTel, Veli.AdSoyad, Veli.CepTel," +
                    " Ogrenci.Snf AS Sınıf, VeliGorusu.Konu AS Konu, VeliGorusu.Gorus AS Görüş, VeliGorusu.OlusturmaTarihi AS Tarih from Ogrenci" +
                    " INNER JOIN Veli on Ogrenci.ID=veli.OgrID INNER JOIN VeliGorusu on Ogrenci.ID=VeliGorusu.OgrID and velimi=1 and VeliGorusu.Durum=1 join Sube ş on ş.ID=Ogrenci.Sube and ş.Okulid='"+okulid+"'", baglan);
            dt = new DataTable();
            cmdb = new SqlCommandBuilder(da);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglan.Close();
            if (cntrl==true)
            {
                DataGridViewButtonColumn dgvBtn = new DataGridViewButtonColumn();
                dgvBtn.HeaderText = "ÇÖZÜLDÜ";
                dgvBtn.Text = "ÇÖZÜLDÜ";
                dgvBtn.UseColumnTextForButtonValue = true;
                dgvBtn.DefaultCellStyle.SelectionBackColor = Color.Red;
                dgvBtn.Width = 70;
                dataGridView1.Columns.Add(dgvBtn);
            }       
        }
        private void button7_Click(object sender, EventArgs e)
        {
            griddoldur();
            cntrl = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 11)
            {
                baglan.Open();
                string drm = null;
                int secilen = dataGridView1.CurrentCell.RowIndex;
                drm = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
                string guncel = "Update VeliGorusu set Durum=" + 0 + "Where ID =" + drm;
                SqlCommand guncelle = new SqlCommand(guncel, baglan);
                guncelle.ExecuteNonQuery();
                baglan.Close();
                cntrl = false;
                griddoldur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void SorunCozumleri_Load(object sender, EventArgs e)
        {

        }
    }
}
