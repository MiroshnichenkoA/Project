using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Overdraft : Loan, ICard
    {
        #region Fields
        private static readonly LoanName _name = LoanName.overdraft;
        private static readonly string _purpose = Constants.PurposeOverdraft;
        private static readonly double _interestRate = Constants.interestRateOverdraft;
        private static readonly int _maxTermForLoan = (int) MaxTermForLoan.overdraft;
        private static readonly double _minSum = Constants.MinCreditSumOverdraft;
        private static readonly double _maxSum = Constants.MaxCreditSumOverdraft;
        #endregion

        #region Properties
        public LoanName Name { get => _name; }
        public string Purpose { get => _purpose; }
        public int Term { get => _maxTermForLoan; }
        public double InterestRate { get => _interestRate; }
        public double MaxSum { get => _maxSum; }
        public double MinSum { get => _minSum; }
        public CreditCard CreditCard { get; }
        #endregion

        #region Constructs
        public Overdraft()
        { 
        }

        public Overdraft(double creditAmount)
        {
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

        private CreditCard RealeseCard()
        {
            CreditCard creditCard = new CreditCard();
            Logger.Logger.Loging($"Credit card nubber {creditCard.Number} realesed.");
            return creditCard;
        }
    }
}
