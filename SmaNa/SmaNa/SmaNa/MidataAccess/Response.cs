using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.MidataAccess
{
    public class Response
    {
        public string resourceType;
        public string id;
        public meta meta;
        public string type;
        public int total;
        public List<link> link;
        public List<entry> entry;
    }

    public class entry
    {
        public string fullUrl;
        public BodyWeight resource;
    }

    public class meta
    {
        public DateTime lastUpdated;
    }
    public class link
    {
        public string relation;
        public string url;
    }
}
