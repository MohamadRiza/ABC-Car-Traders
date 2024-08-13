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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace ABC_Car_Traders
{
    public partial class BuyaCarasACustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public BuyaCarasACustomer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void comboboxbrand()
        {//for brand combobox
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct (brand) FROM managecars_tbl", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("brand", typeof(string));
                dt.Load(reader);
                combobrand.ValueMember = "brand";
                combobrand.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }
        private void comboboxyear()
        {//for car year combobox
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT distinct (year) FROM managecars_tbl", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("year", typeof(string));
                dt.Load(reader);
                comboyear.ValueMember = "year";
                comboyear.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginDashboard frm = new CustomerLoginDashboard();
            frm.ShowDialog();
        }

        private void BuyaCarasACustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet10.managecars_tbl' table. You can move, or remove it, as needed.
            this.managecars_tblTableAdapter.Fill(this.aBC_Car_TradersDataSet10.managecars_tbl);
            panel4.BackColor = Color.FromArgb(185, Color.Black);
            
            //get data from DB and fetch without data duplication
            comboboxbrand();
            comboboxyear();
            combobrand.SelectedItem = null;
            comboyear.SelectedItem = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(combobrand.Text) || string.IsNullOrEmpty(comboyear.Text))
            {
                MessageBox.Show("select a car brand and year","Empty");
            }
            else
            {
                try
                {
                    int count = 0;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from managecars_tbl where brand = @cbrand and year = @cyear", con);
                    cmd.Parameters.AddWithValue("@cbrand", combobrand.Text);
                    cmd.Parameters.AddWithValue("@cyear", comboyear.Text);
                    cmd.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    count = Convert.ToInt32(dt.Rows.Count.ToString());
                    dataGridView1.DataSource = dt;

                    con.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            { 
                BuycarsClickCarDGV frm = new BuycarsClickCarDGV();

                frm.txtcarid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                frm.txtbrand.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtmodel.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtyear.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm.txtprice.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm.txtstock.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm.richtxtdescription.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();

                // Fetch and display image from the database
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                FetchAndDisplayImage(productId, frm);

                frm.ShowDialog();
            }
        }
        //---

        private void FetchAndDisplayImage(int productId, BuycarsClickCarDGV frm)
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
                        frm.pictureBox1.Image = ByteArrayToImage(imageData);
                    }
                    else
                    {
                        MessageBox.Show("Image not found for the specified Product ID.");
                    }
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        //----
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }


        private void button5_Click_1(object sender, EventArgs e)
        {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM managecars_tbl", con);
                DataTable dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dataGridView1.DataSource = dt;
                con.Close();
                combobrand.SelectedItem = null;
                comboyear.SelectedItem = null;
                textBox1.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string srch = textBox1.Text;
                cmd.CommandText = "select * from managecars_tbl where model like @search";
                cmd.Parameters.AddWithValue("@search", "%" + srch + "%");

                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                count = Convert.ToInt32(dt.Rows.Count.ToString());
                dataGridView1.DataSource = dt;
                con.Close();
                if (count == 0)
                {
                    MessageBox.Show("Record Not Found!");
                    textBox1.Clear();
                }
                combobrand.SelectedItem = null;
                comboyear.SelectedItem = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }
    }
}
