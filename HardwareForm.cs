using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public delegate void MyHardwareEvent(object source);


    public partial class HardwareForm : Form
    {

        public event MyHardwareEvent HardwareChanged;
        public event MyHardwareEvent OnHardwareClose;
        public event MyHardwareEvent OnValueCanConnect;
        public event MyHardwareEvent OnCanCaseConnect;

        public bool valueCan = false;
        public bool canCase = false;
        
        public HardwareForm()
        {
            InitializeComponent();



            hardwareComboBox.Items.Add("ValueCAN3");
            hardwareComboBox.Items.Add("CANcaseXL"); 


        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            OnHardwareClose.Invoke(this);
            Close();
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            if (valueCan)
            {
                //connect to value can

                OnValueCanConnect.Invoke(this);
            }
            else if (canCase)
            {
                if (this.channelListBox.SelectedIndex == 0)
                {
                    //connect with Channel 1

                    OnCanCaseConnect.Invoke(this);
                    Close();
                }
                else if (this.channelListBox.SelectedIndex == 1)
                {
                    //connect with channel 2

                    OnCanCaseConnect.Invoke(this);
                    Close();
                }
                else
                {
                    MessageBox.Show("Please select a channel.");
                }
            }
            else
            {
                MessageBox.Show("Please select a hardware."); 
            }
        }

        private void comPortButton_Click(object sender, EventArgs e)
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = "devmanager.bat";
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
        }

        private void restartButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void hardwareComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (hardwareComboBox.SelectedIndex == 0)
            {
                valueCan = true;
                canCase = false;
                this.channelListBox.Items.Clear();
            }
            else if (hardwareComboBox.SelectedIndex == 1) ;
            {
                valueCan = false;
                canCase = true;
                this.channelListBox.Items.Clear();
                this.channelListBox.Items.Add("Channel 1");
                this.channelListBox.Items.Add("Channel 2"); 

            }
        }
    }
}
