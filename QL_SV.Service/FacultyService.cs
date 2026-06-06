using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using QLSV.WCF.InterfaceService;
namespace QLSV.WCF
{
    public class FacultyService : IFacultyService
    {

        //private readonly string connection;
        //= ConfigurationManager.ConnectionStrings["SMSDbConnection"].ConnectionString;
        private readonly string _connectionString = "Data Source=DESKTOP-3OQCIT0;Database=QL_SV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public List<FacultyDto> GetAllFaculties()
        {
            var faculties = new List<FacultyDto>();
            //using(SqlConnection conn = new SqlConnection(connection))
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT MaKhoa, TenKhoa, DiaChi, Email, Sdt FROM Faculties";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var faculty = new FacultyDto
                        {
                            MaKhoa = reader["MaKhoa"].ToString(),
                            TenKhoa = reader["TenKhoa"].ToString(),
                            DiaChi = reader["DiaChi"].ToString(),
                            Email = reader["Email"].ToString(),
                            Sdt = reader["Sdt"].ToString()
                        };
                        faculties.Add(faculty);
                    }
                }
            }
            return faculties;
        }
        public bool AddFaculty(FacultyDto faculty)
        {
            //using (SqlConnection conn = new SqlConnection(connection))
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Faculties (maKhoa, tenKhoa, diaChi, email, sdt) VALUES (@maKhoa, @tenKhoa, @diaChi, @email, @sdt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maKhoa", faculty.MaKhoa);
                cmd.Parameters.AddWithValue("@tenKhoa", faculty.TenKhoa);
                cmd.Parameters.AddWithValue("@diaChi", (object)faculty.DiaChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@email", (object)faculty.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@sdt", (object)faculty.Sdt ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool UpdateFaculty(string maKhoa,FacultyDto faculty)
        {
            //using (SqlConnection conn = new SqlConnection(connection))
            var existingFaculty = GetAllFaculties().FirstOrDefault(f => f.MaKhoa == maKhoa);
            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                string query = "UPDATE Faculties SET tenKhoa=@tenKhoa, diaChi=@diaChi, email=@email, sdt=@sdt WHERE maKhoa=@maKhoa";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                cmd.Parameters.AddWithValue("@tenKhoa", faculty.TenKhoa);
                cmd.Parameters.AddWithValue("@diaChi", faculty.DiaChi);
                cmd.Parameters.AddWithValue("@email", (object)faculty.Email ?? existingFaculty?.Email);
                cmd.Parameters.AddWithValue("@sdt", (object)faculty.Sdt ?? existingFaculty?.Sdt);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool DeleteFaculty(string maKhoa)
        {
            //using (SqlConnection conn = new SqlConnection(connection))
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Faculties WHERE maKhoa=@maKhoa";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maKhoa", maKhoa);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

    }
}
