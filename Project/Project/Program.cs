using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

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
            //TODO: logg
            #endregion

            #region Bot gets user information to create new Applicant
            bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = bot.GetApplicantFullName();
            DateTime applicantBirthday = bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            bot.CheckIfApplicantIsAdult(applicant);
            //TODO: logg
            #endregion

            #region Bot makes a loan proposal for the applicant
            bot.ShowTheListOfLoans(applicant);
            bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType();
            dynamic loan = bot.CreateALoanType(choosedLoan);
            #endregion
        }
    }
}