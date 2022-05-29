using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Catalog.Entities;
using System.Linq;

namespace Catalog.Repositories
{
    public static class SQLItemsRepository
    {
        static readonly string server = "localhost";
        static readonly string database = "games_catalog";
        static readonly string uid = "root";
        static readonly string password = "#Luna2022!";
     
        static readonly string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

        // static MySqlConnection connection; 

        static MySqlConnection cnn = new MySqlConnection(connectionString);

        public static void OpenConnection()
        {
            try{
                cnn.Open();
                Console.WriteLine("Success");
                cnn.Close();

            }
            catch (Exception ex){
                 Console.WriteLine("Failure: " + ex.ToString());
            }

        }

        public static void SqlSafe(string Name, decimal Price)
        {
            string query = "INSERT INTO Stuff (ID, ItemName, Price, CreateDate) VALUES('" + Guid.NewGuid().ToString() + "','" + Name + "' ," + Price + ",'" + DateTimeOffset.UtcNow.ToString() + "')";

            // string query = "INSERT INTO Stuff (ID, ItemName, Price, CreateDate) VALUES('12', 'Michael', 1200, '12-13-14')";
            MySqlCommand command = new MySqlCommand(query, cnn);

            try
            {
                cnn.Open();
                command.ExecuteNonQuery();
                Console.WriteLine("Records Inserted Successfully");
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                cnn.Close();
            }

        }

        public static IEnumerable<Item> SqlRead()
        {
            cnn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * FROM stuff", cnn);

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Item> items = new List<Item>();

            while (reader.Read())
            {
                var item = new Item
                {
                    Id = Guid.Parse(reader["ID"].ToString()),
                    Name = reader["ItemName"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"]),
                    CreatedDate = Convert.ToDateTime(reader["CreateDate"]) 

                };

                items.Add(item);

            }

            cnn.Close();

            return items;

        }

        public static Item GetItem(Guid id)
        {
            var items = SqlRead();
            return items.Where(items => items.Id == id).SingleOrDefault();
        }

    }

}