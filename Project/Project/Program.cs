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
            Logger.Logger.Loging("Start");
            Bot.Greet();
            int userInterested = Bot.GetUserSimpleAnswer();
            #endregion

            #region Bot gets user information to create new Applicant
            Bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = Bot.GetApplicantFullName();
            DateTime applicantBirthday = Bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            Logger.Logger.Loging($"Created Applicant: {applicant}");
            Logger.Logger.Loging("Notify on");
            Bot.CheckIfApplicantIsAdult(applicant);
            Logger.Logger.Loging("Applicant is adult");
            ArrayList applicantProfile = new ArrayList();
            Logger.Logger.Loging($"Created Applicant profile: {applicantProfile}");
            Bot.InsertIntoProfile(applicantProfile, applicant);
            #endregion

            #region Bot makes a product proposal for the applicant
            Bot.ShowTheListOfLoans(applicant);
            Logger.Logger.Loging("List of loans showed");
            Bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType(applicant);
            Logger.Logger.Loging($"Applicant chosed loan number: {choosedLoan}");
            dynamic loan = Bot.CreateALoanType(choosedLoan);
            Logger.Logger.Loging($"Created Loan: {loan}");
            applicant.Income = Bot.GetApplicantIncome(applicant);
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            double estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
            Logger.Logger.Loging($"Estimated sum: {estimateSum}");
            int applicantWantsAnotherLoan = Bot.AskIfApplicantWantsOtherLoan(applicant, estimateSum);
            while (applicantWantsAnotherLoan != (int)SimpleAnswers.AGREE)
            {
                Bot.ShowTheListOfLoans(applicant);
                Bot.AskToChooseCreditType();
                choosedLoan = applicant.ChooseCreditType(applicant);
                loan = Bot.CreateALoanType(choosedLoan);
                estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
                Logger.Logger.Loging($"Estimated sum: {estimateSum}");
                applicantWantsAnotherLoan = Bot.AskIfApplicantWantsOtherLoan(applicant, estimateSum);
            }
            Bot.InsertIntoProfile(applicantProfile, loan);
            Bot.IfToContinue();
            #endregion

            #region Bot fills in full applicant's profile
            applicant.Passport = applicant.GivePassport();
            Logger.Logger.Loging($"Created passport: {applicant.Passport}");
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            applicant.FillTheProfile();
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            Console.WriteLine(Bot.ProfileIsFilled, applicant.Name);
            if (applicant.PhoneNumber is null) applicant.PhoneNumber = Bot.AskPhoneNumber();
            Bot.UpdateProfileApplicant(applicantProfile, applicant);
            applicant.Notify += SendSMS;
            #endregion

            #region Underwraiter Calculate Credit Sum For Applicant and Do All Needed Checks
            Logger.Logger.Loging("Sleeping starts");
            Thread.Sleep(Constants.TimeForTakingDissicion);
            Logger.Logger.Loging("Sleeping ends");
            loan.CreditAmount = Underwriter.Underwriter.FinalSum(applicantProfile);
            Logger.Logger.Loging($"Credit granted: {loan.CreditAmount}");
            applicant.GetResponseAboutLoanIssue(loan.CreditAmount);
            applicant.Notify -= SendSMS;
            loan = Bot.CreateALoanType(choosedLoan, loan.CreditAmount);
            Logger.Logger.Loging($"Loan created: {loan}");
            Console.WriteLine(Bot.Acepted, applicant.Name, applicant.Surname, (int)loan.CreditAmount, loan.Name, loan.Term / Constants.MonthInYear, loan.InterestRate, (int)loan.Paymont);
            int aceptFromApplicant = Bot.GetUserSimpleAnswer();
            if (aceptFromApplicant == (int)SimpleAnswers.NO) Bot.Goodbye();
            #endregion

            #region Create Loan Agreement
            Bot.UpdateProfileLoan(applicantProfile, loan);
            Bot.CreateLoanAgreement(applicantProfile);
            Logger.Logger.Loging($"JSON file added");
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