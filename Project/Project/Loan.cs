using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public abstract class Loan
    {
        #region Fields
        protected DateTime _issueTime;
        protected DateTime _experianTime;
        protected double _creditAmount;
        protected double _paymontPerMonth;
        protected double _currentBalance;
        #endregion

        #region Properties
        public double Paymont { get { return _paymontPerMonth; } set { _paymontPerMonth = value; } }
        public DateTime IssueTime { get => _issueTime;}
        public DateTime ExpirianTime { get => _experianTime;}
        public double CreditAmount { get { return _creditAmount; } set { _creditAmount = value; } }
        public double CurrentBalance { get { return _currentBalance; }}
        #endregion
    }
}
