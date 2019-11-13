using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Overdraft : Loan, ICard
    {
        public CreditCard CreditCard { get; }

        #region Constructs
        public Overdraft()
        {
            _name = LoanName.overdraft;
        _purpose = Constants.PurposeOverdraft;
       _interestRate = Constants.interestRateOverdraft;
       _maxTermForLoan = (int)MaxTermForLoan.overdraft;
        _minSum = Constants.MinCreditSumOverdraft;
        _maxSum = Constants.MaxCreditSumOverdraft;
    }

        public Overdraft(double creditAmount)
        {
            _name = LoanName.overdraft;
            _purpose = Constants.PurposeOverdraft;
            _interestRate = Constants.interestRateOverdraft;
            _maxTermForLoan = (int)MaxTermForLoan.overdraft;
            _minSum = Constants.MinCreditSumOverdraft;
            _maxSum = Constants.MaxCreditSumOverdraft;
            _creditAmount = creditAmount;
            _issueTime = DateTime.Now;
            _experianTime = _issueTime.AddYears(_maxTermForLoan);
            _paymontPerMonth = (_creditAmount / _maxTermForLoan) + ((_creditAmount * _interestRate) / (Constants.MonthInYear * Constants.ToPer));
            _currentBalance = _creditAmount;
            CreditCard = RealeseCard();
        }
        #endregion
        public static (LoanName, int, double, string, double, double) Conditions()
        {
            return (_name, _maxTermForLoan / Constants.MonthInYear, _interestRate * Constants.ToPer, _purpose, _minSum, _maxSum);
        }
        private CreditCard RealeseCard()
        {
            CreditCard creditCard = new CreditCard();
            Logger.Logger.Loging($"Credit card nubber {creditCard.Number} realesed.");
            return creditCard;
        }
    }
}
