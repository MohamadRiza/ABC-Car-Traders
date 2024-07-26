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
            combocountry.SelectedItem = selectedRow.Cells[2].Value.ToString();
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
                    SqlCommand cmd2 = new SqlCommand("SELECT * FROM customer_tbl", con);
                    DataTable dt = new DataTable();
                    dt.Load(cmd2.ExecuteReader());
                    dataGridView1.DataSource = dt;

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

        }
    }
}
