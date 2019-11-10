﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class EstateLoan : Loan
    {
        private static readonly LoanName _name = LoanName.mortgage;
        private static readonly string _purpose = Constants.PurposeEstate;
        private static readonly double _interestRate = Constants.InterestRateEstate;
        private static readonly int _maxTermForLoan = (int)MaxTermForLoan.estate;
        private static readonly double _minSum = Constants.MinCreditSumEstate;
        private static readonly double _maxSum = Constants.MaxCreditSumEstate;
        private DateTime _issueTime;
        private DateTime _experianTime;
        private double _creditAmount;
        private double _paymontPerMonth;
        private double _currentBalance;

        #region Properties
        public int Term { get { return _maxTermForLoan; } }
        public double InterestRate { get { return _interestRate; } }
        public LoanName Name { get { return _name; } }
        public double MinSum { get { return _minSum; } }
        public double MaxSum { get { return _maxSum; } }
        public DateTime IssueTime { get { return _issueTime; } }
        public DateTime ExperianTime { get { return _experianTime; } }
        protected double CreditAmount { get { return _creditAmount; } set { _creditAmount = value; } }
        public double PaymontPerMonth { get { return _paymontPerMonth; } }
        public double CurrentBalance { get { return _currentBalance; } }
        #endregion

        #region Constructs
        public EstateLoan()
        {
        }

        public EstateLoan(double creditAmount)
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
