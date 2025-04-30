using System.Data.SqlClient;

public class DatabaseHelper
{
    private static readonly string connectionString = "Server=localhost;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }
}
