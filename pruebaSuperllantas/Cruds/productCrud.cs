using pruebaSuperllantas.List;
using pruebaSuperllantas.Models;
using System.Data.SqlClient;
using System.Data;

namespace pruebaSuperllantas.Cruds
{
    public class productCrud : genericList<Product>
    {
        private readonly string _connectionString = "";

        public productCrud(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("stringSQL");
        }

        public async Task<List<Product>> List()
        {
            List<Product> list = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListProducts", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new Product
                        {
                            productId = Convert.ToInt32(dr["productId"]),
                            name = dr["name"].ToString(),
                            price = Convert.ToDecimal(dr["price"]),
                            branchId = Convert.ToInt32(dr["branchId"]),
                            withholdingTax = Convert.ToDecimal(dr["withholdingTax"]),
                            salesTax = Convert.ToDecimal(dr["salesTax"])
                        });
                    }
                }
            }

            return list;
        }

        public async Task<bool> Create(Product model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateProduct", connection);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("price", model.price);
                cmd.Parameters.AddWithValue("branchId", model.branchId);
                cmd.Parameters.AddWithValue("withholdingTax", model.withholdingTax);
                cmd.Parameters.AddWithValue("salesTax", model.salesTax);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(Product model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateProduct", connection);
                cmd.Parameters.AddWithValue("productId", model.productId);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("price", model.price);
                cmd.Parameters.AddWithValue("branchId", model.branchId);
                cmd.Parameters.AddWithValue("withholdingTax", model.withholdingTax);
                cmd.Parameters.AddWithValue("salesTax", model.salesTax);
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
                SqlCommand cmd = new SqlCommand("sp_DeleteProduct", connection);
                cmd.Parameters.AddWithValue("productId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }
    }
}
