using pruebaSuperllantas.List;
using pruebaSuperllantas.Models;
using System.Data.SqlClient;
using System.Data;

namespace pruebaSuperllantas.Cruds
{
    public class SaleCrud : genericList<Sale>
    {
        private readonly string _connectionString;

        public SaleCrud(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("stringSQL");
        }

        public async Task<List<Sale>> List()
        {
            List<Sale> list = new List<Sale>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListSales", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new Sale
                        {
                            saleId = Convert.ToInt32(dr["saleId"]),
                            customerId = Convert.ToInt32(dr["customerId"]),
                            branchId = Convert.ToInt32(dr["branchId"]),
                            saleType = dr["saleType"].ToString(),
                            temporaryDiscount = Convert.ToDecimal(dr["temporaryDiscount"]),
                            saleDate = Convert.ToDateTime(dr["saleDate"])
                        });
                    }
                }
            }

            return list;
        }

        public async Task<bool> Create(Sale model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateSale", connection);
                cmd.Parameters.AddWithValue("customerId", model.customerId);
                cmd.Parameters.AddWithValue("branchId", model.branchId);
                cmd.Parameters.AddWithValue("saleType", model.saleType);
                cmd.Parameters.AddWithValue("temporaryDiscount", model.temporaryDiscount);
                cmd.Parameters.AddWithValue("saleDate", model.saleDate);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(Sale model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateSale", connection);
                cmd.Parameters.AddWithValue("saleId", model.saleId);
                cmd.Parameters.AddWithValue("customerId", model.customerId);
                cmd.Parameters.AddWithValue("branchId", model.branchId);
                cmd.Parameters.AddWithValue("saleType", model.saleType);
                cmd.Parameters.AddWithValue("temporaryDiscount", model.temporaryDiscount);
                cmd.Parameters.AddWithValue("saleDate", model.saleDate);
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
                SqlCommand cmd = new SqlCommand("sp_DeleteSale", connection);
                cmd.Parameters.AddWithValue("saleId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }
    }
}
