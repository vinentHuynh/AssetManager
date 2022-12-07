using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.Classes
{
    public class UserModel
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string phone_number { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string password { get; set; }

        public int? role { get; set; }

        public bool LoginValidate(out string token)
        {
            bool verify = false;
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();

            string query = "SELECT password FROM dbo.[user] WHERE email = '" + email + "'";
            Microsoft.Data.SqlClient.SqlDataReader dr = connection.DataReader(query);
            dr.Read();
            for(int i = 0; i < dr.FieldCount; i++)
            {
                string check = dr.GetString(i);
                verify = BCrypt.Net.BCrypt.Verify(password, check);                        
            }
            connection.CloseConnection();
            
            if (verify)
                token = email;
            else
                token = null;
            return verify;
        }

    }
}
