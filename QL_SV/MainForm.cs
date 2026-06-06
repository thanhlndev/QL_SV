using System;
using System.Windows.Forms;
using Syncfusion.Windows.Forms;

namespace QLSV.WinForms
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
            RegisterMainEvents();
        }

        /// <summary>
        /// Đăng ký tập trung các sự kiện click của MainForm
        /// </summary>
        private void RegisterMainEvents()
        {
            // Gán sự kiện click cho nút Khoa
            this.btnFaculty.Click += BtnFaculty_Click;
            this.btnStudent.Click += BtnStudent_Click;
            this.btnClass.Click += BtnClass_Click;
        }

        /// <summary>
        /// Sự kiện click nút trong Sidebar
        /// </summary>
        private void BtnFaculty_Click(object sender, EventArgs e)
        {
            // Khởi tạo và nạp động FacultyControl vào vùng chứa
            OpenChildControl(new FacultyControl());
        }


        private void BtnClass_Click(object sender, EventArgs e)
        {
            OpenChildControl(new ClassControl());
        }

        private void BtnStudent_Click(object sender, EventArgs e)
        {
            OpenChildControl(new StudentControl());
        }
        /// <summary>
        /// Hàm dùng chung (Helper) để tối ưu luồng nạp động UserControl vào mainPanel
        /// </summary>
        private void OpenChildControl(UserControl childControl)
        {
            // 1. Kiểm tra và giải phóng vùng nhớ của control cũ đang chạy trong mainPanel
            if (mainPanel.Controls.Count > 0)
            {
                var oldControl = mainPanel.Controls[0] as UserControl;
                if (oldControl != null)
                {
                    oldControl.Dispose(); // Hủy triệt để để tránh leak RAM
                }
            }

            // 2. Cấu hình cho Control mới tràn viền khít theo kích thước của mainPanel
            childControl.Dock = DockStyle.Fill;

            // 3. Dọn dẹp vùng chứa và đẩy control mới lên màn hình
            mainPanel.Controls.Clear();
            mainPanel.Controls.Add(childControl);
            childControl.Show();
        }
    }
}