namespace ICSNeoCSharp
{
    partial class PassedForm
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
            this.passedDevicesTB = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // passedDevicesTB
            // 
            this.passedDevicesTB.Location = new System.Drawing.Point(12, 12);
            this.passedDevicesTB.Multiline = true;
            this.passedDevicesTB.Name = "passedDevicesTB";
            this.passedDevicesTB.Size = new System.Drawing.Size(645, 478);
            this.passedDevicesTB.TabIndex = 0;
            // 
            // PassedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(669, 502);
            this.Controls.Add(this.passedDevicesTB);
            this.Name = "PassedForm";
            this.Text = "Passed Devices";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox passedDevicesTB;

    }
}