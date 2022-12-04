using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Classes
{
    public class UserService
    {
        public static Task<UserModel> GetUserByEmail(string email)
        {
            ConnectionDB connectionDB = new ConnectionDB();
            connectionDB.OpenConnection();

            string query = "SELECT id, first_name, last_name, phone_number, email, password, role " +
                "FROM [user] WHERE email = '" + email + "'";
            SqlDataReader dr = connectionDB.DataReader(query);
            dr.Read();

            return Task.FromResult(MapRowToUser<UserModel>(dr));
        }

        public static UserModel MapRowToUser<UserModel>(SqlDataReader dr) where UserModel : class, new()
        {
            Type type = typeof(UserModel);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var user = new UserModel();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                string fieldName = dr.GetName(i);

                if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                {
                    if (!dr.IsDBNull(i))
                    {
                        accessor[user, fieldName] = dr.GetValue(i);
                    }
                }
            }

            return user;
        }
    }
}
