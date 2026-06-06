using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QLSV.WCF.InterfaceService;

namespace QLSV.WCF
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString = "Data Source=DESKTOP-3OQCIT0;Database=QL_SV;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";

        public bool SyncStudents(List<StudentDto> students)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var s in students)
                        {
                            // Logic update (có thể viết thành hàm riêng để tái sử dụng)
                            using (var cmd = new SqlCommand("UPDATE Students SET hoTen=@ht, gioiTinh=@gt, namSinh=@ns, diaChi=@dc, email=@em, sdt=@sdt, maLop=@ml WHERE maSV=@msv", connection, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ht", s.HoTen);
                                cmd.Parameters.AddWithValue("@gt", (object)s.GioiTinh ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ns", (object)s.NamSinh ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@dc", (object)s.DiaChi ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@em", (object)s.Email ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@sdt", (object)s.Sdt ?? DBNull.Value);
                                cmd.Parameters.AddWithValue("@ml", s.MaLop);
                                cmd.Parameters.AddWithValue("@msv", s.MaSV);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        transaction.Commit();
                        return true;
                    }
                    catch { transaction.Rollback(); return false; }
                }
            }
        }
        public List<StudentDto> GetAllStudent()
        {
            var students = new List<StudentDto>();
            // Cập nhật câu lệnh SELECT bao gồm các trường mới
            const string query = "SELECT maSV, hoTen, gioiTinh, namSinh, diaChi, email, sdt, maLop FROM Students";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new StudentDto
                        {
                            // Đọc dữ liệu an toàn bằng cách kiểm tra IsDBNull
                            MaSV = reader.IsDBNull(0) ? string.Empty : reader.GetString(0).Trim(),
                            HoTen = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                            GioiTinh = reader.IsDBNull(2) ? null : reader.GetString(2),
                            NamSinh = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            DiaChi = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Email = reader.IsDBNull(5) ? null : reader.GetString(5).Trim(),
                            Sdt = reader.IsDBNull(6) ? null : reader.GetString(6).Trim(),
                            MaLop = reader.IsDBNull(7) ? string.Empty : reader.GetString(7).Trim()
                        });
                    }
                }
            }
            return students;
        }

        public bool AddStudent(StudentDto student)
        {
            if (student == null || string.IsNullOrWhiteSpace(student.MaSV)) return false;

            // Cập nhật câu lệnh INSERT bao gồm các trường mới
            const string query = @"INSERT INTO Students (maSV, hoTen, gioiTinh, namSinh, diaChi, email, sdt, maLop) 
                                   VALUES (@MaSV, @HoTen, @GioiTinh, @NamSinh, @DiaChi, @Email, @Sdt, @MaLop)";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                // Thêm các tham số cho các trường mới, xử lý DBNull
                command.Parameters.Add("@MaSV", SqlDbType.VarChar, 20).Value = student.MaSV;
                command.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = student.HoTen;
                command.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = (object)student.GioiTinh ?? DBNull.Value;
                command.Parameters.Add("@NamSinh", SqlDbType.Int).Value = (object)student.NamSinh ?? DBNull.Value;
                command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = (object)student.DiaChi ?? DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)student.Email ?? DBNull.Value;
                command.Parameters.Add("@Sdt", SqlDbType.VarChar, 20).Value = (object)student.Sdt ?? DBNull.Value;
                command.Parameters.Add("@MaLop", SqlDbType.VarChar, 20).Value = student.MaLop;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateStudent(string maSV, StudentDto student)
        {
            if (string.IsNullOrWhiteSpace(maSV) || student == null) return false;

            // Cập nhật câu lệnh UPDATE bao gồm các trường mới
            const string query = @"UPDATE Students 
                                   SET hoTen = @HoTen, gioiTinh = @GioiTinh, namSinh = @NamSinh, 
                                       diaChi = @DiaChi, email = @Email, sdt = @Sdt, maLop = @MaLop 
                                   WHERE maSV = @ParamMaSV";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                // Thêm các tham số cho các trường mới, xử lý DBNull
                command.Parameters.Add("@HoTen", SqlDbType.NVarChar, 100).Value = student.HoTen;
                command.Parameters.Add("@GioiTinh", SqlDbType.NVarChar, 10).Value = (object)student.GioiTinh ?? DBNull.Value;
                command.Parameters.Add("@NamSinh", SqlDbType.Int).Value = (object)student.NamSinh ?? DBNull.Value;
                command.Parameters.Add("@DiaChi", SqlDbType.NVarChar, 200).Value = (object)student.DiaChi ?? DBNull.Value;
                command.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = (object)student.Email ?? DBNull.Value;
                command.Parameters.Add("@Sdt", SqlDbType.VarChar, 20).Value = (object)student.Sdt ?? DBNull.Value;
                command.Parameters.Add("@MaLop", SqlDbType.VarChar, 20).Value = student.MaLop;
                command.Parameters.Add("@ParamMaSV", SqlDbType.VarChar, 20).Value = maSV;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteStudent(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV)) return false;

            const string query = "DELETE FROM Students WHERE maSV = @MaSV";

            using (var connection = new SqlConnection(_connectionString))
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@MaSV", SqlDbType.VarChar, 20).Value = maSV;

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
        }
    }
}