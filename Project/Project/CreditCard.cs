using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public class CreditCard : ICard
    {
        private int _number;
        private static int counterOfAllCards = Constants.StartNumberDefenition;

        public CreditCard()
        {
            counterOfAllCards += 1;
            _number = counterOfAllCards;
        }

        private CreditCard RealeseCard()
        {
            CreditCard creditCard = new CreditCard();
            return creditCard;
        }
    }
}
