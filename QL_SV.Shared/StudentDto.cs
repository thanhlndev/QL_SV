using System.Runtime.Serialization;

namespace QL_SV.Shared
{
    [DataContract]
    public class StudentDto
    {
        [DataMember] public string MaSV { get; set; }
        [DataMember] public string HoTen { get; set; }
        [DataMember] public string GioiTinh { get; set; }
        [DataMember] public int NamSinh { get; set; }
        [DataMember] public string DiaChi { get; set; }
        [DataMember] public string Email { get; set; }
        [DataMember] public string Sdt { get; set; }
        [DataMember] public string MaLop { get; set; }
    }
}