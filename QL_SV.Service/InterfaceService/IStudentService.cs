using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace QLSV.WCF.InterfaceService
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        bool SyncStudents(List<StudentDto> students);
        [OperationContract]
        List<StudentDto> GetAllStudent();

        [OperationContract]
        bool AddStudent(StudentDto student);

        [OperationContract]
        bool UpdateStudent(string maSV, StudentDto student);

        [OperationContract]
        bool DeleteStudent(string maSV);
    }

    [DataContract]
    public class StudentDto
    {
        [DataMember] public string MaSV { get; set; }
        [DataMember] public string HoTen { get; set; }
        [DataMember] public string GioiTinh { get; set; }

        [DataMember] public int? NamSinh { get; set; }
        [DataMember] public string DiaChi { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Sdt { get; set; }

        [DataMember] public string MaLop { get; set; }
    }
}