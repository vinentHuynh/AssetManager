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
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        public bool LoginValidate(out string token)
        {
            if (Name.Equals("admin") && Password.Equals("password"))
            {
                token = "1";
                return true;
            }
            else
            {
                token = null;
                return false;
            }

        }

    }
}
