namespace ICSNeoCSharp
{
    partial class HardwareForm
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
            this.hardwareComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.channelListBox = new System.Windows.Forms.ListBox();
            this.connectButton = new System.Windows.Forms.Button();
            this.comPortButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.restartButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hardwareComboBox
            // 
            this.hardwareComboBox.FormattingEnabled = true;
            this.hardwareComboBox.Location = new System.Drawing.Point(193, 16);
            this.hardwareComboBox.Name = "hardwareComboBox";
            this.hardwareComboBox.Size = new System.Drawing.Size(241, 21);
            this.hardwareComboBox.TabIndex = 0;
            this.hardwareComboBox.SelectedIndexChanged += new System.EventHandler(this.hardwareComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select Hardware";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.channelListBox);
            this.groupBox1.Location = new System.Drawing.Point(12, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 159);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Available Channels";
            // 
            // channelListBox
            // 
            this.channelListBox.FormattingEnabled = true;
            this.channelListBox.Location = new System.Drawing.Point(15, 19);
            this.channelListBox.Name = "channelListBox";
            this.channelListBox.Size = new System.Drawing.Size(327, 121);
            this.channelListBox.TabIndex = 0;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(387, 215);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(137, 23);
            this.connectButton.TabIndex = 3;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // comPortButton
            // 
            this.comPortButton.Location = new System.Drawing.Point(387, 157);
            this.comPortButton.Name = "comPortButton";
            this.comPortButton.Size = new System.Drawing.Size(137, 23);
            this.comPortButton.TabIndex = 4;
            this.comPortButton.Text = "COM Port Config";
            this.comPortButton.UseVisualStyleBackColor = true;
            this.comPortButton.Click += new System.EventHandler(this.comPortButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(387, 186);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(137, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // restartButton
            // 
            this.restartButton.Location = new System.Drawing.Point(387, 128);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(137, 23);
            this.restartButton.TabIndex = 6;
            this.restartButton.Text = "Restart";
            this.restartButton.UseVisualStyleBackColor = true;
            this.restartButton.Click += new System.EventHandler(this.restartButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(356, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "NOTE: A application restart is recommended when configuring COM ports.";
            // 
            // HardwareForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(536, 249);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.restartButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.comPortButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hardwareComboBox);
            this.Name = "HardwareForm";
            this.Text = "Hardware Configuration";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox hardwareComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox channelListBox;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button comPortButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button restartButton;
        private System.Windows.Forms.Label label2;
    }
}