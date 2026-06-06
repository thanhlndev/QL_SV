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
using QL_SV.ClassServiceRef;


namespace QLSV.WinForms
{
    public partial class ClassControl : UserControl
    {
        private BindingList<ClassDto> _classDataSource;

        public ClassControl()
        {
            InitializeComponent();
            InitGridConfigurations();
            RegisterControlEvents();
        }

        private void RegisterControlEvents()
        {
            this.Load += async (s, e) => await LoadClassesAsync();
        }

        /// <summary>
        /// Cấu hình các thuộc tính tương tác nâng cao cho sfDGClasses
        /// </summary>
        private void InitGridConfigurations()
        {
            sfDGClasses.AllowEditing = true;
            sfDGClasses.AllowDeleting = true;
            sfDGClasses.AddNewRowPosition = RowPosition.Bottom; // Hàng thêm mới nằm dưới cùng
            sfDGClasses.SelectionMode = GridSelectionMode.Single;
            sfDGClasses.EditMode = EditMode.DoubleClick;

            // Đăng ký các sự kiện xử lý nút bấm và giao diện động của Syncfusion WinForms
            sfDGClasses.QueryCellStyle += sfDGClasses_QueryCellStyle;
            sfDGClasses.CellButtonClick += sfDGClasses_CellButtonClick;
        }

        #region Data Loading & Column Initialization

        private async Task LoadClassesAsync()
        {
            try
            {
                using (var client = new ClassServiceClient())
                {
                    // Gọi hàm GetAllClasses từ WCF Service ẩn dưới Background Thread
                    ClassDto[] classDtos = await Task.Run(() => client.GetAllClasses());

                    sfDGClasses.DataSource = null;
                    sfDGClasses.AutoGenerateColumns = true;

                    // BEST PRACTICE: Ép mảng tĩnh về .ToList() để bọc vào BindingList, giải quyết triệt để lỗi Read-Only
                    _classDataSource = new BindingList<ClassDto>((classDtos ?? new ClassDto[0]).ToList());
                    sfDGClasses.DataSource = _classDataSource;

                    // Khởi tạo hệ thống cột hành động và cấu hình header
                    CreateActionColumns();
                    ConfigureClassGridColumns();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Không thể tải dữ liệu danh sách Lớp học");
            }
        }

        /// <summary>
        /// Khởi tạo các cột nút bấm vật lý nhúng vào cuối lưới
        /// </summary>
        private void CreateActionColumns()
        {
            var existingSaveCol = sfDGClasses.Columns["ColSave"];
            if (existingSaveCol != null) sfDGClasses.Columns.Remove(existingSaveCol);

            var existingDeleteCol = sfDGClasses.Columns["ColDelete"];
            if (existingDeleteCol != null) sfDGClasses.Columns.Remove(existingDeleteCol);

            // Cột nút Lưu / Cập nhật
            GridButtonColumn saveBtnCol = new GridButtonColumn();
            saveBtnCol.MappingName = "ColSave";
            saveBtnCol.HeaderText = "Hành động";
            saveBtnCol.AllowDefaultButtonText = false;
            saveBtnCol.Width = 100;

            // Cột nút Xóa
            GridButtonColumn deleteBtnCol = new GridButtonColumn();
            deleteBtnCol.MappingName = "ColDelete";
            deleteBtnCol.HeaderText = "Xóa bỏ";
            deleteBtnCol.AllowDefaultButtonText = false;
            deleteBtnCol.Width = 90;

            sfDGClasses.Columns.Add(saveBtnCol);
            sfDGClasses.Columns.Add(deleteBtnCol);
        }

        /// <summary>
        /// Định nghĩa lại tiêu đề hiển thị cho các trường thuộc tính của ClassDto
        /// </summary>
        private void ConfigureClassGridColumns()
        {
            if (sfDGClasses.Columns.Count == 0) return;

            sfDGClasses.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

            string[] fields = { "MaLop", "TenLop", "NienKhoa", "MaKhoa" };
            string[] headers = { "Mã Lớp", "Tên Lớp Học", "Niên Khóa", "Mã Khoa Quản Lý" };

            for (int i = 0; i < fields.Length; i++)
            {
                var column = sfDGClasses.Columns[fields[i]];
                if (column != null) column.HeaderText = headers[i];
            }
        }

        #endregion

        #region UI Customization (Đổi chữ nút bấm động theo trạng thái hàng)

        private void sfDGClasses_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "ColSave")
            {
                if (sfDGClasses.IsAddNewRowIndex(e.RowIndex))
                {
                    e.DisplayText = "Thêm";
                    e.Style.BackColor = Color.LightGreen;
                }
                else
                {
                    e.DisplayText = "Cập nhật";
                }
            }
            else if (e.Column.MappingName == "ColDelete")
            {
                if (sfDGClasses.IsAddNewRowIndex(e.RowIndex))
                {
                    e.DisplayText = ""; // Ẩn nút xóa ở hàng thêm mới
                }
                else
                {
                    e.DisplayText = "Xóa";
                }
            }
        }

        #endregion

        #region Explicit Action Handlers (Xử lý sự kiện Click nút)

        private async void sfDGClasses_CellButtonClick(object sender, CellButtonClickEventArgs e)
        {
            ClassDto rowData = null;

            // Phân tích ngữ cảnh lấy dữ liệu chuẩn WinForms Syncfusion
            if (sfDGClasses.IsAddNewRowIndex(e.RowIndex))
            {
                // Đi qua TableControl.RowGenerator để trích xuất thực thể gõ dở ở AddNewRow
                var gridRowInfo = sfDGClasses.TableControl.RowGenerator.Items.FirstOrDefault(item => item.RowIndex == e.RowIndex);
                rowData = gridRowInfo?.RowData as ClassDto;
            }
            else
            {
                // Truy vết chỉ số bản ghi thông thường
                var recordIndex = sfDGClasses.TableControl.ResolveToRecordIndex(e.RowIndex);
                if (recordIndex >= 0 && recordIndex < sfDGClasses.View.Records.Count)
                {
                    rowData = sfDGClasses.View.Records[recordIndex].Data as ClassDto;
                }
            }

            if (rowData == null) return;

            // XỬ LÝ 1: CLICK NÚT THÊM / CẬP NHẬT
            if (e.Column.MappingName == "ColSave")
            {
                if (!ValidateRowData(rowData)) return;

                try
                {
                    using (var client = new ClassServiceClient())
                    {
                        if (sfDGClasses.IsAddNewRowIndex(e.RowIndex))
                        {
                            // Thực thi gọi hàm AddClass qua WCF
                            bool isAdded = await Task.Run(() => client.AddClass(rowData));
                            if (isAdded)
                            {
                                MessageBox.Show("Thêm mới lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                await LoadClassesAsync(); // Làm sạch lưới và nạp lại data
                            }
                        }
                        else
                        {
                            // Thực thi gọi hàm UpdateClass qua WCF
                            bool isUpdated = await Task.Run(() => client.UpdateClass(rowData.MaLop, rowData));
                            if (isUpdated)
                            {
                                MessageBox.Show("Cập nhật thông tin lớp học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại hoặc không tìm thấy mã lớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Lỗi kết nối đồng bộ API WCF lớp học");
                }
            }
            // XỬ LÝ 2: CLICK NÚT XÓA
            else if (e.Column.MappingName == "ColDelete")
            {
                if (sfDGClasses.IsAddNewRowIndex(e.RowIndex)) return;

                var confirmation = MessageBox.Show($"Bạn có chắc chắn muốn xóa lớp [{rowData.MaLop}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes) return;

                try
                {
                    using (var client = new ClassServiceClient())
                    {
                        // Thực thi gọi hàm DeleteClass qua WCF
                        bool isDeleted = await Task.Run(() => client.DeleteClass(rowData.MaLop));
                        if (isDeleted)
                        {
                            MessageBox.Show("Xóa lớp học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadClassesAsync();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa. Lớp học hiện tại đang chứa dữ liệu sinh viên liên kết.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Lỗi trong quá trình xóa lớp học");
                }
            }
        }

        #endregion

        #region Helper & Data Validation Methods

        private bool ValidateRowData(ClassDto data)
        {
            if (data == null) return false;

            if (string.IsNullOrWhiteSpace(data.MaLop))
            {
                MessageBox.Show("Mã lớp học không được phép để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.TenLop))
            {
                MessageBox.Show("Tên lớp học không được phép để trống.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.MaKhoa))
            {
                MessageBox.Show("Vui lòng chỉ định Mã khoa quản lý cho lớp học này.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
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