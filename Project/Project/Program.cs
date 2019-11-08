using System;
using System.Collections;
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
            ArrayList applicantProfile = new ArrayList();
            bot.InsertIntoProfile(applicantProfile, applicantFullName);
            DateTime applicantBirthday = bot.GetApplicantDateOfBirth(applicantFullName);
            bot.InsertIntoProfile(applicantProfile, applicantBirthday);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            bot.CheckIfApplicantIsAdult(applicant);
            //TODO: logg
            #endregion

            #region Bot makes a loan proposal for the applicant
            bot.ShowTheListOfLoans(applicant);
            bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType();
            dynamic loan = bot.CreateALoanType(choosedLoan);
            bot.InsertIntoProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
            double applicantIncome = bot.GetApplicantIncome(applicant);
            bot.InsertIntoProfile(applicantProfile, applicantIncome);
            double estimateSum = bot.EstimateCreditSum(applicantIncome, loan);
            int applicantWantsAnotherLoan = bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            while (applicantWantsAnotherLoan != (int)SimpleAnswers.AGREE)
            {
                bot.DeleteFromProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
                bot.ShowTheListOfLoans(applicant);
                bot.AskToChooseCreditType();
                choosedLoan = applicant.ChooseCreditType();
                loan = bot.CreateALoanType(choosedLoan);
                bot.InsertIntoProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
                estimateSum = bot.EstimateCreditSum(applicantIncome, loan);
                applicantWantsAnotherLoan = bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            }
            bot.IfToContinue();
            // TODO: logg
            #endregion

            #region Bot fills in full applicant's profile
            applicantProfile = applicant.FillTheProfile(applicantProfile);
            #endregion
        }
    }
}