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
    public partial class BuyACarPartasACustomer : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public BuyACarPartasACustomer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void comboBrandPV()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct (carbrand) from manageparts", con);
                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Columns.Add("carbrand", typeof(string));
                dt.Load(reader);
                combobrands.ValueMember = "carbrand";
                combobrands.DataSource = dt;
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void comboyearPV()
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select distinct (year) from manageparts", con);
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

        private void BuyACarPartasACustomer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet12.manageparts' table. You can move, or remove it, as needed.
            this.managepartsTableAdapter.Fill(this.aBC_Car_TradersDataSet12.manageparts);
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet11.manageparts' table. You can move, or remove it, as needed.

            comboBrandPV();
            comboyearPV();

            comboyear.SelectedItem = null;
            combobrands.SelectedItem = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginDashboard frm = new CustomerLoginDashboard();
            frm.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                BuyPartsClickPartsDGV frm1 = new BuyPartsClickPartsDGV();

                frm1.txtpartid.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                frm1.txtpartname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm1.txtbrand.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm1.txtmodel.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                frm1.txtyear.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                frm1.txtprice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                frm1.txtstock.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                frm1.richtxtdescription.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                // Fetch and display image from the database
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                FetchAndDisplayImage(productId, frm1);

                frm1.ShowDialog();
            }
        }

        private void FetchAndDisplayImage(int productId, BuyPartsClickPartsDGV frm1)
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
                        frm1.pictureBox1.Image = ByteArrayToImage(imageData);
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

        private void button5_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from manageparts",con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();

            combobrands.SelectedItem = null;
            comboyear.SelectedItem = null;
            txtsrchmodel.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(combobrands.Text) || string.IsNullOrEmpty(comboyear.Text))
            {
                MessageBox.Show("Select brand and year for filter specific part", "Empty...");
            }
            else
            {
                try
                {
                    int count = 0;
                    con.Open();
                    SqlCommand cmd = new SqlCommand("select * from manageparts where carbrand = @Wbrand and year = @Wyear", con);
                    cmd.Parameters.AddWithValue("@Wbrand", combobrands.Text);
                    cmd.Parameters.AddWithValue("@Wyear", comboyear.Text);
                    cmd.ExecuteNonQuery();

                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    count = Convert.ToInt32(dt.Rows.Count.ToString());
                    dataGridView1.DataSource = dt;

                    con.Close();
                    txtsrchmodel.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {//using 
            try
            {
                int count = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                string srch = txtsrchmodel.Text;
                cmd.CommandText = "SELECT * FROM manageparts WHERE partname LIKE @search OR carmodel LIKE @search";
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
                    txtsrchmodel.Clear();
                }
                combobrands.SelectedItem = null;
                comboyear.SelectedItem = null;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }
    }
}
