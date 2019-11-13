using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Constants
    {
        #region LegalRequirements
        public const byte PassportExpirianTerm = 10;
        public const int PassportIDLength = 14;
        public const byte NumOfWordsInFullName = 2;
        public const byte AdultYears = 18;
        public const double LivingWageBudget = 231.00;
        public const int MonthInYear = 12;
        public const double ToPer = 100;
        public const int MilitaryAge = 27;
        #endregion

        #region UserConstants
        public const string BotName = "Samantha";
        public const string NameOfproduct = "loans";
        public const double CreditPossibilityRatio = 0.4;
        #region Interest Rates
        public const double InterestRateCar = 0.0368;
        public const double InterestRateEstate = 0.1318;
        public const double interestRateConsume = 0.1318;
        public const double interestRateOverdraft = 0.1268;
        #endregion

        #region Min Credit Sum
        public const double MinCreditSumCar = 500.00;
        public const double MinCreditSumEstate = 5000.00;
        public const double MinCreditSumConsume = 50.00;
        public const double MinCreditSumOverdraft = 50.00;
        #endregion

        #region Max Credit Sum
        public const double MaxCreditSumCar = 20000.00;
        public const double MaxCreditSumEstate = 150000.00;
        public const double MaxCreditSumConsume = 30000.00;
        public const double MaxCreditSumOverdraft = 50000.00;
        #endregion

        #region Credit Purpose
        public const string PurposeCar = "for car purchasing only. You can choose any car you want: a new one from a car dealership or a used one.";
        public const string PurposeConsume = "for any purchases you want. Our Corporation will not control the purpose of your lending. Also, you will get all the credit amount at once.";
        public const string PurposeEstate = "for purchasing houses and appartments only.";
        public const string PurposeOverdraft = "for any purchases you want. Our Corporation will not control the purpose of your lending. We will give you a credit card and you can lent as many, as you need rigth in the moment.";
        #endregion
        #endregion

        #region ConstantsNotForChange
        public const byte StartNumberDefenition = 0;
        public const int TimeForTakingDissicion = 5000;

        #region Phone Format
        public const int numberOfSlotsInPhoneNumberFormat = 4;
        public const int numberInFirstSlotInPhoneNumberFormat = 2;
        public const int numberInSecondSlotInPhoneNumberFormat = 3;
        public const int numberOfCharsInPhoneNumberFormat = 12;
        #endregion
        public const string YesString = "YES";
        public const string NoString = "NO";
        public const string YString = "Y";
        public const string NString = "N";
        public const string TellName = "your name";
        public const string TellSurname = "your surname";
        public const string Male = "M";
        public const string Female = "W";
        public const int MaleCode = 3;
        public const int FemaleCode = 4;
        public const int Applicant = 0;
        public const int Loan = 1;
        #endregion
    }
}
