using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Danylo.Users;
using Shop_Danylo.DB;

namespace Shop_Danylo
{

    //клас з методами для купівлі статусу ПРЕМІУМ акаунту
    public static class UpAccount
    {
        public static User UpToPremium(User user) {
                User user2 = new UserPremium(user.UserName, user.Email, user.Password, user.Money - 100);
                DBUsers1.DeleteUser(user.Email);
                DBUsers1.Add(user2);
                Console.WriteLine("\nАкаунт було підвищено до ПЕРІМУМ");
                Console.WriteLine("\n");
                return user2;
                        
        }

        public static User UpToPremiumPlus(User user)
        {
            
            User user2 = new UserPremiumPlus(user.UserName, user.Email, user.Password, user.Money - 200);
            DBUsers1.DeleteUser(user.Email);
            DBUsers1.Add(user2);
            Console.WriteLine("\nАкаунт було підвищено до ПЕРІМУМ+");
            Console.WriteLine("\n");
            return user2;
           
            
        }
    }
}
