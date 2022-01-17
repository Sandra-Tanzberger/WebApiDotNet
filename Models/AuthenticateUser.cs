using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace RefactorThis.Models
{
    public class AuthenticateUser
    {
        public static bool IsAuth(string APIToken)
        {
            //todo decrypt token
            //Encryption.DecryptString();
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                bool isAuth = true;
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select * from Login where APIToken = '{APIToken}' collate nocase";

                var rdr = cmd.ExecuteReader();
                if (!rdr.Read())
                {
                    isAuth = false;
                }
                return isAuth;
            }
        }

        public static Guid GetAPIToken(string userName, string password)
        {
            Guid? APIToken = null;
            using (var conn = dbHelper.NewConnection<SqliteConnection>())
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"select APIToken from Login where name='{userName}' and password='{password}'";

                var rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    APIToken = Guid.Parse(rdr["APIToken"].ToString());
                }

                //todo encrypt token before returning
                //Encryption.EncryptString();
                return (Guid)APIToken;
            }
        }
    }
}