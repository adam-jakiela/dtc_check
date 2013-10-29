using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System;

using CoreScanner;

namespace ICSNeoCSharp
{
    class MotarolaScanner
    {

        bool connected = false;
        bool apiOpen = false;
        String deviceID = null;
        static CCoreScannerClass ccs;
        short[] scannerTypes;
        short numberOfScannerTypes = 1;
        short numberOfScanners;
        int status;
        int[] connectedScannerIDList;
        string scannerType;
        string scannerSerialNumber;
        int scannerID; 

        bool connectionWithCradle = false;

        const string SCANNER_SN = "DC447DB3422D4C4CA9880A9B65CA1007"; 

        string outXML;
        
        
        public MotarolaScanner()  
        {
            ccs = new CoreScanner.CCoreScannerClass();
            openAPI();
        }

        public string getConnectionMessage()
        {
            connectedScannerIDList = new int[255];
            ccs.GetScanners(out numberOfScanners, connectedScannerIDList, out outXML, out status);

            return outXML;

        } 

        public bool isConnected()
        {
            if (!apiOpen)
                openAPI(); 

            //get a list of the connected devices 
            connectedScannerIDList = new int[255];
            ccs.GetScanners(out numberOfScanners, connectedScannerIDList, out outXML, out status);

            if (status == 0)
            {

                //parse the xml 
                using (XmlReader reader = XmlReader.Create(new StringReader(outXML)))
                {
                    connectionWithCradle = true;
                    reader.ReadToFollowing("GUID");

                    //for testing
                    Console.WriteLine(reader.Value.ToString());

                    if (SCANNER_SN == reader.Value.ToString())
                        connected = true;
                    else
                        connected = false;

                    if (connected)
                    {
                        reader.ReadToFollowing("scannerID");
                        scannerID = Convert.ToInt32(reader.Value);
                    }

                }

            }
            else
            {
                connected = false;
                connectionWithCradle = false;
            }
                 
         

            return connected;
        }

        public void openAPI()
        {
            scannerTypes = new short[1];
            scannerTypes[0] = 1;
            numberOfScannerTypes = 1;

            //open the motarola API
            ccs.Open(0, scannerTypes, numberOfScannerTypes, out status);

            if (status == 0)
                apiOpen = true;
            else
                apiOpen = false;
        }


        //main getters
        public int numberOfConnectedScanners()
        {
            return this.numberOfScanners;
        }

        public bool apiIsOpen()
        {
            return this.apiOpen;
        }

        public String getDeviceId()
        {
            return this.deviceID;
        }

        public int[] getListOfConnections()
        {
            return connectedScannerIDList; 
        }

        public string getXMLoutput()
        {
            return this.outXML;
        }

        public string getScannerSerialNumber()
        {
            return scannerSerialNumber;
        }


        public int getScannerID()
        {
            return scannerID;
        }

        public bool getCradleConnection()
        {
            return connectionWithCradle;
        }

    }


}

