using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.DB;

using System.Text.RegularExpressions;

namespace Shop_Danylo.Product_
{
    public class Product
    {
        private float cost;
        public float Cost
        {
            get { return cost; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Cost), "Не коректно задана вартiсть");
                    //Console.WriteLine("Не коректно задана вартість");
                }
                else
                {
                    cost = value;
                }
            }
        }

        public string Name { get; set; }

        public string Type { get; set; } //тип продукту (їжа, електроніка...)

        private string kode;
        public string Kode
        { // Код продукту
            get { return kode; }
            set
            {


                kode = value;
            }
        }

        private int amount;
        public int Amount
        {
            get { return amount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Amount), "Не коректно задана кiлькiсть");
                    //Console.WriteLine("Не коректно задана вартість");
                }
                else
                {
                    amount = value;
                }
            }
        }



        public Product(string name, string kode, float cost, string type, int am)
        {
            Name = name;
            Kode = kode;
            Cost = cost;
            Type = type;
            Amount = am;
            DBProducts.Add(this);
        }
        public Product(string name, string kode, float cost, string type, int am, string a)
        {
            Name = name;
            Kode = kode;
            Cost = cost;
            Type = type;
            Amount = am;
        }

    }
}
