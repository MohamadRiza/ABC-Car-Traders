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
            frma.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCars frm = new ManageCars();
            frm.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCars frm = new ManageCars();
            frm.ShowDialog();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageParts mpfrm = new ManageParts();
            mpfrm.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageParts mpfrm2 = new ManageParts();
            mpfrm2.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("do you want to open Settings?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                Settings frmsting = new Settings();
                frmsting.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            VerifyUnamePsswd frm = new VerifyUnamePsswd();
            frm.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            VerifyUnamePsswd frm = new VerifyUnamePsswd();
            frm.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCustomerOrdersAsaAdmin frm = new ManageCustomerOrdersAsaAdmin();
            frm.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            ManageCustomerOrdersAsaAdmin frm = new ManageCustomerOrdersAsaAdmin();
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }
    }
}
