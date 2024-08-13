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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Xml.Linq;

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
        private void loadtable()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM managecars_tbl", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
        }
        private void clearTextboxed()
        {//clear all textboxed + images(picturebox = empty)
            txtid.Clear();
            txtbrand.Clear();
            txtmodel.Clear();
            //comboyear.ResetText();
            comboyear.SelectedItem = null;
            txtprice.Clear();
            txtstock.Clear();
            richtxtdescription.Clear();
            pictureBox1.Image = null;
            textBox1.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frma = new Admin_Dashboard();
            frma.ShowDialog();
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

            //this.AcceptButton = this.button1;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //********************** insert ************************************
            if (string.IsNullOrWhiteSpace(txtbrand.Text) || string.IsNullOrEmpty(txtmodel.Text) || string.IsNullOrEmpty(comboyear.Text) || string.IsNullOrEmpty(txtprice.Text) || string.IsNullOrEmpty(txtstock.Text))
            {//this if else use for make sure text boxes are not empty
                MessageBox.Show("textboxes are empty!");
            }
            else
            {


                try
                {
                    // Convert image to byte array
                    Image img = new Bitmap(pictureBox1.Image);
                    byte[] arr;
                    //nnow updated for update db
                    using (MemoryStream ms = new MemoryStream())
                    {
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        arr = ms.ToArray();
                    }

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
                    clearTextboxed();
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

        private void button1_Click(object sender, EventArgs e)
        {//this code for search button above the datagrid view //search by car brand
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string srch = textBox1.Text;
                cmd.CommandText = "SELECT * FROM managecars_tbl WHERE brand like '%" + srch + "%' " ;
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                clearTextboxed();
                if(count == 0)
                {
                    MessageBox.Show("Record Not Found!");
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex);
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            clearTextboxed();
            int count = 0;
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            string srch = textBox1.Text;
            cmd.CommandText = "SELECT * FROM managecars_tbl";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            count = Convert.ToInt32(dt.Rows.Count.ToString());
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please select an image before updating.","Select");
                    return;
                }

                Image img = new Bitmap(pictureBox1.Image);
                byte[] arr;

                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    arr = ms.ToArray();
                }

                con.Open();

                // Parameterized query for updating data
                SqlCommand cmd = new SqlCommand("UPDATE managecars_tbl SET brand = @brand, model = @model, year = @year, price = @price, stock = @stock, description = @description, picture = @picture WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@brand", txtbrand.Text);
                cmd.Parameters.AddWithValue("@model", txtmodel.Text);
                cmd.Parameters.AddWithValue("@year", comboyear.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@stock", txtstock.Text);
                cmd.Parameters.AddWithValue("@description", richtxtdescription.Text);
                cmd.Parameters.AddWithValue("@picture", arr);
                cmd.Parameters.AddWithValue("@id", txtid.Text);

                cmd.ExecuteNonQuery();
                loadtable();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                clearTextboxed();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure do you want to Delete?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtid.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE managecars_tbl WHERE id = '" + int.Parse(txtid.Text) + "'", con);
                    cmd.ExecuteNonQuery();
                    loadtable();
                    MessageBox.Show("Successfully Deleted!", "Deleted...!");
                    con.Close();
                    clearTextboxed();
                }
                else
                {
                    MessageBox.Show("Datas are not selected!");
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
