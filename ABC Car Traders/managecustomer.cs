using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//1

namespace ABC_Car_Traders
{
    public partial class managecustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public managecustomer()
        {
            InitializeComponent();
        }

        private void managecustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet6.country_For_Combobox' table. You can move, or remove it, as needed.
            this.country_For_ComboboxTableAdapter.Fill(this.aBC_Car_TradersDataSet6.country_For_Combobox);
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet5.country_For_Combobox' table. You can move, or remove it, as needed.
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet2.customer_tbl' table. You can move, or remove it, as needed.
            this.customer_tblTableAdapter.Fill(this.aBC_Car_TradersDataSet2.customer_tbl);
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
        }
        private void cleartextboxes()
        {
            txtid.Clear();
            txtcname.Clear();
            //combocountry.ResetText();
            combocountry.SelectedItem = null;
            txtcity.Clear();
            txtaddress.Clear();
            txtmobile.Clear();
            txtemail.Clear();
            txtpassword.Clear();
            checkBox1.Checked = false; //remove chacked chackbox
        }
        private void loadtable()
        {
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM customer_tbl", con);
            DataTable dt = new DataTable();
            dt.Load(cmd2.ExecuteReader());
            dataGridView1.DataSource = dt;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frm = new Admin_Dashboard();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cleartextboxes();
            int count = 0;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string srch = textBox1.Text;
            cmd.CommandText = "SELECT * FROM customer_tbl";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            txtid.Text = selectedRow.Cells[0].Value.ToString();
            txtcname.Text = selectedRow.Cells[1].Value.ToString();
            combocountry.Text = selectedRow.Cells[2].Value.ToString();
            txtcity.Text = selectedRow.Cells[3].Value.ToString();
            txtaddress.Text = selectedRow.Cells[4].Value.ToString();
            txtmobile.Text = selectedRow.Cells[5].Value.ToString();
            txtemail.Text = selectedRow.Cells[6].Value.ToString();
            txtpassword.Text = selectedRow.Cells[7].Value.ToString();

            
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //********************** insert ************************************
            if (string.IsNullOrWhiteSpace(txtcname.Text) || string.IsNullOrEmpty(combocountry.Text) || string.IsNullOrEmpty(txtcity.Text) || string.IsNullOrEmpty(txtaddress.Text) || string.IsNullOrEmpty(txtmobile.Text) || string.IsNullOrEmpty(txtemail.Text))
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO customer_tbl (fullname, country, city, address, mobile, email, password) VALUES (@fullname, @country, @city, @address, @mobile, @email, @password)", con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@fullname", txtcname.Text);
                    cmd.Parameters.AddWithValue("@country", combocountry.Text);
                    cmd.Parameters.AddWithValue("@city", txtcity.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtmobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);


                    // Execute the command
                    cmd.ExecuteNonQuery();

                    //load all datas to datagrid view
                    loadtable();

                    // Close the connection
                    con.Close();

                    // Show success message
                    MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartextboxes();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("Select a Customer", "ID is not be Empty!");
            }
            else
            {
                try
                {
                    con.Open();

                    // Parameterized query for updating data
                    SqlCommand cmd = new SqlCommand("UPDATE customer_tbl SET fullname = @fullname, country = @country, city = @city, address = @address, mobile = @mobile, email = @email, password = @password WHERE cid = @cid", con);

                    cmd.Parameters.AddWithValue("@fullname", txtcname.Text);
                    cmd.Parameters.AddWithValue("@country", combocountry.Text);
                    cmd.Parameters.AddWithValue("@city", txtcity.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtmobile.Text);
                    cmd.Parameters.AddWithValue("@email", txtemail.Text);
                    cmd.Parameters.AddWithValue("@password", txtpassword.Text);
                    cmd.Parameters.AddWithValue("@cid", txtid.Text);

                    cmd.ExecuteNonQuery();
                    loadtable();
                    con.Close();
                    MessageBox.Show("Successfully Updated!");

                    cleartextboxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+';
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtid.Text))
            {
                MessageBox.Show("text boxes are empty!", "ID requeired");
            }
            else
            {
                if (MessageBox.Show("Are you sure do you want to delete this customer?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE customer_tbl WHERE cid = '" + int.Parse(txtid.Text) + "'", con);
                        cmd.ExecuteNonQuery();
                        loadtable();
                        MessageBox.Show("Successfully Deleted", "Deleted...!");
                        con.Close();
                        cleartextboxes();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string srch = textBox1.Text;
                cmd.CommandText = "SELECT * FROM customer_tbl WHERE fullname like '%" + srch + "%' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                cleartextboxes();
                if (count == 0)
                {
                    MessageBox.Show("Record Not Found!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }
    }
}
