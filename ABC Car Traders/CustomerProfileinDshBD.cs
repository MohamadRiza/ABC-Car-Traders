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
using System.Data.SqlClient;//1

namespace ABC_Car_Traders
{
    public partial class CustomerProfileinDshBD : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DELL\\MSSQLSERVER01;Initial Catalog=ABC_Car_Traders;Integrated Security=True");//2
        public CustomerProfileinDshBD()
        {
            InitializeComponent();
        }
        private void cleartextboxCDA()
        {
            txtfname.Clear();
            txtcity.Clear();
            txtaddress.Clear();
            txtmobile.Clear();
            combocountry.SelectedItem = null;
        }
        private void fetchdatatotextbox()
        {//this is for fetch datas to textbox customerprogiledashBD >>>>> Change Delivery Details PANEL
            try
            {
                con.Open();
                SqlCommand cmd2 = new SqlCommand("select fullname, country, city, address, mobile  from customer_tbl where email = @Wemail and password = @Wpassword", con);
                cmd2.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
                cmd2.Parameters.AddWithValue("@Wpassword", LoginUser.textboxfetchpassword);
                SqlDataReader DR = cmd2.ExecuteReader();
                if (DR.Read())
                {
                    txtfname.Text = DR["fullname"].ToString();
                    combocountry.Text = DR["country"].ToString();
                    txtcity.Text = DR["city"].ToString();
                    txtaddress.Text = DR["address"].ToString();
                    txtmobile.Text = DR["mobile"].ToString();
                }
                DR.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex.Message);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                confirmpaswd.UseSystemPasswordChar = false;
                password.UseSystemPasswordChar = false;
            }
            else
            {
                confirmpaswd.UseSystemPasswordChar = true;
                password.UseSystemPasswordChar = true;
            }
        }

        private void CustomerProfileinDshBD_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'aBC_Car_TradersDataSet7.country_For_Combobox' table. You can move, or remove it, as needed.
            this.country_For_ComboboxTableAdapter.Fill(this.aBC_Car_TradersDataSet7.country_For_Combobox);
            usrname.Text = LoginUser.welcomeuser;
            password.Text = LoginUser.textboxfetchpassword;
            confirmpaswd.Text = LoginUser.textboxfetchpassword;

            panel5.BackColor = Color.FromArgb(185, Color.Black);
            panel3.BackColor = Color.FromArgb(185, Color.Black);
            combocountry.SelectedItem = null;

            fetchdatatotextbox();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            CustomerLoginDashboard frmcdb = new CustomerLoginDashboard();
            frmcdb.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Username/Password changes after logges out are you sure do you want to change?","Question",MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (string.IsNullOrEmpty(usrname.Text) || string.IsNullOrEmpty(password.Text) || string.IsNullOrEmpty(confirmpaswd.Text))
                {
                    MessageBox.Show("textbox Empty Please fill!", "Empty!");
                }
                else
                {
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update customer_tbl set email = @email, password = @password where email = @Wemail and password = @Wpassword", con);
                        cmd.Parameters.AddWithValue("@email", usrname.Text);
                        cmd.Parameters.AddWithValue("@password", confirmpaswd.Text);
                        cmd.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
                        cmd.Parameters.AddWithValue("@Wpassword", LoginUser.textboxfetchpassword);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("Username/password changed Successfully!", "Success");
                        usrname.Clear();
                        password.Clear();
                        confirmpaswd.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error" + ex.Message);
                    }
                    this.Hide();
                    LoginUser frmu = new LoginUser();
                    frmu.Show();
                }
            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            cleartextboxCDA();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtfname.Text) || string.IsNullOrEmpty(txtcity.Text) || string.IsNullOrEmpty(txtaddress.Text) || string.IsNullOrEmpty(txtmobile.Text))
            {
                MessageBox.Show("textboxes can't be empty", "Empty textbox!");
            }
            else
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE customer_tbl SET fullname = @fname, country = @country, city = @city, address = @address, mobile = @mobile WHERE email = @Wemail AND password = @Wpassword", con);
                    cmd.Parameters.AddWithValue("@fname", txtfname.Text);
                    cmd.Parameters.AddWithValue("@country", combocountry.Text);
                    cmd.Parameters.AddWithValue("@city", txtcity.Text);
                    cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@mobile", txtmobile.Text);
                    cmd.Parameters.AddWithValue("@Wemail", LoginUser.welcomeuser);
                    cmd.Parameters.AddWithValue("@Wpassword", LoginUser.textboxfetchpassword);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Customer Details Updated Successfully!", "Success");
                    cleartextboxCDA();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error" + ex.Message);
                }
            }

        }

        private void txtmobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '+';
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fetchdatatotextbox();
        }
    }
}
