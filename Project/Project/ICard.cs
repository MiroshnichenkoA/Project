using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    public interface ICard
    {
        CreditCard RealeseCard()
        {
            CreditCard creditCard = new CreditCard();
            return creditCard;
        }
    }
}
