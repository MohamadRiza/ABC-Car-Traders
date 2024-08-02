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
    public partial class CustomerRegister : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
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
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet4.country_For_Combobox' table. You can move, or remove it, as needed.
            this.country_For_ComboboxTableAdapter.Fill(this.aBC_Car_TradersDataSet4.country_For_Combobox);
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
                txtcname.Clear();
                txtcity.Clear();
                txtaddress.Clear();
                txtmobile.Clear();
                txtemail.Clear();
                txtpassword.Clear();
                combocountry.ResetText();
                checkBox1.Checked = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //********************** insert ************************************
            if (string.IsNullOrWhiteSpace(txtcname.Text) || string.IsNullOrEmpty(combocountry.Text) || string.IsNullOrEmpty(txtcity.Text) || string.IsNullOrEmpty(txtaddress.Text) || string.IsNullOrEmpty(txtmobile.Text) || string.IsNullOrEmpty(txtemail.Text) || string.IsNullOrEmpty(txtpassword.Text))
            {//this if else use for make sure text boxes are not empty
                MessageBox.Show("textboxes are empty!");
            }
            else
            {


                try
                {

                    // Open the connection
                    con.Open();

                    // Create the SQL command
                    SqlCommand cmd = new SqlCommand("INSERT INTO customer_tbl (fullname, country, city, address, mobile, email, password) VALUES (@fname, @country, @city, @address, @mobile, @email, @password)", con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@fname", txtcname.Text);
                    cmd.Parameters.AddWithValue("@country", combocountry.Text);
                    cmd.Parameters.AddWithValue("@city", txtcity.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtmobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);


                    // Execute the command
                    cmd.ExecuteNonQuery();

                    // Close the connection
                    con.Close();

                    // Show success message
                    if(MessageBox.Show("customer details saved! do you want to login?", "Success", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        this.Hide();
                        LoginUser frm = new LoginUser();
                        frm.Show();
                    }

                    txtcname.Clear();
                    txtcity.Clear();
                    txtaddress.Clear();
                    txtmobile.Clear();
                    txtemail.Clear();
                    txtpassword.Clear();
                    combocountry.ResetText();
                    checkBox1.Checked = false;
                }
                catch (Exception ex)
                {
                    // Handle the exception and show error message
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                finally
                {
                    // Ensure the connection is closed
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }//END Insert Here
            }
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
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+';

        }
    }
}
