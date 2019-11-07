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
        private readonly string _greetingMessage = "- Good {0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        private readonly string _sorryMessage = "- Sorry. Can't understand you!";
        private readonly string _askAgainForInvalidAnswer = " Please, insert \"{0}\" and/or \"{1}\" only";
        private readonly string _goodbye = "- Goodbye! We are hope to see you soon!";
        private readonly string _glad = "- Glad to hear this!";
        private readonly string _askForIntroducing = "- Please, introduse yourself. Tell me your name and surname";
        private readonly string _askWhereIsTheName = "- Is {0} your name?";
        private readonly string _askToProvideBirthday = "- Dear {0}, to continue I need to ask your date of birth. Please, enter the date:";
        private readonly string _loansAreOnlyForAdult = "Sorry, we don't provide loans for minors. You will become an adult in {0} years. \n See you then!";
        private readonly string _incorrectDateOfBirth = "The date {0} you provide is more then current date {1}. \n Are you sure it's your birthday? \n Please, enter the date again:";
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
                Console.WriteLine(String.Format(_askAgainForInvalidAnswer,SimpleAnswers.YES,SimpleAnswers.NO));
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }
        private string[] ValidUserAnswer(string[] userAnswer)
        {
            while (userAnswer.Length != Constants.numOfWordsInFullName)
            {
                Console.WriteLine(_sorryMessage);
                Console.WriteLine(_askAgainForInvalidAnswer,Constants.tellName,Constants.tellSurname);
                string newInput = Console.ReadLine();
                userAnswer = newInput.Split(" ");
            }
            return userAnswer;
        }
        private (string, string) CorrectPositionsOfNameAndSurname((string, string) fullName)
        {
            Console.WriteLine(String.Format(_askWhereIsTheName,fullName.Item1));
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
        private string TryParseToDate(string userInput)
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
            Console.WriteLine(string.Format(_greetingMessage, currentTimeGreetingScript, Constants.botName, Constants.nameOfproduct));
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
            Console.WriteLine(String.Format(_askToProvideBirthday, applicantFullName.Item1));
            string userInput = Console.ReadLine();
            userInput = TryParseToDate(userInput);
            DateTime dateOfBitrh = DateTime.Parse(userInput);
            dateOfBitrh = CheckedBirhday(dateOfBitrh);
            return dateOfBitrh;
        }
        public void CheckIfApplicantIsAdult(Applicant applicant)
        {
            DateTime dateApplicantGetAdult = applicant.WhenApplicantGotAdult();
            if (DateTime.Now < dateApplicantGetAdult) Console.WriteLine(_loansAreOnlyForAdult,(dateApplicantGetAdult.Year - DateTime.Now.Year));
        }
        #endregion
    }
}