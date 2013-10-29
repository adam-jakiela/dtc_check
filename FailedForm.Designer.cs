namespace ICSNeoCSharp
{
    partial class FailedForm
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
            this.failedDataGridView = new System.Windows.Forms.DataGridView();
            this.serialColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.partColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.failedDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // failedDataGridView
            // 
            this.failedDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.failedDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.serialColumn,
            this.partColumn,
            this.timeColumn});
            this.failedDataGridView.Location = new System.Drawing.Point(12, 12);
            this.failedDataGridView.Name = "failedDataGridView";
            this.failedDataGridView.Size = new System.Drawing.Size(343, 481);
            this.failedDataGridView.TabIndex = 0;
            // 
            // serialColumn
            // 
            this.serialColumn.HeaderText = "Serial #";
            this.serialColumn.Name = "serialColumn";
            // 
            // partColumn
            // 
            this.partColumn.HeaderText = "Part #";
            this.partColumn.Name = "partColumn";
            // 
            // timeColumn
            // 
            this.timeColumn.HeaderText = "Time Scanned";
            this.timeColumn.Name = "timeColumn";
            // 
            // FailedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 505);
            this.Controls.Add(this.failedDataGridView);
            this.Name = "FailedForm";
            this.Text = "Failed Devices";
            ((System.ComponentModel.ISupportInitialize)(this.failedDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView failedDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn serialColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn partColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeColumn;

    }
}