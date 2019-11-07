using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Constants
    {
        #region LegalRequirements
        public const byte passportExpirianTerm = 10;
        public const byte numOfWordsInFullName = 2;
        public const byte adultYears = 18;
        #endregion

        #region UserConstants
        public const string botName = "Samantha";
        public const string nameOfproduct = "loans";

        public const double interestRateCar = 0.0368;
        public const double interestRateEstate = 0.1318;
        public const double interestRateConsume = 0.1318;
        public const double interestRateOverdraft = 0.1268;

        public const string purposeCar = "for car purchasing only. You can choose any car you want: a new one from a car dealership or a used one.";
        public const string purposeConsume = "for any purchases you want. Our Corporation will not control the purpose of your lending. Also, you will get all the credit amount at once.";
        public const string purposeEstate = "for purchasing houses and appartments only.";
        public const string purposeOverdraft = "for any purchases you want. Our Corporation will not control the purpose of your lending. We will give you a credit card and you can lent as many, as you need rigth in the moment.";
        #endregion

        #region ConstantsNotForChange
        public const byte startNumberDefenition = 0;
        public const int monthInYear = 12;
        public const double toPer = 100;
        public const string yesString = "YES";
        public const string noString = "NO";
        public const string tellName = "your name";
        public const string tellSurname = "your surname";
        #endregion
    }
}
