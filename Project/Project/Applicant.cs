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

        #region Methods
        #endregion
    }
}
