using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop_Danylo.DB;
using Shop_Danylo.Product_;

namespace Shop_Danylo.Users
{
    public abstract class BaseUser
    {

        public Random rand = new Random();

        private protected float discount; //ціна скидки у відсотках

        private protected float delivery; //ціна доставки

        private static int numer = 0;
        public float Delivery
        {
            get { return delivery; }
            set { delivery = value; }
        }

        public float Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private protected float money;

        public float Money
        {
            get { return money; }
            set { money = Money; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set
            {
                userName = SetName(value);
            }
        }

        public string Email { get; set; }


        public string Password { get; set; }


        public BaseUser(string userName, string email, string password)
        {
            Delivery = 50;
            Discount = 0;
            UserName = userName;
            Email = email;
            Password = Hash.HashPasswrd(password);
            money = 0;
        }

        //конструктор для відновлення акаунту (входу)
        public BaseUser(string userName, string email, string password, float mon)
        {
            Delivery = 50;
            Discount = 0;
            UserName = userName;
            Email = email;
            Password = (password);
            money = mon;
        }


        


        public void UserInformation()
        {
            Console.WriteLine();
            Console.WriteLine("Name: " + UserName);
            Console.WriteLine("Amount of money: " + Money);
            Console.WriteLine("Email: " + Email);
            //Console.WriteLine("Your password: " + Password);

            Console.WriteLine("Your discount: " + Discount);
            Console.WriteLine("Your delivery: " + Delivery);
            string s = "";
            if (String.Compare(Convert.ToString(this.GetType()), "Shop_Danylo.Users.User") == 0)
            {
                s = "Default";
            }
            else if (String.Compare(Convert.ToString(this.GetType()), "Shop_Danylo.Users.UserPremium") == 0)
            {
                s = "Premium";
            }
            else
            {
                s = "Premium+";
            }
            Console.WriteLine("Your type of Acc: " + s);
        }


        //Метод присвоєння імені
        public string SetName(string name)
        {
            if (name.Length <= 1)
            {
                Console.WriteLine("Введено некотректне iм'я. \n1. Спробувати ще раз.\n2. Присвоїти рандомне iм'я");
                int num = Convert.ToInt32(Console.ReadLine());
                if (num == 1)
                {
                    Console.Write("Введiть iм'я коректно: ");
                    name = Console.ReadLine();
                    SetName(name);
                }
                else
                {

                    name = RandName();
                }
            }
            return name;
        }

        //метод створення рандомного імені
        private string RandName()
        {
            string rName = "Name#" + numer;
            numer++;
            return rName;
        }

        public abstract void Buy(Product prod, int n);

        public void AddMoney(float amount)
        {
            money += amount;
            DBUsers1.UpdateMoney(Email, money);
            Console.WriteLine("\nВідбулося успішне поповнення на сумму = " + amount + "грн");
            Console.WriteLine("Поточний Баланс = " + Money + "грн");
        }

    }
}
