using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.DB;

using Shop_Danylo.Users;

namespace Shop_Danylo
{
    public static class SignIn_LogIn
    {
        //клас методів входу та регестрації
        public static void Start()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Ласково посимо до нашого iнтерент магазину!!!\n1. Зареєструватись\n2. Увiйти\nesc - завершення програми\n   Ваш вибір: ");
            string action = Console.ReadLine();
            if (String.Compare(action, "1") == 0)
            {
                LogIn();
            }
            else if (String.Compare(action, "2") == 0)
            {
                SignIn();
            }
            else if (String.Compare(action, "esc") == 0) {
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("\n████████████████████████████████████████\n████████████████████████████████████████\n██████▀░░░░░░░░▀████████▀▀░░░░░░░▀██████\n████▀░░░░░░░░░░░░▀████▀░░░░░░░░░░░░▀████\n██▀░░░░░░░░░░░░░░░░▀▀░░░░░░░░░░░░░░░░▀██\n██░░░░░░░░░░░░░░░░░░░▄▄░░░░░░░░░░░░░░░██\n██░░░░░░░░░░░░░░░░░░█░█░░░░░░░░░░░░░░░██\n██░░░░░░░░░░░░░░░░░▄▀░█░░░░░░░░░░░░░░░██\n██░░░░░░░░░░████▄▄▄▀░░▀▀▀▀▄░░░░░░░░░░░██\n██▄░░░░░░░░░████░░░░░░░░░░█░░░░░░░░░░▄██\n████▄░░░░░░░████░░░░░░░░░░█░░░░░░░░▄████\n██████▄░░░░░████▄▄▄░░░░░░░█░░░░░░▄██████\n████████▄░░░▀▀▀▀░░░▀▀▀▀▀▀▀░░░░░▄████████\n██████████▄░░░░░░░░░░░░░░░░░░▄██████████\n████████████▄░░░░░░░░░░░░░░▄████████████\n██████████████▄░░░░░░░░░░▄██████████████\n████████████████▄░░░░░░▄████████████████\n██████████████████▄▄▄▄██████████████████\n████████████████████████████████████████\n████████████████████████████████████████");
                Console.WriteLine("\nЗавершення програми...");
            }
            else
            {
                Console.WriteLine("Обрана некоректна опцiя");
            }
        }

        public static void LogIn()
        {
            Console.WriteLine("\n===[Створення акаунту]===");
            Console.Write("Введiть вашe iм'я: ");
            string name = Console.ReadLine();
            Console.Write("Введiть ваш Email: ");
            string email = Console.ReadLine();
            if (DBUsers1.CheckEmail(email))
            {
                Console.Write("Введiть ваш пароль: ");
                string password = Console.ReadLine();
                User us1 = new User(name, email, password);
                Console.Write("Ваш акаунт успiшно створено\n\n");
                Start();
            }
            else {
                Console.WriteLine("\n!!! За цiєю поштовою адресою вже зареєстровано користувача !!!");
                Console.WriteLine("Стпробуйте іншу поштову адресу для створення нового акаунту, або увійдіть у вже існуючий\n");
                Start();
            }
            

        }

        public static void SignIn()
        {
            Console.WriteLine("\n===[Вхід до акаунту]===");
            Console.Write("Введiть ваш Email: ");
            string email = Console.ReadLine();
            Console.Write("Введiть ваш пароль: ");
            string password = Console.ReadLine();
            List<String> Info = DBUsers1.GetInfo(email);
            if (!DBUsers1.CheckEmail(email)) {
                if (String.Compare(Info[1], email) == 0 && String.Compare(Info[2], Hash.HashPasswrd(password)) == 0)
                {
                    User us;
                    if (String.Compare(Info[4], "Shop_Danylo.Users.User") == 0)
                    {
                        us = new User(Info[0], email, Hash.HashPasswrd(password), (float)Convert.ToDouble(Info[3]));
                    }
                    else if (String.Compare(Info[4], "Shop_Danylo.Users.UserPremium") == 0)
                    {
                        us = new UserPremium(Info[0], email, Hash.HashPasswrd(password), (float)Convert.ToDouble(Info[3]));
                    }
                    else if (String.Compare(Info[4], "Shop_Danylo.Users.UserPremiumPlus") == 0)
                    {
                        us = new UserPremiumPlus(Info[0], email, Hash.HashPasswrd(password), (float)Convert.ToDouble(Info[3]));
                    }
                    else
                    {
                        us = null;
                    }

                    Console.Write("Ви успiшно авторизувались, Ласкаво просимо " + us.UserName + "\n\n");
                    
                    MenuS.Menu(us);
                }
                else
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.Write("\n!!! Не вдалося увійти в акаунт, перевірте правильність введених даних !!!\n\n");
                    Start();
                }
            }
            else {
                Console.Write("\n!!! Така пошта не була зареєстрована !!!\nСпробуйте авторизуватись повторно\n\n");
                Start();
            }
            
            
            
        }
    }
}
