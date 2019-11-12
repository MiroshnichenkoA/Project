using System;
using System.Collections;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Bot greets user and asks wheather user wants to get a product
            Bot.Greet();
            int userInterested = Bot.GetUserSimpleAnswer();
            #endregion

            #region Bot gets user information to create new Applicant
            Bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = Bot.GetApplicantFullName();
            DateTime applicantBirthday = Bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            applicant.Notify += SendSMS;
            Bot.CheckIfApplicantIsAdult(applicant);
            ArrayList applicantProfile = new ArrayList();
            Bot.InsertIntoProfile(applicantProfile, applicant);
            #endregion

            #region Bot makes a product proposal for the applicant
            Bot.ShowTheListOfLoans(applicant);
            Bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType(applicant);
            dynamic loan = Bot.CreateALoanType(choosedLoan);
            applicant.Income = Bot.GetApplicantIncome(applicant);
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            double estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
            int applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            while (applicantWantsAnotherLoan != (int)SimpleAnswers.AGREE)
            {
                Bot.ShowTheListOfLoans(applicant);
                Bot.AskToChooseCreditType();
                choosedLoan = applicant.ChooseCreditType(applicant);
                loan = Bot.CreateALoanType(choosedLoan);
                estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
                applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            }
            Bot.InsertIntoProfile(applicantProfile, loan);
            Bot.IfToContinue();
            #endregion

            #region Bot fills in full applicant's profile
            applicant.Passport = applicant.GivePassport();
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            applicant.FillTheProfile();
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            Console.WriteLine(Bot.ProfileIsFilled, applicant.Name);
            if (applicant.PhoneNumber is null) applicant.PhoneNumber = Bot.AskPhoneNumber();
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            #endregion

            #region Underwraiter Calculate Credit Sum For Applicant and Do All Needed Checks
            Thread.Sleep(Constants.TimeForTakingDissicion);
            loan.CreditAmount = Underwriter.Underwriter.FinalSum(applicantProfile);
            applicant.GetResponseAboutLoanIssue(loan.CreditAmount);
            Console.WriteLine(Bot.Acepted, applicant.Name, applicant.Surname, (int)loan.CreditAmount, loan.Name, loan.Term / Constants.MonthInYear, loan.InterestRate);
            int aceptFromApplicant = Bot.GetUserSimpleAnswer();
            if (aceptFromApplicant == (int)SimpleAnswers.NO) Bot.Goodbye();
            #endregion

            #region Create Loan Agreement
            loan = Bot.CreateALoanType(choosedLoan, loan.CreditAmount);
            Bot.UpdateProfileLoan(applicantProfile, loan);
            Bot.CreateLoanAgreement(applicantProfile);
            Console.WriteLine(Bot.AgreementIsSavedInJSON);
            Bot.Goodbye();
            #endregion
        }

        #region Method For Event
        private static void SendSMS(string message, string howToNotify)
        {
            // Imitate sending sms on PhoneNumber
            Console.WriteLine(message);
        }
        #endregion
    }
}