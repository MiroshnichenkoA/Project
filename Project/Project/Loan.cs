using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public abstract class Loan
    {
        #region Fields
        protected static LoanName _name;
        protected static string _purpose;
        protected static double _interestRate;
        protected static int _maxTermForLoan;
        protected DateTime _issueTime;
        protected DateTime _experianTime;
        protected double _creditAmount;
        protected double _paymontPerMonth;
        protected double _currentBalance;
        protected static double _minSum;
        protected static double _maxSum;
        #endregion
        public static double InterestRate { get { return _interestRate; } }
        public static LoanName Name { get { return _name; } }

        public static (LoanName, int, double, string, double, double) Conditions()
        {
            (LoanName, int, double, string, double, double) conditions;
            return conditions = (_name, _maxTermForLoan / Constants.monthInYear, _interestRate * Constants.toPer, _purpose, _minSum, _maxSum);
        }
        public (LoanName, double) ThisConditions()
        {
            (LoanName, double) conditions = (_name, _interestRate);
            return conditions;
        }
        public (double, int) ThisConditionsForUnderwriter()
        {
            (double, int) conditions = (_interestRate, _maxTermForLoan);
            return conditions;
        }
    }
}
