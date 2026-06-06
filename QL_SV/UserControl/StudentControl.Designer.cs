namespace QLSV.WinForms
{
    partial class StudentControl
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
            this.sfDGStudents = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.gpHeader = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gdMain = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sfDGStudents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpHeader)).BeginInit();
            this.gpHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdMain)).BeginInit();
            this.gdMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfDGStudents
            // 
            this.sfDGStudents.AccessibleName = "Table";
            this.sfDGStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfDGStudents.Location = new System.Drawing.Point(0, 0);
            this.sfDGStudents.Name = "sfDGStudents";
            this.sfDGStudents.Size = new System.Drawing.Size(1064, 684);
            this.sfDGStudents.TabIndex = 0;
            this.sfDGStudents.Text = "sfDataGrid1";
            // 
            // gpHeader
            // 
            this.gpHeader.Controls.Add(this.autoLabel1);
            this.gpHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpHeader.Location = new System.Drawing.Point(0, 0);
            this.gpHeader.Name = "gpHeader";
            this.gpHeader.Size = new System.Drawing.Size(1068, 37);
            this.gpHeader.TabIndex = 5;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.autoLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.autoLabel1.Location = new System.Drawing.Point(341, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(361, 25);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "THÔNG TIN QUẢN LÝ SINH VIÊN";
            // 
            // gdMain
            // 
            this.gdMain.Controls.Add(this.sfDGStudents);
            this.gdMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gdMain.Location = new System.Drawing.Point(0, 38);
            this.gdMain.Name = "gdMain";
            this.gdMain.Size = new System.Drawing.Size(1068, 688);
            this.gdMain.TabIndex = 6;
            // 
            // StudentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gpHeader);
            this.Controls.Add(this.gdMain);
            this.Name = "StudentControl";
            this.Size = new System.Drawing.Size(1068, 726);
            ((System.ComponentModel.ISupportInitialize)(this.sfDGStudents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gpHeader)).EndInit();
            this.gpHeader.ResumeLayout(false);
            this.gpHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdMain)).EndInit();
            this.gdMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDGStudents;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gpHeader;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gdMain;
    }
}
