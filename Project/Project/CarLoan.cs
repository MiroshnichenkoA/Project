using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class CarLoan : Loan
    {
        #region Constructs
        public CarLoan()
        {
        _name = LoanName.auto;
       _purpose = Constants.PurposeCar;
       _interestRate = Constants.InterestRateCar;
        _maxTermForLoan = (int)MaxTermForLoan.car;
        _minSum = Constants.MinCreditSumCar;
        _maxSum = Constants.MaxCreditSumCar;
    }

        public CarLoan(double creditAmount)
        {
            _name = LoanName.auto;
            _purpose = Constants.PurposeCar;
            _interestRate = Constants.InterestRateCar;
            _maxTermForLoan = (int)MaxTermForLoan.car;
            _minSum = Constants.MinCreditSumCar;
            _maxSum = Constants.MaxCreditSumCar;
            _creditAmount = creditAmount;
            _issueTime = DateTime.Now;
            _experianTime = _issueTime.AddYears(_maxTermForLoan);
            _paymontPerMonth = (_creditAmount / _maxTermForLoan) + ((_creditAmount * _interestRate)/ (Constants.MonthInYear * Constants.ToPer));
            _currentBalance = _creditAmount;
        }
        #endregion

        public static (LoanName, int, double, string, double, double) Conditions()
        {
            return (_name, _maxTermForLoan / Constants.MonthInYear, _interestRate * Constants.ToPer, _purpose, _minSum, _maxSum);
        }
    }
}
