using System;

namespace Underwriter
{
    public static class Underwriter
    {
        public static double EstimateSum(double income)
        {
            double estimateSum = EstimateSumPrivate(income);
            return estimateSum;
        }

        private static double EstimateSumPrivate(double income)
        {
            double estimatePaymontPerMonth = income * Constants.creditPossibilityRatio;
            double estimateCreditSum = estimatePaymontPerMonth + estimatePaymontPerMonth;
            return estimateCreditSum;
        }
    }
}
