using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text; 

namespace ICSNeoCSharp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.GroupBox GroupBox4;
		internal System.Windows.Forms.CheckBox chkAutoRead;
		internal System.Windows.Forms.Label lblReadErrors;
		internal System.Windows.Forms.Label lblReadCount;
		internal System.Windows.Forms.ListBox lstMessage;
		internal System.Windows.Forms.Button cmdReceive;
		internal System.Windows.Forms.ListBox lstErrorHolder;
		internal System.Windows.Forms.Button cmdGetErrors;
		internal System.Windows.Forms.GroupBox GroupBox5;
		internal System.Windows.Forms.TextBox txtIPAddress;
		internal System.Windows.Forms.RadioButton RadioButton1;
		internal System.Windows.Forms.TextBox txtServerPort;
		internal System.Windows.Forms.RadioButton optSerialDevice;
		internal System.Windows.Forms.Button cmdConnect;
		internal System.Windows.Forms.Button cmdDisconnect;
		internal System.Windows.Forms.TextBox txtPortNum;
		internal System.Windows.Forms.RadioButton optUsbDevice;
		internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.RadioButton optTCPIP;
		internal System.Windows.Forms.Label Label13;
		internal System.Windows.Forms.CheckBox chkStartServer;
		internal System.Windows.Forms.GroupBox GroupBox2;
		internal System.Windows.Forms.CheckBox chkExtendedID;
		internal System.Windows.Forms.TextBox txtDataByte11;
		internal System.Windows.Forms.TextBox txtDataByte10;
		internal System.Windows.Forms.TextBox txtDataByte9;
		internal System.Windows.Forms.ComboBox lstNumberOfBytes;
		internal System.Windows.Forms.Label Label14;
		internal System.Windows.Forms.ComboBox lstNetwork;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox txtDataByte2;
		internal System.Windows.Forms.Button cmdTransmit;
		internal System.Windows.Forms.TextBox txtDataByte3;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.TextBox txtDataByte4;
		internal System.Windows.Forms.TextBox txtDataByte5;
		internal System.Windows.Forms.TextBox txtDataByte6;
		internal System.Windows.Forms.TextBox txtDataByte7;
		internal System.Windows.Forms.TextBox txtArbID;
		internal System.Windows.Forms.TextBox txtDataByte8;
		internal System.Windows.Forms.TextBox txtDataByte1;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.GroupBox GroupBox6;
		internal System.Windows.Forms.Label Label18;
		internal System.Windows.Forms.Label Label17;
		internal System.Windows.Forms.Label Label16;
		internal System.Windows.Forms.TextBox txtOverflowCount;
		internal System.Windows.Forms.TextBox txtBufferMax;
		internal System.Windows.Forms.TextBox txtBufferCount;
		internal System.Windows.Forms.Button cmdPerformance;
		public System.Windows.Forms.Timer Timer1;
		internal System.Windows.Forms.GroupBox GroupBox3;
		internal System.Windows.Forms.ComboBox lstMFNetwork;
		internal System.Windows.Forms.Button cmdDisable;
		internal System.Windows.Forms.Button cmdSetupAndEnable;
		internal System.Windows.Forms.Label Label12;
		internal System.Windows.Forms.Label Label11;
		internal System.Windows.Forms.TextBox txtFlowControl;
		internal System.Windows.Forms.TextBox txtFirstFrame;
		internal System.Windows.Forms.ListBox lstStatusItems;
		internal System.Windows.Forms.Button cmdClearStatus;
		internal System.Windows.Forms.Button cmdReadStatus;
		internal System.Windows.Forms.GroupBox GroupBox1;
		internal System.Windows.Forms.Label Label5;
		internal System.Windows.Forms.Button cmdVersion;
		internal System.Windows.Forms.Button cmdFindAllDevice;
		internal System.Windows.Forms.Label Label6;
		internal System.Windows.Forms.ListBox lstUsbDevices;
		internal System.Windows.Forms.Label Label7;
		internal System.Windows.Forms.ListBox lstCommDevices;
		internal System.Windows.Forms.GroupBox Group2;
		internal System.Windows.Forms.Button cmdSet250K;
		internal System.Windows.Forms.Button cmdSet500K;
		internal System.Windows.Forms.Label Label15;
		internal System.Windows.Forms.Button cmdSendHSCanInfo;
		internal System.Windows.Forms.TextBox txtCNF3;
		internal System.Windows.Forms.TextBox txtCNF2;
		internal System.Windows.Forms.TextBox txtCNF1;
		internal System.Windows.Forms.Label Label10;
		internal System.Windows.Forms.Label Label9;
		internal System.Windows.Forms.Label Label8;
		internal System.Windows.Forms.Button cmdGetConfig;
		internal System.Windows.Forms.ListBox lstConfigInformation;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.GroupBox4 = new System.Windows.Forms.GroupBox();
			this.chkAutoRead = new System.Windows.Forms.CheckBox();
			this.lblReadErrors = new System.Windows.Forms.Label();
			this.lblReadCount = new System.Windows.Forms.Label();
			this.lstMessage = new System.Windows.Forms.ListBox();
			this.cmdReceive = new System.Windows.Forms.Button();
			this.lstErrorHolder = new System.Windows.Forms.ListBox();
			this.cmdGetErrors = new System.Windows.Forms.Button();
			this.GroupBox5 = new System.Windows.Forms.GroupBox();
			this.txtIPAddress = new System.Windows.Forms.TextBox();
			this.RadioButton1 = new System.Windows.Forms.RadioButton();
			this.txtServerPort = new System.Windows.Forms.TextBox();
			this.optSerialDevice = new System.Windows.Forms.RadioButton();
			this.cmdConnect = new System.Windows.Forms.Button();
			this.cmdDisconnect = new System.Windows.Forms.Button();
			this.txtPortNum = new System.Windows.Forms.TextBox();
			this.optUsbDevice = new System.Windows.Forms.RadioButton();
			this.Label1 = new System.Windows.Forms.Label();
			this.optTCPIP = new System.Windows.Forms.RadioButton();
			this.Label13 = new System.Windows.Forms.Label();
			this.chkStartServer = new System.Windows.Forms.CheckBox();
			this.GroupBox2 = new System.Windows.Forms.GroupBox();
			this.chkExtendedID = new System.Windows.Forms.CheckBox();
			this.txtDataByte11 = new System.Windows.Forms.TextBox();
			this.txtDataByte10 = new System.Windows.Forms.TextBox();
			this.txtDataByte9 = new System.Windows.Forms.TextBox();
			this.lstNumberOfBytes = new System.Windows.Forms.ComboBox();
			this.Label14 = new System.Windows.Forms.Label();
			this.lstNetwork = new System.Windows.Forms.ComboBox();
			this.Label3 = new System.Windows.Forms.Label();
			this.txtDataByte2 = new System.Windows.Forms.TextBox();
			this.cmdTransmit = new System.Windows.Forms.Button();
			this.txtDataByte3 = new System.Windows.Forms.TextBox();
			this.Label4 = new System.Windows.Forms.Label();
			this.txtDataByte4 = new System.Windows.Forms.TextBox();
			this.txtDataByte5 = new System.Windows.Forms.TextBox();
			this.txtDataByte6 = new System.Windows.Forms.TextBox();
			this.txtDataByte7 = new System.Windows.Forms.TextBox();
			this.txtArbID = new System.Windows.Forms.TextBox();
			this.txtDataByte8 = new System.Windows.Forms.TextBox();
			this.txtDataByte1 = new System.Windows.Forms.TextBox();
			this.Label2 = new System.Windows.Forms.Label();
			this.GroupBox6 = new System.Windows.Forms.GroupBox();
			this.Label18 = new System.Windows.Forms.Label();
			this.Label17 = new System.Windows.Forms.Label();
			this.Label16 = new System.Windows.Forms.Label();
			this.txtOverflowCount = new System.Windows.Forms.TextBox();
			this.txtBufferMax = new System.Windows.Forms.TextBox();
			this.txtBufferCount = new System.Windows.Forms.TextBox();
			this.cmdPerformance = new System.Windows.Forms.Button();
			this.Timer1 = new System.Windows.Forms.Timer(this.components);
			this.GroupBox3 = new System.Windows.Forms.GroupBox();
			this.lstMFNetwork = new System.Windows.Forms.ComboBox();
			this.cmdDisable = new System.Windows.Forms.Button();
			this.cmdSetupAndEnable = new System.Windows.Forms.Button();
			this.Label12 = new System.Windows.Forms.Label();
			this.Label11 = new System.Windows.Forms.Label();
			this.txtFlowControl = new System.Windows.Forms.TextBox();
			this.txtFirstFrame = new System.Windows.Forms.TextBox();
			this.lstStatusItems = new System.Windows.Forms.ListBox();
			this.cmdClearStatus = new System.Windows.Forms.Button();
			this.cmdReadStatus = new System.Windows.Forms.Button();
			this.GroupBox1 = new System.Windows.Forms.GroupBox();
			this.Label5 = new System.Windows.Forms.Label();
			this.cmdVersion = new System.Windows.Forms.Button();
			this.cmdFindAllDevice = new System.Windows.Forms.Button();
			this.Label6 = new System.Windows.Forms.Label();
			this.lstUsbDevices = new System.Windows.Forms.ListBox();
			this.Label7 = new System.Windows.Forms.Label();
			this.lstCommDevices = new System.Windows.Forms.ListBox();
			this.Group2 = new System.Windows.Forms.GroupBox();
			this.cmdSet250K = new System.Windows.Forms.Button();
			this.cmdSet500K = new System.Windows.Forms.Button();
			this.Label15 = new System.Windows.Forms.Label();
			this.cmdSendHSCanInfo = new System.Windows.Forms.Button();
			this.txtCNF3 = new System.Windows.Forms.TextBox();
			this.txtCNF2 = new System.Windows.Forms.TextBox();
			this.txtCNF1 = new System.Windows.Forms.TextBox();
			this.Label10 = new System.Windows.Forms.Label();
			this.Label9 = new System.Windows.Forms.Label();
			this.Label8 = new System.Windows.Forms.Label();
			this.cmdGetConfig = new System.Windows.Forms.Button();
			this.lstConfigInformation = new System.Windows.Forms.ListBox();
			this.GroupBox4.SuspendLayout();
			this.GroupBox5.SuspendLayout();
			this.GroupBox2.SuspendLayout();
			this.GroupBox6.SuspendLayout();
			this.GroupBox3.SuspendLayout();
			this.GroupBox1.SuspendLayout();
			this.Group2.SuspendLayout();
			this.SuspendLayout();
			// 
			// GroupBox4
			// 
			this.GroupBox4.Controls.Add(this.chkAutoRead);
			this.GroupBox4.Controls.Add(this.lblReadErrors);
			this.GroupBox4.Controls.Add(this.lblReadCount);
			this.GroupBox4.Controls.Add(this.lstMessage);
			this.GroupBox4.Controls.Add(this.cmdReceive);
			this.GroupBox4.Controls.Add(this.lstErrorHolder);
			this.GroupBox4.Controls.Add(this.cmdGetErrors);
			this.GroupBox4.Location = new System.Drawing.Point(0, 232);
			this.GroupBox4.Name = "GroupBox4";
			this.GroupBox4.Size = new System.Drawing.Size(536, 232);
			this.GroupBox4.TabIndex = 48;
			this.GroupBox4.TabStop = false;
			this.GroupBox4.Text = "Receive Message";
			// 
			// chkAutoRead
			// 
			this.chkAutoRead.Location = new System.Drawing.Point(176, 16);
			this.chkAutoRead.Name = "chkAutoRead";
			this.chkAutoRead.Size = new System.Drawing.Size(80, 16);
			this.chkAutoRead.TabIndex = 25;
			this.chkAutoRead.Text = "AutoRead";
			this.chkAutoRead.CheckedChanged += new System.EventHandler(this.chkAutoRead_CheckedChanged);
			// 
			// lblReadErrors
			// 
			this.lblReadErrors.Location = new System.Drawing.Point(408, 16);
			this.lblReadErrors.Name = "lblReadErrors";
			this.lblReadErrors.Size = new System.Drawing.Size(112, 16);
			this.lblReadErrors.TabIndex = 24;
			this.lblReadErrors.Text = "Number Errors : ";
			// 
			// lblReadCount
			// 
			this.lblReadCount.Location = new System.Drawing.Point(264, 16);
			this.lblReadCount.Name = "lblReadCount";
			this.lblReadCount.Size = new System.Drawing.Size(144, 16);
			this.lblReadCount.TabIndex = 23;
			this.lblReadCount.Text = "Number Read : ";
			// 
			// lstMessage
			// 
			this.lstMessage.Location = new System.Drawing.Point(8, 40);
			this.lstMessage.Name = "lstMessage";
			this.lstMessage.Size = new System.Drawing.Size(520, 95);
			this.lstMessage.TabIndex = 19;
			// 
			// cmdReceive
			// 
			this.cmdReceive.Location = new System.Drawing.Point(8, 16);
			this.cmdReceive.Name = "cmdReceive";
			this.cmdReceive.Size = new System.Drawing.Size(160, 24);
			this.cmdReceive.TabIndex = 20;
			this.cmdReceive.Text = "Get Messages";
			this.cmdReceive.Click += new System.EventHandler(this.cmdReceive_Click);
			// 
			// lstErrorHolder
			// 
			this.lstErrorHolder.HorizontalScrollbar = true;
			this.lstErrorHolder.Location = new System.Drawing.Point(8, 160);
			this.lstErrorHolder.Name = "lstErrorHolder";
			this.lstErrorHolder.ScrollAlwaysVisible = true;
			this.lstErrorHolder.Size = new System.Drawing.Size(520, 69);
			this.lstErrorHolder.TabIndex = 21;
			// 
			// cmdGetErrors
			// 
			this.cmdGetErrors.Location = new System.Drawing.Point(8, 136);
			this.cmdGetErrors.Name = "cmdGetErrors";
			this.cmdGetErrors.Size = new System.Drawing.Size(520, 24);
			this.cmdGetErrors.TabIndex = 22;
			this.cmdGetErrors.Text = "Get Errors";
			this.cmdGetErrors.Click += new System.EventHandler(this.cmdGetErrors_Click);
			// 
			// GroupBox5
			// 
			this.GroupBox5.Controls.Add(this.txtIPAddress);
			this.GroupBox5.Controls.Add(this.RadioButton1);
			this.GroupBox5.Controls.Add(this.txtServerPort);
			this.GroupBox5.Controls.Add(this.optSerialDevice);
			this.GroupBox5.Controls.Add(this.cmdConnect);
			this.GroupBox5.Controls.Add(this.cmdDisconnect);
			this.GroupBox5.Controls.Add(this.txtPortNum);
			this.GroupBox5.Controls.Add(this.optUsbDevice);
			this.GroupBox5.Controls.Add(this.Label1);
			this.GroupBox5.Controls.Add(this.optTCPIP);
			this.GroupBox5.Controls.Add(this.Label13);
			this.GroupBox5.Controls.Add(this.chkStartServer);
			this.GroupBox5.Location = new System.Drawing.Point(0, 0);
			this.GroupBox5.Name = "GroupBox5";
			this.GroupBox5.Size = new System.Drawing.Size(536, 96);
			this.GroupBox5.TabIndex = 49;
			this.GroupBox5.TabStop = false;
			this.GroupBox5.Text = "Connection";
			// 
			// txtIPAddress
			// 
			this.txtIPAddress.Location = new System.Drawing.Point(264, 48);
			this.txtIPAddress.Name = "txtIPAddress";
			this.txtIPAddress.Size = new System.Drawing.Size(104, 20);
			this.txtIPAddress.TabIndex = 36;
			this.txtIPAddress.Text = "255.255.255.255";
			// 
			// RadioButton1
			// 
			this.RadioButton1.Checked = true;
			this.RadioButton1.Location = new System.Drawing.Point(200, 72);
			this.RadioButton1.Name = "RadioButton1";
			this.RadioButton1.Size = new System.Drawing.Size(136, 16);
			this.RadioButton1.TabIndex = 40;
			this.RadioButton1.TabStop = true;
			this.RadioButton1.Text = "Connect to first device";
			// 
			// txtServerPort
			// 
			this.txtServerPort.Location = new System.Drawing.Point(472, 16);
			this.txtServerPort.Name = "txtServerPort";
			this.txtServerPort.Size = new System.Drawing.Size(48, 20);
			this.txtServerPort.TabIndex = 39;
			this.txtServerPort.Text = "4500";
			// 
			// optSerialDevice
			// 
			this.optSerialDevice.Location = new System.Drawing.Point(200, 32);
			this.optSerialDevice.Name = "optSerialDevice";
			this.optSerialDevice.Size = new System.Drawing.Size(160, 16);
			this.optSerialDevice.TabIndex = 24;
			this.optSerialDevice.Text = "Serial (NeoVI, and VCAN)";
			// 
			// cmdConnect
			// 
			this.cmdConnect.Location = new System.Drawing.Point(8, 16);
			this.cmdConnect.Name = "cmdConnect";
			this.cmdConnect.Size = new System.Drawing.Size(104, 24);
			this.cmdConnect.TabIndex = 3;
			this.cmdConnect.Text = "Connect";
			this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
			// 
			// cmdDisconnect
			// 
			this.cmdDisconnect.CausesValidation = false;
			this.cmdDisconnect.Location = new System.Drawing.Point(8, 48);
			this.cmdDisconnect.Name = "cmdDisconnect";
			this.cmdDisconnect.Size = new System.Drawing.Size(104, 24);
			this.cmdDisconnect.TabIndex = 4;
			this.cmdDisconnect.Text = "Disconnect";
			this.cmdDisconnect.Click += new System.EventHandler(this.cmdDisconnect_Click);
			// 
			// txtPortNum
			// 
			this.txtPortNum.CausesValidation = false;
			this.txtPortNum.Location = new System.Drawing.Point(120, 48);
			this.txtPortNum.Name = "txtPortNum";
			this.txtPortNum.Size = new System.Drawing.Size(48, 20);
			this.txtPortNum.TabIndex = 2;
			this.txtPortNum.Text = "1";
			// 
			// optUsbDevice
			// 
			this.optUsbDevice.Location = new System.Drawing.Point(200, 16);
			this.optUsbDevice.Name = "optUsbDevice";
			this.optUsbDevice.Size = new System.Drawing.Size(104, 16);
			this.optUsbDevice.TabIndex = 23;
			this.optUsbDevice.Text = "USB (NeoVI)";
			// 
			// Label1
			// 
			this.Label1.Location = new System.Drawing.Point(120, 24);
			this.Label1.Name = "Label1";
			this.Label1.Size = new System.Drawing.Size(72, 16);
			this.Label1.TabIndex = 1;
			this.Label1.Text = "Port Number";
			// 
			// optTCPIP
			// 
			this.optTCPIP.BackColor = System.Drawing.SystemColors.Control;
			this.optTCPIP.Location = new System.Drawing.Point(200, 48);
			this.optTCPIP.Name = "optTCPIP";
			this.optTCPIP.Size = new System.Drawing.Size(64, 16);
			this.optTCPIP.TabIndex = 35;
			this.optTCPIP.Text = "TCP/IP";
			// 
			// Label13
			// 
			this.Label13.Location = new System.Drawing.Point(368, 48);
			this.Label13.Name = "Label13";
			this.Label13.Size = new System.Drawing.Size(64, 16);
			this.Label13.TabIndex = 37;
			this.Label13.Text = "IP Address";
			// 
			// chkStartServer
			// 
			this.chkStartServer.Location = new System.Drawing.Point(360, 16);
			this.chkStartServer.Name = "chkStartServer";
			this.chkStartServer.Size = new System.Drawing.Size(112, 16);
			this.chkStartServer.TabIndex = 38;
			this.chkStartServer.Text = "Start Stop Server";
			this.chkStartServer.CheckedChanged += new System.EventHandler(this.chkStartServer_CheckedChanged);
			// 
			// GroupBox2
			// 
			this.GroupBox2.Controls.Add(this.chkExtendedID);
			this.GroupBox2.Controls.Add(this.txtDataByte11);
			this.GroupBox2.Controls.Add(this.txtDataByte10);
			this.GroupBox2.Controls.Add(this.txtDataByte9);
			this.GroupBox2.Controls.Add(this.lstNumberOfBytes);
			this.GroupBox2.Controls.Add(this.Label14);
			this.GroupBox2.Controls.Add(this.lstNetwork);
			this.GroupBox2.Controls.Add(this.Label3);
			this.GroupBox2.Controls.Add(this.txtDataByte2);
			this.GroupBox2.Controls.Add(this.cmdTransmit);
			this.GroupBox2.Controls.Add(this.txtDataByte3);
			this.GroupBox2.Controls.Add(this.Label4);
			this.GroupBox2.Controls.Add(this.txtDataByte4);
			this.GroupBox2.Controls.Add(this.txtDataByte5);
			this.GroupBox2.Controls.Add(this.txtDataByte6);
			this.GroupBox2.Controls.Add(this.txtDataByte7);
			this.GroupBox2.Controls.Add(this.txtArbID);
			this.GroupBox2.Controls.Add(this.txtDataByte8);
			this.GroupBox2.Controls.Add(this.txtDataByte1);
			this.GroupBox2.Controls.Add(this.Label2);
			this.GroupBox2.Location = new System.Drawing.Point(0, 96);
			this.GroupBox2.Name = "GroupBox2";
			this.GroupBox2.Size = new System.Drawing.Size(536, 136);
			this.GroupBox2.TabIndex = 47;
			this.GroupBox2.TabStop = false;
			this.GroupBox2.Text = "Transmit Messages";
			// 
			// chkExtendedID
			// 
			this.chkExtendedID.Location = new System.Drawing.Point(320, 40);
			this.chkExtendedID.Name = "chkExtendedID";
			this.chkExtendedID.Size = new System.Drawing.Size(160, 16);
			this.chkExtendedID.TabIndex = 26;
			this.chkExtendedID.Text = "Send Extended ID";
			// 
			// txtDataByte11
			// 
			this.txtDataByte11.Location = new System.Drawing.Point(400, 64);
			this.txtDataByte11.Name = "txtDataByte11";
			this.txtDataByte11.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte11.TabIndex = 25;
			this.txtDataByte11.Text = "00";
			// 
			// txtDataByte10
			// 
			this.txtDataByte10.Location = new System.Drawing.Point(368, 64);
			this.txtDataByte10.Name = "txtDataByte10";
			this.txtDataByte10.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte10.TabIndex = 24;
			this.txtDataByte10.Text = "00";
			// 
			// txtDataByte9
			// 
			this.txtDataByte9.Location = new System.Drawing.Point(336, 64);
			this.txtDataByte9.Name = "txtDataByte9";
			this.txtDataByte9.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte9.TabIndex = 23;
			this.txtDataByte9.Text = "00";
			// 
			// lstNumberOfBytes
			// 
			this.lstNumberOfBytes.Items.AddRange(new object[] {
																  "0",
																  "1",
																  "2",
																  "3",
																  "4",
																  "5",
																  "6",
																  "7",
																  "8",
																  "9",
																  "10",
																  "11"});
			this.lstNumberOfBytes.Location = new System.Drawing.Point(8, 96);
			this.lstNumberOfBytes.Name = "lstNumberOfBytes";
			this.lstNumberOfBytes.Size = new System.Drawing.Size(48, 21);
			this.lstNumberOfBytes.TabIndex = 22;
			this.lstNumberOfBytes.Text = "ComboBox1";
			// 
			// Label14
			// 
			this.Label14.Location = new System.Drawing.Point(80, 16);
			this.Label14.Name = "Label14";
			this.Label14.Size = new System.Drawing.Size(48, 16);
			this.Label14.TabIndex = 21;
			this.Label14.Text = "Network";
			// 
			// lstNetwork
			// 
			this.lstNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstNetwork.Items.AddRange(new object[] {
															"HSCAN",
															"MSCAN",
															"SWCAN",
															"LSFTCAN",
															"FORD SCP",
															"J1708",
															"Aux Net",
															"J1850 VPW",
															"ISO"});
			this.lstNetwork.Location = new System.Drawing.Point(80, 32);
			this.lstNetwork.Name = "lstNetwork";
			this.lstNetwork.Size = new System.Drawing.Size(104, 21);
			this.lstNetwork.TabIndex = 20;
			// 
			// Label3
			// 
			this.Label3.Location = new System.Drawing.Point(232, 40);
			this.Label3.Name = "Label3";
			this.Label3.Size = new System.Drawing.Size(64, 16);
			this.Label3.TabIndex = 15;
			this.Label3.Text = "Data Bytes";
			// 
			// txtDataByte2
			// 
			this.txtDataByte2.Location = new System.Drawing.Point(112, 64);
			this.txtDataByte2.Name = "txtDataByte2";
			this.txtDataByte2.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte2.TabIndex = 7;
			this.txtDataByte2.Text = "00";
			// 
			// cmdTransmit
			// 
			this.cmdTransmit.Location = new System.Drawing.Point(80, 88);
			this.cmdTransmit.Name = "cmdTransmit";
			this.cmdTransmit.Size = new System.Drawing.Size(352, 24);
			this.cmdTransmit.TabIndex = 16;
			this.cmdTransmit.Text = "Transmit";
			this.cmdTransmit.Click += new System.EventHandler(this.cmdTransmit_Click);
			// 
			// txtDataByte3
			// 
			this.txtDataByte3.Location = new System.Drawing.Point(144, 64);
			this.txtDataByte3.Name = "txtDataByte3";
			this.txtDataByte3.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte3.TabIndex = 8;
			this.txtDataByte3.Text = "00";
			// 
			// Label4
			// 
			this.Label4.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.Label4.Location = new System.Drawing.Point(8, 64);
			this.Label4.Name = "Label4";
			this.Label4.Size = new System.Drawing.Size(64, 32);
			this.Label4.TabIndex = 18;
			this.Label4.Text = "Number of Data Bytes";
			// 
			// txtDataByte4
			// 
			this.txtDataByte4.Location = new System.Drawing.Point(176, 64);
			this.txtDataByte4.Name = "txtDataByte4";
			this.txtDataByte4.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte4.TabIndex = 9;
			this.txtDataByte4.Text = "00";
			// 
			// txtDataByte5
			// 
			this.txtDataByte5.Location = new System.Drawing.Point(208, 64);
			this.txtDataByte5.Name = "txtDataByte5";
			this.txtDataByte5.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte5.TabIndex = 10;
			this.txtDataByte5.Text = "00";
			// 
			// txtDataByte6
			// 
			this.txtDataByte6.Location = new System.Drawing.Point(240, 64);
			this.txtDataByte6.Name = "txtDataByte6";
			this.txtDataByte6.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte6.TabIndex = 11;
			this.txtDataByte6.Text = "00";
			// 
			// txtDataByte7
			// 
			this.txtDataByte7.Location = new System.Drawing.Point(272, 64);
			this.txtDataByte7.Name = "txtDataByte7";
			this.txtDataByte7.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte7.TabIndex = 12;
			this.txtDataByte7.Text = "00";
			// 
			// txtArbID
			// 
			this.txtArbID.Location = new System.Drawing.Point(8, 32);
			this.txtArbID.Name = "txtArbID";
			this.txtArbID.Size = new System.Drawing.Size(40, 20);
			this.txtArbID.TabIndex = 5;
			this.txtArbID.Text = "101";
			// 
			// txtDataByte8
			// 
			this.txtDataByte8.Location = new System.Drawing.Point(304, 64);
			this.txtDataByte8.Name = "txtDataByte8";
			this.txtDataByte8.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte8.TabIndex = 13;
			this.txtDataByte8.Text = "00";
			// 
			// txtDataByte1
			// 
			this.txtDataByte1.Location = new System.Drawing.Point(80, 64);
			this.txtDataByte1.Name = "txtDataByte1";
			this.txtDataByte1.Size = new System.Drawing.Size(32, 20);
			this.txtDataByte1.TabIndex = 6;
			this.txtDataByte1.Text = "00";
			// 
			// Label2
			// 
			this.Label2.Location = new System.Drawing.Point(8, 16);
			this.Label2.Name = "Label2";
			this.Label2.Size = new System.Drawing.Size(40, 16);
			this.Label2.TabIndex = 14;
			this.Label2.Text = "Arb ID";
			// 
			// GroupBox6
			// 
			this.GroupBox6.Controls.Add(this.Label18);
			this.GroupBox6.Controls.Add(this.Label17);
			this.GroupBox6.Controls.Add(this.Label16);
			this.GroupBox6.Controls.Add(this.txtOverflowCount);
			this.GroupBox6.Controls.Add(this.txtBufferMax);
			this.GroupBox6.Controls.Add(this.txtBufferCount);
			this.GroupBox6.Controls.Add(this.cmdPerformance);
			this.GroupBox6.Location = new System.Drawing.Point(648, 464);
			this.GroupBox6.Name = "GroupBox6";
			this.GroupBox6.Size = new System.Drawing.Size(280, 136);
			this.GroupBox6.TabIndex = 50;
			this.GroupBox6.TabStop = false;
			this.GroupBox6.Text = "Performance";
			// 
			// Label18
			// 
			this.Label18.Location = new System.Drawing.Point(112, 64);
			this.Label18.Name = "Label18";
			this.Label18.Size = new System.Drawing.Size(88, 16);
			this.Label18.TabIndex = 6;
			this.Label18.Text = "Buffer Max";
			// 
			// Label17
			// 
			this.Label17.Location = new System.Drawing.Point(112, 96);
			this.Label17.Name = "Label17";
			this.Label17.Size = new System.Drawing.Size(88, 16);
			this.Label17.TabIndex = 5;
			this.Label17.Text = "Overflow Count";
			// 
			// Label16
			// 
			this.Label16.Location = new System.Drawing.Point(112, 32);
			this.Label16.Name = "Label16";
			this.Label16.Size = new System.Drawing.Size(80, 16);
			this.Label16.TabIndex = 4;
			this.Label16.Text = "Buffer Count";
			// 
			// txtOverflowCount
			// 
			this.txtOverflowCount.Location = new System.Drawing.Point(200, 88);
			this.txtOverflowCount.Name = "txtOverflowCount";
			this.txtOverflowCount.Size = new System.Drawing.Size(64, 20);
			this.txtOverflowCount.TabIndex = 3;
			this.txtOverflowCount.Text = "N/A";
			// 
			// txtBufferMax
			// 
			this.txtBufferMax.Location = new System.Drawing.Point(200, 56);
			this.txtBufferMax.Name = "txtBufferMax";
			this.txtBufferMax.Size = new System.Drawing.Size(64, 20);
			this.txtBufferMax.TabIndex = 2;
			this.txtBufferMax.Text = "N/A";
			// 
			// txtBufferCount
			// 
			this.txtBufferCount.Location = new System.Drawing.Point(200, 24);
			this.txtBufferCount.Name = "txtBufferCount";
			this.txtBufferCount.Size = new System.Drawing.Size(64, 20);
			this.txtBufferCount.TabIndex = 1;
			this.txtBufferCount.Text = "N/A";
			// 
			// cmdPerformance
			// 
			this.cmdPerformance.Location = new System.Drawing.Point(8, 40);
			this.cmdPerformance.Name = "cmdPerformance";
			this.cmdPerformance.Size = new System.Drawing.Size(88, 72);
			this.cmdPerformance.TabIndex = 0;
			this.cmdPerformance.Text = "Get Performance";
			this.cmdPerformance.Click += new System.EventHandler(this.cmdPerformance_Click);
			// 
			// Timer1
			// 
			this.Timer1.Interval = 1000;
			this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
			// 
			// GroupBox3
			// 
			this.GroupBox3.Controls.Add(this.lstMFNetwork);
			this.GroupBox3.Controls.Add(this.cmdDisable);
			this.GroupBox3.Controls.Add(this.cmdSetupAndEnable);
			this.GroupBox3.Controls.Add(this.Label12);
			this.GroupBox3.Controls.Add(this.Label11);
			this.GroupBox3.Controls.Add(this.txtFlowControl);
			this.GroupBox3.Controls.Add(this.txtFirstFrame);
			this.GroupBox3.Controls.Add(this.lstStatusItems);
			this.GroupBox3.Controls.Add(this.cmdClearStatus);
			this.GroupBox3.Controls.Add(this.cmdReadStatus);
			this.GroupBox3.Location = new System.Drawing.Point(0, 464);
			this.GroupBox3.Name = "GroupBox3";
			this.GroupBox3.Size = new System.Drawing.Size(648, 136);
			this.GroupBox3.TabIndex = 46;
			this.GroupBox3.TabStop = false;
			this.GroupBox3.Text = "Multiframe Rx";
			// 
			// lstMFNetwork
			// 
			this.lstMFNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstMFNetwork.Items.AddRange(new object[] {
															  "HSCAN",
															  "MSCAN",
															  "SWCAN",
															  "LSFTCAN"});
			this.lstMFNetwork.Location = new System.Drawing.Point(8, 24);
			this.lstMFNetwork.Name = "lstMFNetwork";
			this.lstMFNetwork.Size = new System.Drawing.Size(120, 21);
			this.lstMFNetwork.TabIndex = 9;
			// 
			// cmdDisable
			// 
			this.cmdDisable.Location = new System.Drawing.Point(520, 80);
			this.cmdDisable.Name = "cmdDisable";
			this.cmdDisable.Size = new System.Drawing.Size(104, 48);
			this.cmdDisable.TabIndex = 1;
			this.cmdDisable.Text = "Disable";
			this.cmdDisable.Click += new System.EventHandler(this.cmdDisable_Click);
			// 
			// cmdSetupAndEnable
			// 
			this.cmdSetupAndEnable.Location = new System.Drawing.Point(520, 24);
			this.cmdSetupAndEnable.Name = "cmdSetupAndEnable";
			this.cmdSetupAndEnable.Size = new System.Drawing.Size(104, 48);
			this.cmdSetupAndEnable.TabIndex = 0;
			this.cmdSetupAndEnable.Text = "Setup And Enable";
			this.cmdSetupAndEnable.Click += new System.EventHandler(this.cmdSetupAndEnable_Click);
			// 
			// Label12
			// 
			this.Label12.Location = new System.Drawing.Point(144, 80);
			this.Label12.Name = "Label12";
			this.Label12.Size = new System.Drawing.Size(72, 24);
			this.Label12.TabIndex = 8;
			this.Label12.Text = "Flow Control ID (Hex)";
			// 
			// Label11
			// 
			this.Label11.Location = new System.Drawing.Point(144, 24);
			this.Label11.Name = "Label11";
			this.Label11.Size = new System.Drawing.Size(64, 24);
			this.Label11.TabIndex = 7;
			this.Label11.Text = "First Frame Filter (Hex)";
			// 
			// txtFlowControl
			// 
			this.txtFlowControl.Location = new System.Drawing.Point(144, 104);
			this.txtFlowControl.Name = "txtFlowControl";
			this.txtFlowControl.Size = new System.Drawing.Size(40, 20);
			this.txtFlowControl.TabIndex = 6;
			this.txtFlowControl.Text = "641";
			// 
			// txtFirstFrame
			// 
			this.txtFirstFrame.Location = new System.Drawing.Point(144, 48);
			this.txtFirstFrame.Name = "txtFirstFrame";
			this.txtFirstFrame.Size = new System.Drawing.Size(40, 20);
			this.txtFirstFrame.TabIndex = 5;
			this.txtFirstFrame.Text = "777";
			// 
			// lstStatusItems
			// 
			this.lstStatusItems.Location = new System.Drawing.Point(216, 24);
			this.lstStatusItems.Name = "lstStatusItems";
			this.lstStatusItems.Size = new System.Drawing.Size(296, 108);
			this.lstStatusItems.TabIndex = 4;
			// 
			// cmdClearStatus
			// 
			this.cmdClearStatus.Location = new System.Drawing.Point(8, 96);
			this.cmdClearStatus.Name = "cmdClearStatus";
			this.cmdClearStatus.Size = new System.Drawing.Size(120, 32);
			this.cmdClearStatus.TabIndex = 3;
			this.cmdClearStatus.Text = "Clear Status";
			this.cmdClearStatus.Click += new System.EventHandler(this.cmdClearStatus_Click);
			// 
			// cmdReadStatus
			// 
			this.cmdReadStatus.Location = new System.Drawing.Point(8, 56);
			this.cmdReadStatus.Name = "cmdReadStatus";
			this.cmdReadStatus.Size = new System.Drawing.Size(120, 32);
			this.cmdReadStatus.TabIndex = 2;
			this.cmdReadStatus.Text = "Read Status";
			this.cmdReadStatus.Click += new System.EventHandler(this.cmdReadStatus_Click);
			// 
			// GroupBox1
			// 
			this.GroupBox1.Controls.Add(this.Label5);
			this.GroupBox1.Controls.Add(this.cmdVersion);
			this.GroupBox1.Controls.Add(this.cmdFindAllDevice);
			this.GroupBox1.Controls.Add(this.Label6);
			this.GroupBox1.Controls.Add(this.lstUsbDevices);
			this.GroupBox1.Controls.Add(this.Label7);
			this.GroupBox1.Controls.Add(this.lstCommDevices);
			this.GroupBox1.Location = new System.Drawing.Point(536, 0);
			this.GroupBox1.Name = "GroupBox1";
			this.GroupBox1.Size = new System.Drawing.Size(192, 464);
			this.GroupBox1.TabIndex = 44;
			this.GroupBox1.TabStop = false;
			this.GroupBox1.Text = "Neo Information";
			// 
			// Label5
			// 
			this.Label5.Location = new System.Drawing.Point(8, 16);
			this.Label5.Name = "Label5";
			this.Label5.Size = new System.Drawing.Size(128, 16);
			this.Label5.TabIndex = 27;
			this.Label5.Text = "ICSNeo40.dll version";
			// 
			// cmdVersion
			// 
			this.cmdVersion.Location = new System.Drawing.Point(8, 32);
			this.cmdVersion.Name = "cmdVersion";
			this.cmdVersion.Size = new System.Drawing.Size(176, 24);
			this.cmdVersion.TabIndex = 26;
			this.cmdVersion.Text = "Version";
			this.cmdVersion.Click += new System.EventHandler(this.cmdVersion_Click);
			// 
			// cmdFindAllDevice
			// 
			this.cmdFindAllDevice.Location = new System.Drawing.Point(8, 64);
			this.cmdFindAllDevice.Name = "cmdFindAllDevice";
			this.cmdFindAllDevice.Size = new System.Drawing.Size(176, 24);
			this.cmdFindAllDevice.TabIndex = 25;
			this.cmdFindAllDevice.Text = "find All  Devices";
			this.cmdFindAllDevice.Click += new System.EventHandler(this.cmdFindAllDevice_Click);
			// 
			// Label6
			// 
			this.Label6.Location = new System.Drawing.Point(8, 96);
			this.Label6.Name = "Label6";
			this.Label6.Size = new System.Drawing.Size(96, 16);
			this.Label6.TabIndex = 30;
			this.Label6.Text = "USB Devices";
			// 
			// lstUsbDevices
			// 
			this.lstUsbDevices.Location = new System.Drawing.Point(8, 112);
			this.lstUsbDevices.Name = "lstUsbDevices";
			this.lstUsbDevices.Size = new System.Drawing.Size(176, 160);
			this.lstUsbDevices.TabIndex = 28;
			// 
			// Label7
			// 
			this.Label7.Location = new System.Drawing.Point(8, 280);
			this.Label7.Name = "Label7";
			this.Label7.Size = new System.Drawing.Size(120, 16);
			this.Label7.TabIndex = 31;
			this.Label7.Text = "Comm Baised devices";
			// 
			// lstCommDevices
			// 
			this.lstCommDevices.Location = new System.Drawing.Point(8, 296);
			this.lstCommDevices.Name = "lstCommDevices";
			this.lstCommDevices.Size = new System.Drawing.Size(176, 160);
			this.lstCommDevices.TabIndex = 29;
			// 
			// Group2
			// 
			this.Group2.Controls.Add(this.cmdSet250K);
			this.Group2.Controls.Add(this.cmdSet500K);
			this.Group2.Controls.Add(this.Label15);
			this.Group2.Controls.Add(this.cmdSendHSCanInfo);
			this.Group2.Controls.Add(this.txtCNF3);
			this.Group2.Controls.Add(this.txtCNF2);
			this.Group2.Controls.Add(this.txtCNF1);
			this.Group2.Controls.Add(this.Label10);
			this.Group2.Controls.Add(this.Label9);
			this.Group2.Controls.Add(this.Label8);
			this.Group2.Controls.Add(this.cmdGetConfig);
			this.Group2.Controls.Add(this.lstConfigInformation);
			this.Group2.Location = new System.Drawing.Point(728, 0);
			this.Group2.Name = "Group2";
			this.Group2.Size = new System.Drawing.Size(200, 464);
			this.Group2.TabIndex = 45;
			this.Group2.TabStop = false;
			this.Group2.Text = "Neo Config Information";
			// 
			// cmdSet250K
			// 
			this.cmdSet250K.Location = new System.Drawing.Point(120, 312);
			this.cmdSet250K.Name = "cmdSet250K";
			this.cmdSet250K.Size = new System.Drawing.Size(72, 32);
			this.cmdSet250K.TabIndex = 11;
			this.cmdSet250K.Text = "Set HS CAN 250K";
			this.cmdSet250K.Click += new System.EventHandler(this.cmdSet250K_Click);
			// 
			// cmdSet500K
			// 
			this.cmdSet500K.Location = new System.Drawing.Point(120, 280);
			this.cmdSet500K.Name = "cmdSet500K";
			this.cmdSet500K.Size = new System.Drawing.Size(72, 32);
			this.cmdSet500K.TabIndex = 10;
			this.cmdSet500K.Text = "Set HS CAN 500K";
			this.cmdSet500K.Click += new System.EventHandler(this.cmdSet500K_Click);
			// 
			// Label15
			// 
			this.Label15.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(128)));
			this.Label15.Location = new System.Drawing.Point(8, 400);
			this.Label15.Name = "Label15";
			this.Label15.Size = new System.Drawing.Size(184, 56);
			this.Label15.TabIndex = 9;
			this.Label15.Text = " TIP: use neoVI explorer to get the proper CNFs. ValueCAN CNFs are different than" +
				" neoVI due to different CAN Chip speeds.";
			// 
			// cmdSendHSCanInfo
			// 
			this.cmdSendHSCanInfo.Location = new System.Drawing.Point(16, 352);
			this.cmdSendHSCanInfo.Name = "cmdSendHSCanInfo";
			this.cmdSendHSCanInfo.Size = new System.Drawing.Size(96, 40);
			this.cmdSendHSCanInfo.TabIndex = 8;
			this.cmdSendHSCanInfo.Text = "Send HS CAN Information";
			this.cmdSendHSCanInfo.Click += new System.EventHandler(this.cmdSendHSCanInfo_Click);
			// 
			// txtCNF3
			// 
			this.txtCNF3.Location = new System.Drawing.Point(56, 328);
			this.txtCNF3.Name = "txtCNF3";
			this.txtCNF3.Size = new System.Drawing.Size(56, 20);
			this.txtCNF3.TabIndex = 7;
			this.txtCNF3.Text = "5";
			// 
			// txtCNF2
			// 
			this.txtCNF2.Location = new System.Drawing.Point(56, 304);
			this.txtCNF2.Name = "txtCNF2";
			this.txtCNF2.Size = new System.Drawing.Size(56, 20);
			this.txtCNF2.TabIndex = 6;
			this.txtCNF2.Text = "B8";
			// 
			// txtCNF1
			// 
			this.txtCNF1.Location = new System.Drawing.Point(56, 280);
			this.txtCNF1.Name = "txtCNF1";
			this.txtCNF1.Size = new System.Drawing.Size(56, 20);
			this.txtCNF1.TabIndex = 5;
			this.txtCNF1.TabStop = false;
			this.txtCNF1.Text = "1";
			// 
			// Label10
			// 
			this.Label10.Location = new System.Drawing.Point(16, 328);
			this.Label10.Name = "Label10";
			this.Label10.Size = new System.Drawing.Size(40, 16);
			this.Label10.TabIndex = 4;
			this.Label10.Text = "CNF3";
			// 
			// Label9
			// 
			this.Label9.Location = new System.Drawing.Point(16, 304);
			this.Label9.Name = "Label9";
			this.Label9.Size = new System.Drawing.Size(40, 16);
			this.Label9.TabIndex = 3;
			this.Label9.Text = "CNF2";
			// 
			// Label8
			// 
			this.Label8.Location = new System.Drawing.Point(16, 280);
			this.Label8.Name = "Label8";
			this.Label8.Size = new System.Drawing.Size(40, 16);
			this.Label8.TabIndex = 2;
			this.Label8.Text = "CNF1";
			// 
			// cmdGetConfig
			// 
			this.cmdGetConfig.Cursor = System.Windows.Forms.Cursors.Arrow;
			this.cmdGetConfig.Location = new System.Drawing.Point(8, 32);
			this.cmdGetConfig.Name = "cmdGetConfig";
			this.cmdGetConfig.Size = new System.Drawing.Size(184, 24);
			this.cmdGetConfig.TabIndex = 1;
			this.cmdGetConfig.Text = "Get Configuration";
			this.cmdGetConfig.Click += new System.EventHandler(this.cmdGetConfig_Click);
			// 
			// lstConfigInformation
			// 
			this.lstConfigInformation.Location = new System.Drawing.Point(8, 56);
			this.lstConfigInformation.Name = "lstConfigInformation";
			this.lstConfigInformation.Size = new System.Drawing.Size(184, 212);
			this.lstConfigInformation.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(928, 604);
			this.Controls.Add(this.GroupBox5);
			this.Controls.Add(this.GroupBox2);
			this.Controls.Add(this.GroupBox6);
			this.Controls.Add(this.GroupBox3);
			this.Controls.Add(this.GroupBox1);
			this.Controls.Add(this.Group2);
			this.Controls.Add(this.GroupBox4);
			this.Name = "Form1";
			this.Text = "Form1";
			this.GroupBox4.ResumeLayout(false);
			this.GroupBox5.ResumeLayout(false);
			this.GroupBox2.ResumeLayout(false);
			this.GroupBox6.ResumeLayout(false);
			this.GroupBox3.ResumeLayout(false);
			this.GroupBox1.ResumeLayout(false);
			this.Group2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		int m_hObject = 0;   //Handle of Device
		bool m_bPortOpen = false;  //Port open status
		icsSpyMessage[] stMessages = new icsSpyMessage[2000];   //TempSpace for messages

		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		private void cmdConnect_Click(object sender, System.EventArgs e)
		{
			byte[] bNetworkIDs = new byte[16] {0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
			byte[] bSCPIDs = new byte[256];     // Array of SCP functional IDs passed to the driver
			int iPortNumber = 0;			
			int iReturnVal = 0;					//iReturn value tells status of the function call
			int iIPMSB = 0;
			int iIPLSB = 0;
			iPortNumber = Convert.ToInt32(txtPortNum.Text);   //Convert type of Textbox Value

			
			int[] iDevices = new int[127];  //Array for the device numbers
			int[] iSerialNumbers = new int[127]; //Araay for serial numbers of attached devices
			int[] iOpenedStatus = new int[127]; //Array of the status of the driver
			int iNumDevices = 0;  //Storage for the number of devices
			int[] iCommPortNumbers = new int[127]; //Array of Comm Port numbers in use


			//Exit function if Port is open
			if (m_bPortOpen==true)
			{
				return;
			}

			if (optUsbDevice.Checked == true) //USB device (neoVI green)
			{
				iReturnVal = icsNeoDll.icsneoOpenPortEx(iPortNumber, Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_USB_BULK ), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),iIPMSB , iIPLSB, 1, ref bNetworkIDs[0],ref m_hObject);
			}
			else if (optSerialDevice.Checked == true)  //RS232 or USB Serial device
			{
				iIPLSB = 57600;
				iReturnVal = icsNeoDll.icsneoOpenPortEx(iPortNumber, Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_RS232), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),iIPMSB ,iIPLSB,1, ref bNetworkIDs[0], ref m_hObject);
			}
			else if (optTCPIP.Checked == true)  //TCPIP connection
			{
				//Calculate the IP Address
				iReturnVal = Convert.ToInt32(icsNeoDll.CreateIPParts(txtIPAddress.Text,ref iIPMSB,ref iIPLSB));
				//Open the port
				iReturnVal = icsNeoDll.icsneoOpenPortEx(Convert.ToInt32(txtPortNum.Text), Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_TCPIP), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD), iIPMSB, iIPLSB, 1, ref bNetworkIDs[0],ref m_hObject);
			}
			else  //Auto Detect code
			{
				

				//This option connects to the first device found.  Serial ports checked first, then USB. 
				iReturnVal = icsNeoDll.icsneoFindAllUSBDevices(Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),1,ref iDevices[0],ref iSerialNumbers[0],ref iOpenedStatus[0],ref iNumDevices);
				if (Convert.ToBoolean(iReturnVal == 1 ) && Convert.ToBoolean(iNumDevices == 0))
				{
	                iIPLSB = 57600;
					iReturnVal = icsNeoDll.icsneoFindAllCOMDevices(Convert.ToInt32 (eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD), 1,0,0,ref iDevices[0],ref iCommPortNumbers[0], ref iSerialNumbers[0],ref iNumDevices);
					iReturnVal = icsNeoDll.icsneoOpenPortEx(iCommPortNumbers[0], Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_RS232), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),iIPMSB ,iIPLSB,1, ref bNetworkIDs[0], ref m_hObject);
					txtPortNum.Text = Convert.ToString(iCommPortNumbers[0]);
					if (iReturnVal == 1) optSerialDevice.Checked = true;
				}
				else
				{
	                iIPLSB = 0;
					iReturnVal = icsNeoDll.icsneoOpenPortEx(1, Convert.ToInt32(ePORT_TYPE.NEOVI_COMMTYPE_USB_BULK ), Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),iIPMSB , iIPLSB, 1, ref bNetworkIDs[0],ref m_hObject);
					if (iReturnVal == 1) optUsbDevice.Checked = true;
				}



			}

			

			if (iReturnVal == 0) // test the returned result
			{
				MessageBox.Show("Problem Opening Port");   //Error, Show message
			}
			else 
			{
				MessageBox.Show("Port opened OK!");
				m_bPortOpen = true;   //Set Port Opened Flag
			}
		}

		private void cmdFindAllDevice_Click(object sender, System.EventArgs e)
		{
			int lResult = 0;  //Storage for Result of Function call
			int[] iDevices = new int[127];  //Array for the device numbers
			int[] iSerialNumbers = new int[127]; //Araay for serial numbers of attached devices
			int[] iOpenedStatus = new int[127]; //Array of the status of the driver
			int iNumDevices = 0;  //Storage for the number of devices
			int[] iCommPortNumbers = new int[127]; //Array of Comm Port numbers in use
			int Counter = 0;  //Counter for Counting things

			//function call for Finding all of the USB devices
			lResult = icsNeoDll.icsneoFindAllUSBDevices(Convert.ToInt32(eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD),1,ref iDevices[0],ref iSerialNumbers[0],ref iOpenedStatus[0],ref iNumDevices);
			//check the status of Function call
			if(lResult==1)
			{	
				//Fill list box with device findings
				for(Counter=0;Counter<127; Counter++)
				{
					lstUsbDevices.Items.Add("USB #" + Convert.ToString(iDevices[Counter]) + " SN-" + Convert.ToString(iSerialNumbers[Counter]) + " State-" + Convert.ToString(iOpenedStatus[Counter]));
				}
			}
			else
			{
				//Display error if cound not find anything
				MessageBox.Show("Could Not Find anything");
			}

			//Call function for Finding all Comm deivces
			lResult = icsNeoDll.icsneoFindAllCOMDevices(Convert.ToInt32 (eDRIVER_TYPE.INTREPIDCS_DRIVER_STANDARD), 1,0,0,ref iDevices[0],ref iCommPortNumbers[0], ref iSerialNumbers[0],ref iNumDevices);
            
			//Check the status of the funciton call
			if(lResult==1)
			{
				//Fill in list box with device findings
				for(Counter=0;Counter<127; Counter++)
				{
					lstCommDevices.Items.Add("Device Type-" + Convert.ToString(iDevices[Counter]) + " SN-" + Convert.ToString(iSerialNumbers[Counter]) + " Port #" + Convert.ToString(iCommPortNumbers[Counter]));
				}
			}
			else
			{
				//display error box if could not find anything
				MessageBox.Show("Could Not Find anything");
			}
		}

		private void cmdGetErrors_Click(object sender, System.EventArgs e)
		{
			int iResult = 0;  //Storage for Result of Call
			int[] iErrors = new int[600];  //Array for Error Numbers
			int iNumberOfErrors  = 0;  // Storage for number of errors
			int iCount= 0;   //Counter
			int iSeverity =0;  //tells the Severity of Error
			int iMaxLengthShort = 0;  //Tells Max length of Error String
			int iMaxLengthLong = 0;	//Tells Max Length of Error String
			int lRestart = 0;  //tells if a restart is needed
			StringBuilder sErrorShort = new StringBuilder(256);  //String for Error
			StringBuilder sErrorLong = new StringBuilder(256);  //String for Error

			iMaxLengthShort = 1; //Set initial conditions
			iMaxLengthLong = 1; //Set initial conditions
			// Read Out the errors
			iResult = icsNeoDll.icsneoGetErrorMessages(m_hObject,ref iErrors[0],ref iNumberOfErrors);

			// Test the returned result
			if(iResult == 0)
			{
				MessageBox.Show ("Problem Reading Errors");			
			}
			else
			{
				if(iNumberOfErrors != 0)
				{
					for(iCount=0;iCount< iNumberOfErrors;iCount++)
					{
						//Get Text Description of the Error
						iResult = icsNeoDll.icsneoGetErrorInfo(iErrors[iCount], sErrorShort, sErrorLong ,ref  iMaxLengthShort , ref iMaxLengthLong, ref iSeverity,ref lRestart);
						lstErrorHolder.Items.Add (sErrorShort + " - Description " + sErrorLong + " - Errornum: " + iErrors[iCount]);
					}
				}
			}
		}

		private void cmdVersion_Click(object sender, System.EventArgs e)
		{
				cmdVersion.Text = "Version " + Convert.ToString(icsNeoDll.icsneoGetDLLVersion());

		}

		private void cmdDisconnect_Click(object sender, System.EventArgs e)
		{
			int iResult = 0;    //Space to Store Result of Function Call
			int iNumOfErrors = 0;	//Storage for the Number of Errors
    

			// Has the uset open neoVI yet?;
			if (m_bPortOpen==false) 
			{
				MessageBox.Show("neoVI not opened");
				return; // do not read messages if we haven't opened neoVI yet
			}

			Timer1.Enabled = false;

			//Call for Closing the port
			iResult = icsNeoDll.icsneoClosePort(m_hObject, ref iNumOfErrors);
			//Check the Result to see if successful
			if (iResult==0) 
			{
				MessageBox.Show("Problem Closing Port");  //Show error 
			} 
			else 
			{
				MessageBox.Show("Port Closed OK");
			}
			// set the portopen flag to closed
			m_bPortOpen = false;    //Set flag for other functions
		}

		private void chkStartServer_CheckedChanged(object sender, System.EventArgs e)
		{
			int iStatus;

			if (chkStartServer.Checked == true)
				iStatus = icsNeoDll.icsneoStartSockServer(m_hObject, Convert.ToInt32(txtServerPort.Text)); // start the socket server
			else
				iStatus = icsNeoDll.icsneoStopSockServer(m_hObject); // stop the socket server
		}

		private void cmdGetConfig_Click(object sender, System.EventArgs e)
		{
			byte[] bConfigBytes = new byte[1024];  //Storage for Data Bytes from Device
			int iNumBytes = 1204;    //Storage for Number of Bytes
			int lResult;      //Storage for Result of Called Function
			int Counter;

			//Clear listbox
			if (m_bPortOpen == false) return;

			lstConfigInformation.Items.Clear();

			//Call Get Configuration 
			lResult = icsNeoDll.icsneoGetConfiguration(m_hObject, ref bConfigBytes[0],ref iNumBytes);

			//Fill ListBox with Data From function Call
			for(Counter=0;Counter<1024;Counter++)
			{
				lstConfigInformation.Items.Add("Byte Number-" + Counter + " Byte Data-" + bConfigBytes[Counter]);
			}
		}

		private void cmdSendHSCanInfo_Click(object sender, System.EventArgs e)
		{
			byte[] bConfigBytes= new byte[1024];    //Storage for Data bytes from device
			int iNumBytes = 0;    //Storage for Number of Bytes
			int lResult = 0;    //Storage for Result of Called Function
			int Counter;
			int lNumberOfErrors = 0;  //Storage for Number of Errors Received
	
			//Clear ListBox
			lstConfigInformation.Items.Clear();
	
			//Call Get Configuration
			lResult = icsNeoDll.icsneoGetConfiguration(m_hObject, ref bConfigBytes[0],ref iNumBytes);
	
			//Fill Listbox with Data From Function Call
			for(Counter=0; Counter<1024;Counter++)
			{
				lstConfigInformation.Items.Add("Byte Number-" + Counter + " Byte Data-" + bConfigBytes[Counter]);
			}
	
			//Set HS CAN Baud Rate Information
			bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF1)] = Convert.ToByte(ConvertFromHex(txtCNF1.Text));
			bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF2)] = Convert.ToByte(ConvertFromHex (txtCNF2.Text));
			bConfigBytes[Convert.ToInt32(icsConfigSetup.NEO_CFG_MPIC_HS_CAN_CNF3)] = Convert.ToByte(ConvertFromHex(txtCNF3.Text));
	
			//Call Sned configuration 
			icsNeoDll.icsneoEnableNetworkCom(m_hObject,0);
			lResult = icsNeoDll.icsneoSendConfiguration(m_hObject, ref bConfigBytes[0], iNumBytes);
			icsNeoDll.icsneoEnableNetworkCom(m_hObject,0);


			// make sure the read was successful
			if(lResult==0)
			{
				MessageBox.Show("Problem sending configuration");
				lResult = icsNeoDll.icsneoClosePort(m_hObject,ref lNumberOfErrors);
			}
			else
			{
				MessageBox.Show("Configuration Successfull");
			}
		}

		private void cmdSet500K_Click(object sender, System.EventArgs e)
		{
			txtCNF1.Text = "1";
			txtCNF2.Text = "B8";
			txtCNF3.Text = "5";
		}

		private void cmdSet250K_Click(object sender, System.EventArgs e)
		{
			txtCNF1.Text = "3";
			txtCNF2.Text = "B8";
			txtCNF3.Text = "5";
		}

		private string ConvertToHex(string sInput)
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
			return sOut ;
		} 

		private int ConvertFromHex(string num) 
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
			return Convert.ToInt32(  uiHex);
		}

		private void cmdPerformance_Click(object sender, System.EventArgs e)
		{
			int lResult=0;
			int iBufferCount=0;
			int iBufferMax=0;
			int iOverFlowCount=0;
			int iReserved1=0;
			int iReserved2=0;
			int iReserved3=0;
			int iReserved4=0;
			int iReserved5=0;
	
			if (m_bPortOpen==false)
			{
				MessageBox.Show("Port is not open.");
				return;
			}
	
			lResult = icsNeoDll.icsneoGetPerformanceParameters(m_hObject, ref iBufferCount, ref iBufferMax, ref iOverFlowCount, ref iReserved1, ref iReserved2, ref iReserved3, ref iReserved4, ref iReserved5);
	
			if(lResult!=1)
			{
			    txtBufferCount.Text = "N/A";
				txtBufferMax.Text = "N/A";
				txtOverflowCount.Text = "N/A";
				MessageBox.Show("Problem Getting Performance Parameters");
			}
			else
			{
				txtBufferCount.Text = Convert.ToString(iBufferCount);
				txtBufferMax.Text = Convert.ToString(iBufferMax);
				txtOverflowCount.Text = Convert.ToString(iOverFlowCount);
			}
		}

		private icsSpyMessage CreateEmptyStructure()
		{
			icsSpyMessage InputMessage;
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


		private icsSpyMessageJ1850 CreateEmptyStructureJ1850()
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
			InputMessage.Header1=0;  //Holds (up to 3 byte 1850 header or 29 bit CAN header)
			InputMessage.Header2=0;
			InputMessage.Header3=0;
			InputMessage.Header4=0;
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

		private void cmdTransmit_Click(object sender, System.EventArgs e)
		{
			long lResult;
			icsSpyMessage stMessagesTx;
			icsSpyMessageJ1850 stJMsg;
			long lNetworkID;
			long lNumberBytes;

			stMessagesTx = CreateEmptyStructure();
			stJMsg = CreateEmptyStructureJ1850();
			// Has the uset open neoVI yet?;
			if (m_bPortOpen==false) 
			{
	            MessageBox.Show("neoVI not opened");
				return; // do not read messages if we haven't opened neoVI yet
			}
	
			// Read the Network we will transmit on (indicated by lstNetwork ListBox)
			lNetworkID = lstNetwork.SelectedIndex +1;
	
			// Is this a CAN network or a J1850/ISO one?
			if(lNetworkID <= 4) // its a CAN network
			{
	            // load the message structure
				stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);
				if (chkExtendedID.Checked == true)
				{
	                //Make id Extended
					stMessagesTx.StatusBitField = Convert.ToInt16(eDATA_STATUS_BITFIELD_1.SPY_STATUS_XTD_FRAME); 
				}
				else
				{
	                //Use Normal ID
					stMessagesTx.StatusBitField = 0;
				}
				stMessagesTx.ArbIDOrHeader = ConvertFromHex(txtArbID.Text);            // The ArbID
				stMessagesTx.NumberBytesData = Convert.ToByte(lstNumberOfBytes.SelectedIndex);         // The number of Data Bytes
				if (stMessagesTx.NumberBytesData > 8) stMessagesTx.NumberBytesData = 8; // You can only have 8 databytes with CAN
				// Load all of the data bytes in the structure
	
				stMessagesTx.Data1 = Convert.ToByte(ConvertFromHex(txtDataByte1.Text));
				stMessagesTx.Data2 = Convert.ToByte(ConvertFromHex(txtDataByte2.Text));
				stMessagesTx.Data3 = Convert.ToByte(ConvertFromHex(txtDataByte3.Text));
				stMessagesTx.Data4 = Convert.ToByte(ConvertFromHex(txtDataByte4.Text));
				stMessagesTx.Data5 = Convert.ToByte(ConvertFromHex(txtDataByte5.Text));
				stMessagesTx.Data6 = Convert.ToByte(ConvertFromHex(txtDataByte6.Text));
				stMessagesTx.Data7 = Convert.ToByte(ConvertFromHex(txtDataByte7.Text));
				stMessagesTx.Data8 = Convert.ToByte(ConvertFromHex(txtDataByte8.Text));
			}
			else // its not a CAN network
			{
	            // load the message structure (the J1850 struture type)
				lNumberBytes = lstNumberOfBytes.SelectedIndex;      // how many bytes
				if (lNumberBytes > 3) // how many header (max 3 header bytes) and data bytes
				{
					stJMsg.NumberBytesHeader = 3;
					stJMsg.NumberBytesData = Convert.ToByte(lNumberBytes - 3);
				}
				else
				{
	                stJMsg.NumberBytesHeader = Convert.ToByte(lNumberBytes);
					stJMsg.NumberBytesData = 0;
				}
				stMessagesTx.NetworkID = Convert.ToByte(lNetworkID);
				// for all the header bytes
				stJMsg.Header1 = Convert.ToByte(ConvertFromHex(txtDataByte1.Text));
				stJMsg.Header2 = Convert.ToByte(ConvertFromHex(txtDataByte2.Text));
				stJMsg.Header3 = Convert.ToByte(ConvertFromHex(txtDataByte3.Text));
	
	            // for all the data bytes
				stJMsg.Data1 = Convert.ToByte(ConvertFromHex(txtDataByte4.Text));
				stJMsg.Data2 = Convert.ToByte(ConvertFromHex(txtDataByte5.Text));
				stJMsg.Data3 = Convert.ToByte(ConvertFromHex(txtDataByte6.Text));
				stJMsg.Data4 = Convert.ToByte(ConvertFromHex(txtDataByte7.Text));
				stJMsg.Data5 = Convert.ToByte(ConvertFromHex(txtDataByte8.Text));
				stJMsg.Data6 = Convert.ToByte(ConvertFromHex(txtDataByte9.Text));
				stJMsg.Data7 = Convert.ToByte(ConvertFromHex(txtDataByte10.Text));
				stJMsg.Data8 = Convert.ToByte(ConvertFromHex(txtDataByte11.Text));
	
				// copy the J1850 message structure into the structure that will be sent				
				ConvertJ1850toCAN( ref stMessagesTx, ref stJMsg);
	
			}
	
			// Transmit the assembled message
			lResult=icsNeoDll.icsneoTxMessages(m_hObject,ref stMessagesTx,Convert.ToByte(lNetworkID),0);
			// Test the returned result
			if (lResult!=1) 
			{
				MessageBox.Show("Problem Transmitting Message");
			}
		}

		private void ConvertCANtoJ1850Message( ref icsSpyMessage icsCANStruct, ref icsSpyMessageJ1850 icsJ1850Struct)
		{
			icsJ1850Struct.StatusBitField = icsCANStruct.StatusBitField;
			icsJ1850Struct.StatusBitField2 = icsCANStruct.StatusBitField2;
			icsJ1850Struct.TimeHardware = icsCANStruct.TimeHardware;
			icsJ1850Struct.TimeHardware2 = icsCANStruct.TimeHardware2;
			icsJ1850Struct.TimeSystem = icsCANStruct.TimeSystem;
			icsJ1850Struct.TimeSystem2 = icsCANStruct.TimeSystem2;
			icsJ1850Struct.TimeStampHardwareID = icsCANStruct.TimeStampHardwareID;
			icsJ1850Struct.TimeStampSystemID = icsCANStruct.TimeStampSystemID;
			icsJ1850Struct.NetworkID = icsCANStruct.NetworkID;
			icsJ1850Struct.NodeID = icsCANStruct.NodeID;
			icsJ1850Struct.Protocol = icsCANStruct.Protocol;
			icsJ1850Struct.MessagePieceID = icsCANStruct.MessagePieceID;
			icsJ1850Struct.ColorID = icsCANStruct.ColorID;
			icsJ1850Struct.NumberBytesHeader = icsCANStruct.NumberBytesHeader;
			icsJ1850Struct.NumberBytesData = icsCANStruct.NumberBytesData;
			icsJ1850Struct.DescriptionID = icsCANStruct.DescriptionID;
			icsJ1850Struct.Header1 = Convert.ToByte(icsCANStruct.ArbIDOrHeader & 0xff);
			icsJ1850Struct.Header2 = Convert.ToByte((0xFF00 & icsCANStruct.ArbIDOrHeader) / 256);
			icsJ1850Struct.Header3 = Convert.ToByte((0xFF0000 & icsCANStruct.ArbIDOrHeader) / 65536);
			icsJ1850Struct.Data1 = icsCANStruct.Data1;
			icsJ1850Struct.Data2 = icsCANStruct.Data2;
			icsJ1850Struct.Data3 = icsCANStruct.Data3;
			icsJ1850Struct.Data4 = icsCANStruct.Data4;
			icsJ1850Struct.Data5 = icsCANStruct.Data5;
			icsJ1850Struct.Data6 = icsCANStruct.Data6;
			icsJ1850Struct.Data7 = icsCANStruct.Data7;
			icsJ1850Struct.Data8 = icsCANStruct.Data8;
			icsJ1850Struct.AckBytes1 = icsCANStruct.AckBytes1;
			icsJ1850Struct.AckBytes2 = icsCANStruct.AckBytes2;
			icsJ1850Struct.AckBytes3 = icsCANStruct.AckBytes3;
			icsJ1850Struct.AckBytes4 = icsCANStruct.AckBytes4;
			icsJ1850Struct.AckBytes5 = icsCANStruct.AckBytes5;
			icsJ1850Struct.AckBytes6 = icsCANStruct.AckBytes6;
			icsJ1850Struct.AckBytes7 = icsCANStruct.AckBytes7;
			icsJ1850Struct.AckBytes8 = icsCANStruct.AckBytes8;
			icsJ1850Struct.Value = icsCANStruct.Value;
			icsJ1850Struct.MiscData = icsCANStruct.MiscData;
		}

		private void ConvertJ1850toCAN( ref icsSpyMessage icsCANStruct, ref icsSpyMessageJ1850 icsJ1850Struct)
		{
		//Becuse memcopy is not available.  
			icsCANStruct.StatusBitField = icsJ1850Struct.StatusBitField;
	    	icsCANStruct.StatusBitField2 = icsJ1850Struct.StatusBitField2;
			icsCANStruct.TimeHardware = icsJ1850Struct.TimeHardware;
			icsCANStruct.TimeHardware2 = icsJ1850Struct.TimeHardware2;
			icsCANStruct.TimeSystem = icsJ1850Struct.TimeSystem;
			icsCANStruct.TimeSystem2 = icsJ1850Struct.TimeSystem2;
			icsCANStruct.TimeStampHardwareID = icsJ1850Struct.TimeStampHardwareID;
			icsCANStruct.TimeStampSystemID = icsJ1850Struct.TimeStampSystemID;
			icsCANStruct.NetworkID = icsJ1850Struct.NetworkID;
			icsCANStruct.NodeID = icsJ1850Struct.NodeID;
			icsCANStruct.Protocol = icsJ1850Struct.Protocol;
			icsCANStruct.MessagePieceID = icsJ1850Struct.MessagePieceID;
			icsCANStruct.ColorID = icsJ1850Struct.ColorID;
			icsCANStruct.NumberBytesHeader = icsJ1850Struct.NumberBytesHeader;
			icsCANStruct.NumberBytesData = icsJ1850Struct.NumberBytesData;
			icsCANStruct.DescriptionID = icsJ1850Struct.DescriptionID;
			icsCANStruct.ArbIDOrHeader = (icsJ1850Struct.Header3 * 65536) + (icsJ1850Struct.Header2 * 256) + icsJ1850Struct.Header1;
			icsCANStruct.Data1 = icsJ1850Struct.Data1;
			icsCANStruct.Data2 = icsJ1850Struct.Data2;
			icsCANStruct.Data3 = icsJ1850Struct.Data3;
			icsCANStruct.Data4 = icsJ1850Struct.Data4;
			icsCANStruct.Data5 = icsJ1850Struct.Data5;
			icsCANStruct.Data6 = icsJ1850Struct.Data6;
			icsCANStruct.Data7 = icsJ1850Struct.Data7;
			icsCANStruct.Data8 = icsJ1850Struct.Data8;
			icsCANStruct.AckBytes1 = icsJ1850Struct.AckBytes1;
			icsCANStruct.AckBytes2 = icsJ1850Struct.AckBytes2;
			icsCANStruct.AckBytes3 = icsJ1850Struct.AckBytes3;
			icsCANStruct.AckBytes4 = icsJ1850Struct.AckBytes4;
			icsCANStruct.AckBytes5 = icsJ1850Struct.AckBytes5;
			icsCANStruct.AckBytes6 = icsJ1850Struct.AckBytes6;
			icsCANStruct.AckBytes7 = icsJ1850Struct.AckBytes7;
			icsCANStruct.AckBytes8 = icsJ1850Struct.AckBytes8;
			icsCANStruct.Value = icsJ1850Struct.Value;
			icsCANStruct.MiscData = icsJ1850Struct.MiscData;
		}



		private void cmdReceive_Click(object sender, System.EventArgs e)
		{
			long lResult;
			int lNumberOfMessages=0;
			int lNumberOfErrors=0;
			long lCount;
			string sListString;
	        icsSpyMessageJ1850 stJMsg;
			long lByteCount;
			double dTime;
	
			if (m_bPortOpen==false)
			{
	            MessageBox.Show("neoVI not opened");
				return;  // do not read messages if we haven't opened neoVI yet
			}

			stJMsg = CreateEmptyStructureJ1850();
			// read the messages from the driver
			lResult = icsNeoDll.icsneoGetMessages(m_hObject,ref stMessages[0],ref lNumberOfMessages,ref lNumberOfErrors);
			// was the read successful?
			if (lResult== 1)
			{
	            // clear the previous list of messages
				lstMessage.Items.Clear();
				lblReadCount.Text = "Number Read : " + Convert.ToString(lNumberOfMessages);
				lblReadErrors.Text = "Number Errors : " + Convert.ToString(lNumberOfErrors);
				// for each message we read
				for(lCount = 1;lCount<= lNumberOfMessages;lCount++)
				{
                	// Calculate the messages timestamp
					dTime = icsNeoDll.icsneoGetTimeStamp(stMessages[lCount-1].TimeHardware, stMessages[lCount-1].TimeHardware2);
					sListString = "Time : " + Convert.ToString(dTime);  //Build String

	                   // Was it a tx or rx message
					if ((stMessages[lCount-1].StatusBitField & Convert.ToInt32( eDATA_STATUS_BITFIELD_1.SPY_STATUS_TX_MSG)) > 0)
					{
	                    sListString = sListString + "Tx Message ";
				    }
					else
					{
						sListString = sListString + "Rx Message ";
					}
	
					//Get the byte count
					lByteCount = stMessages[lCount - 1].NumberBytesData;
	
					// Was it a CAN or other protocol
						if (Convert.ToBoolean (stMessages[lCount-1].Protocol == Convert.ToInt32(  ePROTOCOL.SPY_PROTOCOL_CAN)))
						{
							// list the arb id
							sListString = sListString + "Network " + GetStringForNetworkID(Convert.ToInt16(  stMessages[lCount - 1].NetworkID)) + " ArbID : " + ConvertToHex(Convert.ToString(stMessages[lCount - 1].ArbIDOrHeader)) + "  Data ";
							if (lByteCount >= 1) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data1)) + " ";
							if (lByteCount >= 2) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data2)) + " ";
							if (lByteCount >= 3) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data3)) + " ";
							if (lByteCount >= 4) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data4)) + " ";
							if (lByteCount >= 5) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data5)) + " ";
							if (lByteCount >= 6) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data6)) + " ";
							if (lByteCount >= 7) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data7)) + " ";
							if (lByteCount >= 8) sListString = sListString + ConvertToHex(Convert.ToString(stMessages[lCount - 1].Data8)) + " ";
						}
						else
						{
							// list the headers bytes
							ConvertCANtoJ1850Message(ref stMessages[lCount - 1],ref stJMsg);
								sListString = sListString + "Network " + GetStringForNetworkID(stJMsg.NetworkID) + " Data : ";
	
							//add the data bytes
							if (stJMsg.NumberBytesHeader >= 1) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Header1)) + " ";
							if (stJMsg.NumberBytesHeader >= 2) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Header2)) + " ";
							if (stJMsg.NumberBytesHeader >= 3) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Header3)) + " ";
							sListString = sListString + "  ";
							if (lByteCount >= 1) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data1)) + " ";
							if (lByteCount >= 2) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data2)) + " ";
							if (lByteCount >= 3) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data3)) + " ";
							if (lByteCount >= 4) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data4)) + " ";
							if (lByteCount >= 5) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data5)) + " ";
							if (lByteCount >= 6) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data6)) + " ";
							if (lByteCount >= 7) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data7)) + " ";
							if (lByteCount >= 8) sListString = sListString + ConvertToHex(Convert.ToString(stJMsg.Data8)) + " ";
						}

					//Add the message to the list
						lstMessage.Items.Add(sListString);
	
				}
			}
			else
			{
	            MessageBox.Show("Problem Reading Messages");
			}
		}

		private string GetStringForNetworkID(short lNetworkID)
		{
			string sTempOutput = "";
			switch(lNetworkID)
			{
				case 1:  //eNETWORK_ID.NETID_HSCAN:
					sTempOutput= "HSCAN";
					break;
				case 2:  //eNETWORK_ID.NETID_MSCAN:
					sTempOutput = "MSCAN";
					break;
				case 3:  //eNETWORK_ID.NETID_SWCAN:
					sTempOutput = "SWCAN";
					break;
				case 4:  //eNETWORK_ID.NETID_LSFTCAN:
					sTempOutput = "LSFTCAN";
					break;
				case 5:  //eNETWORK_ID.NETID_FORDSCP:
					sTempOutput = "FORD SCP";
					break;
				case 6:  //eNETWORK_ID.NETID_J1708:
					sTempOutput = "J1708";
					break;
				case 7:  //eNETWORK_ID.NETID_AUX:
					sTempOutput = "AUX";
					break;
				case 8:  //eNETWORK_ID.NETID_JVPW:
					sTempOutput = "J1850 VPW";
					break;
				case 9:  //eNETWORK_ID.NETID_ISO:
					sTempOutput = "ISO/UART";				
					break;
				}
			return sTempOutput;
		}




		private void cmdSetupAndEnable_Click(object sender, System.EventArgs e)
		{
			spyFilterLong stFilter;
			icsSpyMessage stMsg;

			//Check to see if the port is open
			if(m_bPortOpen != true)
			{
				MessageBox.Show("Port is not open");
				return;
			}
		
			//Set the filter up 
			stFilter.Header = ConvertFromHex(txtFirstFrame.Text);
			stFilter.HeaderMask = ConvertFromHex("FFF");

			//Set the Flow Control Frame Properties
			stMsg.ArbIDOrHeader = ConvertFromHex(txtFlowControl.Text);
			stMsg.NumberBytesData = 8;
			stMsg.StatusBitField = 2;
			stMsg.Data1 = Convert.ToByte(ConvertFromHex("30"));    //flow control frame
			stMsg.Data2 = 3;       //block size
			stMsg.Data3 = 0;       //stmin =0

			//Set the established parameters
			icsNeoDll.icsneoSetISO15765RxParameters(m_hObject, 1, 1,out stFilter,out stMsg, 100, 0, 0, 0);
		}

		private void cmdDisable_Click(object sender, System.EventArgs e)
		{
			spyFilterLong stFilter;
			icsSpyMessage stMsg;

			//Make sure that the port is open
			if (m_bPortOpen!=true)
			{
				MessageBox.Show("Port is not open.");
				return;
			}

			//Set ISO15765 RX Parameters
			icsNeoDll.icsneoSetISO15765RxParameters(m_hObject, 1, 0,out stFilter,out stMsg, 100, 3, 0, 0);
		}

		private void cmdReadStatus_Click(object sender, System.EventArgs e)
		{
			int lTxStatus = 0;
			int lRxStatus = 0;
	
			//Make sure that the port is open
			if (m_bPortOpen != true)
			{
				MessageBox.Show("Port is not open.");
				return;
			}
	
			//Acquire the ISO 15765 Paramerts
			icsNeoDll.icsneoGetISO15765Status(m_hObject, 1, 0, 0,ref lTxStatus,ref lRxStatus);
	
			//Clear the Status box
			lstStatusItems.Items.Clear();
	
			//Check for Problems in ISO 15765 Rx status
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrGlobal)) > 0)
				lstStatusItems.Items.Add("Problem In Rx Status");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrCFRX_EXP_FF)) > 0 )
				lstStatusItems.Items.Add("Received a Consecutive Frame when expecting first frame");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrFCRX_EXP_FF)) > 0 )
				lstStatusItems.Items.Add("Received a Flow Control Frame when expecting first frame");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrSFRX_EXP_CF)) > 0 )
				lstStatusItems.Items.Add("Received a Single Frame when expecting Consecutive frame");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrFFRX_EXP_CF)) > 0 )
				lstStatusItems.Items.Add("Received a First Frame when expecting Consecutive frame");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrFCRX_EXP_CF)) > 0 )
				lstStatusItems.Items.Add("Received a Flow Control Frame when expecting Consecutive frame");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrCF_TIME_OUT)) > 0 )
				lstStatusItems.Items.Add("Consecutive Timeout");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxComplete)) > 0 )
				lstStatusItems.Items.Add("Last Messaging Successful");

			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxInProgress)) > 0 )
				lstStatusItems.Items.Add("Rx In Progress");
	
			if ((lRxStatus & Convert.ToInt32(icsspy15765RxBitfield.icsspy15765RxErrSeqCntInCF)) > 0 )
				lstStatusItems.Items.Add("Incorrect Sequence Count in Consecutive Frames");
		}

		private void cmdClearStatus_Click(object sender, System.EventArgs e)
		{
			spyFilterLong stFilter;
			icsSpyMessage stMsg;

			//Check to see if the port is open
			if (m_bPortOpen != true)
			{
				MessageBox.Show("Port is not open.");
				return;
			}

			//Call Set iso15765 RX Parameters to clear Status
			icsNeoDll.icsneoSetISO15765RxParameters(m_hObject, 1, 0,out stFilter,out stMsg, 100, 3, 0, 0);
		}

		private void chkAutoRead_CheckedChanged(object sender, System.EventArgs e)
		{
			if(chkAutoRead.Checked == true)
			{
				Timer1.Enabled = true;
			}
			else
			{
				Timer1.Enabled = false;
			}

		}

		private void Timer1_Tick(object sender, System.EventArgs e)
		{
			cmdReceive_Click(cmdReceive,null);
		}
	}
}
