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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginDashboard frm = new CustomerLoginDashboard();
            frm.Show();
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

        private void OrderStatusCustomer_Load_1(object sender, EventArgs e)
        {
            loadgridcar();
        }
    }
}
