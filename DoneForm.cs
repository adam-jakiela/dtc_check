using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public partial class DoneForm : Form
    {

        public bool passwordEntered = false;
        private const int CP_NOCLOSE_BUTTON = 0x200;

        public DoneForm()
        {
            InitializeComponent();
            this.passTB.PasswordChar = '*';
          
            this.passTB.Focus(); 

        }

        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        
       

        private void DoneForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            passwordEntered = true; 
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (passTB.Text == "Gustavo")
                Close();
            else
                MessageBox.Show("Incorrect password.");
        }
    }
}