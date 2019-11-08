using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public enum SimpleAnswers
    {
        YES = 1,
        NO = 2,
        AGREE = 3
    }
    public enum ScriptForTimeOfDay
    {
        morning = 6,
        day = 11,
        evening = 18,
        night = 24
    }
    public enum LoanName
    {
        car = 1,
        estate = 2,
        consumer = 3,
        overdraft = 4 
    }
    public enum MaxTermForLoan
    {
        car = 84,
        estate = 240,
        consumer = 60,
        overdraft = 12
    }
}

