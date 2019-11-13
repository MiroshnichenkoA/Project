using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class CreditCard : ICard
    {
        private static int counterOfAllCards = Constants.StartNumberDefenition;

        public int Number { get;}
       
        public CreditCard()
        {
            counterOfAllCards += 1;
            Number = counterOfAllCards;
        }
    }
}
