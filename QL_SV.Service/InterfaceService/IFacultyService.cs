using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QLSV.WCF.InterfaceService
{
    [ServiceContract]
    public interface IFacultyService
    {
        [OperationContract]
        List<FacultyDto> GetAllFaculties();

        [OperationContract]
        bool AddFaculty(FacultyDto faculty);

        [OperationContract]
        bool UpdateFaculty(string maKhoa,FacultyDto faculty);

        [OperationContract]
        bool DeleteFaculty(string maKhoa);
    }

    [DataContract]
    public class FacultyDto
    {
        [DataMember] public string MaKhoa { get; set; }
        [DataMember] public string TenKhoa { get; set; }
        [DataMember] public string DiaChi { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Sdt { get; set; }
    }
}
