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
    public partial class Admin_Dashboard : Form
    {
        public Admin_Dashboard()
        {
            InitializeComponent();
        }

        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want Logout?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
            { 
            this.Hide();
            Admin frma = new Admin();
            frma.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCars frm = new ManageCars();
            frm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCars frm = new ManageCars();
            frm.Show();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
