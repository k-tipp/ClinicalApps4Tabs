using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.Model
{
    public class Schema
    {
        public string filename { get; set; }
        public string name { get; set; }
        public List<Appointment> appointments { get; set; }

        public Schema()
        {
            name = "";
            filename = "";
            appointments = new List<Appointment>();
        }
    }
}
