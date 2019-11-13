using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class EstateLoan : Loan
    {
        #region Constructs
        public EstateLoan()
        {
         _name = LoanName.mortgage;
        _purpose = Constants.PurposeEstate;
        _interestRate = Constants.InterestRateEstate;
        _maxTermForLoan = (int)MaxTermForLoan.estate;
        _minSum = Constants.MinCreditSumEstate;
        _maxSum = Constants.MaxCreditSumEstate;
    }

        public EstateLoan(double creditAmount)
        {
            _name = LoanName.mortgage;
            _purpose = Constants.PurposeEstate;
            _interestRate = Constants.InterestRateEstate;
            _maxTermForLoan = (int)MaxTermForLoan.estate;
            _minSum = Constants.MinCreditSumEstate;
            _maxSum = Constants.MaxCreditSumEstate;
            _creditAmount = creditAmount;
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
