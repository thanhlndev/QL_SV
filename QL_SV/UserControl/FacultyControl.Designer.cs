namespace QLSV.WinForms
{
    partial class FacultyControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sfDGFaculties = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.gradientPanel1 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gradientPanel2 = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sfDGFaculties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).BeginInit();
            this.gradientPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).BeginInit();
            this.gradientPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfDGFaculties
            // 
            this.sfDGFaculties.AccessibleName = "Table";
            this.sfDGFaculties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfDGFaculties.Location = new System.Drawing.Point(0, 0);
            this.sfDGFaculties.Name = "sfDGFaculties";
            this.sfDGFaculties.Size = new System.Drawing.Size(1064, 685);
            this.sfDGFaculties.TabIndex = 0;
            this.sfDGFaculties.Text = "sfDataGrid1";
            // 
            // gradientPanel1
            // 
            this.gradientPanel1.Controls.Add(this.autoLabel1);
            this.gradientPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gradientPanel1.Location = new System.Drawing.Point(0, 0);
            this.gradientPanel1.Name = "gradientPanel1";
            this.gradientPanel1.Size = new System.Drawing.Size(1068, 37);
            this.gradientPanel1.TabIndex = 1;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.autoLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.autoLabel1.Location = new System.Drawing.Point(274, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(473, 25);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "THÔNG TIN QUẢN LÝ KHOA CHUYÊN MÔN";
            // 
            // gradientPanel2
            // 
            this.gradientPanel2.Controls.Add(this.sfDGFaculties);
            this.gradientPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gradientPanel2.Location = new System.Drawing.Point(0, 37);
            this.gradientPanel2.Name = "gradientPanel2";
            this.gradientPanel2.Size = new System.Drawing.Size(1068, 689);
            this.gradientPanel2.TabIndex = 2;
            // 
            // FacultyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gradientPanel2);
            this.Controls.Add(this.gradientPanel1);
            this.Name = "FacultyControl";
            this.Size = new System.Drawing.Size(1068, 726);
            ((System.ComponentModel.ISupportInitialize)(this.sfDGFaculties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel1)).EndInit();
            this.gradientPanel1.ResumeLayout(false);
            this.gradientPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gradientPanel2)).EndInit();
            this.gradientPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDGFaculties;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel1;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gradientPanel2;
    }
}
