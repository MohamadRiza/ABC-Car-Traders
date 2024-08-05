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
                comboBox1.ValueMember = "brand";
                comboBox1.DataSource = dt;
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
                comboBox3.ValueMember = "year";
                comboBox3.DataSource = dt;
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
            frm.Show();
        }

        private void BuyaCarasACustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet10.managecars_tbl' table. You can move, or remove it, as needed.
            this.managecars_tblTableAdapter.Fill(this.aBC_Car_TradersDataSet10.managecars_tbl);
            panel4.BackColor = Color.FromArgb(185, Color.Black);
            
            //get data from DB and fetch without data duplication
            comboboxbrand();
            comboboxyear();
            comboBox1.SelectedItem = null;
            comboBox3.SelectedItem = null;
        }

        private void button3_Click(object sender, EventArgs e)
        {

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
    }
}
