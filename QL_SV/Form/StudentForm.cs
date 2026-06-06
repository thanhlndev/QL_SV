using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_SV.StudentServiceReference;
using QL_SV.ClassServiceRef;

namespace QLSV.WinForms
{
    public partial class StudentForm : Form
    {
        private BindingList<StudentDto> _studentDataSource;
        private bool _isGridConfigured = false;

        public StudentForm()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Load += async (s, e) => await Form_LoadAsync();

            // Đăng ký sự kiện click cho DataGridView
            dgvStudents.CellClick += DgvStudents_CellClick;

            // Đăng ký sự kiện cho các nút chức năng
            btnAdd.Click += async (s, e) => await AddStudentAsync();
            btnUpdate.Click += async (s, e) => await UpdateStudentAsync();
            btnDel.Click += async (s, e) => await DeleteStudentAsync();
            btnReset.Click += (s, e) => ClearInputFields();
        }

        #region Initialization & Data Loading

        private async Task Form_LoadAsync()
        {
            LoadGenders();
            await LoadClassesToComboBoxAsync();
            await LoadStudentsAsync();
        }

        private void LoadGenders()
        {
            cboGioitinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioitinh.Items.Clear();
            cboGioitinh.Items.AddRange(new[] { "Nam", "Nữ", "Khác" });
            cboGioitinh.SelectedIndex = -1;
        }

        private async Task LoadClassesToComboBoxAsync()
        {
            ClassServiceClient client = null;
            try
            {
                client = new ClassServiceClient();
                var classes = await Task.Run(() => client.GetAllClasses());

                cmbMalop.DataSource = classes;
                cmbMalop.DisplayMember = "TenLop";
                cmbMalop.ValueMember = "MaLop";
                cmbMalop.SelectedIndex = -1;

                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Không thể tải danh sách Lớp học: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadStudentsAsync()
        {
            StudentServiceClient client = null;
            try
            {
                client = new StudentServiceClient();
                var students = await Task.Run(() => client.GetAllStudent());

                _studentDataSource = new BindingList<StudentDto>((students ?? new StudentDto[0]).ToList());

                dgvStudents.AutoGenerateColumns = false;
                dgvStudents.DataSource = _studentDataSource;

                ConfigureGridColumns();

                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Không thể tải danh sách Sinh viên: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            if (_isGridConfigured) return;

            dgvStudents.Columns.Clear();
            dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;

            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSV", HeaderText = "Mã SV", Name = "colMaSV", Width = 80 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Họ Tên", Name = "colHoTen", Width = 150 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GioiTinh", HeaderText = "Giới Tính", Name = "colGioiTinh", Width = 80 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NamSinh", HeaderText = "Năm Sinh", Name = "colNamSinh", Width = 80 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiaChi", HeaderText = "Địa Chỉ", Name = "colDiaChi" });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Name = "colEmail" });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Sdt", HeaderText = "SĐT", Name = "colSdt", Width = 100 });
            dgvStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaLop", HeaderText = "Mã Lớp", Name = "colMaLop", Width = 100 });

            _isGridConfigured = true;
        }

        #endregion

        #region UI Interactions (Grid to Form)

        private void DgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _studentDataSource.Count) return;

            var selectedStudent = _studentDataSource[e.RowIndex];
            if (selectedStudent == null) return;

            // Đổ dữ liệu từ Object lên các Control nhập liệu
            txtMasinhvien.Text = selectedStudent.MaSV;
            txtHoten.Text = selectedStudent.HoTen;
            cboGioitinh.SelectedItem = selectedStudent.GioiTinh;
            txtNamsinh.Text = selectedStudent.NamSinh?.ToString() ?? "";
            txtDiachi.Text = selectedStudent.DiaChi;
            txtEmail.Text = selectedStudent.Email;
            txtSdt.Text = selectedStudent.Sdt;
            cmbMalop.SelectedValue = selectedStudent.MaLop;

            // KHÓA MÃ SV: Bảo vệ Data Integrity không cho phép người dùng sửa khóa chính
            txtMasinhvien.Enabled = false;
        }

        private void ClearInputFields()
        {
            txtMasinhvien.Clear();
            txtHoten.Clear();
            cboGioitinh.SelectedIndex = -1;
            txtNamsinh.Clear();
            txtDiachi.Clear();
            txtEmail.Clear();
            txtSdt.Clear();
            cmbMalop.SelectedIndex = -1;

            txtMasinhvien.Enabled = true; // Mở khóa mã SV để cho phép nhập Thêm mới
            txtMasinhvien.Focus();
        }

        #endregion

        #region CRUD Operations

        private async Task AddStudentAsync()
        {
            var newStudent = GetStudentDtoFromUI();
            if (!ValidateInput(newStudent)) return;

            StudentServiceClient client = null;
            try
            {
                client = new StudentServiceClient();
                bool isAdded = await Task.Run(() => client.AddStudent(newStudent));

                if (isAdded)
                {
                    MessageBox.Show("Thêm sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadStudentsAsync();
                }
                else
                {
                    MessageBox.Show("Mã sinh viên đã tồn tại hoặc lỗi dữ liệu hệ thống.", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text) || txtMasinhvien.Enabled)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên từ danh sách để cập nhật.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var updatedStudent = GetStudentDtoFromUI();
            if (!ValidateInput(updatedStudent)) return;

            StudentServiceClient client = null;
            try
            {
                client = new StudentServiceClient();
                bool isUpdated = await Task.Run(() => client.UpdateStudent(updatedStudent.MaSV, updatedStudent));

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadStudentsAsync();
                }
                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeleteStudentAsync()
        {
            if (string.IsNullOrWhiteSpace(txtMasinhvien.Text) || txtMasinhvien.Enabled)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên từ danh sách để xóa.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên [{txtMasinhvien.Text}] không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            StudentServiceClient client = null;
            try
            {
                client = new StudentServiceClient();
                bool isDeleted = await Task.Run(() => client.DeleteStudent(txtMasinhvien.Text));

                if (isDeleted)
                {
                    MessageBox.Show("Xóa sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadStudentsAsync();
                }
                else
                {
                    MessageBox.Show("Không thể xóa. Sinh viên có thể không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Helpers & Validation

        private StudentDto GetStudentDtoFromUI()
        {
            int? parsedNamSinh = null;
            if (int.TryParse(txtNamsinh.Text.Trim(), out int ns))
            {
                parsedNamSinh = ns;
            }

            return new StudentDto
            {
                MaSV = txtMasinhvien.Text.Trim(),
                HoTen = txtHoten.Text.Trim(),
                GioiTinh = cboGioitinh.SelectedItem?.ToString(),
                NamSinh = parsedNamSinh,
                DiaChi = string.IsNullOrWhiteSpace(txtDiachi.Text) ? null : txtDiachi.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim(),
                Sdt = string.IsNullOrWhiteSpace(txtSdt.Text) ? null : txtSdt.Text.Trim(),
                MaLop = cmbMalop.SelectedValue?.ToString()
            };
        }

        private bool ValidateInput(StudentDto dto)
        {
            // 1. Kiểm tra Not Null
            if (string.IsNullOrWhiteSpace(dto.MaSV))
            {
                MessageBox.Show("Mã sinh viên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.HoTen))
            {
                MessageBox.Show("Họ tên sinh viên không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.MaLop))
            {
                MessageBox.Show("Vui lòng chọn Lớp cho sinh viên.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // 2. Kiểm tra Business Logic
            if (!string.IsNullOrWhiteSpace(txtNamsinh.Text) && !dto.NamSinh.HasValue)
            {
                MessageBox.Show("Năm sinh phải là số nguyên hợp lệ.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dto.NamSinh.HasValue && (dto.NamSinh.Value < 1950 || dto.NamSinh.Value > DateTime.Now.Year))
            {
                MessageBox.Show($"Năm sinh phải nằm trong khoảng từ 1950 đến {DateTime.Now.Year}.", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(dto.Email))
                {
                    MessageBox.Show("Định dạng Email không hợp lệ (VD: abc@gmail.com).", "Sai định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void AbortWcfClient(ICommunicationObject client)
        {
            if (client == null) return;
            try
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Abort();
                }
            }
            catch { /* Im lặng triệt tiêu exception để tránh crash luồng chính */ }
        }

        #endregion
    }
}