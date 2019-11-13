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
        auto = 1,
        mortgage = 2,
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
    public enum Field
    {
        Surname = 0,
        Name = 1,
        Bitrhday = 2,
        Age = 3,
        InternalID = 4,
        Passport = 5,
        PhoneNumber = 6,
        Sex = 7,
        NumOfChild = 8,
        Income = 9,
        ID = 10,
        Issue = 11,
        Expiry = 12,
        Loan = 13,
        Purpose = 14,
        Rate = 15,
        Term = 16,
        MinSum = 17,
        MaxSum = 18,
        Amount = 19,
        Paymont = 20,
        Balance = 21
    }
}

