namespace QL_SV
{
    partial class FacultyForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.pText = new System.Windows.Forms.Panel();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtSdt = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtDiachi = new System.Windows.Forms.TextBox();
            this.txtTenkhoa = new System.Windows.Forms.TextBox();
            this.txtMakhoa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvFaculty = new System.Windows.Forms.DataGridView();
            this.pGrid = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGetFaculty = new System.Windows.Forms.Button();
            this.pText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).BeginInit();
            this.pGrid.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label1.Location = new System.Drawing.Point(244, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(473, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "THÔNG TIN QUẢN LÝ KHOA CHUYÊN MÔN";
            // 
            // pText
            // 
            this.pText.Controls.Add(this.btnReset);
            this.pText.Controls.Add(this.btnDel);
            this.pText.Controls.Add(this.btnEdit);
            this.pText.Controls.Add(this.btnAdd);
            this.pText.Controls.Add(this.txtSdt);
            this.pText.Controls.Add(this.txtEmail);
            this.pText.Controls.Add(this.txtDiachi);
            this.pText.Controls.Add(this.txtTenkhoa);
            this.pText.Controls.Add(this.txtMakhoa);
            this.pText.Controls.Add(this.label7);
            this.pText.Controls.Add(this.label6);
            this.pText.Controls.Add(this.label5);
            this.pText.Controls.Add(this.label4);
            this.pText.Controls.Add(this.label3);
            this.pText.Controls.Add(this.label2);
            this.pText.Location = new System.Drawing.Point(12, 3);
            this.pText.Name = "pText";
            this.pText.Size = new System.Drawing.Size(330, 401);
            this.pText.TabIndex = 1;
            this.pText.Paint += new System.Windows.Forms.PaintEventHandler(this.pText_Paint);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(250, 254);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 14;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(169, 254);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 13;
            this.btnDel.Text = "Del";
            this.btnDel.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(88, 254);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(7, 254);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // txtSdt
            // 
            this.txtSdt.Location = new System.Drawing.Point(116, 185);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.Size = new System.Drawing.Size(175, 20);
            this.txtSdt.TabIndex = 10;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(116, 149);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(175, 20);
            this.txtEmail.TabIndex = 9;
            // 
            // txtDiachi
            // 
            this.txtDiachi.Location = new System.Drawing.Point(116, 110);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.Size = new System.Drawing.Size(175, 20);
            this.txtDiachi.TabIndex = 8;
            // 
            // txtTenkhoa
            // 
            this.txtTenkhoa.Location = new System.Drawing.Point(116, 69);
            this.txtTenkhoa.Name = "txtTenkhoa";
            this.txtTenkhoa.Size = new System.Drawing.Size(175, 20);
            this.txtTenkhoa.TabIndex = 7;
            // 
            // txtMakhoa
            // 
            this.txtMakhoa.Location = new System.Drawing.Point(116, 31);
            this.txtMakhoa.Name = "txtMakhoa";
            this.txtMakhoa.Size = new System.Drawing.Size(100, 20);
            this.txtMakhoa.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 18);
            this.label7.TabIndex = 5;
            this.label7.Text = "Nhập thông tin";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(28, 186);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "SDT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(28, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Email";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(28, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Địa chỉ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(28, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên khoa";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mã khoa";
            // 
            // dgvFaculty
            // 
            this.dgvFaculty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFaculty.Location = new System.Drawing.Point(16, 0);
            this.dgvFaculty.Name = "dgvFaculty";
            this.dgvFaculty.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFaculty.Size = new System.Drawing.Size(550, 309);
            this.dgvFaculty.TabIndex = 0;
            this.dgvFaculty.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFaculty_CellClick);
            // 
            // pGrid
            // 
            this.pGrid.AutoSize = true;
            this.pGrid.Controls.Add(this.dgvFaculty);
            this.pGrid.Location = new System.Drawing.Point(348, 3);
            this.pGrid.Name = "pGrid";
            this.pGrid.Size = new System.Drawing.Size(601, 401);
            this.pGrid.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pText);
            this.panel1.Controls.Add(this.pGrid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 95);
            this.panel1.MinimumSize = new System.Drawing.Size(793, 334);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(961, 416);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGetFaculty);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(961, 89);
            this.panel2.TabIndex = 4;
            // 
            // btnGetFaculty
            // 
            this.btnGetFaculty.Location = new System.Drawing.Point(839, 12);
            this.btnGetFaculty.Name = "btnGetFaculty";
            this.btnGetFaculty.Size = new System.Drawing.Size(75, 23);
            this.btnGetFaculty.TabIndex = 15;
            this.btnGetFaculty.Text = "button1";
            this.btnGetFaculty.UseVisualStyleBackColor = true;
            // 
            // FacultyForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(961, 511);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FacultyForm";
            this.Text = "Faculty";
            this.pText.ResumeLayout(false);
            this.pText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFaculty)).EndInit();
            this.pGrid.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pText;
        private System.Windows.Forms.DataGridView dgvFaculty;
        private System.Windows.Forms.Panel pGrid;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtSdt;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtDiachi;
        private System.Windows.Forms.TextBox txtTenkhoa;
        private System.Windows.Forms.TextBox txtMakhoa;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnGetFaculty;
    }
}