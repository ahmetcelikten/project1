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

namespace project1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;
            string confirmPassword = textBox3.Text;

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match.");
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and Password cannot be empty.");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"))
            {
                conn.Open();
                string query = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password); 

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration successful!");
                    this.Close(); 
                    Form1 form1 = new Form1();
                    form1.Show();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            this.Close();
            Form1 form1 = new Form1();
            form1.Show();
        }
    }
}
