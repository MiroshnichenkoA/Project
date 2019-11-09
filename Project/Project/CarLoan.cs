using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class CarLoan : Loan
    {
        private static readonly LoanName _name = LoanName.auto;
        private static readonly string _purpose = Constants.purposeCar;
        private static readonly double _interestRate = Constants.interestRateCar;
        private static readonly int _maxTermForLoan = (int) MaxTermForLoan.car;
        private static readonly double _minSum = Constants.minCreditSumCar;
        private static readonly double _maxSum = Constants.maxCreditSumCar;

        #region Properties
        public DateTime IssueTime { get { return _issueTime; } }
        public DateTime ExperianTime { get { return _experianTime; } }
        protected double CreditAmount { get { return _creditAmount; } set { _creditAmount = value; } }
        public double PaymontPerMonth { get { return _paymontPerMonth; } }
        public double CurrentBalance { get { return _currentBalance; } }
        #endregion

        #region Constructs
        public CarLoan()
        {
        }

        public CarLoan(double creditAmount)
        {
            _creditAmount = creditAmount;
        }
        #endregion
        public double InterestRate { get { return _interestRate; } }
        public LoanName Name { get { return _name; } }
        public double MinSum { get { return _minSum; } }
        public double MaxSum { get { return _maxSum; } }

        public static (LoanName, int, double, string, double, double) Conditions()
        {
            return (_name, _maxTermForLoan / Constants.monthInYear, _interestRate * Constants.toPer, _purpose, _minSum, _maxSum);
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
