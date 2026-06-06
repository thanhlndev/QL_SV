using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.WinForms.DataGrid;
using Syncfusion.WinForms.DataGrid.Enums;
using Syncfusion.WinForms.DataGrid.Events;
using QL_SV.StudentServiceReference;

namespace QLSV.WinForms
{
    public partial class StudentControl : UserControl
    {
        private BindingList<StudentDto> _studentDataSource;
        private bool _isColumnsInitialized = false;

        public StudentControl()
        {
            InitializeComponent();
            InitGridConfigurations();
            RegisterControlEvents();
        }

        private void RegisterControlEvents()
        {
            this.Load += async (s, e) => await LoadStudentsAsync();
        }

        /// <summary>
        /// Cấu hình Grid và khởi tạo các cột tĩnh một lần duy nhất
        /// </summary>
        private void InitGridConfigurations()
        {
            sfDGStudents.AllowEditing = true;
            sfDGStudents.AllowDeleting = true;
            sfDGStudents.AddNewRowPosition = RowPosition.Bottom;
            sfDGStudents.SelectionMode = GridSelectionMode.Single;
            sfDGStudents.EditMode = EditMode.DoubleClick;

            sfDGStudents.QueryCellStyle += sfDGStudents_QueryCellStyle;
            sfDGStudents.CellButtonClick += sfDGStudents_CellButtonClick;
        }

        #region Data Loading & Column Initialization

        private async Task LoadStudentsAsync()
        {
            StudentServiceClient client = null;
            try
            {
                client = new StudentServiceClient();
                StudentDto[] studentDtos = await Task.Run(() => client.GetAllStudent());

                sfDGStudents.AutoGenerateColumns = false;

                _studentDataSource = new BindingList<StudentDto>((studentDtos ?? new StudentDto[0]).ToList());
                sfDGStudents.DataSource = _studentDataSource;

                ConfigureStudentGridColumns();

                client.Close();
            }
            catch (Exception ex)
            {
                AbortWcfClient(client);
                HandleException(ex, "Không thể tải dữ liệu danh sách Sinh viên từ Service");
            }
        }

        private void CreateActionColumns()
        {
            if (sfDGStudents.Columns.Any(c => c.MappingName == "ColSave" || c.MappingName == "ColDelete"))
                return;

            GridButtonColumn saveBtnCol = new GridButtonColumn
            {
                MappingName = "ColSave",
                HeaderText = "Hành động",
                AllowDefaultButtonText = false,
                Width = 100
            };

            GridButtonColumn deleteBtnCol = new GridButtonColumn
            {
                MappingName = "ColDelete",
                HeaderText = "Xóa bỏ",
                AllowDefaultButtonText = false,
                Width = 90
            };

            sfDGStudents.Columns.Add(saveBtnCol);
            sfDGStudents.Columns.Add(deleteBtnCol);
        }

        private void ConfigureStudentGridColumns()
        {
            sfDGStudents.Columns.Clear();

            sfDGStudents.AutoSizeColumnsMode = AutoSizeColumnsMode.Fill;

            string[] fields = { "MaSV", "HoTen", "GioiTinh", "NamSinh", "DiaChi", "Email", "MaLop" };
            string[] headers = { "Mã Sinh Viên", "Họ Và Tên", "Giới Tính", "Năm Sinh", "Địa Chỉ", "Email", "Mã Lớp Học" };

            for (int i = 0; i < fields.Length; i++)
            {
                GridTextColumn textColumn = new GridTextColumn
                {
                    MappingName = fields[i],
                    HeaderText = headers[i]
                };

                sfDGStudents.Columns.Add(textColumn);
            }

            CreateActionColumns();
        }
        #endregion

        #region UI Customization

        private void sfDGStudents_QueryCellStyle(object sender, QueryCellStyleEventArgs e)
        {
            if (e.Column.MappingName == "ColSave")
            {
                if (sfDGStudents.IsAddNewRowIndex(e.RowIndex))
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
                e.DisplayText = sfDGStudents.IsAddNewRowIndex(e.RowIndex) ? string.Empty : "Xóa";
            }
        }

        #endregion

        #region Explicit Action Handlers

        private async void sfDGStudents_CellButtonClick(object sender, CellButtonClickEventArgs e)
        {
            StudentDto rowData = null;

            if (sfDGStudents.IsAddNewRowIndex(e.RowIndex))
            {
                var gridRowInfo = sfDGStudents.TableControl.RowGenerator.Items.FirstOrDefault(item => item.RowIndex == e.RowIndex);
                rowData = gridRowInfo?.RowData as StudentDto;
            }
            else
            {
                var recordIndex = sfDGStudents.TableControl.ResolveToRecordIndex(e.RowIndex);
                if (recordIndex >= 0 && recordIndex < sfDGStudents.View.Records.Count)
                {
                    rowData = sfDGStudents.View.Records[recordIndex].Data as StudentDto;
                }
            }

            if (rowData == null) return;

            if (e.Column.MappingName == "ColSave")
            {
                if (!ValidateRowData(rowData)) return;

                StudentServiceClient client = null;
                try
                {
                    client = new StudentServiceClient();

                    if (sfDGStudents.IsAddNewRowIndex(e.RowIndex))
                    {
                        bool isAdded = await Task.Run(() => client.AddStudent(rowData));
                        if (isAdded)
                        {
                            MessageBox.Show("Thêm mới sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadStudentsAsync();
                        }
                    }
                    else
                    {
                        bool isUpdated = await Task.Run(() => client.UpdateStudent(rowData.MaSV, rowData));

                        if (isUpdated)
                        {
                            MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại. Vui lòng kiểm tra lại dữ liệu.", "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    client.Close();
                }
                catch (Exception ex)
                {
                    AbortWcfClient(client);
                    HandleException(ex, "Lỗi kết nối đồng bộ hệ thống WCF Sinh viên khi Lưu");
                }
            }
            else if (e.Column.MappingName == "ColDelete")
            {
                if (sfDGStudents.IsAddNewRowIndex(e.RowIndex)) return;

                var confirmation = MessageBox.Show($"Bạn có chắc chắn muốn xóa sinh viên [{rowData.MaSV}]?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation != DialogResult.Yes) return;

                StudentServiceClient client = null;
                try
                {
                    client = new StudentServiceClient();
                    bool isDeleted = await Task.Run(() => client.DeleteStudent(rowData.MaSV));

                    if (isDeleted)
                    {
                        MessageBox.Show("Xóa thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadStudentsAsync();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa bản ghi. Sinh viên không tồn tại hoặc có ràng buộc dữ liệu.", "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    client.Close();
                }
                catch (Exception ex)
                {
                    AbortWcfClient(client);
                    HandleException(ex, "Lỗi trong quá trình xóa dữ liệu sinh viên");
                }
            }
        }

        #endregion

        #region Helper & Data Validation Methods

        private bool ValidateRowData(StudentDto data)
        {
            if (data == null) return false;

            if (string.IsNullOrWhiteSpace(data.MaSV))
            {
                MessageBox.Show("Mã sinh viên không được phép để trống.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.HoTen))
            {
                MessageBox.Show("Họ và tên sinh viên không được phép để trống.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.MaLop))
            {
                MessageBox.Show("Vui lòng chỉ định Mã lớp học cho sinh viên này.", "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Giải phóng WCF Client theo chuẩn Pattern khuyến nghị của Microsoft
        /// </summary>
        private void AbortWcfClient(StudentServiceClient client)
        {
            if (client == null) return;
            try
            {
                if (client.State != CommunicationState.Closed)
                {
                    client.Abort();
                }
            }
            catch
            {
                // Triệt tiêu exception khi abort để tránh crash luồng phụ
            }
        }

        private void HandleException(Exception ex, string customMessage)
        {
            MessageBox.Show($"{customMessage}.\nChi tiết kỹ thuật: {ex.Message}", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion
    }
}