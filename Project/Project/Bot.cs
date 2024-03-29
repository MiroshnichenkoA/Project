﻿using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{
    abstract class Bot : IProfileManager
    {
        #region Fields

        #region Sorry Message Format and Cases for Checking User Inputs: Fields
        private static readonly string _sorryMessage = "- Sorry. Can't understand you! {0}";
        private static readonly string _insertOnlyDate = "Insert date";
        private static readonly string _insertOnlyNumber = "Insert number";
        private static readonly string _phoneNumFormat = "You should insert 9 numbers instead of X in format +375-XX-XXX-XX-XX";
        private static readonly string _passportIDFormat = $"You should insert the ID number from your passport. It contains {Constants.PassportIDLength} chars. Be careful";
        private static readonly string _askAgainForInvalidAnswer = "Please, insert {0} {1} only";
        private static readonly string _loansAreOnlyForAdult = "- Sorry, we don't provide loans for minors. You will become an adult in {0} years. \n See you then!";
        private static readonly string _incorrectDate = "The date {0} you provide is more then current date {1}. \n Are you sure it's right date? \n Please, enter the date again:";
        #endregion

        private static readonly string _greetingMessage = "- Good {0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        private static readonly string _goodbye = "- Goodbye! We are hope to see you soon!";
        private static readonly string _glad = "- Glad to hear this!";
        private static readonly string _askForIntroducing = "- Please, introduce yourself. Tell me your name and surname";
        private static readonly string _askWhereIsTheName = "- Is {0} your name?";
        private static readonly string _askToProvideBirthday = "- Dear {0}, to continue I need to ask your date of birth. Please, enter the date:";       
        private static readonly string _introduceLoans = "- {0}, at the current moment we can propose you {1} types of loans. They are:";
        private static readonly string _introduceLoansCondition = $"Shall I show you the {Constants.NameOfproduct} conditions?";
        private static readonly string _loanConditionsReadFormat = "\n Credit name: {0}. \n Granted for a period of not more than {1} years at a rate of {2} percent per annum. You can be provided from {4} BYN to {5} BYN. \n Aim: {3}";
        private static readonly string _askToChooseCredit = $"\n- Choose the type of {Constants.NameOfproduct} you need. Press the number you need.";
        private static readonly string _askAboutIncome = "- {0}, you have to insert your estimated income. \nI'll use this information to calculate the estimate amount of credit we can propose to you. \nAlso I'll put this information in your profile.";
        private static readonly string _chooseAnotherLoan = "- Sorry, you do not have enough income for {0} loan.";
        private static readonly string _estimateLoan = "- We can provide you about {0} BYN if you'll take {1} credit.";
        private static readonly string _askWhetherToChangeLoan = "- {0}, do you want to choose another loan product?";
        private static readonly string _ifToContinue = $"- So do you want to take a {Constants.NameOfproduct}?";
        private static readonly string _continueToInsertProfile = "- Perfect. In that case let's fill out your profile";
        private static readonly string _askToProvidePassportIssue = "- Insert your passport issue date, please.";
        private static readonly string _askToProvidePassportID = "- Insert your passport ID, please.";
        private static readonly string _whatIsYourSex = "- Are you man or woman?";
        private static readonly string _anyChild = $"- Do you have any kids under {Constants.AdultYears} year's old?";
        private static readonly string _howManyChild = "- How many?";
        private static readonly string _profileIsFilled = "- Thank you {0}! I will send your applicant profile to our specialist. \n After specialists consider the oppotunity to give you a loan, You will be notified by SMS.";
        private static readonly string _noPhoneNumber = "- Oh, I guess we don't have your phone number in our system. We acept only Belorussian phone numbers. Please, provide it in the international format \"+375-XX-XXX-XX-XX\"";
        private static readonly string _acepted = "- {0} {1}, you have been acepted {2} BYN {3} credit under the following conditions: \n term - {4} years \n rate - {5} per year \n Monthly paymont is {6} BYN. \n Do you agree?";
        private static readonly string _agreementIsSavedInJSON = "Done! You can find your loan agreement in .json file.";
        #endregion

        #region Properties
        public static string SorryMessage { get { return _sorryMessage; } }
        public static string AskToChooseCredit { get { return _askToChooseCredit; } }
        public static string ProfileIsFilled { get { return _profileIsFilled; } }
        public static string LoanAreOnlyForAdult { get { return _loansAreOnlyForAdult; } }
        public static string InsertOnlyNumber { get { return _insertOnlyNumber; } }
        public static string Acepted { get { return _acepted; } }
        public static string AgreementIsSavedInJSON { get { return _agreementIsSavedInJSON; } }
        #endregion

        #region Helpping Methods
        private static ScriptForTimeOfDay WhatTimeOfDayIsItNow()
        {
            ScriptForTimeOfDay result;
            if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.morning && DateTime.Now.Hour < (int)ScriptForTimeOfDay.day)
            {
                result = ScriptForTimeOfDay.morning;
                Logger.Logger.Loging($"Time of the day: {result.ToString()}");
            }
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.day && DateTime.Now.Hour < (int)ScriptForTimeOfDay.evening)
            {
                result = ScriptForTimeOfDay.day;
                Logger.Logger.Loging($"Time of the day: {result.ToString()}");
            }
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.evening && DateTime.Now.Hour < (int)ScriptForTimeOfDay.night)
            {
                result = ScriptForTimeOfDay.evening;
                Logger.Logger.Loging($"Time of the day: {result.ToString()}");
            }
            else
            {
                result = ScriptForTimeOfDay.night;
                Logger.Logger.Loging($"Time of the day: {result.ToString()}");
            }
                return result;
        }
        private static string ValidUserAnswer(string userAnswer)
        {
            while ((userAnswer != Constants.YesString) && (userAnswer != Constants.NoString) && (userAnswer != Constants.YString) && (userAnswer != Constants.NString))
            {
                Logger.Logger.Loging($"Invalid user answer. User input: {userAnswer}");
                string sorryExplain = String.Format(_askAgainForInvalidAnswer, SimpleAnswers.YES, SimpleAnswers.NO);
                Console.WriteLine(SorryMessage, sorryExplain);
                userAnswer = Console.ReadLine().ToUpper();
                sorryExplain = null;
            }
            return userAnswer;
        }
        private static string[] ValidUserAnswer(string[] userAnswer)
        {
            Logger.Logger.Loging($"Invalid user answer. User input: {userAnswer}");
            string sorryExplain = String.Format(_askAgainForInvalidAnswer, Constants.TellName, Constants.TellSurname);
            Console.WriteLine(SorryMessage, sorryExplain);
            userAnswer = Console.ReadLine().Split(" ");
            while (userAnswer.Length != Constants.NumOfWordsInFullName)
            {
                sorryExplain = String.Format(_askAgainForInvalidAnswer, Constants.TellName, Constants.TellSurname);
                Console.WriteLine(SorryMessage, sorryExplain);
                userAnswer = Console.ReadLine().Split(" ");
            }
            sorryExplain = null;
            return userAnswer;
        }
        private static (T, T) CorrectPositionsOfNameAndSurname<T>((T, T) fullName)
        {
            Console.WriteLine(_askWhereIsTheName, fullName.Item1);
            int? namePosition = GetUserSimpleAnswer();
            (T, T) correctFullName;
            if (namePosition == (int?)SimpleAnswers.YES)
            {
                correctFullName.Item1 = fullName.Item1;
                correctFullName.Item2 = fullName.Item2;
            }
            else
            {
                correctFullName.Item1 = fullName.Item2;
                correctFullName.Item2 = fullName.Item1;
            }
            namePosition = null;
            Logger.Logger.Loging($"Coreection of position of name and surname. Corrected output: {correctFullName}");
            return correctFullName;
        }
        public static string TryParseToDate(string userInput)
        {
            DateTime dateOfBitrh;
            bool check = DateTime.TryParse(userInput, out dateOfBitrh);
            while (check == false)
            {
                Logger.Logger.Loging($"Invalid user answer. User input: {userInput}");
                Console.WriteLine(SorryMessage, _insertOnlyDate);
                userInput = Console.ReadLine();
                check = DateTime.TryParse(userInput, out dateOfBitrh);
            }
            return userInput;
        }
        public static string TryParseToInt(string userInput)
        {
            int num;
            bool check = Int32.TryParse(userInput, out num);
            while (check == false)
            {
                Logger.Logger.Loging($"Invalid user answer. User input: {userInput}");
                Console.WriteLine(SorryMessage, _insertOnlyNumber);
                userInput = Console.ReadLine();
                check = Int32.TryParse(userInput, out num);
            }
            return userInput;
        }
        public static string TryParseToDouble(string userInput)
        {
            double num;
            bool check = Double.TryParse(userInput, out num);
            while (check == false)
            {
                Logger.Logger.Loging($"Invalid user answer. User input: {userInput}");
                Console.WriteLine(SorryMessage, _insertOnlyNumber);
                userInput = Console.ReadLine();
                check = Double.TryParse(userInput, out num);
            }
            return userInput;
        }
        public static void Goodbye()
        {
            Console.WriteLine(_goodbye);
            Logger.Logger.Loging("Program exit");
            Environment.Exit(0);
        }
        private static DateTime CheckedDate(DateTime date)
        {
            while (date > DateTime.Now)
            {
                Logger.Logger.Loging($"Invalid user answer. User input: {date} which is more than current date");
                Console.WriteLine(_incorrectDate, date, DateTime.Now);
                string userInput = Console.ReadLine();
                userInput = TryParseToDate(userInput);
                date = DateTime.Parse(userInput);
                userInput = null;
            }
            return date;
        }
        private static ArrayList CreateListIfLoans()
        {
            ArrayList collectionOfLoans = new ArrayList();
            collectionOfLoans.Add(LoanName.auto);
            collectionOfLoans.Add(LoanName.consumer);
            collectionOfLoans.Add(LoanName.mortgage);
            collectionOfLoans.Add(LoanName.overdraft);
            return collectionOfLoans;
        }
        public static void ShowConditions()
        {
            (LoanName, int, double, string, double, double)[] conditions = { CarLoan.Conditions(), EstateLoan.Conditions(), ConsumeLoan.Conditions(), Overdraft.Conditions() };
            foreach ((LoanName, int, double, string, double, double) i in conditions)
            {
                Console.WriteLine(_loanConditionsReadFormat, i.Item1, i.Item2, i.Item3, i.Item4, i.Item5, i.Item6);
            }
            Logger.Logger.Loging("Conditions showed");
            conditions = null;
        }
        private static double AskUnderwritter(double income, dynamic loan)
        {
            if (Underwriter.Underwriter.EstimateSum(income, loan.ThisConditionsForUnderwriter()) >= loan.MaxSum) return loan.MaxSum;
            else if (Underwriter.Underwriter.EstimateSum(income, loan.ThisConditionsForUnderwriter()) < loan.MinSum) return 0;
            else if (Underwriter.Underwriter.EstimateSum(income, loan.ThisConditionsForUnderwriter()) >= loan.MinSum && Underwriter.Underwriter.EstimateSum(income, loan.ThisConditionsForUnderwriter()) < loan.MaxSum) return Underwriter.Underwriter.EstimateSum(income, loan.ThisConditionsForUnderwriter());
            else
            {
                Console.WriteLine("Smth goes wrong!!!");
                return 0;
            }
        }
        private static bool CheckedID(string userInput)
        {
            if (userInput.Length == Constants.PassportIDLength)
            {
                Logger.Logger.Loging($"ID contains propper number of chars.");
                return true;
            }
            else
            {
                Logger.Logger.Loging($"Invalid ID. User ID contains: {userInput.Length} chars, but must contain {Constants.PassportIDLength} chars.");
                return false;
            } 
        }
        private static string CheckIsItPhoneNumber(string phoneNumber)
        {
            int flag = Constants.StartNumberDefenition;

            if (phoneNumber.Split('-').Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat)
            { 
                flag += 1;
                Logger.Logger.Loging($"Invalid Phone number. User input - {phoneNumber}.");
            }
            for (int i = 0; i < phoneNumber.Split('-').Length; i++)
            {
                if (Int32.TryParse(phoneNumber.Split('-')[i], out i) == false) flag += 1;
            }
            while (flag != Constants.StartNumberDefenition)
            {
                Console.WriteLine(SorryMessage, _phoneNumFormat);
                Logger.Logger.Loging($"Invalid Phone number. User input - {phoneNumber}.");
                phoneNumber = Console.ReadLine();
                flag = Constants.StartNumberDefenition;
                if (phoneNumber.Split('-').Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat) flag += 1;
                for (int i = 0; i < phoneNumber.Split('-').Length; i++)
                {
                    if (Int32.TryParse(phoneNumber.Split('-')[i], out i) == false) flag += 1;
                }
            }
            return phoneNumber;
        }
        #endregion

        #region Main Methods to get users Info
        public static void Greet()
        {
            Console.WriteLine(_greetingMessage, WhatTimeOfDayIsItNow(), Constants.BotName, Constants.NameOfproduct);
        }
        public static int GetUserSimpleAnswer()
        {
            string userAnswer = Console.ReadLine().ToUpper();
            userAnswer = ValidUserAnswer(userAnswer);
            if (userAnswer == Constants.YesString || userAnswer == Constants.YString) return (int)SimpleAnswers.YES;
            else return (int)SimpleAnswers.NO;
        }
        public static void AskForIntroducing(int userInterested)
        {
            if (userInterested == (int)SimpleAnswers.NO)
            {
                Goodbye();
            }
            else
            {
                Console.WriteLine(_glad);
                Console.WriteLine(_askForIntroducing);
            }
        }
        public static (string, string) GetApplicantFullName()
        {
            string userInput = Console.ReadLine();
            string[] splittedUserInput = userInput.Split(" ");
            splittedUserInput = ValidUserAnswer(splittedUserInput);
            while (String.IsNullOrWhiteSpace(splittedUserInput[0]) || String.IsNullOrWhiteSpace(splittedUserInput[1]))
            {
                splittedUserInput = ValidUserAnswer(splittedUserInput);
            }
            userInput = null;
            return CorrectPositionsOfNameAndSurname((splittedUserInput[0], splittedUserInput[1]));
        }
        public static DateTime GetApplicantDateOfBirth((string, string) applicantFullName)
        {
            Console.WriteLine(_askToProvideBirthday, applicantFullName.Item1);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime dateOfBitrh = DateTime.Parse(userInput);
            userInput = null;
            return CheckedDate(dateOfBitrh);
        }
        public static void CheckIfApplicantIsAdult(Applicant applicant)
        {
            if (Bot.ValidateApplicantAge(applicant) == false)
            {
                Console.WriteLine(Bot.LoanAreOnlyForAdult, (applicant.WhenApplicantGotAdult().Year - DateTime.Now.Year));
                Logger.Logger.Loging($"Applicant is not adult. Applicant age is - {applicant.Age}.");
                Goodbye();
            }
        }
        public static bool ValidateApplicantAge(Applicant applicant)
        {
            Type t = typeof(Applicant);
            object[] fields = t.GetCustomAttributes(true);
            foreach (AdultAgeAttribute field in fields)
            {
                if (applicant.Age >= field.Age) return true;
                else return false;
            }
            return true;
        }
        public static double GetApplicantIncome(Applicant applicant)
        {
            Console.WriteLine(_askAboutIncome, applicant.Name);
            string userInput = Console.ReadLine();
            userInput = TryParseToDouble(userInput);
            double applicantIncome = Double.Parse(userInput);
            userInput = null;
            return applicantIncome;
        }
        public static string AskPassportID()
        {
            Console.WriteLine(_askToProvidePassportID);
            string userInput = Console.ReadLine();
            bool check = CheckedID(userInput);
            while (check == false)
            {
                Console.WriteLine(SorryMessage, _passportIDFormat);
                Console.WriteLine(_askToProvidePassportID);
                userInput = Console.ReadLine();
                check = CheckedID(userInput);
            }
            return userInput;
        }
        public static DateTime AskPassportIssueDate()
        {
            Console.WriteLine(_askToProvidePassportIssue);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime date = DateTime.Parse(userInput);
            date = CheckedDate(date);
            userInput = null;
            return date;
        }
        public static string AskSex()
        {
            Console.WriteLine(_whatIsYourSex);
            string userInput = Console.ReadLine().ToUpper();
            while (userInput.StartsWith(Constants.Male) == false && userInput.StartsWith(Constants.Female) == false)
            {
                Logger.Logger.Loging($"Invalid Sex. User input - {userInput}.");
                Console.WriteLine(_askAgainForInvalidAnswer, Constants.Male, Constants.Female);
                userInput = Console.ReadLine().ToUpper();
            }
            return userInput;
        }
        public static int AskAboutChild()
        {
            Console.WriteLine(_anyChild);
            int answer = GetUserSimpleAnswer();
            string userInput;
            switch (answer)
            {
                case (int)SimpleAnswers.YES:
                    {
                        Console.WriteLine(_howManyChild);
                        userInput = Console.ReadLine();
                        userInput = TryParseToInt(userInput);
                        answer = Int32.Parse(userInput);
                    }
                    break;
                case (int)SimpleAnswers.NO:
                    answer = Constants.StartNumberDefenition;
                    break;
                default:
                    Console.WriteLine("Smth goes wrong!!!");
                    break;
            }
            userInput = null;
            return answer;
        }
        public static string AskPhoneNumber()
        {
            Console.WriteLine(_noPhoneNumber);
            string phoneNumber = Console.ReadLine();
            phoneNumber = CheckIsItPhoneNumber(phoneNumber);
            return phoneNumber;
        }

        #endregion

        #region Methods to work with loans
        public static void ShowTheListOfLoans(Applicant applicant)
        {
            ArrayList collectionOfLoans = CreateListIfLoans();
            Console.WriteLine(_introduceLoans, applicant.Name, collectionOfLoans.Capacity);
            foreach (LoanName loan in collectionOfLoans)
            {
                Console.WriteLine(loan);
            }
            Console.WriteLine(_introduceLoansCondition);
            int userAnswer = GetUserSimpleAnswer();
            if (userAnswer == (int)SimpleAnswers.YES)
            {
                ShowConditions();
            }
        }
        public static void AskToChooseCreditType()
        {
            Console.WriteLine(_askToChooseCredit);
            Console.WriteLine($"{(int)LoanName.auto} - {LoanName.auto}, \n{(int)LoanName.consumer} - {LoanName.consumer}, \n{(int)LoanName.mortgage} - {LoanName.mortgage}, \n{(int)LoanName.overdraft} - {LoanName.overdraft} ");
        }
        public static dynamic CreateALoanType(int loan)
        {
            switch (loan)
            {
                case ((int)LoanName.auto):
                    return new CarLoan();
                case ((int)LoanName.consumer):
                    return new ConsumeLoan();
                case ((int)LoanName.mortgage):
                    return new EstateLoan();
                case ((int)LoanName.overdraft):
                    return new Overdraft();
                default:
                    return null;
            }
        }
        public static dynamic CreateALoanType(int loan, double creditSum)
        {
            switch (loan)
            {
                case ((int)LoanName.auto):
                    return new CarLoan(creditSum);
                case ((int)LoanName.consumer):
                    return new ConsumeLoan(creditSum);
                case ((int)LoanName.mortgage):
                    return new EstateLoan(creditSum);
                case ((int)LoanName.overdraft):
                    return new Overdraft(creditSum);
                default:
                    return null;
            }
        }
        public static int EstimateCreditSum(double income, dynamic loan)
        {
            int estimateSum = (int)AskUnderwritter(income, loan);
            if (estimateSum > 0) Console.WriteLine(_estimateLoan, estimateSum, loan.Name);
            else Console.WriteLine(_chooseAnotherLoan, loan.Name);
            return estimateSum;
        }
        public static int AskIfApplicantWantsOtherLoan(Applicant applicant, double estimateSum)
        {
            Console.WriteLine(_askWhetherToChangeLoan, applicant.Name);
            int decision = GetUserSimpleAnswer();
            switch (decision)
            {
                case (int)SimpleAnswers.NO:
                    if (estimateSum == 0) Goodbye();
                    else decision = (int)SimpleAnswers.AGREE;
                    break;
                case (int)SimpleAnswers.YES:
                    break;
            }
            return decision;
        }
        public static void IfToContinue()
        {
            Console.WriteLine(_ifToContinue);
            int answer = GetUserSimpleAnswer();
            if (answer == (int)SimpleAnswers.NO) Goodbye();
            else Console.WriteLine(_continueToInsertProfile);
        }
        public static async Task CreateLoanAgreement(ArrayList profile)
        {
            using (FileStream fs = new FileStream("Agreement.json", FileMode.OpenOrCreate))
            {
                await JsonSerializer.SerializeAsync<ArrayList>(fs, profile);
            }
            Logger.Logger.Loging($"JSON created.");
        }
        #endregion

        #region InerfaceMethods
        public static void InsertIntoProfile<T>(ArrayList profile, T info)
        {
            profile.Add(info);
            Logger.Logger.Loging($"Information {info} added to {profile}.");
        }

        public static ArrayList UpdateProfileApplicant<T>(ArrayList profile, T info)
        {
            try
            {
                profile[0] = info;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
                Logger.Logger.Loging($"Exeption - {ex.Message}. Method - {ex.TargetSite}, Stack - {ex.StackTrace}");
            }
            return profile;
        }
        public static ArrayList UpdateProfileLoan<T>(ArrayList profile, T info)
        {
            try
            {
                profile[1] = info;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
                Console.WriteLine($"Метод: {ex.TargetSite}");
                Console.WriteLine($"Трассировка стека: {ex.StackTrace}");
                Logger.Logger.Loging($"Exeption - {ex.Message}. Method - {ex.TargetSite}, Stack - {ex.StackTrace}");
            }
            return profile;
        }
        #endregion
    }
}
