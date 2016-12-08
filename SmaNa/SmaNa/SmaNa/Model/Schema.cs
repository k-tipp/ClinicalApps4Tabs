using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.Model
{
    class Schema
    {
        string name { get; set; }
        List<Appointment> appointments { get; set; }

        public Schema()
        {
            name = "";
            appointments = new List<Appointment>();
        }
    }
}
