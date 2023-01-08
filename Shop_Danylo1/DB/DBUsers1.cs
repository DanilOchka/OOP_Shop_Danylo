using System;
using System.Collections.Generic;
using System.Text;
using Shop_Danylo.Users;
using Shop_Danylo.Product_;

using System.IO;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Shop_Danylo.DB
{

    public static class DBUsers1
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString;
        private static SqlConnection sqlConnection = null;

        private static SqlCommand sqlCommand = null;
        private static SqlDataReader sqlDataReader = null;

        //метод додавання користувача до бази даних
        public static void Add(User us) {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            
            var command = "insert into Users (UserName, Email, HashPaswrs, Money, TypeOfAcc) values ('" + us.UserName + "', '" + us.Email + "', '" + us.Password + "', '" + us.Money + "', '" + us.GetType() + "')";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"> Add {sqlCommand.ExecuteNonQuery()} strings. Пользователь добавлен");


            sqlConnection.Close();
        }

        //метод отримання повної інормації про користувача з бази даних
        public static List<String> GetInfo(string mail)
        {
            List<String> Info = new List<String>();
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "select * from [Users] where Email='" + mail + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); // В бд делается выборка. Возвращается двумерный массив 
            string uName = string.Empty;
            string em = string.Empty;
            string psw = string.Empty;
            string money = string.Empty;
            string type = string.Empty;
           
            while (sqlDataReader.Read())
            {
                //Console.WriteLine($"{sqlDataReader["Username"]} {sqlDataReader["Email"]} {sqlDataReader["HashPaswrs"]} {sqlDataReader["Money"]} {sqlDataReader["TypeOfAcc"]}");

                uName = Convert.ToString(sqlDataReader["Username"]);
                em = Convert.ToString(sqlDataReader["Email"]);
                psw = Convert.ToString(sqlDataReader["HashPaswrs"]);
                money = Convert.ToString(sqlDataReader["Money"]);
                type = Convert.ToString(sqlDataReader["TypeOfAcc"]);


            }
            Info.Add(uName);
            Info.Add(em);
            Info.Add(psw);
            Info.Add(money);
            Info.Add(type);


            /*Close Data Reader*/
            if (sqlDataReader != null)
                sqlDataReader.Close();

            return Info;
        }

        //метод видалення акаунту користувача
        public static void DeleteUser(string mail)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "delete from Users where Email= '" + mail + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int x = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"Delete {x} strings");

            sqlConnection.Close();
        }


       //метод оновлення кількості грошей в базі даних
        public static void UpdateMoney(string mail, float x)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            var command = "update Users set Money='" + Convert.ToString(x) + "' where Email='" + mail + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);
            int xx = sqlCommand.ExecuteNonQuery();
            //Console.WriteLine($"Update {sqlCommand.ExecuteNonQuery()} strings");

            sqlConnection.Close();
        }

        //метод перевірки наявності емейлу в базі даних
        public static bool CheckEmail(string mail)
        {

            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            /*Close Data Reader*/
            if (sqlDataReader != null)
                sqlDataReader.Close();

            var command = "select count(*) as count from Users where Email='" + mail + "'";
            sqlCommand = new SqlCommand(command, sqlConnection);

            sqlDataReader = sqlCommand.ExecuteReader(); // В бд делается выборка
            string test = string.Empty;

            while (sqlDataReader.Read())
            {
                test = Convert.ToString(sqlDataReader["count"]);
                if (test != "0")
                {
      
                    sqlConnection.Close();
                    return false;
                }

            }
            sqlConnection.Close();

            return true;
        }
    }
}
