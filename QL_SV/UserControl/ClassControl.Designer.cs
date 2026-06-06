namespace QLSV.WinForms
{
    partial class ClassControl
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
            this.sfDGClasses = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.gdHeader = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            this.autoLabel1 = new Syncfusion.Windows.Forms.Tools.AutoLabel();
            this.gdMain = new Syncfusion.Windows.Forms.Tools.GradientPanel();
            ((System.ComponentModel.ISupportInitialize)(this.sfDGClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).BeginInit();
            this.gdHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdMain)).BeginInit();
            this.gdMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // sfDGClasses
            // 
            this.sfDGClasses.AccessibleName = "Table";
            this.sfDGClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sfDGClasses.Location = new System.Drawing.Point(0, 0);
            this.sfDGClasses.Name = "sfDGClasses";
            this.sfDGClasses.Size = new System.Drawing.Size(1064, 684);
            this.sfDGClasses.TabIndex = 0;
            this.sfDGClasses.Text = "sfDataGrid1";
            // 
            // gdHeader
            // 
            this.gdHeader.Controls.Add(this.autoLabel1);
            this.gdHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gdHeader.Location = new System.Drawing.Point(0, 0);
            this.gdHeader.Name = "gdHeader";
            this.gdHeader.Size = new System.Drawing.Size(1068, 37);
            this.gdHeader.TabIndex = 3;
            // 
            // autoLabel1
            // 
            this.autoLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.autoLabel1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.autoLabel1.Location = new System.Drawing.Point(363, 8);
            this.autoLabel1.Name = "autoLabel1";
            this.autoLabel1.Size = new System.Drawing.Size(294, 25);
            this.autoLabel1.TabIndex = 0;
            this.autoLabel1.Text = "THÔNG TIN QUẢN LÝ LỚP";
            // 
            // gdMain
            // 
            this.gdMain.Controls.Add(this.sfDGClasses);
            this.gdMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gdMain.Location = new System.Drawing.Point(0, 38);
            this.gdMain.Name = "gdMain";
            this.gdMain.Size = new System.Drawing.Size(1068, 688);
            this.gdMain.TabIndex = 4;
            // 
            // ClassControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gdHeader);
            this.Controls.Add(this.gdMain);
            this.Name = "ClassControl";
            this.Size = new System.Drawing.Size(1068, 726);
            ((System.ComponentModel.ISupportInitialize)(this.sfDGClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdHeader)).EndInit();
            this.gdHeader.ResumeLayout(false);
            this.gdHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdMain)).EndInit();
            this.gdMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Syncfusion.WinForms.DataGrid.SfDataGrid sfDGClasses;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gdHeader;
        private Syncfusion.Windows.Forms.Tools.AutoLabel autoLabel1;
        private Syncfusion.Windows.Forms.Tools.GradientPanel gdMain;
    }
}
