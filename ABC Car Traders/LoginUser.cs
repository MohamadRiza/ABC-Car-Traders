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
    public partial class LoginUser : Form
    {
        public LoginUser()
        {
            InitializeComponent();
        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you are the admin?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                Admin frmadmin = new Admin();
                frmadmin.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("You are pressed No!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerRegister frm = new CustomerRegister();
            frm.ShowDialog();
            this.Hide();
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

        private void LoginUser_Load(object sender, EventArgs e)
        {
            AcceptButton = this.button1;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
        public static string welcomeuser = ""; //get username textbox to user dashboard 1
        public static string textboxfetchpassword = ""; // get password textbox to user userprofilepage/usersettings
        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True";
            string query = "SELECT COUNT(1) FROM dbo.customer_tbl WHERE email=@Username AND password=@Password";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", textBox1.Text);
                command.Parameters.AddWithValue("@Password", textBox2.Text);

                connection.Open();
                int result = (int)command.ExecuteScalar();

                if (result == 1)
                {
                    MessageBox.Show("Welcome back", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    welcomeuser = textBox1.Text; //get username textbox to user dashboard 2
                    textboxfetchpassword = textBox2.Text; // get password textbox to user userprofilepage/usersettings
                    this.Hide();
                    CustomerLoginDashboard cdb = new CustomerLoginDashboard();
                    cdb.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect.");
                }
            }
        }
    }
}
