using ExamplePersistence.DataPersistance;
using ExamplePersistence.Model;
using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePersistence
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting...");
            DataStore dataStore = new DataStore();
            dataStore.addAppointment(new Appointment()
            {
                Name = "first Appointment",
                AppointmentDate = new DateTime(2016, 1, 1),
                AppointmentDone = false,
                AppointmentFixed = true,
                Doctor = "Dr. House",
                Location = "Biel"
            });

            Console.WriteLine("Created a new data store...");

            dataStore.save();

            //Console.WriteLine("Stored the data store...");
            //Console.WriteLine("Reading the data store...");
            //Console.WriteLine("");
            //DataStore dataStore2 = DataPersister.deserialize();

            //Console.WriteLine("Before storing:".PadRight(30, ' ') + " | " + "Before storing:".PadRight(20, ' '));
            //for (int i = 0; i < dataStore.appointments.Count; i++)
            //{
            //    Console.WriteLine("".PadRight(53, '-'));
            //    Appointment appointment1 = dataStore.appointments.ElementAt(i);
            //    Appointment appointment2 = dataStore2.appointments.ElementAt(i);
            //    Console.WriteLine("Name:".PadRight(10, ' ') + appointment1.Name.ToString().PadRight(20, ' ') + " | " + appointment2.Name.ToString().PadRight(20, ' '));
            //    Console.WriteLine("Location:".PadRight(10, ' ') + appointment1.Location.ToString().PadRight(20, ' ') + " | " + appointment2.Location.ToString().PadRight(20, ' '));
            //    Console.WriteLine("Doctor:".PadRight(10, ' ') + appointment1.Doctor.ToString().PadRight(20, ' ') + " | " + appointment2.Doctor.ToString().PadRight(20, ' '));
            //    Console.WriteLine("Date:".PadRight(10, ' ') + appointment1.AppointmentDate.ToString().PadRight(20, ' ') + " | " + appointment2.AppointmentDate.ToString().PadRight(20, ' '));
            //    Console.WriteLine("Done:".PadRight(10, ' ') + appointment1.AppointmentDone.ToString().PadRight(20, ' ') + " | " + appointment2.AppointmentDone.ToString().PadRight(20, ' '));
            //    Console.WriteLine("Fixed:".PadRight(10, ' ') + appointment1.AppointmentFixed.ToString().PadRight(20, ' ') + " | " + appointment2.AppointmentFixed.ToString().PadRight(20, ' '));

            //}
            //Console.WriteLine("".PadRight(53, '-'));

            //Console.WriteLine("");
            Console.WriteLine("done");
            Console.Read();

        }
    }
}
