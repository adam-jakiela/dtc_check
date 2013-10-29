namespace ICSNeoCSharp
{
    partial class IgnoreForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.availableListBox = new System.Windows.Forms.ListBox();
            this.ignoredListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.addCustomButton = new System.Windows.Forms.Button();
            this.customTextBox = new System.Windows.Forms.TextBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.ignoreAllButton = new System.Windows.Forms.Button();
            this.resetIgnoreButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Available DTCs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(371, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ignored DTCs";
            // 
            // availableListBox
            // 
            this.availableListBox.FormattingEnabled = true;
            this.availableListBox.Location = new System.Drawing.Point(15, 25);
            this.availableListBox.Name = "availableListBox";
            this.availableListBox.Size = new System.Drawing.Size(300, 277);
            this.availableListBox.TabIndex = 2;
            // 
            // ignoredListBox
            // 
            this.ignoredListBox.FormattingEnabled = true;
            this.ignoredListBox.Location = new System.Drawing.Point(374, 25);
            this.ignoredListBox.Name = "ignoredListBox";
            this.ignoredListBox.Size = new System.Drawing.Size(300, 277);
            this.ignoredListBox.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(321, 148);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(47, 23);
            this.addButton.TabIndex = 4;
            this.addButton.Text = ">>";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(321, 177);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(47, 23);
            this.removeButton.TabIndex = 5;
            this.removeButton.Text = "<<";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.addCustomButton);
            this.groupBox1.Controls.Add(this.customTextBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 317);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 48);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Custom DTC";
            // 
            // addCustomButton
            // 
            this.addCustomButton.Location = new System.Drawing.Point(182, 16);
            this.addCustomButton.Name = "addCustomButton";
            this.addCustomButton.Size = new System.Drawing.Size(160, 23);
            this.addCustomButton.TabIndex = 7;
            this.addCustomButton.Text = "Add to Ignore List";
            this.addCustomButton.UseVisualStyleBackColor = true;
            this.addCustomButton.Click += new System.EventHandler(this.addCustomButton_Click_1);
            // 
            // customTextBox
            // 
            this.customTextBox.Location = new System.Drawing.Point(16, 18);
            this.customTextBox.Name = "customTextBox";
            this.customTextBox.Size = new System.Drawing.Size(160, 20);
            this.customTextBox.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(599, 342);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(600, 313);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(75, 23);
            this.submitButton.TabIndex = 8;
            this.submitButton.Text = "Submit";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.submitButton_Click);
            // 
            // ignoreAllButton
            // 
            this.ignoreAllButton.Location = new System.Drawing.Point(518, 342);
            this.ignoreAllButton.Name = "ignoreAllButton";
            this.ignoreAllButton.Size = new System.Drawing.Size(75, 23);
            this.ignoreAllButton.TabIndex = 9;
            this.ignoreAllButton.Text = "Ignore All";
            this.ignoreAllButton.UseVisualStyleBackColor = true;
            this.ignoreAllButton.Click += new System.EventHandler(this.ignoreAllButton_Click);
            // 
            // resetIgnoreButton
            // 
            this.resetIgnoreButton.Location = new System.Drawing.Point(518, 313);
            this.resetIgnoreButton.Name = "resetIgnoreButton";
            this.resetIgnoreButton.Size = new System.Drawing.Size(75, 23);
            this.resetIgnoreButton.TabIndex = 10;
            this.resetIgnoreButton.Text = "Reset";
            this.resetIgnoreButton.UseVisualStyleBackColor = true;
            this.resetIgnoreButton.Click += new System.EventHandler(this.resetIgnoreButton_Click);
            // 
            // IgnoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 377);
            this.Controls.Add(this.resetIgnoreButton);
            this.Controls.Add(this.ignoreAllButton);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.ignoredListBox);
            this.Controls.Add(this.availableListBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "IgnoreForm";
            this.Text = "Ignore DTCs";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox availableListBox;
        private System.Windows.Forms.ListBox ignoredListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button addCustomButton;
        private System.Windows.Forms.TextBox customTextBox;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.Button ignoreAllButton;
        private System.Windows.Forms.Button resetIgnoreButton;
    }
}