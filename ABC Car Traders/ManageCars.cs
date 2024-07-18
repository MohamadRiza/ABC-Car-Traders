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
using System.Diagnostics;
using System.IO;

namespace ABC_Car_Traders
{
    public partial class ManageCars : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public ManageCars()
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
            Admin_Dashboard frma = new Admin_Dashboard();
            frma.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private void ManageCars_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet.managecars_tbl' table. You can move, or remove it, as needed.
            this.managecars_tblTableAdapter.Fill(this.aBC_Car_TradersDataSet.managecars_tbl);
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //********************** insert ************************************
            try
            {
                // Convert image to byte array
                Image img = pictureBox1.Image;
                byte[] arr;
                ImageConverter converter = new ImageConverter();
                arr = (byte[])converter.ConvertTo(img, typeof(byte[]));

                // Open the connection
                con.Open();

                // Create the SQL command
                SqlCommand cmd = new SqlCommand("INSERT INTO managecars_tbl (brand, model, year, price, stock, description, picture) VALUES (@brand, @model, @year, @price, @stock, @description, @picture)", con);

                // Add parameters
                cmd.Parameters.AddWithValue("@brand", txtbrand.Text);
                cmd.Parameters.AddWithValue("@model", txtmodel.Text);
                cmd.Parameters.AddWithValue("@year", comboyear.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@stock", txtstock.Text);
                cmd.Parameters.AddWithValue("@description", richtxtdescription.Text);
                cmd.Parameters.AddWithValue("@picture", arr);

                // Execute the command
                cmd.ExecuteNonQuery();

                //load all datas to datagrid view
                SqlCommand cmd2 = new SqlCommand("SELECT * FROM managecars_tbl", con);
                DataTable dt = new DataTable();
                dt.Load(cmd2.ExecuteReader());
                dataGridView1.DataSource = dt;

                // Close the connection
                con.Close();

                // Show success message
                MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbrand.Clear();
                txtmodel.Clear();
                comboyear.ResetText();
                txtprice.Clear();
                txtstock.Clear();
                richtxtdescription.Clear();
                pictureBox1.Image = null;
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            txtid.Text = selectedRow.Cells[0].Value.ToString();
            txtbrand.Text = selectedRow.Cells[1].Value.ToString();
            txtmodel.Text = selectedRow.Cells[2].Value.ToString();
            comboyear.SelectedItem = selectedRow.Cells[3].Value.ToString();
            txtprice.Text = selectedRow.Cells[4].Value.ToString();
            txtstock.Text = selectedRow.Cells[5].Value.ToString();
            richtxtdescription.Text = selectedRow.Cells[6].Value.ToString();
            //below is for picture fetch again to picturebox from database
            int productId = Convert.ToInt32(selectedRow.Cells[0].Value);
            FetchAndDisplayImage(productId);

        }
        private void FetchAndDisplayImage(int productId)//Database to fetch car image again to picturebox
        {
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
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
    }
}
