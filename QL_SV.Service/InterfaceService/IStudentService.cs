using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QL_SV.Service.InterfaceService
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        List<StudentDto> GetAllStudent();
        [OperationContract]
        bool AddStudent(StudentDto student);
        [OperationContract]
        bool UpdateStudent(int maSV, StudentDto student);
        [OperationContract]
        bool DeleteStudent(StudentDto student);
    }
    [DataContract]
    public class StudentDto
    {
        [DataMember] public int MaSV { get; set; }
        [DataMember] public string HoTen { get; set; }
        [DataMember] public string GioiTinh { get; set; }
        [DataMember] public string Class { get; set; }
    }
}
