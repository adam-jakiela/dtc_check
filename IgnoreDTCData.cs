using System;
using System.Collections.Generic;
using System.Text; 
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ICSNeoCSharp
{
    [Serializable()]
    class IgnoreDTCData :  ISerializable
    {

        private static readonly string _FILE_PATH = "IgnoreDTCList.osl";
        public List<string> dtcList = new List<string>(); 

        public IgnoreDTCData() 
        {
            dtcList.Clear();
        } 

        public IgnoreDTCData(SerializationInfo info, StreamingContext ctxt)
        {
            dtcList = (List<string>)info.GetValue("dtcList", typeof(List<string>));

        }

        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("dtcList", dtcList);
        }

        public static void saveToFile(IgnoreDTCData idd)
        {
            using (FileStream fileStream = new FileStream(_FILE_PATH, FileMode.OpenOrCreate))
            {

                BinaryFormatter binFormatter = new BinaryFormatter();
                binFormatter.Serialize(fileStream, idd);

            }
        }

        public static IgnoreDTCData LoadFromFile()
        {
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(_FILE_PATH, FileMode.Open);

                BinaryFormatter binFormatter = new BinaryFormatter();
                return (IgnoreDTCData)binFormatter.Deserialize(fileStream);
            }
            catch
            {
                return new IgnoreDTCData();

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





