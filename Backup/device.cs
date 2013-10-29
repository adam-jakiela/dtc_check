using System;
using System.Collections.Generic;
using System.Text;

namespace ICSNeoCSharp
{
    //used to store scanned devices and their data from a session
    public class device
    {
        //we will use strings instead of integers for conversion simplicity
        public string apl = null;
        public string e2p = null;
        public string cal = null;
        public string pbl = null; 
        public string serialNum = null;
        public string partNum = null;
        public string serialPartSum = null;
               
        public DateTime scannedTime;

        public device(string _serialNum, string _partNum)
        {
            serialNum = _serialNum;
            partNum = _partNum;
            makeSum(partNum, serialNum);
           // scannedTime = _scannedTime;
        } 

        //getters 
        public string getSerialNum() { return this.serialNum; }
        public string getPartNum() { return this.partNum; }
      //  public string getScannedTime() { return this.scannedTime; }  
        public string getPartSerialSum() { return serialPartSum; } 

        // setters 
        public void setSerialNum(string _sn) { serialNum = _sn; } 
        public void setPartNum(string _pn) { partNum = _pn; }
        public void setScannedTime(DateTime _st) { scannedTime = _st; }

        public void makeSum(string _part, string _serial)
        {
            serialPartSum = _part + _serial; 
        }

        //overloaded method
        public void makeSum()
        {
            serialPartSum = partNum + serialNum;
        }





        /*
        //getters
        public string getApl() { return this.apl; }
        public string get() { return this.apl; }
        public string getApl() { return this.apl; }
        public string getApl() { return this.apl; }
        public string getApl() { return this.apl; }
        public string getApl() { return this.apl; }
        public string getApl() { return this.apl; }
        public string getApl() { return this.apl; }

        //setters
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; }
        public void setApl(string _apl) { apl = _apl; } 
        */



    }
}
