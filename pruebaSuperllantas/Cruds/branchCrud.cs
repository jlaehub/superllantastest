using pruebaSuperllantas.Models;
using pruebaSuperllantas.List;
using System.Data;
using System.Data.SqlClient;

namespace pruebaSuperllantas.Cruds;

 
    public class branchCrud : genericList<Branch>
{
    private readonly string _cadenaSQL = "";

    public branchCrud(IConfiguration configuracion)
    {
        _cadenaSQL = configuracion.GetConnectionString("stringSQL");
    }

    public async Task<List<Branch>> List()
    {
        List<Branch> list = new List<Branch>();

        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_ListaBranches", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            using (var dr = await cmd.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    list.Add(new Branch
                    {
                        branchId = Convert.ToInt32(dr["branchId"]),
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

    public async Task<bool> Create(Branch model)
    {
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_CreateBranch", connection);
            cmd.Parameters.AddWithValue("companyId", model.companyId);
            cmd.Parameters.AddWithValue("name", model.name);
            cmd.Parameters.AddWithValue("address", model.address);
            cmd.Parameters.AddWithValue("phone", model.phone);
            cmd.CommandType = CommandType.StoredProcedure;

            int affectedRows = await cmd.ExecuteNonQueryAsync();

            return affectedRows > 0;
        }
    }

    public async Task<bool> Update(Branch model)
    {
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateBranch", connection);
            cmd.Parameters.AddWithValue("branchId", model.branchId);
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
        using (var connection = new SqlConnection(_cadenaSQL))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("sp_DeleteBranch", connection);
            cmd.Parameters.AddWithValue("branchId", id);
            cmd.CommandType = CommandType.StoredProcedure;

            int afectedRows = await cmd.ExecuteNonQueryAsync();

            return afectedRows > 0;
        }
    }
}

