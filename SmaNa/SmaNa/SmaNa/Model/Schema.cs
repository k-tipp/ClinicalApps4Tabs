using SmaNa.ViewModel;
using System.Collections.Generic;

namespace SmaNa.Model
{
    /// <summary>
    /// POCO to save the Schema with its data.
    /// </summary>
    public class Schema
    {
        public string filename { get; set; }
        public string name
        {
            get
            {
                if (ViewModelSettings.SmaNaSettings.LanguageString.Equals("fr-CH"))
                {
                    return Name_F;
                }
                else
                {
                    return Name_D;
                }
            }
        }
        public string Name_D { get; set; }
        public string Name_F { get; set; }
        public List<Appointment> appointments { get; set; }

        public Schema()
        {
            filename = "";
            appointments = new List<Appointment>();
        }
    }
}
