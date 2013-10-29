using System;
using System.Collections.Generic;
using System.Text;

namespace ICSNeoCSharp
{
    #region Delegates
    public delegate void MyDataEvent(object source);
    #endregion

    class message
    {

    #region Variables

        private String data = null;
        private String serialNumber = null;
        private String partNumber = null;
        private String didF111 = null;
        private String didF113 = null;
        private String didF124 = null;
        private String didF125 = null;
        private String didF188 = null;
        private byte didRequest = 0x00;
        private String pbl = null;
        private String app = null;
        private String cal = null;
        private String e2p = null;
        private String result = null;
        private String lastSerialNumber = null;
        private String dtc = null;
        private String clearDTC = null;
        private int dtcCount = 0;
                
    #endregion

    #region Constructor

        public message()
        {
            
        }
        #endregion

    #region Properties
        /// <summary>
        /// Accessor string for scanned data
        /// </summary>
        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
                parseData(data);
            }
        }
        /// <summary>
        /// Accessor string for a string DTC list
        /// </summary>
        public string DTC
        {
            get
            {
                return dtc;
            }

            set
            {
                dtc = dtc + value;
            }
        }
        /// <summary>
        /// Accessor string for clearing the string DTC list
        /// </summary>
        public string ClearDTC
        {
            get
            {
                return clearDTC;
            }

            set
            {
                dtc = null;
            }
        }
        /// <summary>
        /// Accessor string for pass fail result
        /// </summary>
        public string Result
        {
            get
            {
                return result;
            }

            set
            {
                result = value;
            }
        }
        /// <summary>
        /// Accessor string for last serial number
        /// </summary>
        public string LastSerialNumber
        {
            get
            {
                return lastSerialNumber;
            }

            set
            {
                lastSerialNumber = value;
            }
        }
        /// <summary>
        /// Accessor string for serial number
        /// </summary>
        public string SerialNumber
        {
            get
            {
                return serialNumber;
            }

            set
            {
                //LastSerialNumber = SerialNumber;
                serialNumber = value;
            }
        }
        /// <summary>
        /// Accessor string for part number
        /// </summary>
        public string PartNumber
        {
            get
            {
                return partNumber;
            }

            set
            {
                partNumber = value;
            }
        }
        /// <summary>
        /// Accessor string for internal APP number
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
        /// Accessor string for internal CAL number
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
        /// Accessor string for internal E2P number
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
        /// Accessor string for internal PBL number
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
        /// Accessor string for DID F111
        /// </summary>
        /// <summary>
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
        /// <summary>
        /// Accessor string for DID F113
        /// </summary>
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
        /// Accessor int for the current count of detected DTCs
        /// </summary>
        public int DTCCount
        {
            get
            {
                return dtcCount;
            }

            set
            {
                dtcCount = value;
            }
        }
        /// <summary>
        /// Accessor byte for current DID Requested
        /// </summary>
        public byte DIDRequest
        {
            get
            {
                return didRequest;
            }

            set
            {
                didRequest = value;
            }
        }
    #endregion

    #region Functions

        private void parseData(string s)
        {
            string[] myString = new string[20];
            string temp = null;
            
            // Parse serial number only
            if (s.Substring(0, 1) == "S" && s.Substring(0, 2) != "SP")
            {
                temp = s.TrimEnd('\r');
                this.SerialNumber = s.TrimStart('S').ToString();
            }
            // Parse part number only
            else if (s.Substring(0,1) == "P" && s.Length < 28 && s.Substring(0,2) != "SP")
            {
                temp = s.TrimEnd('\r');
                temp = temp.Remove(0,1);
                temp = temp.Trim();
                myString = temp.Split();
                temp = "";
                foreach (string x in myString)
                    temp = temp + x;

                temp = temp.Substring(0,4) + "-" + temp.Substring(4,6) + "-" + temp.Substring(10,2);
                this.PartNumber = temp;
            }
            // Parse part number and serial number
            else if (s.Substring(0, 1) == "P" && s.Length > 28 && s.Substring(0, 2) != "SP")
            {
                temp = s.TrimEnd('\r');
                temp = temp.Remove(0, 2);
                temp = temp.Trim();
                myString = temp.Split();
                temp = "";
                foreach (string x in myString)
                    temp = temp + x;

                this.PartNumber = temp.Substring(0, 4) + "-" + temp.Substring(4, 6) + "-" + temp.Substring(10, 2);
                this.SerialNumber = temp.Substring(12, temp.Length - 12);
            }
        }

    #endregion

    }



}
