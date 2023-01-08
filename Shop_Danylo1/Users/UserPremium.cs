using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.DB;
using System.Runtime.Serialization;

namespace Shop_Danylo.Users
{   
    class UserPremium : User
    {
        
        //Преміум покупець в якого немає скидок але безплатна доставка
        public UserPremium(string userName, string email, string password) : base(userName, email, password)
        {
            Delivery = 0;
        }

        public UserPremium(string userName, string email, string password, float mon) : base(userName, email, password, mon)
        {
            Delivery = 0;
        }
    }
}
