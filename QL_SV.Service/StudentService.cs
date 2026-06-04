using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QL_SV.Service.InterfaceService;

namespace QL_SV.Service
{
    public class StudentService : IStudentService
    {

        private readonly string _connectionString = "Data Source=DESKTOP-3OQCIT0;Database=QL_SV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        public List<StudentDto> GetAllStudent()
        {
            var students = new List<StudentDto>();
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("SELECT MaSV, HoTen, GioiTinh, Class FROM Students", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var student = new StudentDto
                        {
                            MaSV = reader.GetInt32(0),
                            HoTen = reader.GetString(1),
                            GioiTinh = reader.GetString(2),
                            Class = reader.GetString(3)
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }
        public bool AddStudent(StudentDto student)
        {
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("INSERT INTO Students (HoTen, GioiTinh, Class) VALUES (@HoTen, @GioiTinh, @Class)", connection);
                command.Parameters.AddWithValue("@HoTen", student.HoTen);
                command.Parameters.AddWithValue("@GioiTinh", student.GioiTinh);
                command.Parameters.AddWithValue("@Class", student.Class);
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateStudent(int maSV, StudentDto student)
        {
            var existingStudent = GetAllStudent().FirstOrDefault(s => s.MaSV == maSV);
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("UPDATE Students SET HoTen = @HoTen, GioiTinh = @GioiTinh, Class = @Class WHERE MaSV = @MaSV", connection);
                command.Parameters.AddWithValue("@HoTen", student.HoTen);
                command.Parameters.AddWithValue("@GioiTinh", student.GioiTinh);
                command.Parameters.AddWithValue("@Class", student.Class);
                command.Parameters.AddWithValue("@MaSV", maSV);
                return command.ExecuteNonQuery() > 0;
            }

        }

        public bool DeleteStudent(StudentDto student)
        {
            var existingStudent = GetAllStudent().FirstOrDefault(s => s.MaSV == student.MaSV);
            using (var connection = new System.Data.SqlClient.SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new System.Data.SqlClient.SqlCommand("DELETE FROM Students WHERE MaSV = @MaSV", connection);
                command.Parameters.AddWithValue("@MaSV", student.MaSV);
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}
