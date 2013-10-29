using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO; 

using CoreScanner; 



namespace ICSNeoCSharp
{

    class MotorolaScan
    {

        static CCoreScannerClass css = new CoreScanner.CCoreScannerClass();
        short[] scannerTypes;
        short numberOfScannerTypes;
        int status;
       

        public MotorolaScan()
        {
            
            openAPI(); 
        }

        private void openAPI()
        {
            scannerTypes = new short[1];
            scannerTypes[0] = 1;
            numberOfScannerTypes = 1;

           // ccs.Open(0, scannerTypes, numberOfScannerTypes, out status);
                          
        }

        public void sendScannerCommand()
        {

        }

       /* public bool scannerConnected()  {
            shot numberOfScanners;
            int[] connectedScannerIDList = new int[255];

            string outxml;

            ccs.GetScanners(out numberOfScanners, connectedScannerIDList, out outxml, out status);

            //parse the xml file to see if any devices are listed 

            StringBuilder output = new StringBuilder();

            using (XmlReader reader = new XmlReader.Create(new StringReader(outxml)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;

            }

        */

        } 
        

      
    }

