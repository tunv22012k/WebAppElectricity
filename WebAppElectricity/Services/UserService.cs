using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using WebAppElectricity.Helpers;
using WebAppElectricity.Models;

namespace WebAppElectricity.Services
{
    public class UserService
    {
        // Phương thức để xác thực người dùng
        public Users Authenticate(string userName, string password)
        {
            string hashedPassword = HashPassword(password);

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        UserId, 
                        Email,
                        UserName,
                        Password,
                        Role
                    FROM 
                        Users
                    WHERE 
                        UserName     = @UserName 
                        AND Password = @Password
                ";

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserName", userName);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Users
                    {
                        UserId      = (int)reader["UserId"],
                        Email       = reader["Email"].ToString(),
                        UserName    = reader["UserName"].ToString()
                    };
                }
            }

            return null;
        }

        // Phương thức để hash mật khẩu
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}