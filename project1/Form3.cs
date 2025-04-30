using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace project1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            LoadEmployees();
            LoadUsers();
        }

        private void LoadUsers()
        {
            // Veritabanı bağlantısını açıyoruz
            SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
            conn.Open();

            // Kullanıcılar tablosundan Id, Username, Password verilerini çekiyoruz
            string query = "SELECT Id, Username, Password FROM Users"; // Users tablosundan veri çekiyoruz
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt); // Veriyi DataTable'a yüklüyoruz

            // Veriyi dataGridView2'ye bağlıyoruz
            dataGridView2.DataSource = dt;

            conn.Close(); // Bağlantıyı kapatıyoruz
        }
        private void LoadEmployees()
        {
            SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
            conn.Open();

            string query = "SELECT EmployeeID, FirstName, LastName, JobTitle, Department, Salary FROM Employees";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt); // Veriyi DataTable'a yükler

            dataGridView1.DataSource = dt; // DataGridView'e aktarır

            conn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan gelen veriler
            string firstName = textBox1.Text;
            string lastName = textBox4.Text;
            string jobTitle = textBox2.Text;
            string department = textBox5.Text;
            decimal salary = decimal.Parse(textBox3.Text); // Maaş decimal türüne çevriliyor

            // Veritabanı bağlantısı
            SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
            conn.Open();

            string query = "INSERT INTO Employees (FirstName, LastName, JobTitle, Department, Salary) " +
                           "VALUES (@firstName, @lastName, @jobTitle, @department, @salary)";
            SqlCommand cmd = new SqlCommand(query, conn);

            // Parametreler ekleniyor
            cmd.Parameters.AddWithValue("@firstName", firstName);
            cmd.Parameters.AddWithValue("@lastName", lastName);
            cmd.Parameters.AddWithValue("@jobTitle", jobTitle);
            cmd.Parameters.AddWithValue("@department", department);
            cmd.Parameters.AddWithValue("@salary", salary);

            // Komut çalıştırılıyor
            cmd.ExecuteNonQuery();
            MessageBox.Show("Employee added successfully!");

            conn.Close();
            LoadEmployees(); // Çalışanları güncelle
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen çalışanın ID'sini alıyoruz
                int employeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmployeeID"].Value);

                // Formdan verileri alıyoruz
                string firstName = textBox1.Text;
                string lastName = textBox4.Text;
                string jobTitle = textBox2.Text;
                string department = textBox5.Text;
                decimal salary = decimal.Parse(textBox3.Text);

                // Veritabanı bağlantısı
                SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
                conn.Open();

                // Update komutu
                string query = "UPDATE Employees SET FirstName = @firstName, LastName = @lastName, JobTitle = @jobTitle, Department = @department, Salary = @salary WHERE EmployeeID = @employeeId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Parametreleri ekliyoruz
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@jobTitle", jobTitle);
                cmd.Parameters.AddWithValue("@department", department);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                // Komut çalıştırılıyor
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee updated successfully!");

                conn.Close();
                LoadEmployees(); // Çalışanları güncelle
            }
            else
            {
                MessageBox.Show("Please select an employee to update.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Seçilen çalışanın ID'sini alıyoruz
                int employeeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["EmployeeID"].Value);

                // Veritabanı bağlantısı
                SqlConnection conn = new SqlConnection("Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
                conn.Open();

                // Delete komutu
                string query = "DELETE FROM Employees WHERE EmployeeID = @employeeId";
                SqlCommand cmd = new SqlCommand(query, conn);

                // Parametreyi ekliyoruz
                cmd.Parameters.AddWithValue("@employeeId", employeeId);

                // Komut çalıştırılıyor
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee deleted successfully!");

                conn.Close();
                LoadEmployees(); // Çalışanları güncelle
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }
    }
}
