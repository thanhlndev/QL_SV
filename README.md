Hệ thống quản lý thông tin sinh viên, khoa và lớp học dựa trên kiến trúc phân tầng (Layered Architecture) sử dụng **C# Windows Forms** và **WCF / Web Services** (.NET Framework).

## Cấu trúc Solution (`QL_SV.sln`)

Solution bao gồm 2 dự án thành phần (2 projects):

### 1. `QL_SV.Service` (Backend Service Layer)
Đóng vai trò là tầng xử lý logic nghiệp vụ và cung cấp dịch vụ thông qua các Interface định sẵn.
* **`InterfaceService/`**: Chứa các bản thiết kế (Contracts) định nghĩa các hành động xử lý dữ liệu.
  * `IClassService.cs`: Giao diện quản lý lớp học.
  * `IFacultyService.cs`: Giao diện quản lý khoa/ngành học.
  * `IStudentService.cs`: Giao diện quản lý thông tin sinh viên.
* **Service Implementations** (Các lớp triển khai chi tiết logic từ Interface):
  * `ClassService.cs`
  * `FacultyService.cs`
  * `StudentService.cs`
* **Cấu hình & Phụ thuộc**:
  * `App.config`: Chứa chuỗi kết nối Database (Connection String) và cấu hình Endpoint của Service.
  * `packages.config`: Quản lý các thư viện Nuget bên thứ ba (như Entity Framework, Dapper,...).

### 2. `QL_SV` (Frontend UI Layer)
Ứng dụng giao diện người dùng hiển thị trực quan (Windows Forms App).
* **`Connected Services`**: 
  * `FacultyServiceRef`: Kết nối và tham chiếu trực tiếp đến dịch vụ `QL_SV.Service` để lấy dữ liệu đổ lên UI.
* **Forms & UI**:
  * `FacultyForm.cs`: Form chức năng giao diện phục vụ việc quản lý, thêm, sửa, xóa Khoa.
* **Khởi chạy hệ thống**:
  * `Program.cs`: Điểm entry-point cấu hình để chạy ứng dụng WinForm.
  * `App.config` & `packages.config`: Cấu hình client endpoint kết nối dịch vụ và các dependency cần thiết cho UI.

---

## Công nghệ sử dụng
* **Ngôn ngữ:** C# (.NET Framework)
* **Giao diện:** Windows Forms (WinForm)
* **Kiến trúc:** Interface-driven / Service-oriented Architecture (SOA)
* **Quản lý package:** NuGet

---

## Hướng dẫn cài đặt và khởi chạy dưới Local

### Điều kiện cần
* Máy tính đã cài đặt **Visual Studio (phiên bản 2019 hoặc 2022)**.
* Đã cài đặt gói **.NET Desktop Development** và **WCF Data Services** (nếu cần) trong Visual Studio Installer.
