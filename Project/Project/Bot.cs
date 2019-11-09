using System;
using System.Collections;

namespace Project
{
    abstract class Bot
    {
        #region Fields

        #region Sorry Message Format and Cases for Checking User Inputs: Fields
        private static readonly string _sorryMessage = "- Sorry. Can't understand you! {0}";
        private static readonly string _insertOnlyDate = "Insert date";
        private static readonly string _insertOnlyNumber = "Insert number";
        private static readonly string _phoneNumFormat = "You should insert 9 numbers instead of X in format +375-XX-XXX-XX-XX";
        //TODO: explain why Sorry
        private static readonly string _askAgainForInvalidAnswer = " Please, insert {0} and/or {1} only";
        private static readonly string _loansAreOnlyForAdult = "Sorry, we don't provide loans for minors. You will become an adult in {0} years. \n See you then!";
        private static readonly string _incorrectDate = "The date {0} you provide is more then current date {1}. \n Are you sure it's right date? \n Please, enter the date again:";
        #endregion

        private static readonly string _greetingMessage = "- Good {0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        private static readonly string _goodbye = "- Goodbye! We are hope to see you soon!";
        private static readonly string _glad = "- Glad to hear this!";
        private static readonly string _askForIntroducing = "- Please, introduce yourself. Tell me your name and surname";
        private static readonly string _askWhereIsTheName = "- Is {0} your name?";
        private static readonly string _askToProvideBirthday = "- Dear {0}, to continue I need to ask your date of birth. Please, enter the date:";       
        private static readonly string _introduceLoans = "- {0}, at the current moment we can propose you {1} types of loans. They are:";
        private static readonly string _introduceLoansCondition = "Shall I show you the loans conditions?";
        private static readonly string _loanConditionsReadFormat = "\n Credit name: {0}. \n Granted for a period of not more than {1} years at a rate of {2} percent per annum. You can be provided from {4} BYN to {5} BYN. \n Aim: {3}";
        private static readonly string _askToChooseCredit = "\n- Choose the type of credit you need. Press the number you need.";
        private static readonly string _askAboutIncome = "- {0}, you have to insert your estimated income. \nI'll use this information to calculate the estimate amount of credit we can propose to you. \nAlso I'll put this information in your profile.";
        private static readonly string _chooseAnotherLoan = "- Sorry, you do not have enough income for {0} loan.";
        private static readonly string _estimateLoan = "- We can provide you about {0} BYN if you'll take {1} credit.";
        private static readonly string _askWhetherToChangeLoan = "- {0}, do you want to choose another loan product?";
        private static readonly string _ifToContinue = "- So do you want to take a loan?";
        private static readonly string _continueToInsertProfile = "- Perfect. In that case let's fill out your profile";
        private static readonly string _askToProvidePassportIssue = "- Insert your passport issue date, please.";
        private static readonly string _askToProvidePassportID = "- Insert your passport ID, please.";
        private static readonly string _whatIsYourSex = "- If you are man, please, enter M, and if you are woman, pleae enter F. Thank you.";
        private static readonly string _anyChild = $"- Do you have kids under {Constants.adultYears} year's old?";
        private static readonly string _howManyChild = "- How many?";
        private static readonly string _profileIsFilled = "- Thank you {0}! I will send your applicant profile to our specialist. \n After specialists consider the oppotunity to give you a loan, You will be notified by SMS.";
        private static readonly string _noPhoneNumber = "- Oh, I guess we don't have your phone number in our system. We acept only Belorussian phone numbers. Please, provide it in the international format \"+375-XX-XXX-XX-XX\"";
        #endregion

        #region Properties
        public static string SorryMessage { get { return _sorryMessage; } }
        public static string AskToChooseCredit { get { return _askToChooseCredit; } }
        public static string ProfileIsFilled { get { return _profileIsFilled; } }
        public static string LoanAreOnlyForAdult { get { return _loansAreOnlyForAdult; } }
        #endregion

        #region Constructor
        public Bot()
        {
            //TODO: logging
        }
        #endregion



        #region Helpping Methods
        private static ScriptForTimeOfDay WhatTimeOfDayIsItNow()
        {
            ScriptForTimeOfDay result;
            if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.morning && DateTime.Now.Hour < (int)ScriptForTimeOfDay.day) result = ScriptForTimeOfDay.morning;
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.day && DateTime.Now.Hour < (int)ScriptForTimeOfDay.evening) result = ScriptForTimeOfDay.day;
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.evening && DateTime.Now.Hour < (int)ScriptForTimeOfDay.night) result = ScriptForTimeOfDay.evening;
            else result = ScriptForTimeOfDay.night;
            return result;
        }
        private static string ValidUserAnswer(string userAnswer)
        {
            while ((userAnswer != Constants.yesString) && (userAnswer != Constants.noString) && (userAnswer != Constants.yString) && (userAnswer != Constants.nString))
            {
                Console.WriteLine(SorryMessage, (_askAgainForInvalidAnswer, SimpleAnswers.YES, SimpleAnswers.NO));
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }
        private static string[] ValidUserAnswer(string[] userAnswer)
        {
            while (userAnswer.Length != Constants.numOfWordsInFullName)
            {
                Console.WriteLine(SorryMessage, (_askAgainForInvalidAnswer, Constants.tellName, Constants.tellSurname));
                userAnswer = Console.ReadLine().Split(" ");
            }
            return userAnswer;
        }
        private static (T, T) CorrectPositionsOfNameAndSurname<T>((T, T) fullName)
        {
            Console.WriteLine(String.Format(_askWhereIsTheName, fullName.Item1));
            int namePosition = GetUserSimpleAnswer();
            (T, T) correctFullName;
            if (namePosition == (int)SimpleAnswers.YES)
            {
                correctFullName.Item1 = fullName.Item1;
                correctFullName.Item2 = fullName.Item2;
            }
            else
            {
                correctFullName.Item1 = fullName.Item2;
                correctFullName.Item2 = fullName.Item1;
            }
            return correctFullName;
        }
        public static string TryParseToDate(string userInput)
        {
            DateTime dateOfBitrh;
            bool check = DateTime.TryParse(userInput, out dateOfBitrh);
            while (check == false)
            {
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
                Console.WriteLine(SorryMessage, _insertOnlyNumber);
                userInput = Console.ReadLine();
                check = Double.TryParse(userInput, out num);
            }
            return userInput;
        }
        public static void Goodbye()
        {
            Console.WriteLine(_goodbye);
            Environment.Exit(0);
        }
        private static DateTime CheckedDate(DateTime date)
        {
            while (date > DateTime.Now)
            {
                Console.WriteLine(_incorrectDate, date, DateTime.Now);
                string userInput = Console.ReadLine();
                userInput = TryParseToDate(userInput);
                date = DateTime.Parse(userInput);
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
        }
        public static (LoanName, double) ShowConditions(dynamic loan)
        {
            return loan.ThisConditions();
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
        private static T SearchInProfile<T>(ArrayList profile, T info)
        {
            return (T)profile[profile.IndexOf(info)];
        }
        private static bool CheckedID(string userInput)
        {
            if (userInput.Length == Constants.passportIDLength) return true;
            else return false;
        }
        private static string CheckIsItPhoneNumber(string phoneNumber)
        {
            int flag = Constants.startNumberDefenition;

            if (phoneNumber.Split('-').Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat) flag += 1;
            for (int i = 0; i < phoneNumber.Split('-').Length; i++)
            {
                if (Int32.TryParse(phoneNumber.Split('-')[i], out i) == false) flag += 1;
            }
            //TODO try catch  0, 2, 3 item of massive = 2, 1 item = 3
            while (flag != Constants.startNumberDefenition)
            {
                Console.WriteLine(SorryMessage, _phoneNumFormat);
                phoneNumber = Console.ReadLine();
                flag = Constants.startNumberDefenition;
                if (phoneNumber.Split('-').Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat) flag += 1;
                for (int i = 0; i < phoneNumber.Split('-').Length; i++)
                {
                    if (Int32.TryParse(phoneNumber.Split('-')[i], out i) == false) flag += 1;
                }
                //TODO try catch  0, 2, 3 item of massive = 2, 1 item = 3
            }
            return phoneNumber;
        }
        #endregion

        #region Main Methods to get users Info
        public static void Greet()
        {
            Console.WriteLine(_greetingMessage, WhatTimeOfDayIsItNow(), Constants.botName, Constants.nameOfproduct);
        }
        public static int GetUserSimpleAnswer()
        {
            string userAnswer = Console.ReadLine().ToUpper();
            userAnswer = ValidUserAnswer(userAnswer);
            if (userAnswer == Constants.yesString || userAnswer == Constants.yString) return (int)SimpleAnswers.YES;
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
        public static void InsertIntoProfile(ArrayList profile, object info)
        {
            profile.Add(info);
            //TODO: logg
        }
        public static void InsertIntoProfile(ArrayList profile, object info1, object info2)
        {
            profile.Add(info1);
            profile.Add(info2);
            //TODO: logg
        }
        public static void InsertIntoProfile(ArrayList profile, object info1, object info2, object info3)
        {
            profile.Add(info1);
            profile.Add(info2);
            profile.Add(info3);
            //TODO: logg
        }
        public static void DeleteFromProfile(ArrayList profile, object info1, object info2, object info3)
        {
            bool searched = profile.Contains(info1);
            if (searched) profile.Remove(info1);
            //TODO: logg
            searched = profile.Contains(info2);
            if (searched) profile.Remove(info2);
            //TODO: logg
            searched = profile.Contains(info3);
            if (searched) profile.Remove(info3);
            //TODO: logg
        }
        public static (string, string) GetApplicantFullName()
        {
            string userInput = Console.ReadLine();
            string[] splittedUserInput = userInput.Split(" ");
            do
            {
                splittedUserInput = ValidUserAnswer(splittedUserInput);
            } while (String.IsNullOrWhiteSpace(splittedUserInput[0]) && String.IsNullOrWhiteSpace(splittedUserInput[1]));        
            return CorrectPositionsOfNameAndSurname((splittedUserInput[0], splittedUserInput[1]));
        }
        public static DateTime GetApplicantDateOfBirth((string, string) applicantFullName)
        {
            Console.WriteLine(_askToProvideBirthday, applicantFullName.Item1);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime dateOfBitrh = DateTime.Parse(userInput);
            return CheckedDate(dateOfBitrh);
        }
        public static void CheckIfApplicantIsAdult(Applicant applicant)
        {
            if (Bot.ValidateApplicantAge(applicant) == false)
            {
                Console.WriteLine(Bot.LoanAreOnlyForAdult, (applicant.WhenApplicantGotAdult().Year - DateTime.Now.Year));
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
            Console.WriteLine(_askAboutIncome, applicant.ApplicantName);
            string userInput = Console.ReadLine();
            userInput = TryParseToDouble(userInput);
            double applicantIncome = Double.Parse(userInput);
            return applicantIncome;
        }
        public static string AskPassportID()
        {
            bool check;
            string userInput;
            do
            {
                Console.WriteLine(_askToProvidePassportID);
                userInput = Console.ReadLine();
                check = CheckedID(userInput);
            } while (check == false);
            return userInput;
        }
        public static DateTime AskPassportIssueDate()
        {
            Console.WriteLine(_askToProvidePassportIssue);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime date = DateTime.Parse(userInput);
            date = CheckedDate(date);
            return date;
        }
        public static string AskSex()
        {
            Console.WriteLine(_whatIsYourSex);
            string userInput = Console.ReadLine().ToUpper();
            while (userInput.StartsWith(Constants.male) && userInput.StartsWith(Constants.female))
            {
                Console.WriteLine(_askAgainForInvalidAnswer, Constants.male, Constants.female);
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
                    answer = Constants.startNumberDefenition;
                    break;
                default:
                    Console.WriteLine("Smth goes wrong!!!");
                    break;
            }
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
            Console.WriteLine(_introduceLoans, applicant.ApplicantName, collectionOfLoans.Capacity);
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
                    Console.WriteLine("Ups. Smth goes wrong!!!");
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
        public static int AskIfApplicantWantOtherLoan(Applicant applicant, double estimateSum)
        {
            Console.WriteLine(_askWhetherToChangeLoan, applicant.ApplicantName);
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
        #endregion
    }
}
