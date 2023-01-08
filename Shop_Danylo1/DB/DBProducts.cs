using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Shop_Danylo.Users;
using Shop_Danylo.Product_;
using System.Data.SqlClient;
using System.Configuration;

namespace Shop_Danylo.DB
{
    public static class DBProducts
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        private static SqlConnection sqlConnection = null;

        private static SqlCommand sqlCommand = null;
        private static SqlDataReader sqlDataReader = null;

        //метод додавання подукту до бази даних
        public static void Add(Product prod)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "insert into  (Name, Kode, Cost, Type, Amount) values ('" + prod.Name + "', '" + prod.Kode + "', '" + prod.Cost + "', '" + prod.Type + "', '" + prod.Amount + "')";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"> Add {sqlCommand.ExecuteNonQuery()} strings. Пользователь добавлен");


            sqlConnection.Close();
        }


        //метод оновлення кількості товару в базі даних
        public static void UpdateAmount(Product prod, int x)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "update Products set Amount='" + Convert.ToString(x) + "' where Kode='" + prod.Kode + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int xx = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"Update {sqlCommand.ExecuteNonQuery()} strings");

            sqlConnection.Close();
        }


        //вивід загального листа продукту
        public static void GetProductList()
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "select * from [Products]";
            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); //в бд робиться вибірка та повертається двовимірний масив
            Console.WriteLine("\n\tЗагальний лист продуктiв");
            Console.WriteLine(" ________________________________________________________________________________");
            Console.WriteLine(" |\tName\t |\tKode\t |\tCost\t |\tType\t |     Amount    |");
            while (sqlDataReader.Read())
            {
                Console.WriteLine(" |-------------------------------------------------------------------------------|");
                Console.WriteLine($" |\t{sqlDataReader["Name"]}\t |\t{ sqlDataReader["Kode"]}\t |\t{ sqlDataReader["Cost"]}\t |\t{ sqlDataReader["Type"]}\t |\t{ sqlDataReader["Amount"]}\t |");
                //Console.WriteLine($"{sqlDataReader["Name"]} {sqlDataReader["Kode"]} {sqlDataReader["Cost"]} {sqlDataReader["Type"]} {sqlDataReader["Amount"]}");

            }
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(" ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");

            //Close Data Reader
            if (sqlDataReader != null)
                sqlDataReader.Close();
        }

        public static int GetProductListType(string type)
        {
            int n = 0;
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();


            var command = "select * from [Products] where Type='" + type + "'";

            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); //в бд робиться вибірка та повертається двовимірний масив
            Console.WriteLine("\n\tОбрана категорiя = "+type);
            Console.WriteLine(" ________________________________________________________________________________");
            Console.WriteLine(" |\tName\t |\tKode\t |\tCost\t |\tType\t |     Amount    |");
            while (sqlDataReader.Read())
            {
                Console.WriteLine(" |-------------------------------------------------------------------------------|");
                Console.WriteLine($" |\t{sqlDataReader["Name"]}\t |\t{ sqlDataReader["Kode"]}\t |\t{ sqlDataReader["Cost"]}\t |\t{ sqlDataReader["Type"]}\t |\t{ sqlDataReader["Amount"]}\t |");
                //Console.WriteLine($"{sqlDataReader["Name"]} {sqlDataReader["Kode"]} {sqlDataReader["Cost"]} {sqlDataReader["Type"]} {sqlDataReader["Amount"]}");
                n++;
            }
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(" ‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");

            //Close Data Reader
            if (sqlDataReader != null)
                sqlDataReader.Close();

            return n;
        }

        //метод видалення продукту
        public static void DeleteProduct(string kode)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "delete from Products where Kode= '" + kode + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"Delete {x} strings");

            sqlConnection.Close();
        }



        public static List<String> GetInfo(string kode)
        {
            List<String> Info = new List<String>();
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "select * from [Products] where Kode='" + kode + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); // В бд делается выборка. Возвращается двумерный массив 
            string name = string.Empty;
            string kod = string.Empty;
            string cost = string.Empty;
            string type = string.Empty;
            string am = string.Empty;

            while (sqlDataReader.Read())
            {
                name = Convert.ToString(sqlDataReader["Name"]);
                kod = Convert.ToString(sqlDataReader["Kode"]);
                cost = Convert.ToString(sqlDataReader["Cost"]);
                type = Convert.ToString(sqlDataReader["Type"]);
                am = Convert.ToString(sqlDataReader["Amount"]);



            }
            Info.Add(name);
            Info.Add(kod);
            Info.Add(cost);
            Info.Add(type);
            Info.Add(am);


            /*Close Data Reader*/
            if (sqlDataReader != null)
                sqlDataReader.Close();

            return Info;
        }

        public static bool CheckKode(string kode)
        {

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            /*Close Data Reader*/
            if (sqlDataReader != null)
                sqlDataReader.Close();

            var command = "select count(*) as count from Products where Kode='" + kode + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); // В бд делается выборка
            string test = string.Empty;

            while (sqlDataReader.Read())
            {
                test = Convert.ToString(sqlDataReader["count"]);
                if (test != "0")
                {

                    sqlConnection.Close();
                    return true;
                }

            }
            sqlConnection.Close();

            return false;
        }
    }
}
