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

        #region Properties
        public static int Term { get { return _maxTermForLoan; } }
        public static double InterestRate { get { return _interestRate; } }
        public static LoanName Name { get { return _name; } }
        public double Paymont { get { return _paymontPerMonth; } set { _paymontPerMonth = value; } } 
        #endregion

        protected (LoanName, double) ThisConditions()
        {
            (LoanName, double) conditions = (_name, _interestRate);
            return conditions;
        }

        protected (double, int) ThisConditionsForUnderwriter()
        {
            (double, int) conditions = (_interestRate, _maxTermForLoan);
            return conditions;
        }

        public object GetInfo(int index)
        {
            switch (index)
            {
                case (int)Field.Loan: return _name;
                case (int)Field.Purpose: return _purpose;
                case (int)Field.Rate: return _interestRate;
                case (int)Field.Term: return _maxTermForLoan;
                case (int)Field.MinSum: return _minSum;
                case (int)Field.MaxSum: return _maxSum;
                case (int)Field.Issue: return _issueTime;
                case (int)Field.Expiry: return _experianTime;
                case (int)Field.Amount: return _creditAmount;
                case (int)Field.Paymont: return _paymontPerMonth;
                case (int)Field.Balance: return _currentBalance;
            }
            return null;
        }
    }
}
