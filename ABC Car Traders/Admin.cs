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
using System.Data.SqlClient;//1

namespace ABC_Car_Traders
{
    public partial class Admin : Form
    {
        //SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=grifindo_new;Integrated Security=True");//2
        public Admin()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True";
            string query = "SELECT COUNT(1) FROM dbo.Admin_Login WHERE username=@Username AND password=@Password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", textBox1.Text);
                command.Parameters.AddWithValue("@Password", textBox2.Text);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result == 1)
                {
                    MessageBox.Show("Welcome back Admin!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Admin_Dashboard adb = new Admin_Dashboard();
                    adb.Show();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginUser frm = new LoginUser();
            frm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerRegister frm = new CustomerRegister();
            frm.Show();
            this.Hide();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            this.AcceptButton = this.button1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
