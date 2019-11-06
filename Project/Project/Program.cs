using System;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.Greet();
            int userInterested = bot.GetUserSimpleAnswer();
            bot.AskForIntroducing(userInterested);
        }
    }
}