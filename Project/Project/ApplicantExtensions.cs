using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    static class ApplicantExtensions
    {
        public static void FillTheProfile(this Applicant applicant)
        {
            applicant.Sex = Bot.AskSex();
            applicant.Passport.Sex = applicant.Sex;
            applicant.NumOfChild = Bot.AskAboutChild();
        }

        public static Passport GivePassport(this Applicant applicant)
        {
            string passportID = Bot.AskPassportID();
            DateTime issue = Bot.AskPassportIssueDate();
            applicant.Passport = new Passport(applicant.Name, applicant.Surname, applicant.Birthday, passportID, issue);
            return applicant.Passport;
        }
    }
}
