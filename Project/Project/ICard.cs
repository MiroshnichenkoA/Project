using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface ICard
    {
        private CreditCard RealeseCard()
        {
            CreditCard creditCard = new CreditCard();
            return creditCard;
        }
    }
}
