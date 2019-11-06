using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    sealed class Bot
    {
        #region Fields
        private readonly string _greetingMessage = "-{0} and welcom to the Humster Bank Corporation. \n My name is {1}. We provide different types of {2} with attractive rates. \n Are you interested in {2}?";
        private readonly string _askAgainForInvalidAnswer = "- Sorry, can't underastand your answer. \n Please, answer \"{0}\" or \"{1}\"";
        private readonly string _goodbye = "Goodbye! We are hope to see you soon!";
        private readonly string _glad = "Glad to hear this!";
        private readonly string _askForIntroducing = "Please, introduse yourself. Tell me your name and surname";
        
        #endregion

        #region Constructor
        public Bot()
        {
            //TODO: logging
        }
        #endregion

        #region Helpping Methods
        private string WhatTimeOfDayIsItNow()
        {
            string result;
            if (DateTime.Now.Hour >= Constants.minMorningTime && DateTime.Now.Hour < Constants.minDayTime) result = Constants.scriptMorning;
            else if (DateTime.Now.Hour >= Constants.minDayTime && DateTime.Now.Hour < Constants.minEveningTime) result = Constants.scriptDay;
            else if (DateTime.Now.Hour >= Constants.minEveningTime && DateTime.Now.Hour < Constants.minNightTime) result = Constants.scriptEvening;
            else result = Constants.scriptNight;
            return result;
        }
        private string[] SplitUserInput(string userInput)
        {
            userInput = userInput.ToUpper();
            string[] splittdUserInput = userInput.Split(" ");
            return splittdUserInput;
        }
        private string GetApplicantSurname(string applicantFullName)
        {
            string surname = SplitUserInput(applicantFullName)[1];
            return surname;
        }
        private string GetApplicantName(string applicantFullName)
        {
            string name = SplitUserInput(applicantFullName)[2];
            return name;
            //TODO проверить не пусть ли тут на случай если пользователь ввел одно слово без пробела
        }
        private string ValidUserAnswer(string userAnswer)
        {
            while ((userAnswer != Constants.yesString) && (userAnswer != Constants.noString))
            {
                Console.WriteLine(String.Format(_askAgainForInvalidAnswer,Constants.yesString,Constants.noString));
                userAnswer = Console.ReadLine().ToUpper();
            }
            return userAnswer;
        }
        #endregion

        #region Main Methods
        public void Greet()
        {
            string currentTimeGreetingScript = WhatTimeOfDayIsItNow();
            Console.WriteLine(string.Format(_greetingMessage, currentTimeGreetingScript, Constants.botName, Constants.nameOfproduct));
        }
        public int GetUserSimpleAnswer()
        {   
            string userAnswer = Console.ReadLine().ToUpper();
            userAnswer = ValidUserAnswer(userAnswer);
            
            int result = Constants.startNumberDefenition;
            if (userAnswer == Constants.yesString) result = Constants.yesInt;
            else result = Constants.noInt;
            return result;
        }
        public void AskForIntroducing(int userInterested)
        {
            if (userInterested == Constants.noInt)
            {
                Console.WriteLine(_goodbye);
                //.Shutdown;
            }
            else
            {
                Console.WriteLine(_glad);
                Console.WriteLine(_askForIntroducing);
            }
        }
        #endregion
    }
}