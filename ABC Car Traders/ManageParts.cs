using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABC_Car_Traders
{
    public partial class ManageParts : Form
    {
        public ManageParts()
        {
            InitializeComponent();
        }
        private void cleartextbox()
        {
            txtpartid.Clear();
            txtname.Clear();
            txtbrand.Clear();
            txtcapatability.Clear();
            txtprice.Clear();
            txtstock.Clear();
            richtxtdescription.Clear();
            textBox1.Clear();
            pictureBox1.Image = null;
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frm = new Admin_Dashboard();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cleartextbox();
        }

        private void ManageParts_Load(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }
    }
}
