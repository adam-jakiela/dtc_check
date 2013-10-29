using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public partial class DeleteForm : Form
    {

        string appLoc = AppDomain.CurrentDomain.BaseDirectory;
        string fileName = "saved_settings.txt";
        
        public DeleteForm()
        {
            InitializeComponent();
            this.passTB.PasswordChar = '*';
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (passTB.Text == "Gustavo")
            {
                try
                {
                    System.IO.StreamWriter file = new System.IO.StreamWriter(appLoc + fileName);
                }
                catch
                {
                    MessageBox.Show("Could not open file.");
                }

                Close();
                

            }
            else
            {
                MessageBox.Show("Incorrect password.");
            }

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}