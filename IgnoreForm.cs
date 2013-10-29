using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ICSNeoCSharp


{

    public delegate void MyIgnoreEvent(object source);
    public partial class IgnoreForm : Form
    {

        public List<string> ignoredDtcs = new List<string>();
        public List<string> savedDtcs = new List<string>();
        private const int CP_NOCLOSE_BUTTON = 0x200;
        string appLoc;
        string fileName = "ignored_dtcs.txt"; 
        string addedDTCFilename = "added_dtcs.txt";
        System.IO.StreamReader file;

        public event MyIgnoreEvent OnIgnoreClose;

        public IgnoreForm()
        {
            
            InitializeComponent();

            appLoc = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(appLoc);
            this.availableListBox.Items.Add("E10000 - Initial config not complete.");
            this.availableListBox.Items.Add("E10100 - Misconfiguration.");
            this.availableListBox.Items.Add("919F13 - GPS antenna circuit open.");
            this.availableListBox.Items.Add("919F01 - GPS antenna electrical failure.");
            this.availableListBox.Items.Add("9A8913 - SDARS antenna circuit open.");
            this.availableListBox.Items.Add("9A8901 - SDARS antenna electrical failure.");
            this.availableListBox.Items.Add("908E63 - Display touch screen stuck.");
            this.availableListBox.Items.Add("921C01 - Hard disk drive failure.");
            this.availableListBox.Items.Add("9D1949 - DVD mechanism electrical failure.");
            this.availableListBox.Items.Add("9D194B - DVD mechanism over temperature.");
            this.availableListBox.Items.Add("9D7813 - AUX input circuit open.");
            this.availableListBox.Items.Add("E01363 - Front bezel stuck button.");
            this.availableListBox.Items.Add("F00317 - Battery voltage above threshold.");
            this.availableListBox.Items.Add("F00316 - Battery voltage below threshold.");
            this.availableListBox.Items.Add("C14000 - Lost Comms body control module.");
            this.availableListBox.Items.Add("C15500 - Lost Comms IPC.");
            this.availableListBox.Items.Add("C15900 - Lost Comms PAM.");
            this.availableListBox.Items.Add("C16200 - Lost Comms Display.");
            this.availableListBox.Items.Add("C16400 - Lost Comms HVAC.");
            this.availableListBox.Items.Add("C19300 - Lost Comms External SDARS.");
            this.availableListBox.Items.Add("C19600 - Lost Comms with FES.");
            this.availableListBox.Items.Add("C19700 - Lost Comms with CPM.");
            this.availableListBox.Items.Add("C23800 - Lost Comms with DSP AMP.");
            this.availableListBox.Items.Add("C24900 - Lost Comms with RSEM.");
            this.availableListBox.Items.Add("C25600 - Lost Comms with FCIM.");
            this.availableListBox.Items.Add("C18400 - Lost Comms with ACM.");
            this.availableListBox.Items.Add("E01409 - Gyro hardware component failure.");
            this.availableListBox.Items.Add("E01441 - Flash checksum failure.");
            this.availableListBox.Items.Add("E01442 - Control Module General Memory Failure.");
            this.availableListBox.Items.Add("E01496 - Control Module Component Internal Failure.");
            this.availableListBox.Items.Add("E01496 - SDARS component hardware failure.");
            this.availableListBox.Items.Add("E01A51 - Calibration file missing.");
            this.availableListBox.Items.Add("F00045 - HDD FS-Info corrupt.");
            this.availableListBox.Items.Add("500101 - Rear camera no signal.");
            this.availableListBox.Items.Add("91BA1C - SWC1 voltage out of range.");
            this.availableListBox.Items.Add("92011C - SWC2 voltage out of range.");
            this.availableListBox.Items.Add("91BA63 - SWC1 stuck button.");
            this.availableListBox.Items.Add("920163 - SWC2 stuck button.");
            this.availableListBox.Items.Add("91BB12 - Rear park aide audio input circuit short to battery.");
            this.availableListBox.Items.Add("9A0101 - Speaker #1 General Electric Failure.");
            this.availableListBox.Items.Add("9A0111 - Speaker #1 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0112 - Speaker #1 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0113 - Speaker #1 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0201 - Speaker #2 General Electric Failure.");
            this.availableListBox.Items.Add("9A0211 - Speaker #2 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0212 - Speaker #2 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0213 - Speaker #2 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0301 - Speaker #3 General Electric Failure.");
            this.availableListBox.Items.Add("9A0311 - Speaker #3 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0312 - Speaker #3 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0313 - Speaker #3 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0401 - Speaker #4 General Electric Failure.");
            this.availableListBox.Items.Add("9A0411 - Speaker #3 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0412 - Speaker #4 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0413 - Speaker #4 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0502 - Speaker #5 General Signal Failure");
            this.availableListBox.Items.Add("9A0602 - Speaker #6 General Signal Failure");
            this.availableListBox.Items.Add("9A5621 - Antenna signal amplitude below minimun value failure.");
            this.availableListBox.Items.Add("E00512 - Vehicle speed circuit short to battery failure.");

            int counter = 0; 
            string line;

            //get ignored dtcs
            try
            {
                file = new System.IO.StreamReader(appLoc + fileName);

                while ((line = file.ReadLine()) != null)
                {
                    this.ignoredListBox.Items.Add(line);
              
                    counter++;
                }

                if (counter > 0)
                {
                    Console.WriteLine("Ignored DTC List Loaded");
                }
                file.Close();
            }
            catch
            {

                Console.WriteLine("warning: dtc file not found....");
            }

           



            for (int x = 0; x < ignoredListBox.Items.Count; x++)
            {
                for (int y = 0; y < availableListBox.Items.Count; y++)
                {
                    if (ignoredListBox.Items[x].ToString() == availableListBox.Items[y].ToString())
                    {
                        availableListBox.Items.RemoveAt(y);
                    }
                }
            }
        }

        private void AddClose(object source)
        {

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

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (availableListBox.SelectedIndex != -1)
            {
                try
                {
                    ignoredListBox.Items.Add(availableListBox.SelectedItem);
                    availableListBox.Items.Remove(availableListBox.SelectedItem);
                }
                catch
                {
                    Console.WriteLine("Failure");
                }
            }
            
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            if (ignoredListBox.SelectedIndex != -1)
            {
                try
                {
                    bool inAvailable = false;
                    for (int x = 0; x < availableListBox.Items.Count; x++)
                    {
                        if (availableListBox.Items[x] == ignoredListBox.SelectedItem)
                        {
                            inAvailable = true;
                        }
                    }
                    if (!inAvailable)
                    {
                        availableListBox.Items.Add(ignoredListBox.SelectedItem);
                    }
                    ignoredListBox.Items.Remove(ignoredListBox.SelectedItem);
                }
                catch
                {
                    Console.WriteLine("Failure");
                }
            }
        }

 
        private void cancelButton_Click(object sender, EventArgs e)
        {

         //   ignoredDtcs.Clear();
            Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            ignoredDtcs.Clear();
            try
            {
              // file.WriteAllText(appLoc.ToString() + fileName, String.Empty); 

                Console.WriteLine("1");
                string[] lines = new string[ignoredListBox.Items.Count];

                for (int x = 0; x < ignoredListBox.Items.Count; x++)
                {
                    Console.WriteLine(ignoredListBox.Items[x].ToString() + "  " + x);
                }

                for (int x = 0; x < ignoredListBox.Items.Count; x++)
                {
                    Console.WriteLine(ignoredListBox.Items[x].ToString()); 
                    ignoredDtcs.Add(ignoredListBox.Items[x].ToString());
                    lines[x] = ignoredListBox.Items[x].ToString();
                }

                //write all ignored DTCs  


                Console.WriteLine("2");
                System.IO.File.WriteAllLines(appLoc.ToString() + fileName, lines); 
                // file.WriteAllText(appLoc + fileName, String.Empty);
                Console.WriteLine("");

                

                file.Close(); 

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }
            finally
            {
                OnIgnoreClose.Invoke(this);
                Close();
            }
        }


        public List<string> getDTCs()
        {   
            return ignoredDtcs;
        }


        private void addTS_Click(object sender, EventArgs e)
        {
          //  af.StartPosition = FormStartPosition.CenterScreen;
          //  af.ShowDialog();
        }

        private void ignoreAllButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < this.availableListBox.Items.Count; x++)
            {
                this.ignoredListBox.Items.Add(this.availableListBox.Items[x]);
               
            }

            this.availableListBox.Items.Clear();
        }

        private void removeIgnoresButton_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < this.ignoredListBox.Items.Count; x++)
            {
                this.availableListBox.Items.Add(this.ignoredListBox.Items[x]);

            }

            this.ignoredListBox.Items.Clear();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show( 
                "Are you sure you would like to reset all data to defualts?", "Revert to Default", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //remove dtc from ignore list and put them in availabe list 

                reset();

                //clear file 
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ignored_dtcs.txt", String.Empty);
              
            }
            
        }

        private void reset()
        {
            this.availableListBox.Items.Clear();
            this.ignoredListBox.Items.Clear(); 


            this.availableListBox.Items.Add("E10000 - Initial config not complete.");
            this.availableListBox.Items.Add("E10100 - Misconfiguration.");
            this.availableListBox.Items.Add("919F13 - GPS antenna circuit open.");
            this.availableListBox.Items.Add("919F01 - GPS antenna electrical failure.");
            this.availableListBox.Items.Add("9A8913 - SDARS antenna circuit open.");
            this.availableListBox.Items.Add("9A8901 - SDARS antenna electrical failure.");
            this.availableListBox.Items.Add("908E63 - Display touch screen stuck.");
            this.availableListBox.Items.Add("921C01 - Hard disk drive failure.");
            this.availableListBox.Items.Add("9D1949 - DVD mechanism electrical failure.");
            this.availableListBox.Items.Add("9D194B - DVD mechanism over temperature.");
            this.availableListBox.Items.Add("9D7813 - AUX input circuit open.");
            this.availableListBox.Items.Add("E01363 - Front bezel stuck button.");
            this.availableListBox.Items.Add("F00317 - Battery voltage above threshold.");
            this.availableListBox.Items.Add("F00316 - Battery voltage below threshold.");
            this.availableListBox.Items.Add("C14000 - Lost Comms body control module.");
            this.availableListBox.Items.Add("C15500 - Lost Comms IPC.");
            this.availableListBox.Items.Add("C15900 - Lost Comms PAM.");
            this.availableListBox.Items.Add("C16200 - Lost Comms Display.");
            this.availableListBox.Items.Add("C16400 - Lost Comms HVAC.");
            this.availableListBox.Items.Add("C19300 - Lost Comms External SDARS.");
            this.availableListBox.Items.Add("C19600 - Lost Comms with FES.");
            this.availableListBox.Items.Add("C19700 - Lost Comms with CPM.");
            this.availableListBox.Items.Add("C23800 - Lost Comms with DSP AMP.");
            this.availableListBox.Items.Add("C24900 - Lost Comms with RSEM.");
            this.availableListBox.Items.Add("C25600 - Lost Comms with FCIM.");
            this.availableListBox.Items.Add("C18400 - Lost Comms with ACM.");
            this.availableListBox.Items.Add("E01409 - Gyro hardware component failure.");
            this.availableListBox.Items.Add("E01441 - Flash checksum failure.");
            this.availableListBox.Items.Add("E01442 - Control Module General Memory Failure.");
            this.availableListBox.Items.Add("E01496 - Control Module Component Internal Failure.");
            this.availableListBox.Items.Add("E01496 - SDARS component hardware failure.");
            this.availableListBox.Items.Add("E01A51 - Calibration file missing.");
            this.availableListBox.Items.Add("F00045 - HDD FS-Info corrupt.");
            this.availableListBox.Items.Add("500101 - Rear camera no signal.");
            this.availableListBox.Items.Add("91BA1C - SWC1 voltage out of range.");
            this.availableListBox.Items.Add("92011C - SWC2 voltage out of range.");
            this.availableListBox.Items.Add("91BA63 - SWC1 stuck button.");
            this.availableListBox.Items.Add("920163 - SWC2 stuck button.");
            this.availableListBox.Items.Add("91BB12 - Rear park aide audio input circuit short to battery.");
            this.availableListBox.Items.Add("9A0101 - Speaker #1 General Electric Failure.");
            this.availableListBox.Items.Add("9A0111 - Speaker #1 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0112 - Speaker #1 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0113 - Speaker #1 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0201 - Speaker #2 General Electric Failure.");
            this.availableListBox.Items.Add("9A0211 - Speaker #2 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0212 - Speaker #2 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0213 - Speaker #2 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0301 - Speaker #3 General Electric Failure.");
            this.availableListBox.Items.Add("9A0311 - Speaker #3 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0312 - Speaker #3 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0313 - Speaker #3 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0401 - Speaker #4 General Electric Failure.");
            this.availableListBox.Items.Add("9A0411 - Speaker #3 Short to Ground Failure");
            this.availableListBox.Items.Add("9A0412 - Speaker #4 Short to Battery Failure");
            this.availableListBox.Items.Add("9A0413 - Speaker #4 Open Circuit Failure");
            this.availableListBox.Items.Add("9A0502 - Speaker #5 General Signal Failure");
            this.availableListBox.Items.Add("9A0602 - Speaker #6 General Signal Failure");
            this.availableListBox.Items.Add("9A5621 - Antenna signal amplitude below minimun value failure.");
            this.availableListBox.Items.Add("E00512 - Vehicle speed circuit short to battery failure.");
        }

        private void addNewButton_Click(object sender, EventArgs e)
        {
            if (this.hexTextBox.Text != "" && this.descTextBox.Text != "")
            {
                string dtc = hexTextBox.Text + " - " + this.descTextBox.Text;
                //add to ignored list box 
                ignoredListBox.Items.Add(dtc);

                //add to file 
                 string path = appLoc + addedDTCFilename;
                // This text is added only once to the file. 
                 if (!File.Exists(path))
                 {
                     // Create a file to write to. 
                     using (StreamWriter sw = File.CreateText(path))
                     {
                         sw.WriteLine(dtc);

                     }
                 }
                 else
                 {
                     using (StreamWriter sw = File.AppendText(path))
                     {
                         sw.WriteLine(dtc);
                       
                     }
                 }

                this.hexTextBox.Text = "";
                this.descTextBox.Text = "";

            }
            else
            {
                MessageBox.Show("Data incomplete");
            }
        }

       
        
    }
}