using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Project
{
    class Applicant
    {
        #region PrivateFields
        private readonly string _applicantSurname;
        private readonly string _applicantName;
        private readonly DateTime _applicantDateOfBirth;
        private readonly int _applicantInternalID;
        private static int indexForApplicantCounting = Constants.startNumberDefenition;
        private Passport _applicantPassport;
        #endregion

        #region Constructor
        public Applicant(string surname, string name, DateTime dateOfBirth)
        {
            _applicantSurname = surname;
            _applicantName = name;
            _applicantDateOfBirth = dateOfBirth;
            _applicantInternalID = indexForApplicantCounting + 1;
            indexForApplicantCounting += 1;
        }
        #endregion

        #region Properties
        public string ApplicantSurname {get { return _applicantSurname; } }
        public string ApplicantName { get { return _applicantName; } }
        public DateTime ApplicantDateOfBirth { get { return _applicantDateOfBirth; } }
        public int ApplicantInternalID { get { return _applicantInternalID; } }
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
        private object SearchInProfile(ArrayList profile, object info)
        {
            int index = profile.IndexOf(info);
            object searched = profile[index];
            return searched;
        }
        #endregion

        #region Main Methods
        internal DateTime WhenApplicantGotAdult()
        {
            DateTime dateApplicantGetAdult = ApplicantDateOfBirth.AddYears(Constants.adultYears);
            return dateApplicantGetAdult;
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
            string name = (string)SearchInProfile(applicantProfile, ApplicantName);
            string surname = (string)SearchInProfile(applicantProfile, ApplicantSurname);
            DateTime birthday = (DateTime)SearchInProfile(applicantProfile, ApplicantDateOfBirth);
            string passportID = Bot.AskPassportID();
            DateTime issue = Bot.AskPassportIssueDate();
            return applicantProfile;
        }
        #endregion
    }
}
