using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();

            this.label2.Text = "For any issues:";
            this.label1.Text = "or feel free to call (248-895-5858)";
            this.lblContactInfo.Text = "Please contact Adam Jakiela (afjakiela@gmail.com)  ";

        }
    }
}