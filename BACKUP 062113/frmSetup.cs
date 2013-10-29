using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO.Ports;
using System.IO;



namespace ICSNeoCSharp
{
    public delegate void MySettingsEvent(object source);

    public partial class FormSetup : Form
    {
        #region Variables
        private string didF111 = null;
        private string didF124 = null;
        private string didF125 = null;
        private string didF188 = null;
        private string app = null;
        private string pbl = null;
        private string e2p = null;
        private string cal = null;
        private string radioType = null;
        private string logFile = null;
        private string trackingNum = null;
        private string partQuantity = null;
        private bool quickClose = false;
        private string shift = null;
        private string partNum = null;

        public bool setupDone = false;

        SerialPort comPort;
        message myMessage;
		
        public event MySettingsEvent DIDF111Changed;
        public event MySettingsEvent DIDF124Changed;
        public event MySettingsEvent DIDF125Changed;
        public event MySettingsEvent DIDF188Changed; 
      //  public event MySettingsEvent trackingNumChanged;
      //  public event MySettingsEvent quantityChanged;
        public event MySettingsEvent BaudRateChanged;
        public event MySettingsEvent LogFileChanged;
        
		#endregion

        #region Constructor
        public FormSetup(SerialPort _com)
        {
            InitializeComponent();

            comPort = new SerialPort();
            myMessage = new message();

            comPort = _com;
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

            radioType = "ACM";
            //this.rdoACM.Checked = true;

            mtxtDIDF111.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF124.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF125.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF188.CharacterCasing = CharacterCasing.Upper;
            txtAPP.CharacterCasing = CharacterCasing.Upper;
            txtPBL.CharacterCasing = CharacterCasing.Upper;
            txtCAL.CharacterCasing = CharacterCasing.Upper;
            txtE2P.CharacterCasing = CharacterCasing.Upper;

            txtPassword.PasswordChar = '*';

            mtxtDIDF111.Enabled = false;
            mtxtDIDF124.Enabled = false;
            mtxtDIDF125.Enabled = false;
            mtxtDIDF188.Enabled = false;
            txtAPP.Enabled = false;
            txtPBL.Enabled = false;
            txtCAL.Enabled = false;
            txtE2P.Enabled = false;
            txtLogFile.Enabled = false;
            rdoACM.Enabled = false;
            rdoEFP.Enabled = false;
            rdoLXF.Enabled = false;
            trackingLabelTB.Enabled = false;
            quantityTB.Enabled = false;


            //DIDs FOR TESTING ONLY 
            /*
            this.mtxtDIDF111.Text = "EB5T-14F188-BA";
            this.mtxtDIDF124.Text = "EB5T-14D100-BA";
            this.mtxtDIDF125.Text = "EB5T-14D100-EB";
            this.mtxtDIDF188.Text = "EB5T-14D099-BB";

            //GEN 3.1 PART NUMBERS FOR TESTING ONLY
            this.txtAPP.Text = "470-3963-47";
            this.txtCAL.Text = "485-0491-02";
            this.txtE2P.Text = "485-0515-12";
            this.txtPBL.Text = "470-1700-11"; 
            
            

            this.lblF125.Text = "DID F110";
            this.txtLogFile.Text = "Excel.xls";
            LogFile = "Excel.xls"; 
             * 
             * 
             */

            this.partNumTB.Enabled = false;

            this.txtPassword.Focus();  

           
        }

       

        
        void comPort_DataReceived(object sender, EventArgs e)
        {

            if (comPort.IsOpen)
                myMessage.Data = comPort.ReadExisting();

            string input = myMessage.Data;

            if (myMessage.Data[0] == 'T')
                trackingLabelTB.Text = input;
            else if (myMessage.Data[0] == 'Q')
                quantityTB.Text = input;
            else if (myMessage.Data[0] == 'P')
                Invoke(new Action(() => {mtxtDIDF111 = "Hi";}));
            else if (myMessage.Data[0] == 'A')
                txtAPP.Text = input;
            else if (myMessage.Data[0] == 'C')
                txtCAL.Text = input;
            else if (myMessage.Data[0] == 'F')
                txtE2P.Text = input;
            else if (myMessage.Data[0] == 'M')
                this.mtxtDIDF111.Text = input;
            else if (myMessage.Data[0] == 'S')
                mtxtDIDF188.Text = input;
            else if (myMessage.Data[0] == 'G')
                mtxtDIDF124.Text = input;
            else if (myMessage.Data[0] == 'E')
                mtxtDIDF125.Text = input;
            else if (myMessage.Data[0] == 'B')
                txtPBL.Text = input;
            else
                MessageBox.Show("INPUT: " + input); 
              
        }


        public void setDf111(string input) { mtxtDIDF111.Text = input; } 

        #endregion

        #region Properties
        /// <summary>
        /// Accessor string for APP
        /// </summary>
        public string APP
        {
            get
            {
                return app;
            }

            set
            {
                app = value;
            }
        }
        /// <summary>
        /// Accessor string for PBL
        /// </summary>
        public string PBL
        {
            get
            {
                return pbl;
            }

            set
            {
                pbl = value;
            }
        }
        /// <summary>
        /// Accessor string for CAL
        /// </summary>
        public string CAL
        {
            get
            {
                return cal;
            }

            set
            {
                cal = value;
            }
        }
        /// <summary>
        /// Accessor string for E2P
        /// </summary>
        public string E2P
        {
            get
            {
                return e2p;
            }

            set
            {
                e2p = value;
            }
        }
        /// <summary>
        /// Accessor string for DID F111
        /// </summary>
        public string DIDF111
        {
            get
            {
                return didF111;
            }

            set
            {
                didF111 = value;
            }
        }
        /// <summary>
        /// Accessor string for DID F124
        /// </summary>
        public string DIDF124
        {
            get
            {
                return didF124;
            }

            set
            {
                didF124 = value;
            }
        }
        /// <summary>
        /// Accessor string for DID F125
        /// </summary>
        public string DIDF125
        {
            get
            {
                return didF125;
            }

            set
            {
                didF125 = value;
            }
        }
        /// <summary>
        /// Accessor string for DID F188
        /// </summary>
        public string DIDF188
        {
            get
            {
                return didF188;
            }

            set
            {
                didF188 = value;
            }
        }
        /// <summary>
        /// Accessor string for current Radio Type Selected
        /// </summary>
        public string RadioType
        {
            get
            {
                return radioType;
            }

            set
            {
                radioType = value;
            }
        }
        /// <summary>
        /// Accessor string for Log File
        /// </summary>
        public string LogFile
        {
            get
            {
                return logFile;
            }

            set
            {
                logFile = value;
            }
        }

        // Accessor string for Tracking number
        public string trackNum
        {
            get
            {
                return trackingNum;
            }

            set
            {
                trackingNum = value;
            }
        }

        // Accessor string for part number
        public string partNumber
        {
            get
            {
                return partNum;
            }

            set
            {
                partNum = value;
            }
        }


        // Accessor string for shift number
        public string Shift
        {
            get
            {
                return shift;
            }

            set
            {
                shift = value;
            }
        }

        //Accessor integer for quantity. 
        public string quantity
        {
            get
            {
                return partQuantity;
            }

            set
            {
                partQuantity = value;
            }
        }
        #endregion

        #region Private Functions
        private void frmSetup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!quickClose)
            {

                setupDone = true;
                //get values from form and set them to their appropriate labels.
                DIDF111 = mtxtDIDF111.Text;
                DIDF111Changed.Invoke(this);
                DIDF124 = mtxtDIDF124.Text;
                DIDF124Changed.Invoke(this);
                DIDF125 = mtxtDIDF125.Text;
                DIDF125Changed.Invoke(this);
                DIDF188 = mtxtDIDF188.Text;
                DIDF188Changed.Invoke(this);
                if (quantityTB.Text != "")
                {
                    partQuantity = quantityTB.Text.ToString();
                    //   quantityChanged.Invoke(this);
                }
                else
                {
                    MessageBox.Show("Quantity empty.");
                }
                
                trackingNum = trackingLabelTB.Text;
               // trackingNumChanged.Invoke(this); 

                // Only reset log file if password was entered
                if (this.txtPassword.Text == "Gustavo")
                {
                    LogFile = txtLogFile.Text;
                    LogFileChanged.Invoke(this);
                }
                APP = txtAPP.Text;
                PBL = txtPBL.Text;
                CAL = txtCAL.Text;
                E2P = txtE2P.Text;

                if (txtPassword.Text == "Gustavo")
                {
                    if (mtxtDIDF111.Text.Length != 14 && mtxtDIDF111.Text.Length != 17)
                    {
                        MessageBox.Show("Unable to close, DID F111 length is incorrect!", "Length Error - Example: xxxx-xxxxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.mtxtDIDF111.Focus();

                    }
                    if (mtxtDIDF124.Text.Length != 14 && mtxtDIDF124.Text.Length != 17)
                    {
                        MessageBox.Show("Unable to close, DID F124 length is incorrect!", "Length Error - Example: xxxx-xxxxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.mtxtDIDF124.Focus();

                    }
                    if ((this.lblF125.Text == "DID F125") & (mtxtDIDF125.Text.Length != 14 && mtxtDIDF125.Text.Length != 17))
                    {
                        MessageBox.Show("Unable to close, DID F125 length is incorrect!", "Length Error - Example: xxxx-xxxxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.mtxtDIDF125.Focus();

                    }
                    if ((this.lblF125.Text == "DID F110") & (mtxtDIDF125.Text.Length != 17 && mtxtDIDF125.Text.Length != 20))
                    {
                        MessageBox.Show("Unable to close, DID F110 length is incorrect!", "Length Error - Example: xx-xxxx-xxxxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.mtxtDIDF125.Focus();

                    }
                    if (mtxtDIDF188.Text.Length != 14 && mtxtDIDF188.Text.Length != 17)
                    {
                        MessageBox.Show("Unable to close, DID F188 length is incorrect!", "Length Error - Example: xxxx-xxxxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.mtxtDIDF188.Focus();
                    }
                    if (txtLogFile.Text == "")
                    {
                        MessageBox.Show("Unable to close, Log File needs to be defined", "Log File Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                        e.Cancel = true;
                        txtPassword.Text = "Gustavo";
                        this.txtLogFile.Focus();
                    }

                    if (RadioType == "LXF")
                    {
                        if (txtAPP.Text.Length != 2 && txtAPP.Text.Length != 11)
                        {
                            MessageBox.Show("Unable to close, APP File needs to be defined as length 11", "APP File Name Error - Example: xxx-xxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            e.Cancel = true;
                            txtPassword.Text = "Gustavo";
                            this.txtAPP.Focus();
                        }
                        if (txtPBL.Text.Length != 2 && txtPBL.Text.Length != 11)
                        {
                            MessageBox.Show("Unable to close, PBL File needs to be defined as length 11", "PBL File Name Error - Example: xxx-xxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            e.Cancel = true;
                            txtPassword.Text = "Gustavo";
                            this.txtPBL.Focus();
                        }
                        if (txtCAL.Text.Length != 2 && txtCAL.Text.Length != 11)
                        {
                            MessageBox.Show("Unable to close, CAL File needs to be defined as length 11", "CAL File Name Error - Example: xxx-xxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            e.Cancel = true;
                            txtPassword.Text = "Gustavo";
                            this.txtCAL.Focus();
                        }
                        if (txtE2P.Text.Length != 2 && txtE2P.Text.Length != 11)
                        {
                            MessageBox.Show("Unable to close, E2P File needs to be defined as length 11", "E2P File Name Error - Example: xxx-xxxx-xx", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                            e.Cancel = true;
                            txtPassword.Text = "Gustavo";
                            this.txtE2P.Focus();
                        }

                        
                    }
                }

                //check to make sure all values are entered 
                if (partNumTB.Text == null)
                    MessageBox.Show("Must input a part number.");

                if (trackingLabelTB.Text == null)
                    MessageBox.Show("Must input a tracking label.");

                if (!firstRadio.Checked && !secondRadio.Checked)
                    MessageBox.Show("Please select a shift.");

                //Make the excel file name
                // p#, date, tracking#, shiftNumber .xls 
                 

                //get date 
                DateTime myDate = DateTime.Now.Date;

                int day = myDate.Day;
                int month = myDate.Month;
                int year = myDate.Year;

                string _day, _month, _year;

                if (day < 10)
                    _day = "0" + day.ToString();
                else
                    _day = day.ToString();

                if (month < 10)
                    _month = "0" + month.ToString();
                else
                    _month = month.ToString(); 

                _year = myDate.Year.ToString().Substring(2,2);


                string date = _day + _month + _year;

                //get the shift number from the radio button
                if (firstRadio.Checked)
                    shift = firstRadio.Text;
                else if (secondRadio.Checked)
                    shift = secondRadio.Text; 

                //get tracking number 
                trackingNum = trackingLabelTB.Text.ToString(); 

                //get part number
                partNum = partNumTB.Text.ToString();

                //set the file name
                logFile = partNum + "-" + date + "-" + trackingNum + "-" + shift + ".xls"; 

                //just for testing 
                MessageBox.Show(logFile);

                

                 
                
            }
            quickClose = false;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text == "Gustavo")
            {
                rdoLXF.Select();
                mtxtDIDF111.Enabled = true;
                mtxtDIDF124.Enabled = true;
                mtxtDIDF125.Enabled = true;
                mtxtDIDF188.Enabled = true;
                quantityTB.Enabled = true;
                trackingLabelTB.Enabled = true;


                if (RadioType == "LXF")
                {
                    txtAPP.Enabled = true;
                    txtPBL.Enabled = true;
                    txtCAL.Enabled = true;
                    txtE2P.Enabled = true;
                }
                txtLogFile.Enabled = true;
                rdoLXF.Enabled = true;
                rdoACM.Enabled = true;
                rdoEFP.Enabled = true;
                partNumTB.Enabled = true;

                
                txtPassword.Enabled = false;
                this.mtxtDIDF111.Focus();
            }
            else
            {
                mtxtDIDF111.Enabled = false;
                mtxtDIDF124.Enabled = false;
                mtxtDIDF125.Enabled = false;
                mtxtDIDF188.Enabled = false;
                txtAPP.Enabled = false;
                txtPBL.Enabled = false;
                txtCAL.Enabled = false;
                txtE2P.Enabled = false;
                txtLogFile.Enabled = false;
                rdoACM.Enabled = false;
                rdoEFP.Enabled = false;
                rdoLXF.Enabled = false;
                partNumTB.Enabled = false;
                quantityTB.Enabled = false;
                txtPassword.Enabled = true;
            }
        }
        private void frmSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            txtPassword.Text = "";
            txtPassword.Focus();
        }
        private void frmSetup_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quickClose = true;
            System.Windows.Forms.Form.ActiveForm.Close();
        }
        #endregion
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application excelApp;
            Excel.Workbook excelWorkBook;
            Excel.Worksheet excelWorkSheet;

            excelApp = new Excel.Application();
            string myPath = null;
            string misValue = null;

            // If file already exists then open it
            if (File.Exists(myPath))
            {
                // Open the Excel file and get worksheet
                excelWorkBook = excelApp.Workbooks.Open(myPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                // 
            }
        }

        private void rdoACM_Click(object sender, EventArgs e)
        {
            RadioType = "ACM";
            this.lblF125.Text = "DID F110";
            BaudRateChanged.Invoke(this);

            this.txtAPP.Enabled = false;
            this.txtCAL.Enabled = false;
            this.txtE2P.Enabled = false;
            this.txtPBL.Enabled = false;
        }

        private void rdoEFP_Click(object sender, EventArgs e)
        {
            RadioType = "EFP";
            this.lblF125.Text = "DID F110";
            BaudRateChanged.Invoke(this);

            this.txtAPP.Enabled = false;
            this.txtCAL.Enabled = false;
            this.txtE2P.Enabled = false;
            this.txtPBL.Enabled = false;
        }

        private void rdoLXF_Click(object sender, EventArgs e)
        {

            RadioType = "LXF";
            this.lblF125.Text = "DID F125";
            BaudRateChanged.Invoke(this);
            if (txtPassword.Text == "Gustavo" && RadioType == "LXF")
            {
                this.txtAPP.Enabled = true;
                this.txtCAL.Enabled = true;
                this.txtE2P.Enabled = true;
                this.txtPBL.Enabled = true;
            }
            else
            {
                this.txtAPP.Enabled = false;
                this.txtCAL.Enabled = false;
                this.txtE2P.Enabled = false;
                this.txtPBL.Enabled = false;
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    } 
}