using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Car_Traders
{
    public partial class VerifyUnamePsswd : Form
    {
        public VerifyUnamePsswd()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frm = new Admin_Dashboard();
            frm.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpasswd.UseSystemPasswordChar = false;
            }
            else
            {
                txtpasswd.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True";
            string query = "SELECT COUNT(1) FROM dbo.Admin_Login WHERE username=@Username AND password=@Password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", txtuname.Text);
                command.Parameters.AddWithValue("@Password", txtpasswd.Text);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result == 1)
                {
                    MessageBox.Show("Thanks for Verifying...!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    managecustomer frm = new managecustomer();
                    frm.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
        }

        private void VerifyUnamePsswd_Load(object sender, EventArgs e)
        {

        }
    }
}
