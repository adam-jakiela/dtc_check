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
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Win32;





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
        private string didF113 = null;
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
        public string bench = null;

        private int passedQuantity = 0;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        public List<string> ignoredDTCs = new List<string>();
        public List<string> docs = new List<string>();

        public bool setupDone = false;
        public bool closed = false;
        public bool cancel = false;

        SerialPort comPort;
        message myMessage;

        public event MySettingsEvent DIDF111Changed;
        public event MySettingsEvent DIDF124Changed;
        public event MySettingsEvent DIDF125Changed;
        public event MySettingsEvent DIDF188Changed;
        public event MySettingsEvent DIDF113Changed;

        public event MySettingsEvent CALChanged;
        public event MySettingsEvent APLChanged;
        public event MySettingsEvent E2PChanged;
        public event MySettingsEvent PBLChanged;
        public event MySettingsEvent BaudRateChanged;
        public event MySettingsEvent LogFileChanged;
        public event MySettingsEvent OnSetupClose;
        public event MySettingsEvent OnSetupOpen;


        // new events 
        public event MySettingsEvent SBLChanged; 
        public event MySettingsEvent DIDF110Changed;
        public event MySettingsEvent DocListChanged;
        public event MySettingsEvent PackageIDChanged;
        public event MySettingsEvent QuantityChanged;
        public event MySettingsEvent BenchChanged;
        public event MySettingsEvent OperatorChanged;
     
        
        List<string> data = new List<string>(); 

        IgnoreForm ignoreForm = new IgnoreForm();

        #endregion


        private XDocument xdoc;
        private bool wirelessScanner = false;
        public int palletQuantity = 0;

        public bool reset = false;

        public string opName = "";
        public string sbl = "";
        public string f110 = "";
        public string packageId = "";

        private DataSerializer ds;
        private bool dataLoaded = false;
        public string dataFileName = "DTCSaveState.osl";
        public bool deserialized = true;

        public List<string> docList = new List<string>();

        public double currentPrecentage = 0;
        public bool palletComplete = true;
        public bool duplicateDevice = false;

    
        //    KeyboardHook hook = new KeyboardHook();



        #region Constructor
        public FormSetup(SerialPort _com)
        {
            InitializeComponent();
            //OnSetupOpen.Invoke(this);

            cancel = false;

            if (palletComplete)
            {
                this.rdoLXF.Checked = true;
                this.txtAPP.Enabled = true;
                this.txtCAL.Enabled = true;
                this.txtE2P.Enabled = true;
                this.pbl_tb.Enabled = true;
            }

            if (closed)
                closed = false;

            if (cancel)
                cancel = false;

            comPort = new SerialPort();
            myMessage = new message();

            comPort = _com;
            comPort.DataReceived += new SerialDataReceivedEventHandler(comPort_DataReceived);

            this.KeyPress += new KeyPressEventHandler(Form1_KeyPress);

            this.rdoLXF.Checked = true;

            radioType = "LXF";
            //this.rdoACM.Checked = true;

            mtxtDIDF111.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF124.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF125.CharacterCasing = CharacterCasing.Upper;
            mtxtDIDF188.CharacterCasing = CharacterCasing.Upper;
            txtAPP.CharacterCasing = CharacterCasing.Upper;
            pbl_tb.CharacterCasing = CharacterCasing.Upper;
            txtCAL.CharacterCasing = CharacterCasing.Upper;
            txtE2P.CharacterCasing = CharacterCasing.Upper;
            f110_tb.CharacterCasing = CharacterCasing.Upper;
            f113_tb.CharacterCasing = CharacterCasing.Upper;
            quantity_tb.CharacterCasing = CharacterCasing.Upper;



            this.txtAPP.Text = "470-321-137";
            this.txtCAL.Text = "485-0489-08";
            this.txtE2P.Text = "485-0513-22";
            this.f113_tb.Text = "DL3T-19C107-CH";
            this.mtxtDIDF111.Text = "DL3T-14F188-CC";
            this.mtxtDIDF188.Text = "DL3T-14D099-CG";
            this.mtxtDIDF124.Text = "DL3T-14D100-CD";
            this.mtxtDIDF125.Text = "DL3T-14D100-FH";

            this.sbl_tb.Text = "DL3T-14D101-AA";
            this.pbl_tb.Text = "470-170-011";
            this.f110_tb.Text = "DS-DL3T-19C107-AA";
            this.quantity_tb.Text = "80";


           ds = DataSerializer.LoadFromFile();


            if (currentPrecentage != 100 && currentPrecentage != 0)
            {
                palletComplete = false;
                this.txtAPP.Enabled = false;
                this.txtE2P.Enabled = false;
                this.txtCAL.Enabled = false;
                this.pbl_tb.Enabled = false;
                this.f113_tb.Enabled = false;
                this.f110_tb.Enabled = false;
                this.mtxtDIDF111.Enabled = false;
                this.mtxtDIDF124.Enabled = false;
                this.mtxtDIDF125.Enabled = false;
                this.mtxtDIDF188.Enabled = false;
                this.sbl_tb.Enabled = false;
                this.docListBox.Enabled = false;
                this.quantity_tb.Enabled = false;
                this.package_tb.Enabled = false;
                this.txt_benchNum.Enabled = false;
                this.txt_opName.Enabled = false;
                this.submitButton.Enabled = false;
                this.clear_button.Enabled = false;


            }
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

        void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                //MessageBox.Show("SDASDFD");
                this.submitButton.PerformClick();
            }
        }


        public void setPrecentage(double _precentage)
        {
            Console.WriteLine("CURRENT PRECENTAGE: " + currentPrecentage);
            currentPrecentage = _precentage;
        }

        public void clearALL()
        {
            //textboxes
            this.txtAPP.Text = "";
            this.txtCAL.Text = "";
            this.txtE2P.Text = "";
            this.txt_opName.Text = "";
            this.txt_benchNum.Text = "";
            this.trackingNum = "";
            this.sbl_tb.Text = "";
            this.quantity_tb.Text = "";
            this.quantity = "";
            this.pbl_tb.Text = "";
            this.pbl = "";
            this.passedQuantity = 0;
            this.partQuantity = "";
            this.partNumber = "";
            this.partNum = "";;
            this.palletQuantity = 0;
            this.packageId = "";
            this.package_tb.Text = "";
            this.opName = "";
            this.mtxtDIDF188.Text = "";
            this.mtxtDIDF125.Text = "";
            this.mtxtDIDF124.Text = "";
            this.mtxtDIDF111.Text = "";
            this.f113_tb.Text = "";
            this.f110_tb.Text = ""; 
            this.docListBox.Text = "";

            //variables
            this.app = "";
            this.cal = "";
            this.e2p = "";
            this.pbl = "";
            this.palletQuantity = 0;
            this.partNum = "";
            this.passedQuantity = 0;
            this.quantity = "";
            this.partQuantity = "";
            this.packageId = "";
            this.opName = "";
            this.logFile = "";
            this.sbl = "";
            this.bench = "";
            
        }


        public void setPassedQuantity(int q)
        {
            passedQuantity = q;
        }

        void comPort_DataReceived(object sender, EventArgs e)
        {
            if (palletComplete)
            {
                if (!wirelessScanner)
                {
                    if (this.Visible)
                    {

                        if (comPort.IsOpen)
                            myMessage.Data = comPort.ReadExisting();

                        string input = myMessage.Data;
                        //   MessageBox.Show(myMessage.Data);


                        if (myMessage.Data[0] == 'A')
                        {
                            if (this.txtAPP.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("A"))
                                    {
                                        this.txtAPP.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("A"))
                                {
                                    this.txtAPP.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'B')
                        { //txt CAL
                            if (this.txtCAL.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("B"))
                                    {
                                        this.txtCAL.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("B"))
                                {
                                    this.txtCAL.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'L')
                        { //txte2p
                            if (this.txtE2P.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("L"))
                                    {
                                        this.txtE2P.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("L"))
                                {
                                    this.txtE2P.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'E')
                        { //mtxtdidf111
                            if (this.mtxtDIDF111.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("E"))
                                    {
                                        this.mtxtDIDF111.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("E"))
                                {
                                    this.mtxtDIDF111.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'F')
                        { //mtxtdidf188
                            if (this.mtxtDIDF188.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {

                                    if (myMessage.Data.StartsWith("F"))
                                    {
                                        this.mtxtDIDF188.Text = this.myMessage.Data.Remove(0, 2);
                                    }

                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("F"))
                                {
                                    this.mtxtDIDF188.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'G')
                        { //mtxtdidf124

                            if (this.mtxtDIDF124.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("G"))
                                    {
                                        this.mtxtDIDF124.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("G"))
                                {
                                    this.mtxtDIDF124.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }
                        else if (myMessage.Data[0] == 'H')
                        { //mtxtdidf125
                            //make sure radio button is set to 3.1 
                            if (RadioType == "LXF")
                            {
                                if (this.mtxtDIDF125.InvokeRequired)
                                {
                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        if (myMessage.Data.StartsWith("H"))
                                        {
                                            this.mtxtDIDF125.Text = this.myMessage.Data.Remove(0, 2);
                                        }
                                    });
                                }
                                else
                                {
                                    if (myMessage.Data.StartsWith("H"))
                                    {
                                        this.mtxtDIDF125.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("F125 part number scanned. Did you mean to select gen 3.1?");
                            }

                        }
                        else if (myMessage.Data[0] == 'J') //txtpbl
                        {
                            if (this.pbl_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("J"))
                                    {
                                        this.pbl_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("J"))
                                {
                                    this.pbl_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'D') //f113
                        {
                            if (this.f113_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("D"))
                                    {
                                        this.f113_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("D"))
                                {
                                    this.f113_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'I') //sbl
                        {
                            if (this.sbl_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("I"))
                                    {
                                        this.sbl_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("I"))
                                {
                                    this.sbl_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'K') //f110
                        {
                            if (this.f110_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("K"))
                                    {
                                        this.f110_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("J"))
                                {
                                    this.f110_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'Q') //quantity
                        {
                            if (this.quantity_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("Q"))
                                    {
                                        this.quantity_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("Q"))
                                {
                                    this.quantity_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'C') //package ID
                        {
                            if (this.package_tb.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("C"))
                                    {
                                        this.package_tb.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("C"))
                                {
                                    this.package_tb.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'O') //operator name
                        {
                            if (this.txt_opName.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("O"))
                                    {
                                        this.txt_opName.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("O"))
                                {
                                    this.txt_opName.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }

                        else if (myMessage.Data[0] == 'R') //bench
                        {
                            if (this.txt_benchNum.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("R"))
                                    {
                                        this.txt_benchNum.Text = this.myMessage.Data.Remove(0, 2);
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("R"))
                                {
                                    this.txt_benchNum.Text = this.myMessage.Data.Remove(0, 2);
                                }
                            }
                        }




                        //DOC SCANNED 

                        else if (myMessage.Data[0] == 'Z')
                        {
                            if (this.docListBox.InvokeRequired)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    if (myMessage.Data.StartsWith("Z"))
                                    {
                                        bool inList = false;
                                        for (int x = 0; x < this.docListBox.Items.Count; x++)
                                        {
                                            if (this.myMessage.Data.Remove(0, 2) == this.docListBox.Items[x])
                                            {
                                                inList = true;
                                            }
                                        }

                                        if (!inList)
                                        {
                                            this.docListBox.Items.Add(this.myMessage.Data.Remove(0, 2));
                                        }
                                    }
                                });
                            }
                            else
                            {
                                if (myMessage.Data.StartsWith("Z"))
                                {
                                    bool inList = false;
                                    for (int x = 0; x < this.docListBox.Items.Count; x++)
                                    {
                                        if (this.myMessage.Data.Remove(0, 2) == this.docListBox.Items[x])
                                        {
                                            inList = true;
                                        }
                                    }

                                    if (!inList)
                                    {
                                        this.docListBox.Items.Add(this.myMessage.Data.Remove(0, 2));
                                    }
                                }
                            }
                        }
                    }
                }
            }
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


        public string DIDF113
        {
            get
            {
                return didF113;
            }

            set
            {
                didF113 = value;
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

        public string SBL_PUBLIC
        {
            get
            {
                return sbl;
            }

            set
            {
                sbl = value;
            }
        }


        public List<string> DOC_LIST_PUBLIC
        {
            get
            {
                return docList;
            }

            set
            {
                docList = value;
            }

        }


        public string OPNAME
        {

            get
            {
                return opName;
            }

            set
            {
                opName = value;

            }
        }

        public string PACKAGE_ID
        {

            get
            {
                return packageId;
            }

            set
            {
                packageId = value;

            }
        }

        public string BENCH
        {

            get
            {

                return bench;
            }
            set
            {
                bench = value;
            }
        }


        #endregion

        #region Private Functions
        private void frmSetup_FormClosing(object sender, FormClosingEventArgs e)
        {

            Console.WriteLine("In cvlosing");
            if (!quickClose && !cancel)
            {


                try
                {
                    
                    palletQuantity = Convert.ToInt32(this.quantity_tb.Text);
                    Console.WriteLine("PALLET");
                }
                catch
                {
                    MessageBox.Show("Imvalid pallet quantity");
                }

                closed = true;
                setupDone = true;
                //get values from form and set them to their appropriate labels. 

               
                  DIDF111 = mtxtDIDF111.Text;
                    DIDF111Changed.Invoke(this);
                    Console.WriteLine("HEREASDASDASDASD");
                    DIDF124 = mtxtDIDF124.Text;
                    DIDF124Changed.Invoke(this);
                
                    DIDF125 = mtxtDIDF125.Text;
                    DIDF125Changed.Invoke(this);
                    DIDF188 = mtxtDIDF188.Text;
                    DIDF188Changed.Invoke(this);
                    DIDF113 = f113_tb.Text;
                    DIDF113Changed.Invoke(this);

                    sbl = sbl_tb.Text;
                    SBLChanged.Invoke(this);

                    pbl = pbl_tb.Text;
                    PBLChanged.Invoke(this);

                    f110 = f110_tb.Text;
                    DIDF110Changed.Invoke(this);

                    packageId = package_tb.Text;
                    PackageIDChanged.Invoke(this);

                    quantity = quantity_tb.Text;
                    QuantityChanged.Invoke(this);

                    bench = txt_benchNum.Text;
                    BenchChanged.Invoke(this);

                    opName = this.txt_opName.Text;
                    OperatorChanged.Invoke(this);

                    DocListChanged.Invoke(this);

                
                    
               

               Console.WriteLine("here 2");
                try
                {

                    try
                    {
                        DIDF111 = DIDF111.Substring(0, 17);
                    }
                    catch
                    {
                        DIDF111 = DIDF111.Substring(0, 14);
                    }


                    try
                    {
                        DIDF124 = DIDF124.Substring(0, 17);
                    }
                    catch
                    {
                        DIDF124 = DIDF124.Substring(0, 14);
                    }

                    try
                    {

                        DIDF125 = DIDF125.Substring(0, 17);
                    }
                    catch
                    {
                        DIDF125 = DIDF125.Substring(0, 14);
                    }


                    try
                    {
                        DIDF188 = DIDF188.Substring(0, 17);
                    }
                    catch
                    {
                        DIDF188 = DIDF188.Substring(0, 14);
                    }

                    try
                    {
                        DIDF113 = DIDF113.Substring(0, 17);
                    }
                    catch
                    {
                        DIDF113 = DIDF113.Substring(0, 14);
                    }

                    mtxtDIDF111.Text = DIDF111;
                    mtxtDIDF124.Text = DIDF124;
                    mtxtDIDF125.Text = DIDF125;
                    mtxtDIDF188.Text = DIDF188;
                    f113_tb.Text = DIDF113;


                }
                catch
                {
                    MessageBox.Show("Error", "Check inputted values.");
                }

                APP = txtAPP.Text.Replace(" ", string.Empty).Replace("*", string.Empty);
                PBL = pbl_tb.Text.Replace(" ", string.Empty).Replace("*", string.Empty);
                CAL = txtCAL.Text.Replace(" ", string.Empty).Replace("*", string.Empty);
                E2P = txtE2P.Text.Replace(" ", string.Empty).Replace("*", string.Empty);


                if (APP.Length > 11)
                    APP = APP.Substring(0, 11);

                if (E2P.Length > 11)
                    E2P = E2P.Substring(0, 11);

                if (CAL.Length > 11)
                    CAL = CAL.Substring(0, 11);

                if (PBL.Length > 11)
                    PBL = PBL.Substring(0, 11);


                APLChanged.Invoke(this);
                E2PChanged.Invoke(this);
                PBLChanged.Invoke(this);
                CALChanged.Invoke(this);

                txtAPP.Text = APP;
                txtCAL.Text = CAL;
                txtE2P.Text = E2P;
                pbl_tb.Text = PBL;

                sbl_tb.Text = sbl;
                f110_tb.Text = f110;


                reset = true;
                //get date  
                //NAME THE EXCEL FILE  


                if (txtAPP.Text != "" && txtCAL.Text != "" && txtE2P.Text != "")
                {
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

                    Console.WriteLine("Before reg");

                    //determine which version of excel we are using and save the file accordingly
                    RegistryKey localMachine = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Office");
                    bool officeFound = false;

                    foreach (string key in localMachine.GetSubKeyNames())
                    {
                        if (key == "11.0")
                        {//2003 
                            logFile = "PNV" + "-" + date + "-" + DateTime.Now.ToString("HHmmtt") + ".xls";
                            officeFound = true;
                            Console.WriteLine("EXCEL 03 FOUND"); 
                            break;
                        }
                        else if (key == "12.0" || key == "14.0")
                        {
                            logFile = "PNV" + "-" + date + "-" + DateTime.Now.ToString("HHmmtt") + ".xls";
                            if (key == "12.0")
                            {
                                Console.WriteLine("EXCEL 2007 FOUND");
                            }
                            else if (key == "14.0")
                            {
                                Console.WriteLine("EXCEL 2010 FOUND");

                            }
                            officeFound = true;
                            break;
                        }

                    }


                    if (!officeFound)
                    {
                        MessageBox.Show("Excel could not be found on this computer.");
                    }

                    LogFileChanged.Invoke(this);
                }

              
                    ds.F111EXPECTED = mtxtDIDF111.Text;
                    ds.F113EXPECTED = f113_tb.Text;
                    ds.F124EXPECTED = mtxtDIDF124.Text;
                    ds.F125EXPECTED = mtxtDIDF125.Text;
                    ds.F188EXPECTED = mtxtDIDF188.Text;

                    ds.APLEXPECTED = txtAPP.Text;
                    ds.PBLEXPECTED = pbl_tb.Text;
                    ds.CALEXPECTED = txtCAL.Text;
                    ds.E2PEXPECTED = txtE2P.Text;

                    ds.SBL = sbl_tb.Text;
                    ds.OPERATORNAME = txt_opName.Text;
                    ds.BENCH = txt_benchNum.Text;
                    ds.QUANTITY = Convert.ToInt32(this.quantity_tb.Text);
                    ds.PACKAGEID = package_tb.Text;

                    ds.F110 = f110_tb.Text;

                    ds.DOCLIST = this.docList; 

                    DataSerializer.saveToFile(ds);
                   
               
                quickClose = false;
            }

            OnSetupClose.Invoke(this);
        }


        private void frmSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
        
            
            if (!cancel)
            {
                setupDone = true;
            } 
             


        }
        private void frmSetup_Load(object sender, EventArgs e)
        {
            // txtPassword.Focus(); 

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
            if (palletComplete)
            {
                RadioType = "ACM";
                //this.lblF125.Text = "DID F110";
                BaudRateChanged.Invoke(this);

                this.txtAPP.Enabled = false;
                this.txtCAL.Enabled = false;
                this.txtE2P.Enabled = false;
                this.pbl_tb.Enabled = false;
            }
        }

        private void rdoEFP_Click(object sender, EventArgs e)
        {
            if (palletComplete)
            {
                RadioType = "EFP";
            //    this.lblF125.Text = "DID F110";
                BaudRateChanged.Invoke(this);

                this.txtAPP.Enabled = false;
                this.txtCAL.Enabled = false;
                this.txtE2P.Enabled = false;
                this.pbl_tb.Enabled = false;
            }
        }

        private void rdoLXF_Click(object sender, EventArgs e)
        {
            if (palletComplete)
            {
                RadioType = "LXF";
              //  this.lblF125.Text = "DID F125";
                BaudRateChanged.Invoke(this);
                if (radioType == "LXF")
                {
                    this.txtAPP.Enabled = true;
                    this.txtCAL.Enabled = true;
                    this.txtE2P.Enabled = true;
                    this.pbl_tb.Enabled = true;
                    this.sbl_tb.Enabled = true;
                    this.f110_tb.Enabled = true;
                    this.f113_tb.Enabled = true;
                }
                else
                {
                    this.txtAPP.Enabled = false;
                    this.txtCAL.Enabled = false;
                    this.txtE2P.Enabled = false;
                    this.pbl_tb.Enabled = false;
                    this.sbl_tb.Enabled = false;
                    this.f110_tb.Enabled = false;
                    this.f113_tb.Enabled = false;
                }
            }
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            opName = this.txt_opName.Text;
            packageId = this.package_tb.Text;
            for (int x = 0; x < docListBox.Items.Count; x++)
            {
                docList.Add(docListBox.Items[x].ToString()); 
            }

            sbl = this.sbl_tb.Text;
            f110 = this.f110_tb.Text;
            bench = this.txt_benchNum.Text; 
            closed = true;
            setupDone = true;
            ignoredDTCs = ignoreForm.ignoredDtcs;

            Console.WriteLine("HERE");
            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            cancel = true;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ignoreForm.StartPosition = FormStartPosition.CenterScreen;
            ignoreForm.ShowDialog();



        }

        void IgnoreForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            ignoredDTCs = ignoreForm.getDTCs();
        }

        public List<string> getDTCs()
        {
            return this.ignoredDTCs;
        }

        private void lblPassword_Click(object sender, EventArgs e)
        {

        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            this.mtxtDIDF111.Text = "";
            this.mtxtDIDF124.Text = "";
            this.mtxtDIDF125.Text = "";
            this.mtxtDIDF188.Text = "";
            this.txtAPP.Text = "";
            this.txtCAL.Text = "";
            this.txtE2P.Text = "";
            this.pbl_tb.Text = "";
            this.quantity_tb.Text = "";
            this.f110_tb.Text = "";
            this.f113_tb.Text = "";
            this.sbl_tb.Text = "";
            this.package_tb.Text = "";

            this.app = "";
            this.cal = "";
            this.e2p = "";
            this.pbl = "";
            this.palletQuantity = 0;
            this.partNum = "";
            this.passedQuantity = 0;
            this.quantity = "";
            this.partQuantity = "";
            this.packageId = "";
            this.opName = "";
            this.logFile = "";
            this.sbl = "";
            this.bench = "";
            
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

    }
}