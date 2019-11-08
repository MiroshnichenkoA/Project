using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{
    abstract class Bot
    {
        #region Fields
        private static readonly string _greetingMessage = "- Good {0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        private static readonly string _sorryMessage = "- Sorry. Can't understand you!";
        private static readonly string _askAgainForInvalidAnswer = " Please, insert {0} and/or {1} only";
        private static readonly string _goodbye = "- Goodbye! We are hope to see you soon!";
        private static readonly string _glad = "- Glad to hear this!";
        private static readonly string _askForIntroducing = "- Please, introduce yourself. Tell me your name and surname";
        private static readonly string _askWhereIsTheName = "- Is {0} your name?";
        private static readonly string _askToProvideBirthday = "- Dear {0}, to continue I need to ask your date of birth. Please, enter the date:";
        private static readonly string _loansAreOnlyForAdult = "Sorry, we don't provide loans for minors. You will become an adult in {0} years. \n See you then!";
        private static readonly string _incorrectDate = "The date {0} you provide is more then current date {1}. \n Are you sure it's right date? \n Please, enter the date again:";
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

        public static string SorryMessage { get { return _sorryMessage; } }
        public static string AskToChooseCredit { get { return _askToChooseCredit; } }
        public static string ProfileIsFilled { get { return _profileIsFilled; } }

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
            while ((userAnswer != Constants.yesString) && (userAnswer != Constants.noString))
            {
                Console.WriteLine(_sorryMessage);
                Console.WriteLine(String.Format(_askAgainForInvalidAnswer, SimpleAnswers.YES, SimpleAnswers.NO));
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }
        private static string[] ValidUserAnswer(string[] userAnswer)
        {
            while (userAnswer.Length != Constants.numOfWordsInFullName)
            {
                Console.WriteLine(_sorryMessage);
                Console.WriteLine(_askAgainForInvalidAnswer, Constants.tellName, Constants.tellSurname);
                string newInput = Console.ReadLine();
                userAnswer = newInput.Split(" ");
            }
            return userAnswer;
        }
        private static (string, string) CorrectPositionsOfNameAndSurname((string, string) fullName)
        {
            Console.WriteLine(String.Format(_askWhereIsTheName, fullName.Item1));
            int namePosition = GetUserSimpleAnswer();
            (string, string) correctFullName;
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
                Console.WriteLine(_sorryMessage);
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
                Console.WriteLine(_sorryMessage);
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
                Console.WriteLine(_sorryMessage);
                userInput = Console.ReadLine();
                check = Double.TryParse(userInput, out num);
            }
            return userInput;
        }
        private static void Goodbye()
        {
            Console.WriteLine(_goodbye);
            //TODO: shut down
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
            collectionOfLoans.Add(LoanName.car);
            collectionOfLoans.Add(LoanName.consumer);
            collectionOfLoans.Add(LoanName.estate);
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
            (LoanName, double) conditions = loan.ThisConditions();
            return conditions;
        }
        private static double AskUnderwritter(double income, dynamic loan)
        {
            (double, int) conditions = loan.ThisConditionsForUnderwriter();
            double estimateSum = Underwriter.Underwriter.EstimateSum(income, conditions);
            if (estimateSum >= loan.MaxSum) estimateSum = loan.MaxSum;
            else if (estimateSum < loan.MinSum) estimateSum = 0;
            else if (estimateSum >= loan.MinSum && estimateSum < loan.MaxSum) estimateSum = estimateSum;
            else Console.WriteLine("Smth goes wrong!!!");
            return estimateSum;
        }
        private static object SearchInProfile(ArrayList profile, object info)
        {
            int index = profile.IndexOf(info);
            object searched = profile[index];
            return searched;
        }
        private static bool CheckedID(string userInput)
        {
            bool check = false;
            if (userInput.Length == 14) check = true;
            else check = false;
            return check;
        }
        private static string CheckIsItPhoneNumber(string phoneNumber)
        {
            string[] numbersInPhoneNumber = phoneNumber.Split('-');
            int flag = Constants.startNumberDefenition;
       
            if (numbersInPhoneNumber.Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat) flag += 1;
            for (int i = 0; i < numbersInPhoneNumber.Length; i++)
            {
                if (Int32.TryParse(numbersInPhoneNumber[i], out i) == false) flag += 1;  
            }
            //TODO try catch  0, 2, 3 item of massive = 2, 1 item = 3
            while (flag != Constants.startNumberDefenition)
            {
                Console.WriteLine(_sorryMessage);
                phoneNumber = Console.ReadLine();
                numbersInPhoneNumber = phoneNumber.Split('-');
                flag = Constants.startNumberDefenition;
                if (numbersInPhoneNumber.Length != Constants.numberOfSlotsInPhoneNumberFormat || phoneNumber.Length != Constants.numberOfCharsInPhoneNumberFormat) flag += 1;
                for (int i = 0; i < numbersInPhoneNumber.Length; i++)
                {
                    if (Int32.TryParse(numbersInPhoneNumber[i], out i) == false) flag += 1;
                }
                //TODO try catch  0, 2, 3 item of massive = 2, 1 item = 3
            }
            return phoneNumber;
        }
        #endregion

        #region Main Methods to get users Info
        public static void Greet()
        {
            ScriptForTimeOfDay currentTimeGreetingScript = WhatTimeOfDayIsItNow();
            Console.WriteLine(_greetingMessage, currentTimeGreetingScript, Constants.botName, Constants.nameOfproduct);
        }
        public static int GetUserSimpleAnswer()
        {
            string userAnswer = Console.ReadLine().ToUpper();
            userAnswer = ValidUserAnswer(userAnswer);

            int result = Constants.startNumberDefenition;
            if (userAnswer == Constants.yesString) result = (int)SimpleAnswers.YES;
            else result = (int)SimpleAnswers.NO;
            return result;
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
        }
        public static void InsertIntoProfile(ArrayList profile, object info1, object info2)
        {
            profile.Add(info1);
            profile.Add(info2);
        }
        public static void InsertIntoProfile(ArrayList profile, object info1, object info2, object info3)
        {
            profile.Add(info1);
            profile.Add(info2);
            profile.Add(info3);
        }
        public static void DeleteFromProfile(ArrayList profile, object info1, object info2, object info3)
        {
            bool searched = profile.Contains(info1);
            if (searched) profile.Remove(info1);
            searched = profile.Contains(info2);
            if (searched) profile.Remove(info2);
            searched = profile.Contains(info3);
            if (searched) profile.Remove(info3);
        }
        public static (string, string) GetApplicantFullName()
        {
            string userInput = Console.ReadLine();
            string[] splittedUserInput = userInput.Split(" ");
            splittedUserInput = ValidUserAnswer(splittedUserInput);
            (string, string) fullName = (splittedUserInput[0], splittedUserInput[1]);
            (string, string) correctFullName = CorrectPositionsOfNameAndSurname(fullName);
            return correctFullName;
        }
        public static DateTime GetApplicantDateOfBirth((string, string) applicantFullName)
        {
            Console.WriteLine(_askToProvideBirthday, applicantFullName.Item1);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime dateOfBitrh = DateTime.Parse(userInput);
            dateOfBitrh = CheckedDate(dateOfBitrh);
            return dateOfBitrh;
        }
        public static void CheckIfApplicantIsAdult(Applicant applicant)
        {
            DateTime dateApplicantGetAdult = applicant.WhenApplicantGotAdult();
            if (DateTime.Now < dateApplicantGetAdult)
            {
                Console.WriteLine(_loansAreOnlyForAdult, (dateApplicantGetAdult.Year - DateTime.Now.Year));
                Goodbye();
            }
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
            while (userInput != Constants.male && userInput != Constants.female)
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
                case (int)SimpleAnswers.NO: answer = Constants.startNumberDefenition;
                    break;
                default:
                    Console.WriteLine("Smth goes wrong!!!");
                    break;
            }
            return answer;
        }
        public static string AskPhoneNumber(Applicant applicant)
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
            Console.WriteLine($"{(int)LoanName.car} - {LoanName.car}, \n{(int)LoanName.consumer} - {LoanName.consumer}, \n{(int)LoanName.estate} - {LoanName.estate}, \n{(int)LoanName.overdraft} - {LoanName.overdraft} ");
        }
        public static dynamic CreateALoanType(int loan)
        {
            dynamic defaultLoan = new ConsumeLoan();
            switch (loan)
            {
                case ((int)LoanName.car): 
                    defaultLoan = new CarLoan();
                    break;
                case ((int)LoanName.consumer): 
                    defaultLoan = new ConsumeLoan();
                    break;
                case ((int)LoanName.estate): 
                    defaultLoan = new EstateLoan();
                    break;
                case ((int)LoanName.overdraft): 
                    defaultLoan = new Overdraft(); 
                    break;
                default: 
                    Console.WriteLine("Ups. Smth goes wrong!!!");
                    defaultLoan = null;
                    break;
            }
            return defaultLoan;
        }
        public static double EstimateCreditSum(double income, dynamic loan)
        {
            double estimateSum = AskUnderwritter(income, loan);
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
