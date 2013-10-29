using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ICSNeoCSharp
{
    public partial class FailedForm : Form
    {

        public List<device> devices = new List<device>(); 
        

        public FailedForm()
        {
            InitializeComponent();

            device testDevice = new device("1234serial", "5678part");
            devices.Add(testDevice); 

            for (int x = 0; x < devices.Count; x++)
            {
                string val1, val2, val3;
                val1 = devices[x].getSerialNum();
                val2 = devices[x].getPartNum();
                val3 = "NULL";

                failedDataGridView.Rows.Add(val1, val2, val3);
            }

             
        }

        public void setData(List<device> data)
        {
            this.devices = data; 
        }

        
    }
}