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
            this.Hide();
            Admin frma = new Admin();
            frma.Show();
        }
    }
}
