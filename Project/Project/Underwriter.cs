using Project;
using System;
using System.Collections;

namespace Underwriter
{
    public static class Underwriter
    {
        private static readonly string _denyed = "Denyed";

        public static string Denyed { get { return _denyed; } }

        public static double FinalSum(ArrayList profile)
        {
            bool passportControl = PassportControl.ValidPassport(profile);
            if (passportControl == false || ManUnder27(profile)) 
            {
                return Constants.Denyed;
                //TODO logger
            }
            else
            {
                double income = (double)SearchInProfileApplicant(profile, (int)Field.Income);
                double existingPayments = ExistingPaymonts(profile);
                double elsePayments = Constants.LivingWageBudget + Constants.LivingWageBudget * (int)SearchInProfileApplicant(profile, (int)Field.NumOfChild);
                income = income - existingPayments - elsePayments;
                double rate = (double)SearchInProfileLoan(profile, (int)Field.Rate);
                int term = (int)SearchInProfileLoan(profile, (int)Field.Term);
                return CreditPosibility(income) / (1 + rate) * term;
            }
        }
        public static double EstimateSum(double income, (double, int) conditions)
        {
            return CreditPosibility(income) / (1 + conditions.Item1) * conditions.Item2;
        }
        static double CreditPosibility(double income)
        {
            return income * Constants.CreditPossibilityRatio;
        }

        static double ExistingPaymonts(ArrayList profile)
        {
            if ((DateTime)SearchInProfileLoan(profile, (int)Field.Expiry) > DateTime.Now) return (double)SearchInProfileLoan(profile, (int)Field.Paymont);
            else return 0;
        }

        static bool ManUnder27(ArrayList profile)
        {
            string sex = (string)SearchInProfileApplicant(profile, (int)Field.Sex);
            int age = (int)SearchInProfileApplicant(profile, (int)Field.Age);

            if (sex == Constants.Male)
            {
                if (age < Constants.MilitaryAge) return true;
                else return false;
            }
            else return false;
        }
        public static object SearchInProfileApplicant(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            return applicant.GetInfo(index);
        }

        public static object SearchInProfilePassport(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            Passport passport = applicant.Passport;
            return passport.GetInfo(index);
        }

        public static object SearchInProfileLoan(ArrayList profile, int index)
        {
            dynamic loan = (dynamic)profile[Constants.Loan];
            return loan.GetInfo(index);
        }
    }
}
