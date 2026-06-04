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
    public interface IClassService
    {
        [OperationContract]
        List<ClassDto> GetAllClasses();
        [OperationContract]
        bool AddClass(ClassDto classDto);
        [OperationContract]
        bool UpdateClass(string maLop, ClassDto classDto);
        [OperationContract]
        bool DeleteClass(string maLop);

    }

    [DataContract]
    public class ClassDto
    {
        [DataMember]
        public string MaLop { get; set; }
        [DataMember]
        public string TenLop { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string MaKhoa { get; set; }
    }
}
