using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    class ApplicantProfile
    {
        private string _fullName;
        private int _fullYearsFromBirth;
        private Passport _passport;
        private int _numOfChilds;
        private DateTime _fullMonthFromEmployment;
        private double _income;
        
        public ApplicantProfile(Applicant applicant, double income)
        {
            _fullName = String.Join(" ", applicant.ApplicantName, applicant.ApplicantSurname);
            _fullYearsFromBirth = applicant.ApplicantDateOfBirth.Year;
            _income = income;
        }
    }
}
