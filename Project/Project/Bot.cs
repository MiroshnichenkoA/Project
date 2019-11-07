using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project
{
    sealed class Bot
    {
        #region Fields
        private static readonly string _greetingMessage = "- Good {0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        public static readonly string _sorryMessage = "- Sorry. Can't understand you!";
        private static readonly string _askAgainForInvalidAnswer = " Please, insert \"{0}\" and/or \"{1}\" only";
        private static readonly string _goodbye = "- Goodbye! We are hope to see you soon!";
        private static readonly string _glad = "- Glad to hear this!";
        private static readonly string _askForIntroducing = "- Please, introduce yourself. Tell me your name and surname";
        private static readonly string _askWhereIsTheName = "- Is {0} your name?";
        private static readonly string _askToProvideBirthday = "- Dear {0}, to continue I need to ask your date of birth. Please, enter the date:";
        private static readonly string _loansAreOnlyForAdult = "Sorry, we don't provide loans for minors. You will become an adult in {0} years. \n See you then!";
        private static readonly string _incorrectDateOfBirth = "The date {0} you provide is more then current date {1}. \n Are you sure it's your birthday? \n Please, enter the date again:";
        private static readonly string _introduceLoans = "- {0}, at the current moment we can propose you {1} types of loans. They are:";
        private static readonly string _introduceLoansCondition = "Shall I show you the loans conditions?";
        private static readonly string _loanConditionsReadFormat = "\n Credit name: {0}. \n Granted for a period of not more than {1} years at a rate of {2} percent per annum. \n Aim: {3}";
        public static readonly string _askToChooseCredit = "\n- Choose the type of credit you need. Press the number you need.";
        #endregion

        #region Constructor
        public Bot()
        {
            //TODO: logging
        }
        #endregion

        #region Helpping Methods
        private ScriptForTimeOfDay WhatTimeOfDayIsItNow()
        {
            ScriptForTimeOfDay result;
            if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.morning && DateTime.Now.Hour < (int)ScriptForTimeOfDay.day) result = ScriptForTimeOfDay.morning;
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.day && DateTime.Now.Hour < (int)ScriptForTimeOfDay.evening) result = ScriptForTimeOfDay.day;
            else if (DateTime.Now.Hour >= (int)ScriptForTimeOfDay.evening && DateTime.Now.Hour < (int)ScriptForTimeOfDay.night) result = ScriptForTimeOfDay.evening;
            else result = ScriptForTimeOfDay.night;
            return result;
        }
        private string ValidUserAnswer(string userAnswer)
        {
            while ((userAnswer != Constants.yesString) && (userAnswer != Constants.noString))
            {
                Console.WriteLine(_sorryMessage);
                Console.WriteLine(String.Format(_askAgainForInvalidAnswer, SimpleAnswers.YES, SimpleAnswers.NO));
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }
        private string[] ValidUserAnswer(string[] userAnswer)
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
        private (string, string) CorrectPositionsOfNameAndSurname((string, string) fullName)
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
        private void Goodbye()
        {
            Console.WriteLine(_goodbye);
            //TODO: shut down
        }
        private DateTime CheckedBirhday(DateTime dateOfBirth)
        {
            while (dateOfBirth > DateTime.Now)
            {
                Console.WriteLine(_incorrectDateOfBirth, dateOfBirth, DateTime.Now);
                string userInput = Console.ReadLine();
                userInput = TryParseToDate(userInput);
                dateOfBirth = DateTime.Parse(userInput);
            }
            return dateOfBirth;
        }
        #endregion

        #region Main Methods to get users Info
        public void Greet()
        {
            ScriptForTimeOfDay currentTimeGreetingScript = WhatTimeOfDayIsItNow();
            Console.WriteLine(_greetingMessage, currentTimeGreetingScript, Constants.botName, Constants.nameOfproduct);
        }
        public int GetUserSimpleAnswer()
        {
            string userAnswer = Console.ReadLine().ToUpper();
            userAnswer = ValidUserAnswer(userAnswer);

            int result = Constants.startNumberDefenition;
            if (userAnswer == Constants.yesString) result = (int)SimpleAnswers.YES;
            else result = (int)SimpleAnswers.NO;
            return result;
        }
        public void AskForIntroducing(int userInterested)
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
        public (string, string) GetApplicantFullName()
        {
            string userInput = Console.ReadLine();
            string[] splittedUserInput = userInput.Split(" ");
            splittedUserInput = ValidUserAnswer(splittedUserInput);
            (string, string) fullName = (splittedUserInput[0], splittedUserInput[1]);
            (string, string) correctFullName = CorrectPositionsOfNameAndSurname(fullName);
            return correctFullName;
        }
        public DateTime GetApplicantDateOfBirth((string, string) applicantFullName)
        {
            Console.WriteLine(_askToProvideBirthday, applicantFullName.Item1);
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime dateOfBitrh = DateTime.Parse(userInput);
            dateOfBitrh = CheckedBirhday(dateOfBitrh);
            return dateOfBitrh;
        }
        public void CheckIfApplicantIsAdult(Applicant applicant)
        {
            DateTime dateApplicantGetAdult = applicant.WhenApplicantGotAdult();
            if (DateTime.Now < dateApplicantGetAdult)
            {
                Console.WriteLine(_loansAreOnlyForAdult, (dateApplicantGetAdult.Year - DateTime.Now.Year));
                Goodbye();
            }
        }
        #endregion

        #region Methods to work with loans
        public void ShowTheListOfLoans(Applicant applicant)
        {
            Console.WriteLine(_introduceLoans, applicant.ApplicantName, Loan.collectionOfLoansThatCanBeProvided.Capacity);
            foreach (LoanName loan in Loan.collectionOfLoansThatCanBeProvided)
            {
                Console.WriteLine(loan);
            }
            Console.WriteLine(_introduceLoansCondition);
            int userAnswer = GetUserSimpleAnswer();
            if (userAnswer == (int)SimpleAnswers.YES)
            {
                (LoanName, int, double, string)[] conditions = { CarLoan.Conditions(), EstateLoan.Conditions(), ConsumeLoan.Conditions(), Overdraft.Conditions() };
                foreach ((LoanName, int, double, string) i in conditions)
                {
                    Console.WriteLine(_loanConditionsReadFormat, i.Item1, i.Item2, i.Item3, i.Item4);
                }
            }
        }
        public void AskToChooseCreditType()
        {
            Console.WriteLine(_askToChooseCredit);
            Console.WriteLine($"{(int)LoanName.car} - {LoanName.car}, \n{(int)LoanName.consumer} - {LoanName.consumer}, \n{(int)LoanName.estate} - {LoanName.estate}, \n{(int)LoanName.overdraft} - {LoanName.overdraft} ");
        }
        public dynamic CreateALoanType(int loan)
        {
            Loan defaultLoan = new Loan();
            switch (loan)
            {
                case ((int)LoanName.car): defaultLoan = new CarLoan();
                    break;
                case ((int)LoanName.consumer): defaultLoan = new ConsumeLoan();
                    break;
                case ((int)LoanName.estate): defaultLoan = new EstateLoan();
                    break;
                case ((int)LoanName.overdraft): defaultLoan = new Overdraft(); 
                    break;
                default: Console.WriteLine("Ups. Smth goes wrong!!!");
                    break;
            }
            return defaultLoan;
        }
        #endregion
    }
}
