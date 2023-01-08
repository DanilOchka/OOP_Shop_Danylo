using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop_Danylo.Product_;
using Shop_Danylo.Users;
using System.Data.SqlClient;
using System.Configuration;

namespace Shop_Danylo.DB
{
    public static class DBHistoryOfShopping
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        private static SqlConnection sqlConnection = null;

        private static SqlCommand sqlCommand = null;
        private static SqlDataReader sqlDataReader = null;

        //метод додавання покупки до бази данних історії всіх покупок
       
        public static void Add(User us, Product prod, int amount, float mon)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "insert into HistoryOfShopping (UserName, Email, Prod, Kode, Type, Cost, Amount, Date, Money, Delivery, Discount) values ('" + us.UserName + "', '" + us.Email + "', '" + prod.Name + "', '" + prod.Kode + "', '" + prod.Type + "', '" + prod.Cost + "', '" + amount + "',  '" + DateTime.Now + "', '" + mon + "', '" + us.Delivery + "', '" + us.Discount + "')";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"> Add {sqlCommand.ExecuteNonQuery()} strings. Пользователь добавлен");


            sqlConnection.Close();
        }

        public static void Addprem(User us, string prem,  int cost, float mon)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "insert into HistoryOfShopping (UserName, Email, Prod, Kode, Type, Cost, Amount, Date, Money, Delivery, Discount) values ('" + us.UserName + "', '" + us.Email + "', '" + "-----" + "', '" + "-----" + "', '" + prem + "', '" + cost + "', '" + "----" + "',  '" + DateTime.Now + "', '" + mon + "', '" + "--" + "', '" + "--" + "')";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"> Add {sqlCommand.ExecuteNonQuery()} strings. Пользователь добавлен");


            sqlConnection.Close();
        }



        public static int GetHistory(string email)
        {
            int n = 0;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();


            var command = "select * from [HistoryOfShopping] where Email='" + email + "'";

            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); //в бд робиться вибірка та повертається двовимірний масив
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(" ________________________________________________________________________________________________________________________________________________");
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(" |\tProd\t |\tKode\t |\tType\t |\tCost\t |     Amount    |\t  Date         |\tMoney\t| Delivery | Discount  |");
            while (sqlDataReader.Read())
            {
                Console.WriteLine(" |---------------------------------------------------------------------------------------------------------------------------------------------|");
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine($" |\t{sqlDataReader["Prod"]}\t |\t{ sqlDataReader["Kode"]}\t |\t{ sqlDataReader["Type"]}\t |\t{ sqlDataReader["Cost"]}\t |\t{ sqlDataReader["Amount"]}\t | { sqlDataReader["Date"]} |\t{ sqlDataReader["Money"]}\t|    { sqlDataReader["Delivery"]}    |      { sqlDataReader["Discount"]}   |");
                 //Console.WriteLine($"{sqlDataReader["Name"]} {sqlDataReader["Kode"]} {sqlDataReader["Cost"]} {sqlDataReader["Type"]} {sqlDataReader["Amount"]}");
                 n++;
            }
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(" ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");

            //Close Data Reader
            if (sqlDataReader != null)
                sqlDataReader.Close();

            return n;
        }
    }
}
 