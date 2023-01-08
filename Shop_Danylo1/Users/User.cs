using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.DB;
using Shop_Danylo.Product_;


namespace Shop_Danylo.Users
{
    public class User : BaseUser
    {
        //Звичайний покупець в якого немає скидок та платна доставка (50 грн)




        //конструктор для початкового створення акаунту
        public User(string userName, string email, string password) : base(userName, email, password)
        {
            DBUsers1.Add(this);
        }

        //конструктор для відновлення акаунту (входу)
        public User(string userName, string email, string password, float mon) : base(userName, email, password, mon)
        {
        }

        

        public override void Buy(Product prod, int n)
        {
            float priceNew = n*prod.Cost - n*(prod.Cost / 100 * Discount);

            Boolean bbb = true;
            while (bbb)
            {
                if ((Money - priceNew - Delivery) >= 0)
                {
                    money -= (priceNew + Delivery);
                    Console.WriteLine("\nТранзакція проведена успішно)");
                    DBProducts.UpdateAmount(prod, prod.Amount - n);
                    DBUsers1.UpdateMoney(Email, money);
                    DBHistoryOfShopping.Add(this, prod, n, priceNew);
                    bbb = false;
                }
                else
                {
                    Console.Write("На балнсi не достатньо коштiв.\nВаш баланс = "+Money+"грн\n"+"Потрiбна сума для покупки = "+ (priceNew + Delivery) +"грн\n"+ "Бажаєте поповнити?\n1. Yes\n2. No\nВаш вибір: ");
                    string num = (Console.ReadLine());
                    if (String.Compare(num, "1") == 0)
                    {
                        Console.Write("Введіть кількість поповнення: ");
                        int mon = Convert.ToInt32(Console.ReadLine());
                        AddMoney(mon);
                    }
                    else
                    {
                        Console.WriteLine("Покупку не виконано :(");
                        bbb = false;
                    }
                }
            }
            
        }

        
    }
}
