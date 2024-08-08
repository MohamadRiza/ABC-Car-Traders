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
    public partial class BuyPartsClickPartsDGV : Form
    {
        public BuyPartsClickPartsDGV()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void BuyPartsClickPartsDGV_Load(object sender, EventArgs e)
        {
            panel1.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}
