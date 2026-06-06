using System;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_SV.ClassServiceRef;
using QL_SV.FacultyServiceReference;

namespace QLSV.WinForms
{
    public partial class ClassForm : Form
    {
        private BindingList<ClassDto> _classDataSource;
        private bool _isGridConfigured = false;

        public ClassForm()
        {
            InitializeComponent();
            RegisterEvents();
        }

        private void RegisterEvents()
        {
            this.Load += async (s, e) => await Form_LoadAsync();

            // Đăng ký sự kiện click cho DataGridView
            dgvClasses.CellClick += DgvClasses_CellClick;

            // Đăng ký sự kiện cho các nút bấm (Giả định bạn đã tạo các nút này trên giao diện)
            btnAdd.Click += async (s, e) => await AddClassAsync();
            btnUpdate.Click += async (s, e) => await UpdateClassAsync();
            btnDel.Click += async (s, e) => await DeleteClassAsync();
            btnReset.Click += (s, e) => ClearInputFields();
        }

        #region Initialization & Data Loading

        private async Task Form_LoadAsync()
        {
            // Tải danh sách Khoa trước để đổ vào ComboBox, sau đó mới tải danh sách Lớp
            await LoadFacultiesToComboBoxAsync();
            await LoadClassesAsync();
        }

        private async Task LoadFacultiesToComboBoxAsync()
        {
            FacultyServiceClient client = null;
            try
            {
                client = new FacultyServiceClient();
                var faculties = await Task.Run(() => client.GetAllFacultiesAsync());

                // Sử dụng ComboBox chuẩn của WinForms
                cmbMaKhoa.DataSource = faculties;
                cmbMaKhoa.DisplayMember = "TenKhoa"; // Chữ hiển thị cho người dùng xem
                cmbMaKhoa.ValueMember = "MaKhoa";    // Giá trị thực tế ngầm định (Foreign Key)
                cmbMaKhoa.SelectedIndex = -1;        // Mặc định không chọn gì cả

                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Không thể tải danh sách Khoa: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadClassesAsync()
        {
            ClassServiceClient client = null;
            try
            {
                client = new ClassServiceClient();
                var classes = await Task.Run(() => client.GetAllClasses());

                _classDataSource = new BindingList<ClassDto>((classes ?? new ClassDto[0]).ToList());

                // Tắt tự động sinh cột để kiểm soát bằng tay
                dgvClasses.AutoGenerateColumns = false;
                dgvClasses.DataSource = _classDataSource;

                ConfigureGridColumns();

                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Không thể tải danh sách Lớp học: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            if (_isGridConfigured) return; // Chỉ cấu hình 1 lần duy nhất

            dgvClasses.Columns.Clear();
            dgvClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClasses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClasses.MultiSelect = false;
            dgvClasses.ReadOnly = true; // Lưới chỉ để xem, nhập liệu qua TextBox

            dgvClasses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaLop",
                HeaderText = "Mã Lớp Học",
                Name = "colMaLop"
            });

            dgvClasses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenLop",
                HeaderText = "Tên Lớp Học",
                Name = "colTenLop"
            });

            dgvClasses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email Liên Hệ",
                Name = "colEmail"
            });

            dgvClasses.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MaKhoa",
                HeaderText = "Mã Khoa Quản Lý",
                Name = "colMaKhoa"
            });

            _isGridConfigured = true;
        }

        #endregion

        #region UI Interactions (Grid to Form)

        private void DgvClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex < 0 || e.RowIndex >= _classDataSource.Count) return;

            var selectedClass = _classDataSource[e.RowIndex];
            if (selectedClass == null) return;

            txtMalop.Text = selectedClass.MaLop;
            txtTenlop.Text = selectedClass.TenLop;
            txtEmail.Text = selectedClass.Email;
            cmbMaKhoa.SelectedValue = selectedClass.MaKhoa;

            txtMalop.Enabled = false;
        }

        private void ClearInputFields()
        {
            txtMalop.Clear();
            txtTenlop.Clear();
            txtEmail.Clear();
            cmbMaKhoa.SelectedIndex = -1;

            txtMalop.Enabled = true; // Mở khóa mã lớp để chuẩn bị Thêm mới
            txtMalop.Focus();
        }

        #endregion

        #region CRUD Operations

        private async Task AddClassAsync()
        {
            var newClass = GetClassDtoFromUI();
            if (!ValidateInput(newClass)) return;

            ClassServiceClient client = null;
            try
            {
                client = new ClassServiceClient();
                bool isAdded = await Task.Run(() => client.AddClass(newClass));

                if (isAdded)
                {
                    MessageBox.Show("Thêm lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadClassesAsync();
                }
                else
                {
                    MessageBox.Show("Mã lớp đã tồn tại hoặc lỗi dữ liệu.", "Thêm thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task UpdateClassAsync()
        {
            if (string.IsNullOrWhiteSpace(txtMalop.Text) || txtMalop.Enabled)
            {
                MessageBox.Show("Vui lòng chọn một lớp học từ danh sách bên dưới để cập nhật.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var updatedClass = GetClassDtoFromUI();
            if (!ValidateInput(updatedClass)) return;

            ClassServiceClient client = null;
            try
            {
                client = new ClassServiceClient();
                // Truyền đúng signature: (string maLop, ClassDto classDto)
                bool isUpdated = await Task.Run(() => client.UpdateClass(updatedClass.MaLop, updatedClass));

                if (isUpdated)
                {
                    MessageBox.Show("Cập nhật lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadClassesAsync();
                }
                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task DeleteClassAsync()
        {
            if (string.IsNullOrWhiteSpace(txtMalop.Text) || txtMalop.Enabled)
            {
                MessageBox.Show("Vui lòng chọn một lớp học từ danh sách để xóa.", "Nhắc nhở", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }

            var confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp [{txtMalop.Text}] không?\nLưu ý: Không thể xóa nếu lớp đang có sinh viên.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            ClassServiceClient client = null;
            try
            {
                client = new ClassServiceClient();
                bool isDeleted = await Task.Run(() => client.DeleteClass(txtMalop.Text));

                if (isDeleted)
                {
                    MessageBox.Show("Xóa lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields();
                    await LoadClassesAsync();
                }
                else
                {
                    MessageBox.Show("Không thể xóa lớp. Lớp học có thể chứa dữ liệu ràng buộc.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private ClassDto GetClassDtoFromUI()
        {
            return new ClassDto
            {
                MaLop = txtMalop.Text.Trim(),
                TenLop = txtTenlop.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                MaKhoa = cmbMaKhoa.SelectedValue?.ToString()
            };
        }

        private bool ValidateInput(ClassDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.MaLop))
            {
                MessageBox.Show("Mã lớp không được để trống.", "Kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.TenLop))
            {
                MessageBox.Show("Tên lớp không được để trống.", "Kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dto.MaKhoa))
            {
                MessageBox.Show("Vui lòng chọn Khoa quản lý cho lớp học này.", "Kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate sơ bộ định dạng Email nếu có nhập
            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (!emailRegex.IsMatch(dto.Email))
                {
                    MessageBox.Show("Định dạng Email không hợp lệ.", "Kiểm tra dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            catch { /* Nuốt exception trong quá trình abort để tránh sập UI */ }
        }

        #endregion
    }
}