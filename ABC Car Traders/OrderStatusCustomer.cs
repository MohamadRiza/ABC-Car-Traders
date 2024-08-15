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
using System.IO;

namespace ABC_Car_Traders
{
    public partial class OrderStatusCustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public OrderStatusCustomer()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void loadcarstbl()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM managecars_tbl", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }
        private void loadpartstbl()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM manageparts", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginDashboard frm = new CustomerLoginDashboard();
            frm.ShowDialog();
        }

        private void OrderStatusCustomer_Load(object sender, EventArgs e)
        {

        }
        private void loadgridcar()
        {
            con.Open();

            // Retrieve the Customer ID based on the logged-in user's email and password
            SqlCommand cmd2 = new SqlCommand("SELECT cid FROM customer_tbl WHERE email = @Wemail AND password = @psswd", con);
            cmd2.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
            cmd2.Parameters.AddWithValue("@psswd", LoginUser.textboxfetchpassword);

            SqlDataReader reader = cmd2.ExecuteReader();
            int customerId = 0;
            if (reader.Read())
            {
                customerId = (int)reader["cid"];
            }
            reader.Close();
            if (customerId > 0)
            {
                // Fetch orders specific to the logged-in customer
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer_Orders_A_Car WHERE customerID = @customerId", con);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No orders found for this customer.");
            }

            con.Close();

        }
        private void loadgridpart()
        {
            con.Open();

            SqlCommand cmd2 = new SqlCommand("SELECT cid FROM customer_tbl WHERE email = @Wemail AND password = @psswd", con);
            cmd2.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
            cmd2.Parameters.AddWithValue("@psswd", LoginUser.textboxfetchpassword);

            SqlDataReader reader = cmd2.ExecuteReader();
            int customerId = 0;
            if (reader.Read())
            {
                customerId = (int)reader["cid"];
            }
            reader.Close();
            if (customerId > 0)
            {
                // Fetch orders specific to the logged-in customer
                SqlCommand cmd = new SqlCommand("SELECT * FROM Customer_Orders_A_Part WHERE CustomerID = @customerId", con);
                cmd.Parameters.AddWithValue("@customerId", customerId);

                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView2.DataSource = dt;
            }
            else
            {
                MessageBox.Show("No orders found for this customer.");
            }

            con.Close();
        }

        private void OrderStatusCustomer_Load_1(object sender, EventArgs e)
        {
            panel7.BackColor = Color.FromArgb(185, Color.Black);
            loadgridcar();
            loadgridpart();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //cars cell click
            int index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView1.Rows[index];

            labeldeliverystatus.Text = selectedrow.Cells[4].Value.ToString();
            labelOrdDate.Text = selectedrow.Cells[3].Value.ToString();
            labelORDID.Text = selectedrow.Cells[0].Value.ToString();

            //below is for fetch image from database and show from order status form
            int productId = Convert.ToInt32(selectedrow.Cells[1].Value);
            FetchAndDisplayImage(productId);

            loadgridpart();
        }
        private void FetchAndDisplayImage(int productId)//Database to fetch car image again to picturebox
        {//this is for datagridview cell click IMAGE
            // Connection string to your database
            string connectionString = "Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True";

            // SQL query to fetch the image data
            string query = "SELECT picture FROM managecars_tbl WHERE id = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);

                    connection.Open();
                    byte[] imageData = command.ExecuteScalar() as byte[];
                    connection.Close();

                    if (imageData != null)
                    {
                        pictureBox1.Image = ByteArrayToImage(imageData);
                    }
                    else
                    {
                        MessageBox.Show("Image not found for the specified Product ID.");
                    }
                }
            }
        }
        private Image ByteArrayToImage(byte[] byteArray)//this is also for fetch image 
        {//this is for datagridview cell click IMAGE
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //parts cell click
            int index = e.RowIndex;
            DataGridViewRow selectedrow = dataGridView2.Rows[index];

            labelORDID.Text = selectedrow.Cells[0].Value.ToString();
            labeldeliverystatus.Text = selectedrow.Cells[4].Value.ToString();
            labelOrdDate.Text = selectedrow.Cells[3].Value.ToString();

            //below is for fetch image from database and show from order status form
            int productId1 = Convert.ToInt32(selectedrow.Cells[1].Value);
            FetchAndDisplayImage1(productId1);

            loadgridcar();
        }
        private void FetchAndDisplayImage1(int productId)//Database to fetch car image again to picturebox
        {//this is for datagridview cell click IMAGE
            string connectionString = "Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True";

            // SQL query to fetch the image data
            string query = "SELECT picture FROM manageparts WHERE partid = @ProductID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);

                    connection.Open();
                    byte[] imageData = command.ExecuteScalar() as byte[];
                    connection.Close();

                    if (imageData != null)
                    {
                        pictureBox1.Image = ByteArrayToImage(imageData);
                    }
                    else
                    {
                        MessageBox.Show("Image not found for the specified Product ID.");
                    }
                }
            }
        }
    }

}
