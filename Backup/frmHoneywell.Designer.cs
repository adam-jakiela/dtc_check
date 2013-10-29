namespace ICSNeoCSharp
{
    partial class frmHoneywell
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
            this.api_label = new System.Windows.Forms.Label();
            this.connection_label = new System.Windows.Forms.Label();
            this.message_tb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.test_tb = new System.Windows.Forms.TextBox();
            this.connection_button = new System.Windows.Forms.Button();
            this.comms_button = new System.Windows.Forms.Button();
            this.api_button = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // api_label
            // 
            this.api_label.AutoSize = true;
            this.api_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.api_label.Location = new System.Drawing.Point(12, 9);
            this.api_label.Name = "api_label";
            this.api_label.Size = new System.Drawing.Size(70, 25);
            this.api_label.TabIndex = 0;
            this.api_label.Text = "label1";
            // 
            // connection_label
            // 
            this.connection_label.AutoSize = true;
            this.connection_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connection_label.Location = new System.Drawing.Point(161, 9);
            this.connection_label.Name = "connection_label";
            this.connection_label.Size = new System.Drawing.Size(70, 25);
            this.connection_label.TabIndex = 1;
            this.connection_label.Text = "label2";
            // 
            // message_tb
            // 
            this.message_tb.Location = new System.Drawing.Point(6, 19);
            this.message_tb.Multiline = true;
            this.message_tb.Name = "message_tb";
            this.message_tb.Size = new System.Drawing.Size(264, 293);
            this.message_tb.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.message_tb);
            this.groupBox1.Location = new System.Drawing.Point(12, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 318);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Messages:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.test_tb);
            this.groupBox2.Location = new System.Drawing.Point(294, 37);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 318);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Device";
            // 
            // test_tb
            // 
            this.test_tb.Location = new System.Drawing.Point(6, 19);
            this.test_tb.Multiline = true;
            this.test_tb.Name = "test_tb";
            this.test_tb.Size = new System.Drawing.Size(337, 293);
            this.test_tb.TabIndex = 0;
            // 
            // connection_button
            // 
            this.connection_button.Location = new System.Drawing.Point(12, 361);
            this.connection_button.Name = "connection_button";
            this.connection_button.Size = new System.Drawing.Size(98, 23);
            this.connection_button.TabIndex = 5;
            this.connection_button.Text = "Test Connection";
            this.connection_button.UseVisualStyleBackColor = true;
            this.connection_button.Click += new System.EventHandler(this.connection_button_Click);
            // 
            // comms_button
            // 
            this.comms_button.Location = new System.Drawing.Point(116, 361);
            this.comms_button.Name = "comms_button";
            this.comms_button.Size = new System.Drawing.Size(122, 23);
            this.comms_button.TabIndex = 6;
            this.comms_button.Text = "Test Communications";
            this.comms_button.UseVisualStyleBackColor = true;
            this.comms_button.Click += new System.EventHandler(this.comms_button_Click);
            // 
            // api_button
            // 
            this.api_button.Location = new System.Drawing.Point(244, 361);
            this.api_button.Name = "api_button";
            this.api_button.Size = new System.Drawing.Size(119, 23);
            this.api_button.TabIndex = 7;
            this.api_button.Text = "Attempt to open API";
            this.api_button.UseVisualStyleBackColor = true;
            this.api_button.Click += new System.EventHandler(this.api_button_Click);
            // 
            // honewell
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 392);
            this.Controls.Add(this.api_button);
            this.Controls.Add(this.comms_button);
            this.Controls.Add(this.connection_button);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.connection_label);
            this.Controls.Add(this.api_label);
            this.Name = "honewell";
            this.Text = "Honeywell Wired Scanner Testing";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label api_label;
        private System.Windows.Forms.Label connection_label;
        private System.Windows.Forms.TextBox message_tb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox test_tb;
        private System.Windows.Forms.Button connection_button;
        private System.Windows.Forms.Button comms_button;
        private System.Windows.Forms.Button api_button;
    }
}