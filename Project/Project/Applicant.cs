using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project
{
    [AdultAge]
    sealed class Applicant
    {
        #region PrivateFields
        private readonly string _applicantSurname;
        private readonly string _applicantName;
        private readonly DateTime _applicantDateOfBirth;
        private readonly int _age;
        private readonly int _applicantInternalID;
        private static int indexForApplicantCounting = Constants.startNumberDefenition;
        private Passport _applicantPassport;
        private string _phoneNumber;
        #endregion

        #region Constructor
        public Applicant(string surname, string name, DateTime dateOfBirth)
        {
            _applicantSurname = surname;
            _applicantName = name;
            _applicantDateOfBirth = dateOfBirth;
            _age = DateTime.Now.Year - _applicantDateOfBirth.Year;
            _applicantInternalID = indexForApplicantCounting + 1;
            indexForApplicantCounting += 1;
        }
        #endregion

        #region Properties
        public string ApplicantSurname {get { return _applicantSurname; } }
        public string ApplicantName { get { return _applicantName; } }
        public DateTime ApplicantDateOfBirth { get { return _applicantDateOfBirth; } }
        public int Age { get { return _age; } }
        public int ApplicantInternalID { get { return _applicantInternalID; } }
        public string PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public Passport ApplicantPassport { get { return _applicantPassport; } set { _applicantPassport = value; } }
        #endregion

        #region Help Methods
        private int DoesThisLoanNumExist(int choise)
        {
            while (choise != (int)LoanName.car && choise != (int)LoanName.consumer && choise != (int)LoanName.estate && choise != (int)LoanName.overdraft)
            {
                Console.WriteLine(Bot.SorryMessage);
                Console.WriteLine(Bot.AskToChooseCredit);
                Console.WriteLine($"{(int)LoanName.car} - {LoanName.car}, \n{(int)LoanName.consumer} - {LoanName.consumer}, \n{(int)LoanName.estate} - {LoanName.estate}, \n{(int)LoanName.overdraft} - {LoanName.overdraft} ");
                string inputAgain = Console.ReadLine();
                inputAgain = Bot.TryParseToInt(inputAgain);
                choise = Int32.Parse(inputAgain);
            }
            return choise;
        }
        private T SearchInProfile<T>(ArrayList profile, T info)
        {
            int index = profile.IndexOf(info);
            T searched = (T)profile[index];
            return searched;
        }
        #endregion

        #region Main Methods
        internal DateTime WhenApplicantGotAdult()
        {
            return ApplicantDateOfBirth.AddYears(Constants.adultYears);
        }
        public int ChooseCreditType()
        {
            string choise = Console.ReadLine();
            choise = Bot.TryParseToInt(choise);
            int choiseChecked = Int32.Parse(choise);
            choiseChecked = DoesThisLoanNumExist(choiseChecked);
            return choiseChecked;
        }
        public ArrayList FillTheProfile(ArrayList applicantProfile)
        {
            string sex = Bot.AskSex();
            int child = Bot.AskAboutChild();
            Bot.InsertIntoProfile(applicantProfile, sex, child);
            return applicantProfile;
        }
        public Passport GivePassport(Applicant applicant)
        {
            string passportID = Bot.AskPassportID();
            DateTime issue = Bot.AskPassportIssueDate();
            Passport passport = new Passport(applicant.ApplicantName, applicant.ApplicantSurname, applicant.ApplicantDateOfBirth, passportID, issue);
            applicant.ApplicantPassport = passport;
            return passport;
        }
        #endregion
    }
}
