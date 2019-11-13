using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    [AttributeUsage(AttributeTargets.Field)]
    public class AdultAgeAttribute : System.Attribute
    {
        public int Age { get; set; }

        public AdultAgeAttribute()
        {
            Age = Constants.AdultYears;
        }
    }
}
