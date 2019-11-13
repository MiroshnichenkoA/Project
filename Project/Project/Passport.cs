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
        private string _sex;
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
            _sex = null;
            _passportID = passportID;
            _dateOfIssue = dateOfIssue;
            _dateOfExpiry = _dateOfIssue.AddYears(Constants.PassportExpirianTerm);
            Logger.Logger.Loging($"Passport created.");
        }
        #endregion

        #region Properties
        public string Surname { get { return _surname; } }
        public string Name { get { return _name; } }
        public DateTime DateOfBirth { get { return _dateOfBirth; } }
        public string Sex { get { return _sex; } set { _sex = value; } }
        public string ID { get { return _passportID; } }
        public DateTime DateOfIssue { get { return _dateOfIssue; } }
        public DateTime DateOfExpiry { get { return _dateOfExpiry; } }
        #endregion

        public object GetInfo(int index)
        {
            switch (index)
            {
                case (int)Field.Surname: return _surname;
                case (int)Field.Name: return _name;
                case (int)Field.Bitrhday: return _dateOfBirth;
                case (int)Field.Sex: return _sex;
                case (int)Field.ID: return _passportID;
                case (int)Field.Issue: return _dateOfIssue;
                case (int)Field.Expiry: return _dateOfExpiry;
            }
            return null;
        }
    }
}
