using FastMember;
using Microsoft.Data.SqlClient;

namespace AssetManager.Model;

public class UserPartialService
{
    public static List<UserPartial> GetAllUsers()
    {
        // get all users
        ConnectionDB connectionDB = new ConnectionDB();
        connectionDB.OpenConnection();

        string query = "SELECT id, first_name, last_name FROM [user]";
        SqlDataReader dr = connectionDB.DataReader(query);

        // map users to object list
        List<UserPartial> users = new List<UserPartial>();
        while (dr.Read())
        {
            users.Add(MapRowToUser<UserPartial>(dr));
        }

        return users;
    }

    public static UserPartial MapRowToUser<UserPartial>(SqlDataReader dr) where UserPartial : class, new()
    {
        Type type = typeof(UserPartial);
        var accessor = TypeAccessor.Create(type);
        var members = accessor.GetMembers();
        var user = new UserPartial();

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

