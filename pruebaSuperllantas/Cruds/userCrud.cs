using pruebaSuperllantas.List;
using pruebaSuperllantas.Models;
using System.Data.SqlClient;
using System.Data;

namespace pruebaSuperllantas.Cruds
{
    public class UserCrud : genericList<User>
    {
        private readonly string _connectionString;

        public UserCrud(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("stringSQL");
        }

        public async Task<List<User>> List()
        {
            List<User> list = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListUsers", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new User
                        {
                            userId = Convert.ToInt32(dr["userId"]),
                            name = dr["name"].ToString(),
                            lastName = dr["lastName"].ToString(),
                            phone = dr["phone"].ToString(),
                            email = dr["email"].ToString(),
                            password = dr["password"].ToString(),
                            userType = dr["userType"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public async Task<bool> Create(User model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateUser", connection);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("lastName", model.lastName);
                cmd.Parameters.AddWithValue("phone", model.phone);
                cmd.Parameters.AddWithValue("email", model.email);
                cmd.Parameters.AddWithValue("password", model.password);
                cmd.Parameters.AddWithValue("userType", model.userType);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(User model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateUser", connection);
                cmd.Parameters.AddWithValue("userId", model.userId);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("lastName", model.lastName);
                cmd.Parameters.AddWithValue("phone", model.phone);
                cmd.Parameters.AddWithValue("email", model.email);
                cmd.Parameters.AddWithValue("password", model.password);
                cmd.Parameters.AddWithValue("userType", model.userType);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_DeleteUser", connection);
                cmd.Parameters.AddWithValue("userId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }
    }
}
