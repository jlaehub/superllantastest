using pruebaSuperllantas.List;
using pruebaSuperllantas.Models;
using System.Data.SqlClient;
using System.Data;

namespace pruebaSuperllantas.Cruds
{
    public class CustomerCrud : genericList<Customer>
    {
        private readonly string _connectionString;

        public CustomerCrud(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("stringSQL");
        }

        public async Task<List<Customer>> List()
        {
            List<Customer> list = new List<Customer>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_ListCustomers", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        list.Add(new Customer
                        {
                            customerId = Convert.ToInt32(dr["customerId"]),
                            customerType = dr["customerType"].ToString(),
                            name = dr["name"].ToString(),
                            email = dr["email"].ToString(),
                            phone = dr["phone"].ToString(),
                            specialDiscount = Convert.ToDecimal(dr["specialDiscount"]),
                            salesAdvisorId = Convert.ToInt32(dr["salesAdvisorId"])
                        });
                    }
                }
            }

            return list;
        }

        public async Task<bool> Create(Customer model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_CreateCustomer", connection);
                cmd.Parameters.AddWithValue("customerType", model.customerType);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("email", model.email);
                cmd.Parameters.AddWithValue("phone", model.phone);
                cmd.Parameters.AddWithValue("specialDiscount", model.specialDiscount);
                cmd.Parameters.AddWithValue("salesAdvisorId", model.salesAdvisorId);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }

        public async Task<bool> Update(Customer model)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("sp_UpdateCustomer", connection);
                cmd.Parameters.AddWithValue("customerId", model.customerId);
                cmd.Parameters.AddWithValue("customerType", model.customerType);
                cmd.Parameters.AddWithValue("name", model.name);
                cmd.Parameters.AddWithValue("email", model.email);
                cmd.Parameters.AddWithValue("phone", model.phone);
                cmd.Parameters.AddWithValue("specialDiscount", model.specialDiscount);
                cmd.Parameters.AddWithValue("salesAdvisorId", model.salesAdvisorId);
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
                SqlCommand cmd = new SqlCommand("sp_DeleteCustomer", connection);
                cmd.Parameters.AddWithValue("customerId", id);
                cmd.CommandType = CommandType.StoredProcedure;

                int affectedRows = await cmd.ExecuteNonQueryAsync();

                return affectedRows > 0;
            }
        }
    }
}
