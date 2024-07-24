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
    public partial class ManageParts : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public ManageParts()
        {
            InitializeComponent();
        }
        private void cleartextbox()
        {
            txtpartid.Clear();
            txtname.Clear();
            txtbrand.Clear();
            txtmodel.Clear();
            txtprice.Clear();
            txtstock.Clear();
            richtxtdescription.Clear();
            textBox1.Clear();
            pictureBox1.Image = null;
            comboyear.ResetText();

        }
        private void loadtable()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM manageparts", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //********************** insert ************************************
            if (string.IsNullOrWhiteSpace(txtname.Text) || string.IsNullOrEmpty(txtmodel.Text) || string.IsNullOrEmpty(txtbrand.Text) || string.IsNullOrEmpty(comboyear.Text) || string.IsNullOrEmpty(txtprice.Text) || string.IsNullOrEmpty(txtstock.Text))
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO manageparts (partname, carbrand, carmodel, year, price, stock, description, picture) VALUES (@partname, @carbrand, @carmodel, @year, @price, @stock, @description, @picture)", con);

                    // Add parameters
                    cmd.Parameters.AddWithValue("@partname", txtname.Text);
                    cmd.Parameters.AddWithValue("@carbrand", txtbrand.Text);
                    cmd.Parameters.AddWithValue("@carmodel", txtmodel.Text);
                    cmd.Parameters.AddWithValue("@year", comboyear.Text);
                    cmd.Parameters.AddWithValue("@price", txtprice.Text);
                    cmd.Parameters.AddWithValue("@stock", txtstock.Text);
                    cmd.Parameters.AddWithValue("@description", richtxtdescription.Text);
                    cmd.Parameters.AddWithValue("@picture", arr);

                    // Execute the command
                    cmd.ExecuteNonQuery();

                    //load all datas to datagrid view
                    loadtable();

                    // Close the connection
                    con.Close();

                    // Show success message
                    MessageBox.Show("Data Inserted Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cleartextbox();
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
            cleartextbox();
        }

        private void ManageParts_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet1.manageparts' table. You can move, or remove it, as needed.
            this.managepartsTableAdapter.Fill(this.aBC_Car_TradersDataSet1.manageparts);
            panel6.BackColor = Color.FromArgb(185, Color.Black);
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
            panel7.BackColor = Color.FromArgb(185, Color.Black);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(openFileDialog.FileName);
            }
        }

        private Image ByteArrayToImage(byte[] byteArray)//this is also for fetch image 
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }

            private void FetchAndDisplayImage(int productId)//Database to fetch car image again to picturebox
        {
            // Connection string to your database
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            txtpartid.Text = selectedRow.Cells[0].Value.ToString();
            txtname.Text = selectedRow.Cells[1].Value.ToString();
            txtbrand.Text = selectedRow.Cells[2].Value.ToString();
            txtmodel.Text = selectedRow.Cells[3].Value.ToString();
            comboyear.Text = selectedRow.Cells[4].Value.ToString();
            txtprice.Text = selectedRow.Cells[5].Value.ToString();
            txtstock.Text = selectedRow.Cells[6].Value.ToString();
            richtxtdescription.Text = selectedRow.Cells[7].Value.ToString();

            int productId = Convert.ToInt32(selectedRow.Cells[0].Value);
            FetchAndDisplayImage(productId);
        }

        private void button4_Click(object sender, EventArgs e)
        {//update btn
            try
            {
                if (pictureBox1.Image == null)
                {
                    MessageBox.Show("Please select an image before updating.", "Select");
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
                SqlCommand cmd = new SqlCommand("UPDATE manageparts SET partname = @partname, carbrand = @carbrand, carmodel = @carmodel, year = @year, price = @price, stock = @stock, description = @description,picture = @picture WHERE partid = @id", con);
                cmd.Parameters.AddWithValue("@partname", txtname.Text);
                cmd.Parameters.AddWithValue("@carbrand", txtbrand.Text);
                cmd.Parameters.AddWithValue("@carmodel", txtmodel.Text);
                cmd.Parameters.AddWithValue("@year", comboyear.Text);
                cmd.Parameters.AddWithValue("@price", txtprice.Text);
                cmd.Parameters.AddWithValue("@stock", txtstock.Text);
                cmd.Parameters.AddWithValue("@description", richtxtdescription.Text);
                cmd.Parameters.AddWithValue("@picture", arr);
                cmd.Parameters.AddWithValue("@id", txtpartid.Text);

                cmd.ExecuteNonQuery();
                loadtable();
                con.Close();
                MessageBox.Show("Successfully Updated!");

                cleartextbox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {//search btn
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string srch = textBox1.Text;
                cmd.CommandText = "SELECT * FROM manageparts WHERE partname like '%" + srch + "%' ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                cleartextbox();
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

        private void button7_Click(object sender, EventArgs e)
        {//delete btn
            if (MessageBox.Show("Are you sure do you want to Delete?", "Delete Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (txtpartid.Text != "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE manageparts WHERE partid = '" + int.Parse(txtpartid.Text) + "'", con);
                    cmd.ExecuteNonQuery();
                    loadtable();
                    MessageBox.Show("Successfully Deleted!", "Deleted...!");
                    con.Close();
                    cleartextbox();
                }
                else
                {
                    MessageBox.Show("Datas are not selected!");
                }
            }
        }
    }
}
