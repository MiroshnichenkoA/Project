using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
   sealed class Bot
    {
        #region Fields
        private readonly string _greetingMessage = "{0} and velcom to the Humster Bank Corporation. My name is {1}. How can I help you?";
        #endregion

        #region Properties
        #endregion

        #region Methods
        private string WhatTimeOfDayIsItNow()
        {
            string result;
            if (DateTime.Now.Hour < Constants.minMorningTime) result = Constants.scriptMorning;
            else if (DateTime.Now.Hour < Constants.minDayTime) result = Constants.scriptDay;
            else if (DateTime.Now.Hour < Constants.minEveningTime) result = Constants.scriptEvening;
            else result = Constants.scriptNight;
            return result;
        }

        public void Greet()
        {
            string currentTimeGreetingScript = WhatTimeOfDayIsItNow();
            Console.WriteLine(DateTime.Now.Hour);
            Console.WriteLine(string.Format(_greetingMessage, currentTimeGreetingScript, Constants.botName));
        }
        #endregion

        #region MethodsForUserIntroducing
        string[] SplitUserInput(string userInput)
        {
            userInput = userInput.ToUpper();
            string[] splittdUserInput = userInput.Split(" ");
            return splittdUserInput;
        }

        string GetApplicantSurname(string applicantFullName)
        {
            string surname = SplitUserInput(applicantFullName)[1];
            return surname;
        }

        string GetApplicantName(string applicantFullName)
        {
            string name = SplitUserInput(applicantFullName)[2];
            return name;
            //TODO проверить не пусть ли тут на случай если пользователь ввел одно слово без пробела
        }
        #endregion
    }
}
 