﻿using Project;
using System;
using System.Collections;

namespace Underwriter
{
    public static class Underwriter
    {
        private static readonly string _denyed = "Denyed. Reason: {0}";
        private static readonly string _reasonPassportControl = "The passport is expired or doesn;t belong to the applicant";

        public static string Denyed { get { return _denyed; } }
        public static string ReasonPassportControl { get { return _reasonPassportControl; } }

        static void Main()
        {
        }
        public static double FinalSum(ArrayList profile)
        {
            bool passportControl = PassportControl.ValidPassport(profile);
            if (passportControl == false) 
            {
                return Constants.Denyed;
                //TODO logger
            }
            else
            {
                double income = (double)SearchInProfileApplicant(profile, (int)Field.Income);
                double existingPayments = ExistingPaymonts(profile);
                double elsePayments = Constants.LivingWageBudget + Constants.LivingWageBudget * (int)SearchInProfileApplicant(profile, (int)Field.NumOfChild);
                return CreditPosibility(income - existingPayments - elsePayments) / (1 + (double)SearchInProfileLoan(profile, (int)Field.Rate)) * (double)SearchInProfileLoan(profile, (int)Field.Term);
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
        public static object SearchInProfileApplicant(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            return applicant.GetInfo(index);
        }

        public static object SearchInProfilePassport(ArrayList profile, int index)
        {
            Applicant applicant = (Applicant)profile[Constants.Applicant];
            Passport passport = applicant._passport;
            return passport.GetInfo(index);
        }

        public static object SearchInProfileLoan(ArrayList profile, int index)
        {
            Loan loan = (Loan)profile[Constants.Loan];
            return loan.GetInfo(index);
        }
    }
}
