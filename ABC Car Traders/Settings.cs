using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//1

namespace ABC_Car_Traders
{
    public partial class Settings : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public Settings()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Admin_Dashboard frmd = new Admin_Dashboard();
            //frmd.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                confirmpaswd.UseSystemPasswordChar = false;
                password.UseSystemPasswordChar = false;
            }
            else
            {
                confirmpaswd.UseSystemPasswordChar = true;
                password.UseSystemPasswordChar = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password.Text) || string.IsNullOrWhiteSpace(confirmpaswd.Text))
            {
                MessageBox.Show("Password and Confirm Password fields cannot be empty.", "Input Required");
            }
            else if (password.Text == confirmpaswd.Text)
            {
                MessageBox.Show("Password Updated successfully.", "Success");

                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Admin_Login SET username = @usernametxt, password = @passwordtxt", con);
                    cmd.Parameters.AddWithValue("passwordtxt", confirmpaswd.Text);
                    cmd.Parameters.AddWithValue("usernametxt", usrname.Text);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    usrname.Clear();
                    password.Clear();
                    confirmpaswd.Clear();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }
            }
            else
            {
                MessageBox.Show("Check password and Confirm Password. They do not match.", "Not Matching");
            }
        }
    }
}
