using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ICSNeoCSharp
{

        [Serializable()]
        class DataSerializer : ISerializable
        {

            private static readonly string _FILE_PATH = "DTCSaveState.osl";


            //gui 
            public string F111EXPECTED;
            public string F113EXPECTED;
            public string F124EXPECTED;
            public string F125EXPECTED;
            public string F188EXPECTED;
            public string APLEXPECTED;
            public string CALEXPECTED;
            public string PBLEXPECTED;
            public string E2PEXPECTED;
            public string PASSCOUNTERLABEL;
            public int PASSCOUNTER;
            public string FAILCOUNTERLABEL;
            public int FAILCOUNTER;
            public string DUPLICATECOUNTERLABEL;
            public int DUPLICATECOUNTER;
            public int PALLETPROGRESSBARVALUE;
            public int PALLETPRECENTAGE;
            public string PALLETSTATUSLABEL;
            public string STATUS;
            public string DTCRESULT;
            public string DEVICERESULT;
            
            //setup  
            public string SBL;
            public string PBL;
            public string F110;
            public List<string> DOCLIST = new List<string>();
            public int QUANTITY;
            public string PACKAGEID;
            public string OPERATORNAME;
            public string BENCH;

            //ignore dtc
            public List<string> FULLDTCLIST = new List<string>();
            public List<string> ADDEDDTCLIST = new List<string>();
            public List<string> IGNOREDDTCLIST = new List<string>();

            //maintentce variables 
            public bool readFile;
            public List<string> SERIALHISTORY = new List<string>();
            public string DTCLIST;
            public string LOGFILE;
           
            public DataSerializer()
            {
                 //main form
                 F111EXPECTED = "";
                 F113EXPECTED = "";
                 F124EXPECTED = "";
                 F125EXPECTED = "";
                 F188EXPECTED = "";
                 APLEXPECTED = "";
                 CALEXPECTED = "";
                 PBLEXPECTED = "";
                 E2PEXPECTED = "";
                 PASSCOUNTERLABEL = "";
                 PASSCOUNTER = 0;
                 FAILCOUNTERLABEL = "";
                 FAILCOUNTER = 0;
                 DUPLICATECOUNTERLABEL = "";
                 DUPLICATECOUNTER = 0;
                 PALLETPROGRESSBARVALUE = 0;
                 PALLETPRECENTAGE = 0;
                 PALLETSTATUSLABEL = "";
                 STATUS = "";
                 DTCRESULT = "";
                 DEVICERESULT = "";
            
                 //setup  
                 SBL = "";
                 PBL = "";
                 F110 = "";
                 DOCLIST.Clear();
                 QUANTITY = 0;
                 PACKAGEID = "";
                 OPERATORNAME = "";
                 BENCH = ""; 

                 //ignoreDTC
                 FULLDTCLIST.Clear();
                 ADDEDDTCLIST.Clear();
                 IGNOREDDTCLIST.Clear();

                // readFile = false;
                 SERIALHISTORY.Clear();
                 DTCLIST = "";
                 LOGFILE = "";

            }

            //DESERIALIZATION CONSTRUCT
            public DataSerializer(SerializationInfo info, StreamingContext ctxt)
            {
                F111EXPECTED = (string)info.GetValue("F111EXPECTED", typeof(string));
                F113EXPECTED = (string)info.GetValue("F113EXPECTED", typeof(string));
                F124EXPECTED = (string)info.GetValue("F124EXPECTED", typeof(string));
                F125EXPECTED = (string)info.GetValue("F125EXPECTED", typeof(string));
                F188EXPECTED = (string)info.GetValue("F188EXPECTED", typeof(string));
                APLEXPECTED = (string)info.GetValue("APLEXPECTED", typeof(string));
                CALEXPECTED = (string)info.GetValue("CALEXPECTED", typeof(string));
                PBLEXPECTED = (string)info.GetValue("PBLEXPECTED", typeof(string));
                E2PEXPECTED = (string)info.GetValue("E2PEXPECTED", typeof(string));

                PASSCOUNTERLABEL = (string)info.GetValue("PASSCOUNTERLABEL", typeof(string));
                PASSCOUNTER = (int)info.GetValue("PASSCOUNTER", typeof(int));
                FAILCOUNTERLABEL = (string)info.GetValue("FAILCOUNTERLABEL", typeof(string));
                FAILCOUNTER = (int)info.GetValue("FAILCOUNTER", typeof(int));
                DUPLICATECOUNTERLABEL = (string)info.GetValue("DUPLICATECOUNTERLABEL", typeof(string));
                DUPLICATECOUNTER = (int)info.GetValue("DUPLICATECOUNTER", typeof(int));

                PALLETPROGRESSBARVALUE = (int)info.GetValue("PALLETPROGRESSBARVALUE", typeof(int));
                PALLETPRECENTAGE = (int)info.GetValue("PALLETPRECENTAGE", typeof(int));
                PALLETSTATUSLABEL = (string)info.GetValue("PALLETSTATUSLABEL", typeof(string));
                STATUS = (string)info.GetValue("STATUS", typeof(string));
                DTCRESULT = (string)info.GetValue("DTCRESULT", typeof(string));
                DEVICERESULT = (string)info.GetValue("DEVICERESULT", typeof(string));

                SBL = (string)info.GetValue("SBL", typeof(string));
                PBL = (string)info.GetValue("PBL", typeof(string));
                F110 = (string)info.GetValue("F110", typeof(string));
                DOCLIST = (List<string>)info.GetValue("DOCLIST", typeof(List<string>));
                QUANTITY = (int)info.GetValue("QUANTITY", typeof(int));
                PACKAGEID = (string)info.GetValue("PACKAGEID", typeof(string));
                OPERATORNAME = (string)info.GetValue("OPERATORNAME", typeof(string));
                BENCH = (string)info.GetValue("BENCH", typeof(string));

                FULLDTCLIST = (List<string>)info.GetValue("FULLDTCLIST", typeof(List<string>));
                ADDEDDTCLIST = (List<string>)info.GetValue("ADDEDDTCLIST", typeof(List<string>));
                IGNOREDDTCLIST = (List<string>)info.GetValue("IGNOREDTCLIST", typeof(List<string>));

                readFile = (bool)info.GetValue("readFile", typeof(bool));
                SERIALHISTORY = (List<string>)info.GetValue("SERIALHISTORY", typeof(List<string>));
                DTCLIST = (string)info.GetValue("DTCLIST", typeof(string));
                LOGFILE = (string)info.GetValue("LOGFILE", typeof(string));

            }  

            //SERIALIZATION FUNCTION 
            public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
            {
                info.AddValue("F111EXPECTED", F111EXPECTED);
                info.AddValue("F113EXPECTED", F113EXPECTED);
                info.AddValue("F124EXPECTED", F124EXPECTED);
                info.AddValue("F125EXPECTED", F125EXPECTED);
                info.AddValue("F188EXPECTED", F188EXPECTED);
                info.AddValue("APLEXPECTED", APLEXPECTED);
                info.AddValue("CALEXPECTED", CALEXPECTED);
                info.AddValue("PBLEXPECTED", PBLEXPECTED);
                info.AddValue("E2PEXPECTED", E2PEXPECTED);
                info.AddValue("PASSCOUNTERLABEL", PASSCOUNTERLABEL);
                info.AddValue("PASSCOUNTER", PASSCOUNTER);
                info.AddValue("FAILCOUNTERLABEL", FAILCOUNTERLABEL);
                info.AddValue("FAILCOUNTER", FAILCOUNTER);
                info.AddValue("DUPLICATECOUNTERLABEL", DUPLICATECOUNTERLABEL);
                info.AddValue("DUPLICATECOUNTER", DUPLICATECOUNTER);
                info.AddValue("PALLETPROGRESSBARVALUE", PALLETPROGRESSBARVALUE);
                info.AddValue("PALLETPRECENTAGE", PALLETPRECENTAGE);
                info.AddValue("PALLETSTATUSLABEL", PALLETSTATUSLABEL);
                info.AddValue("STATUS", STATUS);
                info.AddValue("DTCRESULT", DTCRESULT);
                info.AddValue("DEVICERESULT", DEVICERESULT);
                info.AddValue("SBL", SBL);
                info.AddValue("PBL", PBL);
                info.AddValue("F110", F110);
                info.AddValue("DOCLIST", DOCLIST);
                info.AddValue("QUANTITY", QUANTITY);
                info.AddValue("PACKAGEID", PACKAGEID);
                info.AddValue("OPERATORNAME", OPERATORNAME);
                info.AddValue("BENCH", BENCH);
                info.AddValue("FULLDTCLIST", FULLDTCLIST);
                info.AddValue("ADDEDDTCLIST", ADDEDDTCLIST);
                info.AddValue("IGNOREDDTCLIST", IGNOREDDTCLIST);
                info.AddValue("readFile", readFile);
                info.AddValue("SERIALHISTORY", SERIALHISTORY);
                info.AddValue("DTCLIST", DTCLIST);
                info.AddValue("LOGFILE", LOGFILE);

            }


            public void reset()
            {
                //main form
                F111EXPECTED = "";
                F113EXPECTED = "";
                F124EXPECTED = "";
                F125EXPECTED = "";
                F188EXPECTED = "";
                APLEXPECTED = "";
                CALEXPECTED = "";
                PBLEXPECTED = "";
                E2PEXPECTED = "";
                PASSCOUNTERLABEL = "";
                PASSCOUNTER = 0;
                FAILCOUNTERLABEL = "";
                FAILCOUNTER = 0;
                DUPLICATECOUNTERLABEL = "";
                DUPLICATECOUNTER = 0;
                PALLETPROGRESSBARVALUE = 0;
                PALLETPRECENTAGE = 0;
                PALLETSTATUSLABEL = "";
                STATUS = "";
                DTCRESULT = "";
                DEVICERESULT = "";

                //setup  
                SBL = "";
                PBL = "";
                F110 = "";
                DOCLIST.Clear();
                QUANTITY = 0;
                PACKAGEID = "";
                OPERATORNAME = "";
                BENCH = "";

                //ignoreDTC
                FULLDTCLIST.Clear();
                ADDEDDTCLIST.Clear();
                IGNOREDDTCLIST.Clear();

                readFile = false;
                SERIALHISTORY.Clear();
                DTCLIST = "";
                LOGFILE = "";

            }

            public static void saveToFile(DataSerializer ds)
            {
                using (FileStream fileStream = new FileStream(_FILE_PATH, FileMode.OpenOrCreate))
                {

                    BinaryFormatter binFormatter = new BinaryFormatter();
                    binFormatter.Serialize(fileStream, ds);
                    fileStream.Close();

                }
            }

            public static DataSerializer LoadFromFile()
            {
                FileStream fileStream = null;
                try
                {
                    fileStream = new FileStream(_FILE_PATH, FileMode.Open);

                    BinaryFormatter binFormatter = new BinaryFormatter();
                    return (DataSerializer)binFormatter.Deserialize(fileStream);
                }
                catch
                {
                    return new DataSerializer();

                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();

                    }
                }

            }
        }
    }

