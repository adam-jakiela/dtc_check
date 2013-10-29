using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using CoreScanner;

namespace ICSNeoCSharp 
    
{

    // A form used for testing the connection of the usb scanner
    public partial class ScannerForm : Form
    {

        static CCoreScannerClass ccs;

        //expected values for testing scanner unit 
        //REAL VALUES MAY DIFFER
        const string EXPECTED_GUID = "DC447DB3422D4C4CA9880A9B65CA1007";
        const string EXPECTED_VID = "1504";
        const string EXPECTED_PID = "4608";
        const string EXPECTED_TYPE = "USBHIDKB";

        public ScannerForm()
        {
            InitializeComponent();
            ccs = new CCoreScannerClass();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ccs = new CCoreScannerClass();

            //Call Open API
            short[] scannerTypes = new short[1]; // Scanner Types you are interested in
            scannerTypes[0] = 1; // 1 for all scanner types 
            short numberOfScannerTypes = 1; // Size of the scannerTypes array
            int status; // Extended API return code 

            ccs.Open(0, scannerTypes, numberOfScannerTypes, out status);
            // Lets list down all the scanners connected to the host
            short numberOfScanners; // Number of scanners expect to be used
            int[] connectedScannerIDList = new int[255];
            // List of scanner IDs to be returned
            string outXML; //Scanner details output
            ccs.GetScanners(out numberOfScanners, connectedScannerIDList,
            out outXML, out status);
            output_tb.Text = outXML;

            if (numberOfScanners > 0)
            {
                state_label.Text = "GOOD"; 
                state_label.ForeColor = Color.Green;
            } else { 
                state_label.Text = "FAILED"; 
                state_label.ForeColor = Color.Red;
            }

            if (status == 0)
            {

                //parse the xml 
                using (XmlReader reader = XmlReader.Create(new StringReader(outXML)))
                {
                    //get scanner type
                    reader.ReadToFollowing("scanner type");
                    if (EXPECTED_TYPE == reader.Value.ToString())
                    {
                        type_label.Text = reader.Value.ToString();
                        type_label.ForeColor = Color.Green;
                    }
                    else
                    {
                        type_label.Text = reader.Value.ToString();
                        type_label.ForeColor = Color.Red;
                    } 

                    //get ID 
                    reader.ReadToFollowing("scannerID");
                    id_label.Text = reader.Value.ToString();
                    

                    //get GUID
                    reader.ReadToFollowing("GUID");
                    if (EXPECTED_GUID == reader.Value.ToString())
                    {
                        guid_label.Text = reader.Value.ToString();
                        guid_label.ForeColor = Color.Green;
                    }
                    else
                    {
                        guid_label.Text = reader.Value.ToString();
                        guid_label.ForeColor = Color.Red;
                    }

                    

                    //get VID
                    reader.ReadToFollowing("VID");
                    if (EXPECTED_VID == reader.Value.ToString())
                    {
                        vid_label.Text = reader.Value.ToString();
                        vid_label.ForeColor = Color.Green;
                    }
                    else
                    {
                        vid_label.Text = reader.Value.ToString();
                        vid_label.ForeColor = Color.Red;
                    }

                    //get PID
                    reader.ReadToFollowing("PID");
                    if (EXPECTED_PID == reader.Value.ToString())
                    {
                        pid_label.Text = reader.Value.ToString();
                        pid_label.ForeColor = Color.Green;
                    }
                    else
                    {
                        pid_label.Text = reader.Value.ToString();
                        pid_label.ForeColor = Color.Red;
                    }               

                }

            }
        }
    }
}