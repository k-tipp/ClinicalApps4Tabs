using System;
using System.Globalization;
using System.Xml.Serialization;

namespace SmaNa.Model
{
    /// <summary>
    /// POCO to save the Settings
    /// </summary>
    public class Settings
    {
        // the selected Langauge
        [XmlIgnore]
        public CultureInfo Language { get; set; }
        public string LanguageString
        {
            get
            {
                return Language.Name;
            }
            set
            {
                Language = new CultureInfo(value);
            }
        }
        //public Enumerations.TnmT TnmT { get; set; }
        //public Enumerations.TnmN TnmN { get; set; }
        //public Enumerations.TnmM TnmM { get; set; }
        public string Schema { get; set; }
        public DateTime OperationDate { get; set; }
        //public Enumerations.CancerType CarcinomType { get; set; }
        public bool StageingComplete { get; set; }
    }
}
