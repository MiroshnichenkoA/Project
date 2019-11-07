﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Overdraft : Loan, ICard
    {
        private static readonly LoanName _name = LoanName.overdraft;
        private static readonly string _purpose = Constants.purposeOverdraft;
        private static readonly double _interestRate = Constants.interestRateOverdraft;
        private static readonly int _maxTermForLoan = (int)MaxTermForLoan.overdraft;

        #region Properties
        public DateTime IssueTime { get { return _issueTime; } }
        public DateTime ExperianTime { get { return _experianTime; } }
        protected double CreditAmount { get { return _creditAmount; } set { _creditAmount = value; } }
        public double PaymontPerMonth { get { return _paymontPerMonth; } }
        public double CurrentBalance { get { return _currentBalance; } }
        #endregion

        #region Constructs
        public Overdraft()
        {
        }

        public Overdraft(double creditAmount)
        {
            _creditAmount = creditAmount;
        }
        #endregion
        public static (LoanName, int, double, string) Conditions()
        {
            (LoanName, int, double, string) conditions;
            return conditions = (_name, _maxTermForLoan / Constants.monthInYear, _interestRate * Constants.toPer, _purpose);
        }
    }
}