using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models {
    public class ProductOptions {

        public static List<ProductOption> GetProductOptions(Guid Id)
        {
            List<ProductOption> Items = new List<ProductOption>();
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();

                cmd.CommandText = $"select id from productoptions where Id '{Id.ToString().ToUpper().Trim()}'";

                var rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Items.ToList().Add(ProductOption.GetProductOption(Guid.Parse(rdr["Id"].ToString())));
                }
            }   
            return Items;
        }
    }
}
