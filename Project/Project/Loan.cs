using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public abstract class Loan
    {
        #region Fields
        protected private static LoanName _name;
        protected private static string _purpose;
        protected private static double _interestRate;
        protected private static int _maxTermForLoan;
        protected private DateTime? _issueTime;
        protected private DateTime? _experianTime;
        protected private double? _creditAmount;
        protected private double? _paymontPerMonth;
        protected private double? _currentBalance;
        protected private static double _minSum;
        protected private static double _maxSum;
        #endregion

        #region Properties
        public static int Term { get { return _maxTermForLoan; } }
        public static double InterestRate { get { return _interestRate; } }
        public static LoanName Name { get { return _name; } }
        #endregion

        private static (LoanName, int, double, string, double, double) Conditions()
        {
            return (_name, _maxTermForLoan / Constants.MonthInYear, _interestRate * Constants.ToPer, _purpose, _minSum, _maxSum);
        }

        private (LoanName, double) ThisConditions()
        {
            (LoanName, double) conditions = (_name, _interestRate);
            return conditions;
        }

        private (double, int) ThisConditionsForUnderwriter()
        {
            (double, int) conditions = (_interestRate, _maxTermForLoan);
            return conditions;
        }

        public object GetInfo(int index)
        {
            switch (index)
            {
                case (int)Field.Loan:
                    return _name;
                    break;
                case (int)Field.Purpose:
                    return _purpose;
                    break;
                case (int)Field.Rate:
                    return _interestRate;
                    break;
                case (int)Field.Term:
                    return _maxTermForLoan;
                    break;
                case (int)Field.MinSum:
                    return _minSum;
                    break;
                case (int)Field.MaxSum:
                    return _maxSum;
                    break;
                case (int)Field.Issue:
                    return _issueTime;
                    break;
                case (int)Field.Expiry:
                    return _experianTime;
                    break;
                case (int)Field.Amount:
                    return _creditAmount;
                    break;
                case (int)Field.Paymont:
                    return _paymontPerMonth;
                    break;
                case (int)Field.Balance:
                    return _currentBalance;
                    break;
            }
            return null;
        }
    }
}
