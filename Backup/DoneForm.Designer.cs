namespace ICSNeoCSharp
{
    partial class DoneForm
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
            this.passTB = new System.Windows.Forms.TextBox();
            this.continueButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(423, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Please enter the supervisor password:";
            // 
            // passTB
            // 
            this.passTB.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passTB.Location = new System.Drawing.Point(45, 54);
            this.passTB.Name = "passTB";
            this.passTB.Size = new System.Drawing.Size(504, 35);
            this.passTB.TabIndex = 1;
            // 
            // continueButton
            // 
            this.continueButton.Location = new System.Drawing.Point(493, 115);
            this.continueButton.Name = "continueButton";
            this.continueButton.Size = new System.Drawing.Size(75, 23);
            this.continueButton.TabIndex = 0;
            this.continueButton.Text = "Continue";
            this.continueButton.Click += new System.EventHandler(this.continueButton_Click);
            // 
            // DoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 150);
            this.Controls.Add(this.continueButton);
            this.Controls.Add(this.passTB);
            this.Controls.Add(this.label1);
            this.Name = "DoneForm";
            this.Text = "Pallet Complete";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passTB;
        private System.Windows.Forms.Button continueButton;
    }
}