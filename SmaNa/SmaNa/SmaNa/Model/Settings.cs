using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmaNa.Model
{
    class Settings
    {
        // the selected Langauge
        public string Language { get; set; }
        public string TnmT { get; set; }
        public string TnmN { get; set; }
        public DateTime OperationDate { get; set; }
        public string CarcinomType { get; set; }
        public bool StageingComplete { get; set; }        
    }
}
