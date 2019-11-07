using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            #region Bot greets user and asks wheather user wants to get a loan
            bot.Greet();
            int userInterested = bot.GetUserSimpleAnswer();
            #endregion
            #region Bot gets user information to create new Applicant
            bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = bot.GetApplicantFullName();
            DateTime applicantBirthday = bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item1, applicantFullName.Item2, applicantBirthday);
            
            #endregion
            #region Bot makes a loan proposal for the applicant

            #endregion
        }
    }
}