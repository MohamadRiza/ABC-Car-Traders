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
    public partial class CustomerLoginDashboard : Form
    {
        public CustomerLoginDashboard()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to Logout?", "Logout Request!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                LoginUser frm = new LoginUser();
                frm.Show();
            }
        }

        private void CustomerLoginDashboard_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            label2.Text = LoginUser.welcomeuser; //get username textbox to user dashboard (Fetch Here)
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerProfileinDshBD frmc = new CustomerProfileinDshBD();
            frmc.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerProfileinDshBD frmc = new CustomerProfileinDshBD();
            frmc.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyaCarasACustomer bcfrm = new BuyaCarasACustomer();
            bcfrm.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyaCarasACustomer bcfrm = new BuyaCarasACustomer();
            bcfrm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyACarPartasACustomer frm = new BuyACarPartasACustomer();
            frm.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BuyACarPartasACustomer frm = new BuyACarPartasACustomer();
            frm.Show();
        }
    }
}
