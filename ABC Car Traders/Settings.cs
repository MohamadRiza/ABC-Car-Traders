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
        public void loadtable()
        {
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM country_For_Combobox", con);
            DataTable dt = new DataTable();
            dt.Load(cmd2.ExecuteReader());
            dataGridView1.DataSource = dt;
        }
        public void cleartextboxes()
        {
            txtid.Clear();
            txtcountry.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frmd = new Admin_Dashboard();
            frmd.Show();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcountry.Text))
            {
                MessageBox.Show("textbox Country is Empty!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO country_For_Combobox (country) VALUES (@country)",con);
                    cmd.Parameters.AddWithValue("@country", txtcountry.Text);
                    cmd.ExecuteNonQuery();
                    loadtable();
                    con.Close();
                    MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartextboxes();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet3.country_For_Combobox' table. You can move, or remove it, as needed.
            this.country_For_ComboboxTableAdapter.Fill(this.aBC_Car_TradersDataSet3.country_For_Combobox);
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Select a Country");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE country_For_Combobox SET country = @country WHERE id = @id", con);
                    cmd.Parameters.AddWithValue("@country", txtcountry.Text);
                    cmd.Parameters.AddWithValue("@id", txtid.Text);
                    cmd.ExecuteNonQuery();
                    loadtable();
                    con.Close();
                    MessageBox.Show("Successfully Updated!");
                    cleartextboxes();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            txtid.Text = selectedRow.Cells[0].Value.ToString();
            txtcountry.Text = selectedRow.Cells[1].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Select a Record!", "ID Requeired!");
            }
            else
            {
                if (MessageBox.Show("Are you sure do you want to delete this record?", "Delete Request", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE country_For_Combobox WHERE id = '" + int.Parse(txtid.Text) + "'", con);
                        cmd.ExecuteNonQuery();
                        loadtable();
                        MessageBox.Show("Successfully Deleted!", "Deleted!");
                        con.Close();
                        cleartextboxes();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            cleartextboxes();
        }
    }
}
