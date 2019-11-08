using System;
using System.Collections;
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
        private readonly char _sex;
        private readonly string _passportID;
        private readonly DateTime _dateOfIssue;
        private readonly DateTime _dateOfExpiry;
        #endregion

        #region Constructor
        public Passport(string name, string surname, DateTime dateOfBirth, string passportID, DateTime dateOfIssue)
        {
            _surname = name;
            _name = surname;
            _dateOfBirth = dateOfBirth;
            _passportID = passportID;
            _dateOfIssue = dateOfIssue;
            _dateOfExpiry = _dateOfIssue.AddYears(Constants.passportExpirianTerm);
        }
        #endregion

        #region Properties
        public string Surname { get { return _surname; } }
        public string Name { get { return _name; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } }
        public char Sex { get { return _sex; } }
        public string ID { get { return _passportID; } }
        public DateTime DateOfIssue { get { return _dateOfIssue; } }
        public DateTime DateOfExpiry { get { return _dateOfExpiry; } }
        #endregion
    }
}
