using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class AdultAgeAttribute : System.Attribute
    {
        public int Age { get; set; }

        public AdultAgeAttribute()
        {
            Age = Constants.adultYears;
        }
    }
}
