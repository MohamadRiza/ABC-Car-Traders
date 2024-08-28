using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ABC_Car_Traders
{
    public partial class GenarateaReport : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public GenarateaReport()
        {
            InitializeComponent();
        }

        private void GenarateaReport_Load(object sender, EventArgs e)
        {
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            panel5.BackColor = Color.FromArgb(185, Color.Black);
            
            //load data for grid view 1 (Cars)
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT co.O_id, co.CarID, m.brand, co.customerID, c.fullname, co.orderDateTime, co.OrderStatus FROM Customer_Orders_A_Car co JOIN managecars_tbl m ON co.CarID = m.id JOIN customer_tbl c ON co.customerID = c.cid", con);
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataGridView1.DataSource = dt;
            con.Close();

            //load data for grid view 2 (Parts)
            con.Open();
            SqlCommand cmd2 = new SqlCommand("SELECT co.O_id, co.PartID, m.partname, co.customerID, c.fullname, co.orderDateTime, co.OrderStatus FROM Customer_Orders_A_Part co JOIN manageparts m ON co.PartID = m.partid JOIN customer_tbl c ON co.customerID = c.cid", con);
            DataTable dt2 = new DataTable();
            dt2.Load(cmd2.ExecuteReader());
            dataGridView2.DataSource = dt2;
            con.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin_Dashboard frm = new Admin_Dashboard();
            frm.ShowDialog();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the click is on a valid row
            {
                // Retrieve the data from the selected row
                string orderId = dataGridView1.Rows[e.RowIndex].Cells["O_id"].Value.ToString();
                string carId = dataGridView1.Rows[e.RowIndex].Cells["CarID"].Value.ToString();
                string brand = dataGridView1.Rows[e.RowIndex].Cells["brand"].Value.ToString();
                string customerId = dataGridView1.Rows[e.RowIndex].Cells["customerID"].Value.ToString();
                string fullName = dataGridView1.Rows[e.RowIndex].Cells["fullname"].Value.ToString();
                string orderDateTime = dataGridView1.Rows[e.RowIndex].Cells["orderDateTime"].Value.ToString();
                string orderStatus = dataGridView1.Rows[e.RowIndex].Cells["OrderStatus"].Value.ToString();

                MessageBox.Show($"Order ID: {orderId}, Car ID: {carId}, Brand: {brand}, Customer ID: {customerId}, Full Name: {fullName}, Order DateTime: {orderDateTime}, Order Status: {orderStatus}");


                // Generate the Crystal Report with the selected data
                GenerateCrystalReport(orderId, carId, brand, customerId, fullName, orderDateTime, orderStatus);
            }
        }
        private void GenerateCrystalReport(string orderId, string carId, string brand, string customerId, string fullName, string orderDateTime, string orderStatus)
        {
            // Create an instance of your Crystal Report
            CrystalReport3 myReport = new CrystalReport3();

            // Create a dataset or datatable to pass to the report
            DataSet myDataSet = new DataSet();
            DataTable myDataTable = new DataTable("CustomerOrders");

            // Define the columns (match these to your report fields)
            myDataTable.Columns.Add("O_id", typeof(string));
            myDataTable.Columns.Add("CarID", typeof(string));
            myDataTable.Columns.Add("brand", typeof(string));
            myDataTable.Columns.Add("customerID", typeof(string));
            myDataTable.Columns.Add("fullname", typeof(string));
            myDataTable.Columns.Add("orderDateTime", typeof(string));
            myDataTable.Columns.Add("OrderStatus", typeof(string));

            // Clear existing rows (if any)
            myDataTable.Clear();

            // Add the selected data to the datatable
            myDataTable.Rows.Add(orderId, carId, brand, customerId, fullName, orderDateTime, orderStatus);

            // Add the datatable to the dataset
            myDataSet.Tables.Add(myDataTable);

            // Set the report's data source
            myReport.SetDataSource(myDataSet);

            // Create a new form to display the CrystalReportViewer
            Form reportForm = new Form();
            CrystalReportViewer crystalReportViewer = new CrystalReportViewer
            {
                Dock = DockStyle.Fill,
                ReportSource = myReport
            };

            // Clear previous data
            crystalReportViewer.ReportSource = null;
            crystalReportViewer.ReportSource = myReport;

            reportForm.Controls.Add(crystalReportViewer);
            reportForm.WindowState = FormWindowState.Maximized;
            reportForm.ShowDialog();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure the click is on a valid row
            {
                string orderId2 = dataGridView2.Rows[e.RowIndex].Cells["O_id"].Value.ToString();
                string partID2 = dataGridView2.Rows[e.RowIndex].Cells["PartID"].Value.ToString();
                string partname = dataGridView2.Rows[e.RowIndex].Cells["partname"].Value.ToString();
                string customerid2 = dataGridView2.Rows[e.RowIndex].Cells["customerID"].Value.ToString();
                string fullname2 = dataGridView2.Rows[e.RowIndex].Cells["fullname"].Value.ToString();
                string ordtime = dataGridView2.Rows[e.RowIndex].Cells["orderDateTime"].Value.ToString();
                string ostatus = dataGridView2.Rows[e.RowIndex].Cells["OrderStatus"].Value.ToString();

                MessageBox.Show($"Order ID: {orderId2}, part ID: {partID2}, partname: {partname}, Customer ID: {customerid2}, Full Name: {fullname2}, Order DateTime: {ordtime}, Order Status: {ostatus}");
                GenerateCrystalReport2(orderId2, partID2, partname, customerid2, fullname2, ordtime, ostatus);

            }
        }
        private void GenerateCrystalReport2(string orderId, string partId, string partName, string customerId, string fullName, string orderDateTime, string orderStatus)
        {
            // Create an instance of your Crystal Report
            CrystalReport3 myReport = new CrystalReport3();

            // Create a dataset or datatable to pass to the report
            DataSet myDataSet = new DataSet();
            DataTable myDataTable = new DataTable("CustomerOrders");

            // Define the columns (match these to your report fields)
            myDataTable.Columns.Add("O_id", typeof(string));
            myDataTable.Columns.Add("PartID", typeof(string));
            myDataTable.Columns.Add("partname", typeof(string));
            myDataTable.Columns.Add("customerID", typeof(string));
            myDataTable.Columns.Add("fullname", typeof(string));
            myDataTable.Columns.Add("orderDateTime", typeof(string));
            myDataTable.Columns.Add("OrderStatus", typeof(string));

            // Clear existing rows (if any)
            myDataTable.Clear();

            // Add the selected data to the datatable
            myDataTable.Rows.Add(orderId, partId, partName, customerId, fullName, orderDateTime, orderStatus);

            // Add the datatable to the dataset
            myDataSet.Tables.Add(myDataTable);

            // Set the report's data source
            myReport.SetDataSource(myDataSet);

            // Create a new form to display the CrystalReportViewer
            Form reportForm = new Form();
            CrystalReportViewer crystalReportViewer = new CrystalReportViewer
            {
                Dock = DockStyle.Fill,
                ReportSource = myReport
            };

            reportForm.Controls.Add(crystalReportViewer);
            reportForm.WindowState = FormWindowState.Maximized;
            reportForm.ShowDialog();
        }
        /*
         // Create an instance of your Crystal Report
                CrystalReport3 myReport = new CrystalReport3();

                // Create a dataset or datatable to pass to the report
                DataSet myDataSet = new DataSet();
                DataTable myDataTable = new DataTable("managecars_tbl");

                // Define the columns (match these to your report fields)
                myDataTable.Columns.Add("id", typeof(string));
                myDataTable.Columns.Add("brand", typeof(string));

                // Add the selected data to the datatable
                myDataTable.Rows.Add(carId, carName);

                // Add the datatable to the dataset
                myDataSet.Tables.Add(myDataTable);

                // Set the report's data source
                myReport.SetDataSource(myDataSet);

                // Create a new form to display the CrystalReportViewer
                Form reportForm = new Form();
                CrystalReportViewer crystalReportViewer = new CrystalReportViewer
                {
                    Dock = DockStyle.Fill,
                    ReportSource = myReport
                };
                    reportForm.Controls.Add(crystalReportViewer);
                    reportForm.WindowState = FormWindowState.Maximized;
                    reportForm.ShowDialog();

            }
         */

    }
}
