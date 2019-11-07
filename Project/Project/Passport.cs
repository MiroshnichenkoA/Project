using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class Passport
    {
        #region PrivateFields
        private readonly string _surname;
        private readonly string _name;
        private readonly DateTime _dateOfBirth;
        private readonly string _sex;
        private readonly string _passportID;
        private readonly DateTime _dateOfIssue;
        private readonly DateTime _dateOfExpiry;
        private readonly string _placeOfBirth;
        #endregion

        #region Constructor
        public Passport(Applicant applicant, string passportID, DateTime dateOfIssue, string placeOfBirth)
        {
            _surname = applicant.ApplicantSurname;
            _name = applicant.ApplicantName;
            _dateOfBirth = applicant.ApplicantDateOfBirth;
            _passportID = passportID;
            _dateOfIssue = dateOfIssue;
            _dateOfExpiry = _dateOfIssue.AddYears(Constants.passportExpirianTerm);
            _placeOfBirth = placeOfBirth;
        }
        #endregion

        #region Properties
        public string Surname { get { return _surname; } }
        public string Name { get { return _name; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } }
        public string Sex { get { return _sex; } }
        public string ID { get { return _passportID; } }
        public DateTime DateOfIssue { get { return _dateOfIssue; } }
        public DateTime DateOfExpiry { get { return _dateOfExpiry; } }
        public string PlaceOfBirth { get { return _placeOfBirth; } }
        #endregion
    }
}
