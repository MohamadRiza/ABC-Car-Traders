using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//1

namespace ABC_Car_Traders
{
    public partial class BuycarsClickCarDGV : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public BuycarsClickCarDGV()
        {
            InitializeComponent();
        }
        private void BuycarsClickCarDGV_Load(object sender, EventArgs e)
        {
            //panel background transperant85
            panel1.BackColor = Color.FromArgb(185, Color.Black);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string recipientEmail = "recipient@example.com";
            string gmailUrl = $"https://mail.google.com/mail/?view=cm&fs=1&to={recipientEmail}";

            try
            {
                Process.Start(new ProcessStartInfo(gmailUrl) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Your shipping details is 100% correct?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                // update Stock -1
                SqlCommand cmd = new SqlCommand("UPDATE managecars_tbl SET stock = stock - 1  WHERE id = @id", con);
                cmd.Parameters.AddWithValue("@id", txtcarid.Text);
                cmd.ExecuteNonQuery();

                //chack Stock
                SqlCommand cmd2 = new SqlCommand("SELECT stock FROM managecars_tbl WHERE id = @id", con);
                cmd2.Parameters.AddWithValue("@id", txtcarid.Text);
                int updatedStock = (int)cmd2.ExecuteScalar(); // Getting the updated stock value

                if (updatedStock <= 0)
                {
                    MessageBox.Show("Stock is empty. Unable to process further orders.");
                }
                else
                {

                    //cmd4
                    SqlCommand cmd4 = new SqlCommand("select cid from customer_tbl where email = @Wemail and password = @Wpassword", con);
                    cmd4.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
                    cmd4.Parameters.AddWithValue("@Wpassword", LoginUser.textboxfetchpassword);
                    SqlDataReader DR = cmd4.ExecuteReader();

                    int CustomerID = 0;
                    if (DR.Read())
                    {
                        CustomerID = (int)DR["cid"];
                    }
                    DR.Close();

                    //save customer order details
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO Customer_Orders_A_Car (CarID, customerID, orderDateTime, OrderStatus) VALUES (@CarID, @customerID, @OrderDateTime, @OrderStatus)", con);
                    cmd3.Parameters.AddWithValue("@CarID", txtcarid.Text);
                    cmd3.Parameters.AddWithValue("@customerID", CustomerID);//add here customer id
                    cmd3.Parameters.AddWithValue("@OrderDateTime", DateTime.Now);
                    cmd3.Parameters.AddWithValue("@OrderStatus", "Order Confirmed! Waiting to Ship");
                    cmd3.ExecuteNonQuery();
                   


                    MessageBox.Show("Order confirmed!");

                    con.Close();
                }
            }
            else if(MessageBox.Show("do you want to change details?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CustomerProfileinDshBD frm = new CustomerProfileinDshBD();
                frm.Show();
            }
        }
    }
}
