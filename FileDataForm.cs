using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    
    public partial class FileDataForm : Form
    {

        private DataSerializer ds;

        public FileDataForm()
        {
            InitializeComponent();
            
        }

        private void FileDataForm_Load(object sender, EventArgs e)
        {

            ds = DataSerializer.LoadFromFile();


            this.dataListBox.Items.Add("APP: " + ds.APLEXPECTED);
            this.dataListBox.Items.Add("CAL: " + ds.CALEXPECTED);
            this.dataListBox.Items.Add("PBL: " + ds.PBLEXPECTED);
            this.dataListBox.Items.Add("E2P: " + ds.E2PEXPECTED);
            this.dataListBox.Items.Add("SBL: " + ds.SBL);
            this.dataListBox.Items.Add("F110: " + ds.F110);
            this.dataListBox.Items.Add("F113: " + ds.F113EXPECTED);
            this.dataListBox.Items.Add("F124: " + ds.F124EXPECTED);
            this.dataListBox.Items.Add("F125: " + ds.F125EXPECTED);
            this.dataListBox.Items.Add("F188: " + ds.F188EXPECTED);
            this.dataListBox.Items.Add("FAILED: " + ds.FAILCOUNTER);
            this.dataListBox.Items.Add("PASSED: " + ds.PASSCOUNTER);
            this.dataListBox.Items.Add("DUPLICATE: " + ds.DUPLICATECOUNTER);
            this.dataListBox.Items.Add("PALLET: " + ds.QUANTITY);
            this.dataListBox.Items.Add("PACKAGE ID: " + ds.PACKAGEID);
            this.dataListBox.Items.Add("LOGFILE: " + ds.LOGFILE); 


        }
    }
}