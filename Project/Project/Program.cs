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
            #region Bot greets user and asks wheather user wants to get a loan
            Bot.Greet();
            int userInterested = Bot.GetUserSimpleAnswer();
            //TODO: logg
            #endregion

            #region Bot gets user information to create new Applicant
            Bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = Bot.GetApplicantFullName();
            DateTime applicantBirthday = Bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            Bot.CheckIfApplicantIsAdult(applicant);
            ArrayList applicantProfile = new ArrayList();
            Bot.InsertIntoProfile(applicantProfile, applicant.ApplicantName, applicant.ApplicantSurname, applicant.ApplicantDateOfBirth);
            //TODO: logg
            #endregion

            #region Bot makes a loan proposal for the applicant
            Bot.ShowTheListOfLoans(applicant);
            Bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType();
            dynamic loan = Bot.CreateALoanType(choosedLoan);
            Bot.InsertIntoProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
            double applicantIncome = Bot.GetApplicantIncome(applicant);
            Bot.InsertIntoProfile(applicantProfile, applicantIncome);
            double estimateSum = Bot.EstimateCreditSum(applicantIncome, loan);
            int applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            while (applicantWantsAnotherLoan != (int)SimpleAnswers.AGREE)
            {
                Bot.DeleteFromProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
                Bot.ShowTheListOfLoans(applicant);
                Bot.AskToChooseCreditType();
                choosedLoan = applicant.ChooseCreditType();
                loan = Bot.CreateALoanType(choosedLoan);
                Bot.InsertIntoProfile(applicantProfile, loan.GetType(), loan.ThisConditions().Item1, loan.ThisConditions().Item2);
                estimateSum = Bot.EstimateCreditSum(applicantIncome, loan);
                applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            }
            Bot.IfToContinue();
            //TO DO: how much does applicant actually wants?
            // TODO: logg
            #endregion

            #region Bot fills in full applicant's profile
            Passport passport = applicant.GivePassport(applicant);
            Bot.InsertIntoProfile(applicantProfile, passport.ID, passport.DateOfIssue, passport.DateOfExpiry);
            applicantProfile = applicant.FillTheProfile(applicantProfile);
            Console.WriteLine(Bot.ProfileIsFilled, applicant.ApplicantName);
            if (applicant.PhoneNumber is null) applicant.PhoneNumber = Bot.AskPhoneNumber();
            Bot.InsertIntoProfile(applicantProfile, applicant.PhoneNumber);
            //TODO Event sms
            // TODO logg
            #endregion

            #region Underwraiter Calculate Credit Sum For Applicant
           // double credit = Underwriter.Underwriter.FinalSum(applicantProfile);
            #endregion

            #region
            #endregion
        }
    }
}