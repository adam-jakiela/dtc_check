using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace ICSNeoCSharp {

    public delegate void MyPasswordEvent(object source);

    public partial class PasswordForm : Form 
    {
        public event MySettingsEvent PasswordChanged;

        private const int CP_NOCLOSE_BUTTON = 0x200;


        String password = "Gustavo";

        SerialPort comPort;
        message myMessage;

        public PasswordForm(SerialPort _com)
        {
            InitializeComponent();
            this.txt_pass.PasswordChar = '*';
            comPort = new SerialPort();
            myMessage = new message();

            comPort = _com;
           comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

            this.continue_button.Focus();

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

        
        void comPort_DataReceived(object sender, EventArgs e)
        {
            if(comPort.IsOpen)
                myMessage.Data = comPort.ReadExisting();

            Console.WriteLine("Scanned.");
            if (myMessage.Data[0] == 'X')
            {
                if (this.txt_pass.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        if (myMessage.Data.StartsWith("X"))
                        {
                            txt_pass.Text = this.myMessage.Data.Remove(0, 2);

                            if (txt_pass.Text == password)
                            {
                                continue_button.Enabled = true;
                            }
                        }
                    });
                }
                else
                {
                    if (myMessage.Data.StartsWith("X"))
                    {
                        txt_pass.Text = this.myMessage.Data.Remove(0, 2);
                   
                        if (txt_pass.Text == password)
                        {
                            continue_button.Enabled = true;
                        }
                    }
                }
            }
        } 
          
         

        private void continue_button_Click(object sender, EventArgs e)
        {
            if (txt_pass.Text == password)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Incorrect Password.");
                this.txt_pass.Text = "";
            }
        }  
    }
}