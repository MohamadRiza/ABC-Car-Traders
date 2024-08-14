﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;//1
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ABC_Car_Traders
{
    public partial class ManageCustomerOrdersAsaAdmin : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public ManageCustomerOrdersAsaAdmin()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }
        private void loadtablecars()
        {
            SqlCommand cmd2 = new SqlCommand("SELECT * FROM Customer_Orders_A_Car", con);
            DataTable dt = new DataTable();
            dt.Load(cmd2.ExecuteReader());
            dataGridView1.DataSource = dt;
        }
        private void cleartextboxescars()
        {
            combofiltercar.SelectedItem = null;
            comboupdatecars.SelectedItem = null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frm = new Admin_Dashboard();
            frm.ShowDialog();
        }

        private void ManageCustomerOrdersAsaAdmin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet18.Customer_Orders_A_Car' table. You can move, or remove it, as needed.
            this.customer_Orders_A_CarTableAdapter1.Fill(this.aBC_Car_TradersDataSet18.Customer_Orders_A_Car);
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet17.Customer_Orders_A_Part' table. You can move, or remove it, as needed.
            this.customer_Orders_A_PartTableAdapter1.Fill(this.aBC_Car_TradersDataSet17.Customer_Orders_A_Part);
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet16.Customer_Orders_A_Part' table. You can move, or remove it, as needed.
            this.customer_Orders_A_PartTableAdapter.Fill(this.aBC_Car_TradersDataSet16.Customer_Orders_A_Part);
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet15.Customer_Orders_A_Car' table. You can move, or remove it, as needed.
            this.customer_Orders_A_CarTableAdapter.Fill(this.aBC_Car_TradersDataSet15.Customer_Orders_A_Car);
            cleartextboxescars();
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel5.BackColor = Color.FromArgb(185, Color.Black);

        }

        private void btnfiltercar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(combofiltercar.Text))
            {
                MessageBox.Show("Select a Status", "Empty!");
            }
            else
            {
                try
                {
                    int count = 0;
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    string srch = combofiltercar.Text;
                    cmd.CommandText = "SELECT * FROM Customer_Orders_A_Car WHERE OrderStatus like '%" + srch + "%' ";
                    cmd.ExecuteNonQuery();
                    DataTable dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    count = Convert.ToInt32(dt.Rows.Count.ToString());
                    dataGridView1.DataSource = dt;
                    con.Close();
                    cleartextboxescars();
                    if (count == 0)
                    {
                        MessageBox.Show("Record Not Found!");
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[index];

            combofiltercar.SelectedItem = selectedRow.Cells[3].Value.ToString();
            comboupdatecars.SelectedItem = selectedRow.Cells[4].Value.ToString();
            string ordid = selectedRow.Cells[0].Value.ToString();

            selectedOrdid = ordid;
            
        }
        //for get ordid to below
        private string selectedOrdid;

        private void btnupdatecar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedOrdid))
            {
                MessageBox.Show("Select a order first", "Not Selected");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE Customer_Orders_A_Car SET OrderStatus = @OSC WHERE O_id = @OID", con);
                    cmd.Parameters.AddWithValue("@OSC", comboupdatecars.Text);
                    cmd.Parameters.AddWithValue("@OID", selectedOrdid);
                    cmd.ExecuteNonQuery();
                    loadtablecars();
                    con.Close();
                    MessageBox.Show("Successfully Updated Order Status");
                    cleartextboxescars();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            loadtablecars();
            con.Close();
            cleartextboxescars();
        }
    }
}
