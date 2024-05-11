using pruebaSuperllantas.List;
using pruebaSuperllantas.Models;
using System.Data.SqlClient;
using System.Data;

namespace pruebaSuperllantas.Cruds
{
    public class companyCrud : genericList<Company>
    {
        private readonly string _connectionString = "";

        public companyCrud(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("stringSQL");
        }

        public async Task<List<Company>> List()
        {
            List<Company> list = new List<Company>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListCompanies", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new Company
                        {
                            companyId = Convert.ToInt32(dr["companyId"]),
                            name = dr["name"].ToString(),
                            address = dr["address"].ToString(),
                            phone = dr["phone"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public async Task<bool> Create(Company model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateCompany", connection);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("address", model.address);
                cmd.Parameters.AddWithValue("phone", model.phone);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(Company model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateCompany", connection);
                cmd.Parameters.AddWithValue("companyId", model.companyId);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("address", model.address);
                cmd.Parameters.AddWithValue("phone", model.phone);
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
                SqlCommand cmd = new SqlCommand("sp_DeleteCompany", connection);
                cmd.Parameters.AddWithValue("companyId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int afectedRows = await cmd.ExecuteNonQueryAsync();

                return afectedRows > 0;
            }
        }
    }
}
    
