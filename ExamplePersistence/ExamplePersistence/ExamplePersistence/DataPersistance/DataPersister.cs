using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ExamplePersistence.Model;

namespace ExamplePersistence.DataPersistance
{
    public static class DataPersister
    {
        public static void serialize(DataStore dataStore)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DataStore));
            using (TextWriter writer = new StreamWriter(@"C:\Users\kevin\Desktop\xml\Xml.xml"))
            {
                serializer.Serialize(writer, dataStore);
            }
        }

        public static DataStore deserialize()
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(DataStore));
            TextReader reader = new StreamReader(@"C:\Users\kevin\Desktop\xml\Xml.xml");
            object obj = deserializer.Deserialize(reader);
            return (DataStore)obj;
            reader.Close();
        }

    }
}
