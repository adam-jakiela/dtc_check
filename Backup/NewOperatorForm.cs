using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public delegate void OperatorEvent(object source); 


    public partial class NewOperatorForm : Form
    { 

        public event OperatorEvent OperatorNameChanged;
        public string operatorName = "";

        public NewOperatorForm()
        {
            InitializeComponent();
        } 

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (this.operatorTextBox.Text == "")
            {
                MessageBox.Show("Please enter a name.");
            }
            else
            {
                operatorName = this.operatorTextBox.Text;
                OperatorNameChanged.Invoke(this);
                Close();
            }
        }
    }
} 
