using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SmaNa.LocalDataAccess
{
    /// <summary>
    /// Generic Class to store and load XML Data
    /// @created Marwin Philips
    /// </summary>
    /// <typeparam name="E">Has to be a somehow serializable Datatype which can be stored</typeparam>
    public class XMLAccess<E>
    {
        private IFileManager _fileManager;
        private string _fileName;
        // the serializer is used to serialize and dezerialize the data
        private XmlSerializer _serializer = new XmlSerializer(typeof(E));
        private Encrypter _encrypter;
        // used to write the XML. No newlines, no Indent (no Tabs to bring the xml in a human readable form), Encoding HAS to be Unicode for encryption.
        private XmlWriterSettings _writerSettings = new XmlWriterSettings() { NewLineHandling = NewLineHandling.None, Indent = false, Encoding = Encoding.Unicode };

        /// <summary>
        /// initializes the XMLAccess which writes all the encrypted data to the File with the filename
        /// </summary>
        /// <param name="Filename">Name of the file the encrypted XML is written to</param>
        public XMLAccess(string Filename)
        {
            _fileName = Filename;
            // The encrypter is used to encrypt / decrypt the data.
            _encrypter = App.Encrypter;
            // The filemanager has the platformdependent fileaccess.
            _fileManager = App.FileManager;
        }

        /// <summary>
        /// Saves the document handed over encrypted in the filename's file.
        /// </summary>
        /// <param name="document">The document to be stored</param>
        public void Save(E document)
        {
            // StringWriter used to not write the file directly on the filesystem, because we first have to encrypt it.
            var sw = new StringWriter();
            // the XmlWriter uses the special settings to use less space.
            XmlWriter xmlWriter = XmlWriter.Create(sw, _writerSettings);
            // actuall serializing of the document into a string
            _serializer.Serialize(xmlWriter, document);
            string textToEncrypt = sw.ToString();
            // encrypt the XML
            var encryptedString = _encrypter.EncryptAes(textToEncrypt);
            // and save it to the filename.
            _fileManager.SaveFile(_fileName, encryptedString);
        }

        /// <summary>
        /// Loads the encrypted file stored under the filename and returns its deserialized Item
        /// </summary>
        /// <returns>the stored item or its default if there is no item saved.</returns>
        public E Load()
        {
            // Load encrypted file from filesystem
            string encryptedData = _fileManager.LoadFile(_fileName);
            // decrypt the file
            string decryptedData = _encrypter.DecryptAes(encryptedData);
            // in case the file is empty-> return default value
            if (decryptedData == "")
            {
                return default(E);
            }
            else {
                // no special check for the specific type! creates the Object from the decrypted XML.
                return (E)_serializer.Deserialize(new StringReader(decryptedData));
            }
        }
    }
}
