using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SmaNa.LocalDataAccess
{
    /// <summary>
    /// Generic Class to store and load CSV Data
    /// @created Kevin Tippenhauer
    /// </summary>
    public class CsvAccess
    {
        private IFileManager _fileManager;
        private string _fileName;

        /// <summary>
        /// initializes the CsvAccess which writes all the data to the File with the filename
        /// </summary>
        /// <param name="Filename">Name of the file the encrypted CSV is written to</param>
        public CsvAccess(string Filename)
        {
            _fileName = Filename;

            // The filemanager has the platformdependent fileaccess.
            _fileManager = App.FileManager;
        }

        /// <summary>
        /// Loads the file stored under the filename and returns its deserialized Item
        /// </summary>
        /// <returns>the stored item or its default if there is no item saved.</returns>
        public List<Appointment> Load(DateTime operationDate)
        {
            // Load encrypted file from filesystem
            string stringData = _fileManager.LoadAsset(_fileName);
            string[] lines = stringData.Split(Environment.NewLine.ToCharArray());
            List<Appointment> appointments = new List<Appointment>();

            foreach (string line in lines)
            {
                string[] fields = line.Split('|');
                if (fields.Length != 6)
                {
                    continue;
                }
                Appointment appointment = new Appointment();
                appointment.Name_D = fields[0];
                appointment.Name_F = fields[1];
                appointment.Location = "";
                appointment.Doctor = "";
                appointment.AppointmentPeriode = operationDate.Add(TimeSpan.ParseExact(fields[2], "c", null));
                appointment.AppointmentDate = default(DateTime);
                appointment.AppointmentReminder = (fields[3] == "true" ? true : false);
                appointment.Generated = true;
                appointments.Add(appointment);
            }

            return appointments.OrderBy(x=>x.AppointmentPeriode).ToList();
        }

        public Dictionary<string, Schema> LoadSchemas(string language)
        {

            Dictionary<string, string> schemasStrings = _fileManager.LoadSchemas(language);
            Dictionary<string, Schema> schemaDict = new Dictionary<string, Schema>();

            foreach (string schemaString in schemasStrings.Values)
            {
                string[] lines = schemaString.Split(Environment.NewLine.ToCharArray());
                List<Appointment> appointments = new List<Appointment>();

                Schema schema = new Schema();
                schema.filename = schemasStrings.Where(p => p.Value == schemaString).Select(p => p.Key).First<string>();


                foreach (string line in lines)
                {
                    string[] fields = line.Split('|');
                    if (fields.Length == 2)
                    {
                        // the schema name in line 1 is only splittable in 2 fields and therefor does not larger array
                        schema.Name_D = fields[0];
                        schema.Name_F = fields[1];
                        break;
                    }
                    Appointment appointment = new Appointment();
                    appointment.Name_D = fields[0];
                    appointment.Name_F = fields[1];
                    appointment.Generated = true;
                    appointments.Add(appointment);
                }
                schema.appointments = appointments;
                schemaDict.Add(schema.name, schema);
            }
            return schemaDict;
        }
    }
}
