using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QL_SV.Shared
{
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
