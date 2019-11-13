using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class ConsumeLoan : Loan
    {
        #region Constructs
        public ConsumeLoan()
        {
            _name = LoanName.consumer;
            _purpose = Constants.PurposeConsume;
            _interestRate = Constants.interestRateConsume;
            _maxTermForLoan = (int)MaxTermForLoan.consumer;
            _minSum = Constants.MinCreditSumConsume;
            _maxSum = Constants.MaxCreditSumConsume;
        }

        public ConsumeLoan(double creditAmount)
        {
            _name = LoanName.consumer;
            _purpose = Constants.PurposeConsume;
            _interestRate = Constants.interestRateConsume;
            _maxTermForLoan = (int)MaxTermForLoan.consumer;
            _minSum = Constants.MinCreditSumConsume;
            _maxSum = Constants.MaxCreditSumConsume;
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
    }
}
