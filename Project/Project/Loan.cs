using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class Loan
    {
        #region Fields
        protected static LoanName _name;
        protected static string _purpose;
        protected static double _interestRate;
        protected static int _maxTermForLoan;
        protected DateTime _issueTime;
        protected DateTime _experianTime;
        protected double _creditAmount;
        protected double _paymontPerMonth;
        protected double _currentBalance;
        protected static List<LoanName> _collectionOfLoansThatCanBeProvided = new List<LoanName> { LoanName.car, LoanName.consumer, LoanName.estate, LoanName.overdraft };
        #endregion

        public static List<LoanName> CollectionOfLoansThatCanBeProvided { get { return _collectionOfLoansThatCanBeProvided; } }
    }
}
