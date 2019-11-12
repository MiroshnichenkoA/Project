using Project;
using System;
using System.Collections;

namespace Underwriter
{
    public static class Underwriter
    {
        public static double? FinalSum(ArrayList profile)
        {
            bool passportControl = PassportControl.ValidPassport(profile);
            if (passportControl == false || ManUnder27(profile)) 
            {
                return null;
            }
            else
            {
                try
                {
                    double check = (double)SearchInProfileApplicant(profile, (int)Field.Income);
                    check = (double)ExistingPaymonts(profile);
                    check = Constants.LivingWageBudget + Constants.LivingWageBudget * (int)SearchInProfileApplicant(profile, (int)Field.NumOfChild);
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
                double income = (double)SearchInProfileApplicant(profile, (int)Field.Income);
                double existingPayments = (double)ExistingPaymonts(profile);
                double elsePayments = Constants.LivingWageBudget + Constants.LivingWageBudget * (int)SearchInProfileApplicant(profile, (int)Field.NumOfChild);    
                income = income - existingPayments - elsePayments;
                double rate = (double)SearchInProfileLoan(profile, (int)Field.Rate);
                int term = (int)SearchInProfileLoan(profile, (int)Field.Term);
                try
                {
                    double check = CreditPosibility(income) / (1 + rate) * term;
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine($"Exception: {ex.Message}");
                    Console.WriteLine($"Method: {ex.TargetSite}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                }
                double creditSum = CreditPosibility(income) / (1 + rate) * term;
                double minSum = (double)SearchInProfileLoan(profile, (int)Field.MinSum);
                double maxSum = (double)SearchInProfileLoan(profile, (int)Field.MaxSum);
                if (creditSum < minSum) return null;
                if (creditSum > maxSum) return maxSum;
                return creditSum;
            }
        }

        public static double EstimateSum(double? income, (double, int) conditions)
        {
            try
            {
                double result = (double)CreditPosibility(income) / (1 + conditions.Item1) * conditions.Item2;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Method: {ex.TargetSite}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return (double)CreditPosibility(income) / (1 + conditions.Item1) * conditions.Item2;
        }

        static double CreditPosibility(double? income)
        {
            try
            {
                double result = (double)income * Constants.CreditPossibilityRatio;
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                Console.WriteLine($"Method: {ex.TargetSite}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
            return (double)income * Constants.CreditPossibilityRatio;
        }

        static double? ExistingPaymonts(ArrayList profile)
        {
            if ((DateTime)SearchInProfileLoan(profile, (int)Field.Expiry) > DateTime.Now) return (double)SearchInProfileLoan(profile, (int)Field.Paymont);
            else return null;
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
