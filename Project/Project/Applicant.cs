using System;
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
        private readonly string _applicantSex;
        private readonly int _applicantInternalID;
        public static int indexForApplicantCounting = Constants.startNumberDefenition;
        private Passport _applicantPassport;
        #endregion

        #region Constructor
        public Applicant(string surname, string name, DateTime dateOfBirth, string sex)
        {
            _applicantSurname = surname;
            _applicantName = name;
            _applicantDateOfBirth = dateOfBirth;
            _applicantSex = sex;
            _applicantInternalID = indexForApplicantCounting + 1;
            indexForApplicantCounting += 1;
        }
        #endregion

        #region Properties
        public string ApplicantSurname {get { return _applicantSurname; } }
        public string ApplicantName { get { return _applicantName; } }
        public DateTime ApplicantDateOfBirth { get { return _applicantDateOfBirth; } }
        public string ApplicantSex { get { return _applicantSex; } }
        public int ApplicantInternalID { get { return _applicantInternalID; } }
        #endregion

        #region Methods
        bool PassportIsFake(Applicant applicant)
        {
           // TODO
            bool result = true;
            return result;
        }
        #endregion
    }
}
