using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebAppElectricity.Helpers;
using WebAppElectricity.Models;

namespace WebAppElectricity.Services
{
    public class FileService
    {
        public List<Category> GetDataCategory(string UserId)
        {
            List<Category> category = new List<Category>();

            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                string query = @"
                    SELECT 
                        *
                    FROM 
                        Category
                ";

                if (!string.IsNullOrEmpty(UserId))
                {
                    query += " WHERE UserId = @UserId";
                }

                SqlCommand cmd = new SqlCommand(query, connection);

                if (!string.IsNullOrEmpty(UserId))
                {
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                }

                connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category.Add(new Category
                        {
                            CategoryId          = (int)reader["CategoryId"],
                            UserId              = (int)reader["UserId"],
                            CategoryName        = reader["CategoryName"].ToString(),
                            StepAccept          = reader["StepAccept"] != DBNull.Value ? (int)reader["StepAccept"] : (int?)null,
                            StepDeny            = reader["StepDeny"].ToString(),
                            CreatedAt           = Convert.ToDateTime(reader["CreatedAt"])
                        });
                    }
                }
            }

            return category;
        }

        public bool CreateCategory(string UserId, string CategoryName)
        {
            // Tạo kết nối với cơ sở dữ liệu và lưu thông tin người dùng
            using (var connection = new SqlConnection(DatabaseHelper.connectionString))
            {
                bool isSuccess = false;
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string query = @"
                        INSERT INTO Category 
                        (
                            UserId,
                            CategoryName,
                            CreatedAt
                        ) 
                        VALUES 
                        (
                            @UserId,
                            @CategoryName,
                            @CreatedAt
                        )
                    ";

                    SqlCommand cmd = new SqlCommand(query, connection, transaction);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@CategoryName", CategoryName);
                    cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Nếu số hàng bị ảnh hưởng lớn hơn 0, quá trình chèn đã thành công
                    isSuccess = rowsAffected > 0;

                    transaction.Commit();
                    return isSuccess;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback(); // Hoàn tác các thay đổi nếu có lỗi
                    }

                    throw new Exception("Cannot open database connection: " + ex.Message);
                }
                finally
                {
                    connection.Close(); // Đảm bảo kết nối được đóng
                }
            }
        }
    }
}