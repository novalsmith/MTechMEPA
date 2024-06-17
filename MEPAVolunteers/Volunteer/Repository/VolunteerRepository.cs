using Dapper;
using Main.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Volunteer.Interface;
using Volunteer.Model;
using Volunteer.Utility;

namespace Volunteer.Repository
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly ApplicationDBContext _context;
        public VolunteerRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public Task<VolunteerModel> CreateVolunteer(VolunteerDTO item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VolunteerModel>> GetVolunteerAync()
        {
            try
            {
                var connectionString = _context.Database.GetDbConnection().ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    // Buka koneksi
                    await connection.OpenAsync();

                    // Eksekusi stored procedure dan ambil hasilnya
                    var result = await connection.QueryAsync<VolunteerModel>("dbo.GetVolunteers", commandType: CommandType.StoredProcedure);

                    // Kembalikan hasil
                    return result.ToList();
                }
            }
            catch (SqlException ex)
            {
                // Tangani exception yang terkait dengan koneksi database atau eksekusi perintah SQL
                Console.WriteLine("SQL Exception occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }
            catch (Exception ex)
            {
                // Tangani exception umum
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }
        }

        public async Task<VolunteerModel> GetVolunteerByIDAync(string VolunteerID)
        {
            try
            {
                var connectionString = _context.Database.GetDbConnection().ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    // Buka koneksi
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@VolunteerID", VolunteerID, DbType.String);

                    // Eksekusi stored procedure dan ambil hasilnya
                    var result = await connection.QueryFirstOrDefaultAsync<VolunteerModel>("dbo.GetVolunteers", parameters, commandType: CommandType.StoredProcedure);

                    // Kembalikan hasil
                    return result;
                }
            }
            catch (SqlException ex)
            {
                // Tangani exception yang terkait dengan koneksi database atau eksekusi perintah SQL
                Console.WriteLine("SQL Exception occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }
            catch (Exception ex)
            {
                // Tangani exception umum
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }
        }

        public async Task<bool> RegisterVolunteer(RegisterModel item, Role role)
        {
            var isSuccess = false;
            try
            {
                
                var connectionString = _context.Database.GetDbConnection().ConnectionString;

                using (var connection = new SqlConnection(connectionString))
                {
                    // Buka koneksi
                    await connection.OpenAsync();

                    // Buat DataTable dan tambahkan data ke dalamnya
                    DataTable volunteerTable = new DataTable();
                    volunteerTable.Columns.Add("VolunteerID", typeof(string));
                    volunteerTable.Columns.Add("Name", typeof(string));
                    volunteerTable.Columns.Add("Phone", typeof(string));
                    volunteerTable.Columns.Add("Address", typeof(string));
                    volunteerTable.Columns.Add("Email", typeof(string));
                    volunteerTable.Columns.Add("Gender", typeof(string));
                    volunteerTable.Columns.Add("PersonID", typeof(string));
                    volunteerTable.Columns.Add("Status", typeof(string));
                    volunteerTable.Columns.Add("Password", typeof(string));
                    volunteerTable.Columns.Add("BirthDate", typeof(DateTime));
                    volunteerTable.Columns.Add("Regency", typeof(string));
                    volunteerTable.Columns.Add("District", typeof(string));
                    volunteerTable.Columns.Add("Ward", typeof(string));
                    volunteerTable.Columns.Add("Village", typeof(string));
                    volunteerTable.Columns.Add("RT", typeof(string));
                    volunteerTable.Columns.Add("RW", typeof(string));
                    volunteerTable.Columns.Add("VolunteerName", typeof(string));
                    volunteerTable.Columns.Add("VolunteerRegency", typeof(string));
                    volunteerTable.Columns.Add("VolunteerDistrict", typeof(string));
                    volunteerTable.Columns.Add("Role", typeof(string));

                    // Tambahkan contoh data ke DataTable
                    volunteerTable.Rows.Add(
                         null,
                         item.Name,
                         item.Phone,
                         null,
                         item.Email,
                         null,
                         null,
                         null,
                         item.Password,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         null,
                         role
                     );

                    // Buat SqlCommand
                    using (var command = new SqlCommand("UpsertVolunteers", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Tambahkan parameter table type
                        var tableParam = command.Parameters.AddWithValue("@Volunteers", volunteerTable);
                        tableParam.SqlDbType = SqlDbType.Structured;
                        tableParam.TypeName = "VolunteerTableType";

                        // Add OUTPUT parameter to capture affected row count
                        var outputParam = command.Parameters.Add("@AffectedVolunteerCount", SqlDbType.Int);
                        outputParam.Direction = ParameterDirection.Output;

                        // Execute stored procedure
                        await command.ExecuteNonQueryAsync();

                        // Return affected row count
                        if ((int)outputParam.Value > 0) {
                            isSuccess = true;
                        } 
                    }
                }
            }
            catch (SqlException ex)
            {
                // Tangani exception yang terkait dengan koneksi database atau eksekusi perintah SQL
                Console.WriteLine("SQL Exception occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }
            catch (Exception ex)
            {
                // Tangani exception umum
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Anda bisa memilih untuk melemparkan kembali exception atau menangani secara spesifik di sini
            }

            return isSuccess;
        }
    }
}
