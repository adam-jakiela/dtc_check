using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public partial class frmHoneywell : Form
    {
        public bool api = false;
        public bool connection = false;
        public bool has_comms = false;


        public frmHoneywell()
        {
            InitializeComponent();

            checkForErrors();
            updateLabels();
        }

        public void setApiBool(bool _api)
        {
            api = _api; 
        }

        public void setConnectionBool(bool _connection)
        {
            connection = _connection;
        }

        public void setComms(bool _comms)
        {
            has_comms = _comms;
        }

        public bool getApiState() { return api; }
        public bool getConnectionState() { return connection; }
        public bool getComms() { return has_comms; }

        public void checkForErrors()
        {
            if (!api)
            {
                this.message_tb.Text = message_tb.Text + "API cannot open. Is scanner SDK installed and up to date?\n";
            }

            if (!connection)
            {
                this.message_tb.Text = message_tb.Text + "No connection with device. Hit 'connect device' button.\n";
            }

            if (!has_comms)
            {

            }

            

        }

        public void updateLabels()
        {
            if (api)
            {
                this.api_label.Text = "API: OPEN";
                this.api_label.ForeColor = Color.Green; 
            }
            else
            {
                this.api_label.Text = "API: CLOSED";
                this.api_label.ForeColor = Color.Red; 
            }

            if (connection)
            {
                this.connection_label.Text = "CONNECTION: PASS";
                this.connection_label.ForeColor = Color.Green; 
            }
            else
            {
                this.connection_label.Text = "CONNECTION: FAIL";
                this.connection_label.ForeColor = Color.Red; 

            }
        }

        private void connection_button_Click(object sender, EventArgs e)
        {

        }

        private void comms_button_Click(object sender, EventArgs e)
        {

        }

        private void api_button_Click(object sender, EventArgs e)
        {

        }

       
    }
}