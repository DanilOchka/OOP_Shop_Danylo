using System;
using System.Collections.Generic;
using System.Text;

using Shop_Danylo.Users;
using Shop_Danylo.DB;

namespace Shop_Danylo
{
    //класс методів головного меню магазину
    public static class  MenuS
    {
        public static void Menu(User user)
        {
            Boolean b = true;
            while (b)
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write("Оберіть опцію:\n1 - поповнити баланс;\n2 - переглянути інформацію про акаунт; \n3 - зробити покупку;\n4 - придбати ПРЕМІУМ статус акаунту;\n5 - історія покупок;\n6 - видалити акаунт;\n7 - вихід з акаунту; \nesc - завершення програми\n    Ваш вибір: ");
                string action = Console.ReadLine();

                if (String.Compare(action, "1") == 0)
                {
                    Console.Write("Введіть кількість поповнення: ");
                    int mon = Convert.ToInt32(Console.ReadLine());
                    user.AddMoney(mon);

                    Console.WriteLine("\n");
                }
                else if (String.Compare(action, "2") == 0)
                {
                    user.UserInformation();
                    Console.WriteLine("\n");
                }
                else if (String.Compare(action, "3") == 0)
                {
                    Console.WriteLine("\nЗробити покупку"); //тест меню покупок
                    Shopping.ChooseList(user);
                    
                    Console.WriteLine("\n");
                }
                else if (String.Compare(action, "4") == 0)
                {
                    Boolean bb = true;
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\nОберіть тип акаунту:\n  1) «Преміум» [100грн] - перевага: безкоштовна доставка.\n  2) «Преміум+» [200грн] - перевага: безкоштовна доставка + скидка 10% на всі товари.");
                    while (bb)
                    {
                        Console.Write("\n    Ваш вибір: ");
                        string action2 = Console.ReadLine();
                        if (String.Compare(action2, "1") == 0)
                        {
                            if (String.Compare(Convert.ToString(user.GetType()), "Shop_Danylo.Users.UserPremium") == 0)
                            {
                                Console.OutputEncoding = Encoding.UTF8;
                                Console.WriteLine("Ви вже є «Преміум» користувачем. Хочете підвищити статусдо:\n  2) «Преміум+» [200грн] - перевага: безкоштовна доставка + скидка 10% на всі товари.");
                            }
                            else if (String.Compare(Convert.ToString(user.GetType()), "Shop_Danylo.Users.UserPremiumPlus") == 0)
                            {
                                Console.OutputEncoding = Encoding.UTF8;
                                Console.WriteLine("Ви вже є «Преміум+» користувачем. Це максимельний статус\n");
                                bb = false;
                            }
                            else
                            {
                                Boolean bbb = true;
                                while (bbb)
                                {
                                    if (user.Money >= 100)
                                    {
                                        user = UpAccount.UpToPremium(user);
                                        DBHistoryOfShopping.Addprem(user, "Prem", 100, 100);
                                        bbb = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("На балнсi не достатньо коштiв. Бажаєте поповнити?\n1. Yes\n2. No\nВаш вибір: ");
                                        int num = Convert.ToInt32(Console.ReadLine());
                                        if (num == 1)
                                        {
                                            Console.WriteLine("Введіть кількість поповнення: ");
                                            int mon = Convert.ToInt32(Console.ReadLine());
                                            user.AddMoney(mon);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Покупку не виконано :(");
                                            bbb = false;
                                        }
                                    }
                                }
                                bb = false;
                            }                           
                            
                        }
                        else if (String.Compare(action2, "2") == 0)
                        {
                            if (String.Compare(Convert.ToString(user.GetType()), "Shop_Danylo.Users.UserPremiumPlus") == 0)
                            {
                                Console.OutputEncoding = Encoding.UTF8;
                                Console.WriteLine("Ви вже є «Преміум+» користувачем. Це максимельний статус");
                                bb = false;
                            }
                            else
                            {
                                Boolean bbb = true;
                                while (bbb)
                                {
                                    if (user.Money >= 200)
                                    {
                                        user = UpAccount.UpToPremiumPlus(user);
                                        DBHistoryOfShopping.Addprem(user, "Prem+", 200, 200);
                                        bbb = false;
                                    }
                                    else
                                    {
                                        Console.WriteLine("На балнсi не достатньо коштiв. Бажаєте поповнити?\n1. Yes\n2. No\nВаш вибір: ");
                                        int num = Convert.ToInt32(Console.ReadLine());
                                        if (num == 1)
                                        {
                                            Console.WriteLine("Введіть кількість поповнення: ");
                                            int mon = Convert.ToInt32(Console.ReadLine());
                                            user.AddMoney(mon);
                                        }
                                        else
                                        {
                                            Console.WriteLine("Покупку не виконано :(");
                                            bbb = false;
                                        }
                                    }
                                }
                                bb = false;
                            }


                            

                        }
                        else
                        {
                            Console.WriteLine("Некоректно обраний пункт меню, спробуйте ще раз");
                            Console.WriteLine("\n");
                        }
                    }
                }
                else if (String.Compare(action, "5") == 0)
                {
                    Console.WriteLine("\n\tIсторія покупок");
                    int k = DBHistoryOfShopping.GetHistory(user.Email);
                    if (k == 0) {
                        Console.WriteLine("\nу вас ще немає покупок");
                    }
                }

                else if (String.Compare(action, "6") == 0)
                {
                    DBUsers1.DeleteUser(user.Email);
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n!!! Ваш обліковий запис було видалено... !!!\n");
                    SignIn_LogIn.Start();
                }
                else if (String.Compare(action, "7") == 0)
                {
                    Console.WriteLine("\n");
                    SignIn_LogIn.Start();
                }
                else if (String.Compare(action, "esc") == 0)
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\n████████████████████████████████████████\n████████████████████████████████████████\n██████▀░░░░░░░░▀████████▀▀░░░░░░░▀██████\n████▀░░░░░░░░░░░░▀████▀░░░░░░░░░░░░▀████\n██▀░░░░░░░░░░░░░░░░▀▀░░░░░░░░░░░░░░░░▀██\n██░░░░░░░░░░░░░░░░░░░▄▄░░░░░░░░░░░░░░░██\n██░░░░░░░░░░░░░░░░░░█░█░░░░░░░░░░░░░░░██\n██░░░░░░░░░░░░░░░░░▄▀░█░░░░░░░░░░░░░░░██\n██░░░░░░░░░░████▄▄▄▀░░▀▀▀▀▄░░░░░░░░░░░██\n██▄░░░░░░░░░████░░░░░░░░░░█░░░░░░░░░░▄██\n████▄░░░░░░░████░░░░░░░░░░█░░░░░░░░▄████\n██████▄░░░░░████▄▄▄░░░░░░░█░░░░░░▄██████\n████████▄░░░▀▀▀▀░░░▀▀▀▀▀▀▀░░░░░▄████████\n██████████▄░░░░░░░░░░░░░░░░░░▄██████████\n████████████▄░░░░░░░░░░░░░░▄████████████\n██████████████▄░░░░░░░░░░▄██████████████\n████████████████▄░░░░░░▄████████████████\n██████████████████▄▄▄▄██████████████████\n████████████████████████████████████████\n████████████████████████████████████████");
                    Console.WriteLine("\nЗавершення програми...");
                    b = false;
                }
                else
                {
                    Console.WriteLine("Некоректно обраний пункт меню, спробуйте ще раз");
                    Console.WriteLine("\n");
                }
            }

        }
        
    }
}
