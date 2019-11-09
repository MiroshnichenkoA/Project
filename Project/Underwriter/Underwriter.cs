using System;
using System.Collections;

namespace Underwriter
{
    public static class Underwriter
    {
        static void Main()
        {
        }
        public static double EstimateSum(double income, (double, int) conditions)
        {
            return CreditPosibility(income) / (1 + conditions.Item1) * conditions.Item2;
        }
        public static ArrayList FinalSum(ArrayList profile)
        {
            return profile;
        }
        private static double CreditPosibility(double income)
        {
            return income * Constants.creditPossibilityRatio;
        }
        private static T SearchInProfile<T>(ArrayList profile, T info)
        {
            int index = profile.IndexOf(info);
            return (T)profile[index];
        }
    }
}
