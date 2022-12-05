using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace AssetManager.Classes
{
    public class Cryptography
    {
        public string hashPassword(string ptPassword)
        {
            string hashed = BCrypt.Net.BCrypt.HashPassword(ptPassword);
            return hashed;
        }

        public bool verifyPassword(string password, string email)
        {
            string storedPassword = "";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            string query = "SELECT password FROM dbo.[user] WHERE email ='" + email + "'";
            Microsoft.Data.SqlClient.SqlDataReader dr = connection.DataReader(query);
            while (dr.Read())
            {
                storedPassword = dr.GetString(0);
            }
            connection.CloseConnection();

            bool verify = BCrypt.Net.BCrypt.Verify(password, storedPassword);
            return verify;

        }


    }
}
