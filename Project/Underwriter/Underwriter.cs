using System;

namespace Underwriter
{
    public static class Underwriter
    {
        static void Main()
        {
        }
        public static double EstimateSum(double income, (double, int) conditions)
        {
            double estimateSum = CreditPosibility(income);
            estimateSum = estimateSum / (1 + conditions.Item1);
            estimateSum = estimateSum * conditions.Item2;
            return estimateSum;
        }

        private static double CreditPosibility(double income)
        {
            double estimatePaymontPerMonth = income * Constants.creditPossibilityRatio;
            return estimatePaymontPerMonth;
        }
    }
}
