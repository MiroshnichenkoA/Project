using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project
{
    [AdultAge]
    sealed class Applicant : IProfileManager
    {
        public delegate void SMSHandler(string message, string howToNotify);
        public event SMSHandler Notify;

        #region PrivateFields
        private readonly string _surname;
        private readonly string _name;
        private readonly DateTime _bitrhday;
        private readonly int _age;
        private Passport _passport;
        private string _phoneNumber;
        private string _sex;
        private int _numOfChild;
        private double _income;
        #endregion

        #region Constructor
        public Applicant(string surname, string name, DateTime birthday)
        {
            _surname = surname;
            _name = name;
            _bitrhday = birthday;
            _age = DateTime.Now.Year - _bitrhday.Year;
        }
        #endregion

        #region Properties
        public string Surname {get { return _surname; } }
        public string Name { get { return _name; } }
        public DateTime Birthday { get { return _bitrhday; } }
        public int? Age { get { return _age; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public Passport Passport { get { return _passport; } set { _passport = value; } }
        public string Sex { get { return _sex; } set { _sex = value; } }
        public int NumOfChild { get { return _numOfChild; } set { _numOfChild = value; } }
        public double Income { get { return _income; }set { _income = value; } }
        #endregion

        #region Help Methods
        private int DoesThisLoanNumExist(int choise)
        {
            while (choise != (int)LoanName.auto && choise != (int)LoanName.consumer && choise != (int)LoanName.mortgage && choise != (int)LoanName.overdraft)
            {
                Console.WriteLine(Bot.SorryMessage, Bot.InsertOnlyNumber);
                Console.WriteLine(Bot.AskToChooseCredit);
                Console.WriteLine($"{(int)LoanName.auto} - {LoanName.auto}, \n{(int)LoanName.consumer} - {LoanName.consumer}, \n{(int)LoanName.mortgage} - {LoanName.mortgage}, \n{(int)LoanName.overdraft} - {LoanName.overdraft} ");
                string inputAgain = Console.ReadLine();
                inputAgain = Bot.TryParseToInt(inputAgain);
                choise = Int32.Parse(inputAgain);
            }
            return choise;
        }
        #endregion

        #region Main Methods
        internal DateTime WhenApplicantGotAdult()
        {
            return Birthday.AddYears(Constants.AdultYears);
        }
        public void GetResponseAboutLoanIssue(double? creditSum)
        {
            if (creditSum == null)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Notify?.Invoke("Dear customer, we are forced to refuse to provide you a loan.", PhoneNumber);
                Console.ResetColor();
                Bot.Goodbye();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Notify?.Invoke($"Dear customer, you have been approved a loan for {(int)creditSum} BYN. Waiting for you to sign a loan agreement!", PhoneNumber);
                Console.ResetColor();
            }
        }
        public int ChooseCreditType(Applicant applicant)
        {
            string choise = Console.ReadLine();
            choise = Bot.TryParseToInt(choise);
            int choiseChecked = Int32.Parse(choise);
            choiseChecked = applicant.DoesThisLoanNumExist(choiseChecked);
            return choiseChecked;
        }
        #endregion
        public object GetInfo(int index)
        {
            switch (index)
            {
                case (int)Field.Surname:
                    return _surname;
                case (int)Field.Name:
                    return _name;
                case (int)Field.Bitrhday:
                    return _bitrhday;
                case (int)Field.Age:
                    return _age;
                case (int)Field.Passport:
                    return _passport;
                case (int)Field.PhoneNumber:
                    return _phoneNumber;
                case (int)Field.Sex:
                    return _sex;
                case (int)Field.NumOfChild:
                    return _numOfChild;
                case (int)Field.Income:
                    return _income;
            }
            return null;
        }
    }
}
