using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.DB;
using System.Runtime.Serialization;

namespace Shop_Danylo.Users
{
    class UserPremiumPlus : UserPremium
    {

        //Преміум+ покупець в якого скидка 10% та безплатна доставка
        
        public UserPremiumPlus(string userName, string email, string password) : base(userName, email, password)
        {
            Discount = 10;
        }
        public UserPremiumPlus(string userName, string email, string password, float mon) : base(userName, email, password, mon)
        {
            Discount = 10;
        }
    }
}
