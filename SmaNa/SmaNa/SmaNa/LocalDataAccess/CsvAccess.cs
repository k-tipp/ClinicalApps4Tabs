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
        public List<Appointment> Load()
        {
            // Load encrypted file from filesystem
            string stringData = _fileManager.LoadAsset(_fileName);
            string[] lines = stringData.Split(Environment.NewLine.ToCharArray());
            List<Appointment> appointments = new List<Appointment>();

            foreach(string line in lines)
            {
                string[] fields = line.Split('|');
                Appointment appointment = new Appointment();
                appointment.Name = fields[0];
                appointment.Location = fields[1];
                appointment.Doctor = fields[2];
                appointment.AppointmentPeriode = DateTime.Parse(fields[3]);
                appointment.AppointmentDate = DateTime.Parse(fields[4]);
                appointment.AppointmentReminder = (fields[5] == "true" ? true : false);
                appointment.AppointmentFixed = (fields[6] == "true" ? true : false);
                appointment.AppointmentDone = (fields[7] == "true" ? true : false);
                appointment.Generated = true;
                appointments.Add(appointment);
            }

            return appointments;
        }
    }
}
