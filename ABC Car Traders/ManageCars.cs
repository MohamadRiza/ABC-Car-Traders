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
    public partial class ManageCars : Form
    {
        public ManageCars()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frma = new Admin_Dashboard();
            frma.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void ManageCars_Load(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
        }
    }
}
