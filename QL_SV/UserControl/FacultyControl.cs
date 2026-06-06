using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using QL_SV.FacultyServiceReference;

namespace QLSV.WinForms
{
    public partial class FacultyControl : UserControl
    {
        private BindingList<FacultyDto> _facultyDataSource;

        public FacultyControl()
        {
            InitializeComponent();
            InitGridConfigurations();
            RegisterControlEvents();
        }

        private void RegisterControlEvents()
        {
            this.Load += async (s, e) => await LoadFacultiesAsync();
        }

        private void InitGridConfigurations()
        {
            sfDGFaculties.AllowEditing = true;
            sfDGFaculties.AllowDeleting = true;
            sfDGFaculties.AddNewRowPosition = RowPosition.Bottom;
            sfDGFaculties.SelectionMode = GridSelectionMode.Single;
            sfDGFaculties.EditMode = EditMode.DoubleClick;

            // Đăng ký các sự kiện điều khiển nút bấm của WinForms SfDataGrid
            sfDGFaculties.QueryCellStyle += sfDGFaculties_QueryCellStyle;
            sfDGFaculties.CellButtonClick += sfDGFaculties_CellButtonClick;
            sfDGFaculties.RowValidating += sfDGFaculties_RowValidating;
        }
        private void sfDGFaculties_RowValidating(object sender, RowValidatingEventArgs e)
        {
            // Lấy dữ liệu dòng đang thực hiện kiểm tra
            var rowData = e.DataRow.RowData as FacultyDto;
            if (rowData == null) return;

            // XỬ LÝ DÒNG THÊM MỚI (AddNewRow) KHI CLICK CHUỘT RA NGOÀI
            if (sfDGFaculties.IsAddNewRowIndex(e.DataRow.RowIndex))
            {
                // Nếu dòng thêm mới hoàn toàn trống (User click nhầm vào rồi click ra, chưa gõ gì)
                // Bỏ qua không báo lỗi để lưới tự động hủy dòng trống này một cách tự nhiên
                if (string.IsNullOrWhiteSpace(rowData.MaKhoa) && string.IsNullOrWhiteSpace(rowData.TenKhoa))
                {
                    return;
                }
            }

            // BỘ LỌC KIỂM TRA DỮ LIỆU (Chỉ chặn lại nếu họ gõ dở nửa chừng rồi bỏ đi)
            if (string.IsNullOrWhiteSpace(rowData.MaKhoa))
            {
                e.IsValid = false;
                e.ErrorMessage = "Mã khoa không được bỏ trống.";
                return;
            }

            if (string.IsNullOrWhiteSpace(rowData.TenKhoa))
            {
                e.IsValid = false;
                e.ErrorMessage = "Tên khoa không được bỏ trống.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(rowData.Email))
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(rowData.Email.Trim(), emailPattern))
                {
                    e.IsValid = false;
                    e.ErrorMessage = "Định dạng Email không hợp lệ.";
                    return;
                }
            }
        }
        #region Data Loading & Column Initialization

        private async Task LoadFacultiesAsync()
        {
            try
            {
                using (var client = new FacultyServiceClient())
                {
                    FacultyDto[] facultyDtos = await Task.Run(() => client.GetAllFaculties());

                    sfDGFaculties.DataSource = null;
                    sfDGFaculties.AutoGenerateColumns = true;

                    _facultyDataSource = new BindingList<FacultyDto>(facultyDtos ?? new FacultyDto[0]);
                    sfDGFaculties.DataSource = _facultyDataSource;

                    // Khởi tạo cột hành động
                    CreateActionColumns();
                    ConfigureFacultyGridColumns();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Không thể tải dữ liệu danh sách Khoa");
            }
        }

        private void CreateActionColumns()
        {
            // FIX lỗi CS1503: Tìm và xóa cột cũ bằng cách truyền đối tượng GridColumn vật lý
            var existingSaveCol = sfDGFaculties.Columns["ColSave"];
            if (existingSaveCol != null) sfDGFaculties.Columns.Remove(existingSaveCol);

            var existingDeleteCol = sfDGFaculties.Columns["ColDelete"];
            if (existingDeleteCol != null) sfDGFaculties.Columns.Remove(existingDeleteCol);

            // Tạo cột nút bấm Lưu / Cập nhật
            GridButtonColumn saveBtnCol = new GridButtonColumn();
            saveBtnCol.MappingName = "ColSave";
            saveBtnCol.HeaderText = "Hành động";
            saveBtnCol.AllowDefaultButtonText = false;
            saveBtnCol.Width = 100;

            // Tạo cột nút bấm Xóa
            GridButtonColumn deleteBtnCol = new GridButtonColumn();
            deleteBtnCol.MappingName = "ColDelete";
            deleteBtnCol.HeaderText = "Xóa bỏ";
            deleteBtnCol.AllowDefaultButtonText = false;
            deleteBtnCol.Width = 90;

            sfDGFaculties.Columns.Add(saveBtnCol);
            sfDGFaculties.Columns.Add(deleteBtnCol);
        }

        private void ConfigureFacultyGridColumns()
        {
            if (sfDGFaculties.Columns.Count == 0) return;

            sfDGFaculties.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

            string[] fields = { "MaKhoa", "TenKhoa", "DiaChi", "Email", "Sdt" };
            string[] headers = { "Mã Khoa", "Tên Khoa", "Địa chỉ", "Email", "SĐT" };

            for (int i = 0; i < fields.Length; i++)
            {
                var column = sfDGFaculties.Columns[fields[i]];
                if (column != null) column.HeaderText = headers[i];
            }
        }

        #endregion

        #region UI Customization (FIX lỗi CellValue -> Đổi sang DisplayText)

        private void sfDGFaculties_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "ColSave")
            {
                if (sfDGFaculties.IsAddNewRowIndex(e.RowIndex))
                {
                    e.DisplayText = "Thêm"; // Đổi chữ nút bấm thành "Thêm" trên WinForms
                    e.Style.BackColor = Color.LightGreen;
                }
                else
                {
                    e.DisplayText = "Cập nhật";
                }
            }
            else if (e.Column.MappingName == "ColDelete")
            {
                if (sfDGFaculties.IsAddNewRowIndex(e.RowIndex))
                {
                    e.DisplayText = ""; // Dòng thêm mới thì ẩn nút xóa đi
                }
                else
                {
                    e.DisplayText = "Xóa";
                }
            }
        }

        #endregion

        #region Explicit Action Handlers (FIX lỗi lấy dữ liệu dòng)

        private async void sfDGFaculties_CellButtonClick(object sender, CellButtonClickEventArgs e)
        {
            FacultyDto rowData = null;

            // Kiểm tra và lấy dữ liệu dòng tương ứng trên WinForms
            if (sfDGFaculties.IsAddNewRowIndex(e.RowIndex))
            {
                // Đã bổ sung .TableControl để truy cập đúng API của Syncfusion WinForms
                var gridRowInfo = sfDGFaculties.TableControl.RowGenerator.Items.FirstOrDefault(item => item.RowIndex == e.RowIndex);
                rowData = gridRowInfo?.RowData as FacultyDto;
            }
            else
            {
                // Dòng dữ liệu thông thường
                var recordIndex = sfDGFaculties.TableControl.ResolveToRecordIndex(e.RowIndex);
                if (recordIndex >= 0 && recordIndex < sfDGFaculties.View.Records.Count)
                {
                    rowData = sfDGFaculties.View.Records[recordIndex].Data as FacultyDto;
                }
            }

            if (rowData == null) return;

            // Xử lý khi bấm nút THÊM / CẬP NHẬT
            if (e.Column.MappingName == "ColSave")
            {
                if (!ValidateRowData(rowData)) return;

                try
                {
                    using (var client = new FacultyServiceClient())
                    {
                        if (sfDGFaculties.IsAddNewRowIndex(e.RowIndex))
                        {
                            bool isAdded = await Task.Run(() => client.AddFaculty(rowData));
                            if (isAdded)
                            {
                                MessageBox.Show("Thêm mới khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadFacultiesAsync(); // Tải lại lưới để làm sạch dòng AddNewRow
                            }
                        }
                        else
                        {
                            bool isUpdated = await Task.Run(() => client.UpdateFaculty(rowData.MaKhoa, rowData));
                            if (isUpdated)
                            {
                                MessageBox.Show("Cập nhật thông tin khoa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Lỗi kết nối đồng bộ API WCF");
                }
            }
            // Xử lý khi bấm nút XÓA
            else if (e.Column.MappingName == "ColDelete")
            {
                if (sfDGFaculties.IsAddNewRowIndex(e.RowIndex)) return;

                var confirmation = MessageBox.Show($"Bạn có chắc chắn muốn xóa khoa [{rowData.MaKhoa}]?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes) return;

                try
                {
                    using (var client = new FacultyServiceClient())
                    {
                        bool isDeleted = await Task.Run(() => client.DeleteFaculty(rowData.MaKhoa));
                        if (isDeleted)
                        {
                            MessageBox.Show("Xóa khoa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadFacultiesAsync();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa bản ghi do có ràng buộc dữ liệu.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Lỗi trong quá trình xóa dữ liệu");
                }
            }
        }

        #endregion

        #region Helper & Validation Methods

        private bool ValidateRowData(FacultyDto data)
        {
            if (data == null) return false;

            if (string.IsNullOrWhiteSpace(data.MaKhoa))
            {
                MessageBox.Show("Mã khoa không được phép để trống.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.TenKhoa))
            {
                MessageBox.Show("Tên khoa không được phép để trống.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!string.IsNullOrWhiteSpace(data.Email))
            {
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(data.Email.Trim(), emailPattern))
                {
                    MessageBox.Show("Định dạng Email không chính xác.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        private void HandleException(Exception ex, string customMessage)
        {
            MessageBox.Show($"{customMessage}.\nChi tiết kỹ thuật: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}