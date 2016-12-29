using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/*
data = {
        resourceType: 'Observation',
        code: {
        coding: [{
            system: 'http://loinc.org',
            code: '3141-9',
            display: 'Weight Measured'
        }]
        },
        effectiveDateTime: dateTime,
        valueQuantity: {
          value: val,
          unit: 'kg',
          system: 'http://unitsofmeasure.org'
        }
      };
    }
    
    */
namespace SmaNa.MidataAccess
{
    public class BodyWeight
    {
        public string resourceType;
        public code code;
        public DateTime effectiveDateTime;
        public valueQuantity valueQuantity;
        // Default constructor still needed for Deserialization.
        public BodyWeight()
        {

        }
        public BodyWeight(string weight, DateTime dateTime)
        {
            resourceType = "Observation";
            code = new code() { coding = new List<Coding>() { new Coding() { system = "http://loinc.org", code = "3141-9", display = "Weight Measured" } } };
            effectiveDateTime = dateTime;
            valueQuantity = new valueQuantity() { system = "http://unitsofmeasure.org", unit = "kg", value = weight };
        }
    }

    public class code
    {
        public List<Coding> coding;
    }
    public class Coding
    {
        public string system;
        public string code;
        public string display;
    }
    public class valueQuantity
    {
        public string value;
        public string unit;
        public string system;
    }
}
