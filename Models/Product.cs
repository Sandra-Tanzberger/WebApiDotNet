using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace RefactorThis.Models {
    public class Product
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public static Product GetProduct(Guid Id)
        {
            Product product; 
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from Products where Id = '{Id.ToString().ToUpper().Trim()}' collate nocase";

                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                {
                    return null;
                }

                product = new Product
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString(),
                    Price = decimal.Parse(rdr["Price"].ToString()),
                    DeliveryPrice = decimal.Parse(rdr["DeliveryPrice"].ToString())
                };
            }
            return product;
        }

        public static void CreateProduct(Product product)
        {
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"insert into Products (id, name, description, price, deliveryprice) values ('{Guid.NewGuid()}', '{product.Name.Trim()}', '{product.Description.Trim()}', '{product.Price}', '{product.DeliveryPrice}')";
                cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateProduct(Product product)
        {
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"update Products set name = '{product.Name.Trim()}', description = '{product.Description.Trim()}', price = '{product.Price}', deliveryprice = '{product.DeliveryPrice}' where id = '{product.Id.ToString().ToUpper().Trim()}' collate nocase";
                cmd.ExecuteNonQuery();
            }
        }

        public static void DeleteProduct(Guid Id)
        {
            foreach (var option in ProductOptions.GetProductOptions(Id))
            {
                ProductOption.DeleteProductOption((Guid)option.Id);
            }

            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            { 
                conn.Open();
                var cmd = conn.CreateCommand();

                //alternate way of deleting productoptions table dependent records instead of iterating through productOptions list
                //cmd.CommandText = $"delete from ProductOptions where ProductId = '{Id}' collate nocase";
                //cmd.ExecuteNonQuery();

                cmd.CommandText = $"delete from Products where id = '{Id.ToString().ToUpper().Trim()}' collate nocase";
                cmd.ExecuteNonQuery();
            }           

        }
    }
}
