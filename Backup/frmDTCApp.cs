using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.IO;
using ICSNeoCSharp;
using System.Xml;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.VisualBasic; 



namespace ICSNeoCSharp
{
    public class FormDTCApplication : System.Windows.Forms.Form
    {
        #region Variables

        public int requestCounter = 0; 

        internal System.Windows.Forms.GroupBox grpConnections;
        internal System.Windows.Forms.GroupBox device_groupbox;
        private System.ComponentModel.IContainer components = null;
        int m_hObject = 0;   //Handle of Device
        bool m_bPortOpen = false;
        private System.Windows.Forms.GroupBox grpResult;
        private MenuStrip mnuFile;
        private ToolStripMenuItem tsmSetup;  //Port open status
        private ICSNeoCSharp.IcsSpyMessage[] stMessages = new IcsSpyMessage[20000];
        private ToolStripMenuItem exitToolStripMenuItem;   //TempSpace for messages
        // A class for setting the record properties set by user

        // A class for showing the about form
        FormAbout myAbout = new FormAbout();
        private string[] gmyDTC = new string[35];
        byte[] myDTCs = new byte[200];
        string[] dtcArray = new String[50];
        spyFilterLong stFilter;
        IcsSpyMessage stMsg;
        public int myTimer = 0;
        private ToolStripMenuItem setupToolStripMenuItem;
        private bool ignoreValueCANDialog = false;
        private bool humanInput = true;

        stateMachine myStateMachine = new stateMachine();
        SerialPort comPort = new SerialPort("COM26", 38400, Parity.None, 8, StopBits.One);
        FormSetup myForm;
        string[] portNames = new string[25];
        private System.Windows.Forms.Label lblScanner;
        private System.Windows.Forms.Label lblValueCAN;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.Label lblHoneywellResult;
        private System.Windows.Forms.Label lblValueCANResult;
        private System.Windows.Forms.Label lblReady;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.GroupBox grpDIDValidation;
        private System.Windows.Forms.Label lblF188;
        private System.Windows.Forms.Label lblDIDF113;
        private System.Windows.Forms.Label lblDIDF124;
        private System.Windows.Forms.Label lblF125;
        private System.Windows.Forms.Label lblDIDF111;
        private System.Windows.Forms.TextBox txtDIDF111Expected;
        private System.Windows.Forms.Label lblExpected;
        private System.Windows.Forms.TextBox txtDIDF188Expected;
        private System.Windows.Forms.TextBox txtDIDF125Expected;
        private System.Windows.Forms.TextBox txtDIDF124Expected;
        private System.Windows.Forms.TextBox txtDIDF113Expected;
        private System.Windows.Forms.Label lblReceived;
        private System.Windows.Forms.TextBox txtDIDF188Received;
        private System.Windows.Forms.TextBox txtDIDF125Received;
        private System.Windows.Forms.TextBox txtDIDF124Received;
        private System.Windows.Forms.TextBox txtDIDF113Received;
        private System.Windows.Forms.TextBox txtDIDF111Received;

        message myMessage = new message();
        long lNetworkID = 1;
        private System.Windows.Forms.Label lblResult4;
        private System.Windows.Forms.Label lblResult3;
        private Button btnLogFile;

        private string myDID = null;
        Excel.Application myExcelApp = null;
        Excel.Workbook myExcelWorkBook = null;
        Excel.Worksheet myExcelWorkSheet = null;
        private bool logOpen = false;
        private TextBox txtPBL;
        private TextBox txtAPP;
        private TextBox txtE2P;
        private TextBox txtCAL;
        private GroupBox grpDTCs;
        private int myCounter = 1;
        private ListBox lstDTCs;
        //   private System.Windows.Forms.Timer flowControlTimer;
        private string oldResult = null;
        private int myBitRate = 0;
        private bool handled = false;

        private bool noSerial = false;

        //hold the devices
        //used to detect duplicate values
        List<device> deviceList = new List<device>();
        List<device> duplicateDevices = new List<device>();

        List<device> failedDevices = new List<device>();
        List<device> passedDevices = new List<device>();
        List<string> ignoredDTCs = new List<string>();
        List<string> DTCs = new List<string>();

        List<string> serialNumbers = new List<string>(); 
       // PassedForm myPassedForm = new PassedForm();
        frmHoneywell honeyForm = new frmHoneywell();
        DoneForm doneForm = new DoneForm();
        FileDataForm fdf = new FileDataForm();
        NewOperatorForm nof = new NewOperatorForm();

        PasswordForm passwordForm; 

        //FormDuplicate myDuplicateForm = new FormDuplicate();

        private int duplicateCounter = 0;

        private bool loadCompleate = false;

        private bool honeyWellDialogIgnored = false;

        private bool spaceInput = false;

        private IgnoreForm ignoreForm = new IgnoreForm();

        //Variables for wireless scanner
        private GroupBox groupBox1;
        private Label deviceResultLabel;
        private Label dtcResultLabel;

        public bool inSetup = false;
        //   private System.Windows.Forms.Timer timer2;
        public bool transferInProgress = false;


        public bool flashDTC = false;
        public bool flashResult = false;
        private System.Windows.Forms.Timer checkConnectionTimer;

        //booleans for connection state
        public bool deviceConnected = false;
        public bool isConnectionRequest = false;
        //   private System.Windows.Forms.Timer timer5;
        public bool deviceScanned = false;
        public int countDown = 6;
        public bool[] connectionArray = new bool[3];
        public bool testResponse = false;

        public int labelCounter = 0;
        private System.Windows.Forms.Timer flowControlTimer;
        private System.Windows.Forms.Timer flashLabelTimer;
        //  private System.Windows.Forms.Timer checkConnectionTimer;
        public bool deviceFailed = false;

        public bool isWriting = false;
        private TextBox txtPartNumber;
        private Label lblPartNumber;
        private ProgressBar progressBar1;
        private Label radio_connection_label;
        private Label label3;
        public bool isReading = false;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label palletStatusLabel;
        private Label duplicateQuantityLabel;
        private Label failedQuantityLabel;
        private Label passedQuantityLabel;
        private ProgressBar progressBar2;
        private Label deviceScanningStatusLabel;
        
        public int passedQuantity = 0;
        public int palletQuantity = 0;
        public int failedQuantity = 0;
        public int dupQuantity = 0;

        public bool updatingGUI = false;
        public bool inScan = false;

        //public variables to write to file
        public double precentage = 0;
        public string exlFileName = "";
        private TextBox txtE2PExpected;
        private TextBox txtPBLExpected;
        private TextBox txtAPLExpected;
        private Label lblAPP;
        private TextBox txtCALExpected;
        private Label lblPBL;
        private Label lblCAL;
        private Label lblE2P;

        private bool firstWrite = true;
        private Button button1;
        private bool scan = true;
        private GroupBox groupBox4;
        private bool afterScan = false;

        List<string> data = new List<string>(); 
        bool dataLoaded = false;
        private ToolStripMenuItem deleteDataToolStripMenuItem;



        DeleteForm delForm = new DeleteForm();

        private bool stopTimers = false;
        public bool setupOverride = false;
        private bool waitingForSerial = false;

        private DataSerializer ds;
        private ToolStripMenuItem showFileDataToolStripMenuItem;
        public string dataFileName = "DTCSaveState.osl";


        private bool checkHoneywell = true;
        private ToolStripMenuItem changeOperatorToolStripMenuItem;
        private bool setupIsOpen = false;
        private ToolStripMenuItem ignoredDTCsToolStripMenuItem;
        private ToolStripMenuItem saveInformationToolStripMenuItem;
        private ToolStripMenuItem openLogFileToolStripMenuItem;
        private ToolStripMenuItem dataFileToolStripMenuItem;
        private ToolStripMenuItem ignoreListToolStripMenuItem;
        private ToolStripMenuItem addDTCToolStripMenuItem;
        private ToolStripMenuItem logFileToolStripMenuItem;
        private ToolStripMenuItem dataFileToolStripMenuItem1;
        private ToolStripMenuItem ignoreListToolStripMenuItem1;
        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem ignoreListToolStripMenuItem2;
        private ToolStripMenuItem dTCDataToolStripMenuItem;
        private ToolStripMenuItem allToolStripMenuItem;


        private bool duplicateDevice = false;


        #endregion

        public delegate void MyApplicationEvent(object source);


        #region Constructor/Destructor
        public FormDTCApplication()
        {
      
            InitializeComponent();
            updateReadyLabel("PLEASE COMPLETE SETUP");
            this.radio_connection_label.ForeColor = Color.Black;
            this.radio_connection_label.Text = "NO";

            
            //hides part number input
         //   this.txtDIDF113Expected.Visible = false;
            this.lblPartNumber.Visible = false;
            this.txtPartNumber.Visible = false;

            myForm = new FormSetup(comPort);
            myForm.OnSetupClose += new MySettingsEvent(OnSetupClose);
            myForm.OnSetupOpen += new MySettingsEvent(OnSetupOpen);


            ignoreForm.OnIgnoreClose += new MyIgnoreEvent(this.OnIgnoreClose);
            // we will always use lxf
            myForm.RadioType = "LXF";
         //   myForm.rdoLXF.Checked = true;

            //// Setup event handlers for Settings change
            myForm.DIDF111Changed += new MySettingsEvent(OnDIDF111Change);
            myForm.DIDF124Changed += new MySettingsEvent(OnDIDF124Change);
            myForm.DIDF125Changed += new MySettingsEvent(OnDIDF125Change);
            myForm.DIDF188Changed += new MySettingsEvent(OnDIDF188Change);
            myForm.DIDF113Changed += new MySettingsEvent(OnDIDF113Change);
            myForm.CALChanged += new MySettingsEvent(OnCALChange);
            myForm.APLChanged += new MySettingsEvent(OnAPLChange);
            myForm.E2PChanged += new MySettingsEvent(OnE2PChange);
            myForm.PBLChanged += new MySettingsEvent(OnPBLChange);
            myForm.BaudRateChanged += new MySettingsEvent(OnBaudRateChange);
            myForm.LogFileChanged += new MySettingsEvent(OnLogFileChange);
            myForm.SBLChanged += new MySettingsEvent(OnSBLChange);
            myForm.PBLChanged += new MySettingsEvent(OnPBLChange);
            myForm.DIDF110Changed += new MySettingsEvent(OnDIDF110Change);
            myForm.PackageIDChanged += new MySettingsEvent(this.OnPackageIDChange);
            myForm.QuantityChanged += new MySettingsEvent(OnQuantityChange);
            myForm.BenchChanged += new MySettingsEvent(OnBenchChange);
            nof.OperatorNameChanged += new OperatorEvent(OnNewOperator);
            

            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

           myDID = "F125";
            lblF125.Text = "DID F125";

            this.KeyUp += new KeyEventHandler(OnKeyPress);
           
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpConnections = new System.Windows.Forms.GroupBox();
            this.radio_connection_label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblHoneywellResult = new System.Windows.Forms.Label();
            this.lblValueCANResult = new System.Windows.Forms.Label();
            this.lblScanner = new System.Windows.Forms.Label();
            this.lblValueCAN = new System.Windows.Forms.Label();
            this.device_groupbox = new System.Windows.Forms.GroupBox();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.deviceResultLabel = new System.Windows.Forms.Label();
            this.lblResult4 = new System.Windows.Forms.Label();
            this.lblResult3 = new System.Windows.Forms.Label();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.mnuFile = new System.Windows.Forms.MenuStrip();
            this.tsmSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.saveInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLogFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showFileDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataFileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreListToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.dTCDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoredDTCsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ignoreListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addDTCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeOperatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblReady = new System.Windows.Forms.Label();
            this.grpDIDValidation = new System.Windows.Forms.GroupBox();
            this.txtE2PExpected = new System.Windows.Forms.TextBox();
            this.txtCAL = new System.Windows.Forms.TextBox();
            this.txtAPP = new System.Windows.Forms.TextBox();
            this.lblReceived = new System.Windows.Forms.Label();
            this.txtE2P = new System.Windows.Forms.TextBox();
            this.txtPBLExpected = new System.Windows.Forms.TextBox();
            this.txtPBL = new System.Windows.Forms.TextBox();
            this.txtDIDF188Received = new System.Windows.Forms.TextBox();
            this.txtAPLExpected = new System.Windows.Forms.TextBox();
            this.txtDIDF125Received = new System.Windows.Forms.TextBox();
            this.lblAPP = new System.Windows.Forms.Label();
            this.txtDIDF124Received = new System.Windows.Forms.TextBox();
            this.txtCALExpected = new System.Windows.Forms.TextBox();
            this.txtDIDF113Received = new System.Windows.Forms.TextBox();
            this.lblPBL = new System.Windows.Forms.Label();
            this.txtDIDF111Received = new System.Windows.Forms.TextBox();
            this.lblExpected = new System.Windows.Forms.Label();
            this.lblCAL = new System.Windows.Forms.Label();
            this.txtDIDF188Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF125Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF124Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF113Expected = new System.Windows.Forms.TextBox();
            this.lblE2P = new System.Windows.Forms.Label();
            this.txtDIDF111Expected = new System.Windows.Forms.TextBox();
            this.lblF188 = new System.Windows.Forms.Label();
            this.lblDIDF113 = new System.Windows.Forms.Label();
            this.lblDIDF124 = new System.Windows.Forms.Label();
            this.lblF125 = new System.Windows.Forms.Label();
            this.lblDIDF111 = new System.Windows.Forms.Label();
            this.grpDTCs = new System.Windows.Forms.GroupBox();
            this.lstDTCs = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtcResultLabel = new System.Windows.Forms.Label();
            this.flowControlTimer = new System.Windows.Forms.Timer(this.components);
            this.flashLabelTimer = new System.Windows.Forms.Timer(this.components);
            this.checkConnectionTimer = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.deviceScanningStatusLabel = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.duplicateQuantityLabel = new System.Windows.Forms.Label();
            this.failedQuantityLabel = new System.Windows.Forms.Label();
            this.passedQuantityLabel = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.palletStatusLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.grpConnections.SuspendLayout();
            this.device_groupbox.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.mnuFile.SuspendLayout();
            this.grpDIDValidation.SuspendLayout();
            this.grpDTCs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnections
            // 
            this.grpConnections.Controls.Add(this.radio_connection_label);
            this.grpConnections.Controls.Add(this.label3);
            this.grpConnections.Controls.Add(this.lblHoneywellResult);
            this.grpConnections.Controls.Add(this.lblValueCANResult);
            this.grpConnections.Controls.Add(this.lblScanner);
            this.grpConnections.Controls.Add(this.lblValueCAN);
            this.grpConnections.Location = new System.Drawing.Point(13, 28);
            this.grpConnections.Name = "grpConnections";
            this.grpConnections.Size = new System.Drawing.Size(393, 116);
            this.grpConnections.TabIndex = 49;
            this.grpConnections.TabStop = false;
            this.grpConnections.Text = "Connections";
            // 
            // radio_connection_label
            // 
            this.radio_connection_label.AutoSize = true;
            this.radio_connection_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radio_connection_label.ForeColor = System.Drawing.Color.Green;
            this.radio_connection_label.Location = new System.Drawing.Point(283, 79);
            this.radio_connection_label.Name = "radio_connection_label";
            this.radio_connection_label.Size = new System.Drawing.Size(80, 29);
            this.radio_connection_label.TabIndex = 7;
            this.radio_connection_label.Text = "PASS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(215, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "Radio Connected";
            // 
            // lblHoneywellResult
            // 
            this.lblHoneywellResult.AutoSize = true;
            this.lblHoneywellResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoneywellResult.ForeColor = System.Drawing.Color.Green;
            this.lblHoneywellResult.Location = new System.Drawing.Point(283, 50);
            this.lblHoneywellResult.Name = "lblHoneywellResult";
            this.lblHoneywellResult.Size = new System.Drawing.Size(80, 29);
            this.lblHoneywellResult.TabIndex = 5;
            this.lblHoneywellResult.Text = "PASS";
            // 
            // lblValueCANResult
            // 
            this.lblValueCANResult.AutoSize = true;
            this.lblValueCANResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCANResult.Location = new System.Drawing.Point(283, 21);
            this.lblValueCANResult.Name = "lblValueCANResult";
            this.lblValueCANResult.Size = new System.Drawing.Size(80, 29);
            this.lblValueCANResult.TabIndex = 4;
            this.lblValueCANResult.Text = "PASS";
            // 
            // lblScanner
            // 
            this.lblScanner.AutoSize = true;
            this.lblScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanner.Location = new System.Drawing.Point(17, 50);
            this.lblScanner.Name = "lblScanner";
            this.lblScanner.Size = new System.Drawing.Size(109, 29);
            this.lblScanner.TabIndex = 1;
            this.lblScanner.Text = "Scanner";
            // 
            // lblValueCAN
            // 
            this.lblValueCAN.AutoSize = true;
            this.lblValueCAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCAN.Location = new System.Drawing.Point(17, 21);
            this.lblValueCAN.Name = "lblValueCAN";
            this.lblValueCAN.Size = new System.Drawing.Size(132, 29);
            this.lblValueCAN.TabIndex = 0;
            this.lblValueCAN.Text = "ValueCAN";
            this.lblValueCAN.Click += new System.EventHandler(this.lblValueCAN_Click);
            // 
            // device_groupbox
            // 
            this.device_groupbox.Controls.Add(this.txtPartNumber);
            this.device_groupbox.Controls.Add(this.txtSerialNumber);
            this.device_groupbox.Controls.Add(this.lblPartNumber);
            this.device_groupbox.Controls.Add(this.lblSerialNumber);
            this.device_groupbox.Location = new System.Drawing.Point(12, 150);
            this.device_groupbox.Name = "device_groupbox";
            this.device_groupbox.Size = new System.Drawing.Size(623, 80);
            this.device_groupbox.TabIndex = 47;
            this.device_groupbox.TabStop = false;
            this.device_groupbox.Text = "Scanned Values";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartNumber.ForeColor = System.Drawing.Color.Green;
            this.txtPartNumber.Location = new System.Drawing.Point(279, 86);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.ReadOnly = true;
            this.txtPartNumber.Size = new System.Drawing.Size(548, 44);
            this.txtPartNumber.TabIndex = 7;
            this.txtPartNumber.TabStop = false;
            this.txtPartNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.ForeColor = System.Drawing.Color.Green;
            this.txtSerialNumber.Location = new System.Drawing.Point(229, 18);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.ReadOnly = true;
            this.txtSerialNumber.Size = new System.Drawing.Size(374, 44);
            this.txtSerialNumber.TabIndex = 6;
            this.txtSerialNumber.TabStop = false;
            this.txtSerialNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblPartNumber
            // 
            this.lblPartNumber.AutoSize = true;
            this.lblPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartNumber.Location = new System.Drawing.Point(7, 83);
            this.lblPartNumber.Name = "lblPartNumber";
            this.lblPartNumber.Size = new System.Drawing.Size(239, 42);
            this.lblPartNumber.TabIndex = 2;
            this.lblPartNumber.Text = "Part Number";
            // 
            // lblSerialNumber
            // 
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(8, 24);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(215, 33);
            this.lblSerialNumber.TabIndex = 1;
            this.lblSerialNumber.Text = "Serial Number";
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.deviceResultLabel);
            this.grpResult.Controls.Add(this.lblResult4);
            this.grpResult.Controls.Add(this.lblResult3);
            this.grpResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpResult.Location = new System.Drawing.Point(950, 233);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(293, 187);
            this.grpResult.TabIndex = 51;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Device Result";
            // 
            // deviceResultLabel
            // 
            this.deviceResultLabel.AutoSize = true;
            this.deviceResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceResultLabel.Location = new System.Drawing.Point(37, 68);
            this.deviceResultLabel.Name = "deviceResultLabel";
            this.deviceResultLabel.Size = new System.Drawing.Size(0, 73);
            this.deviceResultLabel.TabIndex = 54;
            // 
            // lblResult4
            // 
            this.lblResult4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult4.AutoSize = true;
            this.lblResult4.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult4.Location = new System.Drawing.Point(31, 183);
            this.lblResult4.Name = "lblResult4";
            this.lblResult4.Size = new System.Drawing.Size(0, 58);
            this.lblResult4.TabIndex = 53;
            this.lblResult4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult4.UseCompatibleTextRendering = true;
            // 
            // lblResult3
            // 
            this.lblResult3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult3.AutoSize = true;
            this.lblResult3.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult3.Location = new System.Drawing.Point(31, 135);
            this.lblResult3.Name = "lblResult3";
            this.lblResult3.Size = new System.Drawing.Size(0, 58);
            this.lblResult3.TabIndex = 52;
            this.lblResult3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult3.UseCompatibleTextRendering = true;
            // 
            // btnLogFile
            // 
            this.btnLogFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogFile.Location = new System.Drawing.Point(13, 625);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(317, 80);
            this.btnLogFile.TabIndex = 54;
            this.btnLogFile.Text = "LOG FILE";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click_1);
            // 
            // mnuFile
            // 
            this.mnuFile.BackColor = System.Drawing.SystemColors.Info;
            this.mnuFile.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSetup,
            this.setupToolStripMenuItem,
            this.ignoredDTCsToolStripMenuItem,
            this.changeOperatorToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mnuFile.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mnuFile.Location = new System.Drawing.Point(0, 0);
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(1259, 24);
            this.mnuFile.TabIndex = 52;
            this.mnuFile.Text = "File";
            // 
            // tsmSetup
            // 
            this.tsmSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveInformationToolStripMenuItem,
            this.openLogFileToolStripMenuItem,
            this.showFileDataToolStripMenuItem,
            this.deleteDataToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.tsmSetup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmSetup.Name = "tsmSetup";
            this.tsmSetup.Size = new System.Drawing.Size(40, 20);
            this.tsmSetup.Text = "File";
            // 
            // saveInformationToolStripMenuItem
            // 
            this.saveInformationToolStripMenuItem.Name = "saveInformationToolStripMenuItem";
            this.saveInformationToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveInformationToolStripMenuItem.Text = "Save";
            // 
            // openLogFileToolStripMenuItem
            // 
            this.openLogFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataFileToolStripMenuItem});
            this.openLogFileToolStripMenuItem.Name = "openLogFileToolStripMenuItem";
            this.openLogFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openLogFileToolStripMenuItem.Text = "Open";
            // 
            // dataFileToolStripMenuItem
            // 
            this.dataFileToolStripMenuItem.Name = "dataFileToolStripMenuItem";
            this.dataFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.dataFileToolStripMenuItem.Text = "Data File";
            this.dataFileToolStripMenuItem.Click += new System.EventHandler(this.dataFileToolStripMenuItem_Click);
            // 
            // showFileDataToolStripMenuItem
            // 
            this.showFileDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logFileToolStripMenuItem,
            this.dataFileToolStripMenuItem1,
            this.ignoreListToolStripMenuItem1});
            this.showFileDataToolStripMenuItem.Name = "showFileDataToolStripMenuItem";
            this.showFileDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showFileDataToolStripMenuItem.Text = "Show";
            this.showFileDataToolStripMenuItem.Click += new System.EventHandler(this.showFileDataToolStripMenuItem_Click);
            // 
            // logFileToolStripMenuItem
            // 
            this.logFileToolStripMenuItem.Name = "logFileToolStripMenuItem";
            this.logFileToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.logFileToolStripMenuItem.Text = "Log File";
            // 
            // dataFileToolStripMenuItem1
            // 
            this.dataFileToolStripMenuItem1.Name = "dataFileToolStripMenuItem1";
            this.dataFileToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.dataFileToolStripMenuItem1.Text = "Data File";
            // 
            // ignoreListToolStripMenuItem1
            // 
            this.ignoreListToolStripMenuItem1.Name = "ignoreListToolStripMenuItem1";
            this.ignoreListToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.ignoreListToolStripMenuItem1.Text = "Ignore List";
            // 
            // deleteDataToolStripMenuItem
            // 
            this.deleteDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dataToolStripMenuItem,
            this.ignoreListToolStripMenuItem2,
            this.dTCDataToolStripMenuItem,
            this.allToolStripMenuItem});
            this.deleteDataToolStripMenuItem.Name = "deleteDataToolStripMenuItem";
            this.deleteDataToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteDataToolStripMenuItem.Text = "Reset";
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // ignoreListToolStripMenuItem2
            // 
            this.ignoreListToolStripMenuItem2.Name = "ignoreListToolStripMenuItem2";
            this.ignoreListToolStripMenuItem2.Size = new System.Drawing.Size(136, 22);
            this.ignoreListToolStripMenuItem2.Text = "Ignore List";
            // 
            // dTCDataToolStripMenuItem
            // 
            this.dTCDataToolStripMenuItem.Name = "dTCDataToolStripMenuItem";
            this.dTCDataToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.dTCDataToolStripMenuItem.Text = "DTC Data";
            // 
            // allToolStripMenuItem
            // 
            this.allToolStripMenuItem.Name = "allToolStripMenuItem";
            this.allToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.allToolStripMenuItem.Text = "All";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
            // 
            // ignoredDTCsToolStripMenuItem
            // 
            this.ignoredDTCsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ignoreListToolStripMenuItem,
            this.addDTCToolStripMenuItem});
            this.ignoredDTCsToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F);
            this.ignoredDTCsToolStripMenuItem.Name = "ignoredDTCsToolStripMenuItem";
            this.ignoredDTCsToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.ignoredDTCsToolStripMenuItem.Text = "Ignored DTCs";
            this.ignoredDTCsToolStripMenuItem.Click += new System.EventHandler(this.ignoredDTCsToolStripMenuItem_Click);
            // 
            // ignoreListToolStripMenuItem
            // 
            this.ignoreListToolStripMenuItem.Name = "ignoreListToolStripMenuItem";
            this.ignoreListToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.ignoreListToolStripMenuItem.Text = "Ignore List";
            // 
            // addDTCToolStripMenuItem
            // 
            this.addDTCToolStripMenuItem.Name = "addDTCToolStripMenuItem";
            this.addDTCToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.addDTCToolStripMenuItem.Text = "Add DTC";
            // 
            // changeOperatorToolStripMenuItem
            // 
            this.changeOperatorToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeOperatorToolStripMenuItem.Name = "changeOperatorToolStripMenuItem";
            this.changeOperatorToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.changeOperatorToolStripMenuItem.Text = "Change Operator";
            this.changeOperatorToolStripMenuItem.Click += new System.EventHandler(this.changeOperatorToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblReady
            // 
            this.lblReady.AutoSize = true;
            this.lblReady.Font = new System.Drawing.Font("Arial", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReady.Location = new System.Drawing.Point(6, 27);
            this.lblReady.Name = "lblReady";
            this.lblReady.Size = new System.Drawing.Size(0, 35);
            this.lblReady.TabIndex = 55;
            this.lblReady.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpDIDValidation
            // 
            this.grpDIDValidation.Controls.Add(this.txtE2PExpected);
            this.grpDIDValidation.Controls.Add(this.txtCAL);
            this.grpDIDValidation.Controls.Add(this.txtAPP);
            this.grpDIDValidation.Controls.Add(this.lblReceived);
            this.grpDIDValidation.Controls.Add(this.txtE2P);
            this.grpDIDValidation.Controls.Add(this.txtPBLExpected);
            this.grpDIDValidation.Controls.Add(this.txtPBL);
            this.grpDIDValidation.Controls.Add(this.txtDIDF188Received);
            this.grpDIDValidation.Controls.Add(this.txtAPLExpected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF125Received);
            this.grpDIDValidation.Controls.Add(this.lblAPP);
            this.grpDIDValidation.Controls.Add(this.txtDIDF124Received);
            this.grpDIDValidation.Controls.Add(this.txtCALExpected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF113Received);
            this.grpDIDValidation.Controls.Add(this.lblPBL);
            this.grpDIDValidation.Controls.Add(this.txtDIDF111Received);
            this.grpDIDValidation.Controls.Add(this.lblExpected);
            this.grpDIDValidation.Controls.Add(this.lblCAL);
            this.grpDIDValidation.Controls.Add(this.txtDIDF188Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF125Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF124Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF113Expected);
            this.grpDIDValidation.Controls.Add(this.lblE2P);
            this.grpDIDValidation.Controls.Add(this.txtDIDF111Expected);
            this.grpDIDValidation.Controls.Add(this.lblF188);
            this.grpDIDValidation.Controls.Add(this.lblDIDF113);
            this.grpDIDValidation.Controls.Add(this.lblDIDF124);
            this.grpDIDValidation.Controls.Add(this.lblF125);
            this.grpDIDValidation.Controls.Add(this.lblDIDF111);
            this.grpDIDValidation.Location = new System.Drawing.Point(13, 236);
            this.grpDIDValidation.Name = "grpDIDValidation";
            this.grpDIDValidation.Size = new System.Drawing.Size(622, 383);
            this.grpDIDValidation.TabIndex = 56;
            this.grpDIDValidation.TabStop = false;
            this.grpDIDValidation.Text = "Validation";
            // 
            // txtE2PExpected
            // 
            this.txtE2PExpected.BackColor = System.Drawing.SystemColors.Control;
            this.txtE2PExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtE2PExpected.ForeColor = System.Drawing.Color.Green;
            this.txtE2PExpected.Location = new System.Drawing.Point(116, 341);
            this.txtE2PExpected.Name = "txtE2PExpected";
            this.txtE2PExpected.ReadOnly = true;
            this.txtE2PExpected.Size = new System.Drawing.Size(230, 29);
            this.txtE2PExpected.TabIndex = 71;
            this.txtE2PExpected.TabStop = false;
            this.txtE2PExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCAL
            // 
            this.txtCAL.BackColor = System.Drawing.SystemColors.Control;
            this.txtCAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCAL.ForeColor = System.Drawing.Color.Green;
            this.txtCAL.Location = new System.Drawing.Point(360, 270);
            this.txtCAL.Name = "txtCAL";
            this.txtCAL.ReadOnly = true;
            this.txtCAL.Size = new System.Drawing.Size(230, 29);
            this.txtCAL.TabIndex = 7;
            this.txtCAL.TabStop = false;
            this.txtCAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAPP
            // 
            this.txtAPP.BackColor = System.Drawing.SystemColors.Control;
            this.txtAPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPP.ForeColor = System.Drawing.Color.Green;
            this.txtAPP.Location = new System.Drawing.Point(360, 235);
            this.txtAPP.Name = "txtAPP";
            this.txtAPP.ReadOnly = true;
            this.txtAPP.Size = new System.Drawing.Size(230, 29);
            this.txtAPP.TabIndex = 9;
            this.txtAPP.TabStop = false;
            this.txtAPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceived.Location = new System.Drawing.Point(427, 15);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(90, 24);
            this.lblReceived.TabIndex = 67;
            this.lblReceived.Text = "Received";
            // 
            // txtE2P
            // 
            this.txtE2P.BackColor = System.Drawing.SystemColors.Control;
            this.txtE2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtE2P.ForeColor = System.Drawing.Color.Green;
            this.txtE2P.Location = new System.Drawing.Point(360, 341);
            this.txtE2P.Name = "txtE2P";
            this.txtE2P.ReadOnly = true;
            this.txtE2P.Size = new System.Drawing.Size(230, 29);
            this.txtE2P.TabIndex = 8;
            this.txtE2P.TabStop = false;
            this.txtE2P.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPBLExpected
            // 
            this.txtPBLExpected.BackColor = System.Drawing.SystemColors.Control;
            this.txtPBLExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBLExpected.ForeColor = System.Drawing.Color.Green;
            this.txtPBLExpected.Location = new System.Drawing.Point(116, 305);
            this.txtPBLExpected.Name = "txtPBLExpected";
            this.txtPBLExpected.ReadOnly = true;
            this.txtPBLExpected.Size = new System.Drawing.Size(230, 29);
            this.txtPBLExpected.TabIndex = 73;
            this.txtPBLExpected.TabStop = false;
            this.txtPBLExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPBL
            // 
            this.txtPBL.BackColor = System.Drawing.SystemColors.Control;
            this.txtPBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBL.ForeColor = System.Drawing.Color.Green;
            this.txtPBL.Location = new System.Drawing.Point(360, 305);
            this.txtPBL.Name = "txtPBL";
            this.txtPBL.ReadOnly = true;
            this.txtPBL.Size = new System.Drawing.Size(230, 29);
            this.txtPBL.TabIndex = 10;
            this.txtPBL.TabStop = false;
            this.txtPBL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF188Received
            // 
            this.txtDIDF188Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF188Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF188Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF188Received.Location = new System.Drawing.Point(360, 200);
            this.txtDIDF188Received.Name = "txtDIDF188Received";
            this.txtDIDF188Received.ReadOnly = true;
            this.txtDIDF188Received.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF188Received.TabIndex = 66;
            this.txtDIDF188Received.TabStop = false;
            this.txtDIDF188Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAPLExpected
            // 
            this.txtAPLExpected.BackColor = System.Drawing.SystemColors.Control;
            this.txtAPLExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPLExpected.ForeColor = System.Drawing.Color.Green;
            this.txtAPLExpected.Location = new System.Drawing.Point(117, 235);
            this.txtAPLExpected.Name = "txtAPLExpected";
            this.txtAPLExpected.ReadOnly = true;
            this.txtAPLExpected.Size = new System.Drawing.Size(229, 29);
            this.txtAPLExpected.TabIndex = 72;
            this.txtAPLExpected.TabStop = false;
            this.txtAPLExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF125Received
            // 
            this.txtDIDF125Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF125Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF125Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF125Received.Location = new System.Drawing.Point(360, 161);
            this.txtDIDF125Received.Name = "txtDIDF125Received";
            this.txtDIDF125Received.ReadOnly = true;
            this.txtDIDF125Received.Size = new System.Drawing.Size(231, 29);
            this.txtDIDF125Received.TabIndex = 65;
            this.txtDIDF125Received.TabStop = false;
            this.txtDIDF125Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAPP
            // 
            this.lblAPP.AutoSize = true;
            this.lblAPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAPP.Location = new System.Drawing.Point(42, 238);
            this.lblAPP.Name = "lblAPP";
            this.lblAPP.Size = new System.Drawing.Size(45, 24);
            this.lblAPP.TabIndex = 11;
            this.lblAPP.Text = "APL";
            // 
            // txtDIDF124Received
            // 
            this.txtDIDF124Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF124Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF124Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF124Received.Location = new System.Drawing.Point(360, 123);
            this.txtDIDF124Received.Name = "txtDIDF124Received";
            this.txtDIDF124Received.ReadOnly = true;
            this.txtDIDF124Received.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF124Received.TabIndex = 64;
            this.txtDIDF124Received.TabStop = false;
            this.txtDIDF124Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtCALExpected
            // 
            this.txtCALExpected.BackColor = System.Drawing.SystemColors.Control;
            this.txtCALExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCALExpected.ForeColor = System.Drawing.Color.Green;
            this.txtCALExpected.Location = new System.Drawing.Point(116, 270);
            this.txtCALExpected.Name = "txtCALExpected";
            this.txtCALExpected.ReadOnly = true;
            this.txtCALExpected.Size = new System.Drawing.Size(230, 29);
            this.txtCALExpected.TabIndex = 70;
            this.txtCALExpected.TabStop = false;
            this.txtCALExpected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF113Received
            // 
            this.txtDIDF113Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF113Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF113Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF113Received.Location = new System.Drawing.Point(360, 84);
            this.txtDIDF113Received.Name = "txtDIDF113Received";
            this.txtDIDF113Received.ReadOnly = true;
            this.txtDIDF113Received.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF113Received.TabIndex = 63;
            this.txtDIDF113Received.TabStop = false;
            this.txtDIDF113Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPBL
            // 
            this.lblPBL.AutoSize = true;
            this.lblPBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPBL.Location = new System.Drawing.Point(42, 308);
            this.lblPBL.Name = "lblPBL";
            this.lblPBL.Size = new System.Drawing.Size(44, 24);
            this.lblPBL.TabIndex = 12;
            this.lblPBL.Text = "PBL";
            // 
            // txtDIDF111Received
            // 
            this.txtDIDF111Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF111Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF111Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF111Received.Location = new System.Drawing.Point(360, 49);
            this.txtDIDF111Received.Name = "txtDIDF111Received";
            this.txtDIDF111Received.ReadOnly = true;
            this.txtDIDF111Received.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF111Received.TabIndex = 62;
            this.txtDIDF111Received.TabStop = false;
            this.txtDIDF111Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblExpected
            // 
            this.lblExpected.AutoSize = true;
            this.lblExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpected.Location = new System.Drawing.Point(181, 15);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(91, 24);
            this.lblExpected.TabIndex = 61;
            this.lblExpected.Text = "Expected";
            // 
            // lblCAL
            // 
            this.lblCAL.AutoSize = true;
            this.lblCAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCAL.Location = new System.Drawing.Point(39, 273);
            this.lblCAL.Name = "lblCAL";
            this.lblCAL.Size = new System.Drawing.Size(46, 24);
            this.lblCAL.TabIndex = 14;
            this.lblCAL.Text = "CAL";
            // 
            // txtDIDF188Expected
            // 
            this.txtDIDF188Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF188Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF188Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF188Expected.Location = new System.Drawing.Point(117, 200);
            this.txtDIDF188Expected.Name = "txtDIDF188Expected";
            this.txtDIDF188Expected.ReadOnly = true;
            this.txtDIDF188Expected.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF188Expected.TabIndex = 60;
            this.txtDIDF188Expected.TabStop = false;
            this.txtDIDF188Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF125Expected
            // 
            this.txtDIDF125Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF125Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF125Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF125Expected.Location = new System.Drawing.Point(117, 161);
            this.txtDIDF125Expected.Name = "txtDIDF125Expected";
            this.txtDIDF125Expected.ReadOnly = true;
            this.txtDIDF125Expected.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF125Expected.TabIndex = 59;
            this.txtDIDF125Expected.TabStop = false;
            this.txtDIDF125Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF124Expected
            // 
            this.txtDIDF124Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF124Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF124Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF124Expected.Location = new System.Drawing.Point(116, 123);
            this.txtDIDF124Expected.Name = "txtDIDF124Expected";
            this.txtDIDF124Expected.ReadOnly = true;
            this.txtDIDF124Expected.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF124Expected.TabIndex = 58;
            this.txtDIDF124Expected.TabStop = false;
            this.txtDIDF124Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF113Expected
            // 
            this.txtDIDF113Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF113Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF113Expected.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtDIDF113Expected.Location = new System.Drawing.Point(116, 84);
            this.txtDIDF113Expected.Name = "txtDIDF113Expected";
            this.txtDIDF113Expected.ReadOnly = true;
            this.txtDIDF113Expected.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF113Expected.TabIndex = 57;
            this.txtDIDF113Expected.TabStop = false;
            this.txtDIDF113Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblE2P
            // 
            this.lblE2P.AutoSize = true;
            this.lblE2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblE2P.Location = new System.Drawing.Point(41, 344);
            this.lblE2P.Name = "lblE2P";
            this.lblE2P.Size = new System.Drawing.Size(45, 24);
            this.lblE2P.TabIndex = 13;
            this.lblE2P.Text = "E2P";
            // 
            // txtDIDF111Expected
            // 
            this.txtDIDF111Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF111Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF111Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF111Expected.Location = new System.Drawing.Point(116, 49);
            this.txtDIDF111Expected.Name = "txtDIDF111Expected";
            this.txtDIDF111Expected.ReadOnly = true;
            this.txtDIDF111Expected.Size = new System.Drawing.Size(230, 29);
            this.txtDIDF111Expected.TabIndex = 56;
            this.txtDIDF111Expected.TabStop = false;
            this.txtDIDF111Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblF188
            // 
            this.lblF188.AutoSize = true;
            this.lblF188.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF188.Location = new System.Drawing.Point(24, 196);
            this.lblF188.Name = "lblF188";
            this.lblF188.Size = new System.Drawing.Size(87, 24);
            this.lblF188.TabIndex = 55;
            this.lblF188.Text = "DID F188";
            // 
            // lblDIDF113
            // 
            this.lblDIDF113.AutoSize = true;
            this.lblDIDF113.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF113.Location = new System.Drawing.Point(24, 85);
            this.lblDIDF113.Name = "lblDIDF113";
            this.lblDIDF113.Size = new System.Drawing.Size(87, 24);
            this.lblDIDF113.TabIndex = 54;
            this.lblDIDF113.Text = "DID F113";
            // 
            // lblDIDF124
            // 
            this.lblDIDF124.AutoSize = true;
            this.lblDIDF124.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF124.Location = new System.Drawing.Point(24, 124);
            this.lblDIDF124.Name = "lblDIDF124";
            this.lblDIDF124.Size = new System.Drawing.Size(87, 24);
            this.lblDIDF124.TabIndex = 53;
            this.lblDIDF124.Text = "DID F124";
            // 
            // lblF125
            // 
            this.lblF125.AutoSize = true;
            this.lblF125.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF125.Location = new System.Drawing.Point(24, 160);
            this.lblF125.Name = "lblF125";
            this.lblF125.Size = new System.Drawing.Size(87, 24);
            this.lblF125.TabIndex = 52;
            this.lblF125.Text = "DID F125";
            // 
            // lblDIDF111
            // 
            this.lblDIDF111.AutoSize = true;
            this.lblDIDF111.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF111.Location = new System.Drawing.Point(24, 46);
            this.lblDIDF111.Name = "lblDIDF111";
            this.lblDIDF111.Size = new System.Drawing.Size(87, 24);
            this.lblDIDF111.TabIndex = 51;
            this.lblDIDF111.Text = "DID F111";
            // 
            // grpDTCs
            // 
            this.grpDTCs.Controls.Add(this.lstDTCs);
            this.grpDTCs.Location = new System.Drawing.Point(644, 426);
            this.grpDTCs.Name = "grpDTCs";
            this.grpDTCs.Size = new System.Drawing.Size(599, 279);
            this.grpDTCs.TabIndex = 58;
            this.grpDTCs.TabStop = false;
            this.grpDTCs.Text = "DTCs";
            // 
            // lstDTCs
            // 
            this.lstDTCs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDTCs.FormattingEnabled = true;
            this.lstDTCs.ItemHeight = 24;
            this.lstDTCs.Location = new System.Drawing.Point(11, 19);
            this.lstDTCs.Name = "lstDTCs";
            this.lstDTCs.Size = new System.Drawing.Size(571, 244);
            this.lstDTCs.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtcResultLabel);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(644, 233);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 187);
            this.groupBox1.TabIndex = 65;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DTC Result";
            // 
            // dtcResultLabel
            // 
            this.dtcResultLabel.AutoSize = true;
            this.dtcResultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtcResultLabel.Location = new System.Drawing.Point(47, 68);
            this.dtcResultLabel.Name = "dtcResultLabel";
            this.dtcResultLabel.Size = new System.Drawing.Size(0, 73);
            this.dtcResultLabel.TabIndex = 0;
            // 
            // flowControlTimer
            // 
            this.flowControlTimer.Enabled = true;
            this.flowControlTimer.Interval = 30;
            this.flowControlTimer.Tick += new System.EventHandler(this.flowControlTimer_Tick);
            // 
            // flashLabelTimer
            // 
            this.flashLabelTimer.Enabled = true;
            this.flashLabelTimer.Interval = 1000;
            this.flashLabelTimer.Tick += new System.EventHandler(this.flashLabelTimer_Tick);
            // 
            // checkConnectionTimer
            // 
            this.checkConnectionTimer.Enabled = true;
            this.checkConnectionTimer.Interval = 1000;
            this.checkConnectionTimer.Tick += new System.EventHandler(this.checkConnectionTimer_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.progressBar1.Location = new System.Drawing.Point(14, 77);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(437, 23);
            this.progressBar1.TabIndex = 67;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.deviceScanningStatusLabel);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Location = new System.Drawing.Point(412, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(463, 116);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Radio Scan Status";
            // 
            // deviceScanningStatusLabel
            // 
            this.deviceScanningStatusLabel.AutoSize = true;
            this.deviceScanningStatusLabel.Cursor = System.Windows.Forms.Cursors.No;
            this.deviceScanningStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deviceScanningStatusLabel.Location = new System.Drawing.Point(10, 26);
            this.deviceScanningStatusLabel.Name = "deviceScanningStatusLabel";
            this.deviceScanningStatusLabel.Size = new System.Drawing.Size(144, 24);
            this.deviceScanningStatusLabel.TabIndex = 68;
            this.deviceScanningStatusLabel.Text = "Connect Device";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.duplicateQuantityLabel);
            this.groupBox3.Controls.Add(this.failedQuantityLabel);
            this.groupBox3.Controls.Add(this.passedQuantityLabel);
            this.groupBox3.Controls.Add(this.progressBar2);
            this.groupBox3.Controls.Add(this.palletStatusLabel);
            this.groupBox3.Location = new System.Drawing.Point(881, 28);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(362, 116);
            this.groupBox3.TabIndex = 69;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pallet Status";
            // 
            // duplicateQuantityLabel
            // 
            this.duplicateQuantityLabel.AutoSize = true;
            this.duplicateQuantityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.duplicateQuantityLabel.Location = new System.Drawing.Point(274, 21);
            this.duplicateQuantityLabel.Name = "duplicateQuantityLabel";
            this.duplicateQuantityLabel.Size = new System.Drawing.Size(71, 20);
            this.duplicateQuantityLabel.TabIndex = 4;
            this.duplicateQuantityLabel.Text = "DUP.: 0";
            // 
            // failedQuantityLabel
            // 
            this.failedQuantityLabel.AutoSize = true;
            this.failedQuantityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.failedQuantityLabel.Location = new System.Drawing.Point(141, 21);
            this.failedQuantityLabel.Name = "failedQuantityLabel";
            this.failedQuantityLabel.Size = new System.Drawing.Size(68, 20);
            this.failedQuantityLabel.TabIndex = 3;
            this.failedQuantityLabel.Text = "FAIL: 0";
            // 
            // passedQuantityLabel
            // 
            this.passedQuantityLabel.AutoSize = true;
            this.passedQuantityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passedQuantityLabel.Location = new System.Drawing.Point(6, 21);
            this.passedQuantityLabel.Name = "passedQuantityLabel";
            this.passedQuantityLabel.Size = new System.Drawing.Size(76, 20);
            this.passedQuantityLabel.TabIndex = 2;
            this.passedQuantityLabel.Text = "PASS: 0";
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(11, 77);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(335, 23);
            this.progressBar2.TabIndex = 1;
            // 
            // palletStatusLabel
            // 
            this.palletStatusLabel.AutoSize = true;
            this.palletStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.palletStatusLabel.Location = new System.Drawing.Point(6, 50);
            this.palletStatusLabel.Name = "palletStatusLabel";
            this.palletStatusLabel.Size = new System.Drawing.Size(186, 25);
            this.palletStatusLabel.TabIndex = 0;
            this.palletStatusLabel.Text = "Pallet Completion:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(336, 625);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(296, 80);
            this.button1.TabIndex = 71;
            this.button1.Text = "RETEST UNIT";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblReady);
            this.groupBox4.Location = new System.Drawing.Point(647, 150);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(596, 80);
            this.groupBox4.TabIndex = 72;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "STATUS";
            // 
            // FormDTCApplication
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1259, 715);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpDTCs);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLogFile);
            this.Controls.Add(this.grpDIDValidation);
            this.Controls.Add(this.grpConnections);
            this.Controls.Add(this.device_groupbox);
            this.Controls.Add(this.mnuFile);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuFile;
            this.Name = "FormDTCApplication";
            this.Text = "DTC Checker V1.4 x86";
            this.Load += new System.EventHandler(this.frmDTCApp_Load);
            this.grpConnections.ResumeLayout(false);
            this.grpConnections.PerformLayout();
            this.device_groupbox.ResumeLayout(false);
            this.device_groupbox.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.mnuFile.ResumeLayout(false);
            this.mnuFile.PerformLayout();
            this.grpDIDValidation.ResumeLayout(false);
            this.grpDIDValidation.PerformLayout();
            this.grpDTCs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region Start Thread
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new FormDTCApplication());


            //For connection 




        }
        #endregion

        #region Private Functions
        static private string convertToHex(string sInput)
        {
            string sOut;
            uint uiDecimal = 0;

            try
            {
                //Convert text string to unsigned integer
                uiDecimal = checked((uint)System.Convert.ToUInt32(sInput));
            }
            catch (System.OverflowException)
            {
                sOut = "Overflow";
                return sOut;
            }
            //Format unsigned integer value to hex 
            sOut = String.Format("{0:x2}", uiDecimal);
            return sOut.ToUpper();
        }
        static private int convertFromHex(string num)
        {
            //To hold our converted unsigned integer32 value
            uint uiHex = 0;
            try
            {
                // Convert hex string to unsigned integer
                uiHex = System.Convert.ToUInt32(num, 16);
            }
            catch (System.OverflowException)
            {
                //
            }
            return Convert.ToInt32(uiHex);
        }
        /// <summary>
        /// Message structure for ValueCAN message
        /// </summary>
        /// <returns></returns>
        static private IcsSpyMessage CreateEmptyStructure()
        {
            IcsSpyMessage InputMessage;
            InputMessage.StatusBitField = 0;
            InputMessage.StatusBitField2 = 0;
            InputMessage.TimeHardware = 0;
            InputMessage.TimeHardware2 = 0;
            InputMessage.TimeSystem = 0;
            InputMessage.TimeSystem2 = 0;
            InputMessage.TimeStampHardwareID = 0;
            InputMessage.TimeStampSystemID = 0;
            InputMessage.NetworkID = 0;
            InputMessage.NodeID = 0;
            InputMessage.Protocol = 0;
            InputMessage.MessagePieceID = 0;
            InputMessage.ColorID = 0;
            InputMessage.NumberBytesHeader = 0;
            InputMessage.NumberBytesData = 0;
            InputMessage.DescriptionID = 0;
            InputMessage.ArbIDOrHeader = 0;
            InputMessage.Data1 = 0;
            InputMessage.Data2 = 0;
            InputMessage.Data3 = 0;
            InputMessage.Data4 = 0;
            InputMessage.Data5 = 0;
            InputMessage.Data6 = 0;
            InputMessage.Data7 = 0;
            InputMessage.Data8 = 0;
            InputMessage.AckBytes1 = 0;
            InputMessage.AckBytes2 = 0;
            InputMessage.AckBytes3 = 0;
            InputMessage.AckBytes4 = 0;
            InputMessage.AckBytes5 = 0;
            InputMessage.AckBytes6 = 0;
            InputMessage.AckBytes7 = 0;
            InputMessage.AckBytes8 = 0;
            InputMessage.Value = 0;
            InputMessage.MiscData = 0;
            return InputMessage;
        }
        static private icsSpyMessageJ1850 CreateEmptyStructureJ1850()
        {
            icsSpyMessageJ1850 InputMessage;
            InputMessage.StatusBitField = 0;
            InputMessage.StatusBitField2 = 0;
            InputMessage.TimeHardware = 0;
            InputMessage.TimeHardware2 = 0;
            InputMessage.TimeSystem = 0;
            InputMessage.TimeSystem2 = 0;
            InputMessage.TimeStampHardwareID = 0;
            InputMessage.TimeStampSystemID = 0;
            InputMessage.NetworkID = 0;
            InputMessage.NodeID = 0;
            InputMessage.Protocol = 0;
            InputMessage.MessagePieceID = 0;
            InputMessage.ColorID = 0;
            InputMessage.NumberBytesHeader = 0;
            InputMessage.NumberBytesData = 0;
            InputMessage.DescriptionID = 0;
            InputMessage.Header1 = 0;  //Holds (up to 3 byte 1850 header or 29 bit CAN header)
            InputMessage.Header2 = 0;
            InputMessage.Header3 = 0;
            InputMessage.Header4 = 0;
            InputMessage.Data1 = 0;
            InputMessage.Data2 = 0;
            InputMessage.Data3 = 0;
            InputMessage.Data4 = 0;
            InputMessage.Data5 = 0;
            InputMessage.Data6 = 0;
            InputMessage.Data7 = 0;
            InputMessage.Data8 = 0;
            InputMessage.AckBytes1 = 0;
            InputMessage.AckBytes2 = 0;
            InputMessage.AckBytes3 = 0;
            InputMessage.AckBytes4 = 0;
            InputMessage.AckBytes5 = 0;
            InputMessage.AckBytes6 = 0;
            InputMessage.AckBytes7 = 0;
            InputMessage.AckBytes8 = 0;
            InputMessage.Value = 0;
            InputMessage.MiscData = 0;
            return InputMessage;
        }
        /// <summary>
        /// Requests DIDs from device. Must call extractDID() after to retrive the DID information.
        /// </summary>
        /// <param name="DID"></param>
        private void requestDIDs(string DID)
        {
            //  updateReadyLabel("Request DID " + DID);
            long lResult;
            IcsSpyMessage stMessagesTx;

            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;
            if (myForm.RadioType == "ACM")
                stMessagesTx.ArbIDOrHeader = 0x727;
            else if (myForm.RadioType == "EFP")
                stMessagesTx.ArbIDOrHeader = 0x7A7;
            else if (myForm.RadioType == "LXF")
            {
                //      Console.WriteLine("DID Request LXF for " + DID);
                stMessagesTx.ArbIDOrHeader = 0x727;
            }

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request DTCs...
            stMessagesTx.Data1 = 0x03;
            stMessagesTx.Data2 = 0x22;
            stMessagesTx.Data3 = Convert.ToByte(DID.Substring(0, 2), 16);
            stMessagesTx.Data4 = Convert.ToByte(DID.Substring(2, 2), 16);
            myMessage.DIDRequest = Convert.ToByte(DID.Substring(2, 2), 16);
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to read the DID
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                //     Console.WriteLine("problem with " + DID);
                //   lblValueCANResult.Text = "PROBLEM";
                //MessageBox.Show("Problem Transmitting Message");
            }
        }

        /// <summary>
        /// Reads the part numbers from the device. Must call readPartNumber() after.
        /// </summary>
        /// <param name="Number"></param>
        private void requestPartNumber(int Number)
        {
            long lResult;
            IcsSpyMessage stMessagesTx;

            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

       

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;
            if (myForm.RadioType == "ACM")
                stMessagesTx.ArbIDOrHeader = 0x727;
            else if (myForm.RadioType == "EFP")
                stMessagesTx.ArbIDOrHeader = 0x7A7;
            else if (myForm.RadioType == "LXF")
                stMessagesTx.ArbIDOrHeader = 0x727;

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request DTCs...
            stMessagesTx.Data1 = 0x03;
            stMessagesTx.Data2 = 0xBA;
            stMessagesTx.Data3 = 0x11;
            stMessagesTx.Data4 = Convert.ToByte(Number);
            myMessage.DIDRequest = Convert.ToByte(Number);
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to read the DID
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                lblValueCANResult.Text = "PROBLEM";
                //MessageBox.Show("Problem Transmitting Message");
            }
        }
        private void changeSession(int Number)
        {
            long lResult;
            IcsSpyMessage stMessagesTx;

            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;
            if (myForm.RadioType == "ACM")
                stMessagesTx.ArbIDOrHeader = 0x727;
            else if (myForm.RadioType == "EFP")
                stMessagesTx.ArbIDOrHeader = 0x7A7;
            else if (myForm.RadioType == "LXF")
                stMessagesTx.ArbIDOrHeader = 0x727;

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request DTCs...
            stMessagesTx.Data1 = 0x02;
            stMessagesTx.Data2 = 0x10;
            stMessagesTx.Data3 = 0x03;
            stMessagesTx.Data4 = 0;
            myMessage.DIDRequest = 0;
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to read the DID
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                lblValueCANResult.Text = "PROBLEM";
                //MessageBox.Show("Problem Transmitting Message");
            }
        }

        /// <summary>
        /// Gets the DTCs from the device
        /// </summary>
        private void requestDTCs()
        {
            long lResult;
            IcsSpyMessage stMessagesTx;
            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;

            stMessagesTx.ArbIDOrHeader = 0x727;

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request DTCs...
            stMessagesTx.Data1 = 0x03;
            stMessagesTx.Data2 = 0x19;
            stMessagesTx.Data3 = 0x02;
            stMessagesTx.Data4 = 0x8F;
            myMessage.DIDRequest = 0x19;
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to read the DID
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                lblValueCANResult.Text = "PROBLEM";
                // MessageBox.Show("Problem Transmitting Message");
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {

         Console.WriteLine(this.precentage);

         if (ds.PASSCOUNTER != 0 && (ds.PASSCOUNTER != ds.QUANTITY))
         {
             DialogResult dialogResult = MessageBox.Show(
                 "Pallet is not complete. Are you sure you want to close?",
                 "Pallet Incomplete",
                 MessageBoxButtons.YesNo);

             if (dialogResult == DialogResult.Yes)
             {
                 //write data to file 

                 Console.WriteLine("Writing data to file.");
                 ds.readFile = true;

                 Console.WriteLine("APP: " + ds.APLEXPECTED);
                 Console.WriteLine("CAL: " + ds.CALEXPECTED);
                 Console.WriteLine("PBL: " + ds.PBLEXPECTED);
                 Console.WriteLine("E2P: " + ds.E2PEXPECTED);
                 Console.WriteLine("SBL: " + ds.SBL);
                 Console.WriteLine("F110: " + ds.F110);
                 Console.WriteLine("F113: " + ds.F113EXPECTED);
                 Console.WriteLine("F124: " + ds.F124EXPECTED);
                 Console.WriteLine("F125: " + ds.F125EXPECTED);
                 Console.WriteLine("F188: " + ds.F188EXPECTED);
                 Console.WriteLine("FAILED: " + ds.FAILCOUNTER);
                 Console.WriteLine("PASSED: " + ds.PASSCOUNTER);
                 Console.WriteLine("DUPLICATE: " + ds.DUPLICATECOUNTER);
                 Console.WriteLine("PALLET: " + ds.QUANTITY);
                 Console.WriteLine("PACKAGE ID: " + ds.PACKAGEID);
                 Console.WriteLine("LOGFILE: " + ds.LOGFILE);

                 

                 DataSerializer.saveToFile(ds);

                 Console.ReadKey();

                 
             }
             else if (dialogResult == DialogResult.No)
             {
                 ds.readFile = false;
                 ds.reset();
                 DataSerializer.saveToFile(ds);
                 e.Cancel = true;
              
             }
         }

          base.OnClosed(e);
        }

        /// <summary>
        /// Called at application start.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDTCApp_Load(object sender, EventArgs e)
        {

            this.Text = "Version Checker (" + Application.ProductVersion + 
                " | Windows 7 Edition)";
            
            //main loader function
            bool valueCAN = false;
            bool honeywell = false;
            Application.EnableVisualStyles();
            

            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Normal;
            this.CenterToScreen();
            this.MaximizeBox = false;

          

            if (!comPort.IsOpen)
            {
                try
                {
                    comPort.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "COMPORT ERROR");
                }
            }

        
            checkHoneywell = true;

            valueCAN = connectValueCAN();
            honeywell = connectHoneywell();


            loadCompleate = true;

           //deserialize (read) the data and load it
            ds = DataSerializer.LoadFromFile();

            Console.WriteLine("APP: " + ds.APLEXPECTED);
            Console.WriteLine("CAL: " + ds.CALEXPECTED);
            Console.WriteLine("PBL: " + ds.PBLEXPECTED);
            Console.WriteLine("E2P: " + ds.E2PEXPECTED);
            Console.WriteLine("SBL: " + ds.SBL);
            Console.WriteLine("F110: " + ds.F110);
            Console.WriteLine("F113: " + ds.F113EXPECTED);
            Console.WriteLine("F124: " + ds.F124EXPECTED);
            Console.WriteLine("F125: " + ds.F125EXPECTED);
            Console.WriteLine("F188: " + ds.F188EXPECTED);
            Console.WriteLine("FAILED: " + ds.FAILCOUNTER);
            Console.WriteLine("PASSED: " + ds.PASSCOUNTER);
            Console.WriteLine("DUPLICATE: " + ds.DUPLICATECOUNTER);
            Console.WriteLine("PALLET: " + ds.QUANTITY);
            Console.WriteLine("PACKAGE ID: " + ds.PACKAGEID);
            Console.WriteLine("LOGFILE: " + ds.LOGFILE);



            Console.WriteLine(ds.readFile.ToString());
            if(ds.readFile) {

                Console.WriteLine("QUANTITY: " + ds.QUANTITY);                   
           //assign data to appropriate UI elements 
            updatePassedQuantityLabel("PASS: " + ds.PASSCOUNTER); 
            updateFailedQuantityLabel("FAIL: " + ds.FAILCOUNTER); 
            updateDuplicateQuantityLabel("DUP: " + ds.DUPLICATECOUNTER);

           

             this.txtAPLExpected.BeginInvoke((MethodInvoker)delegate
             {
                        txtAPLExpected.Text = ds.APLEXPECTED;
             });

           
            this.txtE2PExpected.BeginInvoke((MethodInvoker)delegate
             {
                        txtE2PExpected.Text = ds.E2PEXPECTED;
             });

             this.txtCALExpected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtCALExpected.Text = ds.CALEXPECTED;

                    });
                   
            this.txtPBLExpected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtPBLExpected.Text = ds.PBLEXPECTED;

                    });
                 
                    this.txtDIDF111Expected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtDIDF111Expected.Text = ds.F111EXPECTED;

                    });



                    this.txtDIDF113Expected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtDIDF113Expected.Text = ds.F113EXPECTED;

                    });



                    this.txtDIDF124Expected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtDIDF124Expected.Text = ds.F124EXPECTED;

                    });


       

                    this.txtDIDF125Expected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtDIDF125Expected.Text = ds.F125EXPECTED;

                    });



                    this.txtDIDF188Expected.BeginInvoke((MethodInvoker)delegate
                    {
                        this.txtDIDF188Expected.Text = ds.F188EXPECTED;

                    });


                        setupOverride = true;
                        myForm.setupDone = true;

                        updateReadyLabel("Previous Session Loaded. READY");


                        

                        this.progressBar2.BeginInvoke((MethodInvoker)delegate
                         {

                            double palletprecentage = ((double)ds.PASSCOUNTER / (double)ds.QUANTITY) * 100.00;

                             if (palletprecentage <= 100)
                             {
                                 try
                                 {
                                     this.progressBar2.Value = Convert.ToInt32(palletprecentage);
                                 }
                                 catch (DivideByZeroException dz)
                                 {
                                     MessageBox.Show("The Pallet Qauantity is zero.");
                                 }
                             }
                         });
                    }
                    
                }
         

        /// <summary>
        /// Called at application start up to make sure valueCan is connected.
        /// </summary>
        /// <returns></returns>
        private bool connectValueCAN()
        {

            //byte[] bNetworkIDs = new byte[16] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            //byte[] bSCPIDs = new byte[256];     // Array of SCP functional IDs passed to the driver		
            int iReturnVal = 0;					//iReturn value tells status of the function call
            NeoDevice ndNeoToOpen = new NeoDevice();
            int iNumberOfDevices;
            int lDevTypes;
            lDevTypes = IcsNeoDll.NEODEVICE_ANY;
            //int m_hObject = 0;  // handle for device
            byte[] bNetwork = new byte[64]; //List of the hardware IDs
            int iCount;  // counter    

            //Set the number of devices to find
            iNumberOfDevices = 1;

            //Search for the connected hardware
            iReturnVal = IcsNeoDll.icsneoFindNeoDevices(65535, ref ndNeoToOpen, ref iNumberOfDevices);
            if (iReturnVal == 0)
                return false;
            else
            {
                for (iCount = 0; iCount < 64; iCount++)
                    bNetwork[iCount] = Convert.ToByte(iCount);
                // Open the first found device, ndNeoToOpen acquired from Find NeoDevices
                iReturnVal = IcsNeoDll.icsneoOpenNeoDevice(ref ndNeoToOpen, ref m_hObject, ref bNetwork[0], 1, 0);
                if (iReturnVal == 0)
                    return false;

                m_bPortOpen = true;   //Set Port Opened Flag
                this.updateCANStatus("PASS");
                this.lblValueCANResult.ForeColor = Color.Green;

                //Set the filter up 
                stFilter.Header = convertFromHex("727");
                stFilter.HeaderMask = convertFromHex("FFF");

                //Set the Flow Control Frame Properties
                stMsg.ArbIDOrHeader = convertFromHex("727");
                stMsg.NumberBytesData = 8;
                stMsg.StatusBitField = 2;
                stMsg.Data1 = Convert.ToByte(convertFromHex("30"));    //flow control frame
                stMsg.Data2 = 0;       //block size
                stMsg.Data3 = 0;       //stmin =0

                // load the message structure
                if (myForm.RadioType == "ACM")
                    myBitRate = 125000;
                else if (myForm.RadioType == "EFP")
                    myBitRate = 125000;
                else if (myForm.RadioType == "LXF")
                    myBitRate = 500000;
                else if (myForm.RadioType == "CSACM")
                    myBitRate = 125000;

                //Set the established parameters
                IcsNeoDll.icsneoSetISO15765RxParameters(m_hObject, 1, 1, ref stFilter, ref stMsg, 300, 0, 0, 0);
                IcsNeoDll.icsneoSetBitRate(m_hObject, myBitRate, 1);
                return true;
            }
        }

        /// <summary>
        /// Called to make sure the wireless scanner is connected to the device
        /// </summary>
        /// <returns></returns>
        private bool connectHoneywell()
        {
            if (!comPort.IsOpen)
            {
                try
                {
                    comPort.Open();
                    this.lblHoneywellResult.Text = "PASS";
                    this.lblHoneywellResult.ForeColor = Color.Green;
                    //  checkHoneywell = false;
                }

                catch (Exception ex)
                {

                    //    this.lblResult1.Text = ex.Message;
                    //   this.lblReady.Text = "Connect Scanner";
                    this.lblReady.ForeColor = Color.Red;
                    this.lblHoneywellResult.Text = "FAIL";
                    this.lblHoneywellResult.ForeColor = Color.Red;
                    checkHoneywell = true;
                    // MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            return true;
        }
        private bool testCOMMS()
        {
            #region Send Tester Present
            long lResult;
            IcsSpyMessage stMessagesTx;

            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;
            if (myForm.RadioType == "ACM")
                stMessagesTx.ArbIDOrHeader = 0x727;
            else if (myForm.RadioType == "EFP")
                stMessagesTx.ArbIDOrHeader = 0x7A7;
            else if (myForm.RadioType == "LXF")
                stMessagesTx.ArbIDOrHeader = 0x727;
            else if (myForm.RadioType == "CSACM")
                stMessagesTx.ArbIDOrHeader = 0x727;


            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request tester present to establish COMMS.
            stMessagesTx.Data1 = 0x02;
            stMessagesTx.Data2 = 0x3E;
            stMessagesTx.Data3 = 0;
            stMessagesTx.Data4 = 0;
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to request tester present
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                this.lblValueCANResult.Text = "ERROR";
                this.lblValueCANResult.ForeColor = Color.Red;
                //    if (!ignoreValueCANDialog)
                //   MessageBox.Show("Please reconnect the ValueCAN and then press OK.");
                //   MessageBox.Show("Problem Transmitting Message");

            }
            else
            {
                lblValueCANResult.Text = "PASS";
                this.lblValueCANResult.ForeColor = Color.Green;
            }
            #endregion

            Thread.Sleep(300);

            #region Get Tester Present Response
            lResult = 0;
            int lNumberOfMessages = 0;
            int lNumberOfErrors = 0;
            long lCount;
            icsSpyMessageJ1850 stJMsg;


            if (m_bPortOpen == false)
            {
                // No need to execute read if port is closed because this is a no comms situation. 
                //     Console.WriteLine("MBPORT FAILURE");
                return false;
            }

            stJMsg = CreateEmptyStructureJ1850();

            // read the messages from the driver
            lResult = IcsNeoDll.icsneoGetMessages(m_hObject, ref stMessages[0], ref lNumberOfMessages, ref lNumberOfErrors);

            // Check to see the number of messages read is greater than zero
            if (lNumberOfMessages == 0)
            {
                // No messages = no comms
                //      Console.WriteLine("NO MESSAGES NO COMMS");
                return false;
            }

            // was the read successful?
            if (lResult == 1)
            {
                // for each message we read gather the necessary info
                for (lCount = 1; lCount <= lNumberOfMessages; lCount++)
                {
                    // Used to filter for all 72F and 7AF diagnostic messages
                    if ((stMessages[lCount - 1].ArbIDOrHeader == 1839) || (stMessages[lCount - 1].ArbIDOrHeader == 1967))
                    {
                        // Processing for Negative Response
                        if (stMessages[lCount - 1].Data2 == 0x7F)
                        {
                            // Return true because comms passes if a negative response is received
                            return true;

                        }
                        else if (stMessages[lCount - 1].Data2 == 0x7E)
                        {
                            // Return true because comms passes if a positive response is received
                            return true;
                        }
                    }
                }
                // Return false because comms fails becsuse no response is received 
                //     Console.WriteLine("NO RESPONSE RECEIVED");
                return false;
            }
            return false;
            //     Console.WriteLine("FAILURE");

            #endregion

        }


        /// <summary>
        /// Controls flow control for CAN bus. Called evertime flowControlTimer ticks.
        /// </summary>
        /// <param name="myArbID"></param>
        private void transmitFlowControl(int myArbID)
        {

           
            int lResult;
            IcsSpyMessage stMessagesTx;
            int counter = 0;


            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;
            stMessagesTx.ArbIDOrHeader = myArbID;

            // Load all of the data bytes in the structure
            // This is for request tester present to establish COMMS.
            stMessagesTx.Data1 = 0x30;
            stMessagesTx.Data2 = 0x00;
            stMessagesTx.Data3 = 0;
            stMessagesTx.Data4 = 0;
            stMessagesTx.Data5 = 0;
            stMessagesTx.Data6 = 0;
            stMessagesTx.Data7 = 0;
            stMessagesTx.Data8 = 0;

            // Transmit the assembled message to request tester present
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                //This is the one throwing the error
                lblValueCANResult.Text = "FAIL";

                this.lblValueCANResult.ForeColor = Color.Red;

           

            }
            else
            {
                lblValueCANResult.Text = "PASS";
                this.lblValueCANResult.ForeColor = Color.Green;
            }
        }
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!inScan && !isWriting)
            {
                inSetup = true;
                stopTimers = true;
                myForm.setPassedQuantity(passedQuantity);
                myForm.StartPosition = FormStartPosition.CenterScreen;
               // myForm.setPrecentage(this.precentage);
                myForm.ShowDialog();
                stopTimers = false;
            }
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myAbout.StartPosition = FormStartPosition.CenterScreen;
            myAbout.ShowDialog();
        }


        /// <summary>
        /// Organizes requested DID information gathed from a DID request.
        /// </summary>
        private void readDIDs()
        {
            long lResult = 0;
            int lNumberOfMessages = 0;
            int lNumberOfErrors = 0;
            long lCount;
            icsSpyMessageJ1850 stJMsg;
            int DTCcounter = 0;


            if (m_bPortOpen == false)
            {
                // No need to execute read if port is closed 
                //     Console.WriteLine("PORT CLOSED");
                return;
            }

            stJMsg = CreateEmptyStructureJ1850();

            // read the messages from the driver
            lResult = IcsNeoDll.icsneoGetMessages(m_hObject, ref stMessages[0], ref lNumberOfMessages, ref lNumberOfErrors);

            // Check to see the number of messages read is greater than zero
            if (lNumberOfMessages == 0)
            {
                //     Console.WriteLine("0 messages");
                return;
            }



            // was the read successful?
            if (lResult == 1)
            {
                //      Console.WriteLine("GOOD");
                // for each message we read gather the necessary info
                for (lCount = 1; lCount <= lNumberOfMessages; lCount++)
                {
                    // Used to filter for all 72F diagnostic messages
                    if ((stMessages[lCount - 1].ArbIDOrHeader == 1839) || (stMessages[lCount - 1].ArbIDOrHeader == 1967))
                    {

                        // Processing for Negative Response
                        if (stMessages[lCount - 1].Data2 == 0x7F)
                        {
                            //     Console.WriteLine("0x7F");
                            // Create strings for negative response to read DID 

                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            extractDID();

                        }
                        else if (stMessages[lCount - 1].Data1 != 0x30)
                        {


                            byte testByte = Convert.ToByte(stMessages[lCount - 1].Data2);

                            if (testByte != 0x00 && isConnectionRequest)
                            {
                                testResponse = true;
                                //    deviceConnected = true;
                                return;
                            }
                            else if (testByte != 0x00 && deviceScanned)
                            {
                                testResponse = true;
                                isConnectionRequest = false;
                                deviceScanned = false;
                                return;
                            }
                            else if (testByte == 0x00)
                            {
                                testResponse = false;
                                //   updateReadyLabel("Connect Device");
                            }

                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            //   Console.WriteLine(myDTCs.ToString());
                            DTCcounter = DTCcounter + 7;
                            //    Console.WriteLine("DTCcounter: " + DTCcounter);
                            if (DTCcounter >= 27)
                            {
                                extractDID();
                            }
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Organizes part number information gatherd from a part number request.
        /// </summary>
        private void readPartNumber()
        {
            long lResult = 0;
            int lNumberOfMessages = 0;
            int lNumberOfErrors = 0;
            long lCount;
            icsSpyMessageJ1850 stJMsg;
            int DTCcounter = 0;


            if (m_bPortOpen == false)
            {
                // No need to execute read if port is closed
                return;
            }

            stJMsg = CreateEmptyStructureJ1850();

            // read the messages from the driver
            lResult = IcsNeoDll.icsneoGetMessages(m_hObject, ref stMessages[0], ref lNumberOfMessages, ref lNumberOfErrors);

            // Check to see the number of messages read is greater than zero
            if (lNumberOfMessages == 0)
            {
                return;
            }

            // was the read successful?
            if (lResult == 1)
            {
                // for each message we read gather the necessary info
                for (lCount = 1; lCount <= lNumberOfMessages; lCount++)
                {
                    // Used to filter for all 72F diagnostic messages
                    if ((stMessages[lCount - 1].ArbIDOrHeader == 1839) || (stMessages[lCount - 1].ArbIDOrHeader == 1967))
                    {
                        // Processing for Negative Response
                        if (stMessages[lCount - 1].Data2 == 0x7F)
                        {
                            // Create strings for negative response to read DID
                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            extractPartNumber();

                        }
                        else if (stMessages[lCount - 1].Data1 != 0x30 & stMessages[lCount - 1].Data1 != 0x06)
                        {
                            // Create strings for positive response to read DID
                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            DTCcounter = DTCcounter + 7;
                            if (DTCcounter >= 8)
                            {
                                extractPartNumber();
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Organizes DTC information gatherd from a part number request.
        /// </summary>
        private void readDTCs()
        {
            long lResult = 0;
            int lNumberOfMessages = 0;
            int lNumberOfErrors = 0;
            long lCount;
            icsSpyMessageJ1850 stJMsg;
            int DTCcounter = 0;


            if (m_bPortOpen == false)
            {
                // No need to execute read if port is closed
                return;
            }

            stJMsg = CreateEmptyStructureJ1850();

            // read the messages from the driver
            lResult = IcsNeoDll.icsneoGetMessages(m_hObject, ref stMessages[0], ref lNumberOfMessages, ref lNumberOfErrors);

            // Check to see the number of messages read is greater than zero
            if (lNumberOfMessages == 0)
            {
                return;
            }

            
            // was the read successful?
            if (lResult == 1)
            {
                // for each message we read gather the necessary info
                for (lCount = 1; lCount <= lNumberOfMessages; lCount++)
                {
                    // Used to filter for all 72F or 7AF diagnostic messages
                    if ((stMessages[lCount - 1].ArbIDOrHeader == 1839) || (stMessages[lCount - 1].ArbIDOrHeader == 1967))
                    {
                        //for testing 
                        byte b1 = stMessages[lCount - 1].Data1;
                        byte b2 = stMessages[lCount - 1].Data2;
                        byte b3 = stMessages[lCount - 1].Data3;
                        byte b4 = stMessages[lCount - 1].Data4;
                        byte b5 = stMessages[lCount - 1].Data5;
                        byte b6 = stMessages[lCount - 1].Data6;
                        byte b7 = stMessages[lCount - 1].Data7;
                        byte b8 = stMessages[lCount - 1].Data8;

                        Console.WriteLine(b1.ToString("X2") + " " + b2.ToString("X2") + " " + b3.ToString("X2") + 
                            " " + b4.ToString("X2") + " " + b5.ToString("X2") + " " 
                            + b6.ToString("X2") + " " + b7.ToString("X2") + " " + b8.ToString("X2"));

                        // Processing for Negative Response
                        if (stMessages[lCount - 1].Data2 == 0x7F)
                        {
                            Console.WriteLine("NEGATIVE");
                            // Create strings for negative response to read DID

                           
                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            //extractDTC();

                        }
                        else if (stMessages[lCount - 1].Data1 != 0x30)
                        {
                            Console.WriteLine("POSITIVE");
                            // Create strings for positive response to read DID
                            myDTCs[DTCcounter] = Convert.ToByte(stMessages[lCount - 1].Data1);
                            myDTCs[DTCcounter + 1] = Convert.ToByte(stMessages[lCount - 1].Data2);
                            myDTCs[DTCcounter + 2] = Convert.ToByte(stMessages[lCount - 1].Data3);
                            myDTCs[DTCcounter + 3] = Convert.ToByte(stMessages[lCount - 1].Data4);
                            myDTCs[DTCcounter + 4] = Convert.ToByte(stMessages[lCount - 1].Data5);
                            myDTCs[DTCcounter + 5] = Convert.ToByte(stMessages[lCount - 1].Data6);
                            myDTCs[DTCcounter + 6] = Convert.ToByte(stMessages[lCount - 1].Data7);
                            myDTCs[DTCcounter + 7] = Convert.ToByte(stMessages[lCount - 1].Data8);
                            DTCcounter = DTCcounter + 8;
                        }

                        Console.WriteLine("OTHER");
                    }
                }
            }
            extractDTC();

        }

        /// <summary>
        /// Formally closes Excel log file.
        /// </summary>
        private void closeLogFile()
        {
            string myPath = @"C:\DTC Checker\" + myForm.LogFile;
            object misValue = System.Reflection.Missing.Value;

            // If file already exists and is open
            if (File.Exists(myPath))
            {
                if (logOpen == true)
                {
                    myExcelWorkBook = myExcelApp.Workbooks.Open(myPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                    myExcelWorkSheet = (Excel.Worksheet)myExcelWorkBook.Worksheets.get_Item(1);
                    myExcelWorkBook.Close(true, misValue, misValue);
                    myExcelApp.Quit();
                    logOpen = false;
                    releaseObject(myExcelApp);
                    releaseObject(myExcelWorkBook);
                    releaseObject(myExcelWorkSheet);
                }
            }
        }


        /// <summary>
        /// Called when the wireless scanner scans a barcode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void comPort_DataReceived(object sender, EventArgs e)
        {
            humanInput = false;
            //FOR TESTING 

            // MessageBox.Show("COM DATA RECIEVED");
            // Close log file if open before beginning
            closeLogFile();
            oldResult = myMessage.Result;

            bool myTest = false;
            bool comOpen = true;

            //reads data from serial port  


            if (!comPort.IsOpen)
            {
                try
                {
                    comPort.Open();

                }
                catch
                {
                    comOpen = false;
                    MessageBox.Show("Com port could not be opened. L1863");
                }
            }



            if (comPort.IsOpen)
                myMessage.Data = comPort.ReadExisting();
            else
                MessageBox.Show("Comport not open");

            if (myForm.Visible != true && scan)
            {

                if ((myMessage.Data[0] == 'S') && (myForm.Visible != true) && (myMessage.Data[0].ToString() + myMessage.Data[1].ToString() != "SP")) // Update serial number only
                {
                    
                    this.duplicateDevice = false;
                    waitingForSerial = false;
                    stopTimers = true;
                    scan = false;
                    bool duplicate = false;
                    noSerial = false;
                    for (int x = 0; x < ds.SERIALHISTORY.Count; x++)
                    {
                        if (ds.SERIALHISTORY[x].ToString() == myMessage.SerialNumber)
                        {
                            duplicate = true;
                        }
               
                    } 

                        ds.SERIALHISTORY.Add(myMessage.SerialNumber);
                        updateSerialNumber(myMessage.SerialNumber);
                      

                        if (this.deviceResultLabel.Text == "PASS")
                        {
                            ds.DEVICERESULT = "PASS";
                           this.deviceResultLabel.ForeColor = Color.Green;
                           this.deviceResultLabel.BackColor = Color.Transparent;
                        }


                        if (this.dtcResultLabel.Text == "PASS")
                        {
                            ds.DTCRESULT = "PASS";
                            this.dtcResultLabel.ForeColor = Color.Green;
                            this.dtcResultLabel.BackColor = Color.Transparent;
                        }



                        //passed
                        if (!duplicate && this.deviceResultLabel.Text == "PASS" && this.dtcResultLabel.Text == "PASS")
                        {
                            ds.PASSCOUNTER++;
                            //invoke gui

                            this.passedQuantityLabel.BeginInvoke((MethodInvoker)delegate
                            {
                                this.passedQuantityLabel.Text = "PASS: " + ds.PASSCOUNTER;
                                if (ds.PASSCOUNTER == ds.QUANTITY)
                                {
                                    MessageBox.Show("PALLET COMPLETE");
                                    Application.Restart();
                                }
                            });

                        

                            this.progressBar2.BeginInvoke((MethodInvoker)delegate
                             {

                                double progressPrecentage = ((double)ds.PASSCOUNTER / (double)ds.QUANTITY) * 100.00;

                                 if (progressPrecentage <= 100)
                                 {
                                     try
                                     {
                                         this.progressBar2.Value = Convert.ToInt32(progressPrecentage);
                                     }
                                     catch (DivideByZeroException dz)
                                     {
                                         MessageBox.Show("The Pallet Qauantity is zero.");
                                     }
                                 }
                             });

                        } 

                         //failed 
                        if (!duplicate && ((this.deviceResultLabel.Text == "FAIL" && this.dtcResultLabel.Text == "FAIL") ||
                            (this.deviceResultLabel.Text == "FAIL" || this.dtcResultLabel.Text == "FAIL")))
                        {

                            //start the failed form to ask for supervisor password 

                            Console.WriteLine("in the failed case.");
                            passwordForm = new PasswordForm(comPort);
                            this.passwordForm.StartPosition = FormStartPosition.CenterScreen;
                            this.passwordForm.ShowDialog();  


                            ds.FAILCOUNTER++;

                            this.failedQuantityLabel.BeginInvoke((MethodInvoker)delegate
                            {
                                this.failedQuantityLabel.Text = "FAIL: " + ds.FAILCOUNTER;
                            }); 
                            
                        } 


                        //duplicate
                        if (duplicate)
                        {
                            duplicateDevice = true;
                            ds.DUPLICATECOUNTER++;
                            this.duplicateQuantityLabel.BeginInvoke((MethodInvoker)delegate
                            {
                                this.duplicateQuantityLabel.Text = "DUP: " + ds.DUPLICATECOUNTER;
                            });
                        }


                        flashLabelTimer.Stop();
                        flashLabelTimer.Dispose();

                        updateReadyLabel("WRITING TO EXCEL");
                        isWriting = true;
                        writeToExcel();
                        myMessage.ClearDTC = "";
                        clearDTCList();
                        clearForm1();
                        clearForm2();
                        DTCs.Clear(); 

                        //reset progress bar
                        this.progressBar1.BeginInvoke((MethodInvoker)delegate
                        {
                            this.progressBar1.Value = 0;
                        });

                        updateSerialNumber("");
                        updateDTCResult("");
                        myMessage.ClearDTC = "";
                        this.lblReady.ForeColor = Color.Black;
 
                        isWriting = false;
                        updateReadyLabel("READY");
                      
                        afterScan = false;
                        scan = true;
                }

                //    updateReadyLabel("Press Spacebar to Read Device");
                transferInProgress = false;
            }
            else if (inSetup)
            {
                //      Console.WriteLine("setup scan");
            }
            humanInput = true;
            transferInProgress = false;
            stopTimers = false;
            //    checkConnectionTimer.Start();
        }




        void clearForm1()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            updatePartNumber(null);
        //    updateDIDF113(null);
            myMessage.PartNumber = null;
            updateDeviceResult("");
            myMessage.DIDF111 = "";
            myMessage.DIDF113 = null;
            myMessage.DIDF124 = "";
            myMessage.DIDF125 = "";
            myMessage.DIDF188 = "";
            myMessage.APP = "";
            myMessage.CAL = "";
            myMessage.E2P = "";
            myMessage.PBL = "";
            myMessage.ClearDTC = "";
            myMessage.DTCCount = 0;
            receiveAPP("");
            receivePBL("");
            receiveCAL("");
            receiveE2P("");

        }
        void clearForm2()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            updateDeviceResult("");
            myMessage.DIDF111 = "";
            myMessage.DIDF113 = null;
            myMessage.DIDF124 = "";
            myMessage.DIDF125 = "";
            myMessage.DIDF188 = "";
            myMessage.APP = "";
            myMessage.CAL = "";
            myMessage.E2P = "";
            myMessage.PBL = "";
            myMessage.ClearDTC = "";
            myMessage.DTCCount = 0;
            receiveAPP("");
            receivePBL("");
            receiveCAL("");
            receiveE2P("");
        }
        void clearMessageData()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            updateDeviceResult("");
            myMessage.DIDF111 = "";
            myMessage.DIDF113 = "";
            myMessage.DIDF124 = "";
            myMessage.DIDF125 = "";
            myMessage.DIDF188 = "";
            myMessage.APP = "";
            myMessage.CAL = "";
            myMessage.E2P = "";
            myMessage.PBL = "";
            myMessage.PartNumber = "";
            receiveAPP("");
            receivePBL("");
            receiveCAL("");
            receiveE2P("");
        }
        void clearDTCList()
        {
            if (this.lstDTCs.InvokeRequired == true)
            {
                this.lstDTCs.Invoke((MethodInvoker)delegate()
                {
                    clearDTCList();
                });
            }
            else
                this.lstDTCs.Items.Clear();
        }
        void updatePartNumber(string m)
        {
            if (this.txtPartNumber.InvokeRequired == true)
            {
                this.txtPartNumber.Invoke((MethodInvoker)delegate()
                {
                    updatePartNumber(m);
                });
            }
            else
                this.txtPartNumber.Text = m;
        }

        void updateDeviceScanStatus(string m)
        {
            if (this.deviceScanningStatusLabel.InvokeRequired == true)
            {
                this.deviceScanningStatusLabel.Invoke((MethodInvoker)delegate()
                {
                    updateDeviceScanStatus(m);
                });
            }
            else
                this.deviceScanningStatusLabel.Text = m;
        }
        void updateReadyLabel(string m)
        {
          //  Console.WriteLine("UPDATE READY CALLED!!!!!");
            if (this.lblReady.InvokeRequired == true)
            {
                this.lblReady.Invoke((MethodInvoker)delegate()
                {
                    updateReadyLabel(m);
                });
            }
            else
                this.lblReady.Text = m;
        }
        void updateDTCList(string m)
        {
            if (this.lstDTCs.InvokeRequired == true)
            {
                this.lstDTCs.Invoke((MethodInvoker)delegate()
                {
                    updateDTCList(m);
                });
            }
            else
                this.lstDTCs.Items.Add(m);
        }
        void removeDTCItem(string m)
        {
            if (this.lstDTCs.InvokeRequired == true)
            {
                this.lstDTCs.Invoke((MethodInvoker)delegate()
                {
                    removeDTCItem(m);
                });
            }
            else
                this.lstDTCs.Items.Remove(m);
        }
        void updateCounter(string m)
        {
            if (this.btnLogFile.InvokeRequired == true)
            {
                this.btnLogFile.Invoke((MethodInvoker)delegate()
                {
                    updateCounter(m);
                });
            }
            else
                this.btnLogFile.Text = m;
        }
        void updateDIDF113(string m)
        {
            if (this.txtDIDF113Expected.InvokeRequired == true)
            {
                this.txtDIDF113Expected.Invoke((MethodInvoker)delegate()
                {
                    updateDIDF113(m);
                });
            }
            else
                this.txtDIDF113Expected.Text = m;
        }
        void updateSerialNumber(string m)
        {
            if (this.txtSerialNumber.InvokeRequired == true)
            {
                this.txtSerialNumber.Invoke((MethodInvoker)delegate()
                {
                    updateSerialNumber(m);
                });
            }
            else
                this.txtSerialNumber.Text = m;
        }
        void updateDTCResult(string m)
        {
            if (this.dtcResultLabel.InvokeRequired == true)
            {
                this.dtcResultLabel.Invoke((MethodInvoker)delegate()
                {
                    updateDTCResult(m);
                });
            }
            else
                this.dtcResultLabel.Text = m;
        }

        void updatePassedQuantityLabel(string m)
        {
            if (this.passedQuantityLabel.InvokeRequired == true)
            {
                this.passedQuantityLabel.Invoke((MethodInvoker)delegate()
                {
                  //  updatePassedQuanityLabel(m);
                });
            }
            else
                this.passedQuantityLabel.Text = m;
        }

        void updateFailedQuantityLabel(string m)
        {
            if (this.failedQuantityLabel.InvokeRequired == true)
            {
                this.failedQuantityLabel.Invoke((MethodInvoker)delegate()
                {
                    updateFailedQuantityLabel(m);
                });
            }
            else
                this.failedQuantityLabel.Text = m;
        }

        void updateDuplicateQuantityLabel(string m)
        {
            if (this.duplicateQuantityLabel.InvokeRequired == true)
            {
                this.duplicateQuantityLabel.Invoke((MethodInvoker)delegate()
                {
                    updateDuplicateQuantityLabel(m);
                });
            }
            else
                this.duplicateQuantityLabel.Text = m;
        }


        /// <summary>
        /// Puts DID in propor location on the GUI
        /// </summary>
        void extractDID()
        {
            //    Console.WriteLine("ExtractDID called");
            string myTemp;

            //Check to see that a positive response was received to request DID
            if (myDTCs[1] == 0x62)
            {
                if (lblPartNumber.Text == "DID F125")
                    myTemp = ASCIIEncoding.ASCII.GetString(myDTCs, 4, 14);
                else
                    myTemp = ASCIIEncoding.ASCII.GetString(myDTCs, 4, 17);
                

                if (myDTCs[3] == 0x11)
                    myMessage.DIDF111 = myTemp;
                else if (myDTCs[3] == 0x13)
                    myMessage.DIDF113 = myTemp;
                else if (myDTCs[3] == 0x24)
                    myMessage.DIDF124 = myTemp;
                else if (myDTCs[3] == 0x25 | myDTCs[3] == 0x10)
                    myMessage.DIDF125 = myTemp;
                else if (myDTCs[3] == 0x88)
                    myMessage.DIDF188 = myTemp;

            }
            // Setup reply for a negative response
            else if (myDTCs[0] == 0x7f)
            {
                //         Console.WriteLine("Negative response");
                if (myMessage.DIDRequest == 0x11)
                    myMessage.DIDF111 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x13)
                    myMessage.DIDF113 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x24)
                    myMessage.DIDF124 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x25)
                    myMessage.DIDF125 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x88)
                    myMessage.DIDF188 = "NRC " + myDTCs[2].ToString("X");
            }
            else if (myDTCs[0] != 0x50)
            {
                //      Console.WriteLine("!= 0x50");
                myMessage.DIDF111 = "No Response";
                myMessage.DIDF113 = "No Response";
                myMessage.DIDF124 = "No Response";
                myMessage.DIDF125 = "No Response";
                myMessage.DIDF188 = "No Response";
            }
        }

       

        /// <summary>
        /// Gets the DTCs and matches them with their description. 
        /// Then outputs the DTCs on the GUI DTC list. 
        /// Does not print off the DTCs if they are ignored or inactive.
        /// </summary>
        void extractDTC()
        {
          //  Console.WriteLine(Encoding.Default.GetString(myDTCs));

            for (int x = 0; x < 200; x++)
            {
                Console.WriteLine( x + " " + myDTCs[x].ToString("X2"));
            }
            Console.WriteLine("EXTRACT CALLED");
            this.ignoredDTCs = myForm.ignoredDTCs;
            string[] myTemp = new string[75];
            string myList = null;
            string myQuickString = null;
          //  Console.WriteLine(myDTCs.ToString());

            // CASE 1 :  Positive response and 1 DTCs
            if (myDTCs[0] == 0x07 && myDTCs[1] == 0x59)
            {
                Console.WriteLine("CASE 1");
                for (int i = 0; i < 4; i++)
                {
                    if (myDTCs[i + 4] <= 0xF)
                    {
                        myTemp[i] = "0" + myDTCs[i + 4].ToString("X");
                        //           Console.WriteLine(myTemp[i]);
                    }
                    else
                    {
                        myTemp[i] = myDTCs[i + 4].ToString("X");
                        //            Console.WriteLine(myTemp[i]);
                    }
                }

              
                //   Console.WriteLine("MyQuickString " + myQuickString);
                myQuickString = commentDTCs(myTemp[0] + myTemp[1] + myTemp[2] + myTemp[3]);
                updateDTCList(myQuickString);
                myMessage.DTC = myQuickString + ",";
                myMessage.DTCCount = myMessage.DTCCount + 1;
            }
            // CASE 2:  Positive response and 0 DTCs
            else if (myDTCs[0] == 0x03 && myDTCs[1] == 0x59)
            {
                Console.WriteLine("CASE 2");
                updateDTCList("NO DTCs");
                myMessage.DTC = "NO DTCs";
            }
            // CASE 3:  Positive response and 2 or more DTCs
            else if (myDTCs[0] == 0x10 && myDTCs[2] == 0x59)
            {
                Console.WriteLine("Original DTC request.");
                Console.WriteLine("CASE 3");

                for (int i = 1; i <= 40; i++)
                {
                    if ((i + 4) % 8 != 0)
                    {
                        if (myDTCs[i + 4] <= 0xF)
                        {
                            myTemp[i] = "0" + myDTCs[i + 4].ToString("X");

                            if (myTemp[i] == "0A")
                                Console.WriteLine("FOUND 0A");
                            else if (myTemp[i] == "08")
                                Console.WriteLine("FOUND 08");

                            Console.WriteLine(i + ": " + myTemp[i]);
                        }
                        else
                        {
                            myTemp[i] = myDTCs[i + 4].ToString("X");

                            if (myTemp[i] == "48")
                                Console.WriteLine("FOUND 48");

                            Console.WriteLine(i + ": " + myTemp[i]);
                        }
                    }
                }

            

                // Parse out the list of DTCs to 1 entire string

                if (myDTCs.Length > 0)
                {
                    updateDTCResult("FAIL");
                    //    timer2.Start();
                    this.dtcResultLabel.ForeColor = Color.Red;

                }
                else
                {
                    updateDTCResult("PASS");
                    //    timer2.Stop();
                    this.dtcResultLabel.ForeColor = Color.Green;
                }

                foreach (String i in myTemp)
                    myList = myList + i;


                Console.WriteLine(myList);

                // Set length to ensure that it equals CAN message frame length
                myList = myList.Substring(0, ((myDTCs[1] - 3) * 2));
                Console.WriteLine("MY LIST: " + myList);

                // Create a loop to add each DTC from the list to the DTC List Box
                for (int i = 0; i < (myList.Length - 7); i++)
                {
                    string currentDTC = myList.Substring(i, 8);
                    Console.WriteLine("CURRENT DTC: " + currentDTC);

                    //for testing only
                  //  updateDTCList(currentDTC);

                 //   if (!currentDTC.EndsWith("48") && !currentDTC.EndsWith("08"))
                 //   {
                    //    this.ignoredDTCs = myForm.ignoredDTCs;
                        myQuickString = commentDTCs(myList.Substring(i, 8));
                        Console.WriteLine(myQuickString);

                        bool inList = false;
                        for (int x = 0; x < ignoredDTCs.Count; x++)
                        {
                            if (myQuickString.Substring(0, 5) == ignoredDTCs[x].ToString().Substring(0, 5))
                            {
                                inList = true;
                                Console.WriteLine("IN LIST"); 
                            }
                        }

                        int counter = 0;
                        string line;

                        string appLoc = AppDomain.CurrentDomain.BaseDirectory;

                        try
                        {
                            System.IO.StreamReader file = new System.IO.StreamReader(appLoc + "ignored_dtcs.txt");

                            while ((line = file.ReadLine()) != null)
                            {
                                this.ignoredDTCs.Add(line);
                                counter++;
                            }

                          

                            file.Close();
                        }
                        catch
                        {

                            Console.WriteLine("warning: dtc file not found....");
                        }

                        bool inList2 = false;
                        for (int x = 0; x < ignoredDTCs.Count; x++)
                        {
                            if (myQuickString.Substring(0, 5) == ignoredDTCs[x].ToString().Substring(0, 5))
                            {
                                inList2 = true;
                                Console.WriteLine("IN LIST");
                            }
                        }

                        DTCs.Add(myQuickString);
                        if (myQuickString != "E100000A - Initial config not complete."
                            && myQuickString != "E101000A - Misconfiguration." &&
                           !inList && myQuickString != "00000000" && !inList2)
                        {
                            updateDTCList(myQuickString);
                            myMessage.DTC = myQuickString + ",";
                            myMessage.DTCCount = myMessage.DTCCount + 1;
                        }
                  //  }

                    i = i + 7;
                }

                if (myMessage.DTCCount == 0)
                {
                    //    timer2.Stop();
                    updateDTCResult("PASS");
                    this.dtcResultLabel.ForeColor = Color.Green;

                }

            }
            // CASE 4:  Negative response 
            else if (myDTCs[0] == 0x03 && myDTCs[1] == 0x7F)
            {
                Console.WriteLine("CASE 4");
                updateDTCList("NRC " + myDTCs[2].ToString("X"));
                myMessage.DTC = ("NRC " + myDTCs[2].ToString("X"));
            }

            else if (myDTCs[24] == 0x10 && myDTCs[26] == 0x59)
            {
                Console.WriteLine("here");
                Console.WriteLine(myDTCs.Length);
                //find where the end of the data is 
              /*  int length = 0;
                for(int x = myDTCs.Length; x > 0; x--) 
                {
                    byte testByte = myDTCs[x];
                    if (testByte != 0x00)
                    {
                        length = x - 1;
                        Console.WriteLine("LENGTH: " + x);
                        break;
                    }
                } */

                Console.WriteLine("CASE 5");

                for (int i = 25; i <= 65; i++)
                {
                    Console.WriteLine("in loop " + i);
                    if ((i + 4) % 8 != 0)
                    {
                        if (myDTCs[i + 4] <= 0xF)
                        {
                            myTemp[i] = "0" + myDTCs[i + 4].ToString("X");

                            if (myTemp[i] == "0A")
                                Console.WriteLine("FOUND 0A");
                            else if (myTemp[i] == "08")
                                Console.WriteLine("FOUND 08");

                            Console.WriteLine(i + ": " + myTemp[i]);
                        }
                        else
                        {
                            myTemp[i] = myDTCs[i + 4].ToString("X");

                            if (myTemp[i] == "48")
                                Console.WriteLine("FOUND 48");

                            Console.WriteLine(i + ": " + myTemp[i]);
                        }
                    }
                }

                Console.WriteLine("post loop");

                // Parse out the list of DTCs to 1 entire string

                if (myDTCs.Length > 0)
                {
                    updateDTCResult("FAIL");
                    //    timer2.Start();
                    this.dtcResultLabel.ForeColor = Color.Red;

                }
                else
                {
                    updateDTCResult("PASS");
                    //    timer2.Stop();
                    this.dtcResultLabel.ForeColor = Color.Green;
                }

                foreach (String i in myTemp)
                    myList = myList + i;


                Console.WriteLine(myList);

                // Set length to ensure that it equals CAN message frame length
                //myList = myList.Substring(0, ((myDTCs[25] - 3) * 2) - 2);

                // Create a loop to add each DTC from the list to the DTC List Box
                for (int i = 0; i < (myList.Length - 7); i++)
                {
                    string currentDTC = myList.Substring(i, 8);

                    Console.WriteLine("CURRENT DTC: " + currentDTC);

                    //for testing only
                 //   updateDTCList(currentDTC);

                    if (!currentDTC.EndsWith("48") && !currentDTC.EndsWith("08"))
                    {
                      //  this.ignoredDTCs = myForm.ignoredDTCs;
                        myQuickString = commentDTCs(myList.Substring(i, 8));
                        Console.WriteLine(myQuickString);

                        bool inList = false;
                        for (int x = 0; x < ignoredDTCs.Count; x++)
                        {
                            if (myQuickString.Substring(0, 5) == ignoredDTCs[x].ToString().Substring(0, 5))
                            {
                                inList = true;
                            }
                        }

                        int counter = 0;
                        string line;

                        string appLoc = AppDomain.CurrentDomain.BaseDirectory;

                        try
                        {
                            System.IO.StreamReader file = new System.IO.StreamReader(appLoc + "ignored_dtcs.txt");

                            while ((line = file.ReadLine()) != null)
                            {
                                this.ignoredDTCs.Add(line);
                                counter++;
                            }

                            

                            file.Close();
                        }
                        catch
                        {

                            Console.WriteLine("warning: dtc file not found....");
                        }

                        bool inList2 = false;
                        for (int x = 0; x < ignoredDTCs.Count; x++)
                        {
                            if (myQuickString.Substring(0, 5) == ignoredDTCs[x].ToString().Substring(0, 5))
                            {
                                inList2 = true;
                                Console.WriteLine("IN LIST");
                            }
                        }

                        DTCs.Add(myQuickString);
                        if (myQuickString != "E100000A - Initial config not complete."
                            && myQuickString != "E101000A - Misconfiguration."
                            && !inList && !inList2 && myQuickString != "00000000")
                        {
                            updateDTCList(myQuickString);
                            myMessage.DTC = myQuickString + ",";
                            myMessage.DTCCount = myMessage.DTCCount + 1;
                        }
                    }

                    i = i + 7;
                }

                Console.WriteLine("post for loop");

                if (myMessage.DTCCount == 0)
                {
                    //    timer2.Stop();
                    updateDTCResult("PASS");
                    this.dtcResultLabel.ForeColor = Color.Green;

                }

            }

            else
            {
                Console.WriteLine("NO CASE");
            }

            Console.WriteLine("end of extraction");
        }

        /// <summary>
        /// Gets the part number from the part number data.
        /// </summary>
        void extractPartNumber()
        {
            string myTemp;
            string output = "";

            //Check to see that a positive response was received to request DID
            if (myDTCs[1] == 0xFA)
            {

                output = output + myDTCs[4].ToString("X");
                Console.WriteLine(output + " #4");

                for (int x = 5; x < 10; x++)
                {
                    string temp = myDTCs[x].ToString("X");

                    if (Convert.ToInt16(temp) < 10 && Convert.ToInt16(temp) >= 0)
                    {

                        temp = "0" + temp;
                        Console.WriteLine(temp + " #" + x);
                        output = output + temp;
                    }
                    else
                        output = output + temp;

                }

               
               

                if (myDTCs[3] == 0x01)
                {
                    Console.WriteLine("APP************************");
                 //  output = output.Substring(0, 3) + "-" + output.Substring(4, 3) + "-" + output.Substring(6, 3);
                    output = Regex.Replace(output, @"(\w{3})(\w{3})(\w{3})", @"$1-$2-$3").ToString(); 
                    output = output.Remove(output.Length - 2, 2);
                    myMessage.APP = output;
                }
                else if (myDTCs[3] == 0x02)
                {
                    Console.WriteLine("CAL************************");
                //   output = output.Substring(0, 3) + "-" + output.Substring(4, 4) + "-" + output.Substring(7, 2); 
                    output = Regex.Replace(output, @"(\w{3})(\w{4})(\w{2})", @"$1-$2-$3").ToString();
                    output = output.Remove(output.Length - 2, 2);
                    myMessage.CAL = output;
                }
                else if (myDTCs[3] == 0x03)
                {
                    Console.WriteLine("E2P************************");
                //    output = output.Substring(0, 3) + "-" + output.Substring(4, 4) + "-" + output.Substring(7, 2); 
                    output = Regex.Replace(output, @"(\w{3})(\w{4})(\w{2})", @"$1-$2-$3").ToString();
                    output = output.Remove(output.Length - 2, 2);
                    myMessage.E2P = output;
                }
                else if (myDTCs[3] == 0x04)
                {
                    Console.WriteLine("PBL************************");
                //   output = output.Substring(0, 3) + "-" + output.Substring(4, 3) + "-" + output.Substring(6, 3); 
                    output = Regex.Replace(output, @"(\w{3})(\w{3})(\w{3})", @"$1-$2-$3").ToString();
                    output = output.Remove(output.Length - 2, 2);
                    myMessage.PBL = output;
                }

            //outputs things the old way
                else if (this.myForm.APP.Length == 2)
                {
                    myTemp = myDTCs[8].ToString("X");

                    // Add zero pad in front of value is it is less than 15
                    if (Convert.ToInt16(myTemp) < 15)
                        myTemp = "0" + myTemp;


                    if (myDTCs[3] == 0x01)
                        myMessage.APP = myTemp.Substring(myTemp.Length - 2, 2);
                    else if (myDTCs[3] == 0x02)
                        myMessage.CAL = myTemp.Substring(myTemp.Length - 2, 2);
                    else if (myDTCs[3] == 0x03)
                        myMessage.E2P = myTemp.Substring(myTemp.Length - 2, 2);
                    else if (myDTCs[3] == 0x04)
                        myMessage.PBL = myTemp.Substring(myTemp.Length - 2, 2);

                }


            }
            // Setup reply for a negative response
            else if (myDTCs[0] == 0x7f)
            {
                if (myMessage.DIDRequest == 0x11)
                    myMessage.DIDF111 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x13)
                    myMessage.DIDF113 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x24)
                    myMessage.DIDF124 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x25)
                    myMessage.DIDF125 = "NRC " + myDTCs[2].ToString("X");
                else if (myMessage.DIDRequest == 0x88)
                    myMessage.DIDF188 = "NRC " + myDTCs[2].ToString("X");
            }
            else
            {
                myMessage.APP = "NR";
                myMessage.CAL = "NR";
                myMessage.E2P = "NR";
                myMessage.PBL = "NR";
            }


        }
        void receiveAPP(string m)
        {
            if (this.txtAPP.InvokeRequired == true)
            {
                this.txtAPP.Invoke((MethodInvoker)delegate()
                {
                    receiveAPP(m);
                });
            }
            else
                this.txtAPP.Text = m;
        }
        void receiveCAL(string m)
        {
            if (this.txtCAL.InvokeRequired == true)
            {
                this.txtCAL.Invoke((MethodInvoker)delegate()
                {
                    receiveCAL(m);
                });
            }
            else
                this.txtCAL.Text = m;
        }
        void receiveE2P(string m)
        {
            if (this.txtE2P.InvokeRequired == true)
            {
                this.txtE2P.Invoke((MethodInvoker)delegate()
                {
                    receiveE2P(m);
                });
            }
            else
                this.txtE2P.Text = m;
        }
        void receivePBL(string m)
        {
            if (this.txtPBL.InvokeRequired == true)
            {
                this.txtPBL.Invoke((MethodInvoker)delegate()
                {
                    receivePBL(m);
                });
            }
            else
                this.txtPBL.Text = m;
        }
        void receiveDIDF111(string m)
        {
            if (this.txtDIDF111Received.InvokeRequired == true)
            {
                this.txtDIDF111Received.Invoke((MethodInvoker)delegate()
                {
                    receiveDIDF111(m);
                });
            }
            else
                this.txtDIDF111Received.Text = m;
        }
        void receiveDIDF113(string m)
        {
            if (this.txtDIDF113Received.InvokeRequired == true)
            {
                this.txtDIDF113Received.Invoke((MethodInvoker)delegate()
                {
                    receiveDIDF113(m);
                });
            }
            else
                this.txtDIDF113Received.Text = m;
        }
        void receiveDIDF124(string m)
        {
            if (this.txtDIDF124Received.InvokeRequired == true)
            {
                this.txtDIDF124Received.Invoke((MethodInvoker)delegate()
                {
                    receiveDIDF124(m);
                });
            }
            else
                this.txtDIDF124Received.Text = m;
        }
        void receiveDIDF125(string m)
        {
            if (this.txtDIDF125Received.InvokeRequired == true)
            {
                this.txtDIDF125Received.Invoke((MethodInvoker)delegate()
                {
                    receiveDIDF125(m);
                });
            }
            else
                this.txtDIDF125Received.Text = m;
        }
        void receiveDIDF188(string m)
        {
            if (this.txtDIDF188Received.InvokeRequired == true)
            {
                this.txtDIDF188Received.Invoke((MethodInvoker)delegate()
                {
                    receiveDIDF188(m);
                });
            }
            else
                this.txtDIDF188Received.Text = m;
        }
        void updateDeviceResult(string m)
        {
            if (this.deviceResultLabel.InvokeRequired == true)
            {
                this.deviceResultLabel.Invoke((MethodInvoker)delegate()
                {
                    updateDeviceResult(m);
                });
            }
            else
                this.deviceResultLabel.Text = m;
        }
        void updateCANStatus(string m)
        {
            if (this.lblValueCANResult.InvokeRequired == true)
            {
                this.lblValueCANResult.Invoke((MethodInvoker)delegate()
                {
                    updateCANStatus(m);
                });
            }
            else
                this.lblValueCANResult.Text = m;
        }

        /// <summary>
        /// Checks to see if all gatherd information matches that declared in
        /// the setup form.
        /// </summary>
        void checkResults()
        {
            Console.WriteLine("check results called");
            bool f111 = false;
            bool f113 = false;
            bool f124 = false;
            bool f125 = false;
            bool f188 = false;
            bool app = false;
            bool pbl = false;
            bool e2p = false;
            bool cal = false;

            // Checking if app expected = received
            if (this.myForm.APP.Trim() == this.txtAPP.Text.Trim())
            {
                app = true;
                this.txtAPP.ForeColor = Color.Green;
            }
            else
            {
                app = false; 
                
                this.txtAPP.ForeColor = Color.Red;
            }

            // Checking if cal expected = received
            if (this.myForm.CAL.Trim() == this.txtCAL.Text.Trim())
            {
                cal = true;
                this.txtCAL.ForeColor = Color.Green;
            }
            else
            {
                cal = false;
                this.txtCAL.ForeColor = Color.Red;
            }

            // Checking if e2p expected = received
            if (this.myForm.E2P.Trim() == this.txtE2P.Text.Trim())
            {
                e2p = true;
                this.txtE2P.ForeColor = Color.Green;
            }
            else
            {
                e2p = false;
                this.txtE2P.ForeColor = Color.Red;
            }

            // Checking if pbl expected = received
            if (this.myForm.PBL.Trim() == this.txtPBL.Text.Trim())
            {
                pbl = true;
                this.txtPBL.ForeColor = Color.Green;
            }
            else
            {
                pbl = false;
                this.txtPBL.ForeColor = Color.Red;
            }

            // Checking if DID F111 expected = received
            if (txtDIDF111Expected.Text.Trim() == txtDIDF111Received.Text.Trim())
            {
                f111 = true;
                txtDIDF111Received.ForeColor = Color.Green;
            }
            else
            {
                f111 = false;
             
                txtDIDF111Received.ForeColor = Color.Red;
            }

            // Checking if DID F113 expected = received
            
            if (txtDIDF113Expected.Text.Trim() == txtDIDF113Received.Text.Trim())
            {
                f113 = true;
                txtDIDF113Received.ForeColor = Color.Green;
            }
            else
            {
                f113 = false; 
                txtDIDF113Received.ForeColor = Color.Red;
            
            } 

           // txtDIDF113Received.ForeColor = Color.Black;

            // Checking if DID F124 expected = received
            if (txtDIDF124Expected.Text.Trim() == txtDIDF124Received.Text.Trim())
            {
                f124 = true;
                txtDIDF124Received.ForeColor = Color.Green;
            }
            else
            {
                f124 = false;
              
                txtDIDF124Received.ForeColor = Color.Red;
            }

            // Checking if DID F125 expected = received
            if (txtDIDF125Expected.Text.Trim() == txtDIDF125Received.Text.Trim())
            {
                f125 = true;
                txtDIDF125Received.ForeColor = Color.Green;
            }
            else
            {
                f125 = false;
            
                txtDIDF125Received.ForeColor = Color.Red;
            }

            // Checking if DID F188 expected = received
            if (txtDIDF188Expected.Text.Trim() == txtDIDF188Received.Text.Trim())
            {
                f188 = true;
                txtDIDF188Received.ForeColor = Color.Green;
            }
            else
            {
                f188 = false;
      
                txtDIDF188Received.ForeColor = Color.Red;
            }


            // Final check to rate entire DID request sequence as pass/fail 
            // The below need to be changed
            if (myForm.RadioType == "LXF")
            {
                if (f111 && f113 && f124 && f125 && f188 && app && cal && pbl && e2p)
                {


                    myMessage.Result = "PASS";
                    updateDeviceResult("PASS");

                    //add a new value to passed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    passedDevices.Add(d);
                    deviceList.Add(d);

                    //updateLabels();
                }
                else
                {

                    //   timer2.Start();
                    myMessage.Result = "FAIL";
                    updateDeviceResult("FAIL");

                    //add a new value to failed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    failedDevices.Add(d);
                    deviceList.Add(d);

                    deviceFailed = true;

                    //updateLabels();

                }
            }
            else
            {
                if (f111 && f113 && f124 && f125 && f188)
                {

                    myMessage.Result = "PASS";
                    updateDeviceResult("PASS");

                    //add a new value to passed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    passedDevices.Add(d);
                    deviceList.Add(d);

                    //updateLabels();
                }
                else
                {

                    myMessage.Result = "FAIL";
                    //     timer2.Start();
                    updateDeviceResult("PASS");

                    //add a new value to failed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    failedDevices.Add(d);
                    deviceList.Add(d);

                    deviceFailed = true;

                    //updateLabels();
                }
            }
        }


        /// <summary>
        /// When a device is done being scanned, This method is called to add it to the excel spread sheet.
        /// </summary>
        void writeToExcel()
        {
            Excel.Application excelApp;
            Excel.Workbook excelWorkBook;
            Excel.Worksheet excelWorkSheet;
            Excel.Worksheet failedWorkSheet;

            Excel.Range chartRange; 

            // we must make sure we are saving for the correct version of excel
            Console.WriteLine(myForm.LogFile);
            string myPath = @"C:\DTC Checker\" + myForm.LogFile;
            object misValue = System.Reflection.Missing.Value;

            excelApp = new Excel.Application();

            // If file already exists then open it
            if (File.Exists(myPath))
            {
                // Open the Excel file and get worksheet
                excelWorkBook = excelApp.Workbooks.Open(myPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
                failedWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(2);


               

               

                // Determine the last used row.
                Excel.Range lastRow = null;
                int myRow = 0;
                myRow = excelWorkSheet.UsedRange.Count / 25 + 1;

                // Update Data
                // Assign column headers
                excelWorkSheet.Cells[myRow, 1] = (myRow - 5).ToString();
                excelWorkSheet.Cells[myRow, 2] = System.DateTime.Now.Date.ToShortDateString();
                excelWorkSheet.Cells[myRow, 3] = System.DateTime.Now.ToShortTimeString();
                excelWorkSheet.Cells[myRow, 4] = myMessage.SerialNumber;
                excelWorkSheet.Cells[myRow, 5] = this.txtDIDF111Expected.Text;
                excelWorkSheet.Cells[myRow, 6] = myMessage.DIDF111;
                excelWorkSheet.Cells[myRow, 7] = this.txtDIDF113Expected.Text;
                excelWorkSheet.Cells[myRow, 8] = myMessage.DIDF113;
                excelWorkSheet.Cells[myRow, 9] = this.txtDIDF124Expected.Text;
                excelWorkSheet.Cells[myRow, 10] = myMessage.DIDF124;
                excelWorkSheet.Cells[myRow, 11] = this.txtDIDF125Expected.Text;
                excelWorkSheet.Cells[myRow, 12] = myMessage.DIDF125;
                excelWorkSheet.Cells[myRow, 13] = this.txtDIDF188Expected.Text;
                excelWorkSheet.Cells[myRow, 14] = myMessage.DIDF188;
                excelWorkSheet.Cells[myRow, 15] = this.myForm.APP;
                excelWorkSheet.Cells[myRow, 16] = myMessage.APP;
                excelWorkSheet.Cells[myRow, 17] = this.myForm.PBL;
                excelWorkSheet.Cells[myRow, 18] = myMessage.PBL;
                excelWorkSheet.Cells[myRow, 19] = this.myForm.CAL;
                excelWorkSheet.Cells[myRow, 20] = myMessage.CAL;
                excelWorkSheet.Cells[myRow, 21] = this.myForm.E2P;
                excelWorkSheet.Cells[myRow, 22] = myMessage.E2P;
                excelWorkSheet.Cells[myRow, 23] = myMessage.DTCCount;

                for (int x = 0; x < DTCs.Count; x++)
                {
                    excelWorkSheet.Cells[myRow, 24] += DTCs[x];
                }
                // excelWorkSheet.Cells[myRow, 25] = this.lblResult1.Text + this.lblResult2.Text + this.lblResult3.Text + this.lblResult4.Text;

                if (duplicateDevice)
                {
                    duplicateDevice = false;
                    excelWorkSheet.Cells[myRow, 25] = "DUPLICATE";
                }
                else
                {
                    excelWorkSheet.Cells[myRow, 25] = this.deviceResultLabel.Text;
                }

                
                excelWorkSheet.Cells[myRow, 26] = this.dtcResultLabel.Text;

                if (deviceFailed)
                {
                    failedWorkSheet.Cells[myRow, 1] = (myRow - 2).ToString();
                    failedWorkSheet.Cells[myRow, 2] = System.DateTime.Now.Date.ToShortDateString();
                    failedWorkSheet.Cells[myRow, 3] = System.DateTime.Now.ToShortTimeString();
                    failedWorkSheet.Cells[myRow, 4] = myMessage.SerialNumber;
                    failedWorkSheet.Cells[myRow, 5] = this.txtDIDF111Expected.Text;
                    failedWorkSheet.Cells[myRow, 6] = myMessage.DIDF111;
                    failedWorkSheet.Cells[myRow, 7] = this.txtDIDF113Expected.Text;
                    failedWorkSheet.Cells[myRow, 8] = myMessage.DIDF113;
                    failedWorkSheet.Cells[myRow, 9] = this.txtDIDF124Expected.Text;
                    failedWorkSheet.Cells[myRow, 10] = myMessage.DIDF124;
                    failedWorkSheet.Cells[myRow, 11] = this.txtDIDF125Expected.Text;
                    failedWorkSheet.Cells[myRow, 12] = myMessage.DIDF125;
                    failedWorkSheet.Cells[myRow, 13] = this.txtDIDF188Expected.Text;
                    failedWorkSheet.Cells[myRow, 14] = myMessage.DIDF188;
                    failedWorkSheet.Cells[myRow, 15] = this.myForm.APP;
                    failedWorkSheet.Cells[myRow, 16] = myMessage.APP;
                    failedWorkSheet.Cells[myRow, 17] = this.myForm.PBL;
                    failedWorkSheet.Cells[myRow, 18] = myMessage.PBL;
                    failedWorkSheet.Cells[myRow, 19] = this.myForm.CAL;
                    failedWorkSheet.Cells[myRow, 20] = myMessage.CAL;
                    failedWorkSheet.Cells[myRow, 21] = this.myForm.E2P;
                    failedWorkSheet.Cells[myRow, 22] = myMessage.E2P;
                    failedWorkSheet.Cells[myRow, 23] = myMessage.DTCCount;
                    failedWorkSheet.Cells[myRow, 24] = DTCs.ToString();
                    failedWorkSheet.Cells[myRow, 25] = this.dtcResultLabel.Text;


                }

                DTCs.Clear();

                // Set border around row to thin continous line
                lastRow = excelWorkSheet.get_Range("a" + myRow.ToString(), "y" + myRow.ToString());
                lastRow.Borders.Weight = Excel.XlBorderWeight.xlThin;
                lastRow.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Use Autofit for formatting
                lastRow = excelWorkSheet.get_Range("a1", "a4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("b1", "b4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("c1", "c4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("d1", "d4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("e1", "e4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("f1", "f4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("g1", "g4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("h1", "h4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("i1", "i4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("j1", "j4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("k1", "k4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("l1", "l4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("m1", "m4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("n1", "n4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("o1", "o4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("p1", "p4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("q1", "q4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("r1", "r4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("s1", "s4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("t1", "t4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("u1", "u4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("v1", "v4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("w1", "w4");
                lastRow.EntireColumn.AutoFit();
                lastRow = excelWorkSheet.get_Range("x1", "x4");
                lastRow.EntireColumn.ColumnWidth = 25;
                lastRow = excelWorkSheet.get_Range("y1", "y4");
                lastRow.EntireColumn.AutoFit();

                //do the same but for the failed worksheet 
                // Set border around row to thin continous line 
                Excel.Range lastFailedRow = null;
                int myFailedRow = 0;
                myFailedRow = failedWorkSheet.UsedRange.Count / 25 + 1;

                lastFailedRow = failedWorkSheet.get_Range("a" + myFailedRow.ToString(), "y" + myFailedRow.ToString());
                lastFailedRow.Borders.Weight = Excel.XlBorderWeight.xlThin;
                lastFailedRow.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Use Autofit for formatting
                lastFailedRow = failedWorkSheet.get_Range("a1", "a4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("b1", "b4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("c1", "c4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("d1", "d4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("e1", "e4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("f1", "f4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("g1", "g4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("h1", "h4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("i1", "i4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("j1", "j4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("k1", "k4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("l1", "l4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("m1", "m4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("n1", "n4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("o1", "o4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("p1", "p4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("q1", "q4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("r1", "r4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("s1", "s4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("t1", "t4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("u1", "u4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("v1", "v4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("w1", "w4");
                lastFailedRow.EntireColumn.AutoFit();
                lastFailedRow = failedWorkSheet.get_Range("x1", "x4");
                lastFailedRow.EntireColumn.ColumnWidth = 25;
                lastFailedRow = failedWorkSheet.get_Range("y1", "y4");
                lastFailedRow.EntireColumn.AutoFit();

            }
            // If file does not already exist then create it and add headers
            else
            {
                myPath = @"C:\DTC Checker";
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(myPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create directory for Version Checker: " + ex.ToString());
                }
                finally { }

                Console.WriteLine("myfrom logfile " + myForm.LogFile);
                Console.WriteLine("ds logfile " + ds.LOGFILE);


                myPath = @"C:\DTC Checker\" + ds.LOGFILE; 
                    firstWrite = false;
                    Console.WriteLine("OPENING OLD FILE");
               

                try
                {
                    excelWorkBook = excelApp.Workbooks.Add(misValue);
                    excelWorkBook.SaveAs(myPath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
                    failedWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(2);


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

                    _year = myDate.Year.ToString().Substring(2, 2);


                    string date = _day + _month + _year;

                    //get the shift number from the radio button



                    if (ds.PACKAGEID != "")
                    {
                        excelWorkSheet.Cells[1, 1] = "PID PALLET " + ds.PACKAGEID;
                    }
                    else 
                    {
                        excelWorkSheet.Cells[1, 1] = "PID PALLET " + myForm.PACKAGE_ID;
                    }


                        excelWorkSheet.Cells[2, 1] = "Start Date: " + date;
                        

                        if (ds.OPERATORNAME != "")
                        {
                            excelWorkSheet.Cells[2, 3] = "Operator: " + ds.OPERATORNAME;
                            Console.WriteLine("DS OP: " + ds.OPERATORNAME); 
                        }
                        else
                        {
                            excelWorkSheet.Cells[2, 3] = "NO OP DATA";
                        }
                       

                        chartRange = excelWorkSheet.get_Range("a1", "y2");
                        chartRange.Font.Bold = true;
                        chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                        chartRange.HorizontalAlignment = 3;
                        chartRange.VerticalAlignment = 3;

                        chartRange = excelWorkSheet.get_Range("a1", "b1");
                        chartRange.Font.Size = 23;

                        chartRange = excelWorkSheet.get_Range("a3", "z3");
                        chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

                        for (int x = 0; x < myForm.DOC_LIST_PUBLIC.Count; x++)
                        {
                            excelWorkSheet.Cells[2, (x + 1)] = myForm.DOC_LIST_PUBLIC[x]; 

                        }

                   

                        chartRange = excelWorkSheet.get_Range("a1", "y2");
                        chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                        chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                        firstWrite = false;

                   
                }
                catch
                {
                    MessageBox.Show("Unable to start Excel");
                    return;
                }

                // Range object used to format Excel file
               // Excel.Range chartRange; 

                // Area before DIDs
                excelWorkSheet.get_Range("a4", "d4").Merge(false);

                // Make bold, center and color yellow
                chartRange = excelWorkSheet.get_Range("a4", "y5");
                chartRange.Font.Bold = true;
                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Freeze panes for easy viewing in log starting at row 2
               // chartRange.Application.ActiveWindow.SplitRow = 5; 
               // chartRange = excelWorkSheet.get_Range("a1", "y5");
              //  chartRange.Application.ActiveWindow.FreezePanes = false; ;

                // Set Border for header 1 cells
                chartRange = excelWorkSheet.get_Range("a4", "y4");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Set Border for header 2 cells
                chartRange = excelWorkSheet.get_Range("a5", "y5");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // DID F111
                excelWorkSheet.get_Range("e4", "f4").Merge(false);
                chartRange = excelWorkSheet.get_Range("e4", "f4");
                chartRange.FormulaR1C1 = "DID F111";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F113
                excelWorkSheet.get_Range("g4", "h4").Merge(false);
                chartRange = excelWorkSheet.get_Range("g4", "h4");
                chartRange.FormulaR1C1 = "DID F113";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F124
                excelWorkSheet.get_Range("i4", "j4").Merge(false);
                chartRange = excelWorkSheet.get_Range("i4", "j4");
                chartRange.FormulaR1C1 = "DID F124";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F125
                excelWorkSheet.get_Range("k4", "l4").Merge(false);
                chartRange = excelWorkSheet.get_Range("k4", "l4");
                if (myForm.RadioType == "LXF")
                    chartRange.FormulaR1C1 = "DID F125";
                else
                    chartRange.FormulaR1C1 = "DID F110";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F188
                excelWorkSheet.get_Range("m4", "n4").Merge(false);
                chartRange = excelWorkSheet.get_Range("m4", "n4");
                chartRange.FormulaR1C1 = "DID F188";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // APP
                excelWorkSheet.get_Range("o4", "p4").Merge(false);
                chartRange = excelWorkSheet.get_Range("o4", "p4");
                chartRange.FormulaR1C1 = "APP";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // PBL
                excelWorkSheet.get_Range("q4", "r4").Merge(false);
                chartRange = excelWorkSheet.get_Range("q4", "r4");
                chartRange.FormulaR1C1 = "PBL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // CAL
                excelWorkSheet.get_Range("s4", "t4").Merge(false);
                chartRange = excelWorkSheet.get_Range("s4", "t4");
                chartRange.FormulaR1C1 = "CAL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // E2P
                excelWorkSheet.get_Range("u4", "v4").Merge(false);
                chartRange = excelWorkSheet.get_Range("u4", "v4");
                chartRange.FormulaR1C1 = "E2P";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DTCs
                excelWorkSheet.get_Range("w4", "x4").Merge(false);
                chartRange = excelWorkSheet.get_Range("w4", "x4");
                chartRange.FormulaR1C1 = "DTCs";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Assign column headers
                excelWorkSheet.Cells[5, 1] = "Line Item";
                excelWorkSheet.Cells[5, 2] = "Date";
                excelWorkSheet.Cells[5, 3] = "Time";
                excelWorkSheet.Cells[5, 4] = "Serial Number";
                excelWorkSheet.Cells[5, 5] = "Expected";
                excelWorkSheet.Cells[5, 6] = "Received";
                excelWorkSheet.Cells[5, 7] = "Expected";
                excelWorkSheet.Cells[5, 8] = "Received";
                excelWorkSheet.Cells[5, 9] = "Expected";
                excelWorkSheet.Cells[5, 10] = "Received";
                excelWorkSheet.Cells[5, 11] = "Expected";
                excelWorkSheet.Cells[5, 12] = "Received";
                excelWorkSheet.Cells[5, 13] = "Expected";
                excelWorkSheet.Cells[5, 14] = "Received";
                excelWorkSheet.Cells[5, 15] = "Expected";
                excelWorkSheet.Cells[5, 16] = "Received";
                excelWorkSheet.Cells[5, 17] = "Expected";
                excelWorkSheet.Cells[5, 18] = "Received";
                excelWorkSheet.Cells[5, 19] = "Expected";
                excelWorkSheet.Cells[5, 20] = "Received";
                excelWorkSheet.Cells[5, 21] = "Expected";
                excelWorkSheet.Cells[5, 22] = "Received";
                excelWorkSheet.Cells[5, 23] = "Count";
                excelWorkSheet.Cells[5, 24] = "List";
                excelWorkSheet.Cells[5, 25] = "Device Result";
                excelWorkSheet.Cells[5, 26] = "DTC Result";


                // Use Autofit for formatting
                chartRange = excelWorkSheet.get_Range("a1", "a4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("b1", "b4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("c1", "c4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("d1", "d4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("e1", "e4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("f1", "f4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("g1", "g4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("h1", "h4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("i1", "i4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("j1", "j4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("k1", "k4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("l1", "l4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("m1", "m4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("n1", "n4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("o1", "o4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("p1", "p4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("q1", "q4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("r1", "r4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("s1", "s4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("t1", "t4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("u1", "u4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("v1", "v4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("w1", "w4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = excelWorkSheet.get_Range("x1", "x4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = excelWorkSheet.get_Range("y1", "y4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("z1", "z4");
                chartRange.EntireColumn.AutoFit();


                // Set thick border around entire header
              //  chartRange = excelWorkSheet.get_Range("a1", "y2");
            //    chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                // Update Data
                // Assign column headers
                excelWorkSheet.Cells[6, 1] = "1";
                excelWorkSheet.Cells[6, 2] = System.DateTime.Now.Date.ToShortDateString();
                excelWorkSheet.Cells[6, 3] = System.DateTime.Now.ToShortTimeString();
                excelWorkSheet.Cells[6, 4] = myMessage.SerialNumber;
                excelWorkSheet.Cells[6, 5] = this.txtDIDF111Expected.Text;
                excelWorkSheet.Cells[6, 6] = myMessage.DIDF111;
                excelWorkSheet.Cells[6, 7] = this.txtDIDF113Expected.Text;
                excelWorkSheet.Cells[6, 8] = myMessage.DIDF113;
                excelWorkSheet.Cells[6, 9] = this.txtDIDF124Expected.Text;
                excelWorkSheet.Cells[6, 10] = myMessage.DIDF124;
                excelWorkSheet.Cells[6, 11] = this.txtDIDF125Expected.Text;
                excelWorkSheet.Cells[6, 12] = myMessage.DIDF125;
                excelWorkSheet.Cells[6, 13] = this.txtDIDF188Expected.Text;
                excelWorkSheet.Cells[6, 14] = myMessage.DIDF188;
                excelWorkSheet.Cells[6, 15] = this.myForm.APP;
                excelWorkSheet.Cells[6, 16] = myMessage.APP;
                excelWorkSheet.Cells[6, 17] = this.myForm.PBL;
                excelWorkSheet.Cells[6, 18] = myMessage.PBL;
                excelWorkSheet.Cells[6, 19] = this.myForm.CAL;
                excelWorkSheet.Cells[6, 20] = myMessage.CAL;
                excelWorkSheet.Cells[6, 21] = this.myForm.E2P;
                excelWorkSheet.Cells[6, 22] = myMessage.E2P;
                excelWorkSheet.Cells[6, 23] = myMessage.DTCCount;
                excelWorkSheet.Cells[6, 24] = myMessage.DTC;
                // excelWorkSheet.Cells[3, 25] = this.lblResult1.Text + this.lblResult2.Text + this.lblResult3.Text + this.lblResult4.Text;
                excelWorkSheet.Cells[6, 25] = this.deviceResultLabel.Text;
                excelWorkSheet.Cells[6, 26] = this.dtcResultLabel.Text; 
                
                // Set border around row to thin continous line
                chartRange = excelWorkSheet.get_Range("a3", "z3");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Use Autofit for formatting
                chartRange = excelWorkSheet.get_Range("a1", "a4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("b1", "b4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("c1", "c4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("d1", "d4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("e1", "e4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("f1", "f4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("g1", "g4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("h1", "h4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("i1", "i4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("j1", "j4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("k1", "k4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("l1", "l4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("m1", "m4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("n1", "n4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("o1", "o4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("p1", "p4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("q1", "q4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("r1", "r4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("s1", "s4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("t1", "t4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("u1", "u4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("v1", "v4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("w1", "w4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("x1", "x4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = excelWorkSheet.get_Range("y1", "y4");
                chartRange.EntireColumn.AutoFit();
                chartRange = excelWorkSheet.get_Range("z1", "z4");
                chartRange.EntireColumn.AutoFit();

        #endregion NormalWorksheet

                #region FailedWorksheet



                // Make bold, center and color yellow
                chartRange = failedWorkSheet.get_Range("a1", "z2");
                chartRange.Font.Bold = true;
                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                chartRange = failedWorkSheet.get_Range("a1", "d1");
                chartRange.Font.Bold = true;
                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Freeze panes for easy viewing in log starting at row 2
                chartRange.Application.ActiveWindow.SplitRow = 2;
                chartRange.Application.ActiveWindow.FreezePanes = true;

                // Set Border for header 1 cells
                chartRange = failedWorkSheet.get_Range("a1", "y1");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Set Border for header 2 cells
                chartRange = failedWorkSheet.get_Range("a2", "y2");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Area before DIDs
                failedWorkSheet.get_Range("a1", "d1").Merge(false);
                chartRange = failedWorkSheet.get_Range("a1", "d1");
                chartRange.FormulaR1C1 = "FAILED UNITS";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // DID F111
                failedWorkSheet.get_Range("e1", "f1").Merge(false);
                chartRange = failedWorkSheet.get_Range("e1", "f1");
                chartRange.FormulaR1C1 = "DID F111";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F113
                failedWorkSheet.get_Range("g1", "h1").Merge(false);
                chartRange = failedWorkSheet.get_Range("g1", "h1");
                chartRange.FormulaR1C1 = "DID F113";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F124
                failedWorkSheet.get_Range("i1", "j1").Merge(false);
                chartRange = failedWorkSheet.get_Range("i1", "j1");
                chartRange.FormulaR1C1 = "DID F124";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F125
                failedWorkSheet.get_Range("k1", "l1").Merge(false);
                chartRange = failedWorkSheet.get_Range("k1", "l1");
                if (myForm.RadioType == "LXF")
                    chartRange.FormulaR1C1 = "DID F125";
                else
                    chartRange.FormulaR1C1 = "DID F110";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F188
                failedWorkSheet.get_Range("m1", "n1").Merge(false);
                chartRange = failedWorkSheet.get_Range("m1", "n1");
                chartRange.FormulaR1C1 = "DID F188";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // APP
                failedWorkSheet.get_Range("o1", "p1").Merge(false);
                chartRange = failedWorkSheet.get_Range("o1", "p1");
                chartRange.FormulaR1C1 = "APP";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // PBL
                failedWorkSheet.get_Range("q1", "r1").Merge(false);
                chartRange = failedWorkSheet.get_Range("q1", "r1");
                chartRange.FormulaR1C1 = "PBL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // CAL
                failedWorkSheet.get_Range("s1", "t1").Merge(false);
                chartRange = failedWorkSheet.get_Range("s1", "t1");
                chartRange.FormulaR1C1 = "CAL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // E2P
                failedWorkSheet.get_Range("u1", "v1").Merge(false);
                chartRange = failedWorkSheet.get_Range("u1", "v1");
                chartRange.FormulaR1C1 = "E2P";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DTCs
                failedWorkSheet.get_Range("w1", "x1").Merge(false);
                chartRange = failedWorkSheet.get_Range("w1", "x1");
                chartRange.FormulaR1C1 = "DTCs";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Assign column headers 
                // failedWorkSheet.Cells[1, 1] = "FAILED UNITS";
                failedWorkSheet.Cells[2, 1] = "Line Item";
                failedWorkSheet.Cells[2, 2] = "Date";
                failedWorkSheet.Cells[2, 3] = "Time";
                failedWorkSheet.Cells[2, 4] = "Serial Number";
                failedWorkSheet.Cells[2, 5] = "Expected";
                failedWorkSheet.Cells[2, 6] = "Received";
                failedWorkSheet.Cells[2, 7] = "Expected";
                failedWorkSheet.Cells[2, 8] = "Received";
                failedWorkSheet.Cells[2, 9] = "Expected";
                failedWorkSheet.Cells[2, 10] = "Received";
                failedWorkSheet.Cells[2, 11] = "Expected";
                failedWorkSheet.Cells[2, 12] = "Received";
                failedWorkSheet.Cells[2, 13] = "Expected";
                failedWorkSheet.Cells[2, 14] = "Received";
                failedWorkSheet.Cells[2, 15] = "Expected";
                failedWorkSheet.Cells[2, 16] = "Received";
                failedWorkSheet.Cells[2, 17] = "Expected";
                failedWorkSheet.Cells[2, 18] = "Received";
                failedWorkSheet.Cells[2, 19] = "Expected";
                failedWorkSheet.Cells[2, 20] = "Received";
                failedWorkSheet.Cells[2, 21] = "Expected";
                failedWorkSheet.Cells[2, 22] = "Received";
                failedWorkSheet.Cells[2, 23] = "Count";
                failedWorkSheet.Cells[2, 24] = "List";
                failedWorkSheet.Cells[2, 25] = "DTC Result";
                failedWorkSheet.Cells[2, 26] = "Device Result";


                // Use Autofit for formatting
                chartRange = failedWorkSheet.get_Range("a1", "a4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("b1", "b4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("c1", "c4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("d1", "d4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("e1", "e4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("f1", "f4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("g1", "g4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("h1", "h4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("i1", "i4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("j1", "j4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("k1", "k4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("l1", "l4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("m1", "m4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("n1", "n4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("o1", "o4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("p1", "p4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("q1", "q4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("r1", "r4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("s1", "s4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("t1", "t4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("u1", "u4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("v1", "v4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("w1", "w4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = failedWorkSheet.get_Range("x1", "x4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = failedWorkSheet.get_Range("y1", "y4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("z1", "z4");
                chartRange.EntireColumn.AutoFit();


                // Set thick border around entire header
                chartRange = failedWorkSheet.get_Range("a1", "y2");
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                // Update Data
                // Assign column headers
                //failedWorkSheet.Cells[1, 1] = "FAILED DEVICES"; 

                if (deviceFailed)
                {
                    failedWorkSheet.Cells[3, 1] = "1";
                    failedWorkSheet.Cells[3, 2] = System.DateTime.Now.Date.ToShortDateString();
                    failedWorkSheet.Cells[3, 3] = System.DateTime.Now.ToShortTimeString();
                    failedWorkSheet.Cells[3, 4] = myMessage.SerialNumber;
                    failedWorkSheet.Cells[3, 5] = this.txtDIDF111Expected.Text;
                    failedWorkSheet.Cells[3, 6] = myMessage.DIDF111;
                    failedWorkSheet.Cells[3, 7] = this.txtDIDF113Expected.Text;
                    failedWorkSheet.Cells[3, 8] = myMessage.DIDF113;
                    failedWorkSheet.Cells[3, 9] = this.txtDIDF124Expected.Text;
                    failedWorkSheet.Cells[3, 10] = myMessage.DIDF124;
                    failedWorkSheet.Cells[3, 11] = this.txtDIDF125Expected.Text;
                    failedWorkSheet.Cells[3, 12] = myMessage.DIDF125;
                    failedWorkSheet.Cells[3, 13] = this.txtDIDF188Expected.Text;
                    failedWorkSheet.Cells[3, 14] = myMessage.DIDF188;
                    failedWorkSheet.Cells[3, 15] = this.myForm.APP;
                    failedWorkSheet.Cells[3, 16] = myMessage.APP;
                    failedWorkSheet.Cells[3, 17] = this.myForm.PBL;
                    failedWorkSheet.Cells[3, 18] = myMessage.PBL;
                    failedWorkSheet.Cells[3, 19] = this.myForm.CAL;
                    failedWorkSheet.Cells[3, 20] = myMessage.CAL;
                    failedWorkSheet.Cells[3, 21] = this.myForm.E2P;
                    failedWorkSheet.Cells[3, 22] = myMessage.E2P;
                    failedWorkSheet.Cells[3, 23] = myMessage.DTCCount;
                    failedWorkSheet.Cells[3, 24] = myMessage.DTC;
                    failedWorkSheet.Cells[3, 25] = this.dtcResultLabel.Text;
                    failedWorkSheet.Cells[3, 26] = this.deviceResultLabel.Text; 
                    
                }
                // failedWorkSheet.Cells[3, 25] = this.deviceResultLabel.Text;
                // Set border around row to thin continous line
                chartRange = failedWorkSheet.get_Range("a3", "y3");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                // Use Autofit for formatting
                chartRange = failedWorkSheet.get_Range("a1", "a4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("b1", "b4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("c1", "c4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("d1", "d4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("e1", "e4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("f1", "f4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("g1", "g4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("h1", "h4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("i1", "i4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("j1", "j4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("k1", "k4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("l1", "l4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("m1", "m4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("n1", "n4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("o1", "o4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("p1", "p4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("q1", "q4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("r1", "r4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("s1", "s4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("t1", "t4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("u1", "u4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("v1", "v4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("w1", "w4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("x1", "x4");
                chartRange.EntireColumn.ColumnWidth = 25;
                chartRange = failedWorkSheet.get_Range("y1", "y4");
                chartRange.EntireColumn.AutoFit();
                chartRange = failedWorkSheet.get_Range("z1", "z4");
                chartRange.EntireColumn.AutoFit();

                #endregion FailedWorksheet


            }

            //excelApp.Visible = true; 
            try
            {
                excelWorkBook.Save();
            }
            catch
            {
                MessageBox.Show("Workbook not saved");
            }
            excelWorkBook.Save();
            excelWorkBook.Close(true, misValue, misValue);
            excelApp.Quit();

            releaseObject(excelApp);
            releaseObject(excelWorkBook);
            releaseObject(excelWorkSheet);
        }

        /// <summary>
        /// Matches a DTC with its proper description.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        string commentDTCs(string m)
        {
            #region Config
            // Initial config not with 0x0A status
            if (m.Substring(0, 6) == "E10000")
            {
                return m + " - Initial config not complete.";
            }
            // Misconfig with 0x0A status
            else if (m.Substring(0, 6) == "E10100")
            {
                return m + " - Misconfiguration.";
            }
            #endregion

            #region GPS Antenna
            // GPS Antenna Open 0x0A status
            else if (m.Substring(0, 6) == "919F13")
            {
                return m + " - GPS antenna circuit open.";
            }
            // GPS Antenna Electrial Failure 0x0A status
            else if (m.Substring(0, 6) == "919F01")
            {
                return m + " - GPS antenna electrical failure.";
            }
            #endregion

            #region SDARS Antenna
            // SDARS Antenna Open 0x0A status
            else if (m.Substring(0, 6) == "9A8913")
            {
                return m + " - SDARS antenna circuit open.";
            }

            // SDARS Antenna Electrical Failure 0x0A status
            else if (m.Substring(0, 6) == "9A8901")
            {
                return m + " - SDARS antenna electrical failure.";
            }
            #endregion

            #region Display
            // Display Touch Screen 0x0A status
            else if (m.Substring(0, 6) == "908E63")
            {
                return m + " - Display touch screen stuck.";
            }
            #endregion

            #region HDD
            // Hard Disk Drive Failure 0x0A status
            else if (m.Substring(0, 6) == "921C01")
            {
                return m = " - Hard disk drive failure.";
            }
            #endregion

            #region DVD Mech
            // DVD Mech Electrical Failure 0x0A status
            else if (m.Substring(0, 6) == "9D1949")
            {
                return m + " - DVD mechanism electrical failure.";
            }
            // DVD Mech Over Temp Failure 0x0A status
            else if (m.Substring(0, 6) == "9D194B")
            {
                return m + " - DVD mechanism over temperature.";
            }
            #endregion

            #region AUX
            // Aux Input Circuit Open Failure 0x0A status
            else if (m.Substring(0, 6) == "9D7813")
            {
                return m + " - AUX input circuit open.";
            }
            #endregion

            #region Bezel
            // Front Bezel Stuck Button Failure 0x0A status
            else if (m.Substring(0, 6) == "E01363")
            {
                return m + " - Front bezel stuck button.";
            }
            #endregion

            #region Battery Voltage
            // Battery Voltage Above Threshold 0x0A status
            else if (m.Substring(0, 6) == "F00317")
            {
                return m + " - Battery voltage above threshold.";
            }
            // Battery Voltage Below Threshold 0x0A status
            else if (m.Substring(0, 6) == "F00316")
            {
                return m + " - Battery voltage below threshold.";
            }
            #endregion

            #region Lost Comms
            // Lost Communication with Body Control Module 0x0A status
            else if (m.Substring(0, 6) == "C14000")
            {
                return m + " - Lost Comms body control module.";
            }

            // Lost Communication with IPC 0x0A status
            else if (m.Substring(0, 6) == "C15500")
            {
                return m + " - Lost Comms IPC.";
            }

            // Lost Communication with PAM 0x0A status
            else if (m.Substring(0, 6) == "C15900")
            {
                return m + " - Lost Comms PAM.";
            }

            // Lost Communication with Display 0x0A status
            else if (m.Substring(0, 6) == "C16200")
            {
                return m + " - Lost Comms Display.";
            }

            // Lost Communication with HVAC 0x0A status
            else if (m.Substring(0, 6) == "C16400")
            {
                return m + " - Lost Comms HVAC.";
            }

            // Lost Communication with External SDARS 0x0A status
            else if (m.Substring(0, 6) == "C19300")
            {
                return m + " - Lost Comms External SDARS.";
            }

            // Lost Communication with FES 0x0A status
            else if (m.Substring(0, 6) == "C19600")
            {
                return m + " - Lost Comms with FES.";
            }

            // Lost Communication with CPM 0x0A status
            else if (m.Substring(0, 6) == "C19700")
            {
                return m + " - Lost Comms with CPM.";
            }

            // Lost Communication with DSP AMP 0x0A status
            else if (m.Substring(0, 6) == "C23800")
            {
                return m + " - Lost Comms with DSP AMP.";
            }

            // Lost Communication with RSEM 0x0A status
            else if (m.Substring(0, 6) == "C24900")
            {
                return m + " - Lost Comms with RSEM.";
            }

            // Lost Communication with CID 0x0A status
            else if (m.Substring(0, 6) == "C25500")
            {
                return m + " - Lost Comms with CID.";
            }

            // Lost Communication with FCIM 0x0A status
            else if (m.Substring(0, 6) == "C25600")
            {
                return m + " - Lost Comms with FCIM.";
            }

            // Lost Communication with ACM 0x0A status
            else if (m.Substring(0, 6) == "C18400")
            {
                return m + " - Lost Comms with ACM.";
            }

            #endregion

            #region GYRO
            // Gyro Hardware Component Failure 0x0A status
            else if (m.Substring(0, 6) == "E01409")
            {
                return m + " - Gyro hardware component failure.";
            }

            #endregion

            #region Flash Checksum
            // Flash Checksum Failure 0x0A status
            else if (m.Substring(0, 6) == "E01441")
            {
                return m + " - Flash checksum failure.";
            }
            // Control Module General Memory Failure 0x0A status
            else if (m.Substring(0, 6) == "E01442")
            {
                return m + " - Control Module General Memory Failure.";
            }
            // Control Module Component Internal Failure 0x0A status
            else if (m.Substring(0, 6) == "E01496")
            {
                return m + " - Control Module Component Internal Failure.";
            }
            #endregion

            #region SDARS
            // SDARS Component Hardware Failure 0x0A status
            else if (m.Substring(0, 6) == "E01496")
            {
                return m + " - SDARS component hardware failure.";
            }

            #endregion

            #region Calibration File
            // Calibration File Missing Failure 0x0A status
            else if (m.Substring(0, 6) == "E01A51")
            {
                return m + " - Calibration file missing.";
            }

            #endregion

            #region HDD FS-INFO
            // HDD FS-Info Failure 0x0A status
            else if (m.Substring(0, 6) == "F00045")
            {
                return m + " - HDD FS-Info corrupt.";
            }

            #endregion

            #region Rear Camera
            // Rear Camera No Signal Failure 0x0A status
            else if (m.Substring(0, 6) == "500101")
            {
                return m + " - Rear camera no signal.";
            }

            #endregion

            #region Steering Wheel Controls
            // SWC1 Voltage Out of Range Failure 0x0A status
            else if (m.Substring(0, 6) == "91BA1C")
            {
                return m + " - SWC1 voltage out of range.";
            }

            // SWC2 Voltage Out of Range Failure 0x0A status
            else if (m.Substring(0, 6) == "92011C")
            {
                return m + " - SWC2 voltage out of range.";
            }

            // SWC1 Stuck Button Failure 0x0A status
            else if (m.Substring(0, 6) == "91BA63")
            {
                return m + " - SWC1 stuck button.";
            }

            // SWC2 Stuck Button Failure 0x0A status
            else if (m.Substring(0, 6) == "920163")
            {
                return m + " - SWC2 stuck button.";
            }

            #endregion

            #region RPA
            // Rear Park Aide audio input circuit short to battery Failure 0x0A status
            else if (m.Substring(0, 6) == "91BB12")
            {
                return m + " - Rear park aide audio input circuit short to battery.";
            }

            #endregion

            #region Speakers
            // Speaker #1 General Electric Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0101")
            {
                return m + " - Speaker #1 General Electric Failure.";
            }
            // Speaker #1 Short to Ground Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0111")
            {
                return m + " - Speaker #1 Short to Ground Failure";
            }
            // Speaker #1 Short to Battery Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0112")
            {
                return m + " - Speaker #1 Short to Battery Failure";
            }
            // Speaker #1 Open Circuit Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0113")
            {
                return m + " - Speaker #1 Open Circuit Failure";
            }
            // Speaker #2 General Electric Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0201")
            {
                return m + " - Speaker #2 General Electric Failure.";
            }
            // Speaker #2 Short to Ground Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0211")
            {
                return m + " - Speaker #2 Short to Ground Failure";
            }
            // Speaker #2 Short to Battery Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0212")
            {
                return m + " - Speaker #2 Short to Battery Failure";
            }
            // Speaker #2 Open Circuit Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0213")
            {
                return m + " - Speaker #2 Open Circuit Failure";
            }
            // Speaker #3 General Electric Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0301")
            {
                return m + " - Speaker #3 General Electric Failure.";
            }
            // Speaker #3 Short to Ground Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0311")
            {
                return m + " - Speaker #3 Short to Ground Failure";
            }
            // Speaker #3 Short to Battery Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0312")
            {
                return m + " - Speaker #3 Short to Battery Failure";
            }
            // Speaker #3 Open Circuit Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0313")
            {
                return m + " - Speaker #3 Open Circuit Failure";
            }
            // Speaker #4 General Electric Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0401")
            {
                return m + " - Speaker #4 General Electric Failure.";
            }
            // Speaker #4 Short to Ground Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0411")
            {
                return m + " - Speaker #3 Short to Ground Failure";
            }
            // Speaker #4 Short to Battery Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0412")
            {
                return m + " - Speaker #4 Short to Battery Failure";
            }
            // Speaker #4 Open Circuit Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0413")
            {
                return m + " - Speaker #4 Open Circuit Failure";
            }
            // Speaker #5 General Signal Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0502")
            {
                return m + " - Speaker #5 General Signal Failure";
            }
            // Speaker #6 General Signal Failure 0x0A status
            else if (m.Substring(0, 6) == "9A0602")
            {
                return m + " - Speaker #6 General Signal Failure";
            }

            #endregion

            #region Antenna Test
            // Antenna signal amplitude below minimun value failure 0x0A status
            else if (m.Substring(0, 6) == "9A5621")
            {
                return m + " - Antenna signal amplitude below minimun value failure.";
            }

            #endregion

            #region Vehicle Speed
            // Vehicle speed circuit short to battery failure 0x0A status
            else if (m.Substring(0, 6) == "E00512")
            {
                return m + " - Vehicle speed circuit short to battery failure.";
            }

            #endregion

            #region Added in V1.5
            else if (m.Substring(0, 6) == "9A8911")
            {
                return m + " - Satellite Antenna Circuit Short to Ground";
            }
            else if (m.Substring(0, 6) == "9A5621")
            {
                return m + " - Antenna Signal Amplitude < Minimum";
            }
            else if (m.Substring(0, 6) == "9A8913")
            {
                return m + " - Satellite Antenna Circuit Open";
            }
            else if (m.Substring(0, 6) == "9D1949")
            {
                return m + " - Compact Disk Unit Internal Electronic Failure";
            }
          
            else if (m.Substring(0, 6) == "9D1949")
            {
                return m + " - Compact Disk Unit Internal Electronic Failure";
            }
            else if (m.Substring(0, 6) == "9A0113")
            {
                return m + " - Speaker #1 Circuit Open";
            }
            else if (m.Substring(0, 6) == "9A0213")
            {
                return m + " - Speaker #2 Circuit Open";
            }
            else if (m.Substring(0, 6) == "9A0313")
            {
                return m + " - Speaker #3 Circuit Open";
            }
            else if (m.Substring(0, 6) == "9A0413")
            {
                return m + " - Speaker #4 Circuit Open";
            }
            else if (m.Substring(0, 6) == "9A0513")
            {
                return m + " - Speaker #5 Circuit Open";
            }
            else if (m.Substring(0, 6) == "9A0613")
            {
                return m + " - Speaker #6 Circuit Open";
            }
            else if (m.Substring(0, 6) == "C15500")
            {
                return m + " - Lost Communication with IPC";
            }
            else if (m.Substring(0, 6) == "C25600")
            {
                return m + " - Lost Communication with front controls interface module A.";
            }
            else if (m.Substring(0, 6) == "9D194B")
            {
                return m + " - Compact disk unit over temperature.";
            }
            else if (m.Substring(0, 6) == "C23700")
            {
                return m + " - Lost Comms with Digital Audio Control Module C";
            }
            else if (m.Substring(0, 6) == "C23800")
            {
                return m + " - Lost Comms with Digital Audio Control Module D";
            }
            else if (m.Substring(0, 6) == "C25300")
            {
                return m + " - Lost Comms with accessory protoal interface module.";
            }
            else if (m.Substring(0, 6) == "C23700")
            {
                return m + " - Lost Comms with Digital Audio Control Module C";
            }
            else if (m.Substring(0, 6) == "C23600")
            {
                return m + " - Lost Comms with Digital Audio Control Module A";
            }
            else if (m.Substring(0, 6) == "C25700")
            {
                return m + " - Lost Comms with Front Controls / Display Interface Module No Sub";
            }
            else if (m.Substring(0, 6) == "E01A51")
            {
                return m + " - Control Module Main Calibration Data Not Programmed";
            }
            else if (m.Substring(0, 6) == "F00041")
            {
                return m + " - Control Module General Checksun Failure";
            }
            else if (m.Substring(0, 6) == "F00042")
            {
                return m + " - Control Module General Memory Failure";
            }
            else if (m.Substring(0, 6) == "F00096")
            {
                return m + " - Control Module Component Internal Failure";
            }
            else if (m.Substring(0, 6) == "F00316")
            {
                return m + " - Battery Voltage Circuit Voltage Below Threshold";
            }
            else if (m.Substring(0, 6) == "C23800")
            {
                return m + " - Battery Volate Circuit Voltage Above Threshold";
            }




            #endregion



            else
                return m;
        }
        static private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (NullReferenceException ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the COM object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }

        }

        public void resetPalletInformation()
        {
            updateReadyLabel("READY");

            this.passedQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.passedQuantityLabel.Text = "PASS: " + passedQuantity;
            });

        

            this.duplicateQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.duplicateQuantityLabel.Text = "DUP.: " + duplicateCounter;
            });

            this.failedQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.failedQuantityLabel.Text = "FAIL: " + failedQuantity;
            });



            this.progressBar2.BeginInvoke((MethodInvoker)delegate
             {

                 this.progressBar2.Value = 0;
             }); 

        }

        #region Events

        void OnSetupClose(object source)
        {
            if (myForm.setupDone)
            {
                resetPalletInformation();
            }
            setupIsOpen = false;

        }

        void OnIgnoreClose(object source)
        {
            ignoredDTCs = ignoreForm.ignoredDtcs; 
        }

        void OnNewOperator(object source)
        {
            ds.OPERATORNAME = nof.operatorName;
            this.Text = "Version Checker (" + Application.ProductVersion +
               " | Windows 7 Edition) - Operator: " + ds.OPERATORNAME;
        }


        //Add more of these
       //DIDs
        void OnDIDF111Change(object source)
        {
            this.txtDIDF111Expected.Text = myForm.DIDF111;
            ds.F111EXPECTED = myForm.DIDF111;
        }
        void OnDIDF124Change(object source)
        {
            this.txtDIDF124Expected.Text = myForm.DIDF124;
            ds.F124EXPECTED = myForm.DIDF124;
        }
        void OnDIDF125Change(object source)
        {
            this.txtDIDF125Expected.Text = myForm.DIDF125;
            ds.F125EXPECTED = myForm.DIDF125;
        }
        void OnDIDF188Change(object source)
        {
            this.txtDIDF188Expected.Text = myForm.DIDF188;
            ds.F188EXPECTED = myForm.DIDF188;
        }
        void OnDIDF113Change(object source)
        {
            this.txtDIDF113Expected.Text = myForm.DIDF113;
            ds.F113EXPECTED = myForm.DIDF113;
        } 

        //PARTS 
        void OnAPLChange(object source)
        {
            this.txtAPLExpected.Text = myForm.APP;
            ds.APLEXPECTED = myForm.APP;
        }

        void OnE2PChange(object source)
        {
            this.txtE2PExpected.Text = myForm.E2P;
            ds.E2PEXPECTED = myForm.E2P;
        }

        void OnCALChange(object source)
        {
            this.txtCALExpected.Text = myForm.CAL;
            ds.CALEXPECTED = myForm.CAL;
        }

        void OnPBLChange(object source)
        {
            this.txtPBLExpected.Text = myForm.PBL;
            ds.PBL = myForm.PBL;
        }

        void OnSBLChange(object source)
        {
            ds.SBL = myForm.sbl;
        }

        void OnDIDF110Change(object source)
        {
            ds.F110 = myForm.f110;
        }

        void OnDocListChange(object source)
        {
            ds.DOCLIST = myForm.docList;
        }

        void OnPackageIDChange(object source)
        {
            ds.PACKAGEID = myForm.packageId;
        }

        void OnQuantityChange(object source)
        {
            try
            {
                ds.QUANTITY = Convert.ToInt32(myForm.quantity);
            }
            catch
            {

                ds.QUANTITY = 0;
            }
        }
        void OnOperatorChange(object source)
        {
            ds.OPERATORNAME = myForm.opName;
        }
        void OnBenchChange(object source)
        {
            ds.BENCH = myForm.bench;
        }
        void OnLogFileChange(object source)
        {
            ds.LOGFILE = myForm.LogFile;
        }
        void OnSetupOpen(object source)
        {
            setupIsOpen = true;

        }


       
        

        void OnBaudRateChange(object source)
        {
            byte[] bConfigBytes = new byte[1024];
            int iNumBytes = 0;
            int lResult = 0;

            lResult = IcsNeoDll.icsneoGetConfiguration(m_hObject, ref bConfigBytes[0], ref iNumBytes);

            if (myForm.RadioType == "LXF")
            {
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF1)] = 0x01;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF2)] = 0xB8;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF3)] = 0x05;
                lNetworkID = 1;

                //Set the filter up 
                stFilter.Header = convertFromHex("72F");
                stFilter.HeaderMask = convertFromHex("FFF");

                //Set the Flow Control Frame Properties
                stMsg.ArbIDOrHeader = convertFromHex("727");
                stMsg.NumberBytesData = 8;
                stMsg.StatusBitField = 2;
                stMsg.Data1 = Convert.ToByte(convertFromHex("30"));    //flow control frame
                stMsg.Data2 = 0;       //block size
                stMsg.Data3 = 0;       //stmin =0

                myDID = "F125";
                this.lblF125.Text = "DID F125";
            }
            else if (myForm.RadioType == "ACM" || myForm.RadioType == "CSACM")
            {
                lNetworkID = 1;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF1)] = 0x07;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF2)] = 0xB8;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF3)] = 0x05;

                //Set the filter up 
                stFilter.Header = convertFromHex("72F");
                stFilter.HeaderMask = convertFromHex("FFF");

                //Set the Flow Control Frame Properties
                stMsg.ArbIDOrHeader = convertFromHex("727");
                stMsg.NumberBytesData = 8;
                stMsg.StatusBitField = 2;
                stMsg.Data1 = Convert.ToByte(convertFromHex("30"));    //flow control frame
                stMsg.Data2 = 0;       //block size
                stMsg.Data3 = 0;       //stmin =0

             //   myDID = "F110";
             //   this.lblF125.Text = "DID F110";
            }
            else if (myForm.RadioType == "EFP")
            {
                lNetworkID = 1;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF1)] = 0x07;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF2)] = 0xB8;
                bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF3)] = 0x05;

                //Set the filter up 
                stFilter.Header = convertFromHex("7AF");
                stFilter.HeaderMask = convertFromHex("FFF");

                //Set the Flow Control Frame Properties
                stMsg.ArbIDOrHeader = convertFromHex("7A7");
                stMsg.NumberBytesData = 8;
                stMsg.StatusBitField = 2;
                stMsg.Data1 = Convert.ToByte(convertFromHex("30"));    //flow control frame
                stMsg.Data2 = 0;       //block size
                stMsg.Data3 = 0;       //stmin =0

                //myDID = "F110";
                //this.lblF125.Text = "DID F110";
            }

            IcsNeoDll.icsneoEnableNetworkCom(m_hObject, 0);
            lResult = IcsNeoDll.icsneoSendConfiguration(m_hObject, ref bConfigBytes[0], iNumBytes);
            IcsNeoDll.icsneoEnableNetworkCom(m_hObject, 1);

            //Set the established parameters
            IcsNeoDll.icsneoSetISO15765RxParameters(m_hObject, 1, 1, ref stFilter, ref stMsg, 300, 0, 0, 0);

            // load the message structure
            if (myForm.RadioType == "ACM")
                myBitRate = 125000;
            else if (myForm.RadioType == "EFP")
                myBitRate = 125000;
            else if (myForm.RadioType == "LXF")
                myBitRate = 500000;
            else if (myForm.RadioType == "CSACM")
                myBitRate = 125000;

            //Set the established parameters
            IcsNeoDll.icsneoSetBitRate(m_hObject, myBitRate, 1);

            clearForm1();
        }
        
        void OnKeyPress(object sender, KeyEventArgs e)
        {
            string myPath = @"C:\DTC Checker\" + myForm.LogFile;
            object misValue = System.Reflection.Missing.Value;

            // throw new Exception("The method or operation is not implemented.");

            if (e.KeyData == Keys.L)
            {
                // If file already exists and is not open
                if (File.Exists(myPath))
                {
                    // Open the Excel file and get worksheet
                    if (logOpen == false)
                    {
                        try
                        {

                            myExcelApp = new Excel.Application();
                            myExcelWorkBook = myExcelApp.Workbooks.Open(myPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                            myExcelWorkSheet = (Excel.Worksheet)myExcelWorkBook.Worksheets.get_Item(1);
                            myExcelApp.Visible = true;
                            logOpen = true;
                        }
                        catch
                        {
                            MessageBox.Show("Unable to open Log File");
                            return;
                        }
                    }
                    else
                    {
                        myExcelWorkBook = myExcelApp.Workbooks.Open(myPath, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);
                        myExcelWorkSheet = (Excel.Worksheet)myExcelWorkBook.Worksheets.get_Item(1);
                        myExcelWorkBook.Close(true, misValue, misValue);
                        myExcelApp.Quit();
                        logOpen = false;
                        releaseObject(myExcelApp);
                        releaseObject(myExcelWorkBook);
                        releaseObject(myExcelWorkSheet);
                    }
                }
            }
            else
                return;
        }
  
        #endregion
      
        private void honeywellCONN()
        {


            if (!comPort.IsOpen)
            {

                flowControlTimer.Stop();

                try
                {
                    comPort.Open();
                    MessageBox.Show("Comms: " + comPort.IsOpen.ToString());
                    //    comPort_status.Text = "OPEN";
                    //   comPort_status.ForeColor = Color.Green;
                    lblHoneywellResult.Text = "PASS";
                    lblHoneywellResult.ForeColor = Color.Green;
                    lblReady.ForeColor = Color.Black;
                    //  lblReady.Text = "Ready to scan.";
                    checkHoneywell = false;
                }
                //nested try catch block is for honeywell bug
                //TODO: add messages which allows user to know where the problem is
                catch (Exception ex)
                {
                }

                flowControlTimer.Start();
            }
        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            //on the log button click add the new device to the list and see if there is a duplicate
            string serialNum = this.txtSerialNumber.Text.ToString();
            string partNum = this.txtPartNumber.Text.ToString();
            bool duplicate = false;

            if (handled)
            {
                handled = false;
                return;


                DateTime dt = new DateTime();
                device d = new device(serialNum, partNum);

                if (txtPartNumber.Text == "")
                {

                    MessageBox.Show("Please enter a part number.");
                }
                else if (txtSerialNumber.Text == "")
                {
                    MessageBox.Show("Please enter a serial number.");
                }
                else if (txtSerialNumber.Text == "" && txtPartNumber.Text == "")
                {
                    MessageBox.Show("Please scan the proper device information.");
                }
                else
                {

                    List<device> localDuplicated = new List<device>();

                    for (int x = 0; x < deviceList.Count; x++)
                    {
                        if (d.getPartSerialSum() == deviceList[x].getPartSerialSum())
                        {
                            //stores all values which are duplicated with the current device
                            localDuplicated.Add(deviceList[x]);

                            //add the device to the master list of duplicated values
                            duplicateDevices.Add(d);
                            duplicate = true;
                            break;
                        }
                    }

                    if (duplicate)
                    {
                        duplicateCounter++;
                        MessageBox.Show("Combination of serial Number " + d.getSerialNum() + "and part number " + d.getPartNum() + "has already been scanned.");
                    }

                    deviceList.Add(d);
                }
            }
        }

        private void scanDevice_button_Click(object sender, EventArgs e)
        {
            if (handled)
            {
                handled = false;
                return;
            }
        }

        private void txtPartNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
          //  this.ignoredDTCs = myForm.ignoredDTCs;
            int counter = 0;
            string line;

            string appLoc = AppDomain.CurrentDomain.BaseDirectory;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(appLoc + "ignored_dtcs.txt");

                while ((line = file.ReadLine()) != null)
                {
                    this.ignoredDTCs.Add(line);
                    counter++;
                }

                

                file.Close();
            }
            catch
            {

                Console.WriteLine("warning: dtc file not found....");
            }


            Console.WriteLine("ignored dtc 1: " + ignoredDTCs[1].ToString());
        }


        /// <summary>
        /// Click listener for reset button. Clears all forms.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reset_button_Click(object sender, EventArgs e)
        {

            clearDTCList();
            clearForm1();
            clearForm2();

        }


        /// <summary>
        /// Called to scan the device when it is found to be connected.
        /// </summary>
        private void initiateScan()
        {
            this.radio_connection_label.BeginInvoke((MethodInvoker)delegate
                            {
                                this.radio_connection_label.Text = "YES";
                            });


            if (!afterScan)
            {
                inScan = true;
                updatingGUI = true;
                if (this.radio_connection_label.Text == "NO")
                {
                    this.radio_connection_label.BeginInvoke((MethodInvoker)delegate
                    {
                        this.radio_connection_label.Text = "YES";
                    });
                }
                updatingGUI = false;

                clearDTCList();

                bool myTest = false;
                bool comOpen = true;

                if (!comPort.IsOpen)
                {
                    try
                    {
                        comPort.Open();
                    }
                    catch
                    {
                        comOpen = false;
                        MessageBox.Show("Com port could not be opened. L1863");
                    }
                }

                if (comPort.IsOpen)
                    myMessage.Data = comPort.ReadExisting();
                else
                    MessageBox.Show("Comport not open");

                updatingGUI = true;
                updateReadyLabel("SCANNING DEVICE");
                updatingGUI = false;

                clearDTCList();

                updateDeviceScanStatus("Getting DIDF111");

                #region DIDs
                requestDIDs("F111");
                Thread.Sleep(300);
                readDIDs();
                if (myMessage.DIDF111 == "" | myMessage.DIDF111 == null)
                {
                    requestDIDs("F111");
                    Thread.Sleep(400);
                    readDIDs();
                }
                if (myMessage.DIDF111 == "" | myMessage.DIDF111 == null)
                {
                    requestDIDs("F111");
                    Thread.Sleep(500);
                    readDIDs();
                }
                if (myMessage.DIDF111 == "" | myMessage.DIDF111 == null)
                {
                    myMessage.DIDF111 = "No Response";
                    requestDIDs("F111");
                    Thread.Sleep(600);
                    readDIDs();
                }

                progressBar1.ForeColor = Color.Blue;
                progressBar1.Increment(+7);

                updateDeviceScanStatus("Getting DIDF113");

                requestDIDs("F113");
                Thread.Sleep(300);
                readDIDs();
                if (myMessage.DIDF113 == "" | myMessage.DIDF113 == null)
                {
                    requestDIDs("F113");
                    Thread.Sleep(400);
                    readDIDs();
                }
                if (myMessage.DIDF113 == "" | myMessage.DIDF113 == null)
                {
                    requestDIDs("F113");
                    Thread.Sleep(500);
                    readDIDs();
                }
                if (myMessage.DIDF113 == "" | myMessage.DIDF113 == null)
                {
                    myMessage.DIDF113 = "No Response";
                    requestDIDs("F113");
                    Thread.Sleep(600);
                    readDIDs();
                }

                progressBar1.Increment(+7);

                // updateDeviceScanStatus("Getting DIDF124");

                requestDIDs("F124");
                Thread.Sleep(300);
                readDIDs();
                if (myMessage.DIDF124 == "" | myMessage.DIDF124 == null)
                {
                    requestDIDs("F124");
                    Thread.Sleep(400);
                    readDIDs();
                }
                if (myMessage.DIDF124 == "" | myMessage.DIDF124 == null)
                {
                    requestDIDs("F124");
                    Thread.Sleep(500);
                    readDIDs();
                }
                if (myMessage.DIDF124 == "" | myMessage.DIDF124 == null)
                {
                    myMessage.DIDF124 = "No Response";
                    requestDIDs("F124");
                    Thread.Sleep(600);
                    readDIDs();
                }

                progressBar1.Increment(+7);

                updateDeviceScanStatus("Getting DID" + myDID);

                requestDIDs(myDID);
                Thread.Sleep(300);
                readDIDs();
                if (myMessage.DIDF125 == "" | myMessage.DIDF125 == null)
                {
                    requestDIDs(myDID);
                    Thread.Sleep(400);
                    readDIDs();
                }
                if (myMessage.DIDF125 == "" | myMessage.DIDF125 == null)
                {
                    requestDIDs(myDID);
                    Thread.Sleep(500);
                    readDIDs();
                }
                if (myMessage.DIDF125 == "" | myMessage.DIDF125 == null)
                {
                    myMessage.DIDF125 = "No Response";
                    requestDIDs(myDID);
                    Thread.Sleep(600);
                    readDIDs();
                }

                //     progressBar1.ForeColor = Color.Orange;
                progressBar1.Increment(+7);
                updateDeviceScanStatus("Getting DIDF188");

                requestDIDs("F188");
                Thread.Sleep(300);
                readDIDs();
                if (myMessage.DIDF188 == "" | myMessage.DIDF188 == null)
                {
                    requestDIDs("F188");
                    Thread.Sleep(400);
                    readDIDs();
                }
                if (myMessage.DIDF188 == "" | myMessage.DIDF188 == null)
                {
                    requestDIDs("F188");
                    Thread.Sleep(500);
                    readDIDs();
                }
                if (myMessage.DIDF188 == "" | myMessage.DIDF188 == null)
                {
                    myMessage.DIDF188 = "No Response";
                    requestDIDs("F188");
                    Thread.Sleep(600);
                    readDIDs();
                }

                progressBar1.Increment(+16); 

                #endregion

                #region LXF ONLY
                // Only execute this software check if Gen3.1 is selected.
                // we are only doing lxf now
             //   if (myForm.RadioType == "LXF")
             //   {
                    //  updateReadyLabel("Reading device");
                    // For Internal APP Part Number
                    changeSession(3);
                    Thread.Sleep(300);
                    //corresponds to the 0x01 hex val for APP
                    requestPartNumber(1);
                    updateDeviceScanStatus("Getting APP");
                    //     updateReadyLabel("Requesting APP...");
                    readPartNumber();
                    if (myMessage.APP == "" | myMessage.APP == null | myMessage.APP == "NR")
                    {
                        myMessage.APP = "NR";
                        requestPartNumber(1);
                        Thread.Sleep(300);
                        readPartNumber();
                    }
                    if (myMessage.APP == "" | myMessage.APP == null | myMessage.APP == "NR")
                    {
                        myMessage.APP = "NR";
                        requestPartNumber(1);
                        Thread.Sleep(400);
                        readPartNumber();
                    }
                    if (myMessage.APP == "" | myMessage.APP == null | myMessage.APP == "NR")
                    {
                        myMessage.APP = "NR";
                        requestPartNumber(1);
                        Thread.Sleep(500);
                        readPartNumber();
                    }

                    progressBar1.Increment(+7);

                    updateDeviceScanStatus("Getting CAL");
                    // For Internal CAL Part Number
                    requestPartNumber(2);
                    //    updateReadyLabel("Requesting CAL...");
                    readPartNumber();
                    if (myMessage.CAL == "" | myMessage.CAL == null | myMessage.CAL == "NR")
                    {
                        myMessage.CAL = "NR";
                        requestPartNumber(2);
                        Thread.Sleep(300);
                        readPartNumber();
                    }
                    if (myMessage.CAL == "" | myMessage.CAL == null | myMessage.CAL == "NR")
                    {
                        myMessage.CAL = "NR";
                        requestPartNumber(2);
                        Thread.Sleep(400);
                        readPartNumber();
                    }
                    if (myMessage.CAL == "" | myMessage.CAL == null | myMessage.CAL == "NR")
                    {
                        myMessage.CAL = "NR";
                        requestPartNumber(2);
                        Thread.Sleep(500);
                        readPartNumber();
                    }

                    progressBar1.Increment(+7);

                    updateDeviceScanStatus("Getting E2P");
                    inScan = false;

                    // For Internal E2P Part Number
                    requestPartNumber(3);
                    //     updateReadyLabel("Requesting E2P...");
                    readPartNumber();
                    if (myMessage.E2P == "" | myMessage.E2P == null | myMessage.E2P == "NR")
                    {
                        myMessage.E2P = "NR";
                        requestPartNumber(3);
                        Thread.Sleep(300);
                        readPartNumber();
                    }
                    if (myMessage.E2P == "" | myMessage.E2P == null | myMessage.E2P == "NR")
                    {
                        myMessage.E2P = "NR";
                        requestPartNumber(3);
                        Thread.Sleep(400);
                        readPartNumber();
                    }
                    if (myMessage.E2P == "" | myMessage.E2P == null | myMessage.E2P == "NR")
                    {
                        myMessage.E2P = "NR";
                        requestPartNumber(3);
                        Thread.Sleep(500);
                        readPartNumber();
                    }

                    progressBar1.Increment(+7);
                    updateDeviceScanStatus("Device Complete");

                    // For Internal PBL Part Number
                    requestPartNumber(4);
                    //   updateReadyLabel("Requesting PBL...");
                    readPartNumber();
                    if (myMessage.PBL == "" | myMessage.PBL == null | myMessage.PBL == "NR")
                    {
                        myMessage.PBL = "NR";
                        requestPartNumber(4);
                        Thread.Sleep(300);
                        readPartNumber();
                    }
                    if (myMessage.PBL == "" | myMessage.PBL == null | myMessage.PBL == "NR")
                    {
                        myMessage.PBL = "NR";
                        requestPartNumber(4);
                        Thread.Sleep(400);
                        readPartNumber();
                    }
                    if (myMessage.PBL == "" | myMessage.PBL == null | myMessage.PBL == "NR")
                    {
                        myMessage.PBL = "NR";
                        requestPartNumber(4);
                        Thread.Sleep(500);
                        readPartNumber();
                    }

                    updateReadyLabel("Device Complete.");

                    progressBar1.Increment(+7);
                   // updateDeviceScanStatus("Device Complete");

                    receiveAPP(myMessage.APP);
                    receiveCAL(myMessage.CAL);
                    receivePBL(myMessage.PBL);
                    receiveE2P(myMessage.E2P);
                
                #endregion

                receiveDIDF111(myMessage.DIDF111);
                receiveDIDF113(myMessage.DIDF113);
                receiveDIDF124(myMessage.DIDF124);
                receiveDIDF125(myMessage.DIDF125);
                receiveDIDF188(myMessage.DIDF188);


                requestCounter = 0;
                //NEW 
   
                //2. diagnosticSessionControl, extendedDiagnosticSession 10 03  
                sendDiagnosticMessage(0x02, 0x10, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00);
                Console.WriteLine("CALLED 02 10 03");
                Thread.Sleep(700);
                requestCounter++; 
                progressBar1.Increment(+7);
                //3. routine control, startRoutine, on demand self test 31 01 02 02 
                sendDiagnosticMessage(0x04, 0x31, 0x01, 0x02, 0x02, 0x00, 0x00, 0x00);
                Console.WriteLine("CALLED 04 31 01 02 02");
                Thread.Sleep(700);
                requestCounter++;
               // readDTCs();
                //4. diagnosticSessionControl, extendedDiagnosticSession 10 03   
                sendDiagnosticMessage(0x02, 0x10, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00);
                Console.WriteLine("CALLED 02 10 03");
                Thread.Sleep(700);
                requestCounter++; 
                progressBar1.Increment(+7);
                //5. readdtcinformation, reportdtcbystatusmask, testnotcompletethisoperationcycle 19 02 40 
                sendDiagnosticMessage(0x03, 0x19, 0x02, 0x8F, 0x00, 0x00, 0x00, 0x00);
                Console.WriteLine("CALLED 03 19 02 40");
                Thread.Sleep(700);
                readDTCs();
                progressBar1.Increment(+7);

                checkResults();


                waitingForSerial = true;

                if ((oldResult != myMessage.Result) || (myMessage.LastSerialNumber != myMessage.SerialNumber))
                {

                    myCounter++;
                    myMessage.LastSerialNumber = myMessage.SerialNumber;
                }


                deviceScanned = true;

                progressBar1.Increment(+10);

                updatingGUI = true;
                updateReadyLabel("PLEASE SCAN SERIAL NUMBER");
                updatingGUI = false;
                afterScan = true;
            }

        }

       private void sendDiagnosticMessage(byte b1, byte b2, byte b3, byte b4, byte b5, byte b6, byte b7, byte b8) { 
           
            long lResult;
            IcsSpyMessage stMessagesTx;
            stMessagesTx = CreateEmptyStructure();
            stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);

            // load the message structure
            stMessagesTx.StatusBitField = 0x00;

            stMessagesTx.ArbIDOrHeader = 0x727;

            // Number of data bytes always equal to 8
            stMessagesTx.NumberBytesData = 0x08;

            // Load all of the data bytes in the structure
            // This is for request DTCs...
            stMessagesTx.Data1 = b1;
            stMessagesTx.Data2 = b2;
            stMessagesTx.Data3 = b3;
            stMessagesTx.Data4 = b4;
            myMessage.DIDRequest = 0x19;
            stMessagesTx.Data5 = b5;
            stMessagesTx.Data6 = b6;
            stMessagesTx.Data7 = b7;
            stMessagesTx.Data8 = b8;

            // Transmit the assembled message to read the DID
            lResult = IcsNeoDll.icsneoTxMessages(m_hObject, ref stMessagesTx, Convert.ToByte(lNetworkID), 0);
            // Test the returned result
            if (lResult != 1)
            {
                lblValueCANResult.Text = "FAIL";
                // MessageBox.Show("Problem Transmitting Message");
            }
       }

        private void flowControlTimer_Tick(object sender, EventArgs e)
        {
            

            if (!isWriting && !updatingGUI && !afterScan && 
                !stopTimers && !setupIsOpen)
            {

                if ((myForm.setupDone || setupOverride) && lblReady.Text == "PLEASE COMPLETE SETUP")
                {
                    updateReadyLabel("");

                }

                if (inScan && lblReady.Text != "SCANNING DEVICE")
                {

                    updateReadyLabel("SCANNING DEVICE");
                }

                transmitFlowControl(0x727);
                transmitFlowControl(0x7A7);
                if (!transferInProgress)
                {
                    if (loadCompleate)
                    {
                        if (loadCompleate)
                            honeywellCONN();
                    }
                }
            }
        }

        private void flashLabelTimer_Tick(object sender, EventArgs e)
        {
            


                if (dtcResultLabel.Text == "FAIL")
                {
                    if (flashDTC)
                    {
                        this.dtcResultLabel.ForeColor = Color.Red;
                        this.dtcResultLabel.BackColor = Color.Transparent;
                        flashDTC = !flashDTC;
                    }
                    else
                    {
                        this.dtcResultLabel.ForeColor = Color.Yellow;
                        this.dtcResultLabel.BackColor = Color.Red;
                        flashDTC = !flashDTC;
                    }
                }
                else if (dtcResultLabel.Text == "PASS")
                {
                    this.dtcResultLabel.ForeColor = Color.Green;
                    this.dtcResultLabel.BackColor = Color.Transparent;
                }

                if (deviceResultLabel.Text == "FAIL")
                {
                    if (flashResult)
                    {
                        this.deviceResultLabel.ForeColor = Color.Red;
                        this.deviceResultLabel.BackColor = Color.Transparent;
                        flashResult = false;
                    }
                    else
                    {
                        this.deviceResultLabel.ForeColor = Color.Yellow;
                        this.deviceResultLabel.BackColor = Color.Red;
                        flashResult = true;
                    }
                }
                else if (deviceResultLabel.Text == "PASS")
                {
                    this.deviceResultLabel.ForeColor = Color.Green;
                    this.deviceResultLabel.BackColor = Color.Transparent;
                }


        }

        private void checkConnectionTimer_Tick(object sender, EventArgs e)
        {
            if (!isWriting && !updatingGUI && !noSerial  
                && !afterScan && !stopTimers && !setupIsOpen)
            {
                try
                {
                    if (myForm.setupDone || setupOverride)
                    {

                        isConnectionRequest = true;

                        //get 3 trues for connected or three falses for disconnect
                        for (int x = 0; x <= 2; x++)
                        {
                            //try to get f111
                            requestDIDs("F111");
                            Thread.Sleep(300);
                            readDIDs();

                            //was it successful?
                            if (testResponse)
                            {
                                connectionArray[x] = true;
                            }
                            else
                            {
                                connectionArray[x] = false;
                            }

                            //reset the response boolean
                            testResponse = false;
                        }

                        //how did we do? 
                        //all true?
                        if (connectionArray[0] && connectionArray[1] 
                            && connectionArray[2] && !deviceConnected)
                        {
                             

                             updateReadyLabel("SCANNING");
                        

                            deviceConnected = true;
                            isConnectionRequest = false;
                                     
                            isReading = true;

                            if (this.txtSerialNumber.Text == "")
                            {
                                Thread.Sleep(100);
                                initiateScan();
                            }
                            else
                            {
                                MessageBox.Show("Please scan the serial number for the last device.");
                                noSerial = true;

                            }
                            isReading = false;
                          
                        }
                        //all false?
                        else if (!connectionArray[0] && !connectionArray[1] && !connectionArray[2])
                        {
                            deviceConnected = false;

                            this.radio_connection_label.BeginInvoke((MethodInvoker)delegate
                            {
                                this.radio_connection_label.Text = "NO";
                            });
                        }
                        else
                        {
                            for (int x = 0; x < 2; x++)
                            {
                                connectionArray[x] = false;
                            }
                            return;
                        }

                        //any other combination voids nothing

                        //reset the connection array
                        for (int x = 0; x < 2; x++)
                        {
                            connectionArray[x] = false;
                        }

                    }
                }
                catch
                {
                    Console.WriteLine("no form created");
                }
            }
        }

        private void lblValueCAN_Click(object sender, EventArgs e)
        {

        }

        private void hardReset()
        {

            clearDTCList();
            clearForm1();
            clearForm2();
            clearMessageData();

            this.palletQuantity = 0;
            this.failedQuantity = 0;
            this.duplicateCounter = 0;
            this.passedQuantity = 0; 

            this.failedQuantityLabel.Text = "FAIL: ";
            this.duplicateQuantityLabel.Text = "DUP.: "; 
            this.passedQuantityLabel.Text = "PASS: ";



          //  this.palletStatusLabel.Text = "Please complete pallet setup.";
            
            myForm.clearALL();

           // ds.reset();

            DataSerializer.saveToFile(ds);  


            //clear all textboxes  

            this.duplicateQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.duplicateQuantityLabel.Text = "DUP: " + 0;
            });

            this.passedQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.passedQuantityLabel.Text = "PASS: " + 0;
            });


            this.failedQuantityLabel.BeginInvoke((MethodInvoker)delegate
            {
                this.failedQuantityLabel.Text = "FAIL: " + 0;
            });


            this.txtAPLExpected.BeginInvoke((MethodInvoker)delegate
            {
                txtAPLExpected.Text = "";
            });


            this.txtE2PExpected.BeginInvoke((MethodInvoker)delegate
             {
                 txtE2PExpected.Text = "";
             });

            this.txtCALExpected.BeginInvoke((MethodInvoker)delegate
            {
                 this.txtCALExpected.Text = "";

            });

            this.txtPBLExpected.BeginInvoke((MethodInvoker)delegate
            {
                 this.txtPBLExpected.Text = "";

             });

            this.txtDIDF111Expected.BeginInvoke((MethodInvoker)delegate
            {
                this.txtDIDF111Expected.Text = "";

            });


            this.txtDIDF113Expected.BeginInvoke((MethodInvoker)delegate
            {
                this.txtDIDF113Expected.Text = "";

            });


            this.txtDIDF124Expected.BeginInvoke((MethodInvoker)delegate
            {
                this.txtDIDF124Expected.Text = "";

            });


            this.txtDIDF125Expected.BeginInvoke((MethodInvoker)delegate
            {
                this.txtDIDF125Expected.Text = "";

            });


            this.txtDIDF188Expected.BeginInvoke((MethodInvoker)delegate
            {
                this.txtDIDF188Expected.Text = "";

            });




        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearDTCList();
            clearForm1();
            clearForm2();
            clearMessageData();
            updateDTCResult("");
            updateDeviceResult("");
            progressBar1.Value = 0;
            afterScan = false;
            initiateScan();
        }

       
        private void showFileDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
           fdf.StartPosition = FormStartPosition.CenterScreen;
           fdf.ShowDialog();
        }

        private void changeOperatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (myForm.setupDone)
            {
                nof.StartPosition = FormStartPosition.CenterScreen;
                nof.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please complete setup first.");
            }
        }

        private void dataStatusToolLabel_Click(object sender, EventArgs e)
        {

        }

        private void btnLogFile_Click_1(object sender, EventArgs e)
        {
            /*
            string myPath = @"C:\DTC Checker\" + myForm.LogFile;
            Application excel = new Application();
            Workbook wb = excel.Workbooks.Open(myPath);
             * */
        }

        private void ignoredDTCsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ignoreForm.StartPosition = FormStartPosition.CenterScreen;
            ignoreForm.ShowDialog();
        }

        private void writeToSerializer()
        {
            DataSerializer.saveToFile(ds);  

        }

        private void readToSerializer()
        {
            ds = DataSerializer.LoadFromFile();
        }

        private void updateSetupVariables()
        {

        }

        private void updateDTCExceptionVariables()
        {


        }

        private void updateAllLabels()
        {


        }

        private void dataFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fdf.StartPosition = FormStartPosition.CenterScreen;
            fdf.ShowDialog();
        }


    }
}


