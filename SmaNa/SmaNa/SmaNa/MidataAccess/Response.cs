using System;
using System.Collections.Generic;

namespace SmaNa.MidataAccess
{
    /// <summary>
    /// This classes handle the Response you get from Midata after any Query. Entry is currently only for BodyWeight.
    /// The Attributes of the classes are written small because midata attends it like this.
    /// </summary>
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
