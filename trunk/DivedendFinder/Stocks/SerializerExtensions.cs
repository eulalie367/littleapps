using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        public static IEnumerable<T> DeSerialze_Binary<T>(this string fileName) //where T : Serializable
        {
            IEnumerable<T> retVal = null;
            try
            {
                using (Stream str = File.OpenRead(fileName))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    var a = formatter.Deserialize(str);
                    retVal = a as IEnumerable<T>;
                }
            }
            catch
            {
            }
            return retVal;
        }
    }
}
