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
            #region Bot greets user and asks wheather user wants to get a product
            Bot.Greet();
            int userInterested = Bot.GetUserSimpleAnswer();
            #endregion

            #region Bot gets user information to create new Applicant
            Bot.AskForIntroducing(userInterested);
            (string, string) applicantFullName = Bot.GetApplicantFullName();
            DateTime applicantBirthday = Bot.GetApplicantDateOfBirth(applicantFullName);
            Applicant applicant = new Applicant(applicantFullName.Item2, applicantFullName.Item1, applicantBirthday);
            Bot.CheckIfApplicantIsAdult(applicant);
            ArrayList applicantProfile = new ArrayList();
            Bot.InsertIntoProfile(applicantProfile, applicant);
            #endregion

            #region Bot makes a product proposal for the applicant
            Bot.ShowTheListOfLoans(applicant);
            Bot.AskToChooseCreditType();
            int choosedLoan = applicant.ChooseCreditType();
            dynamic loan = Bot.CreateALoanType(choosedLoan);
            Bot.InsertIntoProfile(applicantProfile, loan);
            applicant.Income = Bot.GetApplicantIncome(applicant);
            Bot.UpdateProfile(applicantProfile, applicant);
            double estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
            int applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            while (applicantWantsAnotherLoan != (int)SimpleAnswers.AGREE)
            {
                Bot.ShowTheListOfLoans(applicant);
                Bot.AskToChooseCreditType();
                choosedLoan = applicant.ChooseCreditType();
                loan = Bot.CreateALoanType(choosedLoan);
                Bot.UpdateProfile(applicantProfile, loan);
                estimateSum = Bot.EstimateCreditSum(applicant.Income, loan);
                applicantWantsAnotherLoan = Bot.AskIfApplicantWantOtherLoan(applicant, estimateSum);
            }
            Bot.IfToContinue();
            #endregion

            #region Bot fills in full applicant's profile
            applicant.Passport = applicant.GivePassport(applicant);
            Bot.UpdateProfile(applicantProfile, applicant);
            applicant.FillTheProfile();
            Bot.UpdateProfile(applicantProfile, applicant);
            Console.WriteLine(Bot.ProfileIsFilled, applicant.Name);
            if (applicant.PhoneNumber is null) applicant.PhoneNumber = Bot.AskPhoneNumber();
            Bot.UpdateProfile(applicantProfile, applicant);
            //TODO Event sms
            // TODO logg
            #endregion

            #region Underwraiter Calculate Credit Sum For Applicant and Do All Needed Checks
            double credit = Underwriter.Underwriter.FinalSum(applicantProfile);
            Console.WriteLine($"YOU CAN TAKe - {credit} BYN");
            #endregion

            #region
            #endregion
        }
    }
}