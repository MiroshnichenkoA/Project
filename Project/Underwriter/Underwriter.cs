using System;
using System.Collections;

namespace Underwriter
{
    public static class Underwriter
    {
        static void Main(ArrayList profile)
        {
           bool passportControl = PassportControl.ValidPassport(profile);
            // check other credits
            // calculate sum
        }
            
            public static double EstimateSum(double income, (double, int) conditions)
            {
                return CreditPosibility(income) / (1 + conditions.Item1) * conditions.Item2;
            }
            
            public static double FinalSum(ArrayList profile)
            {

                return 10;
            }

            static double CreditPosibility(double income)
            {
                return income * Constants.creditPossibilityRatio;
            }
            static T SearchInProfile<T>(ArrayList profile, T info)
            {
                int index = profile.IndexOf(info);
                return (T)profile[index];
            }
        }
    }
