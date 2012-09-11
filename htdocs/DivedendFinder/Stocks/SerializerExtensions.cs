using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Extensions
{
    public static class SerializerExtensions
    {
        public static bool Serialze_Binary(this object ls, string fileName)
        {
            try
            {
                using (Stream str = File.OpenWrite(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(str, ls);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static T DeSerialze_Binary<T>(this string fileName) //where T : Serializable
        {
            T retVal = default(T);
            try
            {
                using (Stream str = File.OpenRead(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var a = formatter.Deserialize(str);
                    retVal = (T) a;
                }
            }
            catch
            {
            }
            return retVal;
        }
        public static bool Serialze_XML(this object ls, string fileName)
        {
            try
            {
                using (Stream str = File.OpenWrite(fileName))
                {
                    XmlSerializer formatter = new XmlSerializer(ls.GetType());
                    formatter.Serialize(str, ls);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
