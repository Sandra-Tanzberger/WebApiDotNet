using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models
{
    public class Products { 
         public static List<Product> GetProducts(string name)
        {
            List<Product> Items = new List<Product>();
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select Id from Products where Name = '{name}'";

                string text = cmd.CommandText;

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Items.Add(Product.GetProduct(Guid.Parse(rdr["Id"].ToString())));
                }
            }    
            return Items;
        }

        public static List<Product> GetProducts(Guid Id)
        {

            List<Product> Items = new List<Product>();
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select id from Products where Id = '{Id.ToString().ToUpper().Trim()}'";

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Items.Add(Product.GetProduct(Guid.Parse(rdr["Id"].ToString())));
                }
            }
            return Items;
        }
    }
}