using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ICSNeoCSharp
{
	/// <summary>
	/// list of all available server types
	/// </summary>
	public enum eDATA_STATUS_BITFIELD_1//: long 
	{
		SPY_STATUS_GLOBAL_ERR = 0x01,
		SPY_STATUS_TX_MSG = 0x02,
		SPY_STATUS_XTD_FRAME = 0x04,
		SPY_STATUS_REMOTE_FRAME = 0x08,

		SPY_STATUS_CRC_ERROR = 0x10,
		SPY_STATUS_CAN_ERROR_PASSIVE = 0x20,
		SPY_STATUS_INCOMPLETE_FRAME = 0x40,
		SPY_STATUS_LOST_ARBITRATION = 0x80,

		SPY_STATUS_UNDEFINED_ERROR = 0x100,
		SPY_STATUS_CAN_BUS_OFF = 0x200,
		SPY_STATUS_CAN_ERROR_WARNING = 0x400,
		SPY_STATUS_BUS_SHORTED_PLUS = 0x800,

		SPY_STATUS_BUS_SHORTED_GND = 0x1000,
		SPY_STATUS_CHECKSUM_ERROR = 0x2000,
		SPY_STATUS_BAD_MESSAGE_BIT_TIME_ERROR = 0x4000,
		SPY_STATUS_IFR_DATA = 0x8000,

		SPY_STATUS_HARDWARE_COMM_ERROR = 0x10000,
		SPY_STATUS_EXPECTED_LEN_ERROR = 0x20000,
		SPY_STATUS_INCOMING_NO_MATCH = 0x40000,
		SPY_STATUS_BREAK = 0x80000,

		SPY_STATUS_AVSI_REC_OVERFLOW = 0x100000,
		SPY_STATUS_TEST_TRIGGER = 0x200000,
		SPY_STATUS_AUDIO_COMMENT = 0x400000,
		SPY_STATUS_GPS_DATA = 0x800000,

		SPY_STATUS_ANALOG_DIGITAL_INPUT = 0x1000000,
		SPY_STATUS_TEXT_COMMENT = 0x2000000,
		SPY_STATUS_NETWORK_MESSAGE_TYPE = 0x4000000,
		SPY_STATUS_VSI_TX_UNDERRUN = 0x8000000,

		SPY_STATUS_VSI_IFR_CRC_Bit = 0x10000000,
		SPY_STATUS_INIT_MESSAGE = 0x20000000,
		SPY_STATUS_HIGH_SPEED_MESSAGE = 0x40000000,
	}

	public enum eDATA_STATUS_BITFIELD_2 
	{
		SPY_STATUS2_HAS_VALUE = 0,
		SPY_STATUS2_VALUE_IS_BOOLEAN = 2,
		SPY_STATUS2_HIGH_VOLTAGE = 4,
		SPY_STATUS2_LONG_MESSAGE = 8,
	}

	public enum icsspy15765RxBitfield
	{
		icsspy15765RxErrGlobal = 2 ^ 0,
		icsspy15765RxErrCFRX_EXP_FF = 2 ^ 1,
		icsspy15765RxErrFCRX_EXP_FF = 2 ^ 2,
		icsspy15765RxErrSFRX_EXP_CF = 2 ^ 3,
		icsspy15765RxErrFFRX_EXP_CF = 2 ^ 4,
		icsspy15765RxErrFCRX_EXP_CF = 2 ^ 5,
		icsspy15765RxErrCF_TIME_OUT = 2 ^ 6,
		icsspy15765RxComplete = 2 ^ 7,
		icsspy15765RxInProgress = 2 ^ 8,
		icsspy15765RxErrSeqCntInCF = 2 ^ 9,
	}

	// Network ID
	public enum eNETWORK_ID: short  
	{
		NETID_DEVICE = 0,
		NETID_HSCAN = 1,
		NETID_MSCAN = 2,
		NETID_SWCAN = 3,
		NETID_LSFTCAN = 4,
		NETID_FORDSCP = 5,
		NETID_J1708 = 6,
		NETID_AUX = 7,
		NETID_JVPW = 8,
		NETID_ISO = 9,
		NETID_ISOPIC = 10,
		NETID_MAIN51 = 11,
		NETID_HOST = 12,
	}

	public enum icsConfigSetup : short
	{
		NEO_CFG_MPIC_HS_CAN_CNF1 = 512 + 10,
		NEO_CFG_MPIC_HS_CAN_CNF2 = 512 + 9,
		NEO_CFG_MPIC_HS_CAN_CNF3 = 512 + 8,
		NEO_CFG_MPIC_HS_CAN_MODE = 512 + 54,
	
		// med speed CAN
		NEO_CFG_MPIC_MS_CAN_CNF1 = 512 + 22,
		NEO_CFG_MPIC_MS_CAN_CNF2 = 512 + 21,
		NEO_CFG_MPIC_MS_CAN_CNF3 = 512 + 20,
	
		NEO_CFG_MPIC_SW_CAN_CNF1 = 512 + 34,
		NEO_CFG_MPIC_SW_CAN_CNF2 = 512 + 33,
		NEO_CFG_MPIC_SW_CAN_CNF3 = 512 + 32,
	
		NEO_CFG_MPIC_LSFT_CAN_CNF1 = 512 + 46,
		NEO_CFG_MPIC_LSFT_CAN_CNF2 = 512 + 45,
		NEO_CFG_MPIC_LSFT_CAN_CNF3 = 512 + 44,
	}

	// ePROTOCOL
	public enum ePROTOCOL : short
	{
		SPY_PROTOCOL_CUSTOM = 0,
		SPY_PROTOCOL_CAN = 1,
		SPY_PROTOCOL_GMLAN = 2,
		SPY_PROTOCOL_J1850VPW = 3,
		SPY_PROTOCOL_J1850PWM = 4,
		SPY_PROTOCOL_ISO9141 = 5,
		SPY_PROTOCOL_Keyword2000 = 6,
		SPY_PROTOCOL_GM_ALDL_UART = 7,
		SPY_PROTOCOL_CHRYSLER_CCD = 8,
		SPY_PROTOCOL_CHRYSLER_SCI = 9,
		SPY_PROTOCOL_FORD_UBP = 10,
		SPY_PROTOCOL_BEAN = 11,
		SPY_PROTOCOL_LIN = 12,
	}

	// Driver Type Constants
	public enum eDRIVER_TYPE : short 
	{
		INTREPIDCS_DRIVER_STANDARD = 0,
		INTREPIDCS_DRIVER_TEST = 1,
	}

	// Port Type Constants
	public enum ePORT_TYPE : short 
	{
		NEOVI_COMMTYPE_RS232 = 0,
		NEOVI_COMMTYPE_USB_BULK = 1,
		NEOVI_COMMTYPE_USB_ISO = 2,
		NEOVI_COMMTYPE_TCPIP = 3,
		NEOVI_COMMTYPE_USB_ISO_SN =	4,
	}
	[StructLayout(LayoutKind.Sequential)]	
	public struct icsSpyMessage   //reff
	{
		public int StatusBitField; //4
		public int StatusBitField2; //new '4
		public int TimeHardware; // 4
		public int TimeHardware2; //new ' 4
		public int TimeSystem; // 4
		public int TimeSystem2;
		public byte TimeStampHardwareID; //new ' 1
		public byte TimeStampSystemID;
		public byte NetworkID; //new ' 1
		public byte NodeID;
		public byte Protocol;
		public byte MessagePieceID; // 1
		public byte ColorID; //1
		public byte NumberBytesHeader; // 1
		public byte NumberBytesData; // 1
		public short DescriptionID; // 2
		public int ArbIDOrHeader; // Holds (up to 3 byte 1850 header or 29 bit CAN header) '4
		//public byte[] Data = new byte[8]; //(1 To 8); //8
		public byte Data1;
		public byte Data2;
		public byte Data3;
		public byte Data4;
		public byte Data5;
		public byte Data6;
		public byte Data7;
		public byte Data8;
		public byte AckBytes1;
		public byte AckBytes2;
		public byte AckBytes3;
		public byte AckBytes4;
		public byte AckBytes5;
		public byte AckBytes6;
		public byte AckBytes7;
		public byte AckBytes8;
		//public byte[] AckBytes = new byte[8]; //(1 To 8); //new '8
		public Single Value; // As Single ' 4
		public byte MiscData;
	}
	[StructLayout(LayoutKind.Sequential)]	
	public struct icsSpyMessageLong
	{
		public int StatusBitField; // 4
		public int StatusBitField2; //new '4
		public int TimeHardware;
		public int TimeHardware2; //new ' 4
		public int TimeSystem; //4
		public int TimeSystem2;
		public byte TimeStampHardwareID; //new ' 1
		public byte TimeStampSystemID;
		public byte NetworkID; //new ' 1
		public byte NodeID;
		public byte Protocol;
		public byte MessagePieceID; // 1
		public byte ColorID; // 1
		public byte NumberBytesHeader; //
		public byte NumberBytesData; //2
		public short DescriptionID; //2
		public int ArbIDOrHeader;// Holds (up to 3 byte 1850 header or 29 bit CAN header)
		public int DataMsb;
		public int DataLsb;
		public byte AckBytes1;
		public byte AckBytes2;
		public byte AckBytes3;
		public byte AckBytes4;
		public byte AckBytes5;
		public byte AckBytes6;
		public byte AckBytes7;
		public byte AckBytes8;
		public Single Value; // As Single
		public byte MiscData;
    
	}
	[StructLayout(LayoutKind.Sequential)]	
	public struct spyFilterLong
	{
		public int StatusValue; 
		public int StatusMask;
		public int Status2Value;
		public int Status2Mask;
		public int Header;
		public int HeaderMask; 
		public int MiscData;
		public int MiscDataMask; 
		public int ByteDataMsb;
		public int ByteDataLsb;
		public int ByteDataMaskMsb;
		public int ByteDataMaskLsb;
		public int HeaderLength;
		public int ByteDataLength;
		public int NetworkID;
		public bool FrameMaster;
		public byte bStuff1;
		public byte bStuff2;
		public int ExpectedLength;
		public int NodeID;
	}


	[StructLayout(LayoutKind.Sequential)]	
	public struct icsSpyMessageJ1850 
	{
		public int StatusBitField; //4
		public int StatusBitField2; //new '4
		public int TimeHardware; //4
		public int TimeHardware2; //new ' 4
		public int TimeSystem; //4
		public int TimeSystem2;
		public byte TimeStampHardwareID; //new ' 1
		public byte TimeStampSystemID;
		public byte NetworkID; //new ' 1
		public byte NodeID;
		public byte Protocol;
		public byte MessagePieceID; // 1 new
		public byte ColorID; // 1
		public byte NumberBytesHeader; //1
		public byte NumberBytesData; //1
		public short DescriptionID; //2
		public byte Header1;  //Holds (up to 3 byte 1850 header or 29 bit CAN header)
		public byte Header2;
		public byte Header3;
		public byte Header4;
		public byte Data1;
		public byte Data2;
		public byte Data3;
		public byte Data4;
		public byte Data5;
		public byte Data6;
		public byte Data7;
		public byte Data8;
		public byte AckBytes1;
		public byte AckBytes2;
		public byte AckBytes3;
		public byte AckBytes4;
		public byte AckBytes5;
		public byte AckBytes6;
		public byte AckBytes7;
		public byte AckBytes8;
		public Single Value; // As Single '4
		public byte MiscData;
	}


	/// <summary>
	/// Summary description for dllimports.
	/// </summary>
	public class icsNeoDll
	{
		public const double NEOVI_TIMEHARDWARE2_SCALING = 0.1048576;
		public const double NEOVI_TIMEHARDWARE_SCALING = 0.0000016;

		public const double NEOVIPRO_VCAN_TIMEHARDWARE2_SCALING = 0.065536;
		public const double NEOVIPRO_VCAN_TIMEHARDWARE_SCALING = 0.000001;

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoOpenPort(int lPortNumber,int lPortType, int lDriverType,	ref byte bNetworkID, ref byte bSCPFunctionID, ref int hObject);

		[DllImport("icsneo40.dll")]
		public static extern int icsneoOpenPortEx(int lPortNumber, int lPortType, int lDriverID,int lIPAddressMSB, int lIPAddressLSBOrBaudRate, int lForceConfigRead, ref byte bNetworkID,ref int hObject);
		
		[DllImport("icsneo40.dll")] 
		public static extern int icsneoClosePort(int hObject,ref int pNumberOfErrors);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoGetMessages(int hObject, ref icsSpyMessage pMsg, ref int pNumberOfMessages, ref int pNumberOfErrors);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoTxMessages(int hObject,ref icsSpyMessage pMsg,int iNetwork,int iNumMessages);

		[DllImport("icsneo40.dll")] 
		public static extern void icsneoFreeObject(int hObject);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoGetDLLVersion();

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoFindAllCOMDevices(int lDriverID, int lGetSerialNumbers,int lStopAtFirst, int iUSBCommOnly, ref int p_lDeviceTypes, ref int p_lComPorts,ref int p_lSerialNumber, ref int lNumDevices);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoFindAllUSBDevices(int lDriverID, int lGetSerialNumbers,ref int p_lDevices,ref int p_lSerialNumbers,ref int p_lOpenedDevices,ref int lNumDevices);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoGetConfiguration (int hObject,ref byte p_bData,ref int lNumBytes);
		
		[DllImport("icsneo40.dll")] 
		public static extern int icsneoSendConfiguration(int hObject, ref byte p_bData, int iNumBytes);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoEnableNetworkCom(int hObject, int lEnable);

		[DllImport("icsneo40.dll")]
		public static extern void icsneoGetISO15765Status(int hObject, int iNetwork, int iClearTxStatus, int iClearRxStatus, ref int lTxStatus, ref int lRxStatus);
		
		[DllImport("icsneo40.dll")]
		public static extern void icsneoSetISO15765RxParameters (int  hObject, int iNetwork, int iEnable, out spyFilterLong pFF_CFMsgFilter , out icsSpyMessage pFlowCTxMsg, int lCFTimeOutMs, int lFlowCBlockSize, int lUsesExtendedAddressing, int lUseHardwareIfPresent);

		[DllImport("icsneo40.dll")]
		public static extern int icsneoStartSockServer(int hObject, int iPort);

		[DllImport("icsneo40.dll")]
		public static extern int icsneoStopSockServer(int hObject);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoGetErrorMessages(int hObject, ref int p_lErrorsMsq, ref int lNumberOfErrors);

		[DllImport("icsneo40.dll")] 
		public static extern int icsneoGetPerformanceParameters(int hObject,ref int iBufferCount, ref int iBufferMax, ref int iOverFlowCount , ref int iReserved1, ref int iReserved2 , ref int iReserved3, ref int iReserved4 ,ref int iReserved5);
		
		[DllImport("icsneo40.dll", ExactSpelling = true, CharSet = CharSet.Ansi , CallingConvention = CallingConvention.Winapi)] 
		public static extern int icsneoGetErrorInfo(int iErrorNumber , StringBuilder sErrorDescriptionShort, StringBuilder sErrorDescriptionLong, ref int iMaxLengthShort, ref int iMaxLengthLong, ref int lErrorSeverity , ref int lRestartNeeded);

		public static double icsneoGetTimeStamp(long TimeHardware, long TimeHardware2) 
		{
			return NEOVI_TIMEHARDWARE2_SCALING * TimeHardware2 + NEOVI_TIMEHARDWARE_SCALING * TimeHardware;
		}
	

		public static bool CreateIPParts(string sIPAddress, ref int iIPMsb, ref int iIPLsb)
		{
			//This function is to aid in Converting IP addresses
			// The sIPAddress String input is in form of xxx.xxx.xxx.xxx
			// The output is the values for MSB and LSB
			string[] vParts;
			Double dValue;
			
			vParts = sIPAddress.Split('.');
	
			if (vParts.GetUpperBound(0) != 3 )
			return(false);
		
			dValue = Convert.ToDouble(vParts[3]);
			if (dValue < 0) 
				return(false);
	
			if (dValue > 255) 
				return(false);
		
			iIPMsb = Convert.ToInt32(dValue) * 256;
		
			dValue = Convert.ToDouble(vParts[2]);
			if (dValue < 0) 
				return(false);
			if (dValue > 255) 
				return(false);
		
			iIPMsb = iIPMsb + Convert.ToInt32(dValue);
		
			dValue = Convert.ToDouble(vParts[1]);
			if (dValue < 0) 
					return(false);
			if (dValue > 255)
					return(false);
		
			iIPLsb = Convert.ToInt32(dValue) * 256;
	
			dValue = Convert.ToDouble(vParts[0]);
			if (dValue < 0 )
				return(false);
			if (dValue > 255)
				return(false);
	
			iIPLsb = iIPLsb + Convert.ToInt32(dValue);
	
			return(true);
		}

		
	}

}