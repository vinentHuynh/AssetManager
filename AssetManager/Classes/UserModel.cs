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
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public int Role { get; set; }

        public bool LoginValidate(out string token)
        {
            bool verify = false;
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();

            string query = "SELECT password FROM dbo.[user] WHERE email = '" + Username + "'";
            Microsoft.Data.SqlClient.SqlDataReader dr = connection.DataReader(query);
            dr.Read();
            for(int i = 0; i < dr.FieldCount; i++)
            {
                string check = dr.GetString(i);
                if (check == Password)                
                    verify = true;                         
            }
            connection.CloseConnection();

            //TO-DO: generate unique tokens per user
            if (verify)
                token = "1";
            else
                token = null;
            return verify;
        }

    }
}
