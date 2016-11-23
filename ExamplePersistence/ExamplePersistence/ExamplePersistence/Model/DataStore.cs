﻿using ExamplePersistence.DataPersistance;
using SmaNa.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamplePersistence.Model
{
    public class DataStore
    {
        public DataStore()
        {
            this.appointments = new List<Appointment>();
        }
        public List<Appointment> appointments { get; set; }


        public void addAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }

        public void save()
        {
            DataPersister.serialize(this);
        }
    }
}
