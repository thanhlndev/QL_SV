using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using QL_SV.FacultyServiceReference;

namespace QLSV.WinForms
{
    public partial class FacultyForm : Form
    {
        public FacultyForm()
        {
            InitializeComponent();
            RegisterFormEvents();
        }

        /// <summary>
        /// Đăng ký tập trung tất cả sự kiện của các Control trên Form
        /// </summary>
        private void RegisterFormEvents()
        {
            this.Load += FacultyForm_Load;
            this.btnAdd.Click += async (s, e) => await BtnAdd_ClickAsync();
            this.btnEdit.Click += async (s, e) => await BtnEdit_ClickAsync();
            this.btnDel.Click += async (s, e) => await BtnDelete_ClickAsync();
            this.btnReset.Click += BtnReset_Click;
            this.btnGetFaculty.Click += async (s, e) => await btnGetFaculty_ClickAsync();
            this.dgvFaculty.CellClick += dgvFaculty_CellClick;
        }

        #region CRUD Operations

        private async Task BtnAdd_ClickAsync()
        {
            if (!ValidateFacultyInput()) return;

            var faculty = PackageFacultyDto();

            try
            {
                using (var client = new FacultyServiceClient())
                {
                    await Task.Run(() => client.AddFaculty(faculty));

                    MessageBox.Show("Thêm thông tin khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetFormFields();
                    await LoadFacultiesAsync();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Lỗi khi thêm mới khoa");
            }
        }

        private async Task BtnEdit_ClickAsync()
        {
            var selectedMaKhoa = GetSelectedMaKhoa();
            if (string.IsNullOrEmpty(selectedMaKhoa))
            {
                MessageBox.Show("Hãy chọn khoa trên bảng dữ liệu trước khi chỉnh sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateFacultyInput()) return;

            var facultyDto = PackageFacultyDto();
            facultyDto.MaKhoa = selectedMaKhoa;

            try
            {
                using (var client = new FacultyServiceClient())
                {
                    bool isUpdated = await Task.Run(() => client.UpdateFaculty(selectedMaKhoa, facultyDto));

                    if (isUpdated)
                    {
                        MessageBox.Show("Cập nhật thông tin khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadFacultiesAsync();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại. Không tìm thấy mã khoa tương ứng trong hệ thống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Lỗi khi cập nhật dữ liệu");
            }
        }

        private async Task BtnDelete_ClickAsync()
        {
            var selectedMaKhoa = GetSelectedMaKhoa();
            if (string.IsNullOrEmpty(selectedMaKhoa))
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa dữ liệu trên bảng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirmation = MessageBox.Show($"Bạn có chắc chắn muốn xóa khoa {selectedMaKhoa} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation != DialogResult.Yes) return;

            try
            {
                using (var client = new FacultyServiceClient())
                {
                    bool isDeleted = await Task.Run(() => client.DeleteFaculty(selectedMaKhoa));
                    if (isDeleted)
                    {
                        MessageBox.Show("Xoá thông tin khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetFormFields();
                        await LoadFacultiesAsync();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xoá thông tin khoa. Dữ liệu có thể đang được sử dụng ở bảng khác.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Lỗi khi thực hiện xóa dữ liệu");
            }
        }

        private async Task btnGetFaculty_ClickAsync()
        {
            try
            {
                using (var client = new FacultyServiceClient())
                {
                    FacultyDto[] facultyDtos = await Task.Run(() => client.GetAllFaculties());

                    if (facultyDtos != null && facultyDtos.Length > 0)
                    {
                        var lines = facultyDtos.Select(f => $"ID: {f.MaKhoa} - Tên Khoa: {f.TenKhoa}");
                        string result = string.Join(Environment.NewLine, lines);
                        MessageBox.Show(result, "Danh sách Khoa");
                    }
                    else
                    {
                        MessageBox.Show("Không có dữ liệu khoa nào được trả về.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Lỗi khi gọi Service lấy danh sách");
            }
        }

        #endregion

        #region Data Loading & UI Management

        private async void FacultyForm_Load(object sender, EventArgs e)
        {
            await LoadFacultiesAsync();
        }

        private async Task LoadFacultiesAsync()
        {
            try
            {
                using (var client = new FacultyServiceClient())
                {
                    FacultyDto[] facultyDtos = await Task.Run(() => client.GetAllFaculties());

                    dgvFaculty.DataSource = null;
                    dgvFaculty.AutoGenerateColumns = true;
                    dgvFaculty.DataSource = facultyDtos;

                    ConfigureFacultyGridColumns();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Không thể tải dữ liệu danh sách Khoa");
            }
        }

        private void ConfigureFacultyGridColumns()
        {
            if (dgvFaculty.Columns.Count == 0) return;

            dgvFaculty.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            string[] fields = { "MaKhoa", "TenKhoa", "DiaChi", "Email", "Sdt" };
            string[] headers = { "Mã Khoa", "Tên Khoa", "Địa chỉ", "Email", "SĐT" };

            for (int i = 0; i < fields.Length; i++)
            {
                var column = dgvFaculty.Columns[fields[i]];
                if (column != null)
                {
                    column.HeaderText = headers[i];
                    column.DisplayIndex = i;
                }
            }
        }

        private void dgvFaculty_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvFaculty.Rows.Count) return;

            var row = dgvFaculty.Rows[e.RowIndex];

            txtMakhoa.Text = row.Cells["MaKhoa"].Value?.ToString()?.Trim() ?? string.Empty;
            txtMakhoa.ReadOnly = true;

            txtTenkhoa.Text = row.Cells["TenKhoa"].Value?.ToString()?.Trim() ?? string.Empty;
            txtDiachi.Text = row.Cells["DiaChi"].Value?.ToString()?.Trim() ?? string.Empty;
            txtEmail.Text = row.Cells["Email"].Value?.ToString()?.Trim() ?? string.Empty;
            txtSdt.Text = row.Cells["Sdt"].Value?.ToString()?.Trim() ?? string.Empty;
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            ResetFormFields();
        }

        #endregion

        #region Helper Methods

        private void ResetFormFields()
        {
            txtMakhoa.Clear();
            txtMakhoa.ReadOnly = false;
            txtTenkhoa.Clear();
            txtDiachi.Clear();
            txtEmail.Clear();
            txtSdt.Clear();
            if (dgvFaculty.SelectedRows.Count > 0) dgvFaculty.ClearSelection();
        }

        private FacultyDto PackageFacultyDto()
        {
            return new FacultyDto
            {
                MaKhoa = txtMakhoa.Text.Trim(),
                TenKhoa = txtTenkhoa.Text.Trim(),
                DiaChi = txtDiachi.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Sdt = txtSdt.Text.Trim()
            };
        }

        private string GetSelectedMaKhoa()
        {
            if (dgvFaculty.SelectedRows.Count == 0) return null;
            return dgvFaculty.SelectedRows[0].Cells["MaKhoa"].Value?.ToString();
        }

        /// <summary>
        /// Bộ lọc kiểm tra tính đúng đắn của dữ liệu đầu vào (Client-side Validation)
        /// </summary>
        private bool ValidateFacultyInput()
        {
            if (string.IsNullOrWhiteSpace(txtMakhoa.Text))
            {
                MessageBox.Show("Mã khoa không được bỏ trống.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMakhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenkhoa.Text))
            {
                MessageBox.Show("Tên khoa không được bỏ trống.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenkhoa.Focus();
                return false;
            }

            // Kiểm tra định dạng Email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(txtEmail.Text.Trim(), emailPattern))
                {
                    MessageBox.Show("Định dạng Email không hợp lệ.", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private void HandleException(Exception ex, string customMessage)
        {
            MessageBox.Show($"{customMessage}.\nChi tiết kỹ thuật: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void pText_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion
    }
}