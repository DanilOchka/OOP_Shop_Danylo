using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Shop_Danylo.Users;
using Shop_Danylo.DB;
using Shop_Danylo.Product_;

namespace Shop_Danylo
{
    public static class Shopping
    {

        public static void ChooseProd(User us) {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Оберіть товар за кодом, який хоете придбати\n  Ваш віибір:");
            string kode = (Console.ReadLine());
            if (DBProducts.CheckKode(kode))
            {
                BuyProd(kode, us);
            }
            else {
                Console.WriteLine("  !!! Такий товар відсутній, або КОД було задана некоректно !!!");
            }
            
        }


        public static void ChooseList(User us)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("Оберіть список товарів\n");

            Console.OutputEncoding = Encoding.UTF8;
            Console.Write("  1 - загальний список;\n  2 - список за категоріями\n  Ваш віибір:");
            string nn = (Console.ReadLine());
            if (String.Compare(nn, "1") == 0)
            {

                DBProducts.GetProductList();
                
                ChooseProd(us);
                
            }
            else if (String.Compare(nn, "2") == 0)
            {
                int k = 0;
                Console.OutputEncoding = Encoding.UTF8;
                Console.Write("\nІснуючі категорії товарів\n  1 - food;\n  2 - electronics;\n  3 - chancellery;\n  4 - car;\n  5 - household chemicals;\n  6 - cosmetic;\n  Ваш віибір:");
                string nn2 = (Console.ReadLine());
                if (String.Compare(nn2, "1") == 0)
                {
                    k = DBProducts.GetProductListType("food");
                }
                else if (String.Compare(nn2, "2") == 0)
                {
                    k = DBProducts.GetProductListType("electr");
                }
                else if (String.Compare(nn2, "3") == 0)
                {
                    k = DBProducts.GetProductListType("chanc");
                }
                else if (String.Compare(nn2, "4") == 0)
                {
                    k =DBProducts.GetProductListType("car");
                }
                else if (String.Compare(nn2, "5") == 0)
                {
                    k=DBProducts.GetProductListType("chemic");
                }
                else if (String.Compare(nn2, "6") == 0)
                {
                    k=DBProducts.GetProductListType("cosm");
                }
                else
                {
                    Console.WriteLine("\nВідсутній товар за обраною категорією");
                }


                if (k == 0)
                {
                    Console.OutputEncoding = Encoding.UTF8;
                    Console.WriteLine("\nТака категорія відстіня");
                }
                else {
                    ChooseProd(us);
                }
            }
            else
            {
                Console.WriteLine("\nВідсутній товар за обраною категорією");
            }
            


        }

        public static void BuyProd(string kode, User us)
        {
            List<string> InfoProd = DBProducts.GetInfo(kode);

            Product b = new Product(InfoProd[0], InfoProd[1], (float)Convert.ToDouble(InfoProd[2]), InfoProd[3], Convert.ToInt32(InfoProd[4]), "+");
            Console.WriteLine("Ви обрали товар «"+b.Name+ "»");

            Boolean bbb = true;
            while (bbb)
            {
                Console.Write("Введіть кількість штук обраного продукту: ");
                int n = Convert.ToInt32(Console.ReadLine());
                if (n <= b.Amount)
                {
                    us.Buy(b, n);
                    Console.WriteLine("Ви придбали " + n + "шт. продукту «" + b.Name + "» на суму " + b.Cost * n + "грн + вартiсть доставки " + us.Delivery + "грн   | Ваша знижка  = "+n*b.Cost / 100 * us.Discount+"грн");
                    Console.WriteLine("З вашого рахунку списано = " + (b.Cost * n - n * b.Cost / 100 * us.Discount + us.Delivery) + "грн");
                    Console.WriteLine("Залишок на балансi = " + us.Money + "грн");
                    if (b.Amount <= 0)
                    {
                        DBProducts.DeleteProduct(kode);
                    }
                    bbb = false;
                }
                else
                {
                    Console.Write("\nТакої кількості товару немає в наявності, оберіть меншу кількість, або дочекайтеся нових поставок.\n  1 - задати меншу кількість товару\n  2 - вийти в меню\n  Ваш вибiр: ");
                    string nn = (Console.ReadLine());
                    if (String.Compare(nn, "1")==0)
                    {
                        bbb = true;
                    }
                    else if (String.Compare(nn, "2") == 0)
                    {
                        bbb = false;
                    }
                    else
                    {
                        Console.WriteLine("\nВибрана некоректна опцiя. Вілбувся вихід в МЕНЮ");
                        bbb = false;
                    }
                }
            }        
        }
    }
}
