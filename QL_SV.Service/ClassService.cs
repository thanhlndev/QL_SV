using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QL_SV.Service.InterfaceService;
namespace QL_SV.Service
{
    public class ClassService : IClassService
    {
        private readonly string _connectionString = "Data Source=DESKTOP-3OQCIT0;Database=QL_SV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public List<ClassDto> GetAllClasses()
        {
            var classes = new List<ClassDto>();
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("SELECT MaLop, TenLop, Email, MaKhoa FROM Classes", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classes.Add(new ClassDto
                        {
                            MaLop = reader["MaLop"].ToString(),
                            TenLop = reader["TenLop"].ToString(),
                            Email = reader["Email"].ToString(),
                            MaKhoa = reader["MaKhoa"].ToString()
                        });
                    }
                }
                return classes;
            }
        }
        public bool AddClass(ClassDto classDto)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("INSERT INTO Classes (MaLop, TenLop, Email, MaKhoa) VALUES (@MaLop, @TenLop, @Email, @MaKhoa)", connection);
                command.Parameters.AddWithValue("@MaLop", classDto.MaLop);
                command.Parameters.AddWithValue("@TenLop", classDto.TenLop);
                command.Parameters.AddWithValue("@Email", classDto.Email);
                command.Parameters.AddWithValue("@MaKhoa", classDto.MaKhoa);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateClass(string maLop, ClassDto classDto)
        {
            var existingClass = GetAllClasses().FirstOrDefault(c => c.MaLop == maLop);
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("UPDATE Classes SET TenLop = @TenLop, Email = @Email, MaKhoa = @MaKhoa WHERE MaLop = @MaLop", connection);
                command.Parameters.AddWithValue("@MaLop", maLop);
                command.Parameters.AddWithValue("@TenLop", classDto.TenLop ?? existingClass?.TenLop);
                command.Parameters.AddWithValue("@Email", classDto.Email ?? existingClass?.Email);
                command.Parameters.AddWithValue("@MaKhoa", classDto.MaKhoa ?? existingClass?.MaKhoa);
                return command.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteClass(string maLop)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("DELETE FROM Classes WHERE MaLop = @MaLop", connection);
                command.Parameters.AddWithValue("@MaLop", maLop);
                return command.ExecuteNonQuery() > 0;
            }
        }

    }
}
