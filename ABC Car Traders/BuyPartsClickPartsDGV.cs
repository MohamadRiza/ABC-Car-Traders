using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
            string recipientEmail = "recipient@example.com";
            string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={recipientEmail}";

            try
            {
                Process.Start(new ProcessStartInfo(gmailUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
