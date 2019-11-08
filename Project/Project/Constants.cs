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

        #region Interest Rates
        public const double interestRateCar = 0.0368;
        public const double interestRateEstate = 0.1318;
        public const double interestRateConsume = 0.1318;
        public const double interestRateOverdraft = 0.1268;
        #endregion

        #region Min Credit Sum
        public const double minCreditSumCar = 500.00;
        public const double minCreditSumEstate = 5000.00;
        public const double minCreditSumConsume = 50.00;
        public const double minCreditSumOverdraft = 50.00;
        #endregion

        #region Max Credit Sum
        public const double maxCreditSumCar = 20000.00;
        public const double maxCreditSumEstate = 150000.00;
        public const double maxCreditSumConsume = 30000.00;
        public const double maxCreditSumOverdraft = 50000.00;
        #endregion

        #region Credit Purpose
        public const string purposeCar = "for car purchasing only. You can choose any car you want: a new one from a car dealership or a used one.";
        public const string purposeConsume = "for any purchases you want. Our Corporation will not control the purpose of your lending. Also, you will get all the credit amount at once.";
        public const string purposeEstate = "for purchasing houses and appartments only.";
        public const string purposeOverdraft = "for any purchases you want. Our Corporation will not control the purpose of your lending. We will give you a credit card and you can lent as many, as you need rigth in the moment.";
        #endregion
        #endregion

        #region ConstantsNotForChange
        public const byte startNumberDefenition = 0;
        public const int monthInYear = 12;
        public const double toPer = 100;
        #region Phone Format
        public const int numberOfSlotsInPhoneNumberFormat = 4;
        public const int numberInFirstSlotInPhoneNumberFormat = 2;
        public const int numberInSecondSlotInPhoneNumberFormat = 3;
        public const int numberOfCharsInPhoneNumberFormat = 12;
        #endregion
        public const string yesString = "YES";
        public const string noString = "NO";
        public const string tellName = "your name";
        public const string tellSurname = "your surname";
        public const string male = "M";
        public const string female = "F";
        #endregion
    }
}
