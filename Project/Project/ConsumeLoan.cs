using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class ConsumeLoan : Loan
    {
        #region Fields
        private static readonly LoanName _name = LoanName.consumer;
        private static readonly string _purpose = Constants.PurposeConsume;
        private static readonly double _interestRate = Constants.interestRateConsume;
        private static readonly int _maxTermForLoan = (int) MaxTermForLoan.consumer;
        private static readonly double _minSum = Constants.MinCreditSumConsume;
        private static readonly double _maxSum = Constants.MaxCreditSumConsume;
        #endregion

        #region Properties
        public LoanName Name { get => _name; }
        public string Purpose { get => _purpose; }
        public int Term { get => _maxTermForLoan; }
        public double InterestRate { get => _interestRate; }
        public double MaxSum { get => _maxSum; }
        public double MinSum { get => _minSum; }
        #endregion

        #region Constructs
        public ConsumeLoan()
        {
        }

        public ConsumeLoan(double creditAmount)
        {
            _creditAmount = creditAmount;
            _issueTime = DateTime.Now;
            _experianTime = _issueTime.AddYears(_maxTermForLoan);
            _paymontPerMonth = (_creditAmount / _maxTermForLoan) + ((_creditAmount * _interestRate) / (Constants.MonthInYear * Constants.ToPer));
            _currentBalance = _creditAmount;
        }
        #endregion

        public static (LoanName, int, double, string, double, double) Conditions()
        {
            return (_name, _maxTermForLoan / Constants.MonthInYear, _interestRate * Constants.ToPer, _purpose, _minSum, _maxSum);
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
