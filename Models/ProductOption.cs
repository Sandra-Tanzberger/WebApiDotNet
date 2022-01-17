using System;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models {
    public class ProductOption
    {
        public Guid? Id { get; set; }

        public Guid? ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public static ProductOption GetProductOption(Guid id)
        {
            ProductOption productOption;
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                //um, no, it's string interpolation ;) using a string literal
                cmd.CommandText = $"select * from productoptions where id = '{id.ToString().ToUpper().Trim()}' collate nocase";

                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                {
                    return null;
                }

                productOption = new ProductOption
                {
                    Id = Guid.Parse(rdr["Id"].ToString()),
                    ProductId = Guid.Parse(rdr["ProductId"].ToString()),
                    Name = rdr["Name"].ToString(),
                    Description = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString()
                };
            }
            return productOption;
        }
        
        public static void CreateProductOption(ProductOption productOption)
        {
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            { 
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"insert into productoptions (id, productid, name, description) values ('{(Guid.NewGuid())}', '{productOption.ProductId}', '{productOption.Name.Trim()}', '{productOption.Description.Trim()}')";
                cmd.ExecuteNonQuery();
            }
        }
        //not yet in use according to ReadMe requirements
        /*
        public static void UpdateProductOptions(Guid Id)
        {
            using(var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"update productoptions set name = '{Name}', description = '{Description}' where id = '{Id.ToString().ToUpper().Trim()}' collate nocase";
                cmd.ExecuteNonQuery();
            }
        }
        */
        public static void DeleteProductOption(Guid Id)
        {
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"delete from ProductOptions where id = '{Id.ToString().ToUpper().Trim()}' collate nocase";
                cmd.ExecuteReader();
            }
        }
    }
}
