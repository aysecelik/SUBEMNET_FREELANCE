using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DersDagitim;

namespace SUBEMNET
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void gÜNLÜKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUNLUK fr = new GUNLUK();
            fr.Show();
        }

        private void vİRMANToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gECİKENÖDEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void öĞRENCİÖDEMEDETAYToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iNDİRİMLERLİSTEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mAAŞKONTROLToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void tEDARİKÇİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TEDARIKCILER fr = new TEDARIKCILER();
            fr.Show();
        }

        private void bORÇLARKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BORÇLAR fr = new BORÇLAR();
            fr.Show();
        }

        private void kREDİKARTIÖDEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            KREDIKARTI_ODEME fr = new KREDIKARTI_ODEME();
            fr.Show();
        }

        private void dİĞERGELİREKLEToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mÜŞTERİLERToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fATURAİŞLEMEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FATURA_ISLEME fr = new FATURA_ISLEME();
            fr.Show();
        }

        private void tOPLUFATURAİŞLEMLERİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fATURAİSTATİSTİKToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gİDERBULToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GIDERLER fr = new GIDERLER();
            fr.Show();
        }

        private void tAKIMÇALIŞMASIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakımÇalışması fr = new TakımÇalışması();
            fr.Show();
        }

        private void tOPLANTILARToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TOPLANTI fr = new TOPLANTI();
            fr.Show();
        }

        private void iNSANKAYNAKLARIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            İnsanKaynkaları fr = new İnsanKaynkaları();
            fr.Show();
        }

        private void öĞRENCİToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dERSPROGRAMIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DersDagitim.AnaForm af = new DersDagitim.AnaForm();
            af.Show();
        }
    }
}
