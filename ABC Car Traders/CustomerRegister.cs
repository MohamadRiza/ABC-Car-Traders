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
    public partial class CustomerRegister : Form
    {
        public CustomerRegister()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you are the admin?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                Admin frmadmin = new Admin();
                frmadmin.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You are pressed No!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CustomerRegister_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginUser frm = new LoginUser();
            frm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to clear?", "Clear", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
                comboBox1.ResetText();
                checkBox1.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox6.UseSystemPasswordChar = false;
            }
            else
            {
                textBox6.UseSystemPasswordChar = true;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
