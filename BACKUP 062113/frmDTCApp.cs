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
using CoreScanner;

namespace ICSNeoCSharp
{
    public class FormDTCApplication : System.Windows.Forms.Form
    {
        #region Variables

        internal System.Windows.Forms.GroupBox grpConnections;
        internal System.Windows.Forms.GroupBox grpScannedValues;
        private System.ComponentModel.IContainer components = null;
        int m_hObject = 0;   //Handle of Device
        bool m_bPortOpen = false;
        private System.Windows.Forms.Label lblResult1;
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
        private System.Windows.Forms.Label lblPartNumber;
        private System.Windows.Forms.Label lblSerialNumber;
        private System.Windows.Forms.TextBox txtSerialNumber;
        private System.Windows.Forms.TextBox txtPartNumber;
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
        private System.Windows.Forms.Label lblResult2;
        private Button btnLogFile;

        private string myDID = null;
        Excel.Application myExcelApp = null;
        Excel.Workbook myExcelWorkBook = null;
        Excel.Worksheet myExcelWorkSheet = null;
        private bool logOpen = false;
        private GroupBox groupBox1;
        private Label lblCAL;
        private Label lblE2P;
        private Label lblPBL;
        private Label lblAPP;
        private TextBox txtPBL;
        private TextBox txtAPP;
        private TextBox txtE2P;
        private TextBox txtCAL;
        private GroupBox grpDTCs;
        private int myCounter = 1;
        private ListBox lstDTCs;
        private System.Windows.Forms.Timer timer1;
        private string oldResult = null;
        private int myBitRate = 0;

        //hold the devices
        //used to detect duplicate values
        List<device> deviceList = new List<device>();
        List<device> duplicateDevices = new List<device>();

        List<device> failedDevices = new List<device>();
        List<device> passedDevices = new List<device>();

        private ToolStripMenuItem dataToolStripMenuItem;
        private ToolStripMenuItem duplicatesToolStripMenuItem;
        private ToolStripMenuItem passedToolStripMenuItem;
        private ToolStripMenuItem failedToolStripMenuItem;

        FailedForm myFailedForm = new FailedForm();
        PassedForm myPassedForm = new PassedForm();
        frmHoneywell honeyForm = new frmHoneywell();  

        private Label passedLabel;
        private Label failedLabel;
        private Label duplicateLabel;
        private Label totalLabel;
        //FormDuplicate myDuplicateForm = new FormDuplicate();

        private int duplicateCounter = 0;
        private bool canState = false;

        private bool checkHoneywell = false;
        private ToolStripMenuItem motatrolaScannerSetupToolStripMenuItem;
        private bool loadCompleate = false;
        private Label comPort_status;
        private Label wiScanner_status;
        private Label comlabel;
        private Label wiscannerlabel;
        private GroupBox Counters;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel trackingStripStatusLabel1;
        private ToolStripStatusLabel quantityStripStatusLabel2;
        private ToolStripStatusLabel partStripStatusLabel3;
        private ToolStripStatusLabel fileStripStatusLabel4;
        private ToolStripStatusLabel duplicatesStripStatusLabel5;
        private ToolStripStatusLabel precentageStripStatusLabel1;

       

        ScannerForm scanForm = new ScannerForm(); 


        //Variables for wireless scanner
        CCoreScannerClass ccs = new CCoreScannerClass(); 
        short[] scannerTypes;
        short numberOfScannerTypes;
        private ToolStripStatusLabel mototrolaStripStatusLabel1;   
        int status;
        private ToolStripMenuItem wirelessMotorolaToolStripMenuItem;
        private ToolStripMenuItem wiredHoneywellToolStripMenuItem;
        bool wiScannerApiOpen = false;
       

        #endregion

        #region Constructor/Destructor
        public FormDTCApplication()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            myForm = new FormSetup(comPort);

            //// Setup event handlers for Settings change
            myForm.DIDF111Changed += new MySettingsEvent(OnDIDF111Change);
            myForm.DIDF124Changed += new MySettingsEvent(OnDIDF124Change);
            myForm.DIDF125Changed += new MySettingsEvent(OnDIDF125Change);
            myForm.DIDF188Changed += new MySettingsEvent(OnDIDF188Change);
            myForm.BaudRateChanged += new MySettingsEvent(OnBaudRateChange);
            myForm.LogFileChanged += new MySettingsEvent(OnLogFileChange);
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

            myDID = "F110";
            lblF125.Text = "DID F110";

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
            this.comPort_status = new System.Windows.Forms.Label();
            this.wiScanner_status = new System.Windows.Forms.Label();
            this.comlabel = new System.Windows.Forms.Label();
            this.wiscannerlabel = new System.Windows.Forms.Label();
            this.lblHoneywellResult = new System.Windows.Forms.Label();
            this.lblValueCANResult = new System.Windows.Forms.Label();
            this.lblScanner = new System.Windows.Forms.Label();
            this.lblValueCAN = new System.Windows.Forms.Label();
            this.grpScannedValues = new System.Windows.Forms.GroupBox();
            this.txtPartNumber = new System.Windows.Forms.TextBox();
            this.txtSerialNumber = new System.Windows.Forms.TextBox();
            this.lblPartNumber = new System.Windows.Forms.Label();
            this.lblSerialNumber = new System.Windows.Forms.Label();
            this.lblResult1 = new System.Windows.Forms.Label();
            this.grpResult = new System.Windows.Forms.GroupBox();
            this.btnLogFile = new System.Windows.Forms.Button();
            this.lblResult4 = new System.Windows.Forms.Label();
            this.lblResult3 = new System.Windows.Forms.Label();
            this.lblResult2 = new System.Windows.Forms.Label();
            this.mnuFile = new System.Windows.Forms.MenuStrip();
            this.tsmSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.duplicatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.passedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.failedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motatrolaScannerSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wirelessMotorolaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wiredHoneywellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblReady = new System.Windows.Forms.Label();
            this.grpDIDValidation = new System.Windows.Forms.GroupBox();
            this.lblReceived = new System.Windows.Forms.Label();
            this.txtDIDF188Received = new System.Windows.Forms.TextBox();
            this.txtDIDF125Received = new System.Windows.Forms.TextBox();
            this.txtDIDF124Received = new System.Windows.Forms.TextBox();
            this.txtDIDF113Received = new System.Windows.Forms.TextBox();
            this.txtDIDF111Received = new System.Windows.Forms.TextBox();
            this.lblExpected = new System.Windows.Forms.Label();
            this.txtDIDF188Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF125Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF124Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF113Expected = new System.Windows.Forms.TextBox();
            this.txtDIDF111Expected = new System.Windows.Forms.TextBox();
            this.lblF188 = new System.Windows.Forms.Label();
            this.lblDIDF113 = new System.Windows.Forms.Label();
            this.lblDIDF124 = new System.Windows.Forms.Label();
            this.lblF125 = new System.Windows.Forms.Label();
            this.lblDIDF111 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCAL = new System.Windows.Forms.Label();
            this.lblE2P = new System.Windows.Forms.Label();
            this.lblPBL = new System.Windows.Forms.Label();
            this.lblAPP = new System.Windows.Forms.Label();
            this.txtPBL = new System.Windows.Forms.TextBox();
            this.txtAPP = new System.Windows.Forms.TextBox();
            this.txtE2P = new System.Windows.Forms.TextBox();
            this.txtCAL = new System.Windows.Forms.TextBox();
            this.grpDTCs = new System.Windows.Forms.GroupBox();
            this.lstDTCs = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.passedLabel = new System.Windows.Forms.Label();
            this.failedLabel = new System.Windows.Forms.Label();
            this.duplicateLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.Counters = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.trackingStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.quantityStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.partStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.duplicatesStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.precentageStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.mototrolaStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.grpConnections.SuspendLayout();
            this.grpScannedValues.SuspendLayout();
            this.grpResult.SuspendLayout();
            this.mnuFile.SuspendLayout();
            this.grpDIDValidation.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.grpDTCs.SuspendLayout();
            this.Counters.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnections
            // 
            this.grpConnections.Controls.Add(this.comPort_status);
            this.grpConnections.Controls.Add(this.wiScanner_status);
            this.grpConnections.Controls.Add(this.comlabel);
            this.grpConnections.Controls.Add(this.wiscannerlabel);
            this.grpConnections.Controls.Add(this.lblHoneywellResult);
            this.grpConnections.Controls.Add(this.lblValueCANResult);
            this.grpConnections.Controls.Add(this.lblScanner);
            this.grpConnections.Controls.Add(this.lblValueCAN);
            this.grpConnections.Location = new System.Drawing.Point(13, 28);
            this.grpConnections.Name = "grpConnections";
            this.grpConnections.Size = new System.Drawing.Size(385, 141);
            this.grpConnections.TabIndex = 49;
            this.grpConnections.TabStop = false;
            this.grpConnections.Text = "Connections";
            // 
            // comPort_status
            // 
            this.comPort_status.AutoSize = true;
            this.comPort_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comPort_status.Location = new System.Drawing.Point(255, 103);
            this.comPort_status.Name = "comPort_status";
            this.comPort_status.Size = new System.Drawing.Size(86, 29);
            this.comPort_status.TabIndex = 9;
            this.comPort_status.Text = "OPEN";
            this.comPort_status.Click += new System.EventHandler(this.comPort_status_Click);
            // 
            // wiScanner_status
            // 
            this.wiScanner_status.AutoSize = true;
            this.wiScanner_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wiScanner_status.Location = new System.Drawing.Point(255, 74);
            this.wiScanner_status.Name = "wiScanner_status";
            this.wiScanner_status.Size = new System.Drawing.Size(80, 29);
            this.wiScanner_status.TabIndex = 8;
            this.wiScanner_status.Text = "PASS";
            // 
            // comlabel
            // 
            this.comlabel.AutoSize = true;
            this.comlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comlabel.Location = new System.Drawing.Point(8, 103);
            this.comlabel.Name = "comlabel";
            this.comlabel.Size = new System.Drawing.Size(127, 29);
            this.comlabel.TabIndex = 7;
            this.comlabel.Text = "COM Port";
            // 
            // wiscannerlabel
            // 
            this.wiscannerlabel.AutoSize = true;
            this.wiscannerlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wiscannerlabel.Location = new System.Drawing.Point(8, 74);
            this.wiscannerlabel.Name = "wiscannerlabel";
            this.wiscannerlabel.Size = new System.Drawing.Size(218, 29);
            this.wiscannerlabel.TabIndex = 6;
            this.wiscannerlabel.Text = "Wireless Scanner";
            // 
            // lblHoneywellResult
            // 
            this.lblHoneywellResult.AutoSize = true;
            this.lblHoneywellResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoneywellResult.Location = new System.Drawing.Point(255, 45);
            this.lblHoneywellResult.Name = "lblHoneywellResult";
            this.lblHoneywellResult.Size = new System.Drawing.Size(80, 29);
            this.lblHoneywellResult.TabIndex = 5;
            this.lblHoneywellResult.Text = "PASS";
            this.lblHoneywellResult.ForeColor = Color.Green;
            // 
            // lblValueCANResult
            // 
            this.lblValueCANResult.AutoSize = true;
            this.lblValueCANResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCANResult.Location = new System.Drawing.Point(255, 16);
            this.lblValueCANResult.Name = "lblValueCANResult";
            this.lblValueCANResult.Size = new System.Drawing.Size(80, 29);
            this.lblValueCANResult.TabIndex = 4;
            this.lblValueCANResult.Text = "PASS";
            // 
            // lblScanner
            // 
            this.lblScanner.AutoSize = true;
            this.lblScanner.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScanner.Location = new System.Drawing.Point(8, 45);
            this.lblScanner.Name = "lblScanner";
            this.lblScanner.Size = new System.Drawing.Size(109, 29);
            this.lblScanner.TabIndex = 1;
            this.lblScanner.Text = "Scanner";
            // 
            // lblValueCAN
            // 
            this.lblValueCAN.AutoSize = true;
            this.lblValueCAN.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValueCAN.Location = new System.Drawing.Point(8, 16);
            this.lblValueCAN.Name = "lblValueCAN";
            this.lblValueCAN.Size = new System.Drawing.Size(132, 29);
            this.lblValueCAN.TabIndex = 0;
            this.lblValueCAN.Text = "ValueCAN";
            // 
            // grpScannedValues
            // 
            this.grpScannedValues.Controls.Add(this.txtPartNumber);
            this.grpScannedValues.Controls.Add(this.txtSerialNumber);
            this.grpScannedValues.Controls.Add(this.lblPartNumber);
            this.grpScannedValues.Controls.Add(this.lblSerialNumber);
            this.grpScannedValues.Location = new System.Drawing.Point(412, 28);
            this.grpScannedValues.Name = "grpScannedValues";
            this.grpScannedValues.Size = new System.Drawing.Size(658, 141);
            this.grpScannedValues.TabIndex = 47;
            this.grpScannedValues.TabStop = false;
            this.grpScannedValues.Text = "Scanned Values";
            // 
            // txtPartNumber
            // 
            this.txtPartNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtPartNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPartNumber.ForeColor = System.Drawing.Color.Green;
            this.txtPartNumber.Location = new System.Drawing.Point(284, 86);
            this.txtPartNumber.Name = "txtPartNumber";
            this.txtPartNumber.ReadOnly = true;
            this.txtPartNumber.Size = new System.Drawing.Size(361, 44);
            this.txtPartNumber.TabIndex = 7;
            this.txtPartNumber.TabStop = false;
            this.txtPartNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSerialNumber
            // 
            this.txtSerialNumber.BackColor = System.Drawing.SystemColors.Control;
            this.txtSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSerialNumber.ForeColor = System.Drawing.Color.Green;
            this.txtSerialNumber.Location = new System.Drawing.Point(284, 24);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.ReadOnly = true;
            this.txtSerialNumber.Size = new System.Drawing.Size(361, 44);
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
            this.lblSerialNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerialNumber.Location = new System.Drawing.Point(5, 21);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new System.Drawing.Size(268, 42);
            this.lblSerialNumber.TabIndex = 1;
            this.lblSerialNumber.Text = "Serial Number";
            // 
            // lblResult1
            // 
            this.lblResult1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult1.AutoSize = true;
            this.lblResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult1.Location = new System.Drawing.Point(31, 28);
            this.lblResult1.Name = "lblResult1";
            this.lblResult1.Size = new System.Drawing.Size(0, 58);
            this.lblResult1.TabIndex = 50;
            this.lblResult1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult1.UseCompatibleTextRendering = true;
            // 
            // grpResult
            // 
            this.grpResult.Controls.Add(this.btnLogFile);
            this.grpResult.Controls.Add(this.lblResult4);
            this.grpResult.Controls.Add(this.lblResult3);
            this.grpResult.Controls.Add(this.lblResult2);
            this.grpResult.Controls.Add(this.lblResult1);
            this.grpResult.Location = new System.Drawing.Point(1088, 28);
            this.grpResult.Name = "grpResult";
            this.grpResult.Size = new System.Drawing.Size(162, 419);
            this.grpResult.TabIndex = 51;
            this.grpResult.TabStop = false;
            this.grpResult.Text = "Result";
            this.grpResult.Enter += new System.EventHandler(this.grpResult_Enter);
            // 
            // btnLogFile
            // 
            this.btnLogFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogFile.Location = new System.Drawing.Point(6, 308);
            this.btnLogFile.Name = "btnLogFile";
            this.btnLogFile.Size = new System.Drawing.Size(150, 102);
            this.btnLogFile.TabIndex = 54;
            this.btnLogFile.Text = "LOG FILE";
            this.btnLogFile.UseVisualStyleBackColor = true;
            this.btnLogFile.Click += new System.EventHandler(this.btnLogFile_Click);
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
            // lblResult2
            // 
            this.lblResult2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult2.AutoSize = true;
            this.lblResult2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult2.Location = new System.Drawing.Point(25, 74);
            this.lblResult2.Name = "lblResult2";
            this.lblResult2.Size = new System.Drawing.Size(0, 58);
            this.lblResult2.TabIndex = 51;
            this.lblResult2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult2.UseCompatibleTextRendering = true;
            // 
            // mnuFile
            // 
            this.mnuFile.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuFile.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSetup,
            this.setupToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.motatrolaScannerSetupToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.mnuFile.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.mnuFile.Location = new System.Drawing.Point(0, 0);
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(1262, 25);
            this.mnuFile.TabIndex = 52;
            this.mnuFile.Text = "File";
            // 
            // tsmSetup
            // 
            this.tsmSetup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.tsmSetup.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmSetup.Name = "tsmSetup";
            this.tsmSetup.Size = new System.Drawing.Size(38, 21);
            this.tsmSetup.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // setupToolStripMenuItem
            // 
            this.setupToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setupToolStripMenuItem.Name = "setupToolStripMenuItem";
            this.setupToolStripMenuItem.Size = new System.Drawing.Size(56, 21);
            this.setupToolStripMenuItem.Text = "Setup";
            this.setupToolStripMenuItem.Click += new System.EventHandler(this.setupToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.duplicatesToolStripMenuItem,
            this.passedToolStripMenuItem,
            this.failedToolStripMenuItem});
            this.dataToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(46, 21);
            this.dataToolStripMenuItem.Text = "Data";
            // 
            // duplicatesToolStripMenuItem
            // 
            this.duplicatesToolStripMenuItem.Name = "duplicatesToolStripMenuItem";
            this.duplicatesToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.duplicatesToolStripMenuItem.Text = "Duplicates";
            this.duplicatesToolStripMenuItem.Click += new System.EventHandler(this.duplicatesToolStripMenuItem_Click);
            // 
            // passedToolStripMenuItem
            // 
            this.passedToolStripMenuItem.Name = "passedToolStripMenuItem";
            this.passedToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.passedToolStripMenuItem.Text = "Passed";
            this.passedToolStripMenuItem.Click += new System.EventHandler(this.passedToolStripMenuItem_Click);
            // 
            // failedToolStripMenuItem
            // 
            this.failedToolStripMenuItem.Name = "failedToolStripMenuItem";
            this.failedToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.failedToolStripMenuItem.Text = "Failed";
            this.failedToolStripMenuItem.Click += new System.EventHandler(this.failedToolStripMenuItem_Click);
            // 
            // motatrolaScannerSetupToolStripMenuItem
            // 
            this.motatrolaScannerSetupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wirelessMotorolaToolStripMenuItem,
            this.wiredHoneywellToolStripMenuItem});
            this.motatrolaScannerSetupToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.motatrolaScannerSetupToolStripMenuItem.Name = "motatrolaScannerSetupToolStripMenuItem";
            this.motatrolaScannerSetupToolStripMenuItem.Size = new System.Drawing.Size(201, 21);
            this.motatrolaScannerSetupToolStripMenuItem.Text = "Scanner Troubleshooting/Setup";
            this.motatrolaScannerSetupToolStripMenuItem.Click += new System.EventHandler(this.motatrolaScannerSetupToolStripMenuItem_Click);
            // 
            // wirelessMotorolaToolStripMenuItem
            // 
            this.wirelessMotorolaToolStripMenuItem.Name = "wirelessMotorolaToolStripMenuItem";
            this.wirelessMotorolaToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.wirelessMotorolaToolStripMenuItem.Text = "Wireless (Motorola)";
            this.wirelessMotorolaToolStripMenuItem.Click += new System.EventHandler(this.wirelessMotorolaToolStripMenuItem_Click);
            // 
            // wiredHoneywellToolStripMenuItem
            // 
            this.wiredHoneywellToolStripMenuItem.Name = "wiredHoneywellToolStripMenuItem";
            this.wiredHoneywellToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.wiredHoneywellToolStripMenuItem.Text = "Wired (Honeywell)";
            this.wiredHoneywellToolStripMenuItem.Click += new System.EventHandler(this.wiredHoneywellToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(57, 21);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // lblReady
            // 
            this.lblReady.AutoSize = true;
            this.lblReady.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReady.Location = new System.Drawing.Point(351, 181);
            this.lblReady.Name = "lblReady";
            this.lblReady.Size = new System.Drawing.Size(0, 31);
            this.lblReady.TabIndex = 55;
            // 
            // grpDIDValidation
            // 
            this.grpDIDValidation.Controls.Add(this.lblReceived);
            this.grpDIDValidation.Controls.Add(this.txtDIDF188Received);
            this.grpDIDValidation.Controls.Add(this.txtDIDF125Received);
            this.grpDIDValidation.Controls.Add(this.txtDIDF124Received);
            this.grpDIDValidation.Controls.Add(this.txtDIDF113Received);
            this.grpDIDValidation.Controls.Add(this.txtDIDF111Received);
            this.grpDIDValidation.Controls.Add(this.lblExpected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF188Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF125Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF124Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF113Expected);
            this.grpDIDValidation.Controls.Add(this.txtDIDF111Expected);
            this.grpDIDValidation.Controls.Add(this.lblF188);
            this.grpDIDValidation.Controls.Add(this.lblDIDF113);
            this.grpDIDValidation.Controls.Add(this.lblDIDF124);
            this.grpDIDValidation.Controls.Add(this.lblF125);
            this.grpDIDValidation.Controls.Add(this.lblDIDF111);
            this.grpDIDValidation.Location = new System.Drawing.Point(13, 211);
            this.grpDIDValidation.Name = "grpDIDValidation";
            this.grpDIDValidation.Size = new System.Drawing.Size(1057, 319);
            this.grpDIDValidation.TabIndex = 56;
            this.grpDIDValidation.TabStop = false;
            this.grpDIDValidation.Text = "Validation";
            // 
            // lblReceived
            // 
            this.lblReceived.AutoSize = true;
            this.lblReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReceived.Location = new System.Drawing.Point(771, 16);
            this.lblReceived.Name = "lblReceived";
            this.lblReceived.Size = new System.Drawing.Size(136, 31);
            this.lblReceived.TabIndex = 67;
            this.lblReceived.Text = "Received";
            // 
            // txtDIDF188Received
            // 
            this.txtDIDF188Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF188Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF188Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF188Received.Location = new System.Drawing.Point(683, 264);
            this.txtDIDF188Received.Name = "txtDIDF188Received";
            this.txtDIDF188Received.ReadOnly = true;
            this.txtDIDF188Received.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF188Received.TabIndex = 66;
            this.txtDIDF188Received.TabStop = false;
            this.txtDIDF188Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF125Received
            // 
            this.txtDIDF125Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF125Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF125Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF125Received.Location = new System.Drawing.Point(684, 209);
            this.txtDIDF125Received.Name = "txtDIDF125Received";
            this.txtDIDF125Received.ReadOnly = true;
            this.txtDIDF125Received.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF125Received.TabIndex = 65;
            this.txtDIDF125Received.TabStop = false;
            this.txtDIDF125Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF124Received
            // 
            this.txtDIDF124Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF124Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF124Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF124Received.Location = new System.Drawing.Point(683, 155);
            this.txtDIDF124Received.Name = "txtDIDF124Received";
            this.txtDIDF124Received.ReadOnly = true;
            this.txtDIDF124Received.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF124Received.TabIndex = 64;
            this.txtDIDF124Received.TabStop = false;
            this.txtDIDF124Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF113Received
            // 
            this.txtDIDF113Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF113Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF113Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF113Received.Location = new System.Drawing.Point(683, 100);
            this.txtDIDF113Received.Name = "txtDIDF113Received";
            this.txtDIDF113Received.ReadOnly = true;
            this.txtDIDF113Received.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF113Received.TabIndex = 63;
            this.txtDIDF113Received.TabStop = false;
            this.txtDIDF113Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF111Received
            // 
            this.txtDIDF111Received.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF111Received.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF111Received.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF111Received.Location = new System.Drawing.Point(683, 50);
            this.txtDIDF111Received.Name = "txtDIDF111Received";
            this.txtDIDF111Received.ReadOnly = true;
            this.txtDIDF111Received.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF111Received.TabIndex = 62;
            this.txtDIDF111Received.TabStop = false;
            this.txtDIDF111Received.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblExpected
            // 
            this.lblExpected.AutoSize = true;
            this.lblExpected.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExpected.Location = new System.Drawing.Point(393, 16);
            this.lblExpected.Name = "lblExpected";
            this.lblExpected.Size = new System.Drawing.Size(135, 31);
            this.lblExpected.TabIndex = 61;
            this.lblExpected.Text = "Expected";
            // 
            // txtDIDF188Expected
            // 
            this.txtDIDF188Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF188Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF188Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF188Expected.Location = new System.Drawing.Point(286, 264);
            this.txtDIDF188Expected.Name = "txtDIDF188Expected";
            this.txtDIDF188Expected.ReadOnly = true;
            this.txtDIDF188Expected.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF188Expected.TabIndex = 60;
            this.txtDIDF188Expected.TabStop = false;
            this.txtDIDF188Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF125Expected
            // 
            this.txtDIDF125Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF125Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF125Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF125Expected.Location = new System.Drawing.Point(286, 209);
            this.txtDIDF125Expected.Name = "txtDIDF125Expected";
            this.txtDIDF125Expected.ReadOnly = true;
            this.txtDIDF125Expected.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF125Expected.TabIndex = 59;
            this.txtDIDF125Expected.TabStop = false;
            this.txtDIDF125Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF124Expected
            // 
            this.txtDIDF124Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF124Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF124Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF124Expected.Location = new System.Drawing.Point(285, 155);
            this.txtDIDF124Expected.Name = "txtDIDF124Expected";
            this.txtDIDF124Expected.ReadOnly = true;
            this.txtDIDF124Expected.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF124Expected.TabIndex = 58;
            this.txtDIDF124Expected.TabStop = false;
            this.txtDIDF124Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF113Expected
            // 
            this.txtDIDF113Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF113Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF113Expected.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtDIDF113Expected.Location = new System.Drawing.Point(285, 100);
            this.txtDIDF113Expected.Name = "txtDIDF113Expected";
            this.txtDIDF113Expected.ReadOnly = true;
            this.txtDIDF113Expected.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF113Expected.TabIndex = 57;
            this.txtDIDF113Expected.TabStop = false;
            this.txtDIDF113Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDIDF111Expected
            // 
            this.txtDIDF111Expected.BackColor = System.Drawing.SystemColors.Control;
            this.txtDIDF111Expected.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDIDF111Expected.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDIDF111Expected.Location = new System.Drawing.Point(285, 50);
            this.txtDIDF111Expected.Name = "txtDIDF111Expected";
            this.txtDIDF111Expected.ReadOnly = true;
            this.txtDIDF111Expected.Size = new System.Drawing.Size(361, 44);
            this.txtDIDF111Expected.TabIndex = 56;
            this.txtDIDF111Expected.TabStop = false;
            this.txtDIDF111Expected.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblF188
            // 
            this.lblF188.AutoSize = true;
            this.lblF188.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF188.Location = new System.Drawing.Point(6, 260);
            this.lblF188.Name = "lblF188";
            this.lblF188.Size = new System.Drawing.Size(185, 42);
            this.lblF188.TabIndex = 55;
            this.lblF188.Text = "DID F188";
            // 
            // lblDIDF113
            // 
            this.lblDIDF113.AutoSize = true;
            this.lblDIDF113.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF113.Location = new System.Drawing.Point(6, 101);
            this.lblDIDF113.Name = "lblDIDF113";
            this.lblDIDF113.Size = new System.Drawing.Size(185, 42);
            this.lblDIDF113.TabIndex = 54;
            this.lblDIDF113.Text = "DID F113";
            // 
            // lblDIDF124
            // 
            this.lblDIDF124.AutoSize = true;
            this.lblDIDF124.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF124.Location = new System.Drawing.Point(6, 156);
            this.lblDIDF124.Name = "lblDIDF124";
            this.lblDIDF124.Size = new System.Drawing.Size(185, 42);
            this.lblDIDF124.TabIndex = 53;
            this.lblDIDF124.Text = "DID F124";
            // 
            // lblF125
            // 
            this.lblF125.AutoSize = true;
            this.lblF125.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblF125.Location = new System.Drawing.Point(6, 208);
            this.lblF125.Name = "lblF125";
            this.lblF125.Size = new System.Drawing.Size(185, 42);
            this.lblF125.TabIndex = 52;
            this.lblF125.Text = "DID F125";
            // 
            // lblDIDF111
            // 
            this.lblDIDF111.AutoSize = true;
            this.lblDIDF111.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDIDF111.Location = new System.Drawing.Point(6, 47);
            this.lblDIDF111.Name = "lblDIDF111";
            this.lblDIDF111.Size = new System.Drawing.Size(185, 42);
            this.lblDIDF111.TabIndex = 51;
            this.lblDIDF111.Text = "DID F111";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCAL);
            this.groupBox1.Controls.Add(this.lblE2P);
            this.groupBox1.Controls.Add(this.lblPBL);
            this.groupBox1.Controls.Add(this.lblAPP);
            this.groupBox1.Controls.Add(this.txtPBL);
            this.groupBox1.Controls.Add(this.txtAPP);
            this.groupBox1.Controls.Add(this.txtE2P);
            this.groupBox1.Controls.Add(this.txtCAL);
            this.groupBox1.Location = new System.Drawing.Point(13, 536);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1237, 82);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Internal Part Numbers (GEN 3.1 Only)";
            // 
            // lblCAL
            // 
            this.lblCAL.AutoSize = true;
            this.lblCAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCAL.Location = new System.Drawing.Point(278, 24);
            this.lblCAL.Name = "lblCAL";
            this.lblCAL.Size = new System.Drawing.Size(83, 37);
            this.lblCAL.TabIndex = 14;
            this.lblCAL.Text = "CAL";
            // 
            // lblE2P
            // 
            this.lblE2P.AutoSize = true;
            this.lblE2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblE2P.Location = new System.Drawing.Point(908, 24);
            this.lblE2P.Name = "lblE2P";
            this.lblE2P.Size = new System.Drawing.Size(80, 37);
            this.lblE2P.TabIndex = 13;
            this.lblE2P.Text = "E2P";
            this.lblE2P.Click += new System.EventHandler(this.lblE2P_Click);
            // 
            // lblPBL
            // 
            this.lblPBL.AutoSize = true;
            this.lblPBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPBL.Location = new System.Drawing.Point(597, 24);
            this.lblPBL.Name = "lblPBL";
            this.lblPBL.Size = new System.Drawing.Size(80, 37);
            this.lblPBL.TabIndex = 12;
            this.lblPBL.Text = "PBL";
            // 
            // lblAPP
            // 
            this.lblAPP.AutoSize = true;
            this.lblAPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAPP.Location = new System.Drawing.Point(6, 24);
            this.lblAPP.Name = "lblAPP";
            this.lblAPP.Size = new System.Drawing.Size(84, 37);
            this.lblAPP.TabIndex = 11;
            this.lblAPP.Text = "APP";
            // 
            // txtPBL
            // 
            this.txtPBL.BackColor = System.Drawing.SystemColors.Control;
            this.txtPBL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPBL.ForeColor = System.Drawing.Color.Green;
            this.txtPBL.Location = new System.Drawing.Point(682, 21);
            this.txtPBL.Name = "txtPBL";
            this.txtPBL.ReadOnly = true;
            this.txtPBL.Size = new System.Drawing.Size(220, 35);
            this.txtPBL.TabIndex = 10;
            this.txtPBL.TabStop = false;
            this.txtPBL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAPP
            // 
            this.txtAPP.BackColor = System.Drawing.SystemColors.Control;
            this.txtAPP.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAPP.ForeColor = System.Drawing.Color.Green;
            this.txtAPP.Location = new System.Drawing.Point(92, 21);
            this.txtAPP.Name = "txtAPP";
            this.txtAPP.ReadOnly = true;
            this.txtAPP.Size = new System.Drawing.Size(180, 35);
            this.txtAPP.TabIndex = 9;
            this.txtAPP.TabStop = false;
            this.txtAPP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtE2P
            // 
            this.txtE2P.BackColor = System.Drawing.SystemColors.Control;
            this.txtE2P.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtE2P.ForeColor = System.Drawing.Color.Green;
            this.txtE2P.Location = new System.Drawing.Point(994, 21);
            this.txtE2P.Name = "txtE2P";
            this.txtE2P.ReadOnly = true;
            this.txtE2P.Size = new System.Drawing.Size(226, 35);
            this.txtE2P.TabIndex = 8;
            this.txtE2P.TabStop = false;
            this.txtE2P.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtE2P.TextChanged += new System.EventHandler(this.txtE2P_TextChanged);
            // 
            // txtCAL
            // 
            this.txtCAL.BackColor = System.Drawing.SystemColors.Control;
            this.txtCAL.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCAL.ForeColor = System.Drawing.Color.Green;
            this.txtCAL.Location = new System.Drawing.Point(367, 21);
            this.txtCAL.Name = "txtCAL";
            this.txtCAL.ReadOnly = true;
            this.txtCAL.Size = new System.Drawing.Size(224, 35);
            this.txtCAL.TabIndex = 7;
            this.txtCAL.TabStop = false;
            this.txtCAL.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // grpDTCs
            // 
            this.grpDTCs.Controls.Add(this.lstDTCs);
            this.grpDTCs.Location = new System.Drawing.Point(13, 624);
            this.grpDTCs.Name = "grpDTCs";
            this.grpDTCs.Size = new System.Drawing.Size(1237, 105);
            this.grpDTCs.TabIndex = 58;
            this.grpDTCs.TabStop = false;
            this.grpDTCs.Text = "DTCs";
            // 
            // lstDTCs
            // 
            this.lstDTCs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstDTCs.FormattingEnabled = true;
            this.lstDTCs.ItemHeight = 24;
            this.lstDTCs.Location = new System.Drawing.Point(9, 20);
            this.lstDTCs.Name = "lstDTCs";
            this.lstDTCs.Size = new System.Drawing.Size(1211, 76);
            this.lstDTCs.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // passedLabel
            // 
            this.passedLabel.AutoSize = true;
            this.passedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.passedLabel.Location = new System.Drawing.Point(9, 33);
            this.passedLabel.Name = "passedLabel";
            this.passedLabel.Size = new System.Drawing.Size(63, 17);
            this.passedLabel.TabIndex = 59;
            this.passedLabel.Text = "Passed: ";
            // 
            // failedLabel
            // 
            this.failedLabel.AutoSize = true;
            this.failedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.failedLabel.Location = new System.Drawing.Point(9, 50);
            this.failedLabel.Name = "failedLabel";
            this.failedLabel.Size = new System.Drawing.Size(54, 17);
            this.failedLabel.TabIndex = 60;
            this.failedLabel.Text = "Failed: ";
            // 
            // duplicateLabel
            // 
            this.duplicateLabel.AutoSize = true;
            this.duplicateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.duplicateLabel.Location = new System.Drawing.Point(9, 67);
            this.duplicateLabel.Name = "duplicateLabel";
            this.duplicateLabel.Size = new System.Drawing.Size(78, 17);
            this.duplicateLabel.TabIndex = 61;
            this.duplicateLabel.Text = "Duplicates:";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.totalLabel.Location = new System.Drawing.Point(9, 16);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(44, 17);
            this.totalLabel.TabIndex = 62;
            this.totalLabel.Text = "Total:";
            // 
            // Counters
            // 
            this.Counters.Controls.Add(this.duplicateLabel);
            this.Counters.Controls.Add(this.totalLabel);
            this.Counters.Controls.Add(this.failedLabel);
            this.Counters.Controls.Add(this.passedLabel);
            this.Counters.Location = new System.Drawing.Point(1076, 446);
            this.Counters.Name = "Counters";
            this.Counters.Size = new System.Drawing.Size(174, 100);
            this.Counters.TabIndex = 63;
            this.Counters.TabStop = false;
            this.Counters.Text = "Counters";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackingStripStatusLabel1,
            this.quantityStripStatusLabel2,
            this.partStripStatusLabel3,
            this.fileStripStatusLabel4,
            this.duplicatesStripStatusLabel5,
            this.precentageStripStatusLabel1,
            this.mototrolaStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 731);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1262, 22);
            this.statusStrip1.TabIndex = 64;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // trackingStripStatusLabel1
            // 
            this.trackingStripStatusLabel1.Name = "trackingStripStatusLabel1";
            this.trackingStripStatusLabel1.Size = new System.Drawing.Size(65, 17);
            this.trackingStripStatusLabel1.Text = "Tracking #: ";
            // 
            // quantityStripStatusLabel2
            // 
            this.quantityStripStatusLabel2.Name = "quantityStripStatusLabel2";
            this.quantityStripStatusLabel2.Size = new System.Drawing.Size(56, 17);
            this.quantityStripStatusLabel2.Text = "Quantity: ";
            // 
            // partStripStatusLabel3
            // 
            this.partStripStatusLabel3.Name = "partStripStatusLabel3";
            this.partStripStatusLabel3.Size = new System.Drawing.Size(39, 17);
            this.partStripStatusLabel3.Text = "Part#:";
            // 
            // fileStripStatusLabel4
            // 
            this.fileStripStatusLabel4.Name = "fileStripStatusLabel4";
            this.fileStripStatusLabel4.Size = new System.Drawing.Size(30, 17);
            this.fileStripStatusLabel4.Text = "File: ";
            // 
            // duplicatesStripStatusLabel5
            // 
            this.duplicatesStripStatusLabel5.AutoSize = false;
            this.duplicatesStripStatusLabel5.ForeColor = System.Drawing.Color.Red;
            this.duplicatesStripStatusLabel5.Name = "duplicatesStripStatusLabel5";
            this.duplicatesStripStatusLabel5.Size = new System.Drawing.Size(60, 17);
            this.duplicatesStripStatusLabel5.Text = "Duplicates:";
            // 
            // precentageStripStatusLabel1
            // 
            this.precentageStripStatusLabel1.Name = "precentageStripStatusLabel1";
            this.precentageStripStatusLabel1.Size = new System.Drawing.Size(114, 17);
            this.precentageStripStatusLabel1.Text = "Precentage Complete:";
            // 
            // mototrolaStripStatusLabel1
            // 
            this.mototrolaStripStatusLabel1.Name = "mototrolaStripStatusLabel1";
            this.mototrolaStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.mototrolaStripStatusLabel1.Text = "Motorola Scanner API: ";
            // 
            // FormDTCApplication
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1262, 753);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.Counters);
            this.Controls.Add(this.grpDTCs);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpDIDValidation);
            this.Controls.Add(this.grpResult);
            this.Controls.Add(this.grpConnections);
            this.Controls.Add(this.grpScannedValues);
            this.Controls.Add(this.mnuFile);
            this.Controls.Add(this.lblReady);
            this.KeyPreview = true;
            this.MainMenuStrip = this.mnuFile;
            this.Name = "FormDTCApplication";
            this.Text = "Version Checker V1.6";
            this.Load += new System.EventHandler(this.frmDTCApp_Load);
            this.grpConnections.ResumeLayout(false);
            this.grpConnections.PerformLayout();
            this.grpScannedValues.ResumeLayout(false);
            this.grpScannedValues.PerformLayout();
            this.grpResult.ResumeLayout(false);
            this.grpResult.PerformLayout();
            this.mnuFile.ResumeLayout(false);
            this.mnuFile.PerformLayout();
            this.grpDIDValidation.ResumeLayout(false);
            this.grpDIDValidation.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.grpDTCs.ResumeLayout(false);
            this.Counters.ResumeLayout(false);
            this.Counters.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        static private IcsSpyMessage CreateEmptyStructure()
        {
            IcsSpyMessage InputMessage;
            InputMessage.StatusBitField = 0;
            InputMessage.StatusBitField2 = 0;
            InputMessage.TimeHardware = 0;
            InputMessage.TimeHardware2 = 0;
            InputMessage.TimeSystem =  0;
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
            InputMessage.TimeSystem =  0;
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
        private void requestDIDs(string DID)
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
                lblValueCANResult.Text = "PROBLEM";
                //MessageBox.Show("Problem Transmitting Message");
            }
        }

            //called before read part number
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
        private void requestDTCs()
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
        private void frmDTCApp_Load(object sender, EventArgs e)
        {
            //main loader function
            bool valueCAN = false;
            bool honeywell = false;
        
            
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.CenterToScreen();

            if (!comPort.IsOpen)
            {
                try
                {
                    comPort.Open();
                    //FAILING HERE!!!
                    //we are not getting to this point
                    comPort_status.ForeColor = Color.Green;
                    comPort_status.Text = "PASS";
                }
                catch(Exception ex)
                {
                    comPort_status.Text = "EX MAIN";
                    comPort_status.ForeColor = Color.Red;
                    //MessageBox.Show("com pert will not open on start thread");
                    MessageBox.Show(ex.Message, "COMPORT ERROR"); 

                    
                }



            }

          //  myForm = new FormSetup(comPort);

           // myForm.setComPort(comPort); 

            checkHoneywell = true;
            
            valueCAN = connectValueCAN();
            honeywell = connectHoneywell();
            

            //Call Open API for the wireless scanner
            scannerTypes = new short[1]; // Scanner Types you are interested in
            scannerTypes[0] = 1; // 1 for all scanner types
            numberOfScannerTypes = 1; // Size of the scannerTypes array
            ccs.Open(0, scannerTypes, numberOfScannerTypes, out status);

            if (status == 0)
            {
                Console.WriteLine("CoreScanner API: Open Successful");
                //add a status bar label
                mototrolaStripStatusLabel1.ForeColor = Color.Green;
                mototrolaStripStatusLabel1.Text = "Wireless Scanner API: OPEN";
                wiScannerApiOpen = true;
            }
            else
            {
                Console.WriteLine("CoreScanner API: Open Failed"); 
                //add a status bar label 
                mototrolaStripStatusLabel1.ForeColor = Color.Red;
                mototrolaStripStatusLabel1.Text = "Wireless Scanner API: FAILED TO OPEN";
                MessageBox.Show("The wireless scanner may not work correctly. Please check all connections.");
                wiScannerApiOpen = false; 
            }

            if (valueCAN && honeywell)
                this.lblReady.Text = "READY TO SCAN...";

            loadCompleate = true; 
       }
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

            //int iIPMSB = 0;
            //int iIPLSB = 0;
            //int[] iDevices = new int[127];  //Array for the device numbers
            //int[] iSerialNumbers = new int[127]; //Araay for serial numbers of attached devices
            //int[] iOpenedStatus = new int[127]; //Array of the status of the driver
            //int[] iCommPortNumbers = new int[127]; //Array of Comm Port numbers in use
            //int iPortNumber = 1;  // Integer for the COM port number

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
                // Auto Detect for connect to ValueCAN always
                // This option connects to the first USB device found.  
                // If COM Port reaches over 25 then and error is returned.
                //while (iPortNumber != 0)
                //{
                //    // Assign baud rate to use
                //    iIPLSB = 57600;
                //    iReturnVal = IcsNeoDll.icsneoOpenPortEx(iPortNumber, Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_RS232), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD), iIPMSB, iIPLSB, 1, ref bNetworkIDs[0], ref m_hObject);
                //    if (iReturnVal == 0) // test the returned result
                //        iPortNumber++;
                //    else
                //    {
                        m_bPortOpen = true;   //Set Port Opened Flag
                       this.lblValueCANResult.Text = "PASS";
                       this.lblValueCANResult.ForeColor = Color.Green;
                //        iPortNumber = 0;
                //    }
                //    //  Code to detect if no USB COM is detected.
                //    if (iPortNumber > 25)
                //    {
                //        //Error, Show message that ValueCAN is not connected
                //        this.lblValueCANResult.Text = "FAIL";
                //        this.lblReady.Text = "Connect ValueCAN";
                //        this.lblReady.ForeColor = Color.Red;
                //        this.lblValueCANResult.ForeColor = Color.Red;
                //        iPortNumber = 0;
                //        return false;
                //    }
                //}



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
                    MessageBox.Show(ex.ToString());
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
                MessageBox.Show("Please reconnect the ValueCAN and then press OK.");
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
                return false;
            }

            stJMsg = CreateEmptyStructureJ1850();

            // read the messages from the driver
            lResult = IcsNeoDll.icsneoGetMessages(m_hObject, ref stMessages[0], ref lNumberOfMessages, ref lNumberOfErrors);

            // Check to see the number of messages read is greater than zero
            if (lNumberOfMessages == 0)
            {
                // No messages = no comms
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
                return false;
            }
            return false;

            #endregion

        }
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
                timer1.Stop();
                MessageBox.Show("Please reconnect the ValueCAN and then press OK.");
                connectValueCAN();
                timer1.Start();
                
                
                
                
                
            }
            else
            {
                lblValueCANResult.Text = "PASS";
                this.lblValueCANResult.ForeColor = Color.Green;
            }
        }
        private void setupToolStripMenuItem_Click(object sender, EventArgs e)
        {
              myForm.StartPosition = FormStartPosition.CenterScreen;
              myForm.ShowDialog();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
                myAbout.StartPosition = FormStartPosition.CenterScreen;
                myAbout.ShowDialog();
        }     
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
                            extractDID();

                        }
                        else if (stMessages[lCount - 1].Data1 != 0x30)
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
                            if (DTCcounter >= 27)
                            {
                                extractDID();
                            }
                        }
                    }
                }
            }
            
        }
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
                            //extractDTC();

                        }
                        else if (stMessages[lCount - 1].Data1 != 0x30)
                        {
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
                    }
                }
            }
            extractDTC();

        }
        private void closeLogFile()
        {
            string myPath = @"C:\Version Checker\" + myForm.LogFile;
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
        void comPort_DataReceived(object sender, EventArgs e)
        {
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
              
              

            if(comPort.IsOpen)
               myMessage.Data = comPort.ReadExisting();

            
            updateReadyLabel("Reading Data From Scan...");

            //triggered when initial scan occours
            if ((myMessage.Data[0] == 'S') && (myForm.Visible != true) && (myMessage.Data[0].ToString() + myMessage.Data[1].ToString() != "SP")) // Update serial number only
            {
                    updateSerialNumber(myMessage.SerialNumber);
                    clearForm1();
                    this.lblReady.ForeColor = Color.Black; 

                    //updateReadyLabel("LOADING DATA FROM SCAN...");
                    updateReadyLabel("READY TO SCAN...");
                    clearDTCList();
                    myMessage.ClearDTC = "";
            }
            else if ((myMessage.Data[0] == 'P') && (myForm.Visible != true)) // Update part number only also check for serial number.
            {
                
                updatePartNumber(myMessage.PartNumber);
                updateDIDF113(myMessage.PartNumber);
                if (myMessage.SerialNumber != null && myMessage.SerialNumber != "")
                    updateSerialNumber(myMessage.SerialNumber);

                this.lblReady.ForeColor = Color.Black;
                //updateReadyLabel("Reading Data From Scan...");
                updateReadyLabel("READY TO SCAN...");
                clearForm2();
                clearDTCList();
                myMessage.ClearDTC = "";
            }
           
            else if ((myMessage.Data[0].ToString() + myMessage.Data[1].ToString() == "1P") && (myForm.Visible != true) ||
                     (myMessage.Data[0].ToString() + myMessage.Data[1].ToString() == "SP") && (myForm.Visible != true)) //read and validate DIDs
            {
                myTest = testCOMMS();
                if (myTest)
                {
                    if ((myMessage.PartNumber != null) & (myMessage.SerialNumber != null) & (lblReady.Text != "NO COMMS (Is Setup Complete?)"));
                    {
                        clearMessageData();
                        clearDTCList();

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
                        #endregion

                        #region LXF ONLY
                        // Only execute this software check if Gen3.1 is selected.
                        if (myForm.RadioType == "LXF")
                        {
                            // For Internal APP Part Number
                            changeSession(3);
                            Thread.Sleep(300);
                            //corresponds to the 0x01 hex val for APP
                            requestPartNumber(1);
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

                            // For Internal CAL Part Number
                            requestPartNumber(2);
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

                            // For Internal E2P Part Number
                            requestPartNumber(3);
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

                            // For Internal PBL Part Number
                            requestPartNumber(4);
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


                            receiveAPP(myMessage.APP);
                            receiveCAL(myMessage.CAL);
                            receivePBL(myMessage.PBL);
                            receiveE2P(myMessage.E2P);
                        }
                        #endregion

                        receiveDIDF111(myMessage.DIDF111);
                        receiveDIDF113(myMessage.DIDF113);
                        receiveDIDF124(myMessage.DIDF124);
                        receiveDIDF125(myMessage.DIDF125);
                        receiveDIDF188(myMessage.DIDF188);

                        // Check DTCs
                        requestDTCs();
                        Thread.Sleep(300);
                        readDTCs(); 
                       
                        checkResults();
                        if ((oldResult != myMessage.Result) || (myMessage.LastSerialNumber != myMessage.SerialNumber))
                        {
                            updateCounter("Log File" + "          " + myCounter.ToString());
                            writeToExcel();
                            myCounter++;
                            myMessage.LastSerialNumber = myMessage.SerialNumber;
                        }           
                    }
                }
                else
                {
                    clearForm1();
                    clearDTCList();
                    myMessage.ClearDTC = "";
                    this.lblReady.ForeColor = Color.Red;
                    
                    //not having setup compleated was giving a NO COMMS error.
                    updateReadyLabel("NO COMMS (Is Setup Complete?)");
                }
                updateReadyLabel("READY TO SCAN...");
            }      
        }

        void clearForm1()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            updatePartNumber(null);
            updateDIDF113(null);
            myMessage.PartNumber = null;
            updateResult1("");
            updateResult2("");
            updateResult3("");
            updateResult4("");
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
            //foreach string in myDTCs
                
            //myMessage.SerialNumber = "";
            //myMessage.Data = "";
        }
        void clearForm2()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            //if (myMessage.Data.Length < 20)
            //{
            //    updatePartNumber(null);
            //    updateDIDF113(null);
            //    myMessage.PartNumber = null;
            //}
            updateResult1("");
            updateResult2("");
            updateResult3("");
            updateResult4("");
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
            //foreach string in myDTCs

            //myMessage.SerialNumber = "";
            //myMessage.Data = "";
        }
        void clearMessageData()
        {
            receiveDIDF111("");
            receiveDIDF113("");
            receiveDIDF124("");
            receiveDIDF125("");
            receiveDIDF188("");
            //updatePartNumber("");
            //updateDIDF113("");
            updateResult1("");
            updateResult2("");
            updateResult3("");
            updateResult4("");
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
            //foreach string in myDTCs

            //myMessage.SerialNumber = "";
            //myMessage.Data = "";
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
            if(this.txtPartNumber.InvokeRequired == true)   
            {
                this.txtPartNumber.Invoke((MethodInvoker)delegate()
                {
                    updatePartNumber(m);
                });
            }
            else
                this.txtPartNumber.Text = m;
        }
        void updateReadyLabel(string m)
        {
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
        void extractDID()
        {
            string myTemp;

            //Check to see that a positive response was received to request DID
            if (myDTCs[1] == 0x62)
            {
                if(lblPartNumber.Text == "DID F125")
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
                myMessage.DIDF111 = "No Response";
                myMessage.DIDF113 = "No Response";
                myMessage.DIDF124 = "No Response";
                myMessage.DIDF125 = "No Response";
                myMessage.DIDF188 = "No Response";
            }
        }
        void extractDTC()
        {
            string[] myTemp = new string[75];
            string myList = null;
            string myQuickString = null;

            // CASE:  Positive response and 1 DTCs
            if (myDTCs[0] == 0x07 && myDTCs[1] == 0x59)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (myDTCs[i + 4] <= 0xF)
                        myTemp[i] = "0" + myDTCs[i + 4].ToString("X");
                    else
                        myTemp[i] = myDTCs[i + 4].ToString("X");
                }
                myQuickString = commentDTCs(myTemp[0] + myTemp[1] + myTemp[2] + myTemp[3]);
                updateDTCList(myQuickString);
                myMessage.DTC = myQuickString + ",";
                myMessage.DTCCount = myMessage.DTCCount + 1;
            }
            // CASE:  Positive response and 0 DTCs
            else if (myDTCs[0] == 0x03 && myDTCs[1] == 0x59)
            {
                updateDTCList("NO DTCs");
                myMessage.DTC = "NO DTCs";
            }
            // CASE:  Positive response and 2 or more DTCs
            else if (myDTCs[0] == 0x10 && myDTCs[2] == 0x59)
            {
                for (int i = 1; i <= (myDTCs[1]); i++)
                {
                    if ((i + 4) % 8 != 0)
                    {
                        if (myDTCs[i + 4] <= 0xF)
                            myTemp[i] = "0" + myDTCs[i + 4].ToString("X");
                        else
                            myTemp[i] = myDTCs[i + 4].ToString("X");
                    }
                }
                // Parse out the list of DTCs to 1 entire string
                foreach (String i in myTemp)
                    myList = myList + i;

                // Set length to ensure that it equals CAN message frame length
                myList = myList.Substring(0, (myDTCs[1] - 3) * 2);

                // Create a loop to add each DTC from the list to the DTC List Box
                for (int i = 0; i < myList.Length; i++)
                {
                    myQuickString = commentDTCs(myList.Substring(i, 8));
                    updateDTCList(myQuickString);
                    myMessage.DTC = myQuickString + ",";
                    myMessage.DTCCount = myMessage.DTCCount + 1;
                    i = i + 7;
                }

            }
            // CASE:  Negative response 
            else if (myDTCs[0] == 0x03 && myDTCs[1] == 0x7F)
            {
                updateDTCList("NRC " + myDTCs[2].ToString("X"));
                myMessage.DTC = ("NRC " + myDTCs[2].ToString("X"));
            }
        }
        void extractPartNumber()
        {
            string myTemp; 
            string output = "";

            //Check to see that a positive response was received to request DID
            if (myDTCs[1] == 0xFA) 
            {
                //if the full part number is inputted in setup, compare with 
                //full part number recieved by valueCAN 
                //the below condition only checks APP becauase all should be the same length
                if (this.myForm.APP.Length == 11)
                {
                    //myTemp = myDTCs[8].ToString("X");


                    //output will contain the full part number
                    output = output + myDTCs[4].ToString("X");
                    Console.WriteLine(output + " #4");

                    for (int x = 5; x < 10; x++)
                    {
                        string temp = myDTCs[x].ToString("X");

                        if (Convert.ToInt16(temp) < 12 && Convert.ToInt16(temp) > 0)
                        {
                            
                            temp = "0" + temp;
                            Console.WriteLine(temp + " #" + x);
                            output = output + temp;
                        }
                        else
                            output = output + temp;

                    }

                    //formatted output
                    output = output.Substring(0, 3) + "-" + output.Substring(3, 4) + "-" + output.Substring(7, 2);

                    if (myDTCs[3] == 0x01)
                    {
                        Console.WriteLine("APP************************");
                        myMessage.APP = output;
                    }
                    else if (myDTCs[3] == 0x02)
                    {
                        Console.WriteLine("CAL************************");
                        myMessage.CAL = output;
                    }
                    else if (myDTCs[3] == 0x03)
                    {
                        Console.WriteLine("E2P************************");
                        myMessage.E2P = output;
                    }
                    else if (myDTCs[3] == 0x04)
                    {
                        Console.WriteLine("PBL************************");
                        myMessage.PBL = output;
                    }
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
        void updateResult1(string m)
        {
            if (this.lblResult1.InvokeRequired == true)
            {
                this.lblResult1.Invoke((MethodInvoker)delegate()
                {
                    updateResult1(m);
                });
            }
            else
                this.lblResult1.Text = m;
        }
        void updateResult2(string m)
        {
            if (this.lblResult2.InvokeRequired == true)
            {
                this.lblResult2.Invoke((MethodInvoker)delegate()
                {
                    updateResult2(m);
                });
            }
            else
                this.lblResult2.Text = m;
        }
        void updateResult3(string m)
        {
            if (this.lblResult3.InvokeRequired == true)
            {
                this.lblResult3.Invoke((MethodInvoker)delegate()
                {
                    updateResult3(m);
                });
            }
            else
                this.lblResult3.Text = m;
        }
        void updateResult4(string m)
        {
            if (this.lblResult4.InvokeRequired == true)
            {
                this.lblResult4.Invoke((MethodInvoker)delegate()
                {
                    updateResult4(m);
                });
            }
            else
                this.lblResult4.Text = m;
        }
        void checkResults()
        {
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
            if (this.myForm.APP  == this.txtAPP.Text)
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
            if (this.myForm.CAL == this.txtCAL.Text)
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
            if (this.myForm.E2P == this.txtE2P.Text)
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
            if (this.myForm.PBL == this.txtPBL.Text)
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
            if (txtDIDF111Expected.Text == txtDIDF111Received.Text)
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
            if (txtDIDF113Expected.Text == txtDIDF113Received.Text)
            {
                f113 = true;
                txtDIDF113Received.ForeColor = Color.Green;
            }
            else
            {
                f113 = false;
                txtDIDF113Received.ForeColor = Color.Red;
            }

            // Checking if DID F124 expected = received
            if (txtDIDF124Expected.Text == txtDIDF124Received.Text)
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
            if (txtDIDF125Expected.Text == txtDIDF125Received.Text)
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
            if (txtDIDF188Expected.Text == txtDIDF188Received.Text)
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
            if (myForm.RadioType == "LXF")
            {
                if (f111 && f113 && f124 && f125 && f188 && app && cal && pbl && e2p)
                {
                    updateResult1("P");
                    updateResult2("A");
                    updateResult3("S");
                    updateResult4("S");
                    myMessage.Result = "PASS";
                    this.lblResult1.ForeColor = Color.Green;
                    this.lblResult2.ForeColor = Color.Green;
                    this.lblResult3.ForeColor = Color.Green;
                    this.lblResult4.ForeColor = Color.Green; 

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
                    updateResult1("F");
                    updateResult2("A");
                    updateResult3("I");
                    updateResult4("L");
                    myMessage.Result = "FAIL";
                    this.lblResult1.ForeColor = Color.Red;
                    this.lblResult2.ForeColor = Color.Red;
                    this.lblResult3.ForeColor = Color.Red;
                    this.lblResult4.ForeColor = Color.Red;

                    //add a new value to failed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    failedDevices.Add(d);
                    deviceList.Add(d);

                    //updateLabels();
                    
                }
            }
            else
            {
                if (f111 && f113 && f124 && f125 && f188)
                {
                    updateResult1("P");
                    updateResult2("A");
                    updateResult3("S");
                    updateResult4("S");
                    myMessage.Result = "PASS";
                    this.lblResult1.ForeColor = Color.Green;
                    this.lblResult2.ForeColor = Color.Green;
                    this.lblResult3.ForeColor = Color.Green;
                    this.lblResult4.ForeColor = Color.Green;

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
                    updateResult1("F");
                    updateResult2("A");
                    updateResult3("I");
                    updateResult4("L");
                    myMessage.Result = "FAIL";
                    this.lblResult1.ForeColor = Color.Red;
                    this.lblResult2.ForeColor = Color.Red;
                    this.lblResult3.ForeColor = Color.Red;
                    this.lblResult4.ForeColor = Color.Red;

                    //add a new value to failed 
                    string serial = this.txtSerialNumber.ToString();
                    string part = this.txtPartNumber.ToString();


                    device d = new device(serial, part);
                    failedDevices.Add(d);
                    deviceList.Add(d);

                    //updateLabels();
                }
            }
        }
        void writeToExcel()
        {
            Excel.Application excelApp;
            Excel.Workbook excelWorkBook;
            Excel.Worksheet excelWorkSheet;


            string myPath = @"C:\Version Checker\" + myForm.LogFile;
            object misValue = System.Reflection.Missing.Value;

            excelApp = new Excel.Application();

            // If file already exists then open it
            if(File.Exists(myPath))
            {
                // Open the Excel file and get worksheet
                excelWorkBook = excelApp.Workbooks.Open(myPath,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue,misValue);
                excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                // Determine the last used row.
                Excel.Range lastRow = null ;
                int myRow = 0;
                myRow = excelWorkSheet.UsedRange.Count/25 + 1;

                // Update Data
                // Assign column headers
                excelWorkSheet.Cells[myRow, 1] = (myRow-2).ToString();
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
                excelWorkSheet.Cells[myRow, 24] = myMessage.DTC;
                excelWorkSheet.Cells[myRow, 25] = this.lblResult1.Text + this.lblResult2.Text + this.lblResult3.Text + this.lblResult4.Text;


                // Set border around row to thin continous line
                lastRow = excelWorkSheet.get_Range("a"+myRow.ToString(), "y"+myRow.ToString());
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
            }
            // If file does not already exist then create it and add headers
            else
            {
                myPath = @"C:\Version Checker";
                try
                {
                    DirectoryInfo di = Directory.CreateDirectory(myPath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Unable to create directory for Version Checker: " + ex.ToString());
                }
                finally { }

                myPath = @"C:\Version Checker\"+myForm.LogFile;

                try
                {
                    excelWorkBook = excelApp.Workbooks.Add(misValue);
                    excelWorkBook.SaveAs(myPath, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);
                }
                catch
                {
                    MessageBox.Show("Unalbe to start Excel");
                    return;
                }
               
                // Range object used to format Excel file
                Excel.Range chartRange;

                // Area before DIDs
                excelWorkSheet.get_Range("a1", "d1").Merge(false);

                // Make bold, center and color yellow
                chartRange = excelWorkSheet.get_Range("a1", "y2");
                chartRange.Font.Bold = true;
                chartRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Freeze panes for easy viewing in log starting at row 2
                chartRange.Application.ActiveWindow.SplitRow = 2;
                chartRange.Application.ActiveWindow.FreezePanes = true;

                // Set Border for header 1 cells
                chartRange = excelWorkSheet.get_Range("a1", "y1");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                
                // Set Border for header 2 cells
                chartRange = excelWorkSheet.get_Range("a2", "y2");
                chartRange.Borders.Weight = Excel.XlBorderWeight.xlThin;
                chartRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                               
                // DID F111
                excelWorkSheet.get_Range("e1", "f1").Merge(false);
                chartRange = excelWorkSheet.get_Range("e1", "f1");
                chartRange.FormulaR1C1 = "DID F111";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F113
                excelWorkSheet.get_Range("g1", "h1").Merge(false);
                chartRange = excelWorkSheet.get_Range("g1", "h1");
                chartRange.FormulaR1C1 = "DID F113";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F124
                excelWorkSheet.get_Range("i1", "j1").Merge(false);
                chartRange = excelWorkSheet.get_Range("i1", "j1");
                chartRange.FormulaR1C1 = "DID F124";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F125
                excelWorkSheet.get_Range("k1", "l1").Merge(false);
                chartRange = excelWorkSheet.get_Range("k1", "l1");
                if(myForm.RadioType == "LXF")
                    chartRange.FormulaR1C1 = "DID F125";
                else
                    chartRange.FormulaR1C1 = "DID F110";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DID F188
                excelWorkSheet.get_Range("m1", "n1").Merge(false);
                chartRange = excelWorkSheet.get_Range("m1", "n1");
                chartRange.FormulaR1C1 = "DID F188";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // APP
                excelWorkSheet.get_Range("o1", "p1").Merge(false);
                chartRange = excelWorkSheet.get_Range("o1", "p1");
                chartRange.FormulaR1C1 = "APP";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // PBL
                excelWorkSheet.get_Range("q1", "r1").Merge(false);
                chartRange = excelWorkSheet.get_Range("q1", "r1");
                chartRange.FormulaR1C1 = "PBL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // CAL
                excelWorkSheet.get_Range("s1", "t1").Merge(false);
                chartRange = excelWorkSheet.get_Range("s1", "t1");
                chartRange.FormulaR1C1 = "CAL";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // E2P
                excelWorkSheet.get_Range("u1", "v1").Merge(false);
                chartRange = excelWorkSheet.get_Range("u1", "v1");
                chartRange.FormulaR1C1 = "E2P";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;
                // DTCs
                excelWorkSheet.get_Range("w1", "x1").Merge(false);
                chartRange = excelWorkSheet.get_Range("w1", "x1");
                chartRange.FormulaR1C1 = "DTCs";
                chartRange.HorizontalAlignment = 3;
                chartRange.VerticalAlignment = 3;

                // Assign column headers
                excelWorkSheet.Cells[2, 1] = "Line Item";
                excelWorkSheet.Cells[2, 2] = "Date";
                excelWorkSheet.Cells[2, 3] = "Time";
                excelWorkSheet.Cells[2, 4] = "Serial Number";
                excelWorkSheet.Cells[2, 5] = "Expected";
                excelWorkSheet.Cells[2, 6] = "Received";
                excelWorkSheet.Cells[2, 7] = "Expected";
                excelWorkSheet.Cells[2, 8] = "Received";
                excelWorkSheet.Cells[2, 9] = "Expected";
                excelWorkSheet.Cells[2, 10] = "Received";
                excelWorkSheet.Cells[2, 11] = "Expected";
                excelWorkSheet.Cells[2, 12] = "Received";
                excelWorkSheet.Cells[2, 13] = "Expected";
                excelWorkSheet.Cells[2, 14] = "Received";
                excelWorkSheet.Cells[2, 15] = "Expected";
                excelWorkSheet.Cells[2, 16] = "Received";
                excelWorkSheet.Cells[2, 17] = "Expected";
                excelWorkSheet.Cells[2, 18] = "Received";
                excelWorkSheet.Cells[2, 19] = "Expected";
                excelWorkSheet.Cells[2, 20] = "Received";
                excelWorkSheet.Cells[2, 21] = "Expected";
                excelWorkSheet.Cells[2, 22] = "Received";
                excelWorkSheet.Cells[2, 23] = "Count";
                excelWorkSheet.Cells[2, 24] = "List";
                excelWorkSheet.Cells[2, 25] = "Result";
                

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
                

                // Set thick border around entire header
                chartRange = excelWorkSheet.get_Range("a1", "y2");
                chartRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);

                // Update Data
                // Assign column headers
                excelWorkSheet.Cells[3, 1] = "1";
                excelWorkSheet.Cells[3, 2] = System.DateTime.Now.Date.ToShortDateString();
                excelWorkSheet.Cells[3, 3] = System.DateTime.Now.ToShortTimeString();
                excelWorkSheet.Cells[3, 4] = myMessage.SerialNumber;
                excelWorkSheet.Cells[3, 5] = this.txtDIDF111Expected.Text;
                excelWorkSheet.Cells[3, 6] = myMessage.DIDF111;
                excelWorkSheet.Cells[3, 7] = this.txtDIDF113Expected.Text;
                excelWorkSheet.Cells[3, 8] = myMessage.DIDF113;
                excelWorkSheet.Cells[3, 9] = this.txtDIDF124Expected.Text;
                excelWorkSheet.Cells[3, 10] = myMessage.DIDF124;
                excelWorkSheet.Cells[3, 11] = this.txtDIDF125Expected.Text;
                excelWorkSheet.Cells[3, 12] = myMessage.DIDF125;
                excelWorkSheet.Cells[3, 13] = this.txtDIDF188Expected.Text;
                excelWorkSheet.Cells[3, 14] = myMessage.DIDF188;
                excelWorkSheet.Cells[3, 15] = this.myForm.APP;
                excelWorkSheet.Cells[3, 16] = myMessage.APP;
                excelWorkSheet.Cells[3, 17] = this.myForm.PBL;
                excelWorkSheet.Cells[3, 18] = myMessage.PBL;
                excelWorkSheet.Cells[3, 19] = this.myForm.CAL;
                excelWorkSheet.Cells[3, 20] = myMessage.CAL;
                excelWorkSheet.Cells[3, 21] = this.myForm.E2P;
                excelWorkSheet.Cells[3, 22] = myMessage.E2P;
                excelWorkSheet.Cells[3, 23] = myMessage.DTCCount;
                excelWorkSheet.Cells[3, 24] = myMessage.DTC;
                excelWorkSheet.Cells[3, 25] = this.lblResult1.Text + this.lblResult2.Text + this.lblResult3.Text + this.lblResult4.Text;

                // Set border around row to thin continous line
                chartRange = excelWorkSheet.get_Range("a3", "y3");
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
            }

            //excelApp.Visible = true;
            excelWorkBook.Save();
            excelWorkBook.Close(true, misValue, misValue);
            excelApp.Quit();

            releaseObject(excelApp);
            releaseObject(excelWorkBook);
            releaseObject(excelWorkSheet);           
        }
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
        #endregion    
    
        #region Events
        void OnDIDF111Change(object source)
        {
            this.txtDIDF111Expected.Text = myForm.DIDF111;
        }
        void OnDIDF124Change(object source)
        {
            this.txtDIDF124Expected.Text = myForm.DIDF124;
        }
        void OnDIDF125Change(object source)
        {
            this.txtDIDF125Expected.Text = myForm.DIDF125;
        }
        void OnDIDF188Change(object source)
        {
            this.txtDIDF188Expected.Text = myForm.DIDF188;
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

                myDID = "F110";
                this.lblF125.Text = "DID F110";
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

                myDID = "F110";
                this.lblF125.Text = "DID F110";
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
        void OnLogFileChange(object source)
        {
            this.myCounter = 1;
            updateCounter("Log File");
        }
        void OnKeyPress(object sender, KeyEventArgs e)
        {
            string myPath = @"C:\Version Checker\" + myForm.LogFile;
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
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            
            transmitFlowControl(0x727);
            transmitFlowControl(0x7A7);
            updateLabels();
            updateToolstrip();
                
                            
            if (loadCompleate)
            {
                checkWireless();
                if (loadCompleate)
                    honeywellCONN();

                
            }  
            
            
        }
        #endregion


        //Determines if scanner is connected at launch of program 
        //synced with timer unlike original methods 


        private void checkWireless()
        {
            timer1.Stop();

            if (wiScannerApiOpen)
            {
                short numberOfScanners; // Number of scanners expect to be used
                int[] connectedScannerIDList = new int[255];
                // List of scanner IDs to be returned
                string outXML; //Scanner details output
                ccs.GetScanners(out numberOfScanners, connectedScannerIDList, out outXML, out status);
               
                if (numberOfScanners > 0)
                {
                    wiScanner_status.Text = "PASS";
                    wiScanner_status.ForeColor = Color.Green;
                }
                else
                {

                    wiScanner_status.Text = "FAIL";
                    wiScanner_status.ForeColor = Color.Red;
                }
            }
            else
            {
                MessageBox.Show("The wireless scanner needs to be initialized. Would you like to initialize it?"); 
                //TODO
            }


             


            timer1.Start();
        }

        private void openWIScannerAPI()
        {
            //TODO
        }

        private void honeywellCONN()
        {

            
               if(!comPort.IsOpen) {

                   timer1.Stop();

                   try
                   {
                       comPort.Open();
                       MessageBox.Show("Comms: " + comPort.IsOpen.ToString());
                       comPort_status.Text = "OPEN";
                       comPort_status.ForeColor = Color.Green;
                       lblHoneywellResult.Text = "PASS";
                       lblHoneywellResult.ForeColor = Color.Green;
                       lblReady.ForeColor = Color.Black;
                       lblReady.Text = "Ready to scan.";
                       checkHoneywell = false;
                   }
                   //nested try catch block is for honeywell bug
                   //TODO: add messages which allows user to know where the problem is
                   catch(Exception ex)
                   {
                       MessageBox.Show(ex.Message, "Honeywell Scanner Error");
                   }
                

                timer1.Start(); 
               
            } 
             

        }
        private void lblE2P_Click(object sender, EventArgs e)
        {
        
        }

        private void txtE2P_TextChanged(object sender, EventArgs e)
        {
        
        }

        private void grpResult_Enter(object sender, EventArgs e)
        {

        }

        private void btnLogFile_Click(object sender, EventArgs e)
        {
            //on the log button click add the new device to the list and see if there is a duplicate
            string serialNum = this.txtSerialNumber.Text.ToString(); 
            string partNum = this.txtPartNumber.Text.ToString();
            bool duplicate = false;
            
            DateTime dt = new DateTime();
            device d = new device(serialNum, partNum);

            if (txtPartNumber.Text == null)
            {

                MessageBox.Show("Please enter a part number.");
            }  
            else if(txtSerialNumber.Text == null) 
            { 
                MessageBox.Show("Please enter a serial number.");
            } 
            else if(txtSerialNumber.Text == null && txtPartNumber.Text == null) 
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


                updateLabels();  
            }
        }

        private void updateLabels()
        {
            this.failedLabel.Text = "Failed: " + this.failedDevices.Count.ToString();
            this.passedLabel.Text = "Passed: " + passedDevices.Count;
            this.duplicateLabel.Text = "Duplicates: " + duplicateDevices.Count;
            this.totalLabel.Text = "Total: " + deviceList.Count;
        }

        //EXPERIMENTAL FORMS

        //open duplicates form
        private void duplicatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        //open passed form
        private void passedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myPassedForm.StartPosition = FormStartPosition.CenterScreen;
            myPassedForm.ShowDialog();
            
        }

        //open failed form
        private void failedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            myFailedForm.StartPosition = FormStartPosition.CenterScreen;
            myFailedForm.setData(failedDevices);
            myFailedForm.ShowDialog();
        }

        private void motatrolaScannerSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            

        }

        public void updateToolstrip()
        {
            int precentage = 0;

            if (myForm.setupDone)
            {
                timer1.Stop();

                if (myForm.trackNum != null)
                    this.trackingStripStatusLabel1.Text = "Tracking #: " + myForm.trackNum.ToString();

                if (myForm.partNumber != null)
                    this.partStripStatusLabel3.Text = "Part #" + myForm.partNumber.ToString();

                if (myForm.quantity != null)
                    this.quantityStripStatusLabel2.Text = "Quantity: " + myForm.quantity.ToString();

                if (duplicateDevices.Count != 0)
                    this.duplicatesStripStatusLabel5.Text = "Duplicates: " + duplicateDevices.Count.ToString();

                if (myForm.LogFile != null)
                    this.fileStripStatusLabel4.Text = "File: " + myForm.LogFile;

                if (Convert.ToInt32(myForm.quantity) != 0)
                {
                    precentage = deviceList.Count / Convert.ToInt32(myForm.quantity);
                    this.precentageStripStatusLabel1.Text = "Precentage Complete: " + precentage + "%";
                }

                timer1.Start();
            }
            
        }

        private void wiredHoneywellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            honeyForm.StartPosition = FormStartPosition.CenterScreen;
           // honeyForm.setApiBool();
            honeyForm.setComms(comPort.IsOpen);
         //   honeyForm.setConnectionBool();
            honeyForm.ShowDialog(); 
        }

        private void wirelessMotorolaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scanForm.StartPosition = FormStartPosition.CenterScreen;
            scanForm.ShowDialog();
        }

        private void comPort_status_Click(object sender, EventArgs e)
        {

        }

        //runs on a looped thread to constantly check if they are connected or not
        

    }
}

